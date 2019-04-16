using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Reports.TopPanelsReport
{
    class clsTopPanelsReportSettings
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


        private string _Type;

        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }


        private string _Meas;

        public string Meas
        {
            get
            {
                return _Meas;
            }
            set
            {
                _Meas = value;
            }
        }





        private string _ReportType;

        public string ReportType
        {
            get
            {
                return _ReportType;
            }
            set
            {
                _ReportType = value;
            }
        }
    }
}
