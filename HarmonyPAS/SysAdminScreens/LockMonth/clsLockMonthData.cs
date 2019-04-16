using Mineware.Systems.Global;
using MWDataManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.LockMonth
{
    public class clsLockMonthData : clsBase
    {
        public DataTable GetProdMonth()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select CurrentProductionMonth Prodmonth from SYSSET");
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

        public DataTable GetBusinessPlanData()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine(" ");
                sb.AppendLine(" ");
                sb.AppendLine(" ");
                sb.AppendLine(" ");
                sb.AppendLine(" ");
                sb.AppendLine(" ");
                sb.AppendLine(" ");
                sb.AppendLine(" ");
                sb.AppendLine(" ");
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

        public DataTable GetPlanningLockData(string _prodMonth)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select Prodmonth, sectionid_2, sectionid_2 + ':' + name_2 Section");
                sb.AppendLine("from Section_Complete");
                sb.AppendLine(string.Format("where Prodmonth = '{0:0}'", _prodMonth));
                sb.AppendLine("group by Prodmonth, sectionid_2, name_2");
                sb.AppendLine("order by Prodmonth, sectionid_2, name_2");

                sb.AppendLine("select k.Prodmonth, sc.sectionid_2");
                sb.AppendLine("from Kriging k left outer join ");
                sb.AppendLine("PLANMONTHLocked p on");
                sb.AppendLine("k.workplaceid = p.workplaceid");
                sb.AppendLine("inner join Section_Complete sc");
                sb.AppendLine("on sc.Prodmonth = k.Prodmonth");
                sb.AppendLine("and sc.SectionID = k.Sectionid");
                sb.AppendLine(string.Format("where k.Prodmonth = '{0:0}'", _prodMonth));
                sb.AppendLine("group by k.Prodmonth, sc.sectionid_2");
                sb.AppendLine("order by k.Prodmonth, sc.sectionid_2");

                sb.AppendLine("select p.Prodmonth, sc.sectionid_2");
                sb.AppendLine("from PLANMONTH p");
                sb.AppendLine("inner join Section_Complete sc");
                sb.AppendLine("	on sc.Prodmonth = p.Prodmonth");
                sb.AppendLine("	and sc.SectionID = p.Sectionid");
                sb.AppendLine(string.Format("where p.Prodmonth = '{0:0}'", _prodMonth));
                sb.AppendLine("group by p.Prodmonth, sc.sectionid_2");
                sb.AppendLine("order by p.Prodmonth, sc.sectionid_2");
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

        public DataTable UnlockLockSurveyData(string _prodMonth)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("update  Survey_Locks  ");
                sb.AppendLine("set Locked = 0");
                sb.AppendLine("");
                sb.AppendLine(" where prodmonth = '" + _prodMonth + "' ");

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

        public DataTable LockSurveyData(string _prodMonth)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine(" begin try insert into Survey_Locks values ( ");
                sb.AppendLine(" '"+ _prodMonth+"',1,getdate(), ");
                sb.AppendLine(" '"+TUserInfo.UserID.ToString()+"' ");
                sb.AppendLine(" ) end try begin catch update  Survey_Locks  set Locked = 1, datelocked = getdate(),LockedByID = '" + TUserInfo.UserID.ToString() + "' where prodmonth = '" + _prodMonth + "' end catch");

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

        public DataTable GetSurveyLockData()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select ");
                sb.AppendLine("distinct(p.ProdMonth) ProdMonth,");
                sb.AppendLine("isnull(sl.Locked, 0) Locked,");
                sb.AppendLine("sl.DateLocked,");
                sb.AppendLine("sl.LockedByID");
                sb.AppendLine("from PLANNING p");
                sb.AppendLine("left outer join survey_locks sl on p.ProdMonth = sl.ProdMonth");
                sb.AppendLine("order by p.ProdMonth desc");
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

        public void SavePlanningLockData()
        {

        }

        public void SaveSurveyLockData()
        {

        }
    }
}
