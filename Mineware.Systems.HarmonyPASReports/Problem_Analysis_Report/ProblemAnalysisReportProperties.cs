using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Reports.Problem_Analysis_Report
{
    class ProblemAnalysisReportProperties
    {
        private string _Period;

        public string Period
        {
            get
            {
                return _Period;
            }
            set
            {
                _Period = value;
            }
        }

        private DateTime _FromProdmonth;

        public DateTime FromProdmonth
        {
            get
            {
                return _FromProdmonth;
            }
            set
            {
                _FromProdmonth = value;
            }
        }

        private DateTime _ToProdmonth;

        public DateTime ToProdmonth
        {
            get
            {
                return _ToProdmonth;
            }
            set
            {
                _ToProdmonth = value;
            }
        }

        private DateTime _FromDate;

        public DateTime FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                _FromDate = value;
            }
        }

        private DateTime _ToDate;

        public DateTime ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                _ToDate = value;
            }
        }

        private string _Sections;

        public string Sections
        {
            get
            {
                return _Sections;
            }
            set
            {
                _Sections = value;
            }
        }

        public event UpdateSumOn UpdateSumOnRequest;

        protected void OnUpdateSumOnRequest(UpdateSumOnArg e)
        {
            if (UpdateSumOnRequest != null)
            {
                UpdateSumOnRequest(this, e);
            }
        }

        public delegate void UpdateSumOn(object sender, UpdateSumOnArg e);

        public class UpdateSumOnArg : EventArgs
        {

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

        private string _NAME;

        public string NAME
        {
            get
            {
                return _NAME;
            }
            set
            {
                _NAME = value;
                UpdateSumOnArg e = new UpdateSumOnArg();
                OnUpdateSumOnRequest(e);
            }
        }

        private string _SectionSelect;

        public string SectionSelect
        {
            get
            {
                return _SectionSelect;
            }
            set
            {
                _SectionSelect = value;
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

        private bool _Details;

        public bool Details
        {
            get
            {
                return _Details;
            }
            set
            {
                _Details = value;
            }
        }

        private bool _DetailsGraph;

        public bool DetailsGraph
        {
            get
            {
                return _DetailsGraph;
            }
            set
            {
                _DetailsGraph = value;
            }
        }

        private bool _TrendGraph;

        public bool TrendGraph
        {
            get
            {
                return _TrendGraph;
            }
            set
            {
                _TrendGraph = value;
            }
        }


        private bool _PerShaft;

        public bool PerShaft 
        { 
            get 
            {
                return _PerShaft; 
            } 
            set 
            {
                _PerShaft = value; 
            } 
        }

        private bool _LostBlasts;

        public bool LostBlasts
        {
            get
            {
                return _LostBlasts;
            }
            set
            {
                _LostBlasts = value;
            }
        }

        private string _GraphInfo;

        public string GraphInfo
        {
            get
            {
                return _GraphInfo;
            }
            set
            {
                _GraphInfo = value;
            }
        }

        private string _Available;

        public string Available
        {
            get
            {
                return _Available;
            }
            set
            {
                _Available = value;
            }
        }
    }
}
