USE [PAS_DNK_Syncromine]
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dolf van den Berg
-- Create date: 2019/02/20
-- Description:	To get the workpace cycle code
-- =============================================
create FUNCTION GetCycleCode 
(
	-- Add the parameters for the function here
	 @workplaceID varchar(12),
        @sectionidMO varchar(10),
		@Activity numeric(7,0)
)
RETURNS varchar(120)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar varchar(120), @RowCount int
	
	set @RowCount = (select count(cycle) from [dbo].[Code_Cycle_WPExceptions] where workplaceid = @workplaceID)

	if(@RowCount = 1)
	begin
	set @ResultVar = (select cycle from [dbo].[Code_Cycle_WPExceptions] where workplaceid = @workplaceID)
	end
	else
	begin
	set @ResultVar = ( select case when @Activity = 0 then StopingCycle else DevCycle end from Code_Cycle_MOCycleConfig where sectionid = @sectionidMO)
	
	end

	RETURN @ResultVar

END

