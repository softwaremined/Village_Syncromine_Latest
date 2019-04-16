using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Reports._6ShiftRecon
{
    class clsReportSettings
    {
        public string _DBTag;
        public string DBTag { get { return _DBTag; } set { _DBTag = value; } }
        public string _UserCurrentInfo;
        public string UserCurrentInfo { get { return _UserCurrentInfo; } set { _UserCurrentInfo = value; } }
    }
}
