using System;

namespace Mineware.Systems.Reports.ProblemsReport
{
    class clsProblemsReportSettings
    {
        public string _DBTag;
        public string DBTag { get { return _DBTag; } set { _DBTag = value; } }
        public string _UserCurrentInfo;
        public string UserCurrentInfo { get { return _UserCurrentInfo; } set { _UserCurrentInfo = value; } }

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

        private string _TheType;

        public string TheType
        {
            get
            {
                return _TheType;
            }
            set
            {
                _TheType = value;
            }
        }


        private string _Prodmonth;

        public string Prodmonth
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

        private Boolean _IncludeGraphs;

        public Boolean IncludeGraphs
        {
            get
            {
                return _IncludeGraphs;
            }
            set
            {
                _IncludeGraphs = value;
            }
        }
    }
}
