using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Mineware.Systems.Global;
using System.Drawing;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.CalendarTypes
{
    class clsCalTypeData:clsBase 
    {
        private StringBuilder sb = new StringBuilder();

        public DataTable getCalTypes()
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                sb.Clear();
                sb.AppendLine("SELECT [CalendarCode],[Description] ");
                sb.AppendLine("  FROM [dbo].[CODE_CALENDAR]");
                theData.SqlStatement = sb.ToString();
                theData.ExecuteInstruction();

                return theData.ResultsDataTable;
            }
            catch (Exception e)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", e.Message, Color.Red);
                return null;
            }
        }

        public DataTable getWorkingDays(string calType)
        {
            try
            {
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            sb.Clear();
            sb.AppendLine("SELECT [CalendarCode]");
            sb.AppendLine("      ,cast(1 as bit) AllDay");
            sb.AppendLine("	  ,CASE WHEN  [Workingday] = 'Y' THEN 'Working Day' ELSE 'Non Working Day' END Heading");
            sb.AppendLine("	  ,CASE WHEN  [Workingday] = 'Y' THEN 0 ELSE 1 END theID");
            sb.AppendLine("      ,[CalendarDate] StartTime");
            sb.AppendLine("	  ,[CalendarDate] + 0.7 EndTime");
            sb.AppendLine("      ,[Workingday]");
            sb.AppendLine("  FROM [dbo].[CALTYPE] WHERE CalendarCode = '" + calType +"'");
            theData.SqlStatement = sb.ToString();
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;

            }
            catch (Exception e)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", e.Message, Color.Red);
                return null;
            }

        }

        public bool saveCalType(string CalendarCode, String CalType)
        {
            bool Result = false;
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.SqlStatement = "SP_Manage_CalcTypeItem";
                SqlParameter[] _paramCollectionS = 
                            {
                             theData.CreateParameter("@CalendarCode", SqlDbType.VarChar, 20,CalendarCode ),
                             theData.CreateParameter("@CalType", SqlDbType.VarChar, 50,CalType )
                            };
                theData.ParamCollection = _paramCollectionS;
                theData.ExecuteInstruction();
                Result = true;
            }
            catch (Exception e)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", e.Message, Color.Red);
                Result = false;
            }

            return Result;
        }

        public bool saveDateInfo(string CalendarCode, DateTime theDay, bool isWorking)
        {
            bool Result = false;
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.SqlStatement = "SP_Manage_CalcType";
                char WorkDay;
                if (isWorking) { WorkDay = 'Y'; } else { WorkDay = 'N'; }
                SqlParameter[] _paramCollectionS = 
                            {
                             theData.CreateParameter("@CalendarCode", SqlDbType.VarChar, 20,CalendarCode ),
                             theData.CreateParameter("@theDay", SqlDbType.Date  , 50,theDay.ToShortDateString() ),
                             theData.CreateParameter("@isWorking", SqlDbType.Char, 1,WorkDay  )

                            };

               
                theData.ParamCollection = _paramCollectionS;
                theData.ExecuteInstruction();
                Result = true;
            }
            catch (Exception e)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", e.Message, Color.Red);
                Result = false;
            }

            return Result;
        }
    }
}
