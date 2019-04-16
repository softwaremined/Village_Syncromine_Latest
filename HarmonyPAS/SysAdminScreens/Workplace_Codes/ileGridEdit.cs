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
using DevExpress.XtraGrid;
using Mineware.Systems.Global;
using DevExpress.XtraGrid.Columns;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Workplace_Codes
{
    public partial class ileGridEdit : EditFormUserControl 
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsWorkplaceCodes _clsWorkplaceCodes  = new clsWorkplaceCodes();
        public string result = "false";
        //ucWorkplace_Codes _ucWorkplace_Codes = new ucWorkplace_Codes();
        public string update = "";
        public ileGridEdit()
        {
            InitializeComponent();
        }

        void loaddata()
        {
            _clsWorkplaceCodes.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            lkpDivision.Properties.DataSource = _clsWorkplaceCodes.loadDivision();
            lkpDivision.Properties.ValueMember = "DivisionCode";
            lkpDivision.Properties.DisplayMember = "DivisionCodeDescription";
            lkpDivision.Focus();
        }

        private void frmGridEdit_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        public  void btnUpdate_Click(object sender, EventArgs e)
        {
            _clsWorkplaceCodes.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //BindingSource bs = (BindingSource)gvGridEdit.DataSource;
            DataTable gridedit = new DataTable();
           // DataTable dt = new DataTable();
            foreach (GridColumn column in gvGridEdit  .VisibleColumns)
            {
                gridedit.Columns.Add(column.FieldName, column.ColumnType);
            }
            for (int i = 0; i < gvGridEdit.DataRowCount; i++)
            {
                DataRow row = gridedit.NewRow();
                foreach (GridColumn column in gvGridEdit.VisibleColumns)
                {
                    row[column.FieldName] = gvGridEdit.GetRowCellValue(i, column);
                }
                gridedit.Rows.Add(row);
            }
            _clsWorkplaceCodes.savedata(update, lkpDivision.EditValue.ToString(), txtGrid.Text, txtDescription.Text, txtCostarea.Text, gridedit);
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("SAVED DATA", "All data was saved successfully", System.Drawing.Color.Blue);
            //_ucWorkplace_Codes.loaddata();
            result = "true";
            //this.ileGridEdit_Leave(null,null);
           
           
          

            //_uc.loaddata();
            //this.End();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ileGridEdit_Leave(object sender, EventArgs e)
        {
            //this.gvGridEdit.CloseEditForm();
            //var aa = new ucWorkplace_Codes();
            //aa.loaddata();
        }
    }
}
