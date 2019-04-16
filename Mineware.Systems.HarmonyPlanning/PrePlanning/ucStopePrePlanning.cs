using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Planning.PrePlanning
{
    public partial class ucStopePrePlanning : ucBaseUserControl
    {
        private PlanningDefaults PlanningClass;
        public ucStopePrePlanning()
        {
            InitializeComponent();
            ShowProgressPanel = true;
            ShowProgressPanel = false;
            ucCycleScreen1.CycleValue += UcCycleScreen1_CycleValue;
            ucCycleScreen1.Visible = false;
        }

        public int GetSelectedRow()
        {
            return viewPlanningStoping.GetDataSourceRowIndex(viewPlanningStoping.FocusedRowHandle);
        }

        /// <summary>
        /// To allow the view to do a PostEditor
        /// </summary>
        public void ForceViewUpdate()
        {
            viewPlanningStoping.PostEditor();
            viewPlanningStoping.RefreshData();
        }

        public void SavePlanning()
        {

            PlanningClass.SaveStopingData();
        }

        private void UcCycleScreen1_CycleValue(object sender, EventArgs e)
        {
            string WorkplaceID = viewPlanningStoping.GetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["Workplaceid"]).ToString();
            var WPPlanningData = PlanningClass.PlanningCycle.planningCycleData
                    .Where(a => a.WorkplaceID == WorkplaceID)
                    .FirstOrDefault();

            viewPlanningStoping.SetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["callValue"], WPPlanningData.CycleCall);
        }

        public void SetPlanningClass(PlanningDefaults clsPlanning)
        {
            PlanningClass = clsPlanning;
            ucPlanningHeader.SetPlanningSettings(PlanningClass.PlanningSettings);

            #region Set calender selections in grid max and min date selection 
            MainGrid.DataSource = PlanningClass.tblPlanningData;
            reStartDate.MinValue = PlanningClass.theBeginDate;
            reStartDate.MaxValue = PlanningClass.theEndDate;
            reEndDate.MinValue = PlanningClass.theBeginDate;
            reEndDate.MaxValue = PlanningClass.theEndDate;
            #endregion

            #region Set Miner list in grid
            reMinerSelection.DataSource = PlanningClass.tblMinerListData;
            reMinerSelection.DisplayMember = "Name";
            reMinerSelection.ValueMember = "SECTIONID";
            reMinerSelection.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            #endregion

            #region Set Orgunit Lists
            reOrgDaySelection.DataSource = PlanningClass.tblOrgUnitsData;
            reOrgDaySelection.DisplayMember = "Crew_Org";
            reOrgDaySelection.ValueMember = "GangNo";
            reOrgDaySelection.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

            reOrgNightSelection.DataSource = PlanningClass.tblOrgUnitsData;
            reOrgNightSelection.DisplayMember = "Crew_Org";
            reOrgNightSelection.ValueMember = "GangNo";
            reOrgNightSelection.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

            reOrgAfternoon.DataSource = PlanningClass.tblOrgUnitsData;
            reOrgAfternoon.DisplayMember = "Crew_Org";
            reOrgAfternoon.ValueMember = "GangNo";
            reOrgAfternoon.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

            reOrgRoving.DataSource = PlanningClass.tblOrgUnitsData;
            reOrgRoving.DisplayMember = "Crew_Org";
            reOrgRoving.ValueMember = "GangNo";
            reOrgRoving.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            #endregion

            reMiningMethods.DataSource = PlanningClass.tblMiningMethods;
            reMiningMethods.DisplayMember = "Description";
            reMiningMethods.ValueMember = "TargetID";
            //  ucCycleScreen1.SetData();
        }

        private void reMinerSelection_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit editor = sender as DevExpress.XtraEditors.LookUpEdit;
            DataRowView row = editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue) as DataRowView;
            object value = row["SectionID"];

            string ShiftBoss = PlanningClass.GetShiftBoss(value.ToString());

            viewPlanningStoping.SetRowCellValue(viewPlanningStoping.FocusedRowHandle, viewPlanningStoping.Columns["Sectionid_1"], ShiftBoss);

        }

        private void viewPlanningStoping_ShowingEditor(object sender, CancelEventArgs e)
        {

            GridView view;
            view = sender as GridView;

            if ((view.FocusedColumn.FieldName == "CMGT" || view.FocusedColumn.FieldName == "ChannelW" || view.FocusedColumn.FieldName == "IdealSW") && PlanningClass.tblPlanningData.Rows[view.FocusedRowHandle]["isApproved"].ToString() == "1")
            {
                e.Cancel = true;
            }
            if (PlanningClass.tblPlanningData.Rows[view.FocusedRowHandle]["hasRevised"] != null)
            {
                if (Convert.ToBoolean(PlanningClass.tblPlanningData.Rows[view.FocusedRowHandle]["hasRevised"].ToString()) == true)
                {
                    e.Cancel = true;
                }
            }
        }

        public void calVariance()
        {
            viewPlanningStoping.InvalidateFooter();
            viewPlanningStoping.RefreshData();
        }

        private void viewPlanningStoping_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void viewPlanningStoping_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {

             
                string WorkplaceID = viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["Workplaceid"]).ToString();
                string SectionID = viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["SectionID"]).ToString();
                var WPPlanningData = PlanningClass.PlanningCycle.planningCycleData
                        .Where(a => a.WorkplaceID == WorkplaceID)
                        .FirstOrDefault();
                if (e.Column.FieldName == "MonthlyReefSQM")
                {
                    int WasteSQM = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyWatseSQM"]));

                    int newCall = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyReefSQM"])) + WasteSQM;

                    viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyTotalSQM"], newCall);

                    WPPlanningData.MonthCall = newCall;
                    ucCycleScreen1.RefreshGrid();
                }

                if (e.Column.FieldName == "MonthlyWatseSQM")
                {
                    int ReefSQM = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyReefSQM"]));

                    int newCall = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyWatseSQM"])) + ReefSQM;

                    viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyTotalSQM"], newCall);

                    WPPlanningData.MonthCall = newCall;
                    ucCycleScreen1.RefreshGrid();
                }

                if (e.Column.FieldName == "MonthlyTotalSQM")
                {
                    string theCallValue = viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"]).ToString();
                    if (Convert.ToDouble(theCallValue) == 0)
                    {
                        if(WPPlanningData != null)
                        {
                            double newCall = WPPlanningData.DailyCycleCall * WPPlanningData.TotalShifts;
                            viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"], newCall.ToString());
                        }
                    }
                    else
                    {
                        viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"], e.Value.ToString());
                    }

                    //if (viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"]).ToString() != TotlSqmLbl.Text)
                    //{
                    //    viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"], e.Value.ToString());
                    //}

                    //viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"], e.Value.ToString());
                }
                // when Face Length changes we need to update the cycle face length value 
                if (e.Column.FieldName == "FL")
                {
                    double newFL = Convert.ToDouble(e.Value.ToString());
                    if (WPPlanningData != null)
                    {
                        WPPlanningData.FaceLength = newFL;
                        double newCall = WPPlanningData.DailyCycleCall * WPPlanningData.TotalShifts;
                        WPPlanningData.CycleCall = newCall;
                        viewPlanningStoping.SetRowCellValue(e.RowHandle,
                                                            viewPlanningStoping.Columns["callValue"],
                                                            newCall);

                        var updateCycle = PlanningClass.PlanningCycle.planningCycleData
                            .Where(a => a.WorkplaceID == WorkplaceID)
                            .FirstOrDefault();
                        if(updateCycle != null)
                        {
                            PlanningClass.PlanningCycle.planningCycleData.Remove(updateCycle);
                            updateCycle = null;
                            PlanningClass.AddCyclePlan(WorkplaceID,
                                                       SectionID,
                                                       PlanningClass.PlanningSettings.MOSectionID,
                                                       PlanningClass.PlanningSettings.ProdMonth,
                                                       PlanningClass.PlanningSettings.ActivityID,
                                                       WPPlanningData.FaceLength);
                        }
                        WPPlanningData = null;
                        var wpCycleDataNew = PlanningClass.PlanningCycle.planningCycleData
                            .Where(a => a.WorkplaceID == WorkplaceID)
                            .FirstOrDefault();
                        newCall = wpCycleDataNew.DailyCycleCall * wpCycleDataNew.TotalShifts;
                        int MonthlyTotalSQM = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle,
                                                                                                  viewPlanningStoping.Columns["MonthlyTotalSQM"]));
                        wpCycleDataNew.CycleCall = newCall;
                        wpCycleDataNew.MonthCall = MonthlyTotalSQM;
                        wpCycleDataNew.UpdateCycleData();
                        viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"], wpCycleDataNew.CycleCall.ToString());
                        ucCycleScreen1.CycleData = wpCycleDataNew;
                        ucCycleScreen1.SetData();
                        ucCycleScreen1.RefreshGrid();
                    }
                    else
                    {
                        PlanningCycleData newCycleData = new PlanningCycleData();
                        newCycleData.WorkplaceID = WorkplaceID;
                        newCycleData.FaceLength = newFL;
                        PlanningClass.PlanningCycle.planningCycleData.Add(newCycleData);
                        PlanningClass.AddCyclePlan(WorkplaceID,
                                                   SectionID,
                                                   PlanningClass.PlanningSettings.MOSectionID,
                                                   PlanningClass.PlanningSettings.ProdMonth,
                                                   PlanningClass.PlanningSettings.ActivityID,
                                                  newFL);

                        var wpCycleDataNew = PlanningClass.PlanningCycle.planningCycleData
                           .Where(a => a.WorkplaceID == WorkplaceID)
                           .FirstOrDefault();
                        double newCall = wpCycleDataNew.DailyCycleCall * wpCycleDataNew.TotalShifts;
                        int MonthlyTotalSQM = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle,
                                                                                                  viewPlanningStoping.Columns["MonthlyTotalSQM"]));
                        wpCycleDataNew.CycleCall = newCall;
                        wpCycleDataNew.MonthCall = MonthlyTotalSQM;
                        wpCycleDataNew.UpdateCycleData();
                        viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"], wpCycleDataNew.CycleCall.ToString());
                        ucCycleScreen1.CycleData = wpCycleDataNew;
                        ucCycleScreen1.SetData();
                        ucCycleScreen1.RefreshGrid();
                    }
                    
                }

                if (e.Column.FieldName == "callValue")
                {
                    decimal TotMonthcall = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyTotalSQM"]));
                    decimal ReefMonthcall = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["MonthlyReefSQM"]));

                    decimal ratio = 0;

                    if (ReefMonthcall != 0)
                    {
                        ratio = TotMonthcall / ReefMonthcall;
                    }

                    int TotActCall = Convert.ToInt32(viewPlanningStoping.GetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["callValue"]));
                    int ReefCall = 0;
                    int WasteCall = 0;

                    if (ratio != 0)
                    {
                        ReefCall = Convert.ToInt32(Convert.ToDecimal(TotActCall) / ratio);
                    }

                    if (TotMonthcall != 0)
                    {
                        WasteCall = TotActCall - ReefCall;
                    }
                    else
                    {
                        ReefCall = TotActCall;
                    }



                    viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["ReefSQM"], ReefCall);
                    viewPlanningStoping.SetRowCellValue(e.RowHandle, viewPlanningStoping.Columns["WasteSQM"], WasteCall);

                    //viewPlanningDevelopment.SetRowCellValue(e.RowHandle, viewPlanningDevelopment.Columns["Metresadvance"], e.Value.ToString());

                }


                MWDataManager.clsDataAccess _CalcData = new MWDataManager.clsDataAccess() { ConnectionString = TConnections.GetConnectionString(PlanningClass.systemDBTag, PlanningClass.UserCurrentInfo.Connection), queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement, queryReturnType = MWDataManager.ReturnType.DataTable };

                BandedGridView view;
                view = sender as BandedGridView;
                object row = view.GetRow(view.FocusedRowHandle); // get the current row 


                (row as DataRowView)["hasChanged"] = 1;

                if (view.FocusedColumn.FieldName == "SectionID")
                {
                    // updates start and en date based on selected miner. Will update if no date or date is different from current
                    string selectedSectioID = (row as DataRowView)["SectionID"].ToString();
                    DataRow[] result = PlanningClass.tblSectionStartEndDates.Select(String.Format("SectionID = '{0}'", selectedSectioID));
                    if (result.Length == 1)
                    {
                        if ((row as DataRowView)["StartDate"] == null || (row as DataRowView)["StartDate"] != result[0]["StartDate"].ToString())
                        {
                            (row as DataRowView)["StartDate"] = result[0]["StartDate"].ToString();
                        }
                        if ((row as DataRowView)["EndDate"] == null || (row as DataRowView)["EndDate"] != result[0]["EndDate"].ToString())
                        {
                            (row as DataRowView)["EndDate"] = result[0]["EndDate"].ToString();
                        }
                    }

                }

                if (PlanningClass.PlanningSettings.ActivityID == 0)
                {

                    if (view.FocusedColumn.FieldName == "ReefSQM" || view.FocusedColumn.FieldName == "WasteSQM" || view.FocusedColumn.FieldName == "FL" ||
                        view.FocusedColumn.FieldName == "SW" || view.FocusedColumn.FieldName == "CW" || view.FocusedColumn.FieldName == "CMGT" ||
                        view.FocusedColumn.FieldName == "FaceCMGT" || view.FocusedColumn.FieldName == "CubicsReef" || view.FocusedColumn.FieldName == "CubicsWaste")
                    {
                        if ((row as DataRowView)["FaceCMGT"] == null || (row as DataRowView)["FaceCMGT"].ToString() == "")
                        {
                            (row as DataRowView)["FaceCMGT"] = 0;
                        }

                        if ((row as DataRowView)["CW"] == null || (row as DataRowView)["CW"].ToString() == "")
                        {
                            (row as DataRowView)["CW"] = 0;
                        }

                        if ((row as DataRowView)["CubicsReef"] == null || (row as DataRowView)["CubicsReef"].ToString() == "")
                        {
                            (row as DataRowView)["CubicsReef"] = 0;
                        }

                        if ((row as DataRowView)["CubicsWaste"] == null || (row as DataRowView)["CubicsWaste"].ToString() == "")
                        {
                            (row as DataRowView)["CubicsWaste"] = 0;
                        }

                        if ((row as DataRowView)["DynamicCubicGT"] == null || (row as DataRowView)["DynamicCubicGT"].ToString() == "")
                        {
                            (row as DataRowView)["DynamicCubicGT"] = 0;
                        }

                        string cubicgrams = (row as DataRowView)["DynamicCubicGT"].ToString();


                        _CalcData.SqlStatement = "DECLARE @StopeWidth float,@CubicMeters float,@FaceLength float,@OnReefSQM float, @OffReefSQM float, " +
                                                  "@CMGT float,@CMKGT float,@Workplaceid varchar(50),@FaceCMGT float,@FaceAdvance float,@GoldBrokenSQM float,@UraniumBrokenSQM float, " +
                                                  "@GoldBrokenCUB float,@UraniumBrokenCUB float, @FaceBrokenKG float, @FaceTonsSQM float, @FaceTonsCube float, @FaceValue float, " +
                                                  "@TrammedTons float, @TrammedValue float,@ChannelW int,@cubicgrams float  " +
                                                  " SET @StopeWidth = " + (row as DataRowView)["SW"].ToString() +
                                                  " SET @CubicMeters = " + (row as DataRowView)["CubicMetres"] +
                                                  " SET @FaceLength = " + (row as DataRowView)["FL"] +
                                                  " SET @OnReefSQM = " + (row as DataRowView)["ReefSQM"] +
                                                  " SET @OffReefSQM = " + (row as DataRowView)["WasteSQM"] +
                                                  " SET @CMGT = " + (row as DataRowView)["CMGT"] +
                                                   " SET @CMKGT = " + (row as DataRowView)["CMKGT"] +
                                                  " SET @Workplaceid = '" + (row as DataRowView)["Workplaceid"] + "' " +
                                                  " SET @FaceCMGT  = " + (row as DataRowView)["FaceCMGT"] +
                                                  " SET @ChannelW  = " + (row as DataRowView)["CW"] +

                                                  "SET @cubicgrams=" + (row as DataRowView)["DynamicCubicGT"] + "" +


                                                  " SET @FaceAdvance = dbo.CalcFaceAdvance(@StopeWidth,@CubicMeters,@FaceLength,@OnReefSQM,@OffReefSQM) " +
                                                  " set @GoldBrokenSQM = dbo.CalcGoldBrokenSQM(@OnReefSQM,@CMGT,@Workplaceid) " +
                                                    " set @GoldBrokenCUB = dbo.CalcGoldBrokenCUB(@CubicMeters,@cubicgrams,@StopeWidth,@Workplaceid) " +
                                                  "  set @UraniumBrokenSQM = dbo.CalcGoldBrokenSQM(@OnReefSQM,@CMKGT,@Workplaceid)  " +
                                                    "set @UraniumBrokenCUB=dbo.CalcUraniumBrokenCUB(@CubicMeters,@cubicgrams,@StopeWidth,@Workplaceid) " +
                                                  " set @FaceBrokenKG = dbo.CalcFaceBrokenKG(@OnReefSQM,@FaceCMGT,@Workplaceid)  " +
                                                  " set @FaceTonsSQM = dbo.CalcFaceTonsSQM(@OnReefSQM + @OffReefSQM,@StopeWidth,@Workplaceid)" +
                                                  " SET @FaceTonsCube = dbo.CalcFaceTonsCUBE(@CubicMeters,@Workplaceid) " +
                                                  " SET @FaceValue = dbo.CalcFaceValue(@GoldBrokenSQM + @GoldBrokenCUB,@FaceTonsSQM + @FaceTonsCube) " +
                                                  " SET @TrammedTons = dbo.CalcTrammedTons(@FaceTonsSQM + @FaceTonsCube) " +
                                                  " SET @TrammedValue = dbo.CalcTrammedValue(@TrammedTons,@GoldBrokenSQM + @GoldBrokenCUB) " +
                                                  " SELECT   @FaceAdvance FaceAdvance,@GoldBrokenSQM  GoldBrokenSQM,@UraniumBrokenSQM UraniumBrokenSQM, @GoldBrokenCUB GoldBrokenCUB,@UraniumBrokenCUB UraniumBrokenCUB,@FaceBrokenKG FaceBrokenKG, " +
                                                  "         @FaceTonsSQM FaceTonsSQM,@FaceTonsCube FaceTonsCubes,@FaceValue FaceValue,@TrammedTons TrammedTons, @TrammedValue TrammedValue,dbo.CalcIdealSW(@ChannelW) IdealSW ";
                        _CalcData.ExecuteInstruction();



                        foreach (DataRow r in _CalcData.ResultsDataTable.Rows)
                        {
                            (row as DataRowView)["FaceAdvance"] = r["FaceAdvance"];
                            (row as DataRowView)["GoldBroken"] = Convert.ToDecimal(r["GoldBrokenSQM"]) + (Convert.ToDecimal(r["GoldBrokenCUB"]) / 1000);
                            (row as DataRowView)["UraniumBroken"] = Convert.ToDecimal(r["UraniumBrokenSQM"]) + (Convert.ToDecimal(r["UraniumBrokenCUB"]) / 100);
                            (row as DataRowView)["FaceBrokenKG"] = r["FaceBrokenKG"];
                            (row as DataRowView)["FaceTons"] = Convert.ToDecimal(r["FaceTonsSQM"]) + Convert.ToDecimal(r["FaceTonsCubes"]);
                            (row as DataRowView)["FaceValue"] = r["FaceValue"];
                            (row as DataRowView)["TrammedTons"] = r["TrammedTons"];
                            (row as DataRowView)["TrammedValue"] = r["TrammedValue"];
                            (row as DataRowView)["IdealSW"] = r["IdealSW"];
                            (row as DataRowView)["CubicMetres"] = Convert.ToDecimal((row as DataRowView)["CubicsReef"]) + Convert.ToDecimal((row as DataRowView)["CubicsWaste"]);


                        }

                        string theSQM = String.Format("{0:0}", (row as DataRowView)["ReefSQM"]);
                        string theWasteSQM = String.Format("{0:0}", (row as DataRowView)["WasteSQM"]);
                        Int32 theTotal = Convert.ToInt32(theSQM) + Convert.ToInt32(theWasteSQM);

                        //     (row as DataRowView)["callValue"] = theTotal;
                        //editTotalPlanSQM.Text = Convert.ToString(getTotalSQM(tblPrePlanData));
                        viewPlanningStoping.UpdateTotalSummary();
                        calVariance();


                    }



                }

                viewPlanningStoping.RefreshData();
            }
            catch (Exception eException)
            {

                var error = eException.Message;
            }
        }

        public void ShowCycle(bool canShow)
        {
            ucCycleScreen1.Visible = canShow;
        }

        private void viewPlanningStoping_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {


                int currentRow = viewPlanningStoping.GetDataSourceRowIndex(e.FocusedRowHandle);
                string sectionID = PlanningClass.tblPlanningData.Rows[currentRow]["SectionID"].ToString();
                if (sectionID != "-1")
                {
                    string workplaceID = PlanningClass.tblPlanningData.Rows[currentRow]["Workplaceid"].ToString();
                    var wpCycleData = PlanningClass.PlanningCycle.planningCycleData
                        .Where(a => a.WorkplaceID == workplaceID)
                        .FirstOrDefault();

                    if (wpCycleData != null) // no cycle data need to add from DB 
                    {
                        //ucCycleScreen1.Visible = true;
                        ucCycleScreen1.CycleData = wpCycleData;
                        ucCycleScreen1.SetData();
                    }
                    else
                    {
                        PlanningClass.AddCyclePlan(workplaceID,
                               sectionID,
                               PlanningClass.PlanningSettings.MOSectionID,
                               PlanningClass.PlanningSettings.ProdMonth,
                               PlanningClass.PlanningSettings.ActivityID, -1);

                        var wpCycleDataNew = PlanningClass.PlanningCycle.planningCycleData
                                             .Where(a => a.WorkplaceID == workplaceID)
                                             .FirstOrDefault();

                        //ucCycleScreen1.Visible = true;
                        ucCycleScreen1.CycleData = wpCycleDataNew;
                        ucCycleScreen1.SetData();
                    }
                }
                else
                {
                    //ucCycleScreen1.Visible = false;
                }
            }
            catch (Exception)
            {

                //ucCycleScreen1.Visible = false;
            }
        }
    }
}
