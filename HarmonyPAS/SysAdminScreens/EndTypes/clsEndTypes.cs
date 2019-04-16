using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.EndTypes
{
    class clsEndTypes:clsBase 
    {
        public DataTable loadEndTypes()
        {
            theData.SqlStatement = " select EndTypeID, isnull(Description,'') Description, \r\n " +
                                 "       EndHeight = isnull(EndHeight,0.00), \r\n " +
                                 "       EndWidth = isnull(EndWidth,0.00), \r\n " +
                                 "       ReefWaste = isnull(ReefWaste,''), \r\n " +
                                 "       Rate = isnull(Rate,0) , \r\n " +
                                 "       DetRatio = isnull(DetRatio,0), \r\n " +
                                 "       ProcessCode = isnull(ProcessCode,'') \r\n " +
                                 "   from endtype order by endtypeid";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }
    }
}
