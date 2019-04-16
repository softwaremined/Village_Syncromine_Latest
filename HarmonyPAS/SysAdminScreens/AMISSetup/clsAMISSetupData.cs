using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;
using MWDataManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Production.SysAdminScreens.AMISSetup
{
    class clsAMISSetupData : clsBase
    {
        public DataTable GetShaft()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select distinct [Description] Name from PAS_Central_Test.dbo.CODE_WPDIVISION");
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

        public DataTable GetCourseBlankSampleData(string _shaft)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select LabDetectionLimit ");
                sb.AppendLine("from tblCourseBlankSample where Shaft = '" + _shaft + "'");
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

        public DataTable GetStandardCRMData(string _shaft)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select CRM, MeanValue, StdDev, ");
                sb.AppendLine("UpperWarningLimit = (MeanValue + 2 * (StdDev/2)), ");
                sb.AppendLine("LowerWarningLimit = (MeanValue - 2 * StdDev), ");
                sb.AppendLine("UpperFailureLimit = (MeanValue + 3 * (StdDev/2)), ");
                sb.AppendLine("LowerFailureLimit = (MeanValue - 3 * (StdDev/2)) ");
                sb.AppendLine("from tblStandardCRM where Shaft = '" + _shaft + "'");
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

        public DataTable GetDuplicateSampleData(string _shaft)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select PayLimit ");
                sb.AppendLine("from tblDuplicateSample where Shaft = '" + _shaft + "'");
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

        public void SaveCourseBlankSampleData(string _shaft, decimal _labDetectionLimit, string _action)
        {
            clsDataAccess newData = new clsDataAccess();

            try
            {
                if (_action == "Add")
                {

                    sb.Clear();
                    sb.AppendLine("select * from tblCourseBlankSample where Shaft = '" + _shaft + "'");
                    newData.ConnectionString = theData.ConnectionString;
                    newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    DataTable dtAMIS = newData.ResultsDataTable;

                    if (dtAMIS.Rows.Count == 0)
                    {
                        sb.Clear();
                        sb.AppendLine("insert into tblCourseBlankSample (Shaft, LabDetectionLimit) values ");
                        sb.AppendLine("('" + _shaft + "', '" + _labDetectionLimit + "')");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "AMIS Data was saved", Color.CornflowerBlue);
                    }

                    else if (dtAMIS.Rows.Count > 0)
                    {

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Data Already exists for this Shaft", Color.Red);

                    }
                }
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
        }

        public void SaveStandardCRMData(string _shaft, string _crm, decimal _meanValue, decimal _stdDev, string _action)
        {
            clsDataAccess newData = new clsDataAccess();

            try
            {
                if (_action == "Add")
                {

                    sb.Clear();
                    sb.AppendLine("select * from tblStandardCRM where Shaft = '" + _shaft + "' and CRM = '" + _crm + "'");
                    newData.ConnectionString = theData.ConnectionString;
                    newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    DataTable dtAMIS = newData.ResultsDataTable;

                    if (dtAMIS.Rows.Count == 0)
                    {
                        sb.Clear();
                        sb.AppendLine("insert into tblStandardCRM (Shaft, CRM, MeanValue, StdDev) values ");
                        sb.AppendLine("('" + _shaft + "', '" + _crm + "', '" + _meanValue + "', '" + _stdDev + "') ");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "AMIS Data was saved", Color.CornflowerBlue);
                    }

                    else if (dtAMIS.Rows.Count > 0)
                    {

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Data Already exists for this Shaft", Color.Red);

                    }
                }
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
        }

        public void SaveDuplicateSampleData(string _shaft, decimal _payLimit, string _action)
        {
            clsDataAccess newData = new clsDataAccess();

            try
            {
                if (_action == "Add")
                {

                    sb.Clear();
                    sb.AppendLine("select * from tblDuplicateSample where Shaft = '" + _shaft + "'");
                    newData.ConnectionString = theData.ConnectionString;
                    newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    DataTable dtAMIS = newData.ResultsDataTable;

                    if (dtAMIS.Rows.Count == 0)
                    {
                        sb.Clear();
                        sb.AppendLine("insert into tblDuplicateSample (Shaft, PayLimit) values ");
                        sb.AppendLine("('" + _shaft + "', '" + _payLimit + "') ");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "AMIS Data was saved", Color.CornflowerBlue);
                    }

                    else if (dtAMIS.Rows.Count > 0)
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Data Already exists for this Shaft", Color.Red);
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
