CREATE TABLE [dbo].[XYZCoordinates_Errors](
	[Export_Date] [datetime] NULL,
	[Blast_Date] [datetime] NULL,
	[SQM] [int] NULL,
	[WPID] [varchar](12) NULL,
	[Description] [varchar](50) NULL,
	[ProdMonth] [varchar](10) NULL,
	[ErrorMsg] [varchar](100) NULL,
	[OldWPName] [varchar](50) NULL
) ON [PRIMARY]
GO