


CREATE View [dbo].[Vw_SectionComplete_DW] as (
    Select WorkplaceDivisionCode, wpd.Description, pd.ProdMonth, pd.SectionId, pd.SectionName, pd.SectionId_1 SBID, pd.SectionName_1 SBName, pd.SectionId_2 MOID, pd.SectionName_2 MOName,
		  pd.SectionId_3 UMID, pd.SectionName_3 UMName, pd.SectionId_4 PMID, pd.SectionName_4 PMName, pd.SectionId_5 MANID, pd.SectionName_5 MANName, Mine
    From ConsMART.dbo.ProductionData pd
    Inner Join ConsMART.dbo.Code_WpDivision wpd on pd.Operation = wpd.Mine
									   And pd.WorkplaceDivisionCode = wpd.DivisionCode
)
