using System;
using System.Data;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.SICCapture
{
    class clsSICCapture : clsBase
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        public DataTable get_Sysset()
        {
            theData.SqlStatement = " select * from Sysset ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsSICCapture", "get_Sysset", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Sections(string _prodmonth, string _select)
        {
            if (_select != "Cleaned")//(rbCleaning.Checked != true)
            {
                if (_select == "Backfill")// (rbBackFill.Checked == true)
                {
                    theData.SqlStatement = " select distinct SectionID_5 Section, SectionID_5 + ': ' + Name_5 Name \r\n " +
                                                    " from Section_Complete \r\n " +
                                                    " where ProdMonth = '" + _prodmonth + "' \r\n " +
                                                    " order by SectionID_5 \r\n ";
                }
                else
                {
                    theData.SqlStatement = " select distinct(SectionID_2) Section, SectionID_2 + ': ' + Name_2 Name \r\n " +
                                                    " from Section_Complete \r\n " +
                                                    " where ProdMonth = '" + _prodmonth + "' \r\n " +
                                                    " order by SectionID_2 \r\n ";
                }
            }
            else
            {
                theData.SqlStatement = " select distinct(sc.SectionID_1) Section, \r\n " +
                                                "     sc.SectionID_1 + ': ' + sc.Name_1 Name \r\n " +
                                                " from Planning p \r\n " +
                                                " inner join Section_Complete sc on \r\n " +
                                                "   sc.ProdMonth = p.ProdMonth and \r\n " +
                                                "   sc.SectionID = p.SectionID \r\n " +
                                                " where p.ProdMonth = '" + _prodmonth + "'\r\n  " +
                                                " order by sc.SectionID_1 \r\n ";
            }
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsSICCapture", "get_Sections", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Problems_Desc(string TheActivity, string ProblemID)
        {
            theData.SqlStatement = " select PROBLEMID + ':' + Description ProblemDesc \r\n " +
                    " from CODE_PROBLEM  \r\n " +
                    " where ProblemID = '" + ProblemID + "' and \r\n " +
                    "       activity = '" + TheActivity + "' and Deleted <> 'Y'";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsSICCapture", "get_Problems_Desc", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_CausedLostBlast(string TheActivity, string ProblemID)
        {
            theData.SqlStatement = " select CausedLostBlast = case when isnull(CausedLostBlast,'') = 'Y' then 'Y' else 'N' end \r\n " +
                    " from CODE_PROBLEM  \r\n " +
                    " where ProblemID = '" + ProblemID + "' and \r\n " +
                    "       activity = '" + TheActivity + "' and Deleted <> 'Y'";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsSICCapture", "get_Problems_Desc", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_SICData(string _typemonth, string _prdmth, string _sectionid, string _hierid, string _kpi)
        {
            theData.SqlStatement = "[sp_SICCapture_Load]";
            theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            theData.ResultsTableName = "Kriging";

            SqlParameter[] _paramCollection =
                        {
                    theData.CreateParameter("@TypeMonth", SqlDbType.VarChar, 20, _typemonth),
                    theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, _prdmth),
                    theData.CreateParameter("@SectionID", SqlDbType.VarChar, 20, _sectionid),
                    theData.CreateParameter("@SectionLevel", SqlDbType.VarChar, 1, _hierid),
                    theData.CreateParameter("@KPI", SqlDbType.VarChar, 50, _kpi),
                };

            theData.ParamCollection = _paramCollection;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsSICCapture", "get_SICData", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public Boolean Save_SIC(DataTable dtSic, string _prodmonth, string _sectionid, string _kpi)
        {
            theData.SqlStatement = "";
            theData.SqlStatement =
                    " delete b from SICCapture b \r\n " +
                    " inner join Code_SICCapture cc on \r\n " +
                    "   cc.SICKey = b.sickey \r\n " +
                    " where b.MillMonth = '" + _prodmonth + "' and \r\n " +
                    "       b.sectionid = '" + _sectionid + "' and \r\n " +
                    "       cc.KPI = '" + _kpi + "' \r\n ";
            foreach (DataRow dr in dtSic.Rows)
            {
                string _thedate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dr["CalendarDate"].ToString()));
                if (dr["TheValue"] != null)
                    if (dr["TheValue"].ToString() != "")
                        if (dr["ShiftNo"] !=null)
                            if (dr["ShiftNo"].ToString() != "N")
                            { 
                                theData.SqlStatement = theData.SqlStatement +
                                    " INSERT INTO SICCapture " +
                                    " (SicKey, CalendarDate, SectionID, SortHeading \r\n " +
                                    " ,MillMonth, Value, WorkPlaceID, ProblemCode) \r\n " +
                                    " VALUES (" +
                                    " '" + dr["SICKey"].ToString() + "', '" + _thedate + "', " +
                                    " '" + _sectionid + "', null, '" + _prodmonth + "', " +
                                    " '" + dr["TheValue"].ToString() + "', null, null) \r\n ";
                            }
            }
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsSICCapture", "Save_SIC", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
        public Boolean Save_SICClean(DataTable dtSic, string _prodmonth, string _sectionid, string _kpi)
        {
            string _sicKey;
            theData.SqlStatement = "";
            theData.SqlStatement =
                    " delete b from SICCapture b \r\n " +
                    " inner join Code_SICCapture cc on \r\n " +
                    "   cc.SICKey = b.sickey \r\n " +
                    " where b.MillMonth = '" + _prodmonth + "' and \r\n " +
                    "       b.sectionid = '" + _sectionid + "' and \r\n " +
                    "       cc.KPI = '" + _kpi + "' \r\n ";
            foreach (DataRow dr in dtSic.Rows)
            {
                if ((dr["Type"].ToString() == "Book") |
                    (dr["Type"].ToString() == "Plan"))
                {
                    string _wp = ProductionGlobal.TProductionGlobal.ExtractBeforeColon(dr["Workplace"].ToString());
                    //string _wp = "";
                    string _thedate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dr["CalendarDate"].ToString()));
                    if (dr["TheValue"] != null)
                        if (dr["TheValue"].ToString() != "")
                            if (dr["ShiftNo"] != null)
                                if (dr["ShiftNo"].ToString() != "N")
                                {
                                    string theVal = "0";
                                    if (dr["ProblemCode"] != null)
                                        if (dr["ProblemCode"].ToString() == "")
                                            if (dr["Activity"].ToString() != "1")
                                                theVal = dr["TheValue"].ToString();
                                            else
                                            {
                                                if (dr["TheValue"].ToString() == "Yes")
                                                    theVal = "99";
                                                else
                                                    theVal = ProductionGlobal.TProductionGlobal.ExtractBeforeColon(dr["TheValue"].ToString());
                                            }
                                    theData.SqlStatement = theData.SqlStatement +
                                        " INSERT INTO SICCapture " +
                                        " (SicKey, CalendarDate, SectionID, SortHeading \r\n " +
                                        " ,MillMonth, Value, WorkPlaceID, ProblemCode, SBNotes, CausedLostBlast) \r\n " +
                                        " VALUES (" +
                                        " '" + dr["SICKey"].ToString() + "', '" + _thedate + "', " +
                                        " '" + _sectionid + "', null, '" + _prodmonth + "', " +
                                        " '" + theVal + "', '" + _wp + "', " +
                                        " '" + dr["ProblemCode"].ToString() + "', '" + dr["SBNotes"].ToString() + "', \r\n " +
                                        " '" + dr["CausedLostBlast"].ToString() + "') \r\n ";
                                }
                }
            }
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsSICCapture", "Save_SICClean", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
    }
}
