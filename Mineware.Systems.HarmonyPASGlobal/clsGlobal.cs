using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Printing;
using Microsoft.Office.Interop;
using System.Reflection;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;


public enum ReportTypes
{
    repDaily = 1,
    repLosses = 2,
    repMonthEndRecon = 3,
    repMonthEndReconDev = 4,
    repCrewPerformance = 5,
    repPlanChangeAudit = 6,
    repLostBlastAnal = 7,
    repCrewRanking = 8,
    repTopFiveLosses = 9,
    repProductionAnalysis = 10,
    repSBRanking = 11,
    repSBPerformance = 12,
    rep6Shift = 13,
    repPlanning = 14
}

public class Procedures
{
    private static string m_BookFrm = "";
    public static string BookFrm { get { return m_BookFrm; } set { m_BookFrm = value; } }

    private static string m_PropFrm1 = "";
    public static string PropFrm1 { get { return m_PropFrm1; } set { m_PropFrm1 = value; } }

    private static string m_ServicesFrm = "";
    public static string ServicesFrm { get { return m_ServicesFrm; } set { m_ServicesFrm = value; } }

    private static int m_Prod = 0;
    private static string m_Prod2 = "";

    public static int Prod { get { return m_Prod; } set { m_Prod = value; } }
    public static string Prod2 { get { return m_Prod2; } set { m_Prod2 = value; } }

    private static string m_MsgText = "";
    public static string MsgText { get { return m_MsgText; } set { m_MsgText = value; } }

    private static string m_MsgInfo = "";
    public static string MsgInfo { get { return m_MsgInfo; } set { m_MsgInfo = value; } }

    public string systemDBTag;
    public string connection;

    public class CalendarColumn : DataGridViewColumn
    {
        public CalendarColumn()
            : base(new CalendarCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class CalendarCell : DataGridViewTextBoxCell
    {
        public CalendarCell()
            : base()
        {
            // Use the short date format.
            this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            CalendarEditingControl ctl = DataGridView.EditingControl as CalendarEditingControl;
            // Use the default row value when Value property is null.
            if (this.Value == null)
            {
                ctl.Value = (DateTime)this.DefaultNewRowValue;
            }
            else
            {
                //  ctl.Value = (DateTime)this.Value;
                ctl.Value = Convert.ToDateTime(this.Value);
            }
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that CalendarCell uses.
                return typeof(CalendarEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.

                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
    }

    public class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        public CalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Short;
        }

        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue 
        // property.
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
            }
            set
            {
                if (value is String)
                {
                    try
                    {
                        // This will throw an exception of the string is 
                        // null, empty, or not in the format of a date.
                        this.Value = DateTime.Parse((String)value);
                    }
                    catch
                    {
                        // In the case of an exception, just use the 
                        // default value so we're not left with a null
                        // value.
                        this.Value = DateTime.Now;
                    }
                }
            }
        }

        // Implements the 
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the 
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex 
        // property.
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey 
        // method.
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit 
        // method.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        // Implements the IDataGridViewEditingControl
        // .RepositionEditingControlOnValueChange property.
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlDataGridView property.
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlValueChanged property.
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingPanelCursor property.
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell
            // have changed.
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }

    //public static void ExportToExcel(DataGridView dgv, string excel_file)
    //{
    //    Microsoft.Office.Interop.Excel.Application xlApp;
    //    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
    //    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
    //    object misValue = System.Reflection.Missing.Value;

    //    xlApp = new Microsoft.Office.Interop.Excel.Application();
    //    xlWorkBook = xlApp.Workbooks.Add(misValue);
    //    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
    //    int i = 0;
    //    int j = 0;

    //    for (j = 0; j <= dgv.ColumnCount - 1; j++)
    //    {
    //        DataGridViewCell cell = dgv.Columns[j].HeaderCell;
    //        xlWorkSheet.Cells[1, j + 1] = cell.Value;
    //    }

    //    for (i = 0; i <= dgv.RowCount - 1; i++)
    //    {
    //        for (j = 0; j <= dgv.ColumnCount - 1; j++)
    //        {
    //            DataGridViewCell cell = dgv[j, i];
    //            xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
    //        }
    //    }

    //    xlWorkBook.SaveAs(excel_file, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
    //    xlWorkBook.Close(true, misValue, misValue);
    //    xlApp.Quit();

    //    releaseObject(xlWorkSheet);
    //    releaseObject(xlWorkBook);
    //    releaseObject(xlApp);

    //    MessageBox.Show("Excel file created , you can find the file " + excel_file);
    //}

    private static void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
        }
        finally
        {
            GC.Collect();
        }
    }

    //fxn that exports content of a datagridview object to an excel file 
    public static void export_datagridview_to_excel(DataGridView dgv, string excel_file)
    {
        int cols;

        //open file
        StreamWriter wr = new StreamWriter(excel_file); //Excel_File = FileName ie "C:\\ExcelExport.xls"

        //determine the number of columns and write columns to file 
        cols = dgv.Columns.Count;
        for (int i = 0; i < cols; i++)
            wr.Write(dgv.Columns[i].Name.ToString().ToUpper() + "\t");
        wr.WriteLine();

        //write rows to excel file 
        for (int i = 0; i < (dgv.Rows.Count); i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (dgv.Rows[i].Cells[j].Value != null)
                    wr.Write(dgv.Rows[i].Cells[j].Value + "\t");
                else
                    wr.Write("\t");
            }
            wr.WriteLine();
        }

        //close file 
        wr.Close();
    }


    //Call this method as Procedures.changeMonth(ref NumericUpDownBox, ref TextBox);
    //ref Operator allows you to change the value of the sent object
    //static Operator allows you to be able to call a method without instantiating the container class
    public static void changeMonth(ref NumericUpDown box1, ref TextBox box2)
    {
        Procedures procs = new Procedures();
        procs.ProdMonthCalc(Convert.ToInt32(box1.Value.ToString()));
        box1.Text = Procedures.Prod.ToString();
        procs.ProdMonthVis(Convert.ToInt32(box1.Text));
        box2.Text = Procedures.Prod2;
    }

    public void addGridColumn(int colType, String HeaderText, String Name, DataGridView grid, int Width, Boolean Visible, Boolean Readonly, DataTable dataSource, String valueMember, String displayMember)
    {
        if (colType == 3)
        {
            CalendarColumn newCol = new CalendarColumn();
            CalendarCell newcell = new CalendarCell();
            newCol.CellTemplate = newcell;
            newCol.HeaderText = HeaderText;
            newCol.Name = Name;
            newCol.Width = Width;
            newCol.ReadOnly = Readonly;
            newCol.Visible = Visible;

            grid.Columns.Add(newCol);
        }
        else
        {
            if (colType != 1)
            {
                DataGridViewColumn newCol = new DataGridViewColumn();
                DataGridViewCell newcell = null;
                switch (colType)
                {
                    case 0:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                    case 2:
                        newcell = new DataGridViewCheckBoxCell();
                        break;
                    case 3:
                       // newcell = new PAS.PlanningFrm.CalendarCell();
                        break;
                    default:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                }
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                grid.Columns.Add(newCol);
            }
            else
            {
                DataGridViewComboBoxColumn newCol = new DataGridViewComboBoxColumn();
                DataGridViewCell newcell = new DataGridViewComboBoxCell();
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                newCol.DataSource = dataSource;
                newCol.DisplayMember = displayMember;
                newCol.ValueMember = valueMember;
                newCol.FlatStyle = FlatStyle.Flat;
                grid.Columns.Add(newCol);
            }
        }
    }

    public void addGridColumn(int colType, String HeaderText, String Name, DataGridView grid, int Width, Boolean Visible, Boolean Readonly, DataTable dataSource, String valueMember, String displayMember, DataGridViewColumnSortMode sortMode, Boolean frozen)
    {
        if (colType == 3)
        {
            CalendarColumn newCol = new CalendarColumn();
            CalendarCell newcell = new CalendarCell();
            newCol.CellTemplate = newcell;
            newCol.HeaderText = HeaderText;
            newCol.Name = Name;
            newCol.Width = Width;
            newCol.ReadOnly = Readonly;
            newCol.SortMode = sortMode;
            grid.Columns.Add(newCol);
            newCol.Frozen = frozen;
            newCol.Visible = Visible;
        }
        else
        {
            if (colType != 1)
            {
                DataGridViewColumn newCol = new DataGridViewColumn();
                DataGridViewCell newcell = null;
                switch (colType)
                {
                    case 0:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                    case 2:
                        newcell = new DataGridViewCheckBoxCell();
                        break;
                    case 3:
                      //  newcell = new PAS.PlanningFrm.CalendarCell();
                        break;
                    default:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                }
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                grid.Columns.Add(newCol);
                newCol.SortMode = sortMode;
                newCol.Frozen = frozen;
            }
            else
            {
                DataGridViewComboBoxColumn newCol = new DataGridViewComboBoxColumn();
                DataGridViewCell newcell = new DataGridViewComboBoxCell();
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                grid.Columns.Add(newCol);
                newCol.DataSource = dataSource;
                newCol.DisplayMember = displayMember;
                newCol.ValueMember = valueMember;
                newCol.SortMode = sortMode;
                newCol.FlatStyle = FlatStyle.Flat;
                newCol.Frozen = frozen;
            }
        }
    }

    public void addGridColumn(int colType, String HeaderText, String Name, DataGridView grid, int Width, Boolean Visible, Boolean Readonly, DataTable dataSource, String valueMember, String displayMember, Boolean frozen)
    {
        if (colType == 3)
        {
            CalendarColumn newCol = new CalendarColumn();
            CalendarCell newcell = new CalendarCell();
            newCol.CellTemplate = newcell;
            newCol.HeaderText = HeaderText;
            newCol.Name = Name;
            newCol.Width = Width;
            newCol.ReadOnly = Readonly;
            grid.Columns.Add(newCol);
            newCol.Frozen = frozen;
            newCol.Visible = Visible;
        }
        else
        {
            if (colType != 1)
            {
                DataGridViewColumn newCol = new DataGridViewColumn();
                DataGridViewCell newcell = null;
                switch (colType)
                {
                    case 0:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                    case 2:
                        newcell = new DataGridViewCheckBoxCell();
                        break;
                    case 3:
                       // newcell = new PAS.PlanningFrm.CalendarCell();
                        break;
                    default:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                }
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                grid.Columns.Add(newCol);
                newCol.Frozen = frozen;
            }
            else
            {
                DataGridViewComboBoxColumn newCol = new DataGridViewComboBoxColumn();
                DataGridViewCell newcell = new DataGridViewComboBoxCell();
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                grid.Columns.Add(newCol);
                newCol.DataSource = dataSource;
                newCol.DisplayMember = displayMember;
                newCol.ValueMember = valueMember;
                newCol.FlatStyle = FlatStyle.Flat;
                newCol.Frozen = frozen;
            }
        }
    }

    public void addGridColumn(int colType, String HeaderText, String Name, DataGridView grid, int Width, Boolean Visible, Boolean Readonly, DataTable dataSource, String valueMember, String displayMember, DataGridViewColumnSortMode sortMode, Boolean frozen, DataGridViewContentAlignment Alignment)
    {
        if (colType == 3)
        {
            CalendarColumn newCol = new CalendarColumn();
            CalendarCell newcell = new CalendarCell();
            newCol.CellTemplate = newcell;
            newCol.HeaderText = HeaderText;
            newCol.Name = Name;
            newCol.Width = Width;
            newCol.ReadOnly = Readonly;
            newCol.SortMode = sortMode;
            newCol.DefaultCellStyle.Alignment = Alignment;
            grid.Columns.Add(newCol);
            newCol.Frozen = frozen;
            newCol.Visible = Visible;
        }
        else
        {
            if (colType != 1)
            {
                DataGridViewColumn newCol = new DataGridViewColumn();
                DataGridViewCell newcell = null;
                switch (colType)
                {
                    case 0:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                    case 2:
                        newcell = new DataGridViewCheckBoxCell();
                        break;
                    case 3:
                       // newcell = new PAS.PlanningFrm.CalendarCell();
                        break;
                    default:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                }
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                newCol.DefaultCellStyle.Alignment = Alignment;
                grid.Columns.Add(newCol);
                newCol.SortMode = sortMode;
                newCol.Frozen = frozen;
            }
            else
            {
                DataGridViewComboBoxColumn newCol = new DataGridViewComboBoxColumn();
                DataGridViewCell newcell = new DataGridViewComboBoxCell();
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                newCol.DefaultCellStyle.Alignment = Alignment;
                grid.Columns.Add(newCol);
                newCol.DataSource = dataSource;
                newCol.DisplayMember = displayMember;
                newCol.ValueMember = valueMember;
                newCol.SortMode = sortMode;
                newCol.FlatStyle = FlatStyle.Flat;
                newCol.Frozen = frozen;
            }
        }
    }

    public void addGridColumn(int colType, String HeaderText, String Name, DataGridView grid, int Width, Boolean Visible, Boolean Readonly, DataTable dataSource, String valueMember, String displayMember, DataGridViewColumnSortMode sortMode, Boolean frozen, DataGridViewContentAlignment Alignment, Color forecolor)
    {
        if (colType == 3)
        {
            CalendarColumn newCol = new CalendarColumn();
            CalendarCell newcell = new CalendarCell();
            newCol.CellTemplate = newcell;
            newCol.HeaderText = HeaderText;
            newCol.Name = Name;
            newCol.Width = Width;
            newCol.ReadOnly = Readonly;
            newCol.SortMode = sortMode;
            newCol.DefaultCellStyle.Alignment = Alignment;
            newCol.DefaultCellStyle.ForeColor = forecolor;
            grid.Columns.Add(newCol);
            newCol.Frozen = frozen;
        }
        else
        {
            if (colType != 1)
            {
                DataGridViewColumn newCol = new DataGridViewColumn();
                DataGridViewCell newcell = null;
                switch (colType)
                {
                    case 0:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                    case 2:
                        newcell = new DataGridViewCheckBoxCell();
                        break;
                    case 3:
                      //  newcell = new PAS.PlanningFrm.CalendarCell();
                        break;
                    default:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                }
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                newCol.DefaultCellStyle.Alignment = Alignment;
                newCol.DefaultCellStyle.ForeColor = forecolor;
                grid.Columns.Add(newCol);
                newCol.SortMode = sortMode;
                newCol.Frozen = frozen;
            }
            else
            {
                DataGridViewComboBoxColumn newCol = new DataGridViewComboBoxColumn();
                DataGridViewCell newcell = new DataGridViewComboBoxCell();
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                newCol.DefaultCellStyle.Alignment = Alignment;
                newCol.DefaultCellStyle.ForeColor = forecolor;
                grid.Columns.Add(newCol);
                newCol.DataSource = dataSource;
                newCol.DisplayMember = displayMember;
                newCol.ValueMember = valueMember;
                newCol.SortMode = sortMode;
                newCol.FlatStyle = FlatStyle.Flat;
                newCol.Frozen = frozen;
            }
        }
    }

    public void addGridColumn(int colType, String HeaderText, String Name, DataGridView grid, int Width, Boolean Visible, Boolean Readonly, DataTable dataSource, String valueMember, String displayMember, DataGridViewContentAlignment alignment)
    {
        if (colType == 3)
        {
            CalendarColumn newCol = new CalendarColumn();
            CalendarCell newcell = new CalendarCell();
            newcell.Style.Alignment = alignment;
            newCol.CellTemplate = newcell;
            newCol.HeaderText = HeaderText;
            newCol.Name = Name;
            newCol.Width = Width;
            newCol.ReadOnly = Readonly;

            grid.Columns.Add(newCol);
        }
        else
        {
            if (colType != 1)
            {
                DataGridViewColumn newCol = new DataGridViewColumn();
                DataGridViewCell newcell = null;
                switch (colType)
                {
                    case 0:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                    case 2:
                        newcell = new DataGridViewCheckBoxCell();
                        break;
                    case 3:
                      //  newcell = new PAS.PlanningFrm.CalendarCell();
                        break;
                    default:
                        newcell = new DataGridViewTextBoxCell();
                        break;
                }
                newcell.Style.Alignment = alignment;
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                grid.Columns.Add(newCol);
            }
            else
            {
                DataGridViewComboBoxColumn newCol = new DataGridViewComboBoxColumn();
                DataGridViewCell newcell = new DataGridViewComboBoxCell();
                newcell.Style.Alignment = alignment;
                newCol.CellTemplate = newcell;
                newCol.HeaderText = HeaderText;
                newCol.Name = Name;
                newCol.Visible = Visible;
                newCol.ReadOnly = Readonly;
                newCol.Width = Width;
                grid.Columns.Add(newCol);
                newCol.DataSource = dataSource;
                newCol.DisplayMember = displayMember;
                newCol.ValueMember = valueMember;
                newCol.FlatStyle = FlatStyle.Flat;
            }
        }
    }

    //Production month to be used for system calculations
    public void ProdMonthCalc(int ProdMonth1)
    {
        //int Prod;
        Decimal month = Convert.ToDecimal(ProdMonth1);
        String PMonth = month.ToString();
        PMonth.Substring(4, 2);
        if (Convert.ToInt32(PMonth.Substring(4, 2)) > 12)
        {
            int M = Convert.ToInt32(PMonth.Substring(0, 4));
            M++;
            PMonth = M.ToString();
            PMonth = PMonth + "01";
            ProdMonth1 = Convert.ToInt32(PMonth);
        }
        else
        {
            if (Convert.ToInt32(PMonth.Substring(4, 2)) < 1)
            {
                int M = Convert.ToInt32(PMonth.Substring(0, 4));
                M--;
                PMonth = M.ToString();
                PMonth = PMonth + "12";
                ProdMonth1 = Convert.ToInt32(PMonth);
            }
        }
        Procedures.Prod = ProdMonth1;
    }


    public void ProdMonthVis2(int ProdMonth1)
    {


        Procedures.Prod2 = ProdMonth1.ToString().Substring(0, 4);

        if (ProdMonth1.ToString().Substring(4, 2) == "01")
        {
            Procedures.Prod2 = "Jan-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "02")
        {
            Procedures.Prod2 = "Feb-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "03")
        {
            Procedures.Prod2 = "Mar-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "04")
        {
            Procedures.Prod2 = "Apr-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "05")
        {
            Procedures.Prod2 = "May-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "06")
        {
            Procedures.Prod2 = "Jun-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "07")
        {
            Procedures.Prod2 = "Jul-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "08")
        {
            Procedures.Prod2 = "Aug-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "09")
        {
            Procedures.Prod2 = "Sep-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "10")
        {
            Procedures.Prod2 = "Oct-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "11")
        {
            Procedures.Prod2 = "Nov-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "12")
        {
            Procedures.Prod2 = "Dec-" + Procedures.Prod2;
        }
    }

    //Production month that that will be displayed on the front end
    public void ProdMonthVis(int ProdMonth1)
    {


        Procedures.Prod2 = ProdMonth1.ToString().Substring(0, 4);

        if (ProdMonth1.ToString().Substring(4, 2) == "01")
        {
            Procedures.Prod2 = "Jan-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "02")
        {
            Procedures.Prod2 = "Feb-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "03")
        {
            Procedures.Prod2 = "Mar-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "04")
        {
            Procedures.Prod2 = "Apr-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "05")
        {
            Procedures.Prod2 = "May-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "06")
        {
            Procedures.Prod2 = "Jun-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "07")
        {
            Procedures.Prod2 = "Jul-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "08")
        {
            Procedures.Prod2 = "Aug-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "09")
        {
            Procedures.Prod2 = "Sep-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "10")
        {
            Procedures.Prod2 = "Oct-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "11")
        {
            Procedures.Prod2 = "Nov-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "12")
        {
            Procedures.Prod2 = "Dec-" + Procedures.Prod2;
        }
    }

    //extracts the string value before the colon
    public string ExtractBeforeColon(string TheString)
    {
        if (TheString != "")
        {
            string BeforeColon;

            int index = TheString.IndexOf(":");
            BeforeColon = index < 0 ? TheString : TheString.Substring(0, index);

            return BeforeColon;

        }
        else
        {
            return "";
        }
    }

    //extracts the string value after the colon
    public string ExtractAfterColon(string TheString)
    {
        string AfterColon;

        int index = TheString.IndexOf(":"); // Kry die postion van die :

        AfterColon = TheString.Substring(index + 1); // kry alles na :

        return AfterColon;
    }


    public DataTable GetSections(string ProdMonth, string HierId, string SectionId)
    {


        MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
        //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
        _dbMan.ConnectionString = TConnections.GetConnectionString(systemDBTag, connection); 
      //  _dbMan.SqlStatement = " Select DISTINCT s.SectionID, s.Name,  Hierarchicalid Hier from Section s ";
        _dbMan.SqlStatement = " Select DISTINCT s.SectionID, s.SectionID + ':'+ s.Name Name from Section s ";
        _dbMan.SqlStatement += "left join Section_Complete sc on s.SectionID = sc.SECTIONID_2 and s.Prodmonth = sc.Prodmonth \r\n";
        _dbMan.SqlStatement += "left join SECCAL sec on sc.SECTIONID_1 = sec.Sectionid and sc.Prodmonth = sec.Prodmonth \r\n";
        _dbMan.SqlStatement += "where s.Prodmonth = '" + ProdMonth.ToString() + "'";
        if (HierId.ToString() != "MO")
        {
            _dbMan.SqlStatement = _dbMan.SqlStatement + " and Hierarchicalid = '" + HierId.ToString() + "' ";
        }
        if (clsUserInfo.Hier == 1)
        {
            _dbMan.SqlStatement += "and s.Sectionid like '" + SectionId.ToString() + "%' ";
        }
        if (clsUserInfo.Hier == 2)
        {
            _dbMan.SqlStatement += "and sc.Sectionid_4 =  '" + clsUserInfo.UserBookSection + "'";
        }
        if (clsUserInfo.Hier == 3)
        {
            _dbMan.SqlStatement += "and sc.Sectionid_3 =  '" + clsUserInfo.UserBookSection + "'";
        }
        if (clsUserInfo.Hier == 4)
        {
            _dbMan.SqlStatement += "and sc.Sectionid_2 =  '" + clsUserInfo.UserBookSection + "'";
        }
        if (clsUserInfo.Hier == 5)
        {
            _dbMan.SqlStatement += "and sc.Sectionid_1 =  '" + clsUserInfo.UserBookSection + "'";
        }
        if (clsUserInfo.Hier == 6)
        {
            _dbMan.SqlStatement += "and sc.Sectionid =  '" + clsUserInfo.UserBookSection + "'";
        }


        _dbMan.SqlStatement = _dbMan.SqlStatement + " order by s.SECTIONid ";
        _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
        _dbMan.ResultsTableName = "GetSections";
        _dbMan.ExecuteInstruction();

        DataTable dt1 = _dbMan.ResultsDataTable;
        return dt1;
    }

    public DataView Search(DataTable SearchTable, string SearchString)
    {
        DataView dv = new DataView(SearchTable);
        string SearchExpression = null;

        if (!String.IsNullOrEmpty(SearchString))//(Filtertxt.Text))
        {
            SearchExpression = string.Format("'{0}%'", SearchString);//Filtertxt.Text);
            dv.RowFilter = "Description like " + SearchExpression;
        }
        return dv;
    }

    public DataView Search(DataTable SearchTable, string SearchString, String IDName)
    {
        DataView dv = new DataView(SearchTable);
        if (IDName == "ID")
        {
            string SearchExpression = null;

            if (!String.IsNullOrEmpty(SearchString))//(Filtertxt.Text))
            {
                SearchExpression = string.Format("'{0}%'", SearchString);//Filtertxt.Text);
                dv.RowFilter = "ID like " + SearchExpression;
            }
        }

        if (IDName == "Name")
        {
            string SearchExpression = null;

            if (!String.IsNullOrEmpty(SearchString))//(Filtertxt.Text))
            {
                SearchExpression = string.Format("'{0}%'", SearchString);//Filtertxt.Text);
                dv.RowFilter = "Description like " + SearchExpression;
            }
        }
        return dv;
    }
}

public class SysSettings
{
    private static int m_ProdMonth = 0;
    private static int m_MillMonth = 0;
    private static string m_Banner = "";
    private static Decimal m_StdAdv = 0;
    private static string m_CheckMeas = "";
    private static string M_PlanType = "";
    private static string M_CleanShift = "";
    private static string M_AdjBook = "";
    private static int M_BlastQual = 0;
    private static string M_DSOrg = "N";
    private static string M_CHkMeasLevel = "MO";
    private static string M_PlanNotes = "";
    private static string M_CurDir = "";
    private static Decimal m_RockDensity = 0;
    private static Decimal m_BrokenRockDensity = 0;
    private static int m_IsCentralized = 0;



    public static int ProdMonth { get { return m_ProdMonth; } set { m_ProdMonth = value; } }
    public static int MillMonth { get { return m_MillMonth; } set { m_MillMonth = value; } }
    public static string Banner { get { return m_Banner; } set { m_Banner = value; } }
    public static Decimal StdAdv { get { return m_StdAdv; } set { m_StdAdv = value; } }
    public static string CheckMeas { get { return m_CheckMeas; } set { m_CheckMeas = value; } }
    public static string PlanType { get { return M_PlanType; } set { M_PlanType = value; } }
    public static string CleanShift { get { return M_CleanShift; } set { M_CleanShift = value; } }
    public static string AdjBook { get { return M_AdjBook; } set { M_AdjBook = value; } }
    public static int BlastQual { get { return M_BlastQual; } set { M_BlastQual = value; } }
    public static string DSOrg { get { return M_DSOrg; } set { M_DSOrg = value; } }
    public static string CHkMeasLevel { get { return M_CHkMeasLevel; } set { M_CHkMeasLevel = value; } }
    public static string PlanNotes { get { return M_PlanNotes; } set { M_PlanNotes = value; } }
    public static string CurDir { get { return M_CurDir; } set { M_CurDir = value; } }
    public static Decimal RockDensity { get { return m_RockDensity; } set { m_RockDensity = value; } }
    public static Decimal BrokenRockDensity { get { return m_BrokenRockDensity; } set { m_BrokenRockDensity = value; } }
    public static int IsCentralized { get { return m_IsCentralized; } set { m_IsCentralized = value; } }

    public string systemDBTag;
    public string connection;
    //gets all the fields from the SYSSET table

    public bool CheckSec(string UserID, string FormID)
    {
        MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
        //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];

        _dbMan.SqlStatement = "select AccessLevel " +
                              " from  UserFormsLink where UserID = (select useridnum from cpmusers where userid ='" + UserID + "') and " +
                              "  FormID = (select formid from forms where formcode = '" + FormID + "') ";
        _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
        _dbMan.ExecuteInstruction();
        DataTable SubB = _dbMan.ResultsDataTable;
        try
        {
            if (SubB.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch { return false; }
    }


    public void GetSysSettings()
    {
        MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
        _dbMan.ConnectionString = TConnections.GetConnectionString(systemDBTag, connection);
        //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];

        _dbMan.SqlStatement = "select * from sysset ";
        _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
        _dbMan.ExecuteInstruction();
        DataTable SubB = _dbMan.ResultsDataTable;


        SysSettings.ProdMonth = Convert.ToInt32(SubB.Rows[0]["currentproductionmonth"].ToString());
        SysSettings.MillMonth = Convert.ToInt32(SubB.Rows[0]["currentmillmonth"].ToString());
        SysSettings.Banner = SubB.Rows[0]["Banner"].ToString();
        SysSettings.StdAdv = Convert.ToDecimal(SubB.Rows[0]["stpadv"].ToString());
        SysSettings.CheckMeas = SubB.Rows[0]["CheckMeas"].ToString();
        SysSettings.PlanType = SubB.Rows[0]["PlanType"].ToString();
        SysSettings.CleanShift = SubB.Rows[0]["CleanShift"].ToString();
        SysSettings.AdjBook = SubB.Rows[0]["AdjBook"].ToString();
        SysSettings.BlastQual = Convert.ToInt32(Math.Round(Convert.ToDecimal(SubB.Rows[0]["percblastqualification"].ToString()), 0));
        SysSettings.DSOrg = SubB.Rows[0]["dsorg"].ToString();
        SysSettings.CHkMeasLevel = SubB.Rows[0]["checkmeaslvl"].ToString();
        SysSettings.PlanNotes = SubB.Rows[0]["PlanNotes"].ToString();
        SysSettings.RockDensity = Convert.ToDecimal(SubB.Rows[0]["RockDensity"].ToString());
        SysSettings.BrokenRockDensity = Convert.ToDecimal(SubB.Rows[0]["BROKENROCKDENSITY"].ToString());
        SysSettings.IsCentralized = Convert.ToInt32(SubB.Rows[0]["IsCentralizedDatabase"].ToString());
        SysSettings.CurDir = SubB.Rows[0]["REPDIR"].ToString();
    }

    //sets the logged on user information
    public void SetUserInfo()
    {
        MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
        //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];

        _dbMan.SqlStatement = "select c.*, b.hierarchicalid from CpmUsers c " +
                              "left outer join (select * from Section where prodmonth = (select currentproductionmonth from sysset)) b " +
                              "on c.passectionid = b.sectionid " +
                              "where UserID = '" + clsUserInfo.UserID + "'";
        _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
        _dbMan.ExecuteInstruction();
        DataTable SubA = _dbMan.ResultsDataTable;

        clsUserInfo.UserName = SubA.Rows[0]["username"].ToString();
        clsUserInfo.UserBookSection = SubA.Rows[0]["Passectionid"].ToString();
        clsUserInfo.Hier = Convert.ToInt32(SubA.Rows[0]["hierarchicalid"] == DBNull.Value ? 1 : SubA.Rows[0]["hierarchicalid"]);

        clsUserInfo.Tram = SubA.Rows[0]["Tram"].ToString();
        clsUserInfo.Hoist = SubA.Rows[0]["Hoist"].ToString();
        clsUserInfo.mill = SubA.Rows[0]["mill"].ToString();
        clsUserInfo.book = SubA.Rows[0]["pasbook"].ToString();
        clsUserInfo.dropraise = SubA.Rows[0]["dropraise"].ToString();
        clsUserInfo.sys = SubA.Rows[0]["systemadmin"].ToString();
        clsUserInfo.plan = SubA.Rows[0]["pasplan"].ToString();
        clsUserInfo.samp = SubA.Rows[0]["sampling"].ToString();
        clsUserInfo.Surv = SubA.Rows[0]["survey"].ToString();
        clsUserInfo.Expl = SubA.Rows[0]["Explosive"].ToString();
        clsUserInfo.WPProduction = SubA.Rows[0]["WPProduction"].ToString();
        clsUserInfo.WPSurface = SubA.Rows[0]["WPSurface"].ToString();
        clsUserInfo.WPUnderground = SubA.Rows[0]["WPUnderground"].ToString();
        clsUserInfo.WPEditName = SubA.Rows[0]["WPEditName"].ToString();
        clsUserInfo.WPEditAttribute = SubA.Rows[0]["WPEditAttribute"].ToString();
        clsUserInfo.WPClassify = SubA.Rows[0]["WPClassify"].ToString();
        clsUserInfo.WPGiveAccess = SubA.Rows[0]["GiveAccess"].ToString();

        clsUserInfo.BackDateBooking = SubA.Rows[0]["BackDateBook"].ToString();
        if (SubA.Rows[0]["BackDateBookDays"] != DBNull.Value)
            clsUserInfo.BackDateBookingDays = (SubA.Rows[0]["BackDateBookDays"].ToString());
    }


    public String encrypt(String Decrypted)
    {
        String Encrypted = "";
        for (int i = 0; i < Decrypted.Length; i++)
        {
            if ((i % 2) == 0)
            {
                Encrypted += (char)((int)Decrypted[i] - 3);
            }
            else
            {
                Encrypted += (char)((int)Decrypted[i] + 3);
            }
        }
        return Encrypted;
    }

    public String decrypt(String Encrypted)
    {
        String Decrypted = "";
        for (int i = 0; i < Encrypted.Length; i++)
        {
            if ((i % 2) == 0)
            {
                Decrypted += (char)((int)Encrypted[i] + 3);
            }
            else
            {
                Decrypted += (char)((int)Encrypted[i] - 3);
            }
        }
        return Decrypted;
    }



}

public class clsValidations
{
    public enum ValidationType
    {
        MWInteger5,
        MWInteger12,
        MWCleanText,
        MWDouble5D1,
        MWDouble5D2,
        MWDouble12D2,
        MWDouble5D3,
        MWDouble5D4,
        MWBinary,
        MWDate
    }
    public ValidationType MWValidationType;

    public clsValidations()
    {
    }

    public string _MWInput;
    public string MWInput { set { _MWInput = value; } }

    public bool Validate()
    {
        try
        {
            switch (MWValidationType)
            {
                case ValidationType.MWCleanText:

                    // Clean Text without ' and / \ only a-z A-Z and -

                    Regex ValFactor1 = new Regex(@"^\s*[a-zA-Z0-9,\s\-]+\s*$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                case ValidationType.MWDate:
                    break;
                case ValidationType.MWInteger5:

                    // Limits to 5 left

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }
                case ValidationType.MWInteger12:

                    // Limits to 12 left

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,12}$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                case ValidationType.MWBinary:

                    // Limits to 1 left

                    ValFactor1 = new Regex(@"(^([0]|[1])$)\d{0,1}$"); //Regex(@"^(?=.*[1]?.*$)\d{0,1}$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                case ValidationType.MWDouble5D1:

                    // Limits to 5 left and only 1 decimal

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,1})?$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                case ValidationType.MWDouble5D2:

                    // Limits to 5 left and only 2 decimals

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,2})?$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                case ValidationType.MWDouble12D2:

                    // Limits to 12 left and only 2 decimals

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,12}(?:\.\d{0,2})?$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }
                case ValidationType.MWDouble5D3:

                    // Limits to 5 left and only 1 decimals

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,3})?$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                case ValidationType.MWDouble5D4:

                    // Limits to 5 left and only 1 decimals

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,4})?$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }


            }
            return true;
        }
        catch
        {
            return false;
        }
    }

}
public class AuditActions
{
    public const string Login = "Log in";
    public const string UserCreated = "User Created";
    public const string UserAccessChanged = "User Access Changed";
    public const string UserStatusChanged = "User Status Changed";
    public const string UserAccountUnlocked = "User Account Unlocked";
    public const string UserPasswordReset = "User Password Reset";
}

public class Audit
{

    public static void LogAudit(string action, string userId, string affectedUserId)
    {
        var _dbMan = new MWDataManager.clsDataAccess
        {
            queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement,
            queryReturnType = MWDataManager.ReturnType.DataTable,
            SqlStatement = String.Format("SELECT * FROM AuditActions WHERE ActionName='{0}';", action)
        };
        _dbMan.ExecuteInstruction();
        if (_dbMan.ResultsDataTable.Rows.Count > 0)
        {
            _dbMan.SqlStatement = String.Format("INSERT INTO Audit(Timestamp, [User], DomainUsername, HostName, AffectedUserId, Action) VALUES(GETDATE(),'{0}','{1}','{2}','{3}','{4}');", userId, Environment.UserName, Environment.MachineName, affectedUserId, _dbMan.ResultsDataTable.Rows[0]["ActionId"]);
            _dbMan.ExecuteInstruction();
        }
    }
}

public class Form : System.Windows.Forms.Form
{
    protected override void OnLoad(EventArgs e)
    {
      //  this.Icon = PAS.Properties.Resources.PASICONNew;
      //  base.OnLoad(e);
    }
}
