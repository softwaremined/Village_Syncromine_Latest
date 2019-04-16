using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Production.SysAdminScreens.OreBody
{
    class clsOreBody:clsBase 
    {
        public string Valid;
        public string edit;
        public DataTable loadOreBody()
        {
            theData.SqlStatement = "SELECT * FROM CommonAreas";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

        public DataTable loadGridEdit(string Division)
        {
            //if (action == "Add")
            //{
                theData.SqlStatement = " SELECT  name SubSection, Id FROM CommonAreaSubSections WHERE CommonArea = '" + Division + "'";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            //}
            //else
            //{
            //    theData.SqlStatement = " SELECT c.TypeCode Code, c.Description, \r\n " +
            //                             "  Selected = case when isnull(g.Grid,'') <> '' then cast(1 as bit) else cast(0 as bit) end \r\n " +
            //                             "  FROM CODE_WPTYPE c \r\n " +
            //                             "  left outer join CODE_WPTypeGridLink g on \r\n " +
            //                             "    g.TypeCode = c.TypeCode and \r\n " +
            //                             "    g.Division = '" + Division + "' and \r\n " +
            //                             "    g.Grid = '" + Code + "'  \r\n " +
            //                             "  ORDER BY c.TypeCode ";
            //    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //    theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    theData.ExecuteInstruction();
            //}
            return theData.ResultsDataTable;
        }


       public  void SaveOreBody(string name, string edit, string action, DataTable gridedit)
        {
            string EditID = "";
            EditID = edit;
            Valid = "N";

            
               
             theData.SqlStatement  = "SELECT * FROM CommonAreas WHERE Name='" + name + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            DataTable dt = new DataTable();
            dt = theData.ResultsDataTable;
            

            int MaxIDCommon = 0;
         
            

            Valid = "Y";
            int MaxIDSub = 0;
            if (EditID == "")
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                  //  MessageBox.Show("Common Area already exists.", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Valid = "N";
                }
                else
                {
                   // if (SysSettings.IsCentralized.ToString() == "0")
                        //_dbMan.SqlStatement = "INSERT INTO CommonAreas(Id, Name) VALUES(" + MaxIDCommon + ", '" + this.txtCommonArea.Text.Trim() + "')";
                        theData.SqlStatement = "INSERT INTO CommonAreas( Name) VALUES( '" + name + "')";
                    //else
                    //    theData.SqlStatement = "INSERT INTO CommonAreas(Id, Name, Mine) VALUES(" + MaxIDCommon + ", '" + this.txtCommonArea.Text.Trim() + "', '" + cmbCommonDiv.SelectedItem.ToString() + "' )";
                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    theData.ExecuteInstruction();
                   
                    if (gridedit.Rows.Count > 0)
                    {
                        theData.SqlStatement = "";
                        

                        theData.SqlStatement = "";
                        foreach (DataRow  row in gridedit.Rows)
                        {
                            if (row["SubSection"].ToString () != "" || row["SubSection"] != null  )
                            {
                                theData.SqlStatement = "INSERT INTO CommonAreaSubSections(CommonArea, Name) VALUES((SELECT max(id) MaxID from CommonAreas),'" + row["SubSection"].ToString() + "') ";
                                
                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                                theData.ExecuteInstruction();
                             
                            }
                        }

                        
                    }
                    //frmMessageFrm MsgFrm = new frmMessageFrm();
                    //MsgFrm.Text = "Record Inserted";
                    //Procedures.MsgText = "Common Area Inserted";
                    //MsgFrm.Show();
                }
            }
            else
            {
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Id"].ToString() != edit )
                {
                   // MessageBox.Show("Common Area already exists.", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Valid = "N";
                }
                else
                {
                    
                    theData.SqlStatement = "UPDATE CommonAreas SET Name = '" + name + "' WHERE ID = " + EditID;
                    theData.ExecuteInstruction();
                   
                    foreach (DataRow  row in gridedit.Rows)
                    {
                        if (row["SubSection"].ToString () != "" || row["SubSection"]!= null )
                        {
                            if (row["Id"].ToString() == "" || row["Id"] == null )
                            {
                                    theData.SqlStatement = "";
                                    theData.SqlStatement += "INSERT INTO CommonAreaSubSections(CommonArea, Name) VALUES(" + EditID + ",'" + row["SubSection"].ToString() + "')";
                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                                theData.ExecuteInstruction();
                            }
                            else
                            {
                                theData.SqlStatement = "";
                                theData.SqlStatement += "UPDATE CommonAreaSubSections SET Name = '" + row["SubSection"].ToString () + "' WHERE ID = '" + row["Id"].ToString() + "' ";
                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                                theData.ExecuteInstruction();
                            }
                        }
                    }
                    // _dbMan.ExecuteInstruction();
                    //frmMessageFrm MsgFrm = new frmMessageFrm();
                    //MsgFrm.Text = "Record Updated";
                    //Procedures.MsgText = "Common Area Updated";
                    //MsgFrm.Show();
                }
            }


        }
    }
}
