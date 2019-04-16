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

namespace Mineware.Systems.Production.SysAdminScreens.HoistingBooking
{
    class clsHoistingBookingData : clsBase
    {
        public DataTable GetMillMonth()
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

        public DataTable GetShaft()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select OreflowID OreflowID, OreflowID + ': ' + Name Name ");
                sb.AppendLine("from OreflowEntities ");
                sb.AppendLine("where OreflowCode = 'Shaft' ");
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

        public DataTable GetHoistingPlanningData(string _millMonth, string _shaft)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("select isnull(PlanTons,0) PlanTons,   isnull(PlanBeltGrade,0) PlanBeltGrade,   isnull(PlanGold,0) PlanGold ");
                sb.AppendLine("from PlanHoist ");
                sb.AppendLine("where MillMonth = '" + _millMonth + "' and ");
                sb.AppendLine("OreflowID = '" + _shaft + "' ");
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

        public DataTable GetHoistingBookingData(string _millMonth, string _shaft)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select distinct(cl.CalendarDate) CalendarDate, TotalShifts, ");
                sb.AppendLine("isnull(b.ReefTons, 0) ReefTons, ");
                sb.AppendLine("isnull(b.WasteTons, 0) WasteTons, ");
                sb.AppendLine("isnull(b.BeltGrade, 0) BeltGrade, ");
                sb.AppendLine("isnull(b.Gold, 0) Gold, ");
                sb.AppendLine("isnull(b.PlanTons, 0) PlanTons, ");
                sb.AppendLine("isnull(b.PlanBeltGrade, 0) PlanBeltGrade, ");
                sb.AppendLine("isnull(b.PlanGold, 0) PlanGold, WorkingDay, PlanTons * TotalShifts PlanMonthTons, ");
                sb.AppendLine("PlanBeltGrade PlanMonthBeltGrade, PlanGold * TotalShifts PlanMonthGold ");
                sb.AppendLine("from CODE_CALENDAR cc ");
                sb.AppendLine("inner join CALENDARMILL cm on ");
                sb.AppendLine("cc.CalendarCode = cm.CalendarCode and ");
                sb.AppendLine("cm.MillMonth = '" + _millMonth + "'   inner join CALTYPE cl on ");
                sb.AppendLine("cl.CalendarCode = cc.CalendarCode and ");
                sb.AppendLine("cl.CalendarDate >= cm.StartDate and ");
                sb.AppendLine("cl.CalendarDate <= cm.EndDate ");
                sb.AppendLine("left join BookingHoist b on ");
                sb.AppendLine("b.CalendarDate = cl.CalendarDate and b.millmonth = cm.millmonth and ");
                sb.AppendLine("b.OreflowID = '" + _shaft + "' ");
                sb.AppendLine("where cc.Description = 'Mill Calendar' and cl.Workingday = 'Y'");
                sb.AppendLine("order by cl.CalendarDate ");
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

        public void SaveData(string _millMonth, string _oreflowID, int _planTons,
                            decimal _planBeltGrade, int _planGold)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from PlanHoist");
                sb.AppendLine("where MillMonth = '" + _millMonth + "' and OreflowID = '" + _oreflowID + "'");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

                if (newData.ResultsDataTable.Rows.Count == 0)
                {
                    sb.Clear();
                    sb.AppendLine("insert into PlanHoist(MillMonth, OreflowID, PlanTons, PlanBeltGrade, PlanGold) values ");
                    sb.AppendLine("('" + _millMonth + "', '" + _oreflowID + "', '" + _planTons + "', '" + _planBeltGrade + "', '" + _planGold + "')");
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();
                }

                else if (newData.ResultsDataTable.Rows.Count != 0)
                {
                    sb.Clear();
                    sb.AppendLine("update PlanHoist ");
                    sb.AppendLine("set PlanTons = '" + _planTons + "', ");
                    sb.AppendLine("PlanBeltGrade = '" + _planBeltGrade + "' ,");
                    sb.AppendLine("PlanGold = '" + _planGold + "' ");
                    sb.AppendLine("where MillMonth = '" + _millMonth + "' and OreflowID = '" + _oreflowID + "'");
                    sb.AppendLine(" ");
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();
                }


                    theData.SqlStatement = "[sp_BOOKINGHoistingCycle]";
                    theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    SqlParameter[] _paramCollection9 =
                    {
                              theData.CreateParameter("@MillMonth", SqlDbType.Int ,6, _millMonth),
                              theData.CreateParameter("@Shaft", SqlDbType.VarChar,10, _oreflowID),
                              theData.CreateParameter("@PlanTons", SqlDbType.Int,10, _planTons),
                              theData.CreateParameter("@PlanBeltGrade", SqlDbType.Decimal,10, _planBeltGrade),
                              theData.CreateParameter("@PlanGold", SqlDbType.Int,10, _planGold),
                            };
                    theData.ParamCollection = _paramCollection9;
                    theData.ExecuteInstruction();

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Hoisting Booking Data was saved", Color.CornflowerBlue);
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            
        }

        public void SaveActualData(string _millMonth, string _shaft, DateTime _calendarDate,
                                        int _reefTons, decimal _wasteTons, decimal _beltGrade)
        {

            decimal _gold = Convert.ToDecimal((_beltGrade * _reefTons) / 1000);

            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from BOOKINGHoist  ");
                sb.AppendLine("where MillMonth = '" + _millMonth + "' and OreflowID = '" + _shaft + "' and CalendarDate = '" + _calendarDate + "'");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

                if (newData.ResultsDataTable.Rows.Count == 0)
                {
                    sb.Clear();
                    sb.AppendLine(" insert into BOOKINGHoist (MillMonth, OreflowID, CalendarDate, ReefTons, WasteTons, BeltGrade, Gold) values ");
                    sb.AppendLine("('" + _millMonth + "', '" + _shaft + "', '" + _calendarDate + "',");
                    sb.AppendLine("'" + _reefTons + "', '" + _wasteTons + "', '" + _beltGrade + "', '" + _gold + "') ");

                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Hoisting Booking Data was saved", Color.CornflowerBlue);
                }

                else if (newData.ResultsDataTable.Rows.Count != 0)
                {
                    sb.Clear();
                    sb.AppendLine(" update BOOKINGHoist set ");
                    sb.AppendLine(" ReefTons = '" + _reefTons + "', ");
                    sb.AppendLine(" WasteTons = '" + _wasteTons + "', ");
                    sb.AppendLine(" BeltGrade = '" + _beltGrade + "', ");
                    sb.AppendLine(" Gold = '" + _gold + "' ");
                    sb.AppendLine(" where MillMonth = '" + _millMonth + "' and OreflowID = '" + _shaft + "' and CalendarDate = '" + _calendarDate + "'");
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Hoisting Booking Data was saved", Color.CornflowerBlue);
                }
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
        }
    }
}
