using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System.Windows.Forms;
using Mineware.Systems.Planning.PrePlanning.ChangeOfPlan.DataClass;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    public partial class ucDrillRiggChanges : ucBaseUserControl
    {
        private clChangeOfPlanData _clChnageOfPlanData = new clChangeOfPlanData();
        private string theSectionID;
        private string theSectionID_2;
        private int theActivity;
        public string CBD = "";
        public ucDrillRiggChanges()
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
                memoReason.Text = r["Comments"].ToString();
                memoReason.Properties.ReadOnly = true;
                loadCurrentValues(r["WorkplaceID"].ToString(), Convert.ToInt32(r["ProdMonth"].ToString()), r["SectionID"].ToString(), r["SectionID_2"].ToString(), true);
                editDrillRigg.Properties.NullText = r["DrillRig"].ToString();

            }


        }

        private void loadCurrentValues(String WorkplaceID, int ProdMonth, string SectionID, string SectionID_2, bool ReadOnly)
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM PLANMONTH " +
                                   "where Prodmonth = " + Convert.ToString(ProdMonth) + " and " +
                                   "      Sectionid = '" + SectionID + "' and " +
                                   "      Workplaceid = '" + WorkplaceID + "' and plancode='LP'";
            _WPData.ExecuteInstruction();
            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {

                editDrillRiggFrom .Properties .NullText  = r["DrillRig"].ToString();
            }

            if (ReadOnly == true)
            {
                editDrillRiggFrom.Properties.ReadOnly = true;
                editDrillRigg.Properties.ReadOnly = true;
                memoReason.Properties.ReadOnly = true;
            }


            _clChnageOfPlanData.SetActivity(theActivity);
        }

        public void DrillRig(string WorkplaceID, int ProdMonth, string SectionID, string SectionID_2, int activity)
        {
            theActivity = activity;
            txtWorkplaceID.Text = WorkplaceID;
            txtProdMonth.Text = Convert.ToString(ProdMonth);
            theSectionID = SectionID;
            theSectionID_2 = SectionID_2;
            _clChnageOfPlanData.SetSectionID(SectionID);
            _clChnageOfPlanData.SetSectionID_2(SectionID_2);
            _clChnageOfPlanData.SetProdMonth(Convert.ToInt32(txtProdMonth.Text));
            _clChnageOfPlanData.SetWorkplaceID(txtWorkplaceID.Text);
            _clChnageOfPlanData.SetRequestType((int)replanningType.rpDrillRig );
            // _clChnageOfPlanData.SetRequestType((int)replanningType.rpStopWp);

            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            DataTable theDates = new DataTable();
            BMEBL.SetsystemDBTag = theSystemDBTag;
            BMEBL.SetUserCurrentInfo = UserCurrentInfo;

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


            CPMBusinessLayer.clsBusinessLayer BMEBL1 = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL1._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL1.SetsystemDBTag = this.theSystemDBTag;
            BMEBL1.SetUserCurrentInfo = this.UserCurrentInfo;
          //  editMiningMethod.Properties.DataSource = BMEBL1.getMiningMethods(theActivity);

            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _MinerData.SqlStatement = "SELECT '' DRID union " +
                                      "select distinct DRID from DRILLRIG_PROJECT  DRP " +
                                      "INNER JOIN vw_Workplace_WorkArea WPWA ON " +
                                      "DRP.Project = WPWA.Project";
            _MinerData.ExecuteInstruction();
            editDrillRigg.Properties.DataSource = _MinerData.ResultsDataTable;     
            editDrillRigg.Properties.ValueMember = "DRID"; ;//TargetID
            editDrillRigg.Properties.DisplayMember = "DRID"; ;

            _WPData.SqlStatement = "SELECT PP.DrillRig from PLANMONTH PP " +
                            " where PP.ProdMonth='" + ProdMonth + "' and  PP.Workplaceid='" + WorkplaceID + "' and PP.Sectionid='" + SectionID + "' and PP.Sectionid_2='" + SectionID_2 + "'";

            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {

                editDrillRigg.Properties.NullText = r["DrillRig"].ToString();
                editDrillRigg.EditValue = editDrillRigg.Properties.GetKeyValueByDisplayText(r["DrillRig"].ToString());
              //  CBD = r["TargetID"].ToString();
            }

            loadCurrentValues(WorkplaceID, ProdMonth, SectionID, SectionID_2, false);

        }

        public bool SendRequest()
        {


            bool canContinue = true;

            #region test if selections are valid
            // test stop date
            if (editDrillRigg.Text == "")
            {
                //MessageBox.Show("Please select a stop date!");
                //canContinue = false;
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

                _clChnageOfPlanData.sendRequest(theSystemDBTag, UserCurrentInfo.Connection);
                return true;
            }
            else { return false; }


        }

        private void editDrillRigg_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetDrillRig(Convert.ToString(editDrillRigg.EditValue));
        }

        private void memoReason_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetUserComments(Convert.ToString(memoReason.Text));
        }

        //private void editMiningMethod_EditValueChanged(object sender, EventArgs e)
        //{
        //    _clChnageOfPlanData.SetMiningMethod(Convert.ToString(editDrillRigg.EditValue));
        //}

        //private void memoReason_EditValueChanged(object sender, EventArgs e)
        //{
        //    _clChnageOfPlanData.SetUserComments(Convert.ToString(memoReason.Text));
        //}
    }
}
