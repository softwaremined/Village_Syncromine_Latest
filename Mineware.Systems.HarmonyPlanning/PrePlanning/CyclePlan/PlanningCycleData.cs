
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Mineware.Systems.Planning.PrePlanning
{
    /// <summary>
    /// This class holds all workplace info needed to generate cycle data 
    /// Created By : Dolf van en Berg 
    /// Date Created : 2019/02/27
    /// </summary>
    public class PlanningCycleData
    {
        private double _FaceLength;


        /// <summary>
        /// Populate Cycle data to a data table. Used when saving the data
        /// </summary>
        /// <param name="prodmonth"></param>
        /// <param name="IsCubics"></param>
        /// <param name="Activity"></param>
        /// <param name="sectionID"></param>
        /// <returns>DataTable with the correct structure to be saved to DB</returns>
        public DataTable GetCycleData(string prodmonth, string IsCubics, int Activity, string sectionID)
        {
            // Create the required data table 
            DataTable cycleData = new DataTable();
            cycleData.Columns.Add(new DataColumn("WorkplaceID", typeof(string)));
            cycleData.Columns.Add(new DataColumn("prodmonth", typeof(int)));
            cycleData.Columns.Add(new DataColumn("IsCubics", typeof(string)));
            cycleData.Columns.Add(new DataColumn("Activity", typeof(int)));
            cycleData.Columns.Add(new DataColumn("sectionID", typeof(string)));
            cycleData.Columns.Add(new DataColumn("Calendardate", typeof(DateTime)));
            cycleData.Columns.Add(new DataColumn("Shift", typeof(int)));
            cycleData.Columns.Add(new DataColumn("PlannedValue", typeof(double)));
            cycleData.Columns.Add(new DataColumn("PlannedCode", typeof(string)));
            cycleData.Columns.Add(new DataColumn("FL", typeof(double)));

            // get the rows for data that needs to be saved to DB
            var calDates = planningCycleDailyData.Where(a => a.RowName == "DateLong").FirstOrDefault();
            var planValues = planningCycleDailyData.Where(a => a.RowName == "Planned Value").FirstOrDefault();
            var planCodes = planningCycleDailyData.Where(a => a.RowName == "Planned Code").FirstOrDefault();
            var shiftDay = planningCycleDailyData.Where(a => a.RowName == "Shift").FirstOrDefault();
            // add the data to the data table 
            for (int k = 1; k <= TotalDays; k++)
            {
                if (calDates.getValue(k) != null)
                {
                    cycleData.Rows
                        .Add(new
                        Object[]
                        {
                            WorkplaceID,
                            prodmonth,
                            IsCubics,
                            Activity,
                            sectionID,
                            calDates.getValue(k).ToString(),
                            shiftDay.getValue(k),
                            planValues.getValue(k),
                            planCodes.getValue(k),
                            _FaceLength
                        });

                }
            }

            return cycleData;

        }

        /// <summary>
        /// Update the cycle based on the new FaceLength
        /// </summary>
        public void UpdateCycleData()
        {

            var rowAdvBlast = planningCycleDailyData.Where(a => a.RowName == "AdvBlast").FirstOrDefault();
            double AdvBlast = 0;
        
            int x = 1;
            do  // needed to add this to accommodate for a first shift that is a off day 
            {
                AdvBlast = Convert.ToDouble(rowAdvBlast.getValue(x));
                x++;
            } while (AdvBlast == 0);

            DailyCycleCall = Math.Round(FaceLength * AdvBlast, 0);

            var WorkingDayRow = planningCycleDailyData.Where(a => a.RowName == "WorkingDays").FirstOrDefault();
            var DefaultValueRow = planningCycleDailyData.Where(a => a.RowName == "Default Value").FirstOrDefault();
            var DefaultCodeRow = planningCycleDailyData.Where(a => a.RowName == "Default Code").FirstOrDefault();

            var PlannedValueRow = planningCycleDailyData.Where(a => a.RowName == "Planned Value").FirstOrDefault();
            var PlannedCodeRow = planningCycleDailyData.Where(a => a.RowName == "Planned Code").FirstOrDefault();
            double NewCycleCall = 0;
            for (int k = 1; k < TotalDays; k++)
            {
                var theCodeDefault = DefaultCodeRow.getValue(k);
                var theCodePlanned = PlannedCodeRow.getValue(k);
                var theCycleCodeDefault = PlanningCycle.cycleCodes.Where(a => a.CycleCode == theCodeDefault).FirstOrDefault();
                var theCycleCodePlanned = PlanningCycle.cycleCodes.Where(a => a.CycleCode == theCodePlanned).FirstOrDefault();
                var workingDay = WorkingDayRow.getValue(k);
                if (theCodeDefault != null && theCycleCodeDefault != null && theCycleCodeDefault.CanBlast && workingDay == "Y")
                {

                    DefaultValueRow.setValue(k, Convert.ToString(DailyCycleCall * theCycleCodeDefault.DayCallPercentage));
                }
                else
                {
                    DefaultValueRow.setValue(k, "0");
                }

                if (theCycleCodePlanned != null && theCycleCodePlanned.CanBlast && workingDay == "Y")
                {
                    NewCycleCall = NewCycleCall + (DailyCycleCall * theCycleCodePlanned.DayCallPercentage);
                    PlannedValueRow.setValue(k, Convert.ToString(DailyCycleCall * theCycleCodePlanned.DayCallPercentage));
                }
                else
                {
                    PlannedValueRow.setValue(k, "0");
                }

            }
            CycleCall = NewCycleCall;

        }

        /// <summary>
        /// The total cycle call for workplace 
        /// </summary>
        public double CycleCall { get; set; }
        /// <summary>
        /// The daily cycle call for workplace 
        /// </summary>
        public double DailyCycleCall { get; set; }


        public double FaceLength
        {
            get
            {
                return _FaceLength;
            }

            set
            {
                _FaceLength = value;
                if (planningCycleDailyData != null && planningCycleDailyData.Count > 0)
                {
                    UpdateCycleData();
                }
            }
        }


        /// <summary>
        /// Planned Month call 
        /// </summary>
        public double MonthCall { get; set; }
        /// <summary>
        /// The daily cycle data 
        /// </summary>
        public BindingList<PlanningCycleDailyData> planningCycleDailyData { get; set; }
        /// <summary>
        /// Total Days for prodmonth
        /// </summary>
        public int TotalDays { get; set; }
        /// <summary>
        /// Total working shifts for prodmonth
        /// </summary>
        public int TotalShifts { get; set; }

        public string WorkplaceID { get; set; }
    }
}
