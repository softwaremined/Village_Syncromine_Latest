using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Reports.DuplicateSampleReport
{
  public   class clsDuplicateSampleReportSettings
    {

        private string _shaft;

        public string Shaft
        {
            get
            {
                return _shaft;
            }
            set
            {
                    _shaft = value;
            }
        }

        private string _amis;

        public string AMIS
        {
            get
            {
                return _amis;
            }
            set
            {
                _amis = value;
            }
        }
    }
}
