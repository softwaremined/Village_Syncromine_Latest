using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.GradeReport
{
    class clsGradeReportSettingsProperties : clsBase
    {
        private DateTime _ReportDate;
        public DateTime ReportDate { get { return _ReportDate; } set { _ReportDate = value; } }

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

        private Int32 _Paylimit;
        public Int32 Paylimit
        {
            get
            {
                return _Paylimit;
            }
            set
            {
                _Paylimit = value;
            }
        }

        private Int32 _ShiftsNo;
        public Int32 ShiftsNo
        {
            get
            {
                return _ShiftsNo;
            }
            set
            {
                _ShiftsNo = value;
            }
        }

        private bool _useShiftsDefault;
        private bool _useShifts;
        public bool useShiftsDefault
        {
            get
            {
                return _useShiftsDefault;
            }
            set
            {
                if (_useShiftsDefault != true && value == true)
                {
                    _useShiftsDefault = value;
                    _useShifts = false;
                }
            }
        }

        public bool useShifts
        {
            get
            {
                return _useShifts;
            }
            set
            {
                if (_useShifts != true && value == true)
                {
                    _useShifts = value;
                    _useShiftsDefault = false;
                }
            }
        }

        private Int32 _CutOffGrade;
        public Int32 CutOffGrade
        {
            get
            {
                return _CutOffGrade;
            }
            set
            {
                _CutOffGrade = value;
            }
        }

        private string _topPanels;

        public string TopPanels
        {
            get
            {
                return _topPanels;
            }
            set
            {

                _topPanels = value;

            }
        }
    }
}
