Create View [dbo].[vw_MonthlyProduction_Crew_DW] as 
(
    Select pm.WorkplaceDivisionCode DivisionCode, wd.Description, wd.Mine, pm.WorkplaceId, pm.ProdMonth, pm.SectionId, pm.UnitOfMeasure Unit, pm.Amount, pm.Version, OrgUnitDay OrgUnitDS
    From ProductionData_Monthly pm
    Inner Join Code_WpDivision wd on pm.WorkplaceDivisionCode = wd.DivisionCode
    Where IsNull(Amount, '') <> '' 
)