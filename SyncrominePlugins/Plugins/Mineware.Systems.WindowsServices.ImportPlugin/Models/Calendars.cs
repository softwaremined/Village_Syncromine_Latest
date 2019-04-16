using System;

namespace Mineware.Systems.WindowsServices.ImportPlugin.Models
{
	public class Calendars
	{
		public DateTime CalendarDate { get; set; }

		public string WorkingDay { get; set; }

		public string ProdMonth { get; set; }

		public string SectionId { get; set; }

		public string CalendarCode { get; set; }

		public DateTime BeginDate { get; set; }

		public DateTime EndDate { get; set; }

		public int TotalShifts { get; set; }

		public string Mine { get; set; }

	}
}