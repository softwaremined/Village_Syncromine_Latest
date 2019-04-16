using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    public partial class ucPlanningValueChanges : ucBaseUserControl
    {
        private DataClass.clChangeOfPlanData _clChnageOfPlanData = new DataClass.clChangeOfPlanData();
           private string theSectionID;
        private string theSectionID_2;
        private int theActivity;
        public ucPlanningValueChanges ()
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
                txtSection.Text = r["Name_2"].ToString();
                txtProdMonth.Text = r["ProdMonth"].ToString();
                txtWorkplaceID.Text = r["WorkplaceID"].ToString();
                txtWorkplaceName.Text = r["WPDesc"].ToString();


                txtOnReefSQMNewC.Text = r["ReefSQM"].ToString();
                txtOffReefSQMNewC .Text  = r["WasteSQM"].ToString();
                txtCubicMeterNEWs.Text = r["CubicMeters"].ToString();
                spinFLNew.Text = r["FL"].ToString();


                txtOnReefMetersNewC.Text = r["Meters"].ToString();
                txtOffReefMetersNewC.Text = r["MetersWaste"].ToString();
                txtCubicMeterNEW.Text = r["CubicMeters"].ToString();
                memoReson.Text = r["Comments"].ToString();
                
                memoReson.Properties.ReadOnly = true;
                theActivity = Convert.ToInt32(r["Activity"]);
                loadCurrentValues(r["WorkplaceID"].ToString(),Convert.ToInt32(r["ProdMonth"].ToString()), r["SectionID"].ToString(), r["SectionID_2"].ToString(), true);


            }

           
        }

        private void loadCurrentValues(String WorkplaceID,int ProdMonth,string SectionID,string SectionID_2, bool ReadOnly)
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM PLANMONTH \n" +
                                   "where Prodmonth = " + Convert.ToString(ProdMonth) + " and \n" +
                                   "      Sectionid = '" + SectionID + "' and \n" +
                                   "      activity=" + theActivity + " and\n" +
                                   "      Workplaceid = '"+ WorkplaceID + "' and plancode='LP'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                theActivity = Convert.ToInt32(r["Activity"].ToString());
                switch (Convert.ToInt32(r["Activity"].ToString()))
                {
                    case 0:
                       Stoppinglayout .Visibility    = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                       developmentlayout .Visibility   = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        developmentlayout . HideToCustomization();      
                        txtOnReefSc.Text = r["ReefSQM"].ToString();
                        txtOffReefSc .Text  = r["WasteSQM"].ToString();

                        txtCubicMetersCs.Text = r["CubicMetres"].ToString();
                        txtFLOld.Text = r["FL"].ToString();

                        //txtOnReefSQMNewC.Text = r["ReefSQM"].ToString();
                        //txtOffReefMetersNewC.Text = r["WasteSQM"].ToString();
                        //txtCubicMeterNEWs.Text = r["CubicMetres"].ToString();
                        //spinFLNew.Text = r["FL"].ToString();

                        //lcStoping.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        //lcDev.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        //lcDev.HideToCustomization();
                        //editSCurrentOnRSQM.Text = r["SQM"].ToString();
                        //editSCurrentOffRSQM.Text = r["WasteSQM"].ToString();
                        //editSCurrentCube.Text = r["CubicMetres"].ToString();
                        break;

                    case 1:
                         developmentlayout .Visibility  = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        Stoppinglayout .Visibility  = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                       Stoppinglayout . HideToCustomization();
                       txtOnReefMetersC.Text = r["ReefAdv"].ToString();
                       txtOffReefMetersC.Text = r["WasteAdv"].ToString();
                       txtCubicMetersC.Text = r["CubicMetres"].ToString();

                       //txtOnReefMetersNewC.Text = r["ReefAdv"].ToString();
                       //txtOffReefMetersNewC.Text = r["WasteAdv"].ToString();
                       //txtCubicMeterNEW.Text = r["CubicMetres"].ToString();

                        //editCurrentDevOnRM.Text = r["Meters"].ToString();
                        //editCurrentDevOffRM.Text = r["MetersWaste"].ToString();
                        //editCurrentDevCube.Text = r["CubicMetres"].ToString();
                        break;

                }
            }

            if (ReadOnly == true)
            {
               // txtCubicMetersC.Properties.ReadOnly = true;
               // txtOnReefMetersC.Properties.ReadOnly = true;
               // txtOffReefMetersC.Properties.ReadOnly = true;
               //// txtCubicMetersC.Properties.ReadOnly = true;
               // txtCubicMetersCs.Properties.ReadOnly = true;
               // txtOnReefSc.Properties.ReadOnly = true;
               // txtOffReefSc.Properties.ReadOnly = true;

                txtCubicMeterNEWs.Properties.ReadOnly = true;
                txtCubicMeterNEW.Properties.ReadOnly = true;
                txtOnReefMetersNewC.Properties.ReadOnly = true;
                txtOnReefSQMNewC.Properties.ReadOnly = true;
                txtOffReefMetersNewC.Properties.ReadOnly = true;
                txtOffReefSQMNewC.Properties.ReadOnly = true;
              //  spinFLNew.Properties.ReadOnly = true;
                //editSNewCube.Properties.ReadOnly = true;
                //editSNewOffRSQM.Properties.ReadOnly = true;
                //editSNewOnRSQM.Properties.ReadOnly = true;
                //editNewDevCube.Properties.ReadOnly = true;
                //editDevNewOffRM.Properties.ReadOnly = true;
                //editDevNewOnRM.Properties.ReadOnly = true;

            }

            //LoadCycles();

        }

        

        public void PlanningChange(String WorkplaceID,int ProdMonth,string SectionID,string SectionID_2, int activity)
        {
            txtWorkplaceID.Text = WorkplaceID;
            txtProdMonth.Text = Convert.ToString(ProdMonth);
            theSectionID = SectionID;
            theSectionID_2 = SectionID_2;
            theActivity = activity;
            _clChnageOfPlanData.SetProdMonth(Convert.ToInt32(ProdMonth));
            _clChnageOfPlanData.SetSectionID(SectionID);
            _clChnageOfPlanData.SetSectionID_2(SectionID_2);
            _clChnageOfPlanData.SetWorkplaceID(WorkplaceID);
            _clChnageOfPlanData .SetRequestType ((int)replanningType .rpCallCahnges );


            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM WORKPLACE WHERE WORKPLACEID = '" + WorkplaceID + "'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                txtWorkplaceName.Text = r["DESCRIPTION"].ToString();
                _clChnageOfPlanData.SetWorplaceName(Convert.ToString(txtWorkplaceName.Text));
            }

            _WPData.SqlStatement = "SELECT DISTINCT Name_2  FROM dbo.SECTION_COMPLETE WHERE PRODMONTH = '" + Convert.ToString(ProdMonth) + "' AND SECTIONID_2 = '" + SectionID_2 + "'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                txtSection.Text = r["Name_2"].ToString();
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
            //if (dateStop.Text == "")
            //{
            //    MessageBox.Show("Please select a stop date!");
            //    canContinue =  false;
            //}

            // test reason

            if (memoReson.Text == "")
            {
                MessageBox.Show("Please provide a reason for your request!");
                canContinue = false;

            }
            else
            {

                //if (string.IsNullOrEmpty(txtOnReefSQMNewC.Text) && string.IsNullOrEmpty(txtOnReefMetersNewC.Text) && string.IsNullOrEmpty(txtOffReefSQMNewC.Text) && string.IsNullOrEmpty(txtOffReefMetersNewC.Text) && string.IsNullOrEmpty(txtCubicMeterNEW.Text) && string.IsNullOrEmpty(txtCubicMeterNEWs.Text))
                //{
                //    MessageBox.Show("Please select at least one value");
                //    canContinue = false;
                //}

                if (txtOnReefSQMNewC.Text=="0" && txtOnReefMetersNewC.Text=="0.0" && txtOffReefSQMNewC.Text=="0" && txtOffReefMetersNewC.Text=="0.0" && txtCubicMeterNEW.Text=="0.0" && txtCubicMeterNEWs.Text=="0")
                {
                    MessageBox.Show("Value cannot be '0'. Please select at least one value");
                    canContinue = false;
                }
                else
                {
                    if (theActivity == 0)
                    {
                        if (spinFLNew.Text == "0")
                        {
                            MessageBox.Show("Facelength value cannot be '0'");
                            canContinue = false; 
                        }
                        canContinue = true;
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
                if (theActivity == 0)
                {
                    DateTime date = new DateTime();
                    //DateTime theNewDate = Convert.ToDateTime(dateStop.Text);
                    _clChnageOfPlanData.SetActivity(Convert.ToInt32(theActivity));
                    //  _clChnageOfPlanData.SetFacelength(Convert.ToInt32(null));
                    _clChnageOfPlanData.SetOldWorkplaceID(Convert.ToString(""));
                    _clChnageOfPlanData.SetDayCrew(Convert.ToString(""));
                    _clChnageOfPlanData.SetAfternoonCrew(Convert.ToString(""));
                    _clChnageOfPlanData.SetRovingCrew(Convert.ToString(""));
                    _clChnageOfPlanData.SetNightCrew(Convert.ToString(""));
                    _clChnageOfPlanData.SetStopDate(Convert.ToDateTime(DateTime.MinValue));
                    _clChnageOfPlanData.SetStartDate(Convert.ToDateTime(DateTime.MinValue));
                    _clChnageOfPlanData.SetRequestBy(Convert.ToString(TUserInfo.UserID));
                    _clChnageOfPlanData.SetMiningMethod(Convert.ToString(""));
                }
                else
                {
                    DateTime date = new DateTime();
                    //DateTime theNewDate = Convert.ToDateTime(dateStop.Text);
                    _clChnageOfPlanData.SetActivity(Convert.ToInt32(theActivity));
                    //  _clChnageOfPlanData.SetFacelength(Convert.ToInt32(null));
                    _clChnageOfPlanData.SetOldWorkplaceID(Convert.ToString(""));
                    _clChnageOfPlanData.SetDayCrew(Convert.ToString(""));
                    _clChnageOfPlanData.SetAfternoonCrew(Convert.ToString(""));
                    _clChnageOfPlanData.SetRovingCrew(Convert.ToString(""));
                    _clChnageOfPlanData.SetNightCrew(Convert.ToString(""));
                    _clChnageOfPlanData.SetStopDate(Convert.ToDateTime(DateTime.MinValue));
                    _clChnageOfPlanData.SetStartDate(Convert.ToDateTime(DateTime.MinValue));
                    _clChnageOfPlanData.SetRequestBy(Convert.ToString(TUserInfo.UserID));
                    _clChnageOfPlanData.SetMiningMethod(Convert.ToString(""));
                }

                _clChnageOfPlanData.sendRequest(theSystemDBTag, UserCurrentInfo.Connection);

                return true;
            }
            else { return false; }

        }
        //public ucPlanningValueChanges()
        //{
        //    InitializeComponent();
        //}

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelControl2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panelControl2_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void txtOnReefSQMNewC_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetSQMOn(Convert.ToInt32(txtOnReefSQMNewC .EditValue ));
        }

        private void txtOffReefSQMNewC_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetSQMOff(Convert.ToInt32(txtOffReefSQMNewC.EditValue));
        }

        private void txtCubicMeterNEWs_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetCube (Convert.ToInt32(txtCubicMeterNEWs.EditValue));
        }

        private void txtOnReefMetersNewC_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetMeterOn(Convert.ToInt32(txtOnReefMetersNewC.EditValue));
        }

        private void txtOffReefMetersNewC_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetMeterOff(Convert.ToInt32(txtOffReefMetersNewC.EditValue));
             
        }

        private void txtCubicMeterNEW_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetCube(Convert.ToInt32(txtCubicMeterNEW.EditValue));
        }

        private void memoReson_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetUserComments(Convert.ToString(memoReson.Text));
        }

        private void spinFLNew_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetFacelength(Convert.ToInt32(spinFLNew.EditValue));
        }

        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }


      

     

      

     
        private void gcMoCycle_DragDrop(object sender, DragEventArgs e)
        {
            
        }

        private void lbxCodeCycles_MouseDown(object sender, MouseEventArgs e)
        {
         
        }

        public string ExtractBeforeColon(string TheString)
        {
            if (TheString != "")
            {
                string BeforeColon;
                int index = TheString.IndexOf(":");
                BeforeColon = TheString.Substring(0, index);
                return BeforeColon;
            }
            else
            {
                return "";
            }
        }

        private void gcMoCycle_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gcMoCycle_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

       
        
    }
}
