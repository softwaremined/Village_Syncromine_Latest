using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.HarmonyPASReports.Daily_Blast_Report
{
    class DailyBlastSettingsProperties
    {
        private DateTime _ReportDate;

        public DateTime ReportDate
        {
            get
            {
                return _ReportDate;
            }
            set
            {
                _ReportDate = value;
            }
        }
    }
}
