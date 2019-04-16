using System;
using System.Collections.Generic;
using Mineware.Systems.WindowsServices.ImportPlugin.Attributes;

namespace Mineware.Systems.WindowsServices.ImportPlugin.Models
{
	public class PlanBookValues
	{
		public string Operation { get; set; }

		public DateTime Calendardate { get; set; }

		public decimal ProdMonth { get; set; }

		public string SectionId { get; set; }

		public string SectionName { get; set; }

		public string SectionId_1 { get; set; }

		public string SectionName_1 { get; set; }

		public string SectionId_2 { get; set; }

		public string SectionName_2 { get; set; }

		public string SectionId_3 { get; set; }

		public string SectionName_3 { get; set; }

		public string SectionId_4 { get; set; }

		public string SectionName_4 { get; set; }

		public string SectionId_5 { get; set; }

		public string SectionName_5 { get; set; }

		public string WorkplaceId { get; set; }

		public string WorkplaceDescription { get; set; }

		public string WorkplaceReefWaste { get; set; }

		public string WorkplaceGridCode { get; set; }

		public string WorkplaceDivisionCode { get; set; }

		public string LevelNumber { get; set; }

		public decimal? ShiftDay { get; set; }

		public string WorkingDay { get; set; }

		public decimal ActivityCode { get; set; }

		public string ActivityDesc { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Sqm")]
		public decimal? PlanSqm { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Sqm")]
		public decimal? BookSqm { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Reef Sqm")]
		public int? PlanReefSqm { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Stope Reef Sqm")]
		public decimal? BookReefSqm { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Waste Sqm")]
		public int? PlanWasteSqm { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Stope Waste Sqm")]
		public decimal? BookWasteSqm { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Face Advance")]
		public decimal? PlanMetresAdvance { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Stope Face Advance")]
		public decimal? BookMetresAdvance { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Face Advance Reef")]
		public decimal? PlanReefAdv { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Stope Face Advance Reef")]
		public decimal? BookReefAdv { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Face Advance Waste")]
		public decimal? PlanWasteAdv { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Stope Face Advance Waste")]
		public decimal? BookWasteAdv { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Tons")]
		public decimal? PlanTons { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Tons")]
		public decimal? BookTons { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Reef Tons", DevUnitOfMeasure = "Dev Reef Tons")]
		public decimal? PlanReefTons { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Stope Reef Tons", DevUnitOfMeasure = "Dev Reef Tons")]
		public decimal? BookReefTons { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Waste Tons", DevUnitOfMeasure = "Dev Waste Tons")]
		public decimal? PlanWasteTons { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Stope Waste Tons", DevUnitOfMeasure = "Dev Waste Tons")]
		public decimal? BookWasteTons { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Content")]
		public decimal? PlanGrams { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Stope Content")]
		public decimal? BookGrams { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Face Length")]
		public decimal? PlanFaceLength { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Stope Face Length")]
		public decimal? BookFaceLength { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Reef Face Length")]
		public int? PlanReefFaceLength { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "Stope Waste Face Length")]
		public int? PlanWasteFaceLength { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "SW")]
		public decimal? PlanStopeWidth { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "SW")]
		public decimal? BookStopeWidth { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "CW")]
		public decimal? PlanChannelWidth { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "CW")]
		public int? BookChannelWidth { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "cmg/t")]
		public decimal? PlanCMGT { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "cmg/t")]
		public int? BookCMGT { get; set; }

		[Unit(Version = "Dynamic Plan", AlternateVersion = "Locked Plan", UnitOfMeasure = "g/t", DevUnitOfMeasure = "On Reef Development Value (g/t)")]
		public decimal? PlanGT { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "g/t")]
		public decimal? BookGT { get; set; }

		public decimal? PlanCubicMetres { get; set; }

		public decimal? BookCubicMetres { get; set; }

		public decimal? PlanCubics { get; set; }

		public decimal? BookCubics { get; set; }

		public int? BookVolume { get; set; }

		public decimal? PlanReefCubics { get; set; }

		public int? BookReefVolume { get; set; }

		public decimal? PlanWasteCubics { get; set; }

		public int? BookWasteVolume { get; set; }

		public decimal? PlanCubicTons { get; set; }

		public decimal? BookCubicTons { get; set; }

		public decimal? PlanCubicDepth { get; set; }

		public decimal? PlanCubicGT { get; set; }

		public decimal? BookCubicGT { get; set; }

		public decimal? BookCubicGrams { get; set; }

		public decimal? BookSweeps { get; set; }

		public decimal? BookReSweeps { get; set; }

		public decimal? BookVamps { get; set; }

		public decimal? Backfill { get; set; }

		public string BookReefWaste { get; set; }

		public string MOCycle { get; set; }

		public string MOCycleCube { get; set; }

		public double? AdjSqm { get; set; }

		public double? AdjCont { get; set; }

		public double? AdjTons { get; set; }

		public string CheckMeasProb { get; set; }

		public decimal? MOFC { get; set; }

		public string ABSCode { get; set; }

		public string ABSNotes { get; set; }

		public string ABSPicNo { get; set; }

		public string ABSPerc { get; set; }

		public string PegID { get; set; }

		public decimal? PegToFace { get; set; }

		public decimal? PegDist { get; set; }

		public decimal? BookOpenUp { get; set; }

		public decimal? BookSecM { get; set; }

		public string BookCode { get; set; }

		public decimal? CheckSqm { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Problem Book SBoss Notes")]
		public string SBossNotes { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Problem Book Caused Lost Blast")]
		public string CausedLostBlast { get; set; }

		public string CycleInput { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Problem Book ID")]
		public string ProblemID { get; set; }

		public string PlanCode { get; set; }

		public string OrgUnitDay { get; set; }

		public string OrgUnitAfternoon { get; set; }

		public string OrgUnitNight { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Problem Book Group Code")]
		public string ProblemGroupCode { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Problem Book HQCat")]
		public string HQCat { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Problem Description")]
		public string ProblemDescription { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Problem Book Lost Sqm")]
		public decimal? LostSqm { get; set; }

		[Unit(Version = "Booked", UnitOfMeasure = "Problem Book Lost Metres")]
		public decimal? LostMetres { get; set; }
	}
}