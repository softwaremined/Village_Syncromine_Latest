using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Reports.CrewRanking
{
    class clsCrewRankingSettings
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

        private string _RatingBy;

        public string RatingBy
        {
            get
            {
                return _RatingBy;
            }
            set
            {
                _RatingBy = value;
            }
        }

        private string _From;

        public string From
        {
            get
            {
                return _From;
            }
            set
            {
                _From = value;
            }
        }

        private string _By;

        public string By
        {
            get
            {
                return _By;
            }
            set
            {
                _By = value;
            }
        }

        private string _OrderBy;

        public string OrderBy
        {
            get
            {
                return _OrderBy;
            }
            set
            {
                _OrderBy = value;
            }
        }
    }
}
