CREATE TABLE [dbo].[Problem](
	[ProblemID] [varchar](20) NOT NULL,
	[ProblemGroupCode] [varchar](15) NOT NULL,
	[EnquirerID] [varchar](3) NULL,
	[Description] [varchar](50) NULL,
	[ExtraInfo] [numeric](7, 0) NULL,
	[Valid] [varchar](2) NULL,
	[EmailGroup] [varchar](50) NULL,
	[HQCat] [varchar](100) NULL,
 CONSTRAINT [PK_PROBLEM_1] PRIMARY KEY CLUSTERED 
(
	[ProblemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]