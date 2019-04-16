using System;
using System.Data;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    class clsSystemSettings : clsBase
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        public DataTable getSysset()
        {
            bool HasError = false;
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "select *, convert(Varchar(10),RunDate,111) CALENDARDATE, \r\n " +
                        " convert(Varchar(10),RunDate,120) theRunDate \r\n " +
                        " from SYSSET s, Code_TopPanelsReport c";
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
        public Boolean updateBookingsSettings(string _AllowSWCWBook, string _SWCheck, string _CWCheck,
                                           string _BookFaceLength, string _DisableBookingCyclePlan, 
                                           string _ProblemNew, string _Problem_ProblemTypeLink,
                                           string _ProblemGroup_ProblemTypeLink, string _ProblemForceNote, 
                                           string _ProblemNewValidation, 
                                           string _SamplingValue, string _SamplingUseLatestForPlan, 
                                           int _ColorA, int _ColorB, int _ColorS,
                                           string _CheckMeas, string _PercBlastQual, string _CheckMeasLvl)
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "UPDATE SYSSET SET " +
                                            "AllowSWCWBook = '" + _AllowSWCWBook + "'" + ", \r\n  " +
                                            "SWCheck = '" + _SWCheck + "'" + ",  \r\n  " +
                                            "CWCheck = '" + _CWCheck + "'" + ", \r\n  " +
                                            "BookFL = '" + _BookFaceLength + "'" + ", \r\n  " +
                                            "DisableBookingCyclePlan = '" + _DisableBookingCyclePlan + "'" + ", \r\n  " +
                                            "ProblemNew = '" + _ProblemNew + "'" + ", \r\n  " +
                                            "Problem_ProblemTypeLink = '" + _Problem_ProblemTypeLink + "'" + ", \r\n  " +
                                            "ProblemGroup_ProblemTypeLink = '" + _ProblemGroup_ProblemTypeLink + "'" + ", \r\n  " +
                                            "ProblemForceNote = '" + _ProblemForceNote + "'" + ", \r\n  " +
                                            "ProblemNewValidation = '" + _ProblemNewValidation + "'" + ", \r\n  " +
                                            "SamplingValue = '" + _SamplingValue + "', \r\n  " +
                                            "SamplingUseLatestForPlan = '" + _SamplingUseLatestForPlan + "', \r\n  " +
                                            "A_Color = '"+ _ColorA + "', \r\n " +
                                            "B_Color = '"+ _ColorB + "', \r\n " +
                                            "S_Color = '"+ _ColorS + "', \r\n " +
                                            "CheckMeas = '"+ _CheckMeas + "', \r\n " +
                                            "PERCBLASTQUALIFICATION = '" + _PercBlastQual + "', \r\n " +
                                            "CheckMeasLvl = '" + _CheckMeasLvl + "' ";
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();
                return true;
            }
            catch (Exception _exception)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "ERROR SAVING Bookings Settings", resHarmonyPAS.systemTag, "clsSystemSettings", "updateBookingsSettings", _exception.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
        }
        public Boolean updateCurrentSettings(string _Maxadvance, string _Percblastqualification, string _Maxadvdev)
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "UPDATE SYSSET SET " +
                                       "Maxadvance = " + _Maxadvance + ", " +
                                       "Percblastqualification = " + _Percblastqualification + ", " +
                                       "Maxadvdev = " + _Maxadvdev;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();
                return true;
            }
            catch (Exception _exception)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "ERROR SAVING Current Settings", resHarmonyPAS.systemTag, "clsSystemSettings", "updateCurrentSettings", _exception.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
        }
        public Boolean updateDatesSettings(string _Rundate, string _Currentproductionmonth, string _Currentmillmonth, string _Finyearstart, string _Finyearend)
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "UPDATE SYSSET SET " +
                                       "Rundate = '" + _Rundate + "', " +
                                       "Currentproductionmonth = " + _Currentproductionmonth + ", " +
                                       "Currentmillmonth = " + _Currentmillmonth + ", " +
                                       "Finyearstart = " + _Finyearstart + ", " +
                                       "Finyearend = " + _Finyearend;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();
                return true;
            }
            catch (Exception _exception)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "ERROR SAVING Dates Settings", resHarmonyPAS.systemTag, "clsSystemSettings", "updateDatesSettings", _exception.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
        }
        public Boolean updateFactorsSettings(string _Rockdensity, string _Brokenrockdensity)
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "UPDATE SYSSET SET " +
                                       "Rockdensity = " + _Rockdensity + ", " +
                                       "Brokenrockdensity = " + _Brokenrockdensity;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();
                return true;
            }
            catch (Exception _exception)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "ERROR SAVING Factors Settings", resHarmonyPAS.systemTag, "clsSystemSettings", "updateFactorsSettings", _exception.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
        }
        public Boolean updateInterfaceSettings(string _Bomfile, string _Calendarfile, string _Sectionfile, string _Plandir, string _Bookdir, string _Costfile, string _Headerfile, string _Cpmpath)
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "UPDATE SYSSET SET " +
                                           "Bomfile = '" + _Bomfile + "'" + ", " +
                                           "Calendarfile = '" + _Calendarfile + "'" + ", " +
                                           "Sectionfile = '" + _Sectionfile + "'" + ", " +
                                           "Plandir = '" + _Plandir + "'" + ", " +
                                           "Bookdir = '" + _Bookdir + "'" + ", " +
                                           "Costfile = '" + _Costfile + "'" + ", " +
                                           "Headerfile = '" + _Headerfile + "'" + ", " +
                                           "Cpmpath = '" + _Cpmpath + "'";
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();
                return true;
            }
            catch (Exception _exception)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "ERROR SAVING Interface Settings", resHarmonyPAS.systemTag, "clsSystemSettings", "updateInterfaceSettings", _exception.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
        }
        public Boolean updateMinesSettings(string _Mine, string _mohierarchicalid, string _Engcaptlevel, string _Engtoprodlink, string _Engbackdatedays)
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = " UPDATE SYSSET SET " +
                                        " Banner = '" + _Mine + "'" + ", " +
                                        " mohierarchicalid = " + _mohierarchicalid + ", " +
                                        " Engcaptlevel = " + _Engcaptlevel + ", " +
                                        " Engtoprodlink = " + _Engtoprodlink + ", " +
                                        " Engbackdatedays = " + _Engbackdatedays;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();
                return true;
            }
            catch (Exception _exception)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "ERROR SAVING Mines Settings", resHarmonyPAS.systemTag, "clsSystemSettings", "updateMinesSettings", _exception.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
        }

        public Boolean updateTopPanelsSettings(int _NoTopPanels)
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = " UPDATE Code_TopPanelsReport SET " +
                                        " NoOfPanels = '" + _NoTopPanels + "' " +                                    
                                        " ";
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();
                return true;
            }
            catch (Exception _exception)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "ERROR SAVING Mines Settings", resHarmonyPAS.systemTag, "clsSystemSettings", "updateMinesSettings", _exception.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
        }
    }
}
