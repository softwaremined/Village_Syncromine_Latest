CREATE TABLE [dbo].[BookingRecon](
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) NOT NULL,
	[WorkplaceID] [varchar](12) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[PlanCode] [char](2) NOT NULL,
	[IsCubics] [char](1) NOT NULL,
	[ShiftDay] [numeric](5, 0) NULL,
	[SQM] [numeric](5, 0) NULL,
	[ReefSQM] [int] NULL,
	[WasteSQM] [int] NULL,
	[Metresadvance] [numeric](9, 3) NULL,
	[ReefAdv] [numeric](10, 3) NULL,
	[WasteAdv] [numeric](10, 3) NULL,
	[Tons] [numeric](10, 3) NULL,
	[ReefTons] [numeric](10, 3) NULL,
	[WasteTons] [numeric](10, 3) NULL,
	[Grams] [numeric](9, 3) NULL,
	[FL] [numeric](9, 3) NULL,
	[ReefFL] [int] NULL,
	[WasteFL] [int] NULL,
	[SW] [numeric](5, 0) NULL,
	[CW] [numeric](5, 0) NULL,
	[CMGT] [numeric](5, 0) NULL,
	[GT] [numeric](8, 2) NULL,
	[CubicMetres] [numeric](13, 3) NULL,
	[Cubics] [numeric](10, 3) NULL,
	[ReefCubics] [numeric](10, 3) NULL,
	[WasteCubics] [numeric](10, 3) NULL,
	[CubicTons] [numeric](10, 3) NULL,
	[CubicGrams] [numeric](10, 3) NULL,
	[CubicDepth] [numeric](10, 3) NULL,
	[CubicGT] [numeric](10, 3) NULL,
	[Backfill] [numeric](13, 3) NULL,
	[BookTons] [numeric](10, 3) NULL,
	[BookReefTons] [numeric](10, 3) NULL,
	[BookWasteTons] [numeric](10, 3) NULL,
	[BookGrams] [numeric](13, 3) NULL,
	[BookMetresadvance] [numeric](6, 3) NULL,
	[BookReefAdv] [numeric](13, 3) NULL,
	[BookWasteAdv] [numeric](13, 3) NULL,
	[BookSQM] [numeric](7, 0) NULL,
	[BookReefSQM] [numeric](7, 0) NULL,
	[BookWasteSQM] [numeric](7, 0) NULL,
	[BookVolume] [int] NULL,
	[BookReefVolume] [int] NULL,
	[BookWasteVolume] [int] NULL,
	[BookCubicmetres] [numeric](7, 0) NULL,
	[BookUpdatedate] [datetime] NULL,
	[BookReef] [varchar](2) NULL,
	[BookFL] [numeric](13, 3) NULL,
	[BookSW] [numeric](7, 0) NULL,
	[Bookcmgt] [int] NULL,
	[BookGT] [numeric](13, 3) NULL,
	[BookCW] [int] NULL,
	[MOCycle] [varchar](5) NULL,
	[MOCycleCube] [varchar](5) NULL,
	[BookCubicTons] [numeric](9, 3) NULL,
	[BookCubicGrams] [numeric](9, 3) NULL,
	[BookCubicGT] [numeric](9, 3) NULL,
	[AdjSqm] [float] NULL,
	[AdjCont] [float] NULL,
	[AdjTons] [float] NULL,
	[CheckMeasProb] [varchar](150) NULL,
	[MOFC] [numeric](7, 0) NULL,
	[ABSCode] [varchar](1) NULL,
	[ABSNotes] [varchar](255) NULL,
	[ABSPicNo] [varchar](3) NULL,
	[ABSPrec] [varchar](1) NULL,
	[BookCubics] [numeric](7, 0) NULL,
	[BookSweeps] [numeric](7, 0) NULL,
	[BookReSweeps] [numeric](7, 0) NULL,
	[BookVamps] [numeric](7, 0) NULL,
	[PegID] [varchar](50) NULL,
	[PegToFace] [decimal](18, 1) NULL,
	[PegDist] [decimal](18, 3) NULL,
	[BookOpenUp] [decimal](18, 1) NULL,
	[BookSecM] [numeric](7, 1) NULL,
	[BookCode] [varchar](4) NULL,
	[CheckSqm] [numeric](7, 0) NULL,
	[SBossNotes] [varchar](200) NULL,
	[CausedLostBlast] [char](1) NULL,
	[CycleInput] [varchar](10) NULL,
	[ProblemID] [varchar](30) NULL,
	[UserId] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[SectionID] ASC,
	[WorkplaceID] ASC,
	[Activity] ASC,
	[Calendardate] ASC,
	[PlanCode] ASC,
	[IsCubics] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

go
-- =============================================
-- Author:		Gawie Schneider
-- Create date: 2017/08/25
-- Description:	Select data for Booking Recon
-- =============================================
-- Changed Date		| Description
-- yyyy/MM/dd		| Some Description
-- =============================================
ALTER PROCEDURE [dbo].[sp_Select_BookingRecon] 
	@Prodmonth VarChar(7),
	@Section VarChar(30),
	@CalendarDate DateTime
AS
BEGIN	
	Select pm.ProdMonth, pm.SectionId, pm.WorkplaceId, pm.Activity ActivityCode, pm.IsCubics, wp.Description Workplace, 
		p.ShiftDay, p.CalendarDate, pm.Activity acttypeid, act.Description Activity,
		ProgFL = case when pm.Activity = 0 then Convert(Numeric(7,1), SUM(ISNULL(p.BookFL, 0))) else 0 end,
		ProgAdv = Convert(Numeric(7,1), SUM(ISNULL(p.BookMetresAdvance, 0))),
		ProgCubics = Convert(Numeric(10,2), SUM(ISNULL(p.BookCubicmetres, 0))),

		ReconFL = case when pm.Activity = 0 then Convert(Numeric(7,1), SUM(ISNULL(p.BookFL, 0)) + SUM(ISNULL(h.BookFL, 0))) else 0 end,
		ReconAdv = Convert(Numeric(7,1), SUM(ISNULL(p.BookMetresAdvance, 0)) + SUM(ISNULL(h.BookMetresAdvance, 0))),
		ReconCubics = Convert(Numeric(10,2), SUM(ISNULL(p.BookCubicmetres, 0)) + SUM(ISNULL(h.BookCubicmetres, 0))),

		Approved = cast(Max(CASE WHEN h.prodmonth IS NULL THEN 0 ELSE 1 END) as bit)
	FROM PLANMONTH pm
	Inner Join PLANNING p 
		on pm.Prodmonth = p.Prodmonth 
		And pm.Sectionid = pm.Sectionid 
		And pm.Activity = p.Activity 
		And pm.Workplaceid = p.WorkplaceID
		And pm.PlanCode = p.PlanCode
	Inner Join Code_Activity act on p.Activity = act.Activity
	Inner Join WORKPLACE wp on p.Workplaceid = wp.WorkplaceID
	Inner Join SECTION_COMPLETE sc on pm.Prodmonth = sc.Prodmonth
	Left Join dbo.BookingRecon h 
		on p.Prodmonth = h.PRODMONTH 
		And p.Sectionid = h.SECTIONID 
		And p.Workplaceid = h.WorkplaceID 
		And p.Activity = h.Activity 
		And p.PlanCode = h.PlanCode 
		And p.CalendarDate = h.Calendardate 
	WHERE pm.Prodmonth = @ProdMonth
	AND sc.SectionID_1 = @Section
	And sc.SectionID = pm.Sectionid
	AND p.Calendardate = @CalendarDate
	And pm.activity in (0,1) 
	And pm.PlanCode = 'MP'
	--And pm.Auth = 'Y' Need to confirm whether this is a requirement with Harmony or not
	--And pm.Locked = 1 Need to confirm whether this is a requirement with Harmony or not
	group by pm.Prodmonth, pm.Sectionid, pm.Workplaceid, p.ShiftDay, wp.Description, pm.Activity, act.Description, pm.IsCubics, p.Calendardate
END
go
-- =============================================
-- Author:		Gawie Schneider
-- Create date: 2017/08/24
-- Description:	Insert Or Update Booking Recon
-- =============================================
-- Changed Date		| Description
-- yyyy/MM/dd		| Some Description
-- =============================================
ALTER PROCEDURE [dbo].[sp_InsertUpd_BookingRecon] 
	@Prodmonth Numeric(7),
	@SectionID VarChar(10),
	@WorkplaceID VarChar(12),
	@Calendardate DateTime,
	@Activity Numeric(2),
	@ShiftDay Numeric(2),
	@ReconFL Numeric(10,1),
	@ReconAdv Numeric(10,2),
	@ReconCubics Numeric(10,2),
	@UserID VarChar(50)
AS
BEGIN
	Declare
		@SW Int,
		@FL Numeric(10,1),
		@ReefWaste int,
		@Density Numeric(10,3),
		@IsCubics VarChar(3),
		@PlanCode VarChar(3),
		@BookTons Numeric(10,3),
		@BookReefTons Numeric(10,3),
		@BookWasteTons Numeric(10,3),
		@BookReefAdv Numeric(13,3),
		@BookWasteAdv Numeric(13,3),
		@BookSQM Numeric(7,0),
		@BookReefSQM Numeric(7,0),
		@BookWasteSQM Numeric(7,0),
		@BookUpdatedate DateTime,
		@ReconSQM Numeric(7,0),
		@CubicTons Numeric(10,3)

	--@Prodmonth Numeric(7),
	--@SectionID VarChar(10),
	--@WorkplaceID VarChar(12),
	--@Calendardate DateTime,
	--@Activity Numeric(2),
	--@ShiftDay Numeric(2),
	--@ReconFL Numeric(10,1),
	--@ReconAdv Numeric(10,2),
	--@ReconCubics Numeric(10,2),
	--@UserID VarChar(50)
	--SET @Prodmonth = '201709'
	--SET @SectionID = 'RECCHHB'
	--SET @WorkplaceID = 'RE010044'
	--SET @Calendardate = '2017/08/24 00:00:00'
	--SET @Activity = 1
	--SET @ShiftDay = 8
	--SET @ReconFL = 3.2
	--SET @ReconAdv = 4.2
	--SET @ReconCubics = 2.2
	--SET @UserID = 'Gawie'

	--Calculate ReconSQM for Stoping
	SET @ReconSQM = @ReconAdv * @ReconFL;

	SELECT @SW = a.SW, @FL = a.FL,	@Density = c.density, @ReefWaste = case when b.ReefWaste = 'R' then 0 else 1 end,	@IsCubics = a.IsCubics,	@PlanCode = a.PlanCode
	FROM planmonth a 
	inner join WORKPLACE b on a.Workplaceid = b.WorkplaceID
	inner join vw_WP_density c on a.Workplaceid = c.WorkplaceID
	WHERE Prodmonth = @Prodmonth 
	AND Sectionid = @SectionID 
	AND a.Workplaceid = @WorkplaceID 
	AND plancode = 'MP'

	SELECT @BookTons = case 
						when @Activity = 0 then @ReconSQM*@SW/100*@Density
						when @Activity = 1 then @ReconAdv*@FL*@SW/100*@Density
					else 0 end,
			@BookReefTons = case 
					when @Activity = 0 and @ReefWaste = 0 then @ReconSQM*@SW/100*@Density
					when @Activity = 1 and @ReefWaste = 0 then @ReconAdv*@FL*@SW/100*@Density
				else 0 end,
			@BookWasteTons = case 
					when @Activity = 0 and @ReefWaste = 1 then @ReconSQM*@SW/100*@Density
					when @Activity = 1 and @ReefWaste = 1 then @ReconAdv*@FL*@SW/100*@Density
				else 0 end,
			@BookReefAdv = case 
					when @Activity = 0 and @ReefWaste = 0 then @ReconAdv
					when @Activity = 1 and @ReefWaste = 0 then @ReconAdv
				else 0 end,
			@BookWasteAdv= case 
					when @Activity = 0 and @ReefWaste = 1 then @ReconAdv
					when @Activity = 1 and @ReefWaste = 1 then @ReconAdv
				else 0 end,
			@BookSQM = case when @Activity = 0 then @ReconSQM else 0 end,
			@BookReefSQM = case when @Activity = 0 and @ReefWaste = 0 then @ReconSQM else 0 end,
			@BookWasteSQM = case when @Activity = 0 and @ReefWaste = 1 then @ReconSQM else 0 end,
			@BookUpdatedate = GetDate(),
			@CubicTons = @Density * @ReconCubics

	--Insert / Update using Merge
	MERGE BookingRecon recon
	USING (SELECT @Prodmonth Prodmonth,
				@SectionID SectionID,
				@WorkplaceID WorkplaceID,
				@Activity Activity,
				@Calendardate Calendardate,
				@PlanCode PlanCode,
				@IsCubics IsCubics,
				@ShiftDay ShiftDay,
				@BookTons BookTons,
				@BookReefTons BookReefTons,
				@BookWasteTons BookWasteTons,
				@ReconAdv BookMetresadvance,
				@BookReefAdv BookReefAdv,
				@BookWasteAdv BookWasteAdv,
				@BookSQM BookSQM,
				@BookReefSQM BookReefSQM,
				@BookWasteSQM BookWasteSQM,
				@BookUpdatedate BookUpdatedate
	) match
		ON recon.[Prodmonth] = match.[Prodmonth]
		AND recon.SectionID = match.SectionID
		AND recon.WorkplaceID = match.WorkplaceID
		AND recon.Activity = match.Activity
		AND recon.Calendardate = match.Calendardate
	WHEN MATCHED THEN
		--Only Update what is required
		UPDATE SET recon.BookTons = match.BookTons,
					recon.BookReefTons = match.BookReefTons,
					recon.BookWasteTons = match.BookWasteTons,
					recon.BookMetresAdvance = match.BookMetresAdvance,
					recon.BookReefAdv = match.BookReefAdv,
					recon.BookWasteAdv = match.BookWasteAdv,
					recon.BookSQM = match.BookSQM,
					recon.BookReefSQM = match.BookReefSQM,
					recon.BookUpdateDate = match.BookUpdateDate,
					recon.BookCubicmetres = @ReconCubics,
					recon.BookFL = @ReconFL
	WHEN NOT MATCHED THEN
	INSERT 
        --Insert all values if not found
		VALUES (@Prodmonth
			,@SectionID
			,@WorkplaceID
			,@Activity
			,@Calendardate
			,@PlanCode
			,@IsCubics
			,@ShiftDay

			--Planning Properties
			,0 --SQM
			,0 --ReefSQM
			,0 --WasteSQM
			,0 --Metresadvance
			,0 --ReefAdv
			,0 --WasteAdv
			,0 --Tons
			,0 --ReefTons
			,0 --WasteTons
			,0 --Grams
			,0 --FL
			,0 --ReefFL
			,0 --WasteFL
			,0 --SW
			,0 --CW
			,0 --CMGT
			,0 --GT
			,0 --CubicMetres
			,0 --Cubics
			,0 --ReefCubics
			,0 --WasteCubics
			,0 --CubicTons
			,0 --CubicGrams
			,0 --CubicDepth
			,0 --CubicGT
			,0 --Backfill

			--Booking Properties
			,@BookTons --BookTons
			,@BookReefTons --BookReefTons
			,@BookWasteTons --BookWasteTons
			,0 --BookGrams
			,@ReconAdv --BookMetresadvance
			,@BookReefAdv --BookReefAdv
			,@BookWasteAdv --BookWasteAdv
			,@BookSQM --BookSQM
			,@BookReefSQM --BookReefSQM
			,@BookWasteSQM --BookWasteSQM
			,0 --BookVolume
			,0 --BookReefVolume
			,0 --BookWasteVolume
			,@ReconCubics --BookCubicmetres
			,@BookUpdatedate --BookUpdatedate
			,'' --BookReef
			,@ReconFL --BookFL
			,0 --BookSW
			,0 --Bookcmgt
			,0 --BookGT
			,0 --BookCW
			,'' --MOCycle
			,'' --MOCycleCube
			,@CubicTons --BookCubicTons
			,0 --BookCubicGrams
			,0 --BookCubicGT
			,0 --AdjSqm
			,0 --AdjCont
			,0 --AdjTons
			,'' --CheckMeasProb
			,0 --MOFC
			,'' --ABSCode
			,'' --ABSNotes
			,'' --ABSPicNo
			,'' --ABSPrec
			,0 --BookCubics
			,0 --BookSweeps
			,0 --BookReSweeps
			,0 --BookVamps
			,'' --PegID
			,0 --PegToFace
			,0 --PegDist
			,0 --BookOpenUp
			,0 --BookSecM
			,'' --BookCode
			,0 --CheckSqm
			,'' --SBossNotes
			,'' --CausedLostBlast
			,'' --CycleInput
			,'' --ProblemID
			,@UserId --UserId
			);
END
