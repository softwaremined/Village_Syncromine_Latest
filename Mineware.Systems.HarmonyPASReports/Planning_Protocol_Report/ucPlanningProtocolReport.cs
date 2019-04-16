using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;
//using Mineware.Systems.Production;
using System.Threading;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global;
using DevExpress.XtraEditors;
using Mineware.Systems.ProductionGlobal;
using System.IO;
using Mineware.Systems.Global;
using System.Text;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.Reports.Planning_Protocol_Report;

namespace Mineware.Systems.Reports
{

    public partial class ucPlanningProtocolReport : ucReportSettingsControl
    {
        //public System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        public     PlanningProtocolReportProperties reportSettings = new PlanningProtocolReportProperties();
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        private bool isDynamic;
        private bool ErrFound;
        public bool IsDynamic { get { return isDynamic; } }
        private bool isLocked;
        public bool IsLocked { get { return isLocked; } }
        private string theSectionid_2;
        public string TheSectionid_2 { get { return theSectionid_2; } }
        private string theProductionMonth;
        public string TheProductionMonth { get { return theProductionMonth; } }
        private string theActivity;
        public string TheActivity { get { return theActivity; } }
        string MS;
        string DL;
        private Report theReport = new Report();
        private frmLockedPLanning lp = new frmLockedPLanning();
        public Boolean RunCheck = true;
        public ucPlanningProtocolReport()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.ShowToolBar = false;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = false;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
              
           

        }

        public override bool prepareReport()
        {


            bool theResult = false;

            ErrFound = false;
            CheckForErrors();

            if (ErrFound == false)
            {

                //if (reportSettings.UserID == null)
                //{


                //    sysMessages.viewMessage(Global.MessageType.Info, "Select Value", "Please make a user selection for the report.", Global.ButtonTypes.OK, Global.MessageDisplayType.FullScreen);
                //    theResult = false;
                //}
                // else
                // {
                //   _ucUserActivityReport.prepareReport(reportSettings);
                // DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                theReportThread = new Thread(new ParameterizedThreadStart(createReport));
                theReportThread.SetApartmentState(ApartmentState.STA);
                theReportThread.Start(reportSettings);
                theResult = true;
                //  }
            }
            return theResult;
        }

        private void indecateDataStatus(string theProdmonth, string theSection, string theActivitycode)
        {
            //MWDataManager.clsDataAccess _DataStatus = new MWDataManager.clsDataAccess();
            //_DataStatus.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_DataStatus.SqlStatement = "SELECT 'L' theStatus, COUNT(Plancode) TheCount FROM PLANMONTH " +
            //                           "WHERE SectionID_2 = '" + theSection + "' and " +
            //                           "Prodmonth = " + theProdmonth  + "  and " +
            //                           "Activity = " + theActivitycode + " and " +
            //                           "Plancode = 'MP' AND LOCKED=1" +
            //                           "UNION " +
            //                           "SELECT 'D' theStatus,COUNT(Plancode) TheCount FROM PLANMONTH " +
            //                           "WHERE SectionID_2 = '" + theSection + "' and " +
            //                           "Prodmonth = " + theProdmonth + " and " +
            //                           "Activity = " + theActivitycode + " and " +
            //                           "Plancode='MP' AND LOCKED=0";
            //_DataStatus.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_DataStatus.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_DataStatus.ExecuteInstruction();

            //foreach (DataRow r in _DataStatus.ResultsDataTable.Rows)
            //{
            //    if (r["theStatus"].ToString() == "D")
            //    {
            //        rpPlanningProtocolData.Items [0].Description = string .Format("Dynamic ({0})", r["TheCount"]);
            //      //  rgDataStatus.Properties.Items[0].Description = String.Format("Dynamic ({0})", r["TheCount"]);
            //        if (r["TheCount"].ToString() == "0")
            //        {
            //            rpPlanningProtocolData.Items [0].Enabled =false ;
            //           // rgDataStatus.Properties.Items[0].Enabled = false;
            //        }
            //        else {  rpPlanningProtocolData.Items [0].Enabled = true; }
            //    }

                //if (r["theStatus"].ToString() == "L")
                //{
                //     rpPlanningProtocolData.Items [1].Description = String.Format("Locked ({0})", r["TheCount"]);
                //    if (r["TheCount"].ToString() == "0")
                //    {
                //         rpPlanningProtocolData.Items [1].Enabled=false ;
                //       // rgDataStatus.Properties.Items[1].Enabled = false;
                //    }
                //    else { rpPlanningProtocolData.Items [1].Enabled = true;  }
                //}

            //}

            //if ( rpPlanningProtocolData.Items [0].Enabled == false && rpPlanningProtocolData.Items [1].Enabled == true)
            //{
            //   reportSettings .PPD ="Locked";
            //   // rgDataStatus.SelectedIndex = 1;
            //    isLocked = true;
            //    isDynamic = false;
            //}

            //if ( rpPlanningProtocolData.Items [0].Enabled == true &&  rpPlanningProtocolData.Items [1].Enabled == false)
            //{
                reportSettings.PPD ="Dynamic";
               // rgDataStatus.SelectedIndex = 0;
                isLocked = false;
                isDynamic = true;
            //}

            //if ( rpPlanningProtocolData.Items [0].Enabled == true &&  rpPlanningProtocolData.Items [1].Enabled== true)
            //{
                // reportSettings.PPD ="Dynamic";
                //rgDataStatus.SelectedIndex = 0;
                //isLocked = false;
                //isDynamic = true;
            //}
        }

        private void CheckForErrors()
        {
            if (RunCheck == true)
            {
                bool theError = false;
                if (reportSettings.SECTIONID_2 == null && reportSettings.WORKPLACEID == null)
                {

                    _sysMessagesClass.viewMessage(MessageType.Info, "Planning Protocol", "Please Select a Section or Workplace", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                    theError = true;
                }
            }
        }

        private DataTable getMOSection(string prodmonth)
        {
            MWDataManager.clsDataAccess _Shafts = new MWDataManager.clsDataAccess();
            _Shafts.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _Shafts.SqlStatement = "SELECT DISTINCT SECTIONID_2,NAME_2 FROM dbo.SECTION_COMPLETE " +
                                   "WHERE PRODMONTH = " + prodmonth + " ORDER BY NAME_2";
            _Shafts.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _Shafts.queryReturnType = MWDataManager.ReturnType.DataTable;
            _Shafts.ExecuteInstruction();

            return _Shafts.ResultsDataTable;
        }

        private SqlParameter[] SetParamters(Int32 TemplateID, string WORKPLACEID,  Int32 PRODMONTH, string SectionID_2, int ActicityCode)
        {
            MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();

            SqlParameter[] _paramCollection = 
                            {
                             theData.CreateParameter("@PRODMONTH", SqlDbType.Int, 0,PRODMONTH),                             
                             theData.CreateParameter("@SectionID_2", SqlDbType.VarChar, 30,SectionID_2),    
                             theData.CreateParameter("@Activity", SqlDbType.Int, 30,ActicityCode),
                             //theData.CreateParameter("@BlankReport", SqlDbType.Int, 30,0),
                            };
            return _paramCollection;
        }

        private SqlParameter[] SetParamters_Detail(Int32 TemplateID, string WORKPLACEID, Int32 PRODMONTH, string SectionID_2, int ActicityCode)
        {
            MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();

            SqlParameter[] _paramCollection =
                            {
                             theData.CreateParameter("@PRODMONTH", SqlDbType.Int, 0,PRODMONTH),
                             theData.CreateParameter("@SectionID_2", SqlDbType.VarChar, 30,SectionID_2),
                             //theData.CreateParameter("@BlankReport", SqlDbType.Int, 30,0),
                            };
            return _paramCollection;
        }



        //public Report approveReport(string SelectedWP, Int32 ProdMonth,String activity)
        //{
        //Report PlanProt = new Report();


        //if (activity == "0")
        //{
        //  //  theLoadScreen.UpdateMessage("Loading Stoping Data");
        //    try
        //    {                
        //        PlanProt = buildStopeReport(SelectedWP, "NONE", ProdMonth);
        //        PlanProt.SetParameterValue("ReportDir",TGlobalItems .ReportsFolder + "\\Planning_Minutes\\");
        //        PlanProt.SetParameterValue("locked", "1");    
        //    }
        //    catch (Exception _exception)
        //    {
        //   //     theLoadScreen.ThreadStop();
        //        throw new ApplicationException(_exception.Message, _exception);
        //    }
        //}
        //else
        //{
        // //   theLoadScreen.UpdateMessage("Loading Development Data");
        //    try
        //    {
        //        PlanProt = buildDevReport(SelectedWP, "NONE", ProdMonth);
        //        PlanProt.SetParameterValue("ReportDir", TGlobalItems.ReportsFolder + "\\Planning_Minutes\\");
        //        PlanProt.SetParameterValue("locked", "1");  
        //    }
        //    catch (Exception _exception)
        //    {
        ////        theLoadScreen.ThreadStop();
        //        throw new ApplicationException(_exception.Message, _exception);
        //    }
        //}

        //return PlanProt;
        // }


        //public Report getReport(lodingData theLoadScreen)
        //{

        //    Report PlanProt = new Report();
        //    string SelectedWP = "", SelectedMO = "";
        //    if (rgPrintAccordingTo.SelectedIndex == 0)
        //    {
        //        SelectedWP = "NONE";
        //        SelectedMO = editMoSection.EditValue.ToString();
        //    }
        //    else
        //    {
        //        if (rgPrintAccordingTo.SelectedIndex == 1)
        //        {
        //            SelectedWP = editWorplace.EditValue.ToString();
        //            SelectedMO = "NONE";

        //        }
        //    }
        //    if (editActivity.Text.ToString() == "Stoping")
        //    {
        //        theLoadScreen.UpdateMessage("Loading Stoping Data");
        //        try
        //        {
        //            PlanProt = buildStopeReport(SelectedWP, SelectedMO, Convert.ToInt32(editProdMonth.Value.ToString()));


        //        }
        //        catch (Exception _exception)
        //        {
        //            theLoadScreen.ThreadStop();
        //            throw new ApplicationException(_exception.Message, _exception);
        //        }
        //    }
        //    else
        //    {
        //        theLoadScreen.UpdateMessage("Loading Development Data");
        //        try
        //        {
        //            PlanProt = buildDevReport(SelectedWP, SelectedMO, Convert.ToInt32(editProdMonth.Value.ToString()));
        //        }
        //        catch (Exception _exception)
        //        {
        //            theLoadScreen.ThreadStop();
        //            throw new ApplicationException(_exception.Message, _exception);
        //        }
        //    }

        //    return PlanProt;
        //}


        private void editWorplace_Enter(object sender, EventArgs e)
        {
            //int activity = Convert.ToInt32(editActivity.EditValue.ToString());
            //MWDataManager.clsDataAccess WPData = new MWDataManager.clsDataAccess();
            //WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //string Activity;
            //if (activity == 0)
            //{
            //    Activity = "S";
            //}
            //else { Activity = "D"; }
            //WPData.SqlStatement = "SELECT WP.WORKPLACEID,WP.DESCRIPTION FROM dbo.WORKPLCE WP " +
            //                      "WHERE WP.[Type] = '" + Activity + "' AND " +
            //                      "(WP.DELETED <> 'Y' OR " +
            //                      "WP.DELETED IS NULL)";
            //WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //WPData.ExecuteInstruction();

            //editWorplace.Properties.DataSource = WPData.ResultsDataTable;
            //editWorplace.Properties.DisplayMember = "DESCRIPTION";
            //editWorplace.Properties.ValueMember = "WORKPLACEID";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void editProdMonth_EditValueChanged_1(object sender, EventArgs e)
        {
            //Decimal month = editProdMonth.Value;
            //String PMonth = month.ToString();
            //PMonth.Substring(4, 2);
            //if (Convert.ToInt32(PMonth.Substring(4, 2)) > 12)
            //{
            //    // MessageBox.Show(PMonth);
            //    int M = Convert.ToInt32(PMonth.Substring(0, 4));
            //    M++;
            //    PMonth = M.ToString();
            //    PMonth = PMonth + "01";
            //    editProdMonth.Value = Convert.ToInt32(PMonth);
            //}
            //else
            //{
            //    if (Convert.ToInt32(PMonth.Substring(4, 2)) < 1)
            //    {
            //        int M = Convert.ToInt32(PMonth.Substring(0, 4));
            //        M--;
            //        PMonth = M.ToString();
            //        PMonth = PMonth + "12";
            //        editProdMonth.Value = Convert.ToDecimal(PMonth);
            //    }
            //}

            //theProductionMonth = editProdMonth.Value.ToString();

            //if (editMoSection.EditValue != null && editActivity.EditValue != null)
            //{
            //    indecateDataStatus(editProdMonth.Value.ToString(), editMoSection.EditValue.ToString(), editActivity.EditValue.ToString());
            //}

        }

        private void editMoSection_Properties_EditValueChanged(object sender, EventArgs e)
        {
            //theSectionid_2 = editMoSection.EditValue.ToString();
            //indecateDataStatus(editProdMonth.Text, editMoSection.EditValue.ToString(),editActivity.EditValue.ToString());
        }

        private void editActivity_EditValueChanged(object sender, EventArgs e)
        {
            //theActivity = editActivity.EditValue.ToString();
            //if (editMoSection.EditValue != null && editActivity.EditValue != null)
            //{                
                
            //    indecateDataStatus(editProdMonth.Text, editMoSection.EditValue.ToString(), editActivity.EditValue.ToString());
            //}
        }

        private void rgPrintAccordingTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if (rgPrintAccordingTo.SelectedIndex == 0)
            //{
            //    editMoSection.Enabled = true;
            //    editWorplace.Enabled = false;

            //}
            //else
            //{
            //    editMoSection.Enabled = false;
            //    editWorplace.Enabled = true;
            //}

        }

        private void rgDataStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (rgDataStatus.SelectedIndex == 1)
            //{
            //    isDynamic = false;
            //    isLocked = true;
            //}
            //if (rgDataStatus.SelectedIndex == 0)
            //{
            //    isDynamic = true;
            //    isLocked = false;
            //}
        }

        private void ucPlanningProtocolReport_Load(object sender, EventArgs e)
        {
            try
            {
                reportSettings.UserCurrentInfo = this.UserCurrentInfo;
                reportSettings.systemDBTag = this.theSystemDBTag;
                reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
                reportSettings .UpdateMOWPSelectionRequest +=reportSettings_UpdateMOWPSelectionRequest;
                reportSettings.UpdatePlanningDataRequest += reportSettings_UpdatePlanningDataRequest;
                reportSettings .UpdateWorkplceRequest +=reportSettings_UpdateWorkplceRequest;


               
                //reportSettings.Code = "";
                //reportSettings.SECTIONID_2 = "";

                //  editProdMonth.Value = TSystemSettings.CurrentProductionMonth;

                CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();

                BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
                BMEBL.SetsystemDBTag = theSystemDBTag;
                BMEBL.SetUserCurrentInfo = UserCurrentInfo;
                if (BMEBL.get_ReviseActivityPlan() == true)
                {
                    rpActivity.DataSource = BMEBL.ResultsDataTable;
                    rpActivity.DisplayMember = "Desc";
                    rpActivity.ValueMember = "Code";
                    reportSettings.Code = BMEBL.ResultsDataTable.Rows[0]["Code"].ToString();
                    //  reportSettings.Code = "Stoping";
                    //  this.tscbShaft.Items.Add("MINE");
                    //editActivity.Properties.DataSource = BMEBL.ResultsDataTable;
                    //editActivity.Properties.DisplayMember = "Desc";
                    //editActivity.Properties.ValueMember = "Code";
                    //editActivity.ItemIndex = 1;

                }
                DataTable Sections = getMOSection(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
                rpMOSection.DataSource = getMOSection(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
                rpMOSection.DisplayMember = "NAME_2";
                rpMOSection.ValueMember = "SECTIONID_2";
                //reportSettings.SECTIONID_2 = Sections.Rows[0]["SECTIONID_2"].ToString();

                //   reportSettings.SECTIONID_2 = "";
                //editMoSection.Properties.DataSource = getMOSection(editProdMonth.Value.ToString());
                //editMoSection.Properties.DisplayMember = "NAME_2";
                //editMoSection.Properties.ValueMember = "SECTIONID_2";
                //rpActivity.EditValueChanged += rpActivity_EditValueChanged;
                //rpMOSection.EditValueChanged += rpMOSection_EditValueChanged;
                reportSettings.Print = "MO Section";
                  iMOSection .Enabled =true ;
                  iMOSection.Visible = true;

                   //editMoSection.Enabled = true;
                   // editWorplace.Enabled = false;

         
              



               
               
                //rpActivity.EditValueChanged += rpActivity_EditValueChanged;
                //rpMOSection.EditValueChanged += rpMOSection_EditValueChanged;
                pgPlanningProtocolReport.SelectedObject = reportSettings;
            }

            catch (Exception)
            {
                
                
            }
        }

        private void reportSettings_UpdateWorkplceRequest(object sender, PlanningProtocolReportProperties.UpdateWorkplceArg e)
        {



        }

        private void reportSettings_UpdateMOWPSelectionRequest(object sender, PlanningProtocolReportProperties.UpdateMOWPSelectionArg e)
        {
           

            if (reportSettings .Print  == "MO Section")
            {
                iMOSection.Enabled = true;
                iMOSection.Visible = true;
                //editMoSection.Enabled = true;
                // editWorplace.Enabled = false;

            }
            else
            {
                iMOSection.Enabled = false;
                iMOSection.Visible = false;
                //editMoSection.Enabled = false;
                //editWorplace.Enabled = true;
            }

           
            pgPlanningProtocolReport.FocusLast();
        }

        private void reportSettings_UpdatePlanningDataRequest(object sender, PlanningProtocolReportProperties.UpdatePlanningDataArg e)
        {

            foreach (DataRow r in reportSettings.Indicatestatus.Rows)
            {
                if (r["theStatus"].ToString() == "D")
                {
                    rpPlanningProtocolData.Items[0].Description = string.Format("Dynamic ({0})", r["TheCount"]);
                    //  rgDataStatus.Properties.Items[0].Description = String.Format("Dynamic ({0})", r["TheCount"]);
                    if (r["TheCount"].ToString() == "0")
                    {
                        rpPlanningProtocolData.Items[0].Enabled = false;
                        // rgDataStatus.Properties.Items[0].Enabled = false;
                    }
                    else { rpPlanningProtocolData.Items[0].Enabled = true; }
                }

                if (r["theStatus"].ToString() == "L")
                {
                    rpPlanningProtocolData.Items[1].Description = String.Format("Locked ({0})", r["TheCount"]);
                    if (r["TheCount"].ToString() == "0")
                    {
                        rpPlanningProtocolData.Items[1].Enabled = false;
                        // rgDataStatus.Properties.Items[1].Enabled = false;
                    }
                    else { rpPlanningProtocolData.Items[1].Enabled = true; }
                }

            }

            if (rpPlanningProtocolData.Items[0].Enabled == false && rpPlanningProtocolData.Items[1].Enabled == true)
            {
                reportSettings.PPD = "Dynamic";
                // rgDataStatus.SelectedIndex = 1;
                isLocked = false;
                isDynamic = true;
            }

            if (rpPlanningProtocolData.Items[0].Enabled == true && rpPlanningProtocolData.Items[1].Enabled == false)
            {
                reportSettings.PPD = "Dynamic";
                // rgDataStatus.SelectedIndex = 0;
                isLocked = false;
                isDynamic = true;
            }

            if (rpPlanningProtocolData.Items[0].Enabled == true && rpPlanningProtocolData.Items[1].Enabled == true)
            {
                reportSettings.PPD = "Dynamic";
                //rgDataStatus.SelectedIndex = 0;
                isLocked = false;
                isDynamic = true;
            }



        }

        public  void createReport(Object theReportSettings)
        {
            
            theSectionid_2 = reportSettings.SECTIONID_2;
                string section;
                string wp;
                string dynamic;
                string locked;
                string SelectedWP = "", SelectedMO = "";
               clsDataResult Datadr;
                StringBuilder SB = new StringBuilder();
                reportSettings = theReportSettings as PlanningProtocolReportProperties;
                if (reportSettings.Print == "MO Section")
                {
                    SelectedWP = "NONE";
                    SelectedMO = TheSectionid_2;
                }
                else
                {
                    if (reportSettings.Print == "Workplace")
                    {
                        SelectedWP = reportSettings.WORKPLACEID;
                        SelectedMO = "NONE";

                    }
                }
                //if (MS == "MO Section")
                //{
                //}

                SqlParameter[] _paramCollection;
                if (reportSettings.PPD != "Locked")
                {

                    ActiveReport.ShowToolBar = true;
                    if (reportSettings.Code == "0")
                    {



                        MWDataManager.clsDataAccess _WPList = new MWDataManager.clsDataAccess();
                        try
                        {
                            _WPList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _WPList.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _WPList.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _WPList.SqlStatement = "sp_PlanProd_WPList";
                            _WPList.ResultsTableName = "WPList";

                            _paramCollection = SetParamters(2000, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                            _WPList.ParamCollection = _paramCollection;
                            _WPList.ExecuteInstruction();
                        }
                        catch (Exception _exception)
                        {
                            throw new ApplicationException("WPList:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtStopeSaftey = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtStopeSaftey.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtStopeSaftey.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtStopeSaftey.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtStopeSaftey.SqlStatement = "sp_PlanProd_Saftey_STP";
                            _PlanProtStopeSaftey.ResultsTableName = "STOPING_SAFTEY";

                            _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                            _PlanProtStopeSaftey.ParamCollection = _paramCollection;
                            Datadr = _PlanProtStopeSaftey.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPPING_SAFTEY:" + _exception.Message, _exception);
                        }

                        MWDataManager.clsDataAccess _Engineering_Equipping_STP = new MWDataManager.clsDataAccess();
                        try
                        {
                        _Engineering_Equipping_STP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Engineering_Equipping_STP.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Engineering_Equipping_STP.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Engineering_Equipping_STP.SqlStatement = "sp_Engineering_Equipping_STP";
                        _Engineering_Equipping_STP.ResultsTableName = "Equipping_STP";

                            _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Engineering_Equipping_STP.ParamCollection = _paramCollection;
                        _Engineering_Equipping_STP.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: Engineering_Equipping_STP:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _Latest_RME_Report_STP = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Latest_RME_Report_STP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Latest_RME_Report_STP.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Latest_RME_Report_STP.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Latest_RME_Report_STP.SqlStatement = "Sp_Latest_RME_Report_Number_STP";
                        _Latest_RME_Report_STP.ResultsTableName = "Latest_RME_Report_STP";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Latest_RME_Report_STP.ParamCollection = _paramCollection;
                        _Latest_RME_Report_STP.ExecuteInstruction();
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Latest_RME_Recommendation_Number_STP:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Latest_RME_Recommendation_Number_STP = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Latest_RME_Recommendation_Number_STP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Latest_RME_Recommendation_Number_STP.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Latest_RME_Recommendation_Number_STP.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Latest_RME_Recommendation_Number_STP.SqlStatement = "sp_Latest_RME_Recommendation_Number_STP";
                        _Latest_RME_Recommendation_Number_STP.ResultsTableName = "Latest_RME_Recommendation_STP";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Latest_RME_Recommendation_Number_STP.ParamCollection = _paramCollection;
                        _Latest_RME_Recommendation_Number_STP.ExecuteInstruction();
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Latest_RME_Report_ST:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Latest_Ventilation_Report_Number_STP = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Latest_Ventilation_Report_Number_STP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Latest_Ventilation_Report_Number_STP.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Latest_Ventilation_Report_Number_STP.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Latest_Ventilation_Report_Number_STP.SqlStatement = "sp_Latest_Ventilation_Report_Number_STP";
                        _Latest_Ventilation_Report_Number_STP.ResultsTableName = "Latest_Ventilation_Number_STP";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Latest_Ventilation_Report_Number_STP.ParamCollection = _paramCollection;
                        _Latest_Ventilation_Report_Number_STP.ExecuteInstruction();
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Latest_Ventilation_Report_Number_STP:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Latest_Geology_Report_Number_STP = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Latest_Geology_Report_Number_STP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Latest_Geology_Report_Number_STP.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Latest_Geology_Report_Number_STP.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Latest_Geology_Report_Number_STP.SqlStatement = "sp_Latest_Geology_Report_Number_STP";
                        _Latest_Geology_Report_Number_STP.ResultsTableName = "Latest_Geology_Report_Number_STP";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Latest_Geology_Report_Number_STP.ParamCollection = _paramCollection;
                        _Latest_Geology_Report_Number_STP.ExecuteInstruction();
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Latest_Geology_Report_Number_STP:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Latest_Survey_Note_Number_STP = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Latest_Survey_Note_Number_STP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Latest_Survey_Note_Number_STP.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Latest_Survey_Note_Number_STP.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Latest_Survey_Note_Number_STP.SqlStatement = "sp_Latest_Survey_Note_Number_STP";
                        _Latest_Survey_Note_Number_STP.ResultsTableName = "Latest_Survey_Note_Number_STP";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Latest_Survey_Note_Number_STP.ParamCollection = _paramCollection;
                        clsDataResult dr = _Latest_Survey_Note_Number_STP.ExecuteInstruction();
                        
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Latest_Survey_Note_Number_STP:" + _exception.Message, _exception);
                    }


                    MWDataManager.clsDataAccess _Human_Resources_STP = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Human_Resources_STP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Human_Resources_STP.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Human_Resources_STP.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Human_Resources_STP.SqlStatement = "sp_Human_Resources_STP";
                        _Human_Resources_STP.ResultsTableName = "Human_Resources_STP";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Human_Resources_STP.ParamCollection = _paramCollection;
                        clsDataResult dr = _Human_Resources_STP.ExecuteInstruction();

                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Human_Resources_STP:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Finance_STP = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Finance_STP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Finance_STP.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Finance_STP.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Finance_STP.SqlStatement = "sp_Finance_STP";
                        _Finance_STP.ResultsTableName = "Finance_STP";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Finance_STP.ParamCollection = _paramCollection;
                        clsDataResult dr = _Finance_STP.ExecuteInstruction();

                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Finance_STP:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Production_STP = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Production_STP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Production_STP.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Production_STP.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Production_STP.SqlStatement = "sp_Production_STP";
                        _Production_STP.ResultsTableName = "Production_STP";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Production_STP.ParamCollection = _paramCollection;
                        clsDataResult dr = _Production_STP.ExecuteInstruction();

                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Production_STP:" + _exception.Message, _exception);
                    }
                    

                    MWDataManager.clsDataAccess _Cycle_STP = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Cycle_STP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Cycle_STP.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Cycle_STP.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Cycle_STP.SqlStatement = "sp_Get_Cycle_Protocol";
                        _Cycle_STP.ResultsTableName = "Cycle_Protocol";

                        _paramCollection = SetParamters(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Cycle_STP.ParamCollection = _paramCollection;
                        clsDataResult dr = _Cycle_STP.ExecuteInstruction();

                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Cycle_STP:" + _exception.Message, _exception);
                    }


                    DataSet repMODDataSet = new DataSet();
                        repMODDataSet.Tables.Clear();
                        theReport.Clear();
                        // add data tables to report

                        repMODDataSet.Tables.Add(_WPList.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtStopeSaftey.ResultsDataTable);
                        repMODDataSet.Tables.Add(_Engineering_Equipping_STP.ResultsDataTable);
                        repMODDataSet.Tables.Add(_Latest_RME_Report_STP.ResultsDataTable);
                        repMODDataSet.Tables.Add(_Latest_RME_Recommendation_Number_STP.ResultsDataTable);
                        repMODDataSet.Tables.Add(_Latest_Ventilation_Report_Number_STP.ResultsDataTable);
                        repMODDataSet.Tables.Add(_Latest_Geology_Report_Number_STP.ResultsDataTable);
                        repMODDataSet.Tables.Add(_Latest_Survey_Note_Number_STP.ResultsDataTable);
                        repMODDataSet.Tables.Add(_Human_Resources_STP.ResultsDataTable);
                        repMODDataSet.Tables.Add(_Finance_STP.ResultsDataTable);
                        repMODDataSet.Tables.Add(_Cycle_STP.ResultsDataTable);
                        repMODDataSet.Tables.Add(_Production_STP.ResultsDataTable);


                    theReport.RegisterData(repMODDataSet);


                       theReport.Load(TGlobalItems.ReportsFolder + "\\Harmony Protocol.frx");



                        // theReport.SetParameterValue("val", 0); 
                        if (TParameters.DesignReport)
                            theReport.Design();
                        theReport.Prepare();

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                        ActiveReport.SetReport = theReport;
                        ActiveReport.isDone = true;


                        //}
                        //else
                        //{
                        //    MessageBox.Show("Unable to load report file " + TSystemSettings.ReportDir + "\\PlanProtStope.frx", "", MessageBoxButtons.OK);
                        //}

                        // return PlanProt;
                    }
                    else 
                    if (reportSettings.Code == "1")
                    {
                    MWDataManager.clsDataAccess _WPList = new MWDataManager.clsDataAccess();
                    try
                    {
                        _WPList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _WPList.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _WPList.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _WPList.SqlStatement = "sp_PlanProd_WPList";
                        _WPList.ResultsTableName = "WPList";

                        _paramCollection = SetParamters(2000, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 1);

                        _WPList.ParamCollection = _paramCollection;
                        _WPList.ExecuteInstruction();
                    }
                    catch (Exception _exception)
                    {
                        throw new ApplicationException("WPList:" + _exception.Message, _exception);
                    }


                    MWDataManager.clsDataAccess _PlanProtStopeSaftey = new MWDataManager.clsDataAccess();
                    try
                    {
                        _PlanProtStopeSaftey.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _PlanProtStopeSaftey.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _PlanProtStopeSaftey.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _PlanProtStopeSaftey.SqlStatement = "sp_PlanProd_Saftey_DEV";
                        _PlanProtStopeSaftey.ResultsTableName = "STOPING_SAFTEY";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _PlanProtStopeSaftey.ParamCollection = _paramCollection;
                        _PlanProtStopeSaftey.ExecuteInstruction();
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: STOPPING_SAFTEY:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Engineering_Equipping_DEV = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Engineering_Equipping_DEV.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Engineering_Equipping_DEV.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Engineering_Equipping_DEV.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Engineering_Equipping_DEV.SqlStatement = "sp_Engineering_Equipping_DEV";
                        _Engineering_Equipping_DEV.ResultsTableName = "Equipping_DEV";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Engineering_Equipping_DEV.ParamCollection = _paramCollection;
                        _Engineering_Equipping_DEV.ExecuteInstruction();
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Engineering_Equipping_DEV:" + _exception.Message, _exception);
                    }


                    MWDataManager.clsDataAccess _Latest_RME_Report_DEV = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Latest_RME_Report_DEV.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Latest_RME_Report_DEV.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Latest_RME_Report_DEV.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Latest_RME_Report_DEV.SqlStatement = "Sp_Latest_RME_Report_Number_DEV";
                        _Latest_RME_Report_DEV.ResultsTableName = "Latest_RME_Report_DEV";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Latest_RME_Report_DEV.ParamCollection = _paramCollection;
                        _Latest_RME_Report_DEV.ExecuteInstruction();
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Latest_RME_Recommendation_Number_DEV:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Latest_RME_Recommendation_Number_DEV = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Latest_RME_Recommendation_Number_DEV.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Latest_RME_Recommendation_Number_DEV.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Latest_RME_Recommendation_Number_DEV.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Latest_RME_Recommendation_Number_DEV.SqlStatement = "sp_Latest_RME_Recommendation_Number_DEV";
                        _Latest_RME_Recommendation_Number_DEV.ResultsTableName = "Latest_RME_Recommendation_DEV";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Latest_RME_Recommendation_Number_DEV.ParamCollection = _paramCollection;
                        clsDataResult dr = _Latest_RME_Recommendation_Number_DEV.ExecuteInstruction();
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Latest_RME_Report_ST:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Latest_Ventilation_Report_Number_DEV = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Latest_Ventilation_Report_Number_DEV.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Latest_Ventilation_Report_Number_DEV.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Latest_Ventilation_Report_Number_DEV.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Latest_Ventilation_Report_Number_DEV.SqlStatement = "sp_Latest_Ventilation_Report_Number_DEV";
                        _Latest_Ventilation_Report_Number_DEV.ResultsTableName = "Latest_Ventilation_Number_DEV";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Latest_Ventilation_Report_Number_DEV.ParamCollection = _paramCollection;
                        _Latest_Ventilation_Report_Number_DEV.ExecuteInstruction();
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Latest_Ventilation_Report_Number_DEV:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Latest_Geology_Report_Number_DEV = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Latest_Geology_Report_Number_DEV.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Latest_Geology_Report_Number_DEV.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Latest_Geology_Report_Number_DEV.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Latest_Geology_Report_Number_DEV.SqlStatement = "sp_Latest_Geology_Report_Number_DEV";
                        _Latest_Geology_Report_Number_DEV.ResultsTableName = "Latest_Geology_Report_Number_DEV";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Latest_Geology_Report_Number_DEV.ParamCollection = _paramCollection;
                        _Latest_Geology_Report_Number_DEV.ExecuteInstruction();
                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Latest_Geology_Report_Number_DEV:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Latest_Survey_Note_Number_DEV = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Latest_Survey_Note_Number_DEV.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Latest_Survey_Note_Number_DEV.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Latest_Survey_Note_Number_DEV.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Latest_Survey_Note_Number_DEV.SqlStatement = "sp_Latest_Survey_Note_Number_DEV";
                        _Latest_Survey_Note_Number_DEV.ResultsTableName = "Latest_Survey_Note_Number_DEV";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Latest_Survey_Note_Number_DEV.ParamCollection = _paramCollection;
                        clsDataResult dr = _Latest_Survey_Note_Number_DEV.ExecuteInstruction();

                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Latest_Survey_Note_Number_DEV:" + _exception.Message, _exception);
                    }


                    MWDataManager.clsDataAccess _Human_Resources_DEV = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Human_Resources_DEV.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Human_Resources_DEV.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Human_Resources_DEV.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Human_Resources_DEV.SqlStatement = "sp_Human_Resources_DEV";
                        _Human_Resources_DEV.ResultsTableName = "Human_Resources_DEV";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Human_Resources_DEV.ParamCollection = _paramCollection;
                        clsDataResult dr = _Human_Resources_DEV.ExecuteInstruction();

                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Human_Resources_DEV:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Finance_DEV = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Finance_DEV.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Finance_DEV.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Finance_DEV.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Finance_DEV.SqlStatement = "sp_Finance_DEV";
                        _Finance_DEV.ResultsTableName = "Finance_DEV";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Finance_DEV.ParamCollection = _paramCollection;
                        clsDataResult dr = _Finance_DEV.ExecuteInstruction();

                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Finance_DEV:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Development_Equipping_Dev = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Development_Equipping_Dev.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Development_Equipping_Dev.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Development_Equipping_Dev.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Development_Equipping_Dev.SqlStatement = "sp_Development_Equipping_Dev";
                        _Development_Equipping_Dev.ResultsTableName = "Development_Equipping_Dev";

                        _paramCollection = SetParamters_Detail(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Development_Equipping_Dev.ParamCollection = _paramCollection;
                        clsDataResult dr = _Development_Equipping_Dev.ExecuteInstruction();

                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Finance_DEV:" + _exception.Message, _exception);
                    }

                    MWDataManager.clsDataAccess _Cycle_Dev = new MWDataManager.clsDataAccess();
                    try
                    {
                        _Cycle_Dev.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _Cycle_Dev.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _Cycle_Dev.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _Cycle_Dev.SqlStatement = "sp_Get_Cycle_Protocol_Dev";
                        _Cycle_Dev.ResultsTableName = "Cycle_Protocol";

                        _paramCollection = SetParamters(11, SelectedWP, Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                        _Cycle_Dev.ParamCollection = _paramCollection;
                        clsDataResult dr = _Cycle_Dev.ExecuteInstruction();

                    }

                    catch (Exception _exception)
                    {
                        throw new ApplicationException("Report Section: Cycle_Dev:" + _exception.Message, _exception);
                    }

                    DataSet repMODDataSet = new DataSet();
                    repMODDataSet.Tables.Clear();
                    theReport.Clear();
                    // add data tables to report

                    repMODDataSet.Tables.Add(_WPList.ResultsDataTable);
                    repMODDataSet.Tables.Add(_PlanProtStopeSaftey.ResultsDataTable);
                    repMODDataSet.Tables.Add(_Engineering_Equipping_DEV.ResultsDataTable);
                    repMODDataSet.Tables.Add(_Latest_RME_Report_DEV.ResultsDataTable);
                    repMODDataSet.Tables.Add(_Latest_RME_Recommendation_Number_DEV.ResultsDataTable);
                    repMODDataSet.Tables.Add(_Latest_Ventilation_Report_Number_DEV.ResultsDataTable);
                    repMODDataSet.Tables.Add(_Latest_Geology_Report_Number_DEV.ResultsDataTable);
                    repMODDataSet.Tables.Add(_Latest_Survey_Note_Number_DEV.ResultsDataTable);
                    repMODDataSet.Tables.Add(_Human_Resources_DEV.ResultsDataTable);
                    repMODDataSet.Tables.Add(_Finance_DEV.ResultsDataTable);
                    repMODDataSet.Tables.Add(_Cycle_Dev.ResultsDataTable);
                    repMODDataSet.Tables.Add(_Development_Equipping_Dev.ResultsDataTable);


                    theReport.RegisterData(repMODDataSet);


                    theReport.Load(TGlobalItems.ReportsFolder + "\\Harmony Protocol Dev.frx");

                    //theReport.Design();

                    // theReport.SetParameterValue("val", 0); 
                    if (TParameters.DesignReport)
                    theReport.Design();
                    theReport.Prepare();

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    ActiveReport.SetReport = theReport;
                    ActiveReport.isDone = true;
                }

                }
                else
                {

                    //theReport.Prepare();
                    ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.GenericControl;
                    copyReportToTemp(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth), TheSectionid_2, reportSettings.Code);
                    lp.setFileList(getData(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth), TheSectionid_2, reportSettings.Code));
                    lp.loadList(loadData(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth), TheSectionid_2, reportSettings.Code));
                    ActiveReport.SetReport = lp;
                    ActiveReport.ShowToolBar = false;
                    ActiveReport.isDone = true;

                }

        }

        public DataTable loadData(string theProdmonth, string theSectionID_2, string theActivity)
        {
            MWDataManager.clsDataAccess _WorkplaceList = new MWDataManager.clsDataAccess();
            try
            {
                _WorkplaceList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _WorkplaceList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _WorkplaceList.queryReturnType = MWDataManager.ReturnType.DataTable;
                _WorkplaceList.SqlStatement = "SELECT WorkplaceID,WorkplaceDesc FROM PlanMonth " +
                                              "WHERE SectionID_2 = '" + theSectionID_2 + "' and " +
                                              "Prodmonth = " + theProdmonth + " and " +
                                              "PlanCode = 'MP' and " + 
                                              "Activity = " + theActivity + " and " +
                                              "Locked = 1 ";
                _WorkplaceList.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }
            return _WorkplaceList.ResultsDataTable;

        }

        private DataTable getData(string theProdmonth, string theSectionID_2, string theActivity)
        {
         
            MWDataManager.clsDataAccess _TheFiles = new MWDataManager.clsDataAccess();
            try
            {
                _TheFiles.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _TheFiles.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _TheFiles.queryReturnType = MWDataManager.ReturnType.DataTable;
                _TheFiles.SqlStatement = "SELECT DISTINCT Prodmonth,Workplaceid,Sectionid_2,Activity,[FileName]  FROM PLANPROT_APPROVEDTEMPLATE " +
                                          "WHERE Prodmonth = " + theProdmonth + " and " +
                                          "SectionID_2 = '" + theSectionID_2 + "' and " +
                                          "Activity = " + theActivity;
                _TheFiles.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            return _TheFiles.ResultsDataTable;
        }

        private void copyReportToTemp(string theProdmonth, string theSectionID_2, string theActivity)
        {
            DataTable theFileList = getData(theProdmonth, theSectionID_2, theActivity);
            string tempFolder = System.Environment.GetEnvironmentVariable("TEMP");
            foreach (DataRow r in theFileList.Rows)
            {


                if (File.Exists(String.Format("{0}\\{1}", TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).PlanProtSaveDir, r["FileName"])))
                {
                    if (File.Exists(String.Format("{0}\\{1}", tempFolder, r["FileName"])))
                        File.Delete(String.Format("{0}\\{1}", tempFolder, r["FileName"]));

                    File.Copy(String.Format("{0}\\{1}", TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).PlanProtSaveDir, r["FileName"]), String.Format("{0}\\{1}", tempFolder, r["FileName"]));

                }
            }


        }

        private void rpPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DevExpress.XtraEditors.RadioGroup editor = (sender as DevExpress.XtraEditors.RadioGroup);
            ////  DataRowView row = editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue) as DataRowView;
            //MS = editor.Properties.GetDisplayText(editor.EditValue);

            //if(MS =="MO Section")
            //{
            //  iMOSection .Enabled =true ;
            //  iMOSection.Visible = true;
            //    iWPSelection .Enabled =false ;
            //    iWPSelection.Visible = false;
            //   //editMoSection.Enabled = true;
            //   // editWorplace.Enabled = false;

            //}
            //else
            //{
            //     iMOSection .Enabled =false ;
            //     iMOSection.Visible = false;
            //    iWPSelection .Enabled =true ;
            //    iWPSelection.Visible = true;
            //    //editMoSection.Enabled = false;
            //    //editWorplace.Enabled = true;
            //}

        }

        private void rpPlanningProtocolData_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.RadioGroup editor = (sender as DevExpress.XtraEditors.RadioGroup);
            //  DataRowView row = editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue) as DataRowView;
            DL = editor.Properties.GetDisplayText(editor.EditValue);
        }

        private void rpActivity_Enter(object sender, EventArgs e)
        {
         
        }


        private void rpMOSection_EditValueChanged(object sender, EventArgs e)
        {
           // DevExpress.XtraEditors.LookUpEdit editor = (sender as DevExpress.XtraEditors.LookUpEdit);
           // DataRowView row = editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue) as DataRowView;
           // object value = row["SECTIONID_2"];
           // theSectionid_2 = Convert.ToString(value);
           // reportSettings.SECTIONID_2 = theSectionid_2;
           // indecateDataStatus(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth), reportSettings.SECTIONID_2, reportSettings.Code);
           //// pgPlanningProtocolReport.SelectedObject = reportSettings;
            pgPlanningProtocolReport.FocusNext();
           
        }

        private void rpActivity_EditValueChanged(object sender, EventArgs e)
        {
           // DevExpress.XtraEditors.LookUpEdit editor = (sender as DevExpress.XtraEditors.LookUpEdit);
           // DataRowView row = editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue) as DataRowView;
           // object value = row["Code"];
           // theActivity = Convert.ToString(value);
           // reportSettings.Code = theActivity;
           //// theActivity = reportSettings.Code;
           // if (reportSettings.SECTIONID_2 != null && reportSettings.Code != null)
           // {

           //     indecateDataStatus(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth), reportSettings.SECTIONID_2, reportSettings.Code);
           
           // }
        }

        private void rpWPSelection_Enter(object sender, EventArgs e)
        {
            // int activity = Convert.ToInt32(editActivity.EditValue.ToString());
            //string ACTIVITY = reportSettings.Code;
            //MWDataManager.clsDataAccess WPData = new MWDataManager.clsDataAccess();
            //WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //string Activity;
            //if (ACTIVITY == "0")
            //{
            //    Activity = "S";
            //}
            //else { Activity = "D"; }
            //WPData.SqlStatement = "SELECT WP.WORKPLACEID,WP.DESCRIPTION FROM dbo.WORKPLCE WP " +
            //                      "WHERE WP.[Type] = '" + Activity + "' AND " +
            //                      "(WP.DELETED <> 'Y' OR " +
            //                      "WP.DELETED IS NULL)";
            //WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //WPData.ExecuteInstruction();

            //rpWPSelection.DataSource = WPData.ResultsDataTable; ;
            //rpWPSelection.DisplayMember = "DESCRIPTION";
            //rpWPSelection.ValueMember = "WORKPLACEID";
            //editWorplace.Properties.DataSource = WPData.ResultsDataTable;
            //editWorplace.Properties.DisplayMember = "DESCRIPTION";
            //editWorplace.Properties.ValueMember = "WORKPLACEID";

            //  pgPlanningProtocolReport .SelectedObject =reportSettings ;
        }

        public Report approveReport(object reportset, string connection)
        {
            bool theResult = false;

            ErrFound = false;
            CheckForErrors();

            if (ErrFound == false)
            {
                UserCurrentInfo.Connection = connection;

                reportSettings = reportset as PlanningProtocolReportProperties;
                theSectionid_2 = reportSettings.SECTIONID_2;
                theReportThread = new Thread(new ParameterizedThreadStart(createReport));
                theReportThread.SetApartmentState(ApartmentState.STA);
                theReportThread.Start(reportset);
                ActiveReport.ShowToolBar = false;
                // planprot = createReport(reportset );
                bool done = isDone();
                do
                {
                    done = isDone();
                    Application.DoEvents();
                } while (done != true);
                
            }

            return theReport;
        }

        private void pgPlanningProtocolReport_Click(object sender, EventArgs e)
        {

        }
    }
}
