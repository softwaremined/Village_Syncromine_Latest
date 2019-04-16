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
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.BonusReport
{
    public partial class ucBonusReport : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        private clsBonusReportSettings reportSettings = new clsBonusReportSettings();
        DataTable dtSections = new DataTable();
        Report theReport = new Report();
        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }
        Procedures procs = new Procedures();
        public ucBonusReport()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
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
            string section = "";
            section=procs.ExtractBeforeColon(reportSettings.SectionID );
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            if (reportSettings.Prodmonth.ToString () != "")
            {
                //if (lkpActivity.EditValue.ToString() == "0")
                //{
                if (reportSettings.Activity  == "Stoping")
                {
                    string act = "0";
                    //gcStoping.Visible = true;
                    //gcDevelopment.Visible = false;
                   // MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement =
                                           " select ''status,divisioncode ExeName, 'xxx' Costareaid, W.Workplaceid, w.Description, s.Prodmonth,cs.StopeTypeDesc Type, 'KR' Type2, " +
                                           " s.LedgeSqm Lsqm, s.StopeSqm SSQM, s.LedgeSqmOSF LOSF, s.StopeSqmOSF SOSF, s.LedgeSqmOS LOS, s.StopeSqmOS SOS, s.FLTotal Facelength, " +
                                           " s.LedgeFL LFL, s.StopeFL FFL, s.FLOSTotal OSFL, s.CleanSqm SQMClean, s.CleanDist DistFromFace, s.CW ChnWidth, s.FW FWWidth, s.HW HWWidth, " +
                                           " s.SWSQM StpWidth, s.CrewMorning, s.CrewAfternoon, s.CrewEvening, " +
                                           " CleaningCrew,TrammingCrew,HlgeMaintainanceCrew,RiggerCrew,RseCleaningCrew, " +
                                           " 'Miner' Hierarchicalname, sc.name, " +
                                           //" M2NOTTOBEPAIDFORMINER,MINSPRETARGETSW2,MidmonthIndicator,DISTFROMFACEMIDMONTH,SweepingPenalty,SF1SF2 "+
                                           " case when M2NOTTOBEPAIDFORMINER is null then 0 else M2NOTTOBEPAIDFORMINER end M2NOTTOBEPAIDFORMINER, " +
                                          "  case when MINSPRETARGETSW2 is null then 0 else MINSPRETARGETSW2 end MINSPRETARGETSW2, " +
                                           " case when MidmonthIndicator is null then 0 else  MidmonthIndicator end MidmonthIndicator, " +
                                           " case when DISTFROMFACEMIDMONTH is null then 0 else DISTFROMFACEMIDMONTH end DISTFROMFACEMIDMONTH, " +
                                           " case when SweepingPenalty is null then 0 else SweepingPenalty end SweepingPenalty, SF1SF2 " +
                                           " from survey s inner join workplace w on w.WorkplaceID  = s.workplaceid inner join Code_StopeTypes cs on cs.StopeTypeID = s.SType " +
                                          // " inner join SectionComplete sc on s.prodmonth = sc.prodmonth and s.sectionid = sc.sectionid where s.activity = '" + lkpActivity.EditValue.ToString() + "' and s.Prodmonth ='" + ProdMonthTxt.Value.ToString() + "'  and sc.MOID='" + procs.ExtractBeforeColon(SectionsCombo.Text) + "' ";
                                          " inner join Section_Complete sc on s.prodmonth = sc.prodmonth and s.sectionid = sc.sectionid where s.activity = '" + act + "' and s.Prodmonth ='" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  and sc.Sectionid_2='" + section + "' ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "BonusStoping";
                    _dbMan.ExecuteInstruction();
                    if (_dbMan.ResultsDataTable.Rows.Count == 0)
                    {
                        //gcStoping.Visible = false;
                        //gcDevelopment.Visible = false;
                        MessageBox.Show("There is no data to display.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        theReport.Prepare();
                        theReport.Refresh();
                        ActiveReport.SetReport = theReport;
                        ActiveReport.isDone = true;
                        return;
                    }
                    else
                    {
                        //gcStoping.DataSource = _dbMan.ResultsDataTable;
                    }

                }
                else
                {
                    //gcStoping.Visible = false;
                    //gcDevelopment.Visible = true;
                    //MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                 
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    string act = "1";
                    _dbMan.SqlStatement =
                                          " select ''status,divisioncode ExeName, 'xxx' Costareaid,s.Prodmonth, W.Workplaceid, w.Description, s.TotalMetres Adv_Total, s.MeasWidth, s.MeasHeight," +
                                         "   s.CubicsMetres [Extra Units], 'Miner' Hierarchicalname, sc.Name, s.CrewMorning,s.CrewAfternoon,s.CrewEvening, e.Description Description2, s.cw ," +
                                         " case when MetersNotToBePaid is NULL then 0 else MetersNotToBePaid end MetersNotToBePaid,CleaningCrew,TrammingCrew,HlgeMaintainanceCrew,RiggerCrew,RseCleaningCrew " +
                                         "   from survey s inner join workplace w on w.WorkplaceID  = s.workplaceid " +
                                        "    inner join Section_Complete sc on s.prodmonth = sc.prodmonth and s.sectionid = sc.sectionid inner join endtype e on e.EndTypeID = w.EndTypeID " +
                                          //"   where  s.activity = '" + lkpActivity.EditValue.ToString() + "' and s.Prodmonth ='" + ProdMonthTxt.Value.ToString() + "' and sc.MOID='" + procs.ExtractBeforeColon(SectionsCombo.Text) + "' ";
                                          "   where  s.activity = '" + act + "' and s.Prodmonth ='" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and sc.Sectionid_2='" + section + "' ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "BonusDevelopment";
                    _dbMan.ExecuteInstruction();
                    if (_dbMan.ResultsDataTable.Rows.Count == 0)
                    {
                        //gcStoping.Visible = false;
                        //gcDevelopment.Visible = false;
                        MessageBox.Show("There is no data to display.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        theReport.Prepare();
                        theReport.Refresh();
                        ActiveReport.SetReport = theReport;
                        ActiveReport.isDone = true;
                        return;
                    }
                    else
                    {
                        //gcDevelopment.DataSource = _dbMan.ResultsDataTable;
                    }
                }
                DataSet Bonusdataset = new DataSet();
                Bonusdataset.Tables.Add(_dbMan.ResultsDataTable);
                theReport.RegisterData(Bonusdataset);
                theReport.Load(TGlobalItems.ReportsFolder + "\\BonusReport.frx");
                string activity = "";
                //if(lkpActivity .EditValue .ToString ()=="0")
                if (reportSettings .Activity  == "Stoping")
                {
                    activity = "Stoping";
                }
                else
                {
                    activity = "Development";
                }
                theReport.SetParameterValue("Activity", activity);
                theReport.SetParameterValue("Prodmonth", TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
                theReport.SetParameterValue("Section", reportSettings.SectionID);
                theReport.SetParameterValue("Banner", SysSettings.Banner);
                theReport.Prepare();
                theReport.Refresh();
                ActiveReport.SetReport = theReport;
                ActiveReport.isDone = true;
                //}
            }
        }


         //   Procedures procs = new Procedures();

        private void LoadSections()
        {
          
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " Select SectionID+':'+Name SectionID " +
                                  "from Section s where s.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'";

            _dbMan.SqlStatement += " and Hierarchicalid  = 4 ";
            _dbMan.SqlStatement += " order by SectionID asc  ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            dtSections = _dbMan.ResultsDataTable;
            riSection.DataSource = dtSections;
            riSection.DisplayMember = "SectionID";
            riSection.ValueMember = "SectionID";
           


            // this.gbSectionID.Text = "Section ID";
        }

        private void ucBonusReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            // reportSettings.ReportDate = DateTime.Now;
            reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            iProdmonth.Properties.Value = reportSettings.Prodmonth;
            LoadSections();
            reportSettings.SectionID = dtSections.Rows[0][0].ToString();
       
            reportSettings.Activity = "Stoping";
          pgBonusReportSettings   .SelectedObject = reportSettings;
        }

        private void pgBonusReportSettings_Click(object sender, EventArgs e)
        {

        }
    }
}
