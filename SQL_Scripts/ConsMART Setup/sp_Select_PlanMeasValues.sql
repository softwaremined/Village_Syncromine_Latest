Alter Procedure sp_Select_PlanMeasValues
						@Operation varchar(20),
						@FromMonth int
As
Begin	
    --Declare @Operation varchar(20),
		  --@FromMonth int
    --Set @Operation = 'DNK'
    --Set @FromMonth = 201708

    Select @Operation Operation,
	   pm.ProdMonth,
	   pm.OrgUnitDay, 
	   pm.OrgUnitAfternoon,
	   pm.OrgUnitNight,
	   sc.SectionId, sc.Name SectionName, 
	   sc.SectionId_1, sc.Name_1 SectionName_1, 
	   sc.SectionId_2, sc.Name_2 SectionName_2, 
	   sc.SectionId_3, sc.Name_3 SectionName_3, 
	   sc.SectionId_4, sc.Name_4 SectionName_4, 
	   sc.SectionId_5, sc.Name_5 SectionName_5, 
	   w.WorkplaceId,
	   w.Description WorkplaceDescription,
	   w.ReefWaste WorkplaceReefWaste,
	   w.GridCode WorkplaceGridCode,
	   w.DivisionCode WorkplaceDivisionCode,
	   oe.LevelNumber, 
	   pm.Activity ActivityCode,
	   case 
		  when pm.activity = 0 then 'Stoping'
		  when pm.activity = 1 then 'Development'
		  when pm.activity = 9 then 'Ledging'
		  else 'Unknown' 
	   end as ActivityDesc,
	   PlanWcReefAdv = case when pm.Activity = 1 And IsNull(s.Indicator, '') = '1' And pm.ReefAdv > 0 then pm.ReefAdv end,
	   PlanCapReefAdv = case when pm.Activity = 1 And IsNull(s.Indicator, '') > '1' And pm.ReefAdv > 0 then pm.ReefAdv end,
	   PlanWcWasteAdv = case when pm.Activity = 1 And IsNull(s.Indicator, '') = '1' And pm.WasteAdv > 0 then pm.WasteAdv end,
	   PlanCapWasteAdv = case when pm.Activity = 1 And IsNull(s.Indicator, '') > '1' And pm.WasteAdv > 0 then pm.ReefAdv end,
	   MeasWcReefAdv = case when s.Activity = 1 And IsNull(s.Indicator, '') = '1' And s.ReefMetres > 0 then s.ReefMetres end,
	   MeasCapReefAdv = case when s.Activity = 1 And IsNull(s.Indicator, '') > '1' And s.ReefMetres > 0 then s.ReefMetres end,
	   MeasWcWasteAdv = case when s.Activity = 1 And IsNull(s.Indicator, '') = '1' And s.WasteMetres > 0 then s.WasteMetres end,
	   MeasCapWasteAdv = case when s.Activity = 1 And IsNull(s.Indicator, '') > '1' And s.WasteMetres > 0 then s.WasteMetres end,
	   PlanDevReefValue = case when pm.Activity = 1 And pm.Tons > 0 And pm.Kg > 0 then CAST((pm.Kg * 1000) / pm.Tons as numeric(10,5)) end,
	   MeasDevReefValue = case when s.Activity = 1 And s.Gt > 0 then s.GT end,
	   PlanDevReefHeight = case when pm.Activity = 1 And IsNull(pm.DHeight, 0) > 0 then pm.DHeight end,
	   MeasDevReefHeight = case when s.Activity = 1 And IsNull(s.MeasHeight, 0) > 0 then s.MeasHeight end,
	   PlanDevReefWidth = case when pm.Activity = 1 And IsNull(pm.DWidth, 0) > 0 then pm.DWidth end ,
	   MeasDevReefWidth = case when s.Activity = 1 And IsNull(s.MeasWidth, 0) > 0 then s.MeasWidth end,
	   PlanStopeReefSqm = case when pm.Activity in (0,9) And pm.ReefSqm > 0 then pm.ReefSqm end,
	   PlanStopeWasteSqm = case when pm.Activity in (0,9) and pm.WasteSqm > 0 then pm.WasteSqm end,
	   MeasStopeReefSqm = case when s.Activity in (0,9) And s.StopeSqmTotal + s.LedgeSqm + s.SqmConvTotal > 0 then s.StopeSqmTotal + s.LedgeSqm + s.SqmConvTotal end,
	   MeasStopeWasteSqm = case when s.Activity in (0,9) And s.StopeSqmOSF + s.StopeSQMOS > 0 then s.StopeSqmOSF + s.StopeSQMOS end,
	   MeasStopeReefTons = case when s.Activity in (0,9) and (s.StopeSqm + s.LedgeSqm + s.SqmConvTotal) * s.SWSQM / 100 * s.Density > 0 then CAST((s.StopeSqm + s.LedgeSqm + s.SqmConvTotal) * s.SWSQM / 100 * s.Density as numeric(10,5)) end,
	   MeasStopeWasteTons = case when s.ACtivity in (0,9) and s.TonsOS > 0 then s.TonsOS end,
	   PlanDevReefTons = case when pm.Activity = 1 And pm.ReefTons > 0 then pm.ReefTons end,
	   PlanDevWasteTons = case when pm.Activity = 1 And pm.WasteTons > 0 then pm.WasteTons end,
	   MeasDevReefTons = case when s.Activity = 1 And s.TonsReef > 0 then s.TonsReef end,
	   MeasDevWasteTons = case when s.Activity = 1 And s.TonsWaste > 0 then s.TonsWaste end,
	   PlanStopeReefTons = case when pm.Activity in (0,9) And pm.ReefTons > 0 then pm.ReefTons end,
	   PlanStopeWasteTons = case when pm.Activity in (0,9) And pm.WasteTons > 0 then pm.WasteTons end,
	   PlanStopeFL = case when pm.Activity in (0,9) And pm.FL > 0 then pm.FL end,
	   MeasStopeFL = case when s.Activity in (0,9) And s.FLTotal > 0 then s.FLTotal end,
	   PlanStopeContent = case when pm.Activity in (0,9) And pm.KG > 0 then pm.KG * 1000 end,
	   MeasStopeContent = case when s.Activity in (0,9) And s.TotalContent > 0 then s.TotalContent end,
	   PlanCmgt = case when pm.GT > 0 then pm.GT end,
	   MeasCmgt = case when s.CMGT > 0 then s.CMGT end,
	   PlanDensity = case when pm.Density > 0 then pm.Density end,
	   MeasDensity = case when s.Density > 0 then s.Density end,
	   PlanStopeAdv = case when pm.Activity in (0,9) And pm.FL > 0 And pm.ReefSqm > 0 then CAST(pm.ReefSqm / pm.FL as Numeric(10,5)) end,
	   PlanGt = case when pm.Gt > 0 And pm.SW > 0 then pm.GT / pm.SW end,
	   MeasGt = case when s.SWSQM > 0 then s.CMGT / s.SWSQM end,
	   PlanSw = case when pm.SW > 0 then pm.SW end,
	   MeasSw = case when s.Activity in (0,9) And s.SwSQM > 0 then s.SwSQM end

    From PlanMonth pm
    Left Join Survey s on pm.WorkplaceId = s.WorkplaceId
				    And pm.SectionId = s.SectionId
				    And pm.Activity = s.Activity
				    And pm.ProdMonth = s.ProdMonth

    Inner Join Section_Complete sc on pm.Prodmonth = sc.Prodmonth And pm.SectionId = sc.SectionID
    Inner Join Workplace w on pm.WorkplaceID = w.WorkplaceID
    Inner Join OreFlowEntities oe on oe.OreFlowID = w.OreFlowID
    Where Pm.PlanCode = 'LP'
    And (IsNull(@FromMonth, 0) = 0 Or pm.ProdMonth >= @FromMonth)
End