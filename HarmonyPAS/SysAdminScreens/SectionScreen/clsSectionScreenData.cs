using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mineware.Systems.Global;
using MWDataManager;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SectionScreen
{
    class clsSectionScreenData : clsBase
    {

        public void updateSecCal(string _prodmonth)
        {
            try
            {

                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                theData.SqlStatement = "sp_UpdateSeccal";
                SqlParameter[] _paramCollection9 =
                    {
                      theData.CreateParameter("@ProdMonth", SqlDbType.VarChar,20, _prodmonth),
                    };
                theData.ParamCollection = _paramCollection9;
                theData.ExecuteInstruction();
            }
            catch (Exception e)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", e.Message, Color.Red);

            }
        }

        public DataTable getSectionScreenList(string _prodmonth)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine(" select s.ProdMonth, s.SectionID, s.Name, s.HierarchicalID, s.ReportToSectionID,");
                sb.AppendLine(" h.Description Occupation, Status = '', s.EmployeeNo IndustryNo ");
                sb.AppendLine(" from Section s, Hierarch h");
                sb.AppendLine(" where s.Hierarchicalid = h.Hierarchicalid and");
                sb.AppendLine(" ProdMonth = '" + _prodmonth + "' order by s.Hierarchicalid, s.SectionID");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

                if (newData.ResultsDataTable.Rows.Count == 0)
                {

                    if (MessageBox.Show("Would you like to copy the Sections from the previous month? All Sections for this month will be overwritten", "Copy Sections", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sb.Clear();
                        sb.AppendLine("INSERT INTO Section (Prodmonth, SectionID, Name, ReportToSectionid, Hierarchicalid, EmployeeNo)");
                        sb.AppendLine(" select ");
                        sb.AppendLine(" (" + _prodmonth + ") Prodmonth,[SectionID],[Name],[ReportToSectionid],[Hierarchicalid],EmployeeNo");
                        sb.AppendLine("  from Section  WHERE");
                        sb.AppendLine("  ProdMonth = [dbo].[GetPrevProdMonth] (" + _prodmonth + ") ");
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        sb.Clear();
                        sb.AppendLine(" select s.ProdMonth, s.SectionID, s.Name, s.HierarchicalID, s.ReportToSectionID,");
                        sb.AppendLine(" h.Description Occupation, Status = ''");
                        sb.AppendLine(" from Section s, Hierarch h");
                        sb.AppendLine(" where s.Hierarchicalid = h.Hierarchicalid and");
                        sb.AppendLine(" ProdMonth = '" + _prodmonth + "' order by s.Hierarchicalid, s.SectionID");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();
                    }

                }

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return newData.ResultsDataTable;
        }


        public DataTable getSectionScreenListOther(string _prodmonth)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine(" select s.ProdMonth, s.SectionID, s.Name, s.HierarchicalID, s.ReportToSectionID,");
                sb.AppendLine(" h.Description Occupation, Status = '', s.EmployeeNo IndustryNo ");
                sb.AppendLine(" from SectionOther s, HierarchOther h");
                sb.AppendLine(" where s.Hierarchicalid = h.Hierarchicalid and");
                sb.AppendLine(" ProdMonth = '" + _prodmonth + "' order by s.Hierarchicalid, s.SectionID");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

                if (newData.ResultsDataTable.Rows.Count == 0)
                {

                    if (MessageBox.Show("Would you like to copy the Sections from the previous month? All Sections for this month will be overwritten", "Copy Sections", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sb.Clear();
                        sb.AppendLine("INSERT INTO SectionOther (Prodmonth, SectionID, Name, ReportToSectionid, Hierarchicalid, EmployeeNo)");
                        sb.AppendLine(" select ");
                        sb.AppendLine(" (" + _prodmonth + ") Prodmonth,[SectionID],[Name],[ReportToSectionid],[Hierarchicalid],EmployeeNo");
                        sb.AppendLine("  from SectionOther  WHERE");
                        sb.AppendLine("  ProdMonth = [dbo].[GetPrevProdMonth] (" + _prodmonth + ") ");
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        sb.Clear();
                        sb.AppendLine(" select s.ProdMonth, s.SectionID, s.Name, s.HierarchicalID, s.ReportToSectionID,");
                        sb.AppendLine(" h.Description Occupation, Status = ''");
                        sb.AppendLine(" from SectionOther s, HierarchOther h");
                        sb.AppendLine(" where s.Hierarchicalid = h.Hierarchicalid and");
                        sb.AppendLine(" ProdMonth = '" + _prodmonth + "' order by s.Hierarchicalid, s.SectionID");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();
                    }

                }

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return newData.ResultsDataTable;
        }

        public DataTable getHierList()
        {
            try
            {
                theData.SqlStatement = " select Hierarchicalid ID, Description Occupation" +
                    " from Hierarch order by HierarchicalID ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }

            return theData.ResultsDataTable;
        }


        public DataTable getHierListOther()
        {
            try
            {
                theData.SqlStatement = " select Hierarchicalid ID, Description Occupation" +
                    " from HierarchOther order by HierarchicalID ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }

            return theData.ResultsDataTable;
        }

        public DataTable getHierName(string _prodmonth, string _sectionID)
        {
            try
            {
                theData.SqlStatement =
                    "select h.hierarchicalid ID, h.Description Occupation from SECTION s " +
                    "inner join HIERARCH h " +
                    "on s.Hierarchicalid = h.HierarchicalID " +
                    "where s.Prodmonth = '" + _prodmonth + "' and s.SectionID = '" + _sectionID + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }

            return theData.ResultsDataTable;
        }

        public DataTable getEmployee(string _prodmonth, string _sectionID)
        {
            try
            {
                theData.SqlStatement =
                    "select h.EmployeeName , s.EmployeeNo from SECTION s " +
                    "inner join EmployeeAll h " +
                    "on s.EmployeeNo = h.EmployeeNo " +
                    "where s.Prodmonth = '" + _prodmonth + "' and s.SectionID = '" + _sectionID + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }

            return theData.ResultsDataTable;
        }

        public DataTable getEmployeeAll(string _prodmonth, string _sectionID)
        {
            try
            {
                theData.SqlStatement =
                    "select EmployeeName , EmployeeNo from  " +
                    "EmployeeAll  " +
                    "" +
                    " ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }

            return theData.ResultsDataTable;
        }
        public DataTable getReportToList(string _prodmonth, int _levelid)
        {
            try
            {
                theData.SqlStatement = " select SectionID ReportToSectionID, Name ReportToName \r\n " +
                    " from Section where Hierarchicalid = " + _levelid + " and \r\n " +
                    " ProdMonth = '" + _prodmonth + "' order by SectionID ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return theData.ResultsDataTable;
        }

        public DataTable getEmployees()
        {
                theData.SqlStatement = "Select EmployeeNo,EmployeeName,EmployeeNo+':'+EmployeeName Emp  from employeeall";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            
            return theData.ResultsDataTable;
        }

        public void SaveData(string _prodMonth, string _sectionID, string _name, int _hierID, string _reportTo, string action,string _EmployeeNo)
        {
            clsDataAccess newData = new clsDataAccess();

            try
            {
                if (action == "Add")
                {
                    if (_sectionID != "" && _name != "")
                    {

                        sb.Clear();
                        sb.AppendLine(" select s.ProdMonth, s.SectionID, s.Name, s.HierarchicalID, s.ReportToSectionID,");
                        sb.AppendLine(" h.Description Occupation, Status = ''");
                        sb.AppendLine(" from Section s, Hierarch h");
                        sb.AppendLine(" where s.Hierarchicalid = h.Hierarchicalid and");
                        sb.AppendLine(" ProdMonth = '" + _prodMonth + "' and SectionID = '" + _sectionID + "' order by s.Hierarchicalid, s.SectionID");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        DataTable dtSectionScreen = newData.ResultsDataTable;

                        if (dtSectionScreen.Rows.Count > 0)
                        {
                            string _sectID = dtSectionScreen.Rows[0]["SectionID"].ToString();

                            if (_sectionID.Contains(_sectID))
                            {
                                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Cannot insert duplicate rows", Color.Red);
                            }
                        }
                        

                        else 
                        {
                            newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            sb.Clear();
                            sb.AppendLine("insert into Section (ProdMonth, SectionID, Name, ReportToSectionID, Hierarchicalid,EmployeeNo) values ");
                            sb.AppendLine("('" + _prodMonth + "', '" + _sectionID + "', '" + _name + "', ");
                            sb.AppendLine("'" + _reportTo + "', '" + _hierID + "' ,'" + _EmployeeNo + "') ");
                            sb.AppendLine("insert into Section (ProdMonth, SectionID, Name, ReportToSectionID, Hierarchicalid,EmployeeNo) values ");
                            sb.AppendLine("(dbo.GetNextProdMonth('" + _prodMonth + "'), '" + _sectionID + "', '" + _name + "', ");
                            sb.AppendLine("'" + _reportTo + "', '" + _hierID + "','" + _EmployeeNo + "')");
                            newData.ConnectionString = theData.ConnectionString;
                            newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                            newData.SqlStatement = sb.ToString();
                            newData.ExecuteInstruction();

                            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Section Data was saved", Color.CornflowerBlue);
                        }
                    }

                    else
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields cannot be blank", Color.Red);
                    }
                }

                else if (action == "Edit")
                {
                    if (_sectionID != "" && _name != "")
                    {
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        sb.Clear();
                        sb.AppendLine("update Section ");
                        sb.AppendLine("set Name = '" + _name + "', ");
                        sb.AppendLine("ReportToSectionID = '" + _reportTo + "', ");
                        sb.AppendLine("HierarchicalID = '" + _hierID + "' , ");
                        sb.AppendLine("EmployeeNo = '" + _EmployeeNo + "' ");
                        sb.AppendLine("where ProdMonth = '" + _prodMonth + "' and SectionID = '" + _sectionID + "' ");
                        sb.AppendLine("");
                        sb.AppendLine("update Section ");
                        sb.AppendLine("set Name = '" + _name + "', ");
                        sb.AppendLine("ReportToSectionID = '" + _reportTo + "', ");
                        sb.AppendLine("HierarchicalID = '" + _hierID + "' , ");
                        sb.AppendLine("EmployeeNo = '" + _EmployeeNo + "' ");
                        sb.AppendLine("where ProdMonth = dbo.GetNextProdMonth('" + _prodMonth + "') and SectionID = '" + _sectionID + "'");
                        sb.AppendLine("");
                        sb.AppendLine("");
                        sb.AppendLine("");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Section Data was saved", Color.CornflowerBlue);
                    }

                    else
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields sectionID and Employee cannot be blank", Color.Red);
                    }
                }

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }

        }



        public void SaveDataOther(string _prodMonth, string _sectionID, string _name, int _hierID, string _reportTo, string action, string _EmployeeNo)
        {
            clsDataAccess newData = new clsDataAccess();

            try
            {
                if (action == "AddOther")
                {
                    if (_sectionID != "" && _name != "")
                    {

                        sb.Clear();
                        sb.AppendLine(" select s.ProdMonth, s.SectionID, s.Name, s.HierarchicalID, s.ReportToSectionID,");
                        sb.AppendLine(" h.Description Occupation, Status = ''");
                        sb.AppendLine(" from SectionOther s, HierarchOther h");
                        sb.AppendLine(" where s.Hierarchicalid = h.Hierarchicalid and");
                        sb.AppendLine(" ProdMonth = '" + _prodMonth + "' and SectionID = '" + _sectionID + "' order by s.Hierarchicalid, s.SectionID");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        DataTable dtSectionScreen = newData.ResultsDataTable;

                        if (dtSectionScreen.Rows.Count > 0)
                        {
                            string _sectID = dtSectionScreen.Rows[0]["SectionID"].ToString();

                            if (_sectionID.Contains(_sectID))
                            {
                                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Cannot insert duplicate rows", Color.Red);
                            }
                        }


                        else
                        {
                            newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            sb.Clear();
                            sb.AppendLine("insert into SectionOther (ProdMonth, SectionID, Name, ReportToSectionID, Hierarchicalid,EmployeeNo) values ");
                            sb.AppendLine("('" + _prodMonth + "', '" + _sectionID + "', '" + _name + "', ");
                            sb.AppendLine("'" + _reportTo + "', '" + _hierID + "' ,'" + _EmployeeNo + "') ");
                            sb.AppendLine("insert into SectionOther (ProdMonth, SectionID, Name, ReportToSectionID, Hierarchicalid,EmployeeNo) values ");
                            sb.AppendLine("(dbo.GetNextProdMonth('" + _prodMonth + "'), '" + _sectionID + "', '" + _name + "', ");
                            sb.AppendLine("'" + _reportTo + "', '" + _hierID + "','" + _EmployeeNo + "')");
                            newData.ConnectionString = theData.ConnectionString;
                            newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                            newData.SqlStatement = sb.ToString();
                            newData.ExecuteInstruction();

                            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Section Data was saved", Color.CornflowerBlue);
                        }
                    }

                    else
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields cannot be blank", Color.Red);
                    }
                }

                else if (action == "EditOther")
                {
                    if (_sectionID != "" && _name != "")
                    {
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        sb.Clear();
                        sb.AppendLine("update SectionOther ");
                        sb.AppendLine("set Name = '" + _name + "', ");
                        sb.AppendLine("ReportToSectionID = '" + _reportTo + "', ");
                        sb.AppendLine("HierarchicalID = '" + _hierID + "' , ");
                        sb.AppendLine("EmployeeNo = '" + _EmployeeNo + "' ");
                        sb.AppendLine("where ProdMonth = '" + _prodMonth + "' and SectionID = '" + _sectionID + "' ");
                        sb.AppendLine("");
                        sb.AppendLine("update SectionOther ");
                        sb.AppendLine("set Name = '" + _name + "', ");
                        sb.AppendLine("ReportToSectionID = '" + _reportTo + "', ");
                        sb.AppendLine("HierarchicalID = '" + _hierID + "' , ");
                        sb.AppendLine("EmployeeNo = '" + _EmployeeNo + "' ");
                        sb.AppendLine("where ProdMonth = dbo.GetNextProdMonth('" + _prodMonth + "') and SectionID = '" + _sectionID + "'");
                        sb.AppendLine("");
                        sb.AppendLine("");
                        sb.AppendLine("");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Section Data was saved", Color.CornflowerBlue);
                    }

                    else
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields sectionID and Employee cannot be blank", Color.Red);
                    }
                }

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }

        }
    }
}
