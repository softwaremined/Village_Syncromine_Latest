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

namespace Mineware.Systems.Production.SysAdminScreens.OCRScheduling
{
    class clsOCRScheduling : clsBase
    {

        public DataTable GetActivity()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select Activity as [Code], Description as [Desc] from CODE_ACTIVITY");
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
    }
}
