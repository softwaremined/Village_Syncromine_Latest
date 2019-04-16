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

namespace Mineware.Systems.Production.SysAdminScreens.ProcessCodes
{
    public partial class ucProcessCodes : ucBaseUserControl
    {
        string theAction = "";
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        public ucProcessCodes()
        {
            InitializeComponent();
        }

        private void ucProcessCodes_Load(object sender, EventArgs e)
        {

            loaddata();
        }

        public void loaddata()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " SELECT * FROM StopingProcessCodes ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable pccode = new DataTable();
            pccode = _dbMan.ResultsDataTable;
            gcProcessCode.DataSource = pccode;
        }

        private void gvProcessCode_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            string NAME = "";
            string ID = "";
            if (gvProcessCode.IsNewItemRow(e.RowHandle))
            {
                object name = e.Row as DataRowView;
                 NAME = Convert.ToString (((DataRowView)name)["Name"]);

            }
            else
            {
                object name = gvProcessCode.GetRowCellValue(gvProcessCode.FocusedRowHandle, gvProcessCode.Columns["Name"]);
                 NAME = Convert.ToString(name);
            }

            if (gvProcessCode.IsNewItemRow(e.RowHandle))
            {
                object id = e.Row as DataRowView;
                ID = Convert.ToString(((DataRowView)id)["Id"]);
            }
            else
            {
                object id = gvProcessCode.GetRowCellValue(gvProcessCode.FocusedRowHandle, gvProcessCode.Columns["Id"]);
                ID = Convert.ToString(id);
            }


            MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
            _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan2.SqlStatement = "select * from  StopingProcessCodes where name='" + NAME + "' ";
            _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan2.ExecuteInstruction();
            DataTable dup = new DataTable();
            dup = _dbMan2.ResultsDataTable;
            if (dup .Rows .Count >0)
            {
                _sysMessagesClass.viewMessage(MessageType.Info, "Info", "Name already exists", ButtonTypes.OK, MessageDisplayType.Small);
                gcProcessCode.DataSource = "";
                loaddata();
                return;
            }

            if (theAction == "Edit")
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = "update  StopingProcessCodes set name='"+NAME +"' where Id='" + ID + "'";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Process Codes data Saved", Color.CornflowerBlue);
            }

            if (theAction == "Add")
            {
                //int MaxIDCommon = 1;
                //MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                //_dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbMan1.SqlStatement = "select max(Id) MaxID from  StopingProcessCodes";
                //_dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbMan1.ExecuteInstruction();
                //if (_dbMan1.ResultsDataTable.Rows.Count > 0)
                //{
                //    if (_dbMan1.ResultsDataTable.Rows[0]["MaxID"].ToString() == "" || _dbMan1.ResultsDataTable.Rows[0]["MaxID"].ToString() == "NULL" || _dbMan1.ResultsDataTable.Rows[0]["MaxID"].ToString() is DBNull)

                //        MaxIDCommon = 1;
                //    else
                //    {
                //        MaxIDCommon = Convert.ToInt32(_dbMan1.ResultsDataTable.Rows[0]["MaxID"].ToString()) + 1;
                //    }
                //}

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = "insert into  StopingProcessCodes values('"+NAME +"') ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();
                loaddata();
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Process Codes data Saved", Color.CornflowerBlue);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gvProcessCode.AddNewRow();
            gvProcessCode.ShowEditForm();
            theAction = "Add";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            theAction = "Edit";
            gvProcessCode.ShowEditForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            object  name = gvProcessCode.GetRowCellValue(gvProcessCode.FocusedRowHandle, gvProcessCode.Columns["Name"]);
            string NAME = Convert.ToString(name);

            if (NAME == "")
            {
                MessageBox.Show("Please select a record to be deleted.", "No record selected", MessageBoxButtons.OK);
            }
            else if (MessageBox.Show("Are you sure you want to delete record", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = "delete FROM StopingProcessCodes where name='" + NAME + "'";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();
                loaddata();
            }

        }

        private void gvProcessCode_DoubleClick(object sender, EventArgs e)
        {
            theAction = "Edit";
        }
    }
}
