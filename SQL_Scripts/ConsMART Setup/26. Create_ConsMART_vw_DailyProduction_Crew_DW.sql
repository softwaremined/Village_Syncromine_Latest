CREATE VIEW [dbo].[vw_DailyProduction_Crew_DW] 
as 
(    
    Select Distinct WorkplaceDivisionCode DivisionCode,
			 WorkplaceDescription Description,
			 Operation Mine,
			 WorkplaceId,
			 SectionId,
			 ProdMonth,
			 CalendarDate,
			 UnitOfMeasure Unit,
			 Amount, 
			 Version,
			 OrgUnitDay
    From ConsMART.dbo.ProductionData
    Where Prodmonth >= '201601'
)