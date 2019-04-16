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

namespace Mineware.Systems.Production.SysAdminScreens.TopPanels
{
    public partial class ucTopPanels : ucBaseUserControl
    {
        clsTopPanelsData _clsTopPanelsData = new clsTopPanelsData();
        DataTable dtTopPanels;
        string ProdMonth = "";

        public ucTopPanels()
        {
            InitializeComponent();
        }

        private void ucMillingBooking_Load(object sender, EventArgs e)
        {
            LoadScreenData();
        }

        void LoadTopPanels()
        {
            gcTopPanels.Visible = true;

            _clsTopPanelsData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtTopPanels = _clsTopPanelsData.GetTopPanelsData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueProdMonth.EditValue.ToString())), 
                lueSectionID.EditValue.ToString());

            gcTopPanels.DataSource = dtTopPanels;

        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadTopPanels();

            rpgSelection.Enabled = false;
            rpgShow.Enabled = false;
            rpgSave.Enabled = true;
        }

        void LoadScreenData()
        {
            gcTopPanels.Visible = false;
            rpgSelection.Enabled = true;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;

            _clsTopPanelsData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtProdMonth = _clsTopPanelsData.GetProdMonth();

            foreach (DataRow r in dtProdMonth.Rows)
            {
                ProdMonth = r["Prodmonth"].ToString();
                lueProdMonth.EditValue = TProductionGlobal.ProdMonthAsDate(ProdMonth.ToString());
            }

            DataTable dtSection = _clsTopPanelsData.GetSection(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueProdMonth.EditValue.ToString())));
            rpSectionID.DataSource = dtSection;
            rpSectionID.ValueMember = "SectionID";
            rpSectionID.DisplayMember = "Name";
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvTopPanels.RowUpdated += gvTopPanels_RowUpdated;
        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rpgSelection.Enabled = true;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;
            LoadScreenData();
        }

        private void gvTopPanels_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var workplaceID = gvTopPanels.GetRowCellValue(gvTopPanels.FocusedRowHandle, gvTopPanels.Columns["WPID"]).ToString();

            var activity = gvTopPanels.GetRowCellValue(gvTopPanels.FocusedRowHandle, gvTopPanels.Columns["Activity"]).ToString();

            var selected = gvTopPanels.GetRowCellValue(gvTopPanels.FocusedRowHandle, gvTopPanels.Columns["Selected"]).ToString();

            _clsTopPanelsData.SaveData(Convert.ToBoolean(selected),
                TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueProdMonth.EditValue.ToString())),
                lueSectionID.EditValue.ToString(), workplaceID.ToString(), activity.ToString());

            LoadTopPanels();
        }

        private void chkSelected_Leave(object sender, EventArgs e)
        {

        }
    }
}
