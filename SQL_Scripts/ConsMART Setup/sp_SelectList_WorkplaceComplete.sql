Alter Procedure sp_SelectList_WorkplaceComplete
		  @Operation varchar(20)
As
Begin
    SELECT DISTINCT @Operation Operation,
		 Workplaceid Workplace_ID,
		 w.Description Workplace_Name,
		 dv.divisioncode Code_Division,
		 dv.description Division_Description,
		 ty.Typecode Code_WP_Type,
		 ty.description WP_Type_Description,
		 o.Oreflowid Code_Level,
		 Levelnumber Level_Number,
		 w.description Level_Description,
		 g.grid Code_Grid,
		 g.description Grid_Description,
		 de.detailcode Code_Detail,
		 de.description Detail_Description,
		 n.Description Code_Number,
		 n.numbercode Number_Description,
		 di.Direction Code_Direction,
		 di.description Direction_Description,
		 ds.Descrcode Code_Description,
		 ds.description Description_Description,
		 dn.Description Code_Description_Number,
		 dn.DescrNumbercode Description_Number_Description,
		 r.reefid Code_Reef,
		 r.WPname Reef_Name,
		 r.Description Reef_Description,
		 w.Converted,
		 w.WPStatus,
		 wPS.description Workplace_Status,
		 w.CreationDate Creation_Date,
		 w.Classification,
		 wpc.description Classification_Indicator,
		 CASE
			WHEN w.OldWorkplaceID = ''
			THEN w.workplaceid
			ELSE w.oldworkplaceid
		 END Old_Workplace_ID,
		 w.EndTypeID,
		 w.Activity,
		 w.ReefWaste,
		 w.StopingCode,
		 w.EndWidth,
		 w.EndHeight,
		 w.Line,
		 w.Priority,
		 w.Mech,
		 w.Cap,
		 w.RiskRating,
		 w.DefaultAdv,
		 w.GMSIWPID,
		 w.Inactive,
		 w.Density,
		 w.BrokenRockDensity,
		 w.SubSection,
		 w.ProcessCode,
		 w.Userid,
		 w.BoxholeId
    FROM workplace w
    LEFT OUTER JOIN oreflowentities o ON o.oreflowid = w.oreflowid
    LEFT OUTER JOIN Code_Grid g ON g.grid = w.gridcode
    LEFT OUTER JOIN Code_direction di ON di.direction = w.direction
    LEFT OUTER JOIN code_wpdivision dv ON dv.divisioncode = w.divisioncode
    LEFT OUTER JOIN code_wpType ty ON ty.typecode = w.typecode
    LEFT OUTER JOIN code_wpdetail de ON de.detailcode = w.detailcode
    LEFT OUTER JOIN code_wpnumber n ON n.numbercode = w.numbercode
    LEFT OUTER JOIN code_wpdescription ds ON ds.descrcode = w.descrcode
    LEFT OUTER JOIN code_wpdescriptionno dn ON dn.descrnumbercode = w.descrnocode
    LEFT OUTER JOIN reef r ON r.reefid = w.reefid
    LEFT OUTER JOIN code_wpstatus wps ON wps.WPStatus = w.WPStatus
    LEFT OUTER JOIN code_wpclassification wpc ON wpc.Classification = w.Classification
End