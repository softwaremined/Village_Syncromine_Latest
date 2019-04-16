using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;

namespace Mineware.Systems.Reports.MODailyDetail
{
    class MODailyProductionSettingsProperties
    {
        private DateTime _ReportDate;
        public DateTime ReportDate { get { return _ReportDate; } set { _ReportDate = value; } }

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

        private string _Color;
        public string Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
            }
        }
    }
}
