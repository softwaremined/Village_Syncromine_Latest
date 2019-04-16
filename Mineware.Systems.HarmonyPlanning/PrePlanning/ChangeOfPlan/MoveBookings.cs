using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.Data;
using FastReport;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    public partial class MoveBookings : ucBaseUserControl
    {
        private DataClass.clChangeOfPlanData _clChnageOfPlanData = new DataClass.clChangeOfPlanData();
        string theSectionID_2;
        int theProdmonth;
        int theActivity;
     //   string theWorkPlaceID;
        private string theWorkplaceID;
        string theSectionID;
        string theSectionIDMO;
        public string ABC = "";
        public string CBD = "";
        public string sqm = "";
        public string wastesqm = "";
        public string cubicsqm = "";
        public string meters = "";
        public string wastemeters = "";
        public string cubicmeters = "";
        public string daycrew = "";
        public string nightcrew = "";
        public string roamingcrew = "";
        public string afternooncrew = "";
       

        public MoveBookings()
        {
            InitializeComponent();
        }
        public void LoadDetails(int ChangeRequestID)
        {


            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _WPData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "[sp_prePlanning_data]";

            SqlParameter[] _paramCollectionS = 
                            {
                             _WPData.CreateParameter("@ChangeRequestID", SqlDbType.Int, 0,Convert .ToString (ChangeRequestID ))
                            };
            _WPData.ParamCollection = _paramCollectionS;
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                
                editFL.Text  = r["FL"].ToString();
                editMiningMethod .Properties .NullText  =r["Description"].ToString ();

                editSection.Text = r["Name_2"].ToString();

                textEdit2.Text = r["ProdMonth"].ToString();

                editToWorkplace.Properties.NullText = r["WPDesc"].ToString();

                memoEdit1 . Text = r["Comments"].ToString();


                loadManageValues(r["OldWorkplaceID"].ToString(), Convert.ToInt32(r["ProdMonth"].ToString()), r["SectionID"].ToString(), r["SectionID_2"].ToString(), true);
            }
        }

        private void loadManageValues(string OldWorkplace, int ProdMonth, string SectionID, string SectionID_2, bool ReadOnly)
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;

            _WPData.SqlStatement = "SELECT * FROM WORKPLACE WHERE DESCRIPTION = '" + OldWorkplace + "'";
            _WPData.ExecuteInstruction();

            string ABC = "";
            string OnreefS = "";
            string OffreefS = "";

            string cube = "";


            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                textEdit1.Text = r["DESCRIPTION"].ToString();
            }


            if (ReadOnly == true)
            {
              

                memoEdit1.Properties.ReadOnly = true;

                editSection.Properties.ReadOnly = true;
                textEdit2.Properties.ReadOnly = true;
                textEdit1.Properties.ReadOnly = true;
                textEdit2.Properties.ReadOnly = true;

                editToWorkplace.Properties.ReadOnly = true;

                editMiningMethod.Properties.ReadOnly = true;
            }
        }

        private void loadCurrentValues(string WorkplaceID, int ProdMonth, string SectionID, string SectionID_2, bool ReadOnly)
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM PLANMONTH " +
                                   "where Prodmonth = " + Convert.ToString(ProdMonth) + " and " +
                                   "      Sectionid = '" + SectionID + "' and " +
                                   "      Workplaceid = '" + WorkplaceID + "'";
            _WPData.ExecuteInstruction();

            string OnreefS = "";
            string OffreefS = "";

            string cube = "";
            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {

                theActivity = Convert.ToInt32(r["Activity"].ToString());
                switch (Convert.ToInt32(r["Activity"].ToString()))
                {
                    case 0:

                        sqm = r["ReefSQM"].ToString();;
                        wastesqm = r["WasteSQM"].ToString();
                        cubicsqm  = r["CubicMetres"].ToString();

                        

                        break;
                    case 1:

                        meters = r["ReefAdv"].ToString();
                        wastemeters = r["WasteAdv"].ToString();
                        cubicmeters = r["CubicMetres"].ToString();
                        break;
                }

                ABC = r["Sectionid"].ToString();
                OnreefS = r["ReefSQM"].ToString();
                OffreefS = r["WasteSQM"].ToString();
                cube = r["CubicMetres"].ToString();
            }

            _WPData.SqlStatement = "SELECT DISTINCT Name, Name_2,SECTIONID  FROM dbo.SECTION_COMPLETE WHERE PRODMONTH = '" + Convert.ToString(ProdMonth) + "' AND SECTIONID_2 = '" + SectionID_2 + "' AND SECTIONID='" + ABC.ToString() + "'";

            _WPData.ExecuteInstruction();

             foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                editSection.Text = r["Name_2"].ToString();
                editSection.Properties.ReadOnly = true;

            }


             _WPData.SqlStatement = "SELECT '' Description,'-1'TargetID from PLANMONTH PP \n"+
                "where PP.ProdMonth='" + ProdMonth +"' and  PP.Workplaceid='" + WorkplaceID + "' and PP.Sectionid='" + SectionID + "'";

            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {

                editMiningMethod.Properties.NullText = r["Description"].ToString();
                editMiningMethod.EditValue = editMiningMethod.Properties.GetKeyValueByDisplayText(r["Description"].ToString());
                CBD = r["TargetID"].ToString();
            }

            _WPData.SqlStatement = "SELECT * FROM WORKPLACE WHERE WORKPLACEID = '" + WorkplaceID + "'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {

                editToWorkplace.Text = r["DESCRIPTION"].ToString();
            }


            if (ReadOnly == true)
            {

                memoEdit1.Properties.ReadOnly = true;
            }



        }

        

        public void MoveBooking(String WorkplaceID, int ProdMonth, string SectionID, string SectionID_2, int activity)
        {


            textEdit2 .Text =Convert.ToString(ProdMonth);
            textEdit2.Properties.ReadOnly = true;
            theSectionID = SectionID;
            theWorkplaceID = WorkplaceID;
            theProdmonth = ProdMonth;
            theSectionID_2 = SectionID_2;
            theActivity = activity;



            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            


            CPMBusinessLayer.clsBusinessLayer BMEBL1 = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL1._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL1.SetsystemDBTag = this.theSystemDBTag;
            BMEBL1.SetUserCurrentInfo = this.UserCurrentInfo;
            editMiningMethod.Properties.DataSource = BMEBL1.getMiningMethods(theActivity);
            editMiningMethod.Properties.ValueMember = "TargetID";//TargetID
            editMiningMethod.Properties.DisplayMember = "Description";

            

            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM WORKPLACE WHERE WORKPLACEID = '" + WorkplaceID + "'";
            _WPData.ExecuteInstruction();


            editToWorkplace.Properties.DataSource = getWorkplaceList();
            editToWorkplace.Properties.ValueMember = "WORKPLACEID";
            editToWorkplace.Properties.DisplayMember  = "DESCRIPTION";

           


            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
               textEdit1 .Text   = r["DESCRIPTION"].ToString();
               textEdit1.Properties.ReadOnly = true;
                
            }

            _WPData.SqlStatement = "SELECT * FROM PLANMONTH WHERE Workplaceid = '" + WorkplaceID + "' and Prodmonth='"+theProdmonth +"' and Sectionid= '" + theSectionID + "' AND PLANCODE='MP'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                theActivity = Convert.ToInt32(r["Activity"].ToString());
                switch (Convert.ToInt32(r["Activity"].ToString()))
                {
                    case 0:
                        layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case 1:
                       layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        break;
                }

                daycrew = r["OrgUnitDay"].ToString(); ;
                nightcrew = r["OrgUnitNight"].ToString();
                afternooncrew = r["OrgUnitAfternoon"].ToString();
                roamingcrew = r["RomingCrew"].ToString();
            }




            loadCurrentValues(WorkplaceID, ProdMonth, SectionID, SectionID_2, false);

        }
        public DataTable getMinerList()
        {
            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _MinerData.SqlStatement = "SELECT SECTIONID,Name FROM dbo.SECTION_COMPLETE " +
                                      "WHERE PRODMONTH = " + theProdmonth.ToString() + "   " +
                                      "AND SECTIONID_2 = '" + theSectionID_2 + "'"+
                                      "ORDER BY NAME";
            _MinerData.ExecuteInstruction();

            return _MinerData.ResultsDataTable;


        }


        public DataTable getWorkplaceList()
        {
            string theType = "";
            switch (theActivity)
            {
                case 0:
                    theType = "0";
                    break;
                case 1:
                    theType = "1";
                    break;
            }
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;

            _WPData.SqlStatement = "Select distinct w.WORKPLACEID, w.DESCRIPTION from WORKPLACE w " +
                        "Where (w.Activity in ('" + theType + "')) " +
                        "AND ((w.Inactive <> 'Y') OR (w.Inactive IS NULL)) " +
                        "AND w.Workplaceid not in (Select pre.Workplaceid from PLANMONTH pre where Prodmonth = " + theProdmonth.ToString() + "  and ISNULL(AutoUnPlan,'') = '') ";

            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        public bool SendRequest()
        {
            bool canContinue;


            #region test if selections are valid

            // test reason
            if (memoEdit1.Text == "")
            {
                MessageBox.Show("Please provide a reason for your request!");
                canContinue = false;

            }

              
                    else
                    {

                        if (editMiningMethod.EditValue == null)
                        {
                            editMiningMethod.EditValue = Convert .ToInt32 ( CBD.ToString());
                            canContinue = true;
                        }

                        else
                        {
                            canContinue = true;
                        }
                    }
                

            #endregion

           

            if (canContinue == true)
            {
                if (theActivity == 0)
                {
                    int sqm1 = decimal.ToInt32(Convert.ToDecimal(sqm));
                    int wastesqm1 = decimal.ToInt32(Convert.ToDecimal(wastesqm));
                    double cubicsqm1 = decimal.ToDouble(Convert.ToDecimal(cubicsqm));
                    double meters1 = decimal.ToDouble(Convert.ToDecimal(0));
                    double wastemeters1 = decimal.ToDouble(Convert.ToDecimal(0));
                    double cubicmeters1 = decimal.ToDouble(Convert.ToDecimal(0));

                    _clChnageOfPlanData.SetProdMonth(Convert.ToInt32(textEdit2.Text));
                    _clChnageOfPlanData.SetSectionID(ABC);
                    _clChnageOfPlanData.SetSectionID_2(theSectionID_2);
                    _clChnageOfPlanData.SetStopDate(Convert.ToDateTime(null));
                    _clChnageOfPlanData.SetRequestBy(TUserInfo.UserID);
                    _clChnageOfPlanData.SetSQMOn(Convert.ToInt32(sqm1));
                    _clChnageOfPlanData.SetSQMOff(Convert.ToInt32(wastesqm1));
                    _clChnageOfPlanData.SetCube(Convert.ToInt32(cubicsqm1));
                    _clChnageOfPlanData.SetMeterOn(Convert.ToInt32(meters1));
                    _clChnageOfPlanData.SetMeterOff(Convert.ToInt32(wastemeters1));
                    _clChnageOfPlanData.SetCube(Convert.ToInt32(cubicmeters1));
                    _clChnageOfPlanData.SetDayCrew(daycrew);
                    _clChnageOfPlanData.SetNightCrew(nightcrew);
                    _clChnageOfPlanData.SetAfternoonCrew(afternooncrew);
                    _clChnageOfPlanData.SetRovingCrew(roamingcrew);
                    _clChnageOfPlanData.SetActivity(theActivity);

                    _clChnageOfPlanData.SetRequestType((int)replanningType.rpMoveWP);
                    _clChnageOfPlanData.SetRequestBy(TUserInfo.UserID);
                    _clChnageOfPlanData.SetActivity(Convert.ToInt32(theActivity));
                    _clChnageOfPlanData.SetOldWorkplaceID(textEdit1.Text);
                    _clChnageOfPlanData.SetWorkplaceID(editToWorkplace.EditValue.ToString ());

                    _clChnageOfPlanData.SetFacelength(Convert.ToInt32(editFL .EditValue ));
                }
                else
                {
                    int sqm1 = decimal.ToInt32(Convert.ToDecimal(0));
                    int wastesqm1 = decimal.ToInt32(Convert.ToDecimal(0));
                    double cubicsqm1 = decimal.ToDouble(Convert.ToDecimal(0));
                    double meters1 = decimal.ToDouble(Convert.ToDecimal(meters));
                    double wastemeters1 = decimal.ToDouble(Convert.ToDecimal(wastemeters));
                    double cubicmeters1 = decimal.ToDouble(Convert.ToDecimal(cubicmeters));


                    _clChnageOfPlanData.SetProdMonth(Convert.ToInt32(textEdit2.Text));
                    _clChnageOfPlanData.SetSectionID(ABC);
                    _clChnageOfPlanData.SetSectionID_2(theSectionID_2);
                    _clChnageOfPlanData.SetStopDate(Convert.ToDateTime(null));
                    _clChnageOfPlanData.SetStartDate(Convert.ToDateTime(null));
                    _clChnageOfPlanData.SetRequestBy(TUserInfo.UserID);
                    _clChnageOfPlanData.SetSQMOn(Convert.ToInt32(sqm1));
                    _clChnageOfPlanData.SetSQMOff(Convert.ToInt32(wastesqm1));
                    _clChnageOfPlanData.SetCube(Convert.ToInt32(cubicsqm1));
                    _clChnageOfPlanData.SetMeterOn(Convert.ToInt32(meters1));
                    _clChnageOfPlanData.SetMeterOff(Convert.ToInt32(wastemeters1));
                    _clChnageOfPlanData.SetCube(Convert.ToInt32(cubicmeters1));
                    _clChnageOfPlanData.SetDayCrew(daycrew);
                    _clChnageOfPlanData.SetNightCrew(nightcrew);
                    _clChnageOfPlanData.SetAfternoonCrew(afternooncrew);
                    _clChnageOfPlanData.SetRovingCrew(roamingcrew);
                    _clChnageOfPlanData.SetActivity(theActivity);
                    _clChnageOfPlanData.SetFacelength(Convert.ToInt32(null));
                    _clChnageOfPlanData.SetRequestType((int)replanningType.rpMoveWP);
                    _clChnageOfPlanData.SetRequestBy(TUserInfo.UserID);
                    _clChnageOfPlanData.SetActivity(Convert.ToInt32(theActivity));
                    _clChnageOfPlanData.SetOldWorkplaceID(textEdit1.Text);
                    _clChnageOfPlanData.SetWorkplaceID(editToWorkplace.EditValue.ToString ());



                }

                _clChnageOfPlanData.sendRequest(theSystemDBTag, UserCurrentInfo.Connection);
                    return true;
                
            }
            else { return false; }

        }

        private void editToWorkplace_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetWorkplaceID(editToWorkplace.EditValue.ToString ());
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;

            _WPData.SqlStatement = "SELECT 1 FL FROM WORKPLACE WHERE WORKPLACEID = '" + editToWorkplace.EditValue + "'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                editFL.Text   = r["FL"].ToString();
            }
        }

        private void editMiningMethod_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetMiningMethod(Convert .ToString (editMiningMethod .EditValue ));
        }


        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetUserComments(memoEdit1.Text);
        }

        private void editFL_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetFacelength(Convert.ToInt32 (editFL.EditValue));
        }
    }



}
