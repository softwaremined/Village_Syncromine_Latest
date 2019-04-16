using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using System.Windows.Forms;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning.HR;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.HR
{
    public partial class frmHRDesignations :Form  
    {
        DataTable theAvalibalData = new DataTable();
        DataTable theSelectedData = new DataTable();
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        public frmHRDesignations()
        {
            InitializeComponent();
        }

        private void loadData(int TargetID)
        {
            MWDataManager.clsDataAccess _theAvalibalData = new MWDataManager.clsDataAccess();
            _theAvalibalData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _theAvalibalData.SqlStatement = "sp_HR_Designations";
            _theAvalibalData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _theAvalibalData.queryReturnType = MWDataManager.ReturnType.DataTable;

            SqlParameter[] _paramCollection = 
             {                                   
                 _theAvalibalData.CreateParameter("@TargetID", SqlDbType.VarChar, 10,TargetID.ToString()),
             };

            _theAvalibalData.ParamCollection = _paramCollection;

            _theAvalibalData.ExecuteInstruction();
            theAvalibalData = _theAvalibalData.ResultsDataTable.Copy();

            gcAvalibalData.DataSource = theAvalibalData;

            MWDataManager.clsDataAccess _theSelectedData = new MWDataManager.clsDataAccess();
            _theSelectedData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _theSelectedData.SqlStatement = "SELECT Designation FROM HRSTDNORMDESIGNATION WHERE TargetID = " + TargetID.ToString();
            _theSelectedData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _theSelectedData.queryReturnType = MWDataManager.ReturnType.DataTable;

            gcSelectedData.DataSource = _theSelectedData.ResultsDataTable;

            _theSelectedData.ExecuteInstruction();
            theSelectedData = _theSelectedData.ResultsDataTable.Copy();

            gcSelectedData.DataSource = theSelectedData;

        }

        public DataTable loadDesignations(int TargetID)
        {
            loadData(TargetID);
            this.ShowDialog();
            return theSelectedData;
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int currentRow = gvAvalibalData.GetDataSourceRowIndex(gvAvalibalData.FocusedRowHandle);

            if (currentRow >= 0 )
            {
                DataRow theRowToDelete = theAvalibalData.Rows[currentRow];


                theSelectedData.Rows.Add(theAvalibalData.Rows[currentRow]["Designation"].ToString());
                //theAvalibalData.Rows[currentRow].Delete();
                theAvalibalData.Rows.Remove(theRowToDelete);
                gcAvalibalData.DataSource = theAvalibalData;

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int currentRow = gvSelectedData.GetDataSourceRowIndex(gvSelectedData.FocusedRowHandle);

            if (currentRow >= 0)
            {
                DataRow theRowToDelete = theSelectedData.Rows[currentRow];


                theAvalibalData.Rows.Add(theSelectedData.Rows[currentRow]["Designation"].ToString());
                //theAvalibalData.Rows[currentRow].Delete();
                theSelectedData.Rows.Remove(theRowToDelete);
                gcSelectedData.DataSource = theSelectedData;

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}