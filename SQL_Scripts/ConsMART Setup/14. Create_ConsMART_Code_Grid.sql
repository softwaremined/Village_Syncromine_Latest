CREATE TABLE [dbo].[Code_Grid](
	[Grid] [varchar](30) NOT NULL,
	[Description] [varchar](30) NULL,
	[Division] [varchar](2) NULL,
	[CostArea] [varchar](1) NULL,
	[Mine] [varchar](50) NOT NULL
 CONSTRAINT [PK_CODE_GRID] PRIMARY KEY CLUSTERED 
(
	[Grid] ASC,
	[Mine] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
) ON [PRIMARY]
GO
