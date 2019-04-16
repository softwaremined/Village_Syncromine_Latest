using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System.Drawing;
using MWDataManager;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.TrammingPlanning
{
    class clsTrammingPlanningData : clsBase
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

        public DataTable GetSections(string _prodmonth)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("Select distinct b.SectionID_2 SectionID, b.SectionID_2 + ' : ' + Name_2 Name ");
                sb.AppendLine("from PLANMONTH a ");
                sb.AppendLine("inner join SECTION_COMPLETE b on ");
                sb.AppendLine("a.PRODMONTH = b.PRODMONTH and ");
                sb.AppendLine("a.SECTIONID = b.SECTIONID ");
                sb.AppendLine("where a.PRODMONTH = '" + _prodmonth + "' ");
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

        public DataTable GetTrammingPlanningData(string Prodmonth, string SectionID)
        {
            try
            {
                theData.SqlStatement = "SP_LoadTramming_Planning ";

                SqlParameter[] _paramCollection =
                        {
                            theData.CreateParameter("@Prodmonth", SqlDbType.Decimal, 0, Prodmonth),
                            theData.CreateParameter("@SectionID", SqlDbType.VarChar, 0, SectionID),
                        };

                theData.ParamCollection = _paramCollection;
                theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();


            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }
            return theData.ResultsDataTable;
        }

        public DataTable GetBoxHole(DataTable thePlanningData)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                foreach (DataRow dr in thePlanningData.Rows)
                {
                    sb.Clear();
                    sb.AppendLine("select o.oreflowid BoxHoleID, o.name BH from workplace w ");
                    sb.AppendLine("inner join oreflowentities o on o.parentoreflowid = w.oreflowid ");
                    sb.AppendLine("where [description] = '" + dr["WorkplaceDesc"] + "' and o.oreflowcode = 'BH'");
                    newData.ConnectionString = theData.ConnectionString;
                    newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();
                }


            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return newData.ResultsDataTable;
        }

        public string GetBoxHoleID(DataTable thePlanningData, string _boxhole)
        {
            string _boxHoleID = "";

            try
            {
                foreach (DataRow dr in thePlanningData.Rows)
                {
                    theData.SqlStatement = "select max(OreFlowID) BoxHoleID from oreflowentities where Name = '" + _boxhole + "'";

                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    theData.ExecuteInstruction();

                    DataTable dAct = theData.ResultsDataTable;

                    if (dAct.Rows.Count != 0)
                    {
                        _boxHoleID = dAct.Rows[0]["BoxHoleID"].ToString();
                    }

                }


            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            return _boxHoleID;
        }

        public Boolean SaveTrammingPlanning(string Prodmonth, DataTable TheDatatable)
        {
            int _Act = 0;
            bool HasError = false;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            try
            {
                foreach (DataRow dr in TheDatatable.Rows)
                {
                    if (dr.RowState == DataRowState.Modified)
                    {
                        if (dr["Activity"].ToString() == "Stoping")
                        {
                            _Act = 0;
                        }

                        else if (dr["Activity"].ToString() == "Development")
                        {
                            _Act = 1;
                        }

                        else if (dr["Activity"].ToString() == "Stoping Cubics")
                        {
                            _Act = 3;
                        }

                        else if (dr["Activity"].ToString() == "Backfill")
                        {
                            _Act = 5;
                        }

                        else if (dr["Activity"].ToString() == "Development Cubics")
                        {
                            _Act = 7;
                        }

                        else if (dr["Activity"].ToString() == "Sweeping")
                        {
                            _Act = 8;
                        }

                        else if (dr["Activity"].ToString() == "Vamping")
                        {
                            _Act = 9;
                        }

                        else if (dr["Activity"].ToString() == "Surface")
                        {
                            _Act = 10;
                        }

                        else if (dr["Activity"].ToString() == "Other Under Ground")
                        {
                            _Act = 11;
                        }
                        if (dr["TheTypePlanning"].ToString() == "Normal")
                        {
                            theData.SqlStatement = "UPDATE PLANMONTH SET " +
                                                    "BoxHoleID = '" + dr["BoxHoleID"].ToString() + "' " +
                                                    "WHERE Prodmonth = " + Prodmonth.ToString() +
                                                    " AND Sectionid = '" + dr["Sectionid"].ToString() + "' " +
                                                    " AND Workplaceid = '" + dr["WorkplaceID"].ToString() + "' " +
                                                    " AND Activity = " + _Act +
                                                    " AND IsCubics = '" + dr["IsCubics"].ToString() + "' ";

                            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                            theData.ExecuteInstruction();

                            theData.SqlStatement = "UPDATE WORKPLACE SET " +
                                                    "BoxHoleID = '" + dr["BoxHoleID"].ToString() + "' " +
                                                    "WHERE Workplaceid = '" + dr["WorkplaceID"].ToString() + "' ";

                            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                            theData.ExecuteInstruction();
                        }

                        if (dr["TheTypePlanning"].ToString() == "Sundry")
                        {
                            theData.SqlStatement = "UPDATE PLANMONTH_SUNDRYMINING SET " +
                                                    "BoxHoleID = '" + dr["BoxHoleID"].ToString() + "' " +
                                                    "WHERE Prodmonth = " + Prodmonth.ToString() +
                                                    " AND Sectionid = '" + dr["Sectionid"].ToString() + "' " +
                                                    " AND Workplaceid = '" + dr["WorkplaceID"].ToString() + "' " +
                                                    " AND Activity = " + _Act;

                            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                            theData.ExecuteInstruction();

                            theData.SqlStatement = "UPDATE WORKPLACE SET " +
                                                    "BoxHoleID = '" + dr["BoxHoleID"].ToString() + "' " +
                                                    "WHERE Workplaceid = '" + dr["WorkplaceID"].ToString() + "' ";

                            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                            theData.ExecuteInstruction();
                        }

                        if (dr["TheTypePlanning"].ToString() == "Sweeps & Vamps")
                        {
                            theData.SqlStatement = "UPDATE Planmonth_Oldgold SET " +
                                                    "BoxHoleID = '" + dr["BoxHoleID"].ToString() + "' " +
                                                    "WHERE Prodmonth = " + Prodmonth.ToString() +
                                                    " AND Sectionid = '" + dr["Sectionid"].ToString() + "' " +
                                                    " AND Workplaceid = '" + dr["WorkplaceID"].ToString() + "' " +
                                                    " AND Activity = " + _Act;

                            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                            theData.ExecuteInstruction();

                            theData.SqlStatement = "UPDATE WORKPLACE SET " +
                                                    "BoxHoleID = '" + dr["BoxHoleID"].ToString() + "' " +
                                                    "WHERE Workplaceid = '" + dr["WorkplaceID"].ToString() + "' ";

                            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                            theData.ExecuteInstruction();
                        }
                    }

                }

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Tramming Planning Data Saved Successfully", Color.CornflowerBlue);
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
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
