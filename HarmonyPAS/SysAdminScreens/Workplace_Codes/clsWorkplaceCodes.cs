using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mineware.Systems.Global;
using System.Windows.Forms;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Workplace_Codes
{
    class clsWorkplaceCodes:clsBase 
    {
        public DataTable loadDivision()
        {
            theData.SqlStatement = "SELECT DivisionCode, (DivisionCode + ':' + Description) DivisionCodeDescription FROM CODE_WPDivision ORDER BY DivisionCode";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

        public DataTable loadGridEdit(string action,string Division,string Code)
        {
            if (action == "Add")
            {
                theData.SqlStatement = "  SELECT TypeCode Code, Description, \r\n " +
                                      " cast(0 as bit) Selected  \r\n " +
                                      "  FROM CODE_WPTYPE \r\n " +
                                      "  ORDER BY TypeCode ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            else
            {
                theData.SqlStatement = " SELECT c.TypeCode Code, c.Description, \r\n " +
                                         "  Selected = case when isnull(g.Grid,'') <> '' then cast(1 as bit) else cast(0 as bit) end \r\n " +
                                         "  FROM CODE_WPTYPE c \r\n " +
                                         "  left outer join CODE_WPTypeGridLink g on \r\n " +
                                         "    g.TypeCode = c.TypeCode and \r\n " +
                                         "    g.Division = '" + Division + "' and \r\n " +
                                         "    g.Grid = '" + Code + "'  \r\n " +
                                         "  ORDER BY c.TypeCode ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            return theData.ResultsDataTable;
        }
        public DataTable getData(int selection)
        {
          
            switch (selection)
            {
                case 0:

                    theData.SqlStatement = "SELECT DivisionCode Code, Description, case when Selected='Y' then cast(1 as bit) else cast(0 as bit) end Selected, Density, Editable FROM CODE_WPDivision ORDER BY DivisionCode ";
                    break;
                case 1:

                    theData.SqlStatement = "SELECT TypeCode Code, Description,case when Selected='Y' then cast(1 as bit) else cast(0 as bit) end Selected,case when Inactive='Y' or Inactive is null then cast(1 as bit) else cast(0 as bit) end Inactive, case when Classification='Y' or Classification is null then cast(1 as bit) else cast(0 as bit) end Classification FROM CODE_WPTYPE ORDER BY TypeCode ";

                    break;
                case 2:

                    //  theData.SqlStatement = "SELECT distinct(Division) Division, Grid Code, Description, 'Y' Selected, CostArea FROM CODE_Grid ORDER BY Division, Grid ";
                    theData.SqlStatement = "SELECT distinct(Division) Division,TheAction='', Grid Code, Description, cast(1 as bit) Selected, CostArea FROM CODE_Grid ORDER BY Division, Grid ";

                    break;
                case 3:

                    theData.SqlStatement = "SELECT DetailCode Code, Description, case when Selected='Y' then cast(1 as bit) else cast(0 as bit) end Selected,case when  Inactive='Y'  then cast(1 as bit) else cast(0 as bit) end Inactive FROM CODE_WPDETAIL ORDER BY DetailCode ";
                    break;
                case 4:

                    theData.SqlStatement = "SELECT NumberCode Code, Description, case when Selected='Y' then cast(1 as bit) else cast(0 as bit) end Selected,case when  Inactive='Y'  then cast(1 as bit) else cast(0 as bit) end Inactive FROM CODE_WPNUMBER ORDER BY NumberCode ";
                    break;
                case 5:

                    theData.SqlStatement = "SELECT DescrCode Code, Description, case when Selected='Y' then cast(1 as bit) else cast(0 as bit) end Selected, case when Inactive='Y'  then cast(1 as bit) else cast(0 as bit) end Inactive FROM CODE_WPDESCRIPTION ORDER BY DescrCode ";
                    break;
                case 6:

                    theData.SqlStatement = "SELECT DescrNumberCode Code, Description, case when Selected='Y' then cast(1 as bit) else cast(0 as bit) end Selected,case when  Inactive='Y'  then cast(1 as bit) else cast(0 as bit) end Inactive FROM CODE_WPDESCRIPTIONNO ORDER BY DescrNumberCode ";
                    break;
                case 7:

                    theData.SqlStatement = "SELECT Reason , Reason Code, case when Selected='Y' then cast(1 as bit) else cast(0 as bit) end Selected,case when  Inactive='Y'  then cast(1 as bit) else cast(0 as bit) end Inactive FROM WorkPlace_Inactivation_Reason ORDER BY Reason ";
                    break;
                case 8:

                    //theData.SqlStatement = " select * from ( select TypeCode, Description from CODE_WPTYPE )a \r\n " +
                    //                " left outer join \r\n " +
                    //                " (select TypeCode TypeCode1, SetupCode SetupCode1 from wptype_setup)b on a.TypeCode = b.TypeCode1 \r\n " +
                    //                " order by a.TypeCode ";


                    theData.SqlStatement = "  select d.TypeCode Code, d.Description, case when d.Development = 0 or d.Development is null then cast(0 as bit) else cast(1 as bit) end Development, \r\n " +
                                           " case when d.Stoping = 0 or d.Stoping is null then cast(0 as bit) else cast(1 as bit) end Stoping, \r\n " +
                                           " case when d.OtherUnderground = 0 or d.OtherUnderground is null then cast(0 as bit) else cast(1 as bit) end OtherUnderground, \r\n " +
                                           " case when d.Surface = 0 or d.Surface is null then cast(0 as bit) else cast(1 as bit) end Surface from \r\n " +
                                           " (select distinct TypeCode, Description,max(Development)Development,max(Stoping)Stoping,max(OtherUnderground)OtherUnderground,max(Surface) Surface \r\n " +
                                           "  from(select TypeCode, Description from CODE_WPTYPE )a \r\n " +
                                           " left outer join \r\n " +
                                           " (select TypeCode TypeCode1, \r\n " +
                                           " case when SetupCode = 'D' THEN cast(1 as int) end Development, \r\n " +
                                           " case when SetupCode = 'S' THEN cast(1 as int) end Stoping, \r\n " +
                                           " case when SetupCode = 'OUG' THEN cast(1 as int) end OtherUnderground, \r\n " +
                                           " case when SetupCode = 'SU' THEN cast(1 as int) end Surface \r\n " +
                                           " from wptype_setup)b on a.TypeCode = b.TypeCode1 " +
                                           " group by TypeCode,Description)d order by TypeCode ";
                    break;


             
            }
          
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
          



            return theData.ResultsDataTable;
        }

       public  void savedata(string Edit, string division, string code, string description,string costarea, DataTable gridedit)
        {
            if (Edit == "Add")
            {
                //foreach (DataRow dd in main.Rows)
                //{
                //    if (dd["TheAction"].ToString() == "Add")
                //    {
                        //division = dd["Division"].ToString();
                        //code = dd["Code"].ToString();
                        //description = dd["Description"].ToString();
                        //costarea = dd["CostArea"].ToString();
                        //theData.SqlStatement = " SELECT * FROM CODE_Grid " +
                        //        " WHERE (Grid = '" + dd["Code"].ToString () + "' OR Description = '" + dd["Description"].ToString () + "') " +
                        //         " and Division = '" + dd["Division"].ToString () + "' ";
                theData.SqlStatement = " SELECT * FROM CODE_Grid " +
                                " WHERE (Grid = '" + code + "' OR Description = '" + description + "') " +
                                 " and Division = '" + division + "' ";
                // if (SysSettings.IsCentralized.ToString() == "1")
                //     theData  += " AND Mine='" + mine + "' ";
                // theData.SqlStatement = sqlStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        theData.ExecuteInstruction();
                //    }
                //}

                if (theData.ResultsDataTable.Rows.Count > 0)
                {
                    MessageBox.Show("Cannot insert a duplicate record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    theData.SqlStatement  = "";
                    theData.SqlStatement += " delete from  CODE_WPTypeGridLink \r\n " +
                                 " where Grid = '" + code + "' and \r\n " +
                                 "      Division = '" + division + "' \r\n ";
                    //if (SysSettings.IsCentralized.ToString() == "1")
                    //{
                    //    sqlStatement += " INSERT INTO CODE_Grid(Grid,Description,Division,CostArea, Mine) \r\n ";
                    //    sqlStatement += " Values ( \r\n ";
                    //    sqlStatement += " '" + txtGrid.Text + "', '" + txtGridDescription.Text + "', \r\n ";
                    //    sqlStatement += " '" + ddlGridDivision.SelectedValue.ToString() + "', \r\n ";
                    //    sqlStatement += " '" + txtGridCostArea.Text + "', \r\n ";
                    //    sqlStatement += " '" + mine + "') \r\n ";
                    //}
                    //else
                    //{
                    theData.SqlStatement += " INSERT INTO CODE_Grid(Grid,Description,Division,CostArea) \r\n " +
                                " Values ( \r\n " +
                                 " '" + code + "', '" + description + "', \r\n " +
                                 " '" + division + "', \r\n " +
                                 " '" + costarea + "') \r\n ";
                   // }
                    //for (int i = 0; i < gridedit  .Rows.Count - 1; i++)
                    //{
                    //    if (gridedit.Rows[i].Cells["Selected"].Tag == "Y")
                    //    {
                    foreach (DataRow dr in gridedit .Rows )
                    { 
                        if(Convert .ToBoolean ( dr["Selected"]) == true )
                        { 
                            theData.SqlStatement += " INSERT INTO CODE_WPTypeGridLink(Grid, Division, TypeCode) \r\n " +
                                                     " Values ( \r\n " +
                                                     " '" + code + "', '" + division + "', \r\n " +
                                                     " '" +dr["Code"].ToString() + "') \r\n ";
                        }
                    }
                }
            }
            else
            { ////edit
                //foreach (DataRow dd in main.Rows)
                //{
                //    if (dd["TheAction"].ToString() == "Edit" || dd["TheAction"].ToString() == "")
                //    {
                //        division = dd["Division"].ToString();
                //        code = dd["Code"].ToString();
                //        description = dd["Description"].ToString();
                //        costarea = dd["CostArea"].ToString();
                //    }
                //}
                theData.SqlStatement = "";
                theData.SqlStatement += " delete from  CODE_WPTypeGridLink \r\n " +
                                        " where Grid = '" + code + "' and \r\n " +
                                        "      Division = '" + division + "' \r\n " +
                                        "Update CODE_Grid \r\n " +
                                        "set Description = '" + description + "', \r\n " +
                                        " CostArea='" + costarea  + "' \r\n " +
                                        " WHERE Grid = '" + code + "' and \r\n " +
                                        "       Division = '" + division + "' \r\n ";
                //if (SysSettings.IsCentralized.ToString() == "1")
                //    sqlStatement += " AND Mine='" + mine + "' \r\n ";
                foreach (DataRow dr in gridedit.Rows)
                {
                    if (Convert.ToBoolean(dr["Selected"]) == true)
                    {
                        theData.SqlStatement += " INSERT INTO CODE_WPTypeGridLink(Grid, Division, TypeCode) \r\n " +
                                                 " Values ( \r\n " +
                                                 " '" + code + "', '" + division + "', \r\n " +
                                                 " '" + dr["Code"].ToString() + "') \r\n ";
                    }
                }

            }

            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
        }

        public  DataTable  Load_WPType()
        {
            

            theData.SqlStatement = "select '' TypeCode, '' Description union all SELECT TypeCode, Description FROM CODE_WPType ORDER BY TypeCode";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;

        }
    }
}
