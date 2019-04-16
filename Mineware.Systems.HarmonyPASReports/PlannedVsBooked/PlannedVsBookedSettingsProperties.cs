using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using MWDataManager;
using System.Drawing;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.PlannedVsBooked
{
    class PlannedVsBookedSettingsProperties : clsBase
    {
        private DateTime _Prodmonth;
        public DateTime Prodmonth { get { return _Prodmonth; } set { _Prodmonth = value; UpdateSumOnArg e = new UpdateSumOnArg(); OnUpdateSumOnRequest(e); } }

        private string _WhatMonth;
        public string WhatMonth { get { return _WhatMonth; } set { _WhatMonth = value; } }
        public string pmonth;
        private string _Desc;

        public class UpdateActivitySelectionArg : EventArgs
        {

            public UpdateActivitySelectionArg(bool stoping, bool dev)
            {
                Stoping = stoping;
                Dev = dev;
            }

            public bool Stoping { get; set; }
            public bool Dev { get; set; }
        }

        public bool Stoping { get; set; }
        public bool Dev { get; set; }

        public string Desc
        {
            get
            {
                return _Desc;
            }
            set
            {
                _Desc = value;
            }
        }

        private bool _Tons;
        public bool Tons
        {
            get
            {
                return _Tons;
            }
            set
            {
                _Tons = value;
            }
        }

        private string _section1;
        public string section1 { get { return _section1; } set { _section1 = value; } }

        //private DataTable _SumOn;
        //public DataTable SumOn
        //{
        //    get
        //    {
        //        _SumOn = loadSumOn();
        //        return _SumOn;
        //    }
        //}

        public DataTable LoadAllLevel()
        {
                MWDataManager.clsDataAccess Bus_Logic = new MWDataManager.clsDataAccess();
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);

            Bus_Logic.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
            Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

            Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;

            Bus_Logic.SqlStatement = "select HierarchicalID from section where prodmonth = '" + Prodmonth.ToString("yyyyMM") + "' and name= '" + _NAME + "'";

            Bus_Logic.ExecuteInstruction();

            if (Bus_Logic.ResultsDataTable.Rows.Count > 0)
            {
                int _hierID = Convert.ToInt32(Bus_Logic.ResultsDataTable.Rows[0]["HierarchicalID"]);

                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

                Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;


                //if (Thelevel == null)
                //{
                //    Thelevel = "0";
                //}
                Bus_Logic.SqlStatement = "select distinct HierarchicalID,  " +
                                        "Description = (select  " +
                                        "case when Hierarchicalid = 1 then 'Business Coach' " +
                                        "when Hierarchicalid = 2 then 'Mine Manager'  " +
                                        "when Hierarchicalid = 3 then 'Mining Manager' " +
                                        "when Hierarchicalid = 4 then 'Mine Overseer' " +
                                        "when Hierarchicalid = 5 then 'Coach' " +
                                        "when Hierarchicalid = 6 then 'Miner' " +
                                        "end) from Section where HierarchicalID > " + _hierID + " and  " +
                                        "ProdMonth = '" + Prodmonth.ToString("yyyyMM") + "'  " +
                                        "union  \r\n " +
                                        "select 7 HierarchicalID, 'Workplace' Description " +
                                        "order by HierarchicalID";
                Bus_Logic.ExecuteInstruction();
            }

            //else
                //Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "There's no data for your selection", Color.Red);

            return Bus_Logic.ResultsDataTable;
        }

        //public DataTable loadSumOn()
        //{
        //    if (_NAME != null && _Prodmonth != null)
        //    {
        //        MWDataManager.clsDataAccess Bus_Logic = new MWDataManager.clsDataAccess();
        //        Bus_Logic.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
        //        Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

        //        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;

        //        Bus_Logic.SqlStatement = "select sectionid from section where prodmonth = '" + Prodmonth.ToString("yyyyMM") + "' and name= '" + _NAME + "'";

        //        Bus_Logic.ExecuteInstruction();

        //        DataTable dt = new DataTable();
        //        dt = Bus_Logic.ResultsDataTable;


        //        foreach (DataRow ds in dt.Rows)
        //        {
        //            _section1 = ds["sectionid"].ToString();
        //        }

        //        Bus_Logic.SqlStatement = "Select HierarchicalID from Section where SectionID = '" + _section1 + "' AND ProdMonth = '" + pmonth + "'";
        //        Bus_Logic.ExecuteInstruction();

        //        DataTable dt1 = new DataTable();
        //        dt1 = Bus_Logic.ResultsDataTable;

        //        foreach (DataRow dr in dt1.Rows)
        //        {
        //            HierarchicalID = Convert.ToInt32(dr["HierarchicalID"]);
        //        }

        //        DataTable dtLevels = LoadAllLevel(HierarchicalID.ToString());

        //        if (dtLevels != null)
        //        {
        //            return Bus_Logic.ResultsDataTable;
        //        }

        //        else { return null; }
        //    }
        //    else { return null; }
        //}

        public event UpdateSumOn UpdateSumOnRequest;
        protected void OnUpdateSumOnRequest(UpdateSumOnArg e)
        {
            if (UpdateSumOnRequest != null)
            {
                UpdateSumOnRequest(this, e);
            }
        }
        
        public delegate void UpdateSumOn(object sender, UpdateSumOnArg e);

        public class UpdateSumOnArg : EventArgs
        {

        }
        private string _SectionID;
        public string SectionID { get { return _SectionID; } set { _SectionID = value; } }

        private string _NAME;
        public string NAME { get { return _NAME; } set { _NAME = value; UpdateSumOnArg e = new UpdateSumOnArg(); OnUpdateSumOnRequest(e); } }

        private DateTime _Showuntil;
        public DateTime Showuntil { get { return _Showuntil; } set { _Showuntil = value; } }

        private int _HierarchicalID;
        public int HierarchicalID { get { return _HierarchicalID; } set { _HierarchicalID = value; } }

        private string  _Type;
        public string Type { get { return _Type; } set { _Type = value; } }

        private string _ShowType;
        public string ShowType { get { return _ShowType; } set { _ShowType = value; } }

        private string _CastType;
        public string CastType { get { return _CastType; } set { _CastType = value; } }

        private string _MSType;
        public string MSType { get { return _MSType; } set { _MSType = value; } }

        public bool get_Activity_Reports()
        {
            bool _executionResult = false;
            try
            {
                MWDataManager.clsDataAccess Bus_Logic = new MWDataManager.clsDataAccess();
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (Bus_Logic.queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT 0 Code, 'Stoping' [Desc] " +
                                        "UNION " +
                                        "SELECT 1 Code, 'Development' [Desc] " +
                                        "union " +
                                        "select 8 Code, 'Sweepings' [Desc] ";

                Bus_Logic.ExecuteInstruction();

                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;
        }

    }
}
