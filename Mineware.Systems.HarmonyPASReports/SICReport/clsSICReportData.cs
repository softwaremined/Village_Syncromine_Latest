using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.SICReport
{
    class clsSICReportData
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();

        public string connectionString;
        public string DBTag;

        public string PerfMonth3;
        public string PerfMonth12;
        public int Themonth;
        public DataTable get_Sections(string _date)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                " select distinct(Name) Name, SectionID SectionID \r\n " +
                " from Section, Sysset ss  \r\n " +
                " where Prodmonth in ( \r\n " +
                "     select distinct(Prodmonth) ProdMonth \r\n " +
                "     from seccal \r\n " +
                "     where BeginDate <= '" + _date + "' and \r\n " +
                "           EndDate >= '" + _date + "') \r\n " +
                " and Hierarchicalid <= ss.MOHIERARCHICALID \r\n " +
                " order by SectionID, Name ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSICReportData", "get_Sections", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Mills()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                " select '' OreflowID, '<<<All>>>' Name " +
                " union all \r\n " +
                " select OreflowID OreflowID, OreflowID + ': ' + Name Name \r\n " +
                " from OreflowEntities where OreflowCode = 'Mill' \r\n ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSICReportData", "get_Mills", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_HierID(string _date, string _sectionid, string _name)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                " select distinct(HierarchicalID) HierID \r\n " +
                " from Seccal sc \r\n " +
                " inner join Section s on \r\n " +
                "   s.Prodmonth = s.Prodmonth \r\n " +
                " where sc.BeginDate <='" + _date + "' and \r\n " +
                "       sc.EndDate >= '" + _date + "' and \r\n " +
                "       s.SectionID = '" + _sectionid + "' and \r\n " +
                "       s.Name = '" + _name + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSICReportData", "get_Parameters", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public bool delete_TempTable(string _userid)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                " delete from TempWorkDaysMO \r\n " +
                " where UserID = '" + _userid + "' \r\n " +
                " delete from Temp_MOStartDate \r\n " +
                " where UserID = '" + _userid + "' " +
                " delete from Temp_SectionStartDate \r\n " +
                " where UserID = '" + _userid + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSICReportData", "delete_TempTable", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
        public bool save_Temp_MOStartDate(string _date, string _userid)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = 
                    " select distinct(sc.SectionID_2) SectionID, s.ProdMonth ProdMonth, \r\n " +
                    " s.BeginDate BeginDate, \r\n " +
                    " s.EndDate EndDate \r\n " +
                    " from SECCAL s \r\n " +
                    " inner join Section_Complete sc on \r\n " +
                    "   sc.SectionID_1 = s.Sectionid and \r\n " +
                    "   sc.Prodmonth = s.Prodmonth \r\n " +
                    " where s.BeginDate <= '" + _date + "' and \r\n " +
                    "        s.EndDate >= '" + _date + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            DataTable dd = theData.ResultsDataTable;
            foreach (DataRow r in dd.Rows)
            {
                theData.SqlStatement = " Insert into Temp_MOStartDate \r\n ";
                theData.SqlStatement += "( UserID, ProdMonth, SectionID, StartDate, EndDate) values ( \r\n ";
                theData.SqlStatement += " '" + _userid + "', \r\n ";
                theData.SqlStatement += " '" + r["ProdMonth"].ToString() + "', \r\n ";
                theData.SqlStatement += " '" + r["SectionID"].ToString() + "', \r\n ";
                theData.SqlStatement += " '" + string.Format("{0:yyyy-MM-dd}", r["BeginDate"]) + "',\r\n ";
                theData.SqlStatement += " '" + string.Format("{0:yyyy-MM-dd}", r["EndDate"]) + "') ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();
            }
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSICReportData", "save_Temp_MOStartDate", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
        public bool saveTemp_SectionStartDate(string _date, string _userid)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = 
                    " select sc.SectionID SectionID, s.ProdMonth ProdMonth, \r\n " +
                    " sc.SectionID_1 SectionID_1, \r\n " +
                    " s.BeginDate BeginDate, s.EndDate EndDate \r\n " +
                    " from SECCAL s \r\n " +
                    " inner join Section_Complete sc on \r\n " +
                    "   sc.sectionID_1 = s.Sectionid and \r\n " +
                    "   sc.Prodmonth = s.Prodmonth \r\n " +
                    " where s.BeginDate <= '" + _date + "' and \r\n " +
                    "        s.EndDate >= '" + _date + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            DataTable aa = theData.ResultsDataTable;
            theData.SqlStatement = "";
            foreach (DataRow r in aa.Rows)
            {
                theData.SqlStatement = theData.SqlStatement +
                        " Insert into Temp_SectionStartDate \r\n " +
                        "( UserID, ProdMonth, SectionID, SectionID_1,StartDate, EndDate) values ( \r\n " +
                        " '" + _userid + "', \r\n " +
                        " '" + r["ProdMonth"].ToString() + "', \r\n " +
                        " '" + r["SectionID"].ToString() + "', \r\n " +
                        " '" + r["SectionID_1"].ToString() + "', \r\n " +
                        " '" + string.Format("{0:yyyy-MM-dd}", r["BeginDate"]) + "', \r\n " +
                        " '" + string.Format("{0:yyyy-MM-dd}", r["EndDate"]) + "') ";
            }        
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            clsDataResult errorMsg = theData.ExecuteInstruction();

            theData.SqlStatement = 
                    " select UserID UserID, SectionID SectionID, ProdMonth ProdMonth, \r\n " +
                    "   SectionID_1 SectionID_1 \r\n " +
                    " from Temp_SectionStartDate \r\n " +
                    " where UserID = '" + _userid + "' and \r\n " +
                    "       StartDate <= '" + _date + "' and \r\n " +
                    "       EndDate >= '" + _date + "' \r\n ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            DataTable bb = theData.ResultsDataTable;
            foreach (DataRow r in bb.Rows)
            {
                PerfMonth3 = r["ProdMonth"].ToString();

                for (int i = 0; i < 1; i++)
                {
                    if (PerfMonth3.Substring(4, 2) == "01")
                    {
                        PerfMonth3 = Convert.ToString((Convert.ToInt32(PerfMonth3.Substring(0, 4)) - 1) + "12");
                    }
                    else
                    {
                        Themonth = Convert.ToInt32(PerfMonth3.ToString()) - 1;
                        PerfMonth3 = Convert.ToString(Themonth);
                    }
                }
                theData.SqlStatement = 
                        " select BeginDate BeginDate, EndDate EndDate \r\n " +
                        " from Seccal where prodMonth = '" + PerfMonth3 + "' and \r\n " +
                        "      SectionID = '" + r["SectionID_1"].ToString() + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();

                DataTable cc = theData.ResultsDataTable;
                theData.SqlStatement = "";
                foreach (DataRow s in cc.Rows)
                {
                    theData.SqlStatement = theData.SqlStatement +
                            " Update Temp_SectionStartDate \r\n " +
                            " set PrevProdMonth = '" + PerfMonth3 + "', " +
                            "     PrevStartDate = '" + string.Format("{0:yyyy-MM-dd}", s["BeginDate"]) + "', \r\n " +
                            "     PrevEndDate = '" + string.Format("{0:yyyy-MM-dd}", s["EndDate"]) + "' \r\n " +
                            " where UserID = '" + _userid + "' and \r\n " +
                            "       SectionID = '" + r["SectionID"].ToString() + "' \r\n ";
                }
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();
            }

            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSICReportData", "save_Temp_SectionStartDate", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
        public bool saveTempWorkdaysMO(string _date, string _userid)
        {
            theData.SqlStatement = 
                    " select distinct(SectionID_2) SectionID, s.ProdMonth ProdMonth \r\n " +
                    " from SECCAL s \r\n " +
                    " inner join Section_Complete sc on \r\n " +
                    "   sc.SectionID_1 = s.Sectionid and \r\n " +
                    "   sc.Prodmonth = s.Prodmonth \r\n " +
                    " where s.BeginDate <= '" + _date + "' and \r\n " +
                    "        s.EndDate >= '" + _date + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            DataTable ww = theData.ResultsDataTable;
            theData.SqlStatement = "";
            foreach (DataRow r in ww.Rows)
            {
                theData.SqlStatement = theData.SqlStatement + 
                    " exec sp_GetShiftMO '" + _userid + "', '" + r["ProdMonth"].ToString() + "', '" + r["SectionID"].ToString() + "' ";                
            }
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            clsDataResult errorMsg = theData.ExecuteInstruction();

            theData.SqlStatement = 
                    " select DISTINCT(SectionID) SectionID, ProdMonth ProdMonth \r\n " +
                    " from TempWorkDaysMO \r\n " +
                    " where UserID = '" + _userid + "'  ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            DataTable xx = theData.ResultsDataTable;
            theData.SqlStatement = "";
            foreach (DataRow r in xx.Rows)
            {
                PerfMonth3 = r["ProdMonth"].ToString();

                for (int i = 0; i < 1; i++)
                {
                    if (PerfMonth3.Substring(4, 2) == "01")
                    {
                        PerfMonth3 = Convert.ToString((Convert.ToInt32(PerfMonth3.Substring(0, 4)) - 1) + "12");
                    }
                    else
                    {
                        Themonth = Convert.ToInt32(PerfMonth3.ToString()) - 1;
                        PerfMonth3 = Convert.ToString(Themonth);
                    }
                }
                theData.SqlStatement = theData.SqlStatement + 
                    " exec sp_GetShiftMO '" + _userid + "', '" + PerfMonth3 + "', '" + r["SectionID"].ToString() + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }

            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSICReportData", "save_Temp_SectionStartDate", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
        public DataTable get_sICDetail(string _prodmonth, string _sectionid, string _workplaceid, string _activity)
        {
            theData.SqlStatement = "[sp_Report_SIC_Detail]";
            theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            theData.ResultsTableName = "SIC_Detail";

            SqlParameter[] _paramCollection =
                {
                    theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, _prodmonth),
                    theData.CreateParameter("@SectionID", SqlDbType.Text, 20, _sectionid),
                    theData.CreateParameter("@WorkplaceID", SqlDbType.Text, 12, _workplaceid),
                    theData.CreateParameter("@Activity", SqlDbType.Text, 1, _activity),
                };

            theData.ParamCollection = _paramCollection;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
    }
}
