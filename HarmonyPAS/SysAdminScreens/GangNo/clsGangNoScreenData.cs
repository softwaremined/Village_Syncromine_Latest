using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.GangNo
{
    class clsGangNoScreenData : clsBase 
    {
        string totshift;
   
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        public DataTable getGangNoData(string _calendarDate)
        {
            try
            {
                theData.SqlStatement = " select * from CREW ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "UPDATING DB", resWPAS.systemTag, "clsSafetyCalendarData", "getSafetyCalendarList", _exception.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
            }
            return theData.ResultsDataTable;
        }
    }
}
