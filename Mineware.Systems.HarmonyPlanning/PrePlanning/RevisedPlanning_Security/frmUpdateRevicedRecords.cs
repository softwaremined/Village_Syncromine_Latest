using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PrePlanning.RevisedPlanning_Security
{
    public partial class frmUpdateRevicedRecords : DevExpress.XtraEditors.XtraForm
    {
        public string Section;
        public string UserID;
        public string UserName;
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        public frmUpdateRevicedRecords()
        {
            InitializeComponent();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _Query = new MWDataManager.clsDataAccess();
            _Query.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _Query.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _Query.queryReturnType = MWDataManager.ReturnType.DataTable;

            String Prodmonth = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString();

            _Query.SqlStatement = "Select isnull(HierarchicalID,0) HierarchicalID from Section where Prodmonth = " + Prodmonth+" and SectionID = '"+Section+"'" ;
            _Query.ExecuteInstruction();

            Int32 Level = 5;

            foreach (DataRow r in _Query.ResultsDataTable.Rows)
            {
                Level = Convert.ToInt32(r["HierarchicalID"]);
            }

            string SectionID = "SectionID_2";

            switch (Level)
            {
                case 1:
                    SectionID = "SectionID_5";
                    break;
                case 2:
                    SectionID = "SectionID_4";
                    break;
                case 3:
                    SectionID = "SectionID_3";
                    break;
                case 4:
                    SectionID = "SectionID_2";
                    break;
                case 5:
                    SectionID = "SectionID_1";
                    break;
                case 6:
                    SectionID = "SectionID";
                    break;
            }

            _Query.SqlStatement = "Insert Into PREPLANNING_CHANGEREQUEST_APPROVAL ( \n"+
                                    "ChangeRequestID, \n" +
                                    "User1, \n" +
                                    "User2, \n" +
                                    "Department, \n" +
                                    "RequestDate, \n" +
                                    "Approved, \n" +
                                    "Declined, \n" +
                                    "ApprovedDeclinedDate, \n" +
                                    "ApprovedDeclinedByUser, \n" +
                                    "Comments, \n" +
                                    "ApprovalRequired) \n" +
                                    "select \n" +
                                    "Distinct \n" +
                                    "b.ChangeRequestID, \n" +
                                    "'"+ UserID + "' User1, \n" +
                                    "User2, \n" +
                                    "Department, \n" +
                                    "RequestDate, \n" +
                                    "Approved, \n" +
                                    "Declined, \n" +
                                    "ApprovedDeclinedDate, \n" +
                                    "ApprovedDeclinedByUser, \n" +
                                    "b.Comments, \n" +
                                    "ApprovalRequired \n" +

                                    "from PREPLANNING_CHANGEREQUEST a inner join \n" +
                                    "PREPLANNING_CHANGEREQUEST_APPROVAL b on \n" +
                                    "a.ChangeRequestID = b.ChangeRequestID \n" +
                                    "inner join Syncromine.dbo.tblUsers c on \n" +
                                    "b.User1 = c.USERID \n" +
                                    "inner join Section_Complete d on \n" +
                                    "a.prodmonth = d.Prodmonth and \n" +
                                    "a.SectionID = d.SectionID \n" +
                                    "where \n" +
                                    "d."+SectionID+" = '"+Section+"' \n" +
                                    "and b.approved = 0 \n" +
                                    "and Declined = 0";
            _Query.queryReturnType = MWDataManager.ReturnType.longNumber;
            _Query.ExecuteInstruction();
            this.Close();

        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _Query = new MWDataManager.clsDataAccess();
            _Query.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _Query.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _Query.queryReturnType = MWDataManager.ReturnType.DataTable;

            String Prodmonth = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString();

            _Query.SqlStatement = "Select isnull(HierarchicalID,0) HierarchicalID from Section where Prodmonth = " + Prodmonth + " and SectionID = '" + Section + "'";
            _Query.ExecuteInstruction();

            Int32 Level = 5;

            foreach (DataRow r in _Query.ResultsDataTable.Rows)
            {
                Level = Convert.ToInt32(r["HierarchicalID"]);
            }

            string SectionID = "SectionID_2";

            switch (Level)
            {
                case 1:
                    SectionID = "SectionID_5";
                    break;
                case 2:
                    SectionID = "SectionID_4";
                    break;
                case 3:
                    SectionID = "SectionID_3";
                    break;
                case 4:
                    SectionID = "SectionID_2";
                    break;
                case 5:
                    SectionID = "SectionID_1";
                    break;
                case 6:
                    SectionID = "SectionID";
                    break;
            }

            _Query.SqlStatement = "update b set user1 = '"+ UserID + "'  \n" +
                                    "from PREPLANNING_CHANGEREQUEST a inner \n" +
                                    "join \n" +
                                    "PREPLANNING_CHANGEREQUEST_APPROVAL b on \n" +
                                    "a.ChangeRequestID = b.ChangeRequestID \n" +
                                    "inner \n" +
                                    "join Syncro_tblUsers c on \n" +
                                    "b.User1 = c.USERID \n" +
                                    "inner \n" +
                                    "join Section_Complete d on \n" +
                                    "a.prodmonth = d.Prodmonth and \n" +
                                    "a.SectionID = d.SectionID \n" +
                                    "where \n" +
                                    "d." + SectionID + " = '" + Section + "' \n" +
                                    "and b.approved = 0 \n" +
                                    "and Declined = 0";
            _Query.queryReturnType = MWDataManager.ReturnType.longNumber;
            _Query.ExecuteInstruction();
            this.Close();
        }

        private void frmUpdateRevicedRecords_Load(object sender, EventArgs e)
        {
            btnAdd.Text = "Add " + UserName + " to the Revised Planning";
            btnMove.Text = "Move all revised planning to " + UserName ;
        }
    }
}
