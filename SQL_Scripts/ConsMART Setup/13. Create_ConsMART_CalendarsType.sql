CREATE TYPE [dbo].CalendarsType AS TABLE (
    [CalendarDate] DateTime NOT NULL,
    [WorkingDay] [varchar](1) NULL,
    [ProdMonth] [varchar](6) NOT NULL,
    [SectionId] [varchar](50) NOT NULL,
    [CalendarCode] [varchar](50) NOT NULL,
    [BeginDate] DateTime Not Null,
    [EndDate] DateTime NOT NULL,
    [TotalShifts] int NOT NULL,
    [Mine] [varchar](50) NOT NULL
)