using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;

namespace Mineware.Systems.Reports.PlathondWallChartReport
{
    class PlathondWallChartReportSettingsPropertiesClass
    {
        private DateTime _prodmonth;

        public DateTime Prodmonth
        {
            get
            {
                return _prodmonth;
            }
            set
            {
                _prodmonth = value;
            }
        }

        private string _name;

        public string NAME
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private string _activity;

        public string Activity
        {
            get
            {
                return _activity;
            }
            set
            {
                _activity = value;
            }
        }
    }
}
