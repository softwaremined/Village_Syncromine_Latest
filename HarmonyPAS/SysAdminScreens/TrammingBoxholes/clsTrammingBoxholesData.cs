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


namespace Mineware.Systems.Production.SysAdminScreens.TrammingBoxholes
{
    class clsTrammingBoxholesData : clsBase
    {
    
        public void testerfunction(object incoming, EventArgs e) {
           // Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("narra", "narra narranarra5", Color.Red);
            //ucTrammingBoxholes _ucc = new ucTrammingBoxholes();

           
        }

        public DataTable LoadBoxholes()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("select o.OreFlowID, o.OreFlowCode, o.Name Name,isnull(o.LevelNumber, '') LevelNumber, ");
                sb.AppendLine("o.Inactive, o.SectionID, o.ReefType, isnull(o.BoxDistance, 0.0) BoxDistance, ");
                sb.AppendLine("oe.name lvl, Status = '' ");
                sb.AppendLine("from dbo.OREFLOWENTITIES o");
                sb.AppendLine("left outer join oreflowentities oe");
                sb.AppendLine("on  o.parentoreflowid = oe.oreflowid where o.oreflowcode = 'BH'");
                sb.AppendLine("order by o.oreflowid");
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

        public DataTable LoadLevels()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select Distinct OreflowID, LevelNumber from Oreflowentities ");
                sb.AppendLine(" where OreflowCode = 'Lvl'  ");
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

        public DataTable LoadMONames()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select distinct Name from SECTION ");
                sb.AppendLine("where Hierarchicalid = 4");
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

        public DataTable LoadReefTypes()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select ReefID, Description from Reef where selected = 1 ");
                sb.AppendLine(" ");
                sb.AppendLine(" ");
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


        public DataTable LoadBHID()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("  select 'BH'+Convert(Varchar(50),max(replace(replace(Oreflowid,'BH', ''),'B',''))+1) BHID from Oreflowentities where OreflowCode = 'BH' ");
                sb.AppendLine(" ");
                sb.AppendLine(" ");
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

        public DataTable LoadSections()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select SectionID,Name");
                sb.AppendLine("from SECTION ");
                sb.AppendLine("where Prodmonth = (select CurrentProductionMonth from SYSSET)");
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

        public DataTable LoadInact()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select Distinct Inactive  ");
                sb.AppendLine("from OREFLOWENTITIES ");
                sb.AppendLine("where Inactive != '' ");
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

        public DataTable GetLevel(string _oreflowID)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select distinct oe.name lvl ");
                sb.AppendLine("from dbo.OREFLOWENTITIES o ");
                sb.AppendLine("left outer join oreflowentities oe ");
                sb.AppendLine("on  o.parentoreflowid = oe.oreflowid ");
                sb.AppendLine("where o.oreflowcode = 'BH' and o.OreFlowID = '" + _oreflowID + "'");
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

        public DataTable GetInAct(string _oreflowID)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select o.Inactive ");
                sb.AppendLine("from dbo.OREFLOWENTITIES o ");
                sb.AppendLine("left outer join oreflowentities oe ");
                sb.AppendLine("on  o.parentoreflowid = oe.oreflowid ");
                sb.AppendLine("where o.oreflowcode = 'BH' and o.OreFlowID = '" + _oreflowID + "'");
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

        public DataTable GetSectionID(string _oreflowID)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select o.SectionID ");
                sb.AppendLine("from dbo.OREFLOWENTITIES o ");
                sb.AppendLine("left outer join oreflowentities oe ");
                sb.AppendLine("on  o.parentoreflowid = oe.oreflowid ");
                sb.AppendLine("where o.oreflowcode = 'BH' and o.OreFlowID = '" + _oreflowID + "'");
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

        public DataTable GetReefType(string _oreflowID)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select o.ReefType ");
                sb.AppendLine("from dbo.OREFLOWENTITIES o ");
                sb.AppendLine("left outer join oreflowentities oe ");
                sb.AppendLine("on  o.parentoreflowid = oe.oreflowid ");
                sb.AppendLine("where o.oreflowcode = 'BH' and o.OreFlowID = '" + _oreflowID + "'");
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

        public void SaveData(string _boxholeID, string _description, string _level,
            int _reefType, string _inAct, string _sectionID, decimal _distance, string action)
        {
            try
            {
                if (action == "Add")
                {
                    if (_boxholeID != "" && _description != "")
                    {
                        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        theData.SqlStatement = " insert into OREFLOWENTITIES (OreFlowID, OreFlowCode, Name, LevelNumber, Capacity, \n" +
                                               "Cur_Tons, Cur_Grade, X1, Y1, Inactive, TFBox, SectionID, ReefType, BoxDistance) values \n" +
                                               "('" + _boxholeID + "', 'BH', '" + _description + "', \n" +
                                               "isnull('" + _level + "', ''), 0, 0, 0, 20, 140, \n" +
                                               "'" + _inAct + "', 'N', '" + _sectionID + "', \n" +
                                               "'" + _reefType + "', '" + Convert.ToDecimal(_distance) + "') ";

                        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();
                    }

                    else
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields cannot be blank", Color.Red);
                    }

                }

                else if (action == "Edit")
                {
                    if (_boxholeID != "" && _description != "")
                    {
                        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        theData.SqlStatement = " update OREFLOWENTITIES \n" +
                                               " set Name = '" + _description + "', \n" +
                                               " LevelNumber = '" + _level + "', \n" +
                                               " ReefType = '" + _reefType + "', \n" +
                                               " Inactive = '" + _inAct + "', \n" +
                                               " SectionID = '" + _sectionID + "', \n" +
                                               " BoxDistance = '" + _distance + "' \n" +
                                               " where OreFlowID = '" + _boxholeID + "'";

                        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();

                        
                    }

                    else
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields cannot be blank", Color.Red);
                    }
                   
                }
                

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }

        }
    }
}
