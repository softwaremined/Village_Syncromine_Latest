using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Users
{
    class clsUserProductionSettings : IDisposable
    {
        public TUserCurrentInfo UserCurrentInfo = new TUserCurrentInfo();
        private MWDataManager.clsDataAccess TheData = new MWDataManager.clsDataAccess();
        private StringBuilder sb = new StringBuilder();
        private Mineware.Systems.Global.sysMessages.sysMessagesClass myMessage = new Global.sysMessages.sysMessagesClass();

        public DataTable getUserSections(string userID, string type)
        {
            TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TheData.queryReturnType = MWDataManager.ReturnType.DataTable;
            TheData.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, this.UserCurrentInfo.Connection);

            sb.Clear();
            sb.AppendLine("SELECT Distinct SEC.SectionID,SEC.Name,SEC.ReportToSectionID, ");
            sb.AppendLine("  CASE WHEN UD.SectionID is not NULL THEN Cast(1 as bit) ELSE Cast(0 as bit) END IsLinked, ");
            sb.AppendLine("  cast(0 as bit) Updated");
            sb.AppendLine("FROM [dbo].[SECTION] SEC");
            sb.AppendLine("LEFT JOIN ");
            sb.AppendLine(String.Format("(SELECT * FROM [dbo].[USERS_SECTION] WHERE UserID = '{0}' and LinkType = '{1}') UD ON", userID, type));
            sb.AppendLine("SEC.SectionID = UD.SectionID");
            sb.AppendLine("WHERE Prodmonth >= " + TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            // sb.AppendLine(String.Format("SELECT * FROM USERS WHERE UserID = '{0}'", userID));
            TheData.SqlStatement = sb.ToString();
            TheData.ExecuteInstruction();
            return TheData.ResultsDataTable;
        }
        public DataTable getUserInfo(string userID)
        {
            TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TheData.queryReturnType = MWDataManager.ReturnType.DataTable;
            TheData.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, this.UserCurrentInfo.Connection);

            sb.Clear();
            sb.AppendLine("SELECT [UserID]");
            sb.AppendLine("      ,BackDateBooking = case when isnull([BackDateBooking],0) = 0 then 0 else [BackDateBooking] end");
            sb.AppendLine("      ,[DaysBackdate]");
            sb.AppendLine("      ,WPProduction = case when [WPProduction] = 'Y' then Cast(1 as bit) else cast(0 as bit) end ");
            sb.AppendLine("      ,WPSurface = case when [WPSurface] = 'Y' then Cast(1 as bit) else cast(0 as bit) end");
            sb.AppendLine("      ,WPUnderGround = case when [WPUnderGround] = 'Y' then Cast(1 as bit) else cast(0 as bit) end");           
            sb.AppendLine("      ,WPEditName = case when [WPEditName] = 'Y' then Cast(1 as bit) else cast(0 as bit) end");
            sb.AppendLine("      ,WPEditAttribute = case when [WPEditAttribute] = 'Y' then Cast(1 as bit) else cast(0 as bit) end");
            sb.AppendLine("      ,WPClassify = case when [WPClassify] = 'Y' then Cast(1 as bit) else cast(0 as bit) end");      
            sb.AppendLine("  FROM [dbo].[USERS]");
            sb.AppendLine(String.Format("WHERE UserID = '{0}'", userID));
            TheData.SqlStatement = sb.ToString();
            TheData.ExecuteInstruction();
            if (TheData.ResultsDataTable.Rows.Count == 0)
            {
                DataRow newUserRow = TheData.ResultsDataTable.NewRow();
                newUserRow["UserID"] = userID;
                newUserRow["BackDateBooking"] = 0;
                newUserRow["DaysBackdate"] = 0;
                newUserRow["WPProduction"] = 0;                
                newUserRow["WPSurface"] = 0;
                newUserRow["WPUnderGround"] = 0;
                newUserRow["WPEditName"] = 0;
                newUserRow["WPEditAttribute"] = 0;
                newUserRow["WPClassify"] = 0;
                TheData.ResultsDataTable.Rows.Add(newUserRow);
            }
            return TheData.ResultsDataTable;
        }



        public bool SaveUserSettings(DataTable userInfo,string userID)
        {
            bool theResult = false;
            TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TheData.queryReturnType = MWDataManager.ReturnType.DataTable;
            TheData.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, this.UserCurrentInfo.Connection);
            TheData.SqlStatement = "SELECT * FROM USERS WHERE UserID = '" + userID + "'";
            TheData.ExecuteInstruction();

            string _wpproduction;
            string _wpsurface;
            string _wpunderground;
            string _wpeditname;
            string _wpeditattribute;
            string _wpclassify;

            if (TheData.ResultsDataTable.Rows.Count == 1)
            {
                sb.Clear();
                foreach (DataRow dr in userInfo.Rows)
                {
                    _wpproduction = "N";
                    _wpsurface = "N";
                    _wpunderground = "N";
                    _wpeditname = "N";
                    _wpeditattribute = "N";
                    _wpclassify = "N";
                    if (Convert.ToBoolean(dr["WPProduction"].ToString()) == true)
                        _wpproduction = "Y";
                    if (Convert.ToBoolean(dr["WPSurface"].ToString()) == true)
                        _wpsurface = "Y";
                    if (Convert.ToBoolean(dr["WPUnderGround"].ToString()) == true)
                        _wpunderground = "Y";
                    if (Convert.ToBoolean(dr["WPEditName"].ToString()) == true)
                        _wpeditname = "Y";
                    if (Convert.ToBoolean(dr["WPEditAttribute"].ToString()) == true)
                        _wpeditattribute = "Y";
                    if (Convert.ToBoolean(dr["WPClassify"].ToString()) == true)
                        _wpclassify = "Y";
                    try
                    {
                        sb.AppendLine("UPDATE [dbo].[USERS]");
                        sb.AppendLine("   SET [BackDateBooking] = " + dr["BackDateBooking"].ToString());
                        sb.AppendLine("      ,[DaysBackdate] = " + dr["DaysBackdate"].ToString());                       
                        sb.AppendLine("      ,[WPProduction] = '" + _wpproduction+"' ");
                        sb.AppendLine("      ,[WPSurface] = '" + _wpsurface + "' ");
                        sb.AppendLine("      ,[WPUnderGround] = '" + _wpunderground + "' ");
                        sb.AppendLine("      ,[WPEditName] = '" + _wpeditname + "' ");
                        sb.AppendLine("      ,[WPEditAttribute] = '" + _wpeditattribute + "' ");
                        sb.AppendLine("      ,[WPClassify] = '" + _wpclassify + "' ");
                        sb.AppendLine(" WHERE [UserID] = '" + userID + "'");

                        TheData.SqlStatement = sb.ToString();

                        TheData.ExecuteInstruction();
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "EDIT USER SETTINGS", userID, UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "EDIT USER BACK DATED BOOKINGS", userID + " : " + dr["BackDateBooking"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "EDIT USER WP PRODUCTION", userID + " : " + dr["WPProduction"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "EDIT USER WP SURFACE", userID + " : " + dr["WPSurface"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "EDIT USER WP UNDERGROUND", userID + " : " + dr["WPUnderGround"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "EDIT USER WP EDIT NAME", userID + " : " + dr["WPEditName"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "EDIT USER WP EDIT ATTRIBUTE", userID + " : " + dr["WPEditAttribute"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "EDIT USER CLASSIFY", userID + " : " + dr["WPClassify"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "EDIT USER ALLOW BACKDATED BOOKINGS", userID + " : " + dr["BackDateBooking"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "EDIT USER ALLOW DAYS BACKDATED BOOKINGS", userID + " : " + dr["DaysBackdate"].ToString(), UserCurrentInfo.Connection);
                        theResult = true;
                    }
                    catch (Exception e)
                    {
                        myMessage.viewMessage(MessageType.Error, "Error Saving User", resHarmonyPAS.systemName, "DL_Users", "SaveUserSettings", e.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                        theResult = false;
                    }
                }

            } // update
            else
            {
                sb.Clear();
                foreach (DataRow dr in userInfo.Rows)
                {
                    _wpproduction = "N";
                    _wpsurface = "N";
                    _wpunderground = "N";
                    _wpeditname = "N";
                    _wpeditattribute = "N";
                    _wpclassify = "N";
                    if (Convert.ToBoolean(dr["WPProduction"].ToString()) == true)
                        _wpproduction = "Y";
                    if (Convert.ToBoolean(dr["WPSurface"].ToString()) == true)
                        _wpsurface = "Y";
                    if (Convert.ToBoolean(dr["WPUnderGround"].ToString()) == true)
                        _wpunderground = "Y";
                    if (Convert.ToBoolean(dr["WPEditName"].ToString()) == true)
                        _wpeditname = "Y";
                    if (Convert.ToBoolean(dr["WPEditAttribute"].ToString()) == true)
                        _wpeditattribute = "Y";
                    if (Convert.ToBoolean(dr["WPClassify"].ToString()) == true)
                        _wpclassify = "Y";
                    sb.AppendLine("INSERT INTO [dbo].[USERS]");
                    sb.AppendLine("           ([UserID]");
                    sb.AppendLine("           ,[BackDateBooking]");
                    sb.AppendLine("           ,[DaysBackdate]");
                    sb.AppendLine("           ,[WPProduction]");
                    sb.AppendLine("           ,[WPSurface]");
                    sb.AppendLine("           ,[WPUnderGround]");                    
                    sb.AppendLine("           ,[WPEditName]");
                    sb.AppendLine("           ,[WPEditAttribute]");
                    sb.AppendLine("           ,[WPClassify])");
                    sb.AppendLine("     VALUES");
                    sb.AppendLine("           ('" + userID + "'");
                    sb.AppendLine("           ," + dr["BackDateBooking"].ToString());
                    sb.AppendLine("           ," + dr["DaysBackdate"].ToString());
                    sb.AppendLine("           ,'" + _wpproduction+"' ");
                    sb.AppendLine("           ,'" + _wpsurface + "' ");
                    sb.AppendLine("           ,'" + _wpunderground + "' ");
                    sb.AppendLine("           ,'" + _wpeditname + "' ");
                    sb.AppendLine("           ,'" + _wpeditattribute + "' ");
                    sb.AppendLine("           ,'" + _wpclassify + "') ");

                    TheData.SqlStatement = sb.ToString();
                    try
                    {
                        TheData.ExecuteInstruction();
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "NEW USER SETTINGS", userID, UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "NEW USER BACK DATED BOOKINGS", userID + " : " + dr["BackDateBooking"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "NEW USER ALLOW BACKDATED BOOKINGS", userID + " : " + dr["BackDateBooking"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "NEW USER WP PRODUCTION", userID + " : " + dr["WPProduction"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "NEW USER WP SURFACE", userID + " : " + dr["WPSurface"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "NEW USER WP UNDERGRUND", userID + " : " + dr["WPUnderGround"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "NEW USER WP EDIT NAME", userID + " : " + dr["WPEditName"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "NEW USER WP EDIT ATTRIBUTE", userID + " : " + dr["WPEditAttribute"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resHarmonyPAS.systemTag, "NEW USER WP CLASSIFY", userID + " : " + dr["WPClassify"].ToString(), UserCurrentInfo.Connection);
                        theResult = true;
                    }
                    catch (Exception e)
                    {
                        myMessage.viewMessage(MessageType.Error, "Error Adding User", Production.resHarmonyPAS.systemName, "clsUserProductionSettings", "SaveUserSettings", e.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                        theResult = false;
                    }
                } // insert                
            }
            return theResult;
        }
        public bool SaveUserSections(DataTable theSecions, string userID, string theType)
        {
            bool theResult = true;
            TheData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TheData.queryReturnType = MWDataManager.ReturnType.DataTable;
            TheData.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, this.UserCurrentInfo.Connection);
            //DataRow[] userSections = theSecions.Select("Updated = 1");
            sb.Clear();
            if (theSecions.Rows.Count != 0)
            {
                try
                {
                    TheData.SqlStatement = String.Format("DELETE FROM [dbo].[USERS_SECTION] WHERE [UserID] = '{0}' and [LinkType] = '{1}' ", userID, theType);
                    TheData.ExecuteInstruction();

                    string Linked;
                    foreach (DataRow dr in theSecions.Rows)
                    {
                        Linked = dr["IsLinked"].ToString();
                        if (Linked == "True")
                        {
                            TheData.SqlStatement = String.Format("INSERT INTO [dbo].[USERS_SECTION] ([UserID] ,[SectionID],[LinkType]) VALUES ('{0}','{1}', '{2}')", userID, dr["SectionID"], theType);
                            TheData.ExecuteInstruction();
                        }
                    }
                    theResult = true;
                }
                catch (Exception e)
                {

                    myMessage.viewMessage(MessageType.Error, "Error Saving Sections", resHarmonyPAS.systemName, "DL_Users", "SaveUserSections", e.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                    theResult = false;
                }
            }
            else theResult = true;

            return theResult;
        }

        public void Dispose()
        {
            if (TheData != null)
            {
                TheData.Dispose();
                TheData = null;
            }
        }
            
    }
}
