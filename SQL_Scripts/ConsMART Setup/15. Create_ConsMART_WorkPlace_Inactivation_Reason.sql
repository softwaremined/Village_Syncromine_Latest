CREATE TABLE [dbo].WorkPlace_Inactivation_Reason(
	[Reason] [varchar](100) NOT NULL,
	[Selected] [varchar](1) NULL,
	[Inactive] [varchar](1) NULL

 CONSTRAINT [PK_WorkPlace_Inactivation_Reason] PRIMARY KEY CLUSTERED 
(
	[Reason] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]