USE [PAS_DNK_Syncromine]
GO


  alter table [dbo].[CODE_CYCLE] add  DayCallPercentage numeric(4,2)

  GO

  update [dbo].[CODE_CYCLE] set DayCallPercentage = 1

  update [dbo].[CODE_CYCLE] set DayCallPercentage = 0.5 where CycleCode = 'SR'

   update [dbo].[CODE_CYCLE] set CycleCode = '' where Description = 'Blank'