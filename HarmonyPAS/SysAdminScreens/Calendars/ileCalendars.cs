using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Production.SysAdminScreens.Calendars
{
    public partial class ileCalendars : EditFormUserControl
    {
        string _calendarCode = "";
        DataTable dtCalendarsData;
        public string theOperation;
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        public string Totalshifts;
        string maxdate;
        DataTable dt = new DataTable();
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        Calendars.clsCalendarsData _clsCalendarsData = new Calendars.clsCalendarsData();
        string Totalshift;
        string TheCalendarCode;
        string PRD;

        public ileCalendars()
        {
            InitializeComponent();
        }

        private void dteEndDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ileSafetyCalendar_Enter(object sender, EventArgs e)
        {
            // 
        }

        private void dtStartdate_EditValueChanged(object sender, EventArgs e)
        {
            _clsCalendarsData.theData.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, UserCurrentInfo.Connection);
            var TheData1 = new MWDataManager.clsDataAccess();
            TheData1.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, UserCurrentInfo.Connection);
            TheData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

            DataTable dt = new DataTable();
            dt = _clsCalendarsData.getTotalShiftsList(String.Format("{0:yyyy-MM-dd}", dtStartdate.EditValue), String.Format("{0:yyyy-MM-dd}", dtEndDate.EditValue), TheCalendarCode);
            foreach (DataRow dr in dt.Rows)
            {
                txtTotalShifts.Text = dr["Column1"].ToString();

            }
        }

        private void dtEndDate_EditValueChanged(object sender, EventArgs e)
        {
            _clsCalendarsData.theData.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, UserCurrentInfo.Connection);
            _clsCalendarsData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            DataTable dt = new DataTable();
            dt = _clsCalendarsData.getTotalShiftsList(String.Format("{0:yyyy-MM-dd}", dtStartdate.EditValue), String.Format("{0:yyyy-MM-dd}", dtEndDate.EditValue), TheCalendarCode);
            foreach (DataRow dr in dt.Rows)
            {
                txtTotalShifts.Text = dr["Column1"].ToString();

            }
            Totalshifts = txtTotalShifts.Text;
        }

        private void txtTotalShifts_EditValueChanged(object sender, EventArgs e)
        {

            DevExpress.XtraEditors.TextEdit editor = (sender as DevExpress.XtraEditors.TextEdit);
            string aaa = editor.Text;
            txtTotalShifts.Text = aaa;
            if (Totalshifts == null)
            { }
            else if (dtStartdate.Text == dtEndDate.Text)
            {
                txtTotalShifts.Text = "0";
            }
            else if (dtStartdate.Text != dtEndDate.Text)
            {
                _clsCalendarsData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                DataTable dt = _clsCalendarsData.getTotalShiftsList(Convert.ToString(dtStartdate.EditValue), Convert.ToString(dtEndDate.EditValue), TheCalendarCode);
                foreach (DataRow dr in dt.Rows)
                {
                    txtTotalShifts.Text = dr["Column1"].ToString();

                }
                Totalshifts = txtTotalShifts.Text;
            }
        }

        public void loadsections()
        {




            MWDataManager.clsDataAccess _dbManCalType = new MWDataManager.clsDataAccess();
            _dbManCalType.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, UserCurrentInfo.Connection);
            _dbManCalType.SqlStatement = "	select * from CODE_CALENDAR where CalendarCode = '" + TheCalendarCode + "'";

            _dbManCalType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManCalType.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManCalType.ExecuteInstruction();

            DataTable CalType = _dbManCalType.ResultsDataTable;


            //lkCalendarType.Properties.DataSource = CalType;
            //lkCalendarType.Properties.DisplayMember = "CalendarCode";
            //lkCalendarType.Properties.ValueMember = "CalendarCode";

            dtStartdate.EditValueChanged += dtStartdate_EditValueChanged;
            dtEndDate.EditValueChanged += dtEndDate_EditValueChanged;
        }

        public void listproject(string prd)
        {
            maxdate = "";
            maxdate = prd;
            _clsCalendarsData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            DataTable dt = new DataTable();
            dt = _clsCalendarsData.getTotalShiftsList(Convert.ToString(dtStartdate.EditValue), Convert.ToString(dtEndDate.EditValue), TheCalendarCode);
            foreach (DataRow dr in dt.Rows)
            {
                txtTotalShifts.Text = dr["Column1"].ToString();

            }
            Totalshifts = txtTotalShifts.Text;
            loadsections();
        }

        private void dtStartdate_Enter(object sender, EventArgs e)
        {
            DateTime newdatemax = new DateTime();
            if (maxdate == null || maxdate == "")
            {
                newdatemax = Convert.ToDateTime(dtStartdate.EditValue);
                dtStartdate.Properties.MinValue = newdatemax;
                dtStartdate.Properties.MaxValue = newdatemax.AddDays(35);
            }
            else
            {
                newdatemax = Convert.ToDateTime(maxdate);
                dtStartdate.Properties.MinValue = newdatemax.AddDays(1);
                dtStartdate.Properties.MaxValue = newdatemax.AddDays(35);
            }

        }

        private void dtEndDate_Enter(object sender, EventArgs e)
        {
            DateTime dat = new DateTime();
            dat = Convert.ToDateTime(dtStartdate.EditValue);
            dtEndDate.Properties.MinValue = dat;
            dtEndDate.Properties.MaxValue = dat.AddDays(35);
        }

        private void dtStartdate_DrawItem(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            MWDataManager.clsDataAccess _dbManCalType = new MWDataManager.clsDataAccess();
            _dbManCalType.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, UserCurrentInfo.Connection);
            _dbManCalType.SqlStatement = "select TOP 35 * from caltype where calendarcode='" + TheCalendarCode + "' and CalendarDate >= '" + String.Format("{0:yyyy-MM-dd}", dtStartdate.EditValue) + "' and workingday='Y'";

            _dbManCalType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManCalType.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManCalType.ExecuteInstruction();


            dt = _dbManCalType.ResultsDataTable;
            foreach (DataRow dr in dt.Rows)
            {
                string abc = "";
                abc = String.Format("{0:yyyy-MM-dd}", e.Date);
                DateTime ABC = Convert.ToDateTime(abc);

                string bc = "";
                bc = String.Format("{0:yyyy-MM-dd}", dr["CalendarDate"]);
                DateTime BC = Convert.ToDateTime(bc);
                if (ABC.Date.Day == BC.Date.Day)
                {
                    e.Style.BackColor = Color.LightGray;

                }
            }
        }

        private void dtStartdate_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        void LoadScreenData()
        {
            _clsCalendarsData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtCalendarCodes = _clsCalendarsData.GetCalendarCodes(TheCalendarCode);
            //lkCalendarType.Properties.DataSource = dtCalendarCodes;
            //lkCalendarType.Properties.ValueMember = "CalendarCode";
            //lkCalendarType.Properties.DisplayMember = "CalendarCode";

            
        }

        private void ileCalendars_Load(object sender, EventArgs e)
        {
            TheCalendarCode = txtCalendarCode.Text;

            LoadScreenData();
        }

        private void lkCalendarType_EditValueChanged(object sender, EventArgs e)
        {
            LoadScreenData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dtCalendarsData = _clsCalendarsData.getCalendarList(TheCalendarCode);

                //DataRowView theRow = e.Row as DataRowView;
                var BeginDate = dtStartdate.EditValue;
                string BeginDate1 = Convert.ToString(BeginDate);
                var EndDate = dtEndDate.EditValue;
                string EndDate1 = Convert.ToString(EndDate);
                var Totalshifts = txtTotalShifts.Text;
                string Totalshifts1 = Convert.ToString(Totalshifts);

                _clsCalendarsData.SaveData2(dtCalendarsData, TheCalendarCode, Totalshifts1, txtMonth.Text, BeginDate1, EndDate1);
               // loadScreenData();
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Calendar Data was saved", Color.CornflowerBlue);
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
        }
    }
}
