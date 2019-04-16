--Drop Procedure sp_InsertUpdate_ProductionDataUsingTable
--Drop Type ProductionDataType

CREATE TYPE [dbo].[ProductionDataType] AS TABLE(
	[Operation] [varchar](50) NOT NULL,
	[ProdMonth] [numeric](7, 0) NOT NULL,
	[CalendarDate] [datetime] NOT NULL,
	[SectionId] [varchar](20) NOT NULL,
	[SectionName] [varchar](50) NOT NULL,
	[SectionId_1] [varchar](20) NOT NULL,
	[SectionName_1] [varchar](50) NOT NULL,
	[SectionId_2] [varchar](20) NOT NULL,
	[SectionName_2] [varchar](50) NOT NULL,
	[SectionId_3] [varchar](20) NOT NULL,
	[SectionName_3] [varchar](50) NOT NULL,
	[SectionId_4] [varchar](20) NOT NULL,
	[SectionName_4] [varchar](50) NOT NULL,
	[SectionId_5] [varchar](20) NOT NULL,
	[SectionName_5] [varchar](50) NOT NULL,
	[OrgUnitDay] [varchar](20) NULL,
	[OrgUnitAfternoon] [varchar](20) NULL,
	[OrgUnitNight] [varchar](20) NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[WorkplaceDescription] [varchar](50) NULL,
	[WorkplaceReefWaste] [varchar](2) NULL,
	[WorkplaceGridCode] [varchar](30) NULL,
	[WorkplaceDivisionCode] [varchar](10) NULL,
	[LevelNumber] [varchar](10) NULL,
	[ShiftDay] [varchar](5) NULL,
	[WorkingDay] [varchar](5) NULL,
	[Activity_Code] [int] NULL,
	[Activity_Desc] [varchar](50) NULL,
	[Version] [varchar](50) NULL,
	[UnitOfMeasure] [varchar](100) NOT NULL,
	[Amount] [varchar](15) NOT NULL
)
GO