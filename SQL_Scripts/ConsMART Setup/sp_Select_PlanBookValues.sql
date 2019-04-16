--Drop Procedure sp_Select_PlanBookValues
--Go

Create Procedure sp_Select_PlanBookValues
						@Operation varchar(20),
						@FromMonth int,
						@CalendarDate date
As
Begin

	--Declare @Operation varchar(20),
	--		@FromMonth int,
	--		@CalendarDate date
	--Set @Operation = 'DNK'
	--Set @FromMonth = 201708
	--Set @CalendarDate = '2017/11/15'

	Select @Operation Operation, 
		p.Calendardate, 
		p.Prodmonth, 
		pm.OrgUnitDay, 
		sc.SectionId, sc.Name SectionName, 
		sc.SectionId_1, sc.Name_1 SectionName_1, 
		sc.SectionId_2, sc.Name_2 SectionName_2, 
		sc.SectionId_3, sc.Name_3 SectionName_3, 
		sc.SectionId_4, sc.Name_4 SectionName_4, 
		sc.SectionId_5, sc.Name_5 SectionName_5, 
		w.WorkplaceID,
		w.Description WorkplaceDescription,
		w.ReefWaste WorkplaceReefWaste,
		w.GridCode WorkplaceGridCode,
		w.DivisionCode WorkplaceDivisionCode,
		oe.LevelNumber, 
		case when ct.WorkingDay = 'N' then 0 else p.ShiftDay end as ShiftDay,
		ct.WorkingDay, 
		p.Activity ActivityCode,
		case 
			when p.activity = 0 then 'Stoping'
			when p.activity = 1 then 'Development'
			when p.activity = 9 then 'Ledging'
			else 'Unknown' 
		end as ActivityDesc,
		p.Sqm PlanSqm,
		p.BookSqm,
		p.ReefSqm PlanReefSqm,
		p.BookReefSqm,
		p.WasteSqm PlanWasteSqm,
		p.BookWasteSqm,
		p.MetresAdvance PlanMetresAdvance,
		p.BookMetresAdvance,
		p.ReefAdv PlanReefAdv,
		p.BookReefAdv,
		p.WasteAdv PlanWasteAdv,
		p.BookWasteAdv,
		p.Tons PlanTons,
		p.BookTons,
		p.ReefTons PlanReefTons,
		p.BookReefTons,
		p.WasteTons PlanWasteTons,
		p.BookWasteTons,
		p.Grams PlanGrams,
		p.BookGrams,
		p.FL PlanFaceLength,
		p.BookFL BookFaceLength,
		p.ReefFL PlanReefFaceLength,
		p.WasteFL PlanWasteFaceLength,
		p.SW PlanStopeWidth,
		p.BookSW BookStopeWidth,
		p.CW PlanChannelWidth,
		p.BookCW BookChannelWidth,
		p.cmgt PlanCMGT,
		p.BookCMGT,
		p.GT PlanGT,
		p.BookGT,
		p.CubicMetres PlanCubicMetres,
		p.BookCubicMetres,
		p.Cubics PlanCubics,
		p.BookCubics,
		p.BookVolume,
		p.ReefCubics PlanReefCubics,
		p.BookReefVolume,
		p.WasteCubics PlanWasteCubics,
		p.BookWasteVolume,
		p.CubicTons PlanCubicTons,
		p.BookCubicTons,
		p.CubicDepth PlanCubicDepth,
		p.CubicGT PlanCubicGT,
		p.BookCubicGT,
		p.BookCubicGrams,
		p.BookSweeps,
		p.BookReSweeps,
		p.BookVamps,
		p.Backfill,
		p.BookReef BookReefWaste,
		p.MOCycle,
		p.MOCycleCube,
		p.AdjSqm,
		p.AdjCont,
		p.AdjTons,
		p.CheckMeasProb,
		p.MOFC,
		p.ABSCode,
		p.ABSNotes,
		p.ABSPicNo,
		p.ABSPrec ABSPerc,
		p.PegId,
		p.PegToFace,
		p.PegDist,
		p.BookOpenUp,
		p.BookSecM,
		p.BookCode,
		p.CheckSqm,
		p.SBossNotes,
		p.CausedLostBlast,
		p.CycleInput,
		p.ProblemID,
		pm.PlanCode,
		pm.OrgUnitAfternoon,
		pm.OrgUnitNight,
		pm.DHeight,
		pm.DWidth
	From Planning p
	Inner Join Section_Complete sc on p.Prodmonth = sc.Prodmonth And p.SectionId = sc.SectionID
	Inner Join PlanMonth pm on p.Prodmonth = pm.Prodmonth And p.SectionID = pm.Sectionid And p.WorkplaceID = pm.Workplaceid And p.PlanCode = pm.PlanCode
	Inner Join Workplace w on p.WorkplaceID = w.WorkplaceID
	Inner Join OreFlowEntities oe on oe.OreFlowID = w.OreFlowID
	Inner Join SecCal s	on s.ProdMonth = p.ProdMonth And s.SectionID = sc.Sectionid_1
	Inner Join CalType ct on ct.CalendarDate = p.CalendarDate and ct.CalendarCode = s.CalendarCode
	Where (IsNull(@FromMonth, 0) = 0 Or p.ProdMonth >= @FromMonth)
	And (IsNull(@CalendarDate, '') = '' Or p.Calendardate = @CalendarDate)
End