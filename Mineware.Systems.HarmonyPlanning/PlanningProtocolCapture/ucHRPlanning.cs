using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Data;
using DevExpress.XtraPivotGrid.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors.Repository;
using FastReport;
using DevExpress.Utils;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Menu;
using DevExpress.Data.Filtering;
using DevExpress.Utils.Win;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning.PlanningProtocolCapture;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PlanningProtocolCapture
{
    public partial class ucHRPlanning : ucBaseUserControl 
    {
        string newone;
        MWDataManager.clsDataAccess _LoadUnplannedData = new MWDataManager.clsDataAccess();
        public ucHRPlanning()
        {
            InitializeComponent();
         
            //countcheck();
        }

        public void loadHRPlanningData(int Prodmonth, string sectionID_2, int Activity)
        {
           // SqlDataAdapter oleDBAdapter1 = new SqlDataAdapter("spHRPlanningWORKPLACELIST", TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection) );
            SqlDataAdapter oleDBAdapter1 = new SqlDataAdapter("sp_HRPlanning_WorkplaceList", TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection));
            //SqlDataAdapter oleDBAdapter1 = new SqlDataAdapter("SELECT WorkplaceDesc, Prodmonth, Sectionid, Sectionid_2,(Select DISTINCT CASE WHEN TargetID is not null then cast(1 as int) else cast(0 as int) end from HRStandardsAndNorms where TargetID = PP.TargetID)  TargetIDValid,CAST(dbo.isNightValid(WorkplaceID,Prodmonth,pp.TargetID) as int) NightCrewValid,  dbo.isPanelLengthValid(WorkplaceID,Prodmonth) PanelLengthValid, dbo.isStopeWidthLengthValid(WorkplaceID,Prodmonth) StopeWidthValid,  dbo.isDrillRigValid(WorkplaceID,Prodmonth) DrillRigValid, dbo.isNumberOfEndsValid (WorkplaceID,Prodmonth) NumberOfEndsValid FROM PrePlanning PP WHERE (PP.Prodmonth = " + Prodmonth + " ) and (PP.SectionID_2 = " + sectionID_2 + " and PP.Activitycode = " + Activity + " and WorkplaceID <> -1)", TUserInfo.ConnectionString);

            //SqlDataAdapter oleDBAdapter1 = new SqlDataAdapter("SELECT  data.WorkplaceDesc,data.Activitycode, data.Prodmonth, data.Sectionid, data.Sectionid_2,CASE WHEN data.TargetIDValid is  null then '0' else '1' end TargetIDValid,data.NightCrewValid,data.PanelLengthValid,data.StopeWidthValid,data.DrillRigValid,data.NumberOfEndsValid from (   SELECT WorkplaceDesc,PP.Activitycode, Prodmonth, Sectionid, Sectionid_2,(Select DISTINCT CASE WHEN TargetID is not null then cast(1 as int) else cast(0 as int) end from HRStandardsAndNorms where TargetID = PP.TargetID)  TargetIDValid,CAST(dbo.isNightValid(WorkplaceID,Prodmonth,PP.TargetID) as int) NightCrewValid,  dbo.isPanelLengthValid(WorkplaceID,Prodmonth) PanelLengthValid, dbo.isStopeWidthLengthValid(WorkplaceID,Prodmonth) StopeWidthValid,  dbo.isDrillRigValid(WorkplaceID,Prodmonth) DrillRigValid, dbo.isNumberOfEndsValid (WorkplaceID,Prodmonth) NumberOfEndsValid FROM PrePlanning PP WHERE (PP.Prodmonth = " + Prodmonth + " ) and (PP.SectionID_2 = " + sectionID_2 + " and PP.Activitycode = " + Activity + " and WorkplaceID <> -1))data", TUserInfo.ConnectionString);

            oleDBAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            // oleDBAdapter1.SelectCommand.Parameters.Add(" @prodmonth ", SqlDbType.Int,).Value = + Prodmonth;
            oleDBAdapter1.SelectCommand.Parameters.AddWithValue("@prodmonth", Prodmonth);
            // oleDBAdapter1.SelectCommand.Parameters.Add(" @SectionID_2 ", SqlDbType.VarChar).Value = + sectionID_2;
            oleDBAdapter1.SelectCommand.Parameters.AddWithValue("@SectionID_2", sectionID_2);

            // oleDBAdapter1.SelectCommand.Parameters.Add("@ActivityCode", SqlDbType.Int).Value = +Activity;
            oleDBAdapter1.SelectCommand.Parameters.AddWithValue("@Activity", Activity);
            SqlDataAdapter oleDBAdapter2 = new SqlDataAdapter("SELECT  Prodmonth, SectionID, Sectionid_2, WorkplaceDesc, Workplaceid, OrgUnitDay, OrgUnitNight, Activity, StdAndNormID, Designation, StdDay, StdNight, PlanDay, PlanNigth,PlanDayLast,PlanNigthLast, Updated FROM vw_HR_GetStdAndNorm WHERE Prodmonth = " + Prodmonth + " and SectionID_2 = '" + sectionID_2 + "' and Activity = " + Activity + " order by WorkplaceDesc,Designation", TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection) );
           

            oleDBAdapter1.Fill(dsHRPlanning.PrePlanning);
            oleDBAdapter2.Fill(dsHRPlanning.HR_GetStdAndNorm );
            loadAdditionalLabour(Prodmonth, sectionID_2, Activity);

            switch (Activity)
            {
                case 0:
                    gcPanelLengthValid.Visible = true;
                    gcStopeWidthValid.Visible = true;
                    gcDrillRigValid.Visible = false;
                    gcNumberOfEndsValid.Visible = false;
                    break;

                case 1:
                    gcPanelLengthValid.Visible = false;
                    gcStopeWidthValid.Visible = false;
                    gcDrillRigValid.Visible = true;
                    gcNumberOfEndsValid.Visible = true;
                    break;
            }

           


        }

        public void countcheck()
        {
            string text = "";
            string separator = "";
            foreach (DataRow r in dsHRPlanning.PrePlanning.Rows)
            {
                string textCMB = text;
                MWDataManager.clsDataAccess _RequiredData = new MWDataManager.clsDataAccess();
                _RequiredData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _RequiredData.SqlStatement = "spCountMiningMethod";
                _RequiredData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                if (Convert.ToInt32(r["TargetIDValid"].ToString()) == 0)
                {

                    SqlParameter[] _paramCollection1 = 
                    {
                    _RequiredData.CreateParameter("@Prodmonth", SqlDbType.Int, 7,Convert.ToInt32(r["Prodmonth"].ToString())),
                    _RequiredData.CreateParameter("@Sectionid_2 ", SqlDbType.VarChar, 50,r["Sectionid_2"].ToString()),
                    //_RequiredData.CreateParameter("@WorkplaceID", SqlDbType.Int, 0,r["WORKPLACEID"]),
                    //_RequiredData.CreateParameter("@TemplateID", SqlDbType.Int, 0,r["TemplateID"]),
                   // _RequiredData.CreateParameter("@ApprovedBy", SqlDbType.VarChar, 0,TUserInfo.UserID),
                    _RequiredData.CreateParameter("@Activity", SqlDbType.Int, 0,r["Activitycode"]),
                  //  _SaveData.CreateParameter("@ApproveItem", SqlDbType.VarChar, 0,"YES"),

                    };

                    _RequiredData.ParamCollection = _paramCollection1;
                    _RequiredData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _RequiredData.ExecuteInstruction();

                    DataTable dt = new DataTable();
                    dt = _RequiredData.ResultsDataTable;

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["MiningMethodCount"]) > 0)
                        {

                            text += separator + r["WorkplaceDesc"];
                            separator = ",";
                            // textCMB += text;

                            string ss = string.Join(",", r["WorkplaceDesc"]);

                        }
                    }

                }
               
            }
            if (text == "")
            {
            }
            else
            {
                MessageBox.Show("Please provide Mining Methods required for the Workplaces " + text, "", MessageBoxButtons.OK);
            }

            string text1 = "";
            string separator1 = "";
            foreach (DataRow r in dsHRPlanning.PrePlanning.Rows)
            {
                string textCMB = text1;
                MWDataManager.clsDataAccess _RequiredData = new MWDataManager.clsDataAccess();
                _RequiredData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _RequiredData.SqlStatement = "spCountNightCrew";
                _RequiredData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                if (Convert.ToInt32(r["NightCrewValid"].ToString()) == 0)
                {

                    SqlParameter[] _paramCollection1 = 
                    {
                    _RequiredData.CreateParameter("@Prodmonth", SqlDbType.Int, 7,Convert.ToInt32(r["Prodmonth"].ToString())),
                    _RequiredData.CreateParameter("@Sectionid_2 ", SqlDbType.VarChar, 50,r["Sectionid_2"].ToString()),
                    //_RequiredData.CreateParameter("@WorkplaceID", SqlDbType.Int, 0,r["WORKPLACEID"]),
                    //_RequiredData.CreateParameter("@TemplateID", SqlDbType.Int, 0,r["TemplateID"]),
                   // _RequiredData.CreateParameter("@ApprovedBy", SqlDbType.VarChar, 0,TUserInfo.UserID),
                    _RequiredData.CreateParameter("@Activity", SqlDbType.Int, 0,r["Activitycode"]),
                  //  _SaveData.CreateParameter("@ApproveItem", SqlDbType.VarChar, 0,"YES"),

                    };

                    _RequiredData.ParamCollection = _paramCollection1;
                    _RequiredData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _RequiredData.ExecuteInstruction();

                    DataTable dt = new DataTable();
                    dt = _RequiredData.ResultsDataTable;

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["NightCrewCount"]) > 0)
                        {

                            text1 += separator1 + r["WorkplaceDesc"];
                            separator1 = ",";
                            // textCMB += text;

                            string ss = string.Join(",", r["WorkplaceDesc"]);

                        }
                    }

                }

            }
            if (text1 == "")
            {
            }
            else
            {
                MessageBox.Show("Please provide Night Crews required for the Workplaces " + text1, "", MessageBoxButtons.OK);
            }

            string text2 = "";
            string separator2 = "";
            foreach (DataRow r in dsHRPlanning.PrePlanning.Rows)
            {
                string textCMB = text1;
                MWDataManager.clsDataAccess _RequiredData = new MWDataManager.clsDataAccess();
                _RequiredData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _RequiredData.SqlStatement = "spCountPanelLengthValid";
                _RequiredData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                if (Convert.ToInt32(r["PanelLengthValid"].ToString()) == 0)
                {

                    SqlParameter[] _paramCollection1 = 
                    {
                    _RequiredData.CreateParameter("@Prodmonth", SqlDbType.Int, 7,Convert.ToInt32(r["Prodmonth"].ToString())),
                    _RequiredData.CreateParameter("@Sectionid_2 ", SqlDbType.VarChar, 50,r["Sectionid_2"].ToString()),
                    //_RequiredData.CreateParameter("@WorkplaceID", SqlDbType.Int, 0,r["WORKPLACEID"]),
                    //_RequiredData.CreateParameter("@TemplateID", SqlDbType.Int, 0,r["TemplateID"]),
                   // _RequiredData.CreateParameter("@ApprovedBy", SqlDbType.VarChar, 0,TUserInfo.UserID),
                    _RequiredData.CreateParameter("@Activity", SqlDbType.Int, 0,r["Activitycode"]),
                  //  _SaveData.CreateParameter("@ApproveItem", SqlDbType.VarChar, 0,"YES"),

                    };

                    _RequiredData.ParamCollection = _paramCollection1;
                    _RequiredData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _RequiredData.ExecuteInstruction();

                    DataTable dt = new DataTable();
                    dt = _RequiredData.ResultsDataTable;

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["PanelLengthCount"]) > 0)
                        {

                            text2 += separator2 + r["WorkplaceDesc"];
                            separator2 = ",";
                            // textCMB += text;

                            string ss = string.Join(",", r["WorkplaceDesc"]);

                        }
                    }

                }

            }
            if (text2 == "")
            {
            }
            else
            {
                MessageBox.Show("Please provide Panel Length required for the Workplaces " + text2, "", MessageBoxButtons.OK);
            }

            string text3 = "";
            string separator3 = "";
            foreach (DataRow r in dsHRPlanning.PrePlanning.Rows)
            {
                string textCMB = text1;
                MWDataManager.clsDataAccess _RequiredData = new MWDataManager.clsDataAccess();
                _RequiredData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _RequiredData.SqlStatement = "spCountStopeWidthValid";
                _RequiredData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                if (Convert.ToInt32(r["StopeWidthValid"].ToString()) == 0)
                {

                    SqlParameter[] _paramCollection1 = 
                    {
                    _RequiredData.CreateParameter("@Prodmonth", SqlDbType.Int, 7,Convert.ToInt32(r["Prodmonth"].ToString())),
                    _RequiredData.CreateParameter("@Sectionid_2 ", SqlDbType.VarChar, 50,r["Sectionid_2"].ToString()),
                 
                    _RequiredData.CreateParameter("@Activity", SqlDbType.Int, 0,r["Activitycode"]),

                    };

                    _RequiredData.ParamCollection = _paramCollection1;
                    _RequiredData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _RequiredData.ExecuteInstruction();

                    DataTable dt = new DataTable();
                    dt = _RequiredData.ResultsDataTable;

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["StopeWidthCount"]) > 0)
                        {

                            text3 += separator3 + r["WorkplaceDesc"];
                            separator3 = ",";
                            // textCMB += text;

                            string ss = string.Join(",", r["WorkplaceDesc"]);

                        }
                    }

                }

            }
            if (text3 == "")
            {
            }
            else
            {
                MessageBox.Show("Please provide Stope Width required for the Workplaces " + text3, "", MessageBoxButtons.OK);
            }
        }

        public void loadAdditionalLabour(int Prodmonth, string sectionID_2, int Activity)
        {

            _LoadUnplannedData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _LoadUnplannedData.SqlStatement = "SELECT DISTINCT PP.Prodmonth,'MO' theLevel,SC.Name_2,SC.SectionID_2 ,'' SectionID_1,(SELECT AtWorkPlan FROM PREPLANNINGHR_UNPLANNED_LABOUR WHERE SectionID_2 = SC.SectionID_2  and theLevel = 'MO' and Prodmonth = pp.Prodmonth)  AtWorkPlan, " +
                                              "(SELECT InServicePlan FROM PREPLANNINGHR_UNPLANNED_LABOUR WHERE SectionID_2 = SC.SectionID_2  and theLevel = 'MO' and Prodmonth = pp.Prodmonth)  InServicePlan FROM Preplanning PP " +
                                     "INNER JOIN SECTION_COMPLETE SC ON " +
                                     "PP.SectionID = SC.SectionID and " +
                                     "PP.Prodmonth = SC.Prodmonth " +
                                     "WHERE PP.Prodmonth = " + Prodmonth.ToString() + " and " +
                                     "      PP.SectionID_2 = '" + sectionID_2.ToString() + "' " +
                                     "UNION " +
                                     "SELECT DISTINCT PP.Prodmonth,'PS' theLevel,SC.Name_1,SC.SectionID_2 ,SC.SectionID_1,(SELECT AtWorkPlan FROM PREPLANNINGHR_UNPLANNED_LABOUR WHERE SectionID_2 = SC.SectionID_2 and SectionID_1 = SC.SectionID_1 and Prodmonth = pp.Prodmonth) AtWorkPlan, " +
                                    "(SELECT InServicePlan FROM PREPLANNINGHR_UNPLANNED_LABOUR WHERE SectionID_2 = SC.SectionID_2 and SectionID_1 = SC.SectionID_1 and Prodmonth = pp.Prodmonth)  InServicePlan FROM Preplanning PP " +
                                     "INNER JOIN SECTION_COMPLETE SC ON " +
                                     "PP.SectionID = SC.SectionID and " +
                                     "PP.Prodmonth = SC.Prodmonth " +
                                     "WHERE PP.Prodmonth = " + Prodmonth.ToString() + " and " +
                                     "      PP.SectionID_2 = '" + sectionID_2.ToString() + "'";
            _LoadUnplannedData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _LoadUnplannedData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _LoadUnplannedData.ExecuteInstruction();
            gridControl2.DataSource = _LoadUnplannedData.ResultsDataTable;

        }

        public void saveHRPlannData()
        {
            StringBuilder sb = new StringBuilder();
            MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
            _SaveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
           // _SaveData.SqlStatement = "spHRManagePlanning";
            _SaveData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            Mineware.Systems.ProductionGlobal.frmProgress theProgress = new Mineware.Systems.ProductionGlobal.frmProgress();
           
           
            theProgress.SetCaption("Save HR Data");
            theProgress.SetProgresMax(dsHRPlanning.HR_GetStdAndNorm.Rows.Count + _LoadUnplannedData.ResultsDataTable.Rows.Count);
            theProgress.Show();
            int thePos = 0;
            sb.AppendLine("Declare @SQL Varchar(8000)");
            foreach (DataRow r in dsHRPlanning.HR_GetStdAndNorm.Rows)
            {
             //   if (r["Updated"].ToString() == "1")
               // {
                    //SqlParameter[] _paramCollection = 
                    //{
                    //    _SaveData.CreateParameter("@Prodmonth", SqlDbType.Int, 7,Convert.ToInt32(r["PRODMONTH"].ToString())),
                    //    _SaveData.CreateParameter("@SectionID ", SqlDbType.VarChar, 50,r["SECTIONID"].ToString()),
                    //    _SaveData.CreateParameter("@Sectionid_2", SqlDbType.VarChar,50,r["Sectionid_2"]),
                    //    _SaveData.CreateParameter("@WorkplaceDesc", SqlDbType.VarChar, 150,r["WorkplaceDesc"]),
                    //    _SaveData.CreateParameter("@Activitycode", SqlDbType.Int, 0,r["Activitycode"]),
                    //    _SaveData.CreateParameter("@Workplaceid", SqlDbType.VarChar, 20,r["WORKPLACEID"]),
                    //    _SaveData.CreateParameter("@StdAndNormID", SqlDbType.Int, 0,r["StdAndNormID"]),
                    //    _SaveData.CreateParameter("@Desgnation", SqlDbType.VarChar,150,r["Designation"]),
                    //    _SaveData.CreateParameter("@DAY", SqlDbType.Int, 0,r["PlanDay"]),
                    //    _SaveData.CreateParameter("@Night", SqlDbType.Int, 0,r["PlanNigth"]),

                    //};

                    //_SaveData.ParamCollection = _paramCollection;
                    //_SaveData.ExecuteInstruction();                

             //   }


                sb.AppendLine("SET @SQL = 'spHRManagePlanning ''" + r["PRODMONTH"].ToString() + "'',''" + r["SECTIONID"].ToString() +
                              "'',''" + r["Sectionid_2"].ToString() + " '',''" + r["WorkplaceDesc"].ToString() + "'',''" + r["Activity"].ToString () +
                              "'',''" + r["WORKPLACEID"].ToString() + "'',''" + r["StdAndNormID"].ToString() + "'',''" +r["Designation"].ToString()+
                              "'',''" + r["PlanDay"].ToString() + "'',''" + r["PlanNigth"].ToString() + "''' ");
                sb.AppendLine("exec (@SQL) ");
                thePos++;
                theProgress.SetProgressPosition(thePos);
            }
            _SaveData.SqlStatement = sb.ToString();
            _SaveData.ExecuteInstruction();
            
            sb.Clear();
            sb.AppendLine("Declare @SQL Varchar(8000)");

            //_SaveData.SqlStatement = "spHRManagePlanningUnPlan";
            foreach (DataRow r in _LoadUnplannedData.ResultsDataTable.Rows)
            {
                //SqlParameter[] _paramCollection = 
                //    {
                //        _SaveData.CreateParameter("@Prodmonth", SqlDbType.Int, 7,Convert.ToInt32(r["PRODMONTH"].ToString())),
                //        _SaveData.CreateParameter("@SectionID_1 ", SqlDbType.VarChar, 50,r["SECTIONID_1"].ToString()),
                //        _SaveData.CreateParameter("@Sectionid_2", SqlDbType.VarChar,50,r["Sectionid_2"]),
                //        _SaveData.CreateParameter("@theLevel", SqlDbType.VarChar, 5,r["theLevel"]),
                //        _SaveData.CreateParameter("@AtWorkPlan", SqlDbType.Int, 0,r["AtWorkPlan"]),
                //        _SaveData.CreateParameter("@InServicePlan", SqlDbType.Int, 0,r["InServicePlan"]),

                //    };

                //_SaveData.ParamCollection = _paramCollection;
                //_SaveData.ExecuteInstruction();

                sb.AppendLine("SET @SQL = 'spHRManagePlanningUnPlan ''" + r["PRODMONTH"].ToString() + "'',''" + r["theLevel"].ToString() +
                              "'',''" + r["Sectionid_2"].ToString() + " '',''" + r["SECTIONID_1"].ToString() + "'',''" + r["AtWorkPlan"].ToString() +
                              "'',''" + r["InServicePlan"].ToString()+ "''' ");
                sb.AppendLine("exec (@SQL) ");
                thePos++;
                theProgress.SetProgressPosition(thePos);

            }

            _SaveData.SqlStatement = sb.ToString();
            _SaveData.ExecuteInstruction();


            theProgress.Close();
        }

        private void gvWorkplaceList_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column == gcValidMiningMethod)
            {
                //if ((dsHRPlanning.PrePlanning.Rows[e.ListSourceRowIndex]["TargetIDValid"]).ToString() == null)
                //{

                //    int i = 0;
                //    e.Value = imagesStatus.Images[i];
                //}
               // else
               // { 
               
                    
                    //if (Convert .ToString ( e.Value)== "")
                    //{
                    //    e.Value = imagesStatus.Images[0];
                    //    MessageBox.Show(" Please enter a valid Mining Method", "", MessageBoxButtons.OK);
                    //}
                    //else
                    //{
                        int i = Convert.ToInt32(dsHRPlanning.PrePlanning.Rows[e.ListSourceRowIndex]["TargetIDValid"].ToString());
                        e.Value = imagesStatus.Images[i];
                        //if (i == 1)
                        //{
                        //    MessageBox.Show("Please eneter a valid Mining Method", "", MessageBoxButtons.OK);
                        //}
                    //}
               // }
            }

            if (e.Column == gcNightCrewValid)
            {
                //if (dsHRPlanning.PrePlanning.Rows[e.RowHandle]["NightCrewValid"] == null)
                //{
                //    int i = 0;
                //    e.Value = imagesStatus.Images[i];
                //}
                //else
                //{
                    
                    //if (e.Value == null)
                    //{
                    //    e.Value = imagesStatus.Images[0];
                    //    MessageBox.Show("Please enter a valid NightCrew", "", MessageBoxButtons.OK);
                    //}
                    //else
                    //{
                        int i = Convert.ToInt32(dsHRPlanning.PrePlanning.Rows[e.ListSourceRowIndex]["NightCrewValid"].ToString());
                        e.Value = imagesStatus.Images[i];
                        //if (i == 1)
                        //{
                        //    MessageBox.Show("Please eneter a valid Night Crew", "", MessageBoxButtons.OK);
                        //}
                   // }
            }

            if (e.Column == gcPanelLengthValid)
            {
                int i = Convert.ToInt32(dsHRPlanning.PrePlanning.Rows[e.ListSourceRowIndex ]["PanelLengthValid"].ToString());
                e.Value = imagesStatus.Images[i];
                //if (i == 1)
                //{
                //    MessageBox.Show("Please eneter a valid PanelLengthValid", "", MessageBoxButtons.OK);
                //}
            }

            if (e.Column == gcStopeWidthValid)
            {
                int i = Convert.ToInt32(dsHRPlanning.PrePlanning.Rows[e.ListSourceRowIndex ]["StopeWidthValid"].ToString());
                e.Value = imagesStatus.Images[i];
                //if (i == 1)
                //{
                //    MessageBox.Show("Please eneter a valid StopeWidthValid", "", MessageBoxButtons.OK);
                //}
            }

            if (e.Column == gcNumberOfEndsValid)
            {
                int i = Convert.ToInt32(dsHRPlanning.PrePlanning.Rows[e.ListSourceRowIndex ]["NumberOfEndsValid"].ToString());
                e.Value = imagesStatus.Images[i];
                //if (i == 1)
                //{
                //    MessageBox.Show("Please eneter a valid NumberOfEndsValid", "", MessageBoxButtons.OK);
                //}
            }

            if (e.Column == gcDrillRigValid)
            {
                int i = Convert.ToInt32(dsHRPlanning.PrePlanning.Rows[e.ListSourceRowIndex ]["DrillRigValid"].ToString());
                e.Value = imagesStatus.Images[i];
                //if (i == 1)
                //{
                //    MessageBox.Show("Please eneter a valid DrillRigValid", "", MessageBoxButtons.OK);
                //}
            }

             
        }

        private void gvWorkplaceList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int abc = Convert.ToInt32(gvWorkplaceList.GetRowCellValue(e.RowHandle, "PlanDay")); ;
            int abcd = Convert.ToInt32(gvWorkplaceList.GetRowCellValue(e.RowHandle, "PlanNight")); ;
            //DataTable dt=new DataTable ();
            //dt=dsHRPlanning .PrePlanning ;
            //string exp = "PlanDay=0 and PlanNight=0";
            //string exp1 = "PlanNight=0";
            //DataRow[] s3=dt.Select (exp);
            //for (int i = 0; i < s3.Length; i++ )
            //{
            //    if (Convert.ToInt32(s3[i]["PlanDay"]) == 0 && Convert.ToInt32(s3 [i] ["PlanNight"]) == 0)
            //    {
            //        if (e.Column.FieldName == "PlanDay" && Convert.ToInt32(s3 [i] ["PlanDay"]) == 0)
            //        {
            //             e.Appearance.BackColor = System.Drawing.Color.Red;
            //            // e.Column.AppearanceCell.BackColor = System.Drawing.Color.Red;
            //        }

            //    }
            //}

            if (e.Column.FieldName == "PlanDay")
            {
               // abc = Convert.ToInt32(gvWorkplaceList.GetRowCellValue(e.RowHandle, "PlanDay"));
                if (Convert.ToInt32(abc) == 0)
                {
                    e.Appearance.BackColor = System.Drawing.Color.Red;
                }
            }

            if (e.Column.FieldName == "PlanNight")
            {
               // abcd = Convert.ToInt32(gvWorkplaceList.GetRowCellValue(e.RowHandle, "PlanNight"));
                if (Convert.ToInt32(abc) == 0 && Convert.ToInt32(abcd) == 0)
                {
                    e.Appearance.BackColor = System.Drawing.Color.Red;
                }
            }
        
        }

        private void gvHRPlann_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            //if (Convert.ToString(e.Column) == "Plan Night")
            //{
            //    //    if(e.RowHandle (
            //    //foreach (DataRow ds in dsHRPlanning.HR_GetStdAndNorm.Rows)
            //    //{
            //    //    if (ds["OrgUnitNight"].ToString() == null)
            //    //    {
            //    //        colPlanNigth.OptionsColumn.AllowEdit = false;
            //    //        colPlanNigth.OptionsColumn.ReadOnly = true;
            //    //    }
            //    //}

                //string abc =Convert .ToString ( gvHRPlann.GetRowCellValue(gvHRPlann .FocusedRowHandle , "OrgUnitNight"));
               // dsHRPlanning.PrePlanning.Rows[e.ListSourceRowIndex]["DrillRigValid"].ToString();
            //    if(abc =="")
            //    {
            //        e.Column.OptionsColumn.AllowEdit = false;
            //        e.Column.OptionsColumn.ReadOnly = true;
            //    }
            //}
            
        }

        private void gvHRPlann_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (Convert.ToString(e.Column) == "Plan Night")
            //{
                //    if(e.RowHandle (
                //foreach (DataRow ds in dsHRPlanning.HR_GetStdAndNorm.Rows)
                //{
                //    if (ds["OrgUnitNight"].ToString() == null)
                //    {
                //        colPlanNigth.OptionsColumn.AllowEdit = false;
                //        colPlanNigth.OptionsColumn.ReadOnly = true;
                //    }
                //}
            string abcd = Convert.ToString  (dsHRPlanning.HR_GetStdAndNorm.Rows [e.RowHandle  ]["OrgUnitDay"].ToString());
               
               // System .Data .DataRow row = gvHRPlann.GetDataRow    (gvHRPlann .FocusedRowHandle  );
               // string abc = row[2].ToString();
                if (newone  == "")
                {

                    colPlanNigth.OptionsColumn.AllowEdit = false;
                    colPlanNigth.OptionsColumn.ReadOnly = true;
                }
                else
                {
                    colPlanNigth.OptionsColumn.AllowEdit = true;
                    colPlanNigth.OptionsColumn.ReadOnly = false;
                }
           // }
        }

        private void gvWorkplaceList_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            GridView master = sender as GridView;
            GridView detail = master.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            detail.Click += new EventHandler(detail_click);
        }

        void detail_click(object sender, EventArgs e)
        {
            GridView gridview = sender as GridView;
            var value=gridview .GetRowCellValue (gridview .FocusedRowHandle ,gridview .Columns ["OrgUnitNight"]);
            newone = Convert.ToString(value);
        }

        private void gvWorkplaceList_MasterRowCollapsed(object sender, CustomMasterRowEventArgs e)
        {
            colPlanNigth.OptionsColumn.AllowEdit = false;
            colPlanNigth.OptionsColumn.ReadOnly = true;
        }
    }
}
