using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Reports.BonusReport
{
    class clsBonusReportSettings
    {
        private DateTime _Prodmonth;

        public DateTime Prodmonth
        {
            get
            {
                return _Prodmonth;
            }
            set
            {
                _Prodmonth = value;
            }
        }

        private string _SectionID;

        public string SectionID
        {
            get
            {
                return _SectionID;
            }
            set
            {
                _SectionID = value;
            }
        }

     

        private string _Activity;

        public string Activity
        {
            get
            {
                return _Activity;
            }
            set
            {
                _Activity = value;
            }
        }
    }
}
