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

namespace Mineware.Systems.Production.SysAdminScreens.BonusCapture
{
    class clsBonusCaptureData : clsBase
    {

        public bool locked;

        public DataTable GetProdMonth()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select CurrentProductionMonth MillMonth from SYSSET");
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

        public DataTable GetActivity()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from CODE_ACTIVITY where activity in(0,1)");
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

        public DataTable GetSections(string _prodMonth)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("Select SectionID, Name ");
                sb.AppendLine("from Section s where s.Prodmonth = '" + _prodMonth + "' ");
                sb.AppendLine("and Hierarchicalid  = 4 ");
                sb.AppendLine("order by Hierarchicalid, SECTIONid asc  ");
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

        public DataTable GetStopingData(string _prodMonth, string _activity, string _section)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("select ''Status,divisioncode ExeName, 'xxx' Costareaid, W.Workplaceid, ");
                sb.AppendLine("w.Description, s.Prodmonth,cs.StopeTypeDesc Type, 'KR' Type2, ");
                sb.AppendLine("s.LedgeSqm Lsqm, s.StopeSqm SSQM, s.LedgeSqmOSF LOSF, s.StopeSqmOSF SOSF, ");
                sb.AppendLine("s.LedgeSqmOS LOS, s.StopeSqmOS SOS, s.FLTotal Facelength, ");
                sb.AppendLine("s.LedgeFL LFL, s.StopeFL FFL, s.FLOSTotal OSFL, s.CleanSqm SQMClean, ");
                sb.AppendLine("s.CleanDist DistFromFace, s.CW ChnWidth, s.FW FWWidth, s.HW HWWidth, ");
                sb.AppendLine("s.SWSQM StpWidth, s.CrewMorning, s.CrewAfternoon, s.CrewEvening, ");
                sb.AppendLine("'Miner' Hierarchicalname, sc.name, ");
                sb.AppendLine("case when M2NOTTOBEPAIDFORMINER is null then 0 else M2NOTTOBEPAIDFORMINER end M2NOTTOBEPAIDFORMINER, ");
                sb.AppendLine("case when MINSPRETARGETSW2 is null then 0 else MINSPRETARGETSW2 end MINSPRETARGETSW2, ");
                sb.AppendLine("case when MidmonthIndicator is null then 0 else  MidmonthIndicator end MidmonthIndicator, ");
                sb.AppendLine("case when DISTFROMFACEMIDMONTH is null then 0 else DISTFROMFACEMIDMONTH end DISTFROMFACEMIDMONTH, ");
                sb.AppendLine("case when SweepingPenalty is null then 0 else SweepingPenalty end SweepingPenalty, SF1SF2 ");
                sb.AppendLine("from survey s inner join workplace w on w.WorkplaceID  = s.workplaceid ");
                sb.AppendLine("inner join Code_StopeTypes cs on cs.StopeTypeID = s.SType ");
                sb.AppendLine("inner join SECTION_COMPLETE sc on s.prodmonth = sc.prodmonth and s.sectionid = sc.sectionid ");
                sb.AppendLine("where s.activity = '" + _activity + "' and s.Prodmonth ='" + _prodMonth + "' and sc.Sectionid_2='" + _section + "'");
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

        public DataTable GetDevelopmentData(string _prodMonth, string _activity, string _section)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select ''Status,divisioncode ExeName, 'xxx' Costareaid,s.Prodmonth, W.Workplaceid, ");
                sb.AppendLine("w.Description, s.TotalMetres Adv_Total, s.MeasWidth, s.MeasHeight, ");
                sb.AppendLine("s.CubicsMetres [Extra Units], 'Miner' Hierarchicalname, sc.Name, s.CrewMorning, ");
                sb.AppendLine("e.Description Description2, s.cw, ");
                sb.AppendLine("case when MetersNotToBePaid is NULL then 0 else MetersNotToBePaid end MetersNotToBePaid, ");
                sb.AppendLine("CleaningCrew,TrammingCrew,HlgeMaintainanceCrew,RiggerCrew,RseCleaningCrew ");
                sb.AppendLine("from survey s inner join workplace w on w.WorkplaceID  = s.workplaceid ");
                sb.AppendLine("inner join SECTION_COMPLETE sc ");
                sb.AppendLine("on s.prodmonth = sc.prodmonth ");
                sb.AppendLine("and s.sectionid = sc.sectionid inner join endtype e on e.EndTypeID = w.EndTypeID ");
                sb.AppendLine("where  s.activity = '" + _activity + "' and s.Prodmonth ='" + _prodMonth + "' and sc.Sectionid_2='" + _section + "'");
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

        public void SaveStopingData(DataTable StopingData, string _prodMonth, string _activity, string _section)
        {
            try
            {
                foreach (DataRow r in StopingData.Rows)
                {
                    if (r["Status"].ToString() == "1")
                    {
                        theData.SqlStatement += " update s set M2NOTTOBEPAIDFORMINER= " + Convert.ToDecimal(r["M2NOTTOBEPAIDFORMINER"]) + " , " +
                                               " MINSPRETARGETSW2= " + Convert.ToDecimal(r["MINSPRETARGETSW2"]) + ", " +
                                               " MidmonthIndicator= " + Convert.ToDecimal(r["MidmonthIndicator"]) + ", " +
                                               " DISTFROMFACEMIDMONTH= " + Convert.ToDecimal(r["DISTFROMFACEMIDMONTH"]) + ", " +
                                               " SweepingPenalty= " + Convert.ToDecimal(r["SweepingPenalty"]) + ", SF1SF2= '" + r["SF1SF2"].ToString() + "' " +
                                               " from survey s inner join SECTION_COMPLETE sc " +
                                               " on s.prodmonth = sc.prodmonth and s.sectionid = sc.sectionid " +
                                               " inner join workplace w on w.WorkplaceID  = s.workplaceid " +
                                               " inner join Code_StopeTypes cs on cs.StopeTypeID = s.SType  " +
                                               " where s.WorkplaceID = '" + r["Workplaceid"] + "' and s.Activity ='" + _activity + "' and s.ProdMonth ='" + _prodMonth + "' " +
                                               " and cs.StopeTypeDesc='" + r["Type"].ToString() + "'";
                    }
                }

                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Stoping Data has been Saved", Color.CornflowerBlue);

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
        }

        public void SaveDevelopmentData(DataTable DevelopmentData, string _prodMonth, string _activity, string _section)
        {
            try
            {
                foreach (DataRow r in DevelopmentData.Rows)
                {
                    if (r["Status"].ToString() == "1")
                    {
                        theData.SqlStatement += " update s set MetersNotToBePaid= " + Convert.ToDecimal(r["MetersNotToBePaid"]) + " "+
                                                " from survey s " +
                                                " inner join SECTION_COMPLETE sc " +
                                                " on s.prodmonth = sc.prodmonth and s.sectionid = sc.sectionid " +
                                                " inner join workplace w on w.WorkplaceID  = s.workplaceid " +
                                                " inner join endtype e on e.EndTypeID = w.EndTypeID " +
                                                " where s.WorkplaceID = '" + r["Workplaceid"] + "' and s.Activity ='" + _activity + "' " +
                                                " and s.ProdMonth ='"+ _prodMonth +"'";
                    }
                }

                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Development Data has been Saved", Color.CornflowerBlue);

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
        }
    }
}
