using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPivotGrid;
using Mineware.Systems.Global;
using Mineware.Systems.Global.sysMessages;
using Mineware.Systems.Global.sysNotification;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.ProductionGlobal;

namespace Mineware.Systems.Production.Controls.BookingsABS
{
    public partial class ucBookingsABS : ucBaseUserControl
    {
		public static DateTime BeginDate, EndDate;
		public static int Shifts, TotalShifts;

		public static DateTime theSelectedDate;

		public static string TheSelectedSection = "",
			TheSelectedWorkpalce = "",
			TheSelectedActivity = "",
			TheSelectedDate = "",
			TheSelectedType = "",
			TheEditProblemID = "",
			TheEditSBossNotes = "",
			TheEditNoteDesc,
			TheEditStoppeddate = "",
			TheEditStartDate = "",
			TheEditABSCode,
			TheEditWorkingDay,
			TheEditShiftDay = "";

		private string _ABSCodeDisplay, _ABSNote, _ABSPicNo, _ABSCode, _ABSPrec;

		private decimal _adjsqm, _adjtons, _adjgrams;
        private bool _allowbook;
		private int _bdbookdays;
		private string _BeginDateString, _EndDateString;
        private decimal _blastqual;
		public string _bookdate;

		private string _BookFL, _BookCode;
		private decimal _calcadv, _calcadvreef, _calcadvwaste;
		private int _calccubics;
		private decimal _calccubictons, _calccubicgrams, _calccubickg;
		private int _calcsqm, _calcsqmreef, _calcsqmwaste;

		private decimal _calctons, _calctonsreef, _calctonswaste, _calcgrams, _calckg;
		private string _checkmeaslvl, _checkmeas, _adjbook, _mohierid, _bdbook;
		private bool _clickDev;

		private bool _clickStp;

		private readonly clsBookingsABS _clsBookingsABS = new clsBookingsABS();

		private string _colorA, _colorB, _colorS;
        private string _DevABSCodeDisplay, _DevABSNote, _DevABSPicNo, _DevABSCode, _DevABSPrec;
        private string _DevBookAdv, _DevBookCode, _DevSBossNotes, _DevProblemID;

		private string _DevWP, _DevWPID, _DevActivity, _DevSectionID, _DevActDesc;
		private bool _foundDev;

        private bool _foundStp;
		public string _prodmonth;
		private DateTime _rundate;
		private string _SBossNotes, _ProblemID;
		private readonly sysMessagesClass _sysMessagesClass = new sysMessagesClass();
		private string _WP, _WPID, _Activity, _SectionID, _ActDesc;

		private DataView clone;
		private string CurrShftLbl;
		private DataTable dtBookSection;
		private DataTable dtCalendarInfo;
		private DataTable dtCausedLostBlast;
		private DataTable dtDetailDevelopment;
		private DataTable dtDetailStoping;
		private DataTable dtDevBookCode;
		private DataTable dtPegs;
		private DataTable dtProblemDesc;
		private DataTable dtStpBookCode;

		private DataTable dtSysset;
		private DataTable dtUser;
		private DataTable dtWPDev;
		private DataTable dtWPStp;

        private DataTable dtProdmonth;

        private DataTable dtActivity;

        private string lblMeas;

        int _FocusedColumnindex = -1;
        int _FocusedRowindex = -1;       

        public string PMSection;

       

        private string SBLabel;

        private void gvStoping_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                string test = e.Value.ToString();

                //string ProblemID = gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(e.ColumnField).ToString();
                string ProblemID = e.Value.ToString();

                DataTable dtCheck = new DataTable();
                dtCheck.Clear();
                dtCheck = _clsBookingsABS.CheckProlemExist(ProblemID,0);

                if (dtCheck.Rows.Count > 0)
                {
                    frmProblemBookCode frm = new frmProblemBookCode();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm._theSystemDBTag = theSystemDBTag;
                    frm._UserCurrentInfoConnection = UserCurrentInfo.Connection;
                    frm.lblProblemID.Text = ProblemID;
                    frm.lblAcitvity.Text = "0";
                    frm.ShowDialog();
                }
            }
        }

        public string ShftNo;

		private decimal theBook;

        private void gcDetailStoping_Click(object sender, EventArgs e)
        {

        }

        public bool UpdateSelectValues = true;

        public ucBookingsABS()
        {
            InitializeComponent();
        }

        private void gvDevelopment_DoubleClick(object sender, EventArgs e)
        {
            //use celldoubleClick Rather
        }

        private void gvDevelopment_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                //string ProblemID = gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(e.ColumnField).ToString();
                string ProblemID = e.Value.ToString();

                DataTable dtCheck = new DataTable();
                dtCheck.Clear();
                dtCheck = _clsBookingsABS.CheckProlemExist(ProblemID,1);

                if (dtCheck.Rows.Count > 0)
                {
                    frmProblemBookCode frm = new frmProblemBookCode();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm._theSystemDBTag = theSystemDBTag;
                    frm._UserCurrentInfoConnection = UserCurrentInfo.Connection;
                    frm.lblProblemID.Text = ProblemID;
                    frm.lblAcitvity.Text = "1";
                    frm.ShowDialog();
                }
            }
        }

        private void ucBookingsABS_Load(object sender, EventArgs e)
        {
			Visibles();
			;
            _foundStp = false;
            _foundDev = false;
            _clsBookingsABS.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
			_bookdate = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
			_prodmonth = string.Format("{0:yyyyMM}", DateTime.Now);

            dtSysset = _clsBookingsABS.get_Sysset();
            if (dtSysset.Rows.Count > 0)
            {
                _colorA = dtSysset.Rows[0]["A_Color"].ToString();
                _colorB = dtSysset.Rows[0]["B_Color"].ToString();
                _colorS = dtSysset.Rows[0]["S_Color"].ToString();
                _checkmeas = dtSysset.Rows[0]["CheckMeas"].ToString();
                _checkmeaslvl = dtSysset.Rows[0]["CheckMeasLvl"].ToString();
                _blastqual = Convert.ToDecimal(dtSysset.Rows[0]["PERCBLASTQUALIFICATION"].ToString());
                _adjbook = dtSysset.Rows[0]["AdjBook"].ToString();
                _mohierid = Convert.ToString(Convert.ToInt32(dtSysset.Rows[0]["MOHierarchicalID"].ToString()) + 1);
                //_bdbook = dtSysset.Rows[0]["BDBook"].ToString();
                rpA.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorA));
                rpB.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorB));
                rpS.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorS));
                _prodmonth = dtSysset.Rows[0]["CurrentProductionMonth"].ToString();
                _bookdate = dtSysset.Rows[0]["theRunDate"].ToString();
                _rundate = Convert.ToDateTime(dtSysset.Rows[0]["RunDate"].ToString());
            }
			barProdMonth.EditValue = TProductionGlobal.ProdMonthAsDate(_prodmonth); //getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            barBookDate.EditValue = _bookdate;

            _bdbook = "0";
            dtUser = _clsBookingsABS.get_Users(UserCurrentInfo.UserID);
            if (dtUser.Rows.Count > 0)
            {
                _bdbook = dtUser.Rows[0]["BackDateBooking"].ToString();
                _bdbookdays = Convert.ToInt32(dtUser.Rows[0]["DaysBackDate"].ToString());
            }
        }

        private void Visibles()
        {
            rpgSelection.Enabled = true;
            rpgBookDate.Enabled = false;
            rpgABS.Enabled = false;
            rpgBookings.Enabled = false;
            tabType.Visible = false;
            tabDetail.Visible = false;
        }

        private void InVisibles()
        {
            rpgBookDate.Enabled = true;
            rpgABS.Enabled = true;
            rpgBookings.Enabled = true;
            btnBack.Enabled = true;
            btnSave.Enabled = false;
        }

        string ReconDone = "N";
        private void btnShow_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            if (barSectionID.EditValue != null)
            {
                if (barSectionID.EditValue.ToString() != "")
                {
                    _clickStp = false;
                    _clickDev = false;
                    InVisibles();

					if (_rundate >= BeginDate &&
					    _rundate <= EndDate)
					{
                        rpBookDate.MaxValue = _rundate;
					}

                    tabType.BringToFront();
                    tabDetail.SendToBack();
                    btnSave.Enabled = false;
                    btnBack.Enabled = false;
                    btnClose.Enabled = true;
                    btnAddBooking.Enabled = true;

                    _clsBookingsABS.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    dtWPStp = _clsBookingsABS.get_WorkplacesSTP(_prodmonth, barSectionID.EditValue.ToString());
                    if (dtWPStp.Rows.Count > 0)
                    {
                        _foundStp = true;
                        gvStoping.DataSource = dtWPStp;
                    }

                    dtWPDev = _clsBookingsABS.get_WorkplacesDev(_prodmonth, barSectionID.EditValue.ToString());
                    if (dtWPDev.Rows.Count > 0)
                    {
                        _foundDev = true;
                        gvDevelopment.DataSource = dtWPDev;
                    }
                    if (dtWPDev.Rows.Count > 0)
                    {                        
                        tabType.SelectedPage = tabTypeDevelopment;
                        tabTypeDevelopment.PageVisible = true;
                        tabType.Visible = true;
                    }
                    else
                    {
                        tabTypeDevelopment.PageVisible = false;
                    }
                    if (dtWPStp.Rows.Count > 0)
                    {  
                        tabType.SelectedPage = tabTypeStoping;
                        tabTypeStoping.PageVisible = true;
                        tabType.Visible = true;
                    }
                    else
                    {
                        tabTypeStoping.PageVisible = false;
                    }

                    string result = "False";
                    if (ReconDone == "N")
                    {
                        if (ValidateShouldReconBooking())
                        {
                            var sectionId = barSectionID.EditValue.ToString();
                            IList sectionList;
                            var listSource = rpSectionID.DataSource as IListSource;
                            
                            if (listSource != null)
                            {
                                sectionList = listSource.GetList();
                            }
                            else
                            {
                                sectionList = rpSectionID.DataSource as IList;
                            }

                            var section = string.Empty;
                            if (sectionList != null)
                            {
                                var dataRowView = sectionList[rpSectionID.GetIndexByKeyValue(sectionId)] as DataRowView;
                                if (dataRowView != null)
                                {
                                    if (dataRowView.Row.ItemArray.Length >= 2)
                                    {
                                        section = dataRowView.Row.ItemArray[1].ToString();
                                    }
                                }
                            }

                            
                            var reconBook = new ReconBookingForm
                            {

                                Calendardate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(barBookDate.EditValue)),
                                Prodmonth = string.Format("{0:yyyyMM}", barProdMonth.EditValue),
                                SectionId = sectionId,
                                Section = section.ToString(),                               
                                UserCurrentInfo = UserCurrentInfo,
                                ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection),
                                
                            };
                            reconBook.ShowDialog();
                            result = reconBook.result;
                           

                        }
                    }
                    else
                    {
                        if (result == "False")
                        {
                            tabType.BringToFront();
                            tabDetail.SendToBack();
                            btnSave.Enabled = false;
                            btnBack.Enabled = false;
                            btnClose.Enabled = true;
                            btnAddBooking.Enabled = false;
                        }
                        else
                        {
                           
                        }
                    }
				}
				else
				{
                    MessageBox.Show("Please select a section", "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a section", "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

		private bool ValidateShouldReconBooking()
        {

            string aa = Convert.ToDateTime(barBookDate.EditValue).DayOfWeek.ToString();
            string bb = "Thursday";
            if (SysSettings.Banner != "Masimong Mine")
            {
                bb = "Thursday";
            }
            else
            {
                bb = "Monday";
            }
            if (aa == bb)
            {
                return true;
            }
            else
            {
                return false;
            }

            
		}

		private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
		{
           
                Dispose();
           
        }

		private void btnABS_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (btnABS.Caption == "Show ABS Colours")
            {
                btnABS.Caption = "Remove ABS Colours";
            }
            else
            {
                btnABS.Caption = "Show ABS Colours";
            }

            btnShow_ItemClick(null, null);
        }

		private void btnRemoveABS_ItemClick(object sender, ItemClickEventArgs e)
        {
			if (_clickStp)
            {
                // No Problem Selected so clear the Data.
				var EditedRow = dtDetailStoping.Select(" ProdMonth = '" + _prodmonth + "' "
                                                   + " and SectionID = '" + _SectionID + "' "
                                                   + " and WPID = '" + _WPID + "' "
                                                   + " and CalendarDate = '" + _bookdate + "' "
                                                   + " and Activity in (0,9) ");
				foreach (var dr in EditedRow)
                {
                    dr["ABSCode"] = "";
                    dr["ABSCodeDisplay"] = "";
                    dr["ABSPicNo"] = "";
                    dr["ABSNotes"] = "";
                    dr["ABSPrec"] = "";
                }
                dtDetailStoping.AcceptChanges();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
			if (_clickDev)
            {
                // No Problem Selected so clear the Data.
				var EditedRow = dtDetailDevelopment.Select(" ProdMonth = '" + _prodmonth + "' "
                                                   + "and SectionID = '" + _DevSectionID + "' "
                                                   + "and WPID = '" + _DevWPID + "' "
                                                   + "and CalendarDate = '" + _bookdate + "' "
                                                   + "and Activity = 1 ");
				foreach (var dr in EditedRow)
                {
                    dr["ABSCode"] = "";
                    dr["ABSCodeDisplay"] = "";
                    dr["ABSPicNo"] = "";
                    dr["ABSNotes"] = "";
                    dr["ABSPrec"] = "";
                }
                dtDetailDevelopment.AcceptChanges();
                gcDetailDevelopment.Refresh();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
        }

		private void btnRemoveABSDev_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_DevActivity == "1")
            {
                // No Problem Selected so clear the Data.
				var EditedRow = dtDetailDevelopment.Select(" ProdMonth = '" + _prodmonth + "' "
                                                   + "and SectionID = '" + _DevSectionID + "' "
                                                   + "and WPID = '" + _DevWPID + "' "
                                                   + "and CalendarDate = '" + _bookdate + "' "
                                                   + "and Activity = 1 ");
				foreach (var dr in EditedRow)
                {
                    dr["ABSCode"] = "";
                    dr["ABSCodeDisplay"] = "";
                    dr["ABSPicNo"] = "";
                    dr["ABSNotes"] = "";
                    dr["ABSPrec"] = "";
                }
                dtDetailDevelopment.AcceptChanges();
                gcDetailDevelopment.Refresh();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
        }

		private void btnAddBooking_ItemClick(object sender, ItemClickEventArgs e)
        {
			var _book = Convert.ToDateTime(barBookDate.EditValue.ToString());
			var _begin = string.Format("{0:yyyy-MM-dd}", BeginDate);
			var _end = string.Format("{0:yyyy-MM-dd}", EndDate);

            LoadDetailStoping();
            LoadDetailDevelopment();
            tabDetail.Visible = true;
            tabDetail.BringToFront();
            tabType.SendToBack();
     
            if (dtWPStp.Rows.Count > 0)
            {
                if (tabType.SelectedPage == tabTypeStoping)
				{
                    tabDetail.SelectedPage = tabDetailStoping;
				}
                tabDetailStoping.PageVisible = true;
            }
            else
            {
                tabDetailStoping.PageVisible = false;
            }
            if (dtWPDev.Rows.Count > 0)
            {
                if (tabType.SelectedPage == tabTypeDevelopment)
				{
                    tabDetail.SelectedPage = tabDetailDevelopment;
				}
                tabDetailDevelopment.PageVisible = true;
            }
            else
            {
                tabDetailDevelopment.PageVisible = false;
            }

            btnBack.Enabled = true;
            btnSave.Enabled = false;
			rpgSelection.Enabled = false; //linda
            _allowbook = true;
            if (_bdbook == "0")
            {
                if (_rundate != Convert.ToDateTime(barBookDate.EditValue.ToString()))
                {
                    _allowbook = false;
                }
            }
            if (_bdbook == "1")
            {
				if (Convert.ToDateTime(barBookDate.EditValue.ToString()) < _rundate.AddDays(-_bdbookdays))
                {
                    _allowbook = false;
                }
            }
        }

		private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            ReconDone = "Y";
            if (tabDetailStoping.PageVisible &&
			    tabDetailDevelopment.PageVisible)
            {
				var theValidStoping = Validate_Stoping();                

                if (theValidStoping)
                {
					var theValidDevelopment = Validate_Development();
					if (theValidDevelopment)
                    {
						var theSaveStoping = _clsBookingsABS.Save_Stoping(dtDetailStoping, _adjbook, lblMeas, _checkmeaslvl);
						if (theSaveStoping)
                        {
                            
                            TsysNotification.showNotification("Data Saved", "Stoping Bookings Saved", Color.CornflowerBlue);
							var theSaveDevelopment = _clsBookingsABS.Save_Development(dtDetailDevelopment);
							if (theSaveDevelopment)
                            {
								TsysNotification.showNotification("Data Saved", "Development Bookings Saved", Color.CornflowerBlue);
                                btnShow_ItemClick(null, null);
                                rpgSelection.Enabled = true;
                                btnShow.Enabled = true;
                                btnAddBooking.Enabled = true;
                                btnBack.Enabled = false;
                            }
                        }
                    }
                }
            }
            else
            {
				if (tabDetailStoping.PageVisible)
                {
					var theValidStoping = Validate_Stoping();
					if (theValidStoping)
                    {
						var theSaveStoping = _clsBookingsABS.Save_Stoping(dtDetailStoping, _adjbook, lblMeas, _checkmeaslvl);
						if (theSaveStoping)
                        {
							TsysNotification.showNotification("Data Saved", "Stoping Bookings Saved", Color.CornflowerBlue);
                            btnShow_ItemClick(null, null);
                            rpgSelection.Enabled = true;
                            btnShow.Enabled = true;
                            btnAddBooking.Enabled = true;
                            btnBack.Enabled = false;
                        }
                    }
                }
                else
                {
					if (tabDetailDevelopment.PageVisible)
                    {
						var theValidDevelopment = Validate_Development();
						if (theValidDevelopment)
                        {
							var theSaveDevelopment = _clsBookingsABS.Save_Development(dtDetailDevelopment);
							if (theSaveDevelopment)
                            {
								TsysNotification.showNotification("Data Saved", "Development Bookings Saved", Color.CornflowerBlue);
                                btnShow_ItemClick(null, null);
                                rpgSelection.Enabled = true;
                                btnShow.Enabled = true;
                                btnAddBooking.Enabled = true;
                                btnBack.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

		private void btnCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnAddBooking_ItemClick(null, null);
        }

		private void btnBack_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (btnSave.Enabled == true)
            {
                DialogResult dialogResult = MessageBox.Show("Are You sure all bookings have been saved?", "Booking Validation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    tabType.BringToFront();
                    rpgSelection.Enabled = true;
                    btnShow.Enabled = true;
                    btnSave.Enabled = false;
                    btnBack.Enabled = false;
                    btnAddBooking.Enabled = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                tabType.BringToFront();
                rpgSelection.Enabled = true;
                btnShow.Enabled = true;
                btnSave.Enabled = false;
                btnBack.Enabled = false;
                btnAddBooking.Enabled = true;
            }


          


           
        }

		private void btnProblemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
			var ProbBook = new ucBookingsABSProblems();
            ProbBook.theConnection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
			if (_clickStp)
            {
                ProbBook.TheSection = _SectionID;
                ProbBook.TheWorkpalce = _WP;
				ProbBook.TheActivity = Convert.ToInt32(_Activity);
                ProbBook.TheDate = DateTime.Now;
                ProbBook.ProblemID = _ProblemID;
                ProbBook.SBossNotes = _SBossNotes;
				dtProblemDesc = _clsBookingsABS.get_Problems_Desc(_Activity, _ProblemID);

                ProbBook.ProblemDesc = "";
                if (dtProblemDesc.Rows.Count > 0)
				{
					ProbBook.ProblemDesc = _ProblemID + ":" + dtProblemDesc.Rows[0]["ProblemDesc"];
				}

                ProbBook.ShowDialog();

                //Update The Master Data
                if (ProbBook.ProblemID != "") // Update The data to selected problem
                {
                    if (dtDetailStoping != null)
                    {
						var EditedRow = dtDetailStoping.Select(
                                    " ProdMonth = '" + _prodmonth + "' "
                                    + "and SectionID = '" + ProbBook.TheSection + "' "
                                    + "and WP = '" + ProbBook.TheWorkpalce + "' "
                                    + "and CalendarDate = '" + _bookdate + "'"
                                    + "and Activity = '" + ProbBook.TheActivity + "' ");
						foreach (var dr in EditedRow)
                        {
                            dr["ProblemID"] = ProbBook.ProblemID;
                            dr["SBossNotes"] = ProbBook.SBossNotes;
                        }
                        dtDetailStoping.AcceptChanges();
                        btnSave.Enabled = true;
                        btnAddBooking.Enabled = false;
                    }
                }
            }
			if (_clickDev)
            {
                ProbBook.TheSection = _DevSectionID;
                ProbBook.TheWorkpalce = _DevWP;
				ProbBook.TheActivity = Convert.ToInt32(_DevActivity);
                ProbBook.TheDate = DateTime.Now;
                ProbBook.ProblemID = _DevProblemID;
                ProbBook.SBossNotes = _DevSBossNotes;
				dtProblemDesc = _clsBookingsABS.get_Problems_Desc(_DevActivity, _DevProblemID);

                ProbBook.ProblemDesc = "";
                if (dtProblemDesc.Rows.Count > 0)
				{
					ProbBook.ProblemDesc = _DevProblemID + ":" + dtProblemDesc.Rows[0]["ProblemDesc"];
				}

                ProbBook.ShowDialog();

                if (ProbBook.ProblemID != "") // Update The data to selected problem
                {
                    if (dtDetailDevelopment != null)
                    {
						var EditedRow = dtDetailDevelopment.Select(
                                    " ProdMonth = '" + _prodmonth + "' "
                                    + "and SectionID = '" + ProbBook.TheSection + "' "
                                    + "and WP = '" + ProbBook.TheWorkpalce + "' "
                                    + "and CalendarDate = '" + _bookdate + "'"
                                    + "and Activity = '" + ProbBook.TheActivity + "' ");
						foreach (var dr in EditedRow)
                        {
                            dr["ProblemID"] = ProbBook.ProblemID;
                            dr["SBossNotes"] = ProbBook.SBossNotes;
                        }
                        dtDetailDevelopment.AcceptChanges();
                        btnSave.Enabled = true;
                        btnAddBooking.Enabled = false;
                    }
                }
            }
        }

		private void btnProblemRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
			if (_clickStp)
            {
                // No Problem Selected so clear the Data.
				var EditedRow = dtDetailStoping.Select(" ProdMonth = '" + _prodmonth + "' "
                                                   + " and SectionID = '" + _SectionID + "' "
                                                   + " and WPID = '" + _WPID + "' "
                                                   + " and CalendarDate = '" + _bookdate + "' "
                                                   + " and Activity in (0,9) ");
				foreach (var dr in EditedRow)
                {
                    dr["BookCodeStp"] = "";
                    dr["ProblemID"] = "";
                    dr["SBossNotes"] = "";
                    dr["BookProb"] = "";
                }
                dtDetailStoping.AcceptChanges();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
			if (_clickDev)
            {
                // No Problem Selected so clear the Data.
				var EditedRow = dtDetailDevelopment.Select(" ProdMonth = '" + _prodmonth + "' "
                                                   + "and SectionID = '" + _DevSectionID + "' "
                                                   + "and WPID = '" + _DevWPID + "' "
                                                   + "and CalendarDate = '" + _bookdate + "' "
                                                   + "and Activity = 1 ");
				foreach (var dr in EditedRow)
                {
                    dr["BookCodeDev"] = "";
                    dr["ProblemID"] = "";
                    dr["SBossNotes"] = "";
                }
                dtDetailDevelopment.AcceptChanges();
                gcDetailDevelopment.Refresh();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
        }

		private void gvDetailStoping_ShowingEditor(object sender, CancelEventArgs e)
        {
			GridView view;
			view = sender as GridView;

            if (_allowbook == false)
			{
                e.Cancel = true;
			}

            if ((view.FocusedColumn.FieldName == "BookReefSqm") |
                (view.FocusedColumn.FieldName == "BookwasteSqm"))
            {
				var cellValue = view.GetRowCellValue(view.FocusedRowHandle, col_StpBookSqm).ToString();
                if ((cellValue == "") | (cellValue == "0"))
				{
                    e.Cancel = true;
            }
			}
            if ((view.FocusedColumn.FieldName == "BookFL") |
                (view.FocusedColumn.FieldName == "BookAdv"))
            {
				var celValue1 = view.GetRowCellValue(view.FocusedRowHandle, col_StpRecSqm).ToString();
                if (celValue1 == "Y")
                {
					var cellValue = view.GetRowCellValue(view.FocusedRowHandle, col_StpABSCode).ToString();
                    if (cellValue == "")
					{
                        e.Cancel = true;
                }
				}
                else
				{
                    e.Cancel = true;
            }
			}
            if (view.FocusedColumn.FieldName == "BookCubicMetres")
            {
				var celValue1 = view.GetRowCellValue(view.FocusedRowHandle, col_StpRecCubics).ToString();
                if (celValue1 == "Y")
                {
					var cellValue = view.GetRowCellValue(view.FocusedRowHandle, col_StpABSCode).ToString();
                    if (cellValue == "")
					{
                        //e.Cancel = true;
                    }
				}
                else
				{
                    //e.Cancel = true;
                }
			}
            if (_BookCode == "ST")
            {
                //if ((view.FocusedColumn.FieldName == "BookFL") |
                //    (view.FocusedColumn.FieldName == "BookAdv") |
                //    (view.FocusedColumn.FieldName == "BookCodeStp"))
                //{
                e.Cancel = true;
                //}
            }
        }

		private void gvDetailStoping_RowClick(object sender, RowClickEventArgs e)
        {
			GridView view;
			view = sender as GridView;

			var row = view.GetRow(e.RowHandle);

            if (_allowbook == false)
			{
                return;
			}
            _clickStp = true;
            _clickDev = false;
            _SectionID = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["SectionID"]) != null)
            {
                var SectionID = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["SectionID"]);
                _SectionID = SectionID.ToString();
            }
            _WPID = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["WPID"]) != null)
            {
                var WPID = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["WPID"]);
                _WPID = WPID.ToString();
            }
            _Activity = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["Activity"]) != null)
            {
                var Activity = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["Activity"]);
                _Activity = Activity.ToString();
            }
            _ActDesc = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ActDesc"]) != null)
            {
                var ActDesc = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ActDesc"]);
                _ActDesc = ActDesc.ToString();
            }
            _BookFL = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["BookFL"]) != null)
            {
                var BookFL = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["BookFL"]);
                _BookFL = BookFL.ToString();
            }
            _ABSCode = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ABSCode"]) != null)
            {
                var AbsCode = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ABSCode"]);
                _ABSCode = AbsCode.ToString();
            }
            _ABSCodeDisplay = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ABSCodeDisplay"]) != null)
            {
                var ABSCodeDisplay = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ABSCodeDisplay"]);
                _ABSCodeDisplay = ABSCodeDisplay.ToString();
            }
            _ABSNote = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ABSNote"]) != null)
            {
                var ABSNote = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ABSNote"]);
                _ABSNote = ABSNote.ToString();
            }
            _ABSPicNo = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ABSPicNo"]) != null)
            {
                var ABSPicNo = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ABSPicNo"]);
                _ABSPicNo = ABSPicNo.ToString();
            }
            _ABSPrec = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ABSPrec"]) != null)
            {
                var ABSPrec = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ABSPrec"]);
                _ABSPrec = ABSPrec.ToString();
            }
            _WP = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["WP"]) != null)
            {
                var WP = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["WP"]);
                _WP = WP.ToString();
            }
            _BookCode = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["BookCodeStp"]) != null)
            {
                var BookCode = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["BookCodeStp"]);
                _BookCode = BookCode.ToString();
            }
            _ProblemID = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ProblemID"]) != null)
            {
                var ProblemID = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["ProblemID"]);
                _ProblemID = ProblemID.ToString();
            }
            _SBossNotes = "";
            if (gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["SBossNotes"]) != null)
            {
                var SBossNotes = gvDetailStoping.GetRowCellValue(gvDetailStoping.FocusedRowHandle, gvDetailStoping.Columns["SBossNotes"]);
                _SBossNotes = SBossNotes.ToString();
            }

            if ((view.FocusedColumn.FieldName != "BookCodeStp") &
				(_ABSCodeDisplay != "aaa") )
            {
				var ABSChoice = new ucBookingsABSChoice();
                //ABSChoice.UserCurrentInfo = UserCurrentInfo;
                ABSChoice.MoSecLbl.Text = _SectionID;
                ABSChoice.TheWorkpalce = _WP;
				ABSChoice.ActLbl.Text = _Activity;
                ABSChoice.lblWorkplaceid.Text = _WPID;
                ABSChoice.TheDate = DateTime.Now;
                ABSChoice.OtherdateTimePicker1.Value = Convert.ToDateTime(barBookDate.EditValue);
                ABSChoice.lblStatus.Text = _ABSCode;
                ABSChoice.lblABSNotes.Text = _ABSNote;
                ABSChoice.TheColorA = _colorA;
                ABSChoice.TheColorB = _colorB;
                ABSChoice.TheColorS = _colorS;

                //theSystemDBTag, UserCurrentInfo.Connection

                ABSChoice._theSystemDBTag = theSystemDBTag;
                ABSChoice._UserCurrentInfoConnection = UserCurrentInfo.Connection;

                ABSChoice.StartPosition = FormStartPosition.CenterScreen;
                



                ABSChoice.ShowDialog();


                if (dtDetailStoping != null)
                {
                    var EditedRow = dtDetailStoping.Select(
                                               " ProdMonth = '" + _prodmonth + "' "
                                               + " and SectionID = '" + ABSChoice.TheSection + "' "
                                               + "and WP = '" + ABSChoice.TheWorkpalce + "' "
                                               + "and CalendarDate = '" + _bookdate + "'"
                                               + "and Activity = '" + ABSChoice.TheActivity + "' ");
                    foreach (var dr in EditedRow)
                    {
                        dr["ABSCodeDisplay"] = ABSChoice.TheABSCode;
                        dr["ABSCode"] = ABSChoice.TheABSCode;
                        dr["ABSPicNo"] = "";
                        dr["ABSNotes"] = ABSChoice.ABSNotestxt.Text;
                        dr["ABSPrec"] = "";
                        _ABSCode = ABSChoice.TheABSCode;
                        _ABSCodeDisplay = ABSChoice.TheABSCode;
                        _ABSPicNo = "";
                        _ABSNote = ABSChoice.ABSNotestxt.Text;
                        _ABSPrec = "";
                    }
                    dtDetailStoping.AcceptChanges();
                }


                    //ABSChoice.

                    //           if (ABSChoice.TheActivity != 1)
                    //           {
                    //               //Update The Master Data
                    //               if (ABSChoice.TheABSCode == "A") // Update The data to selected problem
                    //               {
                    //                   if (dtDetailStoping != null)
                    //                   {
                    //		var EditedRow = dtDetailStoping.Select(
                    //                                   " ProdMonth = '" + _prodmonth + "' "
                    //                                   + " and SectionID = '" + ABSChoice.TheSection + "' "
                    //                                   + "and WP = '" + ABSChoice.TheWorkpalce + "' "
                    //                                   + "and CalendarDate = '" + _bookdate + "'"
                    //                                   + "and Activity = '" + ABSChoice.TheActivity + "' ");
                    //		foreach (var dr in EditedRow)
                    //                       {
                    //                           dr["ABSCodeDisplay"] = ABSChoice.TheABSCode;
                    //                           dr["ABSCode"] = ABSChoice.TheABSCode;
                    //                           dr["ABSPicNo"] = "";
                    //                           dr["ABSNotes"] = "";
                    //                           dr["ABSPrec"] = "";
                    //                           _ABSCode = ABSChoice.TheABSCode;
                    //                           _ABSCodeDisplay = ABSChoice.TheABSCode;
                    //                           _ABSPicNo = "";
                    //                           _ABSNote = "";
                    //                           _ABSPrec = "";
                    //                       }
                    //dtDetailStoping.AcceptChanges();
                    //                       btnSave.Enabled = true;
                    //                       btnAddBooking.Enabled = false;
                    //                   }
                    //               }
                    //               _ABSCode = ABSChoice.TheABSCode;
                    //               _ABSCodeDisplay = ABSChoice.TheABSCode;
                    //           }

                    //           if ((_ABSCode == "B") |
                    //               (_ABSCode == "S"))
                    //           {
                    //var ABS = new ucBookingsABSForm1();
                    //               ABS.TheSection = _SectionID;
                    //               ABS.TheWorkpalce = _WP;
                    //               ABS.TheActivity = Convert.ToInt32(_Activity);
                    //               ABS.TheDate = DateTime.Now;
                    //               ABS.TheABSPicNo = _ABSPicNo;
                    //               ABS.TheABSNotes = _ABSNote;
                    //               ABS.TheABSCodeDisplay = _ABSCodeDisplay;
                    //               ABS.TheABSCode = _ABSCode;
                    //               ABS.TheABSPrec = _ABSPrec;

                    //               ABS.ShowDialog();

                    //               if (ABS.TheABSCode != "") // Update The data to selected problem
                    //               {
                    //                   if (dtDetailStoping != null)
                    //                   {
                    //		var EditedRow = dtDetailStoping.Select(
                    //                               " ProdMonth = '" + _prodmonth + "' "
                    //                               + "and SectionID = '" + ABSChoice.TheSection + "' "
                    //                               + "and WP = '" + ABSChoice.TheWorkpalce + "' "
                    //                               + "and CalendarDate = '" + _bookdate + "'"
                    //                               + "and Activity = '" + ABSChoice.TheActivity + "' ");
                    //		foreach (var dr in EditedRow)
                    //                       {
                    //                           dr["ABSCodeDisplay"] = ABS.TheABSCodeDisplay;
                    //                           dr["ABSCode"] = ABS.TheABSCode;
                    //                           dr["ABSPicNo"] = ABS.TheABSPicNo;
                    //                           dr["ABSNotes"] = ABS.TheABSNotes;
                    //                           dr["ABSPrec"] = ABS.TheABSPrec;
                    //                       }
                    //                       dtDetailStoping.AcceptChanges();
                    //                       btnSave.Enabled = true;
                    //                       btnAddBooking.Enabled = false;
                    //                   }
                    //               }
                    //           }
                }
        }
        
		private void gvDetailStoping_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            BandedGridView view;
            view = sender as BandedGridView;
			var row = view.GetRow(view.FocusedRowHandle); // get the current row 

			var selectedBookCode = (row as DataRowView)["BookCodeStp"].ToString();
			var selectedWP = (row as DataRowView)["WP"].ToString();
			var selectedProblemID = (row as DataRowView)["ProblemID"].ToString();
			var selectedSBossNote = (row as DataRowView)["SBossNotes"].ToString();
			var selectedSectionID = (row as DataRowView)["SectionID"].ToString();
			var selectedActivity = (row as DataRowView)["Activity"].ToString();

            btnSave.Enabled = true;

            if (view.FocusedColumn.FieldName == "BookCodeStp")
            {
                if (selectedBookCode == "PS")
                {


                    var PsBook = new ucBookingsABSPlanStop();
                    PsBook.theConnection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    PsBook.TheSection = selectedSectionID;
                    PsBook.TheWorkpalce = selectedWP;
                    PsBook.TheActivity = Convert.ToInt32(selectedActivity);
                    PsBook.TheDate = DateTime.Now;

                    PsBook.ShowDialog();

                }
                if (selectedBookCode == "PR")
                {
					var ProbBook = new ucBookingsABSProblems();
                    ProbBook.theConnection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    ProbBook.TheSection = selectedSectionID;
                    ProbBook.TheWorkpalce = selectedWP;
					ProbBook.TheActivity = Convert.ToInt32(selectedActivity);
                    ProbBook.TheDate = DateTime.Now;

                    ProbBook.ShowDialog();

                    if (ProbBook.TheActivity != 1)
                    {
                        //Update The Master Data
                        if (ProbBook.ProblemID != "") // Update The data to selected problem
                        {
                            if (dtDetailStoping != null)
                            {
								var _lost = "";
								dtCausedLostBlast = _clsBookingsABS.get_CausedLostBlast(TheSelectedActivity, ProbBook.ProblemID);
                                if (dtCausedLostBlast.Rows.Count > 0)
								{
                                    _lost = dtCausedLostBlast.Rows[0]["CausedLostBlast"].ToString();
								}

								var EditedRow = dtDetailStoping.Select(
                                            " ProdMonth = '" + _prodmonth + "' "
                                            + "and SectionID = '" + ProbBook.TheSection + "' "
                                            + "and WP = '" + ProbBook.TheWorkpalce + "' "
                                            + "and CalendarDate = '" + _bookdate + "'"
                                            + "and Activity = '" + ProbBook.TheActivity + "' ");
								foreach (var dr in EditedRow)
                                {
                                    dr["ProblemID"] = ProbBook.ProblemID;
                                    dr["SBossNotes"] = ProbBook.SBossNotes;
                                    dr["CausedLostBlast"] = _lost;
                                }
                                dtDetailStoping.AcceptChanges();
                                btnSave.Enabled = true;
                                btnAddBooking.Enabled = false;
                            }
                        }
                    }
                }
            }
            if ((view.FocusedColumn.FieldName == "BookFL") |
                (view.FocusedColumn.FieldName == "BookAdv"))
            {
				var selectedVal = (row as DataRowView)["BookFL"].ToString();
                if (selectedVal == "")
                {
                    (row as DataRowView)["BookFL"] = "0";
                    dtDetailDevelopment.AcceptChanges();
                }
                selectedVal = (row as DataRowView)["BookAdv"].ToString();
                if (selectedVal == "")
                {
                    (row as DataRowView)["BookAdv"] = "0.00";
                    dtDetailDevelopment.AcceptChanges();
                }
                Do_CalcsStp(Convert.ToDecimal((row as DataRowView)["BookFL"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookAdv"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookSW"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookCmgt"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookDens"].ToString()));
                (row as DataRowView)["BookSqm"] = _calcsqm;
                (row as DataRowView)["BookReefSqm"] = _calcsqmreef;
                (row as DataRowView)["BookWasteSqm"] = _calcsqmwaste;
                (row as DataRowView)["BookTons"] = _calctons;
                (row as DataRowView)["BookReefTons"] = _calctonsreef;
                (row as DataRowView)["BookWasteTons"] = _calctonswaste;
                (row as DataRowView)["BookGrams"] = _calcgrams;
                (row as DataRowView)["BookKG"] = _calckg;
                (row as DataRowView)["BookAdvReef"] = _calcadvreef;
                (row as DataRowView)["BookAdvWaste"] = _calcadvwaste;
                if (_calcsqm > 0)
				{
                    if (selectedProblemID == "")
					{
                        (row as DataRowView)["BookCodeStp"] = "BL";
					}
                    else
					{
                        (row as DataRowView)["BookCodeStp"] = "PR";
					}
				}
                view.UpdateCurrentRow();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
            if (view.FocusedColumn.FieldName == "BookReefSqm")
            {
				var selectedVal = (row as DataRowView)["BookReefSqm"].ToString();
                if (selectedVal == "")
                {
                    (row as DataRowView)["BookReefSqm"] = "0";
                    dtDetailDevelopment.AcceptChanges();
                }
                Do_CalcsStp1(Convert.ToDecimal((row as DataRowView)["BookSqm"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookReefSqm"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookSW"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookCmgt"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookDens"].ToString()),
                          Convert.ToDecimal((row as DataRowView)["BookFL"].ToString()),
                           Convert.ToDecimal((row as DataRowView)["BookAdv"].ToString()));
                //(row as DataRowView)["BookSqm"] = _calcsqm;
                //(row as DataRowView)["BookReefSqm"] = _calcsqmreef;
                (row as DataRowView)["BookWasteSqm"] = _calcsqmwaste;
                (row as DataRowView)["BookTons"] = _calctons;
                (row as DataRowView)["BookReefTons"] = _calctonsreef;
                (row as DataRowView)["BookWasteTons"] = _calctonswaste;
                (row as DataRowView)["BookGrams"] = _calcgrams;
                (row as DataRowView)["BookKG"] = _calckg;
                (row as DataRowView)["BookAdvReef"] = _calcadvreef;
                (row as DataRowView)["BookAdvWaste"] = _calcadvwaste;
                if (_calcsqm > 0)
				{
                    if (selectedProblemID == "")
					{
                        (row as DataRowView)["BookCodeStp"] = "BL";
					}
                    else
					{
                        (row as DataRowView)["BookCodeStp"] = "PR";
					}
				}
                view.UpdateCurrentRow();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
            if (view.FocusedColumn.FieldName == "BookWasteSqm")
            {
				var selectedVal = (row as DataRowView)["BookWasteSqm"].ToString();
                if (selectedVal == "")
                {
                    (row as DataRowView)["BookWasteSqm"] = "0";
                    dtDetailDevelopment.AcceptChanges();
                }
                Do_CalcsStp2(Convert.ToDecimal((row as DataRowView)["BookSqm"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookWasteSqm"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookSW"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookCmgt"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookDens"].ToString()),
                          Convert.ToDecimal((row as DataRowView)["BookFL"].ToString()),
                           Convert.ToDecimal((row as DataRowView)["BookAdv"].ToString()));
                (row as DataRowView)["BookSqm"] = _calcsqm;
                (row as DataRowView)["BookReefSqm"] = _calcsqmreef;
                (row as DataRowView)["BookWasteSqm"] = _calcsqmwaste;
                (row as DataRowView)["BookTons"] = _calctons;
                (row as DataRowView)["BookReefTons"] = _calctonsreef;
                (row as DataRowView)["BookWasteTons"] = _calctonswaste;
                (row as DataRowView)["BookGrams"] = _calcgrams;
                (row as DataRowView)["BookKG"] = _calckg;
                (row as DataRowView)["BookAdvReef"] = _calcadvreef;
                (row as DataRowView)["BookAdvWaste"] = _calcadvwaste;
                if (_calcsqm > 0)
				{
                    if (selectedProblemID == "")
					{
                        (row as DataRowView)["BookCodeStp"] = "BL";
					}
                    else
					{
                        (row as DataRowView)["BookCodeStp"] = "PR";
					}
				}
                view.UpdateCurrentRow();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
            if (view.FocusedColumn.FieldName == "BookCubicMetres")
            {
				var selectedVal = (row as DataRowView)["BookCubicMetres"].ToString();
                if (selectedVal == "")
                {
                    (row as DataRowView)["BookCubicMetres"] = "0";
                    dtDetailDevelopment.AcceptChanges();
                }
                Do_CalcCubics(Convert.ToInt32((row as DataRowView)["BookCubicMetres"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookCubicGT"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookDens"].ToString()));
                (row as DataRowView)["BookCubicMetres"] = _calccubics;
                (row as DataRowView)["BookCubicTons"] = _calccubictons;
                (row as DataRowView)["BookCubicGrams"] = _calccubicgrams;
                (row as DataRowView)["BookCubicKG"] = _calccubickg;
                if (_calccubics > 0)
				{
                    if (selectedProblemID == "")
					{
                        (row as DataRowView)["BookCodeStp"] = "BL";
					}
                    else
					{
                        (row as DataRowView)["BookCodeStp"] = "PR";
					}
				}
                view.UpdateCurrentRow();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
            if (view.FocusedColumn.FieldName == "CheckSqm")
            {
                if (lblMeas == "Check Measurement Day")
				{
                    if (_adjbook == "Y")
                    {
                        Do_CalcsStpAdj(Convert.ToDecimal((row as DataRowView)["CheckSqm"].ToString()),
                             Convert.ToDecimal((row as DataRowView)["ProgBookSqm"].ToString()),
                             Convert.ToDecimal((row as DataRowView)["PrevAdjSqm"].ToString()),
                             Convert.ToDecimal((row as DataRowView)["BookSW"].ToString()),
                             Convert.ToDecimal((row as DataRowView)["BookCmgt"].ToString()),
                             Convert.ToDecimal((row as DataRowView)["BookDens"].ToString()));
                        (row as DataRowView)["AdjSqm"] = _adjsqm;
                        (row as DataRowView)["AdjTons"] = _adjtons;
                        (row as DataRowView)["AdjCont"] = _adjgrams;
                        view.UpdateCurrentRow();
                        btnSave.Enabled = true;
                        btnAddBooking.Enabled = false;
                    }
            }
        }
		}

        private void Do_CalcsStp(decimal _theFL, decimal _theAdv, decimal _theSW, decimal _thecmgt, decimal _theDens)
        {
            _calcsqm = 0;
            _calcsqmreef = 0;
            _calcsqmwaste = 0;
            _calctons = 0;
            _calctonsreef = 0;
            _calctonswaste = 0;
            _calcgrams = 0;
            _calckg = 0;
            _calcadv = 0;
            _calcadvreef = 0;
            _calcadvwaste = 0;

            _calcsqm = Convert.ToInt32(_theFL * _theAdv);
            _calcsqmreef = Convert.ToInt32(_theFL * _theAdv);
            _calcsqmwaste = _calcsqm - _calcsqmreef;
            if (_theFL == 0)
                _calcadvreef = 0;
            else
            _calcadvreef = Math.Round(_calcsqmreef / _theFL, 1);
            _calcadvwaste = _theAdv - _calcadvreef;
            _calctons = _calcsqm * _theSW / 100 * _theDens; //_theFL * _theAdv * _theSW / 100 * _theDens;
            _calctonsreef = _calcsqmreef * _theSW / 100 * _theDens;
            _calctonswaste = _calcsqmwaste * _theSW / 100 * _theDens;
            _calcgrams = _calcsqmreef * _thecmgt / 100 * _theDens;
                    //_theFL * _theAdv * _theSW / 100 * _theDens * _thegt;
            _calckg = _calcsqmreef * _thecmgt / 100 * _theDens / 1000; 
                    //_theFL * _theAdv * _theSW / 100 * _theDens * _thegt / 1000;
        }

        private void Do_CalcsStp1(decimal _sqm, decimal _sqmreef, decimal _theSW, decimal _thecmgt, decimal _theDens, decimal _theFL, decimal _theAdv)
        {
            _calcsqm = 0;
            _calcsqmreef = 0;
            _calcsqmwaste = 0;
            _calctons = 0;
            _calctonsreef = 0;
            _calctonswaste = 0;
            _calcgrams = 0;
            _calckg = 0;
            _calcadvreef = 0;
            _calcadvwaste = 0;

            _calcsqm = Convert.ToInt32(_sqm);
            _calcsqmreef = Convert.ToInt32(_sqmreef);
            _calcsqmwaste = _calcsqm - _calcsqmreef;
            if (_theFL == 0)
                _calcadvreef = 0;
            else
            _calcadvreef = Math.Round(_calcsqmreef / _theFL, 2);
            _calcadvwaste = _theAdv - _calcadvreef;
            _calctons = _calcsqm * _theSW / 100 * _theDens;
            _calctonsreef = _calcsqmreef * _theSW / 100 * _theDens;
            _calctonswaste = _calcsqmwaste * _theSW / 100 * _theDens;
            _calcgrams = _calcsqmreef * _thecmgt / 100 * _theDens;
            _calckg = _calcsqmreef * _thecmgt / 100 * _theDens / 1000;
        }

        private void Do_CalcsStp2(decimal _sqm, decimal _sqmwaste, decimal _theSW, decimal _thecmgt, decimal _theDens, decimal _theFL, decimal _theAdv)
        {
            _calcsqm = 0;
            _calcsqmreef = 0;
            _calcsqmwaste = 0;
            _calctons = 0;
            _calctonsreef = 0;
            _calctonswaste = 0;
            _calcgrams = 0;
            _calckg = 0;
            _calcadvreef = 0;
            _calcadvwaste = 0;

            _calcsqm = Convert.ToInt32(_sqm);
            _calcsqmreef = Convert.ToInt32(_sqm - _sqmwaste);
            _calcsqmwaste = Convert.ToInt32(_sqmwaste);
            if (_theFL == 0)
                _calcadvwaste = 0;
            else
            _calcadvwaste = Math.Round(_calcsqmwaste / _theFL, 2);
            _calcadvreef = _theAdv - _calcadvwaste;
            _calctons = _calcsqm * _theSW / 100 * _theDens;
            _calctonsreef = _calcsqmreef * _theSW / 100 * _theDens;
            _calctonswaste = _calcsqmwaste * _theSW / 100 * _theDens;
            _calcgrams = _calcsqmreef * _thecmgt / 100 * _theDens;
            _calckg = _calcsqmreef * _thecmgt / 100 * _theDens / 1000;
        }

        private void Do_CalcCubics(int _theCubics, decimal _theGT, decimal _theDens)
        {
            _calccubics = 0;
            _calccubictons = 0;
            _calccubicgrams = 0;
            _calccubickg = 0;

            _calccubics = _theCubics;
            _calccubictons = _calccubics * _theDens;
			_calccubicgrams = _calccubics * _theDens * _theGT;
            _calccubickg = _calccubics * _theDens * _theGT / 1000;
        }

        private void Do_CalcsStpAdj(decimal _theCheckSqm, decimal _theProgBookSqm, decimal _thePrevAdjSqm,
                        decimal _theSW, decimal _thegt, decimal _theDens)
        {
            _adjsqm = 0;
            _adjtons = 0;
            _adjgrams = 0;

            _adjsqm = _theCheckSqm - _theProgBookSqm - _thePrevAdjSqm;
			_adjtons = _adjsqm * _theSW / 100 * _theDens;
			_adjgrams = _adjsqm * _theSW / 100 * _theDens * _thegt;
        }
        string activityrecon = "";
        private void barSectionID_EditValueChanged(object sender, EventArgs e)
        {
            Visibles();
            ReconDone = "N";
            barShiftInfo.EditValue = "";
            if (barSectionID.EditValue != null)
			{
                if (barSectionID.EditValue.ToString() != "")
                {
                    try
                    {
                        dtProdmonth = _clsBookingsABS.GetProdmonth(barSectionID.EditValue.ToString(), barBookDate.EditValue.ToString());
                        dtActivity = _clsBookingsABS.GetActivity(barSectionID.EditValue.ToString(), barBookDate.EditValue.ToString());
                    }
                    catch
                    {
                        _sysMessagesClass.viewMessage(MessageType.Error, "Error finding Calendars", resWPAS.systemTag, "ucBookings", "BtnShow_Click",
                            "Cannot find Calendar or Planning for Prodmonth:" + _prodmonth, ButtonTypes.OK, MessageDisplayType.Small);
                        return;
                    }

                    if (dtProdmonth.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dtProdmonth.Rows)
                        {

                            try
                            {
                                barProdMonth.EditValue = TProductionGlobal.ProdMonthAsDate(dr["Prodmonth"].ToString());
                            }
                            catch
                            {
                                return;
                            }
                        }
                    }

                    if (dtActivity.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dtActivity.Rows)
                        {
                            activityrecon = dr["Activity"].ToString();
                        }
                    }
                    
                        dtCalendarInfo = _clsBookingsABS.GetShiftInfo(Convert.ToDateTime(barProdMonth.EditValue).ToString("yyyyMM"), barSectionID.EditValue.ToString());
                   

                    if (dtCalendarInfo.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dtCalendarInfo.Rows)
                        {
                            BeginDate = Convert.ToDateTime(dr["Begindate"].ToString());
                            EndDate = Convert.ToDateTime(dr["Enddate"].ToString());
                            _BeginDateString = BeginDate.ToString("yyyy-MM-dd");
                            _EndDateString = EndDate.ToString("yyyy-MM-dd");
                            Shifts = Convert.ToInt32(dr["Shift"].ToString());
                            TotalShifts = Convert.ToInt32(dr["TotalShifts"].ToString());
                        }
                    }
                    else
                    {
						_sysMessagesClass.viewMessage(MessageType.Error, "Error finding Calendars", resWPAS.systemTag, "ucBookings", "BtnShow_Click",
							"Cannot find Calendar or Planning for Prodmonth:" + _prodmonth, ButtonTypes.OK, MessageDisplayType.Small);
                        return;
                    }

					barShiftInfo.EditValue = "Calendar dates from " + BeginDate.ToString("yyyy-MM-dd") + " to " + EndDate.ToString("yyyy-MM-dd") + "  Shift " + Shifts + " of " + TotalShifts;
                    lblMeas = Shifts.ToString();
                    rpBookDate.MinValue = BeginDate.AddDays(-1);
                    rpBookDate.MaxValue = EndDate;

                    if (EndDate <= DateTime.Now)
					{
                        barBookDate.EditValue = EndDate;
                }
        }
			}
		}

        private bool Validate_Stoping()
        {
            foreach (DataRow drStp in dtDetailStoping.Rows)
            {
                //if (drStp["ABSCodeDisplay"].ToString() == "No Vis.")
                //{
                //    return true;
                //}


                if (drStp["BookCodeStp"] == null)
                {
                    tabDetail.SelectedPage = tabDetailStoping;
                    MessageBox.Show("No Booking Code has been entered for Workplace : " +
                                    drStp["WP"], "", MessageBoxButtons.OK);
                    return false;
                }
                    if (drStp["BookCodeStp"].ToString() == "")
                    {
                        tabDetail.SelectedPage = tabDetailStoping;
                        MessageBox.Show("No Booking Code has been entered for Workplace : " +
                                        drStp["WP"], "", MessageBoxButtons.OK);
                        return false;
                    }
				var OrgCheck = "F";

				if (drStp["BookCodeStp"].ToString() == "BL" ||
				    drStp["BookCodeStp"].ToString() == "PR" ||
				    drStp["BookCodeStp"].ToString() == "NPB")
                {
                    OrgCheck = "P";
                }
                if (drStp["ABSCodeDisplay"] == null)
                {
                    tabDetail.SelectedPage = tabDetailStoping;
                    MessageBox.Show("No Workplace Report has been entered for Workplace : " +
                                    drStp["WP"], "", MessageBoxButtons.OK);
                    return false;
                }
                    if (drStp["ABSCodeDisplay"].ToString() == "")
                    {
                        tabDetail.SelectedPage = tabDetailStoping;
                        MessageBox.Show("No Workplace Report has been entered for Workplace : " +
                                        drStp["WP"], "", MessageBoxButtons.OK);
                        return false;
                }
				if (drStp["ABSCode"].ToString() == "S" && drStp["ABSNotes"].ToString() == "")
                {
                    tabDetail.SelectedPage = tabDetailStoping;
                    MessageBox.Show("ABS Code is S - please entered for ABS Note for Workplace : " +
                                    drStp["WP"], "", MessageBoxButtons.OK);
                    return false;
                }
                if ((drStp["BookCodeStp"].ToString() == "BL") &
                    (Convert.ToDecimal(drStp["BookFL"].ToString()) == 0))
                {
                    tabDetail.SelectedPage = tabDetailStoping;
                    MessageBox.Show("You have booked a Blast. Please enter a Booked FL for Workplace : " +
					                drStp["WP"] + "", "", MessageBoxButtons.OK);
                    return false;
                }
				if (Convert.ToDecimal(drStp["BookFL"].ToString()) / Convert.ToDecimal(drStp["FL"].ToString()) <
				    _blastqual / 100)
                {
                    tabDetail.SelectedPage = tabDetailStoping;
					if (drStp["BookCodeStp"].ToString() == "DP" || drStp["BookCodeStp"].ToString() == "NPB" ||
					    drStp["BookCodeStp"].ToString() == "ST" || drStp["BookCodeStp"].ToString() == "PS")
					{ }
                    else
                    {
                        if (drStp["ProblemID"].ToString() != "")
						{ }
                        else
                        {
                            tabDetail.SelectedPage = tabDetailStoping;
							MessageBox.Show("Please add either a Problem or a FL greater than " + _blastqual + "% of the Plan FL " +
							                "for Workplace : " + drStp["WP"] + "", "", MessageBoxButtons.OK);
                            return false;
                        }
                    }
                }
				if (drStp["BookCodeStp"].ToString() == "DP" || drStp["BookCodeStp"].ToString() == "NPB" ||
				    drStp["BookCodeStp"].ToString() == "ST" || drStp["BookCodeStp"].ToString() == "PS")
                {
                    if ((Convert.ToDecimal(drStp["BookSqm"].ToString()) > 0) |
                        (Convert.ToDecimal(drStp["BookCubicMetres"].ToString()) > 0))
                    {
                        tabDetail.SelectedPage = tabDetailStoping;
						MessageBox.Show("Sqm or Cubics have been added with code " + drStp["BookCodeStp"] + " " +
						                "for Workplace : " + drStp["WP"] + "", "", MessageBoxButtons.OK);
                        return false;
                    }
                }
                if (drStp["BookCodeStp"].ToString() == "BL")
                {
					if (Convert.ToDecimal(drStp["BookSqm"].ToString()) == 0 &&
					    Convert.ToDecimal(drStp["BookCubicMetres"].ToString()) == 0)
                    {
                        tabDetail.SelectedPage = tabDetailStoping;
                        MessageBox.Show("Zero Sqm / Cubics was Booked and no Lost Blast Problem " +
						                "for Workplace : " + drStp["WP"] + "", "", MessageBoxButtons.OK);
                        return false;
                    }
                }
                if (SBLabel == "Check Measurement Day")
                {
                    if (drStp["CheckSqm"] == null)
                    {
                        tabDetail.SelectedPage = tabDetailStoping;
						MessageBox.Show("No Check measurement has been added for Workplace : " + drStp["WP"], "", MessageBoxButtons.OK);
                        return false;
                    }
                        if (drStp["CheckSqm"].ToString() == "")
                        {
                            tabDetail.SelectedPage = tabDetailStoping;
						MessageBox.Show("No Check measurement has been added for Workplace : " + drStp["WP"], "", MessageBoxButtons.OK);
                            return false;
                        }
                    }
                }
            return true;
        }

        private bool Validate_Development()
        {
            foreach (DataRow drDev in dtDetailDevelopment.Rows)
            {
                //string Orglbl = drDev["OrgUnit"].ToString();

                //if (drDev["ABSCodeDisplay"].ToString() == "No Vis.")
                //{
                //    return true;
                //}

                var OrgCheck = "F";

                if (drDev["BookCodeDev"] == null)
                {
                    tabDetail.SelectedPage = tabDetailDevelopment;
                    MessageBox.Show("No Booking Code has been entered for Workplace : " + drDev["WP"] + "", "", MessageBoxButtons.OK);
                    return false;
                }
                    if (drDev["BookCodeDev"].ToString() == "")
                    {
                        tabDetail.SelectedPage = tabDetailDevelopment;
                        MessageBox.Show("No Booking Code has been entered for Workplace : " +
                                        drDev["WP"], "", MessageBoxButtons.OK);
                        return false;
                    }
				if (drDev["BookCodeDev"].ToString() == "BL" || drDev["BookCodeDev"].ToString() == "PR")
                {
                    OrgCheck = "P";
                }
                if (drDev["ABSCodeDisplay"] == null)
                {
                    tabDetail.SelectedPage = tabDetailDevelopment;
                    MessageBox.Show("No ABS Code has been entered for Workplace : " +
					                drDev["WP"], "", MessageBoxButtons.OK);
                    return false;
                }
                    if (drDev["ABSCodeDisplay"].ToString() == "")
                    {
                        tabDetail.SelectedPage = tabDetailDevelopment;
                        MessageBox.Show("No ABS Code has been entered for Workplace : " +
					                drDev["WP"], "", MessageBoxButtons.OK);
                        return false;
                    }
				if (drDev["ABSCodeDisplay"].ToString() == "S" && drDev["ABSNotes"].ToString() == "")
                {
                    tabDetail.SelectedPage = tabDetailDevelopment;
                    MessageBox.Show("ABS Code is S - please entered for ABS Note for Workplace : " +
                                    drDev["WP"], "", MessageBoxButtons.OK);
                    return false;
                }
				if (drDev["BookCodeDev"].ToString() == "DP" || drDev["BookCodeDev"].ToString() == "NP" ||
				    drDev["BookCodeDev"].ToString() == "ST")
                {
                    if ((Convert.ToDecimal(drDev["BookAdv"].ToString()) > 0) |
                        (Convert.ToDecimal(drDev["BookCubicMetres"].ToString()) > 0))
                    {
                        tabDetail.SelectedPage = tabDetailDevelopment;
						MessageBox.Show("Advance / Cubics have been added with code " + drDev["BookCodeDev"] +
						                " for Workplace : " + drDev["WP"], "", MessageBoxButtons.OK);
                        return false;
                    }
                }
                if (drDev["BookCodeDev"].ToString() == "BL")
                {
					if (Convert.ToDecimal(drDev["BookAdv"].ToString()) == 0 &&
					    Convert.ToDecimal(drDev["BookCubicMetres"].ToString()) == 0)
                    {
                        tabDetail.SelectedPage = tabDetailDevelopment;
						MessageBox.Show("Zero Advance and Cubics have been booked and no Lost Blast Problem for Workplace : " + drDev["WP"], "", MessageBoxButtons.OK);
                        return false;
                    }
                }
                if (drDev["BookCodeDev"].ToString() == "BL")
                {
                    if (Convert.ToDecimal(drDev["BookAdv"].ToString()) < 0)
                    {
                        tabDetail.SelectedPage = tabDetailDevelopment;
						MessageBox.Show("A negative Advance may not be booked for Workplace : " + drDev["WP"], "", MessageBoxButtons.OK);
                        return false;
                    }
                }
                //if (Orglbl == drDev["OrgUnit"].ToString())
                //{
                //    if ((drDev["BookCodeDev"].ToString() == "BL") || (drDev["BookCodeDev"].ToString() == "PR"))
                //    {
                //        if (OrgCheck == "F")
                //        {
                //            OrgCheck = "P";
                //        }
                //    }

                //}
                //else
                //{
                //    if (OrgCheck == "F")
                //    {
                //        MessageBox.Show("No Adv or problem has been booked for orgunit " + Orglbl + "", "", MessageBoxButtons.OK);
                //        return false;
                //    }
                //    Orglbl = drDev["OrgUnit"].ToString();
                //    OrgCheck = "F";
                //    if ((drDev["BookCodeDev"].ToString() == "BL") || (drDev["BookCodeDev"].ToString() == "PR"))
                //    {
                //        OrgCheck = "P";
                //    }

                //}

                // do final check for dual panel
                //if (OrgCheck == "F")
                //{
                //    tabDetail.SelectedPage = tabDetailDevelopment;
                //    MessageBox.Show("No Adv or problem has been booked for orgunit", "", MessageBoxButtons.OK);
                //    return false;
                //}
                //if (Convert.ToDecimal(drDev["BookAdv"].ToString()) == 0)
                //{
                //    tabDetail.SelectedPage = tabDetailDevelopment;
                //    MessageBox.Show("Adv is 0 for Workplace : " + drDev["WP"].ToString(), "", MessageBoxButtons.OK);
                //    return false;
                //}
                //if (Convert.ToDecimal(drDev["BookAdv"].ToString()) < 0)
                //{
                //    tabDetail.SelectedPage = tabDetailDevelopment;
                //    MessageBox.Show("Adv is less than 0 for Workplace : " + drDev["WP"].ToString(), "", MessageBoxButtons.OK);
                //    return false;
                //}
            }
            return true;
        }

        public void LoadDetailStoping()
        {
            _clsBookingsABS.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtStpBookCode = _clsBookingsABS.get_BookCodeStp();
            rpBookCodeStp.DataSource = dtStpBookCode;
            rpBookCodeStp.ValueMember = "BookCodeStp";
            rpBookCodeStp.DisplayMember = "BookDescStp";

            dtDetailStoping = _clsBookingsABS.get_DetailStoping(_prodmonth, barSectionID.EditValue.ToString(), _bookdate);
            if (dtDetailStoping.Rows.Count > 0)
            {
                gcDetailStoping.DataSource = dtDetailStoping;
                //tabType.Visible = false;
                //tabDetail.Visible = true;
                //tabType.SendToBack();
                //tabDetail.BringToFront();
                //tabDetailStoping.PageVisible = true;
                //tabDetailStoping.BringToFront();
                //gcDetailStoping.Visible = true;
               // gcDetailStoping.BringToFront();
            }
        }

        public void LoadDetailDevelopment()
        {
            _clsBookingsABS.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtDevBookCode = _clsBookingsABS.get_BookCodeDev();
            rpBookCodeDev.DataSource = dtDevBookCode;
            rpBookCodeDev.ValueMember = "BookCodeDev";
            rpBookCodeDev.DisplayMember = "BookDescDev";

			if (_foundDev)
            {
                //dsDev = new DataSet();

                //if (dsDev.Relations.Count > 0)
                //{
                //    dsDev.Relations.Clear();
                //}
                //if (dsDev.Tables.Count > 0)
                //{
                //    dsDev.Tables.Clear();
                //}

                //dsDev.EnforceConstraints = false;
                //dsDev.Tables.Add(_clsBookingsABS.get_DetailDevelopment(_prodmonth, barSectionID.EditValue.ToString(), _bookdate));

                dtDetailDevelopment = _clsBookingsABS.get_DetailDevelopment(_prodmonth, barSectionID.EditValue.ToString(), _bookdate);
                if (dtDetailDevelopment.Rows.Count > 0)
                {
                    Load_Pegs();
                    //dsDev.Tables.Add(_clsBookingsABS.get_Pegs(barSectionID.EditValue.ToString(), _DevWPID, "2017-03-07"));
                    //dsDev.Relations.Add("aa", dsDev.Tables[0].Columns["WPID"], dsDev.Tables[1].Columns["WPID"], false);


                    gcDetailDevelopment.DataSource = dtDetailDevelopment;
                }
            }
        }

        string prodmonth = "";

        private void barProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            Visibles();
			var dd = Convert.ToDateTime(barProdMonth.EditValue.ToString());
            _prodmonth = dd.ToString("yyyyMM");
            //barSectionID.EditValue = null;

            Load_Sections();
            barSectionID_EditValueChanged(null, null);
            prodmonth = dd.ToString("yyyyMM");
        }

        private void rpProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            Visibles();
            //barSectionID.EditValue = null;
            Load_Sections();
        }

        private void Load_Sections()
        {
            dtBookSection = _clsBookingsABS.get_UserBookSection(_prodmonth, UserCurrentInfo.UserID);
            if (dtBookSection.Rows.Count > 0)
            {
                rpSectionID.DataSource = dtBookSection;
                rpSectionID.ValueMember = "SectionID";
                rpSectionID.DisplayMember = "Name";
            }           
        }

        private void barBookDate_EditValueChanged(object sender, EventArgs e)
        {
            tabDetail.Visible = false;
            tabType.BringToFront();
            _bookdate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(barBookDate.EditValue.ToString()));
            if (barSectionID.EditValue != null)
            {
                if (barSectionID.EditValue.ToString() != "")
                {

                    dtProdmonth = _clsBookingsABS.GetProdmonth(barSectionID.EditValue.ToString(), barBookDate.EditValue.ToString());

                    if (dtProdmonth.Rows.Count > 0)
                    {
                        _prodmonth = dtProdmonth.Rows[0]["Prodmonth"].ToString();                       
                        //_rundate = Convert.ToDateTime(dtProdmonth.Rows[0]["RunDate"].ToString());
                        barProdMonth.EditValue = TProductionGlobal.ProdMonthAsDate(_prodmonth); //getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
                        barBookDate.EditValue = _bookdate;
                    }
                    
                }


            }
        }

		private void gvDetailStoping_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
			var thecolor = gvDetailStoping.GetRowCellDisplayText(e.RowHandle, "ABSCode");
            if (e.Column.FieldName == "ABSCodeDisplay")
			{
                if (thecolor == "Safe")
				{
					e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorA));
				}
				else if (thecolor == "UnSafe")
				{
					e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorB));
				}
				else if (thecolor == "No Vis.")
				{
					e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorS));
				}
                else
				{
					e.Appearance.BackColor = Color.White;
				}
			}
			if (string.Format("{0:ddd}", DateTime.Now) == _checkmeas)
            {
                if (Convert.ToDecimal(CurrShftLbl) > 4)
                {
                    lblMeas = "Check Measurement Day";

                    if (_checkmeaslvl == "MO")
					{
                        lblMeas = "Check Measurement Day Mineoverseer";
					}

                    //ShiftLbl.Visible = true;
                    if (lblMeas == "Check Measurement Day")
                    {
                        //maak checksqm en mofc visible
                    }
                }
            }
        }

        private void gvDetailStoping_MouseUp(object sender, MouseEventArgs e)
        {
            BandedGridView view;
            view = sender as BandedGridView;
			var row = view.GetRow(view.FocusedRowHandle); // get the current row 

			if (_allowbook)
            {
                if (view.FocusedColumn.FieldName == "BookCodeStp")
                {
                    if (e.Button == MouseButtons.Right)
					{
                        popupProblem.ShowPopup(MousePosition);
                }
				}
                if (view.FocusedColumn.FieldName == "ABSCodeDisplay")
                {
                    if (e.Button == MouseButtons.Right)
					{
                        popupABS.ShowPopup(MousePosition);
					}
                    //popupABSCode.ShowPopup(MousePosition);
                }
            }
        }

		private void gvStoping_CustomDrawCell(object sender, PivotCustomDrawCellEventArgs e)
        {
			if (_foundStp)
            {
                e.Appearance.BackColor = Color.White;
                try
                {
                    if (gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTType).ToString() != null)
                    {
                        if (gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTType).ToString() == "Book")
                        {
							int DayPlan = 0, QualityBlast = 0;
                            decimal BookValue = 0;

                            DateTime Thedate;

                            TheSelectedSection = gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTName).ToString();
                            TheSelectedWorkpalce = gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTWorkplace).ToString();
                            TheSelectedActivity = gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTActivity).ToString();
                            TheSelectedDate = gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTCalendarDate).ToString();
                            Thedate = Convert.ToDateTime(TheSelectedDate);

							var EditedRow = dtWPStp.Select("Name = '" + TheSelectedSection + "' "
                                                                   + "and Workplace = '" + TheSelectedWorkpalce + "' "
                                                                   + "and Activity = '" + TheSelectedActivity + "' "
                                                                   + "and CalendarDate = '" + TheSelectedDate + "' and Type = 'Book'");
                            theBook = 0;
							foreach (var dr in EditedRow)
                            {
                                BookValue = Convert.ToDecimal(dr["theValue"].ToString());
                                TheEditWorkingDay = dr["WorkingDay"].ToString();
                                TheEditShiftDay = dr["ShiftDay"].ToString();
                                // QualityBlast = Convert.ToInt32(dr["Quality_Blast"].ToString());
                                TheEditProblemID = dr["ProblemID"].ToString();
                                TheEditABSCode = dr["ABSCode"].ToString();
                                if (dr["Type"].ToString() == "Book")
								{
                                    theBook = theBook + Convert.ToDecimal(dr["theValue"].ToString());
                            }
							}
							if (TheEditProblemID != "" && Thedate.DayOfWeek != DayOfWeek.Sunday)
							{
                                e.Appearance.BackColor = Color.Orange;
							}
                            

                            //if (Thedate.DayOfWeek == System.DayOfWeek.Sunday)
                            //    e.Appearance.BackColor = Color.LightGray;


                            if (btnABS.Caption == "Remove ABS Colours")
                            {
                                if (TheEditABSCode == "A")
								{
                                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorA));
								}
								else if (TheEditABSCode == "B")
								{
                                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorB));
								}
								else if (TheEditABSCode == "S")
								{
                                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorS));
                            }
							}
                            if (Thedate.ToString("ddd") == _checkmeas)
                            {
                                if (TheEditWorkingDay != "N")
                                {
									if (TheEditShiftDay != "N")
									{
										if (Convert.ToInt32(TheEditShiftDay) > 7)
										{
                                            e.Appearance.BackColor = Color.SkyBlue;
                                }
                            }
								}
							}
                            if (TheEditWorkingDay == "N")
                            {
                                e.Appearance.BackColor = Color.Gainsboro;
                                //    // puma hola
                                //    if (TheEditPumahola == "Y")
                                //        e.Appearance.BackColor = Color.Gold;
                            }
                            //if (TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).NewProblemValidation == "Y")
                            //{
                            //    if (QualityBlast > BookValue && ProblemID == "" && Thedate.DayOfWeek != System.DayOfWeek.Sunday)
                            //        e.Appearance.BackColor = Color.Red;
                            //}
                            //else
                            //{
                            //    if (BookValue < DayPlan && ProblemID == "" && Thedate.DayOfWeek != System.DayOfWeek.Sunday)
                            //        e.Appearance.BackColor = Color.Red;
                            //}
                        }

                        if (gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTType).ToString() == "Plan")
                        {
							var WorkingDay = "N";
                            DateTime Thedate;

                            e.Appearance.BackColor = Color.White;

                            TheSelectedSection = gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTName).ToString();
                            TheSelectedWorkpalce = gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTWorkplace).ToString();
                            TheSelectedActivity = gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTActivity).ToString();
                            TheSelectedDate = gvStoping.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_StpDTCalendarDate).ToString();
                            Thedate = Convert.ToDateTime(TheSelectedDate);

							var EditedRow = dtWPStp.Select("Name = '" + TheSelectedSection + "' "
                                                                   + "and Workplace = '" + TheSelectedWorkpalce + "' "
                                                                   + "and Activity = '" + TheSelectedActivity + "' "
                                                                   + "and CalendarDate = '" + TheSelectedDate + "' and Type = 'Plan'");
							foreach (var dr in EditedRow)
                            {
                                TheEditWorkingDay = dr["WorkingDay"].ToString();
                                TheEditShiftDay = dr["ShiftDay"].ToString();
                            }

                            if (Thedate.ToString("ddd") == _checkmeas)
                            {
                                if (TheEditWorkingDay != "N")
                                {
									if (TheEditShiftDay != "N")
									{
										if (Convert.ToInt32(TheEditShiftDay) > 7)
										{
                                            e.Appearance.BackColor = Color.SkyBlue;
                                }
                            }
								}
							}
                            if (TheEditWorkingDay == "N")
							{
                                e.Appearance.BackColor = Color.Gainsboro;
                        }
                    }
                }
				}
                catch (Exception _exception)
				{ }
            }
        }

		private void gvDevelopment_CustomDrawCell(object sender, PivotCustomDrawCellEventArgs e)
        {
			if (_foundDev)
            {
                e.Appearance.BackColor = Color.White;
                try
                {
                    if (gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTType).ToString() != null)
                    {
                        if (gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTType).ToString() == "Book")
                        {
							int DayPlan = 0, QualityBlast = 0;
                            decimal BookValue = 0;

                            DateTime Thedate;

                            TheSelectedSection = gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTName).ToString();
                            TheSelectedWorkpalce = gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTWorkplace).ToString();
                            TheSelectedActivity = gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTActivity).ToString();
                            TheSelectedDate = gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTCalendarDate).ToString();
                            Thedate = Convert.ToDateTime(TheSelectedDate);

							var EditedRow = dtWPDev.Select("Name = '" + TheSelectedSection + "' "
                                                                   + "and Workplace = '" + TheSelectedWorkpalce + "' "
                                                                   + "and Activity = '" + TheSelectedActivity + "' "
                                                                   + "and CalendarDate = '" + TheSelectedDate + "' and Type = 'Book'");
                            theBook = 0;
							foreach (var dr in EditedRow)
                            {
                                BookValue = Convert.ToDecimal(dr["theValue"].ToString());
                                TheEditWorkingDay = dr["WorkingDay"].ToString();
                                TheEditShiftDay = dr["ShiftDay"].ToString();
                                // QualityBlast = Convert.ToInt32(dr["Quality_Blast"].ToString());
                                TheEditProblemID = dr["ProblemID"].ToString();
                                TheEditABSCode = dr["ABSCode"].ToString();
                                if (dr["Type"].ToString() == "Book")
								{
                                    theBook = theBook + Convert.ToDecimal(dr["theValue"].ToString());
                            }
							}
							if (TheEditProblemID != "" && Thedate.DayOfWeek != DayOfWeek.Sunday)
							{
                                e.Appearance.BackColor = Color.Orange;
							}

                            //if (Thedate.DayOfWeek == System.DayOfWeek.Sunday)
                            //    e.Appearance.BackColor = Color.LightGray;


                            if (btnABS.Caption == "Remove ABS Colours")
                            {
                                if (TheEditABSCode == "A")
								{
                                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorA));
								}
								else if (TheEditABSCode == "B")
								{
                                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorB));
								}
								else if (TheEditABSCode == "S")
								{
                                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorS));
                            }
							}
                            //if (Thedate.ToString("ddd") == _checkmeas)
                            //{
                            //    if (TheEditWorkingDay != "N")
                            //    {
                            //        if (TheEditShiftDay.ToString() != "N")
                            //            if (Convert.ToInt32(TheEditShiftDay.ToString()) > 7)
                            //                e.Appearance.BackColor = Color.SkyBlue;
                            //    }
                            //}
                            if (TheEditWorkingDay == "N")
                            {
                                e.Appearance.BackColor = Color.Gainsboro;
                                //    // puma hola
                                //    if (TheEditPumahola == "Y")
                                //        e.Appearance.BackColor = Color.Gold;
                            }
                            //if (TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).NewProblemValidation == "Y")
                            //{
                            //    if (QualityBlast > BookValue && ProblemID == "" && Thedate.DayOfWeek != System.DayOfWeek.Sunday)
                            //        e.Appearance.BackColor = Color.Red;
                            //}
                            //else
                            //{
                            //    if (BookValue < DayPlan && ProblemID == "" && Thedate.DayOfWeek != System.DayOfWeek.Sunday)
                            //        e.Appearance.BackColor = Color.Red;
                            //}
                        }

                        if (gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTType).ToString() == "Plan")
                        {
							var WorkingDay = "N";
                            DateTime Thedate;

                            e.Appearance.BackColor = Color.White;

                            TheSelectedSection = gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTName).ToString();                
                            TheSelectedWorkpalce = gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTWorkplace).ToString();
                            TheSelectedActivity = gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTActivity).ToString();
                            TheSelectedDate = gvDevelopment.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_DevDTCalendarDate).ToString();
                            Thedate = Convert.ToDateTime(TheSelectedDate);

							var EditedRow = dtWPDev.Select("Name = '" + TheSelectedSection + "' "
                                                                   + "and Workplace = '" + TheSelectedWorkpalce + "' "
                                                                   + "and Activity = '" + TheSelectedActivity + "' "
                                                                   + "and CalendarDate = '" + TheSelectedDate + "' and Type = 'Plan'");
							foreach (var dr in EditedRow)
                            {
                                TheEditWorkingDay = dr["WorkingDay"].ToString();
                                TheEditShiftDay = dr["ShiftDay"].ToString();
                            }

                            //if (Thedate.ToString("ddd") == _checkmeas)
                            //{
                            //    if (TheEditWorkingDay != "N")
                            //    {
                            //        if (TheEditShiftDay.ToString() != "N")
                            //            if (Convert.ToInt32(TheEditShiftDay.ToString()) > 7)
                            //                e.Appearance.BackColor = Color.SkyBlue;
                            //    }
                            //}
                            if (TheEditWorkingDay == "N")
                            {
                                //if (TheSelectedWorkpalce == "RE009933 : 192 N7 BH+ 3")
                                //    MessageBox.Show(TheEditWorkingDay);
                                e.Appearance.BackColor = Color.Gainsboro;
                            }
                        }
                    }
                }
                catch (Exception _exception)
				{ }
            }
        }

		private void gvDetailDevelopment_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            BandedGridView view;
            view = sender as BandedGridView;
			var row = view.GetRow(view.FocusedRowHandle); // get the current row 

			var selectedBookCode = (row as DataRowView)["BookCodeDev"].ToString();
			var selectedWP = (row as DataRowView)["WP"].ToString();
			var selectedProblemID = (row as DataRowView)["ProblemID"].ToString();
			var selectedSBossNote = (row as DataRowView)["SBossNotes"].ToString();
			var selectedSectionID = (row as DataRowView)["SectionID"].ToString();
			var selectedActivity = (row as DataRowView)["Activity"].ToString();
			var selectedPegID = (row as DataRowView)["PegID"].ToString();

            btnSave.Enabled = true;

            if (view.FocusedColumn.FieldName == "BookCodeDev")
            {
                if (selectedBookCode == "PR")
                {
					var ProbBook = new ucBookingsABSProblems();
                    ProbBook.theConnection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    ProbBook.TheSection = selectedSectionID;
                    ProbBook.TheWorkpalce = selectedWP;
					ProbBook.TheActivity = Convert.ToInt32(selectedActivity);
                    ProbBook.TheDate = DateTime.Now;

                    ProbBook.ShowDialog();

                    //Update The Master Data
                    if (ProbBook.ProblemID != "") // Update The data to selected problem
                    {
                        if (dtDetailDevelopment != null)
                        {
							var _lost = "";
							dtCausedLostBlast = _clsBookingsABS.get_CausedLostBlast(TheSelectedActivity, ProbBook.ProblemID);
                            if (dtCausedLostBlast.Rows.Count > 0)
							{
                                _lost = dtCausedLostBlast.Rows[0]["CausedLostBlast"].ToString();
							}

							var EditedRow = dtDetailDevelopment.Select(
                                            " ProdMonth = '" + _prodmonth + "' "
                                            + "and SectionID = '" + ProbBook.TheSection + "' "
                                            + "and WP = '" + ProbBook.TheWorkpalce + "' "
                                            + "and CalendarDate = '" + _bookdate + "'"
                                            + "and Activity = '" + ProbBook.TheActivity + "' ");
							foreach (var dr in EditedRow)
                            {
                                dr["ProblemID"] = ProbBook.ProblemID;
                                dr["SBossNotes"] = ProbBook.SBossNotes;
                                dr["CausedLostBlast"] = _lost;
                            }
                            dtDetailDevelopment.AcceptChanges();
                            btnSave.Enabled = true;
                            btnAddBooking.Enabled = false;
                        }
                    }
                }
            }
            if (view.FocusedColumn.FieldName == "PegID")
            {
				var _newValue = Convert.ToDecimal(TProductionGlobal.ExtractAfterColon(selectedPegID));
				var _oldValue = Convert.ToDecimal(TProductionGlobal.ExtractAfterColon((row as DataRowView)["ThePPegID"].ToString()));
				var _oldToFace = Convert.ToDecimal(TProductionGlobal.ExtractAfterColon((row as DataRowView)["ThePPegToFace"].ToString()));
               // decimal _aa = Convert.ToDecimal((row as DataRowView)["ThePPegDist"].ToString());
				(row as DataRowView)["PegToFace"] = Math.Round(_oldValue + _oldToFace - _newValue, 1);
				(row as DataRowView)["ThePegToFace"] = Math.Round(_oldValue + _oldToFace - _newValue, 1);
				(row as DataRowView)["PegFrom"] = Math.Round(_oldValue + _oldToFace - _newValue, 1);
                //(row as DataRowView)["PegTo"] = Math.Round((_oldValue + _aa) - _newValue,1);
                view.UpdateCurrentRow();
                dtDetailDevelopment.AcceptChanges();
            }
            if (view.FocusedColumn.FieldName == "PegToFace")
            {
				var selectedVal = (row as DataRowView)["PegToFace"].ToString();
                if (selectedVal == "")
                {
                    (row as DataRowView)["PegToFace"] = "0.0";
                    dtDetailDevelopment.AcceptChanges();
                }
                (row as DataRowView)["PegTo"] = Convert.ToDecimal((row as DataRowView)["PegToFace"].ToString());

                dtDetailDevelopment.AcceptChanges();
				var _adv = Convert.ToDecimal((row as DataRowView)["PegTo"].ToString()) -
                               Convert.ToDecimal((row as DataRowView)["PegFrom"].ToString());
                Do_CalcsDev(
                         _adv,
                         Convert.ToDecimal((row as DataRowView)["DHeight"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["DWidth"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["cmgt"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["Density"].ToString()),
                        (row as DataRowView)["ReefWaste"].ToString());
                (row as DataRowView)["BookTons"] = _calctons;
                (row as DataRowView)["BookReefTons"] = _calctonsreef;
                (row as DataRowView)["BookWasteTons"] = _calctonswaste;
                (row as DataRowView)["BookGrams"] = _calcgrams;
                (row as DataRowView)["BookKG"] = _calckg;
                (row as DataRowView)["BookAdv"] = _calcadv;
                (row as DataRowView)["BookReefAdv"] = _calcadvreef;
                (row as DataRowView)["BookWasteAdv"] = _calcadvwaste;

                if (_calcadv > 0)
				{
                    if (selectedProblemID == "")
					{
                        (row as DataRowView)["BookCodeDev"] = "BL";
					}
                    else
					{
                        (row as DataRowView)["BookCodeDev"] = "PR";
					}
				}

                view.UpdateCurrentRow();
                dtDetailDevelopment.AcceptChanges();
                btnSave.Enabled = true;
            }
            if (view.FocusedColumn.FieldName == "BookAdv")
            {
				var selectedVal = (row as DataRowView)["BookAdv"].ToString();
                if (selectedVal == "")
                {
                    (row as DataRowView)["BookAdv"] = "0.0";
                    dtDetailDevelopment.AcceptChanges();
                }
                Do_CalcsDev(
                         Convert.ToDecimal((row as DataRowView)["BookAdv"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["DHeight"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["DWidth"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["cmgt"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["Density"].ToString()),
                        (row as DataRowView)["ReefWaste"].ToString());

                (row as DataRowView)["BookTons"] = _calctons;
                (row as DataRowView)["BookReefTons"] = _calctonsreef;
                (row as DataRowView)["BookWasteTons"] = _calctonswaste;
                (row as DataRowView)["BookGrams"] = _calcgrams;
                (row as DataRowView)["BookKG"] = _calckg;
                (row as DataRowView)["BookAdv"] = _calcadv;
                (row as DataRowView)["BookReefAdv"] = _calcadvreef;
                (row as DataRowView)["BookWasteAdv"] = _calcadvwaste;

                
                view.UpdateCurrentRow();
                dtDetailDevelopment.AcceptChanges();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
            if (view.FocusedColumn.FieldName == "BookReefAdv")
            {
				var selectedVal = (row as DataRowView)["BookReefAdv"].ToString();
                if (selectedVal == "")
                {
                    (row as DataRowView)["BookReefAdv"] = "0.0";
                    dtDetailDevelopment.AcceptChanges();
                }
                Do_CalcsDev1(
                         Convert.ToDecimal((row as DataRowView)["BookAdv"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookReefAdv"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["DHeight"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["DWidth"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["cmgt"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["Density"].ToString()));
                (row as DataRowView)["BookTons"] = _calctons;
                (row as DataRowView)["BookReefTons"] = _calctonsreef;
                (row as DataRowView)["BookWasteTons"] = _calctonswaste;
                (row as DataRowView)["BookGrams"] = _calcgrams;
                (row as DataRowView)["BookKG"] = _calckg;
                (row as DataRowView)["BookAdv"] = _calcadv;
                (row as DataRowView)["BookReefAdv"] = _calcadvreef;
                (row as DataRowView)["BookWasteAdv"] = _calcadvwaste;

                if (_calcadv > 0)
				{
                    if (selectedProblemID == "")
					{
                        (row as DataRowView)["BookCodeDev"] = "BL";
					}
                    else
					{
                        (row as DataRowView)["BookCodeDev"] = "PR";
					}
				}
                view.UpdateCurrentRow();
                dtDetailDevelopment.AcceptChanges();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
            if (view.FocusedColumn.FieldName == "BookWasteAdv")
            {
				var selectedVal = (row as DataRowView)["BookWasteAdv"].ToString();
                if (selectedVal == "")
                {
                    (row as DataRowView)["BookWasteAdv"] = "0.0";
                    dtDetailDevelopment.AcceptChanges();
                }
                Do_CalcsDev2(
                         Convert.ToDecimal((row as DataRowView)["BookAdv"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookWasteAdv"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["DHeight"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["DWidth"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["cmgt"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["Density"].ToString()));
                (row as DataRowView)["BookTons"] = _calctons;
                (row as DataRowView)["BookReefTons"] = _calctonsreef;
                (row as DataRowView)["BookWasteTons"] = _calctonswaste;
                (row as DataRowView)["BookGrams"] = _calcgrams;
                (row as DataRowView)["BookKG"] = _calckg;
                (row as DataRowView)["BookAdv"] = _calcadv;
                (row as DataRowView)["BookReefAdv"] = _calcadvreef;
                (row as DataRowView)["BookWasteAdv"] = _calcadvwaste;

                if (_calcadv > 0)
				{
                    if (selectedProblemID == "")
					{
                        (row as DataRowView)["BookCodeDev"] = "BL";
					}
                    else
					{
                        (row as DataRowView)["BookCodeDev"] = "PR";
					}
				}
                view.UpdateCurrentRow();
                dtDetailDevelopment.AcceptChanges();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
            if (view.FocusedColumn.FieldName == "BookCubicMetres")
            {
				var selectedVal = (row as DataRowView)["BookCubicMetres"].ToString();
                if (selectedVal == "")
                {
                    (row as DataRowView)["BookCubicMetres"] = "0";
                    dtDetailDevelopment.AcceptChanges();
                }
                Do_CalcCubics(Convert.ToInt32((row as DataRowView)["BookCubicMetres"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["BookCubicGT"].ToString()),
                         Convert.ToDecimal((row as DataRowView)["Density"].ToString()));
                (row as DataRowView)["BookCubicMetres"] = _calccubics;
                (row as DataRowView)["BookCubicTons"] = _calccubictons;
                (row as DataRowView)["BookCubicGrams"] = _calccubicgrams;
                (row as DataRowView)["BookCubicKG"] = _calccubickg;
                if (_calccubics > 0)
				{
                    if (selectedProblemID == "")
					{
                        (row as DataRowView)["BookCodeDev"] = "BL";
					}
                    else
					{
                        (row as DataRowView)["BookCodeDev"] = "PR";
					}
				}
                view.UpdateCurrentRow();
                dtDetailDevelopment.AcceptChanges();
                btnSave.Enabled = true;
                btnAddBooking.Enabled = false;
            }
        }

		private void gvDetailDevelopment_ShowingEditor(object sender, CancelEventArgs e)
        {
            BandedGridView view;
            view = sender as BandedGridView;

			if (_allowbook == false)
			{
                e.Cancel = true;
			}

            if (view.FocusedColumn.FieldName == "PegID")
            {
                _clsBookingsABS.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            }
            if ((view.FocusedColumn.FieldName == "PegID") |
                (view.FocusedColumn.FieldName == "PegToFace") |
                (view.FocusedColumn.FieldName == "BookAdv") |
                (view.FocusedColumn.FieldName == "BookReefAdv") |
                (view.FocusedColumn.FieldName == "BookWasteAdv") |
                (view.FocusedColumn.FieldName == "BookCodeDev") |
                (view.FocusedColumn.FieldName == "BookCubicMetres"))
            {
				var cellValue = view.GetRowCellValue(view.FocusedRowHandle, col_DevABSCodeDisplay).ToString();
                if (cellValue == "")
                {
                    e.Cancel = true;
                }
            }
            if ((view.FocusedColumn.FieldName == "BookReefAdv") |
                (view.FocusedColumn.FieldName == "BookWasteAdv"))
            {
				var celValue1 = view.GetRowCellValue(view.FocusedRowHandle, col_DevRecAdv).ToString();
                if (celValue1 == "Y")
                {
					var cellValue = view.GetRowCellValue(view.FocusedRowHandle, col_DevBookAdv).ToString();
                    if ((cellValue == "") | (cellValue == "0.0"))
					{
                        e.Cancel = true;
                }
            }
			}

            if (view.FocusedColumn.FieldName == "BookCubicMetres")
            {
				var celValue1 = view.GetRowCellValue(view.FocusedRowHandle, col_DevRecCubics).ToString();
                if (celValue1 == "N")
                {
                        e.Cancel = true;
                }
            }
        }

		private void gvDetailDevelopment_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
			var thecolor = gvDetailDevelopment.GetRowCellDisplayText(e.RowHandle, "ABSCode");
            if (e.Column.FieldName == "ABSCodeDisplay")
			{
                if (thecolor == "Safe")
                {
                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorA));
                }
                else if (thecolor == "UnSafe")
                {
                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorB));
                }
                else if (thecolor == "No Vis.")
                {
                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(_colorS));
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                }
            }
        }

		private void gvDetailDevelopment_RowClick(object sender, RowClickEventArgs e)
        {
			GridView view;
			view = sender as GridView;

			var row = view.GetRow(e.RowHandle);

            if (_allowbook == false)
			{
                return;
			}

            _clickStp = false;
            _clickDev = true;
            _DevSectionID = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["SectionID"]) != null)
            {
                var SectionID = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["SectionID"]);
                _DevSectionID = SectionID.ToString();
            }
            _DevWPID = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["WPID"]) != null)
            {
                var WPID = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["WPID"]);
                _DevWPID = WPID.ToString();
            }
            _DevActivity = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["Activity"]) != null)
            {
                var Activity = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["Activity"]);
                _DevActivity = Activity.ToString();
            }
            _DevActDesc = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ActDesc"]) != null)
            {
                var ActDesc = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ActDesc"]);
                _DevActDesc = ActDesc.ToString();
            }
            _DevBookAdv = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["BookAdv"]) != null)
            {
                var BookAdv = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["BookAdv"]);
                _DevBookAdv = BookAdv.ToString();
            }
            _DevABSCode = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ABSCode"]) != null)
            {
                var AbsCode = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ABSCode"]);
                _DevABSCode = AbsCode.ToString();
            }
            _DevABSCodeDisplay = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ABSCodeDisplay"]) != null)
            {
                var ABSCodeDisplay = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ABSCodeDisplay"]);
                _DevABSCodeDisplay = ABSCodeDisplay.ToString();
            }
            _DevABSNote = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ABSNote"]) != null)
            {
                var ABSNote = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ABSNote"]);
                _DevABSNote = ABSNote.ToString();
            }
            _DevABSPicNo = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ABSPicNo"]) != null)
            {
                var ABSPicNo = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ABSPicNo"]);
                _DevABSPicNo = ABSPicNo.ToString();
            }
            _DevABSPrec = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ABSPrec"]) != null)
            {
                var ABSPrec = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["ABSPrec"]);
                _DevABSPrec = ABSPrec.ToString();
            }
            _DevWP = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["WP"]) != null)
            {
                var WP = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["WP"]);
                _DevWP = WP.ToString();
            }
            _DevBookCode = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["BookCodeDev"]) != null)
            {
                var BookCode = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["BookCodeDev"]);
                _DevBookCode = BookCode.ToString();
            }
            _DevProblemID = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["DevProblemID"]) != null)
            {
                var DevProblemID = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["DevProblemID"]);
                _DevProblemID = DevProblemID.ToString();
            }
            _DevSBossNotes = "";
            if (gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["SBossNotes"]) != null)
            {
                var SBossNotes = gvDetailDevelopment.GetRowCellValue(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.Columns["SBossNotes"]);
                _DevSBossNotes = SBossNotes.ToString();
            }

			if (_DevABSCodeDisplay != "aaaa" && gvDetailDevelopment.FocusedColumn.FieldName == "ABSCodeDisplay") //)
            {
                var ABSChoice = new ucBookingsABSChoice();
                //ABSChoice.UserCurrentInfo = UserCurrentInfo;
                ABSChoice.MoSecLbl.Text = _DevSectionID;
                ABSChoice.TheWorkpalce = _DevWP;
                ABSChoice.ActLbl.Text = _DevActivity;
                ABSChoice.lblWorkplaceid.Text = _DevWPID;
                ABSChoice.TheDate = DateTime.Now;
                ABSChoice.OtherdateTimePicker1.Value = Convert.ToDateTime(barBookDate.EditValue);
                ABSChoice.lblStatus.Text = _DevABSCode;
                ABSChoice.lblABSNotes.Text = _DevABSNote;
                ABSChoice.TheColorA = _colorA;
                ABSChoice.TheColorB = _colorB;
                ABSChoice.TheColorS = _colorS;

                //theSystemDBTag, UserCurrentInfo.Connection

                ABSChoice._theSystemDBTag = theSystemDBTag;
                ABSChoice._UserCurrentInfoConnection = UserCurrentInfo.Connection;

                ABSChoice.StartPosition = FormStartPosition.CenterScreen;




                ABSChoice.ShowDialog();


                if (dtDetailDevelopment != null)
                {
                    var EditedRow = dtDetailDevelopment.Select(
                                               " ProdMonth = '" + _prodmonth + "' "
                                               + " and SectionID = '" + ABSChoice.TheSection + "' "
                                               + "and WP = '" + ABSChoice.TheWorkpalce + "' "
                                               + "and CalendarDate = '" + _bookdate + "'"
                                               + "and Activity = '" + ABSChoice.TheActivity + "' ");
                    foreach (var dr in EditedRow)
                    {
                        dr["ABSCodeDisplay"] = ABSChoice.TheABSCode;
                        dr["ABSCode"] = ABSChoice.TheABSCode;
                        dr["ABSPicNo"] = ""; 
                        dr["ABSNotes"] = ABSChoice.ABSNotestxt.Text; 
                        dr["ABSPrec"] = "";
                        _DevABSCode = ABSChoice.TheABSCode;
                        _DevABSCodeDisplay = ABSChoice.TheABSCode;
                        _DevABSPicNo = "";
                        _DevABSNote = ABSChoice.ABSNotestxt.Text;
                        _DevABSPrec = "";
                    }
                    dtDetailDevelopment.AcceptChanges();
                    btnSave.Enabled = true;
                    btnAddBooking.Enabled = false;
                }

                //Update The Master Data
     //           if (ABSChoice.TheABSCode == "A") // Update The data to selected problem
     //           {
     //               if (dtDetailDevelopment != null)
     //               {
					//	var EditedRow = dtDetailDevelopment.Select(
     //                               " ProdMonth = '" + _prodmonth + "' "
     //                               + "and SectionID = '" + ABSChoice.TheSection + "' "
     //                               + "and WP = '" + ABSChoice.TheWorkpalce + "' "
     //                               + "and CalendarDate = '" + _bookdate + "'"
     //                               + "and Activity = '" + ABSChoice.TheActivity + "' ");
					//	foreach (var dr in EditedRow)
     //                   {
     //                       dr["ABSCodeDisplay"] = ABSChoice.TheABSCode;
     //                       dr["ABSCode"] = ABSChoice.TheABSCode;
     //                       dr["ABSPicNo"] = "";
     //                       dr["ABSNotes"] = "";
     //                       dr["ABSPrec"] = "";
     //                       _DevABSCode = ABSChoice.TheABSCode;
     //                       _DevABSCodeDisplay = ABSChoice.TheABSCode;
     //                       _DevABSPicNo = "";
     //                       _DevABSNote = "";
     //                       _DevABSPrec = "";
     //                   }
     //                   dtDetailDevelopment.AcceptChanges();
     //                   btnSave.Enabled = true;
     //                   btnAddBooking.Enabled = false;
     //               }
     //           }
     //           _DevABSCode = ABSChoice.TheABSCode;
     //           _DevABSCodeDisplay = ABSChoice.TheABSCode;

     //           if ((_DevABSCode == "B") |
     //               (_DevABSCode == "S"))
     //           {
					//var ABS = new ucBookingsABSForm1();
     //               ABS.TheSection = _DevSectionID;
     //               ABS.TheWorkpalce = _DevWP;
     //               ABS.TheActivity = Convert.ToInt32(_DevActivity);
     //               ABS.TheDate = DateTime.Now;
     //               ABS.TheABSPicNo = _DevABSPicNo;
     //               ABS.TheABSNotes = _DevABSNote;
     //               ABS.TheABSCodeDisplay = _DevABSCodeDisplay;
     //               ABS.TheABSCode = _DevABSCode;
     //               ABS.TheABSPrec = _DevABSPrec;

     //               ABS.ShowDialog();

     //               if (ABS.TheABSCode != "") // Update The data to selected problem
     //               {
     //                   if (dtDetailDevelopment != null)
     //                   {
					//		var EditedRow = dtDetailDevelopment.Select(
     //                                   " ProdMonth = '" + _prodmonth + "' "
     //                                   + "and SectionID = '" + ABSChoice.TheSection + "' "
     //                                   + "and WP = '" + ABSChoice.TheWorkpalce + "' "
     //                                   + "and CalendarDate = '" + _bookdate + "'"
     //                                   + "and Activity = '" + ABSChoice.TheActivity + "' ");
					//		foreach (var dr in EditedRow)
     //                       {
     //                           dr["ABSCodeDisplay"] = ABS.TheABSCodeDisplay;
     //                           dr["ABSCode"] = ABS.TheABSCode;
     //                           dr["ABSPicNo"] = ABS.TheABSPicNo;
     //                           dr["ABSNotes"] = ABS.TheABSNotes;
     //                           dr["ABSPrec"] = ABS.TheABSPrec;
     //                       }
     //                       dtDetailDevelopment.AcceptChanges();
     //                       btnSave.Enabled = true;
     //                       btnAddBooking.Enabled = false;
     //                   }
     //               }
     //           }
                //Load_Pegs();
                //gvDetailDevelopment.Columns["PegID"].FilterInfo = new ColumnFilterInfo("[WPID] = '" + _DevWPID + "' ");
            }
        }

        private void Load_Pegs()
        {
            _clsBookingsABS.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
           
            dtPegs = _clsBookingsABS.get_Pegs(barSectionID.EditValue.ToString(), _DevWPID, _bookdate);
            
            rpDevPegID.DataSource = dtPegs;
            rpDevPegID.ValueMember = "PegID";
            rpDevPegID.DisplayMember = "PegID";
        }

        private void gvDetailDevelopment_MouseUp(object sender, MouseEventArgs e)
        {
            BandedGridView view;
            view = sender as BandedGridView;
			var row = view.GetRow(view.FocusedRowHandle); // get the current row 

			if (_allowbook)
            {
                if (view.FocusedColumn.FieldName == "BookCodeDev")
                {
                    if (e.Button == MouseButtons.Right)
					{
                        popupProblem.ShowPopup(MousePosition);
                }
				}
                if (view.FocusedColumn.FieldName == "ABSCodeDisplay")
                {
                    if (e.Button == MouseButtons.Right)
					{
                        popupABS.ShowPopup(MousePosition);
                }
            }
			}
            //BandedGridView view;
            //view = sender as BandedGridView;
            //object row = view.GetRow(view.FocusedRowHandle); // get the current row 

          //  BandedGridView Pivot = sender as BandedGridView;
            //PivotGridHitInfo hInfo = Pivot.CalcHitInfo(e.Location);

            //GridViewHitInfo
            ////GridViewInfo info = gvDetailDevelopment.GetViewInfo() as GridViewInfo;
            ////GridCellInfo cellInfo = info.GetGridCellInfo(gvDetailDevelopment.FocusedRowHandle, gvDetailDevelopment.FocusedColumn);

            //if (info == null || cellInfo == null)
            //{
            //    return;
            //}

            ////if (view.ColumnIndex >= 0)
            ////{
            //    if (e.Button == MouseButtons.Right)
            //    {
            //        if (view.GetFieldValue(pStopeType, hInfo.CellInfo.RowIndex).ToString() != "Plan")
            //            if (Pivot.GetFieldValue(pStopeType, hInfo.CellInfo.RowIndex).ToString() == "FL")
            //            {
            //                btnMiningDirect.Enabled = true;
            //                popupMenu1.ShowPopup(MousePosition);
            //            }
            //            else
            //            {
            //                btnMiningDirect.Enabled = false;
            //                popupMenu1.ShowPopup(MousePosition);
            //            }
            //    }
            ////}
        }

        private void Do_CalcsDev(decimal _theAdv,
                                 decimal _theHeight, decimal _theWidth, decimal _thegt,
                                 decimal _theDens, string _reefwaste)
        {
            _calctons = 0;
            _calctonsreef = 0;
            _calctonswaste = 0;
            _calcgrams = 0;
            _calckg = 0;
            _calcadv = 0;
            _calcadvreef = 0;
            _calcadvwaste = 0;

            _calcadv = _theAdv;
            _calctons = _calcadv * _theHeight * _theWidth * _theDens;
            if (_reefwaste == "R")
            {
                _calcadvreef = _theAdv;
                _calctonsreef = _calctons;
            }
            else
            {
                _calcadvwaste = _theAdv;
                _calctonswaste = _calctons;
            }

            _calcgrams = _calcadvreef * _theWidth * _theDens * _thegt / 100;
            _calckg = _calcadvreef * _theWidth * _theDens * _thegt / 100000;
        }

        private void Do_CalcsDev1(decimal _theAdv, decimal _theReefAdv,
                                 decimal _theHeight, decimal _theWidth, decimal _thegt,
                                 decimal _theDens)
        {
            _calctons = 0;
            _calctonsreef = 0;
            _calctonswaste = 0;
            _calcgrams = 0;
            _calckg = 0;
            _calcadv = 0;
            _calcadvreef = 0;
            _calcadvwaste = 0;

            _calcadv = _theAdv;
            _calctons = _calcadv * _theHeight * _theWidth * _theDens;

            _calcadvreef = _theReefAdv;
            _calcadvwaste = _calcadv - _calcadvreef;

            _calctonsreef = _calcadvreef * _theHeight * _theWidth * _theDens;
            _calctonswaste = _calctons - _calctonsreef;

            _calcgrams = _calcadvreef * _theWidth * _theDens * _thegt / 100;
            _calckg = _calcadvreef * _theWidth * _theDens * _thegt / 100000;
        }

        private void Do_CalcsDev2(decimal _theAdv, decimal _theWasteAdv,
                                 decimal _theHeight, decimal _theWidth, decimal _thegt,
                                 decimal _theDens)
        {
            _calctons = 0;
            _calctonsreef = 0;
            _calctonswaste = 0;
            _calcgrams = 0;
            _calckg = 0;
            _calcadv = 0;
            _calcadvreef = 0;
            _calcadvwaste = 0;

            _calcadv = _theAdv;
            _calctons = _calcadv * _theHeight * _theWidth * _theDens;

            _calcadvwaste = _theWasteAdv;
            _calcadvreef = _calcadv - _calcadvwaste;

            _calctonswaste = _calcadvwaste * _theHeight * _theWidth * _theDens;
            _calctonsreef = _calctons - _calctonswaste;

            _calcgrams = _calcadvreef * _theWidth * _theDens * _thegt / 100;
            _calckg = _calcadvreef * _theWidth * _theDens * _thegt / 100000;
        }

        private void rpBookCodeDev_EditValueChanged(object sender, EventArgs e)
        {
            gvDetailDevelopment.PostEditor();
        }

        private void rpBookCodeStp_EditValueChanged(object sender, EventArgs e)
        {
            gvDetailStoping.PostEditor();
        }

        private void gvDetailDevelopment_ShownEditor(object sender, EventArgs e)
        {
			GridView view;
			view = sender as GridView;

			if (view.FocusedColumn == col_DevPegID && view.ActiveEditor is LookUpEdit)
            {
                Text = view.ActiveEditor.Parent.Name;

				LookUpEdit edit;

				edit = (LookUpEdit) view.ActiveEditor;

				var table = edit.Properties.DataSource as DataTable;

                clone = new DataView(table);

				var row = view.GetDataRow(view.FocusedRowHandle);

				clone.RowFilter = "[WPID] = '" + row["WPID"] + "'";

                edit.Properties.DataSource = clone;
            }
        }
    }
}
