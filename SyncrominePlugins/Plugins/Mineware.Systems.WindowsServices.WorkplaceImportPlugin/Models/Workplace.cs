using System;
using Mineware.Systems.WindowsServices.Attributes;

namespace Mineware.Systems.WindowsServices.WorkplaceImportPlugin.Models
{
	public class Workplace
	{
		public string Operation { get; set; }

		[Data("Workplace_ID")]
		public string WorkplaceID { get; set; }

		[Data("Workplace_Name")]
		public string Description { get; set; }

		[Data("Code_Division")]
		public string DivisionCode { get; set; }

		[Data("Division_Description")]
		public string DivisionDescription { get; set; }

		[Data("Code_WP_Type")]
		public string TypeCode { get; set; }

		[Data("WP_Type_Description")]
		public string TypeDescription { get; set; }

		[Data("Code_Level")]
		public string OreFlowID { get; set; }

		[Data("Level_Number")]
		public string LevelNumber { get; set; }


		[Data("Level_Description")]
		public string LevelDescription { get; set; }


		[Data("Code_Grid")]
		public string GridCode { get; set; }


		[Data("Grid_Description")]
		public string GridDescription { get; set; }


		[Data("Code_Detail")]
		public string DetailCode { get; set; }


		[Data("Detail_Description")]
		public string DetailDescription { get; set; }


		[Data("Code_Number")]
		public string NumberCode { get; set; }


		[Data("Number_Description")]
		public string NumberDescription { get; set; }


		[Data("Code_Direction")]
		public string DirectionCode { get; set; }


		[Data("Direction_Description")]
		public string DirectionDescription { get; set; }


		[Data("Code_Description")]
		public string DescrCode { get; set; }


		[Data("Description_Description")]
		public string DescrDescription { get; set; }


		[Data("Code_Description_Number")]
		public string DescrNoCode { get; set; }


		[Data("Description_Number_Description")]
		public string DescrNoDescription { get; set; }


		[Data("Code_Reef")]
		public decimal? ReefID { get; set; }


		[Data("Reef_Name")]
		public string ReefName { get; set; }


		[Data("Reef_Description")]
		public string ReefDescription { get; set; }

		public string Converted { get; set; }

		
		public string WPStatus { get; set; }


		[Data("Workplace_Status")]
		public string WPStatusDescription { get; set; }


		[Data("Creation_Date")]
		public DateTime? CreationDate { get; set; }

		public string Classification { get; set; }


		[Data("Classification_Indicator")]
		public string ClassificationDescription { get; set; }


		[Data("Old_Workplace_ID")]
		public string OldWorkplaceid { get; set; }

		public decimal? EndTypeID { get; set; }

		public string EndTypeDesc { get; set; }

		public decimal? Activity { get; set; }

		public string ReefWaste { get; set; }

		public string StopingCode { get; set; }

		public decimal? EndWidth { get; set; }

		public decimal? EndHeight { get; set; }

		public string Line { get; set; }

		public string Priority { get; set; }

		public string Mech { get; set; }

		public string Cap { get; set; }

		public decimal? RiskRating { get; set; }

		public decimal? DefaultAdv { get; set; }

		public decimal? GMSIWPID { get; set; }

		public string Inactive { get; set; }

		public decimal? Density { get; set; }

		public decimal? BrokenRockDensity { get; set; }

		public int? SubSection { get; set; }

		public int? ProcessCode { get; set; }

		public string Userid { get; set; }

		public string BoxholeID { get; set; }

	}

}