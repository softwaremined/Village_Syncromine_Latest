using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Planning.PrePlanning.CyclePlan
{
    /// <summary>
    /// Used to load the cycle codes for activity form DB
    /// </summary>
    public class CycleCodes
    {
        public string CycleCode { get; set; }
        public string Description { get; set; }
        public bool CanBlast { get; set; }
        public double DayCallPercentage { get; set; }
        public string CycleCodeAndDescription { get { return CycleCode + ":" + Description; } }
    }
}
