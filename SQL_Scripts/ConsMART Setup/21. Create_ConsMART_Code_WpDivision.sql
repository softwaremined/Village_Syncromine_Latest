CREATE TABLE [dbo].[Code_WpDivision](
	[DivisionCode] [varchar](2) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[selected] [char](1) NULL,
	[Density] [float] NULL,
	[Editable] [int] NULL CONSTRAINT [DF_CODE_WPDIVISION_Editable]  DEFAULT ((0)),
	[WPLastUsed] [numeric](8, 0) NULL,
	[Mine] [varchar](50) NOT NULL
) ON [PRIMARY]
