using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mineware.Systems.Global;
using MWDataManager;
using System.Data.SqlClient;
using System.Drawing;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.QAQC
{
    class clsQAQCData : clsBase
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

        public DataTable GetAMIS()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select distinct CRM Name from tblStandardCRM");
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

        public DataTable GetCourseBlankSample(string _shaft)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from tblCourseBlankSample where Shaft = '" + _shaft + "'");
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

        public DataTable GetStandardCRM(string _shaft, string _crm)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from tblStandardCRM where Shaft = '"+ _shaft +"' and CRM = '" + _crm + "'");
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

        public DataTable GetDuplicateSample(string _shaft)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from tblDuplicateSample where Shaft = '" + _shaft + "'");
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

        public DataTable GetCourseBlankSampleData(string _shaft, string _amis)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from tblQAQC where Shaft = '" + _shaft + "' and AMIS = '" + _amis + "'");
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

        public DataTable GetStandardCRMData(string _shaft, string _amis, string _crm)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("select * from tblQAQC where Shaft = '" + _shaft + "' and AMIS = '" + _amis + "' and CRM = '" + _crm + "'");
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

        public DataTable GetDuplicateSampleData(string _shaft, string _amis)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("select * from tblQAQC where Shaft = '" + _shaft + "' and AMIS = '" + _amis + "'");
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

        public void SaveCourseBlankSampleData(string _shaft, string _amis,
            DateTime _date, string _ticketNumber, decimal _assayValue, string _outcome, decimal _reAssayValue, string _reAssayOutcome, string _action)
        {
            clsDataAccess newData = new clsDataAccess();

            try
            {

                if (_action == "Add")
                {

                    sb.Clear();
                    sb.AppendLine("select Shaft, Amis, AssayDate, TicketNo, AssayValue, ");
                    sb.AppendLine("Outcome, ReAssayValue, ReAssayOutcome ");
                    sb.AppendLine("from tblQAQC ");
                    sb.AppendLine("where shaft = '" + _shaft + "' and Amis = '" + _amis + "' and TicketNo = '"+ _ticketNumber +"'");
                    //sb.AppendLine("and AssayDate = '" + _date.ToShortDateString() + "'");
                    newData.ConnectionString = theData.ConnectionString;
                    newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    DataTable dtQAQCCourseBlank = newData.ResultsDataTable;

                    if (dtQAQCCourseBlank.Rows.Count > 0)
                    {
                        //string shaft = dtQAQCCourseBlank.Rows[0]["Shaft"].ToString();

                        //string amis = dtQAQCCourseBlank.Rows[0]["Amis"].ToString();

                        //DateTime date = Convert.ToDateTime(dtQAQCCourseBlank.Rows[0]["AssayDate"].ToString());

                        string ticketNo = dtQAQCCourseBlank.Rows[0]["TicketNo"].ToString();

                        //if (_shaft.Contains(shaft) && _amis.Contains(amis))
                        //{
                        //    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Course Blank Data for this date already exists", Color.Red);
                        //}

                        if (_ticketNumber.Contains(ticketNo))
                        {
                            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Ticket No already exists", Color.Red);
                        }

                        else
                        {

                        }
                    }

                    else
                    {
                        sb.Clear();
                        sb.AppendLine("insert into tblQAQC (Shaft, Amis, AssayDate, TicketNo, AssayValue, Outcome, ReAssayValue, ReAssayOutcome) values ");
                        sb.AppendLine("('" + _shaft + "', '" + _amis + "', '" + _date.ToShortDateString() + "', ");
                        sb.AppendLine("'" + _ticketNumber + "', " + _assayValue + ", '" + _outcome + "', isnull(" + _reAssayValue + ", 0.00), isnull('" + _reAssayOutcome + "', 0)) ");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Course Blank Data was saved", Color.CornflowerBlue);
                    }
                }


            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
        }

        public void SaveStandardCRMData(string _shaft, string _amis,
    DateTime _date, string _ticketNumber, decimal _assayValue, string _outcome, decimal _reAssayValue, string _reAssayOutcome, string _crm, string _action)
        {
            clsDataAccess newData = new clsDataAccess();

            try
            {

                if (_action == "Add")
                {

                    sb.Clear();
                    sb.AppendLine("select Shaft, Amis, AssayDate, TicketNo, AssayValue, ");
                    sb.AppendLine("Outcome, ReAssayValue, ReAssayOutcome, CRM ");
                    sb.AppendLine("from tblQAQC ");
                    sb.AppendLine("where shaft = '" + _shaft + "' and Amis = '" + _amis + "' and CRM = '" + _crm + "' and TicketNo = '" + _ticketNumber + "'");
                    //sb.AppendLine("and AssayDate = '" + _date.ToShortDateString() + "'");
                    newData.ConnectionString = theData.ConnectionString;
                    newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    DataTable dtQAQCStandardCRM = newData.ResultsDataTable;

                    if (dtQAQCStandardCRM.Rows.Count > 0)
                    {
                        //string shaft = dtQAQCCourseBlank.Rows[0]["Shaft"].ToString();

                        //string amis = dtQAQCCourseBlank.Rows[0]["Amis"].ToString();

                        //string crm = dtQAQCCourseBlank.Rows[0]["CRM"].ToString();

                        //DateTime date = Convert.ToDateTime(dtQAQCCourseBlank.Rows[0]["AssayDate"].ToString());

                        string ticketNo = dtQAQCStandardCRM.Rows[0]["TicketNo"].ToString();

                        //if (_shaft.Contains(shaft) && _amis.Contains(amis) && _crm.Contains(crm))
                        //{
                        //    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Data already exists", Color.Red);
                        //}

                        if (_ticketNumber.Contains(ticketNo))
                        {
                            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Ticket No already exists", Color.Red);
                        }
                    }

                    else
                    {
                        sb.Clear();
                        sb.AppendLine("insert into tblQAQC (Shaft, Amis, AssayDate, TicketNo, AssayValue, Outcome, ReAssayValue, ReAssayOutcome, CRM) values ");
                        sb.AppendLine("('" + _shaft + "', '" + _amis + "', '" + _date.ToShortDateString() + "', ");
                        sb.AppendLine("'" + _ticketNumber + "', " + _assayValue + ", '" + _outcome + "', isnull(" + _reAssayValue + ", 0.00), isnull('" + _reAssayOutcome + "', ''), '" + _crm + "') ");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Course Blank Data was saved", Color.CornflowerBlue);
                    }
                }


            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
        }


        public void SaveDuplicateSampleData(string _shaft, string _amis, string _ticketNumber, decimal _assayValue,
                                            DateTime _date, string _duplicateTicketNumber, decimal _duplicateAssayValue, string _action)
        {
            clsDataAccess newData = new clsDataAccess();

            try
            {
                if (_action == "Add")
                {

                    sb.Clear();
                    sb.AppendLine("select Shaft, Amis, TicketNo, AssayValue, AssayDate, DuplicateTicketNo, DuplicateAssayValue ");
                    sb.AppendLine("from tblQAQC ");
                    sb.AppendLine("where shaft = '" + _shaft + "' and Amis = '" + _amis + "' and TicketNo = '" + _ticketNumber + "'");
                    //sb.AppendLine("and AssayDate = '" + _date.ToShortDateString() + "'");
                    newData.ConnectionString = theData.ConnectionString;
                    newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    DataTable dtQAQCDuplicate = newData.ResultsDataTable;

                    if (dtQAQCDuplicate.Rows.Count > 0)
                    {
                        //string shaft = dtQAQCCourseBlank.Rows[0]["Shaft"].ToString();

                        //string amis = dtQAQCCourseBlank.Rows[0]["Amis"].ToString();

                        //DateTime date = Convert.ToDateTime(dtQAQCCourseBlank.Rows[0]["AssayDate"].ToString());

                        string ticketNo = dtQAQCDuplicate.Rows[0]["TicketNo"].ToString();

                        //if (_shaft.Contains(shaft) && _amis.Contains(amis))
                        //{
                        //    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Data already exists", Color.Red);
                        //}

                        if (_ticketNumber.Contains(ticketNo))
                        {
                            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Ticket No already exists", Color.Red);
                        }
                    }

                    else 
                    {
                        sb.Clear();
                        sb.AppendLine("insert into tblQAQC (Shaft, Amis, TicketNo, AssayValue, AssayDate, DuplicateTicketNo, DuplicateAssayValue) values ");
                        sb.AppendLine("('" + _shaft + "', '" + _amis + "', '"+ _ticketNumber +"', "+_assayValue+", '" + _date.ToShortDateString() + "', ");
                        sb.AppendLine("'" + _duplicateTicketNumber + "', " + _duplicateAssayValue + ") ");
                        newData.ConnectionString = theData.ConnectionString;
                        newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        newData.SqlStatement = sb.ToString();
                        newData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Duplicate Sample Data successfully saved", Color.CornflowerBlue);
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
