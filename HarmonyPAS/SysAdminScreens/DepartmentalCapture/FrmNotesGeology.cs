using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.GlobalConnect;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using Mineware.Systems.Global;
using DevExpress.XtraGrid.Views.Base;

using System.Threading;
using System.Globalization;
using Newtonsoft.Json;

using Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPdfViewer;
using DevExpress.Pdf;

using System.IO;
using System.Text.RegularExpressions;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class FrmNotesGeology : DevExpress.XtraEditors.XtraForm
    {


        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        public FrmNotesGeology()
        {
            InitializeComponent();
        }

        private void FrmNotesGeology_Load(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManWPST3 = new MWDataManager.clsDataAccess();
            _dbManWPST3.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST3.SqlStatement = "  select Nodeid from dbo.tbl_GeoScience_PlanLongTerm " +

                                        " where Workplace = '" + WPlabel2.Text + "' " +
                                        "  and HoleNo = '" + BHLbl2.Text + "' " +
                                        "  " +
                                        "  ";

            _dbManWPST3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST3.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST3.ResultsTableName = "Table5";
            _dbManWPST3.ExecuteInstruction();


            DataTable dt = _dbManWPST3.ResultsDataTable;

            //NodeID.Text = "";

            if (dt.Rows.Count > 0)
            {

                NodeID.Text = dt.Rows[0]["Nodeid"].ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManWPST5 = new MWDataManager.clsDataAccess();
            _dbManWPST5.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST5.SqlStatement = "insert into tbl_Geology_WorkOrder_Note values ('" + NodeID.Text + "','" + DateTxt.Text + "',  '" + NotesTxt.Text + "', null , getdate(), '" + TUserInfo.UserID + "' )  ";

            _dbManWPST5.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST5.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbManWPST5.ResultsTableName = "Table30";
            _dbManWPST5.ExecuteInstruction();
        }
    }
}
