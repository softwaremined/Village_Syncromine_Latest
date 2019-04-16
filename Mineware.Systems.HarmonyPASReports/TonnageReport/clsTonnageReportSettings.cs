using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Reports.TonnageReport
{
    class clsTonnageReportSettings
    {
        private DateTime _Prodmonth;
        private DateTime _StartDate;
        private DateTime _EndDate;

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

        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }

            set
            {
                _StartDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }

            set
            {
                _EndDate = value;
            }
        }

        private string _ProdMonthSelection;

        public string ProdMonthSelection
        {
            get
            {
                return _ProdMonthSelection;
            }
            set
            {
                _ProdMonthSelection = value;
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

        private string _GraphType;

        public string GraphType
        {
            get
            {
                return _GraphType;
            }
            set
            {
                _GraphType = value;
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
    }
}
