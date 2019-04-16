using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using FastReport;
using System.Threading;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
//using DevExpress.XtraGrid.Columns;
using Mineware.Systems.Reports.MeasuringListReport;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.PlathondWallChartReport
{
    public partial class PlathondWallChartReportUserControl : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        public string checking;
        private PlathondWallChartReportSettingsPropertiesClass reportSettings = new PlathondWallChartReportSettingsPropertiesClass();
        Report theReport = new Report();
        Procedures procs = new Procedures();
        private string _theConnection;

        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }

        public PlathondWallChartReportUserControl()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        private void PlathondWallChartReportUserControl_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());

            iProdMonth.Properties.Value = reportSettings.Prodmonth;
            LoadSections();
            reportSettings.Activity = "Stoping";
            pgMeasuringList.SelectedObject = reportSettings;
        }

        public void LoadSections()
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;

            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

            DataTable dtSectionData = new DataTable();

            if (BMEBL.GetPlanSectionsAndNameADO(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)) == true)
            {
                dtSectionData = BMEBL.ResultsDataTable;
                riSection.DataSource = BMEBL.ResultsDataTable;
                riSection.DisplayMember = "NAME";
                riSection.ValueMember = "sectionid";
            }
            reportSettings.NAME = dtSectionData.Rows[0]["sectionid"].ToString();
        }
        public override bool prepareReport()
        {
            bool theResult = false;
            theReportThread = new Thread(new ParameterizedThreadStart(createReport));
            theReportThread.SetApartmentState(ApartmentState.STA);
            theReportThread.Start(reportSettings);
            theResult = true;
            return theResult;
        }

        private void createReport(Object theReportSettings)
        {
            //string _act = "";

            //if (reportSettings.Activity == "Stoping")
            //{
            //    _act = "0";
            //}

            //else if (reportSettings.Activity == "Development")
            //{
            //    _act = "1";
            //}
            //MWDataManager.clsDataAccess _plathondWallRoomReportData = new MWDataManager.clsDataAccess();

            //_plathondWallRoomReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_plathondWallRoomReportData.SqlStatement = "sp_PlathondWallRoom";
            //_plathondWallRoomReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //_plathondWallRoomReportData.ResultsTableName = "PlathondWallRoomData";

            //SqlParameter[] _DevparamCollection =
            //    {
            //                _plathondWallRoomReportData.CreateParameter("@Prodmonth", SqlDbType.VarChar, 6, TProductionGlobal.ProdMonthAsString ( reportSettings.Prodmonth)),
            //                _plathondWallRoomReportData.CreateParameter("@Section", SqlDbType.VarChar, 50, reportSettings.NAME),
            //                _plathondWallRoomReportData.CreateParameter("@Activity", SqlDbType.VarChar, 1, _act),
            //        };

            //_plathondWallRoomReportData.ParamCollection = _DevparamCollection;
            //_plathondWallRoomReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //clsDataResult result = _plathondWallRoomReportData.ExecuteInstruction();

            //if (!result.success)
            //{
            //    throw new ApplicationException("Report Section:MODailyStopeData:" + result.Message);
            //}

            //DataSet repDataSet = new DataSet();
            //repDataSet.Tables.Add(_plathondWallRoomReportData.ResultsDataTable);
            //theReport.RegisterData(repDataSet);

            //theReport.Load(TGlobalItems.ReportsFolder + "\\PlathondWallRoom.frx");

            //theReport.SetParameterValue("Banner", TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Mine);
            //theReport.SetParameterValue("Prodmonth", TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
            //theReport.SetParameterValue("SectionName", reportSettings.NAME);
            //theReport.SetParameterValue("TheDate", reportSettings.Activity);
            //theReport.SetParameterValue("logo", TGlobalItems.ClientLogo);
            //theReport.SetParameterValue("logoMineware", TGlobalItems.CompanyLogo);

            //if (TParameters.DesignReport)
            //{
            //    theReport.Design();
            //}

            //theReport.Prepare();
            //ActiveReport.SetReport = theReport;
            //ActiveReport.isDone = true;
            try
            {
WarSheetReport();
            }
            catch
            {
                Exception ex;
            }
            

        }


        public void WarSheetReport()
        {
            WarGrid.Visible = false;
            WarGrid.ColumnCount = 1;
            WarGrid.RowCount = 200;
            WarGrid.ColumnCount = 51;
            WarGrid.AllowUserToResizeColumns = true;

            WarGrid.Columns[0].Width = 100;
            WarGrid.Columns[0].HeaderText = "Section";
            WarGrid.Columns[0].ReadOnly = true;
            WarGrid.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            WarGrid.Columns[0].Frozen = true;

            WarGrid.Columns[1].Width = 80;
            WarGrid.Columns[1].HeaderText = "Orgunit";
            WarGrid.Columns[1].ReadOnly = true;
            WarGrid.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            WarGrid.Columns[1].Frozen = true;

            WarGrid.Columns[2].Width = 180;
            WarGrid.Columns[2].HeaderText = "Workplace";
            WarGrid.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            WarGrid.Columns[2].ReadOnly = true;
            WarGrid.Columns[2].Frozen = true;

            WarGrid.Columns[2].Width = 180;
            WarGrid.Columns[2].HeaderText = "FL";
            WarGrid.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            WarGrid.Columns[2].ReadOnly = true;
            WarGrid.Columns[2].Frozen = true;

            int count = 1;
            for (int x = 4; x <= 44; x++)
            {
                WarGrid.Columns[x].Width = 180;
                //WarGrid.Columns[x].HeaderText = "col" + count;
                WarGrid.Columns[x].SortMode = DataGridViewColumnSortMode.NotSortable;
                WarGrid.Columns[x].ReadOnly = true;
                count++;
            }

            WarGrid.Visible = false;

            MWDataManager.clsDataAccess _dbManLosses = new MWDataManager.clsDataAccess();
            _dbManLosses.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManLosses.SqlStatement = "select row_number() over (oRDER BY calendardate) as ShiftDay, Max(calendardate) calendardate, MAX(WorkingDay) WorkingDay   from vw_Planning p, Section s, Section s1, Section s2 where " +
                                        " p.Prodmonth = s.Prodmonth and p.Sectionid = s.SectionID and " +
                                        "s.Prodmonth = s1.Prodmonth and s.reporttoSectionid = s1.SectionID and " +
                                        "s1.Prodmonth = s2.Prodmonth and s1.reporttoSectionid = s2.SectionID and " +
                                        "p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and s2.SectionID  = '" + reportSettings.NAME + "'and p.workingday = 'Y' " +
                                        "group by CalendarDate " +
                                        "order by calendardate ";
            _dbManLosses.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManLosses.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbManLosses.ResultsTableName = "Days";  //get table name
            _dbManLosses.ExecuteInstruction();

            DataTable dt = _dbManLosses.ResultsDataTable;
            int col = 4;
            int row = 0;
            int countShift = 0;
            foreach (DataRow dr in dt.Rows)
            {

                WarGrid.Columns[col].HeaderText = Convert.ToDateTime(dr["calendardate"].ToString()).ToString("dd MMM ddd yyyy");
                if (WarGrid.Columns[col].HeaderText != "")
                {
                    countShift++;
                }
                WarGrid.Rows[row].Cells[col].Value = dr["ShiftDay"].ToString();
                WarGrid.Rows[row + 1].Cells[col].Value = dr["WorkingDay"].ToString();

                col++;
                //row++;
            }
            WarGrid.Columns[3].HeaderText = countShift.ToString();

            MWDataManager.clsDataAccess _dbManCheck = new MWDataManager.clsDataAccess();
            _dbManCheck.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManCheck.SqlStatement = "select CheckMeas from SysSet ";
            _dbManCheck.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManCheck.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _dbManLosses.ResultsTableName = "Days";  //get table name
            _dbManCheck.ExecuteInstruction();

            DataTable dtCheck = _dbManCheck.ResultsDataTable;
            WarGrid.Columns[2].HeaderText = dtCheck.Rows[0]["CheckMeas"].ToString();




        

            MWDataManager.clsDataAccess _dbManStopingHeading = new MWDataManager.clsDataAccess();
            _dbManStopingHeading.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManStopingHeading.SqlStatement = "select '" + clsUserInfo.UserID.ToString() + "' userid,'" + WarGrid.Columns[0].HeaderText.ToString() + "' section,  '" + WarGrid.Columns[1].HeaderText.ToString() + "' orgunit ," +
             " '" + WarGrid.Columns[3].HeaderText.ToString() + "' fl,'" + WarGrid.Columns[4].HeaderText.ToString() + "' col1 , " +
             "'" + WarGrid.Columns[5].HeaderText.ToString() + "' col2, '" + WarGrid.Columns[6].HeaderText.ToString() + "' col3,'" + WarGrid.Columns[7].HeaderText.ToString() + "' col4, " +
             "'" + WarGrid.Columns[8].HeaderText.ToString() + "' col5, '" + WarGrid.Columns[9].HeaderText.ToString() + "' col6,'" + WarGrid.Columns[10].HeaderText.ToString() + "' col7, " +
             "'" + WarGrid.Columns[11].HeaderText.ToString() + "' col8, '" + WarGrid.Columns[12].HeaderText.ToString() + "' col9,'" + WarGrid.Columns[13].HeaderText.ToString() + "' col10 , " +
             "'" + WarGrid.Columns[14].HeaderText.ToString() + "' col11, '" + WarGrid.Columns[15].HeaderText.ToString() + "' col12,'" + WarGrid.Columns[16].HeaderText.ToString() + "' col13, " +
             "'" + WarGrid.Columns[17].HeaderText.ToString() + "' col14, '" + WarGrid.Columns[18].HeaderText.ToString() + "' col15,'" + WarGrid.Columns[19].HeaderText.ToString() + "' col16, " +
             "'" + WarGrid.Columns[20].HeaderText.ToString() + "' col17, '" + WarGrid.Columns[21].HeaderText.ToString() + "' col18,'" + WarGrid.Columns[22].HeaderText.ToString() + "' col19, " +
             "'" + WarGrid.Columns[23].HeaderText.ToString() + "' col20, '" + WarGrid.Columns[24].HeaderText.ToString() + "' col21,'" + WarGrid.Columns[25].HeaderText.ToString() + "' col22, " +
             "'" + WarGrid.Columns[26].HeaderText.ToString() + "' col23, '" + WarGrid.Columns[27].HeaderText.ToString() + "' col24,'" + WarGrid.Columns[28].HeaderText.ToString() + "' col25, " +
             "'" + WarGrid.Columns[29].HeaderText.ToString() + "' col26, '" + WarGrid.Columns[30].HeaderText.ToString() + "' col27,'" + WarGrid.Columns[31].HeaderText.ToString() + "' col28, " +
             "'" + WarGrid.Columns[32].HeaderText.ToString() + "' col29, '" + WarGrid.Columns[33].HeaderText.ToString() + "' col30,'" + WarGrid.Columns[34].HeaderText.ToString() + "' col31, " +
             "'" + WarGrid.Columns[35].HeaderText.ToString() + "' col32, '" + WarGrid.Columns[36].HeaderText.ToString() + "' col33,'" + WarGrid.Columns[37].HeaderText.ToString() + "' col34, " +
             "'" + WarGrid.Columns[38].HeaderText.ToString() + "' col35, '" + WarGrid.Columns[39].HeaderText.ToString() + "' col36,'" + WarGrid.Columns[40].HeaderText.ToString() + "' col37, " +
             "'" + WarGrid.Columns[41].HeaderText.ToString() + "' col38,'" + WarGrid.Columns[42].HeaderText.ToString() + "' col39,'" + WarGrid.Columns[43].HeaderText.ToString() + "' col40,'" + WarGrid.Columns[44].HeaderText.ToString() + "' ProgPlan, '" + WarGrid.Columns[45].HeaderText.ToString() + "' ProgBook, " +
             "'" + WarGrid.Columns[47].HeaderText.ToString() + "' MonthCall, '" + WarGrid.Columns[48].HeaderText.ToString() + "' MonthFC,'" + WarGrid.Columns[49].HeaderText.ToString() + "' Spare, " +
             "'" + WarGrid.Columns[46].HeaderText.ToString() + "' Spare1,'" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' Color,'" + reportSettings.NAME + "' Workplace,'" + WarGrid.Columns[2].HeaderText.ToString() + "' RiskRating ";

            //////////////////////////////////////////////////////////////////////////////////////////
            //MWDataManager.clsDataAccess _dbManStopingHeading = new MWDataManager.clsDataAccess();
            //_dbManStopingHeading.ConnectionString = ConfigurationSettings.AppSettings["SQLConnectionStr"];
            //_dbManStopingHeading.SqlStatement = "select   " +
            //                    " * from Temp_MoDailyHeading where userid = '" + clsUserInfo.UserID + "' ";
            _dbManStopingHeading.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManStopingHeading.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManStopingHeading.ResultsTableName = "MODaily_Stoping_Headings";
            _dbManStopingHeading.ExecuteInstruction();

            DataSet ReportDatasetHeadings = new DataSet();
            ReportDatasetHeadings.Tables.Add(_dbManStopingHeading.ResultsDataTable);
            theReport.RegisterData(ReportDatasetHeadings);





            MWDataManager.clsDataAccess _dbManData = new MWDataManager.clsDataAccess();
            _dbManData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManData.SqlStatement = "select '" + SysSettings.Banner + "' banner, *,  fl1/(totblast+0.001) fl from ( \r\n" +
                                        "select 0 PlanPrimM,p.SqmTotal,p.Activity, case when p.activity <> 1 then convert(decimal(7,0), p.SQM) else convert(decimal(7,1), p.Adv) end as fl1,p.prodmonth,s2.SectionID moid, s2.Name monmae, s1.SectionID sbid, s1.Name sbname,s.SectionID minerID,s.Name MinerName, cr.CrewName OrgUnitDS,  \r\n" +
                                        "p.workplaceid, w.Description wpname,  \r\n" +

                                        "case when endtypeid IN(9,15) then 'C1'  \r\n" +
                                        "when endtypeid not IN(9,15) then 'B1'  \r\n" +
                                        "when p.Activity  <> 1 then 'A1' end as order1,   replace(isnull(p.MOCycle,' '),'OFF ','') MOCycle, p.MOCycleNum  \r\n" +
                                        "from vw_PlanMonth p,Workplace w, Section s, Section s1, Section s2, Crew cr  \r\n" +
                                        "where p.workplaceid = w.workplaceid and p.Prodmonth = s.Prodmonth and p.Sectionid = s.SectionID and  \r\n" +
                                        "s.Prodmonth = s1.Prodmonth and s.reporttoSectionid = s1.SectionID and  \r\n" +
                                        "s1.Prodmonth = s2.Prodmonth and s1.reporttoSectionid = s2.SectionID and p.OrgUnitDay = cr.GangNo  \r\n" +

                                        "and p.prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and s2.SectionID = '" + reportSettings.NAME + "' ) a  \r\n" +


                                        "left outer join   \r\n" +
                                        "(select wp, sec, SUM(totblast) totblast from (  \r\n" +
                                        "select WorkplaceID wp, p.sectionid sec,  case when MOCycle in ('BL', 'BRL', 'SUBL', 'SR') then 1 else 0 end as totblast from vw_Planning p, Section s, Section s1, Section s2  \r\n" +
                                        "where p.Prodmonth = s.Prodmonth and p.Sectionid = s.SectionID and  Sqm <> 0 \r\n" +
                                        "and s.Prodmonth = s1.Prodmonth and s.reporttoSectionid = s1.SectionID   \r\n" +
                                        "and s1.Prodmonth = s2.Prodmonth and s1.reporttoSectionid = s2.SectionID   \r\n" +
                                        "and p.prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and s2.SectionID = '" + reportSettings.NAME + "'  and p.WorkingDay = 'Y') a group by wp, sec) b  \r\n " +
                                        "on a.Workplaceid = b.wp and a.minerID = b.sec  \r\n" +



                                        " order by order1, moid, sbid, OrgUnitDS, wpname ";
            _dbManData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManData.ResultsTableName = "WallChart";  //get table name
            _dbManData.ExecuteInstruction();



            DataSet ReportDatasetLosses = new DataSet();
            ReportDatasetLosses.Tables.Add(_dbManData.ResultsDataTable);
            theReport.RegisterData(ReportDatasetLosses);


            theReport.Load(TGlobalItems.ReportsFolder + "\\WallRoom.frx");

            //theReport.RegisterData(ReportDatasetHeadings, "MODaily_Stoping_Headings");


           //theReport.Design();

            //pcReport.Clear();
            //theReport.Prepare();
            //theReport.Preview = pcReport;
            //theReport.ShowPrepared();

            if (TParameters.DesignReport)
            {
                theReport.Design();
            }
            try
            {
theReport.Prepare();
            }
            catch
            {
                Exception ss;
            }
            finally
            {
ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;

            }
            
            

        }

        private void pgMeasuringList_Click(object sender, EventArgs e)
        {

        }
    }
}
