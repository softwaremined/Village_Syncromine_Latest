using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionGlobal
{
    public class GetSectionsAndName
    {
        public  TUserCurrentInfo UserCurrentInfo;
        public  string theSystemDBTag;
        public MWDataManager.clsDataAccess theSectionsAndName(string ProdMonth, string ForWhom, int WhichLevel)
        {

            int TheLevel = 1;
            string TheSection = "";
            MWDataManager.clsDataAccess _theResults = new MWDataManager.clsDataAccess();
            _theResults.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _theResults.queryReturnType = MWDataManager.ReturnType.DataTable;
            _theResults.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

            MWDataManager.clsDataAccess _theLevel = new MWDataManager.clsDataAccess();
            _theLevel.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _theLevel.queryReturnType = MWDataManager.ReturnType.DataTable;
            _theLevel.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _theLevel.SqlStatement = "select distinct * from section where Prodmonth = " + ProdMonth + 
                                     " and sectionid = '" + ForWhom + "'";
            //_theLevel.SqlStatement = "select * from section where Prodmonth = " + ProdMonth +
            //                        " and HierarchicalID = '" + WhichLevel + "'";
            _theLevel.ExecuteInstruction();
          
            foreach (DataRow s in _theLevel.ResultsDataTable.Rows)
            {
                TheLevel = Convert.ToInt32(s["HierarchicalID"].ToString());
            }

            switch (TheLevel)
            {
                case 1: TheSection = "SectionID_5"; break;
                case 2: TheSection = "SectionID_4"; break;
                case 3: TheSection = "SectionID_3"; break;
                case 4: TheSection = "SectionID_2"; break;
                case 5: TheSection = "SectionID_1"; break;
                case 6: TheSection = "SectionID"; break;
            }



            //if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID - 3)
            //{
            //    _theResults.SqlStatement = "select distinct SectionID_5 As SectionID, NAME_5 As Name from vw_Section_Complete b " +
            //                               "Where b.Prodmonth = " + ProdMonth +
            //                               "and b." + TheSection + "  = '" + ForWhom + "' order by NAME_5, SectionID_5";
            //}

            //if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID - 2)
            //{
            //    _theResults.SqlStatement = "select distinct SectionID_4 As SectionID, NAME_4 As Name from vw_Section_Complete b " +
            //                               "Where b.Prodmonth = " + ProdMonth +
            //                               "and b." + TheSection + "  = '" + ForWhom + "' order by NAME_4, SectionID_4";
            //}

            //if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID - 1)
            //{
            //    _theResults.SqlStatement = "select distinct SectionID_3 As SectionID, NAME_3 As Name from vw_Section_Complete b " +
            //                               "Where b.Prodmonth = " + ProdMonth +
            //                               "and b." + TheSection + "  = '" + ForWhom + "' order by NAME_3, SectionID_3";
            //}

            //if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID)
            //{
            //    _theResults.SqlStatement = "select distinct SectionID_2 As SectionID, NAME_2 As Name from vw_Section_Complete b " +
            //                               "Where b.Prodmonth = " + ProdMonth +
            //                               "and b." + TheSection + "  = '" + ForWhom + "' order by NAME_2, SectionID_2";
            //}

            //if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID + 1)
            //{
            //    _theResults.SqlStatement = "select distinct SectionID_1 As SectionID, NAME_1 As Name from vw_Section_Complete b " +
            //                               "Where b.Prodmonth = " + ProdMonth +
            //                               "and b." + TheSection + "  = '" + ForWhom + "' order by NAME_1, SectionID_1";
            //}
            //if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID + 2)
            //{
            //    _theResults.SqlStatement = "select distinct SectionID As SectionID, NAME As Name from vw_Section_Complete b " +
            //                               "Where b.Prodmonth = " + ProdMonth +
            //                               "and b." + TheSection + "  = '" + ForWhom + "' order by NAME, SectionID";
            //}


            if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID - 3)
            {
                _theResults.SqlStatement = "select distinct SectionID_5 As SectionID, NAME_5 As Name from Section_Complete b " +
                                           "Where b.Prodmonth = " + ProdMonth +
                                           " and b." + TheSection + "  = '" + ForWhom + "' order by NAME_5, SectionID_5";
            }

            if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID - 2)
            {
                _theResults.SqlStatement = "select distinct SectionID_4 As SectionID, NAME_4 As Name from Section_Complete b " +
                                           "Where b.Prodmonth = " + ProdMonth +
                                           " and b." + TheSection + "  = '" + ForWhom + "' order by NAME_4, SectionID_4";
            }

            if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID - 1)
            {
                _theResults.SqlStatement = "select distinct SectionID_3 As SectionID, NAME_3 As Name from Section_Complete b " +
                                           "Where b.Prodmonth = " + ProdMonth +
                                           " and b." + TheSection + "  = '" + ForWhom + "' order by NAME_3, SectionID_3";
            }

            if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID)
            {
                _theResults.SqlStatement = "select distinct SectionID_2 As SectionID, NAME_2 As Name from Section_Complete b " +
                                           "Where b.Prodmonth = " + ProdMonth +
                                           " and b." + TheSection + "  = '" + ForWhom + "' order by NAME_2, SectionID_2";
            }

            if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID + 1)
            {
                _theResults.SqlStatement = "select distinct SectionID_1 As SectionID, NAME_1 As Name from Section_Complete b " +
                                           "Where b.Prodmonth = " + ProdMonth +
                                           " and b." + TheSection + "  = '" + ForWhom + "' order by NAME_1, SectionID_1";
            }
            if (WhichLevel == TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID + 2)
            {
                _theResults.SqlStatement = "select distinct SectionID As SectionID, NAME As Name from Section_Complete b " +
                                           "Where b.Prodmonth = " + ProdMonth +
                                           " and b." + TheSection + "  = '" + ForWhom + "' order by NAME, SectionID";
            }

            //_theResults.ExecuteInstruction();
            return _theResults;
        }
    }

    public class GetHierarchicalLevel
    {
        public TUserCurrentInfo UserCurrentInfo;
        public string theSystemDBTag;
        public int theGetHierarchicalLevel(string ProdMonth, string SectionID)
        {
            int TheLevel = 1;

            MWDataManager.clsDataAccess _theLevel = new MWDataManager.clsDataAccess();
            _theLevel.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _theLevel.queryReturnType = MWDataManager.ReturnType.DataTable;
            _theLevel.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _theLevel.SqlStatement = string.Format("Select HierarchicalID from Section where SectionID = '{0}' AND ProdMonth = {1}", SectionID.ToString(), ProdMonth.ToString());
            _theLevel.ExecuteInstruction();

            foreach (DataRow s in _theLevel.ResultsDataTable.Rows)
            {
                TheLevel = Convert.ToInt32(s["HierarchicalID"].ToString());
            }

            return TheLevel;
        }
    }

    public class Procedures
    {
        public string ExtractAfterColon(string TheString)
        {
            string AfterColon;

            int index = TheString.IndexOf(":"); // Kry die postion van die :

            AfterColon = TheString.Substring(index + 1); // kry alles na :

            return AfterColon;
        }

        public string ExtractBeforeColon(string TheString)
        {
            if (TheString != "")
            {
                string BeforeColon;

                int index = TheString.IndexOf(":");

                BeforeColon = TheString.Substring(0, index);

                return BeforeColon;
            }
            else
            {
                return "";
            }
        }
    }
}
