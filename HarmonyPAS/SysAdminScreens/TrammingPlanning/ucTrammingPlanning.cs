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
using DevExpress.XtraEditors.Controls;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.TrammingPlanning
{
    public partial class ucTrammingPlanning : Mineware.Systems.Global.ucBaseUserControl
    {
        public ucTrammingPlanning()
        {
            InitializeComponent();
        }

        public DataTable dtXC;
        public DataTable dtXCBoxHole;
        public DataTable dtTrammingPlanningData;
        public DataTable dtHoppers;
        string prodMonth = "";
        private DataView clone = null;

        clsTrammingPlanningData _clsTrammingPlanningData = new clsTrammingPlanningData();

        void LoadPlanningScreenData()
        {
            rpgSelection.Enabled = true;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;

            _clsTrammingPlanningData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtProdMonth = _clsTrammingPlanningData.GetProdMonth();

            foreach (DataRow r in dtProdMonth.Rows)
            {
                prodMonth = r["Prodmonth"].ToString();
                luePlanningProdMonth.EditValue = TProductionGlobal.ProdMonthAsDate(prodMonth.ToString()); ;
            }

            DataTable dtSections = _clsTrammingPlanningData.GetSections(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(luePlanningProdMonth.EditValue.ToString())));
            rPlanningSections.DataSource = dtSections;
            rPlanningSections.ValueMember = "SectionID";
            rPlanningSections.DisplayMember = "Name";

            gvTrammingPlanning.PostEditor();

            LookUpColumnInfoCollection collXC = LookupXC.Columns;
            collXC.Add(new LookUpColumnInfo("XC", 0));

            LookUpColumnInfoCollection coll = LookupBoxHole.Columns;
            coll.Add(new LookUpColumnInfo("BH", 0));
        }

        private void ucPlanningSetup_Load(object sender, EventArgs e)
        {
            LoadPlanningScreenData();
        }


        private void LookupBoxHole_EditValueChanged(object sender, EventArgs e)
        {
            var edit = sender as LookUpEdit;
            DataRow dr = gvTrammingPlanning.GetDataRow(gvTrammingPlanning.FocusedRowHandle);

            string setBoxHoleID = _clsTrammingPlanningData.GetBoxHoleID(dtTrammingPlanningData ,edit.Text);
            dr["BoxHoleID"] = setBoxHoleID;
            gvTrammingPlanning.PostEditor();
            gvTrammingPlanning.RefreshData();
        }

        private void gvTrammingPlanning_ShownEditor(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (view.FocusedColumn == gColBoxHole && view.ActiveEditor is DevExpress.XtraEditors.LookUpEdit)
            {
                DevExpress.XtraEditors.LookUpEdit edit;

                edit = (DevExpress.XtraEditors.LookUpEdit)view.ActiveEditor;

                DataTable table = edit.Properties.DataSource as DataTable;

                clone = new DataView(table);

                DataRow row = view.GetDataRow(view.FocusedRowHandle);

                edit.Properties.DataSource = clone;

            }
        }

        private void gvTrammingPlanning_HiddenEditor(object sender, EventArgs e)
        {
            if (clone != null)
            {
                clone.Dispose();
                clone = null;
            }
        }

        private void gvTrammingPlanning_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "BH")
                e.RepositoryItem = LookupBoxHole;

            if (e.Column.FieldName == "XC")
                e.RepositoryItem = LookupXC;
        }

        private void luePlanningProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            rPlanningSections.DataSource = _clsTrammingPlanningData.GetSections(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(luePlanningProdMonth.EditValue.ToString())));
            rPlanningSections.DisplayMember = "Name";
            rPlanningSections.ValueMember = "SectionID";
        }

        private void btnBookingShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnBookingSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _clsTrammingPlanningData.SaveTrammingPlanning(TProductionGlobal.ProdMonthAsString(
                Convert.ToDateTime(luePlanningProdMonth.EditValue.ToString())), dtTrammingPlanningData);

            dtTrammingPlanningData = _clsTrammingPlanningData.GetTrammingPlanningData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(luePlanningProdMonth.EditValue.ToString())), cbPlanningSections.EditValue.ToString());
            gcTrammingPlanning.DataSource = dtTrammingPlanningData;

            rpgSelection.Enabled = false;
            rpgShow.Enabled = false;
            rpgSave.Enabled = true;
        }

        private void btnBookingBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rpgSelection.Enabled = true;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;
        }

        private void btnPlanningShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rpgSelection.Enabled = false;
            rpgShow.Enabled = false;
            rpgSave.Enabled = true;

            dtTrammingPlanningData = _clsTrammingPlanningData.GetTrammingPlanningData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(luePlanningProdMonth.EditValue.ToString())), cbPlanningSections.EditValue.ToString());
            gcTrammingPlanning.DataSource = dtTrammingPlanningData;

            dtXCBoxHole = _clsTrammingPlanningData.GetBoxHole(dtTrammingPlanningData);
            LookupBoxHole.DataSource = dtXCBoxHole;
            LookupBoxHole.DisplayMember = "BH";
            LookupBoxHole.ValueMember = "BH";
        }
    }
}
