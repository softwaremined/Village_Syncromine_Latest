using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Reports.WorstPerformers
{
    class clsWorstPerformers
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

        private int _Activity;
        public int Activity
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


        private string _Crew;
        public string Crew
        {
            get
            {
                return _Crew;
            }
            set
            {
                _Crew = value;
            }
        }

    }
}
