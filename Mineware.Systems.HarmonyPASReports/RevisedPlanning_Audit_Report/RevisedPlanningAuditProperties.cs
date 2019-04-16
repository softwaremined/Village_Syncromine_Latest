using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Reports.RevisedPlanning_Audit_Report
{
    class RevisedPlanningAuditProperties
    {
        private DateTime _FromProdmonth;
        public DateTime FromProdmonth
        {
            get
            {
                return _FromProdmonth;
            }
            set
            {
                _FromProdmonth = value;
            }
        }

        private DateTime _ToProdmonth;
        public DateTime ToProdmonth
        {
            get
            {
                return _ToProdmonth;
            }
            set
            {
                _ToProdmonth = value;
            }
        }

        private string _sectionid;
        public string sectionid 
        { 
            get 
            { 
                return _sectionid; 
            } 
            set 
            { 
                _sectionid = value; 
            } 
        }

        private string _RevisedPlanningType;
        public string RevisedPlanningType 
        { 
            get 
            { 
                return _RevisedPlanningType; 
            } 
            set 
            { 
                _RevisedPlanningType = value; 
            } 
        }
    }
}
