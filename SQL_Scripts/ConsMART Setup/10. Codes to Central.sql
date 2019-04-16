    Declare @Mine varchar(100)
    Set @Mine = 'PAS_dnk'

--Grid
    Print('Grid')
    MERGE Pas_Central.dbo.Code_Grid d
    USING (
	   Select Distinct Grid, Description, Division, CostArea, @Mine Mine FROM Code_Grid 
	   ) s
    ON d.Grid = s.Grid
	   And d.Mine = s.Mine
    WHEN MATCHED THEN UPDATE SET 
	   Description = s.Description,
	   Division = s.Division,
	   CostArea = s.CostArea
    WHEN NOT MATCHED BY TARGET THEN
	   INSERT (Grid, Description, Division, CostArea, Mine)
	   VALUES (s.Grid, s.Description, s.Division, s.CostArea, s.Mine);

--Inavtive Reason
    Print('WP Inactivity Reason')
    MERGE Pas_Central.dbo.WorkPlace_Inactivation_Reason d
    USING (
	   Select Distinct Reason, Selected, Inactive FROM WorkPlace_Inactivation_Reason 
	   ) s
    ON d.Reason = s.Reason
    WHEN MATCHED THEN UPDATE SET 
	   Selected = s.Selected,
	   Inactive = s.Inactive
    WHEN NOT MATCHED BY TARGET THEN
	   INSERT (Reason, Selected, Inactive)
	   VALUES (s.Reason, s.Selected, s.Inactive);

--WP Description No
    Print('WP Description No')
    MERGE Pas_Central.dbo.Code_WpDescriptionNo d
    USING (
	   Select Distinct DescrNumberCode, Description, Selected, Inactive FROM Code_WpDescriptionNo 
	   ) s
    ON d.DescrNumberCode = s.DescrNumberCode
    WHEN MATCHED THEN UPDATE SET 
	   Description = s.Description,
	   Selected = s.Selected,
	   Inactive = s.Inactive
    WHEN NOT MATCHED BY TARGET THEN
	   INSERT (DescrNumberCode, Description, Selected, Inactive)
	   VALUES (s.DescrNumberCode, s.Description, s.Selected, s.Inactive);

--WP Description
    Print('WP Description')
    MERGE Pas_Central.dbo.Code_WPDescription d
    USING (
	   Select Distinct DescrCode, Description, Selected, Inactive FROM Code_WPDescription 
	   ) s
    ON d.DescrCode = s.DescrCode And d.Description = s.Description
    WHEN MATCHED THEN UPDATE SET 
	   Description = s.Description,
	   Selected = s.Selected,
	   Inactive = s.Inactive
    WHEN NOT MATCHED BY TARGET THEN
	   INSERT (DescrCode, Description, Selected, Inactive)
	   VALUES (s.DescrCode, s.Description, s.Selected, s.Inactive);

--WP Number
    Print('WP Number')
    MERGE Pas_Central.dbo.Code_WPNumber d
    USING (
	   Select Distinct NumberCode, Description, Selected, Inactive FROM Code_WPNumber 
	   ) s
    ON d.NumberCode = s.NumberCode
    WHEN MATCHED THEN UPDATE SET 
	   Description = s.Description,
	   Selected = s.Selected,
	   Inactive = s.Inactive
    WHEN NOT MATCHED BY TARGET THEN
	   INSERT (NumberCode, Description, Selected, Inactive)
	   VALUES (s.NumberCode, s.Description, s.Selected, s.Inactive);

--WP Detail
    Print('WP Detail')
    MERGE Pas_Central.dbo.Code_WPDetail d
    USING (
	   Select Distinct DetailCode, Description, Selected, Inactive FROM Code_WPDetail 
	   ) s
    ON d.DetailCode = s.DetailCode
    WHEN MATCHED THEN UPDATE SET 
	   Description = s.Description,
	   Selected = s.Selected,
	   Inactive = s.Inactive
    WHEN NOT MATCHED BY TARGET THEN
	   INSERT (DetailCode, Description, Selected, Inactive)
	   VALUES (s.DetailCode, s.Description, s.Selected, s.Inactive);
    
--WP Type
    Print('WP Type')
    MERGE Pas_Central.dbo.Code_WPType d
    USING (
	   Select Distinct TypeCode, Description, Selected, Inactive, Classification FROM Code_WPType 
	   ) s
    ON d.TypeCode = s.TypeCode
    WHEN MATCHED THEN UPDATE SET 
	   Description = s.Description,
	   Selected = s.Selected,
	   Classification = s.Classification,
	   Inactive = s.Inactive
    WHEN NOT MATCHED BY TARGET THEN
	   INSERT (TypeCode, Description, Selected, Inactive, Classification)
	   VALUES (s.TypeCode, s.Description, s.Selected, s.Inactive, s.Classification);