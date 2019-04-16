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
using System.Threading;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global;
using DevExpress.XtraEditors;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.HarmonyPASGlobal;
using System.IO;
using Mineware.Systems.Global;
using System.Text;
using Mineware.Systems.Production.HarmonyPASReports;

namespace Mineware.Systems.HarmonyPASReports.Planning_Protocol_Report
{

    public partial class ucPlanningProtocolReportOld : ucReportSettingsControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        public     PlanningProtocolReportProperties reportSettings = new PlanningProtocolReportProperties();
        private Thread theReportThread;
        string theSystemDBTag = "CPM";
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
        public ucPlanningProtocolReportOld()
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
            MWDataManager.clsDataAccess _DataStatus = new MWDataManager.clsDataAccess();
            _DataStatus.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _DataStatus.SqlStatement = "SELECT 'L' theStatus, COUNT(Plancode) TheCount FROM PLANMONTH " +
                                       "WHERE SectionID_2 = '" + theSection + "' and " +
                                       "Prodmonth = " + theProdmonth  + "  and " +
                                       "Activity = " + theActivitycode + " and " +
                                       "Plancode = 'MP' AND LOCKED=1" +
                                       "UNION " +
                                       "SELECT 'D' theStatus,COUNT(Plancode) TheCount FROM PLANMONTH " +
                                       "WHERE SectionID_2 = '" + theSection + "' and " +
                                       "Prodmonth = " + theProdmonth + " and " +
                                       "Activity = " + theActivitycode + " and " +
                                       "Plancode='MP' AND LOCKED=0";
            _DataStatus.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _DataStatus.queryReturnType = MWDataManager.ReturnType.DataTable;
            _DataStatus.ExecuteInstruction();

            foreach (DataRow r in _DataStatus.ResultsDataTable.Rows)
            {
                if (r["theStatus"].ToString() == "D")
                {
                    rpPlanningProtocolData.Items [0].Description = string .Format("Dynamic ({0})", r["TheCount"]);
                  //  rgDataStatus.Properties.Items[0].Description = String.Format("Dynamic ({0})", r["TheCount"]);
                    if (r["TheCount"].ToString() == "0")
                    {
                        rpPlanningProtocolData.Items [0].Enabled =false ;
                       // rgDataStatus.Properties.Items[0].Enabled = false;
                    }
                    else {  rpPlanningProtocolData.Items [0].Enabled = true; }
                }

                if (r["theStatus"].ToString() == "L")
                {
                     rpPlanningProtocolData.Items [1].Description = String.Format("Locked ({0})", r["TheCount"]);
                    if (r["TheCount"].ToString() == "0")
                    {
                         rpPlanningProtocolData.Items [1].Enabled=false ;
                       // rgDataStatus.Properties.Items[1].Enabled = false;
                    }
                    else { rpPlanningProtocolData.Items [1].Enabled = true;  }
                }

            }

            if ( rpPlanningProtocolData.Items [0].Enabled == false && rpPlanningProtocolData.Items [1].Enabled == true)
            {
               reportSettings .PPD ="Locked";
               // rgDataStatus.SelectedIndex = 1;
                isLocked = true;
                isDynamic = false;
            }

            if ( rpPlanningProtocolData.Items [0].Enabled == true &&  rpPlanningProtocolData.Items [1].Enabled == false)
            {
                reportSettings .PPD ="Dynamic";
               // rgDataStatus.SelectedIndex = 0;
                isLocked = false;
                isDynamic = true;
            }

            if ( rpPlanningProtocolData.Items [0].Enabled == true &&  rpPlanningProtocolData.Items [1].Enabled== true)
            {
                 reportSettings .PPD ="Dynamic";
                //rgDataStatus.SelectedIndex = 0;
                isLocked = false;
                isDynamic = true;
            }
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
                             theData.CreateParameter("@TemplateID", SqlDbType.Int, 0,TemplateID),
                             theData.CreateParameter("@WORKPLACEID", SqlDbType.VarChar, 60,WORKPLACEID),
                             theData.CreateParameter("@PRODMONTH", SqlDbType.Int, 0,PRODMONTH),                             
                             theData.CreateParameter("@SectionID_2", SqlDbType.VarChar, 30,SectionID_2),    
                             theData.CreateParameter("@ActicityCode", SqlDbType.Int, 30,ActicityCode),
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
                reportSettings.Prodmonth = THarmonyPASGlobal.ProdMonthAsDate(THarmonyPASGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
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
                if (BMEBL.get_Activity() == true)
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
                DataTable Sections = getMOSection(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth));
                rpMOSection.DataSource = getMOSection(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth));
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
                    iWPSelection .Enabled =false ;
                    iWPSelection.Visible = false;
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

            rpWPSelection.DataSource = reportSettings.Workplce;
            //rpWPSelection.DataSource = WPData.ResultsDataTable; ;
            rpWPSelection.DisplayMember = "DESCRIPTION";
            rpWPSelection.ValueMember = "WORKPLACEID";

        }

        private void reportSettings_UpdateMOWPSelectionRequest(object sender, PlanningProtocolReportProperties.UpdateMOWPSelectionArg e)
        {
           

            if (reportSettings .Print  == "MO Section")
            {
                iMOSection.Enabled = true;
                iMOSection.Visible = true;
                iWPSelection.Enabled = false;
                iWPSelection.Visible = false;
                //editMoSection.Enabled = true;
                // editWorplace.Enabled = false;

            }
            else
            {
                iMOSection.Enabled = false;
                iMOSection.Visible = false;
                iWPSelection.Enabled = true;
                iWPSelection.Visible = true;
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
                reportSettings.PPD = "Locked";
                // rgDataStatus.SelectedIndex = 1;
                isLocked = true;
                isDynamic = false;
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
                            _WPList.SqlStatement = "sp_PlanProt_GetReportData";
                            _WPList.ResultsTableName = "WPList";

                            _paramCollection = SetParamters(2000, SelectedWP, Convert.ToInt32(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                            _WPList.ParamCollection = _paramCollection;
                            _WPList.ExecuteInstruction();
                        }
                        catch (Exception _exception)
                        {
                            throw new ApplicationException("WPList:" + _exception.Message, _exception);
                        }
                        //MWDataManager.clsDataAccess _WPListApproved = new MWDataManager.clsDataAccess();
                        //_WPListApproved.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        //_WPListApproved.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        //_WPListApproved.queryReturnType = MWDataManager.ReturnType.DataTable;
                        //_WPListApproved.SqlStatement = "sp_PlanProt_GetReportData";
                        //_WPListApproved.ResultsTableName = "WPListApproved";

                        //_paramCollection = SetParamters(4000, SelectedWP, ProdMonth, SelectedMO, 0);

                        //_WPListApproved.ParamCollection = _paramCollection;
                        //_WPListApproved.ExecuteInstruction();


                        MWDataManager.clsDataAccess _WPInfo = new MWDataManager.clsDataAccess();
                        try
                        {
                            _WPInfo.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _WPInfo.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _WPInfo.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _WPInfo.SqlStatement = "sp_PlanProt_GetReportData";
                            _WPInfo.ResultsTableName = "WPInfo";

                            _paramCollection = SetParamters(2001, SelectedWP, Convert.ToInt32(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);


                            _WPInfo.ParamCollection = _paramCollection;
                            _WPInfo.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: WPList:" + _exception.Message, _exception);
                        }



                        MWDataManager.clsDataAccess _HRDetailInfo = new MWDataManager.clsDataAccess();
                        _HRDetailInfo.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _HRDetailInfo.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _HRDetailInfo.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _HRDetailInfo.SqlStatement = "sp_HRGetPlanProtReportData";
                        _HRDetailInfo.ResultsTableName = "_HRDetailInfo";

                        try
                        {

                            SqlParameter[] _paramCollection2 =
                                {
                             _HRDetailInfo.CreateParameter("@PRODMONTH", SqlDbType.Int, 0, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth) ),
                             _HRDetailInfo.CreateParameter("@SectionID_2", SqlDbType.VarChar, 30,TheSectionid_2 ),
                             _HRDetailInfo.CreateParameter("@CurrentUser", SqlDbType.VarChar, 150,TUserInfo.UserID),
                             _HRDetailInfo.CreateParameter("@WorkpalceID", SqlDbType.VarChar, 30,SelectedWP ),
                            };


                            _HRDetailInfo.ParamCollection = _paramCollection2;
                            _HRDetailInfo.ExecuteInstruction();
                        }
                        catch (Exception _exception)
                        {
                            throw new ApplicationException(_exception.Message, _exception);
                        }

                        MWDataManager.clsDataAccess _HRSumInfo = new MWDataManager.clsDataAccess();
                        _HRSumInfo.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _HRSumInfo.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _HRSumInfo.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _HRSumInfo.SqlStatement = "sp_HRGetPlanProtReportDataSummary";
                        _HRSumInfo.ResultsTableName = "_HRSumInfo";

                        try
                        {
                            SqlParameter[] _paramCollection3 =
                                {
                             _HRSumInfo.CreateParameter("@PRODMONTH", SqlDbType.Int, 0,THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth) ),
                             _HRSumInfo.CreateParameter("@SectionID_2", SqlDbType.VarChar, 30,TheSectionid_2 ),
                             _HRSumInfo.CreateParameter("@WorkplaceID", SqlDbType.VarChar, 30,SelectedWP ),
                             _HRSumInfo.CreateParameter("@CurrentUser", SqlDbType.VarChar, 150,TUserInfo.UserID),
                            };


                            _HRSumInfo.ParamCollection = _paramCollection3;
                            _HRSumInfo.ExecuteInstruction();
                        }
                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section :_HRSumInfo : " + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtStopeSaftey = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtStopeSaftey.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtStopeSaftey.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtStopeSaftey.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtStopeSaftey.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtStopeSaftey.ResultsTableName = "STOPING_SAFTEY";

                            _paramCollection = SetParamters(11, SelectedWP, Convert.ToInt32(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                            _PlanProtStopeSaftey.ParamCollection = _paramCollection;
                            _PlanProtStopeSaftey.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPPING_SAFTEY:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtStopeWORKPLACEPLANNINGCHECKLIST = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtStopeWORKPLACEPLANNINGCHECKLIST.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtStopeWORKPLACEPLANNINGCHECKLIST.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtStopeWORKPLACEPLANNINGCHECKLIST.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtStopeWORKPLACEPLANNINGCHECKLIST.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtStopeWORKPLACEPLANNINGCHECKLIST.ResultsTableName = "STOPING_WP_PLAN_CL";

                            _paramCollection = SetParamters(12, SelectedWP, Convert.ToInt32(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 0);

                            _PlanProtStopeWORKPLACEPLANNINGCHECKLIST.ParamCollection = _paramCollection;
                            _PlanProtStopeWORKPLACEPLANNINGCHECKLIST.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPING_WP_PLAN_CL:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtStopeRocEng = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtStopeRocEng.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtStopeRocEng.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtStopeRocEng.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtStopeRocEng.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtStopeRocEng.ResultsTableName = "STOPE_ROCEN";

                            _paramCollection = SetParamters(13, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                            _PlanProtStopeRocEng.ParamCollection = _paramCollection;
                            _PlanProtStopeRocEng.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPE_ROCEN:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtStopeStores = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtStopeStores.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtStopeStores.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtStopeStores.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtStopeStores.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtStopeStores.ResultsTableName = "STOPE_STORES";

                            _paramCollection = SetParamters(14, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                            _PlanProtStopeStores.ParamCollection = _paramCollection;
                            _PlanProtStopeStores.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPE_STORES:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtStopeGEOLOGY = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtStopeGEOLOGY.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtStopeGEOLOGY.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtStopeGEOLOGY.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtStopeGEOLOGY.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtStopeGEOLOGY.ResultsTableName = "STOPE_GEOLOGY";

                            _paramCollection = SetParamters(15, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                            _PlanProtStopeGEOLOGY.ParamCollection = _paramCollection;
                            _PlanProtStopeGEOLOGY.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPE_GEOLOGY:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _ProdMonVar = new MWDataManager.clsDataAccess();
                        try
                        {
                            _ProdMonVar.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _ProdMonVar.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _ProdMonVar.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _ProdMonVar.SqlStatement = "sp_PlanProt_GetReportData";
                            _ProdMonVar.ResultsTableName = "ProdMonVarCY";

                            _paramCollection = SetParamters(3003, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                            _ProdMonVar.ParamCollection = _paramCollection;
                            _ProdMonVar.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: ProdMonVarCY:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtStopeSI = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtStopeSI.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtStopeSI.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtStopeSI.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtStopeSI.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtStopeSI.ResultsTableName = "SPECIAL_INSTRUCTIONS";

                            _paramCollection = SetParamters(22, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                            _PlanProtStopeSI.ParamCollection = _paramCollection;
                            _PlanProtStopeSI.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: SPECIAL_INSTRUCTIONS:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtSTOPEENGINEERING = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtSTOPEENGINEERING.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtSTOPEENGINEERING.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtSTOPEENGINEERING.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtSTOPEENGINEERING.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtSTOPEENGINEERING.ResultsTableName = "STOPE_ENGINEERING";

                            _paramCollection = SetParamters(19, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                            _PlanProtSTOPEENGINEERING.ParamCollection = _paramCollection;
                            _PlanProtSTOPEENGINEERING.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPE_ENGINEERING:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtSTOPELOGISTICS = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtSTOPELOGISTICS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtSTOPELOGISTICS.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtSTOPELOGISTICS.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtSTOPELOGISTICS.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtSTOPELOGISTICS.ResultsTableName = "STOPE_LOGISTICS";

                            _paramCollection = SetParamters(18, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                            _PlanProtSTOPELOGISTICS.ParamCollection = _paramCollection;
                            _PlanProtSTOPELOGISTICS.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPE_LOGISTICS:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtSTOPEVENTILATION = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtSTOPEVENTILATION.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtSTOPEVENTILATION.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtSTOPEVENTILATION.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtSTOPEVENTILATION.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtSTOPEVENTILATION.ResultsTableName = "STOPE_VENTILATION";

                            _paramCollection = SetParamters(17, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                            _PlanProtSTOPEVENTILATION.ParamCollection = _paramCollection;
                            _PlanProtSTOPEVENTILATION.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPE_VENTILATION:" + _exception.Message, _exception);
                        }



                        MWDataManager.clsDataAccess _PlanProtStopeSurvey = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtStopeSurvey.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtStopeSurvey.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtStopeSurvey.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtStopeSurvey.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtStopeSurvey.ResultsTableName = "STOPE_SURVEY";

                            _paramCollection = SetParamters(20, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                            _PlanProtStopeSurvey.ParamCollection = _paramCollection;
                            _PlanProtStopeSurvey.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPE_SURVEY:" + _exception.Message, _exception);
                        }



                        MWDataManager.clsDataAccess _PlanProtStopeHR = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtStopeHR.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtStopeHR.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtStopeHR.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtStopeHR.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtStopeHR.ResultsTableName = "STOPE_HR";

                            _paramCollection = SetParamters(21, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                            _PlanProtStopeHR.ParamCollection = _paramCollection;
                            _PlanProtStopeHR.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: STOPE_HR:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtStopeEVALUATION = new MWDataManager.clsDataAccess();
                        _PlanProtStopeEVALUATION.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _PlanProtStopeEVALUATION.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _PlanProtStopeEVALUATION.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _PlanProtStopeEVALUATION.SqlStatement = "sp_PlanProt_GetReportData";
                        _PlanProtStopeEVALUATION.ResultsTableName = "STOPE_EVALUATION";

                        _paramCollection = SetParamters(16, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 0);

                        _PlanProtStopeEVALUATION.ParamCollection = _paramCollection;
                        _PlanProtStopeEVALUATION.ExecuteInstruction();



                        DataSet repMODDataSet = new DataSet();
                        repMODDataSet.Tables.Clear();
                        theReport.Clear();
                        // add data tables to report
                        repMODDataSet.Tables.Add(_PlanProtStopeSI.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtStopeGEOLOGY.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtSTOPEENGINEERING.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtStopeStores.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtSTOPELOGISTICS.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtSTOPEVENTILATION.ResultsDataTable);
                        repMODDataSet.Tables.Add(_WPList.ResultsDataTable);
                        repMODDataSet.Tables.Add(_WPInfo.ResultsDataTable);
                        //repMODDataSet.Tables.Add(_WPListApproved.ResultsDataTable);
                        repMODDataSet.Tables.Add(_ProdMonVar.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtStopeSaftey.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtStopeWORKPLACEPLANNINGCHECKLIST.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtStopeHR.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtStopeEVALUATION.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtStopeSurvey.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtStopeRocEng.ResultsDataTable);
                        repMODDataSet.Tables.Add(_HRDetailInfo.ResultsDataTable);
                        repMODDataSet.Tables.Add(_HRSumInfo.ResultsDataTable);


                        theReport.RegisterData(repMODDataSet);



                        //string dirName = Path.GetDirectoryName(TSystemSettings.ReportDir + "\\PlanProtStope.frx");

                        //if (File.Exists(TSystemSettings.ReportDir + "\\PlanProtStope.frx")) // test if file exsits
                        //{
                        // PlanProt.Load(TSystemSettings.ReportDir + "\\PlanProtStope.frx");

                        // PlanProt.SetParameterValue("ReportDir", TSystemSettings.ReportDir + "\\Planning_Minutes\\");

                        theReport.Load(TGlobalItems.ReportsFolder + "\\PlanProtStope.frx");



                        // theReport.SetParameterValue("val", 0); 
                        if (TParameters.DesignReport)
                            theReport.Design();
                        theReport.Prepare();

                        ActiveReport.SetReport = theReport;
                        ActiveReport.isDone = true;
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Unable to load report file " + TSystemSettings.ReportDir + "\\PlanProtStope.frx", "", MessageBoxButtons.OK);
                        //}

                        // return PlanProt;
                    }
                    else if (reportSettings.Code == "1")
                    {
                        MWDataManager.clsDataAccess _WPList = new MWDataManager.clsDataAccess();
                        try
                        {
                            _WPList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _WPList.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _WPList.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _WPList.SqlStatement = "sp_PlanProt_GetReportData";
                            _WPList.ResultsTableName = "WPList";

                            _paramCollection = SetParamters(2000, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _WPList.ParamCollection = _paramCollection;
                            _WPList.ExecuteInstruction();
                        }
                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: WPList:" + _exception.Message, _exception);
                        }

                        //MWDataManager.clsDataAccess _WPListApproved = new MWDataManager.clsDataAccess();
                        //_WPListApproved.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        //_WPListApproved.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        //_WPListApproved.queryReturnType = MWDataManager.ReturnType.DataTable;
                        //_WPListApproved.SqlStatement = "sp_PlanProt_GetReportData";
                        //_WPListApproved.ResultsTableName = "WPListApproved";

                        //_paramCollection = SetParamters(4000, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                        //_WPListApproved.ParamCollection = _paramCollection;
                        //_WPListApproved.ExecuteInstruction();

                        MWDataManager.clsDataAccess _WPInfo = new MWDataManager.clsDataAccess();
                        try
                        {
                            _WPInfo.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _WPInfo.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _WPInfo.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _WPInfo.SqlStatement = "sp_PlanProt_GetReportData";
                            _WPInfo.ResultsTableName = "WPInfo";

                            _paramCollection = SetParamters(2001, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _WPInfo.ParamCollection = _paramCollection;
                            _WPInfo.ExecuteInstruction();
                        }
                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: WPInfo:" + _exception.Message, _exception);
                        }

                        MWDataManager.clsDataAccess _HRDetailInfo = new MWDataManager.clsDataAccess();
                        _HRDetailInfo.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _HRDetailInfo.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _HRDetailInfo.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _HRDetailInfo.SqlStatement = "sp_HRGetPlanProtReportData";
                        _HRDetailInfo.ResultsTableName = "_HRDetailInfo";

                        try
                        {

                            SqlParameter[] _paramCollection2 =
                                {
                             _HRDetailInfo.CreateParameter("@PRODMONTH", SqlDbType.Int, 0,THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth)),
                             _HRDetailInfo.CreateParameter("@SectionID_2", SqlDbType.VarChar, 30,TheSectionid_2),
                             _HRDetailInfo.CreateParameter("@CurrentUser", SqlDbType.VarChar, 150,TUserInfo.UserID),
                             _HRDetailInfo.CreateParameter("@WorkpalceID", SqlDbType.VarChar, 30,SelectedWP.ToString()),
                            };


                            _HRDetailInfo.ParamCollection = _paramCollection2;
                            _HRDetailInfo.ExecuteInstruction();
                        }
                        catch (Exception _exception)
                        {
                            throw new ApplicationException(_exception.Message, _exception);
                        }

                        MWDataManager.clsDataAccess _HRSumInfo = new MWDataManager.clsDataAccess();
                        _HRSumInfo.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _HRSumInfo.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _HRSumInfo.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _HRSumInfo.SqlStatement = "sp_HRGetPlanProtReportDataSummary";
                        _HRSumInfo.ResultsTableName = "_HRSumInfo";

                        try
                        {
                            SqlParameter[] _paramCollection3 =
                                {
                             _HRSumInfo.CreateParameter("@Prodmonth", SqlDbType.Int, 0,THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth)),
                             _HRSumInfo.CreateParameter("@SectionID_2", SqlDbType.VarChar, 30,TheSectionid_2),
                             _HRSumInfo.CreateParameter("@WorkplaceID", SqlDbType.VarChar, 30,SelectedWP.ToString()),
                             _HRSumInfo.CreateParameter("@CurrentUser", SqlDbType.VarChar, 150,TUserInfo.UserID),
                            };


                            _HRSumInfo.ParamCollection = _paramCollection3;
                            _HRSumInfo.ExecuteInstruction();
                        }
                        catch (Exception _exception)
                        {
                            throw new ApplicationException(_exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _ProdMonVar = new MWDataManager.clsDataAccess();
                        try
                        {
                            _ProdMonVar.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _ProdMonVar.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _ProdMonVar.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _ProdMonVar.SqlStatement = "sp_PlanProt_GetReportData";
                            _ProdMonVar.ResultsTableName = "ProdMonVar";

                            _paramCollection = SetParamters(2003, SelectedWP, Convert.ToInt32(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth)), TheSectionid_2, 1);

                            _ProdMonVar.ParamCollection = _paramCollection;
                            _ProdMonVar.ExecuteInstruction();
                        }
                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: ProdMonVar:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtDevSURVEY = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevSURVEY.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevSURVEY.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevSURVEY.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevSURVEY.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevSURVEY.ResultsTableName = "SURVEY";

                            _paramCollection = SetParamters(1, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDevSURVEY.ParamCollection = _paramCollection;
                            _PlanProtDevSURVEY.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: SURVEY:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtDevGEOLOGY = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevGEOLOGY.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevGEOLOGY.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevGEOLOGY.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevGEOLOGY.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevGEOLOGY.ResultsTableName = "GEOLOGY";

                            _paramCollection = SetParamters(5, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDevGEOLOGY.ParamCollection = _paramCollection;
                            _PlanProtDevGEOLOGY.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: GEOLOGY:" + _exception.Message, _exception);
                        }



                        MWDataManager.clsDataAccess _PlanProtDevEQUIPPING = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevEQUIPPING.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevEQUIPPING.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevEQUIPPING.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevEQUIPPING.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevEQUIPPING.ResultsTableName = "DEVELOPMENT_EQUIPPING";

                            _paramCollection = SetParamters(6, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDevEQUIPPING.ParamCollection = _paramCollection;
                            _PlanProtDevEQUIPPING.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: DEVELOPMENT_EQUIPPING:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtDevStores = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevStores.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevStores.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevStores.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevStores.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevStores.ResultsTableName = "DEVELOPMENT_STORES";

                            _paramCollection = SetParamters(8, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);
                            _PlanProtDevStores.ParamCollection = _paramCollection;
                            _PlanProtDevStores.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: DEVELOPMENT_STORES:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtDEVELOPMENT_ENGENINPUT = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDEVELOPMENT_ENGENINPUT.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDEVELOPMENT_ENGENINPUT.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDEVELOPMENT_ENGENINPUT.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDEVELOPMENT_ENGENINPUT.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDEVELOPMENT_ENGENINPUT.ResultsTableName = "DEVELOPMENT_ENGENINPUT";

                            _paramCollection = SetParamters(9, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDEVELOPMENT_ENGENINPUT.ParamCollection = _paramCollection;
                            _PlanProtDEVELOPMENT_ENGENINPUT.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: DEVELOPMENT_ENGENINPUT:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtDevEnvConReq = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevEnvConReq.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevEnvConReq.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevEnvConReq.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevEnvConReq.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevEnvConReq.ResultsTableName = "DEVELOPMENT_ENVCONREQ";

                            _paramCollection = SetParamters(7, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDevEnvConReq.ParamCollection = _paramCollection;
                            _PlanProtDevEnvConReq.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: DEVELOPMENT_ENVCONREQ:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtDevSaftey = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevSaftey.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevSaftey.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevSaftey.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevSaftey.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevSaftey.ResultsTableName = "DEVELOPMENT_SAFTEY";

                            _paramCollection = SetParamters(10, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDevSaftey.ParamCollection = _paramCollection;
                            _PlanProtDevSaftey.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: DEVELOPMENT_SAFETY:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtDevRocEng = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevRocEng.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevRocEng.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevRocEng.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevRocEng.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevRocEng.ResultsTableName = "DEVELOPMENT_ROCEN";

                            _paramCollection = SetParamters(3, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDevRocEng.ParamCollection = _paramCollection;
                            _PlanProtDevRocEng.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: DEVELOPMENT_ROCEN:" + _exception.Message, _exception);
                        }



                        MWDataManager.clsDataAccess _PlanProtDevSurvey = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevSurvey.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevSurvey.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevSurvey.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevSurvey.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevSurvey.ResultsTableName = "DEVELOPMENT_SURVEY";

                            _paramCollection = SetParamters(1, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDevSurvey.ParamCollection = _paramCollection;
                            _PlanProtDevSurvey.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: DEVELOPMENT_SURVEY:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtDevHR = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevHR.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevHR.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevHR.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevHR.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevHR.ResultsTableName = "DEVELOPMENT_HR";

                            _paramCollection = SetParamters(2, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDevHR.ParamCollection = _paramCollection;
                            _PlanProtDevHR.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: DEVELOPMENT_HR:" + _exception.Message, _exception);
                        }


                        MWDataManager.clsDataAccess _PlanProtDevVALUATION = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevVALUATION.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevVALUATION.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevVALUATION.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevVALUATION.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevVALUATION.ResultsTableName = "DEVELOPMENT_VALUATION";

                            _paramCollection = SetParamters(4, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDevVALUATION.ParamCollection = _paramCollection;
                            _PlanProtDevVALUATION.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: DEVELOPMENT_VALUATION:" + _exception.Message, _exception);
                        }

                        MWDataManager.clsDataAccess _PlanProtDevSI = new MWDataManager.clsDataAccess();
                        try
                        {
                            _PlanProtDevSI.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _PlanProtDevSI.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            _PlanProtDevSI.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _PlanProtDevSI.SqlStatement = "sp_PlanProt_GetReportData";
                            _PlanProtDevSI.ResultsTableName = "SPECIAL_INSTRUCTIONS";

                            _paramCollection = SetParamters(23, SelectedWP, THarmonyPASGlobal.ProdMonthAsInt(reportSettings.Prodmonth), TheSectionid_2, 1);

                            _PlanProtDevSI.ParamCollection = _paramCollection;
                            _PlanProtDevSI.ExecuteInstruction();
                        }

                        catch (Exception _exception)
                        {
                            throw new ApplicationException("Report Section: SPECIAL_INSTRUCTIONS:" + _exception.Message, _exception);
                        }



                        DataSet repMODDataSet = new DataSet();

                        repMODDataSet.Tables.Clear();
                        theReport.Clear();
                        repMODDataSet.Tables.Add(_PlanProtDevSURVEY.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDevGEOLOGY.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDevEQUIPPING.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDevStores.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDEVELOPMENT_ENGENINPUT.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDevEnvConReq.ResultsDataTable);
                        repMODDataSet.Tables.Add(_WPList.ResultsDataTable);
                        repMODDataSet.Tables.Add(_WPInfo.ResultsDataTable);
                        //repMODDataSet.Tables.Add(_WPListApproved.ResultsDataTable);
                        repMODDataSet.Tables.Add(_ProdMonVar.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDevSaftey.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDevHR.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDevVALUATION.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDevSurvey.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDevRocEng.ResultsDataTable);
                        repMODDataSet.Tables.Add(_PlanProtDevSI.ResultsDataTable);
                        repMODDataSet.Tables.Add(_HRDetailInfo.ResultsDataTable);
                        repMODDataSet.Tables.Add(_HRSumInfo.ResultsDataTable);



                        theReport.RegisterData(repMODDataSet);



                        //string dirName = Path.GetDirectoryName(TSystemSettings.ReportDir + "\\PlanProtStope.frx");

                        //if (File.Exists(TSystemSettings.ReportDir + "\\PlanProtStope.frx")) // test if file exsits
                        //{
                        // PlanProt.Load(TSystemSettings.ReportDir + "\\PlanProtStope.frx");

                        // PlanProt.SetParameterValue("ReportDir", TSystemSettings.ReportDir + "\\Planning_Minutes\\");


                        theReport.Load(TGlobalItems.ReportsFolder + "\\PlanProtDev.frx");

                        // theReport.SetParameterValue("val", 0);
                        if (TParameters.DesignReport)
                            theReport.Design();
                        theReport.Prepare();

                        ActiveReport.SetReport = theReport;
                        ActiveReport.isDone = true;
                    }

                }
                else
                {

                    //theReport.Prepare();
                    ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.GenericControl;
                    copyReportToTemp(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth), TheSectionid_2, reportSettings.Code);
                    lp.setFileList(getData(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth), TheSectionid_2, reportSettings.Code));
                    lp.loadList(loadData(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth), TheSectionid_2, reportSettings.Code));
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


                if (File.Exists(String.Format("{0}\\{1}", THarmonyPASGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).PlanProtSaveDir, r["FileName"])))
                {
                    if (File.Exists(String.Format("{0}\\{1}", tempFolder, r["FileName"])))
                        File.Delete(String.Format("{0}\\{1}", tempFolder, r["FileName"]));

                    File.Copy(String.Format("{0}\\{1}", THarmonyPASGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).PlanProtSaveDir, r["FileName"]), String.Format("{0}\\{1}", tempFolder, r["FileName"]));

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
           // indecateDataStatus(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth), reportSettings.SECTIONID_2, reportSettings.Code);
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

           //     indecateDataStatus(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth), reportSettings.SECTIONID_2, reportSettings.Code);
           
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
    }
}
