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

namespace Mineware.Systems.Reports.MeasuringListReport
{
    public partial class ucMeasuringListReport : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        public string checking;
        //private DataTable dtSections;
        DataTable dtSections = new DataTable();
        private clsMeasuringListSettingProperties reportSettings = new clsMeasuringListSettingProperties();
        ucMeasureLstProp measure = new ucMeasureLstProp();
        Report theReport = new Report();
        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }
       
        public ucMeasuringListReport()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
         //   (theReport.FindObject("Data1") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.tot12mnthRank", true));
         //   (theReport.FindObject("Data1") as DataBand).Click += UcMeasuringListReport_Click;

        }

        private void UcMeasuringListReport_Click(object sender, EventArgs e)
        {
            if (theReport.GetParameterValue("Bob") != null)
                ParamLbl.Text = "";
                ParamLbl.Text = theReport.GetParameterValue("Bob").ToString();
        }

        Procedures procs = new Procedures();
        public void LoadSections()
        {
            procs.systemDBTag = theSystemDBTag;
            procs.connection = UserCurrentInfo.Connection;
           
            if (clsUserInfo.Hier == 1)
            {
                dtSections = procs.GetSections(TProductionGlobal.ProdMonthAsString  (reportSettings.Prodmonth), "4", "");
            }
            else
            {
                string Sec = clsUserInfo.UserBookSection + "              ".Substring(0, 3).Trim();

                dtSections = procs.GetSections(TProductionGlobal.ProdMonthAsString( reportSettings.Prodmonth), "4", Sec.ToString());
            }
            if (dtSections.Rows.Count != 0)
            {
                riSection.DataSource = dtSections;
                riSection.DisplayMember = "Name";
                riSection.ValueMember = "SectionID";
            }
        }

        private void ucMeasuringListReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            // reportSettings.ReportDate = DateTime.Now;
            reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
          iProdMonth   .Properties.Value = reportSettings.Prodmonth;
            LoadSections();
            reportSettings.SectionID = dtSections.Rows[0][0].ToString();
            reportSettings.Activity = "Stoping";
            pgMeasuringList.SelectedObject = reportSettings;
           // theReport.Preview.Click += Preview_Click;
        }

        private void Preview_Click(object sender, EventArgs e)
        {
          
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
            string Section = procs.ExtractBeforeColon(reportSettings.SectionID);
            string month = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);
            

            MWDataManager.clsDataAccess _dbManLosses = new MWDataManager.clsDataAccess();
            _dbManLosses.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            if (reportSettings .Activity  == "Stoping")
            {
               
                //_dbManLosses.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                _dbManLosses.SqlStatement = " select a.*, b.CrewName OrgUnitDS from ( SELECT a.*, b.tick1, b.tick2, b.tick3, b.tick4, b.tick5, b.tick6  from \r\n";
                _dbManLosses.SqlStatement += "(SELECT s2.sectionid MO, s2.name MOName, ss.Banner, \r\n";
                _dbManLosses.SqlStatement += "CONVERT(varchar,CONVERT(int,p.activity))+s.sectionid +':' +p.workplaceid aa, s1.sectionid SBID, s1.name SBName , s.sectionid MINID, \r\n";
                _dbManLosses.SqlStatement += "s.name MINName,  p.Workplaceid, w.Description,p.OrgUnitDay OrgUnitNo, p.Sqm CallSQM, s3.SqmTotal MeasSQM, CAST(p.Activity AS INT) as Activity, (case when p.Activity =0 then 'Stope'  when p.Activity =1 then 'Devlop' end) as ActivityDescription\r\n";
                _dbManLosses.SqlStatement += "from  section s, section s1, section s2, workplace w, planmonth p left outer join SURVEY s3 on \r\n";
                _dbManLosses.SqlStatement += " s3.Activity = p.Activity \r\n";
                _dbManLosses.SqlStatement += "AND s3.ProdMonth = p.Prodmonth \r\n";
                _dbManLosses.SqlStatement += "AND s3.SectionID = p.Sectionid \r\n";
                _dbManLosses.SqlStatement += "AND s3.WorkplaceID = p.Workplaceid,SYSSET ss \r\n";
                _dbManLosses.SqlStatement += "WHERE  p.workplaceid = w.workplaceid \r\n";
                _dbManLosses.SqlStatement += "AND p.prodmonth = s.prodmonth \r\n";
                _dbManLosses.SqlStatement += "AND p.sectionid = s.sectionid \r\n";
                _dbManLosses.SqlStatement += "AND s.prodmonth = s1.prodmonth \r\n";
                _dbManLosses.SqlStatement += "AND s.reporttosectionid = s1.sectionid \r\n";
                _dbManLosses.SqlStatement += "AND s1.prodmonth = s2.prodmonth \r\n";
                _dbManLosses.SqlStatement += "AND s1.reporttosectionid = s2.sectionid \r\n";
                _dbManLosses.SqlStatement += "AND p.activity IN (0,9) \r\n";

                _dbManLosses.SqlStatement += "AND p.prodmonth = '" + TProductionGlobal.ProdMonthAsString ( reportSettings.Prodmonth) + "'  \r\n";
                _dbManLosses.SqlStatement += "AND s2.sectionid = '" + reportSettings .SectionID  + "' and p.plancode='MP') a \r\n";
                _dbManLosses.SqlStatement += "left outer join (SELECT * FROM MOMeas_Stope WHERE prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)  + "') b on a.minid = b.sectionid and a.workplaceid = b.workplaceid and a.activity = b.activity \r\n";

                _dbManLosses.SqlStatement += " )a left outer join (select Distinct CrewName, Gangno from crew)b on a.OrgUnitNo = b.Gangno  \r\n";



                _dbManLosses.SqlStatement += "ORDER BY a.sbid, a.minid, a.OrgUnitNo \r\n";
                _dbManLosses.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLosses.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLosses.ResultsTableName = "MeasureList";  //get table name
                _dbManLosses.ExecuteInstruction();
            }
            else
            {
                //_dbManLosses.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                _dbManLosses.SqlStatement = " select a.*, b.CrewName OrgUnitDS from (SELECT a.*, b.tick1, b.tick2, b.tick3, b.tick4, b.tick5, b.tick6  from \r\n";
                _dbManLosses.SqlStatement += "(SELECT s2.sectionid MO, s2.name MOName, ss.Banner, \r\n";
                _dbManLosses.SqlStatement += "CONVERT(varchar,CONVERT(int,p.activity))+s.sectionid +':' +p.workplaceid aa, s1.sectionid SBID, s1.name SBName , s.sectionid MINID, \r\n";
                _dbManLosses.SqlStatement += "s.name MINName,  p.Workplaceid, w.Description,p.OrgUnitDay OrgUnitNo,p.Metresadvance CallAdv,s3.MainMetres    MeasAdv , p.Sqm CallSQM, s3.SqmTotal MeasSQM, CAST(p.Activity AS INT) as Activity, (case when p.Activity =0 then 'Stope'  when p.Activity =1 then 'Devlop' end) as ActivityDescription\r\n";
                _dbManLosses.SqlStatement += "from  section s, section s1, section s2, workplace w, planmonth p left outer join SURVEY s3 on \r\n";
                _dbManLosses.SqlStatement += " s3.Activity = p.Activity \r\n";
                _dbManLosses.SqlStatement += "AND s3.ProdMonth = p.Prodmonth \r\n";
                _dbManLosses.SqlStatement += "AND s3.SectionID = p.Sectionid \r\n";
                _dbManLosses.SqlStatement += "AND s3.WorkplaceID = p.Workplaceid,sysset ss \r\n";
                _dbManLosses.SqlStatement += "WHERE  p.workplaceid = w.workplaceid \r\n";
                _dbManLosses.SqlStatement += "AND p.prodmonth = s.prodmonth \r\n";
                _dbManLosses.SqlStatement += "AND p.sectionid = s.sectionid \r\n";
                _dbManLosses.SqlStatement += "AND s.prodmonth = s1.prodmonth \r\n";
                _dbManLosses.SqlStatement += "AND s.reporttosectionid = s1.sectionid \r\n";
                _dbManLosses.SqlStatement += "AND s1.prodmonth = s2.prodmonth \r\n";
                _dbManLosses.SqlStatement += "AND s1.reporttosectionid = s2.sectionid \r\n";
                _dbManLosses.SqlStatement += "AND p.activity IN (1) \r\n";

                _dbManLosses.SqlStatement += "AND p.prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)  + "'  \r\n";
                _dbManLosses.SqlStatement += "AND s2.sectionid = '" + reportSettings .SectionID  + "' and p.plancode='MP') a \r\n";
                _dbManLosses.SqlStatement += "left outer join (SELECT * FROM MOMeas_Stope WHERE prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)  + "') b on a.minid = b.sectionid and a.workplaceid = b.workplaceid and a.activity = b.activity \r\n";
                _dbManLosses.SqlStatement += " )a left outer join (select Distinct CrewName, Gangno from crew)b on a.OrgUnitNo = b.Gangno  \r\n";
                _dbManLosses.SqlStatement += "ORDER BY a.sbid, a.minid, a.OrgUnitNo \r\n";
                _dbManLosses.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLosses.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLosses.ResultsTableName = "MeasureList";  //get table name
                _dbManLosses.ExecuteInstruction();
            }
            DataSet ReportDatasetLosses = new DataSet();
            ReportDatasetLosses.Tables.Add(_dbManLosses.ResultsDataTable);
            theReport.RegisterData(ReportDatasetLosses);

            theReport.SetParameterValue("Bob", "None");
            theReport.SetParameterValue("Bob2", "None");
            theReport.SetParameterValue("Bob3", "None");
            theReport.Clear();
            // theReport.Load("MeasuringListRep.frx");
            if (reportSettings.Activity == "Stoping")
            {
                theReport.Load(TGlobalItems.ReportsFolder + "\\MeasuringListRep.frx");
                theReport.SetParameterValue("month", month.ToString());
                //  theReport.Design();
            }
            else
            {
                theReport.Load(TGlobalItems.ReportsFolder + "\\MeasuringListRepDev.frx");
                theReport.SetParameterValue("month", month.ToString());
                // theReport.Design();
            }

            if (TParameters.DesignReport)
            {
                theReport.Design();
            }

           // theReport.Design();
            theReport.Prepare();
            theReport.Refresh();
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;
            // FastReport.Preview.PreviewControl pcReport = new FastReport.Preview.PreviewControl();
            //  theReport.Preview = pcReport;
            // theReport.ShowPrepared();
            //   ActiveReport.SetReport = pcReport;
            //   ActiveReport.isDone = true;
            // pcReport.Click += PcReport_Click;
          //  (theReport.FindObject("Data1") as DataBand).Click += UcMeasuringListReport_Click;
            (theReport.FindObject("Text7") as TextObject  ).Click += UcMeasuringListReport_Click;
            (theReport.FindObject("Text8") as TextObject).Click += UcMeasuringListReport_Click;
            (theReport.FindObject("Text3") as TextObject).Click += UcMeasuringListReport_Click;
         
            //(theReport.FindObject("Text157") as TextObject).Click += UcMeasuringListReport_Click;
            //(theReport.FindObject("Text9") as TextObject).Click += UcMeasuringListReport_Click;
            //(theReport.FindObject("Text158") as TextObject).Click += UcMeasuringListReport_Click;
            //(theReport.FindObject("Text160") as TextObject).Click += UcMeasuringListReport_Click;

        }

        private void PcReport_Click(object sender, EventArgs e)
        {
          //  if (theReport.GetParameterValue("Bob") != null)
              //  ParamLbl.Text = theReport.GetParameterValue("Bob").ToString();
            
        }

        private void ParamLbl_TextChanged(object sender, EventArgs e)
        {
            if (ParamLbl.Text == ParamLbl1.Text)
            {
                //MessageBox.Show("Please enter the Problem Id", "Insufficient information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ParamLbl1.Text = ParamLbl.Text;

                if (ParamLbl.Text != "")
                {
                    measure.MLPMLbl.Text = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);
                    measure.MLACTLbl.Text = ParamLbl.Text.Substring(0, 1);
                    measure.MLWPLbl.Text = procs.ExtractAfterColon(ParamLbl.Text + "                                  ".Substring(2, 30)).Trim();
                    measure.MLSecLbl.Text = procs.ExtractBeforeColon(ParamLbl.Text).Trim();
                    measure.MLSecLbl.Text = measure.MLSecLbl.Text + "                  ";
                    measure.MLSecLbl.Text = measure.MLSecLbl.Text.Substring(1, 10);
                    measure.MLSecLbl.Text = measure.MLSecLbl.Text.Trim();
                    measure.Connection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    measure.ShowDialog();

                    //MsgFrm.Text = "Record Inserted";
                    //Procedures.MsgText = "Problem Group Added";
                    //MsgFrm.Show();

                 //   showBtn_Click(null, null);


                }
            }
        }

        private void ucMeasuringListReport_Click(object sender, EventArgs e)
        {
            if (theReport.GetParameterValue("Bob") != null)
                ParamLbl.Text = theReport.GetParameterValue("Bob").ToString();
        }

        private void ucMeasuringListReport_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ucMeasuringListReport_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pgMeasuringList_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pgMeasuringList_Click(object sender, EventArgs e)
        {

        }
    }
    }
