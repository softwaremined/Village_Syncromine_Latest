Create Procedure sp_InsertUpdate_ProductionDataMonthlyUsingTable
						@data as dbo.ProductionData_MonthlyType ReadOnly 
As
Begin

	Merge ProductionData_Monthly d
	Using @data data on
				data.Operation				= d.Operation				
			And data.ProdMonth				= d.ProdMonth				
			And data.SectionId				= d.SectionId				
			And data.SectionName			= d.SectionName					
			And data.SectionId_1			= d.SectionId_1					
			And data.SectionName_1			= d.SectionName_1			
			And data.SectionId_2			= d.SectionId_2					
			And data.SectionName_2			= d.SectionName_2			
			And data.SectionId_3			= d.SectionId_3					
			And data.SectionName_3			= d.SectionName_3			
			And data.SectionId_4			= d.SectionId_4					
			And data.SectionName_4			= d.SectionName_4			
			And data.SectionId_5			= d.SectionId_5					
			And data.SectionName_5			= d.SectionName_5			
			And data.WorkplaceID			= d.WorkplaceID					
			And data.WorkplaceDescription	= d.WorkplaceDescription	
			And data.WorkplaceReefWaste		= d.WorkplaceReefWaste		
			And data.WorkplaceGridCode		= d.WorkplaceGridCode		
			And data.WorkplaceDivisionCode	= d.WorkplaceDivisionCode	
			And data.LevelNumber			= d.LevelNumber				
			And data.Activity_Code			= d.Activity_Code			
			And data.Activity_Desc			= d.Activity_Desc			
			And data.Version				= d.Version						
			And data.UnitOfMeasure			= d.UnitOfMeasure			
	When Matched And d.Amount <> data.Amount Then
		Update Set d.Amount = data.Amount,
					d.DateModified = GetDate(),
				d.OrgUnitDay = data.OrgUnitDay,
				d.OrgUnitAfternoon = data.OrgUnitAfternoon,
				d.OrgUnitNight = data.OrgUnitNight
	When Not Matched Then
		Insert
			Values (
				data.Operation,
				data.ProdMonth,
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
				data.OrgUnitDay,
				data.OrgUnitAfternoon,
				data.OrgUnitNight,
				data.WorkplaceID,
				data.WorkplaceDescription,
				data.WorkplaceReefWaste,
				data.WorkplaceGridCode,
				data.WorkplaceDivisionCode,
				data.LevelNumber,
				data.Activity_Code,
				data.Activity_Desc,
				data.Version,
				data.UnitOfMeasure,
				data.Amount,
				GetDate()
			);
End