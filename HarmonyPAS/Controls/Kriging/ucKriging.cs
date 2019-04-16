using System;
using System.Drawing;
using System.Data;
using Mineware.Systems.Global;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using System.Text.RegularExpressions;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using DevExpress.XtraEditors;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.Kriging
{
    public partial class ucKriging : ucBaseUserControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        clsKriging _clsKriging = new clsKriging();
        TProductionGlobal.clsValidation _clsValidation = new TProductionGlobal.clsValidation();

        private DataTable dtSysset;
        private DataTable dtSections;
        private DataTable dtKriging;
        private DataTable dtPlanning;
        private DataTable dtWeekNo;

        public string _prodmonth;
        public string _mohierid = "4";

        private bool ErrorFound;
        private string ErrorMsg;

        private DateTime _begindate;
        private DateTime _enddate;

        private int _weekno;
        private int _sampling;

        private string _Unit;

        private string _dateTrue;

        private string _gt;

        public ucKriging()
        {
            InitializeComponent();
        }

        private void ucKriging_Load(object sender, EventArgs e)
        {
            _prodmonth = String.Format("{0:yyyyMM}", DateTime.Now);
            Visibles();           
            _clsKriging.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSysset = _clsKriging.get_Sysset();
            if (dtSysset.Rows.Count > 0)
            {
                _mohierid = dtSysset.Rows[0]["MOHierarchicalID"].ToString();
                _prodmonth = dtSysset.Rows[0]["CurrentProductionMonth"].ToString();
            }
            barProdMonth.EditValue = ProductionGlobal.TProductionGlobal.ProdMonthAsDate(_prodmonth);//getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());            
            Load_Sections();
        }
        private void loadScreenData()
        {
            _dateTrue = "No";
            if ((Convert.ToDateTime(DateTime.Now.ToShortDateString()) >= Convert.ToDateTime(_begindate.ToShortDateString())) &&
                (Convert.ToDateTime(DateTime.Now.ToShortDateString()) <= Convert.ToDateTime(_enddate.ToShortDateString())))
                _dateTrue = "Yes";

                _clsKriging.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtKriging = _clsKriging.get_Kriging(_prodmonth, barSections.EditValue.ToString(), _weekno, _sampling, _dateTrue);
            if (dtKriging.Rows.Count > 0)
            {
                if ((Convert.ToDateTime(DateTime.Now.ToShortDateString()) >= Convert.ToDateTime(_begindate.ToShortDateString())) &&
                    (Convert.ToDateTime(DateTime.Now.ToShortDateString()) <= Convert.ToDateTime(_enddate.ToShortDateString())))
                {
                    btnWeek1.Visible = true;
                    btnWeek2.Visible = true;
                    btnWeek3.Visible = true;
                    btnWeek4.Visible = true;
                    btnImport1.Visible = true;
                    btnImport2.Visible = true;
                    btnImport3.Visible = true;
                    btnImport4.Visible = true;
                    if ((_weekno == 1) |
                        (_weekno == 0))
                    {
                        btnWeek1.Enabled = true;
                        btnImport1.Enabled = true;
                    }
                    if (_weekno == 2)
                    {
                        btnWeek1.Enabled = true;
                        btnWeek2.Enabled = true;
                        btnImport1.Enabled = true;
                        btnImport2.Enabled = true;
                    }
                    if (_weekno == 3)
                    {
                        btnWeek2.Enabled = true;
                        btnWeek3.Enabled = true;
                        btnImport2.Enabled = true;
                        btnImport3.Enabled = true;
                    }
                    if (_weekno == 4)
                    {
                        btnWeek3.Enabled = true;
                        btnWeek4.Enabled = true;
                        btnImport3.Enabled = true;
                        btnImport4.Enabled = true;
                    }
                    if (_weekno == 5)
                    {
                        btnWeek4.Enabled = true;
                        btnImport4.Enabled = true;
                    }
                }
                gcKriging.DataSource = dtKriging;
                gcKriging.Visible = true;
            }
            else
                MessageBox.Show("No Data for your Selection", "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

        }
        private void Load_Sections()
        {
            barSections.EditValue = null;
            _clsKriging.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSections = _clsKriging.get_Sections(_prodmonth, _mohierid);
            rpSections.DataSource = dtSections;
            rpSections.ValueMember = "SectionID";
            rpSections.DisplayMember = "Name";
        }
        private void Visibles()
        {
            btnCancel.Enabled = false;
            gcKriging.Visible = false;
            btnWeek1.Visible = false;
            btnWeek2.Visible = false;
            btnWeek3.Visible = false;
            btnWeek4.Visible = false;
            btnImport1.Visible = false;
            btnImport2.Visible = false;
            btnImport3.Visible = false;
            btnImport4.Visible = false;
            btnWeek1.Enabled = false;
            btnWeek2.Enabled = false;
            btnWeek3.Enabled = false;
            btnWeek4.Enabled = false;
            btnImport1.Enabled = false;
            btnImport2.Enabled = false;
            btnImport3.Enabled = false;
            btnImport4.Enabled = false;
        }
        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtKriging = null;
            gcKriging.DataSource = dtKriging;
            _weekno = 0;
            _sampling = 0;
            ErrorFound = false;

            if (barSections.EditValue == null)
            {
                ErrorMsg = "Please select a section";
                ErrorFound = true;
            }
            if (ErrorFound == false)
            {
                if (barSections.EditValue.ToString() == "")
                {
                    ErrorMsg = "Please select a section";
                    ErrorFound = true;
                }
            }
            if (ErrorFound == false)
            {
                _clsKriging.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtPlanning = _clsKriging.get_Planning(_prodmonth, barSections.EditValue.ToString());
                if (dtPlanning.Rows.Count == 0)
                {
                    ErrorFound = true;
                    ErrorMsg = "The Plan for the requested month must be locked";
                }
            }
            if (ErrorFound == false)
            {
                _clsKriging.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtPlanning = _clsKriging.get_Calendars(_prodmonth, barSections.EditValue.ToString());
                if (dtPlanning.Rows.Count == 0)
                {
                    ErrorFound = true;
                    ErrorMsg = "No calendar exists for selected section";
                }
                else
                {
                    _begindate = Convert.ToDateTime(dtPlanning.Rows[0]["BeginDate"].ToString());
                    _enddate = Convert.ToDateTime(dtPlanning.Rows[0]["EndDate"].ToString());
                }
            }
            if (ErrorFound == false)
            {
                _clsKriging.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtWeekNo = _clsKriging.get_WeekNo(_prodmonth, barSections.EditValue.ToString());

                if (dtWeekNo.Rows.Count == 0)
                {
                    ErrorFound = true;
                    ErrorMsg = "The Lock Planning values have not been saved";
                }
                else
                {
                    if (dtWeekNo.Rows[0]["WeekNo"].ToString() != null)
                        if (dtWeekNo.Rows[0]["WeekNo"].ToString() != "")
                            _weekno = Convert.ToInt32(dtWeekNo.Rows[0]["WeekNo"].ToString());
                }
            }
            if (ErrorFound == true)
                MessageBox.Show(ErrorMsg, "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            else
                loadScreenData();
            gvKriging.Focus();

        }
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dispose();
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadScreenData();
        }

        private void barProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            DateTime dd = Convert.ToDateTime(barProdMonth.EditValue.ToString());
            _prodmonth = dd.ToString("yyyyMM");
            barSections.EditValue = null;
            Load_Sections();
        }

        private void gvKriging_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if ((Convert.ToDateTime(DateTime.Now.ToShortDateString()) >= Convert.ToDateTime(_begindate.ToShortDateString())) &&
                (Convert.ToDateTime(DateTime.Now.ToShortDateString()) <= Convert.ToDateTime(_enddate.ToShortDateString())))
            {
                if (_Unit == "Total g/t")
                    e.Cancel = true;
                else
                {
                    if (_weekno == 2)
                    {
                        if ((view.FocusedColumn.FieldName != "Week2") &&
                            (view.FocusedColumn.FieldName != "Week3"))
                        {
                            e.Cancel = true;
                        }
                    }
                    if (_weekno == 3)
                    {
                        if ((view.FocusedColumn.FieldName != "Week3") &&
                            (view.FocusedColumn.FieldName != "Week4"))
                        {
                            e.Cancel = true;
                        }
                    }
                    if (_weekno == 4)
                    {
                        if ((view.FocusedColumn.FieldName != "Week4") &&
                            (view.FocusedColumn.FieldName != "Week5"))
                        {
                            e.Cancel = true;
                        }
                    }
                    if (_weekno == 5)
                    {
                        if (view.FocusedColumn.FieldName != "Week5")
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
            else
                e.Cancel = true;
        }

        private void gvKriging_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            e.Appearance.ForeColor = Color.Black;
            e.Appearance.BackColor = Color.White;

            string thetype = gvKriging.GetRowCellDisplayText(e.RowHandle, "Unit").ToString();
            if (thetype == "Total g/t")
            {
                if ((e.Column.FieldName == "SectID") |
                    (e.Column.FieldName == "SectName") |
                    (e.Column.FieldName == "WPID") |
                    (e.Column.FieldName == "WPDesc") |
                    (e.Column.FieldName == "ActDesc") |
                    (e.Column.FieldName == "Unit") |
                    (e.Column.FieldName == "Week1") |
                    (e.Column.FieldName == "Week2") |
                    (e.Column.FieldName == "Week3") |
                    (e.Column.FieldName == "Week4") |
                    (e.Column.FieldName == "Week5"))
                {
                    e.Appearance.BackColor = Color.LightGray;
                }
            }
            else
            {
                if ((Convert.ToDateTime(DateTime.Now.ToShortDateString()) >= Convert.ToDateTime(_begindate.ToShortDateString())) &&
                (Convert.ToDateTime(DateTime.Now.ToShortDateString()) <= Convert.ToDateTime(_enddate.ToShortDateString())))
                {
                    e.Appearance.BackColor = Color.White;
                    if (_weekno == 1)
                    {
                        if (e.Column.FieldName == "Week2")
                        {
                            e.Appearance.BackColor = Color.LightCyan;
                            e.Appearance.ForeColor = Color.Black;
                        }
                    }
                    if (_weekno == 2)
                    {
                        if ((e.Column.FieldName == "Week2") |
                            (e.Column.FieldName == "Week3"))
                        {
                            e.Appearance.BackColor = Color.LightCyan;
                            e.Appearance.ForeColor = Color.Black;
                        }
                    }
                    if (_weekno == 3)
                    {
                        if ((e.Column.FieldName == "Week3") |
                            (e.Column.FieldName == "Week4"))
                        {
                            e.Appearance.BackColor = Color.LightCyan;
                            e.Appearance.ForeColor = Color.Black;
                        }
                    }
                    if (_weekno == 4)
                    {
                        if ((e.Column.FieldName == "Week4") |
                            (e.Column.FieldName == "Week5"))
                        {
                            e.Appearance.BackColor = Color.LightCyan;
                            e.Appearance.ForeColor = Color.Black;
                        }
                    }
                    if (_weekno == 5)
                    {
                        if (e.Column.FieldName == "Week5")
                        {
                            e.Appearance.BackColor = Color.LightCyan;
                            e.Appearance.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }

        private void gvKriging_RowClick(object sender, RowClickEventArgs e)
        {
            _Unit = "";
            if (gvKriging.GetRowCellValue(gvKriging.FocusedRowHandle, gvKriging.Columns["Unit"]) != null)
            {
                var Unit = gvKriging.GetRowCellValue(gvKriging.FocusedRowHandle, gvKriging.Columns["Unit"]);
                _Unit = Unit.ToString();
            }
        }

        private void gvKriging_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                _Unit = gvKriging.GetRowCellValue(gvKriging.FocusedRowHandle, col_Unit).ToString();

            }
        }
        private void gvKriging_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view;
            view = sender as GridView;
            object row = view.GetRow(view.FocusedRowHandle); // get the current row 

            string selectedProdMonth = (row as DataRowView)["ProdMonth"].ToString();
            string selectedSectionID = (row as DataRowView)["SectionID"].ToString();
            string selectedWP = (row as DataRowView)["WorkplaceID"].ToString();
            string selectedActivity = (row as DataRowView)["Activity"].ToString();
            string selectedUnit = (row as DataRowView)["Unit"].ToString();
            string selectedW1Val = (row as DataRowView)["Week2"].ToString();
            string selectedW2Val = (row as DataRowView)["Week3"].ToString();
            string selectedW3Val = (row as DataRowView)["Week4"].ToString();
            string selectedW4Val = (row as DataRowView)["Week5"].ToString();
            if (selectedW1Val == "")
                selectedW1Val = "0";
            if (selectedW2Val == "")
                selectedW2Val = "0";
            if (selectedW3Val == "")
                selectedW3Val = "0";
            if (selectedW4Val == "")
                selectedW4Val = "0";

            if (view.FocusedColumn.FieldName == "Week2")
            {
                if (dtKriging != null)
                {
                    (row as DataRowView)["Week2"] = selectedW1Val;
                    dtKriging.AcceptChanges();
                    DataRow[] EditedRow = dtKriging.Select(
                                " ProdMonth = '" + selectedProdMonth + "' "
                                + "and SectionID = '" + selectedSectionID + "' "
                                + "and WorkplaceID = '" + selectedWP + "' "
                                + "and Activity = '" + selectedActivity + "' ");
                    foreach (DataRow dr in EditedRow)
                    {
                        if (selectedUnit == "cmg/t")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal(selectedW1Val),
                                Convert.ToInt32((row as DataRowView)["W2SW"].ToString()),
                                Convert.ToDecimal((row as DataRowView)["W2EH"].ToString()));
                            dr["W2gt"] = _gt;
                            dr["W2cmgt"] = selectedW1Val;
                        }
                        if (selectedUnit == "SW")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal((row as DataRowView)["W2cmgt"].ToString()),
                                Convert.ToInt32(selectedW1Val),
                                Convert.ToDecimal((row as DataRowView)["W2EH"].ToString()));
                            dr["W2gt"] = _gt;
                            dr["W2SW"] = selectedW1Val;
                        }
                        if (selectedUnit == "CW")
                        {
                            dr["W2CW"] = selectedW1Val;
                        }
                        if (selectedUnit == "EH")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                               Convert.ToDecimal((row as DataRowView)["W2cmgt"].ToString()),
                                Convert.ToInt32((row as DataRowView)["W2SW"].ToString()),
                                Convert.ToDecimal(selectedW1Val));
                            dr["W2gt"] = _gt;
                            dr["W2EH"] = selectedW1Val;
                        }
                        if (selectedUnit == "EW")
                        {
                            dr["W2EW"] = selectedW1Val;
                        }
                    }
                    dtKriging.AcceptChanges();
                    DataRow[] EditedRow1 = dtKriging.Select(
                                " ProdMonth = '" + selectedProdMonth + "' "
                                + "and SectionID = '" + selectedSectionID + "' "
                                + "and WorkplaceID = '" + selectedWP + "' "
                                + "and Activity = '" + selectedActivity + "' "
                                + " and Unit = 'Total g/t' ");
                    foreach (DataRow dr in EditedRow1)
                    {
                        Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal((row as DataRowView)["W2cmgt"].ToString()),
                                Convert.ToInt32((row as DataRowView)["W2SW"].ToString()),
                                Convert.ToDecimal((row as DataRowView)["W2EH"].ToString()));
                        dr["Week2"] = _gt;
                    }
                    dtKriging.AcceptChanges();
                }
            }
            if (view.FocusedColumn.FieldName == "Week3")
            {
                if (dtKriging != null)
                {
                    (row as DataRowView)["Week3"] = selectedW2Val;
                    dtKriging.AcceptChanges();

                    DataRow[] EditedRow = dtKriging.Select(
                                " ProdMonth = '" + selectedProdMonth + "' "
                                + "and SectionID = '" + selectedSectionID + "' "
                                + "and WorkplaceID = '" + selectedWP + "' "
                                + "and Activity = '" + selectedActivity + "' ");

                    decimal _thegt = 0;
                    foreach (DataRow dr in EditedRow)
                    {
                        if (selectedUnit == "cmg/t")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal(selectedW2Val),
                                Convert.ToInt32((row as DataRowView)["W3SW"].ToString()),
                                Convert.ToDecimal((row as DataRowView)["W3EH"].ToString()));
                            dr["W3gt"] = _gt;
                            dr["W3cmgt"] = selectedW2Val;
                        }
                        if (selectedUnit == "SW")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal((row as DataRowView)["W3cmgt"].ToString()),
                                Convert.ToInt32(selectedW2Val),
                                Convert.ToDecimal((row as DataRowView)["W3EH"].ToString()));
                            dr["W3gt"] = _gt;
                            dr["W3SW"] = selectedW2Val;
                        }
                        if (selectedUnit == "CW")
                        {
                            dr["W3CW"] = selectedW2Val;
                        }
                        if (selectedUnit == "EH")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                               Convert.ToDecimal((row as DataRowView)["W3cmgt"].ToString()),
                                Convert.ToInt32((row as DataRowView)["W3SW"].ToString()),
                                Convert.ToDecimal(selectedW2Val));
                            dr["W3gt"] = _gt;
                            dr["W3EH"] = selectedW2Val;
                        }
                        if (selectedUnit == "EW")
                        {
                            dr["W3EW"] = selectedW2Val;
                        }
                    }
                    dtKriging.AcceptChanges();
                    DataRow[] EditedRow1 = dtKriging.Select(
                                " ProdMonth = '" + selectedProdMonth + "' "
                                + "and SectionID = '" + selectedSectionID + "' "
                                + "and WorkplaceID = '" + selectedWP + "' "
                                + "and Activity = '" + selectedActivity + "' "
                                + " and Unit = 'Total g/t' ");
                    foreach (DataRow dr in EditedRow1)
                    {
                        Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal((row as DataRowView)["W3cmgt"].ToString()),
                                Convert.ToInt32((row as DataRowView)["W3SW"].ToString()),
                                Convert.ToDecimal((row as DataRowView)["W3EH"].ToString()));
                        dr["Week3"] = _gt;
                    }
                    dtKriging.AcceptChanges();
                }
            }
            if (view.FocusedColumn.FieldName == "Week4")
            {
                if (dtKriging != null)
                {
                    (row as DataRowView)["Week4"] = selectedW3Val;
                    dtKriging.AcceptChanges();
                    DataRow[] EditedRow = dtKriging.Select(
                                " ProdMonth = '" + selectedProdMonth + "' "
                                + "and SectionID = '" + selectedSectionID + "' "
                                + "and WorkplaceID = '" + selectedWP + "' "
                                + "and Activity = '" + selectedActivity + "' ");
                    foreach (DataRow dr in EditedRow)
                    {
                        if (selectedUnit == "cmg/t")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal(selectedW3Val),
                                Convert.ToInt32((row as DataRowView)["W4SW"].ToString()),
                                Convert.ToDecimal((row as DataRowView)["W4EH"].ToString()));
                            dr["W4gt"] = _gt;
                            dr["W4cmgt"] = selectedW3Val;
                        }
                        if (selectedUnit == "SW")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal((row as DataRowView)["W4cmgt"].ToString()),
                                Convert.ToInt32(selectedW3Val),
                                Convert.ToDecimal((row as DataRowView)["W4EH"].ToString()));
                            dr["W4gt"] = _gt;
                            dr["W4SW"] = selectedW3Val;
                        }
                        if (selectedUnit == "CW")
                        {
                            dr["W4CW"] = selectedW3Val;
                        }
                        if (selectedUnit == "EH")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                               Convert.ToDecimal((row as DataRowView)["W4cmgt"].ToString()),
                                Convert.ToInt32((row as DataRowView)["W4SW"].ToString()),
                                Convert.ToDecimal(selectedW3Val));
                            dr["W4gt"] = _gt;
                            dr["W4EH"] = selectedW3Val;
                        }
                        if (selectedUnit == "EW")
                        {
                            dr["W4EW"] = selectedW3Val;
                        }
                    }
                    dtKriging.AcceptChanges();
                    DataRow[] EditedRow1 = dtKriging.Select(
                                " ProdMonth = '" + selectedProdMonth + "' "
                                + "and SectionID = '" + selectedSectionID + "' "
                                + "and WorkplaceID = '" + selectedWP + "' "
                                + "and Activity = '" + selectedActivity + "' "
                                + " and Unit = 'Total g/t' ");
                    foreach (DataRow dr in EditedRow1)
                    {
                        Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal((row as DataRowView)["W4cmgt"].ToString()),
                                Convert.ToInt32((row as DataRowView)["W4SW"].ToString()),
                                Convert.ToDecimal((row as DataRowView)["W4EH"].ToString()));
                        dr["Week4"] = _gt;
                    }
                    dtKriging.AcceptChanges();
                }
            }
            if (view.FocusedColumn.FieldName == "Week5")
            {
                if (dtKriging != null)
                {
                    (row as DataRowView)["Week5"] = selectedW4Val;
                    dtKriging.AcceptChanges();
                    DataRow[] EditedRow = dtKriging.Select(
                                " ProdMonth = '" + selectedProdMonth + "' "
                                + "and SectionID = '" + selectedSectionID + "' "
                                + "and WorkplaceID = '" + selectedWP + "' "
                                + "and Activity = '" + selectedActivity + "' ");
                    foreach (DataRow dr in EditedRow)
                    {
                        if (selectedUnit == "cmg/t")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal(selectedW4Val),
                                Convert.ToInt32((row as DataRowView)["W5SW"].ToString()),
                                Convert.ToDecimal((row as DataRowView)["W5EH"].ToString()));
                            dr["W5gt"] = _gt;
                            dr["W5cmgt"] = selectedW4Val;
                        }
                        if (selectedUnit == "SW")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal((row as DataRowView)["W5cmgt"].ToString()),
                                Convert.ToInt32(selectedW4Val),
                                Convert.ToDecimal((row as DataRowView)["W5EH"].ToString()));
                            dr["W5gt"] = _gt;
                            dr["W5SW"] = selectedW4Val;
                        }
                        if (selectedUnit == "CW")
                        {
                            dr["W5CW"] = selectedW4Val;
                        }
                        if (selectedUnit == "EH")
                        {
                            Calc_gt((row as DataRowView)["Activity"].ToString(),
                               Convert.ToDecimal((row as DataRowView)["W5cmgt"].ToString()),
                                Convert.ToInt32((row as DataRowView)["W5SW"].ToString()),
                                Convert.ToDecimal(selectedW4Val));
                            dr["W5gt"] = _gt;
                            dr["W5EH"] = selectedW4Val;
                        }
                        if (selectedUnit == "EW")
                        {
                            dr["W5EW"] = selectedW4Val;
                        }
                    }
                    dtKriging.AcceptChanges();
                    DataRow[] EditedRow1 = dtKriging.Select(
                                " ProdMonth = '" + selectedProdMonth + "' "
                                + "and SectionID = '" + selectedSectionID + "' "
                                + "and WorkplaceID = '" + selectedWP + "' "
                                + "and Activity = '" + selectedActivity + "' "
                                + " and Unit = 'Total g/t' ");
                    foreach (DataRow dr in EditedRow1)
                    {
                        Calc_gt((row as DataRowView)["Activity"].ToString(),
                                Convert.ToDecimal((row as DataRowView)["W5cmgt"].ToString()),
                                Convert.ToInt32((row as DataRowView)["W5SW"].ToString()),
                                Convert.ToDecimal((row as DataRowView)["W5EH"].ToString()));
                        dr["Week5"] = _gt;
                    }
                    dtKriging.AcceptChanges();
                }
            }
            btnCancel.Enabled = true;
        }
        private void Calc_gt(string _act, decimal _cmgt, decimal _SW, decimal _EH)
        {
            //_gtAlpha = "0.00";
            //_gt = 0;
            if (_act != "1")
            {
                if (_SW > 0)
                {
                    //_gt = Math.Round(Convert.ToDecimal(_cmgt / _SW),2);
                    _gt = string.Format("{0:0.00}",_cmgt / _SW);
                    //string.Format("{0:0.00}", r["bg"].ToString());
                }
            }
            else
            {
                if (_EH > 0)
                {
                    //_gt = Math.Round(Convert.ToDecimal(_cmgt / _EH),2);
                     _gt = string.Format("{0:0.00}",_cmgt / _EH);
                }
            }
        }

        private void btnImport1_Click(object sender, EventArgs e)
        {
            _sampling = 2;
            loadScreenData();
        }

        private void btnImport2_Click(object sender, EventArgs e)
        {
            _sampling = 3;
            loadScreenData();
        }

        private void btnImport3_Click(object sender, EventArgs e)
        {
            _sampling = 4;
            loadScreenData();
        }

        private void btnImport4_Click(object sender, EventArgs e)
        {
            _sampling = 5;
            loadScreenData();
        }

        private void btnWeek1_Click(object sender, EventArgs e)
        {
            bool theSaveKriging = _clsKriging.save_Data(dtKriging, 2, UserCurrentInfo.UserID, _prodmonth, barSections.EditValue.ToString());
            if (theSaveKriging == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "Kriging Saved for Week 1", Color.CornflowerBlue);
                Visibles();
            }
        }
        private void btnWeek2_Click(object sender, EventArgs e)
        {
            bool theSaveKriging = _clsKriging.save_Data(dtKriging, 3, UserCurrentInfo.UserID, _prodmonth, barSections.EditValue.ToString());
            if (theSaveKriging == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "Kriging Saved for Week 2", Color.CornflowerBlue);
                Visibles();
            }
        }
        private void btnWeek3_Click(object sender, EventArgs e)
        {
            bool theSaveKriging = _clsKriging.save_Data(dtKriging, 4, UserCurrentInfo.UserID, _prodmonth, barSections.EditValue.ToString());
            if (theSaveKriging == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "Kriging Saved for Week 3", Color.CornflowerBlue);
                Visibles();
            }
        }
        private void btnWeek4_Click(object sender, EventArgs e)
        {
            bool theSaveKriging = _clsKriging.save_Data(dtKriging, 5, UserCurrentInfo.UserID, _prodmonth, barSections.EditValue.ToString());
            if (theSaveKriging == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "Kriging Saved for Week 4", Color.CornflowerBlue);
                Visibles();
            }
        }
        private void barSections_EditValueChanged(object sender, EventArgs e)
        {
            Visibles();
        }

        private void rpProdMonth_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            Visibles();
            barSections.EditValue = null;
        }

        private void rpText_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            string test1 = edit.Text;
            if (e.KeyChar.ToString() != "\b") // This just checks for Backspace
            {
                try
                {
                    if ((_Unit == "cmg/t") |
                        (_Unit == "SW") |
                        (_Unit == "CW"))
                    {
                        _clsValidation.MWValidationType = TProductionGlobal.clsValidation.ValidationType.MWInteger5;
                        _clsValidation.MWInput = test1 + e.KeyChar;
                    }
                    if ((_Unit == "EH") |
                        (_Unit == "EW"))
                    {
                        _clsValidation.MWValidationType = TProductionGlobal.clsValidation.ValidationType.MWDouble5D1;
                        _clsValidation.MWInput = test1 + e.KeyChar;
                    }

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
    }
}