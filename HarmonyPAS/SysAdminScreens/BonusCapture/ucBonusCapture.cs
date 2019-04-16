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

namespace Mineware.Systems.Production.SysAdminScreens.BonusCapture
{
    public partial class ucBonusCapture : ucBaseUserControl
    {
        clsBonusCaptureData _clsBonusCaptureData = new clsBonusCaptureData();
        DataTable dtDevelopmentData = new DataTable();
        DataTable dtStopingData = new DataTable();
        string ProdMonth = "";

        public ucBonusCapture()
        {
            InitializeComponent();
        }

        private void ucMillingBooking_Load(object sender, EventArgs e)
        {
            LoadScreenData();
        }

        void LoadStopingData()
        {
            gcStoping.Visible = true;
            gcDevelopment.Visible = false;
            rpgSelection.Enabled = false;
            rpgShow.Enabled = false;
            rpgSave.Enabled = true;

            try
            {
                dtStopingData = _clsBonusCaptureData.GetStopingData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueProdMonth.EditValue.ToString())), cmbActivity.EditValue.ToString(), cmbSection.EditValue.ToString());
                gcStoping.DataSource = dtStopingData;

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red); ;
            }
        }

        void LoadDevelopmentData()
        {
            gcStoping.Visible = false;
            gcDevelopment.Visible = true;
            rpgSelection.Enabled = false;
            rpgShow.Enabled = false;
            rpgSave.Enabled = true;

            try
            {
                gcStoping.Visible = false;
                gcDevelopment.Visible = true;

                dtDevelopmentData = _clsBonusCaptureData.GetDevelopmentData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueProdMonth.EditValue.ToString())), cmbActivity.EditValue.ToString(), cmbSection.EditValue.ToString());
                gcDevelopment.DataSource = dtDevelopmentData;
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red); ;
            }
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "select " +
                    "distinct(p.ProdMonth) ProdMonth, " +
                    "isnull(sl.Locked, 0) Locked, " +
                    "sl.DateLocked, " +
                    "sl.LockedByID " +
                    "from PLANNING p " +
                    "left outer join survey_locks sl on p.ProdMonth = sl.ProdMonth where sl.ProdMonth ='" + TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueProdMonth.EditValue.ToString())) + "'" +
                    "order by p.ProdMonth desc ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            foreach (DataRow dr in _dbMan.ResultsDataTable.Rows)
            {
                if (Convert.ToBoolean(dr["Locked"]) == true)
                {
                    btnSave.Enabled = false;
                }
                else if (Convert.ToBoolean(dr["Locked"]) == false)
                {
                    btnSave.Enabled = true;
                }
            }

            if (cmbActivity.EditValue.ToString() == "0")
            {
                LoadStopingData();
            }

            else if (cmbActivity.EditValue.ToString() == "1")
            {
                LoadDevelopmentData();
            }
        }

        void LoadScreenData()
        {
            gcStoping.Visible = false;
            gcDevelopment.Visible = false;
            rpgSelection.Enabled = true;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;

            _clsBonusCaptureData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtProdMonth = _clsBonusCaptureData.GetProdMonth();

            foreach (DataRow r in dtProdMonth.Rows)
            {
                ProdMonth = r["MillMonth"].ToString();
                lueProdMonth.EditValue = TProductionGlobal.ProdMonthAsDate(ProdMonth.ToString()); ;
            }

            DataTable dtActivity = _clsBonusCaptureData.GetActivity();
            rActivity.DataSource = dtActivity;
            rActivity.ValueMember = "Activity";
            rActivity.DisplayMember = "Description";

            DataTable dtSections = _clsBonusCaptureData.GetSections(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueProdMonth.EditValue.ToString())));
            rSection.DataSource = dtSections;
            rSection.ValueMember = "SectionID";
            rSection.DisplayMember = "Name";
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbActivity.EditValue.ToString() == "0")
            {
                _clsBonusCaptureData.SaveStopingData(dtStopingData, 
                                    TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueProdMonth.EditValue.ToString())), 
                                    cmbActivity.EditValue.ToString(), 
                                    cmbSection.EditValue.ToString());
                LoadStopingData();
            }

            else if (cmbActivity.EditValue.ToString() == "1")
            {
                _clsBonusCaptureData.SaveDevelopmentData(dtDevelopmentData, 
                                    TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueProdMonth.EditValue.ToString())), 
                                    cmbActivity.EditValue.ToString(), 
                                    cmbSection.EditValue.ToString());
                LoadDevelopmentData();
            }
        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadScreenData();
        }

        private void gcStoping_Click(object sender, EventArgs e)
        {

        }

        private void gvStoping_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            object row = view.GetRow(e.RowHandle);
            (row as DataRowView)["Status"] = "1";
            btnSave.Enabled = true;
        }

        private void gvDevelopment_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            object row = view.GetRow(e.RowHandle);
            (row as DataRowView)["Status"] = "1";
            btnSave.Enabled = true;
        }
    }
}
