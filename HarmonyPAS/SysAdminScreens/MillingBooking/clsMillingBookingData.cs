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

namespace Mineware.Systems.Production.SysAdminScreens.MillingBooking
{
    class clsMillingBookingData : clsBase
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

        public DataTable GetMill()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select OreflowID OreflowID, OreflowID + ': ' + Name Name ");
                sb.AppendLine("from OreflowEntities ");
                sb.AppendLine("where OreflowCode = 'Mill'");
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

        public DataTable GetMillingPlanningData(string _millMonth, string _oreflowID)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("select isnull(TonsTreated,0) TonsTreated, isnull(TonsToPlant,0) TonsToPlant ");
                sb.AppendLine("from PLANMilling ");
                sb.AppendLine("where MillMonth = '" + _millMonth + "' and ");
                sb.AppendLine("OreflowID = '" + _oreflowID + "' ");
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

        public DataTable GetMillingData(string _millMonth, string _mill)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("select distinct(cl.CalendarDate) CalendarDate, TotalShifts, ");
                sb.AppendLine("Treated = isnull(convert(numeric(13,0),b.TonsTreated), 0), ");
                sb.AppendLine("ToPlant = isnull(convert(numeric(13,0),b.TonsToPlant), 0), WorkingDay, ");
                sb.AppendLine("PlannedTonsTreated = isnull(convert(numeric(13,0),b.PlannedTonsTreated), 0),  ");
                sb.AppendLine("PlannedTonsToPlant = isnull(convert(numeric(13,0),b.PlannedTonsToPlant), 0)");
                sb.AppendLine("from CODE_CALENDAR cc ");
                sb.AppendLine("inner join CALENDARMILL cm on ");
                sb.AppendLine("cc.CalendarCode = cm.CalendarCode and  ");
                sb.AppendLine("cm.MillMonth = '" + _millMonth + "' ");
                sb.AppendLine("inner join CALTYPE cl on ");
                sb.AppendLine("cl.CalendarCode = cc.CalendarCode and  ");
                sb.AppendLine("cl.CalendarDate >= cm.StartDate and  ");
                sb.AppendLine("cl.CalendarDate <= cm.EndDate ");
                sb.AppendLine("left join BookingMilling b on  ");
                sb.AppendLine("b.CalendarDate = cl.CalendarDate and ");
                sb.AppendLine("b.OreflowID = '" + _mill + "' and b.MillMonth = '" + _millMonth + "' ");
                sb.AppendLine("where cc.Description = 'Mill Calendar' and cl.Workingday = 'Y'");
                sb.AppendLine("order by cl.CalendarDate");
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

        public void SavePlannedData(string _millMonth, string _oreflowID, int _tonsTreated, int _tonsToPlant)
        {
            clsDataAccess newData = new clsDataAccess();

            try
            {
                sb.Clear();
                sb.AppendLine("select * from PLANMilling");
                sb.AppendLine("where MillMonth = '" + _millMonth + "' and OreflowID = '" + _oreflowID + "'");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

                if (newData.ResultsDataTable.Rows.Count == 0)
                {
                    sb.Clear();
                    sb.AppendLine("insert into PLANMilling(MillMonth, OreflowID, TonsTreated, TonsToPlant) values ");
                    sb.AppendLine("('" + _millMonth + "', '" + _oreflowID + "', '" + _tonsTreated + "', '" + _tonsToPlant + "')");
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Milling Booking Data was saved", Color.CornflowerBlue);
                }

                else if (newData.ResultsDataTable.Rows.Count != 0)
                {
                    sb.Clear();
                    sb.AppendLine("update PLANMilling ");
                    sb.AppendLine("set TonsTreated = '" + _tonsTreated + "', ");
                    sb.AppendLine("TonsToPlant = '" + _tonsToPlant + "'");
                    sb.AppendLine("where MillMonth = '" + _millMonth + "' and OreflowID = '" + _oreflowID + "'");
                    sb.AppendLine(" ");
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();
                }

                theData.SqlStatement = "[sp_BOOKINGMillingCycle]";
                theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                SqlParameter[] _paramCollection9 =
                {
                              theData.CreateParameter("@MillMonth", SqlDbType.Int ,6, _millMonth),
                              theData.CreateParameter("@OreflowID", SqlDbType.VarChar,10, _oreflowID),
                              theData.CreateParameter("@TonsTreated", SqlDbType.Int,10, _tonsTreated),
                              theData.CreateParameter("@TonsToPlant", SqlDbType.Int,10, _tonsToPlant),
                            };
                theData.ParamCollection = _paramCollection9;
                theData.ExecuteInstruction();

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Milling Booking Data was saved", Color.CornflowerBlue);

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
        }

        public void SaveActualData(string _millMonth, string _oreflowID, DateTime _calendarDate, decimal _actualTonsTreated, decimal _actualTonsToPlant)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from BOOKINGMilling  ");
                sb.AppendLine("where MillMonth = '" + _millMonth + "' and OreflowID = '" + _oreflowID + "' and CalendarDate = '" + _calendarDate + "'");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

                if (newData.ResultsDataTable.Rows.Count == 0)
                {
                    sb.Clear();
                    sb.AppendLine(" insert into BOOKINGMilling (MillMonth, OreflowID, CalendarDate, TonsTreated, TonsToPlant) values ");
                    sb.AppendLine("('" + _millMonth + "', '" + _oreflowID + "', '" + _calendarDate + "'");
                    sb.AppendLine("'" + _actualTonsTreated + "', '" + _actualTonsToPlant + "') ");

                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Milling Booking Data was saved", Color.CornflowerBlue);
                }

                else if (newData.ResultsDataTable.Rows.Count != 0)
                {
                    sb.Clear();
                    sb.AppendLine(" update BOOKINGMilling set ");
                    sb.AppendLine(" TonsTreated = '" + _actualTonsTreated + "', ");
                    sb.AppendLine(" TonsToPlant = '" + _actualTonsToPlant + "' ");
                    sb.AppendLine(" where MillMonth = '" + _millMonth + "' and OreflowID = '" + _oreflowID + "' and CalendarDate = '" + _calendarDate + "'");
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();

                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Milling Booking Data was saved", Color.CornflowerBlue);
                }
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
        }
    }
}
