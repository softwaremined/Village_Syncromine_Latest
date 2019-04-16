using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Reports.StandardCRMReport
{
  public   class clsStandardCRMReportSettings
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
        
    }
}
