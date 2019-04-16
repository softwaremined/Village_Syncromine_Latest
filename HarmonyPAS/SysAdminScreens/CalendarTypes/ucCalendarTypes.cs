using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using Mineware.Systems.Global;
using DevExpress.XtraScheduler.Commands;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Production.SysAdminScreens.CalendarTypes
{
    public partial class ucCalendarTypes : Mineware.Systems.Global.ucBaseUserControl
    {
        clsCalTypeData _clsCalTypeData = new clsCalTypeData();
        DataTable calTypes = new DataTable();
        ucCalTypeEdit editScreen = new ucCalTypeEdit();
        string CalendarCode = "";
        const int Working = 0;
        const int NonWorking = 1;
        static Color colWorking = new Color();
        static Color colNonWorking = new Color();


        AppointmentLabel lbWorkingDay;
        AppointmentLabel lbNonWorkingDay;

        public ucCalendarTypes()
        {
            InitializeComponent();
            colWorking = Color.LightGreen;
            colNonWorking = Color.LightPink;
            lbWorkingDay =  new AppointmentLabel(colWorking, "Working Day", "WorkingDay");
            lbNonWorkingDay = new AppointmentLabel(colNonWorking, "Non Working Day", "&NonWorkingDay");

            ssWorkingDays.Appointments.Labels.Clear();
            ssWorkingDays.Appointments.Labels.Add(lbWorkingDay);
            ssWorkingDays.Appointments.Labels.Add(lbNonWorkingDay);
         

        }

        private void loadCalTypes()
        {
            calTypes = _clsCalTypeData.getCalTypes();
            gcCalTypes.DataSource = calTypes;
            gvCalTypes.SelectRow(0);
           

        }

        private void loadDates(string calType)
        {
            ssWorkingDays.Appointments.Mappings.AllDay = "AllDay";
            ssWorkingDays.Appointments.Mappings.Subject = "Heading";
            ssWorkingDays.Appointments.Mappings.Start = "StartTime";
            ssWorkingDays.Appointments.Mappings.End = "EndTime";
            ssWorkingDays.Appointments.Mappings.Label = "theID";
            ssWorkingDays.Appointments.DataSource = _clsCalTypeData.getWorkingDays(calType);
         

        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, DevExpress.XtraScheduler.AppointmentFormEventArgs e)
        {
            Appointment apt = e.Appointment;
         


            AppointmentBaseCollection appList = ssWorkingDays.GetAppointments(apt.Start, apt.End);
            if (appList.Count == 0)
            {
                apt.AllDay = true;
                apt.Subject = lbWorkingDay.DisplayName;
                apt.LabelId = Working;
                ssWorkingDays.Appointments.Add(apt);
                _clsCalTypeData.saveDateInfo(CalendarCode, apt.Start, true);
                
               
            }
            else
            {
                if (appList[0].LabelId == Working)
                {
                    appList[0].LabelId = NonWorking;
                    appList[0].Subject = lbNonWorkingDay.DisplayName;
                    //_clsCalTypeData.saveDateInfo(CalendarCode, appList[0].Start, true);
                    _clsCalTypeData.saveDateInfo(CalendarCode, appList[0].Start, false );
                }
                else
                {
                    appList[0].LabelId = Working;
                    appList[0].Subject = lbWorkingDay.DisplayName;
                    //_clsCalTypeData.saveDateInfo(CalendarCode, appList[0].Start, false);
                    _clsCalTypeData.saveDateInfo(CalendarCode, appList[0].Start, true );
                }

            }
           
            scWorkingNonWorking.Refresh();
            e.Handled = true;
        }

        private void ucCalendarTypes_Load(object sender, EventArgs e)
        {
            dnMain.DateTime = DateTime.Now;
            _clsCalTypeData.theData .ConnectionString  = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            gvCalTypes.OptionsEditForm.CustomEditFormLayout = editScreen;
            loadCalTypes();
        }


        private void gvCalTypes_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                CalendarCode = calTypes.Rows[e.FocusedRowHandle]["CalendarCode"].ToString();
                loadDates(CalendarCode);
            }

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            editScreen.isAdd = true;
            gvCalTypes.AddNewRow();
            gcCalTypes.ForceInitialize();
            gvCalTypes.ShowEditForm();
        }

        private void gvCalTypes_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {

            DataRowView dr = e.Row as DataRowView;
            string CalendarCode = dr["CalendarCode"].ToString();
            string CalendarType = dr["Description"].ToString();
            _clsCalTypeData.saveCalType(CalendarCode, CalendarType);

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //this.gvCalTypes.FocusedRowHandle = 0;
            editScreen.isAdd = false;
            gvCalTypes.ShowEditForm();
        }

        private void setDays(int dayType) // 0 working 1 non working
        {
             int theCount = dnMain.SelectedRanges.Count;
            for (int i = 0; i != theCount; i++)
            {
                Appointment apt = scWorkingNonWorking.Storage.CreateAppointment(AppointmentType.Normal); 
                apt.Start = dnMain.SelectedRanges[i].StartDate;
                switch(dayType)
                {
                    case 0 :
                        apt.LabelKey = dayType;
                        apt.Subject = lbWorkingDay.DisplayName;
                        _clsCalTypeData.saveDateInfo(CalendarCode, apt.Start, true);
                        break;
                    case 1 :
                        apt.LabelKey = dayType;
                        apt.Subject = lbNonWorkingDay.DisplayName;
                        _clsCalTypeData.saveDateInfo(CalendarCode, apt.Start, false);
                        break;
            }
            }

            loadDates(CalendarCode);
            scWorkingNonWorking.Refresh();
       

        }

        private void btnSetWDay_Click(object sender, EventArgs e)
        {
            setDays(Working);
        }

        private void btnSetNWDay_Click(object sender, EventArgs e)
        {
            setDays(NonWorking);
        }

        private void gvCalTypes_DoubleClick(object sender, EventArgs e)
        {
            editScreen.isAdd = false;
        }

        private void dnMain_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            AppointmentBaseCollection appList = ssWorkingDays.GetAppointments(e.Date, e.Date.AddHours(0.7));
            Rectangle rec = new Rectangle(e.Bounds.Location,e.Bounds.Size);
          
            System.Drawing.Pen myPen;

           
            if (appList.Count == 1)
            {
                switch (appList[0].LabelId)
                {
                    case NonWorking:
                        System.Drawing.SolidBrush nwBrush = new System.Drawing.SolidBrush(colNonWorking);
                        e.Graphics.FillRectangle(nwBrush, rec);
                            break;
                    case Working:

                            System.Drawing.SolidBrush wBrush = new System.Drawing.SolidBrush(colWorking);


                            e.Graphics.FillRectangle(wBrush, rec);
                        break;
                        
                }
            }
        }

        private void gcCalTypes_Click(object sender, EventArgs e)
        {

        }
    }
}
