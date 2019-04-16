using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PlanningProtocolTemplates
{
    public partial class frmFieldSetup : DevExpress.XtraEditors.XtraForm
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        public frmFieldSetup()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable();
        DataTable dtDescription = new DataTable();
        DataTable dtParent = new DataTable();
        DataTable dtGrid = new DataTable();
        DataTable dtFieldID = new DataTable();
        public Int32 TempId = 0;
        Int32 ParentID = 0;
        string Max;
        public currentAction formAction1;
        public int FieldID = 0;
        Int32 FieldValuesFieldID = 0;



        private void LoadData()
        {
            MWDataManager.clsDataAccess _dbManName = new MWDataManager.clsDataAccess();
            _dbManName.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
            _dbManName.SqlStatement = "Select * from planProt_Fields where FieldID = '"+FieldID+"' ";
            _dbManName.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManName.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManName.ExecuteInstruction();

            DataTableReader readerName = new DataTableReader(_dbManName.ResultsDataTable);
            dtParent.Load(readerName);

            foreach (DataRow dt in dtParent.Rows)
            {
                txtFieldName.EditValue = dt["FieldName"].ToString();
                txtLines.EditValue = dt["NoLines"].ToString();
                txtCharacters.EditValue = dt["NoCharacters"].ToString();
                if (dt["FieldRequired"].ToString() == "True")
                {
                    checkEdit1.Checked  = true;
                }
                else
                {
                    checkEdit1.Checked  = false;
                }
                if (dt["Deleted"].ToString() == "True")
                {
                    ceDeleted.Checked = true;
                }
                else
                {
                    ceDeleted.Checked = false;
                }
            }



            MWDataManager.clsDataAccess _dbManFieldType = new MWDataManager.clsDataAccess();
            _dbManFieldType.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
            _dbManFieldType.SqlStatement = "select FieldID,[FieldType] FieldTypeID,   " +
                                           "Case when FieldType = 1 then 'Grouping Level 1' else " +
                                           "Case when FieldType = 2 then 'Number' else " +
                                           "Case when FieldType = 3 then 'Real' else " +
                                           "Case when FieldType = 4 then 'Date' else " +
                                           "Case when FieldType = 5 then 'Alpha' else " +
                                           "Case when FieldType  = 6 then 'Selection' else " +
                                           "Case when FieldType = 7 then 'Grouping Level 2' else " +
                                           "Case when FieldType = 8 then 'Grouping Level 3' else " +
                                           "Case when FieldType = 9 then 'Grouping Level 4' " +
                                           "end end end end end end end end end FieldType " +
                                           "from PlanProt_Fields " +
                                           "where FieldID = '" + FieldID + "' ";
            _dbManFieldType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManFieldType.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManFieldType.ExecuteInstruction();

            DataTableReader readerType = new DataTableReader(_dbManFieldType.ResultsDataTable);
            dtDescription.Load(readerType);

            foreach (DataRow dr in dtDescription.Rows)
            {
                cmbFieldType.EditValue = Convert.ToInt32( dr["FieldTypeID"].ToString());
            }


            MWDataManager.clsDataAccess _dbManParent = new MWDataManager.clsDataAccess();
            _dbManParent.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
            _dbManParent.SqlStatement = "select distinct pf.FieldName [FieldName] from PlanProt_Data pg " +
                                        "left outer join  PlanProt_Fields pf " +
                                        "on pf.FieldID = pg.FieldID " +
                                        "where pg.FieldID = '" + FieldID + "' ";
            _dbManParent.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManParent.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManParent.ExecuteInstruction();

            DataTableReader readerParent = new DataTableReader(_dbManParent.ResultsDataTable);
            dtParent.Load(readerParent);

            foreach (DataRow dt in dtParent.Rows)
            {
                cmbParent.EditValue = dt["FieldName"].ToString();
            }

            table.Columns.Clear();

            if (cmbFieldType.Text == "Selection")
            {
                //dtGrid.Columns.Clear();
               // checkEdit1.Visible = false;
                MWDataManager.clsDataAccess _dbManGrid = new MWDataManager.clsDataAccess();
                _dbManGrid.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                _dbManGrid.SqlStatement = "Select MinValue [Date with in],RiskRating [Risk Rating]  from planProt_FieldValue where FieldID = '" + FieldID + "' ";
                _dbManGrid.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGrid.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGrid.ExecuteInstruction();

                DataTableReader readerGrid = new DataTableReader(_dbManGrid.ResultsDataTable);
                table.Load(readerGrid);
                bandedGridView1.PopulateColumns(table);
                
            }
            else
            {

                MWDataManager.clsDataAccess _dbManGrid = new MWDataManager.clsDataAccess();
                _dbManGrid.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                _dbManGrid.SqlStatement = "Select MinValue,MaxValue,RiskRating [Risk Rating]  from planProt_FieldValue where FieldID = '" + FieldID + "' ";
                _dbManGrid.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGrid.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGrid.ExecuteInstruction();

                DataTableReader readerGrid = new DataTableReader(_dbManGrid.ResultsDataTable);
                table.Load(readerGrid);
                bandedGridView1.PopulateColumns(table);
            }

            if (cmbFieldType.Text == "Alpha")
            {
                lcLines.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                lcCharacters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                //checkEdit1.Visible = true;
            }
            else
            {
                lcLines.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                lcLines.HideToCustomization();
                lcCharacters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                lcCharacters.HideToCustomization();
               // checkEdit1.Visible = false;
            }

            if (cmbFieldType.Text == "Alpha" || cmbFieldType.Text == "Real" || cmbFieldType.Text == "Number")
            {
                checkEdit1.Enabled = true;
                //checkEdit1.Visible = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;

            }
            else
            {
                checkEdit1.Enabled  = false;
            }

            

            //bandedGridView1.PopulateColumns(dtGrid);
            gridControl1.DataSource = table;

        }


        private void cmbFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            table.Columns.Clear();
            
            
            if ((cmbFieldType.Text == "Number") || (cmbFieldType.Text == "Real"))
            {
                
                table.Columns.Add("Min");
                table.Columns.Add("Max");
                table.Columns.Add("Risk Rating");
                lcRiskRatingGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                checkEdit1.Enabled = true;

            }
            else 
            if (cmbFieldType.Text == "Date")
            {

                table.Columns.Add("Date with in");
                table.Columns.Add("Date");
                table.Columns.Add("Risk Rating");


                lcRiskRatingGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                checkEdit1.Enabled = false;
              
            }
            else if (cmbFieldType.Text == "Selection")
            {

               
                table.Columns.Add("Selection");
                table.Columns.Add("Risk Rating");
                lcRiskRatingGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                checkEdit1.Enabled = false;
               
            }
            else if (cmbFieldType.Text == "Alpha")
            {
                checkEdit1.Enabled = true;
            }
            else if (cmbFieldType.Text == "Grouping")
            {
                checkEdit1.Enabled = false;
            }
            else
            {
                lcRiskRatingGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                checkEdit1.Enabled = false;
            }
            bandedGridView1.PopulateColumns(table);
            gridControl1.DataSource = table;
            
            
        }

        private void gridRating_Click(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbParent.ItemIndex == -1)
                {
                    ParentID = 0;
                }
                else
                {
                    ParentID = Convert.ToInt32( cmbParent.EditValue.ToString());
                }

                if (table.Rows.Count == 0 && formAction1 == currentAction.caAdd)
                {
                    MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
                    _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                    _dbManInsert.SqlStatement = "Select MAX(FIELDID) FIELDID from PlanProt_Fields";
                    _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManInsert.ExecuteInstruction();


                    Int32 TheFieldID = Convert.ToInt32(_dbManInsert.ResultsDataTable.Rows[0]["FIELDID"].ToString())+1;

                    _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                    _dbManInsert.SqlStatement = "Insert into PlanProt_Fields(FieldID, TemplateID,FieldName,FieldType,NoCharacters,NoLines) values('" + TheFieldID + "','" + TempId + "','" + txtFieldName.Text + "','" + cmbFieldType.EditValue.ToString() + "','" + lcCharacters .Text + "','" + lcLines .Text + "')";
                    _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManInsert.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbManFieldName = new MWDataManager.clsDataAccess();
                    _dbManFieldName.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                    _dbManFieldName.SqlStatement = "select FieldId = FieldID from PlanProt_Fields " +
                                                    "where FieldName = '" + txtFieldName.Text + "'";
                    _dbManFieldName.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManFieldName.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManFieldName.ExecuteInstruction();

                    DataTableReader readerName = new DataTableReader(_dbManFieldName.ResultsDataTable);
                    dtFieldID.Load(readerName);

                    foreach (DataRow dt in dtFieldID.Rows)
                    {
                        FieldValuesFieldID = Convert.ToInt32(dt["FieldId"].ToString());
                    }

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = "sp_PlanProt_TemplateSetupSave";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    SqlParameter[] _paramCollection =      
                    { 
                         _dbMan.CreateParameter("@TemplateID", SqlDbType.Int , 7,TempId),
                         _dbMan.CreateParameter("@TemplateName", SqlDbType.VarChar , 7,DBNull.Value),
                         _dbMan.CreateParameter("@Activity", SqlDbType.Int , 7, DBNull.Value),
                         _dbMan.CreateParameter("@FieldID", SqlDbType.Int ,50 ,FieldValuesFieldID ),
                        // _dbMan.CreateParameter("@FieldType", SqlDbType.VarChar ,30 ,cmbFieldType.SelectedValue.ToString() ), 
                         _dbMan.CreateParameter("@SelectedValue", SqlDbType.VarChar, 50,DBNull.Value ),
                         _dbMan.CreateParameter("@MinValue", SqlDbType.VarChar, 30, DBNull.Value ),
                         _dbMan.CreateParameter("@MaxValue", SqlDbType.VarChar, 30,DBNull.Value ),                
                         _dbMan.CreateParameter("@RiskRating", SqlDbType.Int,3 , DBNull.Value), 
                         _dbMan.CreateParameter("@ParentID", SqlDbType.Int, 3, ParentID ),
                         _dbMan.CreateParameter("@FrontBack", SqlDbType.Int,3 ,2 ),
                         _dbMan.CreateParameter("@Action", SqlDbType.Int,3 ,0 ),
                          _dbMan.CreateParameter("@FieldUpdate", SqlDbType.Int,3 ,0 ),
                         _dbMan.CreateParameter("@User1", SqlDbType.VarChar,50 ,"" ),
                          _dbMan.CreateParameter("@User2", SqlDbType.VarChar,50 ,""),
                          _dbMan.CreateParameter("@AprovalReq", SqlDbType.Int,0 ,0 )

                    };
                    _dbMan.ParamCollection = _paramCollection;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();

                    MessageBox.Show("Data Saved Successfull", "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (formAction1 == currentAction.caAdd)
                    {
                        Boolean Deleted = false;

                        if (ceDeleted.Checked == true)
                        {
                            Deleted = true;
                        }

                        MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
                        _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                        _dbManInsert.SqlStatement = "Insert into PlanProt_Fields(TemplateID,FieldName,FieldType, Deleted) values('" + TempId + "','" + txtFieldName.Text + "','" + cmbFieldType.EditValue.ToString() + "','"+Deleted+"')";
                        _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManInsert.ExecuteInstruction();

                        MWDataManager.clsDataAccess _dbManFieldName = new MWDataManager.clsDataAccess();
                        _dbManFieldName.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                        _dbManFieldName.SqlStatement = "select FieldId = FieldID from PlanProt_Fields " +
                                                        "where FieldName = '" + txtFieldName.Text + "'";
                        _dbManFieldName.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManFieldName.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManFieldName.ExecuteInstruction();

                        DataTableReader readerName = new DataTableReader(_dbManFieldName.ResultsDataTable);
                        dtFieldID.Load(readerName);

                        foreach (DataRow dt in dtFieldID.Rows)
                        {
                            FieldValuesFieldID = Convert.ToInt16(dt["FieldId"].ToString());
                        }

                        foreach (DataRow dr in table.Rows)
                        {

                            if (cmbFieldType.Text == "Selection")
                            {
                                Max = dr[1].ToString();
                                Max = "0";

                            }
                            else
                            {
                                Max = dr[1].ToString();
                            }

                            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _dbMan.SqlStatement = "sp_PlanProt_TemplateSetupSave";
                            _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            SqlParameter[] _paramCollection =      
                            { 
                                 _dbMan.CreateParameter("@TemplateID", SqlDbType.Int , 7,TempId),
                                 _dbMan.CreateParameter("@TemplateName", SqlDbType.VarChar , 7," "),
                                 _dbMan.CreateParameter("@Activity", SqlDbType.Int , 7, 0),
                                 _dbMan.CreateParameter("@FieldID", SqlDbType.Int ,50 ,FieldValuesFieldID ),
                                // _dbMan.CreateParameter("@FieldType", SqlDbType.VarChar ,30 ,cmbFieldType.SelectedValue.ToString() ), 
                                 _dbMan.CreateParameter("@SelectedValue", SqlDbType.VarChar, 50,cmbFieldType.EditValue.ToString()  ),
                                 _dbMan.CreateParameter("@MinValue", SqlDbType.VarChar, 30, dr[0].ToString()),
                                 _dbMan.CreateParameter("@MaxValue", SqlDbType.VarChar, 30, Max),                
                                 _dbMan.CreateParameter("@RiskRating", SqlDbType.Int,3 ,dr["Risk Rating"].ToString()), 
                                 _dbMan.CreateParameter("@ParentID", SqlDbType.Int, 3, ParentID ),
                                 _dbMan.CreateParameter("@FrontBack", SqlDbType.Int,3 ,2 ),
                                 _dbMan.CreateParameter("@Action", SqlDbType.Int,3 ,0 ),
                                  _dbMan.CreateParameter("@FieldUpdate", SqlDbType.Int,3 ,0 ),
                                  _dbMan.CreateParameter("@User1", SqlDbType.VarChar,50 ,"" ),
                                  _dbMan.CreateParameter("@User2", SqlDbType.VarChar,50 ,""),
                                  _dbMan.CreateParameter("@AprovalReq", SqlDbType.Int,0 ,0 )


                            };

                            _dbMan.ParamCollection = _paramCollection;
                            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                            clsDataResult result = _dbMan.ExecuteInstruction();
                            

                        }

                        MessageBox.Show("Data Saved Successfull", "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }


                    else // Forms CurrentAction = Edit
                    {

                        if (checkEdit1.Checked == true)
                        {
                            bool abc = true;
                            MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
                            _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                            _dbManInsert.SqlStatement = "Update PlanProt_Fields Set TemplateID = '" + TempId + "' ,FieldName = '" + txtFieldName.Text + "' ,FieldType = '" + cmbFieldType.EditValue.ToString() + "',NoCharacters='" + txtCharacters.Text + "',NoLines='" + txtLines.Text + "',FieldRequired='" + abc + "', Deleted = '"+ceDeleted.Checked+"' where FieldID = '" + FieldID + "' ";
                            _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _dbManInsert.ExecuteInstruction();
                        }
                        else
                        {
                            bool abc1 = false ;
                            MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
                            _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                            _dbManInsert.SqlStatement = "Update PlanProt_Fields Set TemplateID = '" + TempId + "' ,FieldName = '" + txtFieldName.Text + "' ,FieldType = '" + cmbFieldType.EditValue.ToString() + "',NoCharacters='" + txtCharacters.Text + "',NoLines='" + txtLines.Text + "',FieldRequired='" + abc1 + "', Deleted = '" + ceDeleted.Checked + "' where FieldID = '" + FieldID + "' ";
                            _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _dbManInsert.ExecuteInstruction();
                        }

                        MWDataManager.clsDataAccess _dbManFieldName = new MWDataManager.clsDataAccess();
                        _dbManFieldName.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                        _dbManFieldName.SqlStatement = "select FieldId = FieldID from PlanProt_Fields " +
                                                        "where FieldName = '" + txtFieldName.Text + "' and TemplateID = '"+ TempId + "'";
                        _dbManFieldName.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManFieldName.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManFieldName.ExecuteInstruction();

                        DataTableReader readerName = new DataTableReader(_dbManFieldName.ResultsDataTable);
                        dtFieldID.Load(readerName);

                        MWDataManager.clsDataAccess _dbManDelete = new MWDataManager.clsDataAccess();
                        _dbManDelete.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                        _dbManDelete.SqlStatement = "delete from PlanProt_FieldValue where FieldID = '"+FieldID+"'";
                        _dbManDelete.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManDelete.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManDelete.ExecuteInstruction();


                        foreach (DataRow dt in dtFieldID.Rows)
                        {
                            FieldValuesFieldID = Convert.ToInt32(dt["FieldId"].ToString());
                        }

                        foreach (DataRow dr in table.Rows)
                        {

                            if (cmbFieldType.Text == "Selection")
                            {
                                //Max = dr[1].ToString();
                                Max = "0";

                            }
                            else
                            {
                                Max = dr[1].ToString();
                            }

                            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _dbMan.SqlStatement = "sp_PlanProt_TemplateSetupSave";
                            _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                            SqlParameter[] _paramCollection =      
                            { 
                                    _dbMan.CreateParameter("@TemplateID", SqlDbType.Int , 7,TempId),
                                    _dbMan.CreateParameter("@TemplateName", SqlDbType.VarChar , 7," "),
                                    _dbMan.CreateParameter("@Activity", SqlDbType.Int , 7, 0),
                                    _dbMan.CreateParameter("@FieldID", SqlDbType.Int ,50 ,FieldValuesFieldID ),
                                    //_dbMan.CreateParameter("@FieldType", SqlDbType.VarChar ,30 ,cmbFieldType.EditValue.ToString() ), 
                                    _dbMan.CreateParameter("@SelectedValue", SqlDbType.VarChar,2, cmbFieldType.EditValue.ToString() ),
                                    _dbMan.CreateParameter("@MinValue", SqlDbType.VarChar, 50, dr[0].ToString()),
                                    _dbMan.CreateParameter("@MaxValue", SqlDbType.VarChar, 4, Max),                
                                    _dbMan.CreateParameter("@RiskRating", SqlDbType.Int,3 ,dr["Risk Rating"].ToString()), 
                                    _dbMan.CreateParameter("@ParentID", SqlDbType.Int, 3, ParentID ),
                                    _dbMan.CreateParameter("@FrontBack", SqlDbType.Int,3 ,2 ),
                                    _dbMan.CreateParameter("@Action", SqlDbType.Int,3 ,0 ),
                                    _dbMan.CreateParameter("@FieldUpdate", SqlDbType.Int,3 ,1 ),
                                    _dbMan.CreateParameter("@User1", SqlDbType.VarChar,50 ,"" ),
                                    _dbMan.CreateParameter("@User2", SqlDbType.VarChar,50 ,""),
                                    _dbMan.CreateParameter("@AprovalReq", SqlDbType.Int,0 ,0 )


                            };

                            _dbMan.ParamCollection = _paramCollection;
                            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                            clsDataResult result =  _dbMan.ExecuteInstruction();

                        }

                        MessageBox.Show("Data Edited Successfull", "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }



                    
                }

                this.Close();
           
            }
        
            catch (SqlException x)
            {
                MessageBox.Show("Data could not be saved" + x.Message.ToString(), "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmFieldSetup_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (formAction1 == currentAction.caAdd)
            {

                MWDataManager.clsDataAccess _dbManFieldType = new MWDataManager.clsDataAccess();
                _dbManFieldType.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                _dbManFieldType.SqlStatement = "select * from PlanProt_FieldTypes";
                _dbManFieldType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManFieldType.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManFieldType.ExecuteInstruction();

                cmbFieldType.Properties.BeginUpdate();
                cmbFieldType.Properties.DataSource = _dbManFieldType.ResultsDataTable;
                cmbFieldType.Properties.DisplayMember = "FieldDescription";
                cmbFieldType.Properties.ValueMember = "FieldTypeID";
                cmbFieldType.Properties.EndUpdate();





                MWDataManager.clsDataAccess _dbManParent = new MWDataManager.clsDataAccess();
                _dbManParent.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                //_dbManParent.SqlStatement = "select * from PlanProt_Fields p left outer join " +
                //                            "PlanProt_FieldTypes pft on " +
                //                            "p.FieldType = pft.fieldTypeID  " +
                //                            "where Templateid = '" + TempId + "' " +
                //                            "and (fieldTypeID = 1 or " + 
                //                            "fieldTypeID = 7 or " +
                //                            "fieldTypeID = 8 or " +
                //                            "fieldTypeID = 9)";
                _dbManParent.SqlStatement = "select * from PlanProt_Fields p left outer join " +
                                          "PlanProt_FieldTypes pft on " +
                                          "p.FieldType = pft.fieldTypeID  " +
                                          "where Templateid = '" + TempId + "' " +
                                          "and (fieldTypeID = 1 or " +
                                          "fieldTypeID = 2 or " +
                                          "fieldTypeID = 5 or " +
                                          "fieldTypeID = 6)";
                _dbManParent.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManParent.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManParent.ExecuteInstruction();

                cmbParent.Properties.BeginUpdate();
                cmbParent.Properties.DataSource = _dbManParent.ResultsDataTable;
                cmbParent.Properties.DisplayMember = "FieldName";
                cmbParent.Properties.ValueMember = "FieldID";
                cmbParent.Properties.EndUpdate();


                cmbParent.ItemIndex = -1;

            }
            else
            {

                MWDataManager.clsDataAccess _dbManFieldType = new MWDataManager.clsDataAccess();
                _dbManFieldType.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                _dbManFieldType.SqlStatement = "select [FieldTypeID] ,[FieldDescription] from PlanProt_FieldTypes";
                _dbManFieldType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManFieldType.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManFieldType.ExecuteInstruction();

                cmbFieldType.Properties.BeginUpdate();
                cmbFieldType.Properties.DataSource = _dbManFieldType.ResultsDataTable;
                cmbFieldType.Properties.DisplayMember = "FieldDescription";
                cmbFieldType.Properties.ValueMember = "FieldTypeID";
                cmbFieldType.Properties.EndUpdate();



                MWDataManager.clsDataAccess _dbManParent = new MWDataManager.clsDataAccess();
                _dbManParent.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);//ConfigurationSettings.AppSettings["SQLConnectionStr"];
                _dbManParent.SqlStatement = "select * from PlanProt_Fields p left outer join " +
                                            "PlanProt_FieldTypes pft on " +
                                            "p.FieldType = pft.fieldTypeID  " +
                                            "where Templateid = '" + TempId + "' " +
                                        "and (fieldTypeID = 1 or " +
                                        "fieldTypeID = 7 or " +
                                        "fieldTypeID = 8 or " +
                                        "fieldTypeID = 9)";
                _dbManParent.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManParent.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManParent.ExecuteInstruction();

                cmbParent.Properties.BeginUpdate();
                cmbParent.Properties.DataSource = _dbManParent.ResultsDataTable;
                cmbParent.Properties.DisplayMember = "FieldName";
                cmbParent.Properties.ValueMember = "FieldID";
                cmbParent.Properties.EndUpdate();



                LoadData();

            }
        }
    }
}
