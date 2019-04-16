using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.Planning.PrePlanning.ChangeOfPlan.DataClass;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.ProductionGlobal;


namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
   
    public partial class ucStopWorkplace : ucBaseUserControl
    {
        private clChangeOfPlanData _clChnageOfPlanData = new clChangeOfPlanData();
        private string theSectionID;
        private string theSectionID_2;
        private int theActivity;
        public ucStopWorkplace()
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
                dateStop.DateTime = Convert.ToDateTime(r["StopDate"].ToString());
                try
                {
                    DeleteBooking.Checked = Convert.ToBoolean(r["DeleteBookings"].ToString());
                }
                catch
                {
                    DeleteBooking.Checked = false;
                }

                dateStop.Properties.ReadOnly = true;
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
                                   "      Workplaceid = '"+ WorkplaceID + "'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                theActivity = Convert.ToInt32(r["Activity"].ToString());
                switch (Convert.ToInt32(r["Activity"].ToString()))
                {
                    case 0:
                        lcStoping.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcDev.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        lcDev.HideToCustomization();
                        editSCurrentOnRSQM.Text = r["ReefSQM"].ToString();
                        editSCurrentOffRSQM.Text = r["WasteSQM"].ToString();
                        editSCurrentCube.Text = r["CubicMetres"].ToString();
                        break;

                    case 1:
                        lcDev.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcStoping.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        lcStoping.HideToCustomization();
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

            }

            _clChnageOfPlanData.SetActivity(theActivity);

        }

        

        public void StopWorkplace(String WorkplaceID,int ProdMonth,string SectionID,string SectionID_2)
        {
            txtWorkplaceID.Text = WorkplaceID;
            txtProdMonth.Text = Convert.ToString(ProdMonth);
            theSectionID = SectionID;
            theSectionID_2 = SectionID_2;
            _clChnageOfPlanData.SetSectionID(SectionID);
            _clChnageOfPlanData.SetSectionID_2(SectionID_2);
            _clChnageOfPlanData.SetRequestType((int)replanningType.rpStopWp);

            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            DataTable theDates = new DataTable();
            BMEBL.SetsystemDBTag = theSystemDBTag;
            BMEBL.SetUserCurrentInfo = UserCurrentInfo;
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


           dateStop.Properties.MaxValue = theEndDate;
           dateStop.Properties.MinValue = theBeginDate ;

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
            }

            loadCurrentValues(WorkplaceID, ProdMonth, SectionID, SectionID_2,false);

        }

        public bool SendRequest()
        {
            bool canContinue;

            #region test if selections are valid
            // test stop date
            if (dateStop.Text == "")
            {
                MessageBox.Show("Please select a stop date!");
                canContinue =  false;
            }

            // test reason
            else if (memoReason.Text == "")
            {
                MessageBox.Show("Please provide a reason for your request!");
                canContinue = false;

            }
            else
            {
                canContinue = true;
            }

            #endregion 



            if (canContinue == true)
            {
                _clChnageOfPlanData.SetDayCrew(Convert.ToString(""));
                _clChnageOfPlanData.SetAfternoonCrew(Convert.ToString(""));
                _clChnageOfPlanData.SetRovingCrew(Convert.ToString(""));
                _clChnageOfPlanData.SetNightCrew(Convert.ToString(""));
                _clChnageOfPlanData.sendRequest(theSystemDBTag, UserCurrentInfo.Connection);
                return true;
            }
            else
            {
                return false;
            }

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
            _clChnageOfPlanData.SetStopDate(Convert.ToDateTime(dateStop.EditValue));
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

        private void editNewDevCube_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetCube(Convert.ToDouble (editNewDevCube.EditValue));
        }

        private void memoReason_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetUserComments(memoReason.Text);
        }

        private void DeleteBooking_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetDeleteBookings(DeleteBooking.Checked);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            SendRequest();
            bool messageSend = false;


            messageSend = SendRequest();
        }
              
           
    }
}
