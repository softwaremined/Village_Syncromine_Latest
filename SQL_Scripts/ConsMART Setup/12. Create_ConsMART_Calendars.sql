CREATE TABLE [dbo].[Calendars](
	[CalendarDate] DateTime NOT NULL,
	[WorkingDay] [varchar](1) NULL,
	[ProdMonth] [varchar](6) NOT NULL,
	[SectionId] [varchar](50) NOT NULL,
	[CalendarCode] [varchar](50) NOT NULL,
	[BeginDate] DateTime Not Null,
	[EndDate] DateTime NOT NULL,
	[TotalShifts] int NOT NULL,
	[Mine] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CalendarDate] ASC,
	[Mine] ASC,
	[SectionId] ASC,
	[CalendarCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]