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

namespace Mineware.Systems.Production.SysAdminScreens.TrammingProblems
{
    class clsTrammingProblemsData : clsBase
    {
        public DataTable GetTrammingProblems()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("select *, Status = '' from CheckTrammingProblems");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return newData.ResultsDataTable;
        }

        public void SaveData(string _id, string _problemCode, string action)
        {
            clsDataAccess newData = new clsDataAccess();

            try
            {


                if (action == "Add")
                {
                    if (_problemCode != "")
                    {

                        sb.Clear();
                        sb.AppendLine("select * from CheckTrammingProblems where ID = '" + _id + "'");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        DataTable dtProblems = newData.ResultsDataTable;

                        if (dtProblems.Rows.Count > 0)
                        {
                            string _problem = dtProblems.Rows[0]["ProblemCode"].ToString();

                            if (_problemCode.Contains(_problem))
                            {
                                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Cannot insert duplicate rows", Color.Red);
                            }
                        }
                        

                        else 
                        {
                            newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            sb.Clear();
                            sb.AppendLine("insert into CheckTrammingProblems (ProblemCode) values ");
                            sb.AppendLine("('" + _problemCode + "') ");
                            newData.ConnectionString = theData.ConnectionString;
                            newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                            newData.SqlStatement = sb.ToString();
                            newData.ExecuteInstruction();

                            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Data was saved", Color.CornflowerBlue);
                        }
                    }

                    else
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields cannot be blank", Color.Red);
                    }
                }

                else if (action == "Edit")
                {
                    if (_problemCode != "")
                    {
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        sb.Clear();
                        sb.AppendLine("update CheckTrammingProblems ");
                        sb.AppendLine("set ProblemCode = '" + _problemCode + "' ");
                        sb.AppendLine("where ID = '" + _id + "'");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Data was saved", Color.CornflowerBlue);
                    }

                    else
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields cannot be blank", Color.Red);
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
