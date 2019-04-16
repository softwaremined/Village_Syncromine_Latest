Drop Procedure sp_InsertUpdate_ProductionData
Go
Create Procedure sp_InsertUpdate_ProductionData
						@Operation varchar(50),
						@ProdMonth numeric(7, 0),
						@CalendarDate datetime,
						@SectionId varchar(20),
						@SectionName varchar(50),
						@SectionId_1 varchar(20),
						@SectionName_1 varchar(50),
						@SectionId_2 varchar(20),
						@SectionName_2 varchar(50),
						@SectionId_3 varchar(20),
						@SectionName_3 varchar(50),
						@SectionId_4 varchar(20),
						@SectionName_4 varchar(50),
						@SectionId_5 varchar(20),
						@SectionName_5 varchar(50),
						@WorkplaceID varchar(12),
						@WorkplaceDescription varchar(50),
						@WorkplaceReefWaste varchar(2),
						@WorkplaceGridCode varchar(30),
						@WorkplaceDivisionCode varchar(10),
						@LevelNumber varchar(10),
						@ShiftDay varchar(5),
						@WorkingDay varchar(5),
						@Activity_Code int,
						@Activity_Desc varchar(50),
						@Version varchar(50),
						@UnitOfMeasure varchar(30),
						@Amount varchar(15)
As
Begin

	--Declare @Operation	varchar(50),
	--		@ProdMonth		numeric(7, 0),
	--		@CalendarDate	datetime,
	--		@SectionId		varchar(20),
	--		@SectionName	varchar(50),
	--		@SectionId_1	varchar(20),
	--		@SectionName_1	varchar(50),
	--		@SectionId_2	varchar(20),
	--		@SectionName_2	varchar(50),
	--		@SectionId_3	varchar(20),
	--		@SectionName_3	varchar(50),
	--		@SectionId_4	varchar(20),
	--		@SectionName_4	varchar(50),
	--		@SectionId_5	varchar(20),
	--		@SectionName_5	varchar(50),
	--		@WorkplaceID	varchar(12),
	--		@WorkplaceDescription	varchar(50),
	--		@WorkplaceReefWaste		varchar(2),
	--		@WorkplaceGridCode		varchar(30),
	--		@WorkplaceDivisionCode	varchar(10),
	--		@LevelNumber	varchar(10),
	--		@ShiftDay		varchar(5),
	--		@WorkingDay		varchar(5),
	--		@Activity_Code	int,
	--		@Activity_Desc	varchar(50),
	--		@Version		varchar(50),
	--		@UnitOfMeasure	varchar(30),
	--		@Amount			varchar(15)

	Merge ProductionData destination
	Using (
			Select @Operation				Operation,
				   @ProdMonth				ProdMonth,
				   @CalendarDate			CalendarDate,
				   @SectionId				SectionId,
				   @SectionName				SectionName,
				   @SectionId_1				SectionId_1,
				   @SectionName_1			SectionName_1,
				   @SectionId_2				SectionId_2,
				   @SectionName_2			SectionName_2,
				   @SectionId_3				SectionId_3,
				   @SectionName_3			SectionName_3,
				   @SectionId_4				SectionId_4,
				   @SectionName_4			SectionName_4,
				   @SectionId_5				SectionId_5,
				   @SectionName_5			SectionName_5,
				   @WorkplaceID				WorkplaceID,
				   @WorkplaceDescription	WorkplaceDescription,
				   @WorkplaceReefWaste		WorkplaceReefWaste,
				   @WorkplaceGridCode		WorkplaceGridCode,
				   @WorkplaceDivisionCode	WorkplaceDivisionCode,
				   @LevelNumber				LevelNumber,
				   @ShiftDay				ShiftDay,
				   @WorkingDay				WorkingDay,
				   @Activity_Code			Activity_Code,
				   @Activity_Desc			Activity_Desc,
				   @Version					Version,
				   @UnitOfMeasure			UnitOfMeasure,
				   @Amount					Amount
		) data on
				data.Operation				= destination.Operation				
			And data.ProdMonth				= destination.ProdMonth				
			And data.CalendarDate			= destination.CalendarDate			
			And data.SectionId				= destination.SectionId				
			And data.SectionName			= destination.SectionName					
			And data.SectionId_1			= destination.SectionId_1					
			And data.SectionName_1			= destination.SectionName_1			
			And data.SectionId_2			= destination.SectionId_2					
			And data.SectionName_2			= destination.SectionName_2			
			And data.SectionId_3			= destination.SectionId_3					
			And data.SectionName_3			= destination.SectionName_3			
			And data.SectionId_4			= destination.SectionId_4					
			And data.SectionName_4			= destination.SectionName_4			
			And data.SectionId_5			= destination.SectionId_5					
			And data.SectionName_5			= destination.SectionName_5			
			And data.WorkplaceID			= destination.WorkplaceID					
			And data.WorkplaceDescription	= destination.WorkplaceDescription	
			And data.WorkplaceReefWaste		= destination.WorkplaceReefWaste		
			And data.WorkplaceGridCode		= destination.WorkplaceGridCode		
			And data.WorkplaceDivisionCode	= destination.WorkplaceDivisionCode	
			And data.LevelNumber			= destination.LevelNumber					
			And data.ShiftDay				= destination.ShiftDay				
			And data.WorkingDay				= destination.WorkingDay				
			And data.Activity_Code			= destination.Activity_Code			
			And data.Activity_Desc			= destination.Activity_Desc			
			And data.Version				= destination.Version						
			And data.UnitOfMeasure			= destination.UnitOfMeasure			
	When Matched And destination.Amount <> data.Amount Then
		Update Set destination.Amount = data.Amount,
					destination.DateModified = GetDate()
	When Not Matched Then
		Insert
			Values (
				data.Operation,
				data.ProdMonth,
				data.CalendarDate,
				data.SectionId,
				data.SectionName,
				data.SectionId_1,
				data.SectionName_1,
				data.SectionId_2,
				data.SectionName_2,
				data.SectionId_3,
				data.SectionName_3,
				data.SectionId_4,
				data.SectionName_4,
				data.SectionId_5,
				data.SectionName_5,
				data.WorkplaceID,
				data.WorkplaceDescription,
				data.WorkplaceReefWaste,
				data.WorkplaceGridCode,
				data.WorkplaceDivisionCode,
				data.LevelNumber,
				data.ShiftDay,
				data.WorkingDay,
				data.Activity_Code,
				data.Activity_Desc,
				data.Version,
				data.UnitOfMeasure,
				data.Amount,
				GetDate()
			);
End