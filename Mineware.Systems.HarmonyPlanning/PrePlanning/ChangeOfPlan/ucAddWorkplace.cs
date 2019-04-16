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
using Mineware.Systems.Planning;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.Data;
using FastReport;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    public partial class ucAddWorkplace : ucBaseUserControl
    {
        private DataClass.clChangeOfPlanData _clChnageOfPlanData = new DataClass.clChangeOfPlanData();
        string theSectionID_2;
        int theProdmonth;
        int theActivity;
        string theWorkPlaceID;
        string theSectionID;
        string theSectionIDMO;
        public string CBD;

        public ucAddWorkplace()
        {
            InitializeComponent();

        }

        public void LoadDetails(int ChangeRequestID)
        {


            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT DISTINCT PPCR.ChangeRequestID,PPCR.FL, SC.Name_3 Section,SC.Name_2,SC.Name,PPCR.StartDate,PPCR.ProdMonth,PPCR.WorkplaceID,WP.DESCRIPTION WPDesc, \r\n" +
                                   " PPCR.DayCrew,PPCR.SectionID,PPCR.AfternoonCrew,PPCR.NightCrew,PPCR.RovingCrew,PPCR.Activity, \r\n" +
                                   "PPCR.StopDate,PPCR.MiningMethod,m.Description Description,PPCR.Comments,PPCR.ReefSQM,PPCR.WasteSQM,PPCR.Meters,PPCR.MetersWaste,PPCR.CubicMeters, \r\n" +
                                   "PPCR.SectionID,PPCR.SectionID_2 FROM PrePlanning_ChangeRequest PPCR \r\n" +
                                   "INNER JOIN SECTION_COMPLETE SC on \r\n" +
                                   "PPCR.SectionID_2 = SC.SECTIONID_2 and \r\n" +
                                   "PPCR.ProdMonth = SC.PRODMONTH and \r\n" +
                                   "PPCR.SectionID = SC.SECTIONID \r\n" +
                                   "INNER JOIN WORKPLACE WP on \r\n" +
                                   "PPCR.WorkplaceID = WP.WORKPLACEID inner join Code_Methods m on ppcr.MiningMethod = m.TargetID \r\n" +
                                   "WHERE PPCR.ChangeRequestID = " + Convert.ToString(ChangeRequestID);
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                editSection.Text = r["Name_2"].ToString();
                editPeodMonth.Text = r["ProdMonth"].ToString();
                editWorkplace.Properties.NullText = r["WPDesc"].ToString();
                editStartDate.Text = r["StartDate"].ToString();
                editMiner.Properties.NullText = r["Name"].ToString();
                editDayCrew.Properties.NullText = r["DayCrew"].ToString();
                editAfternoonCrew.Properties.NullText = r["AfternoonCrew"].ToString();
                editNightCrew.Properties.NullText = r["NightCrew"].ToString();
                editRovingCrew.Properties.NullText = r["RovingCrew"].ToString();
                editOnSqm.Text = r["ReefSQM"].ToString();
                editOffSqm.Text = r["WasteSQM"].ToString();
                editStopeCubes.Text = r["CubicMeters"].ToString();
                editONMeters.Text = r["Meters"].ToString();
                editOffMeters.Text = r["MetersWaste"].ToString();
                devCubes.Text = r["CubicMeters"].ToString();
                editFaceLength.Text = r["FL"].ToString();
                editReason.Text = r["Comments"].ToString();
                editMiningMethod.Properties.NullText = r["Description"].ToString();             

               

                loadCurrentValues1(Convert.ToInt32(r["ProdMonth"].ToString()), r["SectionID"].ToString(), r["SectionID_2"].ToString(), r["WorkplaceID"].ToString(), Convert.ToInt32(r["Activity"].ToString()), Convert.ToInt32(r["ChangeRequestID"].ToString()), true);
            }

        }


        private void loadCurrentValues1(int ProdMonth, string SectionID, string SectionID_2, String WorkplaceID, int Activity, int ChangeRequestID, bool ReadOnly)
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM PrePlanning_ChangeRequest " +
                                   "where ProdMonth = " + Convert.ToString(ProdMonth) + " and " +
                                   "      SectionID_2 = '" + SectionID_2 + "' and SectionID='" + SectionID + "' and WorkplaceID='" + WorkplaceID + "' and Activity='" + Activity + "' and ChangeRequestID='" + ChangeRequestID + "'";

            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                // theActivity = Convert.ToInt32(r["ActivityCode"].ToString());
                // switch (Convert.ToInt32(r["ActivityCode"].ToString()))
                switch (Activity)
                {
                    case 0:
                        // theType = "S";
                        lcStoping.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcDev.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        lcDev.HideToCustomization();
                        break;
                    case 1:
                        // theType = "D";
                        lcDev.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcStoping.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        lcStoping.HideToCustomization();
                        break;
                }
            }
            if (ReadOnly == true)
            {
                editWorkplace.Properties.ReadOnly = true;
                editStartDate.Properties.ReadOnly = true;
                editMiner.Properties.ReadOnly = true;
                editMiningMethod.Properties.ReadOnly = true;
                editDayCrew.Properties.ReadOnly = true;
                editNightCrew.Properties.ReadOnly = true;
                editAfternoonCrew.Properties.ReadOnly = true;
                editRovingCrew.Properties.ReadOnly = true;
                editStopeCubes.Properties.ReadOnly = true;
                editOnSqm.Properties.ReadOnly = true;
                editOffSqm.Properties.ReadOnly = true;
                editONMeters.Properties.ReadOnly = true;
                editOffMeters.Properties.ReadOnly = true;
                devCubes.Properties.ReadOnly = true;
                editReason.Properties.ReadOnly = true;
                editFaceLength.Properties.ReadOnly = true;
            }
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

            _WPData.SqlStatement = "Select distinct w.WORKPLACEID, w.DESCRIPTION from WORKPLACE w \r" +
                        "Where (w.Activity in ('" + theType + "')) \r" +
                        "AND ((w.Inactive <> 'Y') OR (w.Inactive IS NULL)) \r" +
                         "AND w.Workplaceid not in ( Select Workplaceid from (\r" +
                        " Select pre.Workplaceid from PLANMONTH pre where ISNULL(isStopped,'')='' and  Prodmonth = " + theProdmonth.ToString() + " and ISNULL(AutoUnPlan,'')='' \r" +
                        " union \r" +
                        " select [WorkplaceID] from [dbo].[PREPLANNING_CHANGEREQUEST] where Prodmonth = " + theProdmonth.ToString() + " and [ChangeID]in(2,5)) a where a.workplaceid is not null)\r " +
                        " union \r" +
                          "  select wp.WORKPLACEID, wp.DESCRIPTION from PREPLANNING_CHANGEREQUEST ppcr inner join PREPLANNING_CHANGEREQUEST_APPROVAL ppcra on \r" +
                          "  ppcra.changerequestid=ppcr.changerequestid inner join workplace wp on \r" +
                          "  wp.workplaceid=ppcr.workplaceid where \r" +
                          "  ppcra.declined=1 and ppcra.approved=0 and \r" +
                          "  ppcr.prodmonth=" + theProdmonth.ToString() + "\r" +
                          " AND ppcr.Workplaceid not in \r(" +
                         " Select pre.Workplaceid from PLANMONTH pre where ISNULL(isStopped,'')='' and  Prodmonth = " + theProdmonth.ToString() + " and ISNULL(AutoUnPlan,'')='')";

            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        public DataTable getMinerList()
        {
            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _MinerData.SqlStatement = "SELECT SECTIONID,Name FROM dbo.SECTION_COMPLETE " +
                                      "WHERE PRODMONTH = " + theProdmonth.ToString() + "  AND " +
                                      "SECTIONID_2 = '" + theSectionID_2 + "' ORDER BY Name";
            _MinerData.ExecuteInstruction();

            return _MinerData.ResultsDataTable;


        }


        private void loadCurrentValues(int ProdMonth, string SectionID, string SectionID_2, String WorkplaceID, int Activity, bool ReadOnly)
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM PLANMONTH " +
                                   "where ProdMonth = " + Convert.ToString(ProdMonth) + " and " +
                                   " SectionID='" + SectionID + "' and WorkplaceID='" + WorkplaceID + "' and Activity='" + Activity + "'";

            _WPData.ExecuteInstruction();

            //foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            //{
                // theActivity = Convert.ToInt32(r["ActivityCode"].ToString());
                // switch (Convert.ToInt32(r["ActivityCode"].ToString()))
               
                switch (Activity)
                {
                    case 0:
                        // theType = "S";
                        lcStoping.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcDev.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        lcDev.HideToCustomization();
                        break;
                    case 1:
                        // theType = "D";
                        lcDev.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lcStoping.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        lcStoping.HideToCustomization();
                        break;
               // }
            }
            if (ReadOnly == true)
            {
                editWorkplace.Properties.ReadOnly = true;
                editStartDate.Properties.ReadOnly = true;
                editMiner.Properties.ReadOnly = true;
                editMiningMethod.Properties.ReadOnly = true;
                editDayCrew.Properties.ReadOnly = true;
                editNightCrew.Properties.ReadOnly = true;
                editAfternoonCrew.Properties.ReadOnly = true;
                editRovingCrew.Properties.ReadOnly = true;
                editStopeCubes.Properties.ReadOnly = true;
                editOnSqm.Properties.ReadOnly = true;
                editOffSqm.Properties.ReadOnly = true;
                editONMeters.Properties.ReadOnly = true;
                editOffMeters.Properties.ReadOnly = true;
                devCubes.Properties.ReadOnly = true;
                editReason.Properties.ReadOnly = true;
            }
        }



        public void addWorkplace(String WorkplaceID, int ProdMonth, string SectionID, string SectionID_2, int Activity)
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;

            theActivity = Activity;
            theProdmonth = ProdMonth;
            theSectionID_2 = SectionID_2;
            theWorkPlaceID = WorkplaceID;
            theSectionID = SectionID;

            _clChnageOfPlanData.SetSectionID(SectionID );
            _clChnageOfPlanData.SetProdMonth(Convert.ToInt32 (ProdMonth ));
            _clChnageOfPlanData.SetSectionID_2(SectionID_2);
            _clChnageOfPlanData.SetWorkplaceID(WorkplaceID);
            _clChnageOfPlanData.SetRequestType((int)replanningType.rpNewWP);

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

            editStartDate.Properties.MaxValue = theEndDate;
            editStartDate.Properties.MinValue = theBeginDate;

            CPMBusinessLayer.clsBusinessLayer BMEBL1 = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL1._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL1.SetsystemDBTag = this.theSystemDBTag;
            BMEBL1.SetUserCurrentInfo = this.UserCurrentInfo;
            editMiningMethod.Properties.DataSource = BMEBL1.getMiningMethods(theActivity);
            editMiningMethod.Properties.ValueMember = "TargetID";//TargetID
            editMiningMethod.Properties.DisplayMember = "Description";



            // theSectionIDMO = SectionIDMO;
            editWorkplace.Properties.DataSource = getWorkplaceList();
            editWorkplace.Properties.ValueMember = "WORKPLACEID";
            //editWorkplace.Properties.ValueMember = "Workplaceid";
            editWorkplace.Properties.DisplayMember = "DESCRIPTION";

            editMiner.Properties.DataSource = getMinerList();
            editMiner.Properties.ValueMember = "SECTIONID";
            editMiner.Properties.DisplayMember = "Name";




            string prodmonth = Convert.ToString(theProdmonth);
            editPeodMonth.Text = prodmonth;

            _WPData.SqlStatement = "SELECT DISTINCT Name_2 FROM dbo.SECTION_COMPLETE WHERE PRODMONTH = '" + Convert.ToString(ProdMonth) + "' AND SECTIONID_2 = '" + SectionID_2 + "'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                editSection.Text = r["Name_2"].ToString();
            }

            _WPData.SqlStatement = "SELECT * FROM WORKPLACE WHERE WORKPLACEID = '" + editWorkplace .EditValue  + "'";
            _WPData.ExecuteInstruction();



            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                editFaceLength.Text = r["FL"].ToString();
            }
            _clChnageOfPlanData.SetActivity(Activity);
            loadCurrentValues(ProdMonth, SectionID, SectionID_2, WorkplaceID, Activity, false);

        }

        public bool SendRequest()
        {
            bool canContinue;


            #region test if selections are valid
            // test stop date
            if (editWorkplace.Text == "")
            {
                MessageBox.Show("Please select a workplace");
                ActiveControl = editWorkplace;
                canContinue = false;
            }
            else
            {
                if (editStartDate.Text == "")
                {
                    MessageBox.Show("Please select a value");
                    canContinue = false;
                }
                else
                {
                    if (editMiner.Text == "")
                    {
                        MessageBox.Show("Please select a value");
                        canContinue = false;
                    }
                    if (editMiningMethod.Text == "")
                    {
                        MessageBox.Show("Please select a Mining Method");
                        canContinue = false;
                    }
                    else
                    {
                        // if (dateStop.Text == "")
                        if (string.IsNullOrEmpty(editDayCrew.Text) && string.IsNullOrEmpty(editNightCrew.Text) && string.IsNullOrEmpty(editAfternoonCrew.Text) && string.IsNullOrEmpty(editRovingCrew.Text))
                        //if (editDayCrew.Text == "" || editNightCrew.Text == "" || editAfternoonCrew.Text == "" || editRovingCrew.Text == "")
                        {
                            MessageBox.Show("Please select at least one value");
                            canContinue = false;
                        }
                        else
                        {
                            if (editStopeCubes.Text == "" && editOnSqm.Text == "" && editOffSqm.Text == "" && editONMeters.Text == "" && editOffMeters.Text == "" && devCubes.Text == "")
                            {
                                MessageBox.Show("Please select at least one value");
                                canContinue = false;
                            }
                            else
                            {
                                // test reason
                                if (editReason.Text == "")
                                {
                                    MessageBox.Show("Please provide a reason for your request!");
                                    canContinue = false;

                                }
                                else
                                {
                                    if (theActivity == 0 && Convert.ToInt32(editFaceLength.EditValue) == 0)
                                    {
                                        //if (spinEdit7.EditValue == null)
                                        //  {
                                        // spinEdit7.EditValue = Convert.ToInt32(CBD.ToString());
                                        MessageBox.Show("Please provide a valid Face Length");
                                        canContinue = false;
                                        // }
                                    }
                                    else
                                    {
                                        if (theActivity == 1 && Convert.ToInt32(editONMeters.EditValue) == 0 && Convert.ToInt32(editOffMeters.EditValue) == 0 && Convert.ToInt32(devCubes.EditValue) == 0)
                                        {
                                            MessageBox.Show("Please provide a value greater than '0' for On Reef Metres/Off Reef Metres/ Cubic Metres");
                                            canContinue = false;
                                        }
                                        else
                                        {
                                            if (theActivity == 0 && Convert.ToInt32(editStopeCubes.EditValue) == 0 && Convert.ToInt32(editOnSqm.EditValue) == 0 && Convert.ToInt32(editOffSqm.EditValue) == 0)
                                            {
                                                MessageBox.Show("Please provide a value greater than '0' for On Reef SQM/Off Reef SQM/ Cubic SQM");
                                                canContinue = false;
                                            }
                                            else
                                            {
                                                canContinue = true;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }


            #endregion



            if (canContinue == true)
            {
                _clChnageOfPlanData.SetOldWorkplaceID(Convert.ToString(""));
                _clChnageOfPlanData.sendRequest(theSystemDBTag,UserCurrentInfo.Connection);
                return true;
            }
            else { return false; }

        }
        //return false;


        private void editDayCrew_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetDayCrew(editDayCrew.Text);
        }

        private void editDayCrew_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            if (BMEBL.get_reOrgDaySelection(theProdmonth, theSectionID_2) == true)
            {

                editDayCrew.Properties.DataSource = BMEBL.ResultsDataTable;
                editDayCrew.Properties.DisplayMember = "Crew_Org";
                editDayCrew.Properties.ValueMember = "Crew_Org";

            }

            editDayCrew.Text = editDayCrew.SelectedText;
        }

        private void editNightCrew_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            if (BMEBL.get_reOrgDaySelection(theProdmonth, theSectionID_2) == true)
            {

                editNightCrew.Properties.DataSource = BMEBL.ResultsDataTable;
                editNightCrew.Properties.DisplayMember = "Crew_Org";
                editNightCrew.Properties.ValueMember = "Crew_Org";

            }
            editNightCrew.Text = editNightCrew.SelectedText;
        }

        private void editDayCrew_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void editAfternoonCrew_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            if (BMEBL.get_reOrgDaySelection(theProdmonth, theSectionID_2) == true)
            {

                editAfternoonCrew.Properties.DataSource = BMEBL.ResultsDataTable;
                editAfternoonCrew.Properties.DisplayMember = "Crew_Org";
                editAfternoonCrew.Properties.ValueMember = "Crew_Org";

            }
            editAfternoonCrew.Text = editAfternoonCrew.SelectedText;
        }

        private void editRovingCrew_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            if (BMEBL.get_reOrgDaySelection(theProdmonth, theSectionID_2) == true)
            {

                editRovingCrew.Properties.DataSource = BMEBL.ResultsDataTable;
                editRovingCrew.Properties.DisplayMember = "Crew_Org";
                editRovingCrew.Properties.ValueMember = "Crew_Org";

            }
            editRovingCrew.Text = editRovingCrew.SelectedText;
        }

        private void editWorkplace_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void editWorkplace_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetWorkplaceID  (editWorkplace .EditValue.ToString ());

            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;

            _WPData.SqlStatement = "SELECT *, 1 FL FROM WORKPLACE WHERE WORKPLACEID = '" + editWorkplace.EditValue + "'";
            _WPData.ExecuteInstruction();



            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                editFaceLength.Text = r["FL"].ToString();
            }


        }

        private void editMiningMethod_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetMiningMethod(Convert .ToString ( editMiningMethod.EditValue)); 
        }

        private void editStartDate_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetStartDate(Convert.ToDateTime(editStartDate.EditValue));
        }

        private void editMiner_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetSectionID(editMiner.EditValue.ToString());
        }

        private void editAfternoonCrew_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetAfternoonCrew(Convert.ToString(editAfternoonCrew.Text));
        }

        private void editNightCrew_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetNightCrew(Convert.ToString(editNightCrew.Text));
        }

        private void editRovingCrew_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetRovingCrew(Convert.ToString(editRovingCrew.Text));
        }

        private void spinEdit7_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetFacelength(Convert.ToInt32(editFaceLength .EditValue ));
        }

        private void spinEdit2_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetSQMOn(Convert.ToInt32(editOnSqm.EditValue ));
        }

        private void spinEdit3_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetSQMOff(Convert.ToInt32(editOffSqm.EditValue));
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetCube(Convert.ToInt32(editStopeCubes .EditValue ));
        }

        private void spinEdit4_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetMeterOn(Convert.ToInt32(editONMeters.EditValue));
        }

        private void spinEdit5_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetMeterOff(Convert.ToInt32(editOffMeters.EditValue));
        }

        private void spinEdit6_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetCube (Convert.ToInt32(devCubes.EditValue));
        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {
            _clChnageOfPlanData.SetUserComments(Convert.ToString(editReason.Text));
        }

        private void memoEdit1_EditValueChanged_1(object sender, EventArgs e)
        {

        }
    }
}

