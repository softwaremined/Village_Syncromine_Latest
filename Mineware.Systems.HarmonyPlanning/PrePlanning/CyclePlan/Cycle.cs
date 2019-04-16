using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Planning.PrePlanning.CyclePlan
{
    /// <summary>
    /// Used to load the cycle data for workplace from planmonth
    /// Created By : Dolf van en Berg 
    /// Date Created : 2019/02/27
    /// Neil Code
    /// New comment
    /// </summary>
    public class Cycle
    {
        public string Sectionid { get; set; }
        public string Workplaceid { get; set; }
        public double CycleSQMCall { get; set; }
        public double CycleReefSQMCall { get; set; }
        public double CycleWasteSQMCall { get; set; }
        public double FL { get; set; }
        public double MonthlyReefSQM { get; set; }
        public double MonthlyWatseSQM { get; set; }
        public double MonthlyTotalSQM { get; set; }
        public string CycleCode { get; set; }
        public int TotalShifts { get; set; }
        public DateTime CalendarDate { get; set; }
        public string Workingday { get; set; }
        public string CalendarCode { get; set; }
        public double SQMCycle { get; set; }
        public double SQM { get; set; }
        public double ReefSQMCycle { get; set; }
        public double WasteSQMCycle { get; set; }
        public string MOCycle { get; set; }
        public int ShiftDay { get; set; }
        public string DefaultName { get; set; }
        public double AdvBlast { get; set; }
        public string DefaultValue { get; set; }
        public double DailyDefaultBlastValue { get; set; }


    }
}
