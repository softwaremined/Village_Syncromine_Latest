using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mineware.Systems.Global;
using System.Data.SqlClient;
using System.Data;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PrePlanning.RevisedPlanning_Security
{
    public class RevisedSecurityDB : clsBase
    {
        string dept = "";
        private string _hasChanged;
        public string hasChanged { get { return _hasChanged; } set { _hasChanged = value; } }
        public string UserName { get; set; }

        public DataTable GetDepartments()
        {

	        theData.ConnectionString = TConnections.GetConnectionString("SystemSettings", UserCurrentInfo.Connection);
			theData.SqlStatement = "SELECT DISTINCT  DepartmentID Department,[Description]  FROM  [tblDepartments]  ORDER BY [Description]";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement ;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
              theData.ExecuteInstruction();
            return theData.ResultsDataTable;
            //if (theData.ResultsDataTable.Rows.Count == 1)
            //    return _currentProdMonth = theData.ResultsDataTable.Rows[0]["CurrentProductionMonth"].ToString();
            //else return null;
        }

        //public DataTable GetPreTypes(string department, string USER,string section)
        //{
        //    theData.SqlStatement = "SELECT * FROM REVISEDPLANNING_USERSECURITY_ACTIONS rp inner join  CODE_PREPLANNINGTYPES cp on "+
        //                            "cp.changeid=rp.ChangeID  WHERE DEPARTMENT='" + department + "' AND UserID='" + USER + "' and Section='" + section + "' ";
        //    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        //    theData.queryReturnType = MWDataManager.ReturnType.DataTable;
        //    theData.ExecuteInstruction();
        //    return theData.ResultsDataTable;
        //}

        public DataTable PrePlanningTypes(string department)
        {
            theData.SqlStatement = " select ChangeID ,cp.Description,case when active is null  then cast(0 as bit) else active end Checked from CODE_PREPLANNINGTYPES cp left join  (SELECT distinct rp.Active  ,cp.Description  FROM CODE_PREPLANNINGTYPES cp  inner join  REVISEDPLANNING_USERSECURITY_ACTIONS rp on " +
                                    "cp.changeid=rp.ChangeID  WHERE DEPARTMENT='" + department + "') data on " +
									"data.description =cp.description ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

        public DataTable GetUserData(string department)
        {
            dept = department;
            theData.SqlStatement = "select distinct RS.Section ID,SC.Name,RS.UserID,RS.Department,RS.SecurityType,cs.SecurityType Description,RS.ApprovalRequired,RS.Authorize,'' added,'' edited,'' hasChanged from REVISEDPLANNING_SECURITY RS INNER JOIN REVISEDPLANNING_USERSECURITY_ACTIONS RU ON \n" +
                                   " RS.Department ='" + department + "' INNER JOIN SECTION SC ON \n"+
                                    "SC.SectionID =RS.Section inner join CODE_SECURITYTYPE cs on \n"+
                                    "cs.SecurityTypeID=RS.SecurityType\n" +
                                    "and sc.prodmonth = (Select CurrentProductionMonth from SYSSET)";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
            //if (theData.ResultsDataTable.Rows.Count == 1)
            //    return _currentProdMonth = theData.ResultsDataTable.Rows[0]["CurrentProductionMonth"].ToString();
            //else return null;
        }

        public DataTable GetSections(int prodmonth)
        {

           // theData.SqlStatement = "SELECT SectionID ID,Name,ReportToID ParentID FROM SECTION WHERE PRODMONTH=" + prodmonth + "";
            theData.SqlStatement = "SELECT SectionID ID,Name, ReportToSectionid ParentID FROM SECTION WHERE PRODMONTH=" + prodmonth + " AND HierarchicalID in (3,4)";
	 
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
            //if (theData.ResultsDataTable.Rows.Count == 1)
            //    return _currentProdMonth = theData.ResultsDataTable.Rows[0]["CurrentProductionMonth"].ToString();
            //else return null;
        }

        public DataTable GetUserList()
        {
            //SELECT UserID, NAME+'' ''+LastName UserName FROM ' + @sysDB +'dbo.tblUsers
            theData.SqlStatement = "[sp_Get_User_List]";
            theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

        public DataTable GetUserList(String DepartmentID)
        {
            theData.ConnectionString = TConnections.GetConnectionString("SystemSettings", UserCurrentInfo.Connection);
            //SELECT UserID, NAME+'' ''+LastName UserName FROM ' + @sysDB +'dbo.tblUsers
            theData.SqlStatement = "SELECT UserID, NAME+' '+LastName UserName FROM tblUsers Where DepartmentID = '"+ DepartmentID + "'";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

        public DataTable GetSecurityType()
        {
           // theData.ConnectionString = TConnections.GetConnectionString("SystemSettings", UserCurrentInfo.Connection);
            theData.ConnectionString = TConnections.GetConnectionString("DBHARMONYPAS", UserCurrentInfo.Connection);
            theData.SqlStatement = "select SecurityTypeID SecurityType, SecurityType Description from CODE_SECURITYTYPE";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

    

        //public void savedata(int changeid, string active, string USER, string DEPT,string SECTION)
        //{
        //    theData.SqlStatement = "select * from REVISEDPLANNING_USERSECURITY_ACTIONS where UserID='" + USER + "' and ChangeID =" + changeid + " and department='" + DEPT + "' ";
        //    theData.queryReturnType = MWDataManager.ReturnType.DataTable;
        //    theData.ExecuteInstruction();
        //    DataTable dt = new DataTable();
        //    dt = theData.ResultsDataTable;
        //    if (dt.Rows.Count == 0)
        //    {

        //        //var TheData1 = new MWDataManager.clsDataAccess();
        //        //TheData1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
        //        //TheData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        //        theData.SqlStatement = "";
        //        theData.SqlStatement = "INSERT INTO REVISEDPLANNING_USERSECURITY_ACTIONS (ChangeID, UserID, Active,Department,Section) values (" + changeid + ", '" + USER + "', " + active + ",'" + DEPT + "','" + SECTION + "') ";
        //        theData.queryReturnType = MWDataManager.ReturnType.DataTable;
        //        theData.ExecuteInstruction();
        //    }
        //    else
        //    {
        //        //var TheData1 = new MWDataManager.clsDataAccess();
        //        //TheData1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
        //        //TheData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        //        theData.SqlStatement = "";
        //        theData.SqlStatement = "update REVISEDPLANNING_USERSECURITY_ACTIONS set Active =" + active + ", Section='" + SECTION + "' where ChangeID= " + changeid + " AND UserID='" + USER + "' and Department='" + DEPT + "' ";
        //        theData.queryReturnType = MWDataManager.ReturnType.DataTable;
        //        theData.ExecuteInstruction();
        //    }
        //}

        public void savedatanew(int changeid, int  active, string DEPT)
        {
            theData.ConnectionString = TConnections.GetConnectionString("DBHARMONYPAS", UserCurrentInfo.Connection);
            theData.SqlStatement = "select * from REVISEDPLANNING_USERSECURITY_ACTIONS where ChangeID =" + changeid + " and department='" + DEPT + "' ";
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            DataTable dt = new DataTable();
            dt = theData.ResultsDataTable;
            if (dt.Rows.Count == 0)
            {
                theData.ConnectionString = TConnections.GetConnectionString("DBHARMONYPAS", UserCurrentInfo.Connection);
                theData.SqlStatement = "";
                theData.SqlStatement = "INSERT INTO REVISEDPLANNING_USERSECURITY_ACTIONS (ChangeID, Active,Department) values (" + changeid + ", " + active + ",'" + DEPT + "') ";
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            else
            {
                theData.ConnectionString = TConnections.GetConnectionString("DBHARMONYPAS", UserCurrentInfo.Connection);
                theData.SqlStatement = "";
                theData.SqlStatement = "update REVISEDPLANNING_USERSECURITY_ACTIONS set Active =" + active + " where ChangeID= " + changeid + "  and Department='" + DEPT + "' ";
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
        }

        public void deletedata(string  section,string user, string Dept)
        {

            //theData.SqlStatement = "";
            //theData.SqlStatement = "delete from REVISEDPLANNING_USERSECURITY_ACTIONS where   Department='" + Dept   + "' ";
            //theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //theData.ExecuteInstruction();
            theData.ConnectionString = TConnections.GetConnectionString("DBHARMONYPAS", UserCurrentInfo.Connection);
            theData.SqlStatement = "";
                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.SqlStatement = "delete from REVISEDPLANNING_SECURITY where Section ='" + section + "' and UserID= '" + user + "'  and Department='" + Dept + "' ";
                    theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    theData.ExecuteInstruction();
              
        }

        public DataTable getcrew(string department, string USER, string section)
        {
            theData.SqlStatement = "";
            theData.SqlStatement = "select * from CODE_PREPLANNINGTYPES  where Description not in(SELECT cp.Description   FROM REVISEDPLANNING_USERSECURITY_ACTIONS rp inner join  CODE_PREPLANNINGTYPES cp on " +
                                   " cp.changeid=rp.ChangeID  WHERE DEPARTMENT='" + department + "' AND UserID='" + USER + "' and Section='" + section + "') ";
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

        public void saveRecords(DataTable records, string Dept)
        {
            //var theData = new MWDataManager.clsDataAccess();
            //theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            foreach (DataRow dr in records.Rows)
            {

                if (dr["hasChanged"].ToString() == "1")
                {

                    string Section = dr["ID"].ToString();

                    MWDataManager.clsDataAccess _Query = new MWDataManager.clsDataAccess();
                    _Query.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
                    _Query.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _Query.queryReturnType = MWDataManager.ReturnType.DataTable;

                    String Prodmonth = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString();

                    _Query.SqlStatement = "Select isnull(HierarchicalID,0) HierarchicalID from Section where Prodmonth = " + Prodmonth + " and SectionID = '" + Section + "'";
                    _Query.ExecuteInstruction();

                    Int32 Level = 5;

                    foreach (DataRow r in _Query.ResultsDataTable.Rows)
                    {
                        Level = Convert.ToInt32(r["HierarchicalID"]);
                    }

                    string SectionID = "SectionID_2";

                    switch (Level)
                    {
                        case 1:
                            SectionID = "SectionID_5";
                            break;
                        case 2:
                            SectionID = "SectionID_4";
                            break;
                        case 3:
                            SectionID = "SectionID_3";
                            break;
                        case 4:
                            SectionID = "SectionID_2";
                            break;
                        case 5:
                            SectionID = "SectionID_1";
                            break;
                        case 6:
                            SectionID = "SectionID";
                            break;
                    }

                    _Query.SqlStatement = "Select * \n " +
                                            "from PREPLANNING_CHANGEREQUEST a inner\n " +
                                            "join\n " +
                                            "PREPLANNING_CHANGEREQUEST_APPROVAL b on\n " +
                                            "a.ChangeRequestID = b.ChangeRequestID\n " +
                                            "inner\n " +
                                            "join Syncro_tblUsers c on\n " +
                                            "b.User1 = c.USERID\n " +
                                            "inner\n " +
                                            "join Section_Complete d on\n " +
                                            "a.prodmonth = d.Prodmonth and\n " +
                                            "a.SectionID = d.SectionID\n " +
                                            "where \n" +
                                            "d." + SectionID + " = '" + Section + "' \n" +
                                            "and b.approved = 0 \n" +
                                            "and Declined = 0";
                    _Query.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _Query.ExecuteInstruction();

                    if (_Query.ResultsDataTable.Rows.Count > 0)
                    {
                        frmUpdateRevicedRecords theform = new frmUpdateRevicedRecords();
                        theform.UserID = dr["UserID"].ToString();
                        theform.Section = dr["ID"].ToString();
                        theform.UserName = UserName;
                        theform.UserCurrentInfo = UserCurrentInfo;
                        theform.theSystemDBTag = systemDBTag;

                        theform.ShowDialog();
                    }

                    try
                    {
                        theData.SqlStatement = "";
                        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        theData.SqlStatement = "select * from REVISEDPLANNING_SECURITY where UserID='" + dr["UserID"].ToString() + "' and Section ='" + dr["ID"].ToString() + "' and department='" + Dept + "'  ";
                        theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        theData.ExecuteInstruction();
                    }
                    catch (Exception e)
                    { }
                    DataTable dt = new DataTable();
                    dt = theData.ResultsDataTable;
                    if (dt.Rows.Count == 0)
                    {
                        try
                        {
                            //var TheData1 = new MWDataManager.clsDataAccess();
                            //TheData1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            theData.SqlStatement = "";
                            theData.SqlStatement = "INSERT INTO REVISEDPLANNING_SECURITY (Section, UserID, Department, SecurityType, ApprovalRequired) values ('" + dr["ID"].ToString() + "', '" + dr["UserID"].ToString() + "','" + Dept + "', '" + dr["SecurityType"].ToString() + "', '" + dr["ApprovalRequired"].ToString() + "') ";
                            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                            theData.ExecuteInstruction();
                        }
                        catch (Exception E)
                        { }
                    }
                    else
                    {
                        try
                        {
                            //var TheData1 = new MWDataManager.clsDataAccess();
                            //TheData1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            theData.SqlStatement = "";
                            theData.SqlStatement = "update REVISEDPLANNING_SECURITY set   SecurityType= '" + dr["SecurityType"].ToString() + "', ApprovalRequired= '" + dr["ApprovalRequired"].ToString() + "' where UserID='" + dr["UserID"].ToString() + "' and Department='" + Dept + "' and Section='" + dr["ID"].ToString() + "'";
                            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                            theData.ExecuteInstruction();
                        }
                        catch (Exception E)
                        { }
                    }
                }
            }
        }

    }
}
