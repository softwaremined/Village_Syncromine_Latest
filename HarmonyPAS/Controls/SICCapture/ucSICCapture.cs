using System;
using System.Drawing;
using System.Data;
using Mineware.Systems.Global;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraEditors;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.SICCapture
{
    public partial class ucSICCapture : ucBaseUserControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        clsSICCapture _clsSICCapture = new clsSICCapture();
        
        BookingsABS.ucBookingsABSProblems _clsProblem = new BookingsABS.ucBookingsABSProblems();
        TProductionGlobal.clsValidation _clsValidation = new TProductionGlobal.clsValidation();

        private DataTable dtSysset;
        private DataTable dtSections;
        private DataTable dtSIC;
        private DataTable dtSICClean;
        private DataTable dtProblemDesc;
        private DataTable dtCausedLostBlast;

        private string _yearmonth;
        private string _prodmonth;
        private string _millmonth;
        private string _prdmth;
        private string _typemonth;

        private bool _foundSIC = false;
        private bool _foundClean =  false;

        private int _rowcnt;

        public static string TheEditWorkingDay, TheEditSICKey, TheEditValue, TheEditDate, TheEditShiftNo, TheEditType;
        public static string TheEditWorkplace;
        public static string TheSelectedSICKey, TheSelectedDate, TheSelectedShiftNo, TheSelectedWorkplace, TheSelectedType;
        public static string TheSelectedActivity, TheSelectedProblemCode, TheSelectedName;
        public ucSICCapture()
        {
            InitializeComponent();
        }

        private void ucSICCapture_Load(object sender, EventArgs e)
        {
            pvgSICCapture.Visible = false;
            pvgSICCleaned.Visible = false;
            btnSave.Enabled = false;
            barKPI.EditValue = null;
            barSectionID.EditValue = null;
            _prodmonth = String.Format("{0:yyyyMM}", DateTime.Now);
            _millmonth = String.Format("{0:yyyyMM}", DateTime.Now);
            _yearmonth = String.Format("{0:yyyyMM}", DateTime.Now);
            barKPI.EditValue = "Safety Stoping";
            Load_Month();
        }
        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _rowcnt = 0;
            _foundSIC = false;
            _foundClean = false;
            if (barSectionID.EditValue != null)
            {
                if (barSectionID.EditValue.ToString() != "")
                {
                    _clsSICCapture.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    if (barKPI.EditValue.ToString() != "Cleaned")
                    {
                        dtSIC = _clsSICCapture.get_SICData(_typemonth, _prdmth, barSectionID.EditValue.ToString(), "4", barKPI.EditValue.ToString());

                        if (dtSIC.Rows.Count > 0)
                        {
                            _rowcnt = dtSIC.Rows.Count;
                            pvgSICCapture.DataSource = dtSIC;
                            pvgSICCapture.Visible = true;
                            pvgSICCapture.BringToFront();
                            _foundSIC = true;
                        }
                        else
                        {
                            MessageBox.Show("No Data for your Selection", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        dtSICClean = _clsSICCapture.get_SICData(_typemonth, _prdmth, barSectionID.EditValue.ToString(), "5", barKPI.EditValue.ToString());
                        if (dtSICClean.Rows.Count > 0)
                        {
                            _rowcnt = dtSICClean.Rows.Count;
                            pvgSICCleaned.DataSource = dtSICClean;
                            pvgSICCleaned.Visible = true;
                            pvgSICCleaned.BringToFront();
                            _foundClean = true;
                        }
                        else
                        {
                            MessageBox.Show("No Data for your Selection", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                    }
                }                
                else
                {
                    MessageBox.Show("Please select a Section", "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a Section", "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool theSaveSIC;
            if (barKPI.EditValue.ToString() != "Cleaned")
                theSaveSIC = _clsSICCapture.Save_SIC(dtSIC, _prdmth, barSectionID.EditValue.ToString(), barKPI.EditValue.ToString());
            else
                theSaveSIC = _clsSICCapture.Save_SICClean(dtSICClean, _prdmth, barSectionID.EditValue.ToString(), barKPI.EditValue.ToString());
            if (theSaveSIC == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "SIC Values Saved", Color.CornflowerBlue);
                pvgSICCapture.Visible = false;
                pvgSICCleaned.Visible = false;
                btnShow.Enabled = true;
                btnSave.Enabled = false;
            }
        }
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dispose();
        }
        private void btnProblemRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtSICClean != null)
            {
                DataRow[] EditedRow = dtSICClean.Select(
                            " Name = '" + TheSelectedName + "' "
                            + " and Workplace = '" + TheSelectedWorkplace + "' "
                            + "and CalendarDate = '" + TheSelectedDate + "'"
                            + "and Type = 'Book' ");
                foreach (DataRow dr in EditedRow)
                {
                    dr["TheValue"] = "";
                    dr["ProblemCode"] = "";
                    dr["SBNotes"] = "";
                    dr["CausedLostBlast"] = "";
                }
                dtSICClean.AcceptChanges();
                btnSave.Enabled = true;
            }
        }

        private void btnProblemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _clsProblem.theConnection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _clsProblem.TheSection = TheSelectedName;
            _clsProblem.TheWorkpalce = TheSelectedWorkplace;
            _clsProblem.TheActivity = Convert.ToInt32(TheSelectedActivity.ToString());
            _clsProblem.TheDate = Convert.ToDateTime(TheSelectedDate);
            _clsProblem.ProblemID = "";
            _clsProblem.SBossNotes = "";

            _clsProblem.ShowDialog();

            //Update The Master Data
            if (_clsProblem.ProblemID != "") // Update The data to selected problem
            {
                if (dtSICClean != null)
                {
                    string _lost = "";
                    dtCausedLostBlast = _clsSICCapture.get_CausedLostBlast(TheSelectedActivity.ToString(), _clsProblem.ProblemID);
                    if (dtCausedLostBlast.Rows.Count > 0)
                        _lost = dtCausedLostBlast.Rows[0]["CausedLostBlast"].ToString();
                    DataRow[] EditedRow = dtSICClean.Select(
                                " Name = '" + TheSelectedName + "' "
                                + "and Workplace = '" + TheSelectedWorkplace + "' "
                                + "and CalendarDate = '" + _clsProblem.TheDate + "'"
                                + "and Activity = '" + TheSelectedActivity + "' "
                                + "and Type = 'Book' ");
                    foreach (DataRow dr in EditedRow)
                    {
                        dr["TheValue"] = _clsProblem.lbNoteID.Text;
                        dr["ProblemCode"] = _clsProblem.ProblemID;
                        dr["ProblemDesc"] = _clsProblem.ProblemDesc;
                        dr["SBNotes"] = _clsProblem.SBossNotes;
                        dr["CausedLostBlast"] = _lost;
                    }
                    dtSICClean.AcceptChanges();
                    btnSave.Enabled = true;
                }
            }
        }

        private void btnProblemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _clsProblem.theConnection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _clsProblem.TheSection = TheSelectedName;
            _clsProblem.TheWorkpalce = TheSelectedWorkplace;
            _clsProblem.TheActivity = Convert.ToInt32(TheSelectedActivity.ToString());
            _clsProblem.TheDate = Convert.ToDateTime(TheSelectedDate);
            _clsProblem.ProblemID = ProductionGlobal.TProductionGlobal.ExtractBeforeColon(TheSelectedProblemCode);
            _clsProblem.ProblemDesc = ProductionGlobal.TProductionGlobal.ExtractAfterColon(TheSelectedProblemCode);
            //dtProblemDesc = _clsSICCapture.get_Problems_Desc(TheSelectedActivity.ToString(), TheSelectedProblemCode);
            //_clsProblem.ProblemDesc = "";
            //if (dtProblemDesc.Rows.Count > 0)
            //    _clsProblem.ProblemDesc = TheSelectedProblemCode + ":" + dtProblemDesc.Rows[0]["ProblemDesc"].ToString();
            
            DataRow[] EditedRow1 = dtSICClean.Select(
                                " Name = '" + TheSelectedName + "' "
                                + "and Workplace = '" + TheSelectedWorkplace + "' "
                                + "and CalendarDate = '" + TheSelectedDate + "'"
                                + "and Activity = '" + TheSelectedActivity + "' "
                                + "and Type = 'Book' ");
            foreach (DataRow dr in EditedRow1)
            {
                _clsProblem.SBossNotes = dr["SBNotes"].ToString();
            }

            _clsProblem.ShowDialog();

            //Update The Master Data
            if (_clsProblem.ProblemID != "") // Update The data to selected problem
            {
                if (dtSICClean != null)
                {
                    string _lost = "";
                    dtCausedLostBlast = _clsSICCapture.get_CausedLostBlast(TheSelectedActivity.ToString(), _clsProblem.ProblemID);
                    if (dtCausedLostBlast.Rows.Count > 0)
                        _lost = dtCausedLostBlast.Rows[0]["CausedLostBlast"].ToString();
                    DataRow[] EditedRow = dtSICClean.Select(
                                 " Name = '" + _clsProblem.TheSection + "' "
                                + " and Workplace = '" + _clsProblem.TheWorkpalce + "' "
                                + "and CalendarDate = '" + _clsProblem.TheDate + "'"
                                + "and Activity = '" + _clsProblem.TheActivity + "' "
                                + "and Type = 'Book' and SICKey = 23");
                    foreach (DataRow dr in EditedRow)
                    {
                        dr["TheValue"] = _clsProblem.lbNoteID.Text;
                        dr["ProblemCode"] = _clsProblem.ProblemID;
                        dr["ProblemDesc"] = _clsProblem.ProblemDesc;
                        dr["SBNotes"] = _clsProblem.SBossNotes;
                        dr["CausedLostBlast"] = _lost;
                    }
                    dtSICClean.AcceptChanges();
                    btnSave.Enabled = true;
                }
            }
        }
        private void btnClearRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow[] EditedRow = dtSIC.Select("SICKey = '" + TheSelectedSICKey + "' ");
            foreach (DataRow dr in EditedRow)
            {
                dr["TheValue"] = "";
                dr["ProgValue"] = "";
            }
            dtSIC.AcceptChanges();
            btnSave.Enabled = true;
        }

        private void btnFillRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string _cnt = "";
            decimal _cnt1 = 0;
            string filterExp = "SICKey = '" + TheSelectedSICKey + "' and ShiftNo <> 'N' ";
            string sortExp = "CalendarDate";
            DataRow[] EditedRow;
            EditedRow = dtSIC.Select(filterExp, sortExp, DataViewRowState.CurrentRows);
            if ((TheSelectedSICKey == "7") |
                (TheSelectedSICKey == "14") |
                (TheSelectedSICKey == "21"))
            {
                foreach (DataRow dr in EditedRow)
                {
                    TheEditValue = dr["TheValue"].ToString();
                    if (_cnt1 == 0)
                    {
                        if (TheEditValue != "")
                        {
                            if (_cnt1 != Convert.ToDecimal(TheEditValue))
                                _cnt1 = Convert.ToDecimal(dr["TheValue"].ToString());
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (TheEditValue == "")
                        {
                            _cnt1 = _cnt1 + 1;
                            dr["TheValue"] = _cnt1;
                        }
                        else
                        {
                            _cnt1 = Convert.ToDecimal(dr["TheValue"].ToString());
                        }
                    }
                }
                dtSIC.AcceptChanges();
            }
            else
            {
                foreach (DataRow dr in EditedRow)
                {
                    TheEditValue = dr["TheValue"].ToString();
                    if (_cnt == "")
                    {
                        if (TheEditValue != "")
                        {
                            if (_cnt != TheEditValue)
                                _cnt = dr["TheValue"].ToString();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (TheEditValue == "")
                        {
                            dr["TheValue"] = _cnt;
                        }
                        else
                        {
                            _cnt = dr["TheValue"].ToString();
                        }
                    }
                }
                dtSIC.AcceptChanges();
                Calc_Prog();
            }
            
            btnSave.Enabled = true;
        }
        private void Calc_Prog()
        {
            DataRow[] EditedRow = dtSIC.Select("SICKey = '" + TheSelectedSICKey + "' ");
            decimal _cntShft = 0;
            decimal _value = 0;
            foreach (DataRow dr in EditedRow)
            {
                if (dr["TheValue"] != null)
                    if (dr["TheValue"] != "")
                    {
                        _cntShft = _cntShft + 1;
                        _value = _value + Convert.ToDecimal(dr["TheValue"].ToString());
                    }
            }
            if (_cntShft > 0)
                _value = _value / _cntShft;
            else
                _value = 0;
            EditedRow = dtSIC.Select("SICKey = '" + TheSelectedSICKey + "' ");
            foreach (DataRow dr in EditedRow)
            {
                dr["ProgValue"] = string.Format("{0:0.00}", _value);
            }
            dtSIC.AcceptChanges();
        }
        private void Load_Month()
        {
            if (barKPI.EditValue != null)
            {
                if (barKPI.EditValue.ToString() != "")
                {
                    if ((barKPI.EditValue.ToString() == "Safety Stoping") |
                        (barKPI.EditValue.ToString() == "Safety Development") |
                        (barKPI.EditValue.ToString() == "Safety Services"))
                        _typemonth = "Safety";
                    if ((barKPI.EditValue.ToString() == "Cleaned") |
                        (barKPI.EditValue.ToString() == "Labour Stoping") |
                        (barKPI.EditValue.ToString() == "Labour Development") |
                        (barKPI.EditValue.ToString() == "Enineeringg"))
                        _typemonth = "Production";
                    if ((barKPI.EditValue.ToString() == "Cost Production") |
                        (barKPI.EditValue.ToString() == "Cost Enginerring"))
                        _typemonth = "Costing";
                    if (barKPI.EditValue.ToString() == "Backfill")
                        _typemonth = "Mill";

                    _clsSICCapture.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    dtSysset = _clsSICCapture.get_Sysset();
                    if (dtSysset.Rows.Count > 0)
                    {
                        _prodmonth = dtSysset.Rows[0]["CurrentProductionMonth"].ToString();
                        _millmonth = dtSysset.Rows[0]["CurrentMillMonth"].ToString();
                    }
                    if (_typemonth == "Production")
                        _prdmth = _prodmonth;
                    else
                        if (_typemonth == "Mill")
                        _prdmth = _millmonth;
                    else
                        _prdmth = _yearmonth;
                    barMonth.EditValue = ProductionGlobal.TProductionGlobal.ProdMonthAsDate(_prdmth);
                    Load_Sections();
                }
                else
                {
                    MessageBox.Show("Please select a KPI", "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a KPI", "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        private void Load_Sections()
        {
            dtSections = null;
            barSectionID.EditValue = null;
            _clsSICCapture.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSections = _clsSICCapture.get_Sections(_prdmth, barKPI.EditValue.ToString());
            rpSectionID.DataSource = dtSections;
            rpSectionID.ValueMember = "Section";
            rpSectionID.DisplayMember = "Name";
        }
        
        private void barKPI_EditValueChanged(object sender, EventArgs e)
        {
            pvgSICCapture.Visible = false;
            pvgSICCleaned.Visible = false;
            Load_Month();
        }

        private void barSectionID_EditValueChanged(object sender, EventArgs e)
        {
            pvgSICCapture.Visible = false;
            pvgSICCleaned.Visible = false;
        }

        private void rpMonth_EditValueChanged(object sender, EventArgs e)
        {
            pvgSICCapture.Visible = false;
            pvgSICCleaned.Visible = false;
            barSectionID.EditValue = null;
        }
        private void pvgSICCapture_MouseUp(object sender, MouseEventArgs e)
        {
            PivotGridControl Pivot = sender as PivotGridControl;
            PivotGridHitInfo hInfo = Pivot.CalcHitInfo(e.Location);

            if (hInfo == null || hInfo.CellInfo == null)
            {
                return;
            }

            if (hInfo.CellInfo.ColumnIndex >= 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (Pivot.GetFieldValue(col_CalendarDate, hInfo.CellInfo.RowIndex).ToString() != "")
                    {
                        popupRow.ShowPopup(MousePosition);
                    }
                }
            }
        }
        private void pvgSICCapture_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            TheEditSICKey = pvgSICCapture.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_SICKey).ToString();
            TheEditDate = pvgSICCapture.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_CalendarDate).ToString();
            TheEditShiftNo = pvgSICCapture.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_ShiftNo).ToString();

            DataRow[] EditedRow = dtSIC.Select("SICKey = '" + TheEditSICKey + "' "
                                            + " and CalendarDate = '" + TheEditDate + "' "
                                            + " and ShiftNo <> 'N' ");

            foreach (DataRow dr in EditedRow)
            {
                string aa = string.Format("{0:0.00}",Convert.ToDecimal(e.Editor.EditValue.ToString()));
                dr["TheValue"] = aa;
            }

            dtSIC.AcceptChanges();

            Calc_Prog();

            pvgSICCapture.RefreshData();

            btnSave.Enabled = true;
        }
        private void pvgSICCleaned_DoubleClick(object sender, EventArgs e)
        {
            if (TheSelectedActivity == "1")
            {
                if (dtSICClean != null)
                {
                    DataRow[] EditedRow = dtSICClean.Select(
                                 " Name = '" + TheSelectedName + "' "
                                + " and Workplace = '" + TheSelectedWorkplace + "' "
                                + "and CalendarDate = '" + TheSelectedDate + "'"
                                + "and Activity = '" + TheSelectedActivity + "' "
                                + "and Type = '" + TheSelectedType + "' ");
                    foreach (DataRow dr in EditedRow)
                    {
                        if (dr["ProblemCode"].ToString() == "")
                        {
                            if (dr["TheValue"].ToString() == "Yes")
                                dr["TheValue"] = "";
                            else
                            {
                                if (dr["TheValue"].ToString() == "")
                                    dr["TheValue"] = "Yes";
                            }
                        }
                    }
                    dtSICClean.AcceptChanges();
                    btnSave.Enabled = true;
                }
            }
        }

        private void rpText_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            string test1 = edit.Text;
            if (e.KeyChar.ToString() != "\b") // This just checks for Backspace
            {
                try
                {
                    _clsValidation.MWValidationType = TProductionGlobal.clsValidation.ValidationType.MWDouble5D2;
                    _clsValidation.MWInput = test1 + e.KeyChar;
                    if (!_clsValidation.Validate())
                    {
                        e.Handled = true;
                        return;
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                }
            }
        }

        private void pvgSICCleaned_MouseUp(object sender, MouseEventArgs e)
        {
            PivotGridControl Pivot = sender as PivotGridControl;
            PivotGridHitInfo hInfo = Pivot.CalcHitInfo(e.Location);

            if (hInfo == null || hInfo.CellInfo == null)
            {
                return;
            }

            if (hInfo.CellInfo.ColumnIndex >= 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (Pivot.GetFieldValue(colc_Type, hInfo.CellInfo.RowIndex).ToString() == "Book")
                        if (Pivot.GetFieldValue(colc_CalendarDate, hInfo.CellInfo.RowIndex).ToString() != "")
                        {
                            popupProblem.ShowPopup(MousePosition);
                        }
                }
            }
        }

        private void barMonth_EditValueChanged(object sender, EventArgs e)
        {
            pvgSICCapture.Visible = false;
            pvgSICCleaned.Visible = false;
            DateTime dd = Convert.ToDateTime(barMonth.EditValue.ToString());
            _prdmth = dd.ToString("yyyyMM");
            barSectionID.EditValue = null;
            Load_Sections();
        }
        private void pvgSICCapture_MouseMove(object sender, MouseEventArgs e)
        {
            PivotGridControl Pivot = sender as PivotGridControl;
            PivotGridHitInfo hInfo = Pivot.CalcHitInfo(e.Location);

            if (hInfo == null || hInfo.CellInfo == null)
            {
                return;
            }
            if (hInfo.CellInfo.RowIndex <= Pivot.Cells.RowCount)
            {
                try
                {
                    TheSelectedShiftNo = Pivot.Cells.GetCellInfo(hInfo.CellInfo.ColumnIndex, hInfo.CellInfo.RowIndex).GetFieldValue(col_ShiftNo).ToString();
                    TheSelectedSICKey = Pivot.Cells.GetCellInfo(hInfo.CellInfo.ColumnIndex, hInfo.CellInfo.RowIndex).GetFieldValue(col_SICKey).ToString();
                }
                catch (NullReferenceException ex)
                {

                }
            }
        }
        private void pvgSICCapture_CustomDrawCell(object sender, DevExpress.XtraPivotGrid.PivotCustomDrawCellEventArgs e)
        {
            if (_foundSIC == true)
            {
                e.Appearance.BackColor = Color.White;
                try
                {
                    if (pvgSICCapture.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_Description).ToString() != null)
                    {
                        TheSelectedDate = pvgSICCapture.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(col_CalendarDate).ToString();
                        DataRow[] EditedRow = dtSIC.Select("CalendarDate = '" + TheSelectedDate + "' ");
                        foreach (DataRow dr in EditedRow)
                        {
                            TheEditWorkingDay = dr["ShiftNo"].ToString();                            
                        }
                        if (TheEditWorkingDay == "N")
                        {
                            e.Appearance.BackColor = Color.Red;
                        }
                    }
                }
                catch (Exception _exception)
                {
                    throw new ApplicationException(_exception.Message, _exception);
                }
            }
        }
        private void pvgSICCapture_ShowingEditor(object sender, DevExpress.XtraPivotGrid.CancelPivotCellEditEventArgs e)
        {
            PivotGridControl Pivot = sender as PivotGridControl;

            if (e.RowValueType == PivotGridValueType.Value)
            {
                if (TheSelectedShiftNo.ToString() == "N")
                    e.Cancel = true; 
            }
            else
                 e.Cancel = true;
        }
        private void pvgSICCleaned_CustomDrawCell(object sender, PivotCustomDrawCellEventArgs e)
        {
            if (_foundClean == true)
            {
                e.Appearance.BackColor = Color.White;
                try
                {
                    if (pvgSICCleaned.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(colc_WP).ToString() != null)
                    {
                        TheSelectedDate = pvgSICCleaned.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(colc_CalendarDate).ToString();

                        DataRow[] EditedRow = dtSICClean.Select("CalendarDate = '" + TheSelectedDate + "' ");
                        foreach (DataRow dr in EditedRow)
                        {
                            TheEditWorkingDay = dr["ShiftNo"].ToString();
                        }
                        if (TheEditWorkingDay == "N")
                        {
                            e.Appearance.BackColor = Color.Red;
                        }
                        if (pvgSICCleaned.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(colc_Type).ToString() == "Book FL")
                        {
                            if (TheEditWorkingDay != "N")
                                e.Appearance.BackColor = Color.Gainsboro;
                        }
                    }
                }
                catch (Exception _exception)
                {
                    throw new ApplicationException(_exception.Message, _exception);
                }
            }
        }
        private void pvgSICCleaned_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            TheEditWorkplace = pvgSICCleaned.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(colc_WP).ToString();
            TheEditDate = pvgSICCleaned.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(colc_CalendarDate).ToString();
            TheEditShiftNo = pvgSICCleaned.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(colc_ShiftNo).ToString();
            TheEditType = pvgSICCleaned.Cells.GetCellInfo(e.ColumnIndex, e.RowIndex).GetFieldValue(colc_Type).ToString();

            DataRow[] EditedRow = dtSICClean.Select("Workplace = '" + TheEditWorkplace + "' "
                                            + " and CalendarDate = '" + TheEditDate + "' "
                                            + " and ShiftNo <> 'N' and Type = '" + TheEditType + "' ");

            foreach (DataRow dr in EditedRow)
            {
                dr["TheValue"] = e.Editor.EditValue.ToString();
            }

            dtSICClean.AcceptChanges();
            pvgSICCleaned.RefreshData();

            btnSave.Enabled = true;
        }
        private void pvgSICCleaned_MouseMove(object sender, MouseEventArgs e)
        {
            PivotGridControl Pivot = sender as PivotGridControl;
            PivotGridHitInfo hInfo = Pivot.CalcHitInfo(e.Location);

            if (hInfo == null || hInfo.CellInfo == null)
            {
                return;
            }
            if (hInfo.CellInfo.RowIndex <= Pivot.Cells.RowCount)
            {
                try
                {
                    TheSelectedShiftNo = Pivot.Cells.GetCellInfo(hInfo.CellInfo.ColumnIndex, hInfo.CellInfo.RowIndex).GetFieldValue(colc_ShiftNo).ToString();
                    TheSelectedWorkplace = Pivot.Cells.GetCellInfo(hInfo.CellInfo.ColumnIndex, hInfo.CellInfo.RowIndex).GetFieldValue(colc_WP).ToString();
                    TheSelectedName = Pivot.Cells.GetCellInfo(hInfo.CellInfo.ColumnIndex, hInfo.CellInfo.RowIndex).GetFieldValue(colc_Name).ToString();

                    TheSelectedType = Pivot.Cells.GetCellInfo(hInfo.CellInfo.ColumnIndex, hInfo.CellInfo.RowIndex).GetFieldValue(colc_Type).ToString();
                    TheSelectedActivity = Pivot.Cells.GetCellInfo(hInfo.CellInfo.ColumnIndex, hInfo.CellInfo.RowIndex).GetFieldValue(colc_Activity).ToString();
                    TheSelectedDate = Pivot.Cells.GetCellInfo(hInfo.CellInfo.ColumnIndex, hInfo.CellInfo.RowIndex).GetFieldValue(colc_CalendarDate).ToString();
                    TheSelectedProblemCode = Pivot.Cells.GetCellInfo(hInfo.CellInfo.ColumnIndex, hInfo.CellInfo.RowIndex).GetFieldValue(colc_TheValue).ToString();
                }
                catch (NullReferenceException ex)
                {

                }
            }
        }
        private void pvgSICCleaned_ShowingEditor(object sender, CancelPivotCellEditEventArgs e)
        {
            PivotGridControl Pivot = sender as PivotGridControl;
            if (e.RowValueType == PivotGridValueType.Value)
            {
                if (Pivot.GetFieldValue(colc_Type, e.RowIndex).ToString() == "Book FL")
                    e.Cancel = true;
                if (TheSelectedShiftNo.ToString() == "N")
                    e.Cancel = true;
                if (TheSelectedActivity == "1")
                    e.Cancel = true;
            }
            else
                e.Cancel = true;
        }
        private void rpTextClean_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            string test1 = edit.Text;
            if (e.KeyChar.ToString() != "\b") // This just checks for Backspace
            {
                try
                {
                    if (TheSelectedActivity != "1")
                    {
                        _clsValidation.MWValidationType = TProductionGlobal.clsValidation.ValidationType.MWInteger5;
                        _clsValidation.MWInput = test1 + e.KeyChar;
                        if (!_clsValidation.Validate())
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                    else
                    {

                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                }
            }
        }
    }
}
