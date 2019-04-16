using Mineware.Systems.Planning.PrePlanning.CyclePlan;
using Mineware.Systems.GlobalExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Planning.PrePlanning
{

    /// <summary>
    /// T Main Cycle planning class
    /// Created By : Dolf van en Berg 
    /// Date Created : 2019/02/27
    /// </summary>
    public class PlanningCycle
    {
        public PlanningCycle()
        {
            planningCycleData = new List<PlanningCycleData>();
        }


        public List<PlanningCycleData> planningCycleData { get; set; }
        public static List<CycleCodes> cycleCodes { get; set; }


        /// <summary>
        /// Sets the cycle codes for activity 
        /// </summary>
        /// <param name="CycleCodes">Data from table [CODE_CYCLE] </param>
        public void SetCycleCodes(DataTable CycleCodes)
        {
            cycleCodes = new List<CycleCodes>();
            var cycleCodesTable = CycleCodes.GetDataFromDataTable<CycleCodes>();
            foreach (var a in cycleCodesTable)
            {
                cycleCodes.Add(a);
            }

        }

        /// <summary>
        /// Loads cycle data into cycleData Table 
        /// </summary>
        /// <param name="theData"></param>
        public void LoadPlanningCycleData(DataTable theData)
        {
            if (planningCycleData == null)
            {
                planningCycleData = new List<PlanningCycleData>();
            }
            var cycleDBData = theData.GetDataFromDataTable<Cycle>();
            var workPlaceList = cycleDBData.GroupBy(test => test.Workplaceid).Select(a => a)
                                   .ToList();

            foreach (var wp in workPlaceList)
            {
                var wpData = cycleDBData.Where(a => a.Workplaceid == wp.Key).ToList();
                var currentWorkplace = cycleDBData.Where(a => a.Workplaceid == wp.Key).FirstOrDefault();
                PlanningCycleData cycleData = new PlanningCycleData();
                cycleData.WorkplaceID = wp.Key;
                cycleData.TotalShifts = currentWorkplace.TotalShifts;

                // needed to add this to accommodate for a first shift that is a off day 
                foreach(var DailyCycleCallVal in wpData)
                {
                    if(DailyCycleCallVal.SQMCycle != 0)
                    {
                        cycleData.DailyCycleCall = DailyCycleCallVal.SQMCycle;
                        break;
                    }
                }

                cycleData.CycleCall = currentWorkplace.SQM;
                cycleData.FaceLength = currentWorkplace.FL;
                cycleData.MonthCall = currentWorkplace.MonthlyTotalSQM;
                cycleData.planningCycleDailyData = new System.ComponentModel.BindingList<PlanningCycleDailyData>();

                PlanningCycleDailyData dateRow = new PlanningCycleDailyData();
                dateRow.RowName = "Date";
                dateRow.WorkplaceID = wp.Key;
                int rowCount = 1;
                foreach (var d in wpData)
                {
                    dateRow.setValue(rowCount, d.CalendarDate.ToString("d/M"));
                    rowCount++;
                }
                cycleData.planningCycleDailyData.Add(dateRow);

                PlanningCycleDailyData dateLongRow = new PlanningCycleDailyData();
                dateLongRow.RowName = "DateLong";
                dateLongRow.WorkplaceID = wp.Key;
                rowCount = 1;
                foreach (var d in wpData)
                {
                    dateLongRow.setValue(rowCount, d.CalendarDate.ToString());
                    rowCount++;
                }
                cycleData.planningCycleDailyData.Add(dateLongRow);

                PlanningCycleDailyData shiftRow = new PlanningCycleDailyData();
                shiftRow.RowName = "Shift";
                shiftRow.WorkplaceID = wp.Key;
                rowCount = 1;
                foreach (var d in wpData)
                {
                    shiftRow.setValue(rowCount, d.ShiftDay.ToString());
                    rowCount++;
                }
                cycleData.TotalDays = rowCount;
                cycleData.planningCycleDailyData.Add(shiftRow);

                PlanningCycleDailyData AdvBlastRow = new PlanningCycleDailyData();
                AdvBlastRow.RowName = "AdvBlast";
                AdvBlastRow.WorkplaceID = wp.Key;
                rowCount = 1;
                foreach (var d in wpData)
                {
                    AdvBlastRow.setValue(rowCount, d.AdvBlast.ToString());
                    rowCount++;
                }
                cycleData.TotalDays = rowCount;
                cycleData.planningCycleDailyData.Add(AdvBlastRow);

                PlanningCycleDailyData WorkingDaysRow = new PlanningCycleDailyData();
                WorkingDaysRow.RowName = "WorkingDays";
                WorkingDaysRow.WorkplaceID = wp.Key;
                rowCount = 1;
                foreach (var d in wpData)
                {
                    if (d.Workingday != null)
                    {
                        WorkingDaysRow.setValue(rowCount, d.Workingday.ToString());
                    }
                    rowCount++;
                }
                cycleData.TotalDays = rowCount;
                cycleData.planningCycleDailyData.Add(WorkingDaysRow);



                PlanningCycleDailyData defaultCode = new PlanningCycleDailyData();
                defaultCode.RowName = "Default Code";
                defaultCode.WorkplaceID = wp.Key;
                rowCount = 1;
                foreach (var d in wpData)
                {
                    defaultCode.setValue(rowCount, d.DefaultValue);
                    rowCount++;
                }
               
                cycleData.planningCycleDailyData.Add(defaultCode);

                PlanningCycleDailyData defaultValue = new PlanningCycleDailyData();
                defaultValue.RowName = "Default Value";
                defaultValue.WorkplaceID = wp.Key;
                rowCount = 1;
                foreach (var d in wpData)
                {
                    defaultValue.setValue(rowCount, d.DailyDefaultBlastValue.ToString());
                    rowCount++;
                }

                cycleData.planningCycleDailyData.Add(defaultValue);

                PlanningCycleDailyData moCycleCode = new PlanningCycleDailyData();
                moCycleCode.RowName = "Planned Code";
                moCycleCode.WorkplaceID = wp.Key;
                rowCount = 1;
                foreach (var d in wpData)
                {
                    moCycleCode.setValue(rowCount, d.MOCycle);
                    rowCount++;
                }

                cycleData.planningCycleDailyData.Add(moCycleCode);

                PlanningCycleDailyData moCyclePlan = new PlanningCycleDailyData();
                moCyclePlan.RowName = "Planned Value";
                moCyclePlan.WorkplaceID = wp.Key;
                rowCount = 1;
                foreach (var d in wpData)
                {

                    moCyclePlan.setValue(rowCount, d.SQMCycle.ToString());
                    rowCount++;
                }

                cycleData.planningCycleDailyData.Add(moCyclePlan);

                planningCycleData.Add(cycleData);

            }

        }
    }
}
