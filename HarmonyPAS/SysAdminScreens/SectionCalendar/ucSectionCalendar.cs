using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.ProductionGlobal;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Mineware.Systems.Production.SysAdminScreens.SectionCalendar
{
    public partial class ucSectionCalendar : Mineware.Systems.Global.ucBaseUserControl
    {
        string theEndDate;
        string theBeginDate;
        string SECT;
        string SUPERVISOR;
        string maxdate;
        DateTime maxd;
        bool rowclk = false;
        bool result = false;
        clsSectionCalendarData _clsSectionCalendarData = new clsSectionCalendarData();
        public ucSectionCalendar()
        {
            //View = view;
            InitializeComponent();
            //gvSectionCal.OptionsBehavior.AllowPixelScrolling = DefaultBoolean.True;
            //this.CanClose = true;
        }

        public void loadData(string prodmonth)
        {
            _clsSectionCalendarData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            gcSectionCal.DataSource = _clsSectionCalendarData.getSecCal(prodmonth);
            var sect = gvSectionCal.GetRowCellValue(gvSectionCal.FocusedRowHandle, gvSectionCal.Columns["sectionid"]);
            SECT = Convert.ToString(sect);
            gvSectionCal.OptionsEditForm.CustomEditFormLayout = new Mineware.Systems.Production.SysAdminScreens.SectionCalendar.ileSectionCalendar();
            ((Mineware.Systems.Production.SysAdminScreens.SectionCalendar.ileSectionCalendar)gvSectionCal.OptionsEditForm.CustomEditFormLayout).stopdata(UserCurrentInfo, SECT, editProdmonth.Text);
        }

        public void setProdMonth()
        {
            int ProdMonth;

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [CurrentProductionMonth] ProdMonth FROM [SYSSET]");

            _dbMan.SqlStatement = sb.ToString();
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable SubB = _dbMan.ResultsDataTable;

            ProdMonth = Convert.ToInt32(SubB.Rows[0]["ProdMonth"].ToString());

            editProdmonth.EditValue = TProductionGlobal.ProdMonthAsDate(ProdMonth.ToString());
        }

        private void ucSectionCalendars_Load(object sender, EventArgs e)
        {
            _clsSectionCalendarData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            setProdMonth();
            DateTime dt = Convert.ToDateTime(editProdmonth.EditValue);
            maxd = dt.AddMonths(-1);
            loadData(editProdmonth.Text);

            gvSectionCal.BestFitColumns();


        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            loadData(editProdmonth.Text);
            rowclk = false;
        }

        private void gvSectionCal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            rowclk = true;

            if (gvSectionCal.GetRowCellValue(gvSectionCal.FocusedRowHandle, gvSectionCal.Columns["enddate"]) != null)
                theEndDate = gvSectionCal.GetRowCellValue(gvSectionCal.FocusedRowHandle, gvSectionCal.Columns["enddate"]).ToString();
        }

        private void gvSectionCal_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            //Mineware.Systems.Production.SysAdminScreens.SectionCalendar.ileSectionCalendar myEdit = new Mineware.Systems.Production.SysAdminScreens.SectionCalendar.ileSectionCalendar();

            //if (myEdit.cmbCalTypes.EditValue.ToString() != "")
            //{
            _clsSectionCalendarData.saveData(TUserInfo.UserID, editProdmonth.Text);
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("SAVED DATA", "All data was saved successfully", System.Drawing.Color.Blue);
            loadData(editProdmonth.Text);

            //}

            //else
            //Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Please select a Clendar Type", System.Drawing.Color.Red);
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (rowclk == false)
            {
                MessageItem.viewMessage(MessageType.Info, "Section Calendars", "Please select a record from the list to edit", ButtonTypes.OK, MessageDisplayType.FullScreen);
            }
            else
            {
                bool canShow = false;
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select currentproductionmonth from sysset";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();
                DataTable dt = _dbMan.ResultsDataTable;

                string CurrentProdMonth = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM");

                if (dt.Rows.Count > 0)
                    CurrentProdMonth = dt.Rows[0]["currentproductionmonth"].ToString();

                if (Convert.ToInt32(CurrentProdMonth) <= Convert.ToInt32(editProdmonth.Text))
                    canShow = true;

                DateTime dt1 = DateTime.Now.Date;
                DateTime dt2 = Convert.ToDateTime(theEndDate).Date;

                if (dt1 <= dt2)
                    canShow = true;
                else
                    canShow = false;

                if (canShow == true)
                {
                    gvSectionCal.ShowEditForm();
                }
            }
        }

        private void gvSectionCal_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            object row = view.GetRow(e.RowHandle);
            if ((row as DataRowView)["Status"].ToString() == "0")
            {
                (row as DataRowView)["Status"] = "1";
            }
        }

        private void gvSectionCal_ShowingPopupEditForm(object sender, ShowingPopupEditFormEventArgs e)
        {
            //e.EditForm.Height = value;
            //e.EditForm.WindowState = FormWindowState.Maximized;
            e.EditForm.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
