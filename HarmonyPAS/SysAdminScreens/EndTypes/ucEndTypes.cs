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
using DevExpress.XtraBars;
using DevExpress.Utils;
using DevExpress.XtraNavBar;
using DevExpress.XtraEditors.ViewInfo;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.EndTypes
{
    public partial class ucEndTypes : ucBaseUserControl
    {
        clsEndTypes _clsEndTypes = new clsEndTypes();
        ileEndTypes myEdit = new ileEndTypes();
        public ucEndTypes()
        {
            InitializeComponent();
        }

        private void gcEndTypes_Load(object sender, EventArgs e)
        {
            loadData();
        }

        public void loadData()
        {
            //_clsEndTypes.theData.ConnectionString = TConnections.GetConnectionString(this.theSystemDBTag, this.UserCurrentInfo.Connection);
            _clsEndTypes.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            DataTable Endtypes = new DataTable();
            Endtypes = _clsEndTypes.loadEndTypes();
            gcEndTypes.DataSource = Endtypes;

            myEdit.UserCurrentInfo = this.UserCurrentInfo;
            myEdit.theSystemDBTag = this.theSystemDBTag;
            // myEdit.Tag = editProdmonth.EditValue.ToString();
            gvEndTypes.OptionsEditForm.CustomEditFormLayout = myEdit;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            gvEndTypes.ShowEditForm();
        }

        private void gvEndTypes_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            object endtypeid = gvEndTypes.GetRowCellValue(gvEndTypes.FocusedRowHandle, gvEndTypes.Columns["EndTypeID"]);
            string ENDTYPEID = Convert.ToString(endtypeid);

            object desc = gvEndTypes.GetRowCellValue(gvEndTypes.FocusedRowHandle, gvEndTypes.Columns["Description"]);
            string DESC = Convert.ToString(desc);

            object processcode = gvEndTypes.GetRowCellValue(gvEndTypes.FocusedRowHandle, gvEndTypes.Columns["ProcessCode"]);
            string PROCESSCODE = Convert.ToString(processcode);

            object endheight = gvEndTypes.GetRowCellValue(gvEndTypes.FocusedRowHandle, gvEndTypes.Columns["EndHeight"]);
            string ENDHEIGHT = Convert.ToString(endheight);

            object endwidth = gvEndTypes.GetRowCellValue(gvEndTypes.FocusedRowHandle, gvEndTypes.Columns["EndWidth"]);
            string ENDWIDTH = Convert.ToString(endwidth);

            object rw = gvEndTypes.GetRowCellValue(gvEndTypes.FocusedRowHandle, gvEndTypes.Columns["ReefWaste"]);
            string RW = Convert.ToString(rw);

            object detratio = gvEndTypes.GetRowCellValue(gvEndTypes.FocusedRowHandle, gvEndTypes.Columns["DetRatio"]);
            string DETRATIO = Convert.ToString(detratio);

            object rate = gvEndTypes.GetRowCellValue(gvEndTypes.FocusedRowHandle, gvEndTypes.Columns["Rate"]);
            string RATE = Convert.ToString(rate);


            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " update ENDTYPE set  " +
                                    " Description = '" + DESC  + "', " +
                                    " EndHeight = '" + ENDHEIGHT + "', EndWidth = '" + ENDWIDTH  + "', " +
                                    " ProcessCode = '" + PROCESSCODE + "', " +
                                    " ReefWaste = '" + RW + "', " +
                                    "Rate = '" + RATE  + "', detratio = " + DETRATIO  + " " +
                                     "where EndTypeID = '" + ENDTYPEID + "'  ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CanClose = true;

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
