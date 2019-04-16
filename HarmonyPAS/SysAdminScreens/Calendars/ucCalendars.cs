using System;
using System.Collections.Generic;
using System.Linq;
using Mineware.Systems.Global;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Calendars
{
    public partial class ucCalendars : ucBaseUserControl
    {
        clsCalendarsData _clsCalendarsData = new clsCalendarsData();
        DataTable dtCalendarsData;
        string _calendarCode = "";
        string aa;
        string bb;
        string maxdate;
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        public ucCalendars()
        {
            InitializeComponent();
            this.CanClose = true;
        }

        public void loadScreenData()
        {
            try
            {
                _clsCalendarsData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                DataTable dtCalendarCodes = _clsCalendarsData.GetCalendarCodes(lueCalendarCode.EditValue.ToString());
                lueCalendarCode.Properties.DataSource = dtCalendarCodes;
                lueCalendarCode.Properties.ValueMember = "CalendarCode";
                lueCalendarCode.Properties.DisplayMember = "CalendarCode";

                dtCalendarsData = _clsCalendarsData.getCalendarList(lueCalendarCode.EditValue.ToString());
                gcCalendars.DataSource = dtCalendarsData;

                ileCalendars myEdit = new ileCalendars();
                myEdit.UserCurrentInfo = this.UserCurrentInfo;
                myEdit.theSystemDBTag = this.theSystemDBTag;
                myEdit.theOperation = "New";

                gvCalendars.OptionsEditForm.CustomEditFormLayout = myEdit;
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }



        }
       // string Status = "";
        private void btnEdit_Click(object sender, EventArgs e)
        {
            gvCalendars.ShowEditForm();
            
         //   Status = "";
        }

        private void gvSafetyCalendar_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                var totsh = gvCalendars.GetRowCellValue(gvCalendars.FocusedRowHandle, gvCalendars.Columns["TotalShifts"]);
                string TOTSH = Convert.ToString(totsh);
                DevExpress.XtraGrid.Views.Grid.GridView view;
                view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                object row = view.GetRow(e.RowHandle);
                if ((row as DataRowView)["Status"].ToString() == "")
                {
                    (row as DataRowView)["Status"] = "Update";
                }
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }

        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            loadScreenData();
        }

        private void gvSafetyCalendar_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view;
                view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                object row = view.GetRow(e.RowHandle);

                //foreach (DataRow r in dtCalendarsData.Rows)
                //{


                if (lueCalendarCode.EditValue.ToString() == "Mill")
                {
                    var TheData2 = new MWDataManager.clsDataAccess();
                    TheData2.ConnectionString = TConnections.GetConnectionString(resWPAS.systemDBTag, UserCurrentInfo.Connection);
                    TheData2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    TheData2.SqlStatement = "select * from CALENDARMILL";
                    TheData2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    TheData2.ExecuteInstruction();

                    DataTable dtAct2 = TheData2.ResultsDataTable;

                    if (dtAct2.Rows.Count == 0)
                    {
                        var MaxValue = DateTime.Now.ToString("yyyyMM");
                        var CalCode = lueCalendarCode.EditValue.ToString();
                        aa = MaxValue.ToString();
                        bb = CalCode.ToString();
                        (row as DataRowView)["CalendarCode"] = CalCode.ToString();
                        (row as DataRowView)["Month"] = MaxValue.ToString();
                        var MaxDate = DateTime.Now;
                        aa = MaxDate.ToString();
                        (row as DataRowView)["StartDate"] = DateTime.Now;
                        (row as DataRowView)["EndDate"] = DateTime.Now;
                        (row as DataRowView)["TotalShifts"] = "0";
                        (row as DataRowView)["Status"] = "New";
                    }

                    else if (dtAct2.Rows.Count != 0)
                    {
                        var MaxValue = dtCalendarsData.AsEnumerable().Max(rw => Convert.ToInt32(rw["NextMonth"]));
                        var CalCode = dtCalendarsData.AsEnumerable().Max(rw => rw["CalendarCode"].ToString());
                        aa = MaxValue.ToString();
                        bb = CalCode.ToString();
                        (row as DataRowView)["CalendarCode"] = CalCode.ToString();
                        (row as DataRowView)["Month"] = MaxValue.ToString();
                        var MaxDate = dtCalendarsData.AsEnumerable().Max(rw => rw["NextFirstDate"]);
                        aa = MaxDate.ToString();
                        (row as DataRowView)["StartDate"] = MaxDate.ToString();
                        (row as DataRowView)["EndDate"] = MaxDate.ToString();
                        (row as DataRowView)["TotalShifts"] = "0";
                        (row as DataRowView)["Status"] = "New";
                    }
                }

                else
                {
                    var TheData1 = new MWDataManager.clsDataAccess();
                    TheData1.ConnectionString = TConnections.GetConnectionString(resWPAS.systemDBTag, UserCurrentInfo.Connection);
                    TheData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    TheData1.SqlStatement = "select * from CALENDAROTHER where CalendarCode = '" + lueCalendarCode.EditValue.ToString() + "'";
                    TheData1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    TheData1.ExecuteInstruction();

                    DataTable dtAct = TheData1.ResultsDataTable;

                    if (dtAct.Rows.Count == 0)
                    {
                        var MaxValue = DateTime.Now.ToString("yyyyMM");
                        var CalCode = lueCalendarCode.EditValue.ToString();
                        aa = MaxValue.ToString();
                        bb = CalCode.ToString();
                        (row as DataRowView)["CalendarCode"] = CalCode.ToString();
                        (row as DataRowView)["Month"] = MaxValue.ToString();
                        var MaxDate = DateTime.Now;
                        aa = MaxDate.ToString();
                        (row as DataRowView)["StartDate"] = DateTime.Now;
                        (row as DataRowView)["EndDate"] = DateTime.Now;
                        (row as DataRowView)["TotalShifts"] = "0";
                        (row as DataRowView)["Status"] = "New";
                    }

                    else if (dtAct.Rows.Count != 0)
                    {
                        var MaxValue = dtCalendarsData.AsEnumerable().Max(rw => Convert.ToInt32(rw["NextMonth"]));
                        var CalCode = dtCalendarsData.AsEnumerable().Max(rw => rw["CalendarCode"].ToString());
                        aa = MaxValue.ToString();
                        bb = CalCode.ToString();
                        (row as DataRowView)["CalendarCode"] = CalCode.ToString();
                        (row as DataRowView)["Month"] = MaxValue.ToString();
                        var MaxDate = dtCalendarsData.AsEnumerable().Max(rw => rw["NextFirstDate"]);
                        aa = MaxDate.ToString();
                        (row as DataRowView)["StartDate"] = MaxDate.ToString();
                        (row as DataRowView)["EndDate"] = MaxDate.ToString();
                        (row as DataRowView)["TotalShifts"] = "0";
                        (row as DataRowView)["Status"] = "New";
                    }
                }

                //}
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
        }

        private void ucSafetyCalendar_Load(object sender, EventArgs e)
        {
            lueCalendarCode.EditValue = "Costing";

            loadScreenData();
        }

        private void gvSafetyCalendar_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                DataRowView theRow = e.Row as DataRowView;
                var BeginDate = theRow.Row["Startdate"];
                string BeginDate1 = Convert.ToString(BeginDate);
                var EndDate = theRow.Row["Enddate"];
                string EndDate1 = Convert.ToString(EndDate);
                var Totalshifts = theRow.Row["TotalShifts"];
                string Totalshifts1 = Convert.ToString(Totalshifts);

                _clsCalendarsData.SaveData(dtCalendarsData, lueCalendarCode.EditValue.ToString());
                loadScreenData();
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Calendar Data was saved", Color.CornflowerBlue);
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void gvSafetyCalendar_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void gvSafetyCalendar_RowClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (lueCalendarCode.EditValue.ToString() == "Mill")
                {
                    DataRow DR = gvCalendars.GetDataRow(gvCalendars.FocusedRowHandle);
                    string SD = DR["Month"].ToString();
                    var TheData1 = new MWDataManager.clsDataAccess();
                    TheData1.ConnectionString = TConnections.GetConnectionString(resWPAS.systemDBTag, UserCurrentInfo.Connection);
                    TheData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

                    TheData1.SqlStatement = "select * from CALENDARMILL where MillMonth = dbo.GetPrevProdMonth(" + SD + ") ";
                    TheData1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    TheData1.ExecuteInstruction();
                    DataTable date = new DataTable();
                    date = TheData1.ResultsDataTable;
                    maxdate = "";
                    foreach (DataRow dr in date.Rows)
                    {
                        maxdate = dr["endDate"].ToString();
                    }
            ((ileCalendars)gvCalendars.OptionsEditForm.CustomEditFormLayout).listproject(maxdate);
                }

                else
                {
                    DataRow DR = gvCalendars.GetDataRow(gvCalendars.FocusedRowHandle);
                    string SD = DR["Month"].ToString();
                    var TheData1 = new MWDataManager.clsDataAccess();
                    TheData1.ConnectionString = TConnections.GetConnectionString(resWPAS.systemDBTag, UserCurrentInfo.Connection);
                    TheData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

                    TheData1.SqlStatement = "select * from CALENDAROTHER where CalendarCode = '" + lueCalendarCode.EditValue.ToString() + "' and  Month = dbo.GetPrevProdMonth(" + SD + ") ";
                    TheData1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    TheData1.ExecuteInstruction();
                    DataTable date = new DataTable();
                    date = TheData1.ResultsDataTable;
                    maxdate = "";
                    foreach (DataRow dr in date.Rows)
                    {
                        maxdate = dr["endDate"].ToString();
                    }
            ((ileCalendars)gvCalendars.OptionsEditForm.CustomEditFormLayout).listproject(maxdate);
                }

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
        }

        private void lueCalendarCode_EditValueChanged(object sender, EventArgs e)
        {
            loadScreenData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
          
            gvCalendars.AddNewRow();
            gvCalendars.InitNewRow += gvSafetyCalendar_InitNewRow;
            gvCalendars.ShowEditForm();
        }

        private void mwButton1_Click(object sender, EventArgs e)
        {
            loadScreenData();
            gvCalendars.CloseEditForm();
        }
    }
}
