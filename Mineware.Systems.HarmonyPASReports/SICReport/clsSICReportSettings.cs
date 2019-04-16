using System;

namespace Mineware.Systems.Reports.SICReport
{
    public class clsSICReportSettings
    {
        public string _DBTag;
        public string DBTag { get { return _DBTag; } set { _DBTag = value; } }
        public string _UserCurrentInfo;
        public string UserCurrentInfo { get { return _UserCurrentInfo; } set { _UserCurrentInfo = value; } }

        private DateTime _CalendarDate;
        public DateTime CalendarDate { get { return _CalendarDate; } set { _CalendarDate = value; } }

        private string _SectionID;
        public string SectionID { get { return _SectionID; } set { _SectionID = value; } }
        private string _ReportType;
        public string ReportType { get { return _ReportType; } set { _ReportType = value; } }
        private string _OreflowID;
        public string OreflowID { get { return _OreflowID; } set { _OreflowID = value; } }
    }
}
