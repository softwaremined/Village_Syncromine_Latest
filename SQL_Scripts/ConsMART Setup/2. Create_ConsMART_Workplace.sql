USE [ConsMART]
GO

CREATE TABLE [dbo].[Workplace](
	[Operation] [varchar](50) NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[Description] [varchar](50) NULL,
	[DivisionCode] [varchar](2) NULL,
	[DivisionDescription] [varchar](100) NULL,
	[TypeCode] [varchar](3) NULL,
	[TypeDescription] [varchar](100) NULL,
	[OreFlowID] [varchar](10) NULL,
	[LevelNumber] [varchar](5) NULL,
	[LevelDescription] [varchar](100) NULL,
	[GridCode] [varchar](30) NULL,
	[GridDescription] [varchar](100) NULL,
	[DetailCode] [varchar](10) NULL,
	[DetailDescription] [varchar](100) NULL,
	[NumberCode] [varchar](50) NULL,
	[NumberDescription] [varchar](100) NULL,
	[DirectionCode] [varchar](8) NULL,
	[DirectionDescription] [varchar](100) NULL,
	[DescrCode] [varchar](10) NULL,
	[DescrDescription] [varchar](100) NULL,
	[DescrNoCode] [varchar](20) NULL,
	[DescrNoDescription] [varchar](100) NULL,
	[ReefID] [numeric](7, 0) NULL,
	[ReefName] [varchar](100) NULL,
	[ReefDescription] [varchar](100) NULL,
	[Converted] [char](1) NULL,
	[WPStatus] [char](1) NULL,
	[WPStatusDescription] [varchar](100) NULL,
	[CreationDate] [datetime] NULL,
	[Classification] [char](1) NULL,
	[ClassificationDescription] [varchar](100) NULL,
	[OldWorkplaceid] [varchar](12) NULL,
	[EndTypeID] [numeric](7, 0) NULL,
	[Activity] [numeric](7, 0) NULL,
	[ReefWaste] [varchar](2) NULL,
	[StopingCode] [varchar](8) NULL,
	[EndWidth] [numeric](5, 1) NULL,
	[EndHeight] [numeric](5, 1) NULL,
	[Line] [varchar](20) NULL,
	[Priority] [varchar](2) NULL,
	[Mech] [varchar](2) NULL,
	[Cap] [varchar](50) NULL,
	[RiskRating] [numeric](10, 0) NULL,
	[DefaultAdv] [numeric](18, 2) NULL,
	[GMSIWPID] [numeric](18, 0) NULL,
	[Inactive] [varchar](1) NULL,
	[Density] [decimal](18, 2) NULL,
	[BrokenRockDensity] [decimal](18, 2) NULL,
	[SubSection] [int] NULL,
	[ProcessCode] [int] NULL,
	[Userid] [varchar](20) NULL,
	[BoxholeID] [varchar](50) NULL
CONSTRAINT [PK_WORKPLACE] PRIMARY KEY CLUSTERED 
(
	[Operation],
	[WorkplaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]
GO