using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.BandedGrid;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.Data;
using System.Net;
using System.Text;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using Mineware.Systems.Planning.PrePlanning;
using MWDataManager;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning
{
    public partial class ucPreplanning : ucBaseUserControl
    {
        public PlanningDefaults PlanningClass;
        private bool CycleChanged = false;
     public   DataTable tblPrePlanData = new DataTable();
        DataTable dt = new DataTable();
        DataTable sundry = new DataTable();
        DataTable sweepsvamps = new DataTable();
        DataTable depthrange = new DataTable();
        MWDataManager.clsDataAccess _CycleData = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _LabourData = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _CycleCodes = new MWDataManager.clsDataAccess();
        public int theActivity;
        public string theProdMonth;
        public string theSectionIDMO;
        public string newresult;
        public string abc;
        string smid;
        double gt;
        string depth;
        double OpspalnSQM, OpsplanKG, OpsCMGT, OpsPlanReefDev, OpsPlanWastDev, OpsPlanCap;
        double theValue = 0;
        private System.Timers.Timer _SecurityTimerStope = new System.Timers.Timer();
        GridHitInfo downHitInfo = null;
        private string action = "OFF";

        string SaveWP = "";
        string SaveFL = "0";
        string SaveTotSqm = "0";


        public bool valid;
        private bool _cycleActive;
        public bool cycleActive { get { return _cycleActive; } set { _cycleActive = value; } }
        string machineName;
        string machineIP;
        Mineware.Systems.Global.sysMessages.sysMessagesClass theMessage = new Global.sysMessages.sysMessagesClass();
        int row_Cycle_Date = 0;
        int row_Cycle_Code = -1;
        int row_Cycle_Vale = -1;
        int row_Cycle_CodeCube = -1;
        int row_Cycle_ValeCube = -1;
        int row_Input = -1;
        const string dayOffCode = "OFF";
        public bool replacewp;
        DataTable theDates = new DataTable();
        DateTime   enddate;

        Procedures procs = new Procedures();

        public string _CyclingType;

        public ucPreplanning()
        {
            InitializeComponent();            
            Application.Idle += Application_Idle;

           
        }

        void Application_Idle(object sender, EventArgs e)
        {
            SetStopeSecurity();
        }


        public void UpdateApproveFlag()
        {

            switch (theActivity)
            {
                case 0:
                        int stpcurrentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
                        UpdateApproveFlag(theProdMonth, tblPrePlanData.Rows[stpcurrentRow]["Workplaceid"].ToString(), tblPrePlanData.Rows[stpcurrentRow]["SectionID"].ToString());
                        
                    break;
                case 1:
                        int devcurrentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);
                        UpdateApproveFlag(theProdMonth, tblPrePlanData.Rows[devcurrentRow]["Workplaceid"].ToString(), tblPrePlanData.Rows[devcurrentRow]["SectionID"].ToString());
                    break;
            }
        }


        //string SaveWP = "";
        //string SaveFL = "0";
        //string SaveTotSqm = "0";

       
        public void showCycle(bool value)
        {

            if (theActivity == 0)
            {
                if (viewPlanningStoping.RowCount == 0)
                {
                    return;
                }

                viewPlanningStoping.OptionsView.ShowFooter = !value;
                pcCycle.Visible = value;
                _cycleActive = value;
            }


            if (theActivity == 1)
            {
                if (viewPlanningDevelopment.RowCount == 0)
                {
                    return;
                }

                viewPlanningDevelopment.OptionsView.ShowFooter = !value;
                pcCycle.Visible = value;
                _cycleActive = value;
            }


            if (_cycleActive == false)
                return;


            if ("a" == "a")
            {
                int currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);

                if (theActivity == 1)
                    currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);

                if (tblPrePlanData.Rows[currentRow]["SectionID"].ToString() == "-1")
                {
                    return;
                }

                string SaveWP = tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString();
                string SaveFL = "0";
                string SaveTotSqm = "0";
                string SaveTotCall = "0";

                if (theActivity == 0)
                {
                    SaveFL = tblPrePlanData.Rows[currentRow]["FL"].ToString();
                    SaveTotSqm = tblPrePlanData.Rows[currentRow]["CallValue"].ToString();
                }


                if (theActivity == 1)
                {
                    SaveWP = tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString();

                    MWDataManager.clsDataAccess _dbManLoadEndtype = new MWDataManager.clsDataAccess();
                    _dbManLoadEndtype.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManLoadEndtype.SqlStatement = "select endtypeid from workplace w  where workplaceid = '" + SaveWP + "'  ";

                    _dbManLoadEndtype.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManLoadEndtype.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManLoadEndtype.ExecuteInstruction();

                    if (_dbManLoadEndtype.ResultsDataTable.Rows.Count > 0)
                    {
                        SaveFL = _dbManLoadEndtype.ResultsDataTable.Rows[0][0].ToString();
                    }
                    else
                    {
                        SaveFL = "0";
                    }

                    SaveTotSqm = tblPrePlanData.Rows[currentRow]["Metresadvance"].ToString();
                }

                if (theActivity != 1)
                {
                    SaveTotCall = tblPrePlanData.Rows[currentRow]["callValue"].ToString();

                    if (SaveTotSqm == "0" && SaveTotCall != "" || SaveTotCall == "0")
                    {
                        SaveTotSqm = SaveTotCall;
                    }
                }


                if (SaveTotSqm == "")
                    SaveTotSqm = "0";


                TotlSqmLbl.Text = Math.Round(Convert.ToDecimal(SaveTotSqm), 0).ToString();
                FLlbl.Text = Math.Round(Convert.ToDecimal(SaveFL), 0).ToString();

                MWDataManager.clsDataAccess _dbManLoadCycleCodes = new MWDataManager.clsDataAccess();
                _dbManLoadCycleCodes.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLoadCycleCodes.SqlStatement = "SELECT CycleCode,Description FROM [CODE_CYCLE]    ";
                if (theActivity == 0)
                    _dbManLoadCycleCodes.SqlStatement = _dbManLoadCycleCodes.SqlStatement + "where Stoping = 'Y'  ";
                if (theActivity == 1)
                    _dbManLoadCycleCodes.SqlStatement = _dbManLoadCycleCodes.SqlStatement + "where Developement = 'Y'  ";

                _dbManLoadCycleCodes.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLoadCycleCodes.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLoadCycleCodes.ExecuteInstruction();

                DataTable dtCycleCodes = _dbManLoadCycleCodes.ResultsDataTable;

                lbxCodeCycles.Items.Clear();

                foreach (DataRow dr in dtCycleCodes.Rows)
                {
                    lbxCodeCycles.Items.Add(dr["CycleCode"].ToString() + ":" + dr["Description"].ToString());
                }

                DataSet dsCYcleCoes = new DataSet();
                dsCYcleCoes.Tables.Add(dtCycleCodes);

                gcCycleCodes.DataSource = dsCYcleCoes.Tables[0];

                gcCodeDescription.FieldName = "Description";
                gcCodeCycCodes.FieldName = "CycleCode";


                //int currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
                //pcCycle.Visible = true;

                MWDataManager.clsDataAccess _dbManLoadGrid = new MWDataManager.clsDataAccess();
                _dbManLoadGrid.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLoadGrid.SqlStatement = "   " +
"select* from (    \r\n" +
"select '1' lbl, 'Default' zz, 'Std Cycle' detail   \r\n" +
"union   \r\n" +
"select '2' lbl, 'Default' zz, 'Sqm/Adv' detail   \r\n" +
"union   \r\n" +
"select '3' lbl, 'MO' zz, 'Crew cycle' detail   \r\n" +
"union   \r\n" +
"select '4' lbl, 'MO' zz, 'Sqm/Adv' detail   \r\n" +
") a,   \r\n" +
"(   \r\n" +
"select   \r\n" +
"min(calendardate) dd,   \r\n" +
"isnull(max(Day1), 'EX') Day1 ,isnull(max(Day2), 'EX') Day2,isnull(max(Day3), 'EX') Day3,isnull(max(Day4), 'EX') Day4,isnull(max(Day5), 'EX') Day5,     \r\n" +
"isnull(max(Day6), 'EX') Day6,isnull(max(Day7), 'EX') Day7,isnull(max(Day8), 'EX') Day8,isnull(max(Day9), 'EX') Day9,isnull(max(Day10), 'EX') Day10   \r\n" +

", isnull(max(Day11), 'EX') Day11 ,isnull(max(Day12), 'EX') Day12,isnull(max(Day13), 'EX') Day13,isnull(max(Day14), 'EX') Day14,isnull(max(Day15), 'EX') Day15,     \r\n" +
"isnull(max(Day16), 'EX') Day16,isnull(max(Day17), 'EX') Day17,isnull(max(Day18), 'EX') Day18,isnull(max(Day19), 'EX') Day19,isnull(max(Day20), 'EX') Day20   \r\n" +

", isnull(max(Day21), 'EX') Day21 ,isnull(max(Day22), 'EX') Day22,isnull(max(Day23), 'EX') Day23,isnull(max(Day24), 'EX') Day24,isnull(max(Day25), 'EX') Day25,      \r\n" +
"isnull(max(Day26), 'EX') Day26,isnull(max(Day27), 'EX') Day27,isnull(max(Day28), 'EX') Day28,isnull(max(Day29), 'EX') Day29,isnull(max(Day30), 'EX') Day30   \r\n" +

", isnull(max(Day31), 'EX') Day31 ,isnull(max(Day32), 'EX') Day32,isnull(max(Day33), 'EX') Day33,isnull(max(Day34), 'EX') Day34,isnull(max(Day35), 'EX') Day35,   \r\n" +
"isnull(max(Day36), 'EX') Day36,isnull(max(Day37), 'EX') Day37,isnull(max(Day38), 'EX') Day38,isnull(max(Day39), 'EX') Day39,isnull(max(Day40), 'EX') Day40   \r\n" +

", isnull(max(Day41), 'EX') Day41 ,isnull(max(Day42), 'EX') Day42,isnull(max(Day43), 'EX') Day43,isnull(max(Day44), 'EX') Day44,isnull(max(Day45), 'EX') Day45,       \r\n" +
"isnull(max(Day46), 'EX') Day46,isnull(max(Day47), 'EX') Day47,isnull(max(Day48), 'EX') Day48,isnull(max(Day49), 'EX') Day49,isnull(max(Day50), 'EX') Day50   \r\n" +

"from(   \r\n" +
"select CalendarDate,   \r\n" +

"case when CalendarDate = begindate and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate and workingday = 'Y' then ''   \r\n" +
"end as Day1,   \r\n" +

"case when CalendarDate = begindate + 1 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 1 and workingday = 'Y' then ''   \r\n" +
"end as Day2,   \r\n" +

"case when CalendarDate = begindate + 2 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 2 and workingday = 'Y' then ''   \r\n" +
"end as Day3,   \r\n" +

"case when CalendarDate = begindate + 3 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 3 and workingday = 'Y' then ''   \r\n" +
"end as Day4,   \r\n" +

"case when CalendarDate = begindate + 4 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 4 and workingday = 'Y' then ''   \r\n" +
"end as Day5,   \r\n" +

"case when CalendarDate = begindate + 5 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 5 and workingday = 'Y' then ''   \r\n" +
"end as Day6,   \r\n" +

"case when CalendarDate = begindate + 6 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 6 and workingday = 'Y' then ''   \r\n" +
"end as Day7,   \r\n" +

"case when CalendarDate = begindate + 7 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 7 and workingday = 'Y' then ''   \r\n" +
"end as Day8,   \r\n" +

"case when CalendarDate = begindate + 8 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 8 and workingday = 'Y' then ''   \r\n" +
"end as Day9,   \r\n" +

"case when CalendarDate = begindate + 9 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 9 and workingday = 'Y' then ''   \r\n" +
"end as Day10,   \r\n" +

"case when CalendarDate = begindate + 10 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 10 and workingday = 'Y' then ''   \r\n" +
"end as Day11,   \r\n" +

"case when CalendarDate = begindate + 11 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 11 and workingday = 'Y' then ''   \r\n" +
"end as Day12,   \r\n" +

"case when CalendarDate = begindate + 12 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 12 and workingday = 'Y' then ''   \r\n" +
"end as Day13,   \r\n" +

"case when CalendarDate = begindate + 13 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 13 and workingday = 'Y' then ''   \r\n" +
"end as Day14,   \r\n" +

"case when CalendarDate = begindate + 14 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 14 and workingday = 'Y' then ''   \r\n" +
"end as Day15,   \r\n" +

"case when CalendarDate = begindate + 15 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 15 and workingday = 'Y' then ''   \r\n" +
"end as Day16,   \r\n" +

"case when CalendarDate = begindate + 16 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 16 and workingday = 'Y' then ''   \r\n" +
"end as Day17,   \r\n" +

"case when CalendarDate = begindate + 17 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 17 and workingday = 'Y' then ''   \r\n" +
"end as Day18,   \r\n" +

"case when CalendarDate = begindate + 18 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 18 and workingday = 'Y' then ''   \r\n" +
"end as Day19,   \r\n" +

"case when CalendarDate = begindate + 19 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 19 and workingday = 'Y' then ''   \r\n" +
"end as Day20,   \r\n" +

"case when CalendarDate = begindate + 20 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 20 and workingday = 'Y' then ''   \r\n" +
"end as Day21,   \r\n" +

"case when CalendarDate = begindate + 21 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 21 and workingday = 'Y' then ''   \r\n" +
"end as Day22,   \r\n" +

"case when CalendarDate = begindate + 22 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 22 and workingday = 'Y' then ''   \r\n" +
"end as Day23,   \r\n" +

"case when CalendarDate = begindate + 23 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 23 and workingday = 'Y' then ''   \r\n" +
"end as Day24,   \r\n" +

"case when CalendarDate = begindate + 24 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 24 and workingday = 'Y' then ''   \r\n" +
"end as Day25,   \r\n" +

"case when CalendarDate = begindate + 25 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 25 and workingday = 'Y' then ''   \r\n" +
"end as Day26,   \r\n" +

"case when CalendarDate = begindate + 26 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 26 and workingday = 'Y' then ''   \r\n" +
"end as Day27,   \r\n" +

"case when CalendarDate = begindate + 27 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 27 and workingday = 'Y' then ''   \r\n" +
"end as Day28,   \r\n" +

"case when CalendarDate = begindate + 28 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 28 and workingday = 'Y' then ''   \r\n" +
"end as Day29,   \r\n" +

"case when CalendarDate = begindate + 29 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 29 and workingday = 'Y' then ''   \r\n" +
"end as Day30,   \r\n" +

"case when CalendarDate = begindate + 30 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 30 and workingday = 'Y' then ''   \r\n" +
"end as Day31,   \r\n" +

"case when CalendarDate = begindate + 31 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 31 and workingday = 'Y' then ''   \r\n" +
"end as Day32,   \r\n" +

"case when CalendarDate = begindate + 32 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 32 and workingday = 'Y' then ''   \r\n" +
"end as Day33,   \r\n" +

"case when CalendarDate = begindate + 33 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 33 and workingday = 'Y' then ''   \r\n" +
"end as Day34,   \r\n" +

"case when CalendarDate = begindate + 34 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 34 and workingday = 'Y' then ''   \r\n" +
"end as Day35,   \r\n" +

"case when CalendarDate = begindate + 35 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 35 and workingday = 'Y' then ''   \r\n" +
"end as Day36,   \r\n" +

"case when CalendarDate = begindate + 36 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 36 and workingday = 'Y' then ''   \r\n" +
"end as Day37,   \r\n" +

"case when CalendarDate = begindate + 37 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 37 and workingday = 'Y' then ''   \r\n" +
"end as Day38,   \r\n" +

"case when CalendarDate = begindate + 38 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 38 and workingday = 'Y' then ''   \r\n" +
"end as Day39,   \r\n" +

"case when CalendarDate = begindate + 39 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 39 and workingday = 'Y' then ''   \r\n" +
"end as Day40,   \r\n" +

"case when CalendarDate = begindate + 40 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 40 and workingday = 'Y' then ''   \r\n" +
"end as Day41,   \r\n" +

"case when CalendarDate = begindate + 41 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 41 and workingday = 'Y' then ''   \r\n" +
"end as Day42,   \r\n" +

"case when CalendarDate = begindate + 42 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 42 and workingday = 'Y' then ''   \r\n" +
"end as Day43,   \r\n" +

"case when CalendarDate = begindate + 43 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 43 and workingday = 'Y' then ''   \r\n" +
"end as Day44,   \r\n" +

"case when CalendarDate = begindate + 44 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 44 and workingday = 'Y' then ''   \r\n" +
"end as Day45,   \r\n" +

"case when CalendarDate = begindate + 45 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 45 and workingday = 'Y' then ''   \r\n" +
"end as Day46,   \r\n" +

"case when CalendarDate = begindate + 46 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 46 and workingday = 'Y' then ''   \r\n" +
"end as Day47,   \r\n" +

"case when CalendarDate = begindate + 47 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 47 and workingday = 'Y' then ''   \r\n" +
"end as Day48,   \r\n" +

"case when CalendarDate = begindate + 48 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 48 and workingday = 'Y' then ''   \r\n" +
"end as Day49,   \r\n" +

"case when CalendarDate = begindate + 49 and workingday = 'N' then 'OFF'   \r\n" +
"when CalendarDate = begindate + 49 and workingday = 'Y' then ''   \r\n" +
"end as Day50   \r\n" +

"from[dbo].[CALTYPE] a,   \r\n" +
"(select s.* from seccal s, section s1 where s.prodmonth = s1.prodmonth and s.sectionid = s1.reporttosectionid   \r\n" +
"and s.prodmonth = '" + theProdMonth + "'   \r\n" +
"and s1.sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "') b where a.CalendarCode = b.CalendarCode   \r\n" +
"and CalendarDate >= begindate and calendardate <= enddate   \r\n" +
") a) b   \r\n" +

"--order by calendardate ";

                _dbManLoadGrid.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLoadGrid.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLoadGrid.ExecuteInstruction();

                DataTable dt = _dbManLoadGrid.ResultsDataTable;

                DataSet ds = new DataSet();

                ds.Tables.Add(dt);

                gcMoCycle.DataSource = ds.Tables[0];

                colCycType.FieldName = "zz";
                colCycName.FieldName = "detail";

                colCycDay1.FieldName = "Day1";
                colCycDay2.FieldName = "Day2";
                colCycDay3.FieldName = "Day3";
                colCycDay4.FieldName = "Day4";
                colCycDay5.FieldName = "Day5";
                colCycDay6.FieldName = "Day6";
                colCycDay7.FieldName = "Day7";
                colCycDay8.FieldName = "Day8";
                colCycDay9.FieldName = "Day9";
                colCycDay10.FieldName = "Day10";

                colCycDay11.FieldName = "Day11";
                colCycDay12.FieldName = "Day12";
                colCycDay13.FieldName = "Day13";
                colCycDay14.FieldName = "Day14";
                colCycDay15.FieldName = "Day15";
                colCycDay16.FieldName = "Day16";
                colCycDay17.FieldName = "Day17";
                colCycDay18.FieldName = "Day18";
                colCycDay19.FieldName = "Day19";
                colCycDay20.FieldName = "Day20";

                colCycDay21.FieldName = "Day21";
                colCycDay22.FieldName = "Day22";
                colCycDay23.FieldName = "Day23";
                colCycDay24.FieldName = "Day24";
                colCycDay25.FieldName = "Day25";
                colCycDay26.FieldName = "Day26";
                colCycDay27.FieldName = "Day27";
                colCycDay28.FieldName = "Day28";
                colCycDay29.FieldName = "Day29";
                colCycDay30.FieldName = "Day30";

                colCycDay31.FieldName = "Day31";
                colCycDay32.FieldName = "Day32";
                colCycDay33.FieldName = "Day33";
                colCycDay34.FieldName = "Day34";
                colCycDay35.FieldName = "Day35";
                colCycDay36.FieldName = "Day36";
                colCycDay37.FieldName = "Day37";
                colCycDay38.FieldName = "Day38";
                colCycDay39.FieldName = "Day39";
                colCycDay40.FieldName = "Day40";

                colCycDay41.FieldName = "Day41";
                colCycDay42.FieldName = "Day42";
                colCycDay43.FieldName = "Day43";
                colCycDay44.FieldName = "Day44";
                colCycDay45.FieldName = "Day45";
                colCycDay46.FieldName = "Day46";
                colCycDay47.FieldName = "Day47";
                colCycDay48.FieldName = "Day48";
                colCycDay49.FieldName = "Day49";
                colCycDay50.FieldName = "Day50";


                int columnIndex = 2;

                DateTime StartDate = Convert.ToDateTime(_dbManLoadGrid.ResultsDataTable.Rows[0]["dd"].ToString());
                SaveDate.Value = Convert.ToDateTime(_dbManLoadGrid.ResultsDataTable.Rows[0]["dd"].ToString());

                for (int i = 0; i < 50; i++)
                {
                    string test = gvMoCycle.Columns[columnIndex].Caption;

                    gvMoCycle.Columns[columnIndex].Caption = Convert.ToDateTime(StartDate).ToString("dd MMM yyyy");

                    StartDate = StartDate.AddDays(1);
                    columnIndex++;
                }


                if (theActivity == 0)
                {
                    MWDataManager.clsDataAccess _dbManLoadCycleStopingName = new MWDataManager.clsDataAccess();
                    _dbManLoadCycleStopingName.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManLoadCycleStopingName.SqlStatement = "select cycle from [dbo].[Code_Cycle_WPExceptions] where workplaceid = '" + tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString() + "' \r\n" +
                                                         "union    \r\n" +
                                                         "select StopingCycle from Code_Cycle_MOCycleConfig where sectionid = '" + editMoSectionID.EditValue.ToString() + "'    ";
                    _dbManLoadCycleStopingName.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManLoadCycleStopingName.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManLoadCycleStopingName.ExecuteInstruction();

                    string CycName = "";
                    try
                    {
                        CycName = _dbManLoadCycleStopingName.ResultsDataTable.Rows[0]["cycle"].ToString();
                        if (CycName == "")
                        {
                            MessageBox.Show("No Cycle attached , Please contact your System Admin");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("No Cycle attached , Please contact your System Admin");
                        return;
                    }



                    MWDataManager.clsDataAccess _dbManLoadCycleStoping = new MWDataManager.clsDataAccess();
                    _dbManLoadCycleStoping.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManLoadCycleStoping.SqlStatement = "select * from [dbo].[Code_Cycle_RawData] where name = '" + CycName + "' and fl = '" + SaveFL + "'   ";
                    _dbManLoadCycleStoping.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManLoadCycleStoping.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManLoadCycleStoping.ExecuteInstruction();

                    if (_dbManLoadCycleStoping.ResultsDataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No Cycle Found");
                        return;
                    }

                    int colIndex = 2;
                    int defColIndex = 4;

                    for (int i = 0; i < 25; i++)
                    {
                        //if (i == 2)
                        //{
                        string test = gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[colIndex - 1]).ToString();
                        //}


                        if (gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[colIndex]).ToString() != "OFF")
                        {
                            gvMoCycle.SetRowCellValue(0, gvMoCycle.Columns[colIndex], _dbManLoadCycleStoping.ResultsDataTable.Rows[0][defColIndex + i].ToString());

                            ///Load Default cycle into MOCycle
                             gvMoCycle.SetRowCellValue(2, gvMoCycle.Columns[colIndex], _dbManLoadCycleStoping.ResultsDataTable.Rows[0][defColIndex + i].ToString());
                        }

                        colIndex++;
                    }
                }


                if (theActivity == 1)
                {
                    MWDataManager.clsDataAccess _dbManLoadCycleDevName = new MWDataManager.clsDataAccess();
                    _dbManLoadCycleDevName.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManLoadCycleDevName.SqlStatement = "select cycle from [dbo].[Code_Cycle_WPExceptions] where workplaceid = '" + tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString() + "' \r\n" +
                                                         "union    \r\n" +
                                                         "select DevCycle from Code_Cycle_MOCycleConfig where sectionid = '" + editMoSectionID.EditValue.ToString() + "'    ";
                    _dbManLoadCycleDevName.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManLoadCycleDevName.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManLoadCycleDevName.ExecuteInstruction();

                    string CycName = _dbManLoadCycleDevName.ResultsDataTable.Rows[0]["cycle"].ToString();

                    if (CycName == "")
                    {
                        MessageBox.Show("No Cycle found");
                        return;
                    }


                    MWDataManager.clsDataAccess _dbManLoadCycleDev = new MWDataManager.clsDataAccess();
                    _dbManLoadCycleDev.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManLoadCycleDev.SqlStatement = "select * from [dbo].[Code_Cycle_RawData] where name = '" + CycName + "' and fl = '" + SaveFL + "'   ";
                    _dbManLoadCycleDev.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManLoadCycleDev.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManLoadCycleDev.ExecuteInstruction();

                    if (_dbManLoadCycleDev.ResultsDataTable.Rows.Count != 0)
                    {
                        devADVperBlast.Text = _dbManLoadCycleDev.ResultsDataTable.Rows[0]["AdvBlast"].ToString();

                        int colIndex = 2;
                        int defColIndex = 4;

                        for (int i = 0; i < 25; i++)
                        {
                            if (gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[colIndex]).ToString() != "OFF")
                            {
                                gvMoCycle.SetRowCellValue(0, gvMoCycle.Columns[colIndex], _dbManLoadCycleDev.ResultsDataTable.Rows[0][defColIndex + i].ToString());
                                gvMoCycle.SetRowCellValue(2, gvMoCycle.Columns[colIndex], _dbManLoadCycleDev.ResultsDataTable.Rows[0][defColIndex + i].ToString());
                            }


                            colIndex++;
                        }
                    }
                    else
                    {
                        devADVperBlast.Text = "0.8";
                        MessageBox.Show("No Default Cycle found");
                    }

                }


                //Insert Saved Planning
                MWDataManager.clsDataAccess _dbManLoadPlannedCycle = new MWDataManager.clsDataAccess();
                _dbManLoadPlannedCycle.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLoadPlannedCycle.SqlStatement = "select * from [dbo].[planning] where prodmonth = '" + theProdMonth + "' and sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' and workplaceid = '" + tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString() + "'  ";
                _dbManLoadPlannedCycle.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLoadPlannedCycle.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLoadPlannedCycle.ExecuteInstruction();

                DataTable dtSavedPlanning = _dbManLoadPlannedCycle.ResultsDataTable;

                int colDateIndex = 2;

                int DiffinDates = 0;

              // string aa = _dbManLoadPlannedCycle.ResultsDataTable.Rows[0]["Calendardate"].ToString();
              //  string bb = Convert.ToDateTime(gvMoCycle.Columns[colDateIndex].Caption.ToString().Substring(0,6)).ToString();

                if (_dbManLoadPlannedCycle.ResultsDataTable.Rows.Count > 0)
                {
                    TimeSpan diff = Convert.ToDateTime(_dbManLoadPlannedCycle.ResultsDataTable.Rows[0]["Calendardate"]) - Convert.ToDateTime(gvMoCycle.Columns[colDateIndex].Caption.ToString().Substring(0, 6));
                    DiffinDates = Convert.ToInt32(diff.TotalDays);
                }

                if (Convert.ToInt32(DiffinDates) < 0)
                {
                    colDateIndex = colDateIndex + DiffinDates;
                }

                foreach (DataRow dr in dtSavedPlanning.Rows)
                {

                    //string test = String.Format("{0:yyyy-MM-dd}", dr["Calendardate"]);
                    //string test1 = dr["MOCycle"].ToString();
                    string Headerdate = null;
                    if (colDateIndex >= 2)
                    {
                        Headerdate = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(gvMoCycle.Columns[colDateIndex].Caption.ToString()));
                    }



                    DateTime PlanningDay = Convert.ToDateTime(dr["Calendardate"].ToString());

                    string test = String.Format("{0:yyyy-MM-dd}", PlanningDay);
                    string test1 = dr["MOCycle"].ToString();

                    if (String.Format("{0:yyyy-MM-dd}", PlanningDay) == Headerdate)
                    {
                        if (gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[colDateIndex]).ToString().Trim() != "OFF")
                        {
                            gvMoCycle.SetRowCellValue(2, gvMoCycle.Columns[colDateIndex], dr["MOCycle"].ToString());
                        }
                    }

                    colDateIndex = colDateIndex + 1;
                }

                SQMCalc();

            }
            else
            {



                switch (theActivity)
                {
                    case 0:
                        viewPlanningStoping.OptionsView.ShowFooter = !value;
                        pcCycle.Visible = value;
                        if (value) // only load if value is true
                        {
                            int currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
                            // if (Convert.ToBoolean(tblPrePlanData.Rows[currentRow]["Locked"].ToString()))
                            // {
                            PlanningClass.PlanningScreen.Genirate_Cycle(tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString(), tblPrePlanData.Rows[currentRow]["SectionID"].ToString());
                            loadCyclePlan(theProdMonth, tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString(), tblPrePlanData.Rows[currentRow]["SectionID"].ToString());
                            // }
                        }
                        break;
                    case 1:
                        viewPlanningDevelopment.OptionsView.ShowFooter = !value;
                        pcCycle.Visible = value;
                        if (value) // only load if value is true
                        {
                            int currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);
                            // if (Convert.ToBoolean(tblPrePlanData.Rows[currentRow]["Locked"].ToString()))
                            // {
                            PlanningClass.PlanningScreen.Genirate_Cycle(tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString(), tblPrePlanData.Rows[currentRow]["SectionID"].ToString());
                            loadCyclePlan(theProdMonth, tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString(), tblPrePlanData.Rows[currentRow]["SectionID"].ToString());
                            // }
                        }
                        break;
                }
            }

            CycleChanged = false;

        }

        public void SaveMOCycle()
        {
            if (tblPrePlanData.Rows.Count == 0)
            {
                MessageBox.Show("Please add a Workplace before trying to Save");
                return;
            }

          

            string SaveDefaultCycle = "";
            string SaveDefaultSQM = "";
            string SaveMoCycle = "";
            string saveMoSQM = "";
            int currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);

            if(theActivity == 1)
                currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);

            string test = tblPrePlanData.Rows[currentRow]["SectionID"].ToString();
            string Test1 = tblPrePlanData.Rows[currentRow]["OrgUnitDay"].ToString();

            //if (tblPrePlanData.Rows[currentRow]["Locked"].ToString() == "True")
            //{
            //    MessageBox.Show("Workplace has been Locked, Please contact System Administrator");
            //    return;
            //}


            if (tblPrePlanData.Rows[currentRow]["OrgUnitDay"].ToString() == "")
            {
                MessageBox.Show("Please select a Orgunit Before Saving");
                return;
            }

            if (tblPrePlanData.Rows[currentRow]["SectionID"].ToString() == "-1")
            {
                MessageBox.Show("Please select a Miner Before Saving");
                return;
            }


            if (tblPrePlanData.Rows[currentRow]["OrgUnitDay"].ToString() == "")
            {
                MessageBox.Show("Please select a Orgunit Before Saving");
                return;
            }

            if (gvMoCycle.RowCount == 0)
            {
                MessageBox.Show("Please build a Cycle before trying to save");
                return;
            }



            string WP = tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString();
            ////Get Density
            MWDataManager.clsDataAccess _dbManDens = new MWDataManager.clsDataAccess();
            _dbManDens.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManDens.SqlStatement = "select isnull(Density,2.72)Density from workplace w  where workplaceid = '" + WP + "'  ";

            _dbManDens.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDens.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDens.ExecuteInstruction();

            decimal density = Convert.ToDecimal(_dbManDens.ResultsDataTable.Rows[0][0].ToString());


            int Count = 0;

            decimal SW = 0;
            decimal tons = 0;
            decimal MoCycleGT = 0;
            decimal CW = 0;
            decimal CMGT = 0;
            string FL = "0";
            decimal TotalTOns = 0;
            decimal MetresAdv = 0;

            if (theActivity == 0)
            {
                FL = tblPrePlanData.Rows[currentRow]["FL"].ToString();
            }

            if (theActivity == 1)
            {
                MWDataManager.clsDataAccess _dbManLoadEndtype = new MWDataManager.clsDataAccess();
                _dbManLoadEndtype.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLoadEndtype.SqlStatement = "select endtypeid from workplace w where workplaceid = '" + WP + "'  ";

                _dbManLoadEndtype.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLoadEndtype.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLoadEndtype.ExecuteInstruction();

                if (_dbManLoadEndtype.ResultsDataTable.Rows.Count > 0)
                {
                    FL = _dbManLoadEndtype.ResultsDataTable.Rows[0][0].ToString();
                }
                else
                {
                    FL = "0";
                }
            }

       
            //hierso
            for (int i = 2; i < 49; i++)
            {              

                //if (gvMoCycle.Columns[i].FieldName != null && gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[i].FieldName).ToString() != "EX")
                //{
                SaveDefaultCycle = SaveDefaultCycle + (gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[i].FieldName).ToString() + "       ").Substring(0, 4);


                SaveDefaultSQM = SaveDefaultSQM + (gvMoCycle.GetRowCellValue(1, gvMoCycle.Columns[i].FieldName).ToString() + "       ").Substring(0, 4);


                SaveMoCycle = SaveMoCycle + (gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i].FieldName).ToString() + "       ").Substring(0, 4);

                if (gvMoCycle.GetRowCellValue(3, gvMoCycle.Columns[i].FieldName).ToString() == "OFF")
                {
                    saveMoSQM = saveMoSQM + ("       ").Substring(0, 4);
                }
                else
                {
                    saveMoSQM = saveMoSQM + (gvMoCycle.GetRowCellValue(3, gvMoCycle.Columns[i].FieldName).ToString() + "       ").Substring(0, 4);
                }
                    

                    Count = Count + 1;
                //}
            }

            //MWDataManager.clsDataAccess _dbManGetStope = new MWDataManager.clsDataAccess();
            //_dbManGetStope.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbManGetStope.SqlStatement = " Select SWidth,Channelwidth,Cmgt   FROM [PAS_DNK_Syncromine].[dbo].[SAMPLING] where WorkplaceID = '" + WP + "' ";
            //_dbManGetStope.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbManGetStope.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbManGetStope.ExecuteInstruction();

            //if (_dbManGetStope.ResultsDataTable.Rows.Count > 0)
            //{
            if (theActivity == 0)
                SW = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["SW"].ToString());
            if(theActivity == 0)
                CW = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["CW"].ToString());

                CMGT = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["CMGT"].ToString());
            //}

            if (Convert.ToDecimal(FL) > 0)
            {
                MetresAdv = Convert.ToDecimal(TotlSqmLbl.Text) / Convert.ToDecimal(FL);
            }

            

            if (SW != 0)
            {
                MoCycleGT = Convert.ToDecimal(CMGT) / Convert.ToDecimal(SW);
            }
            else
            {
                MoCycleGT = CMGT;
            }

            if (theActivity == 1)
            {
                string tes = tblPrePlanData.Rows[currentRow]["GramPerTon"].ToString();

                if (tblPrePlanData.Rows[currentRow]["GramPerTon"].ToString() != "")
                {
                    MoCycleGT = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["GramPerTon"].ToString());
                }                
            }

            if (theActivity == 0)
            {
                int ShiftDay = 0;

                

                int subPos = 0;


                MWDataManager.clsDataAccess _dbManPlanningSTP = new MWDataManager.clsDataAccess();
                _dbManPlanningSTP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                for (int i = 0; i < Count; i++)
                {
                    string wday = "Y";

                    string sqmds = saveMoSQM.Substring(subPos, 4).TrimEnd();
                    if (SaveMoCycle.Substring(subPos, 4).TrimEnd() == "OFF")
                    {
                        wday = "N";
                        sqmds = "0";
                    }

                    if (SaveMoCycle.Substring(subPos, 4).TrimEnd() == "EX")
                    {
                        break;
                    }

                    if (sqmds == "" || sqmds == "EX")
                        sqmds = "0";

                    string ReefWaste = tblPrePlanData.Rows[currentRow]["RockType"].ToString();
                   

                    string reefSQM = "0";
                    string WasteSQM = "0";

                    if (ReefWaste == "ON")
                    {
                        reefSQM = sqmds;
                        WasteSQM = "0";
                    }
                    else
                    {
                        reefSQM = "0";
                        WasteSQM = sqmds;
                    }

                    tons = Convert.ToDecimal(sqmds) * Convert.ToDecimal(SW) / 100 * density;

                    TotalTOns = TotalTOns + tons;
                    


                    _dbManPlanningSTP.SqlStatement = _dbManPlanningSTP.SqlStatement +
                       "   \r\n  " +
                       "  BEGIN TRY   \r\n" +
                       "  INSERT INTO PLANNING ( Prodmonth,SectionID,WorkplaceID,Activity, \r\n" +
                       " Calendardate,PlanCode,IsCubics,ShiftDay,SQM,ReefSqm, WasteSqm, MOCycle,  \r\n" +
                       "  Metresadvance,FL,Tons,SW,CW,CMGT,GT,    \r\n"+
                       "  CubicMetres)  \r\n" +
                       "  values( " + theProdMonth + ",'" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "','" + WP + "'  ,'" + theActivity + "', \r\n" +
                       " '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value.AddDays(i)) + "','MP','N', '" + ShiftDay + "','" + sqmds + "','" + reefSQM + "','" + WasteSQM + "','" + SaveMoCycle.Substring(subPos, 4).TrimEnd() + "', \r\n" +
                       "  '"+ MetresAdv + "' ,'" + FL + "' ,'" + tons + "', '" + SW + "','" + CW + "','" + CMGT + "','"+ MoCycleGT + "',  \r\n" +
                       "  '0.000')  \r\n" +
                       "  END TRY \r\n" +
                       "  BEGIN CATCH  \r\n" +
                       "  update PLANNING set shiftday = " + ShiftDay + " ,  \r\n" +
                       "  Sqm = '" + sqmds + "' ,ReefSqm = '" + reefSQM + "' ,WasteSqm = '" + WasteSQM + "' , Tons = '" + tons + "', FL = '" + FL + "', SW = '" + SW + "', CW = '" + CW + "' , CMGT = '" + CMGT + "' , GT = '"+ MoCycleGT + "',  \r\n" +
                       "  MOCycle = '" + SaveMoCycle.Substring(subPos, 4).TrimEnd() + "' ,CubicMetres = '0.000',Metresadvance = '" + MetresAdv + "' \r\n" +
                       "  Where   \r\n" +
                       "  Prodmonth = " + theProdMonth + "  and  WorkplaceID = '" + WP + "' and Activity = '" + theActivity + "'  \r\n" +
                       "  and CalendarDate = '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value.AddDays(i)) + "' and  SectionID = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' and PlanCode = 'MP'  \r\n" +
                       "  END CATCH    \r\n\r\n\r\n";
                    _dbManPlanningSTP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManPlanningSTP.queryReturnType = MWDataManager.ReturnType.DataTable;




                    ShiftDay = ShiftDay + 1;

                    if (wday == "N")
                    {
                        ShiftDay = ShiftDay - 1;
                    }

                    subPos = subPos + 4;
                }
                _dbManPlanningSTP.ExecuteInstruction();

                string TargetID = tblPrePlanData.Rows[currentRow]["TargetID"].ToString();

                if (tblPrePlanData.Rows[currentRow]["TargetID"].ToString() == "")
                {
                    TargetID = "-1";
                }

                saveMoSQM = saveMoSQM + "0";
                if (tblPrePlanData.Rows[currentRow]["Locked"].ToString() == "True")
                {
                    MWDataManager.clsDataAccess _dbManPlanmonthSTP = new MWDataManager.clsDataAccess();
                    _dbManPlanmonthSTP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManPlanmonthSTP.SqlStatement = " Delete from PLANMONTH where Prodmonth = " + theProdMonth + " and Sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "'  and WorkplaceId = '" + WP + "' and activity = " + theActivity + " and PlanCode = 'MP'  \r\n\r\n ";

                    _dbManPlanmonthSTP.SqlStatement = _dbManPlanmonthSTP.SqlStatement +
                    "insert into [dbo].[PLANMONTH]  " +
                    "(Prodmonth,SectionId,WorkplaceId,Activity,PlanCode,IsCubics, \r\n" +
                    "  StartDate,OrgUnitDay,OrgUnitAfternoon,OrgUnitNight ,    \r\n" +
                    "  Locked,Auth,SQM,ReefSQM,WasteSQM, FL,SW,CW ,CMGT,GT,EndDate ,   \r\n" +
                    "  CubicMetres, MOCycle,MOCycleNum,DefaultCycle,Tons, Density, Metresadvance,  \r\n" +
                    "  MonthlyTotalSQM,MonthlyWatseSQM,MonthlyReefSQM , \r\n" +
                    "  TargetID ) values\r\n" +

                    "('" + theProdMonth + "','" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "','" + WP + "'," + theActivity + ", 'MP', 'N', \r\n" +
                    "  '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitDay"].ToString()) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitAfternoon"].ToString()) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitNight"].ToString()) + "'   ,  \r\n" +
                    " 1,'Y'  ,'" + TotlSqmLbl.Text + "','" + tblPrePlanData.Rows[currentRow]["ReefSQM"].ToString() + "','" + tblPrePlanData.Rows[currentRow]["WasteSQM"].ToString() + "' ,'" + FL + "','" + SW + "','" + CW + "','" + CMGT + "', '" + MoCycleGT + "' ,'" + String.Format("{0:yyyy-MM-dd}", tblPrePlanData.Rows[currentRow]["EndDate"].ToString()) + "',   \r\n" +
                    "  '0.000', '" + SaveMoCycle + "','" + saveMoSQM + "' , '" + SaveDefaultCycle + "' , '" + TotalTOns + "' , '" + density + "' , '" + MetresAdv + "' ,  \r\n" +
                    "  '" + tblPrePlanData.Rows[currentRow]["MonthlyTotalSQM"].ToString() + "','" + tblPrePlanData.Rows[currentRow]["MonthlyWatseSQM"].ToString() + "','" + tblPrePlanData.Rows[currentRow]["MonthlyReefSQM"].ToString() + "' ,  \r\n" +
                    "  '" + tblPrePlanData.Rows[currentRow]["TargetID"].ToString() + "' ) ";
                    _dbManPlanmonthSTP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManPlanmonthSTP.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManPlanmonthSTP.ExecuteInstruction();
                }
                else
                {

                    MWDataManager.clsDataAccess _dbManPlanmonthSTP = new MWDataManager.clsDataAccess();
                    _dbManPlanmonthSTP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManPlanmonthSTP.SqlStatement = " Delete from PLANMONTH where Prodmonth = " + theProdMonth + " and Sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "'  and WorkplaceId = '" + WP + "' and activity = " + theActivity + " and PlanCode = 'MP'  \r\n\r\n ";

                    _dbManPlanmonthSTP.SqlStatement = _dbManPlanmonthSTP.SqlStatement +
                    "insert into [dbo].[PLANMONTH]  " +
                    "(Prodmonth,SectionId,WorkplaceId,Activity,PlanCode,IsCubics, \r\n" +
                    "  StartDate,OrgUnitDay,OrgUnitAfternoon,OrgUnitNight ,    \r\n" +
                    "  Locked,Auth,SQM,ReefSQM,WasteSQM, FL,SW,CW ,CMGT,GT,EndDate ,   \r\n" +
                    "  CubicMetres, MOCycle,MOCycleNum,DefaultCycle,Tons, Density, Metresadvance,  \r\n" +
                    "  MonthlyTotalSQM,MonthlyWatseSQM,MonthlyReefSQM , \r\n" +
                    "  TargetID ) values\r\n" +

                    "('" + theProdMonth + "','" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "','" + WP + "'," + theActivity + ", 'MP', 'N', \r\n" +
                    "  '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitDay"].ToString()) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitAfternoon"].ToString()) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitNight"].ToString()) + "'   ,  \r\n" +
                    " 0,'Y'  ,'" + TotlSqmLbl.Text + "','" + tblPrePlanData.Rows[currentRow]["ReefSQM"].ToString() + "','" + tblPrePlanData.Rows[currentRow]["WasteSQM"].ToString() + "' ,'" + FL + "','" + SW + "','" + CW + "','" + CMGT + "', '" + MoCycleGT + "' ,'" + String.Format("{0:yyyy-MM-dd}", tblPrePlanData.Rows[currentRow]["EndDate"].ToString()) + "',   \r\n" +
                    "  '0.000', '" + SaveMoCycle + "','" + saveMoSQM + "' , '" + SaveDefaultCycle + "' , '" + TotalTOns + "' , '" + density + "' , '" + MetresAdv + "' ,  \r\n" +
                    "  '" + tblPrePlanData.Rows[currentRow]["MonthlyTotalSQM"].ToString() + "','" + tblPrePlanData.Rows[currentRow]["MonthlyWatseSQM"].ToString() + "','" + tblPrePlanData.Rows[currentRow]["MonthlyReefSQM"].ToString() + "' ,  \r\n" +
                    "  '" + tblPrePlanData.Rows[currentRow]["TargetID"].ToString() + "' ) ";
                    _dbManPlanmonthSTP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManPlanmonthSTP.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManPlanmonthSTP.ExecuteInstruction();
                }
         
                
                


                MWDataManager.clsDataAccess _dbManPlanmonthFixEX = new MWDataManager.clsDataAccess();
                _dbManPlanmonthFixEX.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManPlanmonthFixEX.SqlStatement = "update  [dbo].[planmonth] set mocycle =  REPLACE(substring(mocycle+'                                                                                XX',1,188), 'EX', '  ')  \r\n" +
                                                    "where prodmonth = '" + theProdMonth + "'   \r\n\r\n";

                _dbManPlanmonthFixEX.SqlStatement = _dbManPlanmonthFixEX.SqlStatement + "update  [dbo].[planmonth] set mocycle =  substring(replace(mocycle,'XX','')+'                                                                                                                                   ',1,200)+'XX'  \r\n" +
                                                    "where prodmonth = '" + theProdMonth + "'   \r\n\r\n";

                _dbManPlanmonthFixEX.SqlStatement = _dbManPlanmonthFixEX.SqlStatement + "update  [dbo].[planmonth] set DefaultCycle =  REPLACE(substring(DefaultCycle+'                                                                                XX',1,188), 'EX', '  ')  \r\n" +
                                                    "where prodmonth = '" + theProdMonth + "'   \r\n\r\n";

                _dbManPlanmonthFixEX.SqlStatement = _dbManPlanmonthFixEX.SqlStatement + "update  [dbo].[planmonth] set DefaultCycle =  substring(replace(DefaultCycle,'XX','')+'                                                                                                                                   ',1,200)+'XX'  \r\n" +
                                                    "where prodmonth = '" + theProdMonth + "'   ";

                _dbManPlanmonthFixEX.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManPlanmonthFixEX.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManPlanmonthFixEX.ExecuteInstruction();


                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Planning saved", Color.CornflowerBlue);
            }


            if (theActivity == 1)
            {

                

                int ShiftDay = 0;

                //decimal density = Convert.ToDecimal(2.72);

                int subPos = 0;
                decimal TotCubics = 0;

                MWDataManager.clsDataAccess _dbManPlanningSTP = new MWDataManager.clsDataAccess();
                _dbManPlanningSTP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                for (int i = 0; i < Count; i++)
                {
                    string wday = "Y";

                    string sqmds = saveMoSQM.Substring(subPos, 4).TrimEnd();
                    if (SaveMoCycle.Substring(subPos, 4).TrimEnd() == "OFF")
                    {
                        wday = "N";
                        sqmds = "0";
                    }

                    if (SaveMoCycle.Substring(subPos, 4).TrimEnd() == "EX")
                    {
                        break;
                    }

                    if (sqmds == "" || sqmds == "EX")
                        sqmds = "0";

                    tons = Convert.ToDecimal(sqmds) * Convert.ToDecimal(SW) / 100 * density;

                    TotalTOns = TotalTOns + tons;

                    decimal wasteFact = 0;

                    TotCubics = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["CubicsReef"].ToString()) + Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["CubicsWaste"].ToString());

                    if (Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["WasteAdv"].ToString()) > 0)
                        wasteFact = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["WasteAdv"].ToString()) / Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["Metresadvance"].ToString());

                    decimal wasteadv = Convert.ToDecimal(sqmds) * wasteFact;
                    decimal reefadv = Convert.ToDecimal(sqmds) - wasteadv;


                    _dbManPlanningSTP.SqlStatement = _dbManPlanningSTP.SqlStatement +
                       "   \r\n  " +
                       "  BEGIN TRY   \r\n" +
                       "  INSERT INTO PLANNING ( Prodmonth,SectionID,WorkplaceID,Activity, \r\n" +
                       " Calendardate,PlanCode,IsCubics,ShiftDay,SQM,MOCycle,  \r\n" +
                       "  Metresadvance, wasteadv, reefadv,FL,Tons,SW,CW,CMGT,GT,    \r\n"+
                       "   CubicMetres  )  \r\n" +
                       "  values( " + theProdMonth + ",'" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "','" + WP + "'  ,'" + theActivity + "', \r\n" +
                       " '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value.AddDays(i)) + "','MP','N', '" + ShiftDay + "','0','" + SaveMoCycle.Substring(subPos, 4).TrimEnd() + "', \r\n" +
                       "  '" + sqmds + "', '" + wasteadv + "', '" + reefadv + "' ,'0' ,'" + tons + "', '" + SW + "','" + CW + "','" + CMGT + "' , '"+ MoCycleGT + "',  \r\n" +
                       "  '" + tblPrePlanData.Rows[currentRow]["CubicMetres"].ToString() + "'  )  \r\n" +
                       "  END TRY \r\n" +
                       "  BEGIN CATCH  \r\n" +
                       "  update PLANNING set shiftday = " + ShiftDay + " ,  \r\n" +
                       "  Sqm = '0' , Tons = '" + tons + "', FL = '0', SW = '" + SW + "', CW = '" + CW + "' , CMGT = '" + CMGT + "' ,GT = '"+ MoCycleGT + "', \r\n" +
                       "  MOCycle = '" + SaveMoCycle.Substring(subPos, 4).TrimEnd() + "' ,CubicMetres = '" + tblPrePlanData.Rows[currentRow]["CubicMetres"].ToString() + "',Metresadvance = '" + sqmds + "', reefadv = '" + reefadv + "' , wasteadv = '" + wasteadv + "'   \r\n" +
                       "  Where   \r\n" +
                       "  Prodmonth = " + theProdMonth + "  and  WorkplaceID = '" + WP + "' and Activity = '" + theActivity + "'  \r\n" +
                       "  and CalendarDate = '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value.AddDays(i)) + "' and  SectionID = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "'  \r\n" +
                       "  END CATCH    \r\n\r\n\r\n";
                    _dbManPlanningSTP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManPlanningSTP.queryReturnType = MWDataManager.ReturnType.DataTable;


                    //dbManPlanningSTP.SqlStatement = _dbManPlanningSTP.SqlStatement +

                    //   "  update PLANNING set   \r\n" +
                    //   "  Sqm = '0' , Tons = '" + tons + "', FL = '0', SW = '" + SW + "', CW = '" + CW + "' , CMGT = '" + CMGT + "' , GT = '" + MoCycleGT + "',  \r\n" +
                    //   "  MOCycle = '" + SaveMoCycle.Substring(subPos, 4).TrimEnd() + "' ,CubicMetres = '0.000',Metresadvance = '" + sqmds + "' \r\n" +
                    //   "  Where   \r\n" +
                    //   "  Prodmonth = " + theProdMonth + "  and  WorkplaceID = '" + WP + "' and Activity = '" + theActivity + "'  \r\n" +
                    //   "  and CalendarDate = '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value.AddDays(i)) + "' and  SectionID = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' and PlanCode = 'MP'    \r\n\r\n\r\n";
                    //_dbManPlanningSTP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    //_dbManPlanningSTP.queryReturnType = MWDataManager.ReturnType.DataTable;



                    ShiftDay = ShiftDay + 1;

                    if (wday == "N")
                    {
                        ShiftDay = ShiftDay - 1;
                    }

                    subPos = subPos + 4;

                }
                _dbManPlanningSTP.ExecuteInstruction();


                string TargetID = tblPrePlanData.Rows[currentRow]["TargetID"].ToString();

                if (tblPrePlanData.Rows[currentRow]["TargetID"].ToString() == "")
                {
                    TargetID = "-1";
                }

                saveMoSQM = saveMoSQM + "0";

                if (tblPrePlanData.Rows[currentRow]["Locked"].ToString() == "True")
                {
                    MWDataManager.clsDataAccess _dbManPlanmonthSTP = new MWDataManager.clsDataAccess();
                    _dbManPlanmonthSTP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManPlanmonthSTP.SqlStatement = " Delete from PLANMONTH where Prodmonth = " + theProdMonth + " and Sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "'  and WorkplaceId = '" + WP + "' and activity = " + theActivity + " and plancode = 'MP'  \r\n\r\n ";

                    _dbManPlanmonthSTP.SqlStatement = _dbManPlanmonthSTP.SqlStatement +
                    "insert into [dbo].[PLANMONTH]  " +
                    "(Prodmonth,SectionId,WorkplaceId,Activity,PlanCode,IsCubics, \r\n" +
                    "  StartDate,OrgUnitDay,OrgUnitAfternoon,OrgUnitNight ,    \r\n" +
                    "  Locked,Auth,SQM,ReefSQM,WasteSQM, FL,SW,CW ,CMGT,GT,EndDate ,   \r\n" +
                    "  Metresadvance, ReefAdv, WasteAdv  ,     \r\n" +
                    "  CubicMetres,CubicsReef,CubicsWaste, MOCycle, MOCycleNum ,DefaultCycle , Tons , Density , \r\n" +
                    "  MonthlyTotalSQM,MonthlyWatseSQM,MonthlyReefSQM, \r\n" +
                    "  TargetID) values\r\n" +
                    "('" + theProdMonth + "','" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "','" + WP + "'," + theActivity + ", 'MP', 'N', \r\n" +
                    "  '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitDay"].ToString()) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitAfternoon"].ToString()) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitNight"].ToString()) + "'   ,  \r\n" +
                    " 1, 'Y'  ,'" + TotlSqmLbl.Text + "','" + TotlSqmLbl.Text + "',0.00 ,'" + FL + "','" + SW + "','" + CW + "','" + CMGT + "','" + MoCycleGT + "','" + String.Format("{0:yyyy-MM-dd}", tblPrePlanData.Rows[currentRow]["EndDate"].ToString()) + "' ,  \r\n" +
                    "  " + tblPrePlanData.Rows[currentRow]["Metresadvance"].ToString() + ", '" + tblPrePlanData.Rows[currentRow]["ReefAdv"].ToString() + "', '" + tblPrePlanData.Rows[currentRow]["WasteAdv"].ToString() + "' ,      \r\n" +
                    "  '" + TotCubics + "' , '" + tblPrePlanData.Rows[currentRow]["CubicsReef"].ToString() + "' ,'" + tblPrePlanData.Rows[currentRow]["CubicsWaste"].ToString() + "' , '" + SaveMoCycle + "', '" + saveMoSQM + "' , '" + SaveDefaultCycle + "' , '" + TotalTOns + "' , '" + density + "', \r\n" +
                    "  '" + tblPrePlanData.Rows[currentRow]["MonthlyTotalSQM"].ToString() + "','" + tblPrePlanData.Rows[currentRow]["MonthlyWatseSQM"].ToString() + "','" + tblPrePlanData.Rows[currentRow]["MonthlyReefSQM"].ToString() + "' ,  \r\n" +
                    "  '" + tblPrePlanData.Rows[currentRow]["TargetID"].ToString() + "') ";
                    _dbManPlanmonthSTP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManPlanmonthSTP.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManPlanmonthSTP.ExecuteInstruction();
                }
                else
                { 

                    MWDataManager.clsDataAccess _dbManPlanmonthSTP = new MWDataManager.clsDataAccess();
                    _dbManPlanmonthSTP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManPlanmonthSTP.SqlStatement = " Delete from PLANMONTH where Prodmonth = " + theProdMonth + " and Sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "'  and WorkplaceId = '" + WP + "' and activity = " + theActivity + " and plancode = 'MP'  \r\n\r\n ";

                    _dbManPlanmonthSTP.SqlStatement = _dbManPlanmonthSTP.SqlStatement +
                    "insert into [dbo].[PLANMONTH]  " +
                    "(Prodmonth,SectionId,WorkplaceId,Activity,PlanCode,IsCubics, \r\n" +
                    "  StartDate,OrgUnitDay,OrgUnitAfternoon,OrgUnitNight ,    \r\n" +
                    "  Locked,Auth,SQM,ReefSQM,WasteSQM, FL,SW,CW ,CMGT,GT,EndDate ,   \r\n" +
                    "  Metresadvance, ReefAdv, WasteAdv  ,     \r\n" +
                    "  CubicMetres,CubicsReef,CubicsWaste, MOCycle, MOCycleNum ,DefaultCycle , Tons , Density , \r\n" +
                    "  MonthlyTotalSQM,MonthlyWatseSQM,MonthlyReefSQM, \r\n" +
                    "  TargetID) values\r\n" +
                    "('" + theProdMonth + "','" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "','" + WP + "'," + theActivity + ", 'MP', 'N', \r\n" +
                    "  '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitDay"].ToString()) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitAfternoon"].ToString()) + "','" + procs.ExtractBeforeColon(tblPrePlanData.Rows[currentRow]["OrgUnitNight"].ToString()) + "'   ,  \r\n" +
                    " 0,'N'  ,'" + TotlSqmLbl.Text + "','" + TotlSqmLbl.Text + "',0.00 ,'" + FL + "','" + SW + "','" + CW + "','" + CMGT + "','" + MoCycleGT + "','" + String.Format("{0:yyyy-MM-dd}", tblPrePlanData.Rows[currentRow]["EndDate"].ToString()) + "' ,  \r\n" +
                    "  " + tblPrePlanData.Rows[currentRow]["Metresadvance"].ToString() + ", '" + tblPrePlanData.Rows[currentRow]["ReefAdv"].ToString() + "', '" + tblPrePlanData.Rows[currentRow]["WasteAdv"].ToString() + "' ,      \r\n" +
                    "  '" + TotCubics + "' ,'" + tblPrePlanData.Rows[currentRow]["CubicsReef"].ToString() + "' ,'" + tblPrePlanData.Rows[currentRow]["CubicsWaste"].ToString() + "' , '" + SaveMoCycle + "', '" + saveMoSQM + "' , '" + SaveDefaultCycle + "' , '" + TotalTOns + "' , '" + density + "', \r\n" +
                    "  '" + tblPrePlanData.Rows[currentRow]["MonthlyTotalSQM"].ToString() + "','" + tblPrePlanData.Rows[currentRow]["MonthlyWatseSQM"].ToString() + "','" + tblPrePlanData.Rows[currentRow]["MonthlyReefSQM"].ToString() + "' ,  \r\n" +
                    "  '" + tblPrePlanData.Rows[currentRow]["TargetID"].ToString() + "') ";
                    _dbManPlanmonthSTP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManPlanmonthSTP.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManPlanmonthSTP.ExecuteInstruction();
                }

                MWDataManager.clsDataAccess _dbManPlanmonthFixEX = new MWDataManager.clsDataAccess();
                _dbManPlanmonthFixEX.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManPlanmonthFixEX.SqlStatement = "update  [dbo].[planmonth] set mocycle =  REPLACE(substring(mocycle+'                                                                                XX',1,188), 'EX', '  ')  \r\n" +
                                                    "where prodmonth = '" + theProdMonth + "'   \r\n\r\n";

                _dbManPlanmonthFixEX.SqlStatement = _dbManPlanmonthFixEX.SqlStatement + "update  [dbo].[planmonth] set mocycle =  substring(replace(mocycle,'XX','')+'                                                                                                                                   ',1,200)+'XX'  \r\n" +
                                                    "where prodmonth = '" + theProdMonth + "'   \r\n\r\n";

                _dbManPlanmonthFixEX.SqlStatement = _dbManPlanmonthFixEX.SqlStatement + "update  [dbo].[planmonth] set DefaultCycle =  REPLACE(substring(DefaultCycle+'                                                                                XX',1,188), 'EX', '  ')  \r\n" +
                                                    "where prodmonth = '" + theProdMonth + "'   \r\n\r\n";

                _dbManPlanmonthFixEX.SqlStatement = _dbManPlanmonthFixEX.SqlStatement + "update  [dbo].[planmonth] set DefaultCycle =  substring(replace(DefaultCycle,'XX','')+'                                                                                                                                   ',1,200)+'XX'  \r\n" +
                                                    "where prodmonth = '" + theProdMonth + "'   ";

                _dbManPlanmonthFixEX.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManPlanmonthFixEX.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManPlanmonthFixEX.ExecuteInstruction();

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Planning saved", Color.CornflowerBlue);
            }
        }

        public void SQMCalc()
        {
            decimal sqmtotal = Convert.ToInt32(TotlSqmLbl.Text);
            int fl = Convert.ToInt32(FLlbl.Text);

            decimal dailycallsqm = Math.Round((Convert.ToDecimal(fl) * Convert.ToDecimal(0.8)), 0);

            if (theActivity == 1)
            {
                dailycallsqm = Math.Round(Convert.ToDecimal(devADVperBlast.Text), 2);
            }

            string aa = gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[1]).ToString();

            decimal MoProgsqm = 0;

            int blast = 0;

            for (int i = 1; i < 49; i++)
            {
                if (gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[i]).ToString()  == "BL" || gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[i]).ToString() == "SUBL")
                {
                    blast = blast + 1;
                    if (MoProgsqm < sqmtotal)
                    {
                        gvMoCycle.SetRowCellValue(1, gvMoCycle.Columns[i], dailycallsqm.ToString());


                        MoProgsqm = MoProgsqm + dailycallsqm;

                    }
                    else
                    {
                        gvMoCycle.SetRowCellValue(0, gvMoCycle.Columns[i], "");

                        //gvMoCycle.SetRowCellValue(2, gvMoCycle.Columns[i], "");
                    }
                }

                if (gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[i]).ToString() == "SR")
                {
                    blast = blast + 1;
                    if (MoProgsqm < sqmtotal)
                    {

                        gvMoCycle.SetRowCellValue(1, gvMoCycle.Columns[i],Convert.ToString( Math.Floor(dailycallsqm/2) ) );

                        //if(theActivity == 1)
                        //    gvMoCycle.SetRowCellValue(1, gvMoCycle.Columns[i], Convert.ToString(Math.Floor(dailycallsqm / 2)));

                        MoProgsqm = MoProgsqm + dailycallsqm;
                    }
                    else
                    {
                        gvMoCycle.SetRowCellValue(0, gvMoCycle.Columns[i], "");

                        //gvMoCycle.SetRowCellValue(2, gvMoCycle.Columns[i], "");
                    }
                }
            }            

          

            DefaultCyclelbl.Text = MoProgsqm.ToString();
            LoadMOSqm();


        }

        public void LoadMOSqm()
        {
            if (gvMoCycle.RowCount == 0)
            {
                return;
            }

            if (_CyclingType == "CycDriven")
            {
                decimal sqmtotal = Convert.ToDecimal(TotlSqmLbl.Text);
                int fl = Convert.ToInt32(FLlbl.Text);

                decimal dailycallsqm = Math.Round((Convert.ToDecimal(fl) * Convert.ToDecimal(0.8)), 0);

                if (theActivity == 1)
                {
                    dailycallsqm = Math.Round(( Convert.ToDecimal(devADVperBlast.Text)), 2);
                }

                string aa = gvMoCycle.GetRowCellValue(0, gvMoCycle.Columns[1]).ToString();

                decimal MoProgsqm = 0;

                int blast = 0;

                for (int i = 2; i < 50; i++)
                {
                    if (gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "BL" || gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "SUBL")
                    {
                        blast = blast + 1;

                            gvMoCycle.SetRowCellValue(3, gvMoCycle.Columns[i], dailycallsqm.ToString());

                            MoProgsqm = MoProgsqm + dailycallsqm;
                    }
                    else  if (gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "SR")
                    {
                        blast = blast + 1;

                        gvMoCycle.SetRowCellValue(3, gvMoCycle.Columns[i],Convert.ToString(   Math.Floor(dailycallsqm /2)   )    );

                        MoProgsqm = MoProgsqm + dailycallsqm;
                    }
                    else if(gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "OFF")
                    {
                        gvMoCycle.SetRowCellValue(3, gvMoCycle.Columns[i], "OFF");
                    }
                    else
                    {
                        gvMoCycle.SetRowCellValue(3, gvMoCycle.Columns[i], "");
                        //MoProgsqm =0;
                    }
                }

                TotlSqmLbl.Text = Convert.ToString(MoProgsqm);

            }
            else
            {
                int blast = 0;                

                for (int i = 1; i < 49; i++)
                {
                    if (gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "BL" || gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "SR" || gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "SUBL")
                    {
                        blast = blast + 1;
                    }
                }

                Blastlbl.Text = blast.ToString();

                decimal sqmperblats = 0;
                decimal Rem = 0;

                if (blast >= 1)
                    sqmperblats = Convert.ToDecimal(TotlSqmLbl.Text) / Convert.ToDecimal(blast);

                if (blast >= 1)
                    Rem = Convert.ToDecimal(TotlSqmLbl.Text) % Convert.ToDecimal(blast);

                decimal Remainingsqm = 0;

                if (Rem != 0)
                {
                    //zz = Math.Floor(sqmperblats);
                    sqmperblats = Math.Floor(sqmperblats);

                    Remainingsqm = Convert.ToDecimal(TotlSqmLbl.Text) - (blast * sqmperblats);

                }

                for (int i = 2; i < 50; i++)
                {
                    if (gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "BL" || gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "SR" || gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "SUBL")
                    {
                        gvMoCycle.SetRowCellValue(3, gvMoCycle.Columns[i], sqmperblats.ToString());

                        if (Remainingsqm > 0)
                        {
                            decimal currentSQM = Convert.ToDecimal(gvMoCycle.GetRowCellValue(3, gvMoCycle.Columns[i]).ToString());

                            gvMoCycle.SetRowCellValue(3, gvMoCycle.Columns[i], currentSQM + 1);

                            Remainingsqm = Remainingsqm - 1;
                        }
                    }
                    else if (gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i]).ToString() == "OFF")
                    {
                        gvMoCycle.SetRowCellValue(3, gvMoCycle.Columns[i], "OFF");
                    }
                    else
                    {
                        gvMoCycle.SetRowCellValue(3, gvMoCycle.Columns[i], "");
                    }
                }

            }

           
                
          }


        public void ResetSQM()
        {
            int currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);

            if (theActivity == 1)
                currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);

            if (currentRow < 0)
            {
                return;
            }

            if (tblPrePlanData.Rows[currentRow]["SectionID"].ToString() == "-1")
            {
                return;
            }

            string SaveWP = tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString();
            string SaveFL = tblPrePlanData.Rows[currentRow]["FL"].ToString();
            string SaveTotSqm = tblPrePlanData.Rows[currentRow]["SQM"].ToString();
            string SaveTotCall = "0";

            if (theActivity == 1)
            {
                SaveWP = tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString();
                SaveFL = tblPrePlanData.Rows[currentRow]["FL"].ToString();
                SaveTotSqm = tblPrePlanData.Rows[currentRow]["Metresadvance"].ToString();
            }

            if (theActivity != 1)
            {
                SaveTotCall = tblPrePlanData.Rows[currentRow]["callValue"].ToString();

                if (SaveTotSqm == "0" && SaveTotCall != "" || SaveTotCall == "0")
                {
                    SaveTotSqm = SaveTotCall;
                }
            }


            if (SaveTotSqm == "")
                SaveTotSqm = "0";


            TotlSqmLbl.Text = Math.Round(Convert.ToDecimal(SaveTotSqm), 0).ToString();
            FLlbl.Text = Math.Round(Convert.ToDecimal(SaveFL), 0).ToString();
        }


        public void SavePlanning()
        {
            for (int i = 2; i < 48; i++)
            {



            }

        }

        public void showLabour(bool value)
        {
           // value = true;
            switch (theActivity)
            {
                case 0:
                    viewPlanningStoping.OptionsView.ShowFooter = !value;
                    //pcCycle.Visible = value;
                   // gridControl1.Visible = value;
                    panelLabour.Visible = value;
                    if (value) // only load if value is true
                    {
                        if (tblPrePlanData != null)
                        {
                            int currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
                            if (Convert.ToBoolean(tblPrePlanData.Rows[currentRow]["Locked"].ToString()))
                            {
                                loadLabourStrength(theProdMonth, tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString(), tblPrePlanData.Rows[currentRow]["SectionID"].ToString());

                            }
                        }
                    }
                    break;
                case 1:
                    viewPlanningDevelopment.OptionsView.ShowFooter = !value;
                    panelLabour.Visible = value;
                    if (value) // only load if value is true
                    {
                        if (tblPrePlanData != null)
                        {
                            int currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);
                            if (Convert.ToBoolean(tblPrePlanData.Rows[currentRow]["Locked"].ToString()))
                            {
                                loadLabourStrength(theProdMonth, tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString(), tblPrePlanData.Rows[currentRow]["SectionID"].ToString());
                            }
                        }
                    }
                    break;
            }

        }

        private void loadLabourStrength(string prodmonth, string workplaceID, string sectionID)
        {
            _LabourData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _LabourData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _LabourData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _LabourData.SqlStatement = 	"select name_2 from section_complete where prodmonth=" + prodmonth + " and   SECTIONID = '" + sectionID + "' ";
           
            _LabourData.ExecuteInstruction();
            DataTable MOName = new DataTable();
            MOName = _LabourData.ResultsDataTable;
            foreach (DataRow dr in MOName.Rows)
            {
                _LabourData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _LabourData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _LabourData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _LabourData.SqlStatement = "";
                _LabourData.SqlStatement = " SELECT DATA.OrgUnitDay,DATA.Labourstrength,sum(DATA.CALVALUE) CALVALUE FROM " +
                                            " (select OrgUnitDay, Labourstrength, PM.REEFSQM + PM.WASTESQM CALVALUE from planmonth PM where PM.prodmonth = " + prodmonth + " and " +
                                            " PM.plancode='MP' AND PM.Activity = '" + theActivity.ToString () + "' and sectionID in " +
                                            " ( select distinct SECTIONID from SECTION_COMPLETE where PRODMONTH = " + prodmonth + " and    name_2 = '" + dr["name_2"].ToString() + "'))DATA group by DATA.Labourstrength,DATA.OrgUnitDay ";

                _LabourData.ExecuteInstruction();
            }
            if (_LabourData.ResultsDataTable.Rows.Count != 0)
            {

                gridControl1.DataSource = _LabourData.ResultsDataTable;


            }
            else
            {
                showLabour(false);
            }

        }

        private void UpdateApproveFlag(string prodmonth, string workplaceID, string sectionID)
        {
            _CycleData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _CycleData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _CycleData.queryReturnType = MWDataManager.ReturnType.longNumber;
            _CycleData.SqlStatement = "Update Planmonth set Locked = 0 where Prodmonth = "+ prodmonth+" and SectionID = '"+sectionID+"' and WorkplaceID = '"+ workplaceID + "' and Plancode = 'MP' and auth is null and lockedby is null and Locked = 1";
            _CycleData.ExecuteInstruction();
        }

        private void loadCyclePlan(string prodmonth, string workplaceID, string sectionID)
        {

            StringBuilder sb = new StringBuilder();

            
            _CycleData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _CycleData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _CycleData.queryReturnType = MWDataManager.ReturnType.DataTable;
            SqlParameter[] _paramCollection = 
                {
                    _CycleData.CreateParameter("@theProdmonth", SqlDbType.Int, 0,Convert.ToInt32(prodmonth)),
                    _CycleData.CreateParameter("@theWorkplaceID", SqlDbType.VarChar, 20,workplaceID),
                    _CycleData.CreateParameter("@theSectionID", SqlDbType.VarChar, 20,sectionID),
                };
            _CycleData.SqlStatement = "sp_Get_Cycle";
            _CycleData.ParamCollection = _paramCollection;
            _CycleData.ExecuteInstruction();
            if (_CycleData.ResultsDataTable.Rows.Count != 0)
            {

                gcCycle.DataSource = _CycleData.ResultsDataTable;

                sb.Clear();
                sb.AppendLine("SELECT [CycleCode]  ,[Description], [CanBlast] FROM [CODE_CYCLE]");
                int count = 0;
                row_Cycle_ValeCube = -1;
                row_Cycle_CodeCube = -1;
                foreach( DataRow dr in _CycleData.ResultsDataTable.Rows)
                {

                    if (dr["RowType"].ToString() == "sDate")
                    {
                        row_Cycle_Date = count;
                    }

                    if (dr["RowType"].ToString() == "sValue")
                    {
                        row_Cycle_Vale = count;
                    }

                    if (dr["RowType"].ToString() == "sValueCube")
                    {
                        row_Cycle_ValeCube = count;
                    }


                    if (dr["RowType"].ToString() == "sCycleCode")
                    {
                        row_Cycle_Code = count;
                    }

                    if (dr["RowType"].ToString() == "sCycleCodeCube")
                    {
                        row_Cycle_CodeCube = count;
                    }

                    if (dr["RowType"].ToString() == "sInput")
                    {
                        row_Input = count;
                    }

                    count++;

                }

                for (int k = 50; k > 0; k--)
                {
                    if (_CycleData.ResultsDataTable.Rows[0]["Day" + Convert.ToString(k)].ToString() == "")
                    {
                        gvCycle.Columns.ColumnByName("gcolDay" + Convert.ToString(k)).Visible = false;
                    }
                }


                _CycleCodes.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _CycleCodes.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _CycleCodes.queryReturnType = MWDataManager.ReturnType.DataTable;
                _CycleCodes.SqlStatement = sb.ToString();
                clsDataResult DataResult = _CycleCodes.ExecuteInstruction();
                gcCycleCodes.DataSource = _CycleCodes.ResultsDataTable;
            }

            else showCycle(false);


        }


        public void prepareReport()
        {


        }


        public DataTable LoadPrePlanningData(string prodMonth, string sectionidMO, int Activity)
        {
            replacewp = false;
            MWDataManager.clsDataAccess _PrePlanningData = new MWDataManager.clsDataAccess();
            _PrePlanningData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _PrePlanningData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _PrePlanningData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _PrePlanningData.SqlStatement = "sp_Load_Planning";

            SqlParameter[] _paramCollection = 
                {
                    _PrePlanningData.CreateParameter("@ProdMonth", SqlDbType.Int, 0,Convert.ToInt32(prodMonth )),
                    _PrePlanningData.CreateParameter("@Sectionid_2", SqlDbType.VarChar, 20,sectionidMO),
                    _PrePlanningData.CreateParameter("@Activity", SqlDbType.Int, 0,Activity),
                };


            _PrePlanningData.ParamCollection = _paramCollection;
            _PrePlanningData.ExecuteInstruction();
            _PrePlanningData.ResultsDataTable.Columns["OrgUnitNight"].DataType = typeof(string);


            if (Activity == 2)
            {
                MWDataManager.clsDataAccess _TestData = new MWDataManager.clsDataAccess();
                _TestData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _TestData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _TestData.queryReturnType = MWDataManager.ReturnType.DataTable;

                _TestData.SqlStatement = "SELECT *,WP.ReefWaste,WP.Description FROM dbo.PLANMONTH_SUNDRYMINING PP inner join WORKPLACE WP ON " +
                                         "   PP.WorkplaceID=WP.WorkplaceID left join SECTION_COMPLETE sc on " +
                                         "    pp.SectionID =sc.SECTIONID and " +
                                         "    pp.Prodmonth = sc.PRODMONTH WHERE PP.Activity =  " + Activity.ToString() + " AND  sc.Sectionid_2 = '" + sectionidMO + "' and PP.Prodmonth = " + prodMonth + " and PP.PlanCode ='MP' ";
                _TestData.ExecuteInstruction();

                DialogResult result;
                if (_TestData.ResultsDataTable.Rows.Count == 0 && TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miAllowImportPreviousMonth_HPASPlanningWPImportPreviousMonth_MinewareSystemsHarmonyPAS.ItemID) == 2)
                {

                    result = MessageBox.Show("You have not yet planned for this month. Do you want to load your plans from last month, to form a basis for this month's plan? ", "Unlock Data", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        _PrePlanningData.ResultsDataTable.Clear();
                        _PrePlanningData.ResultsDataTable.AcceptChanges();
                    }
                }




                _PrePlanningData.ResultsDataTable.AcceptChanges();
                return _PrePlanningData.ResultsDataTable;
            }
            else if (Activity == 8)
            {
                MWDataManager.clsDataAccess _TestData = new MWDataManager.clsDataAccess();
                _TestData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _TestData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _TestData.queryReturnType = MWDataManager.ReturnType.DataTable;

                _TestData.SqlStatement = "SELECT *,WP.ReefWaste,WP.Description FROM dbo.PLANMONTH_OLDGOLD PP inner join WORKPLACE WP ON " +
                                         "   PP.WorkplaceID=WP.WorkplaceID left join SECTION_COMPLETE sc on " +
                                         "    pp.SectionID =sc.SECTIONID and " +
                                         "    pp.Prodmonth = sc.PRODMONTH WHERE PP.Activity =  " + Activity.ToString() + " AND  sc.Sectionid_2 = '" + sectionidMO + "' and PP.Prodmonth = " + prodMonth + " and PP.PlanCode ='MP' ";
                _TestData.ExecuteInstruction();

                DialogResult result;
                if (_TestData.ResultsDataTable.Rows.Count == 0 && TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miAllowImportPreviousMonth_HPASPlanningWPImportPreviousMonth_MinewareSystemsHarmonyPAS.ItemID) == 2)
                {
                    result = MessageBox.Show("You have not yet planned for this month. Do you want to load your plans from last month, to form a basis for this month's plan? ", "Unlock Data", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        _PrePlanningData.ResultsDataTable.Clear();
                        _PrePlanningData.ResultsDataTable.AcceptChanges();
                    }
                }


                _PrePlanningData.ResultsDataTable.AcceptChanges();
                return _PrePlanningData.ResultsDataTable;


            }
            else
            {
                MWDataManager.clsDataAccess _TestData = new MWDataManager.clsDataAccess();
                _TestData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _TestData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _TestData.queryReturnType = MWDataManager.ReturnType.DataTable;

                _TestData.SqlStatement = "SELECT *,PP.ReefSQM,WP.ReefWaste FROM dbo.PLANMONTH PP inner join SECTION_COMPLETE SC ON \n" +
                                         "   PP.Prodmonth =  SC.PRODMONTH AND \n" +
                                         "   PP.Sectionid =  SC.Sectionid \n" +
                                         "   inner join WORKPLACE WP ON \n" +
                                         "   PP.Workplaceid=WP.WORKPLACEID  \n" +
                                         " WHERE PP.Activity =  " + Activity.ToString() +"\n" + 
                                         " AND  SC.Sectionid_2 = '" + sectionidMO + "'" +" \n" + 
                                         " and PP.Prodmonth = " + prodMonth;
                _TestData.ExecuteInstruction();

                DialogResult result;
                if (_TestData.ResultsDataTable.Rows.Count == 0 && TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miAllowImportPreviousMonth_HPASPlanningWPImportPreviousMonth_MinewareSystemsHarmonyPAS.ItemID) == 2)
                {

                    result = MessageBox.Show("You have not yet planned for this month. Do you want to load your plans from last month, to form a basis for this month's plan? ", "Unlock Data", MessageBoxButtons.YesNo);
                    replacewp = true;
                    if (result == DialogResult.No)
                    {
                        replacewp = false;
                        _PrePlanningData.ResultsDataTable.Clear();
                        _PrePlanningData.ResultsDataTable.AcceptChanges();
                    }
                }

                if (Activity.ToString() == "0" || Activity.ToString() == "1")
                {
                    DataRow[] customerRow = _PrePlanningData.ResultsDataTable.Select("IsCubics in ('Y')");

                    foreach (DataRow dr in customerRow)
                    {
                        DataRow[] customerRow2 = _PrePlanningData.ResultsDataTable.Select("WorkPlaceID = '" + dr["WorkPlaceID"] + "' and IsCubics = 'N'");

                        foreach (DataRow dr2 in customerRow2)
                        {
                            dr2["CubicMetres"] = dr["CubicMetres"];
                        }

                    }

                    for (int i = 0; i < customerRow.Length; i++)
                    {
                        customerRow[i].Delete();
                    }



                    _PrePlanningData.ResultsDataTable.AcceptChanges();
                    return _PrePlanningData.ResultsDataTable;
                }
                else
                {
                    _PrePlanningData.ResultsDataTable.AcceptChanges();
                    return _PrePlanningData.ResultsDataTable;
                }
            }
        }


        public DataTable LoadPrePlanningDataRevised(string prodMonth, string sectionidMO, int Activity)
        {
            MWDataManager.clsDataAccess _PrePlanningData = new MWDataManager.clsDataAccess();
            _PrePlanningData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _PrePlanningData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _PrePlanningData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _PrePlanningData.SqlStatement = "sp_Load_Planning";

            SqlParameter[] _paramCollection = 
                {
                    _PrePlanningData.CreateParameter("@ProdMonth", SqlDbType.Int, 0,Convert.ToInt32(prodMonth )),
                    _PrePlanningData.CreateParameter("@SectionID_2", SqlDbType.VarChar, 20,sectionidMO),
                    _PrePlanningData.CreateParameter("@Activity", SqlDbType.Int, 0,Activity),
                };



            _PrePlanningData.ParamCollection = _paramCollection;
            _PrePlanningData.ExecuteInstruction();


            if (Activity == 2)
            {
                MWDataManager.clsDataAccess _TestData = new MWDataManager.clsDataAccess();
                _TestData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _TestData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _TestData.queryReturnType = MWDataManager.ReturnType.DataTable;

                _TestData.SqlStatement = "SELECT *,WP.ReefWaste,WP.Description FROM dbo.PLANMONTH_SUNDRYMINING PP inner join WORKPLACE WP ON " +
                                         "   PP.WorkplaceID=WP.WorkplaceID left join SECTION_COMPLETE sc on " +
                                         "    pp.SectionID =sc.SECTIONID and " +
                                         "    pp.Prodmonth = sc.PRODMONTH WHERE PP.Activity =  " + Activity.ToString() + " AND  sc.Sectionid_2 = '" + sectionidMO + "' and PP.Prodmonth = " + prodMonth + " and PP.PlanCode ='MP' ";
                _TestData.ExecuteInstruction();

                DialogResult result;
                if (_TestData.ResultsDataTable.Rows.Count == 0)
                {

                    result = MessageBox.Show("You have not yet planned for this month. Do you want to load your plans from last month, to form a basis for this month's plan? ", "Unlock Data", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        _PrePlanningData.ResultsDataTable.Clear();
                        _PrePlanningData.ResultsDataTable.AcceptChanges();
                    }
                }



                _PrePlanningData.ResultsDataTable.AcceptChanges();
                return _PrePlanningData.ResultsDataTable;
            }
            else if (Activity == 8)
            {
                MWDataManager.clsDataAccess _TestData = new MWDataManager.clsDataAccess();
                _TestData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _TestData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _TestData.queryReturnType = MWDataManager.ReturnType.DataTable;

                _TestData.SqlStatement = "SELECT *,WP.ReefWaste,WP.Description FROM dbo.PLANMONTH_OLDGOLD PP inner join WORKPLACE WP ON " +
                                         "   PP.WorkplaceID=WP.WorkplaceID left join SECTION_COMPLETE sc on " +
                                         "    pp.SectionID =sc.SECTIONID and " +
                                         "    pp.Prodmonth = sc.PRODMONTH WHERE PP.Activity =  " + Activity.ToString() + " AND  sc.Sectionid_2 = '" + sectionidMO + "' and PP.Prodmonth = " + prodMonth + " and PP.PlanCode ='MP' ";
                _TestData.ExecuteInstruction();

                DialogResult result;
                if (_TestData.ResultsDataTable.Rows.Count == 0)
                {

                    result = MessageBox.Show("You have not yet planned for this month. Do you want to load your plans from last month, to form a basis for this month's plan? ", "Unlock Data", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        _PrePlanningData.ResultsDataTable.Clear();
                        _PrePlanningData.ResultsDataTable.AcceptChanges();
                    }
                }



                _PrePlanningData.ResultsDataTable.AcceptChanges();
                return _PrePlanningData.ResultsDataTable;


            }


            else
            {

                MWDataManager.clsDataAccess _TestData = new MWDataManager.clsDataAccess();
                _TestData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _TestData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _TestData.queryReturnType = MWDataManager.ReturnType.DataTable;

                _TestData.SqlStatement = "SELECT *,PP.ReefSQM,WP.ReefWaste FROM dbo.PLANMONTH PP inner join WORKPLACE WP ON " +
                                         "   PP.WorkplaceDesc=WP.DESCRIPTION and " +
                                         "   PP.Workplaceid=WP.WORKPLACEID WHERE PP.Activity =  " + Activity.ToString() + " AND  PP.Sectionid_2 = '" + sectionidMO + "' and PP.Prodmonth = " + prodMonth;
                _TestData.ExecuteInstruction();



                if (Activity.ToString() == "0" || Activity.ToString() == "1")
                {
                    DataRow[] customerRow = _PrePlanningData.ResultsDataTable.Select("IsCubics = 'Y'");

                    foreach (DataRow dr in customerRow)
                    {
                        DataRow[] customerRow2 = _PrePlanningData.ResultsDataTable.Select("WorkPlaceID = '" + dr["WorkPlaceID"] + "' and IsCubics = 'N' and Locked='True'  ");

                        foreach (DataRow dr2 in customerRow2)
                        {
                            dr2["CubicMetres"] = dr["CubicMetres"];
                        }

                    }

                    for (int i = 0; i < customerRow.Length; i++)
                    {
                        customerRow[i].Delete();
                    }

                    DataTable dt = new DataTable();
                    if (_PrePlanningData.ResultsDataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No data available to display", "", MessageBoxButtons.OK);
                    }
                    else
                    {
                        var table = _PrePlanningData.ResultsDataTable.Select("Locked=True");


                        if (table.Length == 0)
                        {
                          

                         
                                MessageBox.Show("No data available to display", "", MessageBoxButtons.OK);

                        }
                            else
                            {
                                dt = _PrePlanningData.ResultsDataTable.Select("Locked=True").CopyToDataTable();
                                _PrePlanningData.ResultsDataTable.AcceptChanges();
                            }

                       _PrePlanningData.ResultsDataTable.AcceptChanges();
                        //}
                    }
                    return dt;
                }
                else
                {
                    return dt;
                }


            }   //}

        }


        public void Unapprove()
        {
            int currentRow = 0;
            if (theActivity == 0)
            {
                currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
            }
            else { currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle); }



        }

        private void LoadMiningMethod()
        {

            CPMBusinessLayer.clsBusinessLayer bl = new CPMBusinessLayer.clsBusinessLayer();
            bl.SetsystemDBTag = this.theSystemDBTag;
            bl.SetUserCurrentInfo = this.UserCurrentInfo;
            reActivity.DataSource = bl.getMiningMethods(theActivity); //  ActivityList.ResultsDataTable;          
            reActivity.DisplayMember = "Description";
            reActivity.ValueMember = "TargetID";

        }

        private void LoadSundryMiningDescription()
        {
            CPMBusinessLayer.clsBusinessLayer bl = new CPMBusinessLayer.clsBusinessLayer();
            bl.SetsystemDBTag = this.theSystemDBTag;
            bl.SetUserCurrentInfo = this.UserCurrentInfo;

            SundryMining.DataSource = bl.getSundryMiningActivity(theActivity);
            sundry = bl.getSundryMiningActivity(theActivity);
            SundryMining.DisplayMember = "SMDescription";
            SundryMining.ValueMember = "SMID";
        }

        private void LoadSweepVampDescription()
        {
            CPMBusinessLayer.clsBusinessLayer bl = new CPMBusinessLayer.clsBusinessLayer();
            bl.SetsystemDBTag = this.theSystemDBTag;
            bl.SetUserCurrentInfo = this.UserCurrentInfo;

            activitysweepsvamps.DataSource = bl.getSweepsVampsActivity(theActivity);

            sweepsvamps = bl.getSweepsVampsActivity(theActivity);

            activitysweepsvamps.DisplayMember = "Activity";
            activitysweepsvamps.ValueMember = "OGID";
        }

        private void LoadDepthRangeDescription()
        {
            CPMBusinessLayer.clsBusinessLayer bl = new CPMBusinessLayer.clsBusinessLayer();
            bl.SetsystemDBTag = this.theSystemDBTag;
            bl.SetUserCurrentInfo = this.UserCurrentInfo;


            depthrangeedit.DataSource = bl.getDepthRange(theActivity);
            depthrangeedit.DisplayMember = "DepthRange";
            depthrangeedit.ValueMember = "DepthRange";
        }


        private void LoadDrillRig()
        {
            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _MinerData.SqlStatement = "SELECT '' DRID union " +
                                      "select distinct DRID from DRILLRIG_PROJECT  DRP " +
                                      "INNER JOIN vw_Workplace_WorkArea WPWA ON " +
                                      "DRP.Project = WPWA.Project";
            _MinerData.ExecuteInstruction();
            reDrillRig.DataSource = _MinerData.ResultsDataTable;
            reDrillRig.DisplayMember = "DRID";
            reDrillRig.ValueMember = "DRID";

        }

        public void LoadMinerList(string prodMonth, string sectionidMO)
        {
            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;


            _MinerData.SqlStatement = "select distinct a.SECTIONID, Name from section_complete a inner join SECCAL b on " +
                                      "A.PRODMONTH = B.Prodmonth AND " +
                                      "A.SECTIONID_1 = B.SectionID " +
                                      "where A.prodmonth = '" + prodMonth + "' and a.SECTIONID_2 = '" + sectionidMO + "' ORDER BY a.Name";

            _MinerData.ExecuteInstruction();
            reMinerSelection.DataSource = _MinerData.ResultsDataTable;
            reMinerSelection.DisplayMember = "Name";
            reMinerSelection.ValueMember = "SECTIONID";

            reMinerSelection.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

        }


        public void LoadShiftBossList(string prodMonth, string sectionidMO)
        {
            //MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            //_MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;


            //_MinerData.SqlStatement = "select distinct a.SECTIONID_1, Name_1 from section_complete a inner join SECCAL b on " +
            //                          "A.PRODMONTH = B.Prodmonth AND " +
            //                          "A.SECTIONID_1 = B.SectionID " +
            //                          "where A.prodmonth = '" + prodMonth + "' and a.SECTIONID_2 = '" + sectionidMO + "' ORDER BY a.Name_1";

            //_MinerData.ExecuteInstruction();
            //reShiftBossSelect.DataSource = _MinerData.ResultsDataTable;
            //reShiftBossSelect.DisplayMember = "Name_1";
            //reShiftBossSelect.ValueMember = "SECTIONID_1";

            //reShiftBossSelect.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

            //bcShiftBoss.

        }


       

        public DataTable GetOrgUnits(string prodMonth, string sectionidMO, string theOrg)
        {


            MWDataManager.clsDataAccess _OrgUnitsData = new MWDataManager.clsDataAccess();
            _OrgUnitsData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _OrgUnitsData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _OrgUnitsData.queryReturnType = MWDataManager.ReturnType.DataTable;
            if (theOrg == "NONE")
            {
                _OrgUnitsData.SqlStatement = "SELECT 1 thepos,'' Crew_Org " +
                                             " UNION " +
                                             " SELECT 2 thepos,'Contractor' Crew_Org" +
                                             " UNION " +
                                             "SELECT 3 thepos, GangNo Crew_Org FROM [CREW]";
            }
            else
            {
                _OrgUnitsData.SqlStatement = "SELECT 1 thepos,'' Crew_Org " +
                             " UNION " +
                             "SELECT 3 thepos, GangNo Crew_Org FROM CREW";

            }
            _OrgUnitsData.ExecuteInstruction();


            return _OrgUnitsData.ResultsDataTable;
        }

        public void LoadOrgUnits(string prodMonth, string sectionidMO)
        {
            MWDataManager.clsDataAccess _OrgUnitsData = new MWDataManager.clsDataAccess();
            _OrgUnitsData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _OrgUnitsData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _OrgUnitsData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _OrgUnitsData.SqlStatement = "SELECT 1 thepos,'' Crew_Org, ''GangNo " +
                                         " UNION " +
                                         " SELECT 2 thepos,'Contractor'+':'+'0' Crew_Org, '0'GangNo " +
                                         " UNION " +
                                         "SELECT 3 thepos, GangNo+':'+ CrewName+'('+CrewNo+')' Crew_Org,GangNo  FROM CREW --where GangNo like '" + theSectionIDMO+"%' ";
            //      " ORDER BY SECTIONID";
            _OrgUnitsData.ExecuteInstruction();

            reOrgDaySelection.DataSource = _OrgUnitsData.ResultsDataTable;
            reOrgDaySelection.DisplayMember = "Crew_Org";
            reOrgDaySelection.ValueMember = "GangNo";
            reOrgDaySelection.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

            reOrgNightSelection.DataSource = _OrgUnitsData.ResultsDataTable;
            reOrgNightSelection.DisplayMember = "Crew_Org";
            reOrgNightSelection.ValueMember = "GangNo";
            reOrgNightSelection.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

            reOrgAfternoon.DataSource = _OrgUnitsData.ResultsDataTable;
            reOrgAfternoon.DisplayMember = "Crew_Org";
            reOrgAfternoon.ValueMember = "GangNo";
            reOrgAfternoon.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

            reOrgRoving.DataSource = _OrgUnitsData.ResultsDataTable;
            reOrgRoving.DisplayMember = "Crew_Org";
            reOrgRoving.ValueMember = "GangNo";
            reOrgRoving.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
        }

        public void updateReadOnly()
        {

        }

        private void ColumnEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }


        private void ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {

        }


        private void viewPlanningStoping_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView view = sender as GridView;
            var app = viewPlanningStoping.GetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["Locked"]);
            string approved = Convert.ToString(app);
            if (e.Column == gcOrgNight)
            {
                // string abc = e.CellValue.ToString();
                var targetid = viewPlanningStoping.GetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["TargetID"]);

                MWDataManager.clsDataAccess _MiningMethodNeed = new MWDataManager.clsDataAccess();
                _MiningMethodNeed.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                _MiningMethodNeed.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                _MiningMethodNeed.queryReturnType = MWDataManager.ReturnType.DataTable;
                _MiningMethodNeed.SqlStatement = "[sp_MiningMethod_NightCrew]";

                if (targetid is DBNull || Convert.ToString(targetid) == "")
                {
                    MessageBox.Show("Please enter a valid Mining method", "", MessageBoxButtons.OK);

                }
                else
                {
                    SqlParameter[] _paramCollection = 
                                {
                                    _MiningMethodNeed.CreateParameter("@TargetID", SqlDbType.Int, 0,Convert.ToInt32(targetid)),
                                };

                    _MiningMethodNeed.ParamCollection = _paramCollection;
                    _MiningMethodNeed.ExecuteInstruction();

                    DataTable dt = new DataTable();

                    string abcd = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["OrgUnitNight"]).ToString();
                    dt = _MiningMethodNeed.ResultsDataTable;
                }
            }
            if (approved == "False")


            {
                viewPlanningStoping.OptionsBehavior.ReadOnly = false;
                
            }

            else
            {
                viewPlanningStoping.OptionsBehavior.ReadOnly = true;
            }
        }

        private void viewPlanningDevelopment_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView view = sender as GridView;
            // var value = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["Night"]);
            // var abc = view.GetFocusedDisplayText();
            //string abbc = view.ActiveEditor.EditValue.ToString();
            // var value = viewPlanningStoping.GetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["OrgUnitNight"]);
            //newresult = Convert.ToString(value);
            // var app = viewPlanningStoping.GetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["Locked"]);
            var app = viewPlanningDevelopment.GetRowCellValue(viewPlanningDevelopment.FocusedRowHandle, viewPlanningDevelopment.Columns["Locked"]);
            string approved = Convert.ToString(app);
            if (e.Column == bcNightCrewDev)
            {
                // string abc = e.CellValue.ToString();
                var targetid = viewPlanningDevelopment.GetRowCellValue(viewPlanningDevelopment.FocusedRowHandle, viewPlanningDevelopment.Columns["TargetID"]);

                MWDataManager.clsDataAccess _MiningMethodNeed = new MWDataManager.clsDataAccess();
                _MiningMethodNeed.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                _MiningMethodNeed.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                _MiningMethodNeed.queryReturnType = MWDataManager.ReturnType.DataTable;
                _MiningMethodNeed.SqlStatement = "[sp_MiningMethod_NightCrew]";

                if (targetid is DBNull || Convert.ToString(targetid) == "")
                {
                    MessageBox.Show("Please enter a valid Mining method", "", MessageBoxButtons.OK);

                }
                else
                {
                    SqlParameter[] _paramCollection = 
                                {
                                    _MiningMethodNeed.CreateParameter("@TargetID", SqlDbType.Int, 0,Convert.ToInt32(targetid)),
                                    //_MiningMethodNeed.CreateParameter("@Sectionid_2", SqlDbType.VarChar,20 ,theSectionIDMO),
                                    //_MiningMethodNeed.CreateParameter("@WPDESCRIPTION", SqlDbType.VarChar,30 ,WPDesc),
                                };

                    _MiningMethodNeed.ParamCollection = _paramCollection;
                    _MiningMethodNeed.ExecuteInstruction();

                    DataTable dt = new DataTable();
                    //GridView view = sender as GridView;

                    string abcd = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["OrgUnitNight"]).ToString();
                    dt = _MiningMethodNeed.ResultsDataTable;
                }
            }

            if (approved == "False")

            {
                viewPlanningDevelopment.OptionsBehavior.ReadOnly = false;

            }

            else
            {
                viewPlanningDevelopment.OptionsBehavior.ReadOnly = true;
            }
        }


        public void gcOrgNight_ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            viewPlanningStoping.PostEditor();
            BandedGridColumnLookUp lkp = sender as BandedGridColumnLookUp;
            var amba = viewPlanningStoping.GetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["OrgUnitDay"]);
            string con = Convert.ToString(amba);
            var amba1 = viewPlanningStoping.GetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["OrgUnitNight"]);
            string con1 = Convert.ToString(amba1);
            //var amba=view.GetRowCellValue (view.FocusedRowHandle ,view.Columns["OrgUnitNight"]);
            string result = Convert.ToString(amba);
            string expression = "OrgUnitDay = '" + con + "' and OrgUnitNight<>'' and Locked<> 'True' ";
            DataRow[] ds;
            ds = tblPrePlanData.Select(expression);
            if (con1 != "")
            {
                for (int i = 0; i < ds.Length; i++)
                {
                    ds[i]["OrgUnitNight"] = con1;
                }
            }
        }

        public void gcSundryMiningDesc_ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            viewPlanningSundry.PostEditor();
            var amba = viewPlanningSundry.GetRowCellValue(viewPlanningSundry.FocusedRowHandle, viewPlanningSundry.Columns["SMID"]);
            string con = Convert.ToString(amba);
            string expression = "SMID='" + con + "'";
            DataRow[] ds;
            ds = sundry.Select(expression);
            if (con != "")
            {
                string sundry1 = ds[0]["UnitType"].ToString();
                viewPlanningSundry.SetRowCellValue(viewPlanningSundry.FocusedRowHandle, viewPlanningSundry.Columns["UnitType"], sundry1);
            }
        }

        public void gcSweepsvampsMiningDesc_ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            viewPlanningSweepVamp.PostEditor();

            var amba = viewPlanningSweepVamp.GetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["TheID"]);
            string con = Convert.ToString(amba);
            string expression = "OGID='" + con + "'";
            DataRow[] ds;
            ds = sweepsvamps.Select(expression);

            if (con != "")
            {
                string sweep1 = ds[0]["UnitType"].ToString();
                viewPlanningSweepVamp.SetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["UnitType"], sweep1);
            }
        }

        public void bandedGridColumn58_ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {


                GridView view = sender as GridView;
                viewPlanningSweepVamp.PostEditor();
                var amba = viewPlanningSweepVamp.GetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["Call"]);
                var amba1 = viewPlanningSweepVamp.GetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["ActualDepth"]);
                var amba2 = viewPlanningSweepVamp.GetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["gt"]);
                decimal sqm = Convert.ToDecimal(amba);
                decimal depth = Convert.ToDecimal(amba1);
                decimal grade = Convert.ToDecimal(amba2);
                int content=Convert.ToInt32((sqm * depth / 100) * TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).BrokenRockDensity * grade);
                string CONTENT=Convert .ToString (content );
                viewPlanningSweepVamp.SetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["content"], CONTENT);
        }

        public void gcCall_ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            viewPlanningSweepVamp.PostEditor();
            var amba = viewPlanningSweepVamp.GetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["Call"]);
            var amba1 = viewPlanningSweepVamp.GetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["DepthRange"]);
            string depthrange = Convert.ToString(amba1);
            double amb = Convert.ToDouble(amba);
            if (depthrange == "0cm-5cm")
            {
                double a = 0.2 * amb;
                // string ab= Convert .ToString (a);
                viewPlanningSweepVamp.SetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["ccall"], a);
            }
            else if (depthrange == "6cm-15cm")
            {
                double a = 0.5 * amb;
                // string ab= Convert .ToString (a);
                viewPlanningSweepVamp.SetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["ccall"], a);
            }
            else if (depthrange == "16cm-50cm")
            {
                double a = 0.8 * amb;
                // string ab= Convert .ToString (a);
                viewPlanningSweepVamp.SetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["ccall"], a);
            }
            else
            {
                double a = 1 * amb;
                // string ab= Convert .ToString (a);
                viewPlanningSweepVamp.SetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["ccall"], a);
            }

        }

        public void gcDepthRange_ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            viewPlanningSweepVamp.PostEditor();
            var amba = viewPlanningSweepVamp.GetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["Call"]);
            var amba1 = viewPlanningSweepVamp.GetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["DepthRange"]);
            string depthrange = Convert.ToString(amba1);
            double amb = Convert.ToDouble(amba);
            if (depthrange == "0cm-5cm")
            {
                depth = "";
                depth = "0";
                double a = 0.2 * amb;
                // string ab= Convert .ToString (a);
                viewPlanningSweepVamp.SetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["ccall"], a);
            }
            else if (depthrange == "6cm-15cm")
            {
                depth = "";
                depth = "6";
                double a = 0.5 * amb;
                // string ab= Convert .ToString (a);
                viewPlanningSweepVamp.SetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["ccall"], a);
            }
            else if (depthrange == "16cm-50cm")
            {
                depth = "";
                depth = "16";
                double a = 0.8 * amb;
                // string ab= Convert .ToString (a);
                viewPlanningSweepVamp.SetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["ccall"], a);
            }
            else
            {
                depth = "";
                depth = "51";
                double a = 1 * amb;
                // string ab= Convert .ToString (a);
                viewPlanningSweepVamp.SetRowCellValue(viewPlanningSweepVamp.FocusedRowHandle, viewPlanningSweepVamp.Columns["ccall"], a);
            }

        }

        public void setReadonly(bool setReadonly,string message)
        {
            if (setReadonly)
            {
                labReadonly.Text = message;
                labReadonly.Visible = true;
            }
            else
            {
                labReadonly.Visible = false;
            }

        }


        public Boolean loadPreplanning(string prodMonth, string sectionidMO, int activity)
        {
            bool theReturn = true;

            
            #region test is calendar dates is valid. If not exit
            clsResults calValid = PlanningClass.ValidateSectionCalender();
            if (calValid.Successfull)
            {
                reStartDate.MinValue = PlanningClass.theBeginDate;
                reStartDate.MaxValue = PlanningClass.theEndDate;
                reEndDate.MinValue = PlanningClass.theBeginDate;
                reEndDate.MaxValue = PlanningClass.theEndDate;
            }
            else
            {
                theMessage.viewMessage(MessageType.Warning, "INVALID CALENDARS", calValid.Message.ToString(), ButtonTypes.OK, MessageDisplayType.FullScreen);
                theReturn = calValid.Successfull;
                return theReturn;
            }
            #endregion
			if (activity == 0)
            {
                editActi.Text = "Stoping";
            }
            if (activity == 1)
            {
                editActi.Text = "Development";
            }
            if (activity == 2)
            {
                editActi.Text = "Sundry";
            }
            if (activity == 8)
            {
                editActi.Text = "Sweep & Vamps";
            }

            MainGrid.RefreshDataSource();            
            theActivity = activity;
            theProdMonth = prodMonth;
            theSectionIDMO = sectionidMO;
            editProdmonth.Text = prodMonth;
            gcMiningMethod.ColumnEdit.EditValueChanging += ColumnEdit_EditValueChanging;
            gcMiningMethod.ColumnEdit.EditValueChanged += ColumnEdit_EditValueChanged;
            gcOrgNight.ColumnEdit.EditValueChanged += gcOrgNight_ColumnEdit_EditValueChanged;
            viewPlanningStoping.RowCellClick += viewPlanningStoping_RowCellClick;
            viewPlanningDevelopment.RowCellClick += viewPlanningDevelopment_RowCellClick;
            gcSundryMiningDesc.ColumnEdit.EditValueChanged += gcSundryMiningDesc_ColumnEdit_EditValueChanged;
            gcSweepsvampsMiningDesc.ColumnEdit.EditValueChanged += gcSweepsvampsMiningDesc_ColumnEdit_EditValueChanged;
            gcCall.ColumnEdit.EditValueChanged += gcCall_ColumnEdit_EditValueChanged;
            bandedGridColumn58.ColumnEdit.EditValueChanged += bandedGridColumn58_ColumnEdit_EditValueChanged;

           
            gcDepthRange.ColumnEdit.EditValueChanged += gcDepthRange_ColumnEdit_EditValueChanged;

            MWDataManager.clsDataAccess _MOSection = new MWDataManager.clsDataAccess();
            _MOSection.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MOSection.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MOSection.queryReturnType = MWDataManager.ReturnType.DataTable;
            _MOSection.SqlStatement = "SELECT DISTINCT NAME_2,Name_2 FROM dbo.SECTION_COMPLETE " +
                                      "WHERE PRODMONTH = '" + prodMonth + "' AND " +
                                      "SECTIONID_2 = '" + sectionidMO + "'";
            _MOSection.ExecuteInstruction();
            editMoSectionID.Text = sectionidMO;
            foreach (DataRow r in _MOSection.ResultsDataTable.Rows)
            {
                editMoSection.Text = r["Name_2"].ToString();
            }

            PlanningClass.SetCelarReadOnly(true);
            switch (activity)
            {
                case 0:
                    OpspalnSQM = 0;
                    OpsplanKG = 0;




                    MainGrid.DataSource = "";
                    if (tblPrePlanData.Rows.Count > 0)
                    {
                        tblPrePlanData.Rows.Clear();
                        tblPrePlanData.Columns.Clear();
                    }


                    if (!PlanningClass.isRevised)
                    {
                        viewPlanningStoping.RefreshData();
                        tblPrePlanData.Clear();
                        tblPrePlanData = LoadPrePlanningData(prodMonth, sectionidMO, activity);
                    }
                    else
                    {
                        viewPlanningStoping.RefreshData();
                        tblPrePlanData.Clear();
                        tblPrePlanData = LoadPrePlanningDataRevised(prodMonth, sectionidMO, activity);
                        viewPlanningStoping.RefreshData();
                    }
                    foreach (DataRow r in tblPrePlanData.Rows)
                    {
                        // if (r["Locked"].ToString ()== "True")
                        if ((r["Locked"]).ToString() == "Y")
                        {
                            gcMiner.OptionsColumn.AllowEdit = false;
                            gcOrgDay.OptionsColumn.AllowEdit = false;
                            gcOrgNight.OptionsColumn.AllowEdit = false;
                            gcOrgRoving.OptionsColumn.AllowEdit = false;
                            viewPlanningStoping.ActiveEditor.Enabled = false;
                        }
                        else
                        {
                            gcMiner.OptionsColumn.AllowEdit = true;
                            gcOrgDay.OptionsColumn.AllowEdit = true;
                            gcOrgNight.OptionsColumn.AllowEdit = true;
                            gcOrgRoving.OptionsColumn.AllowEdit = true;
                        }

                    }

                    MainGrid.DataSource = tblPrePlanData;

                    MainGrid.MainView = viewPlanningStoping;
                    LoadShiftBossList(prodMonth, sectionidMO);
                    LoadMinerList(prodMonth, sectionidMO);
                    LoadOrgUnits(prodMonth, sectionidMO);
                    LoadMiningMethod();
                    calVariance();
                    tblPrePlanData.Columns.Add(new DataColumn("NightCrewRequired", typeof(int)));
                    if (tblPrePlanData.Rows.Count == 0)
                    {
                        theReturn = false;
                    }
                    break;

                case 1:

                    double wastDev = 0;
                    double OpsPlanReefDev = 0;
                    double OpsPlanWastDev = 0;
                    double OpsPlanCap = 0;

                    MainGrid.DataSource = "";

                    if (tblPrePlanData.Rows.Count > 0)
                    {
                        tblPrePlanData.Rows.Clear();
                        tblPrePlanData.Columns.Clear();
                    }
                    // tblPrePlanData = LoadPrePlanningData(prodMonth, sectionidMO, activity);
                    if (!PlanningClass.isRevised)
                    {

                        viewPlanningDevelopment.RefreshData();
                        tblPrePlanData.Clear();
                        tblPrePlanData = LoadPrePlanningData(prodMonth, sectionidMO, activity);
                    }
                    else
                    {
                        viewPlanningDevelopment.RefreshData();
                        tblPrePlanData.Clear();
                        tblPrePlanData = LoadPrePlanningDataRevised(prodMonth, sectionidMO, activity);
                    }
                    MainGrid.DataSource = tblPrePlanData;

                    MainGrid.MainView = viewPlanningDevelopment;
                    LoadShiftBossList(prodMonth, sectionidMO);
                    LoadMinerList(prodMonth, sectionidMO);
                    LoadOrgUnits(prodMonth, sectionidMO);
                    LoadMiningMethod();
                    LoadDrillRig();
                    tblPrePlanData.Columns.Add(new DataColumn("NightCrewRequired", typeof(int)));
                    if (tblPrePlanData.Rows.Count == 0)
                    {
                        theReturn = false;
                    }
                    break;

                case 2:
                    MainGrid.DataSource = "";
                  //  cy
                    if (tblPrePlanData.Rows.Count > 0)
                    {
                        tblPrePlanData.Rows.Clear();
                        tblPrePlanData.Columns.Clear();
                    }
                    // tblPrePlanData = LoadPrePlanningData(prodMonth, sectionidMO, activity);
                    if (!PlanningClass.isRevised)
                    {

                        viewPlanningSundry.RefreshData();
                        tblPrePlanData.Clear();
                        tblPrePlanData = LoadPrePlanningData(prodMonth, sectionidMO, activity);
                    }
                    else
                    {
                        viewPlanningSundry.RefreshData();
                        tblPrePlanData.Clear();
                        tblPrePlanData = LoadPrePlanningDataRevised(prodMonth, sectionidMO, activity);
                    }
                    foreach (DataRow r in tblPrePlanData.Rows)
                    {
                        if ((r["Locked"]).ToString() == "Y")
                        {
                            bandedGridColumn1.OptionsColumn.AllowEdit = false;
                            bandedGridColumn2.OptionsColumn.AllowEdit = false;
                            bandedGridColumn3.OptionsColumn.AllowEdit = false;
                            viewPlanningSundry.ActiveEditor.Enabled = false;
                        }
                        else
                        {

                            bandedGridColumn1.OptionsColumn.AllowEdit = true;
                            bandedGridColumn2.OptionsColumn.AllowEdit = true;
                            bandedGridColumn3.OptionsColumn.AllowEdit = true;
                        }

                    }
                    MainGrid.DataSource = tblPrePlanData;

                    MainGrid.MainView = viewPlanningSundry;
                    LoadShiftBossList(prodMonth, sectionidMO);
                    LoadMinerList(prodMonth, sectionidMO);
                    LoadOrgUnits(prodMonth, sectionidMO);
                    LoadMiningMethod();
                    LoadSundryMiningDescription();
                    LoadDrillRig();
                    tblPrePlanData.Columns.Add(new DataColumn("NightCrewRequired", typeof(int)));
                    break;

                case 8:
                    MainGrid.DataSource = "";

                    if (tblPrePlanData.Rows.Count > 0)
                    {
                        tblPrePlanData.Rows.Clear();
                        tblPrePlanData.Columns.Clear();
                    }

                    if (!PlanningClass.isRevised)
                    {

                        viewPlanningSweepVamp.RefreshData();
                        tblPrePlanData.Clear();
                        tblPrePlanData = LoadPrePlanningData(prodMonth, sectionidMO, activity);
                    }
                    else
                    {
                        viewPlanningSweepVamp.RefreshData();
                        tblPrePlanData.Clear();
                        tblPrePlanData = LoadPrePlanningDataRevised(prodMonth, sectionidMO, activity);
                    }
                    foreach (DataRow r in tblPrePlanData.Rows)
                    {
                        // if (r["Locked"].ToString ()== "True")
                        if ((r["Locked"]).ToString() == "Y")
                        {

                            bandedGridColumn1.OptionsColumn.AllowEdit = false;
                            bandedGridColumn2.OptionsColumn.AllowEdit = false;
                            bandedGridColumn3.OptionsColumn.AllowEdit = false;
                            viewPlanningSweepVamp.ActiveEditor.Enabled = false;
                        }
                        else
                        {

                            bandedGridColumn1.OptionsColumn.AllowEdit = true;
                            bandedGridColumn2.OptionsColumn.AllowEdit = true;
                            bandedGridColumn3.OptionsColumn.AllowEdit = true;
                        }

                    }
                    MainGrid.DataSource = tblPrePlanData;

                    MainGrid.MainView = viewPlanningSweepVamp;
                    LoadShiftBossList(prodMonth, sectionidMO);
                    LoadMinerList(prodMonth, sectionidMO);
                    LoadOrgUnits(prodMonth, sectionidMO);
                    LoadMiningMethod();
                    LoadSundryMiningDescription();
                    LoadSweepVampDescription();
                    LoadDepthRangeDescription();
                    LoadDrillRig();
                    tblPrePlanData.Columns.Add(new DataColumn("NightCrewRequired", typeof(int)));
                    break;

            }
            return theReturn;

        }

        private void SetStopeSecurity()
        {
            Color readonlyColor = System.Drawing.Color.FromArgb(255, 255, 192);
            //  Set security for Face Length Column
            switch (TUserInfo.theSecurityLevel(TProductionGlobal.WPASMenuStructure.miEditFaceLength_HPASPlanningAllowFaceLengthEdit_MinewareSystemsHarmonyPAS.ItemID))
            {
                case 0:
                    gcFaceLength.OptionsColumn.AllowEdit = false;
                    gcFaceLength.OptionsColumn.ReadOnly = true;
                    gcFaceLength.AppearanceCell.BackColor = readonlyColor;
                    break;
                case 2:
                    gcFaceLength.OptionsColumn.AllowEdit = true;
                    gcFaceLength.OptionsColumn.ReadOnly = false;
                    gcFaceLength.AppearanceCell.BackColor = Color.White;
                    break;
            }

            //  Set security for SQM on off Column
            switch (TUserInfo.theSecurityLevel(TProductionGlobal.WPASMenuStructure.miEditSQM_HPASPlanningAllowEditSQM_MinewareSystemsHarmonyPAS.ItemID))
            {
                case 0:
                    gcSQM.OptionsColumn.AllowEdit = false;
                    gcSQM.OptionsColumn.ReadOnly = true;
                    gcSQM.AppearanceCell.BackColor = readonlyColor;

                    gcWastSQM.OptionsColumn.AllowEdit = false;
                    gcWastSQM.OptionsColumn.ReadOnly = true;
                    gcWastSQM.AppearanceCell.BackColor = readonlyColor;
                    break;
                case 2:
                    gcSQM.OptionsColumn.AllowEdit = true;
                    gcSQM.OptionsColumn.ReadOnly = false;
                    gcSQM.AppearanceCell.BackColor = Color.White;

                    gcWastSQM.OptionsColumn.AllowEdit = true;
                    gcWastSQM.OptionsColumn.ReadOnly = false;
                    gcWastSQM.AppearanceCell.BackColor = Color.White;
                    break;
            }

            gcCubicMetres.OptionsColumn.AllowEdit = false;
            gcCubicMetres.OptionsColumn.ReadOnly = true;
            gcCubicMetres.AppearanceCell.BackColor = readonlyColor;
            //  Set security for Cubes Column
            switch (TUserInfo.theSecurityLevel(TProductionGlobal.WPASMenuStructure.miEditSQM_HPASPlanningAllowEditSQM_MinewareSystemsHarmonyPAS.ItemID))
            {
                
                case 0:
                    gcOnReefCubicMetres.OptionsColumn.AllowEdit = false;
                    gcOnReefCubicMetres.OptionsColumn.ReadOnly = true;
                    gcOnReefCubicMetres.AppearanceCell.BackColor = readonlyColor;
                    gcOffReefCubicMetres.OptionsColumn.AllowEdit = false;
                    gcOffReefCubicMetres.OptionsColumn.ReadOnly = true;
                    gcOffReefCubicMetres.AppearanceCell.BackColor = readonlyColor;
                    break;
                case 2:
                    gcOnReefCubicMetres.OptionsColumn.AllowEdit = true;
                    gcOnReefCubicMetres.OptionsColumn.ReadOnly = false;
                    gcOnReefCubicMetres.AppearanceCell.BackColor = Color.White;
                    gcOffReefCubicMetres.OptionsColumn.AllowEdit = true;
                    gcOffReefCubicMetres.OptionsColumn.ReadOnly = false;
                    gcOffReefCubicMetres.AppearanceCell.BackColor = Color.White;
                    break;
            }

            // Set security for Actual Face Length Column
            switch (TUserInfo.theSecurityLevel("SCASW"))
            {
                case 0:
                    gcActualStopeWidth.OptionsColumn.AllowEdit = false;
                    gcActualStopeWidth.OptionsColumn.ReadOnly = true;
                    gcActualStopeWidth.AppearanceCell.BackColor = readonlyColor;
                    break;
                case 2:
                    gcActualStopeWidth.OptionsColumn.AllowEdit = true;
                    gcActualStopeWidth.OptionsColumn.ReadOnly = false;
                    gcActualStopeWidth.AppearanceCell.BackColor = Color.White;
                    break;
            }

            // Set security for Actual Face Length Column
            switch (TUserInfo.theSecurityLevel("SCCW"))
            {
                case 0:
                    gcChannelWidth.OptionsColumn.AllowEdit = false;
                    gcChannelWidth.OptionsColumn.ReadOnly = true;
                    gcChannelWidth.AppearanceCell.BackColor = readonlyColor;
                    break;
                case 2:
                    gcChannelWidth.OptionsColumn.AllowEdit = true;
                    gcChannelWidth.OptionsColumn.ReadOnly = false;
                    gcChannelWidth.AppearanceCell.BackColor = Color.White;
                    break;
            }

            switch (TUserInfo.theSecurityLevel("SCCMG"))
            {
                case 0:
                    gcCMGTValue.OptionsColumn.AllowEdit = false;
                    gcCMGTValue.OptionsColumn.ReadOnly = true;
                    gcCMGTValue.AppearanceCell.BackColor = readonlyColor;
                    break;
                case 2:
                    gcCMGTValue.OptionsColumn.AllowEdit = true;
                    gcCMGTValue.OptionsColumn.ReadOnly = false;
                    gcCMGTValue.AppearanceCell.BackColor = Color.White;
                    break;
            }


        }

        void _SecurityTimerStope_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SetStopeSecurity();
        }

        public void reloadWorkplaces()
        {
            if (!PlanningClass.isRevised)
            {
                tblPrePlanData = LoadPrePlanningData(theProdMonth, theSectionIDMO, theActivity);
            }
            else
            {
                tblPrePlanData = LoadPrePlanningDataRevised(theProdMonth, theSectionIDMO, theActivity);
            }
            tblPrePlanData.Columns.Add(new DataColumn("NightCrewRequired", typeof(int)));
            MainGrid.DataSource = tblPrePlanData;
        }

        private DataView clone = null;
        int dropTargetRowHandle = -1;
        int DropTargetRowHandle
        {
            get { return dropTargetRowHandle; }
            set
            {
                dropTargetRowHandle = value;
                gcCycle.Invalidate();
            }
        }
        private void viewPlanningStoping_ShownEditor(object sender, EventArgs e)
        {

        }

        private void viewPlanningStoping_HiddenEditor(object sender, EventArgs e)
        {
            if (clone != null)
            {

                clone.Dispose();
                clone = null;
            }
        }


        private void AddWorkplace(string WPDesc)
        {
            MWDataManager.clsDataAccess _NewWorkPlace = new MWDataManager.clsDataAccess();
            _NewWorkPlace.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _NewWorkPlace.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _NewWorkPlace.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _NewWorkPlace.SqlStatement = "spAddPrePlanningWorkPlace";
            _NewWorkPlace.SqlStatement = "sp_PrePlanning_AddWorkplace";


            SqlParameter[] _paramCollection = 
                                {
                                    _NewWorkPlace.CreateParameter("@Prodmonth", SqlDbType.Int, 0,Convert.ToInt32(theProdMonth)),
                                    _NewWorkPlace.CreateParameter("@Sectionid_2", SqlDbType.VarChar,20 ,theSectionIDMO),
                                    _NewWorkPlace.CreateParameter("@WPDESCRIPTION", SqlDbType.VarChar,100,WPDesc), 
                                    _NewWorkPlace.CreateParameter("@ActivityCode", SqlDbType.Int ,0 ,theActivity ),
                                };

            _NewWorkPlace.ParamCollection = _paramCollection;
            clsDataResult exr = _NewWorkPlace.ExecuteInstruction();

            if (exr.success == false)
            {
                MessageBox.Show(exr.Message);
            }

            if (_NewWorkPlace.ResultsDataTable.Rows.Count != 0)
            {

                CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
                BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
                BMEBL.SetsystemDBTag = this.theSystemDBTag;
                BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

                //  DataTable theDates = new DataTable();
                if (BMEBL.get_BeginEndDates(theSectionIDMO, theProdMonth) == true)
                {
                    theDates = BMEBL.ResultsDataTable;
                }




                if (_NewWorkPlace.ResultsDataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in _NewWorkPlace.ResultsDataTable.Rows)
                    {
                        tblPrePlanData.ImportRow(r);
                    }
                }
            }


        }

        public void addWorkPlace()
        {
            frmWorkplaceSelect frmWorkplaceSelect = new frmWorkplaceSelect { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
            ArrayList CurretnWorkplace = new ArrayList();
            CurretnWorkplace = frmWorkplaceSelect.GetWorkPlaceList(theActivity.ToString(), theProdMonth, CurretnWorkplace);

            if (CurretnWorkplace.Count > 0)
            {
                for (int i = 0; i < CurretnWorkplace.Count; i++)
                {
                    AddWorkplace(CurretnWorkplace[i].ToString());
                }
            }

        }
        private int getTotalSQM(DataTable theData)
        {
            if (theData.Rows.Count > 0)
            {
                int totalValue = 0;
                int sqm = 0;
                int wsqm = 0;
                foreach (DataRow r in theData.Rows)
                {
                    if (r["callValue"] is DBNull) sqm = 0;
                    else 
                        sqm = Convert.ToInt16(r["callValue"]);
                    if (r["WasteSQM"] is DBNull) wsqm = 0;
                    else
                        wsqm = Convert.ToInt16(r["WasteSQM"]);
                    totalValue = totalValue + (sqm );
                }
                return totalValue;
            }
            else { return 0; }
        }
		
		private int getTotalU308SQM(DataTable theData)
        {
            if (theData.Rows.Count > 0)
            {
                int totalValue = 0;
                int totalValue1 = 0;
                int sqmCMKGT = 0;
                int sqmU308 = 0;
                foreach (DataRow r in theData.Rows)
                {
                    if (r["CMKGT"] is DBNull || r["CMKGT"].ToString() == "0")
                    {
                        sqmCMKGT = 0;
                        sqmU308 = 0;
                    }

                    else
                    {
                        sqmCMKGT = Convert .ToInt16 ( r["CMKGT"]);
                        if (sqmCMKGT > 0)
                        {
                            sqmU308 = Convert.ToInt16(r["callValue"]);
                        }
                        else
                        {
                            sqmU308 = 0;
                        }

                    }
                    totalValue = totalValue + (sqmCMKGT);
                    totalValue1 = totalValue1 + (sqmU308);
                }
                 
                if (totalValue1 == 0)
                {
                    return 0;
                }
                else
                return totalValue1 ;
            }
            else { return 0; }
        }

        private double getTotalDevMeters(DataTable theData)
        {
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double M = 0, MW = 0, MCAP = 0;
                foreach (DataRow r in theData.Rows)
                {
                    if (r["ReefAdv"] is DBNull)
                    {
                        M = 0;
                    }
                    else M = Convert.ToDouble(r["ReefAdv"]);

                    if (r["WasteAdv"] is DBNull)
                    {
                        MW = 0;
                    }
                    else MW = Convert.ToDouble(r["WasteAdv"]);

                    if (r["DevCap"] is DBNull)
                    {
                        r["DevCap"] = 0;
                    }
                    else MCAP = Convert.ToDouble(r["DevCap"]);
                    totalValue = totalValue + M + MW - MCAP;
                }
                return totalValue;
            }
            else { return 0.0; }
        }

        private double getTotalReef(DataTable theData)
        {
             if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double M = 0, MW = 0, MCAP = 0;
                foreach (DataRow r in theData.Rows)
                {
                    if (r["ReefAdv"] is DBNull)
                    {
                        M = 0;
                    }
                    else M = Convert.ToDouble(r["ReefAdv"]);

                    totalValue = totalValue + M;
                }
                return totalValue;
            }
             else { return 0.0; }
        }

        private double getSubTotalReef(DataTable theData, int rowhandle)
        {
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double M = 0, MW = 0, MCAP = 0;
                DataRow[] r = theData.Select();
                for (int i = 0; i <= rowhandle; i++)
                // foreach (DataRow r in theData.Rows)
                {
                    if (r[i]["ReefAdv"] is DBNull)
                    {
                        MW = 0;
                    }
                    else MW = Convert.ToDouble(r[i]["ReefAdv"]);

                    totalValue = totalValue + MW;
                }
                return totalValue;
            }
            else { return 0.0; }
        }

        private double getTotalWaste(DataTable theData)
        {
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double M = 0, MW = 0, MCAP = 0;
                foreach (DataRow r in theData.Rows)
                {
                    if (r["WasteAdv"] is DBNull)
                    {
                        MW = 0;
                    }
                    else MW = Convert.ToDouble(r["WasteAdv"]);

                    totalValue = totalValue + MW;
                }
                return totalValue;
            }
            else { return 0.0; }
        }

        private double getSubTotalwaste(DataTable theData, int rowhandle)
        {
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double M = 0, MW = 0, MCAP = 0;
                DataRow[] r = theData.Select();
                for (int i = 0; i <= rowhandle; i++)
                // foreach (DataRow r in theData.Rows)
                {
                    if (r[i]["WasteAdv"] is DBNull)
                    {
                        MW = 0;
                    }
                    else MW = Convert.ToDouble(r[i]["WasteAdv"]);

                    totalValue = totalValue + MW;
                }
                return totalValue;
            }
            else { return 0.0; }
        }

        private double getSubTotalDevCap(DataTable theData, int rowhandle)
        {
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double M = 0, MW = 0, MCAP = 0;
                DataRow[] r = theData.Select();
                for (int i = 0; i <= rowhandle ; i++)
                // foreach (DataRow r in theData.Rows)
                {
                    if (r[i]["ReefAdv"] is DBNull)
                    {
                        M = 0;
                    }
                    else M = Convert.ToDouble(r[i]["ReefAdv"]);

                    if (r[i]["WasteAdv"] is DBNull)
                    {
                        MW = 0;
                    }
                    else MW = Convert.ToDouble(r[i]["WasteAdv"]);

                    if (r[i]["DevCap"] is DBNull)
                    {
                        r[i]["DevCap"] = 0;
                        MCAP = Convert.ToDouble(r[i]["DevCap"]);
                    }
                    totalValue = totalValue + M + MW - MCAP;
                }
                return totalValue;
            }
            else { return 0.0; }
        }

        private double getTotalDevCap(DataTable theData)
        {
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double MCAP = 0;
                //DataRow[] ds = theData.Select();
                foreach(DataRow ds in theData.Rows)
                // foreach (DataRow r in theData.Rows)
                {
                    if (ds["DevCap"] is DBNull)
                    {

                        //MCAP = Convert.ToDouble(r["DEVCAP"]);
                        totalValue = totalValue + 0;
                    }
                    else
                    {
                        MCAP = Convert.ToDouble(ds["DevCap"]);
                        totalValue = totalValue + MCAP;
                    }
                }
                return totalValue;
            }
            else { return 0.0; }
        }

        private double getTotalReefDevCap(DataTable theData)
        {
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double MCAP = 0;
                //DataRow[] ds = theData.Select();
                foreach (DataRow ds in theData.Rows)
                // foreach (DataRow r in theData.Rows)
                {
                    if (ds["DevCap"] is DBNull)
                    {

                        //MCAP = Convert.ToDouble(r["DEVCAP"]);
                        totalValue = totalValue + 0;
                    }
                    else
                    {
                        if (ds["ROCKTYPE"].ToString() == "ON")
                        {
                            MCAP = Convert.ToDouble(ds["DevCap"]);
                            totalValue = totalValue + MCAP;
                        }
                    }
                }
                return totalValue;
            }
            else { return 0.0; }
        }

        private double getTotalWasteDevCap(DataTable theData)
        {
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double MCAP = 0;
                //DataRow[] ds = theData.Select();
                foreach (DataRow ds in theData.Rows)
                // foreach (DataRow r in theData.Rows)
                {
                    if (ds["DevCap"] is DBNull)
                    {

                        //MCAP = Convert.ToDouble(r["DEVCAP"]);
                        totalValue = totalValue + 0;
                    }
                    else
                    {
                        if (ds["ROCKTYPE"].ToString() == "OFF")
                        {
                            MCAP = Convert.ToDouble(ds["DevCap"]);
                            totalValue = totalValue + MCAP;
                        }
                    }
                }
                return totalValue;
            }
            else { return 0.0; }
        }

        private double getTotalGoldBroken(DataTable theData)
        {
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double gb = 0;
                foreach (DataRow r in theData.Rows)
                {
                    if (r["GoldBroken"] is DBNull)
                        gb = 0;
                    else gb = Convert.ToDouble(r["GoldBroken"]);

                    totalValue = totalValue + gb;
                }
                return totalValue;
            }
            else { return 0.0; }
        }

        private double getTotalGoldFaceBroken(DataTable theData)
        {
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double gb = 0;
                foreach (DataRow r in theData.Rows)
                {
                    if (r["FaceBrokenKG"] is DBNull)
                        gb = 0;
                    else gb = Convert.ToDouble(r["FaceBrokenKG"]);

                    totalValue = totalValue + gb;
                }
                return totalValue;
            }
            else { return 0.0; }
        }
		private double getTotalUraniumBroken(DataTable theData)
        {
           
              
           // }
            if (theData.Rows.Count > 0)
            {
                double totalValue = 0;
                double gb = 0;
                foreach (DataRow r in theData.Rows)
                {
                    if (r["UraniumBroken"] is DBNull)
                        gb = 0;
                    else gb = Convert.ToDouble(r["UraniumBroken"]);

                    totalValue = totalValue + gb;
                }
                return totalValue;
            }
               
            else { return 0.0; }
        }

        private void viewPlanningStoping_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "MonthlyReefSQM")
            {
                int WasteSQM = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyWatseSQM"]));

                int newCall = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyReefSQM"])) + WasteSQM;

                viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyTotalSQM"], newCall);
            }

            if (e.Column.FieldName == "MonthlyWatseSQM")
            {
                int ReefSQM = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyReefSQM"]));

                int newCall = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyWatseSQM"])) + ReefSQM;

                viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyTotalSQM"], newCall);
            }

            if (e.Column.FieldName == "MonthlyTotalSQM")
            {
                if (viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"]).ToString() != TotlSqmLbl.Text)
                {
                    viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"], e.Value.ToString());
                }

                //viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"], e.Value.ToString());
            }

            if (e.Column.FieldName == "callValue")
            {
                decimal TotMonthcall = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyTotalSQM"]));
                decimal ReefMonthcall = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyReefSQM"]));

                decimal ratio = 0;

                if (ReefMonthcall != 0)
                {
                    ratio = TotMonthcall / ReefMonthcall;
                }

                int TotActCall = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"]));
                int ReefCall = 0;
                int WasteCall = 0;

                if (ratio != 0)
                {
                    ReefCall = Convert.ToInt32(Convert.ToDecimal(TotActCall) / ratio);
                }

                if (TotMonthcall != 0)
                {
                    WasteCall = TotActCall - ReefCall;
                }
                else
                {
                    ReefCall = TotActCall;
                }
                


                viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["ReefSQM"], ReefCall);
                viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["WasteSQM"], WasteCall);

                //viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["Metresadvance"], e.Value.ToString());
               
            }


            MWDataManager.clsDataAccess _CalcData = new MWDataManager.clsDataAccess() { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement, queryReturnType = MWDataManager.ReturnType.DataTable };

            BandedGridView view;
            view = sender as BandedGridView;
            object row = view.GetRow(view.FocusedRowHandle); // get the current row 


            (row as DataRowView)["hasChanged"] = 1;

            if (view.FocusedColumn.FieldName == "SectionID")
            {
                // updates start and en date based on selected miner. Will update if no date or date is different from current
                string selectedSectioID = (row as DataRowView)["SectionID"].ToString();
                DataRow[] result = PlanningClass.tblSectionStartEndDates.Select(String.Format("SectionID = '{0}'", selectedSectioID));
                if (result.Length == 1)
                {
                    if ((row as DataRowView)["StartDate"] == null || (row as DataRowView)["StartDate"] != result[0]["StartDate"].ToString())
                    {
                        (row as DataRowView)["StartDate"] = result[0]["StartDate"].ToString();
                    }
                    if ((row as DataRowView)["EndDate"] == null || (row as DataRowView)["EndDate"] != result[0]["EndDate"].ToString())
                    {
                        (row as DataRowView)["EndDate"] = result[0]["EndDate"].ToString();
                    }
                }

            }

            if (theActivity == 0)
            {

                if (view.FocusedColumn.FieldName == "ReefSQM" || view.FocusedColumn.FieldName == "WasteSQM" || view.FocusedColumn.FieldName == "FL" ||
                    view.FocusedColumn.FieldName == "SW" || view.FocusedColumn.FieldName == "CW" || view.FocusedColumn.FieldName == "CMGT" ||
                    view.FocusedColumn.FieldName == "FaceCMGT" || view.FocusedColumn.FieldName == "CubicsReef" || view.FocusedColumn.FieldName == "CubicsWaste")
                {
                    if ((row as DataRowView)["FaceCMGT"] == null || (row as DataRowView)["FaceCMGT"].ToString() == "")
                    {
                        (row as DataRowView)["FaceCMGT"] = 0;
                    }

                    if ((row as DataRowView)["CW"] == null || (row as DataRowView)["CW"].ToString() == "")
                    {
                        (row as DataRowView)["CW"] = 0;
                    }

                    if ((row as DataRowView)["CubicsReef"] == null || (row as DataRowView)["CubicsReef"].ToString() == "")
                    {
                        (row as DataRowView)["CubicsReef"] = 0;
                    }

                    if ((row as DataRowView)["CubicsWaste"] == null || (row as DataRowView)["CubicsWaste"].ToString() == "")
                    {
                        (row as DataRowView)["CubicsWaste"] = 0;
                    }

                    if ((row as DataRowView)["DynamicCubicGT"] == null || (row as DataRowView)["DynamicCubicGT"].ToString() == "")
                    {
                        (row as DataRowView)["DynamicCubicGT"] = 0;
                    }

                    string cubicgrams = (row as DataRowView)["DynamicCubicGT"].ToString();

                   
                   _CalcData.SqlStatement = "DECLARE @StopeWidth float,@CubicMeters float,@FaceLength float,@OnReefSQM float, @OffReefSQM float, " +
                                             "@CMGT float,@CMKGT float,@Workplaceid varchar(50),@FaceCMGT float,@FaceAdvance float,@GoldBrokenSQM float,@UraniumBrokenSQM float, " +
                                             "@GoldBrokenCUB float,@UraniumBrokenCUB float, @FaceBrokenKG float, @FaceTonsSQM float, @FaceTonsCube float, @FaceValue float, " +
                                             "@TrammedTons float, @TrammedValue float,@ChannelW int,@cubicgrams float  " +
                                             " SET @StopeWidth = " + (row as DataRowView)["SW"].ToString() +
                                             " SET @CubicMeters = " + (row as DataRowView)["CubicMetres"] +
                                             " SET @FaceLength = " + (row as DataRowView)["FL"] +
                                             " SET @OnReefSQM = " + (row as DataRowView)["ReefSQM"] +
                                             " SET @OffReefSQM = " + (row as DataRowView)["WasteSQM"] +
                                             " SET @CMGT = " + (row as DataRowView)["CMGT"] +
                                              " SET @CMKGT = " + (row as DataRowView)["CMKGT"] +
                                             " SET @Workplaceid = '" + (row as DataRowView)["Workplaceid"] + "' " +
                                             " SET @FaceCMGT  = " + (row as DataRowView)["FaceCMGT"] +
                                             " SET @ChannelW  = " + (row as DataRowView)["CW"] +

                                             "SET @cubicgrams=" + (row as DataRowView)["DynamicCubicGT"] + "" +


                                             " SET @FaceAdvance = dbo.CalcFaceAdvance(@StopeWidth,@CubicMeters,@FaceLength,@OnReefSQM,@OffReefSQM) " +
                                             " set @GoldBrokenSQM = dbo.CalcGoldBrokenSQM(@OnReefSQM,@CMGT,@Workplaceid) " +
                                               " set @GoldBrokenCUB = dbo.CalcGoldBrokenCUB(@CubicMeters,@cubicgrams,@StopeWidth,@Workplaceid) " +
                                             "  set @UraniumBrokenSQM = dbo.CalcGoldBrokenSQM(@OnReefSQM,@CMKGT,@Workplaceid)  " +
                                               "set @UraniumBrokenCUB=dbo.CalcUraniumBrokenCUB(@CubicMeters,@cubicgrams,@StopeWidth,@Workplaceid) " +
                                             " set @FaceBrokenKG = dbo.CalcFaceBrokenKG(@OnReefSQM,@FaceCMGT,@Workplaceid)  " +
                                             " set @FaceTonsSQM = dbo.CalcFaceTonsSQM(@OnReefSQM + @OffReefSQM,@StopeWidth,@Workplaceid)" +
                                             " SET @FaceTonsCube = dbo.CalcFaceTonsCUBE(@CubicMeters,@Workplaceid) " +
                                             " SET @FaceValue = dbo.CalcFaceValue(@GoldBrokenSQM + @GoldBrokenCUB,@FaceTonsSQM + @FaceTonsCube) " +
                                             " SET @TrammedTons = dbo.CalcTrammedTons(@FaceTonsSQM + @FaceTonsCube) " +
                                             " SET @TrammedValue = dbo.CalcTrammedValue(@TrammedTons,@GoldBrokenSQM + @GoldBrokenCUB) " +
                                             " SELECT   @FaceAdvance FaceAdvance,@GoldBrokenSQM  GoldBrokenSQM,@UraniumBrokenSQM UraniumBrokenSQM, @GoldBrokenCUB GoldBrokenCUB,@UraniumBrokenCUB UraniumBrokenCUB,@FaceBrokenKG FaceBrokenKG, " +
                                             "         @FaceTonsSQM FaceTonsSQM,@FaceTonsCube FaceTonsCubes,@FaceValue FaceValue,@TrammedTons TrammedTons, @TrammedValue TrammedValue,dbo.CalcIdealSW(@ChannelW) IdealSW ";
                    _CalcData.ExecuteInstruction();



                    foreach (DataRow r in _CalcData.ResultsDataTable.Rows)
                    {
                        (row as DataRowView)["FaceAdvance"] = r["FaceAdvance"];
                        (row as DataRowView)["GoldBroken"] = Convert.ToDecimal(r["GoldBrokenSQM"]) + (Convert.ToDecimal(r["GoldBrokenCUB"])/1000);
                        (row as DataRowView)["UraniumBroken"] = Convert.ToDecimal(r["UraniumBrokenSQM"]) + (Convert.ToDecimal(r["UraniumBrokenCUB"]) / 100);
                        (row as DataRowView)["FaceBrokenKG"] = r["FaceBrokenKG"];
                        (row as DataRowView)["FaceTons"] = Convert.ToDecimal(r["FaceTonsSQM"]) + Convert.ToDecimal(r["FaceTonsCubes"]);
                        (row as DataRowView)["FaceValue"] = r["FaceValue"];
                        (row as DataRowView)["TrammedTons"] = r["TrammedTons"];
                        (row as DataRowView)["TrammedValue"] = r["TrammedValue"];
                        (row as DataRowView)["IdealSW"] = r["IdealSW"];
                        (row as DataRowView)["CubicMetres"] = Convert.ToDecimal((row as DataRowView)["CubicsReef"]) + Convert.ToDecimal((row as DataRowView)["CubicsWaste"]);
                        

                    }

                    string theSQM = String.Format("{0:0}", (row as DataRowView)["ReefSQM"]);
                    string theWasteSQM = String.Format("{0:0}", (row as DataRowView)["WasteSQM"]);
                    Int32 theTotal = Convert.ToInt32(theSQM) + Convert.ToInt32(theWasteSQM);

                    (row as DataRowView)["callValue"] = theTotal;
                    //editTotalPlanSQM.Text = Convert.ToString(getTotalSQM(tblPrePlanData));
                    viewPlanningStoping.UpdateTotalSummary();
                    calVariance();


                }

                viewPlanningStoping.RefreshData();
            }
        }

        public void calVariance()
        {
            viewPlanningStoping.InvalidateFooter();
            viewPlanningStoping.RefreshData();
        }

        public bool canClosePlanning()
        {
            bool theResult = false;

            DataRow[] drarray;

            string filterExp = "hasChanged ='true'";
            if (tblPrePlanData.Rows.Count == 0  )
            {
                theResult = true;
            }
            else
            {
                drarray = tblPrePlanData.Select(filterExp);
                if (drarray.Length > 0)
                {
                    DialogResult result;

                    result = MessageBox.Show("Are you sure you want to cancel the current Pre-Planning? All changes will be lost.", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        theResult = false;
                    }
                    else { theResult = true; }

                }
                else { theResult = true; }
            }

            return theResult;
        }

        public string getSelectedWorkplace()
        {
            if (viewPlanningStoping.State != BandedGridState.Editing || viewPlanningDevelopment.State != BandedGridState.Editing)
            {
                int currentRow = 0;
                if (theActivity == 0)
                {
                    currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
                    
                }
                else { currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle); }
                if (currentRow == -2147483648)
                {
                    return "-1";
                }
               else
                    return tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString();
            }
            else
            {
                MessageBox.Show("Please select a workplace.");
                return "NONE";
            }
        }

        public bool isWorplaceStopped(string workplaceID)
        {
            bool theResult = false;

            DataRow[] foundRows;
            foundRows = tblPrePlanData.Select("Workplaceid = '" + workplaceID + "'");
            // foreach (DataRow r in foundRows.Rows )
            // {
            if (foundRows[0]["IsStopped"].ToString() == "Y")
                theResult = true;
            else theResult = false;
            // }

            return theResult;
        }

        public string getSelectedSectionID()
        {
            if (viewPlanningStoping.State != BandedGridState.Editing || viewPlanningDevelopment.State != BandedGridState.Editing)
            {
                int currentRow = 0;
                if (theActivity == 0)
                {
                    currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
                }
                else { currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle); }
                if (currentRow == -2147483648)
                {
                    return "-1";
                }
                else
                return tblPrePlanData.Rows[currentRow]["SectionID"].ToString();
            }
            else
            {
                MessageBox.Show("Please select a workplace.");
                return "NONE";
            }
        }

        public void delteWorkPlace()
        {
            if (viewPlanningStoping.State != BandedGridState.Editing || viewPlanningDevelopment.State != BandedGridState.Editing)
            {
                DialogResult result;
                MWDataManager.clsDataAccess _DeleteData = new MWDataManager.clsDataAccess() { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement };
                int currentRow = 0;
                if (theActivity == 0)
                {
                    currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
                }
                else if (theActivity == 2)
                {
                    currentRow = viewPlanningSundry.GetDataSourceRowIndex(viewPlanningSundry.FocusedRowHandle);
                }
                else if (theActivity == 8)
                {
                    currentRow = viewPlanningSweepVamp.GetDataSourceRowIndex(viewPlanningSweepVamp.FocusedRowHandle);
                }
                else { currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle); }

                if (tblPrePlanData.Rows.Count > 0)
                {
                    switch (theActivity)
                    {
                        case 0:
                        case 1:
                            if (tblPrePlanData.Rows[currentRow]["Locked"].ToString() == "True")
                            {
                                MessageBox.Show("This workplace has been approved for Planning. Any changes needs to be done in the Planning screen as Re-Planning.", "Delete Workplace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                result = result = MessageBox.Show("Would you like to delete workplace :" + tblPrePlanData.Rows[currentRow]["WorkplaceDesc"].ToString(), "Delete Workplace", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    _DeleteData.SqlStatement = "DELETE FROM dbo.PLANMONTH  \r\n" +
                                                               "WHERE Prodmonth = " + theProdMonth + " AND  \r\n" +
                                                               "      Sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' AND   \r\n" +
                                                               "      WorkplaceID = '" + tblPrePlanData.Rows[currentRow]["WorkplaceID"].ToString() + "' AND  \r\n" +
                                                               "      Activity = " + tblPrePlanData.Rows[currentRow]["Activity"].ToString() + " AND   \r\n" +
                                                               "      IsCubics = '" + tblPrePlanData.Rows[currentRow]["IsCubics"].ToString() + "'   \r\n\r\n";

                                    _DeleteData.SqlStatement = _DeleteData.SqlStatement + "DELETE FROM dbo.PLANNING  \r\n" +
                                                               "WHERE Prodmonth = " + theProdMonth + " AND   \r\n" +
                                                               "      Sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' AND   \r\n" +
                                                               "      WorkplaceID = '" + tblPrePlanData.Rows[currentRow]["WorkplaceID"].ToString() + "' AND   \r\n" +
                                                               "      Activity = " + tblPrePlanData.Rows[currentRow]["Activity"].ToString() + " AND   \r\n" +
                                                               "      IsCubics = '" + tblPrePlanData.Rows[currentRow]["IsCubics"].ToString() + "'";


                                    _DeleteData.ExecuteInstruction();
                                    //_DeleteData.SqlStatement = "DELETE FROM WORKPLACE_PREPLANNING WHERE DESCRIPTION = '" + tblPrePlanData.Rows[currentRow]["WorkplaceDesc"].ToString() + "'";
                                    //_DeleteData.ExecuteInstruction();
                                    tblPrePlanData.Rows[currentRow].Delete();
                                    tblPrePlanData.AcceptChanges();
                                    calVariance();

                                }
                            }
                            break;
                        case 2:
                            result = result = MessageBox.Show("Would you like to delete workplace :" + tblPrePlanData.Rows[currentRow]["WorkplaceDesc"].ToString(), "Delete Workplace", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                _DeleteData.SqlStatement = "DELETE FROM dbo.PLANMONTH_SUNDRYMINING " +
                                                              "WHERE Prodmonth = " + theProdMonth + " AND " +
                                                              "      Sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' AND " +
                                                              "      WorkplaceID = '" + tblPrePlanData.Rows[currentRow]["WorkplaceID"].ToString() + "' AND " +
                                                              "      SMID = '" + tblPrePlanData.Rows[currentRow]["SMID"].ToString() + "'";
                                _DeleteData.ExecuteInstruction();
                                tblPrePlanData.Rows[currentRow].Delete();
                                tblPrePlanData.AcceptChanges();
                                calVariance();

                            }

                            break;
                        case 8:
                            result = result = MessageBox.Show("Would you like to delete workplace :" + tblPrePlanData.Rows[currentRow]["WorkplaceDesc"].ToString(), "Delete Workplace", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                _DeleteData.SqlStatement = "DELETE FROM dbo.PLANMONTH_OLDGOLD " +
                                                              "WHERE Prodmonth = " + theProdMonth + " AND " +
                                                              "      Sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' AND " +
                                                              "      WorkplaceID = '" + tblPrePlanData.Rows[currentRow]["WorkplaceID"].ToString() + "' AND " +
                                                              "      OGID = '" + tblPrePlanData.Rows[currentRow]["TheID"].ToString() + "'";
                                _DeleteData.ExecuteInstruction();
                                tblPrePlanData.Rows[currentRow].Delete();
                                tblPrePlanData.AcceptChanges();
                                calVariance();

                            }
                            break;
                    }

                }
                else { MessageBox.Show("Please select a workplace."); }
            }

        }

        public bool  ReplaceWorkplace()
        {
            if (replacewp == true)
            {
                MessageBox.Show("Please save the data before replacing the workplace", "", MessageBoxButtons.OK);
                return true;
            }
            else
            {
                string activity = "";
                switch (theActivity.ToString())
                {
                    case "0":
                        activity = "S";
                        break;
                    case "1":
                        activity = "D";
                        break;

                    case "2":
                        activity = "2";
                        break;

                }
                CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
                BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
                BMEBL.SetsystemDBTag = this.theSystemDBTag;
                BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

                frmRemoveWorkplace frmRemoveWorkplace = new frmRemoveWorkplace { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                ArrayList CurretnWorkplace = new ArrayList();
                string workplaceid = "";
                string currentworkplaceid = "";
                DataTable dt = new DataTable();

                CurretnWorkplace = frmRemoveWorkplace.GetWorkPlaceList(theActivity.ToString(), theProdMonth, CurretnWorkplace);

                if (CurretnWorkplace.Count > 0)
                {
                    for (int i = 0; i < CurretnWorkplace.Count; i++)
                    {
                        //AddWorkplace(CurretnWorkplace[i].ToString());
                        //}
                        // }
                        MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
                        _SaveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _SaveData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _SaveData.queryReturnType = MWDataManager.ReturnType.DataTable;

                        _SaveData.SqlStatement = "SELECT * FROM WORKPLACE WHERE Description='" + CurretnWorkplace[i].ToString() + "'";
                        _SaveData.ExecuteInstruction();
                        dt = _SaveData.ResultsDataTable;
                        foreach (DataRow ds in dt.Rows)
                        {
                            workplaceid = ds["WorkplaceID"].ToString();
                            currentworkplaceid = CurretnWorkplace[i].ToString();
                        }
                    }
                }



                if (viewPlanningStoping.State != BandedGridState.Editing || viewPlanningDevelopment.State != BandedGridState.Editing)
                {
                    DialogResult result;
                    MWDataManager.clsDataAccess _DeleteData = new MWDataManager.clsDataAccess() { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement };
                    int currentRow = 0;
                    if (theActivity == 0)
                    {
                        currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
                    }
                    else if (theActivity == 2)
                    {
                        currentRow = viewPlanningSundry.GetDataSourceRowIndex(viewPlanningSundry.FocusedRowHandle);
                    }
                    else { currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle); }

                    if (tblPrePlanData.Rows.Count > 0)
                    {
                        if (tblPrePlanData.Rows[currentRow]["Locked"].ToString() == "True")
                        {
                            MessageBox.Show("This workplace has been approved for Planning. Any changes needs to be done in the Planning screen as Re-Planning.", "Delete Workplace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            result = result = MessageBox.Show("Would you like to Replace the workplace :" + tblPrePlanData.Rows[currentRow]["WorkplaceDesc"].ToString(), "Delete Workplace", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {

                                if (theActivity == 0 || theActivity == 1)
                                {
                                    _DeleteData.SqlStatement = "UPDATE  dbo.PLANMONTH SET Workplaceid='" + workplaceid + "', WorkplaceDesc='" + currentworkplaceid + "' " +
                                                               "WHERE Prodmonth = " + theProdMonth + " AND " +
                                                               "      Sectionid = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' AND " +
                                                               "      Sectionid_2 = '" + tblPrePlanData.Rows[currentRow]["Sectionid_2"].ToString() + "' AND " +
                                                               "      WorkplaceDesc = '" + tblPrePlanData.Rows[currentRow]["WorkplaceDesc"].ToString() + "' AND " +
                                                               "      Activity = " + tblPrePlanData.Rows[currentRow]["Activity"].ToString() + " AND " +
                                                               "      IsCubics = '" + tblPrePlanData.Rows[currentRow]["IsCubics"].ToString() + "'";
                                    _DeleteData.ExecuteInstruction();

                                    tblPrePlanData.Clear();
                                    tblPrePlanData = LoadPrePlanningData(Convert.ToInt32(theProdMonth).ToString(), theSectionIDMO, theActivity);
                                    tblPrePlanData.Columns.Add(new DataColumn("NightCrewRequired", typeof(int)));
                                    calVariance();

                                    MainGrid.DataSource = tblPrePlanData;
                                }
                                else if (theActivity == 2)
                                {
                                    viewPlanningSundry.SetRowCellValue(viewPlanningSundry.FocusedRowHandle, viewPlanningSundry.Columns["Workplaceid"].ToString(), workplaceid);
                                    viewPlanningSundry.SetRowCellValue(viewPlanningSundry.FocusedRowHandle, viewPlanningSundry.Columns["WorkplaceDesc"].ToString(), currentworkplaceid);

                                }
                            }
                        }
                    }
                    else { MessageBox.Show("Please select a workplace."); }
                }
                return false;
            }
        }

        private string testNull(DataRow sender, string theCol)
        {
            string theValue;
            if (sender[theCol] == null || sender[theCol].ToString() == "")
            {
                theValue = "0";
            }
            else { theValue = sender[theCol].ToString(); }

            return theValue;
        }

        private bool testStopingData()
        {
            bool passedTest = true;

            foreach (DataRow r in tblPrePlanData.Rows)
            {
            }


            return passedTest;
        }

        public void savePrePlanning()
        {
            // bool valid;
            MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
            _SaveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _SaveData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

            string text = "";
            string separator = "";
            int newTargetID;

            switch (theActivity)
            {
                case 2:
                    //foreach (DataRow r in tblPrePlanData.Rows)
                    //{
                        //foreach (DataRow r in tblPrePlanData.Rows)
                        //{
                        _SaveData.SqlStatement = "Delete from a from planmonth_SundryMining a inner join Section_complete b on \n" +
                                                 "a.prodmonth = b.prodmonth and \n" +
                                                 "a.sectionid = b.Sectionid \n" +
                                                 "where a.Prodmonth =  " + theProdMonth.ToString() + " \n" +
                                                 "and Plancode = 'MP' \n" +
                                                 "and SectionID_2 = '" + theSectionIDMO + "'";
                        _SaveData.ExecuteInstruction();
                        //}
                    //}
                    break;
                case 8:
                    //foreach (DataRow r in tblPrePlanData.Rows)
                    //{
                        _SaveData.SqlStatement = "Delete from a from planmonth_oldgold a inner join Section_complete b on \n" +
                                                 "a.prodmonth = b.prodmonth and \n"+
                                                 "a.sectionid = b.Sectionid \n"+
                                                 "where a.Prodmonth =  " + theProdMonth.ToString() + " \n" +
                                                 "and Plancode = 'MP' \n"+
                                                 "and SectionID_2 = '" + theSectionIDMO + "'";
                        _SaveData.ExecuteInstruction();
                    //}
                    break;
            }

            _SaveData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //  _SaveData.SqlStatement = "spPrePlan_ManagePrePlanning";
            _SaveData.SqlStatement = "sp_Save_Planning";
            valid = true;
            foreach (DataRow r in tblPrePlanData.Rows)
            {
                switch (theActivity)
                {

                    case 0:

                        if (Convert.ToString(r["NightCrewRequired"]) == "1" && (r["OrgUnitNight"].ToString() == "" || r["OrgUnitNight"] != DBNull.Value))
                        {
                            text += separator + r["WorkplaceDesc"];
                            separator = ",";
                            valid = false;
                            MessageBox.Show("Please enter a valid night crew for " + text, "Cannot save Planning", MessageBoxButtons.OK);
                        }
                        if (Convert.ToString(r["SectionID"]) == "-1" || Convert.ToString(r["SectionID"]) == "")
                        {
                            text += separator + r["WorkplaceDesc"];
                            separator = ",";
                            valid = false;
                            MessageBox.Show("Please enter a valid Miner for " + text, "Cannot save Planning", MessageBoxButtons.OK);
                        }

                        if (valid == true)
                        {
                            if (r["TargetID"].ToString() == "")
                                newTargetID = -1;
                            else newTargetID = Convert.ToInt32(r["TargetID"].ToString());
                            viewPlanningStoping.UpdateCurrentRow();
                            if (r.RowState != DataRowState.Deleted && Convert.ToBoolean(r["hasChanged"].ToString()) == true)
                            {
                                decimal SQM = Convert.ToDecimal(r["ReefSQM"].ToString());
                                if (SQM == null) SQM = 0;

                                string StartDate = String.Format("{0:MM/dd/yyyy}", r["StartDate"]);
                                string EndDate = String.Format("{0:MM/dd/yyyy}", r["EndDate"]);
                                SqlParameter[] _paramCollection = 
                                {
                                    _SaveData.CreateParameter("@Prodmonth", SqlDbType.Int, 0,Convert.ToInt32(theProdMonth)),
                                    _SaveData.CreateParameter("@Sectionid", SqlDbType.VarChar, 20,r["Sectionid"].ToString()),
                                    _SaveData.CreateParameter("@Sectionid_2", SqlDbType.VarChar, 20,r["Sectionid_2"].ToString()),
                                    _SaveData.CreateParameter("@Workplaceid", SqlDbType.VarChar, 20,r["Workplaceid"].ToString()),
                                    _SaveData.CreateParameter("@Activity", SqlDbType.Int, 0,theActivity),
                                    _SaveData.CreateParameter("@IsCubics", SqlDbType.VarChar, 5,r["IsCubics"].ToString()),
                                    _SaveData.CreateParameter("@SQM", SqlDbType.Real, 0,SQM),
                                    _SaveData.CreateParameter("@WasteSQM", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"WasteSQM"))),
                                    _SaveData.CreateParameter("@FL", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"FL"))),
                                    _SaveData.CreateParameter("@FaceAdvance", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"FaceAdvance"))),
                                    _SaveData.CreateParameter("@IdealSW", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"IdealSW"))),
                                    _SaveData.CreateParameter("@ActualSW", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"SW"))),
                                    _SaveData.CreateParameter("@ChannelW", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"CW"))),
                                    _SaveData.CreateParameter("@CMGT", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"CMGT"))),
                                    //_SaveData.CreateParameter("@GoldBroken", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"GoldBroken"))),
                                    _SaveData.CreateParameter("@cubicgrams", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"CubicGrams"))),
                                    _SaveData.CreateParameter("@FaceCMGT", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"FaceCMGT"))),
                                    _SaveData.CreateParameter("@FaceBrokenKG", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"FaceBrokenKG"))),
                                    _SaveData.CreateParameter("@FaceTons", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"FaceTons"))),
                                    _SaveData.CreateParameter("@FaceValue", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"FaceValue"))),
                                    _SaveData.CreateParameter("@TrammedTons", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"TrammedTons"))),
                                    _SaveData.CreateParameter("@TrammedValue", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"TrammedValue"))),
                                    _SaveData.CreateParameter("@TrammedLevel", SqlDbType.VarChar, 0,Convert.ToString(testNull(r,"TrammedLevel"))),
                                    _SaveData.CreateParameter("@CubicMetres", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"CubicMetres"))),
                                    _SaveData.CreateParameter("@OrgUnitDay", SqlDbType.VarChar, 50,r["OrgUnitDay"].ToString()),
                                    _SaveData.CreateParameter("@OrgUnitAfternoon", SqlDbType.VarChar, 50,r["OrgUnitAfternoon"].ToString()),
                                    _SaveData.CreateParameter("@OrgUnitNight", SqlDbType.VarChar, 50,r["OrgUnitNight"].ToString()),
                                    _SaveData.CreateParameter("@RomingCrew", SqlDbType.VarChar, 50,r["RomingCrew"].ToString()),
                                    _SaveData.CreateParameter("@MetersWaste", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@Meters", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@WorkplaceDesc", SqlDbType.VarChar, 32,r["WorkplaceDesc"].ToString()),
                                    _SaveData.CreateParameter("@TargetID", SqlDbType.Int, 0,newTargetID),
                                    _SaveData.CreateParameter("@Locked", SqlDbType.Bit, 0,r["Locked"]),
                                    _SaveData.CreateParameter("@DEVTONS", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DEVMAIN", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DEVSEC", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DEVCAP", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DrillRig", SqlDbType.VarChar, 20,""),
                                    _SaveData.CreateParameter("@StartDate", SqlDbType.VarChar, 30, StartDate),
                                    _SaveData.CreateParameter("@EndDate", SqlDbType.VarChar, 30,EndDate),
                                    _SaveData.CreateParameter("@GramPerTon", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@SSID", SqlDbType.Real, 0, 0 ),
                                    // Rushhab I Removed the call here it gave me an error
                                    _SaveData.CreateParameter("@Units", SqlDbType.Real, 0, 0),// Convert .ToDecimal  (r["Call"].ToString ())),
                                    _SaveData.CreateParameter("@AddInfo", SqlDbType.VarChar , 20,""),
                                    _SaveData.CreateParameter("@AutoUnPlan", SqlDbType.VarChar , 2,""),
                                    _SaveData.CreateParameter("@CrewStrength", SqlDbType.Int, 0,0),
                                    _SaveData.CreateParameter("@PlanCode", SqlDbType.VarChar , 2,"MP"),
                                    _SaveData.CreateParameter("@OGID", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@Depth", SqlDbType.Int , 0,0),
                                    _SaveData.CreateParameter("@grams", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@gt", SqlDbType.Real , 0,0 ),
                                    _SaveData.CreateParameter("@actdepth", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@grade", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@kg", SqlDbType.Real , 0,Convert.ToDecimal(testNull(r,"Kg"))),
                                    _SaveData.CreateParameter("@CMKGT", SqlDbType.Real , 0,Convert.ToDecimal(testNull(r,"CMKGT"))),
                                    _SaveData.CreateParameter("@EndHeight", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@EndWidth", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@cubicreef", SqlDbType.Real , 0,Convert.ToDecimal(testNull(r,"cubicsreef"))),
                                    _SaveData.CreateParameter("@cubicwaste", SqlDbType.Real , 0,Convert.ToDecimal(testNull(r,"cubicswaste"))),
                                    _SaveData.CreateParameter("@UraniumBrokenKg",SqlDbType.Real , 0,Convert.ToDecimal(testNull(r,"UraniumBrokenKg"))),
                                    _SaveData.CreateParameter("@RockType",SqlDbType.VarChar , 0,r["RockType"].ToString())
                                    //_SaveData.CreateParameter("@EndHeight", SqlDbType.Real , 0,0),
                                    // _SaveData.CreateParameter("@EndWidth", SqlDbType.Real , 0,0),
                                };

                                _SaveData.ParamCollection = _paramCollection;
                                _SaveData.queryReturnType = MWDataManager.ReturnType.longNumber;
                                
                                int x = -1;
                                StringBuilder tempstring = new StringBuilder();
                                foreach (SqlParameter a in _paramCollection)
                                {
                                    x += 1;
                                    tempstring.AppendLine(a.ParameterName.ToString() + " = " + a.SqlValue.ToString()+",");
                                }


                                tempstring.ToString();

                                _SaveData.ExecuteInstruction();
                            }
                            //return true;
                            break;
                        }
                        else
                        {

                            break;
                        }
                    //}
                    //break;

                    case 1:
                        if (Convert.ToString(r["NightCrewRequired"]) == "1" && (r["OrgUnitNight"].ToString() == "" || r["OrgUnitNight"] != DBNull.Value))
                        {
                            text += separator + r["WorkplaceDesc"];
                            separator = ",";
                            valid = false;
                            MessageBox.Show("Please enter a valid night crew for " + text, "Cannot save Planning", MessageBoxButtons.OK);
                        }
                        if (Convert.ToString(r["SectionID"]) == "-1" || Convert.ToString(r["SectionID"]) == "")
                        {
                            text += separator + r["WorkplaceDesc"];
                            separator = ",";
                            valid = false;
                            MessageBox.Show("Please enter a valid Miner for " + text, "Cannot save Planning", MessageBoxButtons.OK);
                        }

                        if (Convert.ToString(r["ENDWIDTH"]) == "0" || Convert.ToString(r["ENDWIDTH"]) == "")
                        {
                            text += separator + r["WorkplaceDesc"];
                            separator = ",";
                            valid = false;
                            MessageBox.Show("Please enter a valid End Width for " + text, "Cannot save Planning", MessageBoxButtons.OK);
                        }

                        if (Convert.ToString(r["ENDHEIGHT"]) == "0" || Convert.ToString(r["ENDHEIGHT"]) == "")
                        {
                            text += separator + r["WorkplaceDesc"];
                            separator = ",";
                            valid = false;
                            MessageBox.Show("Please enter a valid End Height for " + text, "Cannot save Planning", MessageBoxButtons.OK);
                        }

                        if (valid == true)
                        {

                            if (r["TargetID"].ToString() == "")
                                newTargetID = -1;
                            else newTargetID = Convert.ToInt32(r["TargetID"].ToString());
                            viewPlanningDevelopment.UpdateCurrentRow();
                            if (r.RowState != DataRowState.Deleted && Convert.ToBoolean(r["hasChanged"].ToString()) == true)
                            {
                                string StartDate = String.Format("{0:MM/dd/yyyy}", r["StartDate"]);
                                string EndDate = String.Format("{0:MM/dd/yyyy}", r["EndDate"]);
                                SqlParameter[] _paramCollection =
                                    {
                                    _SaveData.CreateParameter("@Prodmonth", SqlDbType.Int, 0,Convert.ToInt32(theProdMonth)),
                                    _SaveData.CreateParameter("@Sectionid", SqlDbType.VarChar, 20,r["Sectionid"].ToString()),
                                    _SaveData.CreateParameter("@Sectionid_2", SqlDbType.VarChar, 20,r["Sectionid_2"].ToString()),
                                    _SaveData.CreateParameter("@Workplaceid", SqlDbType.VarChar, 20,r["Workplaceid"].ToString()),
                                    _SaveData.CreateParameter("@Activity", SqlDbType.Int, 0,theActivity),
                                    _SaveData.CreateParameter("@IsCubics", SqlDbType.VarChar, 5,r["IsCubics"].ToString()),
                                    _SaveData.CreateParameter("@SQM", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@WasteSQM", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FL", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceAdvance", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@IdealSW", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@ActualSW", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@ChannelW", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@CMGT", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"CMGT"))),
                                    //  _SaveData.CreateParameter("@GoldBroken", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@cubicgrams", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceCMGT", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceBrokenKG", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceTons", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceValue", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@TrammedTons", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@TrammedValue", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@TrammedLevel", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@CubicMetres", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"CubicMetres"))),
                                    _SaveData.CreateParameter("@OrgUnitDay", SqlDbType.VarChar, 50,r["OrgUnitDay"].ToString()),
                                    _SaveData.CreateParameter("@OrgUnitAfternoon", SqlDbType.VarChar, 50,r["OrgUnitAfternoon"].ToString()),
                                    _SaveData.CreateParameter("@OrgUnitNight", SqlDbType.VarChar, 50,r["OrgUnitNight"].ToString()),
                                    _SaveData.CreateParameter("@RomingCrew", SqlDbType.VarChar, 50,r["RomingCrew"].ToString()),
                                    _SaveData.CreateParameter("@MetersWaste", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"Wasteadv"))),
                                    _SaveData.CreateParameter("@Meters", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"Reefadv"))),
                                    _SaveData.CreateParameter("@WorkplaceDesc", SqlDbType.VarChar, 32,r["WorkplaceDesc"].ToString()),
                                    _SaveData.CreateParameter("@TargetID", SqlDbType.Int, 0,newTargetID),
                                    _SaveData.CreateParameter("@Locked", SqlDbType.Bit, 0,r["Locked"]),
                                    _SaveData.CreateParameter("@DEVTONS", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"DEVTONS"))),
                                    _SaveData.CreateParameter("@DEVMAIN", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"DEVMAIN"))),
                                    _SaveData.CreateParameter("@DEVSEC", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"DEVSEC"))),
                                    _SaveData.CreateParameter("@DEVCAP", SqlDbType.Real, 0,Convert.ToDecimal(testNull(r,"DEVCAP"))),
                                    _SaveData.CreateParameter("@DrillRig", SqlDbType.VarChar, 20,r["DrillRig"].ToString()),
                                    _SaveData.CreateParameter("@StartDate", SqlDbType.VarChar, 30, StartDate),
                                    _SaveData.CreateParameter("@EndDate", SqlDbType.VarChar, 30,EndDate),
                                    _SaveData.CreateParameter("@GramPerTon", SqlDbType.Real, 0,Convert.ToDecimal(testNull (r, "GramPerTon"))),

                                    _SaveData.CreateParameter("@SSID", SqlDbType.Real, 0, 0 ),
                                    _SaveData.CreateParameter("@Units", SqlDbType.Real, 0, 0),// Convert .ToDecimal  (r["Call"].ToString ())),
                                    _SaveData.CreateParameter("@AddInfo", SqlDbType.VarChar , 20,""),
                                    _SaveData.CreateParameter("@AutoUnPlan", SqlDbType.VarChar , 2,""),
                                    _SaveData.CreateParameter("@CrewStrength", SqlDbType.Int, 0,0),
                                    _SaveData.CreateParameter("@PlanCode", SqlDbType.VarChar , 2,"MP"),
                                    _SaveData.CreateParameter("@OGID", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@Depth", SqlDbType.Int , 0,0),
                                    _SaveData.CreateParameter("@grams", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@gt", SqlDbType.Real , 0,0 ),
                                    _SaveData.CreateParameter("@actdepth", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@grade", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@kg", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@CMKGT", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@EndHeight", SqlDbType .Real ,0,Convert .ToDecimal (testNull ( r,"ENDHEIGHT"))),
                                    _SaveData.CreateParameter("@EndWidth", SqlDbType .Real ,0,Convert .ToDecimal (testNull ( r,"ENDWIDTH"))),
                                    _SaveData.CreateParameter("@cubicreef", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@cubicwaste", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@UraniumBrokenKg",SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@RockType",SqlDbType.VarChar , 0,r["RockType"].ToString())
                                };

                                _SaveData.ParamCollection = _paramCollection;
                                _SaveData.queryReturnType = MWDataManager.ReturnType.longNumber;
                                _SaveData.ExecuteInstruction();
                            }
                        }

                        break;

                    case 2:

                        if (r["Sectionid"].ToString() == "-1" || r["OrgUnitDay"] is DBNull || r["OrgUnitNight"] is DBNull || r["SMID"] is DBNull || r["Call"] is DBNull)
                        {
                            text += separator + r["WorkplaceDesc"];
                            separator = ",";
                            valid = false;
                            MessageBox.Show("Please enter missing data for " + text, "Cannot save Planning", MessageBoxButtons.OK);
                        }
                        
                        if (valid == true)
                        {
   
       
                            viewPlanningSundry.UpdateCurrentRow();
                            //if (r.RowState != DataRowState.Deleted && Convert.ToBoolean(r["hasChanged"].ToString()) == true)
                            //{

                                string StartDate = String.Format("{0:MM/dd/yyyy}", r["StartDate"]);
                                string EndDate = String.Format("{0:MM/dd/yyyy}", r["EndDate"]);
                                SqlParameter[] _paramCollection = 
                                {
                                    _SaveData.CreateParameter("@Prodmonth", SqlDbType.Int, 0,Convert.ToInt32(theProdMonth)),
                                    _SaveData.CreateParameter("@Sectionid", SqlDbType.VarChar, 20,r["Sectionid"].ToString()),
                                    _SaveData.CreateParameter("@Sectionid_2", SqlDbType.VarChar, 20,""),
                                    _SaveData.CreateParameter("@Workplaceid", SqlDbType.VarChar, 20,r["Workplaceid"].ToString()),
                                    _SaveData.CreateParameter("@Activity", SqlDbType.Int, 0,theActivity),
                                    _SaveData.CreateParameter("@IsCubics", SqlDbType.VarChar, 5,""),
                                    _SaveData.CreateParameter("@SQM", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@WasteSQM", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FL", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceAdvance", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@IdealSW", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@ActualSW", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@ChannelW", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@CMGT", SqlDbType.Real, 0,0),
                                   // _SaveData.CreateParameter("@GoldBroken", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@cubicgrams", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceCMGT", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceBrokenKG", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceTons", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceValue", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@TrammedTons", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@TrammedValue", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@TrammedLevel", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@CubicMetres", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@OrgUnitDay", SqlDbType.VarChar, 50,r["OrgUnitDay"].ToString()),
                                    _SaveData.CreateParameter("@OrgUnitAfternoon", SqlDbType.VarChar, 50,""),
                                    _SaveData.CreateParameter("@OrgUnitNight", SqlDbType.VarChar, 50,r["OrgUnitNight"].ToString()),
                                    _SaveData.CreateParameter("@RomingCrew", SqlDbType.VarChar, 50,""),
                                    _SaveData.CreateParameter("@MetersWaste", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@Meters", SqlDbType.Real, 0,Convert .ToDecimal ( r["Meters"].ToString())),
                                    _SaveData.CreateParameter("@WorkplaceDesc", SqlDbType.VarChar, 32,""),
                                    _SaveData.CreateParameter("@TargetID", SqlDbType.Int, 0,0),
                                    _SaveData.CreateParameter("@Locked", SqlDbType.Bit, 0,0),
                                    _SaveData.CreateParameter("@DEVTONS", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DEVMAIN", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DEVSEC", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DEVCAP", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DrillRig", SqlDbType.VarChar, 20,""),
                                    _SaveData.CreateParameter("@StartDate", SqlDbType.VarChar, 30, StartDate),
                                    _SaveData.CreateParameter("@EndDate", SqlDbType.VarChar, 30,EndDate),
                                    _SaveData.CreateParameter("@GramPerTon", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@SSID", SqlDbType.Real, 0, Convert.ToInt32(r["SMID"].ToString () ).ToString() ),
                                    _SaveData.CreateParameter("@Units", SqlDbType.Real, 0,Convert .ToDecimal  (r["Call"].ToString ())),
                                    _SaveData.CreateParameter("@AddInfo", SqlDbType.VarChar , 20,""),
                                    _SaveData.CreateParameter("@AutoUnPlan", SqlDbType.VarChar , 2,""),
                                    _SaveData.CreateParameter("@CrewStrength", SqlDbType.Int, 0,0),
                                    _SaveData.CreateParameter("@PlanCode", SqlDbType.VarChar , 2,"MP"),
                                     _SaveData.CreateParameter("@OGID", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@Depth", SqlDbType.Int , 0,0),
                                    _SaveData.CreateParameter("@grams", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@gt", SqlDbType.Real , 0,0 ),
                                    _SaveData.CreateParameter("@actdepth", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@grade", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@kg", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@CMKGT", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@EndHeight", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@EndWidth", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@cubicreef", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@cubicwaste", SqlDbType.Real , 0,0),
                                    _SaveData .CreateParameter ("@UraniumBrokenKg",SqlDbType.Real , 0,0)
                                    //_SaveData.CreateParameter("@EndHeight", SqlDbType.Real , 0,0),
                                    // _SaveData.CreateParameter("@EndWidth", SqlDbType.Real , 0,0),
                                };



                                _SaveData.ParamCollection = _paramCollection;
                                _SaveData.queryReturnType = MWDataManager.ReturnType.longNumber;
                                _SaveData.ExecuteInstruction();
                         // }
                        }

                        break;

                    case 8:

                        if (r["Sectionid"].ToString() == "-1" || r["OrgUnitDay"] is DBNull || r["Activity"] is DBNull || r["Call"] is DBNull || r["DepthRange"] is DBNull)
                        {
                            text += separator + r["WorkplaceDesc"];
                            separator = ",";
                            valid = false;
                            MessageBox.Show("Please enter missing data for " + text, "Cannot save Planning", MessageBoxButtons.OK);
                        }

                        if (valid == true)
                        {

                            if (r["gt"] is DBNull)
                            {
                                gt = 0;
                            }
                            else gt = Convert.ToDouble( r["gt"].ToString());

                            viewPlanningSweepVamp.UpdateCurrentRow();
                            //if (r.RowState != DataRowState.Deleted && Convert.ToBoolean(r["hasChanged"].ToString()) == true)
                            //{

                                string StartDate = String.Format("{0:MM/dd/yyyy}", r["StartDate"]);
                                string EndDate = String.Format("{0:MM/dd/yyyy}", r["EndDate"]);
                                SqlParameter[] _paramCollection = 
                                {
                                    _SaveData.CreateParameter("@Prodmonth", SqlDbType.Int, 0,Convert.ToInt32(theProdMonth)),
                                    _SaveData.CreateParameter("@Sectionid", SqlDbType.VarChar, 20,r["Sectionid"].ToString()),
                                    _SaveData.CreateParameter("@Sectionid_2", SqlDbType.VarChar, 20,""),
                                    _SaveData.CreateParameter("@Workplaceid", SqlDbType.VarChar, 20,r["Workplaceid"].ToString()),
                                    _SaveData.CreateParameter("@Activity", SqlDbType.Int, 0,theActivity),
                                    _SaveData.CreateParameter("@IsCubics", SqlDbType.VarChar, 5,""),
                                    _SaveData.CreateParameter("@SQM", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@WasteSQM", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FL", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceAdvance", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@IdealSW", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@ActualSW", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@ChannelW", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@CMGT", SqlDbType.Real, 0,0),
                                  //  _SaveData.CreateParameter("@GoldBroken", SqlDbType.Real, 0,0),
                                   _SaveData.CreateParameter("@cubicgrams", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceCMGT", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceBrokenKG", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceTons", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@FaceValue", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@TrammedTons", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@TrammedValue", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@TrammedLevel", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@CubicMetres", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@OrgUnitDay", SqlDbType.VarChar, 50,r["OrgunitDay"].ToString()),
                                    _SaveData.CreateParameter("@OrgUnitAfternoon", SqlDbType.VarChar, 50,r["OrgunitAfterNoon"].ToString()),
                                    _SaveData.CreateParameter("@OrgUnitNight", SqlDbType.VarChar, 50,r["OrgunitNight"].ToString()),
                                    _SaveData.CreateParameter("@RomingCrew", SqlDbType.VarChar, 50,""),
                                    _SaveData.CreateParameter("@MetersWaste", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@Meters", SqlDbType.Real, 0,0),
                                    _SaveData.CreateParameter("@WorkplaceDesc", SqlDbType.VarChar, 32,""),
                                    _SaveData.CreateParameter("@TargetID", SqlDbType.Int, 0,0),
                                    _SaveData.CreateParameter("@Locked", SqlDbType.Bit, 0,0),
                                    _SaveData.CreateParameter("@DEVTONS", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DEVMAIN", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DEVSEC", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DEVCAP", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@DrillRig", SqlDbType.VarChar, 20,""),
                                    _SaveData.CreateParameter("@StartDate", SqlDbType.VarChar, 30, StartDate),
                                    _SaveData.CreateParameter("@EndDate", SqlDbType.VarChar, 30,EndDate),
                                    _SaveData.CreateParameter("@GramPerTon", SqlDbType.Real, 0,0.0),
                                    _SaveData.CreateParameter("@SSID", SqlDbType.Real, 0, 0),
                                    _SaveData.CreateParameter("@Units", SqlDbType.Real, 0,Convert .ToDecimal  (r["Call"].ToString ())),
                                    _SaveData.CreateParameter("@AddInfo", SqlDbType.VarChar , 20,""),
                                    _SaveData.CreateParameter("@AutoUnPlan", SqlDbType.VarChar , 2,""),
                                    _SaveData.CreateParameter("@CrewStrength", SqlDbType.Int, 0,0),
                                    _SaveData.CreateParameter("@PlanCode", SqlDbType.VarChar , 2,"MP"),
                                    _SaveData.CreateParameter("@OGID", SqlDbType.Int  , 0,Convert.ToInt32(r["TheID"].ToString ())),
                                    _SaveData.CreateParameter("@Depth", SqlDbType.Int , 0,Convert .ToInt32 (depth ) ),
                                    _SaveData.CreateParameter("@grams", SqlDbType.Int  , 0,Convert .ToInt32  ( r["content"].ToString ())),
                                    _SaveData.CreateParameter("@gt", SqlDbType.Real  , 0,Convert .ToDecimal ( gt )),
                                    _SaveData.CreateParameter("@actdepth", SqlDbType.Real  , 0,Convert .ToDecimal  ( r["ActualDepth"].ToString ())),
                                    _SaveData.CreateParameter("@grade", SqlDbType.Real  , 0,0.00), 
                                    _SaveData.CreateParameter("@kg", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@CMKGT", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@EndHeight", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@EndWidth", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@cubicreef", SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@cubicwaste", SqlDbType.Real , 0,0),
                                    _SaveData .CreateParameter ("@UraniumBrokenKg",SqlDbType.Real , 0,0),
                                    _SaveData.CreateParameter("@RockType",SqlDbType.VarChar , 0,r["RockType"].ToString())
                                };
                                int x = -1;
                                StringBuilder tempstring = new StringBuilder();
                                foreach (SqlParameter a in _paramCollection)
                                {
                                    x += 1;
                                    tempstring.AppendLine(a.ParameterName.ToString() + " = " + a.SqlValue.ToString() + ",");
                                }


                                tempstring.ToString();

                                _SaveData.ParamCollection = _paramCollection;
                                _SaveData.queryReturnType = MWDataManager.ReturnType.longNumber;
                                clsDataResult Data =  _SaveData.ExecuteInstruction();
                            //}
                        }

                        break;
                }

            }
            if (valid == true)
            {
                saveLabour(theActivity);

                //aa
                saveCycle();
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Planning Data was saved successfully", Color.CornflowerBlue);
            }
        }

        public void Insertplanning()
        {
            // bool valid;
            MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
            _SaveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _SaveData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

            int Rowhandle;
            string WP = "";

            //DataColumn dtColumn = PlanningClass.PlanningScreen.viewPlanningDevelopment.Columns[""];

            if (theActivity == 0)
            {
                Rowhandle = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
            }
            else
            {
                Rowhandle = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);
            }

            if (Rowhandle > -1)
            {
                if (theActivity == 0)
                {
                    WP = viewPlanningStoping.GetRowCellValue(Rowhandle, "Workplaceid").ToString();
                }
                else
                {
                    WP = viewPlanningDevelopment.GetRowCellValue(Rowhandle, "Workplaceid").ToString();
                }
               
            }

            if (WP != "")
            {
                _SaveData.SqlStatement = " exec sp_InsertPlanningTableSmoothCallNewWP '" + theProdMonth.ToString() + "' , '" + theSectionIDMO.ToString() + "' ,'" + WP + "' ";

                _SaveData.ExecuteInstruction();
            }
        }

        private void saveLabour(int activity)
        {
               StringBuilder sb = new StringBuilder();
            int currentRow;
            MWDataManager.clsDataAccess _SaveData1 = new MWDataManager.clsDataAccess();
            _SaveData1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _SaveData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            
            _SaveData1.queryReturnType = MWDataManager.ReturnType.DataTable;

            if (activity == 0)
            {
                currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
            }
            else
            {
                currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);
            }
            if (_LabourData.ResultsDataTable != null)
            {
                if (_LabourData.ResultsDataTable.Rows.Count != 0)
                {
                    foreach (DataRow dr in _LabourData.ResultsDataTable.Rows)
                    {
                        //   sb.AppendLine("GO");
                       
                        if (dr["Labourstrength"].ToString() != "")
                        {
                            sb.Clear();
                            sb.AppendLine("UPDATE [dbo].[Planmonth]");
                            sb.AppendLine("   SET ");
                            sb.AppendLine("     Labourstrength=" + dr["Labourstrength"].ToString());

                            sb.AppendLine(" WHERE  SectionID_2 = '" + tblPrePlanData.Rows[currentRow]["SectionID_2"].ToString() + "' and Prodmonth = '" + theProdMonth + "' and  PlanCode = 'MP' and [Activity] = " + theActivity.ToString() + " and OrgUnitDay='" + dr["OrgUnitDay"].ToString() + "'");
                            // sb.AppendLine("GO");
                            _SaveData1.SqlStatement = sb.ToString();
                            _SaveData1.ExecuteInstruction();
                        }

                    }


                    
                }
              
            }
        }

        private void saveCycle()
        {
            StringBuilder sb = new StringBuilder();
            int currentRow;
            switch (theActivity)
            {
                case 0:
                    currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
                    if (_CycleData.ResultsDataTable != null)
                    {
                        if (_CycleData.ResultsDataTable.Rows.Count != 0)
                        {
                            for (int i = 1; i < 50; i++)
                            {
                                //   sb.AppendLine("GO");
                                if (_CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()].ToString() != "")
                                {
                                    sb.AppendLine("UPDATE [dbo].[PLANNING]");
                                    sb.AppendLine("   SET ");
                                    sb.AppendLine("      [SQM] = " + _CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()]);
                                    sb.AppendLine("     ,[MOCycle] = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + i.ToString()] + "'");
                                    sb.AppendLine(" WHERE WorkplaceID = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_Code]["WorkplaceID"] + "' and SectionID = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' and Prodmonth = '" + theProdMonth + "' and Calendardate = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_Date]["Day" + i.ToString()] + "' and PlanCode = 'MP' and [Activity] = " + theActivity.ToString());
                                    // sb.AppendLine("GO");
                                    if (row_Cycle_CodeCube != -1)
                                    {
                                        sb.AppendLine("UPDATE [dbo].[PLANNING]");
                                        sb.AppendLine("   SET ");
                                        sb.AppendLine("      [CubicMetres] = " + _CycleData.ResultsDataTable.Rows[row_Cycle_ValeCube]["Day" + i.ToString()]);
                                        sb.AppendLine("     ,[MOCycleCube] = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_CodeCube]["Day" + i.ToString()] + "'");
                                        sb.AppendLine(" WHERE WorkplaceID = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_CodeCube]["WorkplaceID"] + "' and SectionID = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' and Prodmonth = '" + theProdMonth + "' and Calendardate = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_Date]["Day" + i.ToString()] + "' and PlanCode = 'MP' and [Activity] = " + theActivity.ToString());
                                    }
                                }
                            }

                            MWDataManager.clsDataAccess _SaveData1 = new MWDataManager.clsDataAccess();
                            _SaveData1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _SaveData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            _SaveData1.SqlStatement = sb.ToString();
                            _SaveData1.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _SaveData1.ExecuteInstruction();
                        }
                    }
                    break;
                case 1:
                    currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);
                    if (_CycleData.ResultsDataTable != null)
                    {
                        if (_CycleData.ResultsDataTable.Rows.Count != 0)
                        {
                            for (int i = 1; i < 50; i++)
                            {
                                string test = _CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + i.ToString()].ToString();
                                if (_CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + i.ToString()].ToString() != "")
                                {
                                    //   sb.AppendLine("GO");
                                    sb.AppendLine("UPDATE [dbo].[PLANNING]");
                                    sb.AppendLine("   SET ");
                                    sb.AppendLine("      [Metresadvance] = " + _CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()]);
                                    sb.AppendLine("     ,[MOCycle] = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + i.ToString()] + "'");
                                    sb.AppendLine(" WHERE WorkplaceID = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_Code]["WorkplaceID"] + "' and SectionID = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' and Prodmonth = '" + theProdMonth + "' and Calendardate = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_Date]["Day" + i.ToString()] + "' and PlanCode = 'MP' and [Activity] = " + theActivity.ToString());
                                    // sb.AppendLine("GO");
                                    if (row_Cycle_CodeCube != -1)
                                    {
                                        sb.AppendLine("UPDATE [dbo].[PLANNING]");
                                        sb.AppendLine("   SET ");
                                        sb.AppendLine("      [CubicMetres] = " + _CycleData.ResultsDataTable.Rows[row_Cycle_ValeCube]["Day" + i.ToString()]);
                                        sb.AppendLine("     ,[MOCycleCube] = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_CodeCube]["Day" + i.ToString()] + "'");
                                        sb.AppendLine(" WHERE WorkplaceID = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_CodeCube]["WorkplaceID"] + "' and SectionID = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' and Prodmonth = '" + theProdMonth + "' and Calendardate = '" + _CycleData.ResultsDataTable.Rows[row_Cycle_Date]["Day" + i.ToString()] + "' and PlanCode = 'MP' and [Activity] = " + theActivity.ToString());
                                    }
                                }
                            }

                            MWDataManager.clsDataAccess _SaveData1 = new MWDataManager.clsDataAccess();
                            _SaveData1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _SaveData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            _SaveData1.SqlStatement = sb.ToString();
                            _SaveData1.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _SaveData1.ExecuteInstruction();
                        }
                    }
                    break;
            }

        }

        private void editTotalPlan_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void viewPlanningStoping_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {



        }


        public void export()
        {
            // viewPlanningStoping.ExportToText("c:\\text.txt");
            // viewPlanningStoping.ExportToXls("c:\\Excel2007.xls");
            // viewPlanningStoping.ExportToXlsx("c:\\Excel2008.xlsx");
            switch (theActivity)
            {
                case 0:
                    viewPlanningStoping.SelectAll();
                    viewPlanningStoping.CopyToClipboard();
                    break;
                case 1:
                    viewPlanningDevelopment.SelectAll();
                    viewPlanningDevelopment.CopyToClipboard();
                    break;
                case 2:
                    viewPlanningSundry.SelectAll();
                    viewPlanningSundry.CopyToClipboard();
                    break;
                case 8:
                    viewPlanningSweepVamp.SelectAll();
                    viewPlanningSweepVamp.CopyToClipboard();
                    break;
            }

            //MainGrid.ShowPrintPreview();// Print();
        }

        private void setEditColor(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.TextEdit tempEdit;
            tempEdit = sender as DevExpress.XtraEditors.TextEdit;
            if (Convert.ToDouble(tempEdit.Text) < 0)
            {
                tempEdit.Properties.AppearanceReadOnly.ForeColor = Color.Red;
            }
            else { tempEdit.Properties.AppearanceReadOnly.ForeColor = Color.Blue; }
        }

        private void viewPlanningDevelopment_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view;
            view = sender as GridView;

            if ((view.FocusedColumn.FieldName == "GramPerTon" || view.FocusedColumn.FieldName == "CMGT") && (tblPrePlanData.Rows[view.FocusedRowHandle]["Metresadvance"].ToString() == "" || Convert.ToDecimal(tblPrePlanData.Rows[view.FocusedRowHandle]["Metresadvance"].ToString()) == 0))
            {
                e.Cancel = true;
            }


            if (tblPrePlanData.Rows[view.FocusedRowHandle]["Locked"].ToString() == "True")
            {
                e.Cancel = true;
            }
            else
            {
                if (tblPrePlanData.Rows[view.FocusedRowHandle]["WORKPLACEID"].ToString() == "-1")
                {
                    bcSecDev.OptionsColumn.AllowEdit = true;
                    bcMainDev.OptionsColumn.AllowEdit = true;
                    bcCapDev.OptionsColumn.AllowEdit = true;
                    bcTonsDev.OptionsColumn.AllowEdit = true;
                }
                else
                {
                    bcMainDev.OptionsColumn.AllowEdit = true;
                    bcSecDev.OptionsColumn.AllowEdit = true;
                    bcCapDev.OptionsColumn.AllowEdit = false;
                    bcTonsDev.OptionsColumn.AllowEdit = false;
                }
            }

            //if (tblPrePlanData.Rows[view.FocusedRowHandle]["REEFWASTEIND"].ToString() != "0" && view.FocusedColumn.Caption == "Reef") 
            //{
            //    e.Cancel = true;
            //}

            //if (tblPrePlanData.Rows[view.FocusedRowHandle]["REEFWASTEIND"].ToString() != "1" && view.FocusedColumn.Caption == "Wast")
            //{
            //    e.Cancel = true;
            //}


        }

        private void viewPlanningDevelopment_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.FieldName == "MonthlyReefSQM")
            {
                int WasteSQM = Convert.ToInt32( viewPlanningDevelopment.GetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["MonthlyWatseSQM"]) );

                int newCall = Convert.ToInt32(e.Value) + WasteSQM;

                viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["MonthlyTotalSQM"], newCall);
            }

            if (e.Column.FieldName == "MonthlyWatseSQM")
            {
                int ReefSQM = Convert.ToInt32(viewPlanningDevelopment.GetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["MonthlyReefSQM"]));

                int newCall = Convert.ToInt32(e.Value) + ReefSQM;

                viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["MonthlyTotalSQM"], newCall);
            }

            if (e.Column.FieldName == "MonthlyTotalSQM")
            {
                if (viewPlanningDevelopment.GetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["Metresadvance"]).ToString() != TotlSqmLbl.Text)
                {
                    viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["Metresadvance"], e.Value.ToString());
                }                
            }

            if (e.Column.FieldName == "Metresadvance")
            {
                decimal TotMonthcall = Convert.ToInt32( viewPlanningDevelopment.GetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["MonthlyTotalSQM"] )  );
                decimal ReefMonthcall = Convert.ToInt32(viewPlanningDevelopment.GetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["MonthlyReefSQM"])  );

                decimal ratio = 0;

                if (ReefMonthcall != 0)
                {
                    ratio = TotMonthcall / ReefMonthcall;
                }

                decimal TotActCall = Convert.ToDecimal( viewPlanningDevelopment.GetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["Metresadvance"]) );
                decimal ReefCall = 0;
                decimal WasteCall = 0;

                if (ratio != 0)
                {
                    ReefCall = Convert.ToDecimal(Convert.ToDecimal(TotActCall) / ratio);
                }


                if (TotMonthcall != 0)
                {
                    WasteCall = TotActCall - ReefCall;
                }
                else
                {
                    ReefCall = TotActCall;
                }

                if (Convert.ToDecimal(viewPlanningDevelopment.GetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["CubicMetres"]).ToString()) > 0 && TotMonthcall == 0)
                {
                    ReefCall = TotMonthcall;
                    viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["ReefAdv"], TotMonthcall);
                    viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["WasteAdv"], WasteCall);
                }
                else
                {
                    viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["ReefAdv"], ReefCall);
                    viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["WasteAdv"], WasteCall);
                }

                //WasteCall = TotActCall - ReefCall;


               // viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["ReefAdv"], ReefCall);
               // viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["WasteAdv"], WasteCall);

                //viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["Metresadvance"], e.Value.ToString());
            }


            MWDataManager.clsDataAccess _CalcData = new MWDataManager.clsDataAccess() { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement, queryReturnType = MWDataManager.ReturnType.DataTable };

            BandedGridView view;
            view = sender as BandedGridView;
            object row = view.GetRow(view.FocusedRowHandle); // get the current row 


            (row as DataRowView)["hasChanged"] = 1;
            String CMKGT = "0";
            if ((row as DataRowView)["CMKGT"].ToString() != "")
                CMKGT = (row as DataRowView)["CMKGT"].ToString();


            _CalcData.SqlStatement = "DECLARE @StopeWidth float,@CubicMeters float,@FaceLength float,@ReefAdv  float, @WasteAdv  float, " +
                                    "@CMGT float,@CMKGT float,@Workplaceid varchar(50),@FaceCMGT float,@FaceAdvance float,@GoldBrokenSQM float,@UraniumBrokenSQM float, " +
                                    "@GoldBrokenCUB float,@UraniumBrokenCUB float, @FaceBrokenKG float, @FaceTonsSQM float, @FaceTonsCube float, @FaceValue float, " +
                                    "@TrammedTons float, @TrammedValue float,@ChannelW int,@cubicgrams float  " +   

                                    " SET @CMKGT = " + CMKGT +
                                    " SET @Workplaceid = '" + (row as DataRowView)["Workplaceid"] + "' " +
                                    " set @ReefAdv=" + (row as DataRowView)["ReefAdv"] +
                                    " set @UraniumBrokenSQM = dbo.CalcGoldBrokenSQM(@ReefAdv,@CMKGT,@Workplaceid)  " +
                                    " set @UraniumBrokenCUB=dbo.CalcUraniumBrokenCUB(@ReefAdv,@CMKGT,@StopeWidth,@Workplaceid) " +
                                    " select @UraniumBrokenSQM UraniumBrokenSQM,@UraniumBrokenCUB UraniumBrokenCUB";
            _CalcData.ExecuteInstruction();



            foreach (DataRow r in _CalcData.ResultsDataTable.Rows)
            {
                (row as DataRowView)["UraniumBroken"] = Convert.ToDecimal(r["UraniumBrokenSQM"]) + (Convert.ToDecimal(r["UraniumBrokenCUB"]) / 100);
            }


            if (view.FocusedColumn.FieldName == "Sectionid")
            {
                // updates start and en date based on selected miner. Will update if no date or date is different from current
                string selectedSectioID = (row as DataRowView)["SectionID"].ToString();
                DataRow[] result = PlanningClass.tblSectionStartEndDates.Select(String.Format("SectionID = '{0}'", selectedSectioID));
                if (result.Length == 1)
                {
                    if ((row as DataRowView)["StartDate"] == null || (row as DataRowView)["StartDate"] != result[0]["StartDate"].ToString())
                    {
                        (row as DataRowView)["StartDate"] = result[0]["StartDate"].ToString();
                    }
                    if ((row as DataRowView)["EndDate"] == null || (row as DataRowView)["EndDate"] != result[0]["EndDate"].ToString())
                    {
                        (row as DataRowView)["EndDate"] = result[0]["EndDate"].ToString();
                    }
                }

            }



            //  tblPrePlanData.Rows[e.RowHandle]["hasChnaged"] = 1;

            if (view.FocusedColumn.FieldName == "Sectionid")
            {

                string selectedSectioID = (row as DataRowView)["Sectionid"].ToString();
            }

            if (view.FocusedColumn.FieldName == "ReefAdv")
            {
                if ( (row as DataRowView)["GG025_TMS"].ToString() == "M")
                {
                    (row as DataRowView)["DevMain"] = e.Value;
                }
                else if ( (row as DataRowView)["GG025_TMS"].ToString() == "S")
                { (row as DataRowView)["DevSec"] = e.Value; }

                (row as DataRowView)["Metresadvance"] = Convert.ToDecimal((row as DataRowView)["ReefAdv"].ToString()) + Convert.ToDecimal((row as DataRowView)["WasteAdv"].ToString());
            }

            if (view.FocusedColumn.FieldName == "WasteAdv" || view.FocusedColumn.FieldName == "ReefAdv")
            {

                if ((row as DataRowView)["ACCOUNTCODE"].ToString() == "1")
                { (row as DataRowView)["DEVCAP"] = e.Value; }

                if ((row as DataRowView)["WORKPLACEID"].ToString() != "-1")
                {
                    decimal DH;
                    decimal DW;
                    decimal RD;
                    decimal WD;
                   
                    if ((row as DataRowView)["ENDHEIGHT"] is DBNull || (row as DataRowView)["ENDHEIGHT"].ToString() == null)
                    {
                        DH = 0;
                    }
                    else
                    {
                        DH = Convert.ToDecimal((row as DataRowView)["ENDHEIGHT"].ToString());
                    }
                    if ((row as DataRowView)["ENDWIDTH"] is DBNull)
                    {
                        DW = 0;
                    }
                    else
                    {
                     DW = Convert.ToDecimal((row as DataRowView)["ENDWIDTH"].ToString());
                        }

                    if ((row as DataRowView)["Reefadv"] is DBNull)
                    {
                        RD = 0;
                    }
                    else
                    {
                        RD = Convert.ToDecimal((row as DataRowView)["Reefadv"].ToString());
                    }

                    if ((row as DataRowView)["Wasteadv"] is DBNull)
                    {
                        WD = 0;
                    }
                    else
                    {
                        WD = Convert.ToDecimal((row as DataRowView)["Wasteadv"].ToString());
                    }
                    //decimal TotalMeters = Convert.ToDecimal((row as DataRowView)["Reefadv"].ToString()) + Convert.ToDecimal((row as DataRowView)["Wasteadv"].ToString());
                    decimal TotalMeters = RD + WD;
                    (row as DataRowView)["DEVTONS"] = DH * DW * Convert.ToDecimal(Convert.ToDouble(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).RockDensity)) * TotalMeters;
                }
                else (row as DataRowView)["DEVTONS"] = 0.00;
                (row as DataRowView)["Metresadvance"] = Convert.ToDecimal((row as DataRowView)["Reefadv"].ToString()) + Convert.ToDecimal((row as DataRowView)["Wasteadv"].ToString());

            }



            viewPlanningDevelopment.InvalidateFooter();
        }

        //public void SQMCalc()
        //{
        //    int sqmtotal = Convert.ToInt32(SaveTotSqm);
        //    int fl = Convert.ToInt32(SaveFL);

        //    decimal dailycallsqm = Math.Round((Convert.ToDecimal(fl) * Convert.ToDecimal(0.8)),0);



        //    for (int i = 2; i < 48; i++)
        //    {

        //    }
        //}


        //public void SavePlanning()
        //{
        //    for (int i = 2; i < 48; i++)
        //    {



        //    }

        //}


        private void viewPlanningStoping_ShowingEditor(object sender, CancelEventArgs e)
        {

            GridView view;
            view = sender as GridView;

            if ((view.FocusedColumn.FieldName == "CMGT" || view.FocusedColumn.FieldName == "ChannelW" || view.FocusedColumn.FieldName == "IdealSW") && tblPrePlanData.Rows[view.FocusedRowHandle]["isApproved"].ToString() == "1")
            {
                e.Cancel = true;
            }
            if (tblPrePlanData.Rows[view.FocusedRowHandle]["hasRevised"] != null)
            {
                if (Convert.ToBoolean(tblPrePlanData.Rows[view.FocusedRowHandle]["hasRevised"].ToString()) == true)
                {
                    e.Cancel = true;
                }
            }




        }


        private void viewPlanningStoping_EndSorting(object sender, EventArgs e)
        {
            tblPrePlanData.AcceptChanges();
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        

        private void viewPlanningDevelopment_RowClick(object sender, RowClickEventArgs e)
        {
            GridView view;
            view = sender as GridView;

          


            if (tblPrePlanData.Rows[view.FocusedRowHandle]["WORKPLACEID"].ToString() == "-1")
            {
                bcSecDev.OptionsColumn.AllowEdit = true;
                bcMainDev.OptionsColumn.AllowEdit = true;
                bcCapDev.OptionsColumn.AllowEdit = true;
                bcTonsDev.OptionsColumn.AllowEdit = true;
            }
            else
            {

                bcMainDev.OptionsColumn.AllowEdit = true;
                bcSecDev.OptionsColumn.AllowEdit = true;
                bcCapDev.OptionsColumn.AllowEdit = false;
                bcTonsDev.OptionsColumn.AllowEdit = false;
            }


            showCycle(_cycleActive);
 
        }

        private void viewPlanningStoping_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {

            if (e.Column.FieldName == "callValue")
            {

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "2"))
                {


                    e.Info.DisplayText = String.Format("{0:0} m²", OpspalnSQM);
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "3"))
                {

                    double totalSQM = getTotalSQM(tblPrePlanData);
                    double theVar = totalSQM - OpspalnSQM;
                    e.Info.DisplayText = String.Format("{0:0} m²", theVar);
                    if (theVar > 0)
                        e.Appearance.ForeColor = Color.Blue;
                    else e.Appearance.ForeColor = Color.Red;

                }
            }

            if (e.Column.FieldName == "CMGT")
            {

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "1"))
                {



                    double GoldBroken = getTotalGoldBroken(tblPrePlanData);
                    double sqm = getTotalSQM(tblPrePlanData);
                    if (sqm != 0)
                    {
                        theValue = (GoldBroken * 100000) / (sqm * Convert.ToDouble(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).RockDensity));
                    }
                    else { theValue = 0.0; }

                    //             e.TotalValue = theValue;
                    e.Info.DisplayText = String.Format("{0:0}", theValue);
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "2"))
                {
                    //  e.Appearance.ForeColor = Color.Red;

                    //  e.Column.SummaryItem.Collection[1].DisplayFormat = String.Format("{0:0}", OpsplanKG);

                    if (OpspalnSQM != 0)
                    {
                        OpsCMGT = (OpsplanKG * 100000) / (OpspalnSQM * Convert.ToDouble(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).RockDensity));
                        e.Info.DisplayText = String.Format("{0:0}", OpsCMGT);
                    }
                    else { e.Info.DisplayText = String.Format("{0:0}", 0); }
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "3"))
                {
                    //  e.Appearance.ForeColor = Color.Red;
                    double theVar = theValue - OpsCMGT;
                    e.Info.DisplayText = String.Format("{0:0}", theVar);
                    if (theVar > 0)
                        e.Appearance.ForeColor = Color.Blue;
                    else e.Appearance.ForeColor = Color.Red;

                }

            }

            if (e.Column.FieldName == "FaceCMGT")
            {

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "1"))
                {



                    double GoldBroken = getTotalGoldFaceBroken(tblPrePlanData);
                    double sqm = getTotalSQM(tblPrePlanData);
                    if (sqm != 0)
                    {
                        theValue = (GoldBroken * 100000) / (sqm * Convert.ToDouble(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).RockDensity));
                    }
                    else { theValue = 0.0; }

                    //             e.TotalValue = theValue;
                    e.Info.DisplayText = String.Format("{0:0}", theValue);
                }

            }

            if (e.Column.FieldName == "CMKGT")
            {
                if (ReferenceEquals(e.Info.SummaryItem.Tag, "1"))
                {



                    double UraniumBroken = getTotalUraniumBroken(tblPrePlanData);
                    double sqm = getTotalU308SQM(tblPrePlanData);
                    if (sqm != 0)
                    {
                        theValue = (UraniumBroken * 10000) / (sqm * Convert.ToDouble(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).RockDensity));
                    }
                    else { theValue = 0.0; }

                    //             e.TotalValue = theValue;
                    e.Info.DisplayText = String.Format("{0:0}", theValue);
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "2"))
                {
                    //  e.Appearance.ForeColor = Color.Red;

                    //  e.Column.SummaryItem.Collection[1].DisplayFormat = String.Format("{0:0}", OpsplanKG);

                    if (OpspalnSQM != 0)
                    {
                        OpsCMGT = (OpsplanKG * 10000) / (OpspalnSQM * Convert.ToDouble(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).RockDensity));
                        e.Info.DisplayText = String.Format("{0:0}", OpsCMGT);
                    }
                    else { e.Info.DisplayText = String.Format("{0:0}", 0.0); }
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "3"))
                {
                    //  e.Appearance.ForeColor = Color.Red;
                    double theVar = theValue - OpsCMGT;
                    e.Info.DisplayText = String.Format("{0:0}", theVar);
                    if (theVar > 0)
                        e.Appearance.ForeColor = Color.Blue;
                    else e.Appearance.ForeColor = Color.Red;

                }
            }

            if (e.Column.FieldName == "GoldBroken")
            {


                if (ReferenceEquals(e.Info.SummaryItem.Tag, "2"))
                {
                    //  e.Appearance.ForeColor = Color.Red;

                    e.Info.DisplayText = String.Format("{0:F3} kg", OpsplanKG);
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "3"))
                {
                    //  e.Appearance.ForeColor = Color.Red;
                    double GoldBroken = getTotalGoldBroken(tblPrePlanData);
                    double theVar = GoldBroken - OpsplanKG;
                    e.Info.DisplayText = String.Format("{0:F3} kg", theVar);
                    if (theVar > 0)
                        e.Appearance.ForeColor = Color.Blue;
                    else e.Appearance.ForeColor = Color.Red;

                }

            }
			
			 if (e.Column.FieldName == "UraniumBroken")
            {
                if (ReferenceEquals(e.Info.SummaryItem.Tag, "2"))
                {
                    //  e.Appearance.ForeColor = Color.Red;

                    e.Info.DisplayText = String.Format("{0:F3} kg", OpsplanKG);
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "3"))
                {
                    //  e.Appearance.ForeColor = Color.Red;
                    double UraniumBroken = getTotalUraniumBroken(tblPrePlanData);
                    double theVar = UraniumBroken - OpsplanKG;
                    e.Info.DisplayText = String.Format("{0:F3} kg", theVar);
                    if (theVar > 0)
                        e.Appearance.ForeColor = Color.Blue;
                    else e.Appearance.ForeColor = Color.Red;

                }
            }


        }

        private void viewPlanningStoping_LostFocus(object sender, EventArgs e)
        {
            viewPlanningStoping.RefreshData();
        }

        private void viewPlanningStoping_GotFocus(object sender, EventArgs e)
        {
            viewPlanningStoping.RefreshData();
        }

        private void viewPlanningDevelopment_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {

            AppearanceDefault opsplanLook = new AppearanceDefault(Color.Coral, Color.Coral,
            new Font(AppearanceObject.DefaultFont, FontStyle.Bold));

            AppearanceDefault headingLook = new AppearanceDefault(Color.Black, Color.Empty,
           new Font(AppearanceObject.DefaultFont, FontStyle.Bold));

            if (e.Column.FieldName == "WorkplaceDesc")
            {
                AppearanceHelper.Apply(e.Appearance, headingLook);

            }


            if (e.Column.FieldName == "Metresadvance")
            {

                //    if (e.Column.SummaryItem.Tag.ToString() == "0")
                //       return;

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "1"))
                {
                    e.Info.DisplayText = e.Info.DisplayText = String.Format("{0:F1} m", getTotalDevMeters(tblPrePlanData));
                    //e.Info.DisplayText = String.Format("{0:F1} m", getSubTotalDevCap(tblPrePlanData, viewPlanningDevelopment.FocusedRowHandle));      //METERSADV+"m";
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "2"))
                {
                    //  e.Appearance.ForeColor = Color.Red;

                    e.Info.DisplayText = String.Format("{0:F1} m", getTotalDevCap(tblPrePlanData));
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "3"))
                {
                    //  e.Appearance.ForeColor = Color.Red;

                    double TotalDev = getTotalDevMeters(tblPrePlanData);
                    double TotalCapitalDev = getTotalDevCap(tblPrePlanData);
                    e.Info.DisplayText = String.Format("{0:F1} m", TotalDev + TotalCapitalDev);


                }


                if (ReferenceEquals(e.Info.SummaryItem.Tag, "4"))
                {
                    //  e.Appearance.ForeColor = Color.Red;


                    e.Info.DisplayText = String.Format("{0:F1} m", OpsPlanReefDev + OpsPlanWastDev);
                    AppearanceHelper.Apply(e.Appearance, opsplanLook);
                    // e.Appearance.BackColor2 = Color.Coral;
                    //   e.Appearance.BackColor = Color.Coral;


                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "5"))
                {
                    //  e.Appearance.ForeColor = Color.Red;

                    e.Info.DisplayText = String.Format("{0:F1} m", OpsPlanCap);



                }
            }


            if (e.Column.FieldName == "ReefAdv")
            {

                //    if (e.Column.SummaryItem.Tag.ToString() == "0")
                //       return;

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "1"))
                {
                    e.Info.DisplayText = string.Format("{0:F1}", getTotalReef(tblPrePlanData) - getTotalReefDevCap(tblPrePlanData));
                   // e.Info.DisplayText = string.Format("{0:F1}m",getSubTotalReef(tblPrePlanData , viewPlanningDevelopment .FocusedRowHandle ));
                }


                if (ReferenceEquals(e.Info.SummaryItem.Tag, "2"))
                {
                    //e.Info.DisplayText = string.Format("{0:F1}", getTotalDevCap(tblPrePlanData)+ getTotalDevCap(tblPrePlanData));
                    e.Info.DisplayText = string.Format("{0:F1}", getTotalReefDevCap(tblPrePlanData) );
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "3"))
                {
                    e.Info.DisplayText = string.Format("{0:F1}",getTotalReef(tblPrePlanData));
                }


                if (ReferenceEquals(e.Info.SummaryItem.Tag, "4"))
                {
                    //  e.Appearance.ForeColor = Color.Red;

                    double TotalDev = getTotalDevMeters(tblPrePlanData);
                    double TotalCapitalDev = getTotalDevCap(tblPrePlanData);
                    e.Info.DisplayText = String.Format("{0:F1} m", OpsPlanReefDev );
                    AppearanceHelper.Apply(e.Appearance, opsplanLook);
                }



            }

            if (e.Column.FieldName == "WasteAdv")
            {

                //    if (e.Column.SummaryItem.Tag.ToString() == "0")
                //       return;

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "1"))
                {
                    object WasteAdv = viewPlanningDevelopment.GetRowCellValue(viewPlanningDevelopment.FocusedRowHandle, viewPlanningDevelopment.Columns["WasteAdv"]);
                    string WASTEADV = Convert.ToString(WasteAdv);
                    //e.Info.DisplayText =string.Format("{0:F1}m",getSubTotalwaste(tblPrePlanData,viewPlanningDevelopment .FocusedRowHandle ) );
                    e.Info.DisplayText = string.Format("{0:F1}", getTotalWaste(tblPrePlanData) - getTotalWasteDevCap(tblPrePlanData));
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "2"))
                {
                    e.Info.DisplayText = string.Format("{0:F1}", getTotalWasteDevCap(tblPrePlanData));
                }

                if (ReferenceEquals(e.Info.SummaryItem.Tag, "3"))
                {
                    e.Info.DisplayText = string.Format("{0:F1}", getTotalWaste(tblPrePlanData));
                }


                if (ReferenceEquals(e.Info.SummaryItem.Tag, "4"))
                {
                    //  e.Appearance.ForeColor = Color.Red;

                    double TotalDev = getTotalDevMeters(tblPrePlanData);
                    double TotalCapitalDev = getTotalDevCap(tblPrePlanData);
                    e.Info.DisplayText = String.Format("{0:F1} m", OpsPlanWastDev);
                    AppearanceHelper.Apply(e.Appearance, opsplanLook);


                }



            }


        }

        private void viewPlanningDevelopment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewPlanningDevelopment.InvalidateFooter();
        }

        private void viewPlanningStoping_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewPlanningStoping.InvalidateFooter();

        }

        private void viewPlanningStoping_ShownEditor_1(object sender, EventArgs e)
        {

        }

        private void viewPlanningDevelopment_ShownEditor(object sender, EventArgs e)
        {

        }

        private void viewPlanningDevelopment_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void MainGrid_Click(object sender, EventArgs e)
        {

        }

        private void viewPlanningStoping_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void MainGrid_Click_1(object sender, EventArgs e)
        {

        }

        private void viewPlanningStoping_RowClick(object sender, RowClickEventArgs e)
        {
           // showCycle(_cycleActive);
        }

        private void viewPlanningStoping_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {

        }

        private void viewPlanningStoping_RowCellClick_1(object sender, RowCellClickEventArgs e)
        {
            string test = e.Column.FieldName;
        }

        private void viewPlanningStoping_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {

        }

        private void ucPreplanning_Load(object sender, EventArgs e)
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL1 = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL1.SetsystemDBTag = this.theSystemDBTag;
            BMEBL1.SetUserCurrentInfo = this.UserCurrentInfo;

                gcCMKGTvalue.Visible = false ;
                gcUranium.Visible = false;
                bcCMKGTvalue.Visible = false;
                bcUranium.Visible = false;
        }

        private void editMoSectionID_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void viewPlanningSundry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewPlanningSundry.InvalidateFooter();
        }

        private void viewPlanningSundry_GotFocus(object sender, EventArgs e)
        {
            viewPlanningSundry.RefreshData();
        }

        private void viewPlanningSundry_LostFocus(object sender, EventArgs e)
        {
            viewPlanningSundry.RefreshData();
        }

        private void viewPlanningSundry_EndSorting(object sender, EventArgs e)
        {
            tblPrePlanData.AcceptChanges();
        }

        private void viewPlanningSundry_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            BandedGridView view;
            view = sender as BandedGridView;
            object row = view.GetRow(view.FocusedRowHandle); // get the current row 
            (row as DataRowView)["hasChanged"] = 1;
        }

        private void viewPlanningSweepVamp_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            BandedGridView view;
            view = sender as BandedGridView;
            object row = view.GetRow(view.FocusedRowHandle); // get the current row 
            (row as DataRowView)["hasChanged"] = 1;

            if (e.Column.FieldName == "Call" || e.Column.FieldName == "ActualDepth" || e.Column.FieldName == "gt")
            {
                if (tblPrePlanData.Rows[e.RowHandle]["UnitType"].ToString() == "SQM")
                {
                    if (tblPrePlanData.Rows[e.RowHandle]["Call"].ToString() == "")
                    {
                        tblPrePlanData.Rows[e.RowHandle]["Call"] = "0";
                    }
                    decimal sqm = Convert.ToDecimal(tblPrePlanData.Rows[e.RowHandle]["Call"].ToString());
                    decimal depth = Convert.ToDecimal(tblPrePlanData.Rows[e.RowHandle]["ActualDepth"].ToString());
                    if (tblPrePlanData.Rows[e.RowHandle]["gt"].ToString() == "")
                    {
                        tblPrePlanData.Rows[e.RowHandle]["gt"] = "0";
                    }
                    decimal grade = Convert.ToDecimal(tblPrePlanData.Rows[e.RowHandle]["gt"].ToString());
                    if (sqm != 0 && depth != 0) 
                    {
                        sqm = Convert.ToDecimal(tblPrePlanData.Rows[e.RowHandle]["Call"].ToString());
                        depth = Convert.ToDecimal(tblPrePlanData.Rows[e.RowHandle]["ActualDepth"].ToString());
                        grade = Convert.ToDecimal(tblPrePlanData.Rows[e.RowHandle]["gt"].ToString()) ;
                        tblPrePlanData.Rows[e.RowHandle]["content"] = Convert.ToInt32( (sqm * depth / 100) * TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).BrokenRockDensity * grade);
                       // tblPrePlanData.Rows[e.RowHandle]["gt"] = Convert.ToInt32((sqm * depth / 100) * TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).BrokenRockdensity * grade);
                    }
                }
            }
        }

        private void viewPlanningSundry_ShowingEditor(object sender, CancelEventArgs e)
        {

            GridView view;
            view = sender as GridView;
            //if (tblPrePlanData.Rows[view.FocusedRowHandle]["Locked"].ToString() == "True")
            //{
            //    e.Cancel = true;
            //}
        }

        private void reMinerSelection_EditValueChanged(object sender, EventArgs e)
        {
           

            DevExpress.XtraEditors.LookUpEdit editor = sender as DevExpress.XtraEditors.LookUpEdit;
            DataRowView row = editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue) as DataRowView;
            object value = row["SectionID"];

            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;


            _MinerData.SqlStatement = "select distinct a.SECTIONID_1, Name_1 from section_complete a inner join SECCAL b on " +
                                      "A.PRODMONTH = B.Prodmonth AND " +
                                      "A.SECTIONID_1 = B.SectionID " +
                                      "where A.prodmonth = '" + theProdMonth + "' and a.SECTIONID = '" + value + "' ORDER BY a.Name_1";

            _MinerData.ExecuteInstruction();

            if (theActivity == 0)
            {
                viewPlanningStoping.SetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["Sectionid_1"], _MinerData.ResultsDataTable.Rows[0][1].ToString());
            }
            else
            {
                viewPlanningDevelopment.SetRowCellValue(viewPlanningDevelopment.FocusedRowHandle, viewPlanningDevelopment.Columns["Sectionid_1"], _MinerData.ResultsDataTable.Rows[0][1].ToString());
            }






        }

        public void Genirate_Cycle(string Workplace, string Section)
        {

            var theResult = new clsResults();
            clsDataAccess theData = new clsDataAccess();
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, TUserInfo.Connection);
            switch (theActivity)
            {
                case 0:
                    theData.SqlStatement = "Declare @Workplaceid VarChar(20), \n" +
                                            "@Sectionid VarChar(20), \n" +
                                            "@SQL VarChar(8000), \n" +
                                            "@Activitycode Int, \n" +
                                            "@Iscubics Varchar(1), \n" +
                                            "@Prodmonth Numeric(7) \n" +
                                            "\n" +
                                            "DECLARE _Cursor CURSOR FOR \n" +
                                            "\n" +
                                            "select a.Prodmonth, a.Sectionid, a.Workplaceid, a.Activity, a.Iscubics from planmonth a inner \n" +
                                            "join Section_Complete b on \n" +
                                            "a.prodmonth = b.prodmonth and \n" +
                                            "a.sectionid = b.sectionid and \n" +
                                            "PlanCode = 'MP' \n" +
                                            "where a.Prodmonth = " + theProdMonth + " \n" +
                                            "and b.SectionID_2 = '" + editMoSectionID.Text + "' \n" +
                                            "and activity = 0 \n" +
                                            "and Workplaceid = '"+ Workplace + "'" +
                                            "\n" +
                                            "OPEN _Cursor; \n" +
                                            "FETCH NEXT FROM _Cursor \n" +
                                            "into @Prodmonth, @Sectionid, @Workplaceid, @Activitycode, @Iscubics; \n" +
                                            "\n" +
                                            "WHILE @@FETCH_STATUS = 0 \n" +
                                            "BEGIN \n" +
                                            "\n" +
                                            "--select @Sectionid, @Workplaceid, @Activitycode, @Iscubics \n" +
                                            "Set @SQL = 'exec [SP_Save_Stope_CyclePlan] \n" +
                                            "@Username = ''" + TUserInfo.UserID + "'',  \n" +
                                            "@ProdMonth = '+cast(@Prodmonth as Varchar(7))+',  \n" +
                                            "@WorkplaceID = '''+@Workplaceid+''', \n" +
                                            "@SectionID = '''+@Sectionid+''',  \n" +
                                            "@Activity = '+cast(@Activitycode as Varchar(1))+',  \n" +
                                            "@IsCubics = '''+@Iscubics+'''' \n" +
                                            "\n" +
                                            "exec(@SQL) \n" +
                                            "\n" +
                                            "FETCH NEXT FROM _Cursor \n" +
                                            "into @Prodmonth, @Sectionid, @Workplaceid, @Activitycode, @Iscubics; \n" +
                                            "\n" +
                                            "END \n" +
                                            "\n" +
                                            "CLOSE _Cursor; \n" +
                                            "DEALLOCATE _Cursor; ";
                    theData.ExecuteInstruction();
                    break;
                case 1:
                    theData.SqlStatement = "Declare @Workplaceid VarChar(20), \n" +
                                            "@Sectionid VarChar(20), \n" +
                                            "@SQL VarChar(8000), \n" +
                                            "@Activitycode Int, \n" +
                                            "@Iscubics Varchar(1), \n" +
                                            "@Prodmonth Numeric(7) \n" +
                                            "\n" +
                                            "DECLARE _Cursor CURSOR FOR \n" +
                                            "\n" +
                                            "select a.Prodmonth, a.Sectionid, a.Workplaceid, a.Activity, a.Iscubics from planmonth a inner \n" +
                                            "join Section_Complete b on \n" +
                                            "a.prodmonth = b.prodmonth and \n" +
                                            "a.sectionid = b.sectionid and \n" +
                                            "PlanCode = 'MP' \n" +
                                            "where a.Prodmonth = " + theProdMonth + " \n" +
                                            "and b.SectionID_2 = '" + editMoSectionID.Text + "' \n" +
                                            "and activity = 1 \n" +
                                            "and Workplaceid = '" + Workplace + "'" +
                                            "\n" +
                                            "OPEN _Cursor; \n" +
                                            "FETCH NEXT FROM _Cursor \n" +
                                            "into @Prodmonth, @Sectionid, @Workplaceid, @Activitycode, @Iscubics; \n" +
                                            "\n" +
                                            "WHILE @@FETCH_STATUS = 0 \n" +
                                            "BEGIN \n" +
                                            "\n" +
                                            "--select @Sectionid, @Workplaceid, @Activitycode, @Iscubics \n" +
                                            "Set @SQL = 'exec [SP_Save_Dev_CyclePlan] \n" +
                                            "@Username = ''" + TUserInfo.UserID + "'',  \n" +
                                            "@ProdMonth = '+cast(@Prodmonth as Varchar(7))+',  \n" +
                                            "@WorkplaceID = '''+@Workplaceid+''', \n" +
                                            "@SectionID = '''+@Sectionid+''',  \n" +
                                            "@Activity = '+cast(@Activitycode as Varchar(1))+',  \n" +
                                            "@IsCubics = '''+@Iscubics+'''' \n" +
                                            "\n" +
                                            "exec(@SQL) \n" +
                                            "\n" +
                                            "FETCH NEXT FROM _Cursor \n" +
                                            "into @Prodmonth, @Sectionid, @Workplaceid, @Activitycode, @Iscubics; \n" +
                                            "\n" +
                                            "END \n" +
                                            "\n" +
                                            "CLOSE _Cursor; \n" +
                                            "DEALLOCATE _Cursor; ";
                    clsDataResult DataResult =  theData.ExecuteInstruction();
                    break;
            }

        }

        private void gvCycleCodes_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            // Obtain the clicked point.
            GridHitInfo hi = view.CalcHitInfo(new Point(e.X, e.Y));

            // Determine if the clicked point belongs to a card caption.
            if (!hi.InColumn && hi.RowHandle > -1 )
            {
                // Obtain the clicked card.
                string rowID = view.GetRowCellValue(hi.RowHandle, view.Columns["CycleCode"]).ToString();
                gcCycleCodes.DoDragDrop(rowID, DragDropEffects.Copy);
            }
        }



        private void gcCycle_DragOver(object sender, DragEventArgs e)
        {
            
            GridControl grid = (GridControl)sender;

            Point pt = new Point(e.X, e.Y);
            pt = grid.PointToClient(pt);
            GridView view = grid.GetViewAt(pt) as GridView;
            if (view == null) return;

            GridHitInfo hitInfo = view.CalcHitInfo(pt);
            if (hitInfo.HitTest == GridHitTest.EmptyRow)
                DropTargetRowHandle = view.DataRowCount;
            else
                DropTargetRowHandle = hitInfo.RowHandle;

            if (DropTargetRowHandle >= 0 && e.Data.GetDataPresent(typeof(string)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;   
        }

        private void gcCycle_DragDrop(object sender, DragEventArgs e)
        {
            string dragData = e.Data.GetData(DataFormats.Text).ToString();
            Point pt = gcCycle.PointToClient(new Point(e.X, e.Y));
            GridHitInfo hitInfo = ((sender as GridControl).FocusedView as GridView).CalcHitInfo(pt);
            if (hitInfo.InRowCell && hitInfo.RowHandle == row_Cycle_Code || hitInfo.RowHandle == row_Cycle_CodeCube)
            {
                string theColumn = hitInfo.Column.Tag.ToString();
                DateTime TheDate = Convert.ToDateTime(_CycleData.ResultsDataTable.Rows[row_Cycle_Date]["Day" + theColumn].ToString());
                if (hitInfo.RowHandle == row_Cycle_Code)
                {
                    if (TheDate >= TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Rundate)
                    {
                        if (_CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + theColumn].ToString() != dayOffCode)
                            _CycleData.ResultsDataTable.Rows[DropTargetRowHandle]["Day" + theColumn] = dragData;

                        if (_CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Name"].ToString() == "SQM")
                        {
                            UpdateCycleStope();
                        }
                        else
                        {

                            UpdateCycleMeters();
                        }
                    }
                }

                if (hitInfo.RowHandle == row_Cycle_CodeCube)
                {
                    if (TheDate >= TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Rundate)
                    {
                        if (_CycleData.ResultsDataTable.Rows[row_Cycle_CodeCube]["Day" + theColumn].ToString() != dayOffCode)
                            _CycleData.ResultsDataTable.Rows[DropTargetRowHandle]["Day" + theColumn] = dragData;

                        UpdateCycleCube();
                    }
                }
                
            }

            
          
           
        }

        private void gcCycle_Paint(object sender, PaintEventArgs e)
        {

        }

        private Rectangle Transform(Graphics g, int degree, Rectangle r)
        {
            g.TranslateTransform(r.Width / 2, r.Height / 2);
            g.RotateTransform(degree);
            float cos = (float)Math.Round(Math.Cos(degree * (Math.PI / 180)), 2);
            float sin = (float)Math.Round(Math.Sin(degree * (Math.PI / 180)), 2);
            Rectangle r1 = r;
            r1.X = (int)(r.X * cos + r.Y * sin);
            r1.Y = (int)(r.X * (-sin) + r.Y * cos);
            return r1;
        }

        private void gvCycle_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {

            
            if (e.RowHandle == row_Cycle_Date)
            {

                //Rectangle r = e.Graphics. Info.CaptionRect;
                
                //e.DisplayText = "";
                //e.Painter.DrawObject(e.Info);
                e.Appearance.BackColor =  Color.Gainsboro;
                System.Drawing.Drawing2D.GraphicsState state = e.Graphics.Save();
                StringFormat sf = new StringFormat();
                sf.Trimming = StringTrimming.None;
                sf.Alignment = StringAlignment.Near;
                sf.FormatFlags |= StringFormatFlags.NoWrap;
                Rectangle r = e.Bounds;
                Rectangle rect;
                rect = r;
                
                rect.X = 0;
                r.Inflate(20, 0);
                r = Transform(e.Graphics, 90, r);
                rect = r;
                rect.Inflate(0, 0);
                r.X = -30;
                r.Y = r.Y - 6;
                
                Brush backBrush = Brushes.Gainsboro;
                
         
                e.Appearance.Options.UseFont = true;
                Font f = new Font(e.Appearance.Font,FontStyle.Bold);
               
                e.Graphics.DrawString(e.DisplayText, f, e.Appearance.GetForeBrush(e.Cache), r, sf);
               // e.Graphics.FillRectangle(backBrush, rect); 
                e.Graphics.Restore(state);
                e.Handled = true;
                e.Appearance.BackColor = Color.Gainsboro;
 
            }
           

            if ((e.RowHandle == row_Cycle_Code || e.RowHandle == row_Cycle_CodeCube) && e.DisplayText == dayOffCode)
            {
                e.Appearance.BackColor = Color.Gainsboro;
            }

            if (e.RowHandle == row_Input)
            {
                e.Appearance.Options.UseFont = true;
                e.Appearance.ForeColor = Color.Transparent;
                e.Appearance.BackColor = Color.Transparent;
            }


            if ((e.RowHandle == row_Cycle_Vale || e.RowHandle == row_Cycle_ValeCube) && (e.DisplayText == "0" || e.DisplayText == "0.00" || e.DisplayText == "0.000" || e.DisplayText == "0.0000"))
            {
                e.Appearance.BackColor = Color.Gainsboro;
                e.Appearance.ForeColor = Color.Gainsboro;
            }

            if (e.RowHandle == row_Cycle_Code || e.RowHandle == row_Cycle_CodeCube && e.DisplayText != dayOffCode)
            {
                if (CanBlast(e.DisplayText))
                {

                    e.Appearance.BackColor = Color.LightGreen;
                }
                else e.Appearance.BackColor = Color.White;
            }

            //if ((e.RowHandle == row_Cycle_Date || e.RowHandle == row_Cycle_Code || e.RowHandle == row_Cycle_Vale) && e.DisplayText == "")
            //{
            //    e.Column.Visible = false;
            //    e.Column.
               

            //}
            //else { e.Column.Visible = true;  }

                 
        }

        private void gcCycleCodes_Click(object sender, EventArgs e)
        {
            
        }

        private void gvCycleCodes_Click(object sender, EventArgs e)
        {

        }

        private void gvCycleCodes_RowClick(object sender, RowClickEventArgs e)
        {
            DataRow dr = gvCycleCodes.GetFocusedDataRow();
            lblCode.Text = dr["CycleCode"].ToString();
        }

        private void gvCycle_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (theActivity == 0)
            {
                if (gvCycle.FocusedRowHandle == row_Cycle_Vale)
                {
                    string theColumn = gvCycle.FocusedColumn.Tag.ToString();
                    _CycleData.ResultsDataTable.Rows[row_Input]["Day" + theColumn] = "MAN";
                    UpdateCycleStope();
                }

                if (gvCycle.FocusedRowHandle == row_Cycle_ValeCube)
                {
                    string theColumn = gvCycle.FocusedColumn.Tag.ToString();
                    _CycleData.ResultsDataTable.Rows[row_Input]["Day" + theColumn] = "MAN";
                    UpdateCycleStope();
                }
            }

            if (theActivity == 1)
            {
                if (gvCycle.FocusedRowHandle == row_Cycle_Vale)
                {
                    string theColumn = gvCycle.FocusedColumn.Tag.ToString();
                    _CycleData.ResultsDataTable.Rows[row_Input]["Day" + theColumn] = "MAN";
                    UpdateCycleMeters();
                }

            }
        }

        private void gvCycle_ShownEditor(object sender, EventArgs e)
        {

        }

        private void gvCycle_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            if (gvCycle.FocusedRowHandle == row_Cycle_Vale || gvCycle.FocusedRowHandle == row_Cycle_ValeCube)
            {
                string theColumn = gvCycle.FocusedColumn.Tag.ToString();
                DateTime theDate = Convert.ToDateTime(_CycleData.ResultsDataTable.Rows[row_Cycle_Date]["Day" + theColumn].ToString());
                if (theDate < TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Rundate)
                {
                    e.Cancel = true;
                }
                else
                  if (_CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + theColumn].ToString() != "BL")
                      e.Cancel = true;
            }
            else
                 e.Cancel = true;

        }

        private void gvCycle_CustomRowFilter(object sender, RowFilterEventArgs e)
        {

        }

        private void gvMoCycle_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {


            if (gvMoCycle.GetRowCellValue(e.RowHandle,e.Column).ToString() == "OFF")
            {
                e.Appearance.BackColor = Color.Gainsboro;
                e.Appearance.ForeColor = Color.Gainsboro;
            }

            if (gvMoCycle.GetRowCellValue(e.RowHandle, e.Column).ToString() == "BL")
            {
                //e.Appearance.BackColor = Color.Tomato;
                e.Appearance.ForeColor = Color.Tomato;
            }

            if (gvMoCycle.GetRowCellValue(e.RowHandle, e.Column).ToString() == "SR")
            {
                //e.Appearance.BackColor = Color.Tomato;
                e.Appearance.ForeColor = Color.Tomato;
            }

            if (gvMoCycle.GetRowCellValue(e.RowHandle, e.Column).ToString() == "EX")
            {
                e.Column.Visible = false;
            }

            }
            catch (Exception)
            {

                
            }
        }

        private void gcMoCycle_DragDrop(object sender, DragEventArgs e)
        {
            Point p = this.gcMoCycle.PointToClient(new Point(e.X, e.Y));
            int row = gvMoCycle.CalcHitInfo(p.X, p.Y).RowHandle;
            if (row > -1)
            {
                //string test = gvMoCycle.GetRowCellValue(row, gvMoCycle.FocusedColumn).ToString();detail

                if ( gvMoCycle.GetRowCellValue(row, gvMoCycle.CalcHitInfo(p.X, p.Y).Column.FieldName).ToString() == "OFF" || gvMoCycle.CalcHitInfo(p.X, p.Y).Column.FieldName.ToString() == "detail")
                {
                    return;
                }

                if (gvMoCycle.CalcHitInfo(p.X, p.Y).Column.FieldName != null && gvMoCycle.CalcHitInfo(p.X, p.Y).RowHandle == 2)
                {
                    this.gvMoCycle.SetRowCellValue(row, gvMoCycle.CalcHitInfo(p.X, p.Y).Column.FieldName, lblCode.Text);
                    LoadMOSqm();
                }
            }
        }

        private void gcMoCycle_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gcMoCycle_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void gcCycleCodes_MouseDown(object sender, MouseEventArgs e)
        {
            lblCode.Text = gvCycleCodes.GetRowCellValue(gvCycleCodes.FocusedRowHandle, gvCycleCodes.Columns[0]).ToString();
        }

        private void lbxCodeCycles_MouseDown(object sender, MouseEventArgs e)
        {

            if (lbxCodeCycles.Items.Count == 0)
                return;
            int index = lbxCodeCycles.IndexFromPoint(e.X, e.Y);
            if (index > -1)
            {
                string s = lbxCodeCycles.Items[index].ToString();
                lblCode.Text = ExtractBeforeColon(lbxCodeCycles.Items[index].ToString());
                DragDropEffects dde1 = DoDragDrop(s,
                    DragDropEffects.All);
            }
        }

        public string ExtractBeforeColon(string TheString)
        {
            if (TheString != "")
            {
                string BeforeColon;
                int index = TheString.IndexOf(":");
                BeforeColon = TheString.Substring(0, index);
                return BeforeColon;
            }
            else
            {
                return "";
            }
        }

        string Approved = "";

        private void gvMoCycle_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //gvMoCycle.CalcHitInfo(p.X, p.Y).RowHandle == 2

            string test = gvMoCycle.GetRowCellValue(e.RowHandle, gvMoCycle.FocusedColumn).ToString();

            if (gvMoCycle.FocusedColumn.FieldName.ToString() == "detail" || gvMoCycle.GetRowCellValue(e.RowHandle, gvMoCycle.FocusedColumn).ToString() == "OFF" )
            {
                return;
            }


            if (Convert.ToDateTime(e.Column.Caption) < Convert.ToDateTime(System.DateTime.Now))
            {
                if (UserCurrentInfo.UserID == "Kelvin")
                {

                }
                else
                {
                    MessageBox.Show("Error , Cycle cant be changed for past calendar days.");
                    return;
                }
            }

            else if (lblCode.Text != "None" && lblCode.Text != "Code" && e.RowHandle == 2)
            {
                gvMoCycle.SetRowCellValue(e.RowHandle, gvMoCycle.FocusedColumn, lblCode.Text);
                LoadMOSqm();
            }
        }

        private void TotlSqmLbl_TextChanged(object sender, EventArgs e)
        {
            if (theActivity == 0)
            {
                if (TotlSqmLbl.Text != "0")
                {
                    viewPlanningStoping.SetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["callValue"], TotlSqmLbl.Text);
                }                
            }

            if (theActivity == 1)
            {
                if (TotlSqmLbl.Text != "0")
                {
                    viewPlanningDevelopment.SetRowCellValue(viewPlanningDevelopment.FocusedRowHandle, viewPlanningDevelopment.Columns["Metresadvance"], TotlSqmLbl.Text);
                }                
            }
        }

        private void viewPlanningDevelopment_RowCellClick_1(object sender, RowCellClickEventArgs e)
        {
            string test = e.Column.FieldName;
        }

        private void btnUpdatePlanning_Click(object sender, EventArgs e)
        {
            //aa

            int currentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);

            if (theActivity == 1)
                currentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);

            if (theActivity!= '1')
            {
                decimal TotMonthcall = Convert.ToInt32(viewPlanningDevelopment.GetRowCellValue(currentRow, viewPlanningDevelopment.Columns["MonthlyTotalSQM"]));
                decimal ReefMonthcall = Convert.ToInt32(viewPlanningDevelopment.GetRowCellValue(currentRow, viewPlanningDevelopment.Columns["MonthlyReefSQM"]));

                decimal ratio = 0;

                if (ReefMonthcall != 0)
                {
                    ratio = TotMonthcall / ReefMonthcall;
                }

                int TotActCall = Convert.ToInt32(viewPlanningDevelopment.GetRowCellValue(currentRow, viewPlanningDevelopment.Columns["Metresadvance"]));
                int ReefCall = 0;
                int WasteCall = 0;

                if (ratio != 0)
                {
                    ReefCall = Convert.ToInt32(Convert.ToDecimal(TotActCall) / ratio);
                }
            }

            

            int ShiftDay = 0;
            int subPos = 0;
            int Count = 0;
            string SaveMoCycle = "";
            string saveMoSQM = "";

            string WP = tblPrePlanData.Rows[currentRow]["Workplaceid"].ToString();
            ////Get Density
            MWDataManager.clsDataAccess _dbManDens = new MWDataManager.clsDataAccess();
            _dbManDens.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManDens.SqlStatement = "select isnull(Density,2.72)Density from workplace w  where workplaceid = '" + WP + "'  ";

            _dbManDens.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDens.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDens.ExecuteInstruction();

            decimal density = Convert.ToDecimal(_dbManDens.ResultsDataTable.Rows[0][0].ToString());
            //int Count = 0;

            decimal SW = 0;
            decimal tons = 0;
            decimal MoCycleGT = 0;
            decimal CW = 0;
            decimal CMGT = 0;
            string FL = "0";
            decimal TotalTOns = 0;
            decimal MetresAdv = 0;

            if (theActivity == 0)
            {
                FL = tblPrePlanData.Rows[currentRow]["FL"].ToString();
            }

            if (theActivity == 1)
            {
                MWDataManager.clsDataAccess _dbManLoadEndtype = new MWDataManager.clsDataAccess();
                _dbManLoadEndtype.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLoadEndtype.SqlStatement = "select endtypeid from workplace w  where workplaceid = '" + WP + "'  ";

                _dbManLoadEndtype.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLoadEndtype.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLoadEndtype.ExecuteInstruction();

                if (_dbManLoadEndtype.ResultsDataTable.Rows.Count > 0)
                {
                    FL = _dbManLoadEndtype.ResultsDataTable.Rows[0][0].ToString();
                }
                else
                {
                    FL = "0";
                }
            }

            if (theActivity == 0)
                SW = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["SW"].ToString());
            if (theActivity == 0)
                CW = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["CW"].ToString());

            CMGT = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["CMGT"].ToString());
            //}

            if (Convert.ToDecimal(FL) > 0)
            {
                MetresAdv = Convert.ToDecimal(TotlSqmLbl.Text) / Convert.ToDecimal(FL);
            }



            if (SW != 0)
            {
                MoCycleGT = Convert.ToDecimal(CMGT) / Convert.ToDecimal(SW);
            }
            else
            {
                MoCycleGT = CMGT;
            }

            if (theActivity == 1)
            {
                string tes = tblPrePlanData.Rows[currentRow]["GramPerTon"].ToString();

                if (tblPrePlanData.Rows[currentRow]["GramPerTon"].ToString() != "")
                {
                    MoCycleGT = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["GramPerTon"].ToString());
                }
            }

            for (int i = 2; i < 49; i++)
            {
                SaveMoCycle = SaveMoCycle + (gvMoCycle.GetRowCellValue(2, gvMoCycle.Columns[i].FieldName).ToString() + "       ").Substring(0, 4);

                if (gvMoCycle.GetRowCellValue(3, gvMoCycle.Columns[i].FieldName).ToString() == "OFF")
                {
                    saveMoSQM = saveMoSQM + ("       ").Substring(0, 4);
                }
                else
                {
                    saveMoSQM = saveMoSQM + (gvMoCycle.GetRowCellValue(3, gvMoCycle.Columns[i].FieldName).ToString() + "       ").Substring(0, 4);
                }


                Count = Count + 1;
                //}
            }


            MWDataManager.clsDataAccess _dbManPlanningSTP = new MWDataManager.clsDataAccess();
            _dbManPlanningSTP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            for (int i = 0; i < Count; i++)
            {
                string wday = "Y";

                string sqmds = saveMoSQM.Substring(subPos, 4).TrimEnd();
                if (SaveMoCycle.Substring(subPos, 4).TrimEnd() == "OFF")
                {
                    wday = "N";
                    sqmds = "0";
                }

                if (SaveMoCycle.Substring(subPos, 4).TrimEnd() == "EX")
                {
                    break;
                }

                if (sqmds == "" || sqmds == "EX")
                    sqmds = "0";

                tons = Convert.ToDecimal(sqmds) * Convert.ToDecimal(SW) / 100 * density;

                TotalTOns = TotalTOns + tons;

                if (theActivity == 1)
                {
                    // Tons = adv * endwith * endhight * density
                    // Tons = adv * endwith * endhight * density

                    decimal wasteFact = 0;

                    if (Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["WasteAdv"].ToString()) > 0)
                        wasteFact = Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["WasteAdv"].ToString()) / Convert.ToDecimal(tblPrePlanData.Rows[currentRow]["Metresadvance"].ToString());

                    decimal wasteadv = Convert.ToDecimal(sqmds) * wasteFact;
                    decimal reefadv = Convert.ToDecimal(sqmds) - wasteadv;                    

                    _dbManPlanningSTP.SqlStatement = _dbManPlanningSTP.SqlStatement +

                       "  update PLANNING set   \r\n" +
                       "  Sqm = '0' , Tons = '" + tons + "', FL = '0', SW = '" + SW + "', CW = '" + CW + "' , CMGT = '" + CMGT + "' , GT = '" + MoCycleGT + "',  \r\n" +
                       "  MOCycle = '" + SaveMoCycle.Substring(subPos, 4).TrimEnd() + "' ,CubicMetres = '0.000',Metresadvance = '" + sqmds + "', reefadv = '" + reefadv + "' , wasteadv = '" + wasteadv + "' \r\n" +
                       "  Where   \r\n" +
                       "  Prodmonth = " + theProdMonth + "  and  WorkplaceID = '" + WP + "' and Activity = '" + theActivity + "'  \r\n" +
                       "  and CalendarDate = '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value.AddDays(i)) + "' and  SectionID = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' and PlanCode = 'MP'    \r\n\r\n\r\n";

                    
                    _dbManPlanningSTP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManPlanningSTP.queryReturnType = MWDataManager.ReturnType.DataTable;


                }
                else
                {

                    _dbManPlanningSTP.SqlStatement = _dbManPlanningSTP.SqlStatement +

                       "  update PLANNING set   \r\n" +
                       "  Sqm = '" + sqmds + "' , Tons = '" + tons + "', FL = '" + FL + "', SW = '" + SW + "', CW = '" + CW + "' , CMGT = '" + CMGT + "' , GT = '" + MoCycleGT + "',  \r\n" +
                       "  MOCycle = '" + SaveMoCycle.Substring(subPos, 4).TrimEnd() + "' ,CubicMetres = '0.000',Metresadvance = '" + MetresAdv + "' \r\n" +
                       "  Where   \r\n" +
                       "  Prodmonth = " + theProdMonth + "  and  WorkplaceID = '" + WP + "' and Activity = '" + theActivity + "'  \r\n" +
                       "  and CalendarDate = '" + String.Format("{0:yyyy-MM-dd}", SaveDate.Value.AddDays(i)) + "' and  SectionID = '" + tblPrePlanData.Rows[currentRow]["SectionID"].ToString() + "' and PlanCode = 'MP'    \r\n\r\n\r\n";
                    _dbManPlanningSTP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManPlanningSTP.queryReturnType = MWDataManager.ReturnType.DataTable;
                }




                ShiftDay = ShiftDay + 1;

                if (wday == "N")
                {
                    ShiftDay = ShiftDay - 1;
                }

                subPos = subPos + 4;
            }

            string aa = "";
            _dbManPlanningSTP.ExecuteInstruction();
        }

        private void MainGrid_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                return;
                //e.Handled = true;
            }
        }

        private void viewPlanningDevelopment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                return;
                //e.Handled = true;
            }
        }

        private void viewPlanningStoping_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                return;
                //e.Handled = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gcMoCycle_Click(object sender, EventArgs e)
        {

        }

        private void gvMoCycle_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            CycleChanged = true;
        }

        private void viewPlanningSweepVamp_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
           
        }

        private void viewPlanningDevelopment_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
           
        }

        private void viewPlanningStoping_MouseDown(object sender, MouseEventArgs e)
        {
            CheckCycle();
        }

        private void CheckCycle()
        {
            if (CycleChanged)
            {
                MessageResult myResult = MessageItem.viewMessage(MessageType.Info,
                                                        "Save",
                                                        "A changed in the cycle planning was detected. Would you like to save your changes?",
                                                        ButtonTypes.YesNo,
                                                        MessageDisplayType.FullScreen);
                if (myResult == MessageResult.Yes)
                {
                    SaveMOCycle();
                }

            }

            CycleChanged = false;
        }

        private void viewPlanningStoping_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            showCycle(_cycleActive);
        }

        private void UpdateCycleStope()
        {
            int countBlasts = 0;
            double sqm = Convert.ToDouble(_CycleData.ResultsDataTable.Rows[row_Cycle_Code]["SQM"].ToString());
            double Mansqm = 0;
            for(int i = 1; i < 51; i++)
            {              
                if (CanBlast(_CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + i.ToString()].ToString()) && _CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString() == "CAL")
                    countBlasts++;

                if (_CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString() == "MAN")
                {
                    Mansqm = Mansqm + Convert.ToDouble(_CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()]);
                }

            }

            if (sqm-Mansqm <= 0)
            {
                sqm = 0;
            }
            else
            {
                sqm = sqm - Mansqm;
            }

            int dayCall = Convert.ToInt32(sqm) / Convert.ToInt32(countBlasts);
            int rem = Convert.ToInt32(sqm) - (dayCall * countBlasts);
            int addRem = 1;
            for (int i = 1; i < 51; i++)
            {
                if (_CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString() == "CAL")
                {
                    if (CanBlast(_CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + i.ToString()].ToString()))
                    {
                        if (addRem <= rem)
                            _CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()] = (dayCall + 1).ToString("0");
                        else
                            _CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()] = dayCall.ToString("0");

                        addRem++;
                    }
                    else
                        _CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()] = "0";
                }
            }

            for (int i = 1; i < 51; i++)
            {
                if (_CycleData.ResultsDataTable.Rows[row_Cycle_Date]["Day" + i.ToString()].ToString() != "")
                {
                    int stpcurrentRow = viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
                    string Prodmonth = tblPrePlanData.Rows[stpcurrentRow]["Prodmonth"].ToString();
                    string CycleWP = tblPrePlanData.Rows[stpcurrentRow]["Workplaceid"].ToString();
                    string CycleSectionID = tblPrePlanData.Rows[stpcurrentRow]["SectionID"].ToString();
                    string FL = tblPrePlanData.Rows[stpcurrentRow]["FL"].ToString();
                    string SW = tblPrePlanData.Rows[stpcurrentRow]["SW"].ToString();
                    string CW = tblPrePlanData.Rows[stpcurrentRow]["CW"].ToString();
                    string CMGT = tblPrePlanData.Rows[stpcurrentRow]["CMGT"].ToString();
                    string ReefWaste = tblPrePlanData.Rows[stpcurrentRow]["RockType"].ToString();
                    string dayValue = _CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()].ToString();
                    string dayDate = _CycleData.ResultsDataTable.Rows[row_Cycle_Date]["Day" + i.ToString()].ToString();
                    string dayCycle = _CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + i.ToString()].ToString();
                    string dayInput = _CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString();

                    string reefSQM = "0";
                    string WasteSQM = "0";

                    if (ReefWaste == "ON")
                    {
                        reefSQM = dayValue;
                        WasteSQM = "0";
                    }
                    else
                    {
                        reefSQM = "0";
                        WasteSQM = dayValue;
                    }

                    //update the planning record

                    clsDataAccess theData = new clsDataAccess();
                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                    theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, TUserInfo.Connection);

                    theData.SqlStatement = "Update planning set \n" +
                                            "SQM = " + dayValue + ",\n" +
                                            "ReefSQM = " + reefSQM + ",\n" +
                                            "WasteSQM = " + WasteSQM + ",\n" +
                                            "FL = " + FL + ",\n" +
                                            "SW = " + SW + ",\n" +
                                            "CW = " + CW + ",\n" +
                                            "CMGT = " + CMGT + ",\n" +
                                            "CycleInput = '" + dayInput + "',\n" +
                                            "MOCycle = '" + dayCycle + "'\n" +
                                            "Where Prodmonth = " + Prodmonth + "\n" +
                                            "and Sectionid = '" + CycleSectionID + "'\n" +
                                            "and Workplaceid = '" + CycleWP + "'\n" +
                                            "and Calendardate = '" + dayDate + "'\n" +
                                            "and Plancode = 'MP'";
                    clsDataResult dataresult = theData.ExecuteInstruction();
                    Application.DoEvents();
                }
            }

        }

        private void UpdateCycleMeters()
        {
            int countBlasts = 0;
            double meters = Convert.ToDouble(_CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Metresadvance"].ToString());
            double Manm = 0;
            for (int i = 1; i < 51; i++)
            {
                if (CanBlast(_CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + i.ToString()].ToString()) && _CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString() == "CAL")
                    countBlasts++;

                if (_CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString() == "MAN")
                {
                    Manm = Manm + Convert.ToDouble(_CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()]);
                }
            }

            if (meters - Manm <= 0)
            {
                meters = 0;
            }
            else
            {
                meters = meters - Manm;
            }

            double dayCall = Math.Round((Math.Floor((Math.Round(meters / countBlasts, 3, MidpointRounding.AwayFromZero) * 10)) / 10), 1);
            double rem = Math.Round(meters - (dayCall * countBlasts), 1);
            int addRem = 1;
            for (int i = 1; i < 51; i++)
            {
                if (_CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString() == "CAL")
                {
                    if (CanBlast(_CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + i.ToString()].ToString()))
                    {
                        if (addRem <= rem)
                            _CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()] = Convert.ToDouble(dayCall + 1) + .1;
                        else
                            _CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()] = dayCall.ToString();

                        addRem++;
                    }
                    else
                        _CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()] = "0";
                }
            }

            for (int i = 1; i < 51; i++)
            {
                if (_CycleData.ResultsDataTable.Rows[row_Cycle_Date]["Day" + i.ToString()].ToString() != "")
                {
                    int DevcurrentRow = viewPlanningDevelopment.GetDataSourceRowIndex(viewPlanningDevelopment.FocusedRowHandle);
                    string Prodmonth = tblPrePlanData.Rows[DevcurrentRow]["Prodmonth"].ToString();
                    string CycleWP = tblPrePlanData.Rows[DevcurrentRow]["Workplaceid"].ToString();
                    string CycleSectionID = tblPrePlanData.Rows[DevcurrentRow]["SectionID"].ToString();
                    //string EndWith = tblPrePlanData.Rows[DevcurrentRow]["ENDWIDTH"].ToString();
                    //string EndHeight = tblPrePlanData.Rows[DevcurrentRow]["ENDHEIGHT"].ToString();
                    //string GT = tblPrePlanData.Rows[DevcurrentRow]["GT"].ToString();
                    //string CMGT = tblPrePlanData.Rows[DevcurrentRow]["CMGT"].ToString();
                    string ReefWaste = tblPrePlanData.Rows[DevcurrentRow]["RockType"].ToString();
                    string dayValue = _CycleData.ResultsDataTable.Rows[row_Cycle_Vale]["Day" + i.ToString()].ToString();
                    string dayDate = _CycleData.ResultsDataTable.Rows[row_Cycle_Date]["Day" + i.ToString()].ToString();
                    string dayCycle = _CycleData.ResultsDataTable.Rows[row_Cycle_Code]["Day" + i.ToString()].ToString();
                    string dayInput = _CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString();

                    string reefM = "0";
                    string WasteM = "0";

                    if (ReefWaste == "ON")
                    {
                        reefM = dayValue;
                        WasteM = "0";
                    }
                    else
                    {
                        reefM = "0";
                        WasteM = dayValue;
                    }

                    //update the planning record

                    clsDataAccess theData = new clsDataAccess();
                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                    theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, TUserInfo.Connection);

                    theData.SqlStatement = "Update planning set \n" +
                                            "MetresAdvance = " + dayValue + ",\n" +
                                            "ReefAdv = " + reefM + ",\n" +
                                            "WasteAdv = " + WasteM + ",\n" +
                                            "CycleInput = '" + dayInput + "',\n" +
                                            "MOCycle = '" + dayCycle + "'\n" +
                                            "Where Prodmonth = " + Prodmonth + "\n" +
                                            "and Sectionid = '" + CycleSectionID + "'\n" +
                                            "and Workplaceid = '" + CycleWP + "'\n" +
                                            "and Calendardate = '" + dayDate + "'\n" +
                                            "and Plancode = 'MP'";
                    clsDataResult dataresult = theData.ExecuteInstruction();
                    Application.DoEvents();
                }
            }
        }
        private void UpdateCycleCube()
        {
            int countBlasts = 0;
            double sqm = Convert.ToDouble(_CycleData.ResultsDataTable.Rows[row_Cycle_CodeCube]["Cubes"].ToString());
            double Mansqm = 0;
            for (int i = 1; i < 51; i++)
            {
                if (CanBlast(_CycleData.ResultsDataTable.Rows[row_Cycle_CodeCube]["Day" + i.ToString()].ToString()) && _CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString() == "CAL")
                    countBlasts++;

                if (_CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString() == "MAN")
                {
                    Mansqm = Mansqm + Convert.ToDouble(_CycleData.ResultsDataTable.Rows[row_Cycle_ValeCube]["Day" + i.ToString()]);
                }
            }

            int dayCall = Convert.ToInt32(sqm) / Convert.ToInt32(countBlasts);
            int rem = Convert.ToInt32(sqm) - (dayCall * countBlasts);
            int addRem = 1;
            for (int i = 1; i < 51; i++)
            {
                if (_CycleData.ResultsDataTable.Rows[row_Input]["Day" + i.ToString()].ToString() == "CAL")
                {
                    if (CanBlast(_CycleData.ResultsDataTable.Rows[row_Cycle_CodeCube]["Day" + i.ToString()].ToString()))
                    {
                        if (addRem <= rem)
                            _CycleData.ResultsDataTable.Rows[row_Cycle_ValeCube]["Day" + i.ToString()] = (dayCall + 1).ToString();
                        else
                            _CycleData.ResultsDataTable.Rows[row_Cycle_ValeCube]["Day" + i.ToString()] = dayCall.ToString();

                        addRem++;
                    }
                    else
                        _CycleData.ResultsDataTable.Rows[row_Cycle_ValeCube]["Day" + i.ToString()] = "0";
                }
            }
        }


        private bool CanBlast(string cycleCode)
        {
            bool theResult = false;

            DataRow[] theData = _CycleCodes.ResultsDataTable.Select("CycleCode = '" + cycleCode + "'");

            foreach (DataRow dr in theData)
            {
                theResult = Convert.ToBoolean(dr["CanBlast"].ToString());
            }

            return theResult;
        }

 

        private void gvCycle_CalcRowHeight(object sender, RowHeightEventArgs e)
        {
            if (e.RowHandle == row_Cycle_Date)
                e.RowHeight = 80;
        }


    }

    public class CycleCodeID
    {
        public string CycleCode { get; set; }
        public string Description { get; set; }
    }


    
   
}
