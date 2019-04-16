using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System.Data.SqlClient;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.HoistingBooking
{
    public partial class ucHoistingBooking : ucBaseUserControl
    {
        clsHoistingBookingData _clsHoistingBookingData = new clsHoistingBookingData();
        DataTable dtMillingCapture;
        string MillMonth = "";
        int _tonsTreated = 0;
        int _tonsToPlant = 0;

        public ucHoistingBooking()
        {
            InitializeComponent();
        }

        private void ucMillingBooking_Load(object sender, EventArgs e)
        {
            LoadScreenData();
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadHoistingData();
            rpgSelection.Enabled = false;
            rpgInputs.Enabled = true;
            rpgShow.Enabled = false;
            rpgSave.Enabled = true;
        }

        void LoadScreenData()
        {
            gcHoistingBooking.Visible = false;

            rpgSelection.Enabled = true;
            rpgInputs.Enabled = false;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;

            _clsHoistingBookingData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtMillMonth = _clsHoistingBookingData.GetMillMonth();

            foreach (DataRow r in dtMillMonth.Rows)
            {
                MillMonth = r["MillMonth"].ToString();
                lueMillMonth.EditValue = TProductionGlobal.ProdMonthAsDate(MillMonth.ToString()); ;
            }

            DataTable dtShaft = _clsHoistingBookingData.GetShaft();
            rpItemShaft.DataSource = dtShaft;
            rpItemShaft.ValueMember = "OreflowID";
            rpItemShaft.DisplayMember = "Name";
        }

        void LoadHoistingData()
        {
            gcHoistingBooking.Visible = true;

            try
            {
                _clsHoistingBookingData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                DataTable dtHoistPlan = _clsHoistingBookingData.GetHoistingPlanningData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())), lueShaft.EditValue.ToString());

                if (dtHoistPlan.Rows.Count == 0)
                {
                    txtPlanHoistTons.EditValue = 0;
                    txtPlanBeltGrade.EditValue = 0.00;
                    txtPlanGold.EditValue = 0.00;
                }

                else if (dtHoistPlan.Rows.Count != 0)
                {
                    txtPlanHoistTons.EditValue = Convert.ToInt32(dtHoistPlan.Rows[0]["PlanTons"]);
                    txtPlanBeltGrade.EditValue = Convert.ToDecimal(dtHoistPlan.Rows[0]["PlanBeltGrade"]);
                    txtPlanGold.EditValue = Convert.ToDecimal(dtHoistPlan.Rows[0]["PlanGold"]);
                }

                DataTable dtHoistingBooking = _clsHoistingBookingData.GetHoistingBookingData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())), lueShaft.EditValue.ToString());

                if (dtHoistingBooking.Rows.Count != 0)
                {
                    gcHoistingBooking.DataSource = dtHoistingBooking;
                }
                else
                {
                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Cannot Load Grid", Color.Red);
                }
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red); ;
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _clsHoistingBookingData.SaveData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())),
                                            lueShaft.EditValue.ToString(),
                                            Convert.ToInt32(txtPlanHoistTons.EditValue),
                                            Convert.ToDecimal(txtPlanBeltGrade.EditValue), 
                                            Convert.ToInt32(txtPlanGold.EditValue));

            LoadHoistingData();
        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rpgSelection.Enabled = true;
            rpgInputs.Enabled = false;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;
            txtPlanHoistTons.EditValue = 0;
            txtPlanBeltGrade.EditValue = 0;
            txtPlanGold.EditValue = 0;
            lueShaft.EditValue = "";

            LoadScreenData();

        }

        private void txtPlanBeltGrade_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePlanGold();
        }

        private void txtPlanHoistTons_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePlanGold();
        }

        void CalculatePlanGold()
        {
            //if (txtPlanHoistTons.EditValue.ToString() != ""  && txtPlanBeltGrade.EditValue.ToString() != "")
            //{
                int _planTons = Convert.ToInt32(txtPlanHoistTons.EditValue);
                decimal _planBeltGrade = Convert.ToDecimal(txtPlanBeltGrade.EditValue);
                decimal _planGold = Convert.ToDecimal((_planTons * _planBeltGrade) / 1000);

                txtPlanGold.EditValue = _planGold;
            //}

            //else
               // Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Plan Hoist Tons and Plan Belt Grade cannot be empty", Color.Red);

        }

        private void gvHoistingBooking_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var calendarDate = gvHoistingBooking.GetRowCellValue(gvHoistingBooking.FocusedRowHandle, gvHoistingBooking.Columns["CalendarDate"]).ToString();

            var reefTons = gvHoistingBooking.GetRowCellValue(gvHoistingBooking.FocusedRowHandle, gvHoistingBooking.Columns["ReefTons"]).ToString();

            var wasteTons = gvHoistingBooking.GetRowCellValue(gvHoistingBooking.FocusedRowHandle, gvHoistingBooking.Columns["WasteTons"]).ToString();

            var beltGrade = gvHoistingBooking.GetRowCellValue(gvHoistingBooking.FocusedRowHandle, gvHoistingBooking.Columns["BeltGrade"]).ToString();

            _clsHoistingBookingData.SaveActualData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())),
                                        lueShaft.EditValue.ToString(), Convert.ToDateTime(calendarDate),
                                        Convert.ToInt32(reefTons),
                                        Convert.ToDecimal(wasteTons),
                                        Convert.ToDecimal(beltGrade));

            LoadHoistingData();
        }
    }
}
