using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid. Views.Grid;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Global;
using Mineware.Systems.Global.sysNotification;
using System.Drawing;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PlanningProtocolTemplates
{
    public partial class frmPlanProtTemplateSetup : ucBaseUserControl 
    {
        
        public clsPlanProtTemplateData templateData = new clsPlanProtTemplateData();
        public frmPlanProtTemplateSetup()
        {
            InitializeComponent();

        }

        DataSet data = new DataSet();
        DataTable table = new DataTable();
        DataTable tableTemplate = new DataTable();
        DataTable tableGrid = new DataTable();
        DataTable dtProfileInfo = new DataTable();
        MWDataManager.clsDataAccess _dbApproveUsers = new MWDataManager.clsDataAccess();
        public currentAction formAction;
        public int TempID = 0;
        int FieldID = 0;
        string FieldName;

     

        private void frmPlanProtTemplateSetup_Load(object sender, EventArgs e)
        {
            loadingData();
        }

        private void loadingData()
        {
            cmbType.Items.Add("Stoping");
            cmbType.Items.Add("Development");
            userList.DataSource = getUserList("", TempID);

            userList.ValueMember = "USERID";
            userList.DisplayMember = "NAME";

            //gridControl1.ForceInitialize();

            //   Add new template

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "[spUpdate_PlanningTemplate_UserSecurity_Table]";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            SqlParameter[] _paramCollection =      
                { 
                     _dbMan.CreateParameter("@Prodmonth", SqlDbType.Int , 7,TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection) .CurrentProductionMonth),

                      
                };
            _dbMan.ParamCollection = _paramCollection;
            _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
            _dbMan.ExecuteInstruction();
           
            SqlDataAdapter oleDBAdapter1 = new SqlDataAdapter("SELECT TemplateID, TemplateName FROM PlanProt_Template WHERE TemplateID =  " + TempID.ToString(), TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection));
            SqlDataAdapter oleDBAdapter2 = new SqlDataAdapter("SELECT * FROM PlanProt_ApproveUsers WHERE SEction = 'NONE' and Shaft <> 'NONE' ORDER BY Shaft ", TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection));
            SqlDataAdapter oleDBAdapter3 = new SqlDataAdapter("SELECT * FROM PlanProt_ApproveUsers WHERE SEction <> 'NONE' ORDER BY Section ", TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection));
            SqlDataAdapter oleDBAdapter4 = new SqlDataAdapter("SELECT DISTINCT  * FROM PlanProt_ApproveUsers WHERE SEction = 'NONE' and Shaft = 'NONE' ORDER BY Shaft,Unit ", TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection));

           
            oleDBAdapter1.Fill(dsTemplateSecurity.theTemplates);
            oleDBAdapter2.Fill(dsTemplateSecurity.theShaft);
            oleDBAdapter3.Fill(dsTemplateSecurity.theSection);
            oleDBAdapter4.Fill(dsTemplateSecurity.theUnits);

            if (formAction == currentAction.caAdd)
            {
                // Load all User Profiles in CPM
                MWDataManager.clsDataAccess _dbManLastMonthWP = new MWDataManager.clsDataAccess();
                _dbManLastMonthWP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLastMonthWP.SqlStatement = "SELECT USERPROFILEID ProfileName,CAST(FullAccess AS BIT) FullAccess,CAST(ReadOnlyAccess AS BIT) ReadOnlyAccess FROM (SELECT USERPROFILEID,0 FullAccess,1 ReadOnlyAccess FROM dbo.USERPROFILES) theProfile";
                _dbManLastMonthWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLastMonthWP.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLastMonthWP.ExecuteInstruction();

                using (DataTableReader reader = new DataTableReader(_dbManLastMonthWP.ResultsDataTable))
                {
                    dtProfileInfo.Clear();
                    dtProfileInfo.Load(reader);
                }
                gridSecurity.DataSource = dtProfileInfo; // Populate grid


                MWDataManager.clsDataAccess _dbManTemplate = new MWDataManager.clsDataAccess();
                _dbManTemplate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManTemplate.SqlStatement = "select MAX(TemplateID) TemplateID from PlanProt_Template";
                _dbManTemplate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManTemplate.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManTemplate.ExecuteInstruction();

                using (DataTableReader readerTemplate = new DataTableReader(_dbManTemplate.ResultsDataTable))
                {
                    tableTemplate.Load(readerTemplate);
                }

                foreach (DataRow Temp in tableTemplate.Rows)
                {
                    if (Temp["TemplateID"].ToString() == "")
                    {
                        TempID = 1;
                    }
                    else
                    {
                        TempID = Convert.ToInt32(Temp["TemplateID"].ToString()) + 1;
                    }
                }
            }
            else
            if (formAction == currentAction.caEdit)
            {

                MWDataManager.clsDataAccess _dbManTemplateDetail = new MWDataManager.clsDataAccess();
                _dbManTemplateDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManTemplateDetail.SqlStatement = "Select * from PLANPROT_TEMPLATE where TemplateID = '" + TempID + "'";
                _dbManTemplateDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManTemplateDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManTemplateDetail.ExecuteInstruction();


                _dbApproveUsers.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbApproveUsers.SqlStatement = "SELECT TemplateID,Shaft,User1,User2 FROM dbo.PLANPROT_APPROVEUSERS WHERE TemplateID = '" + TempID + "'";
                _dbApproveUsers.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbApproveUsers.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbApproveUsers.ExecuteInstruction();

                cmbType.Enabled = false;
                txtDescription.Enabled = false;

                //   gcApproveData.DataSource = _dbApproveUsers.ResultsDataTable;

                using (DataTableReader readerTempDetail = new DataTableReader(_dbManTemplateDetail.ResultsDataTable))
                {
                    tableTemplate.Load(readerTempDetail);
                }

                foreach (DataRow dr in tableTemplate.Rows)
                {
                    if (dr["Activity"].ToString() == "0")
                    {
                        cmbType.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbType.SelectedIndex = 1;
                    }

                    txtDescription.Text = dr["TemplateName"].ToString();
                    string edit1;

                    if (dr["ApprovalRequired"].ToString() == "True")
                    {
                        rgReqApproval.SelectedIndex = 0;
                    }
                    else rgReqApproval.SelectedIndex = 1;
                }

                DataTable CPMProfiles = new DataTable();

                MWDataManager.clsDataAccess _dbManLastMonthWP = new MWDataManager.clsDataAccess();
                _dbManLastMonthWP.ConnectionString = TConnections.GetConnectionString("SystemSettings", UserCurrentInfo.Connection);
                //_dbManLastMonthWP.SqlStatement = "SELECT USERPROFILEID FROM USERPROFILES ";
                _dbManLastMonthWP.SqlStatement = "SELECT DepartmentID,Description FROM tblDepartments ";

                _dbManLastMonthWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLastMonthWP.queryReturnType = MWDataManager.ReturnType.DataTable;
                var result = _dbManLastMonthWP.ExecuteInstruction();
                if (!result.success)
                {
                    MessageItem.viewMessage(MessageType.Error, "SQL ERROR", theSystemDBTag, "frmPlanProtTemplateSetup", "loadingData", result.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                    return;
                }

                DataTableReader reader = new DataTableReader(_dbManLastMonthWP.ResultsDataTable);
                CPMProfiles.Load(reader);

                _dbManLastMonthWP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLastMonthWP.SqlStatement = "SELECT DepartmentID ,TemplateID ,FullAccess ,ReadOnlyAccess , '' Description , CAST(DATA1 .AccessLevel as bit) AccessLevel FROM ( " +
                                                    "select DepartmentID ,TemplateID, cast(data.FullAccess as bit) FullAccess, cast(data.ReadOnlyAccess as bit) ReadOnlyAccess, AccessLevel = CASE WHEN FullAccess = 0 THEN '0' ELSE '1' END  from ( " +
                                                    "SELECT PPA.DepartmentID ,PPA .TemplateID , " +
                                                    "FullAccess= CASE WHEN PPA .AccessLevel=1 then '1' else '0' end, " +
                                                    "ReadOnlyAccess= case when PPA.AccessLevel =1 then '0' else '1' end , " +
                                                    "PPA.AccessLevel   FROM PLANPROT_PROFILEACCESS PPA WHERE TemplateID = '" + TempID.ToString() + "')data)DATA1 ";
                result = _dbManLastMonthWP.ExecuteInstruction();
                if (!result.success)
                {
                    MessageItem.viewMessage(MessageType.Error, "SQL ERROR", theSystemDBTag, "frmPlanProtTemplateSetup", "loadingData", result.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                    return;
                }

                DataTableReader reader2 = new DataTableReader(_dbManLastMonthWP.ResultsDataTable);
                dtProfileInfo.Load(reader2);

                foreach (DataRow dr in CPMProfiles.Rows)
                {
                    string expression;
                    //expression = "ProfileName = '" + dr["USERPROFILEID"].ToString() + "'";
                    expression = "DepartmentID = '" + dr["DepartmentID"].ToString() + "'";
                    DataRow[] foundRows;
                    foundRows = dtProfileInfo.Select(expression);

                    var description = dr["Description"].ToString();
                    if (foundRows.Length == 0)
                    {
                        //dtProfileInfo.Rows.Add(dr["USERPROFILEID"].ToString(), 0, TempID);
                        //dtProfileInfo.Rows.Add(dr["Description"].ToString(), 0, TempID);
                        dtProfileInfo.Rows.Add(dr["DepartmentID"].ToString(), TempID, false, true, description);
                    }
                    else
                    {
                        foundRows[0]["Description"] = description;
                    }
                }

                dtProfileInfo.Select("");

                //dtProfileInfo.Columns.Add("Access1", typeof(bool));


                //
                //{
                //    if (dr["ProfileAccess"] == DBNull.Value)
                //    {
                //        dr["Access1"] = false;
                //    }
                //    else
                //    {
                //        dr["Access1"] = dr["ProfileAccess"];
                //    }
                //}

                //bandedGridView1.PopulateColumns(dtProfileInfo);
                //LoadColumns();
                gridSecurity.DataSource = dtProfileInfo; // Populate grid
                //bandedGridView1.Columns[1].Visible = false;


            }

            MWDataManager.clsDataAccess _dbManGrid = new MWDataManager.clsDataAccess();
            _dbManGrid.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManGrid.SqlStatement = "select Distinct pf.FieldID [FieldID],pf.FieldName [Field Name],ppf.fieldDescription [Field Type],pf2.FieldName[Parent]  from PlanProt_Fields pf " +
                                    "left outer join " +
                                    "PlanProt_FieldTypes ppf " +
                                    "on pf.FieldType = ppf.fieldTypeID " +
                                    "left outer join PLANPROT_DATA pg " +
                                    "on pf.FieldID = pg.FieldID " +
                                    "left outer join " +
                                    "PlanProt_Fields pf2 " +
                                    "on pg.FieldID = pf2.parentID " +
                                    " where pf.TemplateID = '" + TempID + "' Order by pf.FieldID ";
            _dbManGrid.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManGrid.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManGrid.ExecuteInstruction();

            DataTableReader readerGrid = new DataTableReader(_dbManGrid.ResultsDataTable);
            tableGrid.Load(readerGrid);

            gridOutput.DataSource = tableGrid; // Populate Grid
            // bandedGridView2.Columns[0].Visible = false;
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //int AccessType = 0;
                foreach (DataRow dr in dtProfileInfo.Rows)
                {

                    MWDataManager.clsDataAccess _dbManDelete = new MWDataManager.clsDataAccess();
                    _dbManDelete.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManDelete.SqlStatement = "Delete from PlanProt_ProfileAccess where DepartmentID = '" + dr["DepartmentID"].ToString() + "' and TemplateID = '" + TempID + "'";
                    _dbManDelete.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManDelete.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManDelete.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbManLastMonthWP = new MWDataManager.clsDataAccess();
                    _dbManLastMonthWP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbManLastMonthWP.SqlStatement = "Insert into PlanProt_ProfileAccess (DepartmentID ,FullAccess ,ReadOnlyAccess, TemplateID)  values ('" + dr["ProfileName"].ToString() + "' , '" + dr["FullAccess"] + "' , '" + dr["ReadOnlyAccess"] + "', '" + TempID + "')";
                    if (dr["FullAccess"].ToString() == "True")
                    {
                        _dbManLastMonthWP.SqlStatement = "Insert into PlanProt_ProfileAccess (DepartmentID ,TemplateID,AccessLevel)  values ('" + dr["DepartmentID"].ToString() + "' , '" + TempID + "' ,  'True')";
                    }
                    else
                    {
                        _dbManLastMonthWP.SqlStatement = "Insert into PlanProt_ProfileAccess (DepartmentID ,TemplateID,AccessLevel)  values ('" + dr["DepartmentID"].ToString() + "' , '" + TempID + "' ,  'False')";
                    }


                    _dbManLastMonthWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManLastMonthWP.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManLastMonthWP.ExecuteInstruction();



                }

                if (formAction == currentAction.caAdd)
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = "sp_PlanProt_TemplateSetupSave";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    SqlParameter[] _paramCollection =      
                { 
                     _dbMan.CreateParameter("@TemplateID", SqlDbType.Int , 7,TempID),
                     _dbMan.CreateParameter("@TemplateName", SqlDbType.VarChar , 200,txtDescription.Text),
                     _dbMan.CreateParameter("@Activity", SqlDbType.Int , 7,cmbType.SelectedIndex ),
                      _dbMan.CreateParameter("@Fieldid", SqlDbType.Int ,50 ,DBNull.Value ),
                    // _dbMan.CreateParameter("@FieldType", SqlDbType.VarChar ,30 ,DBNull.Value ), 
                     _dbMan.CreateParameter("@SelectedValue", SqlDbType.VarChar, 4,DBNull.Value ),
                     _dbMan.CreateParameter("@MinValue", SqlDbType.VarChar, 4, DBNull.Value ),
                     _dbMan.CreateParameter("@MaxValue", SqlDbType.VarChar, 4,DBNull.Value ),                
                     _dbMan.CreateParameter("@RiskRating", SqlDbType.Int,3 , DBNull.Value), 
                     _dbMan.CreateParameter("@ParentID", SqlDbType.Int, 3, DBNull.Value ),
                     _dbMan.CreateParameter("@FrontBack", SqlDbType.Int,3 ,1 ),
                      _dbMan.CreateParameter("@Action", SqlDbType.Int,3 ,0 ),
                       _dbMan.CreateParameter("@FieldUpdate", SqlDbType.Int,3 ,DBNull.Value ),
                       _dbMan.CreateParameter("@User1", SqlDbType.VarChar,50 ,"" ),
                       _dbMan.CreateParameter("@User2", SqlDbType.VarChar,50 ,"" ),
                       _dbMan.CreateParameter("@AprovalReq", SqlDbType.Bit,0,rgReqApproval.Properties.Items[rgReqApproval.SelectedIndex].Value ),


                };
                    _dbMan.ParamCollection = _paramCollection;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();

                  
                    MessageBox.Show("Data Saved Successfully", "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = "sp_PlanProt_TemplateSetupSave";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    SqlParameter[] _paramCollection =      
                { 
                     _dbMan.CreateParameter("@TemplateID", SqlDbType.Int , 7,TempID),
                     _dbMan.CreateParameter("@TemplateName", SqlDbType.VarChar , 200,txtDescription.Text),
                     _dbMan.CreateParameter("@Activity", SqlDbType.Int , 7,cmbType.SelectedIndex ),
                    _dbMan.CreateParameter("@Fieldid", SqlDbType.Int ,50 ,DBNull.Value ),
                     //_dbMan.CreateParameter("@FieldType", SqlDbType.VarChar ,30 ,DBNull.Value ), 
                     _dbMan.CreateParameter("@SelectedValue", SqlDbType.VarChar, 4,DBNull.Value ),
                     _dbMan.CreateParameter("@MinValue", SqlDbType.VarChar, 4, DBNull.Value ),
                     _dbMan.CreateParameter("@MaxValue", SqlDbType.VarChar, 4,DBNull.Value ),                
                     _dbMan.CreateParameter("@RiskRating", SqlDbType.Int,3 , DBNull.Value), 
                     _dbMan.CreateParameter("@ParentID", SqlDbType.Int, 3, DBNull.Value ),
                     _dbMan.CreateParameter("@FrontBack", SqlDbType.Int,3 ,1 ),
                     _dbMan.CreateParameter("@Action", SqlDbType.Int,3 ,1 ),
                     _dbMan.CreateParameter("@FieldUpdate", SqlDbType.Int,3 ,DBNull.Value ),
                     _dbMan.CreateParameter("@User1", SqlDbType.VarChar,50 ,"" ),
                     _dbMan.CreateParameter("@User2", SqlDbType.VarChar,50 ,"" ),
                     _dbMan.CreateParameter("@AprovalReq", SqlDbType.Bit,0,rgReqApproval.Properties.Items[rgReqApproval.SelectedIndex].Value ),


                };
                    _dbMan.ParamCollection = _paramCollection;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();

                    MWDataManager.clsDataAccess _UpdateData = new MWDataManager.clsDataAccess();
                    _UpdateData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _UpdateData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _UpdateData.queryReturnType = MWDataManager.ReturnType.DataTable;


                    foreach (DataRow r in dsTemplateSecurity.theUnits .Rows )
                    {

                        if (r["IsUpdated"].ToString() == "1")
                        {
                            _UpdateData.SqlStatement = "UPDATE PlanProt_ApproveUsers SET User1 = '" + r["User1"].ToString() + "'," +
                                                       "                                 User2 = '" + r["User2"].ToString() + "' " +
                                                       "WHERE TemplateID = '" + r["TemplateID"].ToString() + "' and " +
                                                       "      Shaft = '" + r["Shaft"].ToString() + "' and " +
                                                       "      Unit= '" + r["Unit"].ToString () + "' and" +
                                                       "      Section =  '" + r["Section"].ToString() + "' ";

                            _UpdateData.ExecuteInstruction();
                        }

                    }

                    foreach (DataRow r in dsTemplateSecurity.theSection.Rows)
                    {
                      
                        if (r["IsUpdated"].ToString() == "1")
                        {
                            _UpdateData.SqlStatement = "UPDATE PlanProt_ApproveUsers SET User1 = '" + r["User1"].ToString() + "'," +
                                                       "                                 User2 = '" + r["User2"].ToString() + "' " +
                                                       "WHERE TemplateID = '" + r["TemplateID"].ToString() + "' and " +
                                                       "      Shaft = '" + r["Shaft"].ToString() + "' and " +
                                                       "      Section =  '" + r["Section"].ToString() + "' ";

                            _UpdateData.ExecuteInstruction();
                        }

                    }

                    foreach (DataRow r in dsTemplateSecurity.theShaft.Rows)
                    {

                        if (r["IsUpdated"].ToString() == "1")
                        {
                            _UpdateData.SqlStatement = "UPDATE PlanProt_ApproveUsers SET User1 = '" + r["User1"].ToString() + "'," +
                                                       "                                 User2 = '" + r["User2"].ToString() + "' " +
                                                       "WHERE TemplateID = '" + r["TemplateID"].ToString() + "' and " +
                                                       "      Shaft = '" + r["Shaft"].ToString() + "' and " +
                                                       "      Section =  '" + r["Section"].ToString() + "' ";


                            _UpdateData.ExecuteInstruction();
                        }

                    }
                   
                    TsysNotification.showNotification("Data Saved", "Template data was saved successfully", Color.CornflowerBlue);

                    CanClose = true;
                }
                dtProfileInfo.Clear();
                dsTemplateSecurity.theTemplates.Clear();
                dsTemplateSecurity.theShaft.Clear();
                dsTemplateSecurity.theSection.Clear();
                dsTemplateSecurity.theUnits.Clear();

                loadingData();
               // this.Close();
                //gridControl1.Refresh();
                //gridControl1.RefreshDataSource();
              //  gridControl1.DataSource = "";
              
            }
            catch (SqlException s)
            {
                MessageItem.viewMessage(MessageType.Error, "ERROR SAVING DATA", theSystemTag, "frmPlanProtTemplateSetup", "btnSave_Click", s.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                CanClose = false;
            }
        }

        private void gridSecurity_Click(object sender, EventArgs e)
        {
           
        }

     

        private void btnClose_Click(object sender, EventArgs e)
        {
           
            foreach (DataRow r in dsTemplateSecurity.theUnits.Rows)
            {

                if (r["IsUpdated"].ToString() == "1")
                {
                    //this.CanClose = false;
                    DialogResult dr = new DialogResult();
                   MessageBox.Show("The Data is not saved. Are you sure you want to cancel? ", "", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                       
                        //this.CanClose = true;
                        this.CanClose = true;
                       // ucPlanProtTemplateList.SendConroleToMainTabRequest += new SendConroleToMainTabHandler(sendToMainScreen);
                      
                     
                    }
                    else
                    {
                        this.CanClose = true ;
                    }
                }
            }

            foreach (DataRow r in dsTemplateSecurity.theSection.Rows)
            {

                if (r["IsUpdated"].ToString() == "1")
                {
                    DialogResult dr = new DialogResult();
                    MessageBox.Show("The Data is not saved. Are you sure you want to cancel? ", "", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        this.CanClose = true;
                    }
                    else
                    {
                        this.CanClose = false;
                    }
                }
            }

            foreach (DataRow r in dsTemplateSecurity.theShaft.Rows)
            {

                if (r["IsUpdated"].ToString() == "1")
                {
                    DialogResult dr = new DialogResult();
                    MessageBox.Show("The Data is not saved. Are you sure you want to cancel? ", "", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        this.CanClose = true;
                    }
                    else
                    {
                        this.CanClose = false;
                    }
                }
            }
                        
                        

           // this.Close();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FieldID = Convert.ToInt32(tableGrid.Rows[viewFields.FocusedRowHandle]["FieldID"].ToString());
            frmFieldSetup fieldSetup = new frmFieldSetup { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
            fieldSetup.formAction1 = currentAction.caEdit;
            fieldSetup.TempId = TempID;
            fieldSetup.FieldID = FieldID;
            MWDataManager.clsDataAccess _dbManGrid = new MWDataManager.clsDataAccess();
            _dbManGrid.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManGrid.SqlStatement = "select Distinct pf.FieldID [FieldID],pf.FieldName [Field Name],ppf.fieldDescription [Field Type],pf2.FieldName[Parent]  from PlanProt_Fields pf " +
                                    "left outer join " +
                                    "PlanProt_FieldTypes ppf " +
                                    "on pf.FieldType = ppf.fieldTypeID " +
                                    "left outer join " +
                                    "PlanProt_Fields pf2 " +
                                    "on pf.ParentID = pf2.FieldID " +
                                    " where pf.TemplateID = '" + TempID + "'";
            _dbManGrid.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManGrid.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManGrid.ExecuteInstruction();


            gridOutput.DataSource = _dbManGrid.ResultsDataTable;
            fieldSetup.ShowDialog();
        }

        private void bandedGridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            FieldID = Convert.ToInt32(tableGrid.Rows[e.RowHandle]["FieldID"].ToString()); // Get the field id on the selected row
           // MessageBox.Show(FieldID.ToString());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            DialogResult result;

            FieldID = Convert.ToInt32(tableGrid.Rows[viewFields.FocusedRowHandle]["FieldID"].ToString());

            MWDataManager.clsDataAccess _dbManGrid = new MWDataManager.clsDataAccess();
            _dbManGrid.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManGrid.SqlStatement = "Select * from planProt_Data where fieldid = '"+FieldID+"'";
            _dbManGrid.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManGrid.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManGrid.ExecuteInstruction();

            if (_dbManGrid.ResultsDataTable.Rows.Count != 0)
            {
                MessageBox.Show("Field may not be deleted as it has data associated to it ", "Cannot Delete Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MWDataManager.clsDataAccess _dbManCheck = new MWDataManager.clsDataAccess();
                _dbManCheck.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManCheck.SqlStatement = "Select * from planProt_Groupings where parentid = '" + FieldID + "'";
                _dbManCheck.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManCheck.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManCheck.ExecuteInstruction();

                if (_dbManCheck.ResultsDataTable.Rows.Count != 0)
                {
                    MessageBox.Show("Field may not be deleted as it has other fields linked to it. Please Delete linked fields first ", "Cannot Delete Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    MWDataManager.clsDataAccess _dbManDeleteField = new MWDataManager.clsDataAccess();
                    _dbManDeleteField.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManDeleteField.SqlStatement = "select FieldName from PlanProt_Fields where FieldID = '" + FieldID + "'";
                    _dbManDeleteField.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManDeleteField.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManDeleteField.ExecuteInstruction();

                    foreach (DataRow temp in _dbManDeleteField.ResultsDataTable.Rows)
                    {
                        FieldName = temp["FieldName"].ToString();
                    }

                    result = MessageBox.Show("Are you sure you want to delete Field:" + FieldName, "Delete Field", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {


                        MWDataManager.clsDataAccess _dbManDelete = new MWDataManager.clsDataAccess();
                        _dbManDelete.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManDelete.SqlStatement = "Delete from PlanProt_Fields where  fieldid =  '" + FieldID + "'";
                        _dbManDelete.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManDelete.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManDelete.ExecuteInstruction();

                        MWDataManager.clsDataAccess _dbManDeleteFieldValues = new MWDataManager.clsDataAccess();
                        _dbManDeleteFieldValues.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManDeleteFieldValues.SqlStatement = "Delete from PlanProt_FieldValues where  fieldid =  '" + FieldID + "'";
                        _dbManDeleteFieldValues.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManDeleteFieldValues.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManDeleteFieldValues.ExecuteInstruction();

                        MWDataManager.clsDataAccess _dbManDeleteGrouping = new MWDataManager.clsDataAccess();
                        _dbManDeleteGrouping.ConnectionString = TUserInfo.Connection;
                        _dbManDeleteGrouping.SqlStatement = "Delete from PlanProt_Groupings where  fieldid =  '" + FieldID + "'";
                        _dbManDeleteGrouping.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManDeleteGrouping.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManDeleteGrouping.ExecuteInstruction();



                        //bandedGridView2.DeleteSelectedRows();



                        tableGrid.Clear();

                        MWDataManager.clsDataAccess _dbManGridUpdated = new MWDataManager.clsDataAccess();
                        _dbManGridUpdated.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManGridUpdated.SqlStatement = "select Distinct pf.FieldID [FieldID],pf.FieldName [Field Name],ppf.fieldDescription [Field Type],pf2.FieldName[Parent]  from PlanProt_Fields pf " +
                                                "left outer join " +
                                                "PlanProt_FieldTypes ppf " +
                                                "on pf.FieldType = ppf.fieldTypeID " +
                                                "left outer join " +
                                                "PlanProt_Fields pf2 " +
                                                "on pf.ParentID = pf2.FieldID " +
                                                " where pf.TemplateID = '" + TempID + "'";
                        _dbManGridUpdated.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManGridUpdated.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManGridUpdated.ExecuteInstruction();


                        gridOutput.DataSource = _dbManGridUpdated.ResultsDataTable;// Reload the grid


                        MessageBox.Show("Field was Deleted successfully ", "Data Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmFieldSetup setup = new frmFieldSetup() { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
            setup.formAction1 = currentAction.caAdd;
            setup.TempId = TempID;
            setup.ShowDialog();

            //tableGrid.Clear();

            MWDataManager.clsDataAccess _dbManGrid = new MWDataManager.clsDataAccess();
            _dbManGrid.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManGrid.SqlStatement = "select Distinct pf.FieldID [FieldID],pf.FieldName [Field Name],ppf.fieldDescription [Field Type],pf2.FieldName[Parent]  from PlanProt_Fields pf " +
                                    "left outer join " +
                                    "PlanProt_FieldTypes ppf " +
                                    "on pf.FieldType = ppf.fieldTypeID " +
                                    "left outer join " +
                                    "PlanProt_Fields pf2 " +
                                    "on pf.ParentID = pf2.FieldID " +
                                    " where pf.TemplateID = '" + TempID + "'";
            _dbManGrid.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManGrid.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManGrid.ExecuteInstruction();

            gridOutput.DataSource = _dbManGrid.ResultsDataTable; // Populate Grid
        }

        private void bandedGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {


        }

        private void bandedGridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.ToString() == "FullAccess")
            {
                if (dtProfileInfo.Rows[e.RowHandle]["FullAccess"].ToString() == "0")
                {
                    dtProfileInfo.Rows[e.RowHandle]["ReadOnlyAccess"] = 1;
                }
                else dtProfileInfo.Rows[e.RowHandle]["ReadOnlyAccess"] = 0;
            }

            if (e.Column.FieldName.ToString() == "ReadOnlyAccess")
            {
                if (dtProfileInfo.Rows[e.RowHandle]["ReadOnlyAccess"].ToString() == "0")
                {
                    dtProfileInfo.Rows[e.RowHandle]["FullAccess"] = 1;
                }
                else dtProfileInfo.Rows[e.RowHandle]["FullAccess"] = 0;
            }
            dtProfileInfo.AcceptChanges();
            gvSecuritySettings.RefreshRowCell(e.RowHandle, gcAccess);
            gvSecuritySettings.RefreshRowCell(e.RowHandle, gcReadonly);
          //  gridControl1.Refresh();
            
        }

        private DataTable getUserList(string excludeUserID, int TemplateID)
        {
            MWDataManager.clsDataAccess _UserList = new MWDataManager.clsDataAccess();

            _UserList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_UserList.SqlStatement = "SELECT '' AS USERID, '' AS NAME " +
            //                            "UNION ALL SELECT dept.DepartmentID ,dept.Description FROM dbo.PlanProt_Template PPT " +
            //                         "INNER JOIN dbo.PlanProt_ProfileAccess PPPA ON " +
            //                         "PPT.TemplateID = PPPA.TemplateID " +                           
            //                         "INNER JOIN tblDepartments dept on " +                                   
            //                         "dept.DepartmentID= PPPA.DepartmentID " +
            //                         "WHERE PPT.TemplateID = " + TemplateID.ToString() + " AND " +
            //                         "PPPA.AccessLevel = 1 AND " +
            //                         "dept.DepartmentID <> '" + excludeUserID + "'" ;

            _UserList.SqlStatement = "SELECT '' AS USERID, '' AS NAME " +
                                       "UNION ALL SELECT dept.USERID ,dept.NAME + ' ' + dept.LastName NAME FROM dbo.PlanProt_Template PPT " +
                                    "INNER JOIN dbo.PlanProt_ProfileAccess PPPA ON " +
                                    "PPT.TemplateID = PPPA.TemplateID " +
                                    "INNER JOIN tblUsers dept on " +
                                    "dept.DepartmentID= PPPA.DepartmentID " +
                                    "WHERE PPT.TemplateID = " + TemplateID.ToString() + " AND " +
                                    "PPPA.AccessLevel = 1 AND " +
                                    "dept.USERID <> '" + excludeUserID + "'";
            _UserList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _UserList.queryReturnType = MWDataManager.ReturnType.DataTable;
            _UserList.ExecuteInstruction();
            //"INNER JOIN dbo.USERS USERS ON " +
            //"USERS.USERPROFILEID = PPPA.ProfileName " +

            return _UserList.ResultsDataTable;

        }



        private void gvShaft_RowUpdated_1(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GridView gridView = gridControl1.FocusedView as GridView;
            object row = gridView.GetRow(gridView.FocusedRowHandle);

            (row as DataRowView)["IsUpdated"] = 1;


            //dsTemplateSecurity.theShaft.Rows[gridView.FocusedRowHandle]["IsUpdated"] = 1;
        }

        private void gvSection_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GridView gridView = gridControl1.FocusedView as GridView;
            object row = gridView.GetRow(gridView.FocusedRowHandle);

            (row as DataRowView)["IsUpdated"] = 1;


            //dsTemplateSecurity.theShaft.Rows[gridView.FocusedRowHandle]["IsUpdated"] = 1;
        }

        private void gvUnits_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GridView gridView = gridControl1.FocusedView as GridView;
            object row = gridView.GetRow(gridView.FocusedRowHandle);

            (row as DataRowView)["IsUpdated"] = 1;


            //dsTemplateSecurity.theShaft.Rows[gridView.FocusedRowHandle]["IsUpdated"] = 1;
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mwButton2_Click(object sender, EventArgs e)
        {
            if (CanClose)
            {
                CanClose = true;
                OnSendTaskComplete(new SendTaskCompleteHandlerArg(TaskID, TaskResult.Complete));
                OnCloseTabRequest(new CloseTabArg(tabCaption)); ;
            }
            else
            {
                MessageResult myresult = MessageItem.viewMessage(MessageType.Question, "Cancel Action", "If you cancel you will lose all changes", ButtonTypes.YesNo, MessageDisplayType.FullScreen);
                if (myresult == MessageResult.Yes)
                {
                    CanClose = true;
                    OnSendTaskComplete(new SendTaskCompleteHandlerArg(TaskID, TaskResult.Cancel));
                    OnCloseTabRequest(new CloseTabArg(tabCaption)); ;
                }
            }
        }


      
    }
}