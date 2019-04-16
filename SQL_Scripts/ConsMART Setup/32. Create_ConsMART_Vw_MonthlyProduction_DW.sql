CREATE View [dbo].[Vw_MonthlyProduction_DW] as 
(
    Select m.divisioncode, m.description, p.Mine, p.WorkplaceId, p.ProdMonth, p.SectionId, p.Unit,p.Amount, p.Version
    From
	   (
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Reef m (W/C)' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Reef m (Cap)' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Waste m (Cap)' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Reef m (W/C)' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Reef m (Cap)' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Waste m (W/C)' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Waste m (Cap)' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'On Reef Development Value (g/t)' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'On Reef Development Value (g/t)' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'On Reef Development Height (m)' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'On Reef Development Height (m)' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'On Reef Development Width (m)' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Waste Sqm' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Reef Sqm' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Reef Tons' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Waste Tons' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Dev Reef Tons' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Dev Waste Tons' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Dev Reef Tons' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Face length' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Dev Waste Tons' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'On Reef Development Width (m)' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Face length' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Content' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Reef Tons' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Waste Tons' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'cmg/t' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'cmg/t' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Rock Density' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Rock Density' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Face Advance' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'g/t' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'g/t' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'SW' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'SW' And Version = 'Measured' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Reef Sqm' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Waste Sqm' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''
		  Union
			 Select Distinct Operation Mine,WorkplaceId,ProdMonth, SectionId,UnitOfMeasure Unit,Amount, Version, WorkplaceDivisionCode
			 From ConsMART.dbo.ProductionData_Monthly
			 Where UnitOfMeasure = 'Stope Content' And Version = 'Locked Plan' And IsNull(Amount, '') <> ''

	   ) p
    inner join code_wpdivision m on m.mine = p.mine 
						  And m.DivisionCode = p.WorkplaceDivisionCode
)