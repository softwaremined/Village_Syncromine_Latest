using Mineware.Systems.WindowsServices.ImportPlugin.Attributes;

namespace Mineware.Systems.WindowsServices.ImportPlugin.Models
{
	public class PlanMeasValues
	{
		public string Operation { get; set; }

		public decimal ProdMonth { get; set; }

		public string OrgUnitDay { get; set; }

		public string OrgUnitAfternoon { get; set; }

		public string OrgUnitNight { get; set; }

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

		public decimal ActivityCode { get; set; }

		public string ActivityDesc { get; set; }

		public int SurveySequenceNo { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Reef m")]
		public string PlanReefAdv { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Reef m (W/C)")]
		public string PlanWcReefAdv { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Reef m (Cap)")]
		public string PlanCapReefAdv { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Waste m")]
		public string PlanWasteAdv { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Waste m (W/C)")]
		public string PlanWcWasteAdv { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Waste m (Cap)")]
		public string PlanCapWasteAdv { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "m")]
		public string MeasAdv { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Reef m")]
		public string MeasReefAdv { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Reef m (W/C)")]
		public string MeasWcReefAdv { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Reef m (Cap)")]
		public string MeasCapReefAdv { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Waste m")]
		public string MeasWasteAdv { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Waste m (W/C)")]
		public string MeasWcWasteAdv { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Waste m (Cap)")]
		public string MeasCapWasteAdv { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "On Reef Development Value (g/t)")]
		public string PlanDevReefValue { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "On Reef Development Value (g/t)")]
		public string MeasDevReefValue { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "On Reef Development Height (m)")]
		public string PlanDevReefHeight { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "On Reef Development Height (m)")]
		public string MeasDevReefHeight { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "On Reef Development Width (m)")]
		public string PlanDevReefWidth { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "On Reef Development Width (m)")]
		public string MeasDevReefWidth { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Stope Reef Sqm")]
		public string PlanStopeReefSqm { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Stope Waste Sqm")]
		public string PlanStopeWasteSqm { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Sqm")]
		public string MeasSqmTotal { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Reef Sqm")]
		public string MeasReefSqm { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Waste Sqm")]
		public string MeasWasteSqm { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Stope Reef Sqm")]
		public string MeasStopeReefSqm { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Stope Waste Sqm")]
		public string MeasStopeWasteSqm { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Stope Reef Tons")]
		public string MeasStopeReefTons { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Stope Waste Tons")]
		public string MeasStopeWasteTons { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Tons")]
		public string PlanTons { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Dev Reef Tons")]
		public string PlanDevReefTons { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Dev Waste Tons")]
		public string PlanDevWasteTons { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Tons")]
		public string MeasTons { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Dev Reef Tons")]
		public string MeasDevReefTons { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Dev Waste Tons")]
		public string MeasDevWasteTons { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Stope Reef Tons")]
		public string PlanStopeReefTons { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Stope Waste Tons")]
		public string PlanStopeWasteTons { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Face length")]
		public string PlanFL { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Stope Face length")]
		public string PlanStopeFL { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Face length")]
		public string MeasFL { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Stope Face length")]
		public string MeasStopeFL { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Content")]
		public string PlanContent { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Stope Content")]
		public string PlanStopeContent { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Content")]
		public string MeasTotalContent { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Stope Content")]
		public string MeasStopeContent { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "cmg/t")]
		public string PlanCmgt { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "cmg/t")]
		public string MeasCmgt { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Rock Density")]
		public string PlanDensity { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Rock Density")]
		public string MeasDensity { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "Stope Face Advance")]
		public string PlanStopeAdv { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "g/t")]
		public string PlanGt { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "g/t")]
		public string MeasGt { get; set; }

		[Unit(Version = "Locked Plan", UnitOfMeasure = "SW")]
		public string PlanSw { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "SWSQM")]
		public string MeasSwSqm { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Sweep SQM")]
		public string MeasCleanSqm { get; set; }

		[Unit(Version = "Measured", UnitOfMeasure = "Sweep Tons")]
		public string MeasCleanTons { get; set; }
	}
}