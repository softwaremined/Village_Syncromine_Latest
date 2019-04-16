using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mineware.Systems.HarmonyPAS.Controls;
using Mineware.Systems.HarmonyPAS.Controls.BookingsABS;

namespace Mineware.Systems.Harmony.Tests
{
	[TestClass]
	public class BookingTests
	{
		[TestMethod]
		public void BookingReconSelectTest()
		{
			var booking = new clsBookingsABS();

			booking.setConnectionString("server=Gawie-MW\\SQLEXPRESS ;DataBase=PAS_DNK_Syncromine ;user id=mineware ; password=corialanus;");

			var meh = booking.LoadBookingRecon("201709", "RECCHHB", "2017/08/24");
		}

		[TestMethod]
		public void BookingReconInsertTest()
		{
			var booking = new clsBookingsABS();

			booking.setConnectionString("server=Gawie-MW\\SQLEXPRESS ;DataBase=PAS_DNK_Syncromine ;user id=mineware ; password=corialanus;");

			var bookingModels = booking.LoadBookingRecon("201709", "RECCHHB", "2017/08/24").ToList();
			var item = bookingModels.FirstOrDefault();
			item.ReconFaceLength = Convert.ToDecimal(3.2);
			item.ReconAdvance = Convert.ToDecimal(7.2);
			item.ReconCubics = Convert.ToDecimal(9.2);
			item.UserId = "Gawie";
			var meh = booking.SaveBookingRecon(bookingModels);
		}
	}
}