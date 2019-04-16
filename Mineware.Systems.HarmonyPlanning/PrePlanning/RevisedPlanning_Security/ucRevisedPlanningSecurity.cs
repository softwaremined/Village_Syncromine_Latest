using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using DevExpress.XtraEditors.Repository;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning;
using Mineware.Systems.Planning.PrePlanning;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Planning.PrePlanning.RevisedPlanning_Security
{
    public partial class ucRevisedPlanningSecurity : ucBaseUserControl 
    {
        string DEPT = "";
        string SECT = "";
        string USER = "";
        string sectionID = "";
        DataTable UserData= new DataTable ();
        DataTable PreData = new DataTable();
        RevisedSecurityDB RSDB = new RevisedSecurityDB();
      
        public ucRevisedPlanningSecurity()
        {
            InitializeComponent();
        }

        public override void setSecurity()
        {
            updateSecurity();
        }

        private void updateSecurity()
        {
            switch (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miRevisedPlanningSecurity_HPASRevisedPlanningSecurity_MinewareSystemsHarmonyPAS.ItemID))
            {
               case 0:
               case 1:
                    btnAdd.Enabled = false;
                    btnDel.Enabled = false;
                    btnEdit.Enabled = false;
                    
                    break;
                case 2:
                    btnAdd.Enabled = true;
                    btnDel.Enabled = true;
                    btnEdit.Enabled = true;
                    break;

            }
        }



        private void ucRevisedPlanningSecurity_Load(object sender, EventArgs e)
        {

            RSDB.UserCurrentInfo = UserCurrentInfo;
            RSDB.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            gcDepartmentData.DataSource = RSDB.GetDepartments();
          //  gcPlanningTypes.DataSource = RSDB.PrePlanningTypes();
            gcPlanningTypes.Visible = false;
            updateSecurity();


        }

        public void loadScreenData()
        {
            RSDB.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            UserData = RSDB.GetUserData(DEPT );
            gcUserData.DataSource = UserData;

            ileRevisedPlanningSecurity myEdit = new ileRevisedPlanningSecurity { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
           
            myEdit.UserCurrentInfo = this.UserCurrentInfo;
            myEdit.theSystemDBTag = this.theSystemDBTag;
            myEdit.RSDB = RSDB;
            gvUserDataView.OptionsEditForm.CustomEditFormLayout = myEdit;

            reUserList.DataSource = RSDB.GetUserList(DEPT);
            reUserList.ValueMember = "UserID";
            reUserList.DisplayMember = "UserName";
            ((Mineware.Systems.Planning.PrePlanning.RevisedPlanning_Security.ileRevisedPlanningSecurity)gvUserDataView.OptionsEditForm.CustomEditFormLayout).info("1", DEPT, SECT, "", "");
        }

        public void loadPreTypes()
        {
            RSDB.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            PreData  = RSDB.PrePlanningTypes (DEPT);
            gcPlanningTypes.DataSource = PreData;
            //checkedListBoxControl1.DataSource = RSDB.PrePlanningTypes(DEPT);
            //checkedListBoxControl1.DisplayMember = "Description";
            //checkedListBoxControl1.ValueMember = "Checked";


            //checkedListBoxControl1 .ItemCheck +=checkedListBoxControl1_ItemCheck;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int DSRow1 = gvDepartmentsDataView.FocusedRowHandle;
            if (DSRow1 < 0)
            {
                MessageBox.Show("Please select a Department", "", MessageBoxButtons.OK);


            }
            else
            {
                var dept = gvDepartmentsDataView.GetRowCellValue(gvDepartmentsDataView.FocusedRowHandle, gvDepartmentsDataView.Columns["Department"]);
                DEPT = Convert.ToString(dept);
                var sect = gvUserDataView.GetRowCellValue(gvUserDataView.FocusedRowHandle, gvUserDataView.Columns["Name"]);
                SECT = Convert.ToString(sect);
                gvUserDataView.AddNewRow();
                gvUserDataView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                gvUserDataView.ShowEditForm();
                GridView view;
                view = sender as GridView;
                object row = gvUserDataView.GetRow(gvUserDataView.FocusedRowHandle); // get the current row 
                (row as DataRowView)["hasChanged"] = 1;
                RSDB.hasChanged = "1";
                ((Mineware.Systems.Planning.PrePlanning.RevisedPlanning_Security.ileRevisedPlanningSecurity)gvUserDataView.OptionsEditForm.CustomEditFormLayout).info("1", DEPT, "", "","");
            }
        }

        private void gvDepartmentsDataView_Click(object sender, EventArgs e)
        {
            var dept = gvDepartmentsDataView.GetRowCellValue(gvDepartmentsDataView.FocusedRowHandle, gvDepartmentsDataView.Columns["Department"]);
             DEPT = Convert.ToString(dept);
             var sect = gvUserDataView.GetRowCellValue(gvUserDataView.FocusedRowHandle, gvUserDataView.Columns["Name"]);
             SECT = Convert.ToString(sect);
            loadScreenData();
            loadPreTypes();
            gcPlanningTypes.Visible = true;
        }

        private void gvUserDataView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            RSDB.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            UserData.AcceptChanges();


            RSDB.systemDBTag = theSystemDBTag;

            RSDB.saveRecords(UserData,DEPT);



            loadScreenData();
        }

        private void gvUserDataView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            object row = gvUserDataView.GetRow(gvUserDataView.FocusedRowHandle); // get the current row 
            if (row != null)
            {
                (row as DataRowView)["hasChanged"] = 1;
                gvUserDataView.UpdateCurrentRow();

                int DSRow1 = gvDepartmentsDataView.FocusedRowHandle;//gvRequests1.FocusedRowHandle;
                if (DSRow1 < 0)
                {
                    MessageBox.Show("Please select a Department", "", MessageBoxButtons.OK);


                }
                int DSRow = gvUserDataView.FocusedRowHandle;//gvRequests1.FocusedRowHandle;
                if (DSRow < 0)
                {
                    MessageBox.Show("There are no records to Edit", "", MessageBoxButtons.OK);


                }
                else
                {
                    var user = gvUserDataView.GetRowCellValue(gvUserDataView.FocusedRowHandle, gvUserDataView.Columns["UserID"]);
                    USER = Convert.ToString(user);
                    var sect = gvUserDataView.GetRowCellValue(gvUserDataView.FocusedRowHandle, gvUserDataView.Columns["Name"]);
                    string SECT1 = Convert.ToString(sect);
                    var sectype = gvUserDataView.GetRowCellValue(gvUserDataView.FocusedRowHandle, gvUserDataView.Columns["SecurityType"]);
                    string SECTYPE = Convert.ToString(sectype);


                    ((Mineware.Systems.Planning.PrePlanning.RevisedPlanning_Security.ileRevisedPlanningSecurity)gvUserDataView.OptionsEditForm.CustomEditFormLayout).info("2", DEPT, SECT1, USER, SECTYPE);
                    gvUserDataView.ShowEditForm();
                }
            }
        }

        private void gvUserDataView_RowClick(object sender, RowClickEventArgs e)
        {
            object row = gvUserDataView.GetRow(gvUserDataView.FocusedRowHandle); // get the current row 
            (row as DataRowView)["hasChanged"] = 1;
           // gvUserDataView.PostEditor();
            gvUserDataView.RefreshData();
            var sect = gvUserDataView.GetRowCellValue(gvUserDataView.FocusedRowHandle, gvUserDataView.Columns["Name"]);
           string  SECT2 = Convert.ToString(sect);

           var sectype = gvUserDataView.GetRowCellValue(gvUserDataView.FocusedRowHandle, gvUserDataView.Columns["SecurityType"]);
           string SECTYPE = Convert.ToString(sectype);

            var user = gvUserDataView.GetRowCellValue(gvUserDataView.FocusedRowHandle, gvUserDataView.Columns["UserID"]);
            USER = Convert.ToString(user);
            ((Mineware.Systems.Planning.PrePlanning.RevisedPlanning_Security.ileRevisedPlanningSecurity)gvUserDataView.OptionsEditForm.CustomEditFormLayout).info("2", DEPT, SECT2, USER, SECTYPE);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int DSRow = gvUserDataView.FocusedRowHandle;//gvRequests1.FocusedRowHandle;
            if (DSRow < 0)
            {
                MessageBox.Show("There are no records to delete", "", MessageBoxButtons.OK);


            }
            else
            {
                var sect = gvUserDataView.GetRowCellValue(gvUserDataView.FocusedRowHandle, gvUserDataView.Columns["Name"]);
                SECT = Convert.ToString(sect);
                var user = gvUserDataView.GetRowCellValue(gvUserDataView.FocusedRowHandle, gvUserDataView.Columns["UserID"]);
                USER = Convert.ToString(user);
                var theData = new MWDataManager.clsDataAccess();
                theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "SELECT DISTINCT SECTIONID FROM SECTION WHERE Name='" + SECT + "'";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                DataTable sectid = new DataTable();
                sectid = theData.ResultsDataTable;
                foreach (DataRow dr in sectid.Rows)
                {
                    sectionID = dr["SECTIONID"].ToString();
                }
                DialogResult result;
              result=  MessageBox.Show("Are you sure you want to delete the record", "", MessageBoxButtons.YesNo);
              if (result == DialogResult.Yes )
              {
                  RSDB.deletedata(sectionID, USER, DEPT);
              }
              
               
                loadScreenData();
            }
        }

        private void gvUserDataView_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
           // loadScreenData();
        }

        private void checkedListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

       
        }

        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            //foreach (object item in checkedListBoxControl1.CheckedItems)
            //{
            //    DataRowView row = item as DataRowView;
            //    row["Checked"] = e.State == CheckState.Checked;
            //}
        }

        private void gvPlanningTypes_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvPlanningTypes.PostEditor();
            var type = gvPlanningTypes.GetRowCellValue(gvPlanningTypes.FocusedRowHandle, gvPlanningTypes.Columns["Description"]);
            string TYPE = Convert.ToString(type);
            var chk = gvPlanningTypes.GetRowCellValue(gvPlanningTypes.FocusedRowHandle, gvPlanningTypes.Columns["Checked"]);
            int CHK = Convert.ToInt32(chk);
            var changeid = gvPlanningTypes.GetRowCellValue(gvPlanningTypes.FocusedRowHandle, gvPlanningTypes.Columns["ChangeID"]);
            int CHANGEID = Convert.ToInt32(changeid);

            RSDB.savedatanew(CHANGEID, CHK, DEPT);

        }

        private void gvUserDataView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

        }

        private void gcDepartmentData_Click(object sender, EventArgs e)
        {

        }
    }
}
