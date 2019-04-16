using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using System.Collections;
using MWDataManager;
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.LockMonth
{
    public partial class ucLockMonth : Mineware.Systems.Global.ucBaseUserControl
    {
        clsLockMonthData _clsLockMonthData = new clsLockMonthData();

        string ProdMonth = "";

        public ucLockMonth()
        {
            InitializeComponent();
        }

        void LoadScreenData()
        {
            _clsLockMonthData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtProdMonth = _clsLockMonthData.GetProdMonth();

            foreach (DataRow r in dtProdMonth.Rows)
            {
                ProdMonth = r["Prodmonth"].ToString();
                luePlanningProdMonth.EditValue = TProductionGlobal.ProdMonthAsDate(ProdMonth.ToString());

                //lueSurveyProdMonth.EditValue = TProductionGlobal.ProdMonthAsDate(ProdMonth.ToString());
            }
        }

        void LoadBusinessPlanData()
        {
            DataTable dtBusinessPlanData = _clsLockMonthData.GetBusinessPlanData();
            gcBusinessPlanLock.DataSource = dtBusinessPlanData;
        }

        void LoadPlanningLockData()
        {
            DataTable dtPlanningLockData = _clsLockMonthData.GetPlanningLockData(
                TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(luePlanningProdMonth.EditValue.ToString())));
            gcPlanningLock.DataSource = dtPlanningLockData;
        }

        void LoadSurveyLockData()
        {
            DataTable dtSurveyLockData = _clsLockMonthData.GetSurveyLockData();
            gcSurveyLock.DataSource = dtSurveyLockData;
        }

        private void ucLockMonth_Load(object sender, EventArgs e)
        {
            LoadScreenData();

            LoadBusinessPlanData();

            LoadPlanningLockData();

            LoadSurveyLockData();
        }

        private void luePlanningProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            LoadPlanningLockData();
        }

        private void gcBusinessPlanLock_Click(object sender, EventArgs e)
        {

        }

        private void tcLockMonth_Click(object sender, EventArgs e)
        {

        }

        private void gcSurveyLock_Click(object sender, EventArgs e)
        {

        }
        string _Prodmonth = "";
        private void gvSurveyLock_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
           
            
        }

        private void LockBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(_Prodmonth == "")
            {
                System.Windows.Forms.MessageBox.Show("Please select a Month");
            }
            else
            {
                _clsLockMonthData.LockSurveyData(_Prodmonth);
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Month Locked", Color.CornflowerBlue);
                LoadSurveyLockData();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_Prodmonth == "")
            {
                System.Windows.Forms.MessageBox.Show("Please select a Month");
            }
            else
            {
                _clsLockMonthData.UnlockLockSurveyData(_Prodmonth);
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Month Unlocked", Color.CornflowerBlue);
                LoadSurveyLockData();
            }
        }

        private void gvSurveyLock_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            _Prodmonth = "";
            if (gvSurveyLock.GetRowCellValue(gvSurveyLock.FocusedRowHandle, gvSurveyLock.Columns["ProdMonth"]) != null)
            {
                var Prodmonth = gvSurveyLock.GetRowCellValue(gvSurveyLock.FocusedRowHandle, gvSurveyLock.Columns["ProdMonth"]);
                _Prodmonth = Prodmonth.ToString();
            }
        }
    }
}
