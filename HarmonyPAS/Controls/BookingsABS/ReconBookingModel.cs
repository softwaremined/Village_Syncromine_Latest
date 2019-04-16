using System.ComponentModel.DataAnnotations;
using Mineware.Systems.GlobalExtensions;

namespace Mineware.Systems.Production.Controls
{
    public class ReconBookingModel
	{
		[Data]
		public string ProdMonth { get; set; }

	    [Data]
		public string SectionId { get; set; }

	    [Data]
		public string WorkplaceId { get; set; }

		[Data]
        public string Calendardate { get; set; }

		[Data]
		public int ActivityCode { get; set; }

		[Data]
		public string Activity { get; set; }

		[Data]
		public int ShiftDay { get; set; }

		[Data]
        public string Workplace { get; set; }

	    [Data("ProgFL")]
		[Display(Name = "Prog FL")]
        public decimal ProgressiveFaceLength { get; set; }

	    [Data("ReconFL")]
		[Display(Name = "Recon FL")]
        public decimal ReconFaceLength { get; set; }

	    [Data("ProgAdv")]
		[Display(Name = "Prog Adv")]
        public decimal ProgressiveAdvance { get; set; }

	    [Data("ReconAdv")]
		[Display(Name = "Recon Adv")]
        public decimal ReconAdvance { get; set; }

	    [Data("ProgCubics")]
		[Display(Name = "Prog Cubics")]
        public decimal ProgressiveCubics { get; set; }

	    [Data("ReconCubics")]
		[Display(Name = "Recon Cubics")]
        public decimal ReconCubics { get; set; }

        [Data("MOFC")]
        [Display(Name = "MOFC")]
        public decimal MOFC { get; set; }


        [Data("MOComment")]
        [Display(Name = "MOComment")]
        public string MOComment { get; set; }


        [Data("MonthPlan")]
        [Display(Name = "MonthPlan")]
        public decimal MonthPlan { get; set; }


        [Data]
        public bool Approved { get; set; }


        [Data("BlastBar")]
        [Display(Name = "BlastBar")]
        public string BlastBar { get; set; }

        public string UserId { get; set; }
	}
}