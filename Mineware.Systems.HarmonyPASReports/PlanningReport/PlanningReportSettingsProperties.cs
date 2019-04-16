using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;

namespace Mineware.Systems.Reports.PlanningReport
{
    class PlanningReportSettingsProperties
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

        private string _Selection;

        public string Selection
        {
            get
            {
                return _Selection;
            }
            set
            {
                _Selection = value;
            }
        }

        private string _PlanType;

        public string PlanType
        {
            get
            {
                return _PlanType;
            }
            set
            {
                _PlanType = value;
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

        private string _PlanningGroup;

        public string PlanningGroup
        {
            get
            {
                return _PlanningGroup;
            }
            set
            {
                _PlanningGroup = value;
            }
        }


        private string _PlanningTypeGroup;

        public string PlanningTypeGroup
        {
            get
            {
                return _PlanningTypeGroup;
            }
            set
            {
                _PlanningTypeGroup = value;
            }
        }

    }
}
