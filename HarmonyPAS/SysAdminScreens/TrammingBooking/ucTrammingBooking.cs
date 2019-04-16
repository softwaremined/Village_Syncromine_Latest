using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;



namespace Mineware.Systems.Production.SysAdminScreens.TrammingBooking
{
    public partial class ucTrammingBooking : ucBaseUserControl
    {
        public ucTrammingBooking()
        {
            InitializeComponent();
        }

        public DataTable dtTrammingBookingData;
        string prodMonth = "";
        decimal theTotal;
        decimal theNight;
        decimal theMorning;
        decimal theAfternoon;

        clsTrammingBookingData _clsTrammingBookingData = new clsTrammingBookingData();

        void LoadBookingScreenData()
        {
            rpgSelection.Enabled = true;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;
            gcBookings.Visible = false;

            _clsTrammingBookingData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtProdMonth = _clsTrammingBookingData.GetProdMonth();

            foreach (DataRow r in dtProdMonth.Rows)
            {
                prodMonth = r["Prodmonth"].ToString();
                lueBookingProdMonth.EditValue = TProductionGlobal.ProdMonthAsDate(prodMonth.ToString()); ;
            }

            DataTable dtSections = _clsTrammingBookingData.GetSections(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueBookingProdMonth.EditValue.ToString())));
            rpBookingSections.DataSource = dtSections;
            rpBookingSections.ValueMember = "SectionID";
            rpBookingSections.DisplayMember = "Name";
        }

        private void ucBooking_Load(object sender, EventArgs e)
        {
            LoadBookingScreenData();
        }

        private void LoadEnabled()
        {
            btnBookingSave.Enabled = false;
            btnBookingBack.Enabled = false;
            lueBookingProdMonth.Enabled = true;
            cbBookingSections.Enabled = true;
            btnBookingShow.Enabled = true;
            dteDate.Enabled = true;

        }

        private void ShowEnabled()
        {
            btnBookingSave.Enabled = true;
            btnBookingBack.Enabled = true;
            lueBookingProdMonth.Enabled = false;
            cbBookingSections.Enabled = false;
            btnBookingShow.Enabled = false;
            dteDate.Enabled = false;
        }

        private void TextEditTons_EditValueChanged(object sender, EventArgs e)
        {
            bgvBookings.PostEditor();
        }

        private void TextEditUpdateTotal_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //String theWorplaceID = bgvBookings.GetRowCellValue(bgvBookings.FocusedRowHandle, bgcWorkplace).ToString();

            //DataRow[] TotalRow = dtTrammingBookingData.Select("Units = 'Hoppers' and Workplaceid = '" + theWorplaceID + "'");

            //foreach (DataRow dr in TotalRow)
            //{
            //    theNight = Convert.ToDecimal(dr["Night"].ToString());
            //    theMorning = Convert.ToDecimal(dr["Morning"].ToString());
            //    theAfternoon = Convert.ToDecimal(dr["Afternoon"].ToString());

            //    theTotal = theNight + theMorning + theAfternoon;
            //}
            
            //bgvBookings.SetRowCellValue(bgvBookings.FocusedRowHandle, bgvBookings.Columns["Total"], theTotal);
        }

        private void btnBookingShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rpgSelection.Enabled = false;
            rpgShow.Enabled = false;
            rpgSave.Enabled = true;
            gcBookings.Visible = true;

            dtTrammingBookingData = _clsTrammingBookingData.GetTrammingBookingData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueBookingProdMonth.EditValue.ToString())), cbBookingSections.EditValue.ToString(), Convert.ToDateTime(dteDate.EditValue.ToString()));
            gcBookings.DataSource = dtTrammingBookingData;

            gbDate.Caption = dteDate.EditValue.ToString();
        }

        private void btnBookingSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _clsTrammingBookingData.SaveTrammingBooking(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueBookingProdMonth.EditValue.ToString())), cbBookingSections.EditValue.ToString(), Convert.ToDateTime(dteDate.EditValue.ToString()), dtTrammingBookingData);

            dtTrammingBookingData = _clsTrammingBookingData.GetTrammingBookingData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueBookingProdMonth.EditValue.ToString())), cbBookingSections.EditValue.ToString(), Convert.ToDateTime(dteDate.EditValue.ToString()));
            gcBookings.DataSource = dtTrammingBookingData;

            rpgSelection.Enabled = false;
            rpgShow.Enabled = false;
            rpgSave.Enabled = true;
        }

        private void btnBookingBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rpgSelection.Enabled = true;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;
            gcBookings.Visible = false;
        }

        private void lueBookingProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            rpBookingSections.DataSource = _clsTrammingBookingData.GetSections(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueBookingProdMonth.EditValue.ToString())));
            rpBookingSections.ValueMember = "SectionID";
            rpBookingSections.DisplayMember = "Name";
        }

        private void TextEditUpdateTotal_EditValueChanged(object sender, EventArgs e)
        {
            //String theWorplaceID = bgvBookings.GetRowCellValue(bgvBookings.FocusedRowHandle, bgcWorkplace).ToString();

            //DataRow[] TotalRow = dtTrammingBookingData.Select("Units = 'Hoppers' and Workplaceid = '" + theWorplaceID + "'");

            //foreach (DataRow dr in TotalRow)
            //{
            //    theNight = Convert.ToDecimal(dr["Night"].ToString());
            //    theMorning = Convert.ToDecimal(dr["Morning"].ToString());
            //    theAfternoon = Convert.ToDecimal(dr["Afternoon"].ToString());

            //    theTotal = theNight + theMorning + theAfternoon;
            //}

            //bgvBookings.SetRowCellValue(bgvBookings.FocusedRowHandle, bgvBookings.Columns["Total"], theTotal);
        }

        private void TextEditUpdateTotal_Leave(object sender, EventArgs e)
        {

        }

        private void bgvBookings_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }
    }
}
