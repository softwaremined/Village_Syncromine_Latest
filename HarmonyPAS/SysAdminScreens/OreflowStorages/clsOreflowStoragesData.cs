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

namespace Mineware.Systems.Production.SysAdminScreens.OreflowStorages
{
    class clsOreflowStoragesData : clsBase
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

        public DataTable GetSurveyStoragesData(string _prodMonth, string _mill)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from SURVEY_STORAGES ");
                sb.AppendLine("where ProdMonth = '" + _prodMonth + "' and OreFlowID = '" + _mill + "'");
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

        public DataTable GetPlanningStoragesData(string _prodMonth, string _mill)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from PLANNING_STORAGES ");
                sb.AppendLine("where ProdMonth = '" + _prodMonth + "' and OreFlowID = '" + _mill + "'");
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

        public DataTable GetBusinessPlanningStoragesData(string _prodMonth, string _mill)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select * from BUS_PLANNING_STORAGES ");
                sb.AppendLine("where ProdMonth = '" + _prodMonth + "' and OreFlowID = '" + _mill + "'");
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

        public void SaveSurveyStoragesData(string _millMonth, string _oreflowID, int _shaftStoragesBeginTons,
                                                    int _shaftStoragesBeginContent, int _shaftStoragesEndTons,
                                                    int _shaftStoragesEndContent, int _stockPilesBeginTons,
                                                    int _stockPilesBeginContent, int _stockPilesEndTons,
                                                    int _stockPilesEndContent, int _stockpileAtPlantBeginTons,
                                                    int _stockpileAtPlantBeginContent, int _stockpileAtPlantEndTons,
                                                    int _stockpileAtPlantEndContent, int _railwayBinsBeginTons,
                                                    int _railwayBinsBeginContent, int _railwayBinsEndTons,
                                                    int _railwayBinsEndContent, int _millBinsBeginTons,
                                                    int _millBinsBeginContent, int _millBinsEndTons,
                                                    int _millBinsEndContent, int _sludgeTons,
                                                    int _sludgeContent, int _surfaceSourceTons, int _surfaceSourceContent,
                                                    int _wasteDumpsTons, int _wasteDumpsContent,
                                                    int _slimesDamsTons, int _slimesDamsContent,
                                                    int _reefExSortingTons, int _reefExSortingContent,
                                                    int _wasteWashingsTons, int _wasteWashingsContent,
                                                    int _flushingTonsTons, int _flushingTonsContent,
                                                    int _addSourcesToMillTons, int _addSourcesToMillContent,
                                                    int _totalTonsMilledTons, int _totalTonsMilledContent,
                                                    int _goldRecovered, int _reefBallastReclaimedTons,
                                                    int _reefBallastReclaimedContent, int _residue,
                                                    int _beltTonsWeighted, decimal _beltValue, string _uGTransfer, int _uGTransferTons, int _uGTransferContent)
        {
            try
            {
                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.SqlStatement = " delete from SURVEY_STORAGES where ProdMonth = '"+ _millMonth + "' and OreFlowID = '" + _oreflowID + "' \n" +
                                           " insert into SURVEY_STORAGES values (\n" +
                                           " '" + _millMonth  + "', '" + _oreflowID + "', " + _shaftStoragesBeginTons + ", \n" +
                                           " " + _shaftStoragesBeginContent + ", " + _shaftStoragesEndTons + ", \n" +
                                           " " + _shaftStoragesEndContent + ", " + _stockPilesBeginTons + ", \n" +
                                           " " + _stockPilesBeginContent + ", " + _stockPilesEndTons + ", \n" +
                                           " " + _stockPilesEndContent + ", " + _stockpileAtPlantBeginTons + ", \n" +
                                           " " + _stockpileAtPlantBeginContent + ", " + _stockpileAtPlantEndTons + ", \n" +
                                           " " + _stockpileAtPlantEndContent + ", " + _railwayBinsBeginTons + ", \n" +
                                           " " + _railwayBinsBeginContent + ", " + _railwayBinsEndTons + ", \n" +
                                           " " + _railwayBinsEndContent + ", " + _millBinsBeginTons + ", \n" +
                                           " " + _millBinsBeginContent + ", " + _millBinsEndTons + ", \n" +
                                           " " + _millBinsEndContent + ", " + _sludgeTons + ", \n" +
                                           " " + _sludgeContent + ", " + _surfaceSourceTons + ", "+ _surfaceSourceContent +", \n" +
                                           " " + _wasteDumpsTons + ", " + _wasteDumpsContent + ", \n" +
                                           " " + _slimesDamsTons + ", " + _slimesDamsContent + ", \n" +
                                           " " + _reefExSortingTons + ", " + _reefExSortingContent + ", \n" +
                                           " " + _wasteWashingsTons + ", " + _wasteWashingsContent + ", \n" +
                                           " " + _flushingTonsTons + ", " + _flushingTonsContent + ", \n" +
                                           " " + _addSourcesToMillTons + ", " + _addSourcesToMillContent + ", \n" +
                                           " " + _totalTonsMilledTons + ", " + _totalTonsMilledContent + ", \n" +
                                           " " + _goldRecovered + ", " + _reefBallastReclaimedTons + ", \n" +
                                           " " + _reefBallastReclaimedContent + ", " + _residue + ", \n" +
                                           " " + _beltTonsWeighted + ", " + _beltValue + ", \n" +
                                           " '" + _uGTransfer + "', " + _uGTransferTons + ", " + _uGTransferContent + " ) ";

                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                    theData.ExecuteInstruction();

                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Oreflow Storages Data was saved", Color.CornflowerBlue);
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
        }


        public void SavePlanningStoragesData(string _millMonth, int _residue,
                                            int _flushingTons, int _flushingContent, string _oreflowID,
                                            int _waste, int _discrepency, int _mcf,
                                            int _prf)
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = " delete from PLANNING_STORAGES where ProdMonth = '" + _millMonth + "' and OreFlowID = '" + _oreflowID + "' \n" +
                                       " insert into PLANNING_STORAGES values (\n" +
                                       " '" + _millMonth + "', '" + _residue + "', " + _flushingTons + ", \n" +
                                       " " + _flushingContent + ", '" + _oreflowID + "', \n" +
                                       " " + _waste + ", " + _discrepency + ", \n" +
                                       " " + _mcf + ", " + _prf + ") ";

                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Oreflow Storages Data was saved", Color.CornflowerBlue);
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
        }

        public void SaveBusinessPlanningStoragesData(string _millMonth, int _residue,
                                    int _flushingTons, int _flushingContent, string _oreflowID,
                                    int _waste, int _discrepency, int _mcf,
                                    int _prf)
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.SqlStatement = " delete from BUS_PLANNING_STORAGES where ProdMonth = '" + _millMonth + "' and OreFlowID = '" + _oreflowID + "' \n" +
                                       " insert into BUS_PLANNING_STORAGES values (\n" +
                                       " '" + _millMonth + "', '" + _residue + "', " + _flushingTons + ", \n" +
                                       " " + _flushingContent + ", '" + _oreflowID + "', \n" +
                                       " " + _waste + ", " + _discrepency + ", \n" +
                                       " " + _mcf + ", " + _prf + ") ";

                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                theData.ExecuteInstruction();

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Oreflow Storages Data was saved", Color.CornflowerBlue);
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
        }
    }
}
