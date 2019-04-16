using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;
using System.Threading;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports._6ShiftRecon
{
    public partial class ucRecon : ucReportSettingsControl
    {
        private clsReportSettings reportSettings = new clsReportSettings();
        private sysMessagesClass _sysMessagesClass = new sysMessagesClass();
    

        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }

        private Thread theReportThread;
        private string theSystemDBTag = "DBHARMONYPAS";

    
        private string Banner;
        private int MOHier;
        private int Prodmonth;


        DataTable dtSections = new DataTable();

        Procedures procs = new Procedures();

        string sec;

        public ucRecon()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        private void ucRecon_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            reportSettings.UserCurrentInfo = this.UserCurrentInfo.Connection;
            reportSettings.DBTag = theSystemDBTag;  // this.theSystemDBTag;        

            
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan.SqlStatement = " select currentproductionmonth, banner from sysset ";


            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            ProdMonthTxt.Text = Convert.ToString(_dbMan.ResultsDataTable.Rows[0][0].ToString());

            Procedures procs = new Procedures();
            procs.ProdMonthVis2(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;

            SysSettings.Banner = Convert.ToString(_dbMan.ResultsDataTable.Rows[0][1].ToString());



        }



        public override bool prepareReport()
        {
            bool theResult;
            theReportThread = new Thread(new ParameterizedThreadStart(createReport));
            theReportThread.SetApartmentState(ApartmentState.STA);
            theReportThread.Start(reportSettings);
            theResult = true;
            return theResult;
        }


        private void createReport(Object theReportSettings)
        {
            Report theReport = new Report();


            //////////////////////////////


            string ReconDay = "Thu";


            if (SysSettings.Banner == "Masimong Mine")
            {
                ReconDay = "Mon";
            }
            MWDataManager.clsDataAccess _dbMan6Shift = new MWDataManager.clsDataAccess();

            if (TypeRgb.SelectedIndex == 0)
            {
                if (sec != "Total Mine")
                {
                    //first dataset
                    //MWDataManager.clsDataAccess _dbMan6Shift = new MWDataManager.clsDataAccess();
                    _dbMan6Shift.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan6Shift.SqlStatement = " SELECT * from tbl_ReconStopingWP where pm = '" + ProdMonthTxt.Value.ToString() + "' and aa = '" + procs.ExtractBeforeColon(sec) + "' order by d1 desc ";
                    _dbMan6Shift.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan6Shift.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan6Shift.ResultsTableName = "6Shift";
                    _dbMan6Shift.ExecuteInstruction();
                }
                else
                {
                    // MWDataManager.clsDataAccess _dbMan6Shift = new MWDataManager.clsDataAccess();
                    _dbMan6Shift.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan6Shift.SqlStatement = " exec Report_6ShiftTot '" + ProdMonthTxt.Value.ToString() + "', '" + sec + "', '" + ReconDay + "', '" + sec + "' ";
                    _dbMan6Shift.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan6Shift.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan6Shift.ResultsTableName = "6Shift";
                    _dbMan6Shift.ExecuteInstruction();
                }
            }

            if (TypeRgb.SelectedIndex == 1)
            {
                if (sec != "Total Mine")
                {
                    //first dataset
                    //MWDataManager.clsDataAccess _dbMan6Shift = new MWDataManager.clsDataAccess();
                    _dbMan6Shift.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan6Shift.SqlStatement = " exec Report_6ShiftDev '" + ProdMonthTxt.Value.ToString() + "', '" + procs.ExtractBeforeColon(sec) + "', '" + ReconDay + "', '" + sec + "' ";
                    _dbMan6Shift.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan6Shift.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan6Shift.ResultsTableName = "6Shift";
                    _dbMan6Shift.ExecuteInstruction();
                }
                else
                {
                    // MWDataManager.clsDataAccess _dbMan6Shift = new MWDataManager.clsDataAccess();
                    _dbMan6Shift.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan6Shift.SqlStatement = " exec Report_6ShiftTotDev '" + ProdMonthTxt.Value.ToString() + "', '" + sec + "', '" + ReconDay + "', '" + sec + "' ";
                    _dbMan6Shift.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan6Shift.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan6Shift.ResultsTableName = "6Shift";
                    _dbMan6Shift.ExecuteInstruction();
                }
            }

            if (TypeRgb.SelectedIndex == 2)
            {
                if (Otherradio.SelectedIndex == 1)
                {
                    _dbMan6Shift.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan6Shift.SqlStatement = " exec [Report_6ShiftVamp] '" + ProdMonthTxt.Value.ToString() + "' ";
                    _dbMan6Shift.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan6Shift.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan6Shift.ResultsTableName = "6Shift";
                    _dbMan6Shift.ExecuteInstruction();
                }
                else
                {
                    _dbMan6Shift.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan6Shift.SqlStatement = " exec [Report_6ShiftVampAdv] '" + ProdMonthTxt.Value.ToString() + "' ";
                    _dbMan6Shift.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan6Shift.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan6Shift.ResultsTableName = "6Shift";
                    _dbMan6Shift.ExecuteInstruction();

                }

            }



            MWDataManager.clsDataAccess _dbManData = new MWDataManager.clsDataAccess();
            _dbManData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManData.SqlStatement = "select '" + SysSettings.Banner + "' banner, '" + ProdMonth1Txt.Text + "' ppppp, '" + sec + "'Section ";
            _dbManData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManData.ResultsTableName = "Titile";  //get table name
            _dbManData.ExecuteInstruction();



            DataSet ReportTit = new DataSet();
            ReportTit.Tables.Add(_dbManData.ResultsDataTable);
            theReport.RegisterData(ReportTit);

            DataSet ReportDataset = new DataSet();
            ReportDataset.Tables.Add(_dbMan6Shift.ResultsDataTable);

            // DataSet GraphDataSet = new DataSet();
            // GraphDataSet.Tables.Add(_dbMan6Shift1.ResultsDataTable);

            theReport.RegisterData(ReportDataset);

            theReport.SetParameterValue("6Shift", "None");
            theReport.SetParameterValue("6Shift2", "None");
            theReport.SetParameterValue("6Shift3", "None");
            if (TypeRgb.SelectedIndex == 0)
            {
                if (sec != "Total Mine")
                {
                   // theReport.Load("6Shift.frx");
                    theReport.Load(TGlobalItems.ReportsFolder + "\\6Shift.frx");
                }
                else
                {
                    //theReport.Load("6ShiftTotal.frx");
                    theReport.Load(TGlobalItems.ReportsFolder + "\\6ShiftTotal.frx");
                }
            }

            if (TypeRgb.SelectedIndex == 1)
            {
                if (sec != "Total Mine")
                {
                   // theReport.Load("6ShiftDev.frx");
                    theReport.Load(TGlobalItems.ReportsFolder + "\\6ShiftDev.frx");
                }
                else
                {
                   // theReport.Load("6ShiftTotalDev.frx");
                    theReport.Load(TGlobalItems.ReportsFolder + "\\6ShiftTotalDev.frx");
                }
            }

            if (TypeRgb.SelectedIndex == 2)
            {
                if (Otherradio.SelectedIndex == 0)
                {
                    // theReport.Load("6ShiftVampAdv.frx");
                    theReport.Load(TGlobalItems.ReportsFolder + "\\6ShiftVampAdv.frx");
                }
                else
                {
                    // theReport.Load("6ShiftVamp.frx");
                    theReport.Load(TGlobalItems.ReportsFolder + "\\6ShiftVamp.frx");
                }
                // theReport.Design();
            }


            //theReport.Design();


            theReport.Prepare();
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;
        }

        private void ProdMonthTxt_ValueChanged(object sender, EventArgs e)
        {
           SectionsCombo.Items.Clear();
           SectionsCombo.Items.Add("Total Mine");
            MWDataManager.clsDataAccess _dbManGetISAfterStart = new MWDataManager.clsDataAccess();
            _dbManGetISAfterStart.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManGetISAfterStart.SqlStatement = " \r\n " +
                                                "  Select Distinct sc.SectionID_2 sectionid, sc.Name_2 name from Planning p, Section_Complete sc  \r\n " +
                                              " where p.SectionID = sc.SectionID  \r\n " +
                                              " and p.Prodmonth = sc.Prodmonth  \r\n " +
                                              " and p.Prodmonth = '" + ProdMonthTxt.Value + "'  \r\n " +

                                              " union  \r\n " +

                                              " Select Distinct sc.SectionID_2 sectionid, sc.Name_2 name from Planning_Vamping p, Section_Complete sc  \r\n " +
                                              " where p.SectionID = sc.SectionID  \r\n " +
                                              " and p.Prodmonth = sc.Prodmonth  \r\n " +
                                              " and p.Prodmonth = '" + ProdMonthTxt.Value + "'  \r\n " +

                                        " ";
            _dbManGetISAfterStart.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManGetISAfterStart.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManGetISAfterStart.ExecuteInstruction();

            DataTable dtSections1 = _dbManGetISAfterStart.ResultsDataTable;

            foreach (DataRow dr in dtSections1.Rows)
            {
                SectionsCombo.Items.Add(dr["sectionid"].ToString() + ":" + dr["name"].ToString());
            }
        }

        private void SectionsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            sec = SectionsCombo.SelectedItem.ToString();
        }

        private void ProdMonth1Txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProdMonthTxt_Click(object sender, EventArgs e)
        {
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonthTxt.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;
        }
    }
}
