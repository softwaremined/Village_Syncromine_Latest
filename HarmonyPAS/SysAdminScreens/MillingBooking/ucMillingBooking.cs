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

namespace Mineware.Systems.Production.SysAdminScreens.MillingBooking
{
    public partial class ucMillingBooking : ucBaseUserControl
    {
        clsMillingBookingData _clsMillingBookingData = new clsMillingBookingData();
        DataTable dtMillingCapture;
        string MillMonth = "";
        int _tonsTreated = 0;
        int _tonsToPlant = 0;

        public ucMillingBooking()
        {
            InitializeComponent();
        }

        private void ucMillingBooking_Load(object sender, EventArgs e)
        {
            LoadScreenData();
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadMillingData();
        }

        void LoadScreenData()
        {

            gcMillingBooking.Visible = false;
            rpgSelection.Enabled = true;
            rpgInputs.Enabled = false;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;

            _clsMillingBookingData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtMillMonth = _clsMillingBookingData.GetMillMonth();

            foreach (DataRow r in dtMillMonth.Rows)
            {
                MillMonth = r["MillMonth"].ToString();
                lueMillMonth.EditValue = TProductionGlobal.ProdMonthAsDate(MillMonth.ToString()); ;
            }

            DataTable dtMill = _clsMillingBookingData.GetMill();
            rpItemMill.DataSource = dtMill;
            rpItemMill.ValueMember = "OreflowID";
            rpItemMill.DisplayMember = "Name";
        }

        void LoadMillingData()
        {
            gcMillingBooking.Visible = true;
            rpgSelection.Enabled = false;
            rpgInputs.Enabled = true;
            rpgShow.Enabled = false;
            rpgSave.Enabled = true;

            try
            {
                DataTable dtPlanMillingData = _clsMillingBookingData.GetMillingPlanningData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())), lueMill.EditValue.ToString());

                if (dtPlanMillingData.Rows.Count == 0)
                {
                    txtTonsTreated.EditValue = "0";
                    txtTonsToPlant.EditValue = "0";
                }

                else if (dtPlanMillingData.Rows.Count != 0)
                {
                    _tonsTreated = Convert.ToInt32(dtPlanMillingData.Rows[0]["TonsTreated"]);
                    _tonsToPlant = Convert.ToInt32(dtPlanMillingData.Rows[0]["TonsToPlant"]);

                    txtTonsTreated.EditValue = _tonsTreated.ToString();
                    txtTonsToPlant.EditValue = _tonsToPlant.ToString();
                }

                _clsMillingBookingData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtMillingCapture = _clsMillingBookingData.GetMillingData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())), lueMill.EditValue.ToString());

                if (dtMillingCapture.Rows.Count != 0)
                {
                    gcMillingBooking.DataSource = dtMillingCapture;
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


            _clsMillingBookingData.SavePlannedData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())),
                                                    lueMill.EditValue.ToString(),
                                                    Convert.ToInt32(txtTonsTreated.EditValue),
                                                    Convert.ToInt32(txtTonsToPlant.EditValue));

            LoadMillingData();
        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadScreenData();

            txtTonsTreated.EditValue = "";
            txtTonsToPlant.EditValue = "";
        }

        private void txtTonsTreated_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtTonsToPlant_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gvMillingBooking_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var calendarDate = gvMillingBooking.GetRowCellValue(gvMillingBooking.FocusedRowHandle, gvMillingBooking.Columns["CalendarDate"]).ToString();

            var treated = gvMillingBooking.GetRowCellValue(gvMillingBooking.FocusedRowHandle, gvMillingBooking.Columns["Treated"]).ToString();

            var toPlant = gvMillingBooking.GetRowCellValue(gvMillingBooking.FocusedRowHandle, gvMillingBooking.Columns["ToPlant"]).ToString();

            _clsMillingBookingData.SaveActualData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())),
                                        lueMill.EditValue.ToString(), Convert.ToDateTime(calendarDate),
                                        Convert.ToDecimal(treated),
                                        Convert.ToDecimal(toPlant));

            LoadMillingData();
        }
    }
}
