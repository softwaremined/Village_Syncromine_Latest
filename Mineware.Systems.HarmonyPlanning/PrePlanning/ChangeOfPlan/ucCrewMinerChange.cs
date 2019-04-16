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
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.Data;
using FastReport;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    public partial class ucCrewMinerChange : ucBaseUserControl
    {
        private DataClass.clChangeOfPlanData _clChnageOfPlanData = new DataClass.clChangeOfPlanData();
        private string theSectionID;
        private string theWorkplaceID;
        string theSectionID_2;
        string theNewSectionID_2;
        int theProdmonth;
        public string ABC = "";
        public string  daycrew="";
        public string nightcrew = "";
        public string afternooncrew = "";
        public string romingcrew = "";
        public int theActivity;
        public DataTable dtdaycrew;
        public DataTable dtaftercrew;
        public DataTable dtnightcrew;
        public DataTable dtrovingcrew;
        public ucCrewMinerChange()
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
                txtWorkplace.Text = r["WPDesc"].ToString();


             
                editMiner1.Properties.NullText = r["Name"].ToString();
                editDay1.Properties.NullText = "";
                editNight1.Properties.NullText = "";
                editRoming1.Properties.NullText = "";
                editAfternoon1.Properties.NullText = "";
               
                memoReason.Text = r["Comments"].ToString();

                theActivity = Convert.ToInt32(r["Activity"]);
                loadManageValues (r["WorkplaceID"].ToString(), Convert.ToInt32(r["ProdMonth"].ToString()), r["SectionID"].ToString(), r["SectionID_2"].ToString(), true);
            }
        }


        private void loadManageValues(string WorkplaceID, int ProdMonth, string SectionID, string SectionID_2, bool ReadOnly)
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;

            _WPData.SqlStatement = "SELECT * FROM PLANMONTH WHERE Workplaceid = '" + WorkplaceID + "' and " +
                                    "Prodmonth = " + Convert.ToString(ProdMonth) + "  and activity=" + theActivity + " and PLANCODE='LP'";
            _WPData.ExecuteInstruction();

            //string ABC="";
            string OnreefS = "";
            string OffreefS = "";
            
            string cube = "";
            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                //  txtMiner.Text = r["Sectionid_2"].ToString();
                txtDay.Text = r["OrgUnitDay"].ToString();
                txtNight.Text = r["OrgUnitNight"].ToString();
                txtAfternoon.Text = r["OrgUnitAfternoon"].ToString();
                txtRoving.Text = r["RomingCrew"].ToString();
                ABC = r["Sectionid"].ToString();
                OnreefS = r["SQM"].ToString();
                OffreefS = r["WasteSQM"].ToString();
                cube = r["CubicMetres"].ToString();

               
            }


            _WPData.SqlStatement = "SELECT DISTINCT Name, Name_2  FROM dbo.SECTION_COMPLETE WHERE PRODMONTH = '" + Convert.ToString(ProdMonth) + "' AND SECTIONID_2 = '" + SectionID_2 + "' AND SECTIONID='" + ABC.ToString() + "'";

            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                txtMiner.Text = r["Name"].ToString();
            }


            if (ReadOnly == true)
            {
                editMiner1.Properties.ReadOnly = true;
                editDay1.Properties.ReadOnly = true;
                editAfternoon1.Properties.ReadOnly = true;
                editNight1.Properties.ReadOnly = true;
                editRoming1.Properties.ReadOnly = true;
                memoReason.Properties.ReadOnly = true;

            }
        }

        //private void loadCurrentValues(string Section,string Description, int ProdMonth, string SectionID,string day,string night,string afternoon,string roving, bool ReadOnly)
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


            //_WPData.SqlStatement = "SELECT * FROM PrePlanning WHERE Workplaceid = '" + WorkplaceID + "'";
            //_WPData.ExecuteInstruction();

            //string ABC="";
            string OnreefS = "";
            string OffreefS = "";
            
            string cube = "";
            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                txtDay.Text = r["OrgUnitDay"].ToString();
                txtNight.Text = r["OrgUnitNight"].ToString();
                txtAfternoon.Text = r["OrgUnitAfternoon"].ToString();
                txtRoving.Text = r["RomingCrew"].ToString();
                 ABC = r["Sectionid"].ToString();
                 OnreefS = r["SQM"].ToString();
                 OffreefS = r["WasteSQM"].ToString();
                 cube = r["CubicMetres"].ToString();
            }

            _WPData.SqlStatement = "SELECT DISTINCT Name, Name_2  FROM dbo.SECTION_COMPLETE WHERE PRODMONTH = '" + Convert.ToString(ProdMonth) + "' AND SECTIONID_2 = '" + SectionID_2 + "' AND SECTIONID='" +ABC.ToString ()+ "'";
           
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                txtSection.Text = r["Name_2"].ToString();
                txtMiner.Text = r["Name"].ToString();
                editMiner1.Properties.NullText = r["Name"].ToString();
            }

           
            _WPData.SqlStatement = "SELECT * FROM WORKPLACE WHERE WORKPLACEID = '" + WorkplaceID + "'";
          
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                txtWorkplace .Text  = r["DESCRIPTION"].ToString();
            }

            if (ReadOnly == true)
            {
                editDay1.Properties.ReadOnly = true;
                editAfternoon1.Properties.ReadOnly = true;
                editNight1.Properties.ReadOnly = true;
                editRoming1.Properties.ReadOnly = true;
                memoReason.Properties.ReadOnly = true;
                editMiner1.Properties.ReadOnly = true;
            }



        }

        public void changecrew(String WorkplaceID, int ProdMonth, string SectionID, string SectionID_2, int activity)
        {

            txtProdMonth.Text = Convert.ToString(ProdMonth);
            theSectionID = SectionID;
            theWorkplaceID = WorkplaceID;
            theProdmonth = ProdMonth;
            theSectionID_2 = SectionID_2;
            theNewSectionID_2 = SectionID_2;

            MOEdit.Properties.DataSource = getMOList();
            MOEdit.Properties.ValueMember = "SectionID";
            MOEdit.Properties.DisplayMember = "MO";

            MOEdit.EditValue = theSectionID_2;
            editMiner1.Properties.DataSource = getMinerList();
            editMiner1.Properties.ValueMember = "SECTIONID";
            editMiner1.Properties.DisplayMember = "Name";

            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            if (BMEBL.get_reOrgDaySelection(theProdmonth, theNewSectionID_2) == true)
            {
                // editDay1.Properties.DataSource = BMEBL.ResultsDataTable;
                dtaftercrew = BMEBL.ResultsDataTable;
                editAfternoon1.Properties.DataSource = dtaftercrew;
                editAfternoon1.Properties.DisplayMember = "Crew_Org";
                editAfternoon1.Properties.ValueMember = "Crew_Org";

                dtnightcrew = BMEBL.ResultsDataTable;
                editNight1.Properties.DataSource = dtnightcrew;
                editNight1.Properties.DisplayMember = "Crew_Org";
                editNight1.Properties.ValueMember = "Crew_Org";

                dtrovingcrew = BMEBL.ResultsDataTable;
                editRoming1.Properties.DataSource = dtrovingcrew;
                editRoming1.Properties.DisplayMember = "Crew_Org";
                editRoming1.Properties.ValueMember = "Crew_Org";
            }

            CPMBusinessLayer.clsBusinessLayer BMEBL1 = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL1._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL1.SetsystemDBTag = this.theSystemDBTag;
            BMEBL1.SetUserCurrentInfo = this.UserCurrentInfo;
            if (BMEBL1.get_reOrgDaySelection(theProdmonth, theNewSectionID_2) == true)
            {

                DataRow[] rows = BMEBL1.ResultsDataTable.Select("Crew_Org=''");
                BMEBL1.ResultsDataTable.Rows.Remove(rows[0]);
                dtdaycrew = BMEBL1.ResultsDataTable;
                editDay1.Properties.DataSource = dtdaycrew;
                editDay1.Properties.DisplayMember = "Crew_Org";
                editDay1.Properties.ValueMember = "Crew_Org";
            }
            editDay1.Text = editDay1.SelectedText;
            editNight1.Text = editNight1.SelectedText;
            editAfternoon1.Text = editAfternoon1.SelectedText;
            editRoming1.Text = editRoming1.SelectedText;



            theActivity = activity;
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM WORKPLACE WHERE WORKPLACEID = '" + WorkplaceID + "'";
            _WPData.ExecuteInstruction();



            _WPData.SqlStatement = "SELECT * FROM PLANMONTH WHERE Workplaceid = '" + WorkplaceID + "' and Prodmonth='" + ProdMonth + "'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                txtDay.Text = r["OrgUnitDay"].ToString();
                txtNight.Text = r["OrgUnitNight"].ToString();
                txtAfternoon.Text = r["OrgUnitAfternoon"].ToString();
                txtRoving.Text = r["RomingCrew"].ToString();

                editDay1.Properties.NullText = "";
                editDay1.EditValue = r["OrgUnitDay"].ToString();
                _clChnageOfPlanData .SetDayCrew (r["OrgUnitDay"].ToString());
                editNight1.Properties.NullText = "";
                editNight1.EditValue = r["OrgUnitNight"].ToString();
                _clChnageOfPlanData .SetNightCrew (r["OrgUnitNight"].ToString());
                editAfternoon1.Properties.NullText = "";
                editAfternoon1.EditValue = r["OrgUnitAfternoon"].ToString();
                _clChnageOfPlanData .SetAfternoonCrew (r["OrgUnitAfternoon"].ToString());
                editRoming1.Properties.NullText = "";
                editRoming1.EditValue = r["RomingCrew"].ToString();
                _clChnageOfPlanData .SetRovingCrew (r["RomingCrew"].ToString());

            }

            loadCurrentValues(WorkplaceID, ProdMonth, SectionID, SectionID_2, false);

        }

        private void editDay1_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetDayCrew(Convert .ToString ( editDay1 .Text  ));
        }

        private void editDay1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void editAfternoon1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void editNight1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void editRoming1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        public DataTable getMOList()
        {
            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _MinerData.SqlStatement = "SELECT Distinct SECTIONID_2 SectionID,Name_2 MO FROM dbo.SECTION_COMPLETE " +
                                      "WHERE PRODMONTH = " + theProdmonth.ToString() + "   " +
                                      "--AND SECTIONID_2 = '" + theSectionID_2 + "' " +
                                      "ORDER BY Name";
            _MinerData.ExecuteInstruction();

            return _MinerData.ResultsDataTable;


        }
        public DataTable getMinerList()
        {
            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _MinerData.SqlStatement = "SELECT SECTIONID,Name FROM dbo.SECTION_COMPLETE " +
                                      "WHERE PRODMONTH = " + theProdmonth.ToString() + "   " +
                                      "AND SECTIONID_2 = '" + theNewSectionID_2 + "' "+ 
                                      "ORDER BY Name";
            _MinerData.ExecuteInstruction();

            return _MinerData.ResultsDataTable;


        }

        public bool SendRequest()
        {
            bool canContinue;


            #region test if selections are valid
            // test stop date

            if (editMiner1.Text == "")
            {
                MessageBox.Show("Please select a value");
                canContinue = false;
            }
            else
            {
                if (string.IsNullOrEmpty(editDay1.Text))
                {
                    MessageBox.Show("Please select at least one value");
                    canContinue = false;
                }
                else
                {
                    //// if (dateStop.Text == "")
                    //if (string.IsNullOrEmpty(editNight1.Text) && string.IsNullOrEmpty(editAfternoon1.Text) && string.IsNullOrEmpty(editRoming1.Text))
                    ////if (editDayCrew.Text == "" || editNightCrew.Text == "" || editAfternoonCrew.Text == "" || editRovingCrew.Text == "")
                    //{
                    //    MessageBox.Show("Please select at least one value");
                    //    canContinue = false;
                    //}
                    //else
                    //{
                    // test reason
                    if (memoReason.Text == "")
                    {
                        MessageBox.Show("Please provide a reason for your request!");
                        canContinue = false;

                    }
                    else
                    {
                        if (editMiner1.EditValue == null )
                        {
                            //MessageBox.Show("Please select at least one value");
                            //canContinue = false;
                            editMiner1.EditValue = ABC.ToString();
                           // editMiningMethod.EditValue = Convert.ToInt32(CBD.ToString());
                            canContinue = true;
                        }

                        else
                        {
                            canContinue = true;
                        }
                    }
                }
            }
             // }




            if (canContinue == true)
            {
                DateTime value = new DateTime();
                value = DateTime.MinValue;
                daycrew = editDay1.Text;
                nightcrew = editNight1.Text;
                afternooncrew = editAfternoon1.Text;
                romingcrew = editRoming1.Text;
                _clChnageOfPlanData.SetSectionID_2(theSectionID_2);
                _clChnageOfPlanData.SetWorkplaceID(theWorkplaceID);
                _clChnageOfPlanData.SetStartDate(value );
                _clChnageOfPlanData.SetStopDate(value );
                _clChnageOfPlanData.SetSQMOn(0);
                _clChnageOfPlanData.SetSQMOff(0);
                _clChnageOfPlanData.SetMeterOn(0);
                _clChnageOfPlanData.SetMeterOff(0);
                _clChnageOfPlanData.SetCube(0);
                _clChnageOfPlanData.SetMiningMethod("");
                _clChnageOfPlanData.SetOldWorkplaceID("");
                _clChnageOfPlanData.SetActivity(Convert.ToInt32(theActivity ));
                _clChnageOfPlanData.SetFacelength(Convert.ToInt32(null));
                _clChnageOfPlanData.SetRequestType((int)replanningType.rpCrewChnages);
                _clChnageOfPlanData.SetProdMonth(Convert.ToInt32(theProdmonth));
                _clChnageOfPlanData.SetRequestBy(TUserInfo.UserID);
                
                _clChnageOfPlanData.sendRequest(theSystemDBTag, UserCurrentInfo.Connection);
                return true;
            }
            else { return false; }



            #endregion
        }

        private void editMiner1_EditValueChanged(object sender, EventArgs e)
        {
            theNewSectionID_2 = _clChnageOfPlanData.getSectionID_2for_Miner(theSystemDBTag, UserCurrentInfo.Connection, editMiner1.EditValue.ToString(), theProdmonth.ToString());
            _clChnageOfPlanData.SetSectionID(editMiner1.EditValue.ToString());
        }

        private void editAfternoon1_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetAfternoonCrew(Convert .ToString ( editAfternoon1 .Text  ));
        }

        private void editNight1_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetNightCrew(Convert .ToString ( editNight1 .Text  ));
        }

        private void editRoming1_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetRovingCrew(Convert .ToString ( editRoming1 .Text  ));
        }

        private void memoReason_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetUserComments(Convert .ToString ( memoReason.Text));
        }

        private void MOEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (theNewSectionID_2 != MOEdit.EditValue.ToString())
            {
                theNewSectionID_2 = MOEdit.EditValue.ToString();

                DataTable dt = getMinerList();
                editMiner1.Properties.DataSource = dt;
                editMiner1.Properties.ValueMember = "SECTIONID";
                editMiner1.Properties.DisplayMember = "Name";

                CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
                BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
                BMEBL.SetsystemDBTag = this.theSystemDBTag;
                BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

                if (BMEBL.get_reOrgDaySelection(theProdmonth, theNewSectionID_2) == true)
                {
                    // editDay1.Properties.DataSource = BMEBL.ResultsDataTable;
                    dtaftercrew = BMEBL.ResultsDataTable;
                    editAfternoon1.Properties.DataSource = dtaftercrew;
                    editAfternoon1.Properties.DisplayMember = "Crew_Org";
                    editAfternoon1.Properties.ValueMember = "Crew_Org";

                    dtnightcrew = BMEBL.ResultsDataTable;
                    editNight1.Properties.DataSource = dtnightcrew;
                    editNight1.Properties.DisplayMember = "Crew_Org";
                    editNight1.Properties.ValueMember = "Crew_Org";

                    dtrovingcrew = BMEBL.ResultsDataTable;
                    editRoming1.Properties.DataSource = dtrovingcrew;
                    editRoming1.Properties.DisplayMember = "Crew_Org";
                    editRoming1.Properties.ValueMember = "Crew_Org";
                }

                CPMBusinessLayer.clsBusinessLayer BMEBL1 = new CPMBusinessLayer.clsBusinessLayer();
                BMEBL1._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
                BMEBL1.SetsystemDBTag = this.theSystemDBTag;
                BMEBL1.SetUserCurrentInfo = this.UserCurrentInfo;
                if (BMEBL1.get_reOrgDaySelection(theProdmonth, theNewSectionID_2) == true)
                {

                    DataRow[] rows = BMEBL1.ResultsDataTable.Select("Crew_Org=''");
                    BMEBL1.ResultsDataTable.Rows.Remove(rows[0]);
                    //DataRow[] rows1 = BMEBL.ResultsDataTable.Select("Crew_Org='Contractor'");
                    //BMEBL.ResultsDataTable.Rows.Remove(rows1[0]);
                    dtdaycrew = BMEBL1.ResultsDataTable;
                    editDay1.Properties.DataSource = dtdaycrew;
                    editDay1.Properties.DisplayMember = "Crew_Org";
                    editDay1.Properties.ValueMember = "Crew_Org";
                }
                editDay1.Text = editDay1.SelectedText;
                editNight1.Text = editNight1.SelectedText;
                editAfternoon1.Text = editAfternoon1.SelectedText;
                editRoming1.Text = editRoming1.SelectedText;


                editMiner1.EditValue = dt.Rows[0][0].ToString();
                editDay1.EditValue = null;
                editAfternoon1.EditValue = null;
                editNight1.EditValue = null;
                editRoming1.EditValue = null;


            }
        }
    }
}

    

