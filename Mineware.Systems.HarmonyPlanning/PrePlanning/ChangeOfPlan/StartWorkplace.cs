using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    public partial class StartWorkplace : ucBaseUserControl
    {
        private DataClass.clChangeOfPlanData _clChnageOfPlanData = new DataClass.clChangeOfPlanData();
        private string theSectionID;
        private string theSectionID_2;
        private int theActivity;
        public DateTime stopdate;
        public StartWorkplace()
        {
            InitializeComponent();
           // _clChnageOfPlanData.SetActivity(
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
                txtSection.Text = r["Name_2"].ToString();
                txtProdMonth.Text = r["ProdMonth"].ToString();
                txtWorkplaceID.Text = r["WorkplaceID"].ToString();
                txtWorkplaceName.Text = r["WPDesc"].ToString();
                editSNewCube.Text = r["CubicMeters"].ToString();
                editSNewOffRSQM.Text = r["WasteSQM"].ToString();
                editSNewOnRSQM.Text = r["ReefSQM"].ToString();
                editNewDevCube.Text = r["CubicMeters"].ToString();
                editDevNewOffRM.Text = r["MetersWaste"].ToString();
                editDevNewOnRM.Text = r["Meters"].ToString();
                memoReason.Text = r["Comments"].ToString();
                dateStart.DateTime = Convert.ToDateTime(r["StartDate"].ToString());

                dateStart.Properties.ReadOnly = true;
                memoReason.Properties.ReadOnly = true;

                loadCurrentValues(r["WorkplaceID"].ToString(),Convert.ToInt32(r["ProdMonth"].ToString()), r["SectionID"].ToString(), r["SectionID_2"].ToString(), true);


            }
        }

        private void loadCurrentValues(String WorkplaceID,int ProdMonth,string SectionID,string SectionID_2, bool ReadOnly)
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM PLANMONTH " +
                                   "where Prodmonth = " + Convert.ToString(ProdMonth) + " and " +
                                   "      Sectionid = '" + SectionID + "' and " +
                                   "      Workplaceid = '"+ WorkplaceID + "' and plancode='LP'";
            _WPData.ExecuteInstruction();

            
            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                //stopdate = Convert.ToDateTime(r["StoppedDate"].ToString ());
                theActivity = Convert.ToInt32(r["Activity"].ToString());
                switch (Convert.ToInt32(r["Activity"].ToString()))
                {
                    case 0:
                        lcStoping1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcDev1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        lcDev1.HideToCustomization();
                       
                        editSCurrentOnRSQM.Text = r["ReefSQM"].ToString();
                        editSCurrentOffRSQM.Text = r["WasteSQM"].ToString();
                        editSCurrentCube.Text = r["CubicMetres"].ToString();
                        break;

                    case 1:
                        lcDev1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcStoping1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        lcStoping1.HideToCustomization();
                        editCurrentDevOnRM.Text = r["ReefAdv"].ToString();
                        editCurrentDevOffRM.Text = r["WasteAdv"].ToString();
                        editCurrentDevCube.Text = r["CubicMetres"].ToString();
                        break;

                }
            }

            if (ReadOnly == true)
            {
                editSNewCube.Properties.ReadOnly = true;
                editSNewOffRSQM.Properties.ReadOnly = true;
                editSNewOnRSQM.Properties.ReadOnly = true;
                editNewDevCube.Properties.ReadOnly = true;
                editDevNewOffRM.Properties.ReadOnly = true;
                editDevNewOnRM.Properties.ReadOnly = true;
                txtProdMonth.Properties.ReadOnly = true;
                txtSection.Properties.ReadOnly = true;
                txtWorkplaceID.Properties.ReadOnly = true;
                txtWorkplaceName.Properties.ReadOnly = true;
                dateStart.Properties.ReadOnly = true;
                memoReason.Properties.ReadOnly = true;
                editCurrentDevCube.Properties.ReadOnly = true;
                editCurrentDevOffRM.Properties.ReadOnly = true;
                editCurrentDevOnRM.Properties.ReadOnly = true;
                editSCurrentCube.Properties.ReadOnly = true;
                editSCurrentOffRSQM.Properties.ReadOnly = true;
                editSCurrentOnRSQM.Properties.ReadOnly = true;

            }

            _clChnageOfPlanData.SetActivity(theActivity);

        }

        

        public void StopWorkplace(String WorkplaceID,int ProdMonth,string SectionID,string SectionID_2)
        {
            txtWorkplaceID.Text = WorkplaceID;
            txtProdMonth.Text = Convert.ToString(ProdMonth);
            theSectionID = SectionID;
            theSectionID_2 = SectionID_2;
            _clChnageOfPlanData.SetProdMonth(Convert.ToInt32(ProdMonth));
            _clChnageOfPlanData.SetWorkplaceID(WorkplaceID);
            _clChnageOfPlanData.SetSectionID(SectionID);
            _clChnageOfPlanData.SetSectionID_2(SectionID_2);
            _clChnageOfPlanData.SetRequestType((int)replanningType.rpStartWP );

            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            DataTable theDates = new DataTable();
            if (BMEBL.get_BeginEndDates(SectionID_2, ProdMonth.ToString()) == true)
            {
                theDates = BMEBL.ResultsDataTable;
            }

            DateTime theBeginDate = new DateTime();
            DateTime theEndDate = new DateTime();

           foreach (DataRow r in theDates.Rows)
               {
                   theBeginDate = Convert.ToDateTime(r["StartDate"].ToString());
                   theEndDate = Convert.ToDateTime(r["EndDate"].ToString());
                   
               }


           dateStart.Properties.MaxValue = theEndDate;
           dateStart.Properties.MinValue = theBeginDate;

            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM WORKPLACE WHERE WORKPLACEID = '" + WorkplaceID + "'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                txtWorkplaceName.Text = r["DESCRIPTION"].ToString();
            }

            _WPData.SqlStatement = "SELECT DISTINCT Name_2  FROM dbo.SECTION_COMPLETE WHERE PRODMONTH = '" + Convert.ToString(ProdMonth) + "' AND SECTIONID_2 = '" + SectionID_2 + "'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                txtSection.Text = r["Name_2"].ToString();
               // _clChnageOfPlanData.SetSectionID(Convert.ToInt32(txtSection.EditValue));
            }

            loadCurrentValues(WorkplaceID, ProdMonth, SectionID, SectionID_2,false);

        }

        public bool SendRequest()
        {
            bool canContinue;
            //MWDataManager.clsDataAccess _sendRequest = new MWDataManager.clsDataAccess();
            //_sendRequest.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_sendRequest.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //_sendRequest.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_sendRequest.SqlStatement = "sp_RequestReplanning";

            #region test if selections are valid
            // test stop date
            if (dateStart.Text == "")
            {
                MessageBox.Show("Please select a Start date!");
                canContinue = false;
            }
            else
            {
                // canContinue = true;
                // }
                // test reason
                if (memoReason.Text == "")
                {
                    MessageBox.Show("Please provide a reason for your request!");
                    canContinue = false;

                }
                else
                {
                    // canContinue = true;
                    // }
                    if (Convert.ToDateTime(dateStart.EditValue) < stopdate)
                    {
                        MessageBox.Show("StartDate cannot be less than StopDate");
                        canContinue = false;
                    }
                    else
                    {
                        canContinue = true;
                    }
                }
            }

            //switch (theActivity)
            //{
            //    case 0:
            //        if (Convert.ToInt64(editSCurrentOnRSQM.Text) > 0 && Convert.ToInt64(editSNewOnRSQM.Text) ==  )
            //        break;
            //    case 1:
            //        break;
            //}

            #endregion 



            if (canContinue == true)
            {
                
                //TODO:We will need to remove the following code in the future 
                //DateTime theNewDate = Convert.ToDateTime(dateStop.Text);
                
                //switch (theActivity)
                //{
                //    case 0:
                //        SqlParameter[] _paramCollectionS = 
                //            {
                //             _sendRequest.CreateParameter("@requestType", SqlDbType.Int, 0,(int)replanningType.rpStopWp),
                //             _sendRequest.CreateParameter("@ProdMonth", SqlDbType.Int, 0,Convert.ToInt32(txtProdMonth.Text)),
                //             _sendRequest.CreateParameter("@WorkplaceID", SqlDbType.Int, 0,Convert.ToInt32(txtWorkplaceID.Text)),
                //             _sendRequest.CreateParameter("@SectionID", SqlDbType.VarChar, 20,theSectionID),
                //             _sendRequest.CreateParameter("@SectionID_2", SqlDbType.VarChar, 20,theSectionID_2),
                //             _sendRequest.CreateParameter("@StopDate", SqlDbType.VarChar, 20,String.Format("{0:yyy/MM/dd}", theNewDate)),
                //             _sendRequest.CreateParameter("@UserComments", SqlDbType.VarChar, 20,memoReason.Text),
                //             _sendRequest.CreateParameter("@RequestBy", SqlDbType.VarChar, 20,TUserInfo.UserID),
                //             _sendRequest.CreateParameter("@SQMOn", SqlDbType.Float, 0,Convert.ToDecimal(editSNewOnRSQM.Text)),
                //             _sendRequest.CreateParameter("@SQMOff", SqlDbType.Float, 0,Convert.ToDecimal(editSNewOffRSQM.Text)),
                //             _sendRequest.CreateParameter("@Cube", SqlDbType.Float, 0,Convert.ToDecimal(editSNewCube.Text)),
                //             _sendRequest.CreateParameter("@MeterOn", SqlDbType.Float, 0,0),
                //             _sendRequest.CreateParameter("@MeterOff", SqlDbType.Float, 0,0),
                //             _sendRequest.CreateParameter("@StartDate", SqlDbType.VarChar, 20,String.Format("{0:yyy/MM/dd}", DBNull .Value )),
                //               _sendRequest .CreateParameter ("@DayCrew",SqlDbType .VarChar ,10,"" ),
                //             _sendRequest .CreateParameter ("@NightCrew",SqlDbType .VarChar ,10,"" ),
                //             _sendRequest .CreateParameter ("@AfternoonCrew",SqlDbType .VarChar ,10,"" ),
                //        _sendRequest .CreateParameter ("@RovingCrew",SqlDbType .VarChar ,10,"" ),
                //         _sendRequest .CreateParameter ("@MiningMethod",SqlDbType .VarChar ,20,""),
                //           _sendRequest .CreateParameter ("@OldWorkplaceID",SqlDbType .VarChar ,20,""),
                //           _sendRequest .CreateParameter ("@activity",SqlDbType .Int  ,0,theActivity )
                //            };
                //        _sendRequest.ParamCollection = _paramCollectionS;
                //        break;

                //    case 1:
                //        SqlParameter[] _paramCollectionD = 
                //            {
                //             _sendRequest.CreateParameter("@requestType", SqlDbType.Int, 0,(int)replanningType.rpStopWp),
                //             _sendRequest.CreateParameter("@ProdMonth", SqlDbType.Int, 0,Convert.ToInt32(txtProdMonth.Text)),
                //             _sendRequest.CreateParameter("@WorkplaceID", SqlDbType.Int, 0,Convert.ToInt32(txtWorkplaceID.Text)),
                //             _sendRequest.CreateParameter("@SectionID", SqlDbType.VarChar, 20,theSectionID),
                //             _sendRequest.CreateParameter("@SectionID_2", SqlDbType.VarChar, 20,theSectionID_2),
                //             _sendRequest.CreateParameter("@StopDate", SqlDbType.VarChar, 20,String.Format("{0:yyy/MM/dd}", theNewDate)),
                //             _sendRequest.CreateParameter("@UserComments", SqlDbType.VarChar, 20,memoReason.Text),
                //             _sendRequest.CreateParameter("@RequestBy", SqlDbType.VarChar, 20,TUserInfo.UserID),
                //             _sendRequest.CreateParameter("@SQMOn", SqlDbType.Float, 0,0),
                //             _sendRequest.CreateParameter("@SQMOff", SqlDbType.Float, 0,0),
                //             _sendRequest.CreateParameter("@Cube", SqlDbType.Float, 0,Convert.ToDecimal(editNewDevCube.Text)),
                //             _sendRequest.CreateParameter("@MeterOn", SqlDbType.Float, 0,Convert.ToDecimal(editDevNewOnRM.Text)),
                //             _sendRequest.CreateParameter("@MeterOff", SqlDbType.Float, 0,Convert.ToDecimal(editDevNewOffRM.Text)),
                //             _sendRequest.CreateParameter("@StartDate", SqlDbType.VarChar, 20,String.Format("{0:yyy/MM/dd}", DBNull .Value )),
                //               _sendRequest .CreateParameter ("@DayCrew",SqlDbType .VarChar ,10,"" ),
                //             _sendRequest .CreateParameter ("@NightCrew",SqlDbType .VarChar ,10,"" ),
                //             _sendRequest .CreateParameter ("@AfternoonCrew",SqlDbType .VarChar ,10,"" ),
                //        _sendRequest .CreateParameter ("@RovingCrew",SqlDbType .VarChar ,10,"" ),
                //         _sendRequest .CreateParameter ("@MiningMethod",SqlDbType .VarChar ,20,""),
                //           _sendRequest .CreateParameter ("@OldWorkplaceID",SqlDbType .VarChar ,20,""),
                //           _sendRequest .CreateParameter ("@activity",SqlDbType .Int  ,0,theActivity )
                //            };
                //        _sendRequest.ParamCollection = _paramCollectionD;
                //        break;
                //}

               

                //_sendRequest.ExecuteInstruction();
                _clChnageOfPlanData.sendRequest(theSystemDBTag,UserCurrentInfo.Connection);
                return true;
            }
            else { return false; }

        }


        private void txtProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetProdMonth(Convert.ToInt32(txtProdMonth.Text));
        }

        private void txtWorkplaceID_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetWorkplaceID(txtWorkplaceID.Text);
        }

        private void txtWorkplaceName_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetWorplaceName(txtWorkplaceName.Text);
        }

        private void dateStop_EditValueChanged(object sender, EventArgs e)
        {
           // _clChnageOfPlanData.SetStopDate(Convert.ToDateTime(dateStart.EditValue));
        }

  

        private void editSCurrentOnRSQM_EditValueChanged(object sender, EventArgs e)
        {

        }

        //private void editSNewOnRSQM_EditValueChanged(object sender, EventArgs e)
        //{
        //    _clChnageOfPlanData.SetSQMOn(Convert.ToInt32(editSNewOnRSQM.EditValue));
        //}

        //private void editSNewOffRSQM_EditValueChanged_1(object sender, EventArgs e)
        //{
        //    _clChnageOfPlanData.SetSQMOff(Convert.ToInt32(editSNewOffRSQM.EditValue));
        //}

        //private void editSNewCube_EditValueChanged_1(object sender, EventArgs e)
        //{
        //    _clChnageOfPlanData.SetCube(Convert.ToDouble(editSNewCube.EditValue));
        //}

        //private void editDevNewOnRM_EditValueChanged_1(object sender, EventArgs e)
        //{
        //    _clChnageOfPlanData.SetMeterOn(Convert.ToDouble(editDevNewOnRM.EditValue));
        //}

        //private void editDevNewOffRM_EditValueChanged_1(object sender, EventArgs e)
        //{
        //    _clChnageOfPlanData.SetMeterOff(Convert.ToDouble(editDevNewOffRM.EditValue));
        //}

        //private void editNewDevCube_EditValueChanged_1(object sender, EventArgs e)
        //{
        //    _clChnageOfPlanData.SetCube(Convert.ToDouble(editNewDevCube.EditValue));
        //}

        private void memoReason_EditValueChanged_1(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetUserComments(memoReason.Text);
        }

        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void editNewDevCube_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetCube(Convert.ToDouble(editNewDevCube.EditValue));
        }

        private void editSNewOnRSQM_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetSQMOn(Convert.ToInt32(editSNewOnRSQM.EditValue));
        }

        private void editSNewOffRSQM_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetSQMOff(Convert.ToInt32(editSNewOffRSQM.EditValue));
        }

        private void editSNewCube_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetCube(Convert.ToDouble(editSNewCube.EditValue));
        }

        private void editDevNewOnRM_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetMeterOn(Convert.ToDouble(editDevNewOnRM.EditValue));
        }

        private void editDevNewOffRM_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetMeterOff(Convert.ToDouble(editDevNewOffRM.EditValue));
        }

        private void dateStart_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetStartDate(Convert.ToDateTime(dateStart.EditValue));
        }

        
    }
}
