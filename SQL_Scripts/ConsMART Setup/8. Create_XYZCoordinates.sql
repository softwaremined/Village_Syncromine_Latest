CREATE TABLE [dbo].[XYZCoordinates](
	[Export_Date] [datetime] NULL,
	[Blast_Time] [varchar](6) NULL,
	[X_Coordinate] [float] NULL,
	[Y_Coordinate] [float] NULL,
	[Z_Coordinate] [float] NULL,
	[SQM] [int] NULL,
	[StopeWidth] [numeric](7, 0) NULL,
	[Activity] [int] NULL,
	[WPID] [varchar](12) NULL,
	[Description] [varchar](50) NULL,
	[Blast_Date] [datetime] NULL,
	[ProdMonth] [varchar](10) NULL,
	[Bl_Date] [varchar](8) NULL,
	[Ex_Date] [varchar](8) NULL,
	[X1] [float] NULL,
	[Y1] [float] NULL,
	[Z1] [float] NULL,
	[X2] [float] NULL,
	[Y2] [float] NULL,
	[Z2] [float] NULL,
	[Adv] [numeric](18, 2) NULL
) ON [PRIMARY]
GO