using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using Mineware.Systems.Global;

namespace Mineware.Systems.Planning.PrePlanning
{
    public partial class ucCycleScreen : ucBaseUserControl
    {
        public PlanningCycleData CycleData { get; set; }
        public ucCycleScreen()
        {
            InitializeComponent();





        }

        public void SetData()
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            ShowProgressPanel = true;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            gvCyclePlan.Columns.Clear();
            worker.RunWorkerAsync();

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshGrid();
            ShowProgressPanel = false;
        }

        public void RefreshGrid()
        {
            gvCyclePlan.PostEditor();
            gvCyclePlan.RefreshData();
            SetBindings();
        }

        // set binding on edit boxes 
        private void SetBindings()
        {
            if (CycleData != null)
            {
                editMonthPlan.DataBindings.Clear();
                editMonthPlan.DataBindings.Add(new Binding("Text", CycleData, "MonthCall", true));
                editFL.DataBindings.Clear();
                editFL.DataBindings.Add(new Binding("Text", CycleData, "FaceLength", true));
                editCycleCall.DataBindings.Clear();
                editCycleCall.DataBindings.Add(new Binding("Text", CycleData, "CycleCall", true));
            }
        }

        public void GetSelectedWorkplace()
        {

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

            if (CycleData != null && CycleData.planningCycleDailyData != null && CycleData.planningCycleDailyData.Count > 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    GridColumn columnName = new GridColumn();
                    columnName.FieldName = "RowName";
                    columnName.Visible = true;
                    columnName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gvCyclePlan.Columns.Add(columnName);
                    GridColumn columnWP = new GridColumn();
                    columnWP.FieldName = "WorkplaceID";
                    columnWP.Visible = false;
                    columnWP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gvCyclePlan.Columns.Add(columnName);
                    for (int k = 1; k < CycleData.TotalDays; k++)
                    {
                        GridColumn column = new GridColumn();
                        column.Visible = true;
                        column.Width = 40;
                        column.FieldName = "RowValue_" + k;
                        column.Tag = k;
                        column.Caption = "Day" + k;
                        column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;

                        gvCyclePlan.Columns.Add(column);
                    }

                    gcCyclePlan.DataSource = CycleData.planningCycleDailyData;
                    gcCyclePlan.Refresh();

                    ColumnView view = gvCyclePlan;
                    // add a filter to hide rows that we only use for calculations 
                    view.ActiveFilter.Clear();
                    view.ActiveFilter
                        .Add(view.Columns["RowName"],
                             new ColumnFilterInfo("not [RowName]in ('AdvBlast','WorkingDays','Shift','DateLong')", ""));

                    lbxCodeCycles.ValueMember = "CycleCode";
                    lbxCodeCycles.DisplayMember = "CycleCodeAndDescription";
                    lbxCodeCycles.DataSource = PlanningCycle.cycleCodes;
                    SetBindings();
                });
            }
        }

        private void gvCyclePlan_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                //   e.Handled = true;
                var value = gvCyclePlan.GetRowCellValue(e.RowHandle, "RowName").ToString();

                var WorkingDays = CycleData.planningCycleDailyData.Where(a => a.RowName == "WorkingDays").FirstOrDefault();
                string IsWorking = WorkingDays.getValue(Convert.ToInt16(e.Column.Tag.ToString()));

                //var value = gvCyclePlan.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (IsWorking == "N")
                {

                    e.Column.AppearanceCell.BackColor = Color.Gainsboro;
                    e.Column.OptionsColumn.ReadOnly = true;
                    e.Column.OptionsColumn.AllowFocus = false;
                    e.Appearance.BackColor = Color.Gainsboro;
                    e.Appearance.ForeColor = Color.Gainsboro;
                }


                if (e.CellValue.ToString() == "BL")
                {
                    //e.Appearance.BackColor = Color.Tomato;
                    e.Appearance.ForeColor = Color.Tomato;
                }

                if (e.CellValue.ToString() == "SR")
                {
                    //e.Appearance.BackColor = Color.Tomato;
                    e.Appearance.ForeColor = Color.Tomato;
                }
                // e.Handled = false;

                if (value == "Date")
                {


                    e.Appearance.BackColor = Color.YellowGreen;
                }


                if (value == "Planned Code")
                {
                    e.Column.OptionsColumn.ReadOnly = true;
                    e.Column.OptionsColumn.AllowFocus = true;
                }
                //else
                //{
                //    e.Column.OptionsColumn.ReadOnly = true;
                //    e.Column.OptionsColumn.AllowFocus = false;
                //}

            }
            catch (Exception)
            {


            }
        }

        private void lbxCodeCycles_MouseDown(object sender, MouseEventArgs e)
        {

            if (lbxCodeCycles.Items.Count == 0)
                return;
            int index = lbxCodeCycles.IndexFromPoint(e.X, e.Y);
            if (index > -1)
            {
                string theValue = "";
                if (lbxCodeCycles.SelectedValue != null)
                {
                    theValue = lbxCodeCycles.SelectedValue.ToString();
                }

                DragDropEffects dde1 = DoDragDrop(theValue,
                    DragDropEffects.All);
            }
        }

        private void gcCyclePlan_DragDrop(object sender, DragEventArgs e)
        {
            Point p = this.gcCyclePlan.PointToClient(new Point(e.X, e.Y));
            int row = gvCyclePlan.CalcHitInfo(p.X, p.Y).RowHandle;
            var col = gvCyclePlan.CalcHitInfo(p.X, p.Y).Column;
            var theValue = e.Data.GetData(typeof(string));
            if (row > -1)
            {
                //string ColValue = gvCyclePlan.GetRowCellValue(row, col).ToString();

                var index = gvCyclePlan.GetDataSourceRowIndex(row);
                var theRowName = CycleData.planningCycleDailyData[index].RowName;



                if (theRowName == "Planned Code")
                {
                    this.gvCyclePlan.SetRowCellValue(row, gvCyclePlan.CalcHitInfo(p.X, p.Y).Column.FieldName, theValue);
                    RefreshGrid();
                    CycleData.UpdateCycleData();
                    RefreshGrid();
                    OnCycleValue();

                }
            }
        }

        public event EventHandler CycleValue;
        public void OnCycleValue()
        {
            EventHandler handler = CycleValue;
            if (null != handler) handler(this, EventArgs.Empty);
        }




        private void gcCyclePlan_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gcCyclePlan_DragOver(object sender, DragEventArgs e)
        {
            try
            {


                Point p = this.gcCyclePlan.PointToClient(new Point(e.X, e.Y));
                int row = gvCyclePlan.CalcHitInfo(p.X, p.Y).RowHandle;
                var index = gvCyclePlan.GetDataSourceRowIndex(row);
                var col = gvCyclePlan.CalcHitInfo(p.X, p.Y).Column;
                if (index > 0)
                {
                    string ColValue = "";
                    if(gvCyclePlan.GetRowCellValue(row, col) != null)
                    {
                        ColValue = gvCyclePlan.GetRowCellValue(row, col).ToString();
                    }
                    var theRowName = CycleData.planningCycleDailyData[index].RowName;
                    if (theRowName == "Planned Code" && ColValue != "OFF")
                    {
                        e.Effect = DragDropEffects.All;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }

            }
            catch (Exception)
            {


            }
        }

        private void gvCyclePlan_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var value = gvCyclePlan.GetRowCellValue(e.RowHandle, "RowName").ToString();

            var theDates = CycleData.planningCycleDailyData.Where(a => a.RowName == "DateLong").FirstOrDefault();
            string SelectedDate = theDates.getValue(Convert.ToInt16(e.Column.Tag.ToString()));

            //string test = gvCyclePlan.GetRowCellValue(e.RowHandle, gvCyclePlan.FocusedColumn).ToString();

            string SelectedValue = "";
            if(gvCyclePlan.GetRowCellValue(e.RowHandle, gvCyclePlan.FocusedColumn) != null)
            {
                 SelectedValue = gvCyclePlan.GetRowCellValue(e.RowHandle, gvCyclePlan.FocusedColumn).ToString();

            }

            if (gvCyclePlan.FocusedColumn.FieldName.ToString() == "RowName" ||
                gvCyclePlan.FocusedColumn.FieldName.ToString() == "WorkplaceID" ||
                SelectedValue  == "OFF")
            {
                return;
            }


            if (Convert.ToDateTime(SelectedDate) < Convert.ToDateTime(System.DateTime.Now))
            {
                if (UserCurrentInfo.UserID == "Kelvin")
                {

                }
                else
                {
                    MessageBox.Show("Error , Cycle cant be changed for past calendar days.");
                    return;
                }
            }

            else if (lbxCodeCycles.Text != "None" && lbxCodeCycles.Text != "Code" && value == "Planned Code")
            {
                gvCyclePlan.SetRowCellValue(e.RowHandle, gvCyclePlan.FocusedColumn, lbxCodeCycles.SelectedValue);
                RefreshGrid();
                CycleData.UpdateCycleData();
                RefreshGrid();
                OnCycleValue();
            }
        }

        private void gvCyclePlan_ShowingEditor(object sender, CancelEventArgs e)
        {
            var value = gvCyclePlan.GetRowCellValue(gvCyclePlan.FocusedRowHandle, "RowName").ToString();
            if(value != "Planned Code")
            {
                e.Cancel = true;
            }
        }
    }
}
