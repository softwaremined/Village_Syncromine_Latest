CREATE TABLE [dbo].[HIERARCH](
	[HierarchicalID] [numeric](7, 0) NOT NULL,
	[Description] [varchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[HierarchicalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_ACTIVITY](
	[Activity] [numeric](7, 0) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_CODE_ACTIVITY] PRIMARY KEY CLUSTERED 
(
	[Activity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPACTIVITY](
	[Activity] [numeric](7, 0) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_CODE_WPACTIVITY] PRIMARY KEY CLUSTERED 
(
	[Activity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[SECTION](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](20) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ReportToSectionid] [varchar](100) NOT NULL,
	[Hierarchicalid] [numeric](7, 0) NULL
 CONSTRAINT [PK_SECTION] PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[SectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SECTION]  WITH CHECK ADD  CONSTRAINT [FK_SECTION_HIERARCH] FOREIGN KEY([Hierarchicalid])
REFERENCES [dbo].[HIERARCH] ([HierarchicalID])
GO

ALTER TABLE [dbo].[SECTION] CHECK CONSTRAINT [FK_SECTION_HIERARCH]
GO

CREATE TABLE [dbo].[SECTION_COMPLETE](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](20) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Sectionid_1] [varchar](20) NOT NULL,
	[Name_1] [varchar](50) NOT NULL,
	[Sectionid_2] [varchar](20) NOT NULL,
	[Name_2] [varchar](50) NOT NULL,
	[Sectionid_3] [varchar](20) NOT NULL,
	[Name_3] [varchar](50) NOT NULL,
	[Sectionid_4] [varchar](20) NOT NULL,
	[Name_4] [varchar](50) NOT NULL,
	[Sectionid_5] [varchar](20) NOT NULL,
	[Name_5] [varchar](50) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[PRODMONTH] ASC,
	[SECTIONID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[STOPINGPROCESSCODES](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](10) NULL,
 CONSTRAINT [PK_StopingProcessCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[ENDTYPE](
	[EndTypeID] [numeric](7, 0) NOT NULL,
	[Description] [varchar](30) NULL,
	[EndHeight] [numeric](13, 1) NULL,
	[EndWidth] [numeric](13, 1) NULL,
	[ProcessCode] [varchar](10) NULL,
	[ReefWaste] [varchar](2) NULL,
	[Rate] [numeric](10, 2) NULL,
	[DetRatio] [numeric](18, 2) NULL,
 CONSTRAINT [PK_ENDTYPE] PRIMARY KEY CLUSTERED 
(
	[EndTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_OREFLOW](
	[OreFlowCode] [varchar](10) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_CODE_OREFLOW] PRIMARY KEY CLUSTERED 
(
	[OreFlowCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[OREFLOWENTITIES](
	[OreFlowID] [varchar](10) NOT NULL,
	[OreFlowCode] [varchar](10) NOT NULL,
	[Name] [varchar](50) NULL,
	[ParentOreFlowID] [varchar](10) NULL,
	[LevelNumber] [varchar](20) NULL,
	[Capacity] [varchar](50) NULL,
	[Cur_Tons] [numeric](18, 0) NULL,
	[Cur_Grade] [numeric](18, 0) NULL,
	[X1] [numeric](18, 0) NULL,
	[Y1] [numeric](18, 0) NULL,
	[X2] [numeric](18, 0) NULL,
	[Y2] [numeric](18, 0) NULL,
	[SX] [numeric](18, 0) NULL,
	[SY] [numeric](18, 0) NULL,
	[Inactive] [char](5) NULL,
	[TFBox] [char](5) NULL,
	[SectionID] [varchar](20) NULL,
	[HopperFactor] [numeric](18, 1) NULL,
	[Surface] [char](10) NULL,
	[Mine] [char](10) NULL,
	[ReefType] [varchar](50) NULL,
	[CrossTram] [char](1) NULL,
	[BoxDistance] [numeric](18, 1) NULL,
	[SkipFactor] [numeric](13, 3) NULL,
	[Ug2SkipFactor] [numeric](5, 1) NULL,
	[WasteSkipFactor] [numeric](5, 1) NULL,
	[Division] [varchar](2) NULL,
	[CostArea] [varchar](1) NULL,
 CONSTRAINT [PK_OREFLOWENTITIES] PRIMARY KEY CLUSTERED 
(
	[OreFlowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OREFLOWENTITIES]  WITH NOCHECK ADD  CONSTRAINT [FK_OREFLOWENTITIES_CODE_OREFLOW] FOREIGN KEY([OreFlowCode])
REFERENCES [dbo].[CODE_OREFLOW] ([OreFlowCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OREFLOWENTITIES] CHECK CONSTRAINT [FK_OREFLOWENTITIES_CODE_OREFLOW]
GO
CREATE TABLE [dbo].[COMMONAREAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CommonArea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[COMMONAREASUBSECTIONS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommonArea] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CommonAreaSubSections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CommonAreaSubSections]  WITH CHECK ADD  CONSTRAINT [FK_CommonAreaSubSections_CommonAreas] FOREIGN KEY([CommonArea])
REFERENCES [dbo].[CommonAreas] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CommonAreaSubSections] CHECK CONSTRAINT [FK_CommonAreaSubSections_CommonAreas]
GO

CREATE TABLE [dbo].[CODE_WPCLASSIFICATION](
	[CLASSIFICATION] [char](1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CODE_WPCLASSIFICTAION] PRIMARY KEY CLUSTERED 
(
	[CLASSIFICATION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_GRID](
	[Grid] [varchar](30) NOT NULL,
	[Description] [varchar](30) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[CODE_GRID] ADD [Division] [varchar](2) NULL
ALTER TABLE [dbo].[CODE_GRID] ADD [CostArea] [varchar](1) NULL
 CONSTRAINT [PK_CODE_GRID] PRIMARY KEY CLUSTERED 
(
	[Grid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE TABLE [dbo].[CODE_DIRECTION](
	[Direction] [varchar](8) NOT NULL,
	[Description] [varchar](30) NULL,
 CONSTRAINT [PK_CODE_DIRECTION] PRIMARY KEY CLUSTERED 
(
	[Direction] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPDESCRIPTION](
	[DescrCode] [varchar](10) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[selected] [char](1) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[CODE_WPDESCRIPTION] ADD [Inactive] [varchar](1) NULL

GO
CREATE TABLE [dbo].[CODE_WPDESCRIPTIONNO](
	[DescrNumberCode] [varchar](20) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[selected] [char](1) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[CODE_WPDESCRIPTIONNO] ADD [Inactive] [varchar](1) NULL

GO
CREATE TABLE [dbo].[CODE_WPDETAIL](
	[DetailCode] [varchar](10) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[selected] [char](1) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[CODE_WPDETAIL] ADD [Inactive] [varchar](1) NULL

GO
CREATE TABLE [dbo].[CODE_WPDIVISION](
	[DivisionCode] [varchar](2) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[selected] [char](1) NULL,
	[Density] [float] NULL,
	[Editable] [int] NULL CONSTRAINT [DF_CODE_WPDIVISION_Editable]  DEFAULT ((0)),
	[WPLastUsed] [numeric](8, 0) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPNUMBER](
	[NumberCode] [varchar](50) NULL,
	[Description] [varchar](50) NOT NULL,
	[selected] [char](1) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[CODE_WPNUMBER] ADD [Inactive] [varchar](1) NULL

GO
CREATE TABLE [dbo].[CODE_WPSTATUS](
	[WPStatus] [char](1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CODE_WPSTATUS] PRIMARY KEY CLUSTERED 
(
	[WPStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPTYPE](
	[TypeCode] [varchar](3) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[selected] [char](1) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[CODE_WPTYPE] ADD [Inactive] [varchar](1) NULL
ALTER TABLE [dbo].[CODE_WPTYPE] ADD [Classification] [char](1) NULL

GO
CREATE TABLE [dbo].[CODE_WPTYPECLASSIFYCHANGE](
	[TypeCode] [varchar](3) NOT NULL,
	[DateChange] [datetime] NOT NULL,
	[Classification] [varchar](1) NULL,
	[UserID] [varchar](20) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPTYPEGRIDLINK](
	[Division] [varchar](2) NOT NULL,
	[Grid] [varchar](30) NOT NULL,
	[TypeCode] [varchar](3) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPTYPELEVELLINK](
	[Division] [varchar](2) NOT NULL,
	[OreflowID] [varchar](10) NOT NULL,
	[TypeCode] [varchar](3) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPTYPEWPDESCLINK](
	[TypeCode] [varchar](3) NOT NULL,
	[DescrCode] [varchar](10) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPTYPEWPDESCNOLINK](
	[TypeCode] [varchar](3) NOT NULL,
	[DescrNumberCode] [varchar](20) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPTYPEWPDETAILLINK](
	[TypeCode] [varchar](3) NOT NULL,
	[DetailCode] [varchar](10) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPTYPEWPDIRECTIONLINK](
	[Direction] [varchar](8) NOT NULL,
	[TypeCode] [varchar](3) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_WPTYPEWPNUMBERLINK](
	[TypeCode] [varchar](3) NOT NULL,
	[NumberCode] [varchar](50) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[REEF](
	[ReefID] [numeric](7, 0) NOT NULL,
	[Description] [varchar](50) NULL,
	[DeterminantCW] [numeric](18, 0) NULL,
	[MinTheoreticalSW] [numeric](18, 0) NULL,
	[CondonedWaste] [numeric](18, 0) NULL,
	[Density] [numeric](4, 2) NULL,
	[DefaultAdv] [numeric](18, 2) NULL,
	[DetRatio] [numeric](18, 2) NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[REEF] ADD [Selected] [varchar](1) NULL
ALTER TABLE [dbo].[REEF] ADD [WPName] [varchar](50) NULL

GO
CREATE TABLE [dbo].[WORKPLACE_NAME_CHANGE](
	[WorkplaceID] [varchar](12) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[OldDescription] [varchar](50) NULL,
	[Description] [varchar](50) NULL,
	[Calendardate] [datetime] NOT NULL,
	[Reason] [varchar](100) NOT NULL,
 CONSTRAINT [PK_WorkPlace_Name_Change] PRIMARY KEY CLUSTERED 
(
	[WorkplaceID] ASC,
	[Calendardate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[WORKPLACE_OTHER](
	[Description] [varchar](200) NOT NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[WORKPLACE_INACTIVATION_REASON](
	[Reason] [varchar](100) NOT NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[WorkPlace_Inactivation_Reason] ADD [Selected] [varchar](1) NULL
ALTER TABLE [dbo].[WorkPlace_Inactivation_Reason] ADD [Inactive] [varchar](1) NULL
 CONSTRAINT [PK_WorkPlace_Inactivation_Reason] PRIMARY KEY CLUSTERED 
(
	[Reason] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE TABLE [dbo].[WPTYPE_SETUP](
	[TypeCode] [varchar](3) NOT NULL,
	[SetupCode] [varchar](3) NOT NULL,
 CONSTRAINT [PK_WPType_Setup] PRIMARY KEY CLUSTERED 
(
	[TypeCode] ASC,
	[SetupCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[WPTYPE_SETUPCODES](
	[SetupCode] [varchar](3) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_WPType_SetupCodes] PRIMARY KEY CLUSTERED 
(
	[SetupCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[WORKPLACE](
	[WorkplaceID] [varchar](12) NOT NULL,
	[OreFlowID] [varchar](10) NULL,
	[ReefID] [numeric](7, 0) NULL,
	[EndTypeID] [numeric](7, 0) NULL,
	[Description] [varchar](50) NULL,
	[Activity] [numeric](7, 0) NULL,
	[ReefWaste] [varchar](2) NULL,
	[StopingCode] [varchar](8) NULL,
	[EndWidth] [numeric](5, 1) NULL,
	[EndHeight] [numeric](5, 1) NULL,
	[Line] [varchar](20) NULL,
	[Direction] [varchar](8) NULL,
	[Priority] [varchar](2) NULL,
	[Mech] [varchar](2) NULL,
	[Cap] [varchar](50) NULL,
	[RiskRating] [numeric](10, 0) NULL,
	[DefaultAdv] [numeric](18, 2) NULL,
	[GMSIWPID] [numeric](18, 0) NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[WORKPLACE] ADD [DivisionCode] [varchar](2) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [TypeCode] [varchar](3) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [GridCode] [varchar](30) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [DetailCode] [varchar](10) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [NumberCode] [varchar](50) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [DescrCode] [varchar](10) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [DescrNoCode] [varchar](20) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [Inactive] [varchar](1) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [Density] [decimal](18, 2) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [BrokenRockDensity] [decimal](18, 2) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [SubSection] [int] NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [ProcessCode] [int] NULL
SET ANSI_PADDING ON
ALTER TABLE [dbo].[WORKPLACE] ADD [Classification] [char](1) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [WPStatus] [char](1) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [CreationDate] [datetime] NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [Converted] [char](1) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [OldWorkplaceid] [varchar](12) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [Userid] [varchar](20) NULL
ALTER TABLE [dbo].[WORKPLACE] ADD [BoxholeID] varchar(50) NULL
 CONSTRAINT [PK_WORKPLACE] PRIMARY KEY CLUSTERED 
(
	[WorkplaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WORKPLACE]  WITH NOCHECK ADD  CONSTRAINT [FK_WORKPLACE_CODE_WPACTIVITY] FOREIGN KEY([Activity])
REFERENCES [dbo].[CODE_WPACTIVITY] ([Activity])
GO

ALTER TABLE [dbo].[WORKPLACE] CHECK CONSTRAINT [FK_WORKPLACE_CODE_WPACTIVITY]
GO

ALTER TABLE [dbo].[WORKPLACE]  WITH CHECK ADD  CONSTRAINT [FK_WORKPLACE_CommonAreaSubSections] FOREIGN KEY([SubSection])
REFERENCES [dbo].[CommonAreaSubSections] ([Id])
ON UPDATE SET NULL
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[WORKPLACE] CHECK CONSTRAINT [FK_WORKPLACE_CommonAreaSubSections]
GO

ALTER TABLE [dbo].[WORKPLACE]  WITH NOCHECK ADD  CONSTRAINT [FK_WORKPLACE_ENDTYPE] FOREIGN KEY([EndTypeID])
REFERENCES [dbo].[ENDTYPE] ([EndTypeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[WORKPLACE] CHECK CONSTRAINT [FK_WORKPLACE_ENDTYPE]
GO

ALTER TABLE [dbo].[WORKPLACE]  WITH NOCHECK ADD  CONSTRAINT [FK_WORKPLACE_OREFLOWENTITIES] FOREIGN KEY([OreFlowID])
REFERENCES [dbo].[OREFLOWENTITIES] ([OreFlowID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[WORKPLACE] CHECK CONSTRAINT [FK_WORKPLACE_OREFLOWENTITIES]
GO

ALTER TABLE [dbo].[WORKPLACE]  WITH CHECK ADD  CONSTRAINT [FK_WORKPLACE_WORKPLACEStatus] FOREIGN KEY([WPStatus])
REFERENCES [dbo].[CODE_WPSTATUS] ([WPStatus])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[WORKPLACE] CHECK CONSTRAINT [FK_WORKPLACE_WORKPLACEStatus]
GO

CREATE TABLE [dbo].[WORKPLACE_CLASSIFICATION](
	[WorkplaceID] [varchar](12) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[UserID] [varchar](20) NULL,
	[NewClassification] [char](1) NULL,
 CONSTRAINT [PK_WorkPlace_Classification] PRIMARY KEY CLUSTERED 
(
	[WorkplaceID] ASC,
	[Calendardate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WorkPlace_Classification]  WITH CHECK ADD  CONSTRAINT [FK_WorkPlace_Classification_WORKPLACE] FOREIGN KEY([WorkplaceID])
REFERENCES [dbo].[WORKPLACE] ([WorkplaceID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[WorkPlace_Classification] CHECK CONSTRAINT [FK_WorkPlace_Classification_WORKPLACE]
GO

CREATE TABLE [dbo].[WORKPLACE_INACTIVATION](
	[WorkplaceID] [varchar](12) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[UserID] [varchar](20) NULL,
	[NewStatus] [char](1) NULL,
	[Reason] [varchar](100) NOT NULL,
 CONSTRAINT [PK_WorkPlace_Inactivation] PRIMARY KEY CLUSTERED 
(
	[WorkplaceID] ASC,
	[Calendardate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WorkPlace_Inactivation]  WITH CHECK ADD  CONSTRAINT [FK_WorkPlace_Inactivation_WORKPLACE] FOREIGN KEY([WorkplaceID])
REFERENCES [dbo].[WORKPLACE] ([WorkplaceID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[WorkPlace_Inactivation] CHECK CONSTRAINT [FK_WorkPlace_Inactivation_WORKPLACE]
GO

ALTER TABLE [dbo].[WorkPlace_Inactivation]  WITH CHECK ADD  CONSTRAINT [FK_WorkPlace_Inactivation_WorkPlace_Inactivation_Reason] FOREIGN KEY([Reason])
REFERENCES [dbo].[WorkPlace_Inactivation_Reason] ([Reason])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[WorkPlace_Inactivation] CHECK CONSTRAINT [FK_WorkPlace_Inactivation_WorkPlace_Inactivation_Reason]
GO
CREATE TABLE [dbo].[CODE_CALENDAR](
	[CalendarCode] [varchar](20) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_CODE_CALENDAR] PRIMARY KEY CLUSTERED 
(
	[CalendarCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SECCAL](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[Sectionid] [varchar](20) NOT NULL,
	[CalendarCode] [varchar](20) NOT NULL,
	[BeginDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[TotalShifts] [numeric](7, 0) NULL,
 CONSTRAINT [PK_SECCAL] PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[Sectionid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SECCAL]  WITH CHECK ADD  CONSTRAINT [FK_SECCAL_CODE_CALENDAR] FOREIGN KEY([CalendarCode])
REFERENCES [dbo].[CODE_CALENDAR] ([CalendarCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SECCAL] CHECK CONSTRAINT [FK_SECCAL_CODE_CALENDAR]
GO

CREATE FUNCTION [dbo].[GetPrevProdMonth] 
( 
    @ProdMonth NUMERIC(7,0)
    
) 
RETURNS NUMERIC(7,0)
AS 
BEGIN

DECLARE @TheMonth NUMERIC(2,0),@TheYear NUMERIC(4,0),@PrevProdMonth NUMERIC(7,0)

--DECLARE  @ProdMonth NUMERIC(7,0)
--SET @ProdMonth = 201112


SET @TheMonth = CAST(SUBSTRING(CAST(@ProdMonth AS VARCHAR(7)),5,2) AS NUMERIC(2,0))
SET @TheYear = CAST(SUBSTRING(CAST(@ProdMonth AS VARCHAR(7)),1,4) AS NUMERIC(4,0))



IF @TheMonth = 1
BEGIN
SET @PrevProdMonth = CAST(CAST(@TheYear - 1 AS VARCHAR(4)) + '12' AS NUMERIC(7,0))
END
ELSE
IF @TheMonth > 1 and @TheMonth <= 10
BEGIN
SET @PrevProdMonth = CAST(CAST(@TheYear AS VARCHAR(4)) + '0' +CAST(@TheMonth - 1 AS VARCHAR(2)) AS NUMERIC(7,0))
END
ELSE
IF @TheMonth > 10 
BEGIN
SET @PrevProdMonth = CAST(CAST(@TheYear AS VARCHAR(4)) + CAST(@TheMonth - 1 AS VARCHAR(2)) AS NUMERIC(7,0))
END

--SELECT @TheMonth,@TheMonth,@PrevProdMonth


RETURN @PrevProdMonth

END
GO

CREATE FUNCTION [dbo].[GetNextProdMonth] 
( 
    @ProdMonth NUMERIC(7,0)
) 
RETURNS NUMERIC(7,0)
AS 
BEGIN

DECLARE @TheMonth NUMERIC(2,0),@TheYear NUMERIC(4,0),@NextProdMonth NUMERIC(7,0)

--DECLARE  @ProdMonth NUMERIC(7,0)
--SET @ProdMonth = 201112


SET @TheMonth = CAST(SUBSTRING(CAST(@ProdMonth AS VARCHAR(7)),5,2) AS NUMERIC(2,0))
SET @TheYear = CAST(SUBSTRING(CAST(@ProdMonth AS VARCHAR(7)),1,4) AS NUMERIC(4,0))



IF @TheMonth = 12
BEGIN
SET @NextProdMonth = CAST(CAST(@TheYear + 1 AS VARCHAR(4)) + '01' AS NUMERIC(7,0))
END
ELSE
IF @TheMonth >= 1 and @TheMonth < 9
BEGIN
SET @NextProdMonth = CAST(CAST(@TheYear AS VARCHAR(4)) + '0' +CAST(@TheMonth + 1 AS VARCHAR(2)) AS NUMERIC(7,0))
END
ELSE
IF @TheMonth >= 9 
BEGIN
SET @NextProdMonth = CAST(CAST(@TheYear AS VARCHAR(4)) + CAST(@TheMonth + 1 AS VARCHAR(2)) AS NUMERIC(7,0))
END

--SELECT @TheMonth,@TheMonth,@PrevProdMonth


RETURN @NextProdMonth

END
GO

CREATE PROCEDURE [dbo].[sp_Update_SectionComplete] @Prodmonth VARCHAR(250)
as

delete from [SECTION_COMPLETE] where prodmonth = @Prodmonth

insert into [SECTION_COMPLETE]
select s.prodmonth, s.sectionid, s.name, s1.sectionid, s1.name
, s2.sectionid, s2.name, s3.sectionid, s3.name , s4.sectionid, s4.name
, s5.sectionid, s5.name  
from section s, section s1, section s2, section s3, section s4, section s5 
where s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth 
and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth 
and s2.reporttosectionid = s3.sectionid and s2.prodmonth = s3.prodmonth 
and s3.reporttosectionid = s4.sectionid and s3.prodmonth = s4.prodmonth
and s4.reporttosectionid = s5.sectionid and s4.prodmonth = s5.prodmonth

and s.hierarchicalid = 6  and s.prodmonth = @prodmonth

GO

CREATE PROCEDURE [dbo].[sp_UpdateSeccal] 
@Prodmonth VARCHAR(10)

As
DECLARE	@return_value int
EXEC	@return_value = [dbo].[sp_Update_SectionComplete]
		@Prodmonth = @Prodmonth
SELECT	'Return Value' = @return_value

GO

CREATE Procedure [dbo].[sp_CopySections]

@ProdMonth Numeric(7)
as
Insert into SECTION
select @ProdMonth Prodmonth,
a.SectionID,
a.Name,
a.ReportToSectionid,
a.Hierarchicalid from 
(Select * from SECTION where Prodmonth = dbo.GetPrevProdMonth(@ProdMonth)) a left join
(Select * from Section Where Prodmonth = @ProdMonth) b on
a.SectionID = b.SectionID
where b.Prodmonth is NUll

Update SECTION set
Name = a.Name,
ReportToSectionid = a.ReportToSectionid,
Hierarchicalid = a.Hierarchicalid from 
(Select * from SECTION where Prodmonth = dbo.GetNextProdMonth(@ProdMonth)) a left join
(Select * from Section Where Prodmonth = @ProdMonth) b on
a.SectionID = b.SectionID
where
b.Prodmonth =  @ProdMonth and
a.Name <> b.Name and
a.ReportToSectionid <> b.ReportToSectionid and
a.Hierarchicalid <> b.Hierarchicalid 

GO

CREATE TABLE [dbo].[SAMPLING](
	[CalendarDate] [datetime] NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[SWidth] [numeric](18, 0) NULL,
	[Channelwidth] [numeric](18, 0) NULL,
	[HangingWall] [numeric](7, 0) NULL,
	[Footwall] [numeric](7, 0) NULL,
	[Cmgt] [numeric](18, 0) NULL,
	[RIF] [numeric](18, 0) NULL,
	[AssayDate] [datetime] NULL,
	[AuthDate] [datetime] NULL,
 CONSTRAINT [PK_SAMPLING] PRIMARY KEY CLUSTERED 
(
	[CalendarDate] ASC,
	[WorkplaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SAMPLING]  WITH NOCHECK ADD  CONSTRAINT [FK_SAMPLING_WORKPLACE] FOREIGN KEY([WorkplaceID])
REFERENCES [dbo].[WORKPLACE] ([WorkplaceID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SAMPLING] CHECK CONSTRAINT [FK_SAMPLING_WORKPLACE]
GO
CREATE TABLE [dbo].[CALTYPE](
	[CalendarCode] [varchar](20) NOT NULL,
	[CalendarDate] [datetime] NOT NULL,
	[Workingday] [char](1) NULL,
 CONSTRAINT [PK_CALTYPE] PRIMARY KEY CLUSTERED 
(
	[CalendarCode] ASC,
	[CalendarDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CALTYPE]  WITH CHECK ADD  CONSTRAINT [FK_CALTYPE_CODE_CALENDAR] FOREIGN KEY([CalendarCode])
REFERENCES [dbo].[CODE_CALENDAR] ([CalendarCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CALTYPE] CHECK CONSTRAINT [FK_CALTYPE_CODE_CALENDAR]
GO


CREATE PROCEDURE [dbo].[SP_Manage_CalcType]
@CalendarCode varchar(10),@theDay datetime,@isWorking char(1)

as

declare @countItem int

set @countItem = ( SELECT count(CalendarDate) FROM [CALTYPE] WHERE CalendarCode = @CalendarCode and CalendarDate = @theDay)

if @countItem = 1
begin
UPDATE [dbo].[CALTYPE]
   SET [CalendarCode] = @CalendarCode
      ,[CalendarDate] = @theDay
      ,[Workingday] = @isWorking
 WHERE CalendarCode = @CalendarCode and CalendarDate = @theDay
end
else
begin
INSERT INTO [dbo].[CALTYPE]
           ([CalendarCode]
           ,[CalendarDate]
           ,[Workingday])
     VALUES
           (@CalendarCode
           ,@theDay
           ,@isWorking)
end
GO

CREATE PROCEDURE [dbo].[SP_Manage_CalcTypeItem]
@CalendarCode varchar(10),@CalType varchar(50)

as

declare @countItem int

set @countItem = ( SELECT count(CalendarCode) FROM [CODE_CALENDAR] WHERE CalendarCode = @CalendarCode)

if @countItem = 1
begin
UPDATE [dbo].[CODE_CALENDAR]
   SET [Description] = @CalType
      
 WHERE CalendarCode = @CalendarCode
end
else
begin
INSERT INTO [dbo].[CODE_CALENDAR]
           ([CalendarCode]
           ,[Description])
     VALUES
           (@CalendarCode + ' Calendar'
           ,@CalType)
end
GO
CREATE TABLE [dbo].[CALENDARMILL](
	[MillMonth] [varchar](10) NOT NULL,
	[OreFlowID] [varchar](10) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[CalendarCode] [varchar](20) NULL,
	[TotalShifts] [numeric](18, 0) NULL,
 CONSTRAINT [PK_CALENDARMILL] PRIMARY KEY CLUSTERED 
(
	[MillMonth] ASC,
	[OreFlowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CALENDARMILL]  WITH CHECK ADD  CONSTRAINT [FK_CALENDARMILL_CODE_CALENDAR] FOREIGN KEY([CalendarCode])
REFERENCES [dbo].[CODE_CALENDAR] ([CalendarCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CALENDARMILL] CHECK CONSTRAINT [FK_CALENDARMILL_CODE_CALENDAR]
GO

ALTER TABLE [dbo].[CALENDARMILL]  WITH NOCHECK ADD  CONSTRAINT [FK_CALENDARMILL_OREFLOWENTITIES] FOREIGN KEY([OreFlowID])
REFERENCES [dbo].[OREFLOWENTITIES] ([OreFlowID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CALENDARMILL] CHECK CONSTRAINT [FK_CALENDARMILL_OREFLOWENTITIES]
GO
CREATE TABLE [dbo].[CALENDAROTHER](
	[Month] [varchar](10) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[TotalShifts] [numeric](18, 0) NULL,
	[CalendarCode] [varchar](20) NOT NULL,
 CONSTRAINT [PK_CALENDAROTHER] PRIMARY KEY CLUSTERED 
(
	[Month] ASC,
	[CalendarCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

GO

CREATE TABLE [dbo].[SYSSET](
	[BANNER] [varchar](40) NULL,
	[AVERAGECOSTPERTON] [decimal](13, 3) NULL,
	[MINECALLFACTOR] [decimal](13, 3) NULL,
	[MAXGRAMSPERTON] [decimal](13, 3) NULL,
	[MOHIERARCHICALID] [decimal](7, 0) NULL,
	[STOPINGPAYLIMIT] [decimal](13, 3) NULL,
	[DEVELOPMENTPAYLIMIT] [decimal](13, 3) NULL,
	[DEFAULTPLANADVANCE] [decimal](13, 3) NULL,
	[GOLDPRICE] [decimal](13, 3) NULL,
	[URANIUMPRICE] [decimal](13, 3) NULL,
	[CurrentProductionMonth] [decimal](7, 0) NULL,
	[ROCKDENSITY] [decimal](13, 3) NULL,
	[RUNDATE] [datetime] NULL,
	[YEARENDMONTH] [decimal](7, 0) NULL,
	[CURRENTMILLMONTH] [decimal](7, 0) NULL,
	[KGFACTOR] [decimal](13, 3) NULL,
	[HOLESPERSQUAREMETRE] [decimal](13, 3) NULL,
	[COMPANYNUMBER] [varchar](3) NULL,
	[ENGTOPRODLINK] [decimal](7, 0) NULL,
	[LASTUPDATE] [datetime] NULL,
	[MOVINGAVERAGEDAYS] [decimal](7, 0) NULL,
	[SERVERPATH] [varchar](50) NULL,
	[BROKENROCKDENSITY] [decimal](13, 3) NULL,
	[BALLASTDEPTH] [decimal](7, 0) NULL,
	[MINEINDICATOR] [varchar](5) NULL,
	[MAXADVANCE] [decimal](13, 3) NULL,
	[PERCBLASTQUALIFICATION] [decimal](13, 3) NULL,
	[GEOWPLINK] [decimal](7, 0) NULL,
	[ENABLEDATASECURITY] [char](1) NULL,
	[DOLLARSPEROUNCE] [decimal](13, 3) NULL,
	[PLANTONNAGEFACTOR] [decimal](13, 3) NULL,
	[ACTUALTONNAGEFACTOR] [decimal](13, 3) NULL,
	[STOPEVALUETOMILL] [decimal](13, 3) NULL,
	[GULLYWIDTH] [decimal](13, 3) NULL,
	[BACKFILLFACTOR] [decimal](13, 3) NULL,
	[EFFFACTOR] [decimal](5, 2) NULL,
	[STOPEWIDTH] [decimal](7, 0) NULL,
	[CHANNELWIDTH] [decimal](7, 0) NULL,
	[GRADE] [decimal](13, 3) NULL,
	[DATABASETYPE] [varchar](2) NULL,
	[REPDIR] [varchar](100) NULL,
	[HOST] [varchar](255) NULL,
	[Pt] [char](1) NULL,
	[OreFlow] [char](1) NULL,
	[BoxholeType] [decimal](3, 0) NULL,
	[DatabaseName] [varchar](15) NULL,
	[StpAdv] [decimal](7, 2) NULL,
	[BDBook] [decimal](1, 0) NULL,
	[BFDist] [decimal](10, 3) NULL,
	[NCubby] [varchar](20) NULL,
	[DCubby] [varchar](20) NULL,
	[StopDistance] [decimal](13, 3) NULL,
	[CRepDir] [varchar](100) NULL,
	[Version] [varchar](50) NULL,
	[CheckMeas] [varchar](10) NULL,
	[PlanType] [varchar](10) NULL,
	[CleanShift] [varchar](2) NULL,
	[AdjBook] [varchar](1) NULL,
	[DSOrg] [varchar](2) NULL,
	[CheckMeasLvl] [varchar](5) NULL,
	[PlanNotes] [varchar](2) NULL,
	[Remarks] [varchar](255) NULL,
	[A_Color] [varchar](50) NULL,
	[B_Color] [varchar](50) NULL,
	[S_Color] [varchar](50) NULL,
	[ScrollText] [varchar](248) NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[SYSSET] ADD [exename] [varchar](10) NULL
SET ANSI_PADDING ON
ALTER TABLE [dbo].[SYSSET] ADD [soMO] [varchar](1) NULL
ALTER TABLE [dbo].[SYSSET] ADD [soPM] [varchar](1) NULL
ALTER TABLE [dbo].[SYSSET] ADD [soMM] [varchar](1) NULL
ALTER TABLE [dbo].[SYSSET] ADD [soPlan] [varchar](1) NULL
ALTER TABLE [dbo].[SYSSET] ADD [soRM] [varchar](1) NULL
ALTER TABLE [dbo].[SYSSET] ADD [soEval] [varchar](1) NULL
ALTER TABLE [dbo].[SYSSET] ADD [soSurv] [varchar](1) NULL
ALTER TABLE [dbo].[SYSSET] ADD [soGeol] [varchar](1) NULL
ALTER TABLE [dbo].[SYSSET] ADD [IsCentralizedDatabase] [int] NULL CONSTRAINT [DF_SYSSET_IsCentralizedDatabase]  DEFAULT ((0))

GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[SECT](
	[UserID] [varchar](20) NULL,
	[Sectionid] [varchar](20) NOT NULL,
	[CalendarCode] [varchar](20) NULL,
	[BeginDate] [varchar](10) NULL,
	[EndDate] [varchar](10) NULL,
	[TotalShifts] [numeric](7, 0) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_PROBLEM_TYPE](
	[ProblemTypeID] [varchar](5) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[Description] [varchar](40) NULL,
	[Color] [numeric](10, 0) NULL,
	[Deleted] [varchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProblemTypeID] ASC,
	[Activity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CODE_PROBLEM_TYPE]  WITH CHECK ADD  CONSTRAINT [FK_CODE_PROBLEM_TYPE_CODE_ACTIVITY] FOREIGN KEY([Activity])
REFERENCES [dbo].[CODE_ACTIVITY] ([Activity])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CODE_PROBLEM_TYPE] CHECK CONSTRAINT [FK_CODE_PROBLEM_TYPE_CODE_ACTIVITY]
GO
CREATE TABLE [dbo].[CODE_PROBLEM](
	[ProblemID] [varchar](30) NOT NULL,
	[Description] [varchar](60) NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[Color] [numeric](10, 0) NULL,
	[Deleted] [varchar](1) NULL,
	[DisplayOrder] [numeric](2, 0) NULL,
	[ColorText] [varchar](30) NULL,
	[DrillRig] [varchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProblemID] ASC,
	[Activity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CODE_PROBLEM]  WITH CHECK ADD  CONSTRAINT [FK_CODE_PROBLEM_CODE_ACTIVITY] FOREIGN KEY([Activity])
REFERENCES [dbo].[CODE_ACTIVITY] ([Activity])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CODE_PROBLEM] CHECK CONSTRAINT [FK_CODE_PROBLEM_CODE_ACTIVITY]
GO
CREATE TABLE [dbo].[PROBLEM_TYPE](
	[ProblemTypeID] [varchar](5) NOT NULL,
	[ProblemID] [varchar](30) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[Deleted] [varchar](1) NULL,
 CONSTRAINT [PK_PROBLEM_TYPE] PRIMARY KEY CLUSTERED 
(
	[ProblemTypeID] ASC,
	[ProblemID] ASC,
	[Activity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PROBLEM_TYPE]  WITH CHECK ADD  CONSTRAINT [FK_PROBLEM_TYPE_CODE_PROBLEM] FOREIGN KEY([ProblemID], [Activity])
REFERENCES [dbo].[CODE_PROBLEM] ([ProblemID], [Activity])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PROBLEM_TYPE] CHECK CONSTRAINT [FK_PROBLEM_TYPE_CODE_PROBLEM]
GO
CREATE TABLE [dbo].[CODE_PROBLEM_NOTE](
	[NoteID] [varchar](30) NOT NULL,
	[Description] [varchar](150) NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[Color] [numeric](10, 0) NULL,
	[Deleted] [varchar](1) NULL,
	[Explanation] [varchar](250) NULL,
	[NotLostBlastProblem] [varchar](2) NULL,
PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC,
	[Activity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[PROBLEM_NOTE](
	[ProblemID] [varchar](30) NOT NULL,
	[NoteID] [varchar](30) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[Deleted] [varchar](1) NULL,
 CONSTRAINT [PK_Problem_Note] PRIMARY KEY CLUSTERED 
(
	[ProblemID] ASC,
	[NoteID] ASC,
	[Activity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PROBLEM_NOTE]  WITH CHECK ADD  CONSTRAINT [FK_PROBLEM_NOTE_CODE_PROBLEM] FOREIGN KEY([ProblemID], [Activity])
REFERENCES [dbo].[CODE_PROBLEM] ([ProblemID], [Activity])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PROBLEM_NOTE] CHECK CONSTRAINT [FK_PROBLEM_NOTE_CODE_PROBLEM]
GO

ALTER TABLE [dbo].[PROBLEM_NOTE]  WITH CHECK ADD  CONSTRAINT [FK_PROBLEM_NOTE_CODE_PROBLEM_NOTE] FOREIGN KEY([NoteID], [Activity])
REFERENCES [dbo].[CODE_PROBLEM_NOTE] ([NoteID], [Activity])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PROBLEM_NOTE] CHECK CONSTRAINT [FK_PROBLEM_NOTE_CODE_PROBLEM_NOTE]
GO

CREATE TABLE [dbo].[CREW](
	[Calendardate] [datetime] NOT NULL,
	[GangNo] [varchar](50) NOT NULL,
	[CrewNo] [varchar](50) NULL,
	[CrewName] [varchar](50) NULL,

) ON [PRIMARY]

GO
	ALTER TABLE CREW ADD ProcessCode VARCHAR(50)
ALTER TABLE CREW ADD CostArea VARCHAR(50)

CREATE TABLE [dbo].[CODE_PLANNING](
	[PlanCode] [char](2) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_CODE_PLANNING] PRIMARY KEY CLUSTERED 
(
	[PlanCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[PLANMONTH](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[Sectionid] [varchar](20) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[IsCubics] [varchar](5) NOT NULL,
	[PlanCode] [char](2) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[Workplaceid] [varchar](12) NOT NULL,
	[TargetID] [numeric](10, 0) NULL,
	[OrgUnitDay] [varchar](50) NULL,
	[OrgUnitAfternoon] [varchar](50) NULL,
	[OrgUnitNight] [varchar](50) NULL,
	[RomingCrew] [varchar](50) NULL,
	[Locked] [bit] NULL,
	[Auth] [char](1) NULL,
	[SQM] [numeric](10, 3) NULL,
	[ReefSQM] [numeric](10, 3) NULL,
	[WasteSQM] [numeric](10, 3) NULL,
	[FL] [numeric](10, 3) NULL,
	[ReefFL] [numeric](10, 3) NULL,
	[WasteFL] [numeric](10, 3) NULL,
	[FaceAdvance] [numeric](10, 3) NULL,
	[IdealSW] [numeric](10, 3) NULL,
	[SW] [numeric](10, 3) NULL,
	[CW] [numeric](10, 3) NULL,
	[CMGT] [numeric](10, 3) NULL,
	[GT] [numeric](10, 3) NULL,
	[Kg] [numeric](10, 3) NULL,
	[FaceCMGT] [numeric](10, 3) NULL,
	[FaceKG] [numeric](10, 3) NULL,
	[Tons] [numeric](10, 3) NULL,
	[ReefTons] [numeric](10, 3) NULL,
	[WasteTons] [numeric](10, 3) NULL,
	[FaceValue] [numeric](10, 3) NULL,
	[CubicMetres] [numeric](10, 3) NULL,
	[Cubics] [numeric](10, 3) NULL,
	[CubicsReef] [numeric](10, 3) NULL,
	[CubicsWaste] [numeric](10, 3) NULL,
	[CubicsTons] [numeric](10, 3) NULL,
	[CubicsReefTons] [numeric](10, 3) NULL,
	[CubicsWasteTons] [numeric](10, 3) NULL,
	[CubicGrams] [numeric](10, 3) NULL,
	[CubicDepth] [numeric](10, 3) NULL,
	[ActualDepth] [numeric](10, 3) NULL,
	[CubicGT] [numeric](10, 3) NULL,
	[TrammedTons] [numeric](10, 3) NULL,
	[TrammedValue] [numeric](10, 3) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[PLANMONTH] ADD [TrammedLevel] [varchar](10) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [Metresadvance] [numeric](10, 3) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [ReefAdv] [numeric](10, 3) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [WasteAdv] [numeric](10, 3) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [DevMain] [numeric](10, 3) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [DevSec] [numeric](10, 3) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [DevSecReef] [numeric](10, 3) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [DevCap] [numeric](10, 3) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [LockedDate] [datetime] NULL
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[PLANMONTH] ADD [LockedBY] [varchar](20) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [DrillRig] [varchar](20) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [StoppedDate] [datetime] NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [EndDate] [datetime] NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [IsStopped] [char](1) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [TopEnd] [char](1) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [AutoUnPlan] [char](1) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [LabourStrength] [numeric](7, 0) NULL
SET ANSI_PADDING ON
ALTER TABLE [dbo].[PLANMONTH] ADD [BoxHoleID] [varchar](20) NULL
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[PLANMONTH] ADD [MOCycle] [varchar](250) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [Vac] [varchar](2) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [DC] [varchar](2) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [MOCycleNum] [varchar](250) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [DevFlag] [varchar](2) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [CMKGT] [numeric](7, 0) NULL
ALTER TABLE [dbo].[PLANMONTH] ADD [UraniumBrokenKG] [numeric](10, 3) NULL
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[Sectionid] ASC,
	[WorkplaceID] ASC,
	[Activity] ASC,
	[IsCubics] ASC,
	[PlanCode] ASC,
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PLANMONTH]  WITH CHECK ADD  CONSTRAINT [FK_PLANMONTH_CODE_PLANNING] FOREIGN KEY([PlanCode])
REFERENCES [dbo].[CODE_PLANNING] ([PlanCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PLANMONTH] CHECK CONSTRAINT [FK_PLANMONTH_CODE_PLANNING]
GO



CREATE TABLE [dbo].[CODE_MINEMETHOD](
	[MethodCode] [varchar](4) NOT NULL,
	[Activity] [numeric](7, 0) NULL,
	[Description] [varchar](60) NULL,
	[BOMLookup] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MethodCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CODE_MINEMETHOD]  WITH CHECK ADD  CONSTRAINT [FK_CODE_MINEMETHOD_CODE_ACTIVITY] FOREIGN KEY([Activity])
REFERENCES [dbo].[CODE_ACTIVITY] ([Activity])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CODE_MINEMETHOD] CHECK CONSTRAINT [FK_CODE_MINEMETHOD_CODE_ACTIVITY]
GO


CREATE TABLE [dbo].[CODE_PREPLANNINGTYPES](
	[ChangeID] [int] NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_CODE_PREPLANNINGTYPES] PRIMARY KEY CLUSTERED 
(
	[ChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[AUDIT_PLANMONTH](
	[Audit_ID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UserName] [varchar](128) NULL,
	[MachineID] [varchar](128) NULL,
	[Type] [char](1) NULL,
	[APP_NAME] [varchar](256) NULL,
	[NewPRODMONTH] [numeric](7, 0) NULL,
	[OldPRODMONTH] [numeric](7, 0) NULL,
	[NewSECTIONID] [varchar](10) NULL,
	[OldSECTIONID] [varchar](10) NULL,
	[NewWORKPLACEID] [varchar](12) NULL,
	[OldWORKPLACEID] [varchar](12) NULL,
	[NewACTIVITYCODE] [numeric](7, 0) NULL,
	[OldACTIVITYCODE] [numeric](7, 0) NULL,
	[NewCHANNELWIDTH] [numeric](7, 0) NULL,
	[OldCHANNELWIDTH] [numeric](7, 0) NULL,
	[NewSQUAREMETRES] [numeric](7, 0) NULL,
	[OldSQUAREMETRES] [numeric](7, 0) NULL,
	[NewGOLDGRAMSPERTON] [numeric](13, 3) NULL,
	[OldGOLDGRAMSPERTON] [numeric](13, 3) NULL,
	[NewMETRESADVANCE] [numeric](13, 3) NULL,
	[OldMETRESADVANCE] [numeric](13, 3) NULL,
	[NewSTOPEWIDTH] [numeric](7, 0) NULL,
	[OldSTOPEWIDTH] [numeric](7, 0) NULL,
	[NewFACELENGTH] [numeric](13, 3) NULL,
	[OldFACELENGTH] [numeric](13, 3) NULL,
	[NewCUBICMETRES] [numeric](7, 0) NULL,
	[OldCUBICMETRES] [numeric](7, 0) NULL,
	[NewTONS] [numeric](7, 0) NULL,
	[OldTONS] [numeric](7, 0) NULL,
	[NewGRAMS] [numeric](13, 3) NULL,
	[OldGRAMS] [numeric](13, 3) NULL,
	[NewSTOPINGCUBICS] [numeric](13, 3) NULL,
	[OldSTOPINGCUBICS] [numeric](13, 3) NULL,
	[NewHEIGHT] [numeric](13, 3) NULL,
	[OldHEIGHT] [numeric](13, 3) NULL,
	[NewWIDTH] [numeric](13, 3) NULL,
	[OldWIDTH] [numeric](13, 3) NULL,
	[NewAUTOUNPLAN] [varchar](1) NULL,
	[OldAUTOUNPLAN] [varchar](1) NULL,
	[Newcmgt] [int] NULL,
	[Oldcmgt] [int] NULL,
	[NewReefFL] [numeric](8, 2) NULL,
	[OldReefFL] [numeric](8, 2) NULL,
	[NewWasteFL] [numeric](8, 2) NULL,
	[OldWasteFL] [numeric](8, 2) NULL,
	[NewReefTons] [int] NULL,
	[OldReefTons] [int] NULL,
	[NewWasteTons] [int] NULL,
	[OldWasteTons] [int] NULL,
	[NewReefSQM] [int] NULL,
	[OldReefSQM] [int] NULL,
	[NewWasteSQM] [int] NULL,
	[OldWasteSQM] [int] NULL,
	[NewIsCubics] [char](1) NULL,
	[OldIsCubics] [char](1) NULL,
	[NewIsStopped] [char](1) NULL,
	[OldIsStopped] [char](1) NULL,
	[NewStoppedDate] [datetime] NULL,
	[OldStoppedDate] [datetime] NULL,
	[NewStoppedUserID] [varchar](20) NULL,
	[OldStoppedUserID] [varchar](20) NULL,
	[NewDateTimeStopped] [datetime] NULL,
	[OldDateTimeStopped] [datetime] NULL,
	[NewStartDate] [datetime] NULL,
	[OldStartDate] [datetime] NULL,
	[NewSubActivity_ID] [numeric](2, 0) NULL,
	[OldSubActivity_ID] [numeric](2, 0) NULL,
	[NewCubicTons] [numeric](10, 3) NULL,
	[OldCubicTons] [numeric](10, 3) NULL,
	[NewCubicGrams] [numeric](10, 3) NULL,
	[OldCubicGrams] [numeric](10, 3) NULL,
	[NewCubicDepth] [numeric](10, 3) NULL,
	[OldCubicDepth] [numeric](10, 3) NULL,
	[NewCubicGT] [numeric](10, 3) NULL,
	[OldCubicGT] [numeric](10, 3) NULL,
	[NewReefAdv] [numeric](10, 3) NULL,
	[OldReefAdv] [numeric](10, 3) NULL,
	[NewWasteAdv] [numeric](10, 3) NULL,
	[OldWasteAdv] [numeric](10, 3) NULL,
	[NewOrgunitDay] [varchar](50) NULL,
	[OldOrgunitDay] [varchar](50) NULL,
	[NewOrgunitAfterNoon] [varchar](50) NULL,
	[OldOrgunitAfterNoon] [varchar](50) NULL,
	[NewOrgunitNight] [varchar](50) NULL,
	[OldOrgunitNight] [varchar](50) NULL,
	[NewMinerOrgUnit] [varchar](50) NULL,
	[OldMinerOrgUnit] [varchar](50) NULL,
	[NewRovingCrew] [varchar](50) NULL,
	[OldRovingCrew] [varchar](50) NULL,
	[Newcrewstrength] [numeric](7, 2) NULL,
	[Oldcrewstrength] [numeric](7, 2) NULL,
	[NewDrillRig] [varchar](20) NULL,
	[OldDrillRig] [varchar](20) NULL,
	[NewTargetID] [numeric](13, 0) NULL,
	[OldTargetID] [numeric](13, 0) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


CREATE TABLE [dbo].[Audit_PLANMONTH_OLDGOLD](
	[Audit_ID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UserName] [varchar](128) NULL,
	[MachineID] [varchar](128) NULL,
	[Type] [char](1) NULL,
	[APP_NAME] [varchar](256) NULL,
	[NEW_Prodmonth] [numeric](7, 0) NULL,
	[OLD_Prodmonth] [numeric](7, 0) NULL,
	[NEW_SectionID] [varchar](15) NULL,
	[OLD_SectionID] [varchar](15) NULL,
	[NEW_WorkplaceID] [varchar](15) NULL,
	[OLD_WorkplaceID] [varchar](15) NULL,
	[NEW_Activity] [numeric](2, 0) NULL,
	[OLD_Activity] [numeric](2, 0) NULL,
	[NEW_PlanCode] [varchar](3) NULL,
	[OLD_PlanCode] [varchar](3) NULL,
	[NEW_OGID] [numeric](7, 0) NULL,
	[OLD_OGID] [numeric](7, 0) NULL,
	[NEW_Units] [numeric](7, 0) NULL,
	[OLD_Units] [numeric](7, 0) NULL,
	[NEW_Depth] [numeric](7, 0) NULL,
	[OLD_Depth] [numeric](7, 0) NULL,
	[NEW_Grade] [numeric](7, 0) NULL,
	[OLD_Grade] [numeric](7, 0) NULL,
	[NEW_Grams] [numeric](7, 0) NULL,
	[OLD_Grams] [numeric](7, 0) NULL,
	[NEW_OrgunitDay] [varchar](50) NULL,
	[OLD_OrgunitDay] [varchar](50) NULL,
	[NEW_OrgunitNight] [varchar](50) NULL,
	[OLD_OrgunitNight] [varchar](50) NULL,
	[NEW_OrgunitAfterNoon] [varchar](50) NULL,
	[OLD_OrgunitAfterNoon] [varchar](50) NULL,
	[NEW_ActualDepth] [numeric](7, 0) NULL,
	[OLD_ActualDepth] [numeric](7, 0) NULL,
	[NEW_Locked] [bit] NULL,
	[OLD_Locked] [bit] NULL,
	[NEW_Auth] [char](1) NULL,
	[OLD_Auth] [char](1) NULL,
	[NEW_BoxHoleID] [varchar](20) NULL,
	[OLD_BoxHoleID] [varchar](20) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



CREATE TABLE [dbo].[AUDIT_PLANMONTH_SUNDRYMINING](
	[Audit_ID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UserName] [varchar](128) NULL,
	[MachineID] [varchar](128) NULL,
	[Type] [char](1) NULL,
	[APP_NAME] [varchar](256) NULL,
	[NEW_Prodmonth] [numeric](7, 0) NULL,
	[OLD_Prodmonth] [numeric](7, 0) NULL,
	[NEW_SectionID] [varchar](10) NULL,
	[OLD_SectionID] [varchar](10) NULL,
	[NEW_WorkplaceID] [varchar](12) NULL,
	[OLD_WorkplaceID] [varchar](12) NULL,
	[NEW_Activity] [numeric](2, 0) NULL,
	[OLD_Activity] [numeric](2, 0) NULL,
	[NEW_SMID] [numeric](7, 0) NULL,
	[OLD_SMID] [numeric](7, 0) NULL,
	[NEW_PlanCode] [char](2) NULL,
	[OLD_PlanCode] [char](2) NULL,
	[NEW_OrgunitDay] [varchar](50) NULL,
	[OLD_OrgunitDay] [varchar](50) NULL,
	[NEW_OrgunitNight] [varchar](50) NULL,
	[OLD_OrgunitNight] [varchar](50) NULL,
	[NEW_Units] [numeric](10, 2) NULL,
	[OLD_Units] [numeric](10, 2) NULL,
	[NEW_AddInfo] [varchar](50) NULL,
	[OLD_AddInfo] [varchar](50) NULL,
	[NEW_AutoUnPlan] [varchar](2) NULL,
	[OLD_AutoUnPlan] [varchar](2) NULL,
	[NEW_CrewStrength] [numeric](3, 0) NULL,
	[OLD_CrewStrength] [numeric](3, 0) NULL,
	[NEW_MetresAdvance] [numeric](10, 3) NULL,
	[OLD_MetresAdvance] [numeric](10, 3) NULL,
	[NEW_Locked] [bit] NULL,
	[OLD_Locked] [bit] NULL,
	[NEW_Auth] [char](1) NULL,
	[OLD_Auth] [char](1) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


CREATE TABLE [dbo].[OLDGOLD_UNIT](
	[UnitBase] [numeric](7, 0) NOT NULL,
	[Description] [varchar](20) NULL,
 CONSTRAINT [PK_OLDGOLD_UNIT] PRIMARY KEY CLUSTERED 
(
	[UnitBase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[OLDGOLD_TYPE](
	[OGID] [numeric](7, 0) NOT NULL,
	[OGDescription] [varchar](50) NULL,
	[UnitBase] [numeric](7, 0) NULL,
	[Efficiency] [numeric](5, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[OGID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[OLDGOLD_TYPE]  WITH CHECK ADD  CONSTRAINT [FK_OLDGOLD_TYPE_OLDGOLD_UNIT] FOREIGN KEY([UnitBase])
REFERENCES [dbo].[OLDGOLD_UNIT] ([UnitBase])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OLDGOLD_TYPE] CHECK CONSTRAINT [FK_OLDGOLD_TYPE_OLDGOLD_UNIT]
GO

CREATE TABLE [dbo].[PLANMONTH_SUNDRYMINING](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](20) NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[Activity] [numeric](2, 0) NOT NULL,
	[SMID] [numeric](7, 0) NOT NULL,
	[PlanCode] [char](2) NOT NULL,
	[OrgunitDay] [varchar](50) NOT NULL,
	[OrgunitNight] [varchar](50) NULL,
	[Units] [numeric](10, 2) NULL,
	[AddInfo] [varchar](50) NULL,
	[AutoUnPlan] [varchar](2) NULL,
	[CrewStrength] [numeric](3, 0) NULL,
	[MetresAdvance] [numeric](10, 3) NULL,
	[Locked] [bit] NULL,
	[Auth] [char](1) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[PLANMONTH_SUNDRYMINING] ADD [BoxHoleID] [varchar](20) NULL
 CONSTRAINT [PK__PLANMONTH_SUNDRYMINING] PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[SectionID] ASC,
	[WorkplaceID] ASC,
	[Activity] ASC,
	[SMID] ASC,
	[PlanCode] ASC,
	[OrgunitDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PLANMONTH_SUNDRYMINING]  WITH CHECK ADD  CONSTRAINT [FK_PLANMONTH_SUNDRYMINING_CODE_PLANNING] FOREIGN KEY([PlanCode])
REFERENCES [dbo].[CODE_PLANNING] ([PlanCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PLANMONTH_SUNDRYMINING] CHECK CONSTRAINT [FK_PLANMONTH_SUNDRYMINING_CODE_PLANNING]
GO

ALTER TABLE [dbo].[PLANMONTH_SUNDRYMINING]  WITH CHECK ADD  CONSTRAINT [FK_PLANMONTH_SUNDRYMINING_SECTION] FOREIGN KEY([Prodmonth], [SectionID])
REFERENCES [dbo].[SECTION] ([Prodmonth], [SectionID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PLANMONTH_SUNDRYMINING] CHECK CONSTRAINT [FK_PLANMONTH_SUNDRYMINING_SECTION]
GO

ALTER TABLE [dbo].[PLANMONTH_SUNDRYMINING]  WITH CHECK ADD  CONSTRAINT [FK_PLANMONTH_SUNDRYMINING_WORKPLACE] FOREIGN KEY([WorkplaceID])
REFERENCES [dbo].[WORKPLACE] ([WorkplaceID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PLANMONTH_SUNDRYMINING] CHECK CONSTRAINT [FK_PLANMONTH_SUNDRYMINING_WORKPLACE]
GO




CREATE TABLE [dbo].[PLANMONTH_OLDGOLD](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](20) NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[PlanCode] [char](2) NOT NULL,
	[OGID] [numeric](7, 0) NOT NULL,
	[Units] [numeric](7, 0) NULL,
	[Depth] [numeric](5, 0) NULL,
	[Grade] [numeric](7, 2) NULL,
	[Grams] [numeric](7, 0) NULL,
	[OrgunitDay] [varchar](50) NULL,
	[OrgunitNight] [varchar](50) NULL,
	[OrgunitAfterNoon] [varchar](50) NULL,
	[ActualDepth] [numeric](5, 0) NULL,
	[Locked] [bit] NULL,
	[Auth] [char](1) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[PLANMONTH_OLDGOLD] ADD [BoxHoleID] [varchar](20) NULL
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[SectionID] ASC,
	[WorkplaceID] ASC,
	[Activity] ASC,
	[PlanCode] ASC,
	[OGID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PLANMONTH_OLDGOLD]  WITH CHECK ADD  CONSTRAINT [FK_PLANMONTH_OLDGOLD_CODE_PLANNING] FOREIGN KEY([PlanCode])
REFERENCES [dbo].[CODE_PLANNING] ([PlanCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PLANMONTH_OLDGOLD] CHECK CONSTRAINT [FK_PLANMONTH_OLDGOLD_CODE_PLANNING]
GO

ALTER TABLE [dbo].[PLANMONTH_OLDGOLD]  WITH CHECK ADD  CONSTRAINT [FK_PLANMONTH_OLDGOLD_OLDGOLD_TYPE] FOREIGN KEY([OGID])
REFERENCES [dbo].[OLDGOLD_TYPE] ([OGID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PLANMONTH_OLDGOLD] CHECK CONSTRAINT [FK_PLANMONTH_OLDGOLD_OLDGOLD_TYPE]
GO

ALTER TABLE [dbo].[PLANMONTH_OLDGOLD]  WITH CHECK ADD  CONSTRAINT [FK_PLANMONTH_OLDGOLD_SECTION] FOREIGN KEY([Prodmonth], [SectionID])
REFERENCES [dbo].[SECTION] ([Prodmonth], [SectionID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PLANMONTH_OLDGOLD] CHECK CONSTRAINT [FK_PLANMONTH_OLDGOLD_SECTION]
GO

ALTER TABLE [dbo].[PLANMONTH_OLDGOLD]  WITH CHECK ADD  CONSTRAINT [FK_PLANMONTH_OLDGOLD_WORKPLACE] FOREIGN KEY([WorkplaceID])
REFERENCES [dbo].[WORKPLACE] ([WorkplaceID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PLANMONTH_OLDGOLD] CHECK CONSTRAINT [FK_PLANMONTH_OLDGOLD_WORKPLACE]
GO




CREATE TABLE [dbo].[PLANMONTH_MILLING](
	[OreflowID] [varchar](10) NOT NULL,
	[MillMonth] [numeric](7, 0) NOT NULL,
	[CallTons] [numeric](7, 0) NULL,
	[BudgetTons] [numeric](7, 0) NULL,
	[KgG] [numeric](7, 2) NULL,
	[ActualTons] [int] NULL,
	[ActualKg] [float] NULL,
	[RecoveryPassed] [varchar](3) NULL,
	[ResidueAct] [float] NULL,
	[RecoveryCall] [numeric](5, 2) NULL,
	[RecoveryAct] [numeric](5, 2) NULL,
	[ResidueCall] [float] NULL,
	[UGCallTons] [numeric](7, 0) NULL,
	[UGKgGold] [numeric](10, 3) NULL,
	[SCallTons] [numeric](7, 0) NULL,
	[SKgGold] [numeric](10, 3) NULL,
PRIMARY KEY CLUSTERED 
(
	[OreflowID] ASC,
	[MillMonth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PLANMONTH_MILLING]  WITH CHECK ADD  CONSTRAINT [FK_PLANMONTH_MILLING_OREFLOWENTITIES] FOREIGN KEY([OreflowID])
REFERENCES [dbo].[OREFLOWENTITIES] ([OreFlowID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PLANMONTH_MILLING] CHECK CONSTRAINT [FK_PLANMONTH_MILLING_OREFLOWENTITIES]
GO



CREATE TABLE [dbo].[PLANPROT_APPROVEDTEMPLATE](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[Workplaceid] [varchar](12) NOT NULL,
	[Sectionid_2] [varchar](10) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[FileName] [varchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[Workplaceid] ASC,
	[Sectionid_2] ASC,
	[Activity] ASC,
	[FileName] ASC,
	[DateCreated] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PLANPROT_APPROVEUSERS](
	[TemplateID] [int] NULL,
	[Shaft] [varchar](50) NULL,
	[Section] [varchar](50) NULL,
	[User1] [varchar](60) NULL,
	[User2] [varchar](60) NULL,
	[Unit] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[PLANPROT_DATA](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[FieldID] [int] NOT NULL,
	[TheValue] [varchar](max) NULL,
	[ActivityType] [int] NOT NULL,
	[CaptureDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PlanProt_Data] PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[SectionID] ASC,
	[WorkplaceID] ASC,
	[FieldID] ASC,
	[CaptureDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[PLANPROT_DATAAPPROVED](
	[ApproveID] [int] IDENTITY(1,1) NOT NULL,
	[PRODMONTH] [numeric](7, 0) NOT NULL,
	[SECTIONID] [varchar](10) NOT NULL,
	[WORKPLACEID] [varchar](12) NOT NULL,
	[ActivityType] [int] NOT NULL,
	[TemplateID] [int] NOT NULL,
	[Approved] [bit] NULL,
	[ApprovedBy] [varchar](30) NULL,
	[ApprovedDate] [datetime] NULL,
 CONSTRAINT [PK_PlanProt_DataApproved] PRIMARY KEY CLUSTERED 
(
	[ApproveID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PLANPROT_DATALOCKED](
	[ApprovedID] [int] NOT NULL,
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[FieldID] [int] NOT NULL,
	[TheValue] [varchar](max) NULL,
	[ActivityType] [int] NOT NULL,
	[CaptureDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PlanProt_DataLocked] PRIMARY KEY CLUSTERED 
(
	[ApprovedID] ASC,
	[Prodmonth] ASC,
	[SectionID] ASC,
	[WorkplaceID] ASC,
	[FieldID] ASC,
	[CaptureDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[PLANPROT_DATALOG](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[WorkplaceID] [varchar](30) NULL,
	[FieldID] [int] NULL,
	[UserID] [varchar](60) NULL,
	[LodDate] [datetime] NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PLANPROT_FIELDS](
	[FieldID] [int] IDENTITY(1,1) NOT NULL,
	[TemplateID] [int] NOT NULL,
	[FieldName] [varchar](max) NOT NULL,
	[FieldType] [varchar](10) NOT NULL,
	[CalcField] [bit] NULL,
	[ThePos] [varchar](4) NULL,
	[NoCharacters] [varchar](max) NULL,
	[Nolines] [varchar](max) NULL,
	[FieldRequired] [bit] NULL,
	[ParentID] [int] NULL,
 CONSTRAINT [PK_PlanPlot_Fields] PRIMARY KEY CLUSTERED 
(
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[PLANPROT_FIELDTYPES](
	[FieldTypeID] [int] IDENTITY(1,1) NOT NULL,
	[FieldDescription] [varchar](30) NOT NULL,
 CONSTRAINT [PK_PLANPROT_FIELDTYPES] PRIMARY KEY CLUSTERED 
(
	[FieldTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PLANPROT_FIELDVALUE](
	[FieldValueID] [int] IDENTITY(1,1) NOT NULL,
	[FieldID] [int] NOT NULL,
	[SelectedValue] [varchar](50) NULL,
	[MinValue] [varchar](50) NULL,
	[MaxValue] [varchar](50) NULL,
	[RiskRating] [int] NULL,
 CONSTRAINT [PK_PlanPlot_FieldValues] PRIMARY KEY CLUSTERED 
(
	[FieldValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PLANPROT_PROFILEACCESS](
	[DepartmentID] [int] NOT NULL,
	[TemplateID] [int] NOT NULL,
	[AccessLevel] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PLANPROT_TEMPLATE]    Script Date: 2017/03/18 9:53:03 PM ******/

CREATE TABLE [dbo].[PLANPROT_TEMPLATE](
	[TemplateID] [int] NOT NULL,
	[TemplateName] [varchar](200) NULL,
	[Activity] [numeric](6, 0) NOT NULL,
	[User1] [varchar](60) NULL,
	[User2] [varchar](60) NULL,
	[ApprovalRequired] [bit] NULL,
	[Human_Resources] [bit] NULL,
 CONSTRAINT [PK_PlanProt_Template] PRIMARY KEY CLUSTERED 
(
	[TemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PlanProtWPlistTemp](
	[theUser] [varchar](150) NULL,
	[WPName] [varchar](150) NULL,
	[WPid] [varchar](150) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PREPLANNING_APPROVE_TEMP](
	[CurrentUser] [varchar](60) NULL,
	[WorkplaceID] [varchar](12) NULL,
	[WorkplaceDesc] [varchar](60) NULL,
	[TemplateName] [varchar](60) NULL,
	[TheGroup] [varchar](60) NULL,
	[CanApprove] [int] NULL,
	[Comments] [varchar](80) NULL,
	[Approved] [bit] NULL DEFAULT ((0)),
	[Activity] [int] NULL,
	[SectionID_2] [varchar](20) NULL,
	[SectionID] [varchar](20) NULL,
	[Prodmonth] [varchar](6) NULL,
	[CubicMetres] [numeric](10, 3) NULL,
	[Meters] [numeric](10, 3) NULL,
	[MetersWaste] [numeric](10, 3) NULL,
	[SQM] [numeric](10, 3) NULL,
	[WasteSQM] [numeric](10, 3) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PREPLANNING_CHANGEREQUEST](
	[ChangeRequestID] [int] IDENTITY(0,1) NOT NULL,
	[RequestBy] [varchar](50) NOT NULL,
	[ProdMonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](20) NOT NULL,
	[SectionID_2] [varchar](20) NOT NULL,
	[WorkplaceID] [varchar](20) NULL,
	[ChangeID] [varchar](10) NOT NULL,
	[DayCrew] [varchar](50) NULL,
	[AfternoonCrew] [varchar](50) NULL,
	[NightCrew] [varchar](50) NULL,
	[RovingCrew] [varchar](50) NULL,
	[ReefSQM] [numeric](7, 0) NULL,
	[WasteSQM] [numeric](7, 0) NULL,
	[Meters] [numeric](7, 2) NULL,
	[MetersWaste] [numeric](7, 2) NULL,
	[CubicMeters] [numeric](7, 0) NULL,
	[StartDate] [datetime] NULL,
	[StopDate] [datetime] NULL,
	[Comments] [varchar](150) NULL,
	[MiningMethod] [varchar](20) NULL,
	[OldWorkplaceID] [varchar](20) NULL,
	[Activity] [int] NULL,
	[FL] [numeric](13, 3) NULL,
	[DrillRig] [varchar](150) NULL,
	[DeleteBookings] [bit] NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PREPLANNING_CHANGEREQUEST_APPROVAL](
	[ApproveRequestID] [int] IDENTITY(1,1) NOT NULL,
	[ChangeRequestID] [int] NULL,
	[User1] [varchar](50) NULL,
	[User2] [varchar](50) NULL,
	[Department] [varchar](80) NULL,
	[RequestDate] [datetime] NOT NULL,
	[Approved] [bit] NOT NULL CONSTRAINT [DF_PREPLANNING_CHANGEREQUEST_APPROVAL_Approved]  DEFAULT ((0)),
	[Declined] [bit] NOT NULL CONSTRAINT [DF_PREPLANNING_CHANGEREQUEST_APPROVAL_Declined]  DEFAULT ((0)),
	[ApprovedDeclinedDate] [datetime] NULL,
	[ApprovedDeclinedByUser] [varchar](50) NULL,
	[Comments] [varchar](max) NULL,
	[ApprovalRequired] [bit] NULL,
 CONSTRAINT [PK_PREPLANNING_CHANGEREQUEST_APPROVAL] PRIMARY KEY CLUSTERED 
(
	[ApproveRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[PREPLANNING_LOG](
	[Prodmonth] [numeric](7, 0) NULL,
	[WorkplaceID] [varchar](12) NULL,
	[Activity] [int] NULL,
	[Sectionid] [varchar](20) NULL,
	[CurrentUser] [varchar](60) NULL,
	[Reason] [varchar](max) NULL,
	[theAction] [varchar](20) NULL,
	[theActionDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF

CREATE TABLE [dbo].[PREPLANNING_NOTIFICATION_SECURITY](
	[Shaft] [varchar](150) NOT NULL,
	[Section] [varchar](150) NOT NULL,
	[CPM_UserID1] [varchar](150) NULL,
	[CPM_UserID2] [varchar](150) NULL,
	[Department] [varchar](50) NOT NULL,
	[CrewChange] [bit] NULL,
	[CallChange] [bit] NULL,
	[StopWPChange] [bit] NULL,
	[NewWPChange] [bit] NULL,
	[WPMove] [bit] NULL,
	[StartWPChange] [bit] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

CREATE TABLE [dbo].[PREPLANNINGHR](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[Sectionid] [varchar](20) NOT NULL,
	[Sectionid_2] [varchar](20) NOT NULL,
	[WorkplaceDesc] [varchar](32) NOT NULL,
	[Activitycode] [numeric](7, 0) NOT NULL,
	[Workplaceid] [varchar](20) NULL,
	[StdAndNormID] [int] NOT NULL,
	[Desgnation] [varchar](150) NOT NULL,
	[DAY] [int] NULL,
	[Night] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[Sectionid_2] ASC,
	[Sectionid] ASC,
	[WorkplaceDesc] ASC,
	[Activitycode] ASC,
	[StdAndNormID] ASC,
	[Desgnation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PrePlanningHR_Log](
	[LogDate] [datetime] NULL,
	[Action] [varchar](10) NULL,
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[Sectionid] [varchar](20) NOT NULL,
	[Sectionid_2] [varchar](20) NOT NULL,
	[WorkplaceDesc] [varchar](32) NOT NULL,
	[Activitycode] [numeric](7, 0) NOT NULL,
	[Workplaceid] [varchar](20) NULL,
	[StdAndNormID] [int] NOT NULL,
	[Desgnation] [varchar](150) NOT NULL,
	[DAY] [int] NULL,
	[Night] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

CREATE TABLE [dbo].[PREPLANNINGHR_UNPLANNED_LABOUR](
	[Prodmonth] [numeric](7, 0) NULL,
	[theLevel] [varchar](5) NULL,
	[SectionID_2] [varchar](20) NULL,
	[SectionID_1] [varchar](20) NULL,
	[PlanValue] [int] NULL,
	[AtWorkPlan] [int] NULL,
	[InServicePlan] [int] NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PrePlanningHRUnplannedLabour](
	[Prodmonth] [numeric](7, 0) NULL,
	[theLevel] [varchar](5) NULL,
	[SectionID_2] [varchar](20) NULL,
	[SectionID_1] [varchar](20) NULL,
	[PlanValue] [int] NULL,
	[AtWorkPlan] [int] NULL,
	[InServicePlan] [int] NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[REVISEDPLANNING_SECURITY](
	[Section] [varchar](150) NOT NULL,
	[UserID] [varchar](150) NULL,
	[Department] [varchar](50) NOT NULL,
	[SecurityType] [int] NULL,
	[ApprovalRequired] [bit] NULL,
	[Authorize] [bit] NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[REVISEDPLANNING_USERSECURITY_ACTIONS](
	[ChangeID] [int] NOT NULL,
	[UserID] [varchar](150) NULL,
	[Active] [bit] NULL,
	[Department] [varchar](50) NULL,
	[Section] [varchar](50) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SUNDRYMINING_ADDINFO](
	[SMID] [numeric](7, 0) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SUNDRYMINING_ADDINFO] PRIMARY KEY CLUSTERED 
(
	[SMID] ASC,
	[Description] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SUNDRYMINING_MEASUREMENT](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[SMID] [numeric](7, 0) NOT NULL,
	[OrgunitDay] [varchar](50) NOT NULL,
	[Units] [numeric](10, 2) NULL,
	[MetresAdvance] [numeric](10, 3) NULL,
 CONSTRAINT [PK_SUNDRYMINING_Measurement] PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[SectionID] ASC,
	[WorkplaceID] ASC,
	[Activity] ASC,
	[SMID] ASC,
	[OrgunitDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SUNDRYMINING_TYPE](
	[SMID] [numeric](7, 0) NOT NULL,
	[SMDescription] [varchar](100) NULL,
	[UnitBase] [numeric](7, 0) NULL,
	[Efficiency] [numeric](5, 2) NULL,
	[Deleted] [varchar](2) NULL,
 CONSTRAINT [PK_SUNDRYMINING_TYPE] PRIMARY KEY CLUSTERED 
(
	[SMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[SUNDRYMINING_UNIT](
	[UnitBase] [numeric](7, 0) NOT NULL,
	[Description] [varchar](20) NULL,
 CONSTRAINT [PK_SUNDRYMINING_UNIT] PRIMARY KEY CLUSTERED 
(
	[UnitBase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[PREPLANNING_NOTIFICATION_SECURITY] ADD  DEFAULT ((0)) FOR [CrewChange]
GO
ALTER TABLE [dbo].[PREPLANNING_NOTIFICATION_SECURITY] ADD  DEFAULT ((0)) FOR [CallChange]
GO
ALTER TABLE [dbo].[PREPLANNING_NOTIFICATION_SECURITY] ADD  DEFAULT ((0)) FOR [StopWPChange]
GO
ALTER TABLE [dbo].[PREPLANNING_NOTIFICATION_SECURITY] ADD  DEFAULT ((0)) FOR [NewWPChange]
GO
ALTER TABLE [dbo].[PREPLANNING_NOTIFICATION_SECURITY] ADD  DEFAULT ((0)) FOR [WPMove]
GO
ALTER TABLE [dbo].[SUNDRYMINING_ADDINFO]  WITH CHECK ADD  CONSTRAINT [FK_SUNDRYMINING_ADDINFO_SUNDRYMINING_TYPE] FOREIGN KEY([SMID])
REFERENCES [dbo].[SUNDRYMINING_TYPE] ([SMID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SUNDRYMINING_ADDINFO] CHECK CONSTRAINT [FK_SUNDRYMINING_ADDINFO_SUNDRYMINING_TYPE]
GO
ALTER TABLE [dbo].[SUNDRYMINING_MEASUREMENT]  WITH CHECK ADD  CONSTRAINT [FK_SUNDRYMINING_MEASUREMENT_SUNDRYMINING_TYPE] FOREIGN KEY([SMID])
REFERENCES [dbo].[SUNDRYMINING_TYPE] ([SMID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SUNDRYMINING_MEASUREMENT] CHECK CONSTRAINT [FK_SUNDRYMINING_MEASUREMENT_SUNDRYMINING_TYPE]
GO
ALTER TABLE [dbo].[SUNDRYMINING_MEASUREMENT]  WITH CHECK ADD  CONSTRAINT [FK_SUNDRYMINING_MEASUREMENT_WORKPLACE] FOREIGN KEY([WorkplaceID])
REFERENCES [dbo].[WORKPLACE] ([WorkplaceID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SUNDRYMINING_MEASUREMENT] CHECK CONSTRAINT [FK_SUNDRYMINING_MEASUREMENT_WORKPLACE]
GO
ALTER TABLE [dbo].[SUNDRYMINING_TYPE]  WITH CHECK ADD  CONSTRAINT [FK_SUNDRYMINING_SUNDRYMINING_UNIT] FOREIGN KEY([UnitBase])
REFERENCES [dbo].[SUNDRYMINING_UNIT] ([UnitBase])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SUNDRYMINING_TYPE] CHECK CONSTRAINT [FK_SUNDRYMINING_SUNDRYMINING_UNIT]
GO

CREATE TABLE [dbo].[BOOKINGHoisting](
	[CalendarDate] [datetime] NULL,
	[OreFlowID] [varchar](10) NULL,
	[MillMonth] [numeric](7, 0) NULL,
	[ReefSkips] [numeric](18, 0) NULL,
	[ReefTons] [numeric](7, 0) NULL,
	[ReefFact] [numeric](13, 3) NULL,
	[WasteSkips] [numeric](18, 0) NULL,
	[WasteTons] [numeric](7, 0) NULL,
	[WasteFact] [numeric](13, 3) NULL,
	[Remarks] [varchar](120) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[BOOKINGMilling](
	[MillMonth] [numeric](7, 0) NULL,
	[OreflowID] [varchar](10) NULL,
	[CalendarDate] [datetime] NULL,
	[TonsTreated] [numeric](13, 0) NULL,
	[TonsToPlant] [numeric](13, 0) NULL,
	[PlannedTonsTreated] [numeric](13, 0) NULL,
	[PlannedTonsToPlant] [numeric](13, 0) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[BOOKINGTramming](
	[MillMonth] [numeric](7, 0) NULL,
	[SectionID] [varchar](20) NULL,
	[CalendarDate] [datetime] NULL,
	[Shift] [char](1) NULL,
	[ReefWaste] [char](1) NULL,
	[Factor] [numeric](7, 1) NULL,
	[Tons] [numeric](7, 0) NULL,
	[Hoppers] [numeric](18, 0) NULL,
	[Activity] [numeric](7, 0) NULL,
	[DTons] [numeric](7, 0) NULL,
	[DHoppers] [numeric](7, 0) NULL,
	[ATons] [numeric](7, 0) NULL,
	[AHoppers] [numeric](7, 0) NULL,
	[NTons] [numeric](7, 0) NULL,
	[NHoppers] [numeric](7, 0) NULL,
	[TopTons] [numeric](7, 0) NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[BOOKINGTramming] ADD [TrammingProblem] [varchar](100) NULL
ALTER TABLE [dbo].[BOOKINGTramming] ADD [GT] [numeric](7, 2) NULL
ALTER TABLE [dbo].[BOOKINGTramming] ADD [ImportedFromSmartrail] [bit] NULL DEFAULT ((0))

GO
SET ANSI_PADDING OFF

CREATE TABLE [dbo].[BookingTrammingWP](
	[MillMonth] [numeric](7, 0) NULL,
	[SectionID] [varchar](20) NULL,
	[WorkplaceID] [varchar](12) NULL,
	[CalendarDate] [datetime] NULL,
	[ReefWaste] [char](1) NULL,
	[Tons] [numeric](7, 0) NULL,
	[Activity] [numeric](7, 0) NULL,
	[TopTons] [numeric](7, 0) NULL,
	[ImportedFromSmartrail] [bit] NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[BUS_PLANNING_STORAGES](
	[ProdMonth] [varchar](10) NOT NULL,
	[Residue] [numeric](20, 1) NULL,
	[FlushingTons] [numeric](20, 1) NULL,
	[FlushingContent] [numeric](20, 1) NULL,
	[OreFlowID] [nvarchar](10) NOT NULL,
	[DevWasteTonstoMill] [numeric](20, 1) NULL,
	[Discrepency] [numeric](20, 1) NULL,
	[MCF] [numeric](20, 1) NULL,
	[PRF] [numeric](20, 1) NULL,
 CONSTRAINT [PK_BUS_PLANNING_STORAGES] PRIMARY KEY CLUSTERED 
(
	[ProdMonth] ASC,
	[OreFlowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[KRIGING](
	[ProdMonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](20) NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[WeekNo] [numeric](7, 0) NOT NULL,
	[ChannelWidth] [numeric](18, 0) NULL,
	[StopeWidth] [numeric](18, 0) NULL,
	[CMGT] [numeric](18, 0) NULL,
	[GT] [numeric](7, 2) NULL,
	[EndHeight] [numeric](5, 1) NULL,
	[EndWidth] [numeric](5, 1) NULL,
	[UserID] varchar(50) null,
	[CreateDate] Datetime null
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[PLANNING_STORAGES](
	[ProdMonth] [varchar](10) NOT NULL,
	[Residue] [numeric](20, 1) NULL,
	[FlushingTons] [numeric](20, 1) NULL,
	[FlushingContent] [numeric](20, 1) NULL,
	[OreFlowID] [nvarchar](10) NOT NULL,
	[DevWasteTonstoMill] [numeric](20, 1) NULL,
	[Discrepency] [numeric](20, 1) NULL,
	[MCF] [numeric](20, 1) NULL,
	[PRF] [numeric](20, 1) NULL,
 CONSTRAINT [PK_PLANNING_STORAGES] PRIMARY KEY CLUSTERED 
(
	[ProdMonth] ASC,
	[OreFlowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SICCAPTURE](
	[SICCapKey] [int] IDENTITY(1,1) NOT NULL,
	[SICKey] [varchar](30) NOT NULL,
	[CalendarDate] [datetime] NULL,
	[SectionID] [varchar](20) NULL,
	[SortHeading] [varchar](20) NULL,
	[MillMonth] [numeric](7, 0) NULL,
	[Value] [numeric](11, 4) NULL,
	[WorkplaceID] [varchar](12) NULL,
	[ProblemCode] [varchar](250) NULL,
	[ProgValue] [numeric](11, 4) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[SICCLEANED](
	[CalendarDate] [datetime] NULL,
	[KPI] [varchar](20) NULL,
	[Workplaceid] [varchar](12) NULL,
	[Sectionid] [varchar](20) NULL,
	[Heading] [varchar](100) NULL,
	[SortHeading] [varchar](20) NULL,
	[MillMonth] [numeric](7, 0) NULL,
	[Value] [numeric](11, 4) NULL,
	[ProblemID] [varchar](30) NULL
) ON [PRIMARY]


CREATE TABLE [dbo].[SURVEY](
	[WorkplaceID] [varchar](12) NOT NULL,
	[SectionID] [varchar](20) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[CalendarDate] [datetime] NOT NULL,
	[ProdMonth] [varchar](10) NOT NULL,
	[SeqNo] [numeric](7, 0) NOT NULL,
	[Dip] [numeric](5, 1) NULL,
	[BallDepth] [numeric](5, 2) NULL,
	[MineMethod] [int] NULL,
	[Indicator] [int] NULL,
	[Density] [numeric](13, 3) NULL,
	[SType] [int] NULL,
	[CrewMorning] [varchar](20) NULL,
	[CrewAfternoon] [varchar](20) NULL,
	[CrewEvening] [varchar](20) NULL,
	[PegNo] [varchar](12) NULL,
	[PegValue] [numeric](7, 1) NULL,
	[PegToFace] [numeric](7, 1) NULL,
	[ProgFrom] [numeric](7, 1) NULL,
	[ProgTo] [numeric](7, 1) NULL,
	[ReefMetres] [numeric](6, 3) NULL,
	[WasteMetres] [numeric](6, 3) NULL,
	[MeasWidth] [numeric](6, 3) NULL,
	[MeasHeight] [numeric](6, 3) NULL,
	[PlanWidth] [numeric](6, 3) NULL,
	[PlanHeight] [numeric](6, 3) NULL,
	[LockUpTons] [numeric](5, 1) NULL,
	[Blockno] [varchar](50) NULL,
	[BlockWidth] [numeric](7, 0) NULL,
	[BlockValue] [numeric](7, 2) NULL,
	[StopeSqm] [numeric](7, 2) NULL,
	[StopeSqmOSF] [numeric](7, 2) NULL,
	[StopeSqmOS] [numeric](7, 2) NULL,
	[StopeSqmTotal] [numeric](7, 2) NULL,
	[LedgeSqm] [numeric](7, 2) NULL,
	[LedgeSqmOSF] [numeric](7, 2) NULL,
	[LedgeSqmOS] [numeric](7, 2) NULL,
	[LedgeSqmTotal] [numeric](7, 2) NULL,
	[StopeFL] [numeric](7, 0) NULL,
	[StopeFLOS] [numeric](7, 0) NULL,
	[LedgeFL] [numeric](7, 0) NULL,
	[LedgeFLOS] [numeric](7, 0) NULL,
	[FLOSTotal] [numeric](7, 0) NULL,
	[MeasAdvSW] [numeric](7, 0) NULL,
	[SWIdeal] [numeric](7, 0) NULL,
	[FLTotal] [numeric](7, 0) NULL,
	[SqmConvTotal] [numeric](7, 0) NULL,
	[SqmOSFTotal] [numeric](7, 0) NULL,
	[SqmOSTotal] [numeric](7, 0) NULL,
	[SqmTotal] [numeric](7, 0) NULL,
	[ExtraType] [int] NULL,
	[CubicsMetres] [numeric](7, 2) NULL,
	[CubicsReef] [numeric](7, 0) NULL,
	[CubicsWaste] [numeric](7, 0) NULL,
	[Labour] [numeric](8, 0) NULL,
	[PaidUnpaid] [varchar](1) NULL,
	[FW] [numeric](7, 0) NULL,
	[CW] [numeric](7, 0) NULL,
	[HW] [numeric](7, 0) NULL,
	[SWSqm] [numeric](7, 0) NULL,
	[SWOSF] [numeric](7, 0) NULL,
	[SWOS] [numeric](7, 0) NULL,
	[cmgt] [numeric](7, 0) NULL,
	[Destination] [numeric](2, 0) NULL,
	[Cleantype] [int] NULL,
	[CleanSqm] [numeric](7, 0) NULL,
	[CleanDist] [numeric](7, 2) NULL,
	[CleanVamp] [numeric](7, 2) NULL,
	[CleanTons] [numeric](7, 0) NULL,
	[Cleangt] [numeric](7, 2) NULL,
	[CleanContents] [numeric](7, 0) NULL,
	[TonsReef] [numeric](9, 2) NULL,
	[TonsWaste] [numeric](9, 2) NULL,
	[TonsOSF] [numeric](9, 2) NULL,
	[TonsOS] [numeric](9, 2) NULL,
	[TonsTotal] [numeric](9, 2) NULL,
	[Payment] [numeric](2, 0) NULL,
	[PlanNo] [varchar](20) NULL,
	[MainMetres] [numeric](7, 2) NULL,
	[TotalMetres] [numeric](7, 2) NULL,
	[ValHeight] [numeric](7, 2) NULL,
	[GT] [numeric](7, 2) NULL,
	[OpenUpSqm] [numeric](7, 0) NULL,
	[ReefDevSqm] [numeric](7, 0) NULL,
	[OpenUpcmgt] [numeric](7, 0) NULL,
	[ReefDevcmgt] [numeric](7, 0) NULL,
	[OpenUpFL] [numeric](7, 0) NULL,
	[ReefDevFL] [numeric](7, 0) NULL,
	[OpenUpEquip] [numeric](7, 0) NULL,
	[ReefDevEquip] [numeric](7, 0) NULL,
	[TonsExtra] [numeric](9, 2) NULL,
	[TonsWasteBroken] [numeric](9, 2) NULL,
	[TonsTrammed] [numeric](9, 2) NULL,
	[TonsBallast] [numeric](9, 2) NULL,
	[CleanDepth] [numeric](7, 0) NULL,
	[TotalContent] [numeric](13, 2) NULL,
	[TonsCubicsReef] [numeric](9, 2) NULL,
	[TonsCubicsWaste] [numeric](9, 2) NULL,
	[TonsReefBroken] [numeric](9, 2) NULL,
	[OreflowID] [varchar](10) NULL,
	[CleaningCrew] [varchar](180) NULL,
	[TrammingCrew] [varchar](50) NULL,
	[HlgeMaintainanceCrew] [varchar](50) NULL,
	[RiggerCrew] [varchar](50) NULL,
	[RseCleaningCrew] [varchar](50) NULL,
	[M2NOTTOBEPAIDFORMINER] [numeric](7, 0) NULL,
	[MINSPRETARGETSW2] [numeric](7, 0) NULL,
	[MidmonthIndicator] [numeric](7, 2) NULL,
	[DISTFROMFACEMIDMONTH] [numeric](7, 2) NULL,
	[SweepingPenalty] [numeric](7, 2) NULL,
	[SF1SF2] [varchar](50) NULL,
	[MetersNotToBePaid] [numeric](7, 2) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[TM1IMPORT](
	[VERSION_DSC] [varchar](50) NULL,
	[COMPANY_NAME] [varchar](50) NULL,
	[ACCOUNT_NO] [varchar](50) NULL,
	[PROJECT_NO] [varchar](50) NULL,
	[PROJECT_TASK] [varchar](50) NULL,
	[RESP_CENTER] [varchar](50) NULL,
	[OCCUPATION_CD] [varchar](50) NULL,
	[FISCAL_PERIOD] [varchar](50) NULL,
	[SOURCE_IND] [varchar](50) NULL,
	[BUD_VAL_TYPE] [varchar](50) NULL,
	[BUD_VAL] [float] NULL,
	[POST_ALLOC_BUD_VAL] [float] NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[TOPPANELSSELECTED](
	[TopPanelID] [int] IDENTITY(1,1) NOT NULL,
	[Prodmonth] [nchar](10) NOT NULL,
	[WorkplaceID] [varchar](20) NOT NULL,
	[Activity] [varchar](1) NULL,
	[SectionID] [varchar](20) NOT NULL,
 CONSTRAINT [PK_TopPanelsSelected] PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[WorkplaceID] ASC,
	[SectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[PRODMONTH](
	[ProdMonth] [varchar](10) NOT NULL,
	[QuarterName] [varchar](50) NULL,
 CONSTRAINT [PK_PRODMONTH] PRIMARY KEY CLUSTERED 
(
	[ProdMonth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[GENERICREPORTQUARTER](
	[QuarterName] [varchar](50) NOT NULL,
	[AnnualName] [varchar](50) NULL,
 CONSTRAINT [PK_GENERICREPORTQUARTER] PRIMARY KEY CLUSTERED 
(
	[QuarterName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[GENERICREPORTANNUAL](
	[AnnualName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GENERICREPORTANNUAL] PRIMARY KEY CLUSTERED 
(
	[AnnualName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[PRODMONTH]  WITH CHECK ADD  CONSTRAINT [FK_PRODMONTH_GENERICREPORTQUARTER] FOREIGN KEY([QuarterName])
REFERENCES [dbo].[GENERICREPORTQUARTER] ([QuarterName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PRODMONTH] CHECK CONSTRAINT [FK_PRODMONTH_GENERICREPORTQUARTER]
GO


ALTER TABLE [dbo].[GENERICREPORTQUARTER]  WITH CHECK ADD  CONSTRAINT [FK_GENERICREPORTQUARTER_GENERICREPORTANNUAL] FOREIGN KEY([AnnualName])
REFERENCES [dbo].[GENERICREPORTANNUAL] ([AnnualName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[GENERICREPORTQUARTER] CHECK CONSTRAINT [FK_GENERICREPORTQUARTER_GENERICREPORTANNUAL]
GO



ALTER TABLE [dbo].[BookingTrammingWP] ADD  DEFAULT ((0)) FOR [ImportedFromSmartrail]
GO
ALTER TABLE [dbo].[BUS_PLANNING_STORAGES]  WITH CHECK ADD  CONSTRAINT [FK_BUS_PLANNING_STORAGES_PRODMONTH] FOREIGN KEY([ProdMonth])
REFERENCES [dbo].[PRODMONTH] ([ProdMonth])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BUS_PLANNING_STORAGES] CHECK CONSTRAINT [FK_BUS_PLANNING_STORAGES_PRODMONTH]
GO
ALTER TABLE [dbo].[PLANNING_STORAGES]  WITH CHECK ADD  CONSTRAINT [FK_PLANNING_STORAGES_PRODMONTH] FOREIGN KEY([ProdMonth])
REFERENCES [dbo].[PRODMONTH] ([ProdMonth])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PLANNING_STORAGES] CHECK CONSTRAINT [FK_PLANNING_STORAGES_PRODMONTH]
GO


CREATE TABLE [dbo].[PLANNING](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[PlanCode] [char](2) NOT NULL,
	[IsCubics] [char](1) NOT NULL,
	[ShiftDay] [numeric](5, 0) NULL,
	[SQM] [numeric](5, 0) NULL,
	[ReefSQM] [int] NULL,
	[WasteSQM] [int] NULL,
	[Metresadvance] [numeric](9, 3) NULL,
	[ReefAdv] [numeric](10, 3) NULL,
	[WasteAdv] [numeric](10, 3) NULL,
	[Tons] [numeric](10, 3) NULL,
	[ReefTons] [numeric](10, 3) NULL,
	[WasteTons] [numeric](10, 3) NULL,
	[Grams] [numeric](9, 3) NULL,
	[FL] [numeric](9, 3) NULL,
	[ReefFL] [int] NULL,
	[WasteFL] [int] NULL,
	[SW] [numeric](5, 0) NULL,
	[CW] [numeric](5, 0) NULL,
	[CMGT] [numeric](5, 0) NULL,
	[GT] [numeric](8, 2) NULL,
	[CubicMetres] [numeric](13, 3) NULL,
	[Cubics] [numeric](10, 3) NULL,
	[ReefCubics] [numeric](10, 3) NULL,
	[WasteCubics] [numeric](10, 3) NULL,
	[CubicTons] [numeric](10, 3) NULL,
	[CubicGrams] [numeric](10, 3) NULL,
	[CubicDepth] [numeric](10, 3) NULL,
	[CubicGT] [numeric](10, 3) NULL,
	[Backfill] [numeric](13, 3) NULL,
	[BookTons] [numeric](10, 3) NULL,
	[BookReefTons] [numeric](10, 3) NULL,
	[BookWasteTons] [numeric](10, 3) NULL,
	[BookGrams] [numeric](13, 3) NULL,
	[BookMetresadvance] [numeric](6, 3) NULL,
	[BookReefAdv] [numeric](13, 3) NULL,
	[BookWasteAdv] [numeric](13, 3) NULL,
	[BookSQM] [numeric](7, 0) NULL,
	[BookReefSQM] [numeric](7, 0) NULL,
	[BookWasteSQM] [numeric](7, 0) NULL,
	[BookVolume] [int] NULL,
	[BookReefVolume] [int] NULL,
	[BookWasteVolume] [int] NULL,
	[BookCubicmetres] [numeric](7, 0) NULL,
	[BookUpdatedate] [datetime] NULL,
	[BookReef] [varchar](2) NULL,
	[BookFL] [numeric](13, 3) NULL,
	[BookSW] [numeric](7, 0) NULL,
	[Bookcmgt] [int] NULL,
	[BookGT] [numeric](13, 3) NULL,
	[BookCW] [int] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[PLANNING] ADD [MOCycle] [varchar](5) NULL
ALTER TABLE [dbo].[PLANNING] ADD [MOCycleCube] [varchar](5) NULL
ALTER TABLE [dbo].[PLANNING] ADD [BookCubicTons] [numeric](9, 3) NULL
ALTER TABLE [dbo].[PLANNING] ADD [BookCubicGrams] [numeric](9, 3) NULL
ALTER TABLE [dbo].[PLANNING] ADD [BookCubicGT] [numeric](9, 3) NULL
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[SectionID] ASC,
	[WorkplaceID] ASC,
	[Activity] ASC,
	[Calendardate] ASC,
	[PlanCode] ASC,
	[IsCubics] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[PEG](
	[PegID] [varchar](10) NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[Value] [numeric](18, 2) NULL,
	[Letter1] [varchar](50) NULL,
	[Letter2] [varchar](50) NULL,
	[Letter3] [varchar](50) NULL,
	[CalendarDate] datetime null
 CONSTRAINT [PK_PEGWORKPLACE] PRIMARY KEY CLUSTERED 
(
	[PegID] ASC,
	[WorkplaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PEG]  WITH NOCHECK ADD  CONSTRAINT [FK_PEG_WORKPLACE] FOREIGN KEY([WorkplaceID])
REFERENCES [dbo].[WORKPLACE] ([WorkplaceID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PEG] CHECK CONSTRAINT [FK_PEG_WORKPLACE]
GO


CREATE TABLE [dbo].[CODE_TRAMMINGPROBLEM](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProblemCode] [varchar](max) NULL,
 CONSTRAINT [PK_CODE_TRAMMINGPROBLEM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[BUSINESSPLAN_LOCKS](
	[Year] [int] NOT NULL,
	[Activity] [int] NOT NULL,
	[ProdMonthStart] [int] NOT NULL,
	[ProdMonthEnd] [int] NOT NULL,
	[IsLocked] [bit] NOT NULL
	)
GO



CREATE TABLE [dbo].[SURVEY_LOCKS](
	[ProdMonth] [int] NOT NULL,
	[Locked] [bit] NOT NULL,
	[DateLocked] [datetime] NOT NULL,
	[LockedByID] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Survey_Locks] PRIMARY KEY CLUSTERED 
(
	[ProdMonth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[CheckTrammingProblems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProblemCode] [varchar](max) NULL,
 CONSTRAINT [PK_CheckTrammingProblems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[PLANHOIST](
	[MillMonth] [numeric](7, 0) NULL,
	[OreflowID] [varchar](10) NULL,
	[PlanTons] [numeric](13, 0) NULL,
	[PlanBeltGrade] [numeric](13, 2) NULL,
	[PlanGold] [numeric](13, 3) NULL
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[BOOKINGHOIST](
	[MillMonth] [numeric](7, 0) NULL,
	[OreflowID] [varchar](10) NULL,
	[CalendarDate] [datetime] NULL,
	[ReefTons] [numeric](13, 0) NULL,
	[WasteTons] [numeric](13, 0) NULL,
	[BeltGrade] [numeric](13, 2) NULL,
	[Gold] [numeric](13, 3) NULL,
	[PlanBeltGrade] [numeric](13, 2) NULL,
	[PlanTons] [numeric](13, 0) NULL,
	[PlanGold] [numeric](13, 3) NULL
) ON [PRIMARY]

GO

CREATE View [dbo].[view_CALTYPE]
as
Select 'Mill' CalendarCode, CalendarDate,
WorkingDay
from Caltype
where CalendarCode ='Mill'
and WorkingDay = 'Y'
GO


CREATE Procedure [dbo].[sp_BOOKINGMillingCycle]
@MillMonth varchar(50), 
@OreflowID varchar(50),
@TonsTreated numeric(10, 2),
@TonsToPlant numeric(10, 2)

as
declare 
@TotalShfts int,
@StartDate datetime, 
@EndDate datetime

declare
@RemainderTonsTreated numeric(10, 2),
@RemainderTonsToPlant numeric(10, 2),
@TotalShifts int,
@ShiftNo int,
@TotalBlasts int,
@BlastNo int,
@TonsTreatedPerBlast numeric(10, 2),
@TonsToPlantPerBlast numeric(10, 2),
@Remaining_Shifts Numeric(10),
@Remaining_Blasts Numeric(10),
@TheDate DateTime

select @StartDate = StartDate,
    @EndDate = EndDate,
    @TotalShifts = TotalShifts
from CALENDARMILL 
where MillMonth = @MillMonth 

CREATE TABLE #MILLING_BOOKCYCLE(MillMonth varchar(6), OreflowID varchar(10), CalendarDate datetime NULL,
PlannedTonsTreated numeric(10, 2), PlannedTonsToPlant numeric(10, 2)) 

DECLARE Shaft_Cursor CURSOR FOR

    select c.CalendarDate from CALENDARMILL m
    inner join view_CALTYPE c on
    c.CalendarCode = m.CalendarCode and
    c.CalendarDate >= m.StartDate and
    c.CalendarDate <= m.EndDate
    where m.MillMonth = @MillMonth and c.WorkingDay = 'Y'

OPEN Shaft_Cursor;
FETCH NEXT FROM Shaft_Cursor
into @TheDate

    Set @ShiftNo = 0
    Set @BlastNo = 0
    set @RemainderTonsTreated = @TonsTreated
    set @RemainderTonsToPlant = @TonsToPlant
    Set @TotalBlasts = @TotalShifts
    Set @Remaining_Blasts = @TotalBlasts
    WHILE @@FETCH_STATUS = 0
    BEGIN
        set @Remaining_Shifts = @TotalShifts - @ShiftNo

        set @Remaining_Blasts = @TotalBlasts - @BlastNo

        set @TonsTreatedPerBlast = case when @Remaining_Shifts = 0 then 0 else Convert(Int,round(@RemainderTonsTreated/@Remaining_Blasts, 0)) end
        set @TonsToPlantPerBlast = case when @Remaining_Shifts = 0 then 0 else Convert(Int,round(@RemainderTonsToPlant/@Remaining_Blasts, 0)) end

        set @BlastNo = @BlastNo + 1

        set @RemainderTonsTreated = @RemainderTonsTreated - @TonsTreatedPerBlast
        set @RemainderTonsToPlant = @RemainderTonsToPlant - @TonsToPlantPerBlast

        set @ShiftNo = @ShiftNo + 1

		IF EXISTS (select * from #MILLING_BOOKCYCLE 
		where MillMonth = @MillMonth and OreflowID = @OreflowID and CalendarDate = @TheDate)
			BEGIN
				update #MILLING_BOOKCYCLE set
				PlannedTonsTreated = isnull(@TonsTreatedPerBlast, 0),
				PlannedTonsToPlant = isnull(@TonsToPlantPerBlast, 0)
				where MillMonth = @MillMonth and OreflowID = @OreflowID and CalendarDate = @TheDate
	 
			END
		ELSE
			BEGIN
				insert into #MILLING_BOOKCYCLE (MillMonth, OreflowID, CalendarDate, PlannedTonsTreated, PlannedTonsToPlant)
        
				select @MillMonth, @OreflowID, @TheDate CALENDARDATE, 
				PlannedTonsTreated = isnull(@TonsTreatedPerBlast, 0),
				PlannedTonsToPlant = isnull(@TonsToPlantPerBlast, 0)
			END



        FETCH NEXT FROM Shaft_Cursor
           into @TheDate;
    END
CLOSE Shaft_Cursor;
DEALLOCATE Shaft_Cursor;

if exists (select * from BOOKINGMilling where MillMonth = @MillMonth and OreflowID = @OreflowID)
	BEGIN
		update BOOKINGMilling set PlannedTonsTreated = @TonsTreatedPerBlast,
		PlannedTonsToPlant = @TonsToPlantPerBlast where MillMonth = @MillMonth and OreflowID = @OreflowID
	END

ELSE
	BEGIN -- select * from BOOKINGMilling
		insert into BOOKINGMilling(MillMonth, OreflowID, CalendarDate, PlannedTonsTreated, PlannedTonsToPlant)
	select MillMonth, OreflowID, CalendarDate, PlannedTonsTreated, PlannedTonsToPlant from #MILLING_BOOKCYCLE
	END


select * from #MILLING_BOOKCYCLE

DROP Table #MILLING_BOOKCYCLE
GO

CREATE Procedure [dbo].[sp_BOOKINGHoistingCycle]
@MillMonth varchar(50), 
@Shaft varchar(50),
@PlanTons numeric(10,0),
@PlanBeltGrade numeric(10, 2),
@PlanGold numeric(10, 3)

as
declare 
@TotalShfts int,
@StartDate datetime, 
@EndDate datetime

declare
@RemainderPlanTons numeric(10, 0),
@RemainderPlanGold numeric(10, 3),
@TotalShifts int,
@ShiftNo int,
@TotalBlasts int,
@BlastNo int,
@PlanTonsPerBlast numeric(10, 0),
@PlanBeltGradePerBlast numeric(10, 2),
@PlanGoldPerBlast numeric(10, 3),
@Remaining_Shifts Numeric(10),
@Remaining_Blasts Numeric(10),
@TheDate DateTime

select @StartDate = StartDate,
    @EndDate = EndDate,
    @TotalShifts = TotalShifts
from CALENDARMILL 
where MillMonth = @MillMonth 

CREATE TABLE #HOISTING_BOOKCYCLE(MillMonth varchar(6), Shaft varchar(50), CalendarDate datetime NULL,
PlanTons numeric(10, 0), PlanBeltGrade numeric(10, 2), PlanGold numeric(10, 3)) 

DECLARE Shaft_Cursor CURSOR FOR

    select c.CalendarDate from CALENDARMILL m
    inner join view_CALTYPE c on
    c.CalendarCode = m.CalendarCode and
    c.CalendarDate >= m.StartDate and
    c.CalendarDate <= m.EndDate
    where m.MillMonth = @MillMonth and c.WorkingDay = 'Y'

OPEN Shaft_Cursor;
FETCH NEXT FROM Shaft_Cursor
into @TheDate

    Set @ShiftNo = 0
    Set @BlastNo = 0
	set @RemainderPlanTons = @PlanTons
    set @RemainderPlanGold = @PlanGold
    Set @TotalBlasts = @TotalShifts
    Set @Remaining_Blasts = @TotalBlasts
    WHILE @@FETCH_STATUS = 0
    BEGIN
        set @Remaining_Shifts = @TotalShifts - @ShiftNo

        set @Remaining_Blasts = @TotalBlasts - @BlastNo

		set @PlanTonsPerBlast = case when @Remaining_Shifts = 0 then 0 else Convert(Int,round(@RemainderPlanTons/@Remaining_Blasts,0)) end
        set @PlanGoldPerBlast = case when @Remaining_Shifts = 0 then 0 else Convert(Int,round(@RemainderPlanGold/@Remaining_Blasts,0)) end

        set @BlastNo = @BlastNo + 1

        set @RemainderPlanTons = @RemainderPlanTons - @PlanTonsPerBlast
		set @RemainderPlanGold = @RemainderPlanGold - @PlanGoldPerBlast

        set @ShiftNo = @ShiftNo + 1

		IF EXISTS (select * from #HOISTING_BOOKCYCLE 
		where MillMonth = @MillMonth and Shaft = @Shaft and CalendarDate = @TheDate)
			BEGIN
				update #HOISTING_BOOKCYCLE set
				PlanTons = isnull(@PlanTonsPerBlast, 0),
				PlanBeltGrade = isnull(@PlanBeltGrade, 0),
				PlanGold = isnull(@PlanGoldPerBlast, 0)
				where MillMonth = @MillMonth and Shaft = @Shaft and CalendarDate = @TheDate
	 
			END
		ELSE
			BEGIN
				insert into #HOISTING_BOOKCYCLE (MillMonth, Shaft, CalendarDate, PlanTons, PlanBeltGrade, PlanGold)
        
				select @MillMonth, @Shaft, @TheDate CALENDARDATE, 
				PlanTons = isnull(@PlanTonsPerBlast, 0),
				PlanBeltGrade = isnull(@PlanBeltGrade, 0),
				PlanGold = isnull(@PlanGoldPerBlast, 0)
			END



        FETCH NEXT FROM Shaft_Cursor
           into @TheDate;
    END
CLOSE Shaft_Cursor;
DEALLOCATE Shaft_Cursor;

if exists (select * from BOOKINGHoist where MillMonth = @MillMonth and OreflowID = @Shaft)
	BEGIN
		update BOOKINGHoist set PlanTons = @PlanTonsPerBlast,
		PlanBeltGrade = @PlanBeltGrade, PlanGold = @PlanGoldPerBlast 
		where MillMonth = @MillMonth and OreflowID = @Shaft
	END

ELSE
	BEGIN
		insert into BOOKINGHoist (MillMonth, OreflowID, CalendarDate, PlanTons, PlanBeltGrade, PlanGold)
	select MillMonth, Shaft, CalendarDate, PlanTons, PlanBeltGrade, PlanGold from #HOISTING_BOOKCYCLE
	END


select * from #HOISTING_BOOKCYCLE

DROP Table #HOISTING_BOOKCYCLE


GO



CREATE TABLE [dbo].[USERS](
	[UserID] [varchar](50) NOT NULL,
	[BackDateBooking] [int] NULL,
	[UpdateCyclePlan] [int] NULL,
	[RemAction] [bit] NULL CONSTRAINT [DF_USERS_RemActTrack]  DEFAULT ('N'),
	[RemOverdue] [bit] NULL CONSTRAINT [DF_USERS_RemOverdue]  DEFAULT ('N'),
	[DaysBackdate] [int] NULL,
	[OldgoldGrade] [bit] NULL,
	[DaysLockCurplan] [int] NULL,
	[Updatecrew] [bit] NULL,
	[ForceBooking] [bit] NULL,
	[ChiefPlanner] [bit] NULL,
	[BookingType] [bit] NULL,
	[ForecastBook] [bit] NULL,
	[Active] [bit] NULL,
	[BackdatedRevisedPlanning] [bit] NULL,
	[Authorize] [bit] NULL,
	[LockPlan] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[USERS_SECTION](
	[UserID] [varchar](50) NOT NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[USERS_SECTION] ADD [SectionID] [varchar](10) NOT NULL
ALTER TABLE [dbo].[USERS_SECTION] ADD [LinkType] [char](1) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[SectionID] ASC,
	[LinkType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[USERS_SECTION]  WITH CHECK ADD  CONSTRAINT [FK_USERS_SECTION_USERS] FOREIGN KEY([UserID])
REFERENCES [dbo].[USERS] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[USERS_SECTION] CHECK CONSTRAINT [FK_USERS_SECTION_USERS]
GO

CREATE TABLE [dbo].[CODE_CYCLE](
	[CycleCode] [varchar](5),
	[Description] [varchar](50) NULL,
	[CanBlast] [bit] NULL,
	Primary Key ([CycleCode])
) ON [PRIMARY]

GO
Create Procedure [dbo].[sp_Get_Cycle]
--Declare
@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)

--DECLARE
--@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)
--SET @theProdmonth = 201706
--SET @theWorkplaceID = 'RE007667'
--SET @theSectionID = 'REAAHDA'

--Select * from planmonth where prodmonth = 201601 and workplaceid = '21924'


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
@Prodmonth VARCHAR(40),@WorkplaceID VARCHAR(40), @CycleValue VARCHAR(40), @CycleValueCude VARCHAR(40), @Calendardate VARCHAR(40), @MOCycle VARCHAR(40),@MOCycleCube VARCHAR(40), @WorkingDay VARCHAR(40), @theSQLCalDate varchar(max),@name VARCHAR(40),
@theInsert varchar(max), @dayCount int, @theSQLCycleVal varchar(max),@theSQLCycleValCube varchar(max),@theSQLMOCycle varchar(max),@theSQLMOCycleCube varchar(max),@theSQLWorkingDay varchar(max),@SQM numeric(10,3),@Metresadvance numeric(10,3),@Cubes numeric(10,3)



SET @theInsert = 'INSERT INTO #cycleData (Prodmonth,WorkplaceID,RowType,Name,SQM,Metresadvance,Cubes'


SET @dayCount = 1;
DECLARE db_Items CURSOR FOR 
SELECT PM.Prodmonth,PM.WorkplaceID
      ,CASE WHEN  PM.Activity in (0) and PM.SQM > 0 and PLAND.SQM IS NOT NULL  THEN PLAND.SQM 
	        WHEN  PM.Activity in (1) and PM.Metresadvance  >0 and PLAND.Metresadvance  IS NOT NULL  THEN PLAND.Metresadvance 
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
	  ,CASE WHEN PM.SQM IS NULL then 0 ELSE PM.SQM END SQM
	  ,CASE WHEN PM.Metresadvance IS NULL then 0 ELSE PM.Metresadvance END Metresadvance
	  ,CASE WHEN PM.[CubicMetres] IS NULL THEN 0 ELSE PM.[CubicMetres] END [CubicMetres]
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
FETCH NEXT FROM db_Items INTO @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes


SET @theSQLCalDate = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sDate'',''Date'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
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
SET @theSQLCycleVal = @theSQLCycleVal  + ',''' + Cast(@CycleValue as varchar(MAX)) + ''''
SET @theSQLCycleValCube = @theSQLCycleValCube  + ',''' + Cast(@CycleValueCude as varchar(MAX)) + ''''
SET @theSQLMOCycle = @theSQLMOCycle  + ',''' + Cast(@MOCycle as varchar(MAX)) + ''''
SET @theSQLMOCycleCube = @theSQLMOCycleCube  + ',''' + Cast(@MOCycleCube as varchar(MAX)) + ''''
--SET @theSQL = @theSQL + Cast(@Prodmonth as varchar(MAX))  + ',' + Cast(@WorkplaceID as varchar(MAX)) + ',' + Cast(@CycleValue as varchar(MAX))+ ',' + Cast(@Calendardate as varchar(MAX)) + ',' + Cast(@MOCycle as varchar(MAX)) + ',' + Cast(@WorkingDay as varchar(MAX))
SET @dayCount = @dayCount + 1
FETCH NEXT FROM db_Items INTO  @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes 
END

SET @theSQLCalDate = @theSQLCalDate + ')'
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

CREATE TABLE [dbo].[CODE_SECURITYTYPE](
	[SecurityTypeID] [int]  NOT NULL,
	[SecurityType] [varchar](50) NULL
) ON [PRIMARY]

GO
CREATE FUNCTION  [dbo].[IsEvalApproved]
    (
      @Prodmonth Varchar(7),
      @WorkplaceID Varchar(150),
      @SectionID_2 Varchar(150)
    )    

Returns int
as
BEGIN    
	DECLARE @Approved int

	select @Approved = 
    (SELECT COUNT(Prodmonth)
  FROM [PlanProt_DataApproved]
WHERE Prodmonth = @Prodmonth and
      WorkplaceID =  @WorkplaceID and
      TemplateID = 16 and
      SectionID = @SectionID_2 AND
	  Approved = CAST(1 AS BIT))	
RETURN @Approved	
END      


GO

Create FUNCTION  [dbo].[IsEvalApproved_Dev]
    (
      @Prodmonth Varchar(7),
      @WorkplaceID Varchar(150),
      @SectionID_2 Varchar(150)
    )    

Returns int
as
BEGIN    
	DECLARE @Approved int

	select @Approved = 
    (SELECT COUNT(Prodmonth)
  FROM [PlanProt_DataApproved]
WHERE Prodmonth = @Prodmonth and
      WorkplaceID =  @WorkplaceID and
      TemplateID = 14 and
      SectionID = @SectionID_2 AND
	  Approved = CAST(1 AS BIT))	
RETURN @Approved	
END   



GO

CREATE SYNONYM [dbo].[tblDepartments] FOR [Harmony_Syncromine].[DBO].[tblDepartments]
GO

CREATE SYNONYM [dbo].[tblUsers] FOR [Harmony_Syncromine].[DBO].[tblUsers]
GO

CREATE SYNONYM [dbo].[tblProfiles] FOR [Harmony_Syncromine].[DBO].[tblProfiles]
GO
CREATE View [dbo].[vw_Section_From_MO]
AS
SELECT Section_2.PRODMONTH,
  Section_2.SECTIONID SECTIONID_2, Section_2.NAME NAME_2,
  Section_3.SECTIONID SECTIONID_3, Section_3.NAME NAME_3,
  Section_4.SECTIONID SECTIONID_4, Section_4.NAME NAME_4,
  Section_5.SECTIONID SECTIONID_5, Section_5.NAME NAME_5
 FROM Section Section_2 
 INNER JOIN Section Section_3 ON 
  Section_2.PRODMONTH = Section_3.PRODMONTH 
  AND Section_2.ReportToSectionid = Section_3.SECTIONID
 INNER JOIN Section Section_4 ON 
  Section_3.PRODMONTH = Section_4.PRODMONTH
  AND Section_3.ReportToSectionid = Section_4.SECTIONID
 INNER JOIN Section Section_5 ON 
  Section_4.PRODMONTH = Section_5.PRODMONTH
  AND Section_4.ReportToSectionid = Section_5.SECTIONID
where Section_2.HIERARCHICALID = 4 
GO

CREATE View [dbo].[vw_Section_From_SB]
AS
SELECT Section_1.PRODMONTH,
  Section_1.SECTIONID SECTIONID_1, Section_1.NAME NAME_1,
  Section_2.SECTIONID SECTIONID_2, Section_2.NAME NAME_2,
  Section_3.SECTIONID SECTIONID_3, Section_3.NAME NAME_3,
  Section_4.SECTIONID SECTIONID_4, Section_4.NAME NAME_4,
  Section_5.SECTIONID SECTIONID_5, Section_5.NAME NAME_5
 FROM Section Section_1 
 inner join
  Section Section_2 on
  Section_1.PRODMONTH = Section_2.PRODMONTH 
  AND Section_1.ReportToSectionid = Section_2.SECTIONID
 INNER JOIN Section Section_3 ON 
  Section_2.PRODMONTH = Section_3.PRODMONTH 
  AND Section_2.ReportToSectionid = Section_3.SECTIONID
 INNER JOIN Section Section_4 ON 
  Section_3.PRODMONTH = Section_4.PRODMONTH
  AND Section_3.ReportToSectionid = Section_4.SECTIONID
 INNER JOIN Section Section_5 ON 
  Section_4.PRODMONTH = Section_5.PRODMONTH
  AND Section_4.ReportToSectionid = Section_5.SECTIONID
 INNER JOIN Section Section_6 ON 
  Section_5.PRODMONTH = Section_6.PRODMONTH
  AND Section_5.ReportToSectionid = Section_6.SECTIONID
where Section_2.HIERARCHICALID = 5

GO

CREATE View [dbo].[vw_Section_From_Shaft]
AS
 SELECT SECTION_4.PRODMONTH,
  SECTION_4.SECTIONID SECTIONID_4, SECTION_4.NAME NAME_4,
  SECTION_5.SECTIONID SECTIONID_5, SECTION_5.NAME NAME_5
 FROM SECTION SECTION_4  
 INNER JOIN SECTION SECTION_5 ON 
  SECTION_4.PRODMONTH = SECTION_5.PRODMONTH
  AND SECTION_4.ReportToSectionid = SECTION_5.SECTIONID
where SECTION_4.HIERARCHICALID = 2

GO

CREATE View [dbo].[vw_Section_From_UM]
AS
 SELECT SECTION_3.PRODMONTH,
  SECTION_3.SECTIONID SECTIONID_3, SECTION_3.NAME NAME_3,
  SECTION_4.SECTIONID SECTIONID_4, SECTION_4.NAME NAME_4,
  SECTION_5.SECTIONID SECTIONID_5, SECTION_5.NAME NAME_5
 FROM SECTION SECTION_3 
 INNER JOIN SECTION SECTION_4 ON 
  SECTION_3.PRODMONTH = SECTION_4.PRODMONTH
  AND SECTION_3.ReportToSectionid = SECTION_4.SECTIONID
 INNER JOIN SECTION SECTION_5 ON 
  SECTION_4.PRODMONTH = SECTION_5.PRODMONTH
  AND SECTION_4.ReportToSectionid = SECTION_5.SECTIONID
where SECTION_3.HIERARCHICALID = 3



GO
CREATE Procedure [dbo].[sp_RequiredCountOfDataApproval]
--declare
@ProdMonth Numeric(7,0),@SectionID VARCHAR(10), @TemplateID INT,  @ActivityType INT, @WorkplaceID VARCHAR(10)
AS
--SET @ProdMonth = 201303
--SET @SectionID = '54430'
------SET @WorkplaceID = '19157'
--SET @TemplateID = 13
--SET @ActivityType = 0

--DECLARE @Fieldcount int

SELECT COUNT(MainData .WORKPLACEID  ) FIELDCOUNT FROM(
select PRODMONTH,SECTIONID,WORKPLACEID,TemplateID,FieldID,FieldName,Max(CaptureDate)  CaptureDate,DESCRIPTION  from
 (
					SELECT PPD.PRODMONTH,PPD.ActivityType , PPD.SECTIONID ,PPD.WORKPLACEID ,WP.DESCRIPTION,
					PPF.TemplateID, PPF.FieldID  ,PPF.FieldName ,PPD.TheValue ,PPF.FieldRequired,PPD.CaptureDate   FROM [dbo].[PlanProt_Fields] PPF LEFT JOIN 
					 [dbo].[PlanProt_Data] PPD ON PPD.FieldID =PPF.FieldID inner JOIN 
					 dbo .WORKPLACE WP on WP.WORKPLACEID = PPD.WORKPLACEID) main where main .PRODMONTH =@ProdMonth 
					 and main .TemplateID =@TemplateID and main.SECTIONID  =@SectionID  and main.FieldRequired =1 AND main.ActivityType =@ActivityType
					 group by PRODMONTH,SECTIONID,WORKPLACEID,TemplateID,FieldID,FieldName,DESCRIPTION ) MainData
					 INNER JOIN PlanProt_Data PPD on
					 MainData.PRODMONTH = PPD.PRODMONTH and
					 MainData.SECTIONID = PPD.SECTIONID and
					 MainData.WORKPLACEID = PPD.WORKPLACEID and
					 
					 MainData.FieldID = PPD.FieldID  and
					 MainData.CaptureDate = PPD.CaptureDate where TheValue ='' and PPD.WORKPLACEID =@WorkplaceID

GO

CREATE Procedure [dbo].[sp_PlanProtData_ApproveWorkplaceLIST]
--DECLARE
@ProdMonth Numeric(7,0),@SectionID_2 VARCHAR(10), @TemplateID INT,  @ActivityType INT
AS

--SET @ProdMonth = 201506
--SET @SectionID_2 = '188470'
------SET @WorkplaceID = '19157'
--SET @TemplateID = 16
--SET @ActivityType = 0


SELECT b.DESCRIPTION DESCRIPTION,PP.[Workplaceid] WORKPLACEID,@TemplateID TemplateID,
      (SELECT [TemplateName] FROM  [dbo].[PlanProt_Template] WHERE [TemplateID] = @TemplateID) TemplateName,
	  @ProdMonth PRODMONTH,@SectionID_2 SECTIONID,@ActivityType ActivityType,
	  CASE WHEN PPDA.Approved IS NULL THEN 'NO' WHEN PPDA.Approved = 0 THEN 'NO' ELSE 'YES' END Approved  FROM PLANMONTH PP
	  Inner join WORKPLACE b on
	  pp.Workplaceid = b.WorkplaceID
	  inner join SECTION_COMPLETE c on
	  pp.Prodmonth = c.Prodmonth and
	  pp.Sectionid = c.SectionID
	LEFT join [dbo].[PlanProt_DataApproved] PPDA ON
	PP.WORKPLACEID = PPDA.WORKPLACEID and
	PPDA.TemplateID = @TemplateID and
	PP.Activity =PPDA.ActivityType and
	PP.Prodmonth = PPDA.Prodmonth 
	WHERE PP.Prodmonth = @ProdMonth and
      c.SectionID_2 = @SectionID_2 and
	  PP.Activity = @ActivityType and
	  pp.plancode = 'MP'

GO

CREATE Procedure [dbo].[sp_PlanProt_SaveData]

--DECLARE 
@Prodmonth NUMERIC(7,0), @SectionID VARCHAR(10), @WorkplaceID VARCHAR(30), @FieldID INT, @TheValue VARCHAR(max),@ActivityType INT,
@UserID VARCHAR(60)

AS

DECLARE @theDate DATETIME, @HasData int

SET @theDate = GETDATE()

--SET @Prodmonth = 201103
--SET @SectionID = '318460'
--SET @WorkplaceID = '18292'
--SET @FieldID = 5
--SET @TheValue = 'NO'
--SET @ActivityType = 0

--DECLARE @HasData int

SET @HasData = (
SELECT COUNT(FieldID) FROM dbo.PlanProt_Data
WHERE PRODMONTH = @Prodmonth AND
      SECTIONID = SECTIONID AND
      WORKPLACEID = @WorkplaceID AND
      FieldID = @FieldID)
      
IF @HasData = 0
BEGIN      

INSERT INTO dbo.PlanProt_Data
        ( PRODMONTH ,
          SECTIONID ,
          WORKPLACEID ,
          FieldID ,
          TheValue ,
          ActivityType,
          CaptureDate
        )
VALUES  ( @Prodmonth , -- PRODMONTH - numeric
          @SectionID , -- SECTIONID - varchar(10)
          @WorkplaceID , -- WORKPLACEID - varchar(30)
          @FieldID , -- FieldID - int
          @TheValue , -- TheValue - varchar(max)
          @ActivityType,  -- ActivityType - int
          @theDate
        )
        
INSERT INTO dbo.PlanProt_DataLog
        ( WorkplaceID ,
          FieldID ,
          UserID ,
          LodDate
        )
VALUES  ( @WorkplaceID , -- WorkplaceID - varchar(30)
          @FieldID , -- FieldID - int
          @UserID , -- UserID - varchar(60)
          @theDate  -- LodDate - datetime
        )
END
ELSE
BEGIN
UPDATE dbo.PlanProt_Data SET TheValue = @TheValue
WHERE PRODMONTH = @Prodmonth AND
      SECTIONID = SECTIONID AND
      WORKPLACEID = @WorkplaceID AND
      FieldID = @FieldID
END        
        
GO

Create Procedure [dbo].[sp_PlanProt_LoadData]
@ProdMonth varchar(10),@SectionID VARCHAR(10),@WorkplaceID VARCHAR(30), @TemplateID varchar(10),@ActivityType varchar(5)

AS

--DECLARE @ProdMonth varchar(10),@SectionID VARCHAR(10),@WorkplaceID VARCHAR(30), @TemplateID varchar(10), @CaptureOption VARCHAR(5), @ActivityType varchar(5)

--SET @PRODMONTH = '201705'
--SET @SECTIONID = 'REA'
--SET @WORKPLACEID = 'RE007667'
--SET @TemplateID = '2'
--SET @ActivityType = '0'


DECLARE @LockedData INT, @ApproveID INT


DECLARE @testHasData int
SET @testHasData = (SELECT COUNT(PPF.FieldID) FROM 
dbo.PLANPROT_DATA PPD
INNER JOIN dbo.PLANPROT_FIELDS PPF ON PPD.FieldID = PPF.FieldID
INNER JOIN dbo.PLANPROT_TEMPLATE PPT ON PPF.TemplateID = PPT.TemplateID
WHERE WorkplaceID = @WORKPLACEID AND Prodmonth = @PRODMONTH AND ppt.TemplateID = @TemplateID and SectionID = @SECTIONID)

--select @testHasData

IF(@testHasData = 0)
BEGIN

    INSERT INTO dbo.PLANPROT_DATA
        
	SELECT DISTINCT * FROM(
	SELECT  @PRODMONTH PRODMONTH,@SECTIONID SECTIONID,@WORKPLACEID WORKPLACEID,PPF.FieldID,Max(ppd.TheValue) TheValue,@ActivityType ActivityType,GETDATE() DateCaptured FROM dbo.PLANPROT_TEMPLATE PPT
	INNER JOIN dbo.PLANPROT_FIELDS PPF ON
	ppt.TemplateID = ppf.TemplateID
	left JOIN (SELECT * FROM dbo.PLANPROT_DATA PPD WHERE PPD.Prodmonth = [dbo].GetPrevProdMonth(@PRODMONTH) 
	--AND PPD.SectionID = @SectionID 
	And PPD.WorkplaceID = @WorkplaceID AND PPD.ActivityType = @ActivityType) PPD ON PPD.FieldID = PPF.FieldID
	WHERE PPT.TemplateID = @TemplateID
	group by PPF.FieldID ) tt

	SET @testHasData = (SELECT COUNT(PPF.FieldID) FROM 
	dbo.PLANPROT_DATA PPD
	INNER JOIN dbo.PLANPROT_FIELDS PPF ON PPD.FieldID = PPF.FieldID
	INNER JOIN dbo.PLANPROT_TEMPLATE PPT ON PPF.TemplateID = PPT.TemplateID
	WHERE WorkplaceID = @WORKPLACEID AND Prodmonth = @PRODMONTH AND ppt.TemplateID = @TemplateID)


	--Select @testHasData

	IF @testHasData = 0
	BEGIN
		INSERT INTO dbo.PLANPROT_DATA
		SELECT  @PRODMONTH PRODMONTH,@SECTIONID SECTIONID,@WORKPLACEID WORKPLACEID,PPF.FieldID,'' TheValue,@ActivityType ActivityType,GETDATE() DateCaptured FROM dbo.PLANPROT_TEMPLATE PPT
		INNER JOIN dbo.PLANPROT_FIELDS PPF ON
		ppt.TemplateID = ppf.TemplateID
	WHERE PPT.TemplateID = @TemplateID 
	END

END


CREATE TABLE #MiningTypes (
TargetID int,
Name varchar(120) )


DECLARE @theSQL varchar(8000),@theSQL2 varchar(8000), @IsApproved varchar(50),@ProdMonth2 varchar(10), @hasData int




-- test IF locked

SET @LockedData = (select count(approveid)  from planprot_dataapproved where
prodmonth = @Prodmonth and sectionid = @SectionID and workplaceid = @Workplaceid and templateid = @TemplateID and activitytype = @ActivityType and 
approved = 1)

--Select @LockedData

IF @LockedData > 0
BEGIN
	SET @ApproveID  = (select max(approveid) from planprot_dataapproved where
	prodmonth = @Prodmonth and sectionid = @SectionID and workplaceid = @Workplaceid and templateid = @TemplateID and activitytype = @ActivityType and 
	approved = 1)

	SET @LockedData = (SELECT COUNT(WorkplaceID) FROM dbo.PLANPROT_DATALOCKED WHERE ApprovedID = @ApproveID and prodmonth = @Prodmonth  and workplaceid = @Workplaceid)
	
	IF @LockedData = 0
	BEGIN
		DELETE  FROM dbo.planprot_dataapproved where
	      ApproveID = @ApproveID
	END
END

SET @IsApproved = (select IsApproved = case when count(approveid) > 0 then 'PLANPROT_DATALOCKED' else 'PLANPROT_DATA' end  from planprot_dataapproved where
prodmonth = @Prodmonth and sectionid = @SectionID and workplaceid = @Workplaceid and templateid = @TemplateID and activitytype = @ActivityType and 
approved = 1)

--SELECT @IsApproved

IF @IsApproved = 'PLANPROT_DATA'
BEGIN
SET @IsApproved = (select IsApproved = case when count(approveid) > 0 then 'PLANPROT_DATALOCKED' else 'PLANPROT_DATA' end  from planprot_dataapproved where
prodmonth = @ProdMonth2 and sectionid = @SectionID and workplaceid = @Workplaceid and templateid = @TemplateID and activitytype = @ActivityType and 
approved = 1)
END ELSE BEGIN SET @ProdMonth2 = @PRODMONTH END



set @hasData = (SELECT count(WorkplaceID) FROM PLANPROT_DATA  PD
inner join [dbo].[PLANPROT_FIELDS] PPF on
PD.[FieldID] = PPF.[FieldID]
where PD.prodmonth = @Prodmonth and PD.sectionid = @SectionID and PD.workplaceid = @Workplaceid and  PD.activitytype = @ActivityType and PPF.TemplateID = @TemplateID)

if @hasData > 0
BEGIN
	SET @ProdMonth2 = @PRODMONTH 
END 
ELSE 
BEGIN 
	SET @ProdMonth2 = [dbo].GetPrevProdMonth(@PRODMONTH) 
	SET @IsApproved = (select IsApproved = case when count(approveid) > 0 then 'PLANPROT_DATALOCKED' else 'PLANPROT_DATA' end  from planprot_dataapproved where
	prodmonth = @ProdMonth2 and sectionid = @SectionID and workplaceid = @Workplaceid and templateid = @TemplateID and activitytype = @ActivityType and 
	approved = 1)
END


--SET @theSQL = 
 
-- ' INSERT INTO #MiningTypes 
--select TargetID ID,Description from ' + @BonusDB + 'dbo.Bonus_poolDEfaults
--where Activity in (0,3)
--and TargetID in (select TargetID from Bonus_Pool_Validations
--where
--activity in (0,3)
--and showinplanning = ''Y'')
--UNION
--select  TargetID ID, Description from ' + @BonusDB + 'dbo.Bonus_poolDEfaults
--where Activity in (1)
--and TargetID in (select TargetID from Bonus_Pool_Validations
--where
--activity in (1)
--and showinplanning = ''Y'')'

-- exec (@theSQL)


set @TheSQL2 = '
SELECT MainDAT.*,
       PP.OrgUnitDay,
       PP.OrgUnitNight,pp.FL,pp.SW,pp.SQm  TotalSQM,CASE WHEN PP.CubicMetres IS NULL THEN 0 ELSE PP.CubicMetres END CubicMetres,pp.ReefAdv + pp.WasteAdv TotalMeters,MT.Name MiningType FROM (
SELECT TemplateName, theHeadings.TemplateID,'''+@ActivityType+''' ActivityType,theHeadings.FieldID,
CASE WHEN FieldRequired is NULL or  FieldRequired = 0 THEN CAST(0 as BIT) ELSE CAST(1 as BIT) END FieldRequired,ThePOS,CalcField, CAST(theHeadings.FieldID AS VARCHAR(10)) + '': '' +theHeadings.FieldName  FieldName,
theHeadings.FieldType,theHeadings .NoCharacters ,theHeadings .NoLines , theHeadings.GroupName1,
theHeadings.GroupName2,theHeadings.GroupName3,theHeadings.GroupName4,
       '''+@ProdMonth+''' PRODMONTH,
       '''+@SECTIONID+'''  SECTIONID, 
       CASE WHEN PPD.WORKPLACEID IS NULL THEN '''+@WORKPLACEID+''' ELSE PPD.WORKPLACEID END WORKPLACEID,
       WP.DESCRIPTION WPDESCRIPTION,
       CASE WHEN PPD.TheValue IS NULL THEN '''' ELSE PPD.TheValue END TheValue,
       PPFT.fieldDescription,ppd.CaptureDate, 
          CASE WHEN PPD.TheValue IS NULL or ''' + @PRODMONTH + ''' <> ''' + @ProdMonth2 +  ''' THEN 1 ELSE 0 END ValueChanged,
          
          PPDA.Approved TheApproofValue,

          CASE WHEN PPDA.Approved IS NULL THEN ''NO''
          WHEN PPDA.Approved = 0 THEN ''NO'' ELSE ''YES'' END Approved 
                                                                                                                FROM (
SELECT DISTINCT TemplateName,TemplateID,theFields.FieldID,theFields.FieldRequired,theFields.FieldName,theFields.CalcField,theFields.ThePOS,theFields.FieldType,theFields .NoCharacters ,theFields .NoLines , Group1.GroupName1,Group2.GroupName2,Group3.GroupName3,Group4.GroupName4 FROM (
SELECT PPT.TemplateName, PPF.parentid,PPF.FieldRequired ,PPF.FieldName,PPF.FieldType,PPF .NoCharacters ,PPF .NoLines , PPF.fieldid,PPT.TemplateID,PPF.CalcField,PPF.ThePOS FROM dbo.PlanProt_Template PPT
INNER JOIN dbo.PlanProt_Fields PPF ON
PPT.TemplateID = PPF.TemplateID
--LEFT JOIN dbo.PlanProt_Data PPG ON
--PPF.FieldID = PPG.FieldID 
WHERE PPT.TemplateID = ''' + @TemplateID + ''' AND
      PPF.FieldType <> 1) theFields
LEFT JOIN 
( SELECT FieldName GroupName1,PPF.fieldid,PPF.parentid FROM dbo.PlanProt_Fields PPF 
  --LEFT JOIN dbo.PlanProt_Data PPG ON
  --PPF.FieldID = PPG.FieldID
  WHERE PPF.FieldType = 1  ) Group1 ON
theFields.ParentID = Group1.FieldID
LEFT JOIN 
( SELECT FieldName GroupName2,PPF.fieldid,PPF.parentid FROM dbo.PlanProt_Fields PPF 
  --LEFT JOIN dbo.PlanProt_Data PPG ON
  --PPF.FieldID = PPG.FieldID
  WHERE PPF.FieldType = 1  ) Group2 ON
Group1.ParentID = Group2.FieldID
LEFT JOIN 
( SELECT FieldName GroupName3,PPF.fieldid,PPF.parentid FROM dbo.PlanProt_Fields PPF 
  --LEFT JOIN dbo.PlanProt_Data PPG ON
  --PPF.FieldID = PPG.FieldID
  WHERE PPF.FieldType = 1  ) Group3 ON
Group2.ParentID = Group3.FieldID
LEFT JOIN 
( SELECT FieldName GroupName4,PPF.fieldid,PPF.parentid FROM dbo.PlanProt_Fields PPF 
  --LEFT JOIN dbo.PlanProt_Data PPG ON
  --PPF.FieldID = PPG.FieldID
  WHERE PPF.FieldType = 1  ) Group4 ON
Group3.ParentID = Group4.FieldID
) theHeadings
LEFT JOIN ' + @IsApproved + ' PPD ON
PPD.FieldID = theHeadings.FieldID AND
PPD.PRODMONTH = ''' + @ProdMonth2 + ''' AND
--PPD.SECTIONID = ''' + @SECTIONID + ''' AND
PPD.WORKPLACEID = ''' + @WORKPLACEID + '''
INNER JOIN dbo.PlanProt_FieldTypes PPFT ON 
theHeadings.FieldType = PPFT.fieldTypeID
LEFT JOIN PlanProt_DataApproved PPDA ON
--PPD.ActivityType = PPDA.ActivityType AND
PPDA.PRODMONTH = ''' + @ProdMonth + ''' AND
PPDA.WORKPLACEID = ''' + @WORKPLACEID + ''' AND
theHeadings.TemplateID = PPDA.TemplateID
inner JOIN dbo.WORKPLACE WP ON
WP.WORKPLACEID  = ''' + @WORKPLACEID + '''



--WHERE (PPD.Prodmonth =''' + @ProdMonth + ''') 

) MainDAT
LEFT join (SELECT pp.* FROM PLANMONTH PP inner join Section_Complete b on
pp.Prodmonth = b.Prodmonth and
pp.SectionID = b.SectionID
WHERE
PP.WORKPLACEID = ''' +@WORKPLACEID + ''' AND
b.SECTIONID_2 = ''' +@SECTIONID + ''' AND
PP.PRODMONTH = ''' +@PRODMONTH + ''' and
PP.PLANCODE=''MP'' ) PP ON
MainDAT.WORKPLACEID = PP.WORKPLACEID
LEFT JOIN #MiningTypes MT on
MT.TargetID = pp.TargetID

ORDER BY MainDAT.FieldID'



--select @theSQL
--select @TheSQL2

EXEC (@TheSQL2)

DROP TABLE #MiningTypes


GO


Create Procedure [dbo].[sp_RevisedPlanning_Update] --201111
--DECLARE 
@Prodmonth Numeric(7,0)

AS

DECLARE @theShaft varchar(60),@theDepartment varchar(60),@theSection varchar(60)
--SET @Prodmonth = 201705 

-- Get all the shafts that currenlty is not in security table and add them
DECLARE db_Shafts CURSOR FOR  
SELECT NAME_3 theShaft FROM vw_Section_from_UM SFS
LEFT JOIN PrePlanning_Notification_Security PPNS ON
SFS.NAME_3 = PPNS.Shaft
WHERE Prodmonth = @Prodmonth and 
      PPNS.Shaft is null
      
OPEN db_Shafts   
FETCH NEXT FROM db_Shafts INTO @theShaft  

WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PrePlanning_Notification_Security (Shaft,Section,Department)
SELECT @theShaft Shaft,'NONE' Section,DESCRIPTION Department FROM tblProfiles
FETCH NEXT FROM db_Shafts INTO @theShaft    
END  

CLOSE db_Shafts   
DEALLOCATE db_Shafts   

-- Get all the userprofiles that is not avalibal in security table and add them
DECLARE db_Departments CURSOR FOR
SELECT Description USERPROFILEID FROM tblProfiles UP
LEFT JOIN PrePlanning_Notification_Security PPNS ON
UP.Description = PPNS.Department and 
Section = 'NONE' 
WHERE PPNS.Shaft is null

OPEN db_Departments   
FETCH NEXT FROM db_Departments INTO @theDepartment 
WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PrePlanning_Notification_Security (Shaft,Section,Department)
SELECT NAME_3 theShaft,'NONE',@theDepartment FROM vw_SECTION_FROM_UM SFS
WHERE Prodmonth = @Prodmonth
FETCH NEXT FROM db_Departments INTO @theDepartment
END

CLOSE db_Departments   
DEALLOCATE db_Departments  

DECLARE db_Sections CURSOR FOR
SELECT NAME_3 Shaft,NAME_2 Section FROM vw_SECTION_FROM_MO SFUM
LEFT JOIN PrePlanning_Notification_Security PPNS ON
SFUM.NAME_3 = PPNS.Shaft and
SFUM.NAME_2 = PPNS.Section 
WHERE SFUM.Prodmonth = @Prodmonth and
PPNS.Shaft is null
Order by SFUM.NAME_3,SFUM.NAME_2 

OPEN db_Sections   
FETCH NEXT FROM db_Sections INTO @theShaft,@theSection 

WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PrePlanning_Notification_Security (Shaft,Section,Department)
SELECT @theShaft Shaft,@theSection Section,Description Department FROM tblProfiles
FETCH NEXT FROM db_Sections INTO @theShaft,@theSection 
END

CLOSE db_Sections   
DEALLOCATE db_Sections

DECLARE db_Departments CURSOR FOR
SELECT Description USERPROFILEID FROM tblProfiles UP
LEFT JOIN PrePlanning_Notification_Security PPNS ON
UP.description = PPNS.Department and 
Section <> 'NONE' 
WHERE PPNS.Shaft is null

OPEN db_Departments 

FETCH NEXT FROM db_Departments INTO @theDepartment 
WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PrePlanning_Notification_Security (Shaft,Section,Department)
SELECT NAME_3 theShaft,NAME_2 theSection,@theDepartment FROM vw_SECTION_FROM_MO SFUM
WHERE Prodmonth = @Prodmonth
FETCH NEXT FROM db_Departments INTO @theDepartment
END 

CLOSE db_Departments   
DEALLOCATE db_Departments 

--SELECT * FROM PrePlanning_Notification_Security
GO

CREATE Procedure [dbo].[sp_Get_User_List]
as

SELECT UserID, NAME+' '+LastName UserName FROM tblUsers

GO

CREATE procedure [dbo].[sp_PrePlanning_Prodmonth]
@SECTIONID_2 VARCHAR(20) = ''
as

------a.SECTIONID_2,
------max(a.prodmonth) CurrentProductionMonth 
SELECT 

Distinct min(s.prodmonth) CurrentProductionMonth 

FROM [dbo].[SECTION] S
INNER JOIN [dbo].[SECCAL] SC ON
S.[Prodmonth] = SC.[Prodmonth] and
S.SectionID = SC.SectionID and
SC.Begindate <=  Convert(date,getdate())  and -- DateADD(DAY,-1, getdate()
SC.enddate >=  Convert(date,getdate())  -- DateADD(DAY,-10, getdate()
INNER JOIN SECTION_COMPLETE SECO on
SECO.[Prodmonth] = S.[Prodmonth] and
SECO.SectionID_1 = S.SectionID 

WHERE SECO.[SectionID_2]= @SECTIONID_2 

GO
Create Function [dbo].[GetApprovedCMGT]
    (
      @Prodmonth Varchar(7),
      @WorkplaceID Varchar(150),
      @SectionID_2 Varchar(150)
    )    

Returns float
as
BEGIN    
	Declare @theValue float

	select @theValue = 
    (SELECT CAST(TheValue as float) FROM [PlanProt_DataLocked]
     WHERE FieldID = 1604 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 and ApprovedID = (SELECT 
MAX(ApprovedID)

FROM [PlanProt_DataLocked]
     WHERE FieldID = 1604 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 	   ))
    
	
RETURN @theValue	
END

GO

Create Function [dbo].[GetApprovedCMGT_Dev]
    (
      @Prodmonth Varchar(7),
      @WorkplaceID Varchar(150),
      @SectionID_2 Varchar(150)
    )    

Returns float
as
BEGIN    
	Declare @theValue float

	select @theValue = 
    (SELECT CAST(TheValue as float) FROM [PlanProt_DataLocked]
     WHERE FieldID = 1403 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 and CaptureDate = (SELECT 
MAX(CaptureDate)

FROM [PlanProt_DataLocked]
     WHERE FieldID = 1403 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 	   ))
    
	
RETURN @theValue	
END

GO

Create Function [dbo].[GetApprovedCMKGT]
    (
      @Prodmonth Varchar(7),
      @WorkplaceID Varchar(150),
      @SectionID_2 Varchar(150)
    )    

Returns float
as
BEGIN    
	Declare @theValue float = 0


RETURN @theValue	
END

GO

Create Function [dbo].[GetApprovedCMKGT_Dev]
    (
      @Prodmonth Varchar(7),
      @WorkplaceID Varchar(150),
      @SectionID_2 Varchar(150)
    )    

Returns float
as
BEGIN    
	Declare @theValue float = 0

   
	
RETURN @theValue	
END

GO








CREATE Function [dbo].[GetApprovedCW]
    (
      @Prodmonth Varchar(7),
      @WorkplaceID Varchar(150),
      @SectionID_2 Varchar(150)
    )    

Returns float
as
BEGIN    
	Declare @theValue float

	select @theValue = 
    (SELECT CAST(TheValue as float) FROM [PlanProt_DataLocked]
     WHERE FieldID = 1602 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 and 
		   CaptureDate = (SELECT 
MAX(CaptureDate)

FROM [PlanProt_DataLocked]
     WHERE FieldID = 1602 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 	   ))
    
	
RETURN @theValue	
END
GO

Create Function [dbo].[GetApprovedCW_Dev]
    (
      @Prodmonth Varchar(7),
      @WorkplaceID Varchar(150),
      @SectionID_2 Varchar(150)
    )    

Returns float
as
BEGIN    
	Declare @theValue float

	select @theValue = 
    (SELECT CAST(TheValue as float) FROM [PlanProt_DataLocked]
     WHERE FieldID = 1401 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 and 
		   CaptureDate = (SELECT 
MAX(CaptureDate)

FROM [PlanProt_DataLocked]
     WHERE FieldID = 1401 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 	   ))
    
	
RETURN @theValue	
END

GO

CREATE Function [dbo].[GetApprovedIdealSW]
    (
      @Prodmonth Varchar(7),
      @WorkplaceID Varchar(150),
      @SectionID_2 Varchar(150)
    )    

Returns float
as
BEGIN    
	Declare @theValue float

	select @theValue = 
    (SELECT CAST(TheValue as float) FROM [PlanProt_DataLocked]
     WHERE FieldID = 1601 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 and
		   CaptureDate = (SELECT 
MAX(CaptureDate)

FROM [PlanProt_DataLocked]
     WHERE FieldID = 1601 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 	   ))
    
	
RETURN @theValue	
END

GO

Create Function [dbo].[GetApprovedSW]
    (
      @Prodmonth Varchar(7),
      @WorkplaceID Varchar(150),
      @SectionID_2 Varchar(150)
    )    

Returns float
as
BEGIN    
	Declare @theValue float

	select @theValue = 
    (SELECT CAST(TheValue as float) FROM [PlanProt_DataLocked]
     WHERE FieldID = 1605 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 and
		   CaptureDate = (SELECT 
MAX(CaptureDate)

FROM [PlanProt_DataLocked]
     WHERE FieldID = 1605 and
           Prodmonth = @Prodmonth and
           WorkplaceID =  @WorkplaceID and
           SectionID = @SectionID_2 	   ))
    
	
RETURN @theValue	
END
GO

CREATE Procedure [dbo].[spUpdate_PlanningTemplate_UserSecurity_Table]
@Prodmonth Numeric(7,0)
 AS
DECLARE @theShaft varchar(60),@theTemplate varchar(60),@theSection varchar(60), @theUnit varchar(60),@theUnit1 varchar(60)
--SET @Prodmonth = 201309 

DECLARE db_Units CURSOR FOR  

SELECT DISTINCT NAME_4 theUnit FROM vw_Section_from_MO SFS
LEFT JOIN PlanProt_ApproveUsers PPNS ON
SFS.NAME_4= PPNS.Unit
WHERE Prodmonth = @Prodmonth AND
PPNS.Unit is null
order by NAME_4

OPEN db_Units   
FETCH NEXT FROM db_Units INTO @theUnit

WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PlanProt_ApproveUsers (Shaft,Section,Unit,TemplateID)
SELECT 'NONE','NONE',@theUnit Unit,TemplateID FROM PlanProt_Template
FETCH NEXT FROM db_Units INTO @theUnit    
END  

CLOSE db_Units   
DEALLOCATE db_Units 

DECLARE db_Shafts CURSOR FOR  

SELECT NAME_4 theUnit,NAME_3 theShaft FROM vw_Section_from_UM SFS
LEFT JOIN PlanProt_ApproveUsers PPNS ON
SFS.NAME_4 = PPNS.Unit AND
SFS.NAME_3 = PPNS.Shaft
WHERE Prodmonth = @Prodmonth and--and  SFS.NAME_4 <> 'T' AND--NAME_4 not like 'Mining Unit%' and
PPNS.Shaft is null
order by NAME_4,NAME_3

OPEN db_Shafts   
FETCH NEXT FROM db_Shafts INTO  @theUnit,@theShaft

WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PlanProt_ApproveUsers (Shaft,Section,Unit,TemplateID)
SELECT @theShaft Shaft,'NONE',@theUnit Unit,TemplateID FROM PlanProt_Template
FETCH NEXT FROM db_Shafts INTO  @theUnit ,@theShaft  
END  

CLOSE db_Shafts   
DEALLOCATE db_Shafts 


DECLARE db_Templates CURSOR FOR
SELECT DISTINCT PPT.TemplateID FROM PlanProt_Template PPT
LEFT JOIN PlanProt_ApproveUsers PPNS ON
PPT.TemplateID = PPNS.TemplateID and 
Section = 'NONE' 
OPEN db_Templates   
FETCH NEXT FROM db_Templates INTO @theTemplate 


WHILE @@FETCH_STATUS = 0   
BEGIN 
DECLARE db_Sections CURSOR FOR    
SELECT DISTINCT NAME_4 Unit,NAME_3 Shaft FROM vw_Section_from_UM SFUM
LEFT JOIN PlanProt_ApproveUsers PPNS ON
SFUM.NAME_4 = PPNS.Unit and
SFUM.NAME_3 = PPNS.Shaft 
WHERE SFUM.Prodmonth = @Prodmonth and
PPNS.Shaft is null
Order by SFUM.NAME_4,SFUM.NAME_3 

OPEN db_Sections   
FETCH NEXT FROM db_Sections INTO @theUnit,@theShaft--,@theSection 

WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PlanProt_ApproveUsers (Unit,Shaft,Section,TemplateID)
SELECT @theUnit Unit,@theShaft Shaft,@theSection Section,TemplateID  FROM PlanProt_Template
FETCH NEXT FROM db_Sections INTO @theUnit,@theShaft-- ,@theSection 
END

CLOSE db_Sections   
DEALLOCATE db_Sections


DECLARE db_Sections CURSOR FOR    
SELECT DISTINCT NAME_4 Unit,NAME_2 Section ,NAME_3 Shaft
FROM vw_Section_from_MO SFUM
LEFT JOIN PlanProt_ApproveUsers PPNS ON
SFUM.NAME_4 = PPNS.Unit and
SFUM.NAME_2 = PPNS.Section and 
SFUM.NAME_3=PPNS.Shaft
WHERE SFUM.Prodmonth = @Prodmonth and SFUM.NAME_4 <> 'T' AND
PPNS.Shaft is null
Order by SFUM.NAME_4,SFUM.NAME_2 ,SFUM.NAME_3

OPEN db_Sections   
FETCH NEXT FROM db_Sections INTO @theUnit,@theSection ,@theShaft

WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PlanProt_ApproveUsers (Unit,Shaft,Section,TemplateID)
SELECT @theUnit Unit,@theShaft Shaft,@theSection Section,TemplateID  FROM PlanProt_Template
FETCH NEXT FROM db_Sections INTO @theUnit,@theSection ,@theShaft
END

CLOSE db_Sections   
DEALLOCATE db_Sections


FETCH NEXT FROM db_Templates INTO @theTemplate
END

CLOSE db_Templates   
DEALLOCATE db_Templates
 
GO

CREATE Function [dbo].[FormatDateTime] 
( 
    @dt DATETIME, 
    @format VARCHAR(16) 
) 
RETURNS VARCHAR(64) 
AS 
BEGIN 
    DECLARE @dtVC VARCHAR(64) 
    SELECT @dtVC = CASE @format 
 
    WHEN 'LONGDATE' THEN 
 
        DATENAME(dw, @dt) 
        + ',' + SPACE(1) + DATENAME(m, @dt) 
        + SPACE(1) + CAST(DAY(@dt) AS VARCHAR(2)) 
        + ',' + SPACE(1) + CAST(YEAR(@dt) AS CHAR(4)) 
 
    WHEN 'LONGDATEANDTIME' THEN 
 
        DATENAME(dw, @dt) 
        + ',' + SPACE(1) + DATENAME(m, @dt) 
        + SPACE(1) + CAST(DAY(@dt) AS VARCHAR(2)) 
        + ',' + SPACE(1) + CAST(YEAR(@dt) AS CHAR(4)) 
        + SPACE(1) + RIGHT(CONVERT(CHAR(20), 
        @dt - CONVERT(DATETIME, CONVERT(CHAR(8), 
        @dt, 112)), 22), 11) 
 
    WHEN 'SHORTDATE' THEN 
 
        LEFT(CONVERT(CHAR(19), @dt, 0), 11) 
 
    WHEN 'SHORTDATEANDTIME' THEN 
 
        REPLACE(REPLACE(CONVERT(CHAR(19), @dt, 0), 
            'AM', ' AM'), 'PM', ' PM') 
 
    WHEN 'UNIXTIMESTAMP' THEN 
 
        CAST(DATEDIFF(SECOND, '19700101', @dt) 
        AS VARCHAR(64)) 
 
    WHEN 'YYYYMMDD' THEN 
 
        CONVERT(CHAR(8), @dt, 112) 
 
    WHEN 'YYYY-MM-DD' THEN 
 
        CONVERT(CHAR(10), @dt, 23) 
 
    WHEN 'YYMMDD' THEN 
 
        CONVERT(VARCHAR(8), @dt, 12) 
 
    WHEN 'YY-MM-DD' THEN 
 
        STUFF(STUFF(CONVERT(VARCHAR(8), @dt, 12), 
        5, 0, '-'), 3, 0, '-') 
 
    WHEN 'MMDDYY' THEN 
 
        REPLACE(CONVERT(CHAR(8), @dt, 10), '-', SPACE(0)) 
 
    WHEN 'MM-DD-YY' THEN 
 
        CONVERT(CHAR(8), @dt, 10) 
 
    WHEN 'MM/DD/YY' THEN 
 
        CONVERT(CHAR(8), @dt, 1) 
 
    WHEN 'MM/DD/YYYY' THEN 
 
        CONVERT(CHAR(10), @dt, 101) 
 
    WHEN 'DDMMYY' THEN 
 
        REPLACE(CONVERT(CHAR(8), @dt, 3), '/', SPACE(0)) 
 
    WHEN 'DD-MM-YY' THEN 
 
        REPLACE(CONVERT(CHAR(8), @dt, 3), '/', '-') 
 
    WHEN 'DD/MM/YY' THEN 
 
        CONVERT(CHAR(8), @dt, 3) 
 
    WHEN 'DD/MM/YYYY' THEN 
 
        CONVERT(CHAR(10), @dt, 103) 
 
    WHEN 'HH:MM:SS 24' THEN 
 
        CONVERT(CHAR(8), @dt, 8) 
 
    WHEN 'HH:MM 24' THEN 
 
        LEFT(CONVERT(VARCHAR(8), @dt, 8), 5) 
 
    WHEN 'HH:MM:SS 12' THEN 
 
        LTRIM(RIGHT(CONVERT(VARCHAR(20), @dt, 22), 11)) 
 
    WHEN 'HH:MM 12' THEN 
 
        LTRIM(SUBSTRING(CONVERT( 
        VARCHAR(20), @dt, 22), 10, 5) 
        + RIGHT(CONVERT(VARCHAR(20), @dt, 22), 3)) 
 
    ELSE 
 
        'Invalid format specified' 
 
    END 
    RETURN @dtVC 
END 

GO

CREATE Function [dbo].[GetPlanProdData] 
( 
    @TemplateID INT,
    @WORKPLACEID VARCHAR(30),
    @PRODMONTH NUMERIC(7,0),
    @FieldID INT,
    @SectionID VARCHAR(20) 
) 
RETURNS VARCHAR(800) 
AS 
BEGIN


DECLARE @TheValue VARCHAR(max),
        @HasData INT --,
      --  @TemplateID INT,
        --@WORKPLACEID VARCHAR(30),
        --@PRODMONTH NUMERIC(7,0),
        --@FieldID INT,
        --@SectionID VARCHAR(20)
        
--SET @Prodmonth = 201507
--SET @SectionID = '142470'
--SET @TemplateID = 13
--SET @WorkplaceID = '50240'
--SET @FieldID = 296

SET @HasData = 
(SELECT COUNT(PPDA.PRODMONTH) FROM dbo.PlanProt_DataApproved PPDA
INNER JOIN dbo.PlanProt_Template PPT ON
PPDA.TemplateID = PPT.TemplateID AND
PPDA.ActivityType = PPT.Activity
WHERE PPT.TemplateID = @TemplateID AND
      PPDA.PRODMONTH = @PRODMONTH AND
      PPDA.SECTIONID = @SectionID AND
      PPDA.WORKPLACEID = @WORKPLACEID AND
      PPDA.Approved = 1)
      
IF @HasData = 1 
BEGIN
SELECT @TheValue = (
SELECT DISTINCT CASE WHEN (PPFT.fieldDescription = 'Date' AND (TheValue IS NOT NULL OR TheValue <> ''))  THEN 
                 dbo.FormatDateTime(CAST(TheValue AS DATETIME),'YYYY-MM-DD')
                 ELSE TheValue  END TheValue FROM dbo.PlanProt_DataApproved PPDA
INNER JOIN dbo.PlanProt_DataLocked PPDL ON
PPDA.ApproveID = PPDL.ApprovedID and
PPDA.PRODMONTH = PPDL.PRODMONTH
INNER JOIN dbo.PlanProt_Template PPT ON
PPDA.TemplateID = PPT.TemplateID AND
PPDA.ActivityType = PPT.Activity
INNER JOIN dbo.PlanProt_Fields PPF ON
PPT.TemplateID = PPF.TemplateID AND
PPF.FieldID = PPDL.FieldID
INNER JOIN dbo.PlanProt_FieldTypes PPFT ON
PPF.FieldType = PPFT.fieldTypeID
WHERE PPT.TemplateID = @TemplateID AND
      PPDA.PRODMONTH = @PRODMONTH AND
      PPDA.SECTIONID = @SectionID AND
      PPDA.WORKPLACEID = @WORKPLACEID AND
      PPDA.Approved = 1  AND
      PPDL.FieldID = @FieldID AND
      PPDL.CaptureDate =  (SELECT MAX(CaptureDate) FROM PLANPROT_DATALOCKED PPDT WHERE PPDT.ActivityType = PPDL.ActivityType AND
                                                                             PPDT.FieldID = PPDL.FieldID AND
                                                                             PPDT.PRODMONTH = PPDL.PRODMONTH AND
                                                                             PPDT.SECTIONID = PPDL.SECTIONID AND
                                                                             PPDT.WORKPLACEID = PPDL.WORKPLACEID) )
END
ELSE
BEGIN      
         
DECLARE @hasCurrentData int



set @hasCurrentData = (select count(WORKPLACEID) from PlanProt_Data where      WORKPLACEID = @WORKPLACEID AND
      PRODMONTH = @PRODMONTH AND
      FieldID = @FieldID AND
      SECTIONID = @SectionID)

if @hasCurrentData = 0
begin
set @PRODMONTH = dbo.GetPrevProdMonth(@PRODMONTH)
end



IF @hasCurrentData = 0
BEGIN
SELECT @TheValue = (
SELECT DISTINCT CASE WHEN (PPFT.fieldDescription = 'Date' AND (TheValue IS NOT NULL OR TheValue <> ''))  THEN 
                 dbo.FormatDateTime(CAST(TheValue AS DATETIME),'YYYY-MM-DD')
                 ELSE TheValue  END TheValue FROM dbo.PlanProt_DataApproved PPDA
INNER JOIN dbo.PlanProt_DataLocked PPDL ON
PPDA.ApproveID = PPDL.ApprovedID and
PPDA.PRODMONTH = PPDL.PRODMONTH
INNER JOIN dbo.PlanProt_Template PPT ON
PPDA.TemplateID = PPT.TemplateID AND
PPDA.ActivityType = PPT.Activity
INNER JOIN dbo.PlanProt_Fields PPF ON
PPT.TemplateID = PPF.TemplateID AND
PPF.FieldID = PPDL.FieldID
INNER JOIN dbo.PlanProt_FieldTypes PPFT ON
PPF.FieldType = PPFT.fieldTypeID
WHERE PPT.TemplateID = @TemplateID AND
      PPDA.PRODMONTH = @PRODMONTH AND
      PPDA.SECTIONID = @SectionID AND
      PPDA.WORKPLACEID = @WORKPLACEID AND
      PPDA.Approved = 1  AND
      PPDL.FieldID = @FieldID AND
      PPDL.CaptureDate =  (SELECT MAX(CaptureDate) FROM PLANPROT_DATALOCKED PPDT WHERE PPDT.ActivityType = PPDL.ActivityType AND
                                                                             PPDT.FieldID = PPDL.FieldID AND
                                                                             PPDT.PRODMONTH = PPDL.PRODMONTH AND
                                                                             PPDT.SECTIONID = PPDL.SECTIONID AND
                                                                             PPDT.WORKPLACEID = PPDL.WORKPLACEID) )
END
ELSE
BEGIN
SELECT @TheValue = 
  (  
  SELECT DISTINCT CASE WHEN (PPFT.fieldDescription = 'Date' AND (TheValue IS NOT NULL OR TheValue <> ''))  THEN 
                 dbo.FormatDateTime(CAST(TheValue AS DATETIME),'YYYY-MM-DD')
                 ELSE TheValue  END TheValue  FROM dbo.PlanProt_Template PPT
INNER JOIN dbo.PlanProt_Fields PPF ON
PPT.TemplateID = PPF.TemplateID 

INNER JOIN dbo.PlanProt_Data  PPD ON
PPF.FieldID = PPD.FieldID
INNER JOIN dbo.PlanProt_FieldTypes PPFT ON
PPF.FieldType = PPFT.fieldTypeID
WHERE PPT.TemplateID = @TemplateID AND
      PPD.WORKPLACEID = @WORKPLACEID AND
      PPD.PRODMONTH = @PRODMONTH AND
      PPF.FieldID = @FieldID AND
      PPD.SECTIONID = @SectionID AND
      PPD.CaptureDate =  (SELECT MAX(CaptureDate) FROM PlanProt_Data PPDT WHERE PPDT.ActivityType = PPD.ActivityType AND
                                                                             PPDT.FieldID = PPD.FieldID AND
                                                                             PPDT.PRODMONTH = PPD.PRODMONTH AND
                                                                             PPDT.SECTIONID = PPD.SECTIONID AND
                                                                             PPDT.WORKPLACEID = PPD.WORKPLACEID))
END
END                                                                             

--SELECT @TheValue
RETURN @TheValue  
END









GO

CREATE Function [dbo].[GetPlanProdFieldName] 
( 
    @FieldID INT 
) 
RETURNS VARCHAR(100) 
AS 
BEGIN

--DECLARE    @BMEHeading VARCHAR(100),
--    @BMEValue NUMERIC(38,13),
--    @EntryType VARCHAR(20)

DECLARE @TheValue VARCHAR(max) 

--SET @BMEHeading = 'NCE - Rand per Ton'
--SET @BMEValue = 24197868673.234200743494
--SET @EntryType = 'Variance'


SELECT @TheValue = 
  (SELECT FieldName FROM dbo.PlanProt_Fields PPF WHERE
      PPF.FieldID = @FieldID)

RETURN @TheValue  
END







GO

CREATE Function [dbo].[CalcFaceAdvance]
    (
      @SW float,
      @CUBICMETRES float,
      @FL float,
      @ReefSQM float,
      @WasteSQM float
    )    

Returns float
as
BEGIN    
	Declare @theValue float

	select @theValue = 
    (
    SELECT CASE 
    WHEN @SW > 0 and @FL > 0 THEN
         ((@ReefSQM + @WasteSQM) + (@CUBICMETRES / ( @SW / 100))) / @FL ELSE 0 END FaceAdvance 
    )
    
IF @theValue is null
   set @theValue = 0	
RETURN ROUND(@theValue,2)
END

GO

CREATE Function [dbo].[CalcFaceBrokenKG]
    (
      @SQM float,
      @FaceCMGT float,
      @WPID varchar(50)

    )    

Returns float
as
BEGIN    
	Declare @theValue float
	Declare @density float
	
	SET @density = ( SELECT CASE WHEN b.Density is null then (select rockdensity from sysset) / 100 ELSE b.Density / 100 END FROM WORKPLACE a left join reef b on
	a.ReefID = b.ReefID
	 WHERE WorkplaceID = @WPID)

	select @theValue = 
    (
    SELECT  (@SQM * @density )* @FaceCMGT / 1000
    )
    
IF @theValue is null
   set @theValue = 0	
RETURN ROUND(@theValue,2)
END

GO

CREATE Function [dbo].[CalcFaceTonsCUBE]
    (
      @CUBICMETRES float,     
      @WPID varchar(50)

    )    

Returns float
as
BEGIN    
	Declare @theValue float
	Declare @density float
	
	SET @density = ( SELECT CASE WHEN b.Density is null then (select rockdensity from sysset) / 100 ELSE b.Density / 100 END FROM WORKPLACE a left join reef b on
	a.ReefID = b.ReefID
	WHERE WorkplaceID = @WPID)

	select @theValue = 
    (
    SELECT  @CUBICMETRES * @density
    )
    
IF @theValue is null
   set @theValue = 0	
RETURN ROUND(@theValue,2)
END

GO

CREATE Function [dbo].[CalcFaceTonsSQM]
    (
      @SQM float,
      @ActualSW float,
      @WPID varchar(50)

    )    

Returns float
as
BEGIN    
	Declare @theValue float
	Declare @density float
	
    SET @density = ( SELECT CASE WHEN b.Density is null then (select rockdensity from sysset) / 100 ELSE b.Density / 100 END FROM WORKPLACE a left join reef b on
	a.ReefID = b.ReefID
	WHERE WorkplaceID = @WPID)

	select @theValue = 
    (
    SELECT  @SQM * (@ActualSW / 100) * @density
    )
    
IF @theValue is null
   set @theValue = 0	
RETURN ROUND(@theValue,2)
END

GO

CREATE Function [dbo].[CalcFaceValue]
    (
      @GoldBroken float,
      @FaceTons float

    )    

Returns float
as
BEGIN    
	Declare @theValue float

	
	select @theValue = 
    (
     SELECT  CASE WHEN @FaceTons > 0 THEN (@GoldBroken * 1000) / @FaceTons ELSE 0 END FaceValue
    )
    
IF @theValue is null
   set @theValue = 0	
RETURN ROUND(@theValue,2)
END

GO

CREATE Function [dbo].[CalcGoldBrokenCUB]
    (
      @CubicMetres float,
      @CubicGT float,
      @SW float,
      @WPID varchar(50)

    )    

Returns float
as
BEGIN    
	Declare @theValue float
	Declare @density float
	
	SET @density = ( SELECT CASE WHEN b.Density is null then (select rockdensity from sysset) / 100 ELSE b.Density / 100 END FROM WORKPLACE a left join reef b on
	a.ReefID = b.ReefID
	WHERE WorkplaceID = @WPID)

	select @theValue = 
    (
	(select(	@CubicMetres * @density * @CubicGT))
    )
    
IF @theValue is null
   set @theValue = 0	
RETURN ROUND(@theValue,2)
END


GO

CREATE Function [dbo].[CalcGoldBrokenSQM]
    (
      @SQM float,
      @CMGT float,
      @WPID varchar(50)

    )    

Returns float
as
BEGIN    
	Declare @theValue float
	Declare @density float
	
	SET @density = ( SELECT CASE WHEN b.Density is null then (select rockdensity from sysset) / 100 ELSE b.Density / 100 END FROM WORKPLACE a left join reef b on
	a.ReefID = b.ReefID
	WHERE WorkplaceID = @WPID)

	select @theValue = 
    (
    SELECT  (@SQM * @density * @CMGT) / 1000
    )
    
IF @theValue is null
   set @theValue = 0
   RETURN ROUND(@theValue,2)
END

GO

CREATE Function [dbo].[CalcIdealSW]
    (
      @CW float
    )    

Returns float
as
BEGIN    
	Declare @theValue float

	select @theValue = 
    (
    SELECT CASE 
    WHEN @CW < 100 THEN 120 
    WHEN @CW >= 100 THEN @CW + 20 ELSE 0 END IdealSW 
    )
    
IF @theValue is null
   set @theValue = 0	
RETURN ROUND(@theValue,2)
END

GO

CREATE Function [dbo].[CalcTrammedTons]
    (
      @FaceTons float

    )    

Returns float
as
BEGIN    
	Declare @theValue float

	
	select @theValue = 
    (
     SELECT   @FaceTons / 0.9 
    )
    
IF @theValue is null
   set @theValue = 0	
RETURN ROUND(@theValue,2)
END

GO

CREATE Function [dbo].[CalcTrammedValue]
    (
      @TrammedTons float,
      @GoldBroken float

    )    

Returns float
as
BEGIN    
	Declare @theValue float

	
	select @theValue = 
    (
     SELECT   CASE WHEN @TrammedTons > 0 THEN (@GoldBroken * 1000) / @TrammedTons ELSE 0 END TrammedValue
    )
    
IF @theValue is null
   set @theValue = 0	
RETURN ROUND(@theValue,2)
END

GO

Create function [dbo].[CalcUraniumBrokenCUB]
    (
      @CubicMetres float,
	   @CubicGT float,
     -- @CMGT float,
      @SW float,
      @WPID varchar(50)

    )    

Returns float
as
BEGIN    
	Declare @theValue float
	Declare @density float
	
	SET @density = ( SELECT CASE WHEN b.Density is null then (select rockdensity from sysset) / 100 ELSE b.Density / 100 END FROM WORKPLACE a left join reef b on
	a.ReefID = b.ReefID
    WHERE WorkplaceID = @WPID)

	if @SW=0
	
	begin

	select @SW=case when activity=1 then Endheight*100 else endheight end from workplace where workplaceid=@WPID 
	
	end
	select @theValue = 
    --(
    --SELECT CASE WHEN @SW > 0 THEN
    --(SELECT  ((@CubicMetres / (@SW / 100)) * @density) * (@CMGT)) ELSE 0 END GoldUraniumCUB
    --)

	  (
	(select Case When @SW = 0  then 0 else	@CubicMetres * @density * @CubicGT/@SW end)
  --  (SELECT  ((@CubicMetres / (@SW / 100)) * @density) * (@CMGT/1000)) ELSE 0 END GoldBrokenCUB
    )
  
IF @theValue is null
   set @theValue = 0	
RETURN ROUND(@theValue,2)
END


GO

CREATE function [dbo].[CalcUraniumBrokenSQM]
    (
      @SQM float,
      @CMGT float,
      @WPID varchar(50)

    )    

Returns float
as
BEGIN    
	Declare @theValue float
	Declare @density float
	
	SET @density = ( SELECT CASE WHEN b.Density is null then (select rockdensity from sysset) / 100 ELSE b.Density / 100 END FROM WORKPLACE a left join reef b on
	a.ReefID = b.ReefID
	WHERE WorkplaceID = @WPID)

	select @theValue = 
    (
    SELECT  (@SQM * @density * @CMGT /100)
    )
    
IF @theValue is null
   set @theValue = 0
   RETURN ROUND(@theValue,2)
END

GO

Create view vw_Sampling_Latest
as
Select b.Prodmonth, a.* from sampling a inner join (select a.Prodmonth, a.Workplaceid, Max(d.CalendarDate) CalendarDate from PLANMONTH a inner join SECTION_COMPLETE b on 
a.Prodmonth = b.Prodmonth and
a.sectionid = b.SectionID and
plancode = 'MP'
inner join SECCAL c on
b.Prodmonth = c.Prodmonth and
b.Sectionid_1 = c.Sectionid 
inner join   
SAMPLING d on 
a.Workplaceid = d.WorkplaceID and
c.begindate <= d.CalendarDate and
c.EndDate >= d.CalendarDate
group by a.Prodmonth, a.Workplaceid) b on
a.WorkplaceID = b.Workplaceid and
a.CalendarDate = b.CalendarDate
go

Create view vw_Kriging_Latest
as
Select a.* from KRIGING a inner join (select Prodmonth, Workplaceid, Max(WeekNO) WeekNO from KRIGING group by Prodmonth, Workplaceid) b on
a.ProdMonth = b.ProdMonth and
a.WorkplaceID = b.Workplaceid and
a.WeekNO = b.WeekNO
go

Create Procedure [dbo].[sp_PrePlanning_GetApprovedInfo]
 @Prodmonth VARCHAR(6), @CurrentUser VARCHAR(60),@SectionID_2 VARCHAR(20),@Activity INT

AS


DECLARE @TemplateID INT, @WorkplaceID VARCHAR(30),@WorkplaceDesc VARCHAR(100), @theCount INT

--DECLARE @Prodmonth VARCHAR(6),@SectionID_2 VARCHAR(20), @CurrentUser VARCHAR(60), @Activity INT
--SET @Prodmonth = '201705'
--SET @CurrentUser = 'MINEWARE_Rushabh'
--SET @SectionID_2 = 'REA'
--SET @Activity = 0



DELETE FROM dbo.PREPLANNING_APPROVE_TEMP WHERE currentUser = @CurrentUser;

--SELECT  Workplaceid,WorkplaceDesc FROM dbo.PrePlanning WHERE Activity = @Activity AND Sectionid_2 = @SectionID_2 AND Prodmonth = @Prodmonth AND Locked = 0

DECLARE db_Templates CURSOR FOR  
SELECT TemplateID FROM dbo.PLANPROT_TEMPLATE WHERE Activity  = @Activity

OPEN db_Templates   
FETCH NEXT FROM db_Templates INTO @TemplateID   

WHILE @@FETCH_STATUS = 0   
BEGIN  

DECLARE db_WorkplaceList CURSOR FOR  
SELECT  a.Workplaceid, c.Description WorkplaceDesc FROM dbo.PLANMONTH a inner join 
SECTION_COMPLETE b on
a.Prodmonth = b.prodmonth and
a.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
a.Workplaceid = c.WorkplaceID
  WHERE a.Activity = @Activity AND Sectionid_2 = @SectionID_2 AND a.Prodmonth = @Prodmonth AND Locked = 0 and plancode = 'MP'

OPEN db_WorkplaceList   
FETCH NEXT FROM db_WorkplaceList INTO @WorkplaceID,@WorkplaceDesc

WHILE @@FETCH_STATUS = 0   
BEGIN  


INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT  @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER(PPT.TemplateName),'(B) - Departments Data', CASE WHEN (Approved IS NULL or Approved = 0) and ApprovalRequired = 1 THEN -1
                                                       WHEN Approved IS NULL and ApprovalRequired = 0 THEN 0
                                                       WHEN Approved = 1 THEN 1 ELSE -1 END CanApprove,@Activity ,
                                                       @SectionID_2,'',@Prodmonth  FROM dbo.PlanProt_Template PPT
LEFT JOIN (SELECT TemplateID,WORKPLACEID,SectionID,PPAD.PRODMONTH,CASE WHEN Approved IS NULL THEN -1 ELSE 1 END Approved  FROM dbo.PlanProt_DataApproved PPAD WHERE PPAD.WORKPLACEID = @WorkplaceID AND
                        PRODMONTH = @Prodmonth AND
                        PPAD.TemplateID = @TemplateID AND
                        PPAD.ActivityType = @Activity and
					PPAD.[ApprovedDate] = 
					(SELECT MAX([ApprovedDate]) from PlanProt_DataApproved 
					WHERE [PRODMONTH] = @Prodmonth and
					      [SECTIONID]  = PPAD.[SECTIONID] and
                          [WORKPLACEID] = PPAD.[WORKPLACEID] and
                          [TemplateID] = @TemplateID and
						  ActivityType = @Activity)
  					) PPAD ON
                        PPT.TemplateID = PPAD.TemplateID 
left JOIN dbo.PLANMONTH  PP ON
PP.Workplaceid = PPAD.WORKPLACEId AND
PPAD.PRODMONTH = PP.Prodmonth AND
pp.Sectionid = PPAD.SectionID                        
WHERE PPT.TemplateID = @TemplateID -- AND PP.PLANCODE='MP'
                        
                        
                         
                                            
FETCH NEXT FROM db_WorkplaceList INTO @WorkplaceID,@WorkplaceDesc
END
CLOSE db_WorkplaceList   
DEALLOCATE db_WorkplaceList
       FETCH NEXT FROM db_Templates INTO @TemplateID   
END   

CLOSE db_Templates   
DEALLOCATE db_Templates 



DECLARE db_WorkplaceList CURSOR FOR  
SELECT  a.Workplaceid,c.Description WorkplaceDesc FROM dbo.PLANMONTH a inner join 
SECTION_COMPLETE b on
a.Prodmonth = b.prodmonth and
a.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
a.Workplaceid = c.WorkplaceID WHERE a.Activity = @Activity AND Sectionid_2 = @SectionID_2 AND a.Prodmonth = @Prodmonth and Plancode = 'MP'

OPEN db_WorkplaceList   
FETCH NEXT FROM db_WorkplaceList INTO @WorkplaceID,@WorkplaceDesc

WHILE @@FETCH_STATUS = 0   
BEGIN  
print  @WorkplaceDesc
print @WorkplaceID 
INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Miner Selected'),'(A) - Planning Data',CASE WHEN pp.Sectionid <> '-1' AND pp.Sectionid IS NOT NULL THEN 1 ELSE -1 END Approved,@Activity,
       @SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP 
	   inner join 
SECTION_COMPLETE b on
pp.Prodmonth = b.prodmonth and
pp.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
pp.Workplaceid = c.WorkplaceID
WHERE pp.Activity = @Activity AND
      pp.Prodmonth = @Prodmonth AND
       c.Description = @WorkplaceDesc AND
      Locked = 0 AND PP.PLANCODE='MP'
 
IF @WorkplaceID <> '-1' 
BEGIN    
INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Workplace Info Valid'),'(A) - Planning Data',CASE WHEN (SELECT OreFlowID  FROM WORKPLACE WHERE WorkplaceID = @WorkplaceID ) = '' OR (SELECT OreFlowID FROM WORKPLACE WHERE WorkplaceID = @WorkplaceID ) IS NOT NULL THEN 1 ELSE -1 END Approved,@Activity,
       @SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP 
WHERE Activity = @Activity AND
      Prodmonth = @Prodmonth AND
       WorkplaceID = @WorkplaceID AND
      Locked = 0 AND PLANCODE='MP'
END
ELSE
BEGIN
INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Workplace Info Valid'),'(A) - Planning Data', -1  Approved,@Activity,
       @SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP 
	   inner join 
SECTION_COMPLETE b on
pp.Prodmonth = b.prodmonth and
pp.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
pp.Workplaceid = c.WorkplaceID
WHERE pp.Activity = @Activity AND
      pp.Prodmonth = @Prodmonth AND
      c.Description = @WorkplaceDesc AND
      Locked = 0 AND PLANCODE='MP'
END
      
--SELECT * FROM WORKPLCE WHERE [DESCRIPTION] = 'VC45 43 FWDN'     
            
IF @Activity = 0 
BEGIN      
      INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Valid Values'),'(A) - Planning Data',CASE WHEN SQM  = 0 AND WasteSQM  = 0 AND CubicMetres = 0  THEN -1 ELSE 1 END Approved,@Activity
       ,@SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP  
	   inner join 
SECTION_COMPLETE b on
pp.Prodmonth = b.prodmonth and
pp.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
pp.Workplaceid = c.WorkplaceID

WHERE pp.Activity = @Activity AND
      pp.Prodmonth = @Prodmonth AND
      c.Description = @WorkplaceDesc AND
      Locked = 0 AND PLANCODE='MP'
END    

IF @Activity = 0 
BEGIN      
      INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Valid Stope Width'),'(A) - Planning Data',CASE WHEN (SW = 0) and (pp.[TargetID] <> 50) and  (CubicMetres = 0 ) THEN -1 ELSE 1 END Approved,@Activity
       ,@SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP 
	   inner join 
SECTION_COMPLETE b on
pp.Prodmonth = b.prodmonth and
pp.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
pp.Workplaceid = c.WorkplaceID
 
WHERE pp.Activity = @Activity AND
      pp.Prodmonth = @Prodmonth AND
      c.Description = @WorkplaceDesc AND
      Locked = 0 AND PLANCODE='MP'
END 

IF @Activity = 0 
BEGIN      
      INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Valid Face Length'),'(A) - Planning Data',CASE WHEN FL = 0 and WasteSQM  = 0   and pp.[TargetID] <> 50  AND CubicMetres = 0  THEN -1 ELSE 1 END Approved,@Activity
       ,@SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP  
	   inner join 
SECTION_COMPLETE b on
pp.Prodmonth = b.prodmonth and
pp.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
pp.Workplaceid = c.WorkplaceID
 
WHERE pp.Activity = @Activity AND
      pp.Prodmonth = @Prodmonth AND
      c.Description = @WorkplaceDesc AND
      Locked = 0 AND PLANCODE='MP'
END

IF @Activity = 0 
BEGIN      
      INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Valid CMGT'),'(A) - Planning Data',CASE WHEN CMGT = 0  and WasteSQM  = 0 and CubicMetres = 0    THEN -1 ELSE 1 END Approved,@Activity
       ,@SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP  
	   inner join 
SECTION_COMPLETE b on
pp.Prodmonth = b.prodmonth and
pp.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
pp.Workplaceid = c.WorkplaceID
 
WHERE pp.Activity = @Activity AND
      pp.Prodmonth = @Prodmonth AND
      c.Description = @WorkplaceDesc AND
      Locked = 0 AND PLANCODE='MP'
END

IF @Activity = 1 
BEGIN  
  
      INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Planned Values'),'(A) - Planning Data',CASE WHEN Metresadvance  = 0 AND WasteAdv  = 0 AND CubicMetres = 0  THEN -1 ELSE 1 END Approved,@Activity
       ,@SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP  
	   inner join 
SECTION_COMPLETE b on
pp.Prodmonth = b.prodmonth and
pp.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
pp.Workplaceid = c.WorkplaceID
 
WHERE pp.Activity = @Activity AND
      pp.Prodmonth = @Prodmonth AND
      c.Description = @WorkplaceDesc AND
      Locked = 0 AND PLANCODE='MP'
 END

INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Crew Selected'),'(A) - Planning Data',CASE WHEN (OrgUnitAfternoon <> '' and OrgUnitAfternoon IS NOT NULL)  THEN 1
                                                  WHEN (OrgUnitDay <> '' and OrgUnitDay IS NOT NULL)  THEN 1
                                                  WHEN (OrgUnitNight <> '' and OrgUnitNight IS NOT NULL)  THEN 1 ELSE -1 END Approved,@Activity
                                                  ,@SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP 
	   inner join 
SECTION_COMPLETE b on
pp.Prodmonth = b.prodmonth and
pp.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
pp.Workplaceid = c.WorkplaceID
 
WHERE pp.Activity = @Activity AND
      pp.Prodmonth = @Prodmonth AND
      c.Description = @WorkplaceDesc AND
      Locked = 0 AND PLANCODE='MP'
      
INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Valid Workplace Name '),'(A) - Planning Data',CASE WHEN pp.workplaceID = '-1' THEN -1 ELSE 1 END Approved,@Activity
       ,@SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP 
	   inner join 
SECTION_COMPLETE b on
pp.Prodmonth = b.prodmonth and
pp.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
pp.Workplaceid = c.WorkplaceID
 
WHERE pp.Activity = @Activity AND
      pp.Prodmonth = @Prodmonth AND
      c.Description = @WorkplaceDesc AND
      Locked = 0 AND PLANCODE='MP'
      
--INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
--        ( currentUser ,
--          workplaceID ,
--          WorkplaceDesc ,
--          TemplateName ,
--          TheGroup ,
--          CanApprove,
--          Activity,
--          SectionID_2,
--          SectionID,
--          Prodmonth
--        )
--SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
--       UPPER('Mining Method Selected '),'(A) - Planning Data',CASE WHEN pp.TargetID <= 0 THEN - 1 ELSE 1 END Approved,@Activity
--       ,@SectionID_2,PP.Sectionid,pp.Prodmonth FROM dbo.PLANMONTH  PP  
--	   inner join 
--SECTION_COMPLETE b on
--pp.Prodmonth = b.prodmonth and
--pp.Sectionid = b.SECTIONID 
--inner join WORKPLACE c on
--pp.Workplaceid = c.WorkplaceID
 
--WHERE pp.Activity = @Activity AND
--      pp.Prodmonth = @Prodmonth AND
--      c.Description = @WorkplaceDesc AND
--      Locked = 0 AND PLANCODE='MP'
      
SET @theCount = (
SELECT COUNT(CanApprove) FROM dbo.PREPLANNING_APPROVE_TEMP WHERE currentUser = @CurrentUser AND
CanApprove = -1 AND workplaceID = @WorkplaceID)
--Print '@@'

IF @theCount = 0
BEGIN
SET @theCount = 2

END
ELSE 
BEGIN
SET @theCount = -1
END

INSERT INTO dbo.PREPLANNING_APPROVE_TEMP
        ( currentUser ,
          workplaceID ,
          WorkplaceDesc ,
          TemplateName ,
          TheGroup ,
          CanApprove,
          Activity,
          SectionID_2,
          SectionID,
          Prodmonth,
          CubicMetres,
          Meters ,
          MetersWaste ,
          SQM ,
          WasteSQM 
        )
SELECT DISTINCT @CurrentUser,@WorkplaceID,@WorkplaceDesc WorkplaceDesc,
       UPPER('Can Approve Workplace'),'(C)',@theCount Approved,@Activity,@SectionID_2,PP.Sectionid,pp.Prodmonth,
       pp.CubicMetres,pp.Metresadvance ,PP.WasteAdv ,pp.SQM,pp.WasteSQM  FROM dbo.PLANMONTH  PP  
	   inner join 
SECTION_COMPLETE b on
pp.Prodmonth = b.prodmonth and
pp.Sectionid = b.SECTIONID 
inner join WORKPLACE c on
pp.Workplaceid = c.WorkplaceID
 
WHERE pp.Activity = @Activity AND
      pp.Prodmonth = @Prodmonth AND
      c.Description = @WorkplaceDesc AND
      Locked = 0 AND PLANCODE='MP'
           
       
FETCH NEXT FROM db_WorkplaceList INTO @WorkplaceID,@WorkplaceDesc
  
END    

CLOSE db_WorkplaceList   
DEALLOCATE db_WorkplaceList  


   

SELECT * FROM dbo.PREPLANNING_APPROVE_TEMP WHERE currentUser = @CurrentUser
SELECT * FROM dbo.PREPLANNING_APPROVE_TEMP WHERE currentUser = @CurrentUser and CanApprove = 2
ORDER BY TheGroup







GO


Create procedure [dbo].[sp_CheckRevisedPlanning]
--Declare 
@Prodmonth numeric(7),
@Section Varchar(30)
--@SumLevel int
as
 Declare @TheLevel Int,
        @SQL1 Varchar(MAX),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20)



--set @Prodmonth = 201602
--set @Section = '5972400'

select @TheLevel =  HIERARCHICALID from section where PRODMONTH =@Prodmonth and
sectionid =@Section

--select @TheLevel


  If @TheLevel = 1 
    set @SectionLevel = 'Sectionid_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'Sectionid_4'  
    
   If @TheLevel = 3 
    set @SectionLevel = 'Sectionid_3'  
     
   If @TheLevel = 4 
    set @SectionLevel = 'Sectionid_2'  
    
   If @TheLevel = 5 
    set @SectionLevel = 'Sectionid_1'

   If @TheLevel = 6 
    set @SectionLevel = 'Sectionid'
 
 Set @SQL1 = 'select distinct RS.Section ID,RS.UserID,RS.Department,RS.SecurityType,cs.SecurityType Description,
 RS.ApprovalRequired,RS.Authorize,'''' added,'''' edited,'''' hasChanged from REVISEDPLANNING_SECURITY RS 
 INNER JOIN REVISEDPLANNING_USERSECURITY_ACTIONS RU ON  
 RU.DEPARTMENT=RS.DEPARTMENT
 INNER JOIN SECTION_COMPLETE SC ON
RS.SECTION=SC.'+@SectionLevel+'
 inner join CODE_SECURITYTYPE cs on 
                                    cs.SecurityTypeID=RS.SecurityType and RS.SecurityType=1
WHERE --SC.PRODMONTH = 201508 and 
      SC.SECTIONID_2 ='''+@Section+'''
	  UNION
	  select distinct RS.Section ID,RS.UserID,RS.Department,RS.SecurityType,cs.SecurityType Description,
 RS.ApprovalRequired,RS.Authorize,'''' added,'''' edited,'''' hasChanged from REVISEDPLANNING_SECURITY RS 
 INNER JOIN REVISEDPLANNING_USERSECURITY_ACTIONS RU ON  
 RU.DEPARTMENT=RS.DEPARTMENT
 INNER JOIN SECTION_COMPLETE SC ON
RS.SECTION=SC.SECTIONID_3
 inner join CODE_SECURITYTYPE cs on 
                                    cs.SecurityTypeID=RS.SecurityType and RS.SecurityType=1
WHERE --SC.PRODMONTH = 201508 and 
      SC.SECTIONID_2 ='''+@Section+''''

	  --select @SQL1

	  Exec(@SQL1)
GO

Create Procedure [dbo].[sp_PlanProt_ApproveData]

--DECLARE 
@Prodmonth NUMERIC(7,0), @SectionID VARCHAR(10), @WorkplaceID VARCHAR(30), @TemplateID INT,@ActivityType INT,
@ApprovedBy VARCHAR(30), @ApproveItem VARCHAR(10)

-- spPlanProt_ApproveData 201108,'25884','789955',1,0,'DOLF'

AS

--DECLARE 
--@Prodmonth NUMERIC(7,0), @SectionID VARCHAR(10), @WorkplaceID VARCHAR(30), @TemplateID INT,@ActivityType INT,
--@ApprovedBy VARCHAR(30), @ApproveItem VARCHAR(10)

--SET @ActivityType = 0
--SET @ApprovedBy = 'MINEWARE_dolf'
--SET @Prodmonth = 201706
--SET @SectionID = 'REA'
--SET @TemplateID = 16
--SET @WorkplaceID = 'RE007619'
--SET @ApproveItem = 'YES'



DECLARE @ApprovedDate DATETIME
DECLARE @ApprovedID INT
SET @ApprovedDate = GETDATE()
Declare @MinerSectionID VARCHAR(10)

IF @ApproveItem = 'NO'
BEGIN
-- test if has data
SET @ApprovedID = (SELECT MAX(ApproveID) FROM dbo.PlanProt_DataApproved 
WHERE ActivityType = @ActivityType AND
      PRODMONTH = @Prodmonth AND
      WORKPLACEID = @WorkplaceID AND
      SECTIONID = @SectionID AND
      TemplateID = @TemplateID)
   
      
IF  @ApprovedID > 0 
BEGIN
-- get the ID
SET @ApprovedID = (SELECT ApproveID FROM dbo.PlanProt_DataApproved
WHERE ActivityType = @ActivityType AND
      PRODMONTH = @Prodmonth AND
      WORKPLACEID = @WorkplaceID AND
      SECTIONID = @SectionID AND
      TemplateID = @TemplateID)

      
DELETE from a  FROM PlanProt_DataLocked a Inner join PLANPROT_FIELDS b on
a.FieldID = b.FieldID
Where ActivityType = @ActivityType AND
      PRODMONTH = @Prodmonth AND
      WORKPLACEID = @WorkplaceID AND
      SECTIONID = @SectionID AND
      TemplateID = @TemplateID 

DELETE FROM dbo.PlanProt_DataApproved
WHERE ActivityType = @ActivityType AND
      PRODMONTH = @Prodmonth AND
      WORKPLACEID = @WorkplaceID AND
      SECTIONID = @SectionID AND
      TemplateID = @TemplateID       
END     
  
END

IF @ApproveItem = 'YES'
BEGIN


Select @MinerSectionID = Sectionid from Planmonth where PRODMONTH = @Prodmonth and Workplaceid = @WorkplaceID

INSERT INTO dbo.PlanProt_DataApproved
        ( PRODMONTH ,
          SECTIONID ,
          WORKPLACEID ,
          ActivityType ,
          TemplateID ,
          Approved ,
          ApprovedBy ,
          ApprovedDate
        )
VALUES  ( @Prodmonth , -- PRODMONTH - numeric
          @SectionID , -- SECTIONID - varchar(10)
          @WorkplaceID , -- WORKPLACEID - varbinary(30)
          @ActivityType , -- ActivityType - int
          @TemplateID , -- TemplateID - int
          1 , -- Approved - bit
          @ApprovedBy , -- ApprovedBy - varchar(30)
          @ApprovedDate  -- ApprovedDate - datetime
        )


SET @ApprovedID = (
SELECT ApproveID FROM dbo.PlanProt_DataApproved
WHERE PRODMONTH = @Prodmonth AND
      SECTIONID = @SectionID AND
      WORKPLACEID = @WorkplaceID AND
      ActivityType = @ActivityType AND
      TemplateID = @TemplateID)
      
INSERT INTO dbo.PlanProt_DataLocked
        ( ApprovedID ,
          PRODMONTH ,
          SECTIONID ,
          WORKPLACEID ,
          FieldID ,
          TheValue ,
          ActivityType ,
          CaptureDate
        )   


SELECT @ApprovedID,@Prodmonth PRODMONTH,@SectionID SECTIONID,PPD.WORKPLACEID,PPD.FieldID,
(SELECT CASE WHEN FieldName = 'Signature Date' Then cast(@ApprovedDate as varchar(max)) else PPD.TheValue end TheValue FROM PlanProt_Fields WHERE FieldID = PPD.FieldID)
,PPD.ActivityType,PPD.CaptureDate FROM dbo.PlanProt_Data PPD
INNER JOIN dbo.PlanProt_Fields PPF ON
PPD.FieldID = PPF.FieldID
WHERE PPF.TemplateID = @TemplateID AND
      PPD.ActivityType = @ActivityType AND
      PPD.PRODMONTH = @Prodmonth AND
      PPD.SECTIONID = @SectionID AND
      PPD.WORKPLACEID = @WorkplaceID 

END                                                                             

DECLARE @CMGT numeric(10,3),@CW numeric(10,3),@SW numeric(10,3),@KG numeric(10,3),@IdealSW numeric(10,3),@CMKGT numeric(10,3)

IF @TemplateID= 16 AND @ActivityType = 0
BEGIN
SELECT @CMGT = theUpdate.CMGT,
	   @CMKGT= theUpdate.CMKGT,	
                        @CW = theUpdate.ChannelW,
                        @SW = theUpdate.ActualSW,
						@KG = dbo.CalcGoldBrokenSQM(theUpdate.SQM ,theUpdate.CMGT,theUpdate.WORKPLACEID)  + 
						dbo.CalcGoldBrokenCUB(theUpdate.CUBICMETRES,theUpdate.CMGT,theUpdate.ActualSW,theUpdate.WORKPLACEID),
						@IdealSW = theUpdate.IdealSW

FROM( SELECT pp.Prodmonth,WORKPLACEID,pp.Sectionid,Sectionid_2,SQM,CUBICMETRES,
      CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.GetApprovedCMGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CMGT END CMGT,
	  CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.GetApprovedCMKGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CMKGT END CMKGT,
      CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.GetApprovedCW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CW END ChannelW,
      CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE SW END ActualSW,
	  CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
	        THEN dbo.[GetApprovedIdealSW](PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE IdealSW END IdealSW           
         FROM Planmonth PP inner join SECTION_COMPLETE b on
		 pp.Prodmonth = b.Prodmonth and
		 pp.Sectionid = b.SectionID
WHERE Activity IN (0)  AND
      b.Sectionid_2 = @SectionID AND
      PP.Prodmonth = @Prodmonth AND Plancode = 'MP' and Workplaceid = @WorkplaceID) theUpdate


DECLARE @SampCountCM INT


SET @SampCountCM = 
(SELECT COUNT(Prodmonth) FROM dbo.vw_Kriging_Latest 
WHERE Prodmonth = @Prodmonth AND
      WORKPLACEID = @WorkplaceID and WeekNo = 1)

IF @SampCountCM = 1      
BEGIN
UPDATE dbo.KRIGING SET  StopeWidth = @SW, 
                        ChannelWidth = @CW,
                        CMGT = @CMGT,
                        gt = case when (@SW = 0) or (@SW is null) then 0 else @CMGT / @SW end

WHERE WORKPLACEID = @WorkplaceID AND
      Prodmonth = @Prodmonth and WeekNo  =1


Update PP set 
--Select
CMGT = isnull(dbo.GetApprovedCMGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0), 
CW = isnull(dbo.GetApprovedCW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0),
SW = isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0),
Kg = (ReefSQM * isnull(dbo.GetApprovedCMGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0) / 100 * Density)/1000,
Tons = (SQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * Density),
ReefTons = (ReefSQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * Density),
WasteTons = (WasteSQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * Density) 
                  
         FROM Planmonth PP inner join SECTION_COMPLETE b on
		 pp.Prodmonth = b.Prodmonth and
		 pp.Sectionid = b.SectionID
		 inner join WORKPLACE c on
		 pp.Workplaceid = c.Workplaceid
WHERE pp.Activity IN (0)  AND
      --b.Sectionid_2 = @SectionID AND
      PP.Prodmonth = @Prodmonth AND Plancode = 'MP' and pp.Workplaceid = @WorkplaceID
END

IF @SampCountCM = 0
BEGIN
INSERT INTO dbo.KRIGING
        ( Prodmonth ,
		  SectionID,Activity,
          WORKPLACEID ,
          WeekNo           
        )
VALUES  ( @Prodmonth , -- Prodmonth - numeric
          @MinerSectionID,
		  @ActivityType,
          @WorkplaceID , -- WORKPLACEID - varchar(12)
          1  -- SAMPLINGDATE - datetime
         
        )
               
UPDATE dbo.KRIGING SET  StopeWidth = @SW, 
                        ChannelWidth = @CW,
                        CMGT = @CMGT,
                        gt = case when (@SW = 0) or (@SW is null) then 0 else @CMGT / @SW end
                          
WHERE WORKPLACEID = @WorkplaceID AND
      Prodmonth = @Prodmonth  and WeekNo  =1


Update PP set 
--Select
CMGT = isnull(dbo.GetApprovedCMGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0), 
CW = isnull(dbo.GetApprovedCW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0),
SW = isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0),
Kg = (ReefSQM * isnull(dbo.GetApprovedCMGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0) / 100 * Density)/1000,
Tons = (SQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * Density),
ReefTons = (ReefSQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * Density),
WasteTons = (WasteSQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * Density) 
                  
         FROM Planmonth PP inner join SECTION_COMPLETE b on
		 pp.Prodmonth = b.Prodmonth and
		 pp.Sectionid = b.SectionID
		 inner join WORKPLACE c on
		 pp.Workplaceid = c.Workplaceid
WHERE pp.Activity IN (0)  AND
      --b.Sectionid_2 = @SectionID AND
      PP.Prodmonth = @Prodmonth AND Plancode = 'MP' and pp.Workplaceid = @WorkplaceID
      
END

END

IF @TemplateID= 14 AND @ActivityType = 1
BEGIN
SELECT @CMGT = theUpdate.CMGT,
	   @CMKGT= theUpdate.CMKGT,	
                        @CW = theUpdate.ChannelW--,


FROM( SELECT pp.Prodmonth,WORKPLACEID,pp.Sectionid,Sectionid_2,SQM,CUBICMETRES,
      CASE WHEN dbo.IsEvalApproved_Dev(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.[GetApprovedCMGT_Dev](PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CMGT END CMGT,
	  CASE WHEN dbo.IsEvalApproved_Dev(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.[GetApprovedCMKGT_Dev](PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CMKGT END CMKGT,
      CASE WHEN dbo.IsEvalApproved_Dev(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.GetApprovedCW_Dev(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CW END ChannelW 
       
         FROM Planmonth PP inner join SECTION_COMPLETE b on
		 pp.Prodmonth = b.Prodmonth and
		 pp.Sectionid = b.SectionID
WHERE Activity IN (1)  AND
      b.Sectionid_2 = @SectionID AND
      PP.Prodmonth = @Prodmonth AND Plancode = 'MP' and Workplaceid = @WorkplaceID) theUpdate


--DECLARE @SampCountCM INT


SET @SampCountCM = 
(SELECT COUNT(Prodmonth) FROM dbo.vw_Kriging_Latest 
WHERE Prodmonth = @Prodmonth AND
      WORKPLACEID = @WorkplaceID)

IF @SampCountCM = 1      
BEGIN
UPDATE dbo.KRIGING SET  StopeWidth = @SW, 
                        ChannelWidth = @CW,
                        CMGT = @CMGT,
                        gt = case when (@SW = 0) or (@SW is null) then 0 else @CMGT / @SW end

WHERE WORKPLACEID = @WorkplaceID AND
      Prodmonth = @Prodmonth and WeekNo  =1

END

IF @SampCountCM = 0
BEGIN
INSERT INTO dbo.KRIGING
        ( Prodmonth ,
		  SectionID,Activity,
          WORKPLACEID ,
          WeekNo           
        )
VALUES  ( @Prodmonth , -- Prodmonth - numeric
          @MinerSectionID,
		  @ActivityType,
          @WorkplaceID , -- WORKPLACEID - varchar(12)
          1  -- SAMPLINGDATE - datetime
         
        )


               
UPDATE dbo.KRIGING SET  StopeWidth = @SW, 
                        ChannelWidth = @CW,
                        CMGT = @CMGT,
                        gt = case when (@SW = 0) or (@SW is null) then 0 else @CMGT / @SW end

WHERE WORKPLACEID = @WorkplaceID AND
      Prodmonth = @Prodmonth and WeekNo  =1
end
end

GO

CREATE Procedure [dbo].[sp_Create_Lock_Plan]

--DECLARE
@Prodmonth NUMERIC(7,0),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20), 
@Activity NUMERIC(7,0), 
@IsCubics VARCHAR(5), 
@UserID VARCHAR(20)


----[sp_Create_Lock_Plan] 201604,'233430','26430','23182',1,'N','MINEWARE_D'

--SET @Prodmonth=201705
--SET @SectionID='REAAHDA'
--SET @Sectionid_2='REA'
--SET @WorkplaceID='RE007667'
--SET @Activity=0
--SET @IsCubics='N'
--SET @UserID = 'MINEWARE_DOLF'

AS

declare @ProdmonthCurrent NUMERIC(7,0)
declare @Lock bit


SET @ProdmonthCurrent = (SELECT 

Distinct min(s.prodmonth) CurrentProductionMonth 

FROM [dbo].[SECTION] S
INNER JOIN [dbo].[SECCAL] SC ON
S.[Prodmonth] = SC.[Prodmonth] and
S.SectionID = SC.SectionID and
SC.BeginDate <=  Convert(date,getdate())  and -- DateADD(DAY,-1, getdate()
SC.enddate >=  Convert(date,getdate())  -- DateADD(DAY,-10, getdate()
INNER JOIN SECTION_COMPLETE SECO on
SECO.[Prodmonth] = S.[Prodmonth] and
SECO.SectionID_1 = S.SectionID 

WHERE SECO.[SectionID_2]= @Sectionid_2 )


if @ProdmonthCurrent >= @Prodmonth
begin
	set @Lock = 0 
end 
else 
begin
	set @Lock = 1
end

Delete from Planmonth   FROM [dbo].[PLANMONTH] WHERE WORKPLACEID = @WorkplaceID AND PRODMONTH = @Prodmonth AND Activity = @Activity and IsCubics = @IsCubics and Plancode = 'LP' and SectionID = @Sectionid 

INSERT INTO [PLANMONTH] ([Prodmonth]
           ,[Sectionid]
           --,[Sectionid_2]
           --,[WorkplaceDesc]
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
           ,[MOCycle]
           ,[Vac]
           ,[DC]
           ,[MOCycleNum]
           ,[DevFlag]
           ,[BoxHoleID]
           ,[CMKGT]
           ,[UraniumBrokenKG]
		   )
SELECT [Prodmonth]
           ,[Sectionid]
           --,[Sectionid_2]
           --,[WorkplaceDesc]
           ,[Activity]
           ,[IsCubics]
           ,'LP' [PlanCode]
           ,[StartDate]
           ,[Workplaceid]
           ,[TargetID]
           ,[OrgUnitDay]
           ,[OrgUnitAfternoon]
           ,[OrgUnitNight]
           ,[RomingCrew]
           ,@Lock [Locked]
           ,'Y' [Auth]
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
           ,'' [LockedDate]
           ,'' [LockedBY]
           ,[DrillRig]
           ,[StoppedDate]
           ,[EndDate]
           ,[IsStopped]
           ,[TopEnd]
           ,[AutoUnPlan]
           ,[LabourStrength]
           ,[MOCycle]
           ,[Vac]
           ,[DC]
           ,[MOCycleNum]
           ,[DevFlag]
           ,[BoxHoleID]
           ,[CMKGT]
           ,[UraniumBrokenKG]
  FROM [dbo].[PLANMONTH] WHERE WORKPLACEID = @WorkplaceID AND PRODMONTH = @Prodmonth AND Activity = @Activity and IsCubics = @IsCubics and Plancode = 'MP' and SectionID = @Sectionid  



GO



CREATE Procedure [dbo].[SP_Save_Dev_CyclePlan]
--Declare
@Username VARCHAR(50), 
@ProdMonth VARCHAR(20), 
@WorkplaceID VARCHAR(50),
@SectionID VARCHAR(50), 
@Activity VARCHAR(20), 
@IsCubics Varchar(1)

AS

--Select
--@Username = 'MINEWARE_D',
--@ProdMonth = 201705,
--@WorkplaceID = 'RE010502',
--@SectionID = 'REHAKAA',
--@Activity = 1,
--@IsCubics = 'N'

--select * from planmonth where prodmonth = 201505 and activity = 1
--and plancode = 'MP'
Declare
@SB VARCHAR(50), 
@Adv Numeric(18,10), 
@CMGT NUMERIC(18,2), 
@Tons NUMERIC(18,2), 
@StartShift NUMERIC(18),
@EndShift NUMERIC(18),
@STOPEWIDTH NUMERIC(18),
@Facelength NUMERIC(18),
@GoldGramsPerTon NUMERIC(18,3),
@OnReefADV NUMERIC(18),  
@OffreefPer Numeric(18,3),
@BeginDate DateTime,
@EndDate DateTime,
@TheDate DateTime,
@Locked int,
@MVar Numeric(10,1),
@PerOnReef Numeric(18,3),
@Workingday Varchar(1),
@RemainderAdv Numeric(10,3),
@RemainderCubics Numeric(10),
@TotalShifts int,
@ShiftNo int,
@SaveShiftNo int,
@TotalBlasts int,
@BlastNo int,
@AdvPerBlast Numeric(10,1),
@CubicsPerBlast Numeric(10),
@Remaining_Shifts Numeric(10,1),
@Remaining_Blasts Numeric(10),
@CubicMetres NUMERIC(18),
@MOCycle Varchar(5),
@Density Numeric(10,3)


--select @Username = 'Mineware', 
--@ProdMonth = '201703', 
--@WorkplaceID = '35033',
--@SectionID  = '3555400', 
--@Activity  = '1', 
--@IsCubics  = 'N'


      
Create Table #Planning (
			[Prodmonth] [numeric](7, 0) NOT NULL,
			[SectionID] [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
			[WorkplaceID] [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
			ACTIVITY  [numeric](7, 0),
			IsCubics [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
			SHIFTDAY  [numeric](7, 0),
			Calendardate DateTime,
			PlanCode [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS, 
			SQUAREMETRES [numeric](7, 0),
			METRESADVANCE [numeric](7, 2),
			CUBICMETRES [numeric](7, 0),
			TONS [numeric](7, 3),
			GRAMS [numeric](7, 0),
			FL [numeric](7, 0),
			SW  [numeric](7, 0),
			GT [numeric](7, 0),
			cmgt [numeric](7, 0),
			ReefFL[numeric](7, 0),
			WasteFL [numeric](7, 0),
			ReefTons [numeric](7,3),
			WasteTons [numeric](7,3),
			ReefSQM [numeric](7, 0),
			WasteSQM [numeric](7, 0),
			CubicTons [numeric](7, 0),
			CubicGrams [numeric](7, 0),
			CubicDepth [numeric](7, 0),
			CubicGT [numeric](7, 2), 
			OnReefAdv [numeric](7, 2),
			OffReefAdv [numeric](7, 2),
			MOCycle [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS)


select @SB = ReportToSectionid from section where 
PRODMONTH = @ProdMonth
and SECTIONID = @SectionID


select @BeginDate = BeginDate, @EndDate = ENDDATE from SECCAL where 
PRODMONTH = @ProdMonth
and SECTIONID = @SB

select @Density = Density from WORKPLACE where 
Workplaceid = @Workplaceid

 
select 
--*
@StartShift = COUNT(CALENDARDATE)
 from SECCAL  a inner join 
 CALTYPE b on
a.CALENDARcode = b.CALENDARcode and
a.BeginDate <= b.CALENDARDATE and
a.ENDDATE >= b.CALENDARDATE 
where a.PRODMONTH = @ProdMonth and
a.SECTIONID = @SB and 
b.CALENDARDATE <= @BeginDate
and b.WORKINGDAY = 'Y'

select 
--*     
@EndShift = COUNT(CALENDARDATE)
 from SECCAL a inner join 
CALTYPE b on
a.CALENDARcode = b.CALENDARcode and
a.BeginDate <= b.CALENDARDATE and
a.ENDDATE >= b.CALENDARDATE 
where a.PRODMONTH = @ProdMonth and
a.SECTIONID = @SB and 
b.CALENDARDATE <= @EndDate
and b.WORKINGDAY = 'Y'

CREATE TABLE #MOCycle(
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
	[WorkplaceID] [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
	[Activity] [numeric](7, 0) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[MOCycle] [varchar](5) Null)


insert into #MOCycle
select 
a.ProdMonth,
a.SectionID,
a.WorkplaceID,
a.Activity,
d.CalendarDate,
MOCycle = Case 
When d.Workingday = 'N' then 'OFF'
When d.Workingday = 'Y' And p.MOCycle is null then 'BL'
else p.MOCycle end
from 

Planmonth a inner join SECTION_COMPLETE b on
a.Prodmonth = b.prodmonth and
a.SectionID = b.sectionID 
inner join seccal c on 
b.Prodmonth = c.prodmonth and
b.SectionID_1 = c.sectionID 
inner join CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CalendarDate and
c.EndDate >= d.CalendarDate 
left join planning p on
a.Prodmonth = p.Prodmonth AND
      a.Sectionid = p.SectionID AND
      a.Workplaceid = p.WorkplaceID AND
      a.Activity = p.Activity and
      a.IsCubics = p.IsCubics and
	  a.PlanCode = p.PlanCode and
	  d.CalendarDate = p.CalendarDate 
 WHERE a.Prodmonth = @Prodmonth AND
      a.Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
      a.IsCubics = @IsCubics and
	  a.PlanCode = 'MP'

select 
--*
@TotalBlasts = COUNT(CALENDARDATE)
 from #MOCycle a
where a.Prodmonth = @Prodmonth AND
      a.Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
	  a.MOCycle = 'BL'


--select @SB, @BeginDate, @EndDate, @StartShift, @EndShift

SELECT 
--*
@Adv = MetresAdvance,
@Tons = Tons,
@STOPEWIDTH = SW,
@Facelength = FL,
@GoldGramsPerTon = GT,
@CMGT = cmgt,
@OnReefADV = ReefADV,
@CubicMetres = isnull(Cubicmetres,0)--,
--@OffReefSQM = OffReefSQM
FROM Planmonth
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  plancode = 'MP'
	  
  DECLARE Shaft_Cursor CURSOR FOR
	select CALENDARDATE, WORKINGDAY, TOTALSHIFTS
	 from SECCAL a inner join 
	 CALTYPE b on
	a.CALENDARCODE = b.CALENDARCODE and
	a.BeginDate <= b.CALENDARDATE and
	a.ENDDATE >= b.CALENDARDATE 
	where a.PRODMONTH = @ProdMonth and
	a.SECTIONID = @SB
	--and Workingday = 'Y'
	OPEN Shaft_Cursor;
	FETCH NEXT FROM Shaft_Cursor
	into @TheDate, @Workingday, @TotalShifts;

	Set @ShiftNo = 0
	Set @BlastNo = 0
	 Set @RemainderAdv = @Adv
	 Set @RemainderCubics = @CubicMetres
	 Set @Remaining_Blasts = @TotalBlasts
	 WHILE @@FETCH_STATUS = 0
	   BEGIN
		 --set @AdvPerBlast = round(@Adv/@TotalShifts,0)
		 --set @CubicsPerBlast = round(@CubicMetres/@TotalShifts,0)

		 Select @MOCycle = mocycle from #MOCycle where Calendardate = @TheDate

		 if @Workingday = 'Y'
		 begin

		   set @Remaining_Shifts = @TotalShifts - @ShiftNo

		   if @MOCycle = 'BL'
		   begin
		       set @Remaining_Blasts = @TotalBlasts - @BlastNo

			   set @AdvPerBlast = case when @Remaining_Blasts = 0 then 0 else @RemainderAdv/@Remaining_Blasts end
			   set @CubicsPerBlast = case when @Remaining_Blasts = 0 then 0 else @RemainderCubics/@Remaining_Blasts end 
	   
	           set @RemainderAdv = @RemainderAdv - @AdvPerBlast
			   set @RemainderCubics = @RemainderCubics - @CubicsPerBlast

			   set @BlastNo = @BlastNo + 1
			   

		   end 
		   else
		   begin
		     set @AdvPerBlast = 0
		     set @CubicsPerBlast = 0
		   end	

		   set @ShiftNo = @ShiftNo + 1
		   Set @SaveShiftNo = @ShiftNo

		   
		 end 
		 else
		 begin
		    set @AdvPerBlast = 0
		    set @CubicsPerBlast = 0
			Set @SaveShiftNo = 0
		 end


		 insert into #Planning (
			[Prodmonth] ,
			[SectionID],
			[WorkplaceID],
			ACTIVITY,
			IsCubics,
			SHIFTDAY,
			Calendardate,
			PlanCode,
			SQUAREMETRES,
			METRESADVANCE,
			CUBICMETRES,
			TONS,
			GRAMS,
			FL,
			SW,
			GT,
			cmgt,
			ReefFL,
			WasteFL,
			ReefTons,
			WasteTons,
			ReefSQM,
			WasteSQM,
			CubicTons,
			CubicGrams,
			CubicDepth,
			CubicGT, 
			OnReefAdv,
			OffReefAdv,
			MOCycle)

			select
			@ProdMonth PRODMONTH ,
			@SectionID SECTIONID,
			@WorkplaceID WORKPLACEID,
			@Activity ACTIVITYCODE,
			@IsCubics IsCubics ,
			@SaveShiftNo SHIFTDAY,
			@TheDate TEMPDATE,
			'MP',
			SQUAREMETRES = 0,
			METRESADVANCE = @AdvPerBlast,
			CUBICMETRES = @CubicsPerBlast,
			TONS = 0,
			0 GRAMS,
			@Facelength,
			@STOPEWIDTH StopeWidth,
			@GoldGramsPerTon GoldGramsPerTon,
			@CMGT cmgt,
			0 OnReefFL,
			0 OffReefFL,
			0 OnReefTons,
			0 OffReefTons,
			0 OnReefSQM,
			0 OffReefSQM,
			@CubicsPerBlast*@Density CubicTons,
			0 CubicGrams, 
			0 CubicDepth,
			0 CubicGT,
			0 OnReefAdv,
			0 OffReefAdv,
			@MOCycle
     

		 FETCH NEXT FROM Shaft_Cursor
		   into @TheDate, @Workingday, @TotalShifts;
	   end

CLOSE Shaft_Cursor;
DEALLOCATE Shaft_Cursor;

--select * from #Planning

select @PerOnReef = case when @Adv = 0 then 0 else @OnReefADV/@Adv end

Delete from planning  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'
	  and Calendardate > @EndDate
 
Delete from planning  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'
	  and Calendardate < @BeginDate

Update #Planning set 
OnReefAdv = METRESADVANCE * @PerOnReef,
OffReefAdv = METRESADVANCE - Round(METRESADVANCE * @PerOnReef,1)
 from #Planning a inner join WORKPLACE wp_Density on
 a.WORKPLACEID Collate SQL_Latin1_General_CP1_CI_AS = wp_density.workplaceid Collate SQL_Latin1_General_CP1_CI_AS
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
      IsCubics = @IsCubics

Update #Planning set 
ReefTons = (wp_Density.EndHeight * wp_Density.[EndWidth]) * wp_Density.Density * OnReefAdv,
WasteTons = (wp_Density.[EndHeight] * wp_Density.[EndWidth]) * wp_Density.Density * OffReefAdv,
Tons = (wp_Density.[EndHeight] * wp_Density.[EndWidth]) * wp_Density.Density * METRESADVANCE
 from #Planning a inner join WORKPLACE wp_Density on
 a.WORKPLACEID Collate SQL_Latin1_General_CP1_CI_AS = wp_density.workplaceid Collate SQL_Latin1_General_CP1_CI_AS
  inner join [dbo].[WORKPLACE] wp on
 a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = wp.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
      IsCubics = @IsCubics

--Select * from #Planning
 insert into PLANNING 
([Prodmonth] ,
			[SectionID],
			[WorkplaceID],
			ACTIVITY,
			IsCubics,
			SHIFTDAY,
			Calendardate,
			PlanCode,
			SQM,
			METRESADVANCE,
			CUBICMETRES,
			TONS,
			GRAMS,
			FL,
			SW,
			GT,
			cmgt,
			ReefFL,
			WasteFL,
			ReefTons,
			WasteTons,
			ReefSQM,
			WasteSQM,
			CubicTons,
			CubicGrams,
			CubicDepth,
			CubicGT, 
			ReefAdv,
			WasteAdv,
			MOCycle)
Select a.* from 
#PLANNING a 
left join 
PLANNING b on
      a.Prodmonth = b.Prodmonth AND
      a.Sectionid Collate SQL_Latin1_General_CP1_CI_AS = b.SectionID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Activity = b.Activity and
	  a.Calendardate = b.Calendardate and
	  a.PlanCode Collate SQL_Latin1_General_CP1_CI_AS = b.PlanCode Collate SQL_Latin1_General_CP1_CI_AS and
      a.IsCubics Collate SQL_Latin1_General_CP1_CI_AS = b.IsCubics Collate SQL_Latin1_General_CP1_CI_AS
Where b.Prodmonth is null


Update PLANNING set
SHIFTDAY = a.SHIFTDAY,
			SQM = a.SQUAREMETRES,
			METRESADVANCE = a.METRESADVANCE,
			CUBICMETRES = a.CUBICMETRES,
			TONS = a.TONS,
			GRAMS = a.GRAMS,
			FL = a.FL,
			SW = a.SW,
			GT = a.GT,
			cmgt = a.cmgt,
			ReefFL = a.ReefFL,
			WasteFL = a.WasteFL,
			ReefTons = a.ReefTons,
			WasteTons = a.WasteTons,
			ReefSQM = a.ReefSQM,
			WasteSQM = a.WasteSQM,
			CubicTons = a.CubicTons,
			CubicGrams = a.CubicGrams,
			CubicDepth = a.CubicDepth,
			CubicGT = a.CubicGT, 
			ReefAdv = a.OnReefAdv,
			WasteAdv = a.OffReefAdv,
			MOCycle = a.MOCycle

from 
#PLANNING a 
left join 
PLANNING b on
      a.Prodmonth = b.Prodmonth AND
      a.Sectionid Collate SQL_Latin1_General_CP1_CI_AS = b.SectionID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Activity = b.Activity and
	  a.Calendardate = b.Calendardate and
	  a.PlanCode Collate SQL_Latin1_General_CP1_CI_AS = b.PlanCode Collate SQL_Latin1_General_CP1_CI_AS and
      a.IsCubics Collate SQL_Latin1_General_CP1_CI_AS = b.IsCubics Collate SQL_Latin1_General_CP1_CI_AS


 drop table #Planning
 drop table #MOCycle

GO

CREATE Procedure [dbo].[SP_Save_Stope_CyclePlan]
--declare
@Username VARCHAR(50), 
@ProdMonth VARCHAR(20), 
@WorkplaceID VARCHAR(50),
@SectionID VARCHAR(50), 
@Activity VARCHAR(20), 
@IsCubics Varchar(1)

as

Declare
@SB VARCHAR(50), 
@Sqm Numeric(18,10), 
@CMGT NUMERIC(18,2), 
@Tons NUMERIC(18,2), 
@StartShift NUMERIC(18),
@EndShift NUMERIC(18),
@STOPEWIDTH NUMERIC(18),
@Cubics NUMERIC(18),
@ChannelWIDTH NUMERIC(18),
@Facelength NUMERIC(18),
@GoldGramsPerTon NUMERIC(18,3),
@OnReefSQM NUMERIC(18),
@OffreefPer Numeric(18,3),
@BeginDate DateTime,
@EndDate DateTime,
@Locked int,
@PLanCode Varchar(2),
@Workingday Varchar(1),
@RemainderSQM Numeric(10),
@RemainderCubics Numeric(10),
@TotalShifts int,
@ShiftNo int,
@SaveShiftNo int,
@TotalBlasts int,
@BlastNo int,
@SQMPerBlast Numeric(10),
@CubicsPerBlast Numeric(10),
@Remaining_Shifts Numeric(10),
@Remaining_Blasts Numeric(10),
@TheDate DateTime,
@Density Numeric(18,3),
@MOCycle Varchar(5)



--Select @Username = N'MINEWARE_IGGY',
--		@ProdMonth = N'201611',
--		@WorkplaceID = N'54432',
--		@SectionID = N'2072460',
--		@Activity = N'0',
--		@IsCubics = N'N'


	--	Select * from planmonth where workplaceid = 54432 and prodmonth = 201611
		


select @SB = ReportToSectionid from SECTION where 
PRODMONTH = @ProdMonth
and SECTIONID = @SectionID

select @BeginDate = BeginDate, @EndDate = EndDate from SECCAL where 
PRODMONTH = @ProdMonth
and SECTIONID = @SB

--select @BeginDate, @EndDate from SECCAL where 
--PRODMONTH = @ProdMonth
--and SECTIONID = @SB

--select 
--@BeginDate = StartDate, @EndDate = StoppedDate
--from PLANMONTH
--  WHERE Prodmonth = @Prodmonth AND
--      Sectionid = @SectionID AND
--      Workplaceid = @WorkplaceID AND
--      Activitycode = @Activity and
--      IsCubics = @IsCubics 





select 
--*
@StartShift = COUNT(CALENDARDATE)
 from (select * from SECCAL) a inner join 
 (select * from CalType) b on
a.CalendarCode = b.CalendarCode and
a.BeginDate <= b.CALENDARDATE and
a.ENDDATE >= b.CALENDARDATE 
where a.PRODMONTH = @ProdMonth and
a.SECTIONID = @SB and 
b.CALENDARDATE <= @BeginDate
and b.WORKINGDAY = 'Y'

select 
--*
@EndShift = COUNT(CALENDARDATE)
 from (select * from SECCAL) a inner join 
 (select * from CalType) b on
a.CalendarCode = b.CalendarCode and
a.BeginDate <= b.CALENDARDATE and
a.ENDDATE >= b.CALENDARDATE 
where a.PRODMONTH = @ProdMonth and
a.SECTIONID = @SB and 
b.CALENDARDATE <= @EndDate
and b.WORKINGDAY = 'Y'


--select @SB, @BeginDate, @EndDate, @StartShift, @EndShift

--Drop table #Planning

CREATE TABLE #PLANNING(
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
	[WorkplaceID] [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
	[Activity] [numeric](7, 0) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[PlanCode] [char](2) NOT NULL,
	[IsCubics] [char](1) NOT NULL,
	[ShiftDay] [numeric](5, 0) NULL,
	[SQM] [numeric](5, 0) NULL,
	[ReefSQM] [int] NULL,
	[WasteSQM] [int] NULL,
	[Metresadvance] [numeric](9, 3) NULL,
	[ReefAdv] [numeric](10, 3) NULL,
	[WasteAdv] [numeric](10, 3) NULL,
	[Tons] [numeric](5, 0) NULL,
	[ReefTons] [int] NULL,
	[WasteTons] [int] NULL,
	[Grams] [numeric](9, 3) NULL,
	[FL] [numeric](9, 3) NULL,
	[ReefFL] [int] NULL,
	[WasteFL] [int] NULL,
	[SW] [numeric](5, 0) NULL,
	[CW] [numeric](5, 0) NULL,
	[CMGT] [numeric](5, 0) NULL,
	[GT] [numeric](8, 2) NULL,
	[CubicMetres] [numeric](13, 3) NULL,
	[Cubics] [numeric](10, 3) NULL,
	[ReefCubics] [numeric](10, 3) NULL,
	[WasteCubics] [numeric](10, 3) NULL,
	[CubicTons] [numeric](10, 3) NULL,
	[CubicGrams] [numeric](10, 3) NULL,
	[CubicDepth] [numeric](10, 3) NULL,
	[CubicGT] [numeric](10, 3) NULL,
	[MOCycle] [varchar](5) Null
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[SectionID] ASC,
	[WorkplaceID] ASC,
	[Activity] ASC,
	[Calendardate] ASC,
	[PlanCode] ASC,
	[IsCubics] ASC
))

CREATE TABLE #MOCycle(
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
	[WorkplaceID] [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
	[Activity] [numeric](7, 0) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[MOCycle] [varchar](5) Null)


insert into #MOCycle
select 
a.ProdMonth,
a.SectionID,
a.WorkplaceID,
a.Activity,
d.CalendarDate,
MOCycle = Case 
When d.Workingday = 'N' then 'OFF'
When d.Workingday = 'Y' And p.MOCycle is null then 'BL'
else p.MOCycle end
from 

Planmonth a inner join SECTION_COMPLETE b on
a.Prodmonth = b.prodmonth and
a.SectionID = b.sectionID 
inner join seccal c on 
b.Prodmonth = c.prodmonth and
b.SectionID_1 = c.sectionID 
inner join CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CalendarDate and
c.EndDate >= d.CalendarDate 
left join planning p on
a.Prodmonth = p.Prodmonth AND
      a.Sectionid = p.SectionID AND
      a.Workplaceid = p.WorkplaceID AND
      a.Activity = p.Activity and
      a.IsCubics = p.IsCubics and
	  a.PlanCode = p.PlanCode and
	  d.CalendarDate = p.CalendarDate 
 WHERE a.Prodmonth = @Prodmonth AND
      a.Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
      a.IsCubics = @IsCubics and
	  a.PlanCode = 'MP'

select 
--*
@TotalBlasts = COUNT(CALENDARDATE)
 from #MOCycle a
where a.Prodmonth = @Prodmonth AND
      a.Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
	  a.MOCycle = 'BL'

SELECT 
--*
@Sqm = SQM,
@Tons = Tons,
@STOPEWIDTH = SW,
@ChannelWIDTH = CW,
@Facelength = FL,
@GoldGramsPerTon = GT,
@CMGT = cmgt,
@OnReefSQM = ReefSQM,
@PLanCode = PlanCode,
@Density = b.density,
@Cubics = CubicMetres
FROM Planmonth a inner join Workplace b on
a.Workplaceid = b.workplaceid
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'


--select @Sqm,
--@Tons ,
--@STOPEWIDTH,
--@ChannelWIDTH,
--@Facelength,
--@GoldGramsPerTon,
--@CMGT,
--@OnReefSQM,
--@PLanCode,
--@Density,
--@Cubics

    DECLARE Shaft_Cursor CURSOR FOR
	select CALENDARDATE, WORKINGDAY, TOTALSHIFTS
	 from SECCAL a inner join 
	 CALTYPE b on
	a.CALENDARCODE = b.CALENDARCODE and
	a.BeginDate <= b.CALENDARDATE and
	a.ENDDATE >= b.CALENDARDATE 
	where a.PRODMONTH = @ProdMonth and
	a.SECTIONID = @SB
	--and b.Workingday = 'Y'
	OPEN Shaft_Cursor;
	FETCH NEXT FROM Shaft_Cursor
	into @TheDate, @Workingday, @TotalShifts;

	Set @ShiftNo = 0
	Set @BlastNo = 0
	 Set @RemainderSQM = @Sqm
	 Set @RemainderCubics = @Cubics
	 Set @Remaining_Blasts = @TotalBlasts
	 WHILE @@FETCH_STATUS = 0
	   BEGIN
		 --set @SQMPerBlast = round(@Sqm/@TotalShifts,0)
		 --set @CubicsPerBlast = round(@Cubics/@TotalShifts,0)

		 Select @MOCycle = mocycle from #MOCycle where Calendardate = @TheDate

		 if @Workingday = 'Y'
		 begin

		   set @Remaining_Shifts = @TotalShifts - @ShiftNo
		   
		   if @MOCycle = 'BL'
		   begin
		     set @Remaining_Blasts = @TotalBlasts - @BlastNo

		     set @SQMPerBlast = case when @Remaining_Shifts = 0 then 0 else Convert(Int,round(@RemainderSQM/@Remaining_Blasts,0)) end
		     set @CubicsPerBlast = case when @Remaining_Shifts = 0 then 0 else Convert(Int,round(@RemainderCubics/@Remaining_Blasts,0)) end

			 set @BlastNo = @BlastNo + 1

			 set @RemainderSQM = @RemainderSQM - @SQMPerBlast
		     set @RemainderCubics = @RemainderCubics - @CubicsPerBlast

		   end else
		   begin
		     set @SQMPerBlast = 0
		     set @CubicsPerBlast = 0
		   end		   

		   set @ShiftNo = @ShiftNo + 1
		   Set @SaveShiftNo = @ShiftNo
		   
		 end 
		 else
		 begin
		    set @SQMPerBlast = 0
		    set @CubicsPerBlast = 0
			Set @SaveShiftNo = 0
		 end

		 insert into #PLANNING 
			(Prodmonth,
			SectionID,
			WorkplaceID,
			Activity,
			Calendardate,
			PlanCode,
			IsCubics,
			ShiftDay,
			SQM,
			ReefSQM,
			WasteSQM,
			Metresadvance,
			ReefAdv,
			WasteAdv,
			Tons,
			ReefTons,
			WasteTons,
			Grams,
			FL,
			ReefFL,
			WasteFL,
			SW,
			CW,
			CMGT,
			GT,
			CubicMetres,
			Cubics,
			ReefCubics,
			WasteCubics,
			CubicTons,
			CubicGrams,
			CubicDepth,
			CubicGT,
			[MOCycle])

			select
			@ProdMonth ,
			@SectionID SECTIONID,
			@WorkplaceID WORKPLACEID,
			@Activity ACTIVITY,
			@TheDate CALENDARDATE,
			'MP' PLanCode,
			@IsCubics,
			@SaveShiftNo SHIFTDAY,
			SQUAREMETRES = @SQMPerBlast,
			0 ReefSQM,
			0 WasteSQM,
			0 METRESADVANCE,
			0 ReefAdv,
			0 WasteAdv,
			TONS = @SQMPerBlast*@STOPEWIDTH/100*@Density,
			0 ReefTons,
			0 WasteTons,
			0 GRAMS, 
			@Facelength FL,
			0 ReefFL,
			0 WasteFL,
			@STOPEWIDTH SW,
			@ChannelWIDTH CW,
			@CMGT cmgt,
			@GoldGramsPerTon gt,
			CubicMetres = @CubicsPerBlast,
			Cubics = 0,
			ReefCubics = 0,
			WasteCubics = 0,
			CubicTons = @CubicsPerBlast*@Density,
			CubicGrams = 0,
			CubicDepth = 0,
			CubicGT = 0,
			MOCycle =  @MOCycle
     

		 FETCH NEXT FROM Shaft_Cursor
		   into @TheDate, @Workingday, @TotalShifts;
	   end

CLOSE Shaft_Cursor;
DEALLOCATE Shaft_Cursor;




select @OffreefPer = case when @Sqm = 0 then 0 else @OnReefSQM/@Sqm end

--select @OffreefPer

--select * from #PLANNING

--insert into #PLANNING 
--(Prodmonth,
--SectionID,
--WorkplaceID,
--Activity,
--Calendardate,
--PlanCode,
--IsCubics,
--ShiftDay,
--SQM,
--ReefSQM,
--WasteSQM,
--Metresadvance,
--ReefAdv,
--WasteAdv,
--Tons,
--ReefTons,
--WasteTons,
--Grams,
--FL,
--ReefFL,
--WasteFL,
--SW,
--CW,
--CMGT,
--GT,
--CubicMetres,
--Cubics,
--ReefCubics,
--WasteCubics,
--CubicTons,
--CubicGrams,
--CubicDepth,
--CubicGT,
--MOCycle)

--select 

--@ProdMonth ,
--@SectionID SECTIONID,
--@WorkplaceID WORKPLACEID,
--@Activity ACTIVITY,
--CALENDARDATE,
--@PLanCode,
--@IsCubics,
--0 SHIFTDAY,
--SQUAREMETRES = 0,
--0 ReefSQM,
--0 WasteSQM,
--0 METRESADVANCE,
--0 ReefAdv,
--0 WasteAdv,
--TONS = 0,
--0 ReefTons,
--0 WasteTons,
--0 GRAMS, 
--@Facelength FL,
--0 ReefFL,
--0 WasteFL,
--0 SW,
--0 CW,
--0 cmgt,
--0 gt,
--CubicMetres = 0,
--Cubics = 0,
--ReefCubics = 0,
--WasteCubics = 0,
--CubicTons = 0,
--CubicGrams = 0,
--CubicDepth = 0,
--CubicGT = 0,
--MOCycle = CASE WHEN workingday = 'N' THEN 'OFF' ELSE 'BL' END

-- from (select * from SECCAL) a inner join 
-- (select * from CalType) b on
--a.CalendarCode = b.CalendarCode and
--a.StartDate <= b.CALENDARDATE and
--a.ENDDATE >= b.CALENDARDATE 
--where a.PRODMONTH = @ProdMonth and
--a.SECTIONID = @SB and 
--b.CALENDARDATE <= @EndDate
--and b.WORKINGDAY = 'N'

Update #PLANNING set 
--Select
GRAMS = Round(SQM*@OffreefPer,0)*cmgt/100*(vw_WP_density.density),
METRESADVANCE = case when FL = 0 then 0 else SQM/FL end,
ReefFL = Round(FL*@OffreefPer,0),
WasteFL = FL-Round(FL*@OffreefPer,0),
ReefTons = Round(TONS*@OffreefPer,0),
WasteTons = TONS - Round(TONS*@OffreefPer,0),
ReefSQM = Round(SQM*@OffreefPer,0),
WasteSQM = SQM-Round(SQM*@OffreefPer,0),
ReefAdv = case when FL = 0 then 0 else (SQM/FL)*@OffreefPer end,
WasteAdv = case when FL = 0 then 0 else (SQM/FL)-((SQM/FL)*@OffreefPer) end
from
 #PLANNING PLANNING inner join WORKPLACE vw_WP_density  on
 PLANNING.WORKPLACEID Collate SQL_Latin1_General_CP1_CI_AS = vw_wp_density.workplaceid Collate SQL_Latin1_General_CP1_CI_AS
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      PLANNING.Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'

--select * from #PLANNING

Delete from planning  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'
	  and Calendardate > @EndDate
 
Delete from planning  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'
	  and Calendardate < @BeginDate

Update PLANMONTH set 
ReefFL = Round(FL*@OffreefPer,0),
WasteFL = FL- Round(FL*@OffreefPer,0)
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      Workplaceid = @WorkplaceID AND
      Activity = @Activity and
	  PlanCode = 'MP' and
      IsCubics = @IsCubics

insert into PLANNING 
(Prodmonth,
SectionID,
WorkplaceID,
Activity,
Calendardate,
PlanCode,
IsCubics,
ShiftDay,
SQM,
ReefSQM,
WasteSQM,
Metresadvance,
ReefAdv,
WasteAdv,
Tons,
ReefTons,
WasteTons,
Grams,
FL,
ReefFL,
WasteFL,
SW,
CW,
CMGT,
GT,
CubicMetres,
Cubics,
ReefCubics,
WasteCubics,
CubicTons,
CubicGrams,
CubicDepth,
CubicGT,
MOCycle)
Select a.* from 
#PLANNING a 
left join 
PLANNING b on
      a.Prodmonth = b.Prodmonth AND
      a.Sectionid Collate SQL_Latin1_General_CP1_CI_AS = b.SectionID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Activity = b.Activity and
	  a.Calendardate = b.Calendardate and
	  a.PlanCode Collate SQL_Latin1_General_CP1_CI_AS = b.PlanCode Collate SQL_Latin1_General_CP1_CI_AS and
      a.IsCubics Collate SQL_Latin1_General_CP1_CI_AS = b.IsCubics Collate SQL_Latin1_General_CP1_CI_AS
Where b.Prodmonth is null


Update PLANNING set
ShiftDay = a.ShiftDay,
SQM = a.SQM,
ReefSQM = a.ReefSQM,
WasteSQM = a.WasteSQM,
Metresadvance = a.Metresadvance,
ReefAdv = a.ReefAdv,
WasteAdv = a.WasteAdv,
Tons = a.Tons,
ReefTons = a.ReefTons,
WasteTons = a.WasteTons,
Grams = a.Grams,
FL = a.FL,
ReefFL = a.ReefFL,
WasteFL = a.WasteFL,
SW = a.SW,
CW = a.CW,
CMGT = a.CMGT,
GT = a.GT,
CubicMetres = a.CubicMetres,
Cubics = a.Cubics,
ReefCubics = a.ReefCubics,
WasteCubics = a.WasteCubics,
CubicTons = a.CubicTons,
CubicGrams = a.CubicGrams,
CubicDepth = a.CubicDepth,
CubicGT = a.CubicGT
from 
#PLANNING a 
left join 
PLANNING b on
      a.Prodmonth = b.Prodmonth AND
      a.Sectionid Collate SQL_Latin1_General_CP1_CI_AS = b.SectionID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Activity = b.Activity and
	  a.Calendardate = b.Calendardate and
	  a.PlanCode Collate SQL_Latin1_General_CP1_CI_AS = b.PlanCode Collate SQL_Latin1_General_CP1_CI_AS and
      a.IsCubics Collate SQL_Latin1_General_CP1_CI_AS = b.IsCubics Collate SQL_Latin1_General_CP1_CI_AS


Drop Table #PLANNING
DROP Table #MOCycle


GO

CREATE Procedure [dbo].[sp_Preplanning_Approve]
--DECLARE
@Prodmonth NUMERIC(7,0),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20), 
@Activity NUMERIC(7,0), 
--@IsCubics VARCHAR(5), 
@UserID VARCHAR(10),
@NetworkID VARCHAR(100),
@MachineID VARCHAR(100)

--SET @Prodmonth=201411
--SET @SectionID='194916'
--SET @Sectionid_2='169916'
--SET @WorkplaceID='S00387'
--SET @Activity=0
--SET @IsCubics='N'
--SET @UserID = 'MINEWARE_DOLF'
--SET @NetworkID = 'DOLF-PC'
--SET @MachineID = '1111'

AS

DECLARE @HasData INT
Declare @SQL Varchar(8000)
DECLARE @CUBMICMETERES INT
DECLARE @IsCubics VARCHAR(5) 

Set @IsCubics = 'N'

SET @HasData = (SELECT Count(WORKPLACEID) FROM dbo.PLANMONTH a inner join SECTION_COMPLETE b on
a.Prodmonth = b.Prodmonth and
a.Sectionid = b.SectionID
WHERE SectionID_2 = @Sectionid_2 and WORKPLACEID = @WorkplaceID AND a.PRODMONTH = @Prodmonth AND Activity = @Activity and IsCubics = @IsCubics and Plancode = 'LP')

SET @CUBMICMETERES=(SELECT CubicMetres from PLANMONTH where WORKPLACEID = @WorkplaceID AND PRODMONTH = @Prodmonth AND Activity = @Activity
and Plancode = 'PM')


     
	  
IF @HasData = 0 
BEGIN      


-- 2. Create a lock plan record
set @SQL = '[sp_Create_Lock_Plan] ' +Cast(@Prodmonth as Varchar(30))+ ','''+@SectionID+''','''+@SectionID_2+''','''+@WorkplaceID+''','+Cast(@Activity as Varchar(30))+','''+@IsCubics+''',''' + @UserID +''''
exec (@SQL)

IF @Activity = 0
BEGIN 

-- Add the preplanning to the planning
IF @IsCubics = 'N'
BEGIN

      
      set @SQL = '[SP_Save_Stope_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+@IsCubics+''''
      
      exec (@SQL)
END

-- 1. Update the current plan and set as locked
update PLANMONTH set Locked = 1, [LockedDate] =GETDATE(), [LockedBY] = @UserID, [Auth] = 'Y'
WHERE WORKPLACEID = @WorkplaceID AND PRODMONTH = @Prodmonth AND Activity = @Activity and IsCubics = @IsCubics and Plancode = 'MP'

--IF @IsCubics = 'Y'
--BEGIN
      
--      set @SQL = '[SP_Save_StopeCubics_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+@IsCubics+''''
      
--      exec (@SQL)
--END
      
      --      set @SQL = 'sp_PrePlanning_CreateLog ''' + Cast(@UserID as Varchar(30)) + ''',''' + 
      --                                     Cast(@NetworkID as Varchar(30)) + ''',''' + 
      --                                     Cast(@MachineID as Varchar(30)) + ''',''SAVEPP'',' + 
      --                                     Cast(@Prodmonth as Varchar(30))+','''+
      --                                     Cast(@SectionID as Varchar(30)) +''','''+
      --                                     Cast(@WorkplaceID as Varchar(30))+''','+
      --                                     Cast(7 as Varchar(30))+','''+
      --                                     Cast(@IsCubics as Varchar(30))+'''';
      --exec (@SQL)

      
     
--UPDATE dbo.WORKPLACE  SET FL = PP.FL
--FROM WORKPLACE WP
--INNER JOIN 
--dbo.PLANMONTH PP  ON WP.WORKPLACEID = PP.Workplaceid
--WHERE WP.Workplaceid = @WorkplaceID AND PP.Prodmonth = @Prodmonth  and Plancode = 'MP'   
          
END -- end Stoping 

IF @Activity = 1
BEGIN
IF @IsCubics = 'N'
BEGIN
      
      set @SQL = '[SP_Save_Dev_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+@IsCubics+''''
      
      exec (@SQL)
      
    END   
--IF @IsCubics = 'Y'
--BEGIN

      
--      set @SQL = '[SP_Save_DevCubics_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''',7,''Y'''
      
--      exec (@SQL)
              
--END   

 --set @SQL = 'sp_PrePlanning_CreateLog ''' + Cast(@UserID as Varchar(30)) + ''',''' + 
 --                                          Cast(@NetworkID as Varchar(30)) + ''',''' + 
 --                                          Cast(@MachineID as Varchar(30)) + ''',''SAVEPP'',' + 
 --                                          Cast(@Prodmonth as Varchar(30))+','''+
 --                                          Cast(@SectionID as Varchar(30)) +''','''+
 --                                          Cast(@WorkplaceID as Varchar(30))+''','+
 --                                          Cast(7 as Varchar(30))+','''+
 --                                          Cast(@IsCubics as Varchar(30))+'''';
 --     exec (@SQL) 
--DECLARE @SampCountCM INT
--SET @SampCountCM = 
--(SELECT COUNT(Prodmonth) FROM dbo.vw_Kriging_Latest 
--WHERE Prodmonth = @Prodmonth AND
--      WORKPLACEID = @WORKPLACEID)

--IF @SampCountCM = 0
--BEGIN
--INSERT INTO dbo.KRIGING
--        ( Prodmonth ,
--          WORKPLACEID ,
--          WeekNo           
--        )
--VALUES  ( @Prodmonth , -- Prodmonth - numeric
--          @WorkplaceID , -- WORKPLACEID - varchar(12)
--          1 -- SAMPLINGDATE - datetime
         
--        )


               
--UPDATE dbo.SAMPLING SET LOCKSW = WP.ENDHEIGHT*100, 
--                        DynamicSW = WP.ENDHEIGHT*100,  
--						BOOKSW = WP.ENDHEIGHT*100,  
--                        LOCKCW = dbo.GetApprovedCWDev(pp.Prodmonth,PP.Workplaceid,PP.SectionID_2),
--                        DynamicCW = dbo.GetApprovedCWDev(pp.Prodmonth,PP.Workplaceid,PP.SectionID_2),
--						BOOKCW = dbo.GetApprovedCWDev(pp.Prodmonth,PP.Workplaceid,PP.SectionID_2),
--                        LOCKCMGT = PP.CMGT,
--                        DynamicCMGT = PP.CMGT,
--						BOOKCMGT = PP.CMGT,
--                        LOCKGT = GT,
--                        DynamicGT = GT,
--						BOOKGT = GT,
--                        STANDARDWIDTH = PP.IdealSW,
--                       DynamicCMKGT=PP.CMKGT,
--					   BookCMKGT=PP.CMKGT,
--					   LockCMKGT=PP.CMKGT,
--                        calendardate = GETDATE()
                          
--FROM dbo.SAMPLING SAM
--INNER JOIN dbo.planmonth PP ON
--SAM.Prodmonth = PP.Prodmonth AND
--SAM.WORKPLACEID = PP.Workplaceid
--INNER JOIN WORKPLACE WP on
--PP.WORKPLACEID = WP.Workplaceid
--WHERE SAM.WORKPLACEID = @WorkplaceID AND
--      SAM.Prodmonth = @Prodmonth   and
--	  PP.Metresadvance >0     and plancode = 'MP'       

      
--END

-- 1. Update the current plan and set as locked
update PLANMONTH set Locked = 1, [LockedDate] =GETDATE(), [LockedBY] = @UserID, [Auth] = 'Y'
WHERE WORKPLACEID = @WorkplaceID AND PRODMONTH = @Prodmonth AND Activity = @Activity and IsCubics = @IsCubics and Plancode = 'MP'
                  
END -- end Development   

SELECT 'Approved' theStatus
END 
ELSE 
    BEGIN
     SELECT 'Workplace already locked for Production Month : ' + CAST(PM.PRODMONTH AS VARCHAR(7)) + ' Section : ' + sc.Name_2 theStatus 
	 FROM dbo.PLANMONTH PM
     INNER JOIN dbo.SECTION_COMPLETE SC ON
     PM.PRODMONTH = SC.PRODMONTH AND
     PM.SECTIONID = SC.SECTIONID
     WHERE PM.WORKPLACEID = @WorkplaceID AND PM.PRODMONTH = @Prodmonth
    END

GO

CREATE Function [dbo].[RequestType_MessageBody] 
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
SET @MiningMethod=(select Description from PrePlanning_ChangeRequest PPCR  LEFT JOIN [PERS].[dbo].[Bonus_PoolDefaults] BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
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

CREATE Function [dbo].[RequestIDFormat]
(
@ChangeRequestID INT
)
RETURNS VARCHAR(50)
AS
BEGIN

DECLARE @TheValue varchar(50)
--SET @ChangeRequestID=12
set @TheValue='RI-'+(SELECT RIGHT('000000'+ CONVERT(VARCHAR,@ChangeRequestID),8) AS NUM) --FROM Numbers

RETURN @TheValue
END







GO

Create procedure [dbo].[sp_PrePlanning_AddWorkplace]
@Prodmonth NUMERIC(7,0),@SectionID_2 VARCHAR(20),@WPDescription VARCHAR(100), @ActivityCode int

AS

		--DECLARE @Prodmonth NUMERIC(7,0),@SectionID_2 VARCHAR(20),@WPDescription VARCHAR(100), @ActivityCode int
		--SET @Prodmonth = 201706
		--SET @SectionID_2 = 'REA'
		--SET @WPDescription = '192 N3 E 3'
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
'' OrgunitNight ,'' OrgunitAfterNoon ,'' Activity,'' UnitType,0.00 Call,0.00 ccall,0 content,0 Depth, 0 ActualDepth,CASE WHEN sam.GT is null THEN 0 ELSE sam.GT  END  gt,SECCAL.BeginDate,SECCAL.EndDate,0 Locked from WORKPLACE WP 
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
 
	   @Prodmonth [Prodmonth]
      ,-1 [Sectionid]
      ,@SectionID_2 [Sectionid_2]
      ,WP.DESCRIPTION [WorkplaceDesc]
      ,wp.activity [Activity]
      ,'N' [IsCubics]
      ,'MP' [PlanCode]
      ,GetDate()  [StartDate]
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
		
		FROM dbo.WORKPLACE WP
		INNER JOIN OREFLOWENTITIES o on o.oreflowid = wp.oreflowid 
        LEFT JOIN dbo.vw_Kriging_Latest SAM ON 
		WP.WORKPLACEID = SAM.WORKPLACEID and
		SAM.ProdMonth = @Prodmonth  
         inner JOIN SECTION_COMPLETE SC on
        SC.SECTIONID_2 = @SectionID_2 and 
        SC.PRODMONTH = @Prodmonth
     --   left join SECCAL on
	    --SC.PRODMONTH = SECCAL.PRODMONTH and
	    --SC.SECTIONID_1 = SECCAL.SECTIONID  
        WHERE [DESCRIPTION] = @WPDescription

)data)

if @totalrows > 1
begin

SELECT 
DISTINCT 

	   @Prodmonth [Prodmonth]
      ,-1 [Sectionid]
      ,@SectionID_2 [Sectionid_2]
      ,WP.DESCRIPTION [WorkplaceDesc]
      ,wp.activity [Activity]
      ,'N' [IsCubics]
      ,'MP' [PlanCode]
      ,GetDate()  [StartDate]
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


		FROM dbo.WORKPLACE WP
		INNER JOIN OREFLOWENTITIES o on o.oreflowid = wp.oreflowid 
        LEFT JOIN dbo.vw_Kriging_Latest SAM ON
		WP.WORKPLACEID = SAM.WORKPLACEID and
		SAM.ProdMonth = @Prodmonth 
          
         inner JOIN SECTION_COMPLETE SC on
        SC.SECTIONID_2 = @SectionID_2 and 
        SC.PRODMONTH = @Prodmonth
     --   left join SECCAL on
	    --SC.PRODMONTH = SECCAL.PRODMONTH and
	    --SC.SECTIONID_1 = SECCAL.SECTIONID  
        WHERE [DESCRIPTION] = @WPDescription

end
else 
if @totalrows=1
begin
	SELECT  
	DISTINCT 

		   @Prodmonth [Prodmonth]
		  ,-1 [Sectionid]
		  ,@SectionID_2 [Sectionid_2]
		  ,WP.DESCRIPTION [WorkplaceDesc]
		  ,wp.activity [Activity]
		  ,'N' [IsCubics]
		  ,'MP' [PlanCode]
		  ,GetDate()  [StartDate]
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

		FROM dbo.WORKPLACE WP
			INNER JOIN OREFLOWENTITIES o on o.oreflowid = wp.oreflowid 
			LEFT JOIN dbo.vw_Kriging_Latest SAM ON  
			 WP.WORKPLACEID = SAM.WORKPLACEID and
			 SAM.ProdMonth = @Prodmonth

			 inner JOIN SECTION_COMPLETE SC on
			SC.SECTIONID_2 = @SectionID_2 and 
			SC.PRODMONTH = @Prodmonth
  
			WHERE [DESCRIPTION] = @WPDescription

				  --select * from workplace where workplaceid='53017'


end
else
begin
	SELECT  
	DISTINCT 

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


GO

CREATE Procedure [dbo].[sp_Preplanning_ApprovalMail]
@Prodmonth NUMERIC(6,0), @SectionID_2 VARCHAR(30), @TemplateID INT, @toUserName VARCHAR(60), @theMessage NVARCHAR(MAX)

AS
Declare @SyncroDB VarChar(50)

declare @sqlstatement nvarchar(4000)


DECLARE @tableHTML  NVARCHAR(MAX), @theDepartment NVARCHAR(MAX), @theSection NVARCHAR(MAX), @theToName NVARCHAR(MAX) ,
        @theToEMail NVARCHAR(MAX) 


SET @theDepartment = CAST((SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = @TemplateID) AS  NVARCHAR(MAX))
SET @theSection = CAST((SELECT DISTINCT Name_2 FROM dbo.SECTION_COMPLETE WHERE PRODMONTH = @Prodmonth AND SECTIONID_2 = @SectionID_2) as NVARCHAR(MAX))
set @sqlstatement = 'SELECT @theToName = NAME, @theToEMail = Email FROM tblusers WHERE USERID = '+@toUserName+''
exec(@sqlstatement) 

SET @tableHTML = N'<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"> ' +
                 N'<html>' +
                 N'	<head> ' +
                 N'        <style type="text/css"> ' +
                 N'            .style1 { ' +
                 N'                width: 100%; ' +
                 N'            } ' +
                 N'            .style2 ' +
                 N'            { ' +
                 N'                font-family: Arial, Helvetica, sans-serif; ' +
                 N'            } ' +
                 N'            .style3 ' +
                 N'            { ' +
                 N'                text-align: left; ' +
                 N'            } ' +
                 N'            .style4 ' +
                 N'            { ' +
                 N'                width: 100%; ' +
                 N'                font-family: Arial, Helvetica, sans-serif; ' +
                 N'                font-size: xx-small; ' +
                 N'            } ' +
                 N'        </style> ' +
                 N'    </head> ' +
                 N'	<body> ' +
                 N'	    <table class="style1"> ' +
                 N'            <tr> ' +
                 N'                <td> ' +
                 N'                    <img src="http://172.21.96.23/dashboardv2/images/GF.png"> </img> &nbsp;</td> ' +
                 N'                <td class="style3"> ' +
                 N'                    <span class="style2"><strong style="text-align: left">Approve Data Request</strong></span> &nbsp;</td> ' +
                 N'            </tr> ' +
                 N'            <tr> ' +
                 N'                <td> ' +
                 N'                    <span class="style2"><strong>DEPARTMENT:</strong></span></td> ' +
                 N'                <td> ' +
                 N'                    <span class="style2"><strong>' + @theDepartment + '</strong></span></td> ' +
                 N'            </tr> ' +
                 N'            <tr> ' +
                 N'                <td> ' +
                 N'                   <span class="style2"><strong>SECTION:</strong></span></td> ' +
                 N'                <td> ' +
                 N'                    <span class="style2"><strong>'+ @theSection +'</strong></span></td> ' +
                 N'            </tr> ' +
                 N'            <tr> ' +
                 N'                <td> ' +
                 N'                  <span class="style2"><strong>PRODUCTION MONTH:</strong></span></td> ' +
                 N'                <td> ' +
                 N'                   <span class="style2"><strong>' + CAST(@Prodmonth AS NVARCHAR(MAX) ) +'</strong></span></td> ' +
                 N'            </tr> ' +
                 N'            <tr> ' +
                 N'                <td> ' +
                 N'                   <span class="style2"><strong>TO:</strong></span> &nbsp;</td> ' +
                 N'                <td> ' +
                 N'                    <span class="style2"><strong>' + @theToName + '</strong></span> &nbsp;</td> ' +
                 N'            </tr> ' +
                 N'            <tr> ' +
                 N'                <td> ' +
                 N'                    <span class="style2"><strong>MESSAGE:</strong></span></td> ' +
                 N'                <td> ' +
                 N'                   <span class="style2"><strong>' + @theMessage + '</strong></span></td> ' +
                 N'            </tr> ' +
                 N'        </table> ' +
                 N'	    <p class="style4"> ' +
                 N'            Mail Generated fom CPM</p> ' +
                 N'	</body> ' +
                 N'</html> '
IF @theToEMail <> ''
BEGIN                 
EXEC msdb.dbo.sp_send_dbmail
@profile_name = 'CPM_MAIL',
@recipients = @theToEMail, 
@body = @tableHTML,
@body_format = 'HTML' ,
@subject = 'Approve Data Request' ; 
END                










  
 











GO

CREATE Procedure [dbo].[sp_PrePlanning_CallValueChanges] 
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
@Activity NUMERIC(7,0), 
@SQL Varchar(1000),
@Username VARCHAR(50)

--SET @RequestID = 1076
--SET @UserID = 'MINEWARE'
SET @Prodmonth=( SELECT CR.Prodmonth FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)
SET @SectionID=( SELECT CR.SectionID FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)
SET @WorkplaceID=( SELECT CR.WorkplaceID FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)


SET @Username=@UserID
SET @Activity = --( SELECT CR.Activity FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)
 (SELECT PP.Activity FROM PrePlanning_ChangeRequest CR 
INNER JOIN Planmonth PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and Plancode = 'MP')

IF  @Activity = 0
BEGIN


UPDATE pp SET SQM = CR.ReefSQM + CR.WasteSQM,ReefSQM = CR.ReefSQM,
                       WasteSQM = CR.WasteSQM,
                       --StoppedDate = CR.StopDate,
                       CubicMetres = cr.CubicMeters,
					   KG = Round((CR.ReefSQM*(CMGT/100)*b.density)/1000,3),
					   tons = Round((CR.ReefSQM + CR.WasteSQM)*(SW/100)*b.density,0),
					   Reeftons = Round(CR.ReefSQM*(SW/100)*b.density,0),
			           Wastetons = Round(CR.WasteSQM*(SW/100)*b.density,0),
					   IsStopped = 'N',
					   FL=CR.FL
					  
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN planmonth PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth inner join 
	 WORKPLACE b on
	 PP.Workplaceid = b.workplaceid
WHERE CR.ChangeRequestID = @RequestID  and Plancode = 'MP'



--UPDATE planning  SET SQM = CR.ReefSQM + CR.WasteSQM,
--                     ReefSQM = CR.ReefSQM,
--                     WasteSQM = CR.WasteSQM,
                                    
--					  CUBICMETRES = CR.CubicMeters
					
----SELECT * 
--FROM PrePlanning_ChangeRequest CR 
--INNER JOIN planmonth PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN planning  PD on
--PD.PRODMONTH = pp.Prodmonth and
--PD.WORKPLACEID = PP.Workplaceid and
--PD.SECTIONID = pp.Sectionid and
--pd.plancode = pp.plancode
--WHERE CR.ChangeRequestID = @RequestID --and PM.IsCubics = 'N'
--and pp.plancode = 'MP'

  set @SQL = '[SP_Save_Stope_CyclePlan] '''+@Username+''','+Cast(@ProdMonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+'N'+''''
      
      exec (@SQL)


END  -- end Stoping  
ELSE BEGIN
IF @Activity =1
BEGIN
UPDATE planmonth SET ReefSQM = CR.ReefSQM,
                       WasteSQM = CR.WasteSQM,
                       --StoppedDate = CR.StopDate,
					   METRESADVANCE  =CR.Meters +CR.MetersWaste ,
                       CubicMetres = cr.CubicMeters,
					  Reefadv =CR.Meters ,
					  Wasteadv =CR.MetersWaste,  IsStopped = 'N'--,  SQM = CR.Meters +CR.MetersWaste
					  
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN planmonth PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and plancode = 'MP'


--UPDATE planning  SET METRESADVANCE  =CR.Meters +CR.MetersWaste ,					
--                     ReefAdv   = CR.Meters,
--                     WasteAdv   = CR.MetersWaste,
--   					 sqm = CR.Meters +CR.MetersWaste,					 
--					  CUBICMETRES = CR.CubicMeters,
--					  ReefTons =CR.Meters ,
--					  WasteTons =CR.MetersWaste 
					
----SELECT * 
-- FROM PrePlanning_ChangeRequest CR 
--INNER JOIN planmonth PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN planning  PD on
--PD.PRODMONTH = pp.Prodmonth and
--PD.WORKPLACEID = PP.Workplaceid AND
--PD.SECTIONID = pp.Sectionid and
--pd.plancode = pp.plancode
--WHERE CR.ChangeRequestID = @RequestID and pp.plancode = 'MP'

  set @SQL = '[SP_Save_Dev_CyclePlan] '''+@Username+''','+Cast(@ProdMonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+'N'+''''
      
      exec (@SQL)


END
END








GO

CREATE Procedure [dbo].[sp_PrePlanning_ChangeMail]
--Declare 
 @ChnagrequestID INT
AS
Declare @SyncroDB VarChar(50)

declare @sqlstatement nvarchar(4000)



--SET @ChnagrequestID = 1018

DECLARE @FromName varchar(60),@FromMail varchar(60),@TOName varchar(60),@ToMail varchar(60),@RequestTypeID varchar(60),@Message varchar(120),@department varchar(5),@SecurityType int ,
@WPDesc varchar(50),@SectName varchar(50),@wpid varchar(20)

DECLARE @theSQL varchar(max)


CREATE TABLE #Users(
	[USERID] [varchar](50) NOT NULL,
	[Password] [varchar](200) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NULL,
	[EMail] [varchar](150) NULL,
	[PSWDExpires] [bit] NULL,
	[PSWDLastChanged] [datetime] NULL,
	[AccountLocked] [bit] NULL,
	[LogonAttempts] [int] NULL,
	[ChnagePSWDOnLogon] [bit] NULL,
	[CanBeLocked] [bit] NULL,
	[SuperUser] [bit] NULL,
	[DepartmentID] [int] NULL,
	[SecurityLevel] [int] NULL,
	[IPAddress] [varchar](80) NULL,
	[PCName] [varchar](80) NULL,
	[IsOnline] [bit] NULL,
	UserStatus bit Null)

SET @theSQL = 'INSERT INTO #Users SELECT * FROM tblUsers'
exec (@theSQL)

declare @prodmonth varchar(10),@sectionid_2 varchar(50),@changeid int
set @prodmonth=(select prodmonth from PREPLANNING_CHANGEREQUEST where changerequestid=@ChnagrequestID)
set @sectionid_2=(select sectionid_2 from PREPLANNING_CHANGEREQUEST where changerequestid=@ChnagrequestID)

set @Message=(SELECT  PPCR.Comments FROM PrePlanning_ChangeRequest PPCR INNER JOIN #Users RU On 
PPCR.RequestBy COLLATE SQL_Latin1_General_CP1_CI_AS  = RU.UserID  COLLATE SQL_Latin1_General_CP1_CI_AS
 where ppcr.changerequestid=@ChnagrequestID)

 set @RequestTypeID=(SELECT PPCR. ChangeID FROM PrePlanning_ChangeRequest PPCR INNER JOIN #Users RU On 
PPCR.RequestBy COLLATE SQL_Latin1_General_CP1_CI_AS  = RU.UserID  COLLATE SQL_Latin1_General_CP1_CI_AS
 where ppcr.changerequestid=@ChnagrequestID)

 set @FromName=(SELECT  RU.Name  + ' ' + RU.LastName RequestUser FROM PrePlanning_ChangeRequest PPCR INNER JOIN #Users RU On 
PPCR.RequestBy COLLATE SQL_Latin1_General_CP1_CI_AS  = RU.UserID  COLLATE SQL_Latin1_General_CP1_CI_AS
 where ppcr.changerequestid=@ChnagrequestID)

 set @FromMail=(select RU.EMail RequestUserEMail FROM PrePlanning_ChangeRequest PPCR INNER JOIN #Users RU On 
PPCR.RequestBy COLLATE SQL_Latin1_General_CP1_CI_AS  = RU.UserID  COLLATE SQL_Latin1_General_CP1_CI_AS
 where ppcr.changerequestid=@ChnagrequestID)

 set @changeid=(select changeid from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
 set @wpid=(select Workplaceid from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
 set @WPDesc=(select description from workplace where workplaceid=@wpid)
set @SectName=(select distinct name from section where Prodmonth = @prodmonth and sectionid=@sectionid_2)

 --drop table #Users


declare db_EMailList cursor for 
SELECT  U1.NAME + ' ' + U1.LASTNAME UName,U1.EMail UMail ,ppcra.department, cast(1 as int) SecurityType
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PrePlanning_ChangeRequest_Approval PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
--INNER JOIN #Users RU On 
--PPCR.RequestBy COLLATE SQL_Latin1_General_CP1_CI_AS  = RU.UserID  COLLATE SQL_Latin1_General_CP1_CI_AS
INNER JOIN #Users U1 On
PPCRA.User1 COLLATE SQL_Latin1_General_CP1_CI_AS= U1.UserID COLLATE SQL_Latin1_General_CP1_CI_AS
 where ppcr.changerequestid= @ChnagrequestID
UNION
 SELECT distinct U2.NAME + ' ' + U2.LASTNAME UName,U2.EMail UMail ,ppns.department,RS.SecurityType
FROM REVISEDPLANNING_SECURITY RS inner join REVISEDPLANNING_USERSECURITY_ACTIONS ppns on
ppns.department=rs.department
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_3 inner join #Users U2 on
RS .UserID COLLATE SQL_Latin1_General_CP1_CI_AS=U2 .USERID 
WHERE SC.PRODMONTH = @prodmonth and 
      SC.SECTIONID_2 = @sectionid_2 and PPNS.ChangeID = @changeid   and active=1 and RS.SecurityType =2 and
	  rs.UserID > '' and rs.UserID is not null 
UNION
 SELECT distinct U2.NAME + ' ' + U2.LASTNAME UName,U2.EMail UMail ,ppns.department,RS.SecurityType
FROM REVISEDPLANNING_SECURITY RS inner join REVISEDPLANNING_USERSECURITY_ACTIONS ppns on
ppns.department=rs.department
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2 inner join #Users U2 on
RS .UserID COLLATE SQL_Latin1_General_CP1_CI_AS=U2 .USERID 
WHERE SC.PRODMONTH = @prodmonth and 
      SC.SECTIONID_2 = @sectionid_2 and PPNS.ChangeID = @changeid   and active=1 and RS.SecurityType =2 and
	  rs.UserID > '' and rs.UserID is not null 
--exec(@sqlstatement) 
OPEN db_EMailList

DECLARE @tableHTML  VARCHAR(MAX), @RequestType varchar(50);

SET @RequestType = (SELECT [Description]  FROM CODE_PREPLANNINGTYPES WHERE [ChangeID] = (SELECT [ChangeID] FROM PrePlanning_ChangeRequest WHERE [ChangeRequestID] = @ChnagrequestID)) 

  --drop table #Users

FETCH NEXT FROM db_EMailList INTO @TOName,@ToMail,@department,@SecurityType
WHILE @@FETCH_STATUS = 0   
BEGIN 




IF(@SecurityType = 1)
BEGIN
SET @tableHTML =                         
                     N'<html>' +
                     N'	<head>' +
                     N'        <style type="text/css">' +
                     N'            .style1 {' +
                     N'                width: 100%;' +
                     N'            }' +
                     N'            .style2' +
                     N'            {' +
                     N'                font-family: Arial, Helvetica, sans-serif;' +
                     N'            }' +
                     N'            .style3' +
                     N'            {' +
                     N'                text-align: left;' +
                     N'            }' +
                     N'            .style4' +
                     N'            {' +
                     N'                width: 100%;' +
                     N'                font-family: Arial, Helvetica, sans-serif;' +
                     N'                font-size: xx-small;' +
                     N'            }' +
                     N'        </style>' +
                     N'    </head>' +
                     N'	<body>' +
                     N'	    <table class="style1">' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                    <img src="http://172.21.32.10/images/Sibanye-Gold.jpg"> </img> &nbsp;</td>' +
                     N'                <td class="style3">' +
                     N'                    <span class="style2"><strong style="text-align: left">Planning Change Request</strong></span> &nbsp;</td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td colspan="2">' +
                     N'                    <span class="style2"><strong>You are requested to attend to a planning change request. Please logon to CPM to ' +
                     N'                    view more info.</strong></td>' +
                     N'            </tr>' +                   
                     N'            <tr>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>FROM:</strong></span></td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' + ISNULL(@FromName,'')  + '</strong></span></td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>DATE:</strong></span></td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' + ISNULL(CAST ( GETDATE() AS NVARCHAR(MAX)),'') + '</strong></span></td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>REQUEST TYPE:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@RequestType,'')  + '</strong></span></td>' +
                     N'            </tr>' +
					    N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>PRODUCTION MONTH:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@prodmonth,'')  + '</strong></span></td>' +
                     N'            </tr>' +
					   N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>SECTION:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@SectName,'')  + '</strong></span></td>' +
                     N'            </tr>' +
					    N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>WORKPLACE DESCRIPTION:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@WPDesc,'')  + '</strong></span></td>' +
                     N'            </tr>' +
					   N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>WORKPLACEID:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@wpid,'')  + '</strong></span></td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>TO:</strong></span> &nbsp;</td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' +ISNULL(@TOName,'') + '</strong></span> &nbsp;</td>' +
                     N'            </tr>  <tr>  <td>' +
                     N'                    <span class="style2"><strong>MESSAGE:</strong></span></td>' +
                     N'                <td>' + 
                     N'                   <span class="style2"><strong>' + ISNULL(@Message,'')  + '</strong></span></td>' +
                     N'            </tr>' +
                     N'        </table>'+ ISNULL(dbo.RequestType_MessageBody(@ChnagrequestID),'')+
                     N'	    <p class="style4">' +
                     N'            Mail Generated fom CPM</p>' +                     
                     N'	</body>' +
                     N'</html>' 
                     
                     --PRINT @tableHTML
EXEC msdb.dbo.sp_send_dbmail
@profile_name = 'GFL_MAIL',
@recipients = @ToMail, 
@body =  @tableHTML,
@body_format = 'HTML' ,
@subject = 'Planning Change Request';  
END
ELSE
BEGIN
SET @tableHTML =                         
                     N'<html>' +
                     N'	<head>' +
                     N'        <style type="text/css">' +
                     N'            .style1 {' +
                     N'                width: 100%;' +
                     N'            }' +
                     N'            .style2' +
                     N'            {' +
                     N'                font-family: Arial, Helvetica, sans-serif;' +
                     N'            }' +
                     N'            .style3' +
                     N'            {' +
                     N'                text-align: left;' +
                     N'            }' +
                     N'            .style4' +
                     N'            {' +
                     N'                width: 100%;' +
                     N'                font-family: Arial, Helvetica, sans-serif;' +
                     N'                font-size: xx-small;' +
                     N'            }' +
                     N'        </style>' +
                     N'    </head>' +
                     N'	<body>' +
                     N'	    <table class="style1">' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                    <img src="http://172.21.32.10/images/Sibanye-Gold.jpg"> </img> &nbsp;</td>' +
                     N'                <td class="style3">' +
                     N'                    <span class="style2"><strong style="text-align: left">Planning Change Request Notification</strong></span> &nbsp;</td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td colspan="2">' +
                     N'                    <span class="style2"><strong>Please note the following revised planning request</strong></td>' +
                     N'            </tr>' +                   
                     N'            <tr>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>FROM:</strong></span></td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' + ISNULL(@FromName,'')  + '</strong></span></td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>DATE:</strong></span></td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' + ISNULL(CAST ( GETDATE() AS NVARCHAR(MAX)),'') + '</strong></span></td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>REQUEST TYPE:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@RequestType,'')  + '</strong></span></td>' +
                     N'            </tr>' +
					     N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>PRODUCTION MONTH:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@prodmonth,'')  + '</strong></span></td>' +
                     N'            </tr>' +
					   N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>SECTION:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@SectName,'')  + '</strong></span></td>' +
                     N'            </tr>' +
					    N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>WORKPLACE DESCRIPTION:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@WPDesc,'')  + '</strong></span></td>' +
                     N'            </tr>' +
					   N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>WORKPLACEID:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@wpid,'')  + '</strong></span></td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>TO:</strong></span> &nbsp;</td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' +ISNULL(@TOName,'') + '</strong></span> &nbsp;</td>' +
                     N'            </tr>  <tr>  <td>' +
                     N'                    <span class="style2"><strong>MESSAGE:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@Message,'')  + '</strong></span></td>' +
                     N'            </tr>' +
                     N'        </table>'+ ISNULL(dbo.RequestType_MessageBody(@ChnagrequestID),'')+
                     N'	    <p class="style4">' +
                     N'            Mail Generated fom CPM</p>' +                     
                     N'	</body>' +
                     N'</html>'
                     
                     --PRINT @tableHTML
EXEC msdb.dbo.sp_send_dbmail
@profile_name = 'GFL_MAIL',
@recipients = @ToMail, 
@body =  @tableHTML,
@body_format = 'HTML' ,
@subject = 'Planning Change Notification';  
END
FETCH NEXT FROM db_EMailList INTO @TOName,@ToMail,@department,@SecurityType   
END      

CLOSE db_EMailList   
DEALLOCATE db_EMailList                
 drop table #Users


GO

CREATE Procedure [dbo].[sp_PrePlanning_ChangeMail_Approve]
--declare 
@ChangeRequestID VARCHAR(50)

 as
 --set @ChangeRequestID='1852'
 declare @ToMailScript  nvarchar(4000), @ToMail VarChar(60),@ChangeRequestIDFormat AS VARCHAR(50)
 Declare @SyncroDB VarChar(50)

declare @sqlstatement nvarchar(4000)


 SET @ChangeRequestIDFormat= dbo.RequestIDFormat(@ChangeRequestID)
set @ToMailScript='
	SELECT  U1.EMail UMail
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PrePlanning_ChangeRequest_Approval PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID 
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '''') RU On
PPCR.RequestBy = RU.UserID
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '''') U1 On
PPCR.RequestBy = U1.UserID
WHERE PPCR.ChangeRequestID = '+@ChangeRequestID+'
UNION
SELECT 
U2.EMail UMail
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PrePlanning_ChangeRequest_Approval PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID 
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '''') RU On
PPCR.RequestBy = RU.UserID
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '''') U2 On
PPCR.RequestBy = U2.UserID
WHERE PPCR.ChangeRequestID = '+@ChangeRequestID+''
CREATE TABLE #mail(
	[USERMAIL] [varchar](60) NOT NULL )
	INSERT INTO #mail
 exec (@ToMailScript)
set @ToMail = (select [USERMAIL] from  #mail)


DECLARE @Message varchar(120),@Department varchar(120),@ApprovedDeclinedByUser varchar(50),@ApprovedDeclinedDate datetime,@xml NVARCHAR(MAX),@body NVARCHAR(MAX)


SET @xml = CAST(( 
select Department as 'td','', ApprovedDeclinedByUser as 'td','',CONVERT(VARCHAR(10), ApprovedDeclinedDate,101) as 'td','', Comments  as 'td' from 
(
select Department,ApprovedDeclinedByUser,ApprovedDeclinedDate,Comments from
(
SELECT  
PPCRA.Comments,PPCRA.Department ,U1.Name+' '+U1.LastName ApprovedDeclinedByUser,PPCRA.ApprovedDeclinedDate  
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PrePlanning_ChangeRequest_Approval PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID 
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '') RU On
PPCR.RequestBy = RU.UserID
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '') U1 On
PPCRA.User1 = U1.UserID
WHERE PPCR.ChangeRequestID = @ChangeRequestID)abc
UNION

select Department,ApprovedDeclinedByUser,ApprovedDeclinedDate,Comments from
(
SELECT 

PPCRA.Comments,PPCRA.Department ,U2.Name+' '+U2.LastName ApprovedDeclinedByUser,PPCRA.ApprovedDeclinedDate  
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PrePlanning_ChangeRequest_Approval PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID 
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '') RU On
PPCR.RequestBy = RU.UserID
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '') U2 On
PPCRA.User2 = U2.UserID
WHERE PPCR.ChangeRequestID = @ChangeRequestID)ase)main 
FOR XML PATH('tr'), ELEMENTS ) AS NVARCHAR(MAX))




SET @body ='<html><body><H3>Your Request ' + cast(@ChangeRequestIDFormat as NVARCHAR(MAX)) + ' has been Approved </H3>
<table border = 1> 
<tr>
<th> Department </th> <th> Approved By User </th> <th> Approved Date </th> <th> Comments </th></tr>'
 
SET @body = @body + cast(@xml as NVARCHAR(MAX)) +'</table><p class="style4"> Mail Generated from Syncromine</p></body></html>'                        

EXEC msdb.dbo.sp_send_dbmail
@profile_name = 'GFL_MAIL',
@recipients =  @ToMail, 
@body = @body,
@body_format = 'HTML' ,
@subject = 'Planning Change Request';  
 drop table #mail  
 

GO

CREATE Procedure [dbo].[sp_PrePlanning_ChangeMail_Decline]
@ChangeRequestID varchar(50), @ApproveRequestID varchar(50)

AS

--DECLARE @ChangeRequestID varchar(50),
--@ApproveRequestID varchar(50)
--SET @ChangeRequestID = '18'
--set @ApproveRequestID='10'


declare @sqlstatement nvarchar(4000)


DECLARE @FromName varchar(60),@FromMail varchar(60),@TOName varchar(60),@ToMail varchar(60),@RequestTypeID varchar(60),@Message varchar(120),@Department varchar(120),@RequestID VARCHAR(20)
set @sqlstatement='
DECLARE db_EMailList CURSOR FOR
SELECT  
PPCRA.Comments,PPCR. ChangeID,U1.NAME UName,U1.EMail UMail,Name RequestUser,EMail RequestUserEMail,Department  
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID 
INNER JOIN (SELECT * FROM  tblusers WHERE  UserID <> '') RU On
PPCR.RequestBy = RU.UserID
INNER JOIN (SELECT * FROM tblusers WHERE  UserID <> '') U1 On
PPCRA.User1 = U1.UserID
WHERE PPCR.ChangeRequestID = '+@ChangeRequestID+' and PPCRA.ApproveRequestID ='+@ApproveRequestID+'
UNION
SELECT 

PPCRA.Comments,PPCR. ChangeID,U2.NAME UName,U2.EMail UMail,Name RequestUser,EMail RequestUserEMail,Department  
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID 
INNER JOIN (SELECT * FROM tblusers WHERE  UserID <> '') RU On
PPCR.RequestBy = RU.[UserID]
INNER JOIN (SELECT * FROM tblusers WHERE  UserID <> '') U2 On
PPCRA.User2 = U2.UserID
WHERE PPCR.ChangeRequestID = '+@ChangeRequestID+' and PPCRA.ApproveRequestID ='+@ApproveRequestID+''
exec(@sqlstatement)

select  @sqlstatement
OPEN db_EMailList





DECLARE @tableHTML  VARCHAR(MAX), @RequestType varchar(50) ;
SET @RequestType = (SELECT [Description]  FROM CODE_PREPLANNINGTYPES WHERE [ChangeID] = (SELECT [ChangeID] FROM PrePlanning_ChangeRequest WHERE [ChangeRequestID] = @ChangeRequestID)) 

FETCH NEXT FROM db_EMailList INTO @Message,@RequestTypeID,@FromName,@FromMail,@TOName,@ToMail,@Department
WHILE @@FETCH_STATUS = 0   
BEGIN 




SET @tableHTML =                         
                     N'<html>' +
                     N'	<head>' +
                     N'        <style type="text/css">' +
                     N'            .style1 {' +
                     N'                width: 100%;' +
                     N'            }' +
                     N'            .style2' +
                     N'            {' +
                     N'                font-family: Arial, Helvetica, sans-serif;' +
                     N'            }' +
                     N'            .style3' +
                     N'            {' +
                     N'                text-align: left;' +
                     N'            }' +
                     N'            .style4' +
                     N'            {' +
                     N'                width: 100%;' +
                     N'                font-family: Arial, Helvetica, sans-serif;' +
                     N'                font-size: xx-small;' +
                     N'            }' +
                     N'        </style>' +
                     N'    </head>' +
                     N'	<body>' +
                     N'	    <table class="style1">' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                    <img src="http://172.21.96.54/dashboardv2/images/GF.png"> </img> &nbsp;</td>' +
                     N'                <td class="style3">' +
                     N'                    <span class="style2"><strong style="text-align: left">Planning Change Request</strong></span> &nbsp;</td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td colspan="2">' +
                     N'                    <span class="style2"><strong>Your request has been declined ' +
                     N'                    view more info.</strong></td>' +
                     N'            </tr>' +                   
                     N'            <tr>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>FROM:</strong></span></td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' + ISNULL(@FromName,'')  + '</strong></span></td>' +
                     N'            </tr>' +
					 --N'            <tr>' +
      --                N'                <td>' +
      --               N'                    <span class="style2"><strong>RequestID:</strong></span></td>' +
      --               N'                <td>' +
      --               N'                    <span class="style2"><strong>' + @ChangeRequestID   + '</strong></span></td>' +
      --               N'            </tr>' +                  
                     N'            <tr>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>FROM:</strong></span></td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' + ISNULL(@FromName,'')  + '</strong></span></td>' +
                     N'            </tr>' +

                     N'            <tr>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>DEPARTMENT:</strong></span></td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' + ISNULL(@Department,'')  + '</strong></span></td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>DATE:</strong></span></td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' + ISNULL(CAST ( GETDATE() AS NVARCHAR(MAX)),'') + '</strong></span></td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                  <span class="style2"><strong>REQUEST TYPE:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@RequestType,'')  + '</strong></span></td>' +
                     N'            </tr>' +
                     N'            <tr>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>TO:</strong></span> &nbsp;</td>' +
                     N'                <td>' +
                     N'                    <span class="style2"><strong>' +ISNULL(@TOName,'') + '</strong></span> &nbsp;</td>' +
                     N'            </tr>  <tr>  <td>' +
                     N'                    <span class="style2"><strong>MESSAGE:</strong></span></td>' +
                     N'                <td>' +
                     N'                   <span class="style2"><strong>' + ISNULL(@Message,'')  + '</strong></span></td>' +
                     N'            </tr>' +
                     N'        </table>' +
                     N'	    <p class="style4">' +
                     N'            Mail Generated fom CPM</p>' +                     
                     N'	</body>' +
                     N'</html>'
                     
                     --PRINT @tableHTML
EXEC msdb.dbo.sp_send_dbmail
@profile_name = 'GFL_MAIL',
@recipients = @ToMail, 
--@query = 'Select * from PlanningTemplate_Changes',
--@execute_query_database = 'CPM_Kloof',
@body =  @tableHTML,
@body_format = 'HTML' ,
@subject = 'Planning Change Request';  
FETCH NEXT FROM db_EMailList INTO @Message,@RequestTypeID,@FromName,@FromMail,@TOName,@ToMail,@Department   

--PRINT @tableHTML
--SET @tableHTML = @TOName;
--PRINT @tableHTML
END      

CLOSE db_EMailList   
DEALLOCATE db_EMailList                

GO

CREATE Procedure [dbo].[sp_PrePlanning_CrewChanges] 
--DECLARE 
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
@Activity NUMERIC(7,0), 
@SQL Varchar(1000),
@HasAutoPlan INT,
@NewSectionID_2  VARCHAR(20),
@StartDate DateTime,
@EndDate DateTime

--SET @RequestID = 4222
--SET @UserID = 'MINEWARE_IGGY'

--SET @SQM = (SELECT CR.OnReefSQM + CR.OffReefSQM FROM PrePlanning_ChangeRequest CR 
--WHERE CR.ChangeRequestID = @RequestID)

--SET @Cubes = (SELECT CR.CubicMeters FROM PrePlanning_ChangeRequest CR 
--WHERE CR.ChangeRequestID = @RequestID)
Begin Transaction
SET @SectionID =  (SELECT CR.SectionID FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Activity =  (SELECT CR.Activity FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID = (SELECT CR.Workplaceid FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Prodmonth = (SELECT CR.Prodmonth FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

Select @NewSectionID_2 = SectionID_2, @StartDate = BeginDate, @EndDate = Enddate 
--Select SectionID_2
from Section_complete a inner join seccal b on
a.prodmonth = b.Prodmonth and
a.sectionID_1 = b.SectionID 
where a.Prodmonth = @Prodmonth and
a.Sectionid = @SectionID

--Select @NewSectionID_2, @StartDate, @EndDate, @Prodmonth, @SectionID, @Activity


SET @HasAutoPlan = (SELECT COUNT(Workplaceid) FROM PLANMONTH WHERE Workplaceid = @WorkplaceID AND Prodmonth = @Prodmonth AND AutoUnPlan = 'Y')

--SELECT @HasAutoPlan, @WorkplaceID

IF @HasAutoPlan > 0
BEGIN
	DELETE FROM P
	FROM dbo.PLANNING P 
	INNER JOIN dbo.PLANMONTH PM ON
	PM.Activity = P.Activity AND
	PM.Prodmonth = P.Prodmonth AND
	PM.Workplaceid = P.WorkplaceID  AND
	pm.Sectionid = p.SectionID
	WHERE p.Workplaceid = @WorkplaceID AND p.Prodmonth = @Prodmonth AND pm.AutoUnPlan = 'Y'

	DELETE 
	FROM PLANMONTH WHERE Workplaceid = @WorkplaceID AND Prodmonth = @Prodmonth AND AutoUnPlan = 'Y'
END



UPDATE PM SET
		OrgunitDay =CR.DayCrew ,
		OrgunitAfterNoon =CR.AfternoonCrew ,
		OrgunitNight =CR.NightCrew ,
		RomingCrew =CR.RovingCrew,
		SECTIONID =CR.SectionID,
		--SectionID_2 = @NewSectionID_2,
		StartDate = @Startdate,
		Enddate = @enddate    					  

--SELECT PP.SectionID ,CR.DayCrew,CR.AfternoonCrew,CR.NightCrew,CR.RovingCrew,CR.SectionID 
--SELECT *  
 FROM PlanMonth PM  
INNER JOIN PrePlanning_ChangeRequest CR  ON
CR.SECTIONID = pm.SECTIONID and
CR.WorkplaceID = pm.Workplaceid and
CR.ProdMonth = pm.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and plancode = 'MP'

UPDATE planmonth SET

		SECTIONID =CR.SectionID,
		--SectionID_2 = @NewSectionID_2,
		StartDate = @Startdate,
		Enddate = @enddate    					  
--SELECT *  
FROM planmonth LP 
INNER JOIN PrePlanning_ChangeRequest CR  ON
--CR.SectionID_2 = lp.Sectionid_2 and
CR.WorkplaceID = lp.Workplaceid and
CR.ProdMonth = lp.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and plancode = 'LP' 

UPDATE planning SET 
  Sectionid =CR.SectionID 
 --SELECT *  					  
FROM planning LPD 
INNER JOIN planmonth PP ON
LPD.PRODMONTH = pp.Prodmonth and
LPD.WORKPLACEID = PP.Workplaceid and
lpd.plancode = pp.plancode
INNER JOIN PrePlanning_ChangeRequest CR  ON
pp.Sectionid = CR.SectionID and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID  


--UPDATE BP SET SectionID = CR.SectionID
----SELECT *  
-- FROM dbo.BOOK_PROBLEM BP
--INNER JOIN planmonth PP ON
--BP.PRODMONTH = pp.Prodmonth and
--BP.WORKPLACEID = PP.Workplaceid 
--INNER JOIN PrePlanning_ChangeRequest CR  ON
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--WHERE PP.PlanCode = 'MP' AND PP.Prodmonth = @Prodmonth AND PP.Workplaceid = @WorkplaceID  AND PP.AutoUnPlan IS NULL AND CR.ChangeRequestID = @RequestID  


if @NewSectionID_2 <> @SectionID_2
BEGIN
	UPDATE PLANPROT_DATA SET SectionID = @NewSectionID_2
	WHERE Prodmonth = @Prodmonth AND 
	Workplaceid = @WorkplaceID 

	UPDATE PLANPROT_DATALOCKED SET SectionID = @NewSectionID_2
	WHERE Prodmonth = @Prodmonth AND 
	Workplaceid = @WorkplaceID 
END

COMMIT TRANSACTION

Declare @CycleWorkplaceid VarChar(20),
@CycleSectionid VarChar(20),
@CycleSQL VarChar(8000),
@CycleActivitycode Int,
@CycleIscubics Varchar(1),
@CycleTheProdmonth Numeric(7)

DECLARE _Cursor CURSOR FOR

  select a.Prodmonth, a.Sectionid, a.Workplaceid, a.Activity, a.Iscubics from planmonth a inner join Section_Complete b on
  a.prodmonth = b.prodmonth and
  a.sectionid = b.sectionid and
  PlanCode = 'MP'
  where a.Prodmonth = @Prodmonth 
  and Workplaceid = @WorkplaceID



 OPEN _Cursor;
 FETCH NEXT FROM _Cursor
 into @CycleTheProdmonth, @CycleSectionid, @CycleWorkplaceid, @CycleActivitycode, @CycleIscubics;

 WHILE @@FETCH_STATUS = 0
   BEGIN

     If @CycleActivitycode = 0 

       Set @CycleSQL = 'exec [SP_Save_Stope_CyclePlan] '
     else 
	   Set @CycleSQL = 'exec [SP_Save_Dev_CyclePlan] '

     Set @CycleSQL = @CycleSQL+' @Username = ''MINEWARE'', 
     @ProdMonth = '+cast(@CycleTheProdmonth as Varchar(7))+', 
     @WorkplaceID = '''+@CycleWorkplaceid+''',
     @SectionID = '''+@CycleSectionid+''', 
     @Activity = '+cast(@CycleActivitycode as Varchar(1))+', 
     @IsCubics = '''+@CycleIscubics+''''

   exec (@CycleSQL)

 FETCH NEXT FROM _Cursor
 into @CycleTheProdmonth,@CycleSectionid, @CycleWorkplaceid, @CycleActivitycode, @CycleIscubics;

END

CLOSE _Cursor;
DEALLOCATE _Cursor;


GO

CREATE Procedure [dbo].[sp_PrePlanning_MoveData]
@ChangeRequestID INT
AS
SELECT DISTINCT SC.Name_2 Section,SC.name_2,SC.Name,PPCR.ProdMonth,PPCR.OldWorkplaceID,PPCR.StartDate,PPCR.WorkplaceID,PPCR.DayCrew,
                                   PPCR.SectionID,PPCR.AfternoonCrew,PPCR.NightCrew,PPCR.RovingCrew,WP.DESCRIPTION WPDesc,
                                   PPCR.StopDate,PPCR.MiningMethod,BPD.Description,BPD.TargetID,PPCR.Comments,PPCR.ReefSQM,PPCR.WasteSQM,PPCR.Meters,
								   PPCR.MetersWaste,PPCR.CubicMeters,
                                   PPCR.SectionID,PPCR.SectionID_2 FROM PrePlanning_ChangeRequest PPCR
                                   INNER JOIN SECTION_COMPLETE SC on
                                   PPCR.SectionID_2 = SC.SECTIONID_2 and
                                   PPCR.ProdMonth = SC.PRODMONTH and 
                                   PPCR.SectionID = SC.SECTIONID 
                                   INNER JOIN WORKPLACE WP on 
                                   PPCR.WorkplaceID = WP.WORKPLACEID 
                                    INNER JOIN [PERS].[dbo].[Bonus_PoolDefaults] BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
                                   PPCR.MiningMethod=BPD.TargetID 
                                   WHERE PPCR.ChangeRequestID = @ChangeRequestID








GO

CREATE Procedure  [dbo].[sp_PrePlanning_RequestUserList] 
@UserID varchar(50)

AS

--SET @UserID = 'Mineware'

--SELECT DISTINCT 
-- U.NAME RequestedBy,PPRT.ChangeID,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
--FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
--INNER JOIN PrePlanning_ChangeRequest PPCR ON
--PPCR.UserID = PPCRA.UserID And
--PPCR.ProdMonth = PPCRA.ProdMonth And
--PPCR.SectionID = PPCRA.SectionID And
--PPCR.SectionID_2 = PPCRA.SectionID_2 And
--PPCR.WorkplaceID = PPCRA.WorkplaceID And
--PPCR.ChangeID = PPCRA.ChangeID 
--INNER JOIN CODE_PREPLANNINGTYPES PPRT ON
--PPCR.ChangeID = PPRT.ChangeID
--INNER JOIN  Suncro_Sibanye.dbo.tblUsers U on 
--U.USERID = PPCR.UserID
--INNER JOIN WORKPLACE WP on
--WP.WORKPLACEID = PPCR.WorkplaceID
--INNER JOIN (SELECT PeerName_2,SECTIONID_2,PRODMONTH FROM Section_Complete) SECTION ON
--Section.SECTIONID_2 = PPCR.SectionID_2 and
--Section.PRODMONTH = PPCR.ProdMonth 
--INNER JOIN (SELECT PeerName,SECTIONID,PRODMONTH FROM Section_Complete) MINER ON
--MINER.SECTIONID = PPCR.SectionID and
--MINER.PRODMONTH = PPCR.ProdMonth
--WHERE UserID1 = @UserID and
--      PPCRA.Approved = 0 and
--      PPCRA.Declined = 0
--UNION
--SELECT DISTINCT 
-- U.NAME RequestedBy,PPRT.ChangeID,PPRT.Description ChangeType,WP.DESCRIPTION WPName, 
--WP.WORKPLACEID,PPCR.ProdMonth 
--FROM PrePlanning_ChangeRequest_Approval PPCRA
--INNER JOIN PrePlanning_ChangeRequest PPCR ON
--PPCR.UserID = PPCRA.UserID And
--PPCR.ProdMonth = PPCRA.ProdMonth And
--PPCR.SectionID = PPCRA.SectionID And
--PPCR.SectionID_2 = PPCRA.SectionID_2 And
--PPCR.WorkplaceID = PPCRA.WorkplaceID And
--PPCR.ChangeID = PPCRA.ChangeID 
--INNER JOIN CODE_PREPLANNINGTYPES PPRT ON
--PPCR.ChangeID = PPRT.ChangeID
--INNER JOIN Suncro_Sibanye.dbo.tblUsers U on 
--U.USERID = PPCR.USERID
--INNER JOIN WORKPLACE WP on
--WP.WORKPLACEID = PPCR.WorkplaceID
--INNER JOIN (SELECT PeerName_2,SECTIONID_2,PRODMONTH FROM Section_Complete) SECTION ON
--Section.SECTIONID_2 = PPCR.SectionID_2 and
--Section.PRODMONTH = PPCR.ProdMonth 
--INNER JOIN (SELECT PeerName,SECTIONID,PRODMONTH FROM Section_Complete) MINER ON
--MINER.SECTIONID = PPCR.SectionID and
--MINER.PRODMONTH = PPCR.ProdMonth
--WHERE UserID2 = @UserID and
--      PPCRA.Approved = 0 and
--      PPCRA.Declined = 0



	  SELECT DISTINCT 
(dbo.RequestIDFormat(PPCR.ChangeRequestID ) ) ChangeRequestID,PPCRA.ChangeRequestID , PPCRA.ApproveRequestID , U.UserID  RequestedBy,PPRT.ChangeID,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
INNER JOIN PrePlanning_ChangeRequest PPCR ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
INNER JOIN CODE_PREPLANNINGTYPES PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth
WHERE User1 = @UserID and
      PPCRA.Approved = 0 and
      PPCRA.Declined = 0
UNION
SELECT DISTINCT 
(dbo.RequestIDFormat(PPCR.ChangeRequestID ) ) ChangeRequestID,PPCRA.ChangeRequestID , PPCRA.ApproveRequestID , U.UserID  RequestedBy,PPRT.ChangeID,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
INNER JOIN PrePlanning_ChangeRequest PPCR ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
INNER JOIN CODE_PREPLANNINGTYPES PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth
WHERE User2 = @UserID and
      PPCRA.Approved = 0 and
      PPCRA.Declined = 0











GO

CREATE procedure [dbo].[sp_PrePlanning_Stop_Workplace_Approve] 
--DECLARE 

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
@NewWorkplaceID VARCHAR(20),
@NewSectionID VARCHAR(20),
@Meters NUMERIC(7,3)

--SET @RequestID = 55
--SET @UserID = 'MINEWARE'

SET @SQM = (SELECT CR.ReefSQM + CR.WasteSQM FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Cubes = (SELECT CR.CubicMeters FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

--SET @ActivityCode =  (SELECT PP.Activitycode FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PrePlanning PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--WHERE CR.ChangeRequestID = @RequestID)

SELECT @Prodmonth = PP.Prodmonth, @Sectionid_2 = sc.Sectionid_2, @ActivityCode = pp.Activity FROM PLANMONTH PP 
INNER JOIN SECTION_COMPLETE SC on
PP.Prodmonth = SC.Prodmonth and
PP.Sectionid = SC.Sectionid
INNER JOIN WORKPLACE WP on
PP.Workplaceid = WP.WorkplaceID
INNER JOIN PrePlanning_ChangeRequest CR ON
--CR.SectionID = PP.Sectionid and
CR.SectionID_2 = SC.Sectionid_2 and
CR.OldWorkplaceID = WP.Description and
CR.ProdMonth = PP.Prodmonth --INNER JOIN WORKPLCE WP ON 
--CR.OldWorkplaceID=WP.DESCRIPTION 
WHERE CR.ChangeRequestID=@RequestID


IF  @ActivityCode = 0
BEGIN



IF @SQM > 0  -- Stoping SQM
BEGIN 


--SET @Prodmonth = (SELECT CR.ProdMonth FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PrePlanning PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--WHERE CR.ChangeRequestID = @RequestID)



--SET @SectionID = (SELECT PP.SECTIONID FROM PrePlanning PP INNER JOIN PrePlanning_ChangeRequest CR ON
----CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.OldWorkplaceID = PP.WorkplaceDesc and
--CR.ProdMonth = PP.Prodmonth --INNER JOIN WORKPLCE WP ON 
----CR.OldWorkplaceID=WP.DESCRIPTION 
--WHERE CR.ChangeRequestID=@RequestID)
--SELECT @SectionID

SET @NewSectionID=(SELECT CR.SectionID  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)
--SELECT @NewSectionID

SET @WorkplaceID =  (select WP.WORKPLACEID FROM WORKPLACE WP INNER JOIN PrePlanning_ChangeRequest CR ON
WP.DESCRIPTION= CR.OldWorkplaceID WHERE CR.ChangeRequestID = @RequestID)

UPDATE [dbo].[PLANNING] SET  WORKPLACEID=@NewWorkplaceID WHERE WORKPLACEID=@WorkplaceID  AND PRODMONTH=@Prodmonth
UPDATE [dbo].[PLANNING] SET  SECTIONID=@NewSectionID WHERE WORKPLACEID=@NewWorkplaceID  AND PRODMONTH=@Prodmonth
--SELECT * FROM [dbo].[BOOK_STOPINGLEDGING] where PRODMONTH=@Prodmonth AND WORKPLACEID=@NewWorkplaceID
--UPDATE [dbo].[BOOK_PROBLEMS] SET WORKPLACEID=@NewWorkplaceID WHERE WORKPLACEID=@WorkplaceID  AND PRODMONTH=@Prodmonth
--UPDATE [dbo].[BOOK_PROBLEMS] SET SECTIONID=@NewSectionID WHERE WORKPLACEID=@NewWorkplaceID  AND PRODMONTH=@Prodmonth
--END
--END


--UPDATE PrePlanning SET SQM = CR.ReefSQM,
--                       WasteSQM = CR.WasteSQM,




UPDATE PLANMONTH SET SQM = CR.ReefSQM + CR.WasteSQM,
WasteSQM = CR.WasteSQM,
ReefSQM = CR.ReefSQM,
StoppedDate = CR.StartDate
 FROM PrePlanning_ChangeRequest CR 
 INNER JOIN SECTION_COMPLETE SC on
CR.Prodmonth = SC.Prodmonth and
CR.Sectionid = SC.Sectionid
INNER JOIN WORKPLACE WP on
CR.OldWorkplaceID = WP.Description
INNER JOIN PLANMONTH PP on
--CR.SectionID = PP.Sectionid and
CR.SectionID_2 = SC.Sectionid_2 and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.IsCubics = 'N'


      set @SQL = '[SP_Save_Stope_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+',''' + 'N' + ''''
      
      exec (@SQL)
END  -- End SQM    

IF @Cubes > 0  -- Stoping  QUBES
BEGIN 


--SET @Prodmonth=(SELECT PP.Prodmonth FROM PLANMONTH PP INNER JOIN PrePlanning_ChangeRequest CR ON
----CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.OldWorkplaceID = PP.WorkplaceDesc and
--CR.ProdMonth = PP.Prodmonth --INNER JOIN WORKPLCE WP ON 
----CR.OldWorkplaceID=WP.DESCRIPTION 
--WHERE CR.ChangeRequestID=@RequestID)

--SET @SectionID = (SELECT PP.SECTIONID FROM PLANMONTH PP INNER JOIN PrePlanning_ChangeRequest CR ON
----CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.OldWorkplaceID = PP.WorkplaceDesc and
--CR.ProdMonth = PP.Prodmonth --INNER JOIN WORKPLCE WP ON 
----CR.OldWorkplaceID=WP.DESCRIPTION 
--WHERE CR.ChangeRequestID=@RequestID)


--SET @WorkplaceID =  (select WP.WORKPLACEID FROM WORKPLACE WP INNER JOIN PrePlanning_ChangeRequest CR ON
--WP.DESCRIPTION= CR.OldWorkplaceID WHERE CR.ChangeRequestID = @RequestID)

SELECT @Prodmonth = PP.Prodmonth, @Sectionid_2 = sc.Sectionid_2, @WorkplaceID = WP.WorkplaceID FROM PLANMONTH PP 
INNER JOIN SECTION_COMPLETE SC on
PP.Prodmonth = SC.Prodmonth and
PP.Sectionid = SC.Sectionid

INNER JOIN PrePlanning_ChangeRequest CR ON
--CR.SectionID = PP.Sectionid and
CR.SectionID_2 = SC.Sectionid_2 and
CR.ProdMonth = PP.Prodmonth --INNER JOIN WORKPLCE WP ON 
--CR.OldWorkplaceID=WP.DESCRIPTION 
INNER JOIN WORKPLACE WP on
CR.OldWorkplaceID = WP.Description 
WHERE CR.ChangeRequestID=@RequestID

UPDATE PLANMONTH SET IsStopped = 'Y',
                     SQM = CR.ReefSQM + CR.WasteSQM,
                     CUBICMETRES = CR.CubicMeters,
                     StoppedDate = CR.StartDate
 FROM PrePlanning_ChangeRequest CR 
  INNER JOIN SECTION_COMPLETE SC on
CR.Prodmonth = SC.Prodmonth and
CR.Sectionid = SC.Sectionid
INNER JOIN WORKPLACE WP on
CR.OldWorkplaceID = WP.Description
INNER JOIN PLANMONTH PP on
--CR.SectionID = PP.Sectionid and
CR.SectionID_2 = SC.Sectionid_2 and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.IsCubics = 'Y'

--UPDATE PlanMonth SET IsStopped = 'Y',
--                     SQM = CR.ReefSQM + CR.WasteSQM,
--                     CUBICMETRES = CR.CubicMeters,
--UPDATE PLANMONTH SET IsStopped = 'Y',
--                     SQM = CR.ReefSQM + CR.WasteSQM,
--                     CUBICMETRES = CR.CubicMeters,
--                     StoppedDate = CR.StartDate
----SELECT * 
--FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PLANMONTH PP1 on
----CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp1.Sectionid_2 and
--CR.OldWorkplaceID = PP1.WorkplaceDesc and
--CR.ProdMonth = PP1.Prodmonth
--INNER JOIN PlanMonth PM on
--PM.PRODMONTH = pp1.Prodmonth and
--PM.WORKPLACEID = PP1.Workplaceid and
--PM.SECTIONID = pp1.Sectionid
--WHERE CR.ChangeRequestID = @RequestID and PM.IsCubics = 'Y'



      set @SQL = '[SP_Save_StopeCubics_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+','''+'Y'+''''
      
      exec (@SQL)
END -- end Cubes

END  -- end Stoping      






SET @Meters = (SELECT CR.Meters + CR.MetersWaste FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)



IF  @ActivityCode = 1
BEGIN



IF @Meters > 0  -- Development Meters
BEGIN 


SET @NewSectionID=(SELECT CR.SectionID  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)
--SELECT @NewSectionID

SET @WorkplaceID =  (select WP.WORKPLACEID FROM WORKPLACE WP INNER JOIN PrePlanning_ChangeRequest CR ON
WP.DESCRIPTION= CR.OldWorkplaceID WHERE CR.ChangeRequestID = @RequestID)

UPDATE [dbo].[PLANNING] SET  WORKPLACEID=@NewWorkplaceID WHERE WORKPLACEID=@WorkplaceID  AND PRODMONTH=@Prodmonth
UPDATE [dbo].[PLANNING] SET  SECTIONID=@NewSectionID WHERE WORKPLACEID=@NewWorkplaceID  AND PRODMONTH=@Prodmonth
--SELECT * FROM [dbo].[BOOK_STOPINGLEDGING] where PRODMONTH=@Prodmonth AND WORKPLACEID=@NewWorkplaceID
--UPDATE [dbo].[BOOK_PROBLEMS] SET WORKPLACEID=@NewWorkplaceID WHERE WORKPLACEID=@WorkplaceID  AND PRODMONTH=@Prodmonth
--UPDATE [dbo].[BOOK_PROBLEMS] SET SECTIONID=@NewSectionID WHERE WORKPLACEID=@NewWorkplaceID  AND PRODMONTH=@Prodmonth
--END
--END


--UPDATE PrePlanning SET SQM = CR.ReefSQM,
--                       WasteSQM = CR.WasteSQM,
					   UPDATE PLANMONTH SET  ReefAdv   = CR.Meters,
                     --WasteAdv   = CR.MetersWaste,
                     IsStopped = 'Y',
                     SQM =  CR.Meters +CR.MetersWaste,
                     Metresadvance =CR.Meters ,
					  WasteAdv =CR.MetersWaste,
                     StoppedDate = CR.StartDate
 FROM PrePlanning_ChangeRequest CR 
  INNER JOIN SECTION_COMPLETE SC on
CR.Prodmonth = SC.Prodmonth and
CR.Sectionid = SC.Sectionid
INNER JOIN WORKPLACE WP on
CR.OldWorkplaceID = WP.Description
INNER JOIN PLANMONTH PP on
--CR.SectionID = PP.Sectionid and
CR.SectionID_2 = SC.Sectionid_2 and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.IsCubics = 'N'

--UPDATE PlanMonth SET Call = CR.ReefSQM + CR.WasteSQM,
--                     ReefCall = CR.ReefSQM,
--                     WasteCall = CR.WasteSQM,
					--SQM = CR.ReefSQM + CR.WasteSQM,
     --                WasteSQM = CR.WasteSQM,
     --                ReefSQM = CR.ReefSQM,
     --                StoppedDate = CR.StartDate
--UPDATE PlanMonth SET --Call =  CR.Meters +CR.MetersWaste,
--                     ReefAdv   = CR.Meters,
--                     WasteAdv   = CR.MetersWaste,
--                     IsStopped = 'Y',
--                     SQM =  CR.Meters +CR.MetersWaste,
--                     Metresadvance =CR.Meters ,
--					  WasteAdv =CR.MetersWaste,
--                     StoppedDate = CR.StartDate
----SELECT * 
--FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PLANMONTH PP on
----CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.OldWorkplaceID = PP.WorkplaceDesc and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN PlanMonth PM on
--PM.PRODMONTH = pp.Prodmonth and
--PM.WORKPLACEID = PP.Workplaceid and
--PM.SECTIONID = pp.Sectionid
--WHERE CR.ChangeRequestID = @RequestID and PM.IsCubics = 'N'



      set @SQL = '[SP_Save_Dev_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+',''' + 'N' + ''''
      
      exec (@SQL)
END  -- End SQM    

IF @Cubes > 0  -- Stoping  QUBES
BEGIN 


SELECT @Prodmonth = PP.Prodmonth, @Sectionid_2 = sc.Sectionid_2, @WorkplaceID = WP.WorkplaceID FROM PLANMONTH PP 
INNER JOIN SECTION_COMPLETE SC on
PP.Prodmonth = SC.Prodmonth and
PP.Sectionid = SC.Sectionid

INNER JOIN PrePlanning_ChangeRequest CR ON
--CR.SectionID = PP.Sectionid and
CR.SectionID_2 = SC.Sectionid_2 and
CR.ProdMonth = PP.Prodmonth --INNER JOIN WORKPLCE WP ON 
--CR.OldWorkplaceID=WP.DESCRIPTION 
INNER JOIN WORKPLACE WP on
CR.OldWorkplaceID = WP.Description 
WHERE CR.ChangeRequestID=@RequestID


UPDATE PLANMONTH SET IsStopped = 'Y',
                     SQM = CR.Meters +CR.MetersWaste,
                     CUBICMETRES = CR.CubicMeters,
                     StoppedDate = CR.StartDate
 FROM PrePlanning_ChangeRequest CR 
 INNER JOIN SECTION_COMPLETE SC on
CR.Prodmonth = SC.Prodmonth and
CR.Sectionid = SC.Sectionid
INNER JOIN WORKPLACE WP on
CR.OldWorkplaceID = WP.Description
INNER JOIN PLANMONTH PP on
--CR.SectionID = PP.Sectionid and
CR.SectionID_2 = SC.Sectionid_2 and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.IsCubics = 'Y'

--UPDATE PlanMonth SET IsStopped = 'Y',
--                     SQM = CR.ReefSQM + CR.WasteSQM,
--                     CUBICMETRES = CR.CubicMeters,
--UPDATE PlanMonth SET IsStopped = 'Y',
--                     SQM = CR.Meters +CR.MetersWaste,
--                     CUBICMETRES = CR.CubicMeters,
--                     StoppedDate = CR.StartDate
----SELECT * 
--FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PLANMONTH PP on
----CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.OldWorkplaceID = PP.WorkplaceDesc and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN PlanMonth PM on
--PM.PRODMONTH = pp.Prodmonth and
--PM.WORKPLACEID = PP.Workplaceid and
--PM.SECTIONID = pp.Sectionid
--WHERE CR.ChangeRequestID = @RequestID and PM.IsCubics = 'Y'



      set @SQL = '[SP_Save_DevCubics_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+','''+'Y'+''''
      
      exec (@SQL)
END -- end Cubes

END  -- end Development 






GO

CREATE Procedure [dbo].[sp_PrePlanning_Unapprove]

--DECLARE 
@Prodmonth  numeric(7,0),@WorkplaceID varchar(20), @ActivityCode int, @Sectionid varchar(20) ,@CurrentUser varchar(60), @Reason varchar(8000)

--SET @Prodmonth = 201206
--SET @WorkplaceID = '21151'
--SET @ActivityCode = 1
--SET @Sectionid = 1038430
AS

UPDATE planmonth SET Locked = 0
Where Prodmonth = @Prodmonth and 
      Activity = @ActivityCode and 
      WorkplaceID = @WorkplaceID and 
      Sectionid = @Sectionid AND PLANCODE='MP'
      
DELETE FROM PlanMonth 
Where Prodmonth = @Prodmonth and 
      Activity = @ActivityCode and 
      WorkplaceID = @WorkplaceID and 
      Sectionid = @Sectionid   and PlanCode='LP'
      
DELETE FROM Planning
Where Prodmonth = @Prodmonth and 
      Activity = @ActivityCode and 
      WorkplaceID = @WorkplaceID and 
      Sectionid = @Sectionid 
      
      
INSERT INTO  PrePlanning_Log
SELECT @Prodmonth,@WorkplaceID,@ActivityCode,@Sectionid,@CurrentUser,@Reason,'Unapproved',GETDATE()                   









GO

CREATE TABLE [dbo].[Code_StopeTypes](
	[StopeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[StopeTypeDesc] [varchar](40) NULL
) ON [PRIMARY]

GO

CREATE View [dbo].[WORKPLCE] as 
select WORKPLACEID 
,		DESCRIPTION   
,		case when Activity = 0 then 'S' else 'D' end TYPE   
,		Endtypeid DENDTYPEID     
,		REEFID   
,		substring(o1.parentoreflowid,6, 20) SHAFTID   
,		NULL ITEMA      
,		GMSIWPID GMSIID    
,		NULL TRACKLESS    
,		NULL PLANACTIVITY     
,		NULL TYPECODE  
 from workplace w left outer join oreflowentities o on o.oreflowid = w.oreflowid
 left outer join oreflowentities o1 on o1.oreflowid = o.parentoreflowid
 left outer join oreflowentities o2 on o2.oreflowid = o1.parentoreflowid

GO

CREATE TABLE SURVEY_STORAGES
(
ProdMonth [varchar](10) NOT NULL, 
OreFlowID [nvarchar](10) NOT NULL,
ShaftStoragesTonsBegin [numeric](20, 1) NULL, 
ShaftStoragesContentBegin [numeric](20, 1) NULL,
ShaftStoragesTonsEnd [numeric](20, 1) NULL, 
ShaftStoragesContentEnd [numeric](20, 1) NULL,

StockPilesShaftTonsBegin [numeric](20, 1) NULL, 
StockPilesShaftContentBegin [numeric](20, 1) NULL, 
StockPilesShaftTonsEnd [numeric](20, 1) NULL,
StockPilesShaftContentEnd [numeric](20, 1) NULL, 

StockPilesPlantTonsBegin [numeric](20, 1) NULL, 
StockPilesPlantContentBegin [numeric](20, 1) NULL, 
StockPilesPlantTonsEnd [numeric](20, 1) NULL, 
StockPilesPlantContentEnd [numeric](20, 1) NULL,

RailwayBinsTonsBegin [numeric](20, 1) NULL, 
RailwayBinsContentBegin [numeric](20, 1) NULL,
RailwayBinsTonsEnd [numeric](20, 1) NULL, 
RailwayBinsContentEnd [numeric](20, 1) NULL, 

MillBinsTonsBegin [numeric](20, 1) NULL, 
MillBinsContentBegin [numeric](20, 1) NULL, 
MillBinsTonsEnd [numeric](20, 1) NULL, 
MillBinsContentEnd [numeric](20, 1) NULL, 

SludgeTons [numeric](20, 1) NULL, 
SludgeContent [numeric](20, 1) NULL, 

SurfaceSourcesTons [numeric](20, 1) NULL,
SurfaceSourcesContent [numeric](20, 1) NULL, 

WasteDumpsTons [numeric](20, 1) NULL, 
WasteDumpsContent [numeric](20, 1) NULL, 

SlimesDamTons [numeric](20, 1) NULL, 
SlimesDamContent [numeric](20, 1) NULL, 

ReefExSortingTons [numeric](20, 1) NULL,
ReefExSortingContent [numeric](20, 1) NULL, 

WasteWashingsTons [numeric](20, 1) NULL, 
WasteWashingsContent [numeric](20, 1) NULL, 

 FlushingTons [numeric](20, 1) NULL, 
FlushingContent [numeric](20, 1) NULL, 

AddSourcesToMillTons [numeric](20, 1) NULL, 
AddSourcesToMillContent [numeric](20, 1) NULL, 

TotalTonsMilled [numeric](20, 1) NULL, 
TotalContentMilled [numeric](20, 1) NULL,

GoldRecovered [numeric](20, 1) NULL, 
ReefBallastReclaimedTons [numeric](20, 1) NULL, 
ReefBallastReclaimedContent [numeric](20, 1) NULL, 
Residue [numeric](20, 1) NULL, 
BeltTons [numeric](20, 1) NULL,
BeltValue [numeric](20, 1) NULL,
UGTransfer1 [varchar](20) NULL, 

UGT1Tons [numeric](20, 1) NULL, 
UGT1Content [numeric](20, 1) NULL
)
GO
CREATE table PLANMILLING
(
MillMonth varchar(50) not null,
OreflowID varchar(50) not null,
TonsTreated numeric(10,0) null,
TonsToPlant numeric(10,0) null
)
GO


CREATE Procedure [dbo].[sp_Kriging_Load] 
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
        @WeekNo int,
		@Sampling int,
		@DateTrue varchar(3)
as

--set @Prodmonth = 201703
--set @SectionID = 'REA'
--Set @WeekNo = 3
--Set @Sampling = 1
--set @DateTrue = 'Yes'


Declare @SQL VarChar(8000)
Declare @SQL1 VarChar(8000)

set @SQL = ' 
	select a.ProdMonth, a.SectionID, a.Name, a.WorkplaceID, 
		a.Description, 
		convert(numeric(7,0), a.Activity) Activity, 
		ReefWaste,
		[Order], Unit = [Type],
        RSqm, WSqm, Cubics, 
	    Dens,
		ActDesc = case when [Type] = ''Total g/t'' then a.ActDesc else '''' end,
		SectID = case when [Type] = ''Total g/t'' then a.SectionID else '''' end,
		SectName = case when [Type] = ''Total g/t'' then a.Name else '''' end,
		WPID = case when [Type] = ''Total g/t'' then a.WorkplaceID else '''' end,
		WPDesc = case when [Type] = ''Total g/t'' then a.Description else '''' end, '
IF (@WeekNo = 1)
BEGIN
	set @SQL = @SQL + '
		Week1 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), w1cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W1SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W1CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W1gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W1EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W1EW) as varchar(10))
				    else '''' end,
		Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
								when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
								when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
							else '''' end, 
		Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
					else '''' end,
		Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
					else '''' end,
		W1cmgt, W1SW,  W1CW, W1gt, W1EH, W1EW, 						
		W3cmgt = 0, W3SW = 0, W3CW = 0, W3gt = 0, W3EH = 0, W3EW = 0, 
		W4cmgt = 0, W4SW = 0, W4CW = 0, W4gt = 0, W4EH = 0, W4EW = 0,
		W5cmgt = 0, W5SW = 0, W5CW = 0, W5gt = 0, W5EH = 0, W5EW = 0, '  
		IF (@Sampling = 0)
		BEGIN 
			set @SQL = @SQL + '				 
				Week2 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), w1cmgt) as varchar(10))
								when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W1SW) as varchar(10))
								when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W1CW) as varchar(10))
								when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W1gt) as varchar(10))
								when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W1EH) as varchar(10))
								when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W1EW) as varchar(10))
							else '''' end, 												
				W2cmgt = W1cmgt, W2SW = W1SW, W2CW = W1CW, W2gt = W1gt, W2EH = W1EH, W2EW = W1EW '				
		END
		ELSE
		BEGIN
			set @SQL = @SQL + '
				Week2 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W1EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W1EW) as varchar(10))
					else '''' end, 
				W2cmgt = cmgt, W2SW = SW,  W2CW = CW, W2gt = gt, W2EH, W2EW ' 
		END			
END
IF (@WeekNo = 2) 
BEGIN
	set @SQL = @SQL + '
		Week1 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), w1cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W1SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W1CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W1gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W1EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W1EW) as varchar(10))
				    else '''' end, 
		
		Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
								when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
								when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
							else '''' end,
		Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
					else '''' end,  
		W1cmgt, W1SW,  W1CW, W1gt, W1EH, W1EW, 
		 
		W4cmgt = 0, W4SW = 0, W4CW = 0, W4gt = 0, W4EH = 0, W4EW = 0,
		W5cmgt = 0, W5SW = 0, W5CW = 0, W5gt = 0, W5EH = 0, W5EW  = 0, '
		IF (@Sampling = 0)
		BEGIN
			set @SQL = @SQL + '	
				Week2 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W2cmgt) as varchar(10))
								when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W2SW) as varchar(10))
								when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W2CW) as varchar(10)) 
								when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W2gt) as varchar(10))
								when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W2EH) as varchar(10))
								when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W2EW) as varchar(10))
							else '''' end, 
				W2cmgt, W2SW, W2CW, W2gt, W2EW, W2EH, '
				IF (@DateTrue = 'Yes')
				BEGIN	
					set @SQL = @SQL + '	
						Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W2cmgt) as varchar(10))
										when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W2SW) as varchar(10))
										when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W2CW) as varchar(10)) 
										when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W2gt) as varchar(10))
										when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W2EH) as varchar(10))
										when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W2EW) as varchar(10))
									else '''' end, 																	
						W3cmgt = W2cmgt, W3SW = W2SW, W3CW = W2CW, W3gt = W2gt, W3EW = W2EW, W3EH = W2EH '
				END
				ELSE
				BEGIN
					set @SQL = @SQL + '	
						Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
									when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
									when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10)) 
									when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
									when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
									when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
								else '''' end, 
						W3cmgt = 0, W3SW = 0, W3CW = 0, W3gt = 0, W3EW = 0, W3EH = 0 ' 
				END 
		END
		ELSE
		BEGIN
			IF (@Sampling = 2)
			BEGIN
				set @SQL = @SQL + '
					Week2 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), cmgt) as varchar(10))
									when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), SW) as varchar(10))
									when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), CW) as varchar(10)) 
									when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), gt) as varchar(10))
									when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W2EH) as varchar(10))
									when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W2EW) as varchar(10))
								else '''' end,
					Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
									when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
									when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10)) 
									when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
									when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
									when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
								else '''' end, 
					W2cmgt = cmgt, W2SW = SW, W2CW = CW, W2gt = gt, W2EW, W2EH, 
					W3cmgt = 0, W3SW = 0, W3CW = 0, W3gt = 0, W3EW = 0, W3EH = 0 ' 
			END
			ELSE
			BEGIN
				set @SQL = @SQL + '
					Week2 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W2cmgt) as varchar(10))
									when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W2SW) as varchar(10))
									when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W2CW) as varchar(10)) 
									when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W2gt) as varchar(10))
									when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W2EH) as varchar(10))
									when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W2EW) as varchar(10))
								else '''' end, 
					W2cmgt, W2SW, W2CW, W2gt, W2EW, W2EH, '
					IF (@DateTrue = 'Yes')
					BEGIN
						set @SQL = @SQL + '
							Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), cmgt) as varchar(10))
											when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), SW) as varchar(10))
											when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), CW) as varchar(10)) 
											when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), gt) as varchar(10))
											when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
											when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
										else '''' end, 							
							W3cmgt = cmgt, W3SW = SW, W3CW = CW, W3gt = gt, W3EW = 0, W3EH = 0 ' 
					END
					ELSE
					BEGIN
						set @SQL = @SQL + '
							Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
									when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
									when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10)) 
									when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
									when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
									when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
								else '''' end, 
							W3cmgt = 0, W3SW = 0, W3CW = 0, W3gt = 0, W3EW = 0, W3EH = 0 ' 
					END
			END
		END
END 
IF (@WeekNo = 3)
BEGIN
	set @SQL = @SQL + '
		Week1 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), w1cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W1SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W1CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W1gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W1EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W1EW) as varchar(10))
				    else '''' end, 
		Week2 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W2cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W2SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W2CW) as varchar(10)) 
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W2gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W2EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W2EW) as varchar(10))
				    else '''' end, 
		
		Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
								when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
								when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
							else '''' end, 
		W1cmgt, W1SW,  W1CW, W1gt, W1EH, W1EW, 
		W2cmgt, W2SW, W2CW, W2gt, W2EW, W2EH, 		
		W5cmgt = 0, W5SW = 0, W5CW = 0, W5gt = 0, W5EH = 0, W5EW = 0, '
		IF (@Sampling = 0)
		BEGIN
			set @SQL = @SQL + '	
				Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W3cmgt) as varchar(10))
							when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W3SW) as varchar(10))
							when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W3CW) as varchar(10))
							when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W3gt) as varchar(10))
							when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W3EH) as varchar(10))
							when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W3EW) as varchar(10))
						else '''' end, 
				W3cmgt, W3SW, W3CW, W3gt, W3EH, W3EW, 	'
				IF (@DateTrue = 'Yes')
				BEGIN	
					set @SQL = @SQL + '			
						Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W3cmgt) as varchar(10))
										when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W3SW) as varchar(10))
										when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W3CW) as varchar(10))
										when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W3gt) as varchar(10))
										when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W3EH) as varchar(10))
										when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W3EW) as varchar(10))
									else '''' end,	
						W4cmgt = W3cmgt, W4SW = W3SW, W4CW = W3CW, W4gt = W3gt, W4EH = W3EH, W4EW = W3EW '
				END
				ELSE
				BEGIN
					set @SQL = @SQL + '		
						Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
							when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
							when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
							when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
							when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
							when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
						else '''' end, 
						W4cmgt = 0, W4SW = 0, W4CW = 0, W4gt = 0, W4EH = 0, W4EW = 0 '	
				END				
		END
		ELSE
		BEGIN
			IF (@Sampling = 3)
			BEGIN
				set @SQL = @SQL + '
					Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), cmgt) as varchar(10))
							when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), SW) as varchar(10))
							when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), CW) as varchar(10))
							when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), gt) as varchar(10))
							when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W3EH) as varchar(10))
							when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W3EW) as varchar(10))
						else '''' end, 		
					Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
					else '''' end, 
					W3cmgt = cmgt, W3SW = SW, W3CW = CW, W3gt = gt, W3EH, W3EW, 
					W4cmgt = 0, W4SW = 0, W4CW = 0, W4gt = 0, W4EH = 0, W4EW = 0 '	
			END
			ELSE
			BEGIN
				set @SQL = @SQL + '
					Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W3cmgt) as varchar(10))
							when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W3SW) as varchar(10))
							when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W3CW) as varchar(10))
							when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W3gt) as varchar(10))
							when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W3EH) as varchar(10))
							when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W3EW) as varchar(10))
						else '''' end, 
					W3cmgt, W3SW, W3CW, W3gt, W3EH, W3EW,'
					IF (@DateTrue = 'Yes')
					BEGIN	
						set @SQL = @SQL + '
							Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), cmgt) as varchar(10))
											when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), SW) as varchar(10))
											when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), CW) as varchar(10))
											when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), gt) as varchar(10))
											when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
											when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
										else '''' end,							
							W4cmgt = cmgt, W4SW = SW, W4CW = CW, W4gt = gt, W4EH = 0, W4EW = 0 '	
					END
					ELSE
					BEGIN
						set @SQL = @SQL + '
							Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
										when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
										when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
										when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
										when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
										when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
									else '''' end, 
							W4cmgt = 0, W4SW = 0, W4CW = 0, W4gt = 0, W4EH = 0, W4EW = 0 '	
					END			
			END
		END
END 
IF (@WeekNo = 4)
BEGIN
	set @SQL = @SQL + '
		Week1 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), w1cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W1SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W1CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W1gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W1EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W1EW) as varchar(10))
				    else '''' end, 
		Week2 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W2cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W2SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W2CW) as varchar(10)) 
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W2gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W2EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W2EW) as varchar(10))
				    else '''' end, 
		Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W3cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W3SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W3CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W3gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W3EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W3EW) as varchar(10))
				    else '''' end, 
		W1cmgt, W1SW,  W1CW, W1gt, W1EH, W1EW, 
		W2cmgt, W2SW, W2CW, W2gt, W2EW, W2EH,
		W3cmgt, W3SW, W3CW, W3gt, W3EH, W3EW, '
		IF (@Sampling = 0)
		BEGIN
			set @SQL = @SQL + '
				Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W4cmgt) as varchar(10))
								when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W4SW) as varchar(10))
								when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W4CW) as varchar(10))
								when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W4gt) as varchar(10))
								when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W4EH) as varchar(10))
								when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W4EW) as varchar(10))
					else '''' end, 
				W4cmgt, W4SW, W4CW, W4gt, W4EH, W4EW, '
				If (@DateTrue = 'Yes')
				BEGIN
					set @SQL = @SQL + '
						Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W4cmgt) as varchar(10))
							when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W4SW) as varchar(10))
							when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W4CW) as varchar(10))
							when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W4gt) as varchar(10))
							when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W4EH) as varchar(10))
							when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W4EW) as varchar(10))
						else '''' end,						
						W5cmgt = W4cmgt, W5SW = W4SW, W5CW = W4CW, W5gt = W4gt, W5EH = W4EH, W5EW = W4EW '
				END
				ELSE
				BEGIN
					set @SQL = @SQL + '
						Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
								when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
								when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
								when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
							else '''' end,
						W5cmgt = 0, W5SW = 0, W5CW = 0, W5gt = 0, W5EH = 0, W5EW = 0 '
				END
		END
		ELSE
		BEGIN
			IF (@Sampling = 4)
			BEGIN
				set @SQL = @SQL + '
					Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W4EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W4EW) as varchar(10))
					else '''' end,
					Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
								else '''' end,
					W4cmgt = cmgt, W4SW = SW, W4CW = CW, W4gt = gt, W4EH, W4EW, 
					W5cmgt = 0, W5SW = 0, W5CW = 0, W5gt = 0, W5EH = 0, W5EW = 0 '
			END
			ELSE
			BEGIN
				set @SQL = @SQL + '
					Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W4cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W4SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W4CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W4gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W4EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W4EW) as varchar(10))
					else '''' end,
					W4cmgt, W4SW, W4CW, W4gt, W4EH, W4EW, '
					IF (@DateTrue = 'Yes')
					BEGIN
						set @SQL = @SQL + '
							Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0),cmgt) as varchar(10))
											when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), SW) as varchar(10))
											when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), CW) as varchar(10))
											when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), gt) as varchar(10))
											when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
											when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
										else '''' end,					
							W5cmgt = cmgt, W5SW = SW, W5CW = EH, W5gt = gt, W5EH = 0, W5EW = 0 '
					END
					ELSE
					BEGIN
						set @SQL = @SQL + '
							Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), 0) as varchar(10))
									when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
									when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), 0) as varchar(10))
									when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), 0) as varchar(10))
									when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
									when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), 0) as varchar(10))
									else '''' end,
							W5cmgt = 0, W5SW = 0, W5CW = 0, W5gt = 0, W5EH = 0, W5EW = 0 '
					END
			END
		END
END
IF (@WeekNo = 5)
BEGIN
	set @SQL = @SQL + '
		Week1 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), w1cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W1SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W1CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W1gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W1EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W1EW) as varchar(10))
				    else '''' end, 
		Week2 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W2cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W2SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W2CW) as varchar(10)) 
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W2gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W2EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W2EW) as varchar(10))
				    else '''' end, 
		Week3 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W3cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W3SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W3CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W3gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W3EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W3EW) as varchar(10))
				    else '''' end, 
		Week4 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W4cmgt) as varchar(10))
						when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W4SW) as varchar(10))
						when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W4CW) as varchar(10))
						when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W4gt) as varchar(10))
						when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W4EH) as varchar(10))
						when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W4EW) as varchar(10))
					else '''' end, 
		W1cmgt, W1SW,  W1CW, W1gt, W1EH, W1EW, 
		W2cmgt, W2SW, W2CW, W2gt, W2EW, W2EH,
		W3cmgt, W3SW, W3CW, W3gt, W3EH, W3EW, 
		W4cmgt, W4SW, W4CW, W4gt, W4EH, W4EW, '
		IF (@Sampling = 0)
		BEGIN
			set @SQL = @SQL + '
				Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W5cmgt) as varchar(10))
								when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W5SW) as varchar(10))
								when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W5CW) as varchar(10))
								when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W5gt) as varchar(10))
								when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W5EH) as varchar(10))
								when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W5EW) as varchar(10))
							else '''' end, 		
				W5cmgt, W5SW, W5CW, W5gt, W5EH, W5EW '
		END
		ELSE
		BEGIN
			IF (@DateTrue = 'Yes')
			BEGIN
				set @SQL = @SQL + '
					Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), cmgt) as varchar(10))
									when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), SW) as varchar(10))
									when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), CW) as varchar(10))
									when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), gt) as varchar(10))
									when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W5EH) as varchar(10))
									when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W5EW) as varchar(10))
								else '''' end, 		
					W5cmgt = cmgt, W5SW = SW, W5CW = CW, W5gt = gt, W5EH, W5EW '
			END
			ELSE
			BEGIN
				set @SQL = @SQL + '
				Week5 = case when [Type] = ''cmg/t'' then cast(convert(numeric(7,0), W5cmgt) as varchar(10))
								when [Type] = ''SW'' and Activity <> 1 then cast(convert(numeric(7,0), W5SW) as varchar(10))
								when [Type] = ''CW'' and Activity <> 1 then cast(convert(numeric(7,0), W5CW) as varchar(10))
								when [Type] = ''Total g/t'' then cast(convert(numeric(7,2), W5gt) as varchar(10))
								when [Type] = ''EH'' and Activity = 1 then cast(convert(numeric(7,1), W5EH) as varchar(10))
								when [Type] = ''EW'' and Activity = 1 then cast(convert(numeric(7,1), W5EW) as varchar(10))
							else '''' end, 		
				W5cmgt, W5SW, W5CW, W5gt, W5EH, W5EW '
			END
		END
END
set @SQL1 =' 
	from  
	(  
		select p.ProdMonth, p.SectionID SectionID, sc.NAME Name, 
			p.WorkplaceID WorkplaceID, w.Description Description , p.Activity Activity, 
			ActDesc = Act.Description, 
			isnull(p.ReefWaste,'''') ReefWaste, 
			z.[Order], 
			Type = case when z.[Type] = ''cmg/t'' then  ''cmg/t''
			            when p.Activity <> 1 and z.[Type] = ''SW'' then ''SW'' 
						 when p.Activity <> 1 and z.[Type] = ''CW'' then ''CW''
						 when p.Activity = 1 and z.[Type] = ''EH'' then ''EH''
						 when p.Activity = 1 and z.[Type] = ''EW'' then ''EW''
						 When z.[Type] = ''Total g/t'' then  ''Total g/t'' else '''' end ,
			RSqm = case when p.Activity <> 1 then isnull(p.ReefSqm,0) else isnull(p.ReefAdv,0) end,  
			WSqm = case when p.Activity <> 1 then isnull(p.WasteSqm,0) else isnull(p.WasteAdv,0) end, 
			Cubics = isnull(Cubics,0), 			
			W1cmgt = isnull(convert(numeric(7,0), k1.cmgt),0),  
			W1SW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), k1.StopeWidth),0) else 0 end,  
			W1CW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), k1.Channelwidth),0) else 0 end,	
			W1gt = case when p.activity <> 1 and k1.StopeWidth > 0 then isnull(convert(numeric(7,2), k1.cmgt / k1.StopeWidth),0.00) 
					    when p.activity = 1 and k1.endHeight > 0 then  isnull(convert(numeric(7,2), k1.cmgt / k1.EndHeight),0.00) else 0 end,
			W1EW = case when p.Activity = 1 then isnull(convert(numeric(7,1), k1.EndWidth),0.00) else 0 end,  
			W1EH = case when p.Activity = 1 then isnull(convert(numeric(7,1), k1.EndHeight),0.00) else 0 end, 	 					 
			W2cmgt = isnull(convert(numeric(7,0), k2.cmgt),0),   
			W2SW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), k2.StopeWidth),0) else 0 end,  
			W2CW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), k2.Channelwidth),0) else 0 end,
			W2gt = case when p.activity <> 1 and k2.StopeWidth > 0 then isnull(convert(numeric(7,2), k2.cmgt / k2.StopeWidth),0.00) 
					    when p.activity = 1 and k2.endHeight > 0 then  isnull(convert(numeric(7,2), k2.cmgt / k2.EndHeight),0.00) else 0 end,  
			W2EW = case when p.Activity = 1 then isnull(convert(numeric(7,1), k2.EndWidth),0.00) else 0 end,  
			W2EH = case when p.Activity = 1 then isnull(convert(numeric(7,1), k2.EndHeight),0.00) else 0 end,	
			W3cmgt = isnull(convert(numeric(7,0), k3.cmgt),0),   
			W3SW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), k3.StopeWidth),0) else 0 end,  
			W3CW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), k3.Channelwidth),0) else 0 end,
			W3gt = case when p.activity <> 1 and k3.StopeWidth > 0 then isnull(convert(numeric(7,2), k3.cmgt / k3.StopeWidth),0.00) 
					    when p.activity = 1 and k3.endHeight > 0 then  isnull(convert(numeric(7,2), k3.cmgt / k3.EndHeight),0.00) else 0 end,    
			W3EW = case when p.Activity = 1 then isnull(convert(numeric(7,1), k3.EndWidth),0.00) else 0 end,  
			W3EH = case when p.Activity = 1 then isnull(convert(numeric(7,1), k3.EndHeight),0.00) else 0 end, 		
			W4cmgt = isnull(convert(numeric(7,0), k4.cmgt),0),   
			W4SW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), k4.StopeWidth),0) else 0 end,  
			W4CW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), k4.Channelwidth),0) else 0 end,
			W4gt = case when p.activity <> 1 and k4.StopeWidth > 0 then isnull(convert(numeric(7,2), k4.cmgt / k4.StopeWidth),0.00) 
					    when p.activity = 1 and k4.endHeight > 0 then  isnull(convert(numeric(7,2), k4.cmgt / k4.EndHeight),0.00) else 0 end, 
			W4EW = case when p.Activity = 1 then isnull(convert(numeric(7,1), k4.EndWidth),0.00) else 0 end,  
			W4EH = case when p.Activity = 1 then isnull(convert(numeric(7,1), k4.EndHeight),0.00) else 0 end,    	
			W5cmgt =  isnull(convert(numeric(7,0), k5.cmgt),0),   
			W5SW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), k5.StopeWidth),0) else 0 end,  
			W5CW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), k5.Channelwidth),0) else 0 end, 
			W5gt = case when p.activity <> 1 and k5.StopeWidth > 0 then isnull(convert(numeric(7,2), k5.cmgt / k5.StopeWidth),0.00) 
					    when p.activity = 1 and k5.endHeight > 0 then  isnull(convert(numeric(7,2), k5.cmgt / k5.EndHeight),0.00) else 0 end,
			W5EW = case when p.Activity = 1 then isnull(convert(numeric(7,1), k5.EndWidth),0.00) else 0 end,  
			W5EH = case when p.Activity = 1 then isnull(convert(numeric(7,1), k5.EndHeight),0.00) else 0 end, 
			
			cmgt =  isnull(convert(numeric(7,0), sa.cmgt),0),  
			SW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), sa.SWidth), 0) else 0 end,  
			CW = case when p.Activity <> 1 then isnull(convert(numeric(7,0), sa.Channelwidth), 0) else 0 end, 
			gt = case when p.Activity <> 1 and sa.SWidth > 0 then isnull(convert(numeric(10,2), sa.cmgt / sa.SWidth), 0.00) else 0 end,	 
			ss.RockDensity Dens
		from PlanMonth p  
		inner join Section_Complete sc on  
		  sc.Prodmonth = p.Prodmonth and  
		  sc.SECTIONID = p.SectionID  
		inner join Workplace w on  
		  w.WorkplaceID = p.WorkplaceID  
		inner join Seccal s on  
		  s.Prodmonth = p.Prodmonth and  
		  s.Sectionid = sc.SECTIONID_1   
		left outer join Code_Activity act on  
		  act.Activity = p.Activity   
		left outer join Kriging k1 on  
		  p.ProdMonth = k1.ProdMonth and  
		  p.Workplaceid = k1.WorkplaceID and   
		  p.Activity = k1.Activity and k1.WeekNo = 1
		left outer join Kriging k2 on  
		  p.ProdMonth = k2.ProdMonth and  
		  p.Workplaceid = k2.WorkplaceID and   
		  p.Activity = k2.Activity and k2.WeekNo = 2
		left outer join Kriging k3 on  
		  p.ProdMonth = k3.ProdMonth and  
		  p.Workplaceid = k3.WorkplaceID and   
		  p.Activity = k3.Activity and k3.WeekNo = 3
		left outer join Kriging k4 on  
		  p.ProdMonth = k4.ProdMonth and  
		  p.Workplaceid = k4.WorkplaceID and   
		  p.Activity = k4.Activity and k4.WeekNo = 4 
		left outer join Kriging k5 on  
		  p.ProdMonth = k5.ProdMonth and  
		  p.Workplaceid = k5.WorkplaceID and   
		  p.Activity = k5.Activity and k5.WeekNo = 5
		left outer join 
			(select s.* from sampling s 
			 inner join  
				(select MAX(calendardate) calendardate, workplaceid 
				 from sampling group by workplaceid
				) a on 
				  s.WorkplaceID = a.WorkplaceID and 
				  s.CalendarDate = a.calendardate
			) sa on 
				sa.workplaceid = p.workplaceid, SysSet ss,
		(select [Order] = 1, Type = ''Total g/t''
		union
		select [Order] = 2, Type = ''cmg/t''
		union
		select [Order] = 3, Type = ''SW''
		union
		select [Order] = 4, Type = ''EH''
		union
		select [Order] = 5, Type = ''CW''
		union
		select [Order] = 6, Type = ''EW''
		) z
		where sc.SectionID_2 = '''+@SectionID+''' and  
			 p.Prodmonth = '''+@ProdMonth+''' and
			 p.PlanCode= ''MP''


	) a 
	where a.Type <> '''' 
	order by a.SectionID, a.Name, a.WorkplaceID, a.Description, a.Activity, a.[Order], a.[Type] '


--print (@SQL)
--print (@SQL1)
exec (@SQL+@SQL1)
go



CREATE Procedure [dbo].[sp_Load_BookABSStoping_Detail] 
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
		@BookDate varchar(10)
as

--set @Prodmonth = 201703
--set @SectionID = 'REAA'
--set @BookDate = '2017-03-16'


Declare @SQL VarChar(8000)

set @SQL = ' 
 select * from 
  ( 
     select pm.ProdMonth, pm.sectionid Sec, pm.sectionid SectionID, 
         pm.workplaceid WPID, w.Description, 
         pm.workplaceid + '':'' +w.Description WP, 
         pm.Activity, act.Description ActDesc, ShiftDay, 
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
		 DefaultAdv = cast(1.2 as numeric(7,2)), 
         isnull(pd.CMGT, 0) BookCmgt, 
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
         BookCodeStp = case when prbook.NoteID = ''ST'' then prbook.NoteID else isnull(pd.BookCode,'''') end, 
         isnull(p.SBNotes,'''') BookProb, 
         isnull(p.NoteID,'''') NoteID, isnull(p.SBNotes,'''') SBNotes, 
         isnull(pd.CheckSqm, 0) CheckSqm, isnull(pd.MOFC, 0) MOFC 
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
      left outer join BOOKINGPROBLEM p on 
        pm.Prodmonth = p.Prodmonth and 
        pm.SectionID = p.SectionID and 
        pm.WorkplaceID = p.WorkplaceID and 
        pm.Activity = p.Activity and 
        pm.PlanCode = p.PlanCode and 
		pm.IsCubics = p.IsCubics and
        pd.Calendardate = p.CalendarDate 
      left outer join 
       ( 
             select b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics,
                max(CalendarDate) CalendarDate 
             from BookingProblem b 
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
       left outer join BookingProblem prbook on 
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
        aa.WorkplaceID = p.WorkplaceID and 
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

CREATE Procedure [dbo].[sp_Load_BookABSStoping]
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
        @DaysBackdate int,
        @Shift int
as

--set @Prodmonth = 201703
--set @SectionID = 'REAA'
--Set @DaysBackdate = 0
--set @Shift = 3


Declare @SQL VarChar(8000),
 @Backdate DateTime


 --Select @TheshiftTime

--@SQL1 VarChar(8000), -- Forecast and Cleaning Bookings
--@SQL2 VarChar(8000)
select @backdate = Rundate - @DaysBackdate  From sysset 

set @SQL = ' select * from 
	(select sc.Name_1 SBName, sc.SectionID_2, sc.Name Name, pm.SectionID, 
	pm.Workplaceid+'' : ''+w.Description Workplace,
	pm.Activity, 
	ShiftDay = case when ct.WorkingDay = ''Y'' then convert(varchar(3), p.ShiftDay) else ''N'' end,
	isnull(ct.WorkingDay,'''') WorkingDay,
	ct.CalendarDate CalendarDate,  
	''Book'' BookType,
	isnull(pr.ProblemID,'''') NoteID,
	Type = z.[Type],
	ABSCode = isnull(p.AbsCode,''''), 
	MonthSqm = case when z.[Type] = ''Plan'' then convert(varchar(10), cast(pm.SQM as numeric(7,0))) else '''' end,
	--case when isnull(ProgP.Prog_Plan_SQM,0) ProgPlan,
	ProgSqm =  case when z.[Type] = ''Plan'' then 
					convert(varchar(10), cast(pp.ProgPlanSQM as numeric(7,0))) else 
					convert(varchar(10), cast(pp.ProgBookSQM as numeric(7,0))) end,
	theVal = case 
  when z.[Type] = ''Plan'' and isnull(p.SQM,0) = 0 then ''''
  when z.[Type] = ''Plan'' and isnull(p.SQM,0) > 0 then convert(varchar(10), convert(Numeric(7,0),isnull(p.SQM,0)))
  when z.[Type] = ''Book'' and isnull(pr.ProblemID, '''') <> '''' then isnull(pr.ProblemID,'''')
  when z.[Type] = ''Book'' and isnull(p.BookSQM,0) = 0 then '''' 
  when z.[Type] = ''Book'' and isnull(p.BookSQM,0) > 0 then
		convert(varchar(10), convert(Numeric(7,0),isnull(p.BookSQM,0))) else ''''
	end,
	theValue = case 
  when z.[Type] = ''Plan'' then convert(Numeric(7,0),isnull(p.SQM,0))
  when z.[Type] = ''Book'' then convert(Numeric(7,0),isnull(p.BookSQM,0)) else 0 end
	from planmonth pm	
	inner join Section_Complete sc on 
	  pm.SectionID = sc.SectionID and
	  pm.ProdMonth = sc.ProdMonth
	inner join Seccal s on
	  sc.ProdMonth = s.ProdMonth and
	  sc.SectionID_1 = s.SectionID
	inner join CalType ct on
	  s.CalendarCode = ct.CalendarCode and
	  s.BeginDate <= ct.CalendarDate and
	  s.enddate >= ct.CalendarDate
	inner join Planning p on
	  p.ProdMonth = pm.ProdMonth and 
	  p.SectionID = pm.SectionID and
	  p.WorkplaceID = pm.WorkplaceID and
	  p.Activity = pm.Activity and
	  p.IsCubics = pm.IsCubics and
	  p.PlanCode = pm.PlanCode and
	  p.Calendardate = ct.CalendarDate
	left outer join
		(select ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode, sum(isnull(Booksqm,0)) ProgBookSQM,
		 sum(isnull(SQM,0)) ProgPlanSQM
		 from Planning, Sysset
		 where CalendarDate <= SYSSET.RUNDATE and
		 PlanCode = ''MP'' and
		 Activity <> 1 and
		 IsCubics = ''N''
		 group by ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode
		) pp on
	  pp.ProdMonth = pm.ProdMonth and 
	  pp.SectionID = pm.SectionID and
	  pp.WorkplaceID = pm.WorkplaceID and
	  pp.Activity = pm.Activity and
	  pp.IsCubics = pm.IsCubics and
	  pp.PlanCode = pm.PlanCode
	left outer join BOOKINGPROBLEM pr on 
        pm.Prodmonth = pr.Prodmonth and 
        pm.SectionID = pr.SectionID and 
        pm.WorkplaceID = pr.WorkplaceID and 
        pm.Activity = pr.Activity and 
        pm.PlanCode = pr.PlanCode and 
        p.Calendardate = pr.CalendarDate 
	inner join Workplace W on
	  pm.WorkplaceID = w.WorkplaceID, SYSSET ss,
	(select [Order] = 1,
	Type = ''Plan''
	union
	select [Order] = 2,
	Type = ''Book''
	) z
	where pm.prodmonth = '''+ @Prodmonth +''' and sc.SectionID_1= '''+@SectionID+''' 
	and pm.Activity in (0,9) and pm.tons > 0 and pm.PlanCode = ''MP'' and pm.IsCubics = ''N'''

--if @DaysBackdate = 0
--begin  
--Set @SQL = @SQL+'and ct.calendardate = convert(Date, ss.rundate) ) a '
--end
--else
--begin
Set @SQL = @SQL+'and ct.calendardate <= ss.rundate ) a 
		order by SectionID_2, SectionID, Workplace,CalendarDate ' 
			--	and p.calendardate >= '''+Convert(Varchar(10),@backdate,120)+''' '
--end


--print (@SQL)
exec (@SQL)

go











alter table planmonth
add DHeight Numeric(7,1) null,
DWidth Numeric(7,1) null,
Density Numeric(7,1) null,
ReefWaste varchar(1) null
go

alter table Planning
add AdjSqm float null,
AdjCont float  null,
AdjTons float null,
CheckMeasProb varchar(150) null,
MOFC numeric(7,0) null,
ABSCode varchar(1) null,
ABSNotes varchar(255) null,
ABSPicNo varchar(3) null,
ABSPrec varchar(1) null,
BookCubics numeric(7,0) null,
BookSweeps numeric(7,0) null,
BookReSweeps numeric(7,0) null,
BookVamps numeric(7,0) null,
PegID varchar(50) null,
PegToFace Decimal(18,1) null,
PegDist Decimal(18,3) null,
BookOpenUp Decimal(18,1) null,
BookSecM Numeric(7,1) null,
BookCode varchar(4) null,
CheckSqm Numeric(7,0) null
go

CREATE TABLE [dbo].[CODE_BOOKING](
[Activity] numeric(7,0) not null,
	[BookingCode] [varchar](3) NOT NULL,
	[Description] [varchar](20) NULL,
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Code_SICCapture](
	[SICKey] [int] IDENTITY(1,1) NOT NULL,
	[Kpi] [varchar](30) NULL,
	[Description] [varchar](50) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[KRIGING_LOG](
	[UpdateDate] [datetime] NULL,
	[UserID] [varchar](50) NULL,
	[MachineID] [varchar](128) NULL,
	[Type] [char](1) NULL,
	[APP_NAME] [varchar](256) NULL,
	[OldProdMonth] [numeric](7, 0) NULL,
	[NewProdMonth] [numeric](7, 0) NULL,
	[OldSectionID] [varchar](20) NOT NULL,
	[NewSectionID] [varchar](20) NOT NULL,
	[OldWorkplaceID] [varchar](12) NULL,
	[NewWorkplaceID] [varchar](12) NULL,
	[OldActivity] [numeric](7, 0) NULL,
	[NewActivity] [numeric](7, 0) NULL,
	[OldWeekNo] [numeric](7, 0) NULL,
	[NewWeekNo] [numeric](7, 0) NULL,
	[OldChannelWidth] [numeric](18, 0) NULL,
	[NewChannelWidth] [numeric](18, 0) NULL,
	[OldStopeWidth] [numeric](18, 0) NULL,
	[NewStopeWidth] [numeric](18, 0) NULL,
	[OldCMGT] [numeric](18, 0) NULL,
	[NewCMGT] [numeric](18, 0) NULL,
	[OldGT] [numeric](7, 2) NULL,
	[NewGT] [numeric](7, 2) NULL,
	[OldEndHeight] [numeric](5, 1) NULL,
	[NewEndHeight] [numeric](5, 1) NULL,
	[OldEndWidth] [numeric](5, 1) NULL,
	[NewEndWidth] [numeric](5, 1) NULL,
	[OldUserID] [varchar](50) NULL,
	[NewUserID] [varchar](50) NULL,
	[OldCreateDate] DateTime NULL,
	[NewCreateDate] DateTime NULL
) ON [PRIMARY]

GO


create trigger [dbo].[KRIGING_TRIGGER] on [dbo].[KRIGING] for update, delete
as
declare
	@UpdateDate Datetime ,
	@UserID varchar(128),
	@MachineID varchar(128),
	@Type Char,
	@APP_NAME varchar(256), 
	@OldProdMonth numeric(7, 0),
	@NewProdMonth numeric(7, 0),
	@OldSectionID varchar(20),
	@NewSectionID varchar(20),
	@OldWorkplaceID varchar(12),
	@NewWorkplaceID varchar(12),
	@OldActivity numeric(7, 0),
	@NewActivity numeric(7, 0),
	@OldWeekNo numeric(7, 0),
	@NewWeekNo numeric(7, 0),
	@OldChannelWidth numeric(18, 0),
	@NewChannelWidth numeric(18, 0),
	@OldStopeWidth numeric(18, 0),
	@NewStopeWidth numeric(18, 0),
	@OldCMGT numeric(18, 0),
	@NewCMGT numeric(18, 0),
	@OldGT numeric(7, 2),
	@NewGT numeric(7, 2),
	@OldEndHeight numeric(5, 1),
	@NewEndHeight numeric(5, 1),
	@OldEndWidth numeric(5, 1),
	@NewEndWidth numeric(5, 1),
	@OldUserID varchar(50),
	@NewUserID varchar(50),
	@OldCreateDate Datetime,
	@NewCreateDate Datetime

-- date and user
select @UserID = system_user ,
@UpdateDate = getdate(),
@MachineID = HOST_NAME(),
@APP_NAME = APP_NAME()

-- Action
if exists (select * from inserted)
if exists (select * from deleted)
select @Type = 'U'
else
select @Type = 'I'
else
select @Type = 'D'

select 
	@NewProdMonth = ProdMonth,
	@NewSectionID = SectionID,
	@NewWorkplaceID = WorkplaceID,
	@NewActivity = Activity,
	@NewWeekNo = WeekNo,
	@NewChannelWidth = ChannelWidth,
	@NewStopeWidth = StopeWidth,
	@NewCMGT = CMGT,
	@NewGT = GT,
	@NewEndHeight = EndHeight,
	@NewEndWidth = EndWidth,
	@NewUserID = UserID,
	@NewCreateDate = CreateDate
	from inserted I

select
	@OldProdMonth = ProdMonth,
	@OldSectionID = SectionID,
	@OldWorkplaceID = WorkplaceID,
	@OldActivity = Activity,
	@OldWeekNo = WeekNo,
	@OldChannelWidth = ChannelWidth,
	@OldStopeWidth = StopeWidth,
	@OldCMGT = CMGT,
	@OldGT = GT,
	@OldEndHeight = EndHeight,
	@OldEndWidth = EndWidth,
	@OldUserID = UserID,
	@OldCreateDate = CreateDate
from deleted I

declare @saverec varchar(1)
set @saverec = '';

if (@NewProdMonth <> @OldProdMonth or
	@NewSectionID <> @OldSectionID or
	@NewWorkplaceID <> @OldWorkplaceID or
	@NewActivity <> @OldActivity or
	@NewWeekNo <> @OldWeekNo or
	@NewChannelWidth <> @OldChannelWidth or
	@NewStopeWidth <> @OldStopeWidth or
	@NewCMGT <> @OldCMGT or
	@NewGT <> @OldGT or
	@NewEndHeight <> @OldEndHeight or
	@NewEndWidth <> @OldEndWidth or
	@NewUserID <> @OldUserID or
	@NewCreateDate <> @OldCreateDate)
begin

	Insert Into Kriging_Log 
	(
		UpdateDate, UserID, MachineID, [Type], [APP_NAME],
		NewProdMonth, OldProdMonth,
		NewSectionID, OldSectionID,
		NewWorkplaceID, OldWorkplaceID,
		NewActivity, OldActivity,
		NewWeekNo, OldWeekNo,
		NewChannelWidth, OldChannelWidth,
		NewStopeWidth, OldStopeWidth,
		NewCMGT, OldCMGT,
		NewGT, OldGT,
		NewEndHeight, OldEndHeight,
		NewEndWidth, OldEndWidth,
		NewUserID, OldUserID,
		NewCreateDate, OldCreateDate
	)
	select 
		@UpdateDate UpdateDate, @UserID UserID, @MachineID MachineID, @Type [Type], @APP_NAME [APP_NAME], 
		@NewProdMonth, @OldProdMonth,
		@NewSectionID, @OldSectionID,
		@NewWorkplaceID, @OldWorkplaceID,
		@NewActivity, @OldActivity,
		@NewWeekNo, @OldWeekNo,
		@NewChannelWidth, @OldChannelWidth,
		@NewStopeWidth, @OldStopeWidth,
		@NewCMGT, @OldCMGT,
		@NewGT, @OldGT,
		@NewEndHeight, @OldEndHeight,
		@NewEndWidth, @OldEndWidth,
		@NewUserID, @OldUserID,
		@NewCreateDate, @OldCreateDate
end
go

CREATE TABLE [dbo].[BOOK_TRAMMING](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[Sectionid] [varchar](20) NOT NULL,
	[Workplaceid] [varchar](15) NOT NULL,
	[BookDate] [datetime] NOT NULL,
	[Morning] [numeric](18, 0) NULL,
	[Afternoon] [numeric](18, 0) NULL,
	[Night] [numeric](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[Sectionid] ASC,
	[Workplaceid] ASC,
	[BookDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[BOOK_TRAMMING_COMMENTS](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[Sectionid] [varchar](20) NOT NULL,
	[Workplaceid] [varchar](15) NOT NULL,
	[BookDate] [datetime] NOT NULL,
	[Morning] [varchar](150) NULL,
	[Afternoon] [varchar](150) NULL,
	[Night] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[Sectionid] ASC,
	[Workplaceid] ASC,
	[BookDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[CODE_HOPPERS](
	[theID] [int] IDENTITY(1,1) NOT NULL,
	[theDescription] [varchar](30) NULL,
	[theFactor] [numeric](5, 2) NULL
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[CODE_CLEANTYPES](
	[CleanTypeID] [int] IDENTITY(1,1) NOT NULL,
	[CleanTypeDesc] [varchar](40) NULL,
	[SurvRep] [varchar](1) NULL
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[CODE_CUBICTYPES](
	[CubicTypeID] [int] IDENTITY(1,1) NOT NULL,
	[CubicTypeDesc] [varchar](40) NULL,
	[Activity] [int] NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CODE_INDICATORS](
	[IndicatorID] [int] IDENTITY(1,1) NOT NULL,
	[IndicatorDesc] [varchar](40) NULL
) ON [PRIMARY]

GO

Create Procedure [dbo].[sp_Load_Planning] 
	@Prodmonth NUMERIC(7,0),@Sectionid_2 VARCHAR(20),@Activity NUMERIC(1,0)
	
AS


--DECLARE @iProdmonth NUMERIC(7,0),@iSectionid_2 VARCHAR(20),@iActivity NUMERIC(1,0)
--SET @iProdmonth = 201706
--SET @iSectionid_2 = 'REA'
--SET @iActivity = 0


DECLARE @iProdmonth NUMERIC(7,0),@iSectionid_2 VARCHAR(20),@iActivity NUMERIC(1,0)

SET @iProdmonth = @Prodmonth
SET @iSectionid_2 = @Sectionid_2
SET @iActivity = @Activity


UPDATE Planmonth SET Locked = 0  FROM dbo.Planmonth a inner join SECTION_COMPLETE b on 
a.Prodmonth = b.Prodmonth and
a.sectionid = b.SectionID
where a.Prodmonth = @iProdmonth and Sectionid_2 = @iSectionid_2 and Activity = @iActivity and Locked is null and Plancode = 'MP'

DECLARE @HasValues NUMERIC(3,0),@Locked BIT

--SET @Locked = 'N'

IF @iActivity IN (0,3) -- Start Stoping Ledging
BEGIN
-- Test if there is pre-planning for requested prouction month
SET @HasValues = (SELECT COUNT(a.PRODMONTH) FROM dbo.Planmonth a inner join SECTION_COMPLETE b on 
a.Prodmonth = b.Prodmonth and
a.sectionid = b.SectionID
WHERE Activity = 0 AND
      Sectionid_2 = @iSectionid_2 AND
      a.Prodmonth = @iProdmonth and Plancode = 'MP')
     
     
      
IF @HasValues = 0 -- if no pre-planning load data from previus month dinamic planning
BEGIN
SELECT @iProdmonth Prodmonth,
       SC.SECTIONID SectionID,
       SC.SECTIONID_2 Sectionid_2,
       WP.WORKPLACEID Workplaceid,
       wp.DESCRIPTION WorkplaceDesc,
       PM.ACTIVITY Activity,
       PM.IsCubics,
       PM.TargetID TargetID,
       PM.ReefSQM + PM.WasteSQM callValue,
       PM.ReefSQM ReefSQM,
       PM.WasteSQM WasteSQM,
       PM.CUBICMETRES CubicMetres,
	     case when  PM.CubicsReef is null then 0 else PM.CubicsReef end CubicsReef,
	  case when PM.CubicsWaste is null then 0 else PM.CubicsWaste end CubicsWaste ,
       PM.FL FL,
       dbo.CalcFaceAdvance(PM.SW,PM.CUBICMETRES,PM.FL,PM.SQM,PM.WasteSQM) FaceAdvance,
       CASE WHEN PM.CW < 80 THEN 100
            ELSE PM.CW + 20 END IdealSW,
       SAM.StopeWidth SW,
       SAM.Channelwidth CW,
       cast(0 as bit) isApproved,
       SAM.CMGT CMGT,
	   0 CMKGT,
	   dbo.CalcGoldBrokenSQM(PM.ReefSQM  ,SAM.CMGT,PM.WORKPLACEID) Kg,
	   dbo.CalcUraniumBrokenSQM(PM.ReefSQM  ,0,PM.WORKPLACEID) UraniumBrokenKg,
       dbo.CalcGoldBrokenSQM(PM.ReefSQM  ,SAM.CMGT,PM.WORKPLACEID) +
      (dbo.CalcGoldBrokenCUB(PM.CUBICMETRES,SAM.GT,SAM.StopeWidth,PM.WORKPLACEID)/1000) GoldBroken,
	  (dbo.CalcUraniumBrokenSQM(PM.SQM  ,0,PM.WORKPLACEID) +
       dbo.CalcUraniumBrokenCUB(PM.CUBICMETRES,0,SAM.StopeWidth,PM.WORKPLACEID)) UraniumBroken, 
       dbo.CalcGoldBrokenCUB(PM.CUBICMETRES,SAM.GT,SAM.StopeWidth,PM.WORKPLACEID) CubicGrams,0 DynamicCubicGT,

       CASE WHEN SAM.CMGT IS NULL THEN 0  
			ELSE SAM.CMGT  END FaceCMGT,
       dbo.CalcFaceBrokenKG(PM.ReefSQM  ,SAM.CMGT,PM.WORKPLACEID) FaceBrokenKG,
       dbo.CalcFaceTonsCUBE(PM.CUBICMETRES,PM.WORKPLACEID) + dbo.CalcFaceTonsSQM(PM.ReefSQM  + PM.WasteSQM,SAM.StopeWidth,PM.WORKPLACEID) FaceTons,

             dbo.CalcFaceValue(dbo.CalcGoldBrokenSQM(PM.ReefSQM  + PM.WasteSQM,SAM.CMGT,PM.WORKPLACEID) +
                         dbo.CalcGoldBrokenCUB(PM.CUBICMETRES,SAM.gt,SAM.StopeWidth,PM.WORKPLACEID)/1000,
                         dbo.CalcFaceTonsCUBE(PM.CUBICMETRES,PM.WORKPLACEID) + 
                         dbo.CalcFaceTonsSQM(PM.ReefSQM  + PM.WasteSQM,SAM.StopeWidth,PM.WORKPLACEID) ) FaceValue, 
        dbo.CalcTrammedTons(dbo.CalcFaceTonsCUBE(PM.CUBICMETRES ,PM.WORKPLACEID) + dbo.CalcFaceTonsSQM(PM.SQM + PM.WasteSQM,SAM.StopeWidth,PM.WORKPLACEID)) TrammedTons ,
        dbo.CalcTrammedValue(dbo.CalcFaceTonsCUBE(PM.CUBICMETRES ,PM.WORKPLACEID) + dbo.CalcFaceTonsSQM(PM.SQM + PM.WasteSQM,SAM.StopeWidth,PM.WORKPLACEID),
                             dbo.CalcGoldBrokenSQM(PM.ReefSQM + PM.WasteSQM,PM.cmgt,PM.WORKPLACEID)  +
                             dbo.CalcGoldBrokenCUB(PM.CUBICMETRES,SAM.gt,SAM.StopeWidth,PM.WORKPLACEID)/1000) TrammedValue ,       
       '' TrammedLevel,CASE WHEN wp.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType,
       PM.OrgUnitDay OrgUnitDay,
       PM.OrgUnitAfternoon OrgUnitAfternoon,
       PM.OrgUnitNight OrgUnitNight,
       PM.RomingCrew RomingCrew,
       cast(0 as bit) Locked,
       SECC.BeginDate StartDate,SECC.ENDDATE EndDate, '' StoppedDate, IsStopped, cast(1 as bit) hasChanged ,cast(0 as bit) hasRevised
        FROM dbo.PLANMONTH PM
INNER JOIN dbo.SECTION_COMPLETE SC ON
PM.PRODMONTH = SC.PRODMONTH AND
PM.SECTIONID = SC.SECTIONID
INNER JOIN dbo.WORKPLACE WP ON
WP.WORKPLACEID = PM.WORKPLACEID 
LEFT JOIN OreFlowentities o on o.oreflowid = wp.oreflowid
LEFT JOIN vw_Kriging_Latest SAM on
PM.Prodmonth = SAM.Prodmonth AND
PM.Workplaceid = SAM.Workplaceid
inner join seccal SECC on 
  SECC.prodmonth = @iProdmonth and
  SC.sectionID_1 = SECC.sectionid
WHERE PM.PRODMONTH = dbo.GetPrevProdMonth(@iProdmonth) AND
      SC.SECTIONID_2 = @iSectionid_2 AND
      (PM.IsStopped <> 'Y' OR PM.IsStopped IS NULL) AND 
      PM.ACTIVITY = 0 AND
      PM.IsCubics = 'N' and PM.Plancode = 'MP' and PM.Locked = 1
      
UNION

SELECT  @iProdmonth Prodmonth,
       SC.SECTIONID SectionID,
       SC.SECTIONID_2 Sectionid_2,
       WP.WORKPLACEID Workplaceid,
       wp.DESCRIPTION WorkplaceDesc,
       PM.ACTIVITY Activity,
       'N' IsCubics,
       PM.TargetID TargetID,
       PM.ReefSQM + PM.WasteSQM callValue,
       PM.ReefSQM SQM,
       PM.WasteSQM WasteSQM,
       PM.CUBICMETRES CubicMetres,
	      case when  PM.CubicsReef is null then 0 else PM.CubicsReef end CubicsReef,
	  case when PM.CubicsWaste is null then 0 else PM.CubicsWaste end CubicsWaste ,
       PM.FL FL, 
       dbo.CalcFaceAdvance(PM.SW,PM.CUBICMETRES,PM.FL,PM.ReefSQM,PM.WasteSQM) FaceAdvance,
       CASE WHEN PM.CW < 80 THEN 100
            ELSE PM.CW + 20 END IdealSW,
       SAM.StopeWidth SW,
       SAM.Channelwidth CW,
       cast(0 as bit) isApproved,
       SAM.CMGT CMGT,
	   0 CMKGT,
	   dbo.CalcGoldBrokenSQM(PM.ReefSQM  ,SAM.CMGT,PM.WORKPLACEID) Kg,
	   dbo.CalcUraniumBrokenSQM(PM.ReefSQM  ,0,PM.WORKPLACEID) UraniumBrokenKg,
       dbo.CalcGoldBrokenSQM(PM.ReefSQM  ,SAM.CMGT,PM.WORKPLACEID) +
       dbo.CalcGoldBrokenCUB(PM.CUBICMETRES,SAM.gt,SAM.StopeWidth,PM.WORKPLACEID)/1000 GoldBroken,  
	   dbo.CalcUraniumBrokenSQM(PM.SQM  ,0,PM.WORKPLACEID) +
       dbo.CalcUraniumBrokenCUB(PM.CUBICMETRES,0,SAM.StopeWidth,PM.WORKPLACEID) UraniumBroken,    
	   dbo.CalcGoldBrokenCUB(PM.CUBICMETRES,SAM.GT,SAM.StopeWidth,PM.WORKPLACEID) CubicGrams, DynamicCubicGT = 0  ,   
       SAM.CMGT FaceCMGT,
       dbo.CalcFaceBrokenKG(PM.ReefSQM ,SAM.CMGT,PM.WORKPLACEID) FaceBrokenKG,
       --PM.ReefSQM * 0.027 * SAM.BookCMGT/1000 FaceBrokenKG,
       dbo.CalcFaceTonsCUBE(PM.CUBICMETRES,PM.WORKPLACEID) + dbo.CalcFaceTonsSQM(PM.ReefSQM  + PM.WasteSQM,SAM.StopeWidth,PM.WORKPLACEID) FaceTons,
       --(PM.CUBICMETRES * 2.7) + ((PM.ReefSQM + PM.WasteSQM) * (PM.SW/100) * 2.7) FaceTons,
       dbo.CalcFaceValue(dbo.CalcGoldBrokenSQM(PM.ReefSQM  + PM.WasteSQM,SAM.CMGT,PM.WORKPLACEID) +
                         dbo.CalcGoldBrokenCUB(PM.CUBICMETRES,SAM.gt,SAM.StopeWidth,PM.WORKPLACEID)/1000,
                         dbo.CalcFaceTonsCUBE(PM.CUBICMETRES,PM.WORKPLACEID) + 
                         dbo.CalcFaceTonsSQM(PM.ReefSQM  + PM.WasteSQM,SAM.StopeWidth,PM.WORKPLACEID) ) FaceValue,
       --CASE WHEN PM.CUBICMETRES > 0 AND PM.SW > 0  AND PM.SW > 0 THEN (((PM.CUBICMETRES / ( PM.SW / 100)) * 0.027 * PM.cmgt/1000) * 1000)/ (PM.CUBICMETRES * 2.7) 
       --     WHEN PM.ReefSQM > 0 AND (PM.ReefSQM + PM.WasteSQM) > 0 THEN ((PM.ReefSQM  * 0.027 * PM.cmgt/1000) * 1000)/ ((PM.ReefSQM + PM.WasteSQM) * (SAM.DynamicSW/100) * 2.7) END FaceValue ,
       0 TrammedTons,
       0 TrammedValue,
       '' TrammedLevel,CASE WHEN wp.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType,
       PM.OrgUnitDay OrgUnitDay,
       PM.OrgUnitAfternoon OrgUnitAfternoon,
       PM.OrgUnitNight OrgUnitNight,
       PM.RomingCrew RomingCrew,
       cast(0 as bit) Locked,
       SECC.BeginDate StartDate,SECC.ENDDATE EndDate, '' StoppedDate, IsStopped, cast(1 as bit) hasChanged ,cast(0 as bit) hasRevised
        FROM dbo.PLANMONTH PM
INNER JOIN dbo.SECTION_COMPLETE SC ON
PM.PRODMONTH = SC.PRODMONTH AND
PM.SECTIONID = SC.SECTIONID
INNER JOIN dbo.WORKPLACE WP ON
WP.WORKPLACEID = PM.WORKPLACEID INNER join oreflowentities o on o.oreflowid = wp.oreflowid
LEFT JOIN vw_Kriging_Latest SAM on
PM.Prodmonth = SAM.Prodmonth AND
PM.Workplaceid = SAM.Workplaceid
inner join seccal SECC on 
  SECC.prodmonth = @iProdmonth and 
  SC.sectionID_1 = SECC.sectionid
WHERE PM.PRODMONTH = dbo.GetPrevProdMonth(@iProdmonth) AND
    --  SC.SECTIONID_2 = @iSectionid_2 AND
  --    (PM.IsStopped <> 'Y' OR PM.IsStopped IS NULL) AND
      PM.IsCubics = 'Y'  AND
      PM.CUBICMETRES <> 0 and PM.Plancode = 'MP' and PM.locked = 1
END
ELSE -- load from pre-planning
BEGIN
SELECT pp.Activity ,
      case when SW is null then 0 else SW END SW,
       CASE WHEN CW IS NULL THEN 0 ELSE CW END CW,
       dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,SC.SECTIONID_2) isApproved,
      CASE WHEN  pp.CMGT IS NULL THEN 0 ELSE pp.CMGT END CMGT,
	   0 CMKGT,	  
	   dbo.CalcGoldBrokenSQM(PP.ReefSQM  ,SAM.CMGT,PP.WORKPLACEID) Kg,
	   0 UraniumBrokenKg,
       CubicMetres ,
	    case when  CubicsReef is null then 0 else CubicsReef end CubicsReef,
	  case when CubicsWaste is null then 0 else CubicsWaste end CubicsWaste ,
       dbo.CalcFaceAdvance(PP.SW,PP.CUBICMETRES,PP.FL,PP.ReefSQM,PP.WasteSQM) FaceAdvance,       
       CASE WHEN PP.Locked = 0 AND (SAM.CMGT IS NOT NULL or SAM.CMGT <> 0) 
	        THEN PP.SQM * 0.027 * SAM.CMGT/1000 ELSE CASE WHEN   FaceKG IS NULL THEN 0 ELSE FaceKG END END  FaceBrokenKG,
       CASE WHEN PP.Locked = 0 AND (SAM.CMGT IS NOT NULL or SAM.CMGT <> 0) 
		    THEN SAM.CMGT ELSE CASE WHEN FaceCMGT IS NULL THEN 0 ELSE FaceCMGT END  END FaceCMGT,
      CASE WHEN  PP.FL IS NULL THEN 0 ELSE PP.FL END FL,
       --dbo.CalcFaceTonsCUBE(PP.CUBICMETRES ,PP.WORKPLACEID) + 
	   dbo.CalcFaceTonsSQM(PP.ReefSQM  + PP.WasteSQM,PP.SW,PP.WORKPLACEID) FaceTons,
       dbo.CalcFaceValue(dbo.CalcGoldBrokenSQM(PP.ReefSQM  + PP.WasteSQM,sam.CMGT,PP.WORKPLACEID) +
                         dbo.CalcGoldBrokenCUB(PP.CUBICMETRES,SAM.GT,PP.SW,PP.WORKPLACEID)/1000,
                         dbo.CalcFaceTonsCUBE(PP.CUBICMETRES,PP.WORKPLACEID) + 
                         dbo.CalcFaceTonsSQM(PP.ReefSQM  + PP.WasteSQM,PP.SW,PP.WORKPLACEID) ) FaceValue,               
       dbo.CalcGoldBrokenSQM(PP.ReefSQM ,SAM.CMGT,PP.WORKPLACEID)  +
       (dbo.CalcGoldBrokenCUB(PP.CUBICMETRES,SAM.GT,PP.SW,PP.WORKPLACEID)/1000) GoldBroken,
	   (dbo.CalcUraniumBrokenSQM(PP.ReefSQM  ,0,PP.WORKPLACEID) +
       dbo.CalcUraniumBrokenCUB(PP.CUBICMETRES,0,PP.SW,PP.WORKPLACEID)) UraniumBroken,
	   (dbo.CalcGoldBrokenCUB(PP.CUBICMETRES,SAM.GT,SAM.StopeWidth,PP.WORKPLACEID)) CubicGrams, 0 DynamicCubicGT,
	   CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,SC.SECTIONID_2) = 1 
	   THEN IdealSW ELSE dbo.CalcIdealSW(PP.cw) END IdealSW,
       IsCubics ,
      CASE WHEN ReefSQM + WasteSQM IS NULL THEN 0 ELSE ReefSQM + WasteSQM END callValue,
       Reefadv ,
       Wasteadv ,
       OrgUnitAfternoon ,
       OrgUnitDay ,
       OrgUnitNight ,
       PP.Prodmonth ,
       RomingCrew ,
       PP.Sectionid SectionID,
       SC.Sectionid_2 ,
       SQM ,
       TargetID ,
       '' TrammedLevel ,
      -- dbo.CalcTrammedTons(dbo.CalcFaceTonsCUBE(PP.CUBICMETRES ,PP.WORKPLACEID) + 
	   dbo.CalcFaceTonsSQM(PP.ReefSQM  + PP.WasteSQM,PP.SW,PP.WORKPLACEID) TrammedTons ,
       dbo.CalcTrammedValue(dbo.CalcFaceTonsCUBE(PP.CUBICMETRES ,PP.WORKPLACEID) + dbo.CalcFaceTonsSQM(PP.ReefSQM  + PP.WasteSQM,PP.SW,PP.WORKPLACEID),
       dbo.CalcGoldBrokenSQM(PP.ReefSQM  + PP.WasteSQM,PP.CMGT,PP.WORKPLACEID)  +
       dbo.CalcGoldBrokenCUB(PP.CUBICMETRES,SAM.GT,PP.SW,PP.WORKPLACEID)/1000) TrammedValue ,
	   CASE WHEN wp.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType,
        WasteSQM ,
		case when	ReefSQM is null then 0 else ReefSQM END ReefSQM,
        wp.Description WorkplaceDesc ,
        CASE WHEN  WP.Workplaceid IS NULL THEN '-1' ELSE WP.Workplaceid  END Workplaceid ,
        PP.Locked,
		CASE WHEN PP.StartDate is null THEN SECCAL.Begindate ELSE PP.StartDate END StartDate,
        CASE WHEN PP.EndDate is null then SECCAL.ENDDATE ELSE PP.EndDate END EndDate  ,
		PP.StoppedDate, 
		IsStopped,
		CASE WHEN dbo.CalcIdealSW(PP.cw) <> PP.IdealSW then cast(1 as bit)
		     WHEN dbo.CalcFaceAdvance(PP.SW,PP.CUBICMETRES,PP.FL,PP.ReefSQM,PP.WasteSQM)  <> FaceAdvance then cast(1 as bit) 
		     WHEN WP.Workplaceid <> PP.Workplaceid then cast(1 as bit) 
			 WHEN (dbo.CalcGoldBrokenSQM(PP.ReefSQM ,PP.CMGT,PP.WORKPLACEID)  + dbo.CalcGoldBrokenCUB(PP.CUBICMETRES,SAM.GT,PP.SW,PP.WORKPLACEID)) <> cast(Kg as float) then cast(1 as bit) 
			 else cast(0 as bit) end hasChanged,--PP.CUBICMETRES,SAM.DynamicCubicGT,
	    case when RVP.hasRevised is null then cast( 0 as bit) else RVP.hasRevised end hasRevised
FROM dbo.planmonth PP
LEFT JOIN dbo.WORKPLACE WP ON
pp.Workplaceid = wp.Workplaceid  
left join oreflowentities o on o.oreflowid = wp.oreflowid  
LEFT JOIN vw_Kriging_Latest SAM ON
PP.WORKPLACEID = SAM.WORKPLACEID AND
PP.Prodmonth = SAM.Prodmonth 
LEFT JOIN SECTION_COMPLETE SC on
PP.Sectionid = SC.SECTIONID and
PP.Prodmonth = SC.PRODMONTH
	LEFT join SECCAL on
	SC.PRODMONTH = SECCAL.PRODMONTH and
	SC.SECTIONID_1 = SECCAL.SECTIONID 
left join ( SELECT DepC.WorkplaceID, Case when DepCount = AppCount then cast(1 as bit) else  cast(0 as bit) End hasRevised  FROM
(SELECT CR.WorkplaceID,count(CRA.Department) DepCount
  FROM [dbo].[PREPLANNING_CHANGEREQUEST] CR 
  INNER JOIN [dbo].[PREPLANNING_CHANGEREQUEST_APPROVAL] CRA on
  CR.ChangeRequestID = CRA.ChangeRequestID
  WHERE cr.ProdMonth =  @iProdmonth
  GROUP BY CR.WorkplaceID) DepC
  INNER JOIN 
  (SELECT CR.WorkplaceID,count(CRA.Approved) AppCount
  FROM [dbo].[PREPLANNING_CHANGEREQUEST] CR 
  INNER JOIN [dbo].[PREPLANNING_CHANGEREQUEST_APPROVAL] CRA on
  CR.ChangeRequestID = CRA.ChangeRequestID 
  WHERE CRA.Approved = 1  and cr.ProdMonth =  @iProdmonth
  GROUP BY CR.WorkplaceID) AppC on
  DepC.WorkplaceID = AppC.WorkplaceID) RVP on
RVP.[WorkplaceID] = pp.Workplaceid
WHERE pp.Activity IN (0,3)  AND
      sc.Sectionid_2 = @iSectionid_2 AND
      PP.Prodmonth = @iProdmonth and pp.plancode = 'MP' 
END  

END -- end Stoping Ledging  
      
--IF 0 IN (1) -- Start Dev
if @iActivity in(1)
BEGIN
-- Test if there is pre-planning for requested prouction month
SET @HasValues = (SELECT COUNT(a.PRODMONTH) FROM dbo.planmonth a inner join SECTION_COMPLETE b on 
a.Prodmonth = b.Prodmonth and
a.sectionid = b.SectionID
WHERE Activity = 1 AND
		--Activity = @iActivity  AND
      Sectionid_2 = @iSectionid_2 AND
      a.Prodmonth = @iProdmonth and Plancode = 'MP')
      
IF @HasValues = 0
BEGIN
--declare @iProdmonth numeric(7,0), @iSectionid_2 varchar(50)
--set @iProdmonth =201405
--set @iSectionid_2 ='56430'
SELECT @iProdmonth Prodmonth
       ,PM.SECTIONID Sectionid
	   , SC.SECTIONID_2 Sectionid_2
	   ,wp.DESCRIPTION WorkplaceDesc
	   ,WP.WORKPLACEID Workplaceid
	   ,PM.Activity Activity
	   ,PM.OrgUnitDay
	   ,PM.OrgUnitAfternoon
	   ,cast(PM.OrgUnitNight as varchar(50)) OrgUnitNight
	   ,PM.RomingCrew	   
	   ,PM.IsCubics
	   ,pm.TargetID
	   ,pm.DrillRig
	   ,PM.Reefadv + pm.Wasteadv [Metresadvance]
	   ,PM.Reefadv [ReefAdv]
	   ,pm.Wasteadv [WasteAdv],
	   PM.SW,
	     case when SAM.CMGT is null then 0 else SAM.CMGT end CMGT,
	    0 CMKGT,
	    case when  PM.CubicsReef is null then 0 else PM.CubicsReef end CubicsReef,
	  case when PM.CubicsWaste is null then 0 else PM.CubicsWaste end CubicsWaste ,
	   (dbo.CalcUraniumBrokenSQM(PM.Reefadv  ,0,PM.WORKPLACEID) +
       dbo.CalcUraniumBrokenCUB(PM.Reefadv,0,PM.SW,PM.WORKPLACEID)) UraniumBroken,
	    dbo.CalcUraniumBrokenSQM(PM.Reefadv  ,0,PM.WORKPLACEID) UraniumBrokenKg
	   ,PM.Reefadv DEVMAIN--CASE WHEN wp.ReefWaste = 'R' /*AND WP.GG025_TMS = 'M'*/ THEN PM.Reefadv ELSE 0 END DEVMAIN
	   ,0 DEVSEC --CASE WHEN wp.ReefWaste = 'R' /*AND WP.GG025_TMS = 'S'*/ THEN PM.Reefadv ELSE 0 END DEVSEC
	   ,CASE WHEN 2 = 1 THEN PM.Wasteadv + pm.Reefadv ELSE 0 END DEVCAP
	   ,TONS DEVTONS
	   ,'' TrammedLevel
	   ,ISNULL(PM.CubicMetres,0) CubicMetres
	   ,CASE WHEN wp.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType
	   ,'M' GG025_TMS
	   ,0 ACCOUNTCODE
	   ,ENDHEIGHT = Case when PM.DHeight = 0 or PM.DHeight is null then wp.ENDHEIGHT else PM.DHeight end
	   ,ENDWIDTH = Case when PM.DWidth = 0 or PM.DWidth is null then wp.ENDWIDTH else PM.DWidth end
	   ,wp.ReefWaste
	   --,@Locked Locked
	   ,SECC.BeginDate StartDate
	   ,SECC.ENDDATE EndDate
	   , '' StoppedDate
	   ,CASE WHEN IsStopped IS NULL OR PM.IsStopped = '' THEN 'N' ELSE PM.IsStopped END IsStopped
	   ,CASE WHEN SAM.GT IS NULL THEN 0 ELSE SAM.GT END GramPerTon
	   ,CASE WHEN SAM.CMGT IS NULL THEN 0 ELSE SAM.CMGT END CMGT
	   ,cast(1 as bit) hasChanged
	   ,CAST(0 AS BIT)Locked
FROM dbo.PLANMONTH PM
INNER JOIN dbo.SECTION_COMPLETE SC ON 
PM.PRODMONTH = SC.PRODMONTH AND
PM.SECTIONID = SC.SECTIONID
INNER JOIN dbo.WORKPLACE WP ON
PM.WORKPLACEID = WP.WORKPLACEID
INNER JOIN dbo.Reef R ON
WP.ReefID = R.ReefID
inner join oreflowentities o on o.oreflowid = wp.oreflowid
inner join seccal SECC on 
  SECC.prodmonth = @iProdmonth and
  SC.sectionID_1 = SECC.sectionid
LEFT JOIN vw_Kriging_Latest SAM ON
PM.WORKPLACEID = SAM.WORKPLACEID AND
PM.Prodmonth = SAM.Prodmonth 

WHERE PM.PRODMONTH = dbo.GetPrevProdMonth(@iProdmonth) AND
      SC.SECTIONID_2 = @iSectionid_2 AND
      pm.Activity = 1  AND
      PM.IsCubics = 'N' AND
      (PM.IsStopped <> 'Y' OR PM.IsStopped IS NULL) and plancode = 'MP' and pm.Locked=1
UNION
SELECT @iProdmonth Prodmonth
       ,PM.SECTIONID Sectionid
	   , SC.SECTIONID_2 Sectionid_2
	   ,wp.DESCRIPTION WorkplaceDesc
	   ,WP.WORKPLACEID Workplaceid
	   ,PM.Activity Activity
	   ,PM.OrgUnitDay
	   ,PM.OrgUnitAfternoon
	   ,cast(PM.OrgUnitNight as varchar(50)) OrgUnitNight
	   ,PM.RomingCrew	   
	   ,PM.IsCubics
	   ,pm.TargetID
	   ,pm.DrillRig
	   ,PM.Reefadv + pm.Wasteadv [Metresadvance]
	   ,PM.Reefadv [ReefAdv]
	   ,pm.Wasteadv [WasteAdv]
	   , PM.SW,
	     case when SAM.CMGT is null then 0 else SAM.CMGT end CMGT,
	    0 CMKGT,
	    case when  PM.CubicsReef is null then 0 else PM.CubicsReef end CubicsReef,
	  case when PM.CubicsWaste is null then 0 else PM.CubicsWaste end CubicsWaste ,
	   (dbo.CalcUraniumBrokenSQM(PM.Reefadv  ,0,PM.WORKPLACEID) +
       dbo.CalcUraniumBrokenCUB(PM.Reefadv,0,PM.SW,PM.WORKPLACEID)) UraniumBroken,
	    dbo.CalcUraniumBrokenSQM(PM.Reefadv  ,0,PM.WORKPLACEID) UraniumBrokenKg
	   ,CASE WHEN wp.ReefWaste = 'R' /*AND WP.GG025_TMS = 'M'*/ THEN PM.Reefadv ELSE 0 END DEVMAIN
	   ,CASE WHEN wp.ReefWaste = 'R' /*AND WP.GG025_TMS = 'S'*/ THEN PM.Reefadv ELSE 0 END DEVSEC
	   ,CASE WHEN 2 = 1 THEN PM.Wasteadv + pm.Reefadv ELSE 0 END DEVCAP
	   ,Tons DEVTONS
	   ,CASE WHEN o.Levelnumber IS NULL THEN '0' ELSE o.Levelnumber END  TrammedLevel
	   ,ISNULL(PM.CubicMetres,0) CubicMetres
	   ,CASE WHEN wp.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType
	   ,'M' GG025_TMS
	   ,0 ACCOUNTCODE
	   ,ENDHEIGHT = Case when PM.DHeight = 0 or PM.DHeight is null then wp.ENDHEIGHT else PM.DHeight end
	   ,ENDWIDTH = Case when PM.DWidth = 0 or PM.DWidth is null then wp.ENDWIDTH else PM.DWidth end
	   ,wp.ReefWaste
	   --,@Locked Locked
	   ,SECC.BeginDate StartDate
	   ,SECC.ENDDATE EndDate
	   , '' StoppedDate
	   ,CASE WHEN IsStopped IS NULL OR PM.IsStopped = '' THEN 'N' ELSE PM.IsStopped END IsStopped
	   ,CASE WHEN SAM.GT IS NULL THEN 0 ELSE SAM.GT END GramPerTon
	   ,CASE WHEN SAM.CMGT IS NULL THEN 0 ELSE SAM.CMGT END CMGT
	   ,cast(1 as bit) hasChanged
	   ,CAST(0 AS BIT)Locked
          FROM dbo.PLANMONTH PM
INNER JOIN dbo.SECTION_COMPLETE SC ON 
PM.PRODMONTH = SC.PRODMONTH AND
PM.SECTIONID = SC.SECTIONID
INNER JOIN dbo.WORKPLACE WP ON
PM.WORKPLACEID = WP.WORKPLACEID
INNER JOIN dbo.Reef R ON
WP.ReefID = R.ReefID
inner join oreflowentities o on o.oreflowid = wp.oreflowid
inner join seccal SECC on 
  SECC.prodmonth = dbo.GetPrevProdMonth(@iProdmonth) and
  SC.sectionID_1 = SECC.sectionid
LEFT JOIN vw_Kriging_Latest SAM ON
PM.WORKPLACEID = SAM.WORKPLACEID AND
SAM.Prodmonth = dbo.GetPrevProdMonth(@iProdmonth)   

WHERE PM.PRODMONTH = dbo.GetPrevProdMonth(@iProdmonth) AND
      SC.SECTIONID_2 = @iSectionid_2 AND
      pm.Activity = 7 AND
      PM.IsCubics = 'Y' AND
      (PM.IsStopped <> 'Y' OR PM.IsStopped IS NULL) and plancode = 'MP' and pm.Locked =1
END
ELSE
BEGIN

SELECT PP.Prodmonth,
       PP.Sectionid,
	   SC.Sectionid_2,
	   WP.Description WorkplaceDesc,
	   PP.Activity,
	   PP.[IsCubics],
       PP.[PlanCode],
	   pp.[StartDate],
	   PP.Workplaceid,
	   PP.[TargetID],
	   PP.OrgUnitDay,
	   PP.OrgUnitAfternoon,
	   PP.RomingCrew ,
	   PP.OrgUnitNight,
	   PP.Locked,
	   PP.[Auth],
	   PP.[SQM],
       PP.[ReefSQM],
       PP.[WasteSQM],
	    PP.SW,
	     case when SAM.CMGT is null then 0 else SAM.CMGT end CMGT,
	    0 CMKGT,
	    case when  PP.CubicsReef is null then 0 else PP.CubicsReef end CubicsReef,
	  case when PP.CubicsWaste is null then 0 else PP.CubicsWaste end CubicsWaste ,
	   (dbo.CalcUraniumBrokenSQM(PP.Reefadv  ,0,PP.WORKPLACEID) +
       dbo.CalcUraniumBrokenCUB(PP.Reefadv,0,PP.SW,PP.WORKPLACEID)) UraniumBroken,
	    dbo.CalcUraniumBrokenSQM(PP.Reefadv  ,0,PP.WORKPLACEID) UraniumBrokenKg,
	   CASE WHEN PP.FL IS NULL THEN 0 ELSE PP.FL end [FL],
	  case when PP.[ReefFL] is null then 0 else PP.[ReefFL] end [ReefFL],
      case when  PP.[WasteFL] is null then 0 else PP.[WasteFL] end [WasteFL],
      case when  PP.[FaceAdvance] is null then 0 else PP.[WasteFL] end [WasteFL],
	  case when  PP.[IdealSW] is null then 0 else PP.[IdealSW] end [IdealSW],
	  case when  PP.[SW] is null then 0 else PP.[SW] end [SW],
	  case when PP.[CW] is null then 0 else PP.[CW] end [CW],
	  case when PP.[CMGT] is null then 0 else PP.[CMGT] end [CMGT],
	  'N' IsCubics,
       PP.Workplaceid,
	  case when PP.Reefadv + PP.Wasteadv  is null then 0 else PP.Reefadv + PP.Wasteadv end [Metresadvance],
	  case when PP.Reefadv is null then 0 else PP.Reefadv end [ReefAdv],
	  case when PP.Wasteadv is null then 0 else PP.Wasteadv end [WasteAdv],
	   ISNULL(PP.CubicMetres,0) CubicMetres,
	   PP.TargetID,
	   PP.DrillRig,
       CASE WHEN wp.ReefWaste = 'R' /*AND WP.GG025_TMS = 'M'*/ THEN case when PP.Reefadv is null then 0 else PP.Reefadv end  ELSE 0 END [DevMain],  
       CASE WHEN wp.ReefWaste = 'R' /*AND WP.GG025_TMS = 'S'*/ THEN PP.Reefadv ELSE 0 END [DevSec],
	  ISNULL(PP.[DevCap],0) [DevCap],
        pp.Tons DEVTONS,
       CASE WHEN o.Levelnumber IS NULL  THEN '0' ELSE o.Levelnumber END  TrammedLevel,CASE WHEN wp.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType,
	   'M' GG025_TMS,
	   0 ACCOUNTCODE,
       ENDHEIGHT = Case when PP.DHeight = 0 or PP.DHeight is null then wp.ENDHEIGHT else PP.DHeight end
	   ,ENDWIDTH = Case when PP.DWidth = 0 or PP.DWidth is null then wp.ENDWIDTH else PP.DWidth end
	   ,SECC.BeginDate StartDate,
	   SECC.ENDDATE EndDate,
	   PP.StoppedDate,
	   ISNULL(wp.ReefWaste,'R') , 
	   CASE WHEN PP.IsStopped IS NULL OR PP.IsStopped = '' THEN 'N' ELSE PP.IsStopped END IsStopped,
	   CASE WHEN PP.gt is null THEN 0 ELSE PP.gt END GramPerTon ,
	   ISNULL(PP.CMGT,0),
	   CASE     WHEN WP.Workplaceid <> PP.Workplaceid then cast(1 as bit) else cast(0 as bit) end hasChanged,
	   case when RVP.hasRevised is null then cast( 0 as bit) else RVP.hasRevised end hasRevised
	   
	   
	   
	    
	   FROM dbo.planmonth PP
       
LEFT JOIN dbo.SECTION_COMPLETE SC ON 
PP.PRODMONTH = SC.PRODMONTH AND
PP.SECTIONID = SC.SECTIONID 
LEFT JOIN dbo.WORKPLACE WP ON
PP.Workplaceid = wp.Workplaceid

LEFT JOIN vw_Kriging_Latest SAM ON
PP.WORKPLACEID = SAM.WORKPLACEID AND
SAM.Prodmonth = @iProdmonth 

LEFT JOIN dbo.Reef R ON
WP.ReefID = R.ReefID
left join oreflowentities o on o.oreflowid = wp.oreflowid
left join seccal SECC on 
  SECC.prodmonth = @iProdmonth and
  SC.sectionID_1 = SECC.sectionid
  left join ( SELECT DepC.WorkplaceID, Case when DepCount = AppCount then cast(1 as bit) else  cast(0 as bit) End hasRevised  FROM
(SELECT CR.WorkplaceID,count(CRA.Department) DepCount
  FROM [dbo].[PREPLANNING_CHANGEREQUEST] CR 
  INNER JOIN [dbo].[PREPLANNING_CHANGEREQUEST_APPROVAL] CRA on
  CR.ChangeRequestID = CRA.ChangeRequestID
  WHERE cr.ProdMonth =  @iProdmonth
  GROUP BY CR.WorkplaceID) DepC
  INNER JOIN 
  (SELECT CR.WorkplaceID,count(CRA.Approved) AppCount
  FROM [dbo].[PREPLANNING_CHANGEREQUEST] CR 
  INNER JOIN [dbo].[PREPLANNING_CHANGEREQUEST_APPROVAL] CRA on
  CR.ChangeRequestID = CRA.ChangeRequestID 
  WHERE CRA.Approved = 1  and cr.ProdMonth =  @iProdmonth
  GROUP BY CR.WorkplaceID) AppC on
  DepC.WorkplaceID = AppC.WorkplaceID) RVP on
RVP.[WorkplaceID] = pp.Workplaceid
     
WHERE PP.Activity = 1 AND
      SC.Sectionid_2 = @iSectionid_2 AND
      PP.Prodmonth = @iProdmonth and plancode = 'MP'
END      

END

if @iActivity in(2)
BEGIN
SET @HasValues = (SELECT COUNT(PS.PRODMONTH) FROM dbo.PLANMONTH_SUNDRYMINING PS left join SECTION_COMPLETE  sc on
		 PS.Prodmonth =sc.PRODMONTH and
		 PS.SectionID =sc.SECTIONID
WHERE PS.Activity = 2 AND
		--Activity = @iActivity  AND
      SC.Sectionid_2 = @iSectionid_2 AND
      PS.Prodmonth = @iProdmonth and PS.Plancode = 'MP')
IF @HasValues = 0
BEGIN
select distinct A.Workplaceid,a.SectionID SECTIONID, b.Description WorkplaceDesc,  --lp.Locked,
       C.SMDescription [SupActivity], A.Activity ,CASE WHEN b.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType,
          a.Orgunitday, a.OrgunitNight,C.SMID,C.SMDescription SMDescription,SU.Description UnitType, 
         a.AddInfo, a.Units Call, a.MetresAdvance Meters, C.Efficiency, null OrgunitAfternoon, null RovingCrew, '' Trackless, cc.BeginDate StartDate ,cc.EndDate ,cast(1 as bit) hasChanged,cast(0 as bit) Locked 
         from PLANMONTH_SUNDRYMINING a 
         inner join workplace b on
           a.workplaceid = b.workplaceid
         inner join SUNDRYMINING_TYPE c on
           a.SMID = c.SMID
		   INNER JOIN SUNDRYMINING_UNIT SU ON
  SU.UnitBase =c.UnitBase
		   left join SECTION_COMPLETE  sc on
		 a.Prodmonth =sc.PRODMONTH and
		 a.SectionID =sc.SECTIONID left join SECCAL cc on
		 cc.SectionID =sc.SECTIONID_1 and
		cc.Prodmonth =sc.PRODMONTH 
		
         where SC.SECTIONID_2 =@iSectionid_2
         and a.Prodmonth = dbo.GetPrevProdMonth(@iProdmonth)
         and a.activity = 2 AND PlanCode ='MP'

end
else
begin
select distinct A.Workplaceid,a.SectionID SECTIONID, b.Description WorkplaceDesc,  --lp.Locked,
       C.SMDescription [SupActivity], A.Activity ,CASE WHEN b.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType,
          a.Orgunitday, a.OrgunitNight,C.SMID,C.SMDescription SMDescription,SU.Description UnitType, 
         a.AddInfo, a.Units Call, a.MetresAdvance Meters, C.Efficiency, null OrgunitAfternoon, null RovingCrew, '' Trackless, cc.Begindate StartDate ,cc.EndDate ,cast(1 as bit) hasChanged, a.Locked 
         from PLANMONTH_SUNDRYMINING a 
         inner join workplace b on
           a.workplaceid = b.workplaceid
         inner join SUNDRYMINING_TYPE c on
           a.SMID = c.SMID
		   INNER JOIN SUNDRYMINING_UNIT SU ON
  SU.UnitBase =c.UnitBase
		   left join SECTION_COMPLETE  sc on
		 a.Prodmonth =sc.PRODMONTH and
		 a.SectionID =sc.SECTIONID left join SECCAL cc on
		 cc.SectionID =sc.SECTIONID_1 and
		cc.Prodmonth =sc.PRODMONTH 
		
         where SC.SECTIONID_2 =@iSectionid_2
         and a.Prodmonth = @iProdmonth
         and a.activity = 2 AND PlanCode ='MP'
END
END

if @iActivity in(8)
BEGIN
SET @HasValues = (SELECT COUNT(PO.PRODMONTH) FROM dbo.PlanMonth_Oldgold PO left join SECTION_COMPLETE  sc on
		 PO.Prodmonth =sc.PRODMONTH and
		 PO.SectionID =sc.SECTIONID
WHERE PO.Activity = 8 AND
		--Activity = @iActivity  AND
      SC.Sectionid_2 = @iSectionid_2 AND
      PO.Prodmonth = @iProdmonth and PO.Plancode = 'MP')
IF @HasValues = 0
BEGIN
  select distinct A.Workplaceid,sc.sectionid_2,a.SectionID , ou.Description UnitType, b.Description WorkplaceDesc,'' as DepthRange, convert(varchar(2),c.OGID) TheID, cast(0 as bit) Locked,
   a.units  as ccall, CASE WHEN b.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType,
         C.OGDescription  [Activity],
         c.unitbase UnitType, a.Orgunitday, a.OrgunitAfterNoon , a.OrgunitNight, null RovingCrew,
         a.Units Call, a.Grade, a.Grams content, a.Depth, a.ActualDepth, ot.Efficiency, 
         case when  SAM.StopeWidth is null then 0 else SAM.StopeWidth end sw,case when SAM.GT is null then 0 else sam.GT end gt, '' Trackless,cc.BeginDate ,cc.EndDate ,cast(1 as bit) hasChanged 
         from PlanMonth_Oldgold a 
         inner join workplace b on
         a.workplaceid = b.workplaceid
		 inner join OLDGOLD_TYPE ot on
		 a.OGID =ot.OGID 
		 inner join OLDGOLD_UNIT ou on
		 ou.UnitBase =ot .UnitBase 
		 
         Left Join PLANMONTH_OLDGOLD  Lp on
         a.Prodmonth = lp.Prodmonth and
         a.SectionID = lp.SectionID and
         a.WORKPLACEID = lp.WORKPLACEID and
         a.ACTIVITY = lp.ACTIVITY and
         a.OGID = lp.OGID
         inner join OLDGOLD_TYPE  c on
         a.OGID = c.OGID
		 LEFT JOIN vw_Kriging_Latest SAM ON
		 A.WORKPLACEID = SAM.WORKPLACEID AND
		 SAM.Prodmonth = a.Prodmonth 

		 left join SECTION_COMPLETE  sc on
		 a.Prodmonth =sc.PRODMONTH and
		 a.SectionID =sc.SECTIONID left join SECCAL cc on
		 cc.SectionID =sc.SECTIONID_1 and
		cc.Prodmonth =sc.PRODMONTH

         where SC.SECTIONID_2 =@iSectionid_2
         and a.Prodmonth = dbo.GetPrevProdMonth(@iProdmonth)
         and a.activity = 8 AND a.PlanCode ='MP'
		end
else
begin
 select distinct A.Workplaceid,sc.sectionid_2,a.SectionID , ou.Description UnitType, b.Description WorkplaceDesc,'' as DepthRange, convert(varchar(2),c.OGID) TheID,  a.Locked,
  a.units as ccall, CASE WHEN b.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType,
         C.OGDescription  [Activity],
         c.unitbase UnitType, a.Orgunitday, a.OrgunitAfterNoon , a.OrgunitNight, null RovingCrew,
         a.Units Call, a.Grade, a.Grams content, a.Depth, a.ActualDepth, ot.Efficiency, 
        case when  SAM.StopeWidth is null then 0 else SAM.ChannelWidth end sw, case when SAM.GT is null then 0 else SAM.GT end gt, '' Trackless,cc.BeginDate ,cc.EndDate ,cast(1 as bit) hasChanged 
         from PlanMonth_Oldgold a 
         inner join workplace b on
         a.workplaceid = b.workplaceid
		 inner join OLDGOLD_TYPE ot on
		 a.OGID =ot.OGID 
		 inner join OLDGOLD_UNIT ou on
		 ou.UnitBase =ot .UnitBase 
		 
         Left Join PLANMONTH_OLDGOLD  Lp on
         a.Prodmonth = lp.Prodmonth and
         a.SectionID = lp.SectionID and
         a.WORKPLACEID = lp.WORKPLACEID and
         a.ACTIVITY = lp.ACTIVITY and
         a.OGID = lp.OGID
         inner join OLDGOLD_TYPE  c on
         a.OGID = c.OGID
		 LEFT JOIN vw_Kriging_Latest SAM ON
		 A.WORKPLACEID = SAM.WORKPLACEID AND
		 SAM.Prodmonth = a.Prodmonth 
		 left join SECTION_COMPLETE  sc on
		 a.Prodmonth =sc.PRODMONTH and
		 a.SectionID =sc.SECTIONID left join SECCAL cc on
		 cc.SectionID =sc.SECTIONID_1 and
		cc.Prodmonth =sc.PRODMONTH
         where SC.SECTIONID_2 =@iSectionid_2
         and a.Prodmonth = @iProdmonth
         and a.activity = 8 AND a.PlanCode ='MP'
END
END
GO
Create procedure [dbo].[sp_Save_Planning] 

	@Prodmonth numeric(7,0),@Sectionid VARCHAR(20), @Sectionid_2 VARCHAR(20), @Workplaceid  VARCHAR(20), @Activity NUMERIC(7,0),
	@IsCubics VARCHAR(5),@SQM NUMERIC(10,3),@WasteSQM NUMERIC(10,3),@FL NUMERIC(10,3),@FaceAdvance NUMERIC(10,3), @IdealSW NUMERIC(10,3),
	@ActualSW NUMERIC(10,3),@ChannelW NUMERIC(10,3),@CMGT NUMERIC(10,3),@FaceCMGT NUMERIC(10,3),@FaceBrokenKG NUMERIC(10,3),
	@FaceTons NUMERIC(10,3),@FaceValue NUMERIC(10,3),@TrammedTons NUMERIC(10,3),@TrammedValue NUMERIC(10,3),@TrammedLevel VarChar(10),
	@CubicMetres NUMERIC(10,3),@OrgUnitDay VARCHAR(50) , @OrgUnitAfternoon VARCHAR(50),
	@OrgUnitNight VARCHAR(50),@RomingCrew VARCHAR(50), @Meters NUMERIC(10,3),@MetersWaste NUMERIC(10,3),@WorkplaceDesc VARCHAR(32), @TargetID NUMERIC(13),
	@Locked BIT, @DEVTONS NUMERIC(10,3),@DEVMAIN NUMERIC(13,3),@DEVSEC NUMERIC(10,3),@DEVCAP NUMERIC(10,3),@DrillRig VARCHAR(20),
	@StartDate DATETIME,@EndDate DATETIME, @GramPerTon  NUMERIC(13,3), @SSID NUMERIC(7,0),@Units NUMERIC(10,2),@AddInfo VARCHAR(50),@AutoUnPlan VARCHAR(2),
	@CrewStrength int, @PlanCode varchar(2),@OGID int,@Depth int,@grams int,@actdepth int,@grade numeric(7,2),@gt NUmeric(7,2),@kg NUMERIC(10,3),@cubicgrams NUMERIC(10,3)
	,@EndHeight numeric(13,3),@EndWidth numeric(13,3),
	@cubicreef numeric(10,3),@cubicwaste numeric(10,3),@CMKGT NUMERIC(7,0),@UraniumBrokenKg NUMERIC(10,3), @RockType VarChar(5)
	--, @StoppedDate DATETIME	,@GoldBroken NUMERIC(10,3),
AS


DECLARE @AddUpdate INT
Declare @Density Numeric(7,3)
Declare @ReefWaste Int

if @rocktype = 'OFF' 
  set @ReefWaste = 1 
else 
  set @ReefWaste = 0

Select @Density = isnull(Density, 2.75) from WORKPLACE where workplaceid = @Workplaceid

if @Activity in(0,1)
begin     
SET @AddUpdate = (

SELECT COUNT(WorkplaceID) totalRows FROM dbo.Planmonth 
WHERE Prodmonth = @Prodmonth AND
      --Sectionid = @Sectionid AND
      Sectionid = @Sectionid AND
      WorkplaceID = @WorkplaceID AND
      Activity = @Activity and Plancode = 'MP') 
IF @AddUpdate = 0 
BEGIN                          
--select * from planmonth where prodmonth = 201602
INSERT INTO dbo.Planmonth
        ( Prodmonth ,
          Sectionid ,
          --Sectionid_2 ,
          --WorkplaceDesc ,
          Activity ,
          IsCubics ,
          Workplaceid,
          TargetID,
          SQM ,
		  ReefSQM ,
          WasteSQM ,
          CubicMetres ,
		  CubicsReef ,
		  CubicsWaste ,
          FL ,
          FaceAdvance ,
          IdealSW ,
          SW ,
          CW ,
          CMGT ,
          Kg ,
		  CubicGrams,
          FaceCMGT ,
          FaceKG ,
          Tons ,
		  ReefTons,
		  WasteTons,
          FaceValue ,
          TrammedTons ,
          TrammedValue ,
          TrammedLevel ,
          OrgUnitDay ,
          OrgUnitAfternoon ,
          OrgUnitNight ,
          RomingCrew ,
          Metresadvance ,
		  ReefAdv ,
          WasteAdv,
          Locked,
          DEVMAIN,
          DEVSEC,
          DEVCAP,
          DrillRig,
          StartDate,
          EndDate,
          GT,
		  PlanCode,
		  CMKGT,
		  UraniumBrokenKG,
		  CubicsTons,
		  DHeight, 
		  DWidth, 
		  Density,
		  ReefWaste
        )
VALUES  ( @Prodmonth , -- Prodmonth - numeric
          @Sectionid , -- Sectionid - varchar(20)
          --@Sectionid_2 , -- Sectionid_2 - varchar(20)
          --@WorkplaceDesc,          
          @Activity , -- Activity - numeric
          @IsCubics , -- IsCubics - varchar(5)
          @Workplaceid , -- Workplaceid - varchar(20)                   
          @TargetID,
		 @SQM + @WasteSQM,
          @SQM , -- SQM - numeric
          @WasteSQM , -- WasteSQM - numeric
          @CubicMetres , -- CubicMetres - numeric
		  @Cubicreef, 
		  @cubicwaste, 
          @FL , -- FL - numeric
          @FaceAdvance , -- FaceAdvance - numeric
          @IdealSW , -- IdealSW - numeric
          @ActualSW , -- ActualSW - numeric
          @ChannelW , -- ChannelW - numeric
          @CMGT , -- CMGT - numeric
		  @kg,
          @cubicgrams , -- GoldBroken - numeric
          @FaceCMGT , -- FaceCMGT - numeric
          @FaceBrokenKG , -- FaceBrokenKG - numeric
          case when @Activity = 0 then @FaceTons else (@Meters + @MetersWaste)*@EndHeight*@EndHeight*@Density end , -- FaceTons - numeric
		  case when @Activity = 0 then @SQM*@ActualSW/100*@Density else @Meters*@EndHeight*@EndHeight*@Density end, -- Reef Tons--
		  case when @Activity = 0 then @WasteSQM*@ActualSW/100*@Density else @MetersWaste*@EndHeight*@EndHeight*@Density end, -- Waste Tons--
          @FaceValue , -- FaceValue - numeric
          @TrammedTons , -- TrammedTons - numeric
          @TrammedValue , -- TrammedValue - numeric
          @TrammedLevel , -- TrammedLevel - VarChar
          @OrgUnitDay , -- OrgUnitDay - varchar(50)
          @OrgUnitAfternoon , -- OrgUnitAfternoon - varchar(50)
          @OrgUnitNight , -- OrgUnitNight - varchar(50)
          @RomingCrew , -- RomingCrew - varchar(50)
          @Meters + @MetersWaste,
		  @Meters , -- Meters - numeric
          @MetersWaste,  -- MetersWaste - numeric
          @Locked,
          @DEVMAIN,
          @DEVSEC,
          @DEVCAP,
          @DrillRig,
          @StartDate,
          @EndDate,
          @GramPerTon,
		 'MP',
		 @CMKGT,
		 @UraniumBrokenKg,
		 @CubicMetres*@Density,
		 @EndHeight,
		 @EndWidth,
		 @Density,
		 @ReefWaste
        )     

	  update WORKPLACE SET EndHeight=@EndHeight, EndWidth=@EndWidth where WorkplaceID = @WorkplaceID AND
      Activity = @Activity   
END      
ELSE 
BEGIN
UPDATE dbo.Planmonth SET SQM = @SQM + @WasteSQM,
						   ReefSQM = @SQM,  
                           WasteSQM = @WasteSQM, 
                           CubicMetres = @CubicMetres, 
						   CubicsReef = @CubicReef,
		                   CubicsWaste = @cubicwaste,
                           FL = @FL,
                           FaceAdvance = @FaceAdvance, 
                           IdealSW = @IdealSW, 
                           SW = @ActualSW,
                           CW = @ChannelW,
                           CMGT = @CMGT, 
                           Kg = @kg, 
						   CubicGrams=@cubicgrams,
                           FaceCMGT = @FaceCMGT, 
                           FaceKG = @FaceBrokenKG,
                           Tons = case when @Activity = 0 then @FaceTons else (@Meters + @MetersWaste)*@EndHeight*@EndHeight*@Density end ,
						   ReefTons = case when @Activity = 0 then @SQM*@ActualSW/100*@Density else @Meters*@EndHeight*@EndHeight*@Density end,
						   WasteTons = case when @Activity = 0 then @WasteSQM*@ActualSW/100*@Density else @MetersWaste*@EndHeight*@EndHeight*@Density end,
                           FaceValue = @FaceValue, 
                           TrammedTons = @TrammedTons, 
                           TrammedValue = @TrammedValue,
                           TrammedLevel = @TrammedLevel, 
                           OrgUnitDay = @OrgUnitDay, 
                           OrgUnitAfternoon = @OrgUnitAfternoon,
                           OrgUnitNight = @OrgUnitNight, 
                           RomingCrew = @RomingCrew, 
                           Metresadvance = @Meters + @MetersWaste,
						   ReefAdv = @Meters, 
                           WasteAdv = @MetersWaste,
                           -- WorkplaceDesc = @WorkplaceDesc,
                           Workplaceid = @Workplaceid,  
                           Sectionid = @Sectionid,
                           TargetID = @TargetID,
                           Locked = @Locked,
                           DEVMAIN = @DEVMAIN,
                           DEVSEC = @DEVSEC,
                           DEVCAP = @DEVCAP,
                           DrillRig = @DrillRig,
                           StartDate = @StartDate,
                           EndDate = @EndDate,
                           GT = @GramPerTon,
						   CMKGT=@CMKGT,
						   UraniumBrokenKG=@UraniumBrokenKg,
						   CubicsTons = @CubicMetres*@Density,
						   DHeight = @EndHeight,
						   DWidth = @EndWidth,
						   Density = @Density,
						   ReefWaste = @ReefWaste 
                           
WHERE Prodmonth = @Prodmonth AND
      --Sectionid = @Sectionid AND
      Sectionid = @Sectionid AND
      WorkplaceID = @WorkplaceID AND
      Activity = @Activity AND
      IsCubics = @IsCubics and PLANCODE = 'MP'   
	 
	 	  update WORKPLACE SET EndHeight=@EndHeight, EndWidth=@EndWidth where WorkplaceID = @WorkplaceID AND
      Activity = @Activity                            
END
end
else
if @Activity in(2)
begin
SET @AddUpdate = (

SELECT COUNT(WorkplaceID ) totalRows FROM dbo.PLANMONTH_SUNDRYMINING  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @Sectionid AND
      --Sectionid_2 = @Sectionid_2 AND
      --WorkplaceDesc = @WorkplaceDesc AND
	  WorkplaceID = @Workplaceid and
	  SMID =@SSID and
      Activity = @Activity 
	  and OrgunitDay = @OrgUnitDay
	  and Plancode = 'MP')

IF @AddUpdate = 0 
BEGIN
INSERT INTO [dbo].[PLANMONTH_SUNDRYMINING]
           ([Prodmonth]
           ,[SectionID]
           ,[WorkplaceID]
           ,[Activity]
           ,[SMID]
           ,[PlanCode]
           ,[OrgunitDay]
           ,[OrgunitNight]
           ,[Units]
           ,[AddInfo]
           ,[AutoUnPlan]
           ,[CrewStrength]
           ,[MetresAdvance])
VALUES  (   @Prodmonth , -- Prodmonth - numeric
			@Sectionid , -- Sectionid - varchar(20)
			@Workplaceid , -- Workplaceid - varchar(20)
			-- @Sectionid_2 , -- Sectionid_2 - varchar(20)
			-- @WorkplaceDesc,          
			@Activity , -- Activity - numeric
			-- @IsCubics , -- IsCubics - varchar(5)
			@SSID,
			@PlanCode,		 
			@OrgUnitDay , -- OrgUnitDay - varchar(50)        
			@OrgUnitNight , -- OrgUnitNight - varchar(50)
			@Units,
			@AddInfo,
			@AutoUnPlan,
			@CrewStrength,    
			@Meters  -- Meters - numeric
         
        )     
END      
ELSE 
BEGIN
UPDATE dbo.PLANMONTH_SUNDRYMINING  SET 
OrgUnitNight = @OrgUnitNight,                            
Metresadvance = @Meters,
Units = @Units
--,
--SMID = @SSID,						  
--Workplaceid = @Workplaceid,  
--Sectionid = @Sectionid
                          
                           
WHERE Prodmonth = @Prodmonth AND
      --Sectionid = @Sectionid AND
      Sectionid = @Sectionid AND
      --WorkplaceDesc = @WorkplaceDesc AND
	  WorkplaceID = @Workplaceid and
      Activity = @Activity AND
	  SMID = @SSID and
	  OrgUnitDay = @OrgUnitDay and
      PLANCODE = 'MP'   
	 
	                          
END
end

else
if @Activity in(8)
begin
	SET @AddUpdate = (

	SELECT COUNT(WorkplaceID ) totalRows FROM dbo.[PLANMONTH_OLDGOLD]  
	WHERE Prodmonth = @Prodmonth AND
		  Sectionid = @Sectionid AND
		  OGID = @OGID and
		  WorkplaceID = @Workplaceid and
		  OrgUnitDay = @OrgUnitDay and
		  Activity = @Activity and 
		  Plancode = 'MP')

	IF @AddUpdate = 0 
	BEGIN
	INSERT INTO [dbo].[PLANMONTH_OLDGOLD]
			   ([Prodmonth]
			   ,[SectionID]
			   ,[WorkplaceID]
			   ,[Activity]
			   ,[OGID]
			   ,[PlanCode]
			   ,[OrgunitDay]
			   ,[OrgunitNight]
			   ,[Units]
			   ,[OrgunitAfterNoon]
			   ,[ActualDepth]
			   ,[Depth]
			   ,[Grade],
			  [Grams])
	VALUES  ( @Prodmonth , -- Prodmonth - numeric
			  @Sectionid , -- Sectionid - varchar(20)
			  @Workplaceid , -- Workplaceid - varchar(20)        
			  @Activity , -- Activity - numeric
			  @OGID,
			  @PlanCode,		 
			  @OrgUnitDay , -- OrgUnitDay - varchar(50)        
			  @OrgUnitNight , -- OrgUnitNight - varchar(50)
			  @Units,
		      @OrgUnitAfternoon ,
			  @actdepth ,
			  @Depth ,    
			  @gt ,  -- Meters - numeric
			  @grams 
         
			)     
	END      
	ELSE 
	BEGIN
		UPDATE dbo.PLANMONTH_OLDGOLD  SET 
			OrgUnitNight = @OrgUnitNight,   
			OrgunitAfterNoon =@OrgUnitAfternoon ,                         
			Units  = @Units ,
			Grams= @grams,
			Grade = @gt, 
			ActualDepth = @actdepth  				  
                          
                           
		WHERE Prodmonth = @Prodmonth AND
			  --Sectionid = @Sectionid AND
			  Sectionid = @Sectionid AND
			  --WorkplaceDesc = @WorkplaceDesc AND
			  WorkplaceID =@Workplaceid and
			  Activity = @Activity AND
			  PLANCODE = 'MP' and
			  OrgUnitDay = @OrgUnitDay and  
			  OGID = @OGID

	end 

	SET @AddUpdate = (
	select Count(*) from vw_Kriging_Latest where Prodmonth =@Prodmonth and WorkplaceID =@Workplaceid and WeekNo = 1)

	IF @AddUpdate = 0 
	begin
	  Insert into KRIGING(Prodmonth, SectionID,Workplaceid, Activity, WeekNo, ChannelWidth, StopeWidth, CMGT, gt, EndHeight, EndWidth)
	  Values (@Prodmonth, @Sectionid, @Workplaceid, @Activity, 1, @ChannelW, 1000, @CMGT,@gt,@EndHeight,@EndWidth)
	end
	else
	begin
		update KRIGING set
		ChannelWidth = @ChannelW,
		StopeWidth = 1000,
		CMGT = @CMGT, 
		GT =@gt,
		EndHeight = @EndHeight,
		EndWidth = @EndWidth
		where Prodmonth =@Prodmonth and WorkplaceID =@Workplaceid and
		WeekNo = 1 
	end
end


GO

CREATE Procedure [dbo].[SP_LoadTramming_Planning]
@Prodmonth varchar(6),
@SectionID VarChar(30)

AS
Select TheTypePlanning = 'Normal', a.Prodmonth, b.Sectionid_2, a.Sectionid,  a.WorkplaceID, a.Activity Activitycode, a.IsCubics,
Activity = case 
when a.Activity in (0,3) and IsCubics = 'N' then 'Stoping'
when a.Activity in (0,3) and IsCubics = 'Y' then 'Stoping Cubics'
when a.Activity in (1) then 'Development'
when a.Activity in (7) then 'Development Cubics'
end,
c.Description WorkplaceDesc, XC = case when (a.BoxholeID = '' or a.BoxholeID is Null)
then c.Description
else BHW.Description
end, a.Tons, 
Convert(Numeric(7,3),KG) KG,
Grade = case when a.Tons = 0 then 0 else
Convert(Numeric(7,1),round((KG*1000)/a.Tons,1)) end,
c.BoxholeID, (select Name from OREFLOWENTITIES where OreFlowID = c.BoxholeID) BH, BHW.Description BHXC
from  planmonth a  inner join SECTION_COMPLETE b on a.prodmonth = b.PRODMONTH and a.sectionid = b.SECTIONID  
inner join Workplace c on a.Workplaceid = c.WORKPLACEID  

left join Workplace BHW on a.BOXHoleID = BHW.workplaceid 
where a.Prodmonth = @Prodmonth  and b.SectionID_2 = @SectionID and PLancode = 'MP'
-- [sp_Load_Tramming_Booking] '201703', 'REA', '2017/03/12'
go
CREATE Procedure [dbo].[sp_Load_Tramming_Booking] 
@Prodmonth varchar(6),
@Section Varchar(50),
@Bookdate DateTime

as

select 
wp.Description XC, '' BH, [PLAN].Workplaceid,
[Plan].Tons, [Plan].HoppersPerShift, Units.Units,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Night, 0)) else isnull(Book_Comments.Night, '') end Night,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Morning, 0)) else isnull(Book_Comments.Morning, '') end Morning,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Afternoon, 0)) else isnull(Book_Comments.Afternoon, '') end Afternoon,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Night, 0)  + isnull(Book.Morning, 0) + isnull(Book.Afternoon, 0)) else '' end  Total
from (select a.Prodmonth, b.sectionid_2 SectionID,  BoxHoleID Workplaceid, Sum(Tons)Tons, 
Convert(int, round(Sum(Tons/totalshifts/3/H.TheFactor)+0.5,0)) Hopperspershift
from planmonth a inner join 
SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionid and a.PlanCode = 'MP'
inner join seccal c on b.prodmonth = c.prodmonth and b.Sectionid_1 = c.Sectionid, Code_Hoppers h
where a.prodmonth = @Prodmonth and b.sectionid_2 = @Section and (Boxholeid is not null or Boxholeid <> '')
group by a.prodmonth, b.sectionid_2, BoxHoleID
union
select a.Prodmonth, sectionid_2 SectionID, BoxHoleID Workplaceid, 
Sum(units*(ActualDepth/100)*1.67) Tons, 
Convert(int, round(Sum((units*(ActualDepth/100)*1.67)/totalshifts/3/H.TheFactor)+0.5,0)) Hopperspershift
from planmonth_oldgold a inner join 
SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionid
inner join seccal c on b.prodmonth = c.prodmonth and b.Sectionid_1 = c.Sectionid, Code_Hoppers h
where a.prodmonth = @Prodmonth and b.sectionid_2 = @Section and (Boxholeid is not null or Boxholeid <> '')
group by a.prodmonth, sectionid_2, BoxHoleID
union
select a.Prodmonth, sectionid_2 SectionID, BoxHoleID Workplaceid, Sum(units) Tons, 
Convert(int, round(Sum((units)/totalshifts/3/H.TheFactor)+0.5,0)) Hopperspershift
from PLANMONTH_SUNDRYMINING a inner join 
SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionid
inner join seccal c on b.prodmonth = c.prodmonth and b.Sectionid_1 = c.Sectionid
inner join SUNDRYMINING_TYPE s on a.SMID = s.SMID and s.UnitBase = 5, Code_Hoppers h
where a.prodmonth = @Prodmonth and sectionid_2 = @Section and (Boxholeid is not null or Boxholeid <> '')
group by a.prodmonth, sectionid_2, BoxHoleID) [Plan] inner join 
(select a.PRODMONTH, SECTIONID_2, NAME_2, CALENDARDATE, min(BeginDate) BEGINDATE,
max(ENDDATE) ENDDATE, max(WORKINGDAY) WORKINGDAY
from seccal a inner join SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionID_1
inner join CALTYPE c on a.CalendarCode = c.CalendarCode and
a.BeginDate <= c.CALENDARDATE and a.ENDDATE >= c.CALENDARDATE
Where a.prodmonth = @Prodmonth and SECTIONID_2 = @Section
group by a.PRODMONTH, SECTIONID_2, NAME_2, CALENDARDATE) Cal on
[Plan].Prodmonth = Cal.PRODMONTH and [Plan].Sectionid = Cal.SECTIONID_2
left join Book_Tramming Book on [Plan].prodmonth = Book.Prodmonth and [Plan].Sectionid = Book.sectionid and
[Plan].Workplaceid = Book.Workplaceid and cal.CALENDARDATE = Book.Bookdate
left join Book_Tramming_Comments Book_Comments on
[Plan].prodmonth = Book_Comments.Prodmonth and [Plan].Sectionid = Book_Comments.sectionid and
[Plan].Workplaceid = Book_Comments.Workplaceid and cal.CALENDARDATE = Book_Comments.Bookdate
left join WORKPLACE wp on [Plan].Workplaceid = wp.WORKPLACEID,
(select 1 Orderby, 'Hoppers' Units
union
select 2 Orderby, 'Comments') Units
where [Plan].prodmonth = @Prodmonth and Cal.SECTIONID_2 = @Section and Cal.CALENDARDATE = @Bookdate
and (Boxholeid is not null or Boxholeid <> '')
order by XC,BH, Orderby

GO

CREATE TABLE [dbo].[BOOKINGPROBLEM](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[PlanCode] [char](2) NOT NULL,
	[IsCubics] [char](1) NOT NULL,
	[ProblemID] [varchar](30) NULL,
	[ProblemTypeID] [varchar](5) NULL,
	[NoteID] [varchar](30) NULL,
	[SBNotes] [varchar](200) NULL,
	[CausedLostBlast] [varchar](1) NULL,
	[LostRevenue] [numeric](13, 3) NULL
) ON [PRIMARY]

GO

CREATE Procedure [dbo].[sp_Survey_DEV_Load] 
--Declare 
    @ProdMonth varchar(6),
    @SectionID VarChar(20)
as

--set @Prodmonth = 201702
--set @SectionID = 'REAAHDA'

Select p.WorkplaceID WorkplaceID, w.description as Workplace,
	convert(numeric(7,1), isnull(p.Metresadvance, 0)) PlanAdv,
	convert(numeric(7,1), isnull(pd.BookAdv, 0)) BookAdv,
	convert(numeric(7,1), isnull(s.Adv, 0)) MeasAdv,
	convert(numeric(7,2), isnull(s.Width, 0)) Width,
	convert(numeric(7,2), isnull(s.Height, 0)) Height,
	convert(numeric(7,0), isnull(s.Grade, 0)) Grade,
	convert(numeric(7,0), isnull(s.Cubics, 0)) Cubics,
	convert(numeric(7,0), isnull(s.Content, 0)) Content, 
	convert(numeric(7,0), isnull(s.Tons, 0)) Tons, 
	convert(numeric(7,3), isnull(s.KGs, 0)) KG,
	convert(numeric(7,3), isnull(s.GTs, 0)) GT,
	convert(numeric(7,3), isnull(s.RMs, 0)) RM
from PlanMonth p
inner join Section_Complete sc on
   sc.ProdMonth = p.ProdMonth and
   sc.SectionID = p.SectionID
inner join Workplace w on
  p.WorkplaceID = w.WorkplaceID
left outer join
	(select s.ProdMonth, s.WorkplaceID, s.SectionID, s.Activity,
			Adv = sum(Reefmetres + Wastemetres),
			Height = case when sum(Mainmetres) > 0 then sum(Mainmetres * MeasHeight) / sum(Mainmetres) else 0 end,
			Width = case when sum(Mainmetres) > 0 then sum(Mainmetres * MeasWidth) / sum(Mainmetres) else 0 end,
			Grade = case when sum(Mainmetres) > 0 then sum(Mainmetres * cmgt) / sum(Mainmetres) else 0 end,
			Cubics = sum(cubicsReef + CubicsWaste),
			Tons = SUM(s.TonsTotal),
			Content = SUM(s.TotalContent),
			KGs = SUM(s.TotalContent) / 1000,
			GTs = SUM(Convert(numeric(7,2), isnull(s.GT,0))),
			RMs = SUM(Convert(numeric(7,2), isnull(s.Reefmetres,0)))
	 from Survey s
	 where s.Prodmonth = @ProdMonth and
		   s.Activity = 1 and
		   s.SectionID = @SectionID
	 group by s.ProdMonth, s.WorkplaceID, s.SectionID, s.Activity
	 ) s on
	   p.ProdMonth = s.Prodmonth and
	   p.SectionID = s.SectionID and
	   p.Activity = s.Activity  and
	   p.WorkplaceID = s.WorkplaceID
left outer join
	(select ProdMonth, WorkplaceID, SectionID, Activity, PlanCode, IsCubics,
			BookAdv = sum(BookMetresadvance)
	 from Planning 
	 where Prodmonth = @ProdMonth and
		   Activity = 1 and
		   SectionID = @SectionID and
		   PlanCode = 'MP' and
		   IsCubics = 'N'
	 group by ProdMonth, WorkplaceID, SectionID, Activity, PlanCode, IsCubics) pd on
	   p.ProdMonth = pd.Prodmonth and
	   p.SectionID = pd.SectionID and
	   p.Activity = pd.Activity  and
	   p.WorkplaceID = pd.WorkplaceID and
	   p.PlanCode = pd.PlanCode and
	   p.IsCubics = pd.IsCubics
where p.SectionID = @SectionID and 
      p.activity = 1 and
      p.Prodmonth = @ProdMonth and
	  p.PlanCode = 'MP' and
	  p.IsCubics = 'N'
 
go




CREATE Procedure [dbo].[sp_Survey_STP_Load] 
--Declare 
    @ProdMonth varchar(6),
    @SectionID VarChar(20)
as

--set @Prodmonth = 201702
--set @SectionID = 'REAAHDA'

Select p.WorkplaceID WorkplaceID, w.Description as Workplace, 
   convert(numeric(7,0), p.Sqm) PlanSqm,  
   convert(numeric(7,0), pd.BookSqm) BookSqm,   
   convert(numeric(7,0), s.MeasSQM) MeasSqm,  
   convert(numeric(7,0), s.FL) FL,  
   convert(numeric(7,0), s.SW) SW, 
   convert(numeric(7,0), s.CW) CW,  
   convert(numeric(7,0), s.Grade) Cmgt,  
   convert(numeric(7,0), s.Tons) Tons,  
   convert(numeric(7,0), s.Grams) Content,  
   convert(numeric(7,0), s.Sweeps) Sweeps, 
   convert(numeric(7,0), s.Cubics) Cubics,  
   convert(numeric(7,0), s.OldTons) OldTons,  
   convert(numeric(7,0),  s.OldGrams) OldGrams  
from PlanMonth p 
inner join Workplace w on 
  p.WorkplaceID = w.WorkplaceID  
left outer join  
	(select s.ProdMonth, s.WorkplaceID, s.SectionID, s.Activity,
        MeasSQM = sum(MeasSQM),  
        FL = sum(FL),  
        SW = case when sum(MeasSQM) > 0 then sum(SQMSW) / sum(MeasSQM) else 0 end,  
        CW = case when sum(MeasSQM) > 0 then sum(SQMCW) / sum(MeasSQM) else 0 end,  
        Grade = case when sum(MeasSQM) > 0 then sum(Sqmcmgt) / sum(MeasSQM) else 0 end,  
        Tons = sum(Tons),  
        Grams = sum(Grams),  
        Sweeps = sum(Sweeps),  
        Cubics = sum(Cubics),  
        OldTons = sum(OldTons),  
        OldGrams = sum(OldGrams)  
	from 
		(select s.ProdMonth, s.WorkplaceID, s.SectionID, s.Activity,  
			MeasSQM = SqmTotal,  
			FL = FLTotal,  
			SQMSW = SqmTotal * SWSqm, 
			SQMCW = SqmTotal * CW, 
			Sqmcmgt = SqmTotal * cmgt,  
			Tons = TonsTotal,  
			Grams = TotalContent,  
			Sweeps = CleanSqm,  
			Cubics = CubicsReef + CubicsWaste, 
			OldTons =  case when s.Cleantype = 13 then s.CleanTons else 0 end,  
			OldGrams = case when s.Cleantype = 13 then s.CleanContents else 0 end 
		from Survey s 
		where s.Prodmonth = @ProdMonth and  
		   s.Activity IN(0,9) and  
		   s.SectionID = @SectionID
		) s  
	group by s.ProdMonth, s.WorkplaceID, s.SectionID, s.Activity 
	) s on  
	   p.ProdMonth = s.Prodmonth and  
	   p.SectionID = s.SectionID and  
	   p.WorkplaceID = s.WorkplaceID and
	   p.Activity = s.Activity 
	left outer join  
	 (select pd.ProdMonth, pd.WorkplaceID, pd.SectionID, pd.Activity, pd.PlanCode, pd.IsCubics, 
         BookSQM = sum(BookSqm), MeasSQM = SUM(SQM)  
	  from Planning pd  
	  where pd.Prodmonth = @ProdMonth and 
			pd.Activity IN(0,9) and 
			pd.SectionID = @SectionID 
	  group by pd.ProdMonth, pd.WorkplaceID, pd.SectionID, pd.Activity, pd.PlanCode, pd.IsCubics
	  ) pd on  
    p.ProdMonth = pd.Prodmonth and 
    p.SectionID = pd.SectionID and  
    p.WorkplaceID = pd.WorkplaceID and
	p.Activity = pd.Activity and
	p.PlanCode = pd.PlanCode and
	p.IsCubics = pd.IsCubics
where p.SectionID = @SectionID and   
      p.activity IN(0,9) and  
      p.Prodmonth = @ProdMonth and
	  p.PlanCode = 'MP' and
	  p.IsCubics = 'N'
go

alter table [SECTION]
add OpsPlanLink varchar(50) null,
EmployeeNo varchar(50) null
go

alter table PlanMonth
add SurveyAdded varchar(1) null
go

alter table siccapture
add SBNotes varchar(200) null,
CausedLostBlast varchar(1)
go
CREATE procedure [dbo].[sp_GetShiftMO]
@UserID varchar(20),
@Prodmonth varchar(10),
@SectionID varchar (20)
as
declare @BeginDate Datetime
declare @EndDate Datetime
declare @TheDate Datetime
declare @Calendarcode varchar(50)
declare @TheCount integer
declare @Calendardate Datetime
declare @Workday Char(1)
declare @TotalShifts varchar(2)


select 
  @BeginDate = begindate, @EndDate = enddate, 
  @Calendarcode = calendarcode, @TotalShifts = TotalShifts 
from Seccal p 
inner join SectionComplete sc on
  sc.Prodmonth = p.Prodmonth and
  sc.sbid = p.sectionid
where p.prodmonth = @Prodmonth and
      sc.MOID = @SectionID


set @TheDate = @Begindate
set @TheCount = 0
set @WorkDay = 'Y'
Declare Nonworkdays cursor
for select Calendardate from Caltype where calendarcode = @Calendarcode and 
calendardate >= @Begindate and calendardate <= @Enddate and workingday = 'N'
order by calendardate

OPEN Nonworkdays
FETCH NEXT FROM Nonworkdays
into @Calendardate
WHILE (@TheDate <= @EndDate)
BEGIN
if @Calendardate = @TheDate
begin 
	set @WorkDay = 'N'
end	
 if @Workday = 'Y' 
 begin
	set @TheCount = @TheCount + 1
 end
 insert into [TempWorkdaysMO] values (@UserID, @SectionID, @ProdMonth, @TheDate, @Workday, @TheCount, @TotalShifts)
 set @TheDate = @TheDate + 1
 set @WorkDay = 'Y'
 if @TheDate > @Calendardate 
begin
FETCH NEXT FROM Nonworkdays
 into @Calendardate
 END
End
 CLOSE Nonworkdays
 DEALLOCATE Nonworkdays


go


CREATE procedure [dbo].[sp_Insert_Zeros_PlanMonth] 
@Prodmonth varchar(10),
@SectionID varchar(20),
@WorkplaceID varchar(12),
@Activity varchar(1)

as

declare @TheProdMonth varchar(10)
declare @TheSectionID varchar(20)
declare @TheWorkplaceID varchar(12)
declare @TheActivity varchar(1)

set @TheProdMonth = @Prodmonth
set @TheSectionID = @SectionID
set @TheWorkplaceID = @WorkplaceID
set @TheActivity = @Activity

declare @TheEndDate varchar(10)
declare @TheShiftNo varchar(2)
declare @TheReefWaste varchar(1)
declare @TheWorkingDay varchar(1)

select @TheEndDate = convert(varchar(10),s.EndDate,120), 
	@TheShiftNo = s.TotalShifts,
	@TheWorkingDay = c.Workingday
from Seccal s
inner join Section_Complete sc on
  sc.Prodmonth = s.Prodmonth and
  sc.SectionID_1 = s.Sectionid
inner join CALTYPE c on
  c.CalendarCode = s.CalendarCode and
  c.CalendarDate = s.EndDate   
where  s.Prodmonth = @TheProdMonth and
       sc.Sectionid = @TheSectionID
       
select @TheReefWaste = ReefWaste from WORKPLACE 
where WorkplaceID = @TheWorkplaceID  

IF (@TheReefWaste = '')
BEGIN
  IF (@TheActivity = '0')
    set @TheReefWaste = 'R'
  ELSE
    set @TheReefWaste = 'W'  
END      
        
insert into PlanMonth
   (Prodmonth, Sectionid, Workplaceid, Activity, PlanCode, IsCubics,
	FL, Sqm, OrgUnitDay, OrgUnitAfternoon, OrgUnitNight,
	MetresAdvance, --Mnth1, Mnth2, Mnth3, OreFlowId,
	GT, SW, Density, Tons, KG, --Comments,
	--Cycle, 
	DWidth, DHeight, --DSLab, ASLab, NSLab,
	--Budget, PlanPrimM, PlanSecM, Cubics, OpeningUp,
	--Sweeps, ReSweeps, Vamps, ExtraBudget, 
	StartDate, --StartShift,
	ReefWaste, ReefSQM, WasteSQM, ReefTons, WasteTons,
	ReefAdv, WasteAdv, CW, CMGT, EndDate, SurveyAdded)
values
   (@TheProdMonth, @TheSectionID, @TheWorkplaceID, @TheActivity, 'MP','N',
    0, 0, '', '', '',						--begin line of FL
    0,-- 0, 0, 0, '',							--begin line of MetresAdvance
    0, 0, 0, 0, 0, --'',						--begin line of GT
    --'',										--begin line of Cycle
	0, 0, --0, 0, 0,						    --begin line of DWidth
    --0, 0, 0, 0, 0,							--begin line of Budget 
    --0, 0, 0, 0, 
	@TheEndDate, --@TheShiftNo,   --begin line of Sweeps
    @TheReefWaste, 0, 0, 0, 0,				--begin line of ReefWaste
    0, 0, 0, 0, @TheEndDate, 'Y')					--begin line of Advreef
    
insert into PLANNING
   (Prodmonth, WorkplaceID, SectionID, Activity, PlanCode, IsCubics,
	CalendarDate, ShiftDay, --WorkingDay,
	Sqm, Metresadvance,-- OrgUnitDS, OrgUnitAS, OrgUnitNS,
	Grams, Tons, ABSPicNo,
	BookCode, BookSqm, BookMetresadvance, BookTons,
	BookGrams, BookFL, Bookcmgt, BookSW,
	PegID, PegToFace, PegDist,
	--BookHeight, BookWidth, BookProb,
	--MSBlast, MSClean, MSPrep, ASBlast, ASClean, ASPrep, NSBlast, NSClean, NSPrep,
	--DailyAdv, MSFL, ASFL, NSFL,
	--BookPrimM, BookSecM, PlanPrimM, PlanSecM, MoFc, AdjSqm, Pumahola,
	----BookCubics, BookSweeps, BookReSweeps, BookVamps, BookOpenUp,
	--CheckSqm, AdjCont, AdjTons, CheckMeasProb, ABSCode, ABSNotes ,ExplosDets, Explosives, Explos1,
	--ReefWaste, 
	ReefSQM, WasteSQM, ReefTons, WasteTons, ReefAdv, WasteAdv,
	BookReefSQM, BookWasteSQM, BookReefTons, BookWasteTons,-- MarkedForBlast,
	Cubics)
values
   (@TheProdMonth, @TheWorkplaceID, @TheSectionID, @TheActivity, 'MP', 'N',
    @TheEndDate, @TheShiftNo, --@TheWorkingDay,
    0, 0,-- '', '', '',  --sqm
	0, 0, '', --range
	'', 0, 0, 0, --BookCode
	0, 0, 0, 0,  --BookGrams
	'', 0, 0,  --PegID
	--0, 0, '', --BookHeight
	--'', '', '', '', '', '', '', '', '', --MSBlast
	--0, 0, 0, 0,  --DailyAdv
	--0, 0, 0, 0, 0, 0, '', --BookPrimM
	--0, 0, 0, 0, 0, --BookCubics
	--0, 0, 0, '', '', '', 0, 0, 0, --checksum
	--@TheReefWaste, 
	0, 0, 0, 0, 0, 0,  --Reefwaste
	0, 0, 0, 0, --0,--BookSQMReef
	 0) --Cubics
go
CREATE Procedure [dbo].[sp_Survey_DEV_Load_Detail] 
--Declare 
    @ProdMonth varchar(6),
    @SectionID VarChar(20),
	@WorkplaceID VarChar(12),
	@SeqNo VarChar(7)
as

--set @Prodmonth = 201701
--set @SectionID = 'RECAHGA'
--set @WorkplaceID = 'RE010043'
--set @SeqNo = '1'


select CalendarDate,  
   Dip = Convert(numeric(7,0), isnull(Dip,0)),  
   BallDepth = Convert(numeric(7,1), isnull(BallDepth,0)),  
   MineMethod = Convert(numeric(7,0), isnull(MineMethod,-1)),  
   Density = Convert(numeric(7,2), isnull(Density,-1)),  
   Indicator = Convert(numeric(7,0), isnull(Indicator,-1)),  
   CrewMorning = CrewMorning,  
   CrewAfternoon = CrewAfternoon,  
   CrewEvening = CrewEvening,  
   CleaningCrew = CleaningCrew,  
   TrammingCrew = TrammingCrew,  
   HlgeMaintainanceCrew = HlgeMaintainanceCrew,  
   RiggerCrew = RiggerCrew,  
   RseCleaningCrew = RseCleaningCrew,  
   PegNo PegNo,  
   PegValue = Convert(numeric(7,1), isnull(PegValue,0)),  
   PegToFace = Convert(numeric(7,1), isnull(PegToFace,0)),  
   ProgFrom = Convert(numeric(7,1), isnull(ProgFrom,0)),  
   ProgTo = Convert(numeric(7,1), isnull(ProgTo,0)),  
   Mainmetres = Convert(numeric(7,1), isnull(Mainmetres,0)),  
   MeasWidth = Convert(numeric(7,2), isnull(MeasWidth,0)),  
   MeasHeight = Convert(numeric(7,2), isnull(MeasHeight,0)),  
   PlanWidth = Convert(numeric(7,2), isnull(PlanWidth,0)),  
   PlanHeight = Convert(numeric(7,2), isnull(PlanHeight,0)),  
   Reefmetres = Convert(numeric(7,1), isnull(Reefmetres,0)),  
   Wastemetres = Convert(numeric(7,1), isnull(Wastemetres,0)),  
   Totalmetres = Convert(numeric(7,1), isnull(Totalmetres,0)),  
   Labour = Convert(numeric(7,0), isnull(Labour,0)),  
   PaidUnpaid = isnull(PaidUnpaid,'N'),  
   CW = Convert(numeric(7,0), isnull(CW,0)),  
   ValHeight = Convert(numeric(7,0), isnull(ValHeight,0)),  
   GT = Convert(numeric(7,2), isnull(GT,0)),  
   cmgt = Convert(numeric(7,0), isnull(cmgt,0)),  
   ExtraType = Convert(numeric(7,0), isnull(ExtraType,0)),  
   Cubicsmetres = Convert(numeric(7,1), isnull(Cubicsmetres,-1)),  
   CubicsReef = Convert(numeric(7,0), isnull(CubicsReef,0)),  
   CubicsWaste = Convert(numeric(7,0), isnull(CubicsWaste,0)),  
   OpenUpSqm = Convert(numeric(7,0), isnull(OpenUpSqm,0)),  
   ReefDevSqm = Convert(numeric(7,0), isnull(ReefDevSqm,0)),  
   OpenUpcmgt = Convert(numeric(7,0), isnull(OpenUpcmgt,0)),  
   ReefDevcmgt = Convert(numeric(7,0), isnull(ReefDevcmgt,0)),  
   OpenUpFL = Convert(numeric(7,0), isnull(OpenUpFL,0)),  
   ReefDevFL = Convert(numeric(7,0), isnull(ReefDevFL,0)),  
   OpenUpEquip = Convert(numeric(7,0), isnull(OpenUpEquip,0)),  
   ReefDevEquip = Convert(numeric(7,0), isnull(ReefDevEquip,0)),  
   TonsWasteBroken = Convert(numeric(7,2), isnull(TonsWasteBroken,0)),  
   TonsReefBroken = Convert(numeric(7,2), isnull(TonsReefBroken,0)),  
   TonsCubicsWaste = Convert(numeric(7,2), isnull(TonsCubicsWaste,0)),  
   TonsCubicsReef = Convert(numeric(7,2), isnull(TonsCubicsReef,0)),  
   TonsReef = Convert(numeric(7,2), isnull(TonsReef,0)),  
   TonsWaste = Convert(numeric(7,2), isnull(TonsWaste,0)),  
   TonsTotal = Convert(numeric(7,2), isnull(TonsTotal,0)),  
   TonsTrammed = Convert(numeric(7,2), isnull(TonsTrammed,0)),  
   TonsBallast = Convert(numeric(7,2), isnull(TonsBallast,0)),  
   Destination = Convert(numeric(7,0), isnull(Destination,-1)),  
   OreFlowID = OreFlowID,  
   Payment = Convert(numeric(7,0), isnull(Payment,-1)),  
   PlanNo = isnull(PlanNo,''),  
   CleanTypeID = Convert(numeric(7,0), isnull(Cleantype,-1)),  
   CleanSqm = Convert(numeric(7,0), isnull(CleanSqm,0)),  
   CleanDist = Convert(numeric(7,1), isnull(CleanDist,0)),  
   CleanVamp = Convert(numeric(7,1), isnull(CleanVamp,0)),  
   CleanTons = Convert(numeric(7,0), isnull(CleanTons,0)),  
   Cleangt = Convert(numeric(7,2), isnull(Cleangt,0)),  
   CleanContents = Convert(numeric(7,0), isnull(CleanContents,0))  
 from Survey  
 where ProdMonth = '201701'and   
       SECTIONID = 'RECAHGA' and  
       WorkplaceID = 'RE010043' and  
       Activity = 1  and  
       SeqNo = '1'  
 order by SeqNo Desc  
 go

 CREATE Procedure [dbo].[sp_Survey_STP_Load_Detail] 
--Declare 
    @ProdMonth varchar(6),
    @SectionID VarChar(20),
	@WorkplaceID VarChar(12),
	@SeqNo VarChar(7)
as

--set @Prodmonth = 201701
--set @SectionID = 'REAAHDA'
--set @WorkplaceID = 'RE008853'
--set @SeqNo = '1'

select   
	CalendarDate,   
	Dip = Convert(numeric(7,0), isnull(Dip,0)),   
	MineMethod = Convert(numeric(7,0), isnull(MineMethod,-1)),   
	Density = Convert(numeric(7,2), isnull(Density,0)),   
	StopeTypeID = Convert(numeric(7,0), isnull(SType,-1)),   
	CrewMorning = CrewMorning,   
	CrewAfternoon = CrewAfternoon,   
	CrewEvening =  CrewEvening,   
	CleaningCrew = CleaningCrew,   
	TrammingCrew = TrammingCrew,   
	HlgeMaintainanceCrew = HlgeMaintainanceCrew,   
	RiggerCrew = RiggerCrew,   
	RseCleaningCrew = RseCleaningCrew,   
	Reefmetres = Convert(numeric(7,1), isnull(Reefmetres,0)),   
	Wastemetres = Convert(numeric(7,1), isnull(Wastemetres,0)),   
	MeasWidth = Convert(numeric(7,2), isnull(MeasWidth,0)),   
	MeasHeight = Convert(numeric(7,2), isnull(MeasHeight,0)),   
	PlanWidth = Convert(numeric(7,2), isnull(PlanWidth,0)),   
	PlanHeight = Convert(numeric(7,2), isnull(PlanHeight,0)),   
	LockUpTons = Convert(numeric(7,0), isnull(LockUpTons,0)),   
	Blockno = isnull(Blockno,''),   
	BlockWidth = Convert(numeric(7,0), isnull(BlockWidth,0)),   
	BlockValue = Convert(numeric(7,0), isnull(BlockValue,0)),   
	StopeSqm = Convert(numeric(7,0), isnull(StopeSqm,0)),   
	StopeSqmOSF = Convert(numeric(7,0), isnull(StopeSqmOSF,0)),   
	StopeSqmOS = Convert(numeric(7,0), isnull(StopeSqmOS,0)),   
	StopeSqmTotal = Convert(numeric(7,0), isnull(StopeSqmTotal,0)),   
	LedgeSqm  = Convert(numeric(7,0), isnull(LedgeSqm,0)),   
	LedgeSqmOSF = Convert(numeric(7,0), isnull(LedgeSqmOSF,0)),   
	LedgeSqmOS  = Convert(numeric(7,0), isnull(LedgeSqmOS,0)),   
	LedgeSqmTotal = Convert(numeric(7,0), isnull(LedgeSqmTotal,0)),   
	StopeFL = Convert(numeric(7,0), isnull(StopeFL,0)),   
	StopeFLOS = Convert(numeric(7,0), isnull(StopeFLOS,0)),   
	LedgeFL = Convert(numeric(7,0), isnull(LedgeFL,0)),   
	LedgeFLOS = Convert(numeric(7,0), isnull(LedgeFLOS,0)),   
	FLOSTotal = Convert(numeric(7,0), isnull(FLOSTotal,0)),   
	MeasAdvSW = Convert(numeric(7,1), isnull(MeasAdvSW,0)),   
	SWIdeal = Convert(numeric(7,0), isnull(SWIdeal,0)),  
	FLTotal = Convert(numeric(7,0), isnull(FLTotal,0)),   
	SqmConvTotal = Convert(numeric(7,0), isnull(SqmConvTotal,0)),   
	SqmOSFTotal = Convert(numeric(7,0), isnull(SqmOSFTotal,0)),   
	SqmOSTotal = Convert(numeric(7,0), isnull(SqmOSTotal,0)),   
	SqmTotal = Convert(numeric(7,0), isnull(SqmTotal,0)),   
	ExtraType = Convert(numeric(7,0), isnull(ExtraType,-1)),   
	Cubicsmetres = Convert(numeric(7,1), isnull(Cubicsmetres,0)),   
	CubicsReef = Convert(numeric(7,0), isnull(CubicsReef,0)),   
	CubicsWaste = Convert(numeric(7,0), isnull(CubicsWaste,0)),   
	Labour = Convert(numeric(7,0), isnull(Labour,0)),   
	PaidUnpaid = isnull(PaidUnpaid,'N'),   
	FW = Convert(numeric(7,0), isnull(FW,0)),   
	CW = Convert(numeric(7,0), isnull(CW,0)),   
	HW = Convert(numeric(7,0), isnull(HW,0)),   
	SWSqm = Convert(numeric(7,0), isnull(SWSqm,0)),   
	SWOSF = Convert(numeric(7,0), isnull(SWOSF,0)),   
	SWOS = Convert(numeric(7,0), isnull(SWOS,0)),   
	cmgt = Convert(numeric(7,0), isnull(cmgt,0)),   
	Destination = Convert(numeric(7,0), isnull(Destination,-1)),   
	OreFlowID = OreFlowID,   
	CleanTypeID = Convert(numeric(7,0), isnull(Cleantype,-1)),   
	CleanSqm = Convert(numeric(7,0), isnull(CleanSqm,0)),   
	CleanDist = Convert(numeric(7,1), isnull(CleanDist,0)),   
	CleanVamp = Convert(numeric(7,1), isnull(CleanVamp,0)),   
	CleanTons = Convert(numeric(7,0), isnull(CleanTons,0)),   
	Cleangt = Convert(numeric(7,2), isnull(Cleangt,0)),   
	CleanContents = Convert(numeric(7,0), isnull(CleanContents,0)),   
	TonsReef = Convert(numeric(7,2), isnull(TonsReef,0)),   
	TonsWaste = Convert(numeric(7,2), isnull(TonsWaste,0)),   
	TonsOSF = Convert(numeric(7,2), isnull(TonsOSF,0)),   
	TonsOS = Convert(numeric(7,2), isnull(TonsOS,0)),   
	TonsTotal = Convert(numeric(7,2), isnull(TonsTotal,0)),   
	TotalContent = Convert(numeric(13,0), isnull(TotalContent,0)),   
	CleanDepth = Convert(numeric(13,0), isnull(CleanDepth,0)),   
	Payment = Convert(numeric(7,0), isnull(Payment,-1)),   
	PlanNo = isnull(PlanNo,'') 
from Survey 
where ProdMonth = @ProdMonth and        
	  SECTIONID = @SectionID and       
	  WorkplaceID = @WorkplaceID and       
	  Activity IN(0,9)  and       
	  SeqNo = @SeqNo 
order by SeqNo Desc 

go
CREATE TABLE [dbo].[TempBusPlanImport](
	[WorkplaceID] [varchar](max) NULL,
	[SectionID] [varchar](max) NULL,
	[Prodmonth] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


CREATE TABLE [dbo].[TempWorkdaysMO](
	[UserID] [varchar](20) NOT NULL,
	[SectionID] [varchar](20) NOT NULL,
	[ProdMonth] [varchar](10) NOT NULL,
	[CalendarDate] [datetime] NOT NULL,
	[Workday] [char](1) NULL,
	[Shift] [int] NULL,
	[Totalshifts] [int] NULL
) ON [PRIMARY]

GO

Alter table Planning add [SBossNotes] [varchar](200) NULL,
	[CausedLostBlast] [char](1) NULL


go
 CREATE Procedure [dbo].[sp_Load_BookABSDevelopment_Detail] 
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
		@BookDate varchar(10)
as

--set @Prodmonth = 201703
--set @SectionID = 'RECA'
--set @BookDate = '2017-03-14'


Declare @SQL VarChar(8000)
Declare @SQL1 VarChar(8000)

set @SQL = ' 
 select * from 
  ( 
     select pm.ProdMonth, pm.sectionid Sec, pm.sectionid SectionID, 
            pm.workplaceid WPID, w.Description, 
            pm.workplaceid + '':'' + w.Description WP,  
            1 Activity, ''Development'' ActDesc, ShiftDay, 
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
           isnull(pd.ABSNotes, '''') ABSNotes, 
           --isnull(pd.PegID, '''') PegID, 
		   PegID = pd.PegID, -- +'':''+convert(varchar(10), cast(pg.Value as numeric(10,1))),
		   isnull(pd.PegDist, 0) PegDist, 
           cast(isnull(pd.PegToFace, 0) as numeric(10,1)) PegToFace, 
		  -- isnull(pd.PegToFace, 0) PegToFaceOld, 
		   PPegID = case when isnull(c.ppeg,'''') = '''' then (case when isnull(sss.ppeg,'''') ='''' or (sss.cal < e.cal) then 
		                     e.ppeg else sss.ppeg end) else c.ppeg end ,  
			PPegToFace = case when c.ppegtoface is null then (case when sss.ppegtoface is null or (sss.cal < e.cal) then 
							e.ppegtoface else sss.ppegtoface end) else c.ppegtoface end,
			PPegDist = case when c.ppegdist is null then (case when sss.ppegdist is null or (sss.cal < e.cal) then 
							e.ppegdist else sss.ppegdist end)  else c.ppegdist end,
			--PegFrom = case when c.ppegdist is null then (case when sss.ppegdist is null or (sss.cal < e.cal) then 
			--				e.ppegdist else sss.ppegdist end)  else c.ppegdist end,
			--PegTo = case when c.ppegdist is null then (case when sss.ppegdist is null or (sss.cal < e.cal) then 
			--				convert(e.ppegdist + e.ppeg else sss.ppegdist+sss.ppeg end)  else c.ppegdist + c.ppeg end,
           isnull(pd.BookMetresAdvance, 0) BookAdv, isnull(pd.BookTons, 0) BookTons,
           isnull(pd.BookGrams, 0) BookGrams, BookKG = isnull(pd.BookGrams, 0) / 1000, 
          -- isnull(pd.BookCubics, 0) BookCubics, isnull(pd.BookSweeps, 0) BookSweeps, 
          -- isnull(pd.BookResweeps, 0) BookReSweeps, isnull(pd.BookVamps, 0) BookVamps, 
           --BookTotal = isnull(pd.BookMetresAdvance, 0) + isnull(pd.BookSecM, 0), 
          -- isnull(pd.BookOpenUp, 0) BookOpenUp, isnull(pd.BookSecM, 0) BookSecM, 
           isnull(pd.SW, 0) DHeight, isnull(pd.FL, 0) DWidth, isnull(ss.RockDensity, 0) Density, 
           isnull(pm.GT,0) gt, 
           BookCodeDev = case when prbook.ProblemID = ''ST'' then prbook.ProblemID else isnull(pd.BookCode,'''') end, 
           isnull(p.ProblemID, '''') NoteID, isnull(p.SBossNotes, '''') SBNotes 
      from planmonth pm 
      inner join planning pd on 
        pm.Prodmonth = pd.Prodmonth and 
        pm.SectionID = pd.SectionID and 
        pm.WorkplaceID = pd.WorkplaceID and 
        pm.Activity = pd.Activity and 
        pm.PlanCode = pd.PlanCode and
		pm.IsCubics = pd.IsCubics
	left outer join 

		(select a.wp1, b.bookcode prevcode,  b.pegid ppeg, b.pegtoface ppegtoface, b.pegdist ppegdist, cal 
		from 
			(select p.workplaceid wp1,  max(calendardate) cal 
			from planning p 
			where calendardate < '''+@BookDate+''' and prodmonth = '''+@Prodmonth+''' and isCubics = ''N'' and PlanCode=''MP''
			   and p.activity in (1) and bookmetresadvance is not null group by p.workplaceid
			) a 
			left outer join 
				(select workplaceid , max(bookcode)  bookcode,  max(pegid)  pegid, max(pegtoface) pegtoface, max(pegdist) pegdist,  calendardate 
				from planning 
				where bookmetresadvance is not null and activity in (1) and isCubics = ''N'' and PlanCode=''MP''
				group by workplaceid, calendardate
				) b on a.wp1 = b.workplaceid and a.cal = b.calendardate 
		) c on pm.WorkplaceID = c.wp1 
 
		left outer join  
		 (select a.wp1,  b.pegid + '':'' + convert(varchar(10),b.pegvalue) ppeg, b.pegtoface ppegtoface, b.pegdist ppegdist, cal 
		 from    
			(select p.workplaceid wp1,  max(calendardate) cal	  
			from survey p 
			where prodmonth < '''+@Prodmonth+''' and p.activity in (1) and mainmetres is not null 
			group by p.workplaceid
			) a    
		 left outer join    
			(select workplaceid ,  max(pegno)  pegid, max(pegvalue) pegvalue, max(pegtoface) pegtoface, max(progto) pegdist, calendardate    
			from survey
			where mainmetres is not null and activity in (1) 
			group by workplaceid, calendardate
			) b  on a.wp1 = b.workplaceid and a.cal = b.calendardate
		) sss on pm.WorkplaceID = sss.wp1  '
set @SQL1 = ' 

		left outer join  
			(select a.wp1, b.bookcode prevcode,  b.pegid ppeg, b.pegtoface ppegtoface, b.pegdist ppegdist, cal 
			from 
				(select p.workplaceid wp1,  max(calendardate) cal  
				 from planning p 
				 where calendardate < '''+@BookDate+''' and p.activity in (1) and (bookmetresadvance > 0 or bookmetresadvance < 0) 
				 group by p.workplaceid
				) a    
			left outer join    
				(select workplaceid , max(bookcode)  bookcode,  max(pegid)  pegid, max(pegtoface) pegtoface, max(pegdist) pegdist, calendardate    
				from planning 
				where bookcode is not null and activity in (1) 
				group by workplaceid, calendardate
				) b on a.wp1 = b.workplaceid and a.cal = b.calendardate 
		) e on pm.WorkplaceID = e.wp1  
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
	  left outer join Peg pg on
	    pg.PegID = pd.PegID and
		pg.WorkplaceID = pd.WorkplaceID
      left outer join PROBLEMBOOK p on 
        pm.Prodmonth = p.Prodmonth and 
        pm.SectionID = p.SectionID and 
        pm.WorkplaceID = p.WorkplaceID and 
        pm.Activity = p.Activity and 
        pm.PlanCode = p.PlanCode and 
		pm.IsCubics = p.IsCubics and
        pd.Calendardate = p.CalendarDate 
      left outer join 
       ( 
             select b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics, 
                max(CalendarDate) CalendarDate 
             from PROBLEMBOOK b 
             where ProdMonth = '''+@Prodmonth+''' and 
                   Activity = 1 and 
                   CalendarDate < '''+@BookDate+''' and 
                   PlanCode = ''MP'' and
				   IsCubics = ''N''
             group by b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics
       ) prbook1 on 
         pm.Prodmonth = prbook1.Prodmonth and 
         pm.SectionID = prbook1.SectionID and 
         pm.WorkplaceID = prbook1.WorkplaceID and 
         pm.Activity = prbook1.Activity and 
         pm.PlanCode = prbook1.PlanCode and
		 pm.IsCubics = prbook1.IsCubics
       left outer join ProblemBook prbook on 
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
        aa.WorkplaceID = p.WorkplaceID and 
        aa.CalendarDate = a.calendarDate 
      left outer join 
          (select p.ProdMonth, WorkplaceID, p.SectionID, Activity, 
                  PlanCode, IsCubics, sum(BookSqm) progbook, sum(AdjSqm) AdjSqm 
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
			 pm.IsCubics = ProgSum.IsCubics, Sysset ss
      where pm.ProdMonth = '''+@Prodmonth+''' and 
            pm.Activity = 1 and 
            pm.PlanCode = ''MP'' and 
			pm.IsCubics = ''N'' and 
            pd.CalendarDate = '''+@BookDate+''' and 
            pm.Metresadvance > 0 and 
            ct.WorkingDay = ''Y'' and 
            sc.SectionID_1 = '''+@SectionID+''' 
            --(p.Pumahola = ''Y'' or ct.WorkingDay = ''Y'')) a 
  ) z '

  --print (@SQL)
  -- print (@SQL1)
exec (@SQL+@SQL1)

go

CREATE Procedure [dbo].[sp_Load_BookABSDevelopment]
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
        @DaysBackdate int,
        @Shift int
as

--set @Prodmonth = 201703
--set @SectionID = 'RECA'
--Set @DaysBackdate = 0
--set @Shift = 3


Declare @SQL VarChar(8000),
 @Backdate DateTime


 --Select @TheshiftTime

--@SQL1 VarChar(8000), -- Forecast and Cleaning Bookings
--@SQL2 VarChar(8000)
select @backdate = Rundate - @DaysBackdate  From sysset 

set @SQL = ' select * from 
	(select sc.Name_1 SBName, sc.SectionID_2, sc.Name Name, pm.SectionID, 
	pm.Workplaceid+'' : ''+w.Description Workplace,
	pm.Activity, 
	ShiftDay = case when ct.WorkingDay = ''Y'' then convert(varchar(3), p.ShiftDay) else ''N'' end,
	isnull(ct.WorkingDay,'''') WorkingDay,
	ct.CalendarDate CalendarDate,  
	''Book'' BookType,
	isnull(pr.ProblemID,'''') NoteID,
	Type = z.[Type],
	ABSCode = isnull(p.AbsCode,''''),
	MonthAdv = case when z.[Type] = ''Plan'' then convert(varchar(10), cast(pm.MetresAdvance as numeric(7,0))) else '''' end,
	ProgAdv = case when z.[Type] = ''Plan'' then 
					convert(varchar(10), cast(pp.ProgPlanAdv as numeric(7,0))) else 
					convert(varchar(10), cast(pp.ProgBookAdv as numeric(7,0))) end,
	theVal = case 
		  when z.[Type] = ''Plan'' and isnull(p.MetresAdvance,0) = 0 then ''''
		  when z.[Type] = ''Plan'' and isnull(p.MetresAdvance,0) > 0 then convert(varchar(10), convert(Numeric(7,1),isnull(p.MetresAdvance,0)))
		  when z.[Type] = ''Book'' and isnull(pr.ProblemID, '''') <> '''' then isnull(pr.ProblemID,'''')
		  when z.[Type] = ''Book'' and isnull(p.BookMetresAdvance,0) = 0 then '''' 
		  when z.[Type] = ''Book'' and isnull(p.BookMetresAdvance,0) > 0 then
		convert(varchar(10), convert(Numeric(7,1),isnull(p.BookMetresAdvance,0))) else ''''
	end,
	theValue = case 
		  when z.[Type] = ''Plan'' then convert(Numeric(7,1),isnull(p.MetresAdvance,0))
		  when z.[Type] = ''Book'' then convert(Numeric(7,1),isnull(p.BookMetresAdvance,0)) else 0 end 	
	from planmonth pm	
	inner join Section_Complete sc on 
	  pm.SectionID = sc.SectionID and
	  pm.ProdMonth = sc.ProdMonth
	inner join Seccal s on
	  sc.ProdMonth = s.ProdMonth and
	  sc.SectionID_1 = s.SectionID
	inner join CalType ct on
	  s.CalendarCode = ct.CalendarCode and
	  s.BeginDate <= ct.CalendarDate and
	  s.enddate >= ct.CalendarDate
	inner join Planning p on
	  p.ProdMonth = pm.ProdMonth and 
	  p.SectionID = pm.SectionID and
	  p.WorkplaceID = pm.WorkplaceID and
	  p.Activity = pm.Activity and
	  p.IsCubics = pm.IsCubics and
	  p.PlanCode = pm.PlanCode and
	  p.IsCubics = pm.IsCubics and
	  p.Calendardate = ct.CalendarDate
	left outer join
		(select ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode, sum(isnull(BookMetresAdvance,0)) ProgBookAdv,
		 sum(isnull(MetresAdvance,0)) ProgPlanAdv
		 from Planning, Sysset
		 where CalendarDate <= SYSSET.RUNDATE and
		 PlanCode = ''MP'' and
		 Activity = 1 and
		 IsCubics = ''N''
		 group by ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode
		) pp on
	  pp.ProdMonth = pm.ProdMonth and 
	  pp.SectionID = pm.SectionID and
	  pp.WorkplaceID = pm.WorkplaceID and
	  pp.Activity = pm.Activity and
	  pp.IsCubics = pm.IsCubics and
	  pp.PlanCode = pm.PlanCode
	left outer join BOOKINGPROBLEM pr on 
        pm.Prodmonth = pr.Prodmonth and 
        pm.SectionID = pr.SectionID and 
        pm.WorkplaceID = pr.WorkplaceID and 
        pm.Activity = pr.Activity and 
        pm.PlanCode = pr.PlanCode and
		pm.IsCubics = pm.IsCubics and
        p.Calendardate = pr.CalendarDate 
	inner join Workplace W on
	  pm.WorkplaceID = w.WorkplaceID, SYSSET ss,
	(select [Order] = 1,
	Type = ''Plan''
	union
	select [Order] = 2,
	Type = ''Book''
	) z
	where pm.prodmonth = '''+ @Prodmonth +''' and sc.SectionID_1= '''+@SectionID+''' 
	and pm.Activity = 1 and pm.tons > 0 and pm.PlanCode = ''MP'' and pm.IsCubics = ''N''
	and ct.calendardate <= ss.rundate ) a 
		order by SectionID_2, SectionID, Workplace,CalendarDate ' 


--print (@SQL)
exec (@SQL)

go

CREATE Procedure [dbo].[sp_SICCapture_Load] 
--Declare 
	@TypeMonth varchar(20),
    @ProdMonth varchar(6),
    @SectionID VarChar(20),
	@SectionLevel varchar(1),
	@KPI varchar(50)
as

--set @TypeMonth = 'Production'
--set @Prodmonth = 201703
--set @SectionID = 'RECA'
--set @SectionLevel = 5  --5 for production and Cleaning
--set @KPI = 'Cleaned'

declare @StartDate varchar(10)
declare @EndDate varchar(10)
declare @Diffs varchar(10)
declare @TotalShifts int
declare @CalendarCode varchar(20)

declare @SelectID varchar(20)

declare @RunDate varchar(10)
select @RunDate =  convert(varchar(10), RunDate, 120) from SYSSET

declare @HierID int
select @HierID = MOHierarchicalID from Sysset
IF (@HierID = @SectionLevel)
	 set @SelectID = 'SectionID_2'
ELSE
	set @SelectID = 'SectionID_1'

IF (@TypeMonth = 'Safety') or
   (@TypeMonth = 'Costing')
BEGIN
	select @StartDate = convert(varchar(10),StartDate,120), 
		@EndDate = convert(varchar(10),EndDate,120), 
		@Diffs = datediff(d,Startdate,enddate) + 1, 
		@TotalShifts = convert(varchar(2),TotalShifts) 
	from CalendarOther 
	where [Month] = @ProdMonth and
		  CalendarCode = @TypeMonth
END
IF (@TypeMonth = 'Production')
BEGIN
	IF (@HierID = @SectionLevel)
	BEGIN
		select top 1 @StartDate = convert(varchar(10),s.BeginDate,120), 
			@EndDate = convert(varchar(10),s.EndDate,120), 
			@diffs = datediff(d,s.BeginDate,s.EndDate) + 1, 
			@TotalShifts = s.TotalShifts, 
			@CalendarCode = s.CalendarCode 
		from SECCAL s 
		inner join Section_Complete sc on 
			sc.ProdMonth = s.Prodmonth and 
			sc.SectionID_1 = s.SectionID 
		where s.ProdMonth = @ProdMonth and
				sc.SectionID_2 = @SectionID
	END
	ELSE
	BEGIN
		select top 1 @StartDate = convert(varchar(10),s.BeginDate,120), 
			@EndDate = convert(varchar(10),s.EndDate,120), 
			@diffs = datediff(d,s.BeginDate,s.EndDate) + 1, 
			@TotalShifts = s.TotalShifts, 
			@CalendarCode = s.CalendarCode 
		from SECCAL s 
		inner join Section_Complete sc on 
			sc.ProdMonth = s.Prodmonth and 
			sc.SectionID_1 = s.SectionID 
		where s.ProdMonth = @ProdMonth and
				sc.SectionID_1 = @SectionID
	END
END
IF (@TypeMonth = 'Mill')
BEGIN
	select @StartDate = convert(varchar(10),a.StartDate,120), 
		@EndDate = convert(varchar(10),a.EndDate,120), 
		@diffs = datediff(d,a.Startdate,a.enddate) + 1, 
		@TotalShifts = a.TotalShifts 
	from 
    (
		SELECT DISTINCT StartDate, EndDate, TotalShifts 
		from CALENDARMILL 
		where MillMonth = @ProdMonth and 
			  CalendarCode = @TypeMonth
	) a 
END

declare @TheSICKey int 
declare @TheKPI varchar(30) 
declare @TheDescription varchar(50)

IF (@KPI <> 'Cleaned')
BEGIN
	--delete  from Linda
	if object_id('#tempSic') is not null
   		drop table #tempSic
	CREATE TABLE #tempSic(SICKey int, KPI varchar(30) , Description VARCHAR(50), 
							CalendarDate DateTime, ShiftNo varchar(2), TotalShifts varchar(2), 
							WorkingDay varchar(1), TheValue numeric(11,4), ProgValue numeric(11,4), ShiftValue decimal)

	DECLARE AA_Cursor CURSOR FOR
	select SICKey, KPI, Description from Code_SICCapture where KPI = @KPI

	OPEN AA_Cursor;
	FETCH NEXT FROM AA_Cursor
	into @TheSICKey, @TheKPI, @TheDescription;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		insert into #tempSic  --Linda
		select @TheSICKey, @TheKPI, @TheDescription, 
		ct.CalendarDate,
		ShiftNo = (select count(calendarDate) ShiftNo from Caltype where CalendarCode = @TypeMonth and
				   CalendarDate >= @StartDate and CalendarDate <= ct.CalendarDate and
				   WorkingDay = 'Y'),
		cc.TotalShifts, ct.WorkingDay,  
		TheValue = isnull(s.[Value],0), 0, 0
		from CalType ct
		left outer join SicCapture s on
		   s.CalendarDate = ct.CalendarDate and
		   s.SicKey = @TheSICKey and
		   s.SectionID = @SectionID
		left outer join 
			(select CalendarCode, count(CalendarDate) TotalShifts from CalType
			 where CalendarCode = @TypeMonth and
				   CalendarDate >= @StartDate and
				   CalendarDate <= @EndDate
			 group by CalendarCode) cc on
			ct.CalendarCode = cc.CalendarCode
		where ct.CalendarCode = @TypeMonth and
			@StartDate <= ct.CalendarDate and
			@EndDate >= ct.CalendarDate
		FETCH NEXT FROM AA_Cursor
		 into @TheSICKey, @TheKPI, @TheDescription;
	END; 
	CLOSE AA_Cursor;
	DEALLOCATE AA_Cursor;

	update #tempSic  --Linda
	set ProgValue =
	(select sum(Value) sumValue from SICCAPTURE where
	  SectionID = @SectionID and
	  CalendarDate >= @StartDate and
	  CalendarDate <= @EndDate and
	  SICKey = #tempSic.SICKey)  --linda.SICKey)

	update #tempSic  --Linda
	set ShiftValue =
	(select shiftValue = sum(case when Value > 0 then 1 else 0 end) from SICCAPTURE where
	  SectionID = @SectionID and
	  CalendarDate >= @StartDate and
	  CalendarDate <= @EndDate and
	  SICKey = #tempSic.SICKey)  --linda.SICKey)

	select SICKey, KPI, Description, 
		CalendarDate, 
		ShiftNo = case when WorkingDay = 'Y' then ShiftNo else 'N' end, 
		TotalShifts, 
		TheValue = case when isnull(TheValue,0) = 0 then '' else
		convert(varchar(10), convert(numeric(7,2),TheValue)) end,
		ProgValue = case when SicKey in (7,14,21) then ''
				when ShiftValue > 0 then convert(varchar(10), convert(numeric(7,2),ProgValue / ShiftValue))
				else '' end					
	from #tempSic  --Linda
	order by SicKey, CalendarDate

	DEALLOCATE aa_cursor
	drop table #tempSic
END
ELSE
BEGIN
	select * from
	(
		select SICKey = case 
					  when z.[Type] = 'Book FL' then ''
					  when z.[Type] = 'Plan' then '22'
					  when z.[Type] = 'Book' then '23' else '' end,
		  p.Sectionid +' : '+sc.Name Name, 
		  --p.Sectionid SectionID, sc.Name Name, 
		  p.Workplaceid+' : '+w.Description Workplace,
		  --p.Workplaceid WorkplaceID, w.Description Description, 
		  p.Activity Activity, 
		  p.CalendarDate CalendarDate,
		  s23.ProblemCode, s23.SBNotes, s23.CausedLostBlast,
		  ShiftNo = case when ct.WorkingDay = 'Y' then convert(varchar(2), p.ShiftDay) else 'N' end, 
		  TheValue = case 
					  when z.[Type] = 'Book FL' and p.Activity <> 1 and isnull(p.BookFL,0) = 0 and ct.WorkingDay = 'Y' then '0'
					  when z.[Type] = 'Book FL' and p.Activity <> 1 and isnull(p.BookFL,0) > 0 and ct.WorkingDay = 'Y' then 
							convert(varchar(10), convert(Numeric(7,0),isnull(p.BookFL,0)))
					  when z.[Type] = 'Plan' and p.Activity <> 1 and isnull(s22.Value,0) = 0 then ''
					  when z.[Type] = 'Plan' and p.Activity <> 1 and isnull(s22.Value,0) > 0 then 
							convert(varchar(10), convert(Numeric(7,0),isnull(s22.Value,0)))
					  when z.[Type] = 'Book' and p.Activity <> 1 and isnull(s23.ProblemCode, '') <> '' then 
							isnull(s23.ProblemCode, '')
					  when z.[Type] = 'Book' and p.Activity <> 1 and isnull(s23.Value,0) = 0 then '' 
					  when z.[Type] = 'Book' and p.Activity <> 1 and isnull(s23.Value,0) > 0 then
						   convert(varchar(10), convert(Numeric(7,0),isnull(s23.Value, 0)))
					  when z.[Type] = 'Book FL' and p.Activity = 1 then '' 
					  when z.[Type] = 'Plan' and p.Activity = 1 and isnull(s22.Value,0) = 0 then ''
					  when z.[Type] = 'Plan' and p.Activity = 1 and isnull(s22.Value,0) = 99 then 
							'Yes'
					  when z.[Type] = 'Book' and p.Activity = 1 and isnull(s23.ProblemCode, '') <> '' then 
							isnull(s23.ProblemCode, '')
					  when z.[Type] = 'Book' and p.Activity = 1 and isnull(s23.Value,0) = 0 then '' 
					  when z.[Type] = 'Book' and p.Activity = 1 and isnull(s23.Value,0) = 99 then
						  'Yes' else ''
				  end,
		  Type = z.[Type],
		  [Order] 
		from  Planning p 
		inner join Section_Complete sc on 
		  sc.ProdMonth = p.ProdMonth and 
		  sc.SectionID = p.SectionID
		inner join Seccal ss on
		  ss.ProdMonth = sc.ProdMonth and
		  ss.SectionID = sc.SectionID_1 
	    inner join CalType ct on
		  ss.CalendarCode = ct.CalendarCode and
		  ss.BeginDate <= ct.CalendarDate and
		  ss.Enddate >= ct.CalendarDate and
		  ct.CalendarDate = p.Calendardate
	    left outer join SICCapture s22 on
		  s22.SectionID = sc.SectionID_1 and
		  s22.WorkplaceID = p.WorkplaceID and
		  s22.CalendarDate = ct.CalendarDate and
		  s22.SICKey = 22
	    left outer join SICCapture s23 on
		  s23.SectionID = sc.SectionID_1 and
		  s23.WorkplaceID = p.WorkplaceID and
		  s23.CalendarDate = ct.CalendarDate and
		  s23.SICKey = 23
	    inner join WORKPLACE w on 
		  w.Workplaceid = p.WorkplaceID,
	    (select [Order] = 1,
		  Type = 'Book FL'
	     union
	     select [Order] = 2,
		  Type = 'Plan'
	     union
	     select [Order] = 3,
		  Type = 'Book') z
	    where sc.SectionID_1 = @SectionID and 
			  p.Prodmonth = @ProdMonth and
			  p.PlanCode = 'MP' and
			  p.IsCubics = 'N'
	--and ct.calendardate <= ss.rundate
	 ) a 
	 order by Name, Workplace, CalendarDate, [Order]    
END
go














