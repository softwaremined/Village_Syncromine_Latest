using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning
{
    public partial class ucUnApprovePlanning : ucBaseUserControl
    {
        DataTable theUnApproveData = new DataTable();
        public ucUnApprovePlanning()
        {
            InitializeComponent();
        }

        public bool unApproveSelected()
        {
            MWDataManager.clsDataAccess _UnApproveData = new MWDataManager.clsDataAccess();
            _UnApproveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _UnApproveData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _UnApproveData.queryReturnType = MWDataManager.ReturnType.longNumber;
            _UnApproveData.SqlStatement = "sp_PrePlanning_Unapprove";

            if (editReason.Text == "")
            {
                MessageBox.Show("Provide a Reason for this Un-Approve procedure.");
                return false;
            }
            else
            {
                foreach (DataRow row in theUnApproveData.Rows)
                {
                    try
                    {
                        if (Convert.ToBoolean(row["Selected"].ToString()) == true)
                        {
                            // Unapprove all dinamic plan
                            SqlParameter[] _paramCollection = 
                                                    {                                   
                                                    _UnApproveData.CreateParameter("@Prodmonth",SqlDbType.VarChar, 20,row["Prodmonth"].ToString()),
                                                    _UnApproveData.CreateParameter("@WorkplaceID", SqlDbType.VarChar, 20,row["Workplaceid"]),
                                                    _UnApproveData.CreateParameter("@Activitycode", SqlDbType.Int, 0,row["Activity"]),
                                                    _UnApproveData.CreateParameter("@Sectionid", SqlDbType.VarChar, 20,row["Sectionid"]),                                                    
                                                    _UnApproveData.CreateParameter("@CurrentUser", SqlDbType.VarChar,20,TUserInfo.UserID),
                                                    _UnApproveData.CreateParameter("@Reason", SqlDbType.VarChar,0,editReason.Text),
                                                    
                                                    };
                            _UnApproveData.ParamCollection = _paramCollection;
                            var result = _UnApproveData.ExecuteInstruction();

                            if (Convert.ToDecimal(row["CubicMetres"].ToString()) > 0)
                            {
                               // unapprove cube
                                SqlParameter[] _paramCollection1 = 
                                                    {                                   
                                                    _UnApproveData.CreateParameter("@Prodmonth",SqlDbType.VarChar, 20,row["Prodmonth"].ToString()),
                                                    _UnApproveData.CreateParameter("@WorkplaceID", SqlDbType.VarChar, 20,row["Workplaceid"]),
                                                    _UnApproveData.CreateParameter("@Activitycode", SqlDbType.Int, 0,7),
                                                    _UnApproveData.CreateParameter("@Sectionid", SqlDbType.VarChar, 20,row["Sectionid"]),                                                    
                                                    _UnApproveData.CreateParameter("@CurrentUser", SqlDbType.VarChar,20,TUserInfo.UserID),
                                                    _UnApproveData.CreateParameter("@Reason", SqlDbType.VarChar,0,editReason.Text),
                                                    
                                                    };
                            _UnApproveData.ParamCollection = _paramCollection1;
                            _UnApproveData.ExecuteInstruction();

                            }
                        }
                    }
                    catch (Exception _exception)
                    {
                        throw new ApplicationException(_exception.Message, _exception);
                    }

                    
                }

                return true;
            }
        }

        public bool loadUnApprovePlanning(int Prodmonth, string SectionID_2, int Activity)
        {
            MWDataManager.clsDataAccess _ApprovedInfo = new MWDataManager.clsDataAccess();
            _ApprovedInfo.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _ApprovedInfo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _ApprovedInfo.queryReturnType = MWDataManager.ReturnType.DataTable;
            _ApprovedInfo.SqlStatement = "SELECT distinct a.[Prodmonth],a.[Sectionid],[Sectionid_2],c.Description [WorkplaceDesc],a.[Activity],a.[Workplaceid],\n"+
                                         "cast(0 as bit) [Selected],CubicMetres FROM planmonth a inner join Section_complete b on\n" +
                                         " a.Prodmonth = b.Prodmonth and \n" +
                                         " a.SectionID = b.SectionID \n" +
                                         " inner join Workplace c on\n" +
                                         " a.Workplaceid = c.Workplaceid \n" +
                                         " WHERE a.Prodmonth = " + Prodmonth.ToString() + " and \n" +
                                         "      SectionID_2 = '" + SectionID_2 + "' and \n" +
                                         "      a.Activity = " + Activity.ToString() + " and \n" +
                                         "      Locked = 1";
            _ApprovedInfo.ExecuteInstruction();

            if (_ApprovedInfo.ResultsDataTable.Rows.Count > 0)
            {
                theUnApproveData = _ApprovedInfo.ResultsDataTable.Clone();
                foreach (DataRow row in _ApprovedInfo.ResultsDataTable.Rows)
                {
                    theUnApproveData.ImportRow(row);
                }
                gcWorkplaceList.DataSource = theUnApproveData;
                return true;
            }
            else { return false; }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow r in theUnApproveData.Rows)
            {
                r["Selected"] = checkEdit1.Checked;
                if (checkEdit1.Checked == true)
                {
                    checkEdit1.Text = "Un select All";
                }
                else { checkEdit1.Text = "Select All"; }
            }
        }
    }
}
