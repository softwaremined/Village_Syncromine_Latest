using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;
using System.Threading;
using Mineware.Systems.ProductionGlobal;
using System.Data.SqlClient;
using MWDataManager;
using System.Drawing;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.PlannedVsBooked
{
    public partial class ucPlannedVsBooked : ucReportSettingsControl
    {
        private PlannedVsBookedSettingsProperties reportSettings = new PlannedVsBookedSettingsProperties();
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        string MS;
        private DateTime dteEndDate;
        private string _pmonth;
        private bool ErrFound;


        public ucPlannedVsBooked()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        private void ucPlannedVsBooked_Load(object sender, EventArgs e)
        {
            try
            {
                reportSettings.Type = "Tons";
                reportSettings.ShowType = "Both";
                reportSettings.CastType = "Both";
                reportSettings.MSType = "All";
                reportSettings.WhatMonth = "Production Month";
                CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
                BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;

                BMEBL.SetsystemDBTag = this.theSystemDBTag;
                BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

                DataTable dtActivityData = BMEBL.GetActivityReports();

                rpActivity.DataSource = dtActivityData;
                rpActivity.DisplayMember = "Desc";
                rpActivity.ValueMember = "Code";

                reportSettings.UserCurrentInfo = this.UserCurrentInfo;
                reportSettings.systemDBTag = this.theSystemDBTag;

                reportSettings.pmonth = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString();
                int theYear = Convert.ToInt32(reportSettings.pmonth.Substring(0, 4));
                int theMonth = Convert.ToInt32(reportSettings.pmonth.Substring(4, 2));
                DateTime theDate = new DateTime(theYear, theMonth, 1);
                reportSettings.Prodmonth = theDate.Date;

                reportSettings.UpdateSumOnRequest += reportSettings_UpdateSumOnRequest;
                reportSettings.Showuntil = DateTime.Now;
                iShowuntil.Properties.Value = reportSettings.Showuntil;

                reportSettings.Desc = "0";
                iProdmonth.Properties.Value = reportSettings.Prodmonth.ToString();

                DataTable dtSectionData = new DataTable();

                if (BMEBL.GetPlanSectionsAndNameADO(reportSettings.pmonth) == true)
                {
                    dtSectionData = BMEBL.ResultsDataTable;
                    rpSectionid.DataSource = BMEBL.ResultsDataTable;
                    rpSectionid.DisplayMember = "NAME";
                    rpSectionid.ValueMember = "NAME";
                }
                reportSettings.NAME = dtSectionData.Rows[0]["NAME"].ToString();
                pgPlanVsBookSettings.SelectedObject = reportSettings;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void CheckForErrors()
        {
            ErrFound = false;

            if (ErrFound == false)
            {

                if (reportSettings.HierarchicalID == 0)
                {
                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Please select a Summary On level", Color.Red);

                    ErrFound = true;
                }

            }

        }

        public override bool prepareReport()
        {
            bool theResult = false;

            ErrFound = false;
            CheckForErrors();

            if (ErrFound == false)
            {
                theReportThread = new Thread(new ParameterizedThreadStart(createReport));
                theReportThread.SetApartmentState(ApartmentState.STA);
                theReportThread.Start(reportSettings);
                theResult = true;
            }
            return theResult;
        }

        private void createReport(Object theReportSettings)
        {
            string theTypeMonth = "";
            if (reportSettings.WhatMonth.ToString() == "Production Month")
                theTypeMonth = "P";
            else
                theTypeMonth = "M";

            string theReefWaste = "";
            if (reportSettings.ShowType.ToString() == "Reef")
                theReefWaste = "0";
            else if (reportSettings.ShowType.ToString() == "Waste")
                theReefWaste = "1";

            string theWorkCap = "";
            if (reportSettings.MSType.ToString() == "Main")
                theWorkCap = "M";
            else if (reportSettings.MSType.ToString() == "Secondary")
                theWorkCap = "S";

            string theAccountCode = "";
            if (reportSettings.CastType.ToString() == "Working")
                theAccountCode = "0";
            else if (reportSettings.CastType.ToString() == "Secondary")
                theAccountCode = "1";

            string theDate = reportSettings.Prodmonth.ToString("yyyyMM");
            MWDataManager.clsDataAccess _PlannedVsBookedDay = new MWDataManager.clsDataAccess();
            try
            {
                _PlannedVsBookedDay.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _PlannedVsBookedDay.SqlStatement = "sp_PlannedVsBooked_Daily";
                _PlannedVsBookedDay.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                _PlannedVsBookedDay.ResultsTableName = "PlannedVsBookedDay";

                SqlParameter[] _paramCollection9 =
                    {
                        _PlannedVsBookedDay.CreateParameter("@TypeMonth", SqlDbType.VarChar , 1,theTypeMonth),
                        _PlannedVsBookedDay.CreateParameter("@Prodmonth", SqlDbType.Int , 7,theDate),
                        _PlannedVsBookedDay.CreateParameter("@SectionName", SqlDbType.VarChar  , 60,reportSettings.NAME),
                        _PlannedVsBookedDay.CreateParameter("@RunDate", SqlDbType.VarChar , 10,reportSettings.Showuntil.ToString("yyyy-MM-dd")),
                        _PlannedVsBookedDay.CreateParameter("@SumLevel", SqlDbType.VarChar  , 1,reportSettings.HierarchicalID),
                        _PlannedVsBookedDay.CreateParameter("@Activity", SqlDbType.VarChar  , 1,reportSettings.Desc),
                        _PlannedVsBookedDay.CreateParameter("@Account", SqlDbType.VarChar  , 1,theAccountCode),
                        _PlannedVsBookedDay.CreateParameter("@ReefWaste", SqlDbType.VarChar  , 1,theReefWaste),
                        _PlannedVsBookedDay.CreateParameter("@WorkCap", SqlDbType.VarChar  , 1,theWorkCap),
                        _PlannedVsBookedDay.CreateParameter("@Unit", SqlDbType.VarChar  , 20,reportSettings.Type),
                    };
                _PlannedVsBookedDay.ParamCollection = _paramCollection9;
                _PlannedVsBookedDay.queryReturnType = MWDataManager.ReturnType.DataTable;
                _PlannedVsBookedDay.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException("Report Section:PlannedVsBooked:" + _exception.Message, _exception);
            }

            MWDataManager.clsDataAccess _PlannedVsBookedProg = new MWDataManager.clsDataAccess();
            try
            {
                _PlannedVsBookedProg.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _PlannedVsBookedProg.SqlStatement = "sp_PlannedVsBooked_Prog";
                _PlannedVsBookedProg.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                _PlannedVsBookedProg.ResultsTableName = "PlannedVsBookedProg";

                SqlParameter[] _paramCollection9 =
                    {
                        _PlannedVsBookedProg.CreateParameter("@TypeMonth", SqlDbType.VarChar , 1,theTypeMonth),
                        _PlannedVsBookedProg.CreateParameter("@Prodmonth", SqlDbType.Int , 7,theDate),
                        _PlannedVsBookedProg.CreateParameter("@SectionName", SqlDbType.VarChar  , 60,reportSettings.NAME),
                        _PlannedVsBookedProg.CreateParameter("@RunDate", SqlDbType.VarChar , 10,reportSettings.Showuntil.ToString("yyyy-MM-dd")),
                        _PlannedVsBookedProg.CreateParameter("@SumLevel", SqlDbType.VarChar  , 1,reportSettings.HierarchicalID),
                        _PlannedVsBookedProg.CreateParameter("@Activity", SqlDbType.VarChar  , 1,reportSettings.Desc),
                        _PlannedVsBookedProg.CreateParameter("@Account", SqlDbType.VarChar  , 1,theAccountCode),
                        _PlannedVsBookedProg.CreateParameter("@ReefWaste", SqlDbType.VarChar  , 1,theReefWaste),
                        _PlannedVsBookedProg.CreateParameter("@WorkCap", SqlDbType.VarChar  , 1,theWorkCap),
                        _PlannedVsBookedProg.CreateParameter("@Unit", SqlDbType.VarChar  , 20,reportSettings.Type),
                    };
                _PlannedVsBookedProg.ParamCollection = _paramCollection9;
                _PlannedVsBookedProg.queryReturnType = MWDataManager.ReturnType.DataTable;
                _PlannedVsBookedProg.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException("Report Section:PlannedVsBooked:" + _exception.Message, _exception);
            }

            string whatDate = "";
            if (theTypeMonth == "P")
                whatDate = "Production Month : " + reportSettings.Prodmonth.ToString("yyyyMM") + " until " + reportSettings.Showuntil.ToString("yyyy-MM-dd");
            else
                whatDate = "Mill Month : " + reportSettings.Prodmonth.ToString("yyyyMM") + " until " + reportSettings.Showuntil.ToString("yyyy-MM-dd");
            DataSet PlannedVsBookedset = new DataSet();
            DataTable dt = new DataTable();
            PlannedVsBookedset.Clear();
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Banner", typeof(string));
            dt.Columns.Add("TheDate", typeof(string));

            MWDataManager.clsDataAccess _dbManCheck = new MWDataManager.clsDataAccess();
            _dbManCheck.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManCheck.SqlStatement = "select CheckMeas,Banner from SysSet ";
            _dbManCheck.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManCheck.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _dbManLosses.ResultsTableName = "Days";  //get table name
            _dbManCheck.ExecuteInstruction();



            SysSettings.CheckMeas = _dbManCheck.ResultsDataTable.Rows[0][0].ToString();
            SysSettings.Banner = _dbManCheck.ResultsDataTable.Rows[0][1].ToString();

            dt.Rows.Add(reportSettings.Type, SysSettings.Banner, whatDate);
            Report theReport = new Report();

            if (_PlannedVsBookedDay.ResultsDataTable.Rows.Count == 0 || _PlannedVsBookedProg.ResultsDataTable.Rows.Count == 0)
            {
                MessageBox.Show("There is no data for your selection", "", MessageBoxButtons.OK);
                theReport.Prepare();
                ActiveReport.SetReport = theReport;
                ActiveReport.isDone = true;
            }
            else
            {
                PlannedVsBookedset.Tables.Add(_PlannedVsBookedDay.ResultsDataTable);
                PlannedVsBookedset.Tables.Add(_PlannedVsBookedProg.ResultsDataTable);

                PlannedVsBookedset.Tables.Add(dt);

                theReport.RegisterData(PlannedVsBookedset);
                theReport.Load(TGlobalItems.ReportsFolder + "\\PlannedVsBooked.frx");
                theReport.SetParameterValue("logo", TGlobalItems.ClientLogo);
                theReport.SetParameterValue("logoMineware", TGlobalItems.CompanyLogo);

                //theReport.Design();
                if (TParameters.DesignReport)
                {
                    theReport.Design();
                }
                theReport.Prepare();

                ActiveReport.SetReport = theReport;
                ActiveReport.isDone = true;
            }
        }

        private void rpSectionid_EditValueChanged(object sender, EventArgs e)
        {
            pgPlanVsBookSettings.PostEditor();

            //pgPlanVsBookSettings.FocusNext();

            DataTable dtlevels = reportSettings.LoadAllLevel();

            reportSettings.pmonth = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString();
            rpSummaryon.DataSource = dtlevels;

            if (dtlevels != null)
            {
                rpSummaryon.DisplayMember = "Description";
                rpSummaryon.ValueMember = "HierarchicalID";
            }
        }

        void reportSettings_UpdateSumOnRequest(object sender, PlannedVsBookedSettingsProperties.UpdateSumOnArg e)
        {
            pgPlanVsBookSettings.PostEditor();

            DataTable dtlevels = reportSettings.LoadAllLevel();

            reportSettings.pmonth = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString();
            rpSummaryon.DataSource = dtlevels;

            if (dtlevels != null)
            {
                rpSummaryon.DisplayMember = "Description";
                rpSummaryon.ValueMember = "HierarchicalID";
            }

            //reportSettings.HierarchicalID = reportSettings.SumOn.Rows[0]["HierarchicalID"].ToString();
        }

        private void repositoryItemRadioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.RadioGroup editor = (sender as DevExpress.XtraEditors.RadioGroup);
            MS = editor.Properties.GetDisplayText(editor.EditValue);
        }

        private void rpMonth_Click(object sender, EventArgs e)
        {
            pgPlanVsBookSettings.PostEditor();
            set_Dates();
        }

        private void set_Dates()
        {

            MWDataManager.clsDataAccess BMEBL = new MWDataManager.clsDataAccess();
            BMEBL.queryReturnType = MWDataManager.ReturnType.DataTable;

            BMEBL.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            string whatCalen;
            if (reportSettings.WhatMonth.ToString() == "Production Month")
                whatCalen = "P";
            else
                whatCalen = "M";

            DataTable dtEndDate = new DataTable();


            dtEndDate = loadRunDate(whatCalen, reportSettings.Prodmonth.ToString("yyyyMM"));
            if (dtEndDate.Rows.Count > 0)
            {
                if (dtEndDate.Rows[0]["EDate"] is DBNull)
                {
                }
                else
                {
                    dteEndDate = Convert.ToDateTime(dtEndDate.Rows[0]["EDate"].ToString());
                    if (DateTime.Now < dteEndDate)
                        dteEndDate = DateTime.Now;
                }
            }
            reportSettings.Showuntil = dteEndDate;
            iShowuntil.Properties.Value = reportSettings.Showuntil;
        }

        private void rpProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            pgPlanVsBookSettings.PostEditor();
            if (Convert.ToInt32(reportSettings.Prodmonth.Month.ToString()) < 10)
            {
                _pmonth = reportSettings.Prodmonth.Year.ToString() + "0" + reportSettings.Prodmonth.Month.ToString();
            }
            else
            {
                _pmonth = reportSettings.Prodmonth.Year.ToString() + reportSettings.Prodmonth.Month.ToString();
            }
            set_Dates();
        }

        public DataTable loadRunDate(string _whatCalen, string Prodmonth)
        {
            if (Prodmonth != null)
            {
                if (_whatCalen == "P")
                {
                    MWDataManager.clsDataAccess _loadRunDate = new MWDataManager.clsDataAccess();
                    _loadRunDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _loadRunDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _loadRunDate.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _loadRunDate.SqlStatement = "SELECT Max(EndDate) EDate FROM  SECTION_COMPLETE SC " +
                                             "inner join SECCAL on " +
                                             "SC.PRODMONTH = SECCAL.PRODMONTH and " +
                                             "SC.SECTIONID_1 = SECCAL.SECTIONID   " +
                                             "WHERE SC.PRODMONTH = '" + Prodmonth + "' ";

                    _loadRunDate.ExecuteInstruction();

                    return _loadRunDate.ResultsDataTable;
                }
                else
                {
                    MWDataManager.clsDataAccess _loadRunDate = new MWDataManager.clsDataAccess();
                    _loadRunDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _loadRunDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _loadRunDate.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _loadRunDate.SqlStatement = "select max(EndDate) EDate from CALENDARMILL  " +
                                             "WHERE MillMonth= '" + Prodmonth + "' ";

                    _loadRunDate.ExecuteInstruction();

                    return _loadRunDate.ResultsDataTable;
                }
            }
            else
            {
                return null;
            }
        }

        void reportSettings_UpdateActivitySelectionRequest(object sender, PlannedVsBookedSettingsProperties.UpdateActivitySelectionArg e)
        {
            //iDevelopment.Visible = e.Dev;
            //iStoping.Visible = e.Stoping;
        }

        private void rpActivity_EditValueChanged(object sender, EventArgs e)
        {
            pgPlanVsBookSettings.PostEditor();

            rpType.Items[0].Enabled = true;
            rpType.Items[1].Enabled = true;
            rpType.Items[2].Enabled = true;
            rpType.Items[3].Enabled = true;
            rpType.Items[4].Enabled = true;
            rpType.Items[5].Enabled = true;
            rpType.Items[6].Enabled = true;

            if (reportSettings.Desc == "0")
                rpType.Items[2].Enabled = false;

            if (reportSettings.Desc == "1")
                rpType.Items[1].Enabled = false;
        }

        private void pgPlanVsBookSettings_Click(object sender, EventArgs e)
        {

        }
    }
}
