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

namespace Mineware.Systems.Production.SysAdminScreens.ProblemSetup
{
    class clsProblemSetupData:clsBase 
    {
        private MWDataManager.clsDataAccess TheData = new MWDataManager.clsDataAccess();
        public TUserCurrentInfo CurrentUser = new TUserCurrentInfo();

        public string SystemDBTag;

        public void setConnectionString()
        {
            TheData.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, CurrentUser.Connection);
        }

        public DataTable getProblemType(int _activity)
        {
            bool HasError = false;
            try
            {

                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.SqlStatement = "select * from CODE_PROBLEM_TYPE a inner join " +
                                       "CODE_ACTIVITY b on " +
                                       "a.Activity = b.Activity " +
                                       "where Deleted IN ('Y', 'N') " +
                                       "and a.Activity = " + _activity;

                
                theData.ExecuteInstruction();
            }
            catch
            {
                HasError = true;
            }

            if (HasError == true)
            {
                return null;
            }
            else
            {
                return theData.ResultsDataTable;
            }
        }

        public DataTable getProblemTypeLinks(string Problem_Type, int _activity)
        {
            bool HasError = false;
            try
            {

                TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                TheData.SqlStatement = "select \n" +
                                       "a.ProblemID,\n" +
                                       "a.Description,\n" +
                                       "Linked = Case when b.ProblemTypeID is null then Cast(0 as Bit) else  Cast(1 as Bit) end\n" +
                                       " from CODE_PROBLEM a left join \n" +
                                       "(select a.*, b.ProblemID from CODE_PROBLEM_TYPE a left join PROBLEM_TYPE b \n" +
                                       "on a.Activity = b.Activity and\n" +
                                       "a.ProblemTypeID = b.ProblemTypeID\n" +
                                       "where a.Deleted IN ('Y', 'N')\n" +
                                       "and a.Activity = " + _activity +"\n" +
                                       "and a.ProblemTypeID = '" + Problem_Type + "') b on\n" +
                                       "a.ProblemID = b.ProblemID and\n" +
                                       "a.Activity = b.Activity\n" +
                                       "where \n" +
                                       "a.Deleted IN ('Y', 'N') and\n" +
                                       "a.Activity = " + _activity;
                TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                TheData.queryReturnType = MWDataManager.ReturnType.DataTable;
                TheData.ExecuteInstruction();
            }
            catch
            {
                HasError = true;
            }

            if (HasError == true)
            {
                return null;
            }
            else
            {
                return TheData.ResultsDataTable;
            }
        }

        public DataTable getProblemDescription(int _activity)
        {
            bool HasError = false;
            try
            {

                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "select *,\n" +
                                       "TheDrillRig = case when DrillRig = 'N' then cast(0 as Bit) else cast(1 as Bit) end, \n" +
                                       "IsCausedLostBlast = case when CausedLostBlast = 'Y' then cast(1 as Bit) else cast(0 as Bit) end from CODE_PROBLEM a inner join \n" +
                                       "CODE_ACTIVITY b on \n" +
                                       "a.Activity = b.Activity \n" +
                                       "where Deleted IN ('Y', 'N') \n" +
                                       "and a.Activity = " + _activity;

                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            catch
            {
                HasError = true;
            }

            if (HasError == true)
            {
                return null;
            }
            else
            {
                return theData.ResultsDataTable;
            }
        }

        public DataTable getProblemLinks(string Problem, int _activity)
        {
            bool HasError = false;
            try
            {

                TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                TheData.SqlStatement = "select \n" +
                                       "a.NoteID,\n" +
                                       "a.Description,\n" +
                                       "Linked = Case when b.NoteID is null then Cast(0 as Bit) else  Cast(1 as Bit) end\n" +
                                       " from CODE_PROBLEM_NOTE a left join \n" +
                                       "(select a.*, b.NoteID from CODE_PROBLEM a left join PROBLEM_NOTE b \n" +
                                       "on a.Activity = b.Activity and\n" +
                                       "a.ProblemID = b.ProblemID\n" +
                                       "where a.Deleted IN ('Y', 'N')\n" +
                                       "and a.Activity = " + _activity + "\n" +
                                       "and a.ProblemID = '" + Problem + "') b on\n" +
                                       "a.NoteID = b.NoteID and\n" +
                                       "a.Activity = b.Activity\n" +
                                       "where \n" +
                                       "a.Deleted IN ('Y', 'N') and\n" +
                                       "a.Activity = " + _activity;

                TheData.queryReturnType = MWDataManager.ReturnType.DataTable;
                TheData.ExecuteInstruction();
            }
            catch
            {
                HasError = true;
            }

            if (HasError == true)
            {
                return null;
            }
            else
            {
                return TheData.ResultsDataTable;
            }
        }

        public DataTable getProblemNote(int _activity)
        {
            bool HasError = false;
            try
            {

                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "select * ,\n" +
                                       "LostBlast = case when NotLostBlastProblem = 'N' then cast(1 as Bit) else cast(0 as Bit) end \n" +
                                       "from CODE_PROBLEM_NOTE a inner join \n" +
                                       "CODE_ACTIVITY b on \n" +
                                       "a.Activity = b.Activity \n" +
                                       "where Deleted IN ('Y', 'N') \n" +
                                       "and a.Activity = " + _activity;

                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            catch
            {
                HasError = true;
            }

            if (HasError == true)
            {
                return null;
            }
            else
            {
                return theData.ResultsDataTable;
            }
        }

        public Boolean saveProblemType(string ProblemType, string ProblemTypeDesc, DataTable TheLinkTable, int _activity, string Action)
        {
            Boolean HasError = false;
            try
            {
                if (Action == "A")
                {
                    TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

                    if (ProblemType == "" && ProblemTypeDesc == "")
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields cannot be blank", Color.Red);
                    }

                    else
                    {
                        TheData.SqlStatement = "Insert into CODE_PROBLEM_TYPE (ProblemTypeID, Activity, Description, Deleted) Values (\n" +
                                                "'" + ProblemType + "',\n" +
                                                _activity.ToString() + ",\n" +
                                                "'" + ProblemTypeDesc + "', 'N')";
                        TheData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        TheData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Problem Types Data was saved", Color.CornflowerBlue);
                    }

                }

                if (Action == "E")
                {
                    if (ProblemTypeDesc == "")
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields cannot be blank", Color.Red);
                    }

                    else
                    {
                        TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        TheData.SqlStatement = "update CODE_PROBLEM_TYPE set Description = '" + ProblemTypeDesc + "'\n" +
                                               "where ProblemTypeID = '" + ProblemType + "'\n" +
                                               "and Activity = " + _activity.ToString();

                        TheData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        TheData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Problem Types Data was saved", Color.CornflowerBlue);
                    }

                }

                TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                TheData.SqlStatement = "Delete from PROBLEM_TYPE\n" +
                                       "where ProblemTypeID = '" + ProblemType + "'\n" +
                                       "and Activity = " + _activity.ToString();

                TheData.queryReturnType = MWDataManager.ReturnType.longNumber;
                TheData.ExecuteInstruction();

                foreach (DataRow dr in TheLinkTable.Rows)
                {
                    if (dr["Linked"].ToString() == "True")
                    {
                        TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        TheData.SqlStatement = "Insert into PROBLEM_TYPE (ProblemTypeID, ProblemID, Activity, Deleted) \n" +
                                               "Values ('" + ProblemType + "','" + dr["ProblemID"].ToString() + "'," + _activity.ToString() + ",'N')";

                        TheData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        TheData.ExecuteInstruction();
                    }
                }
            }
            catch
            {
                HasError = true;
            }

            if (HasError == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean saveProblems(string ProblemCode, string ProblemDesc, Boolean DrillRig, Boolean CausedLostBlast, DataTable TheLinkTable, int _activity, string Action)
        {
            string strLostBlast = "N";

            string strDrillrig = "N";

            if (DrillRig.ToString() == "True")
                strDrillrig = "Y";

            if (CausedLostBlast.ToString() == "True")
                strLostBlast = "Y";

            else
            strLostBlast = "N";


            Boolean HasError = false;
            try
            {
                if (Action == "A")
                {
                    if (ProblemCode == "" && ProblemDesc == "")
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", "Required fields cannot be blank", Color.Red);
                    }

                    else
                    {
                        TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        TheData.SqlStatement = "Insert into CODE_PROBLEM (ProblemID, Activity, Description, DrillRig, CausedLostBlast, Deleted) Values (\n" +
                                               "'" + ProblemCode + "',\n" +
                                               _activity.ToString() + ",\n" +
                                              "'" + ProblemDesc + "', '" + strDrillrig + "', 'N')";

                        TheData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        TheData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Problems Data was saved", Color.CornflowerBlue);
                    }


                }

                if (Action == "E")
                {
                    if (ProblemDesc == "")
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", "Required fields cannot be blank", Color.Red);
                    }

                    else
                    {
                        TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        TheData.SqlStatement = "update CODE_PROBLEM set Description = '" + ProblemDesc + "',\n" +
                                               "DrillRig = '" + strDrillrig + "', \n" +
                                               "CausedLostBlast = '" + strLostBlast + "'\n" +
                                                   "where ProblemID = '" + ProblemCode + "'\n" +
                                                   "and Activity = " + _activity.ToString();

                        TheData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        TheData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Problems Data was saved", Color.CornflowerBlue);
                    }

                    
                }

                TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                TheData.SqlStatement = "Delete from PROBLEM_NOTE\n" +
                                       "where ProblemID = '" + ProblemCode + "'\n" +
                                       "and Activity = " + _activity.ToString();

                TheData.queryReturnType = MWDataManager.ReturnType.longNumber;
                TheData.ExecuteInstruction();

                foreach (DataRow dr in TheLinkTable.Rows)
                {
                    if (dr["Linked"].ToString() == "True")
                    {
                        TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        TheData.SqlStatement = "Insert into PROBLEM_NOTE (ProblemID, NOTEID, Activity, Deleted) \n" +
                                               "Values ('" + ProblemCode + "','" + dr["NoteID"].ToString() + "'," + _activity.ToString() + ",'N')";

                        TheData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        TheData.ExecuteInstruction();
                    }
                }
            }
            catch
            {
                HasError = true;
            }

            if (HasError == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean saveNote(string NoteCode, string NoteCodeDesc, string Explanation, Boolean LostBlast, int _activity, string Action)
        {
            string strLostBlast = "Y";

            if (LostBlast.ToString() == "True")
            {
                strLostBlast = "N";
            }

            Boolean HasError = false;
            try
            {
                if (Action == "A")
                {
                    if (NoteCode == "" && NoteCodeDesc == "" && Explanation == "")
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", "Required fields cannot be blank", Color.Red);
                    }

                    else
                    {
                        TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        TheData.SqlStatement = "Insert into CODE_PROBLEM_NOTE (NOTEID, Activity, Description, Explanation, NotLostBlastProblem, Deleted) Values (\n" +
                                               "'" + NoteCode + "',\n" +
                                               _activity.ToString() + ",\n" +
                                              "'" + NoteCodeDesc + "'\n" +
                                              ", '" + Explanation + "', '" + strLostBlast + "', 'N')";



                        TheData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        TheData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Problem Note Data was saved", Color.CornflowerBlue);
                    }
                    
                }

                if (Action == "E")
                {
                    if (NoteCodeDesc == "" && Explanation == "")
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", "Required fields cannot be blank", Color.Red);
                    }

                    else
                    {
                        TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        TheData.SqlStatement = "update CODE_PROBLEM_NOTE set Description = '" + NoteCodeDesc + "',\n" +
                                               "Explanation = '" + Explanation + "',\n" +
                                               "Deleted = 'Y',\n" +
                                               "NotLostBlastProblem = '" + strLostBlast + "',\n" +
                                               "Color = 0\n" +
                                               "where NOTEID = '" + NoteCode + "'\n" +
                                               "and Activity = " + _activity.ToString();

                        TheData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        TheData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Problem Note Data was saved", Color.CornflowerBlue);
                    }

                    
                }
            }
            catch
            {
                HasError = true;
            }

            if (HasError == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
