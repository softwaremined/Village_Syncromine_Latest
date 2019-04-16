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
    public partial class FrmNotesDrillPTO : Form
    {

        string BusUnit = "";
        string aaa = "";
        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        public FrmNotesDrillPTO()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManWPST5 = new MWDataManager.clsDataAccess();
            _dbManWPST5.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST5.SqlStatement = "insert into tbl_Geology_PlanTaskObservation_Note values ('" + NodeID.Text + "','" + DateTxt.Text + "',  '" + NotesTxt.Text + "', '" + BusUnit + "' , getdate(), '" + TUserInfo.UserID + "' )  ";

            _dbManWPST5.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST5.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbManWPST5.ResultsTableName = "Table30";
            _dbManWPST5.ExecuteInstruction();

            FrmNotesDrillPTO_Load(null, null);
        }

        private void FrmNotesDrillPTO_Load(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManWPST1 = new MWDataManager.clsDataAccess();
            _dbManWPST1.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST1.SqlStatement = " select '" + WPlabel.Text + "' wp, CONVERT(VARCHAR(11),getdate(),111) date " +

                                        "  , '" + BHLbl.Text + "' BH, DATEPART(ISOWK,Getdate()) wk1, '" + BHLbl.Text + "'+convert(varchar(10),DATEPART(ISOWK,Getdate())) +'                             ' wordno  " +
                                        " ,'" + NodeID.Text + "'+convert(Varchar(10),DATEPART(ISOWK,Getdate())) aa " +
                                        " , '" + BusUnit + "' BusUnit  " +
                                        "  ";

            _dbManWPST1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST1.ResultsTableName = "Table1";
            _dbManWPST1.ExecuteInstruction();

            DataTable dt = _dbManWPST1.ResultsDataTable;

            foreach (DataRow dr in dt.Rows)
            {
                aaa = dr["aa"].ToString() + "                                      ";
            }


            MWDataManager.clsDataAccess _dbManWPST11 = new MWDataManager.clsDataAccess();
            _dbManWPST11.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST11.SqlStatement = "  select *, '" + aaa + "' WONo from  tbl_Geology_PlanTaskObservation_Note       " +

                                        "  where NodeID =  '" + NodeID.Text + "' " +
                                        " " +
                                        " and NoteDate = (select MAX(NoteDate) from tbl_Geology_PlanTaskObservation_Note where nodeid = '" + NodeID.Text + "' ) " +
                                        "  ";

            _dbManWPST11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST11.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST11.ResultsTableName = "Table2";
            _dbManWPST11.ExecuteInstruction();



            DataTable dt2 = _dbManWPST11.ResultsDataTable;


            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    NotesTxt.Text = dr2["Note"].ToString();
                }
            }




            MWDataManager.clsDataAccess _dbManWPST3 = new MWDataManager.clsDataAccess();
            _dbManWPST3.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST3.SqlStatement = "select  convert(varchar(100), min(CalendarDate),121 ) dd, '" + aaa + "' WONo  from tbl_Planning where CalendarDate > GETDATE() " +
                                       " and  datename(dw,calendardate) = 'Friday'  ";

            _dbManWPST3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST3.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST3.ResultsTableName = "Table3";
            _dbManWPST3.ExecuteInstruction();

            DataTable dtDate = _dbManWPST3.ResultsDataTable;



            DateTxt.Text = "";

            foreach (DataRow dr in dtDate.Rows)
            {
                DateTxt.Text = dr["dd"].ToString();
            }
        }
    }
}
