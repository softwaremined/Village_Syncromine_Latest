using DevExpress.XtraPivotGrid;
using FastReport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System.Net;
using System.Threading;
using System.Text;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning
{
    public partial class ucApprovePlanning : ucBaseUserControl
    {
        private PivotGridField fieldWorkplaceDesc = new PivotGridField("WorkplaceDesc", PivotArea.RowArea);
        private PivotGridField fieldTemplateName = new PivotGridField("TemplateName", PivotArea.ColumnArea);
        private PivotGridField fieldTheGroup = new PivotGridField("TheGroup", PivotArea.ColumnArea);
        private PivotGridField fieldDepartmentApproved = new PivotGridField("CanApprove", PivotArea.DataArea);
        DataTable theApproveData = new DataTable();
        Boolean itemsApproved = false;
        DateTime _Prodmonth;
        string _SectionID_2;
        int _Activity;
        private Thread theApproveThread;

        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;

        }

        private static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        private SqlParameter[] SetParamters(string WORKPLACEID, Int32 PRODMONTH, string SectionID_2, int ActicityCode, string sectionID)
        {
            MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();

            SqlParameter[] _paramCollection = 
                            {                                   
                            theData.CreateParameter("@Prodmonth", SqlDbType.Int, 0,PRODMONTH.ToString()),
                            theData.CreateParameter("@SectionID", SqlDbType.VarChar, 20,sectionID),
                            theData.CreateParameter("@Sectionid_2", SqlDbType.VarChar, 20,SectionID_2),
                            theData.CreateParameter("@WorkplaceID", SqlDbType.VarChar, 20,WORKPLACEID),
                            theData.CreateParameter("@Activity", SqlDbType.Int, 0,ActicityCode),
                            theData.CreateParameter("@UserID", SqlDbType.VarChar, 50,TUserInfo.UserID), 
                            theData.CreateParameter("@NetworkID", SqlDbType.VarChar, 100,System.Environment.UserName), 
                            theData.CreateParameter("@MachineID", SqlDbType.VarChar, 100,System.Environment.MachineName),                             
                            };


            return _paramCollection;
        }

        public void ApprovePlanning()
        {

            //Worker workerObject = new Worker();
            //Thread workerThread = new Thread(workerObject.DoWork);
            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            viewApprovePrePlanning.PostEditor();
            theApproveThread = new Thread(new ThreadStart(DoWork));
            theApproveThread.SetApartmentState(ApartmentState.STA);
            theApproveThread.Start();
        }



        public void DoWork()
        {
            //The following needs to be checked before a workplace can be approved.
            //1. Is the workplace created in CPM.
            //2. Is the workplace saved in pre planning.
            clsDataResult dr = new clsDataResult();
             MWDataManager.clsDataAccess _TestData = new MWDataManager.clsDataAccess();
            _TestData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _TestData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _TestData.queryReturnType = MWDataManager.ReturnType.DataTable;
            
            foreach (DataRow r in theApproveData.Rows)
            {
               

                if (Convert.ToBoolean(r["Approved"].ToString()) == true)
                {
                    MWDataManager.clsDataAccess _ApproveData1 = new MWDataManager.clsDataAccess();
                    _ApproveData1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _ApproveData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _ApproveData1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _ApproveData1.SqlStatement = "SELECT a.* FROM PrePlanning_ChangeRequest  a inner join "+
                                                " PREPLANNING_CHANGEREQUEST_APPROVAL b on " +
                                                " a.changerequestID = b.ChangerequestID Where " +
                                                " Prodmonth = "+ r["Prodmonth"].ToString()+ " and " +
                                                " WorkplaceID = '" + r["WorkplaceID"].ToString() + "' "+
                                                " And ChangeID= 5 " +
                                                " and Approved = 1";
                    _ApproveData1.ExecuteInstruction();

                    DataTable appdata = new DataTable();
                    //string WP = r["WorkplaceID"].ToString ();
                    //string EXP1="WorkplaceID = '"+WP+ "'";
                    //DataRow S1 = new DataRow
                   // S1 = _ApproveData1.ResultsDataTable.Select(EXP1);
                    appdata = _ApproveData1.ResultsDataTable;
                    foreach (DataRow s in appdata.Rows)
                    {
                        //MessageBox.Show(s["WorkplaceID"].ToString() + " " + r["WorkplaceID"].ToString() + " " + s["Prodmonth"].ToString() + " " + r["Prodmonth"].ToString() + " " + s["ChangeID"].ToString());
                        //if (s["WorkplaceID"].ToString() == r["WorkplaceID"].ToString() && Convert.ToInt32(s["ChangeID"]) == 5 && s["Prodmonth"].ToString() == r["Prodmonth"].ToString())
                        //{

                            //MessageBox.Show(s["WorkplaceID"].ToString() + " " + r["WorkplaceID"].ToString() + " " + s["Prodmonth"].ToString() + " " + r["Prodmonth"].ToString() + " " + s["ChangeID"].ToString() + " " + s["ChangeRequestID"].ToString());
                            MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
                            _SaveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _SaveData.SqlStatement = "sp_RevisedPlanning_StopWorkplace_Approve";
                            _SaveData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                            SqlParameter[] _paramCollection =
                            {
                                _SaveData.CreateParameter("@RequestID", SqlDbType.Int,0,s["ChangeRequestID"]),
                               // _SaveData.CreateParameter("@RequestStauts", SqlDbType.Int, 0,Convert.ToInt32(2)),
                                _SaveData.CreateParameter("@UserID", SqlDbType.VarChar, 50,TUserInfo.UserID),
                               // _SaveData.CreateParameter("@Comments", SqlDbType.VarChar, 255,editComments.Text),
                            };

                            _SaveData.ParamCollection = _paramCollection;
                            _SaveData.queryReturnType = MWDataManager.ReturnType.longNumber;
                            _SaveData.ExecuteInstruction();
                        //}
                    }

                    MWDataManager.clsDataAccess _ApproveData = new MWDataManager.clsDataAccess();
                    _ApproveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _ApproveData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    _ApproveData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _ApproveData.SqlStatement = "sp_Preplanning_Approve";

                    //ucPlanningProtocolReport saveRepot = new ucPlanningProtocolReport();
                    //Report theReport = new Report();

                    //saveRepot.get


                    if (r["Activity"].ToString() != "1")
                    {
                        //if (Convert.ToDouble(r["CubicMetres"].ToString()) == 0) Dolf removed code Cycle automatically cycle cubes 
                        //{

                        //    SqlParameter[] _paramCollection = SetParamters(r["WorkPlaceID"].ToString(), Convert.ToInt32(r["Prodmonth"].ToString()), r["Sectionid_2"].ToString(),
                        //                                                  Convert.ToInt32(r["Activity"].ToString()), r["SectionID"].ToString(), "N");
     
                        //    _ApproveData.ParamCollection = _paramCollection;
                        //    _ApproveData.ExecuteInstruction();
                        //}
                        //else
                        //{
                        //    if (r["SQM"].ToString() is DBNull)
                        //    {
                        //        r["SQM"] = 0;
                        //        if (Convert.ToDecimal(r["SQM"].ToString()) + Convert.ToDecimal(r["WasteSQM"].ToString()) != 0)
                        //        {
                        //            SqlParameter[] _paramCollection = SetParamters(r["WorkPlaceID"].ToString(), Convert.ToInt32(r["Prodmonth"].ToString()), r["Sectionid_2"].ToString(),
                        //                                                          Convert.ToInt32(r["Activity"].ToString()), r["SectionID"].ToString(), "N");

                        //            _ApproveData.ParamCollection = _paramCollection;
                        //            _ApproveData.ExecuteInstruction();
                        //        }
                        //    }


                        //}

                        SqlParameter[] _paramCollection1 = SetParamters(r["WorkPlaceID"].ToString(), Convert.ToInt32(r["Prodmonth"].ToString()), r["Sectionid_2"].ToString(),
                                              Convert.ToInt32(r["Activity"].ToString()), r["SectionID"].ToString());

                        _ApproveData.ParamCollection = _paramCollection1;
                        dr = _ApproveData.ExecuteInstruction();

                    }
                    else
                    {
                        if (Convert.ToDouble(r["CubicMetres"].ToString()) == 0)
                        {
                            SqlParameter[] _paramCollection = SetParamters(r["WorkPlaceID"].ToString(), Convert.ToInt32(r["Prodmonth"].ToString()), r["Sectionid_2"].ToString(),
                                                                          Convert.ToInt32(r["Activity"].ToString()), r["SectionID"].ToString());

                            _ApproveData.ParamCollection = _paramCollection;
                            dr = _ApproveData.ExecuteInstruction();
                        }
                        else
                        {
                            if (Convert.ToDecimal(r["Meters"].ToString()) + Convert.ToDecimal(r["MetersWaste"].ToString()) != 0)
                            {
                                SqlParameter[] _paramCollection = SetParamters(r["WorkPlaceID"].ToString(), Convert.ToInt32(r["Prodmonth"].ToString()), r["Sectionid_2"].ToString(),
                                                                              Convert.ToInt32(r["Activity"].ToString()), r["SectionID"].ToString());

                                _ApproveData.ParamCollection = _paramCollection;
                                dr = _ApproveData.ExecuteInstruction();
                            }

                            SqlParameter[] _paramCollection1 = SetParamters(r["WorkPlaceID"].ToString(), Convert.ToInt32(r["Prodmonth"].ToString()), r["Sectionid_2"].ToString(),
                                                                          Convert.ToInt32(r["Activity"].ToString()), r["SectionID"].ToString());

                            _ApproveData.ParamCollection = _paramCollection1;
                            dr = _ApproveData.ExecuteInstruction();
                        }
                    }

                    foreach (DataRow p in _ApproveData.ResultsDataTable.Rows)
                    {
                        //string test = p["theStatus"].ToString();
                        //r["Comments"] = "Approved";
                        //if (test == "Approved")
                        //{
                        //    r["Comments"] = "Approved";
                        //    itemsApproved = true;
                        //    try
                        //    {
                        //        string activity = "";
                        //        HarmonyPASReports.Planning_Protocol_Report.PlanningProtocolReportProperties reportSettings = new Reports.Planning_Protocol_Report.PlanningProtocolReportProperties();
                        //        reportSettings.Prodmonth = _Prodmonth;
                        //        reportSettings.SECTIONID_2 = _SectionID_2;
                        //        if (_Activity == 0)
                        //        {
                        //            activity = "0";
                        //        }
                        //        else if (_Activity == 1)
                        //        {
                        //            activity = "1";
                        //        }

                        //        //reportSettings .Code = activity ;
                        //        HarmonyPASReports.Planning_Protocol_Report.ucPlanningProtocolReport theReportLoader = new Reports.Planning_Protocol_Report.ucPlanningProtocolReport { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };


                        //        //   TODO:Remember to add planning protocol report exsport
                        //        // ReportsInterface.ucPlanningProtocolReport theReportLoader = new ReportsInterface.ucPlanningProtocolReport();
                        //        Report approvedReport = new Report();
                        //        //FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                        //        FastReport.Export.Image.ImageExport export = new FastReport.Export.Image.ImageExport();
                        //        //  export = FastReport.Export.Pdf.PDFExport;
                        //        reportSettings.Prodmonth = _Prodmonth;
                        //        reportSettings.WORKPLACEID = r["WorkPlaceID"].ToString();
                        //        reportSettings.SECTIONID_2 = "NONE";
                        //        reportSettings.Code = r["Activity"].ToString();
                        //        reportSettings.PPD = "Dynamic";
                        //        reportSettings.Print = "Workplace";

                        //        theReportLoader.RunCheck = false;

                        //        approvedReport = theReportLoader.approveReport(reportSettings, UserCurrentInfo.Connection);

                        //        // approvedReport = theReportLoader.approveReport(r["WorkPlaceID"].ToString(), Convert.ToInt32(r["Prodmonth"].ToString()), r["Activitycode"].ToString());
                        //        //   approvedReport.Prepare();

                        //        //  approvedReport.Export(export, String.Format("{0}\\Planning_Minutes\\{1}_{2}.pdf", TSystemSettings.ReportDir, r["Prodmonth"], r["WorkPlaceID"]));

                        //        //  approvedReport.SavePrepared(String.Format("{0}\\Planning_Minutes\\{1}_{2}.fpx", TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).PlanProtLocReportDir, r["Prodmonth"], r["WorkPlaceID"]));


                        //        approvedReport.SavePrepared(String.Format("{0}\\{1}_{2}.fpx", TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).PlanProtSaveDir, r["Prodmonth"], r["WorkPlaceID"]));


                        //        string ThePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                        //        string path = string.Format(@"{0}\Success_{1}_{2}.txt", ThePath, r["Prodmonth"], r["WorkPlaceID"]);

                        //        FileStream fs = File.Create(path);
                        //        string tmp = r["Prodmonth"].ToString() + " - " + r["WorkPlaceID"].ToString();
                        //        Byte[] info = new UTF8Encoding(true).GetBytes(tmp);
                        //        fs.Write(info, 0, info.Length);

                        //        tmp += "\n";
                        //        tmp = System.DateTime.Now.ToShortDateString() + "   " + System.DateTime.Now.ToShortTimeString();
                        //        tmp += "\n";

                        //        info = new UTF8Encoding(true).GetBytes(tmp);

                        //        fs.Write(info, 0, info.Length);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        string ThePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                        //        string path = string.Format(@"{0}\{1}_{2}.txt", ThePath, r["Prodmonth"], r["WorkPlaceID"]);

                        //        FileStream fs = File.Create(path);
                        //        string tmp = r["Prodmonth"].ToString() + " - " + r["WorkPlaceID"].ToString();
                        //        Byte[] info = new UTF8Encoding(true).GetBytes(tmp);
                        //        fs.Write(info, 0, info.Length);

                        //        tmp += "\n";
                        //        tmp = System.DateTime.Now.ToShortDateString() + "   " + System.DateTime.Now.ToShortTimeString();
                        //        tmp += "\n";
                        //        tmp = ex.Message;
                        //        tmp += "\n";
                        //        tmp += "\n";
                        //        tmp += ex.StackTrace;

                        //        info = new UTF8Encoding(true).GetBytes(tmp);

                        //        fs.Write(info, 0, info.Length);

                        //        r["Comments"] = "Report Failed to export";
                        //    }


                        //    MWDataManager.clsDataAccess _AddTemplateApproved = new MWDataManager.clsDataAccess();
                        //    _AddTemplateApproved.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        //    _AddTemplateApproved.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        //    _AddTemplateApproved.queryReturnType = MWDataManager.ReturnType.longNumber;
                        //    _AddTemplateApproved.SqlStatement = "INSERT INTO dbo.PlanProt_ApprovedTemplate " +
                        //                                        "( Prodmonth , " +
                        //                                        "  Workplaceid , " +
                        //                                        "  FileName , " +
                        //                                        " Sectionid_2, " +
                        //                                        " Activity, " +
                        //                                        "  DateCreated" +
                        //                                        ") " +
                        //                                        "VALUES  ( " + r["Prodmonth"].ToString() + " ,  " +
                        //                                        "          '" + r["WorkPlaceID"].ToString() + "' , " +
                        //                                        "          '" + String.Format("{0}_{1}.fpx", r["Prodmonth"], r["WorkPlaceID"]) + "' ,  " +
                        //                                        "          '" + r["Sectionid_2"].ToString() + "' ,  " +
                        //                                        "          " + r["Activity"].ToString() + " ,  " +
                        //                                        "          GETDATE())";
                        //    _AddTemplateApproved.ExecuteInstruction();

                        //    _AddTemplateApproved.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        //       _AddTemplateApproved.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        //       _AddTemplateApproved.queryReturnType = MWDataManager.ReturnType.longNumber;
                        //       _AddTemplateApproved.SqlStatement = "UPDATE dbo.Planmonth SET Locked = 1 , LockedDate = GETDATE() , LockedBY = '" + TUserInfo.UserID + "' " +
                        //                                           "WHERE Prodmonth = " + r["Prodmonth"].ToString() + " AND " +
                        //                                           "      Sectionid = '" + r["Sectionid"].ToString() + "' AND " +
                        //                                           "      Sectionid_2 = '" + r["Sectionid_2"].ToString() + "'  AND " +
                        //                                           "      Workplaceid ='" + r["WorkPlaceID"].ToString() + "'  AND " +
                        //                                           "      Activity = " + r["Activity"].ToString() + " and PlanCode = 'MP'";
                        //       _AddTemplateApproved.ExecuteInstruction();

                        //       r["Comments"] = "Approved";
                        //}
                        //else
                        //{
                        //    r["Comments"] = p["theStatus"];
                        //    r["Approved"] = false;
                        //    itemsApproved = false;

                        //}
                    }

                              
                }

            }
          //  this.Cursor = Cursors.Default;
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Approval Complete", "All selected workplaces was processed.", Color.CornflowerBlue);
             UpdateCompletedArg _e = new UpdateCompletedArg(true); OnUpdateCompleted(_e);
            theApproveThread.Abort();


        }

  
        public ucApprovePlanning()
        {
            InitializeComponent();
        }


        public bool loadPrePlanning( DateTime Prodmonth, string SectionID_2, int Activity)
        {
            MWDataManager.clsDataAccess _ApprovedInfo = new MWDataManager.clsDataAccess();
            _ApprovedInfo.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _ApprovedInfo.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _ApprovedInfo.queryReturnType = MWDataManager.ReturnType.DataTable;
            _ApprovedInfo.SqlStatement = "sp_PrePlanning_GetApprovedInfo";

            _Prodmonth = Prodmonth;
            _SectionID_2 = SectionID_2;
            _Activity = Activity;

            SqlParameter[] _paramCollection = 
                                                    {                                   
                                                    _ApprovedInfo.CreateParameter("@Prodmonth", SqlDbType.Int, 0,TProductionGlobal.ProdMonthAsInt( Prodmonth)),
                                                    _ApprovedInfo.CreateParameter("@SectionID_2", SqlDbType.VarChar, 20,SectionID_2),
                                                    _ApprovedInfo.CreateParameter("@Activity", SqlDbType.Int, 0,Activity),
                                                    _ApprovedInfo.CreateParameter("@CurrentUser", SqlDbType.VarChar,20,TUserInfo.UserID),
                                                    };

            _ApprovedInfo.ParamCollection = _paramCollection;
            _ApprovedInfo.ExecuteInstruction();

            if (_ApprovedInfo.ResultsDataTable.Rows.Count > 0)
            {



                theApproveData = _ApprovedInfo.ResultsDataTable.Clone();
                DataRow[] theApprovedWP = _ApprovedInfo.ResultsDataTable.Select("CanApprove = 2");
                foreach (DataRow row in theApprovedWP)
                {
                    theApproveData.ImportRow(row);
                }
                gcApprovePrePlanning.DataSource = theApproveData;


                this.fieldDepartmentApproved.KPIGraphic = PivotKPIGraphic.Shapes;
                this.pgWorkplaceList.Fields.AddRange(new PivotGridField[] { this.fieldWorkplaceDesc });
                this.pgWorkplaceList.Fields.AddRange(new PivotGridField[] { this.fieldTheGroup });
                this.pgWorkplaceList.Fields.AddRange(new PivotGridField[] { this.fieldTemplateName });
                this.pgWorkplaceList.Fields.AddRange(new PivotGridField[] { fieldDepartmentApproved });
                this.pgWorkplaceList.Groups.Add(fieldTheGroup, fieldTemplateName);

                MWDataManager.clsDataAccess _TemplateList = new MWDataManager.clsDataAccess();
                _TemplateList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _TemplateList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _TemplateList.queryReturnType = MWDataManager.ReturnType.DataTable;
                _TemplateList.SqlStatement = "SELECT TemplateName,TemplateID,USR1.USERID USER1,USR2.USERID USER2 FROM dbo.PlanProt_Template PPT " +
                                             "LEFT JOIN Users USR1 ON " +
                                             "PPT.User1 = USR1.USERID " +
                                             "LEFT JOIN (SELECT * FROM dbo.USERS) USR2 ON " +
                                             "PPt.User2 = USR2.USERID " +
                                             "WHERE Activity = " + Activity.ToString();

                _TemplateList.ExecuteInstruction();

                //foreach (DataRow row in _TemplateList.ResultsDataTable.Rows)
                //{
                //    cbcTemplateList.Items.Add(row["TemplateName"]);
                //}
                cbcTemplateList.DisplayMember = "TemplateName";
                cbcTemplateList.ValueMember = "TemplateID";

                 cbcTemplateList.DataSource = _TemplateList.ResultsDataTable;

                pgWorkplaceList.DataSource = _ApprovedInfo.ResultsDataTable;

                pgWorkplaceList.Fields[1].Appearance.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                pgWorkplaceList.Fields[1].BestFit();

                fieldTemplateName.ColumnValueLineCount = 10;
                fieldWorkplaceDesc.Caption = "Workplace";
                fieldWorkplaceDesc.Width = 120;
                fieldTheGroup.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
                fieldTemplateName.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;

                this.fieldTemplateName.Width = 30;
                return true;
            }
            else { return false; }

        }

        private void imageListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private bool IsColumnHeaderHorizontal(PivotGridField field)
        {
            if (field == null)
            {
                return true;
            }
            if (field.Area == PivotArea.RowArea)
            {
                return true;
            }
            return false;
        }

        private void pgWorkplaceList_CustomDrawCell(object sender, PivotCustomDrawCellEventArgs e)
        {
            if (e.DataField == fieldDepartmentApproved)
            {

                if (Convert.ToInt32(e.Value) == -1)
                {
                    e.Graphics.FillRectangle(new SolidBrush(e.Appearance.BackColor), e.Bounds);
                    e.Graphics.DrawImage(imageCollection1.Images[0],
                                     e.Bounds.Left + (e.Bounds.Width - imageCollection1.Images[0].Width) / 2,
                                     e.Bounds.Top + (e.Bounds.Height - imageCollection1.Images[0].Height) / 2);
                }
                if (Convert.ToInt32(e.Value) == 1)
                {
                    e.Graphics.FillRectangle(new SolidBrush(e.Appearance.BackColor), e.Bounds);
                    e.Graphics.DrawImage(imageCollection1.Images[1],
                                     e.Bounds.Left + (e.Bounds.Width - imageCollection1.Images[1].Width) / 2,
                                     e.Bounds.Top + (e.Bounds.Height - imageCollection1.Images[1].Height) / 2);
                }

                if (Convert.ToInt32(e.Value) == 0)
                {
                    e.Graphics.FillRectangle(new SolidBrush(e.Appearance.BackColor), e.Bounds);
                    e.Graphics.DrawImage(imageCollection1.Images[2],
                                     e.Bounds.Left + (e.Bounds.Width - imageCollection1.Images[2].Width) / 2,
                                     e.Bounds.Top + (e.Bounds.Height - imageCollection1.Images[2].Height) / 2);
                }

                if (Convert.ToInt32(e.Value) == 2)
                {
                    e.Graphics.FillRectangle(new SolidBrush(e.Appearance.BackColor), e.Bounds);
                    e.Graphics.DrawImage(imageCollection1.Images[3],
                                     e.Bounds.Left + (e.Bounds.Width - imageCollection1.Images[3].Width) / 2,
                                     e.Bounds.Top + (e.Bounds.Height - imageCollection1.Images[3].Height) / 2);
                }
                e.Handled = true;
            }

        }

        private void pgWorkplaceList_CustomDrawFieldValue(object sender, PivotCustomDrawFieldValueEventArgs e)
        {
            if (e.Field == fieldTemplateName)
            {
                DevExpress.Utils.Drawing.HeaderObjectPainter newPainter = e.Painter;


                string c = e.Info.Caption;

                e.Info.Caption = "";
                newPainter.DrawObject(e.Info);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                if (IsColumnHeaderHorizontal(e.Field))
                {
                    //New Font(CType(sender, XRTableCell).Font, FontStyle.Bold) 
                    Font newFont = new Font(e.Appearance.Font, FontStyle.Bold);  //Font(e.Appearance.Font.FontFamily,  10);
                    e.Appearance.Font = newFont;

                    StringFormat fmt = new StringFormat();
                    fmt.Alignment = StringAlignment.Far;
                    fmt.Trimming = StringTrimming.EllipsisCharacter;
                    fmt.FormatFlags |= StringFormatFlags.NoWrap;

                    e.GraphicsCache.DrawString(c, e.Appearance.Font,
                        e.Appearance.GetForeBrush(e.GraphicsCache),
                        e.Info.CaptionRect, fmt);
                }
                else
                {
                    Rectangle newRect = new Rectangle();
                    newRect = e.Bounds;
                    //  newRect.X += newRect.Width;
                    //    newRect.Y += 30;// newRect.Height;

                    //   newRect.Width *= 7;
                    //    newRect.Width /= 5;

                    //    newRect.Height *= 7;
                    //    newRect.Height /= 5;

                    //    newRect.Y -= 8;
                    //   newRect.Height -= 8;
                    newRect.Height = 130;
                    //    newRect.Width = 10;
                    StringFormat fmt = new StringFormat();
                    fmt.Alignment = StringAlignment.Near;
                    fmt.Trimming = System.Drawing.StringTrimming.Word;
                    //fmt.Trimming = StringTrimming.EllipsisCharacter;
                    fmt.LineAlignment = System.Drawing.StringAlignment.Center;
                    fmt.FormatFlags = StringFormatFlags.LineLimit;



                    e.GraphicsCache.DrawVString(c, e.Appearance.Font,
                        e.Appearance.GetForeBrush(e.GraphicsCache),
                        newRect, fmt, 270);
                }

                e.Info.InnerElements.DrawObjects(e.Info, e.Info.Cache, Point.Empty);
                e.Handled = true;
            }

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow r in theApproveData.Rows)
            {
                r["Approved"] = checkEdit1.Checked;
                if (checkEdit1.Checked == true)
                {
                    checkEdit1.Text = "Un select All";
                }
                else { checkEdit1.Text = "Select All"; }
            }
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {




            foreach (var item in cbcTemplateList.CheckedItems)
            {
                int items = Convert.ToInt32((item as DataRowView)["TemplateID"].ToString());
                MWDataManager.clsDataAccess _GetData = new MWDataManager.clsDataAccess();
                _GetData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _GetData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _GetData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _GetData.SqlStatement = "SELECT d1.TemplateID,d1.User1,d1.TemplateName from( " +
                                        "SELECT PPT.TemplateID,PPT.TemplateName,PPAU.User1,PPAU.User2 FROM dbo.PlanProt_Template PPT " +
                                        "INNER JOIN dbo.PlanProt_ApproveUsers PPAU ON " +
                                        "PPT.TemplateID = PPAU.TemplateID " +
                                        "INNER JOIN vw_Section_from_MO SC  ON " +
                                        "(SC.Name_3 = PPAU.Shaft OR " +
                                        "SC.Name_4 = PPAU.Shaft) AND " +
                                        "SC.SECTIONID_2 = '" + _SectionID_2 + "' " +
                                        "WHERE PPT.TemplateID = '" + Convert.ToString(items) + "' AND " +
                                        "      PPT.Activity = " + Convert.ToString(_Activity) + "  AND " +
                                        "      SC.PRODMONTH = " + Convert .ToString ( TProductionGlobal.ProdMonthAsInt(_Prodmonth )) + " ) d1 " +
                                        " where d1.User1<>'' GROUP BY TemplateID,User1,TemplateName"; 
                _GetData.ExecuteInstruction();

                foreach (DataRow r in _GetData.ResultsDataTable.Rows)
                {

                    MWDataManager.clsDataAccess _SendMail = new MWDataManager.clsDataAccess();
                    _SendMail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _SendMail.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    _SendMail.queryReturnType = MWDataManager.ReturnType.longNumber;
                    _SendMail.SqlStatement = "sp_MailRequestDataApprovalNew";

                    SqlParameter[] _paramCollection = 
                                                    {                                   
                                                    _SendMail.CreateParameter("@Prodmonth", SqlDbType.Int, 0,  TProductionGlobal.ProdMonthAsInt(_Prodmonth )),
                                                    _SendMail.CreateParameter("@SectionID_2", SqlDbType.VarChar, 20,_SectionID_2),
                                                    _SendMail.CreateParameter("@TemplateID", SqlDbType.Int, 0,Convert.ToInt32(r["TemplateID"].ToString())),
                                                    _SendMail.CreateParameter("@toUserName", SqlDbType.VarChar,20,r["User1"].ToString()),
                                                    _SendMail.CreateParameter("@theMessage", SqlDbType.NVarChar,0,memoMessage.Text),
                                                    };

                    _SendMail.ParamCollection = _paramCollection;

                    _SendMail.ExecuteInstruction();

                }
            }
            MessageBox.Show("Message(s) was successfully sent.");
            //_SendMail.ExecuteInstruction();


        }

        private void pgWorkplaceList_Click(object sender, EventArgs e)
        {

        }

        private void gcWorkplaceList_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
