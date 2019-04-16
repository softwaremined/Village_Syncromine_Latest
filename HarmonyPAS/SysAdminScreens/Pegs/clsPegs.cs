using System;
using System.Data;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Pegs
{
    class clsPegs : clsBase
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        //public DataTable get_Pegs(string _sort
            public DataTable get_Pegs()
        {
            try
            {
                theData.SqlStatement =
                    "  select w.WorkplaceID, w.Description, b.PegID, cast(b.Value as numeric(10,1)) theValue, \r\n " +
                    "    b.Letter1, b.Letter2, b.Letter3 \r\n " +
                    "  from workplace w \r\n " +
                    "  left outer join \r\n " +
                    "     (select a.* from Peg a \r\n " +
                    "      inner join \r\n " +
                    "           (select WorkplaceID, max(CalendarDate) CalendarDate \r\n " +
                    "               from Peg \r\n " +
                    "               group by WorkplaceID) b on \r\n " +
                    "            a.WorkplaceID = b.WorkplaceID and \r\n " +
                    "            a.CalendarDate = b.CalendarDate \r\n " +
                    "      ) b on \r\n " +
                    "    b.WorkplaceID = w.WorkplaceID \r\n " +
                    " where w.Activity = '1'  and w.WorkplaceID like '%' \r\n ";
               /* if (_sort == "")
                    theData.SqlStatement = theData.SqlStatement +
                        " order by w.WorkplaceID ";
                else
                    theData.SqlStatement = theData.SqlStatement +
                        " order by b.CalendarDate Desc ";*/
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable; ;
            }
        }
        public DataTable get_WPPegs(string _wpid)
        {
            try
            {
                theData.SqlStatement =
                    " Select WorkplaceID WPID, PegID WPPegID , cast(Value as numeric(10,1)) WPtheValue \r\n " +
                    " from Peg \r\n " +
                    " where WorkplaceID = '" + _wpid + "' \r\n " +
                    " order by CalendarDate Desc";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable; ;
            }
        }
        public DataTable get_Bookings(string _wpid, string _pegid)
        {
            try
            {
                theData.SqlStatement =
                    " Select WorkplaceID, PegID from Planning \r\n " +
                    " where WorkplaceID = '" + _wpid + "' and \r\n " +
                    "       PegID = '" + _pegid + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable; ;
            }
        }
        public DataTable get_Bookings(string _wpid, string _pegid, string _value)
        {
            try
            {
                string aa = _pegid + ":" + _value;
                theData.SqlStatement =
                    " Select WorkplaceID, PegID from Planning \r\n " +
                    " where WorkplaceID = '" + _wpid + "' and \r\n " +
                    "       PegID = '" + aa + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable; ;
            }
        }
        public DataTable get_WPPegDelete(string _wpid, string _pegid)
        {
            try
            {
                theData.SqlStatement =
                    " delete from Peg \r\n " +
                    " where WorkplaceID = '" + _wpid + "' and \r\n " +
                    "       PegID = '" + _pegid + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable; ;
            }
        }
        public DataTable find_WPPeg(string _wpid, string _pegid)
        {
            try
            {
                theData.SqlStatement =
                    " select * from Peg \r\n " +
                    " where WorkplaceID = '" + _wpid + "' and \r\n " +
                    "       PegID = '" + _pegid + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable; ;
            }
        }
        public Boolean saveData(DataTable TheDatatable)
        {
            try
            {
                bool fndrec = false;
                foreach (DataRow dr in TheDatatable.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        DateTime aa = DateTime.Now;
                        theData.SqlStatement =
                            " Insert into Peg (PegID, WorkplaceID, Value, Letter1, Letter2, Letter3, CalendarDate) \r\n " +
                            " values \r\n " +
                            " ('" + dr["PegID"] + "', '" + dr["WorkplaceID"] + "', '" + dr["theValue"] + "', \r\n " +
                            " '" + dr["Letter1"] + "', '" + dr["Letter2"] + "', '" + dr["Letter3"] + "', '"+ aa +"') ";
                        fndrec = true;
                    }
                }
                if (fndrec == true)
                {
                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                    theData.ExecuteInstruction();
                }
                return true;
            }
            catch (Exception _exception)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "UPDATING ERROR", resHarmonyPAS.systemTag, "clsPegs", "saveData", _exception.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
        }
    }
}
