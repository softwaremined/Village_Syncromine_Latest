using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Level
{
    class clsLevel:clsBase 
    {

        public DataTable loadGrid()
        {
           // theData.SqlStatement = " select w.*,w.name  ,o.name lvl, w.LevelNumber +' Level to '+ o.name  [Description] from dbo.OREFLOWENTITIES w " +
            theData.SqlStatement = " select w.*,o.name lvl, w.LevelNumber +' Level to '+ o.name  Descp from dbo.OREFLOWENTITIES w " +
                                       "left outer join oreflowentities o on  w.parentoreflowid = o.oreflowid and " +
                                       "w.division = o.division where w.oreflowcode = 'Lvl' " +
                                       "order by w.oreflowid";

            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable loadGridEdit(string action, string Division, string Code)
        {
            if (action == "Add")
            {
                theData.SqlStatement = "  SELECT TypeCode Code, Description, \r\n " +
                                      " cast(0 as bit) Selected  \r\n " +
                                      "  FROM CODE_WPTYPE \r\n " +
                                      "  ORDER BY TypeCode ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            else
            {
                theData.SqlStatement = " SELECT c.TypeCode Code, c.Description, \r\n " +
                                   "  Selected = case when isnull(g.OreflowID,'') <> '' then cast(1 as bit) else cast(0 as bit) end \r\n " +
                                    "  FROM CODE_WPTYPE c \r\n " +
                                     "  left outer join CODE_WPTypeLevelLink g on \r\n " +
                                     "    g.TypeCode = c.TypeCode and \r\n " +
                                    "    g.Division = '" + Division + "' and \r\n " +
                                     "    g.OreflowID = '" + Code + "'  \r\n " +
                                     "  ORDER BY c.TypeCode";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
            }
            return theData.ResultsDataTable;
        }

        public DataTable loadSubDivison()
        {
            theData.SqlStatement = "SELECT DivisionCode, (DivisionCode + ':' + Description) DivisionCodeDescription FROM CODE_WPDivision ORDER BY DivisionCode";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

        public DataTable Levelorepass(string division)
        {

            theData.SqlStatement = " select OreFlowID Ore,(OreFlowID+':'+Name) Orepass from oreflowentities where oreflowcode = 'OPass'";
            //if (this.blnIsCentralizedDatabase)
            //{
            if (division != "-1")
                theData.SqlStatement = theData.SqlStatement + " and Division = '" + division + "' ";
       // }
        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            DataTable Shaft = theData.ResultsDataTable;

            //foreach (DataRow dr in Shaft.Rows)
            //    LVLOPassCb.Items.Add(dr["OreFlowID"] + ":" + dr["name"].ToString());
            return theData.ResultsDataTable;
        }

        public DataTable loadReef()
        {
            theData.SqlStatement = "SELECT cast(ReefID as varchar(50)) ReefID,cast(ReefID as varchar(50))+':'+Description Reef FROM Reef WHERE Selected = '1'";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

        public DataTable loadIfEditId(string editid,string division)
        {

            theData.SqlStatement = "select c.*, b.name name1, e.OreFlowID lvl,e.OreFlowID Ore, e.name lvlname, r.ReefID, r.Description, e.Division Division from oreflowentities c left outer join (select * from section " +
                                   " where prodmonth = '" + TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).ProdMonth.ToString() + "'" +
                                    " ) b on c.sectionid = b.sectionid " +
                                    "left outer join oreflowentities e on c.parentoreflowid = e.oreflowid " +
                                    "left outer join Reef r on c.ReefType = convert(varchar(10), r.ReefID) " +
                                    "where c.oreflowid = '" + editid + "' and c.Division = '" + division + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

        public void saveData(string division, string ore, string reefid, bool inact, bool crosstram,
            string EditID,string OreFlowID,string OreFlowDesc,string oreLevel,
            string CostArea, string OreHopperFactor,DataTable dtLevel)
        {

            theData.SqlStatement = "SELECT * FROM code_wpdivision WHERE DivisionCode = '" + division + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();


            string Shaft = "";
            Shaft = ore;//procs.ExtractBeforeColon(LVLOPassCb.Text);
            String ReefType = reefid; //procs.ExtractBeforeColon(LVLReefTypeCb.Text);

            string active = "N";
            if (inact == true)
                active = "Y";

            string crossTram = "N";
            if (crosstram == true)
                crossTram = "Y";

            if (EditID == "")
            {
                theData.SqlStatement = " SELECT * FROM Oreflowentities \r\n " +
                        " WHERE OreflowCode = 'Lvl' and OreflowID = '" + OreFlowID + "' \r\n " +
                        " and Division = '" + division + "' \r\n ";
                //if (SysSettings.IsCentralized.ToString() == "1")
                //    theData.SqlStatement = _dbMan.SqlStatement + " AND Mine2 = '" + GridMine + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();

                if (theData.ResultsDataTable.Rows.Count > 0)
                {
                    // MessageBox.Show("Cannot insert a duplicate record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    theData.SqlStatement = "insert into OreflowEntities(oreflowid,oreflowcode,name,levelnumber,hopperfactor,reeftype,ParentOreFlowID,inactive,CrossTram,Division,CostArea"  +
                        ")VALUES('" + OreFlowID + "', 'Lvl','" + OreFlowDesc.Substring(0, 9) + "','" + oreLevel + "','"
                        + Convert.ToDecimal(OreHopperFactor) + "','" + ReefType + "','" + Shaft + "','" + active + "','" + crossTram
                        + "','" + (division == null ? "" : division) + "','" + CostArea + "') ";
                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    theData.ExecuteInstruction();


                    theData.SqlStatement = " delete from  CODE_WPTypeLevelLink \r\n " +
                   " where OreflowID = '" + OreFlowID + "' and \r\n " +
                   "      Division = '" + division + "' \r\n ";
                    bool FoundRec = false;
                    foreach (DataRow dr in dtLevel.Rows)
                    {
                        if (Convert.ToBoolean(dr["Selected"]) == true)
                        {
                            theData.SqlStatement = theData.SqlStatement + " INSERT INTO CODE_WPTypeLevelLink(OreflowID, Division, TypeCode) \r\n " +
                                        " Values ( \r\n " +
                                        " '" + OreFlowID + "', '" + division + "', \r\n " +
                                        " '" + dr["Code"].ToString() + "') \r\n ";
                            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                            theData.ExecuteInstruction();
                        }
                    }

                }
            }
            else
            {
                string aa = "";
                if (OreFlowDesc.Length > 9)
                    aa = OreFlowDesc.Substring(0, 9);
                else
                    aa = OreFlowDesc;
                theData.SqlStatement = "UPDATE OreFlowEntities SET Name = '" + aa + "', \r\n " +
                                         "LevelNumber = '" + oreLevel + "', \r\n " +
                                         "HopperFactor = '" + Convert.ToDecimal(OreHopperFactor) + "', \r\n " +
                                        "ReefType = '" + ReefType + "', \r\n " +
                                         "ParentOreFlowID = '" + Shaft + "', \r\n " +
                                         "Inactive = '" + active + "', \r\n " +
                                         "Crosstram = '" + crossTram + "',\r\n " +
                                         "Division = '" + (division == null ? "" : division) + "', \r\n " +
                                         "CostArea = '" + CostArea + "' \r\n " +
                                         "WHERE OreFlowID = '" + OreFlowID + "'";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();

                theData.SqlStatement = " delete from  CODE_WPTypeLevelLink \r\n " +
                    " where OreflowID = '" + OreFlowID + "' and \r\n " +
                    "      Division = '" + division + "' \r\n ";
                bool FoundRec = false;
                foreach (DataRow dr in dtLevel.Rows)
                {
                    if (Convert.ToBoolean(dr["Selected"]) == true)
                    {
                        theData.SqlStatement = theData.SqlStatement + " INSERT INTO CODE_WPTypeLevelLink(OreflowID, Division, TypeCode) \r\n " +
                                    " Values ( \r\n " +
                                    " '" + OreFlowID + "', '" + division + "', \r\n " +
                                    " '" +dr["Code"].ToString() + "') \r\n ";
                        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        theData.ExecuteInstruction();
                    }
                }
            }
        }

      
    }
}
