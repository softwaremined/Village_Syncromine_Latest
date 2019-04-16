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

namespace Mineware.Systems.Production.SysAdminScreens.TopPanels
{
    class clsTopPanelsData : clsBase
    {
        public DataTable GetProdMonth()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select CurrentProductionMonth Prodmonth from SYSSET");
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

        public DataTable GetSection(string _prodMonth)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select a.* from ");
                sb.AppendLine("(select distinct(SectionId_2) SectionID, SectionId_2 + ': ' + Name_2 Name ");
                sb.AppendLine("from [Section_Complete] ");
                sb.AppendLine("where ProdMonth = '" + _prodMonth + "'");
                sb.AppendLine("union ");
                sb.AppendLine("select distinct(SectionId_5) SectionID, SectionId_5 + ': ' + Name_5 Name ");
                sb.AppendLine("from [Section_Complete] ");
                sb.AppendLine("where ProdMonth = '" + _prodMonth + "') a ");
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

        public DataTable GetTopPanelsData(string _prodMonth, string _sectionID)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("SELECT distinct a.Workplaceid WPID,Description Workplace ,  ");
                sb.AppendLine("convert(numeric(13,0),isnull(CMGT,0)) CMGT, ");
                sb.AppendLine("convert(numeric(13,3),isnull(Kg,0)) KG,  ");
                sb.AppendLine("case when c.WorkplaceID is null then Cast(0 as Bit) else Cast(1 as Bit) end as 'Selected', ");
                sb.AppendLine("Activity = Convert(numeric(7,0), a.Activity) ");
                sb.AppendLine("FROM PLANMONTH a  ");
                sb.AppendLine("inner join SECTION_COMPLETE sc on  ");
                sb.AppendLine("sc.Prodmonth = a.Prodmonth and  ");
                sb.AppendLine("sc.SectionID = a.Sectionid  ");
                sb.AppendLine("inner join WORKPLACE b  ");
                sb.AppendLine("on a.Workplaceid = b.WorkplaceID  ");
                sb.AppendLine("left join TopPanelsSelected c on  ");
                sb.AppendLine("a.Prodmonth = c.Prodmonth and ");
                sb.AppendLine("a.Workplaceid = c.WorkplaceID and  ");
                sb.AppendLine("c.SectionID = '" + _sectionID + "'  ");
                sb.AppendLine("where a.Prodmonth = '" + _prodMonth + "'  AND Plancode = 'MP'  and");
                if (_sectionID == "GM")
                {
                    sb.AppendLine("sc.Sectionid_5 = '" + _sectionID + "'");
                }

                else
                {
                    sb.AppendLine("sc.Sectionid_2 = '" + _sectionID + "'  ");
                }
                
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

        public void SaveData(bool _selected, string _prodMonth, string _sectionID, string _workplaceID, string _activity)
        {
            if (_selected == true)
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "delete from [dbo].[TopPanelsSelected] \n" +
                                       "where Prodmonth = '" + _prodMonth + "' and SectionID = '" + _sectionID + "' and WorkplaceID = '" + _workplaceID + "' \n" +
                                       "INSERT INTO [dbo].[TopPanelsSelected] " +
                                       "(ProdMonth, SectionID, WorkPlaceID, Activity) \n" +
                                       "Values ( \n" +
                                       " '" + _prodMonth + "', '" + _sectionID + "', \n" +
                                       " '" + _workplaceID + "', '" + _activity + "') ";

                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Top Panels Data was saved", Color.CornflowerBlue);
            }

            if (_selected == false)
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = "delete from [dbo].[TopPanelsSelected] \n" +
                                       "where Prodmonth = '" + _prodMonth + "' and SectionID = '" + _sectionID + "' and WorkplaceID = '" + _workplaceID + "' \n";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Top Panels Data was saved", Color.CornflowerBlue);
            }
        }
    }
}
