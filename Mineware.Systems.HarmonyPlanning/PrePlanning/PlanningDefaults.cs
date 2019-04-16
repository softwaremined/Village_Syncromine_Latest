using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mineware.Systems.Global;
using System.Data.SqlClient;
using System.Data;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

using Mineware.Systems.Global.sysMessages;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace Mineware.Systems.Planning.PrePlanning
{
    public class PlanningDefaults : clsBase, IDisposable
    {


        //private static readonly Lazy<clsPlanning> lazy =
        //        new Lazy<clsPlanning>(() => new clsPlanning());
        //public static clsPlanning Instance { get { return lazy.Value; } 

        #region private fields 
        private int _activity;
        private string _sectionID;
        private string _sectionName;
        private string _prodMonth;
        private string _currentProdMonth;
        private bool _isRevised;
        private readonly sysMessagesClass _sysMessagesClass = new sysMessagesClass();
        private PrePlanningSettings planningSettings = new PrePlanningSettings();
        private PlanningCycle planningCycle = new PlanningCycle();
        #endregion

        #region public fields
        public DataTable tblSections;
        public DataTable tblSectionStartEndDates;
        public DataTable tblPlanningData;
        public DataTable tblPlanningCycleData;
        public DataTable tblMinerListData;
        public DataTable tblOrgUnitsData;
        public DataTable tblMiningMethods;
        public DataTable theDates = new DataTable();
        public DateTime theBeginDate;
        public DateTime theEndDate;
        #endregion

        #region screens declaration
        private ucPreplanning _PlanningScreen;
        private ucStopePrePlanning _StopePrePlanning;
        #endregion

        #region properties


        public string CurrentProdMonth
        {
            get
            {
                //  if (_currentProdMonth == null)
                //  {
                _currentProdMonth = GetCurrentProdMonth();
                // }
                return _currentProdMonth;
            }

        }

        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }

        public string ProdMonth
        {
            get
            {
                return _prodMonth;
            }
            set
            {
                _prodMonth = value;
                updateSections();
            }
        }

        public bool isRevised
        {
            get { return _isRevised; }
            set { _isRevised = value; }
        }

        public int Activity
        {
            get { return _activity; }
            set { _activity = value; }
        }

        public string SectionID
        {
            get { return _sectionID; }
            set { _sectionID = value; }
        }

        public ucPreplanning PlanningScreen
        {
            get
            {
                if (_PlanningScreen == null)
                    _PlanningScreen = new ucPreplanning();
                return _PlanningScreen;
            }
        }

        public PrePlanningSettings PlanningSettings
        {
            get
            {
                return planningSettings;
            }

            set
            {
                planningSettings = value;
            }
        }

        public ucStopePrePlanning StopePrePlanning
        {
            get
            {
                return _StopePrePlanning;
            }

            set
            {
                _StopePrePlanning = value;
            }
        }

        public PlanningCycle PlanningCycle
        {
            get
            {
                return planningCycle;
            }

            set
            {
                planningCycle = value;
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (_PlanningScreen != null)
            {
                _PlanningScreen.Dispose();
                _PlanningScreen = null;
            }
        }
        #endregion

        public PlanningDefaults()
        {
        }

        /// <summary>
        /// Updates the sections list for the selected production month
        /// </summary>
        public void updateSections()
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = systemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

            List<string> Thelist = TProductionGlobal.getUserInfo(UserCurrentInfo.Connection).PlanBookSections;

            DataTable _SectionResult = new DataTable();
            GetSectionsAndName theData = new GetSectionsAndName();
            theData.theSystemDBTag = systemDBTag;
            theData.UserCurrentInfo = UserCurrentInfo;

            List<string> sections = TProductionGlobal.getUserInfo(UserCurrentInfo.Connection).PlanBookSections;
            foreach (string item in sections)
            {
                MWDataManager.clsDataAccess temp = theData.theSectionsAndName(_prodMonth, item, TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID);
                temp.ExecuteInstruction();
                if (temp.ResultsDataTable.Rows.Count != 0)
                {
                    if (_SectionResult.Columns.Count == 0)
                    {
                        _SectionResult = temp.ResultsDataTable.Clone();
                    }
                    _SectionResult.Merge(temp.ResultsDataTable);
                }

            }

            tblSections = _SectionResult;


            //}

            //if (BMEBL.get_Sections(Convert.ToString(_prodMonth), TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID.ToString()) == true)
            //{
            //    tblSections = BMEBL.ResultsDataTable.Clone();
            //    tblSections = BMEBL.ResultsDataTable.Copy();
            //}

        }

        //public void updateSections()
        //{
        //    CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
        //    BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
        //    BMEBL.SetsystemDBTag = systemDBTag;
        //    BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
        //    List<string> Thelist = TProductionGlobal.getUserInfo(UserCurrentInfo.Connection).PlanBookSections;
        //    if (BMEBL.GetSectionsMO(Convert.ToString(_prodMonth), Thelist) == true)
        //    {
        //        tblSections = BMEBL.ResultsDataTable.Clone();
        //        tblSections = BMEBL.ResultsDataTable.Copy();
        //    }

        //}

        /// <summary>
        /// Test if section planning is locked by another user. This will set the planning screen readonly label
        /// </summary>
        public clsResults isReadOnly()
        {
            var theResult = new clsResults();
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.SqlStatement = "SELECT * FROM SECTION WHERE Sectionid = '" + _sectionID + "' AND  Prodmonth = " + _prodMonth + " AND PrePlanLockedBy<>''";
            theData.ExecuteInstruction();
            if (theData.ResultsDataTable.Rows.Count > 0 && theData.ResultsDataTable.Rows[0]["PrePlanLockedBy"].ToString() != UserCurrentInfo.UserID)
            {
                foreach (DataRow r in theData.ResultsDataTable.Rows)
                {
                    theResult.Successfull = false;
                    theResult.Message.AppendLine("Section " + SectionName + " is set to read only by : " + r["PrePlanLockedBy"].ToString());


                    //_PlanningScreen.setReadonly(true,"Read Only Locked By : " + r["PrePlanLockedBy"].ToString());

                }

            }
            else
            {
                theResult.Successfull = true;
            }

            return theResult;
        }

        /// <summary>
        /// Set the section planning readonly status
        /// </summary>
        public void SetCelarReadOnly(bool setReadOnly)
        {
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;

            if (setReadOnly)
            {
                theData.SqlStatement = string.Format("update SECTION SET PrePlanLockedDate=GETDATE(),PrePlanLockedBy='{0}',machineName='{1}',machineIP='{2}' WHERE Prodmonth={3} and SectionID= '{4}' ", UserCurrentInfo.Name, TGlobalItems.GetPCName(), TGlobalItems.GetIP(), _prodMonth, _sectionID);
            }
            else
            {
                theData.SqlStatement = string.Format("update SECTION SET PrePlanLockedDate=GETDATE(),PrePlanLockedBy='{0}',machineName='{1}',machineIP='{2}' WHERE Prodmonth={3} and SectionID= '{4}' ", "", "", "", _prodMonth, _sectionID);
            }

            theData.ExecuteInstruction();
        }

        // 2019/0/05 - DvdB - Removed calendardate form where claus, do not work if there is no planning on the bookDate
        public DataTable GetProdmonth(string section, string bookDate)
        {
            theData.SqlStatement = string.Format("  \r" +


                                                  "  select max(p.prodmonth) Prodmonth \r" +
                                                  " from planning p, Section_Complete s \r" +
                                                  " where s.Prodmonth = p.prodmonth \r" +
                                                  " and s.SectionID = p.Sectionid \r" +
                                                  " and p.calendardate = '" + bookDate + "' \r" +
                                                  " and s.SectionID_2 = '" + section + "' \r" +


                                                 " ");

            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            var errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                // _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", ProductionGlobal.systemTag, "clsBookingsABS", "GetShiftInfo", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            return theData.ResultsDataTable;
        }

        /// <summary>
        /// Gest the sections current production month based on calendars
        /// </summary>
        private string GetCurrentProdMonth()
        {

            theData.SqlStatement = "sp_PrePlanning_Prodmonth";
            theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

            SqlParameter[] _paramCollection1 =
                    {
                      theData.CreateParameter("@SECTIONID_2", SqlDbType.VarChar , 50,_sectionID)
                    };

            theData.ParamCollection = _paramCollection1;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            if (theData.ResultsDataTable.Rows.Count == 1)
                return _currentProdMonth = theData.ResultsDataTable.Rows[0]["CurrentProductionMonth"].ToString();
            else return null;
        }

        /// <summary>
        /// Gest the current sections calendar dates start and end
        /// </summary>
        public clsResults ValidateSectionCalender()
        {
            clsResults theResults = new clsResults();
            tblSectionStartEndDates = new DataTable();
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;

            BMEBL.SetsystemDBTag = systemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

            if (BMEBL.get_MinerSectionStartEndDates(PlanningSettings.ProdMonth,PlanningSettings.MOSectionID) == true)
            {
                tblSectionStartEndDates = BMEBL.ResultsDataTable;
            }

            if (tblSectionStartEndDates != null && tblSectionStartEndDates.Rows.Count > 0)
            {
                foreach (DataRow r in tblSectionStartEndDates.Rows)
                {
                    if (r["StartDate"].ToString() != "") // test if dates are valid
                    {
                        theBeginDate = Convert.ToDateTime(r["StartDate"].ToString());
                        theEndDate = Convert.ToDateTime(r["EndDate"].ToString());
                        theResults.Successfull = true;
                    }
                    else
                    {
                        theResults.Successfull = false;
                        theResults.Message.AppendLine("The calendars for the selected section is invalid");
                    }
                }
            }
            else
            {
                theResults.Successfull = false;
                theResults.Message.AppendLine("The calendars for the selected section is invalid");
            }

            return theResults;
        }

        /// <summary>
        /// To check if there are any workplaces with no sectionID
        /// </summary>
        public void RemoveUnsavedWorkplaces()
        {
            var checkSectionID = tblPlanningData.Select("Sectionid ='-1'");
            if(checkSectionID.Length > 0)
            {
                MWDataManager.clsDataAccess RemoveWorkplace = new MWDataManager.clsDataAccess();
                RemoveWorkplace.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
                RemoveWorkplace.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                RemoveWorkplace.queryReturnType = MWDataManager.ReturnType.DataTable;
                sb.Clear();
                foreach(var r in checkSectionID)
                {
                    sb.AppendLine(String.Format("delete from PLANMONTH where Sectionid = '-1' and Prodmonth = {0} and Activity = {1}",
                                  planningSettings.ProdMonth,planningSettings.ActivityID));
                }
                RemoveWorkplace.SqlStatement = sb.ToString();
                var theResult = RemoveWorkplace.ExecuteInstruction();


            }

        }


        /// <summary>
        /// Load planning data from DB
        /// </summary>
        public void LoadPlanning()
        {
            MWDataManager.clsDataAccess _TestData = new MWDataManager.clsDataAccess();
            _TestData.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
            _TestData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _TestData.queryReturnType = MWDataManager.ReturnType.DataTable;

            _TestData.SqlStatement = "SELECT *,PP.ReefSQM,WP.ReefWaste FROM dbo.PLANMONTH PP inner join SECTION_COMPLETE SC ON \n" +
                                     "   PP.Prodmonth =  SC.PRODMONTH AND \n" +
                                     "   PP.Sectionid =  SC.Sectionid \n" +
                                     "   inner join WORKPLACE WP ON \n" +
                                     "   PP.Workplaceid=WP.WORKPLACEID  \n" +
                                     " WHERE PP.Activity =  " + planningSettings.ActivityID + "\n" +
                                     " AND  SC.Sectionid_2 = '" + planningSettings.MOSectionID + "'" + " \n" +
                                     " and PP.Prodmonth = " + planningSettings.ProdMonth;
            var theResult = _TestData.ExecuteInstruction();
            
            //planningCycle.LoadPlanningCycleData(tblPlanningData);

            if (theResult.success)
            {
                if (_TestData.ResultsDataTable.Rows.Count == 0 && TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.HarmonyPasMenuStructure.miAllowImportPreviousMonth_HPASPlanningWPImportPreviousMonth_MinewareSystemsHarmonyPAS.ItemID) == 2)
                {

                    var canReload = MessageBox.Show("You have not yet planned for this month. Do you want to load your plans from last month, to form a basis for this month's plan? ", "Unlock Data", MessageBoxButtons.YesNo);
                    if (canReload == DialogResult.No)
                    {
                        GetPlanningData();
                        tblPlanningData.Rows.Clear();
                        _TestData = null;
                    }
                    else
                    {
                        ImportPreviousMonthData();
                        GetPlanningData();
                    }
                }
                else
                {
                    GetPlanningData();
                    _TestData = null;
                }
            }


            MWDataManager.clsDataAccess _CycleCodes = new MWDataManager.clsDataAccess();
            _CycleCodes.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
            _CycleCodes.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _CycleCodes.queryReturnType = MWDataManager.ReturnType.DataTable;
            sb.Clear();
            sb.AppendLine("SELECT [CycleCode]");
            sb.AppendLine(",[Description]");
            sb.AppendLine("      ,[CanBlast],DayCallPercentage");
            sb.AppendLine("FROM [PAS_DNK_Syncromine].[dbo].[CODE_CYCLE]");
            sb.AppendLine("where Stoping = 'Y'");

            _CycleCodes.SqlStatement = sb.ToString();
            var CycleCodesResult = _CycleCodes.ExecuteInstruction();
            if (_CycleCodes.ResultsDataTable != null)
            {
                planningCycle.SetCycleCodes(_CycleCodes.ResultsDataTable);
            }



        }

        private void GetPlanningData()
        {
            MWDataManager.clsDataAccess _PrePlanningData = new MWDataManager.clsDataAccess();
            _PrePlanningData.ConnectionString = TConnections.GetConnectionString(systemDBTag,
                                                                                 UserCurrentInfo.Connection);
            _PrePlanningData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _PrePlanningData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _PrePlanningData.SqlStatement = "sp_Load_Planning";

            SqlParameter[] _paramCollection =
            {
                    _PrePlanningData.CreateParameter("@ProdMonth",
                                                     SqlDbType.Int,
                                                     0,
                                                     Convert.ToInt32(planningSettings.ProdMonth)),
                    _PrePlanningData.CreateParameter("@Sectionid_2",
                                                     SqlDbType.VarChar,
                                                     20,
                                                     planningSettings.MOSectionID),
                    _PrePlanningData.CreateParameter("@Activity", SqlDbType.Int, 0, planningSettings.ActivityID),
                };


            _PrePlanningData.ParamCollection = _paramCollection;
            var result = _PrePlanningData.ExecuteInstruction();
            _PrePlanningData.ResultsDataTable.Columns["OrgUnitNight"].DataType = typeof(string);
            tblPlanningData = _PrePlanningData.ResultsDataTable.Copy();
            _PrePlanningData.Dispose();
            _PrePlanningData = null;
        }

        public void LoadPlanningCycle()
        {
            try
            {

                MWDataManager.clsDataAccess _PrePlanningCycleData = new MWDataManager.clsDataAccess();
                _PrePlanningCycleData.ConnectionString = TConnections.GetConnectionString(systemDBTag,
                                                                                          UserCurrentInfo.Connection);
                _PrePlanningCycleData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                _PrePlanningCycleData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _PrePlanningCycleData.SqlStatement = "sp_LoadPlanningCycle";

                SqlParameter[] _paramCollection =
                {
                        _PrePlanningCycleData.CreateParameter("@sectionid",
                                                              SqlDbType.VarChar,
                                                              20,
                                                              (planningSettings.MOSectionID)),
                        _PrePlanningCycleData.CreateParameter("@sectionidMO",
                                                              SqlDbType.VarChar,
                                                              20,
                                                              planningSettings.MOSectionID),
                        _PrePlanningCycleData.CreateParameter("@ProdMonth",
                                                              SqlDbType.Int,
                                                              0,
                                                              Convert.ToInt32(planningSettings.ProdMonth)),

                        _PrePlanningCycleData.CreateParameter("@Activity",
                                                              SqlDbType.Int,
                                                              0,
                                                              planningSettings.ActivityID)
                    };


                _PrePlanningCycleData.ParamCollection = _paramCollection;
                var result = _PrePlanningCycleData.ExecuteInstruction();
                tblPlanningCycleData = _PrePlanningCycleData.ResultsDataTable.Copy();
                planningCycle.LoadPlanningCycleData(tblPlanningCycleData);
                _PrePlanningCycleData.Dispose();
                _PrePlanningCycleData = null;


            }
            catch (Exception)
            {


            }
        }

        /// <summary>
        /// Load all miners for selected MO section and production month 
        /// </summary>
        /// 2019/03/05 : DvdB Updated code to standards 
        public void LoadMinerList()
        {
            MWDataManager.clsDataAccess minerData = new MWDataManager.clsDataAccess();
            minerData.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
            minerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            minerData.queryReturnType = MWDataManager.ReturnType.DataTable;
            sb.Clear();
            sb.AppendLine("select distinct a.SECTIONID, Name from section_complete a inner join SECCAL b on ");
            sb.AppendLine("A.PRODMONTH = B.Prodmonth AND ");
            sb.AppendLine("A.SECTIONID_1 = B.SectionID  ");
            sb.AppendLine(String.Format("where A.prodmonth = '{0}' and a.SECTIONID_2 = '{1}' ORDER BY a.Name", 
                PlanningSettings.ProdMonth, PlanningSettings.MOSectionID));
            minerData.SqlStatement = sb.ToString();
            var theResult = minerData.ExecuteInstruction();
            if (theResult.success) // check if the query was executed correctly 
            {
                tblMinerListData = minerData.ResultsDataTable.Copy();
            }
            minerData.Dispose();
            minerData = null;
        }

        public string GetShiftBoss(string SelectedSection)
        {
            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;

            _MinerData.SqlStatement = "select distinct a.SECTIONID_1, Name_1 from section_complete a inner join SECCAL b on " +
                                      "A.PRODMONTH = B.Prodmonth AND " +
                                      "A.SECTIONID_1 = B.SectionID " +
                                      "where A.prodmonth = '" + PlanningSettings.ProdMonth + "' and a.SECTIONID = '" + SelectedSection + "' ORDER BY a.Name_1";

            _MinerData.ExecuteInstruction();

            return _MinerData.ResultsDataTable.Rows[0][1].ToString();
        }

        public void DeleteWorkplace()
        {
            if (PlanningSettings.ActivityID == 0)
            {
                var selectedRow = StopePrePlanning.GetSelectedRow();
                if (selectedRow != -1)
                {
                    var isLocked = tblPlanningData.Rows[selectedRow]["Locked"].ToString();
                    if (isLocked == "False")
                    {
                        try
                        {
                            var theResult = _sysMessagesClass.viewMessage(MessageType.Info,
                                                                          "Delete Workplace",
                                                                          "Would you like to delete workplace: " +
                                tblPlanningData.Rows[selectedRow]["WorkplaceDesc"].ToString(),
                                                                          ButtonTypes.YesNo,
                                                                          MessageDisplayType.FullScreen);
                            if (theResult == MessageResult.Yes)
                            {
                                MWDataManager.clsDataAccess _DeleteData = new MWDataManager.clsDataAccess()
                                {
                                    ConnectionString =
                                    TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection),
                                    queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement
                                };

                                _DeleteData.SqlStatement = "DELETE FROM dbo.PLANMONTH  \r\n" +
                                    "WHERE Prodmonth = " +
                                    PlanningSettings.ProdMonth +
                                    " AND  \r\n" +
                                    "      Sectionid = '" +
                                    tblPlanningData.Rows[selectedRow]["SectionID"].ToString() +
                                    "' AND   \r\n" +
                                    "      WorkplaceID = '" +
                                    tblPlanningData.Rows[selectedRow]["WorkplaceID"].ToString() +
                                    "' AND  \r\n" +
                                    "      Activity = " +
                                    tblPlanningData.Rows[selectedRow]["Activity"].ToString() +
                                    " AND   \r\n" +
                                    "      IsCubics = '" +
                                    tblPlanningData.Rows[selectedRow]["IsCubics"].ToString() +
                                    "'   \r\n\r\n";

                                _DeleteData.SqlStatement = _DeleteData.SqlStatement +
                                    "DELETE FROM dbo.PLANNING  \r\n" +
                                    "WHERE Prodmonth = " +
                                    PlanningSettings.ProdMonth +
                                    " AND   \r\n" +
                                    "      Sectionid = '" +
                                    tblPlanningData.Rows[selectedRow]["SectionID"].ToString() +
                                    "' AND   \r\n" +
                                    "      WorkplaceID = '" +
                                    tblPlanningData.Rows[selectedRow]["WorkplaceID"].ToString() +
                                    "' AND   \r\n" +
                                    "      Activity = " +
                                    tblPlanningData.Rows[selectedRow]["Activity"].ToString() +
                                    " AND   \r\n" +
                                    "      IsCubics = '" +
                                    tblPlanningData.Rows[selectedRow]["IsCubics"].ToString() +
                                    "'";


                                _DeleteData.ExecuteInstruction();
                                //_DeleteData.SqlStatement = "DELETE FROM WORKPLACE_PREPLANNING WHERE DESCRIPTION = '" + tblPrePlanData.Rows[currentRow]["WorkplaceDesc"].ToString() + "'";
                                //_DeleteData.ExecuteInstruction();


                                var deleteCycle = planningCycle.planningCycleData
                                    .Where(a => a.WorkplaceID ==
                                    tblPlanningData.Rows[selectedRow]["WorkplaceID"].ToString())
                                    .FirstOrDefault();
                                planningCycle.planningCycleData.Remove(deleteCycle);
                                tblPlanningData.Rows[selectedRow].Delete();
                                tblPlanningData.AcceptChanges();
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }
                    else
                    {
                        _sysMessagesClass.viewMessage(MessageType.Info,
                                                                         "Delete Workplace",
                                                                         "This workplace has been approved for Planning. Any changes needs to be done in the Planning screen as Re-Planning.",
                                                                         ButtonTypes.OK,
                                                                         MessageDisplayType.FullScreen);
                    }

                }


            }
            else
            {
                _sysMessagesClass.viewMessage(MessageType.Info,
                                              "Delete Workplace",
                                              "No workplace was selected. Please select a workplace first.",
                                              ButtonTypes.OK,
                                              MessageDisplayType.FullScreen);
            }
        }


        //public string GetMOSection(string SelectedSection)
        //{
        //    MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
        //    _MinerData.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
        //    _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        //    _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;

        //    _MinerData.SqlStatement = "select distinct a.SECTIONID_2, Name_1 from section_complete a inner join SECCAL b on " +
        //                              "A.PRODMONTH = B.Prodmonth AND " +
        //                              "A.SECTIONID_1 = B.SectionID " +
        //                              "where A.prodmonth = '" + PlanningSettings.ProdMonth + "' and a.SECTIONID = '" + SelectedSection + "' ORDER BY a.Name_1";

        //    _MinerData.ExecuteInstruction();

        //    return _MinerData.ResultsDataTable.Rows[0]["SECTIONID_2"].ToString();
        //}

        public void LoadOrgUnits()
        {
            MWDataManager.clsDataAccess _OrgUnitsData = new MWDataManager.clsDataAccess();
            _OrgUnitsData.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
            _OrgUnitsData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _OrgUnitsData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _OrgUnitsData.SqlStatement = "SELECT 1 thepos,'' Crew_Org, ''GangNo " +
                                         " UNION " +
                                         " SELECT 2 thepos,'Contractor'+':'+'0' Crew_Org, '0'GangNo " +
                                         " UNION " +
                                         "SELECT 3 thepos, GangNo+':'+ CrewName+'('+CrewNo+')' Crew_Org,GangNo  FROM CREW --where GangNo like '" + SectionID + "%' ";
            //      " ORDER BY SECTIONID";
            _OrgUnitsData.ExecuteInstruction();

            tblOrgUnitsData = _OrgUnitsData.ResultsDataTable.Copy();


        }

        public void LoadMiningMethod()
        {

            CPMBusinessLayer.clsBusinessLayer bl = new CPMBusinessLayer.clsBusinessLayer();
            bl.SetsystemDBTag = this.systemDBTag;
            bl.SetUserCurrentInfo = this.UserCurrentInfo;
            tblMiningMethods = bl.getMiningMethods(Activity);


        }

        public void addWorkPlace()
        {
            frmWorkplaceSelect frmWorkplaceSelect = new frmWorkplaceSelect { theSystemDBTag = systemDBTag, UserCurrentInfo = this.UserCurrentInfo };
            ArrayList CurrentWorkplace = new ArrayList();
            CurrentWorkplace = frmWorkplaceSelect.GetWorkPlaceList(
                PlanningSettings.ActivityID.ToString(),
                PlanningSettings.ProdMonth,
                CurrentWorkplace);

            if (CurrentWorkplace.Count > 0)
            {
                for (int i = 0; i < CurrentWorkplace.Count; i++)
                {
                    AddWorkplace(CurrentWorkplace[i].ToString());
                }
            }

        }

        private void AddWorkplace(string WPDesc)
        {
            MWDataManager.clsDataAccess _NewWorkPlace = new MWDataManager.clsDataAccess();
            _NewWorkPlace.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);

            _NewWorkPlace.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _NewWorkPlace.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _NewWorkPlace.SqlStatement = "spAddPrePlanningWorkPlace";
            _NewWorkPlace.SqlStatement = "sp_PrePlanning_AddWorkplace";


            SqlParameter[] _paramCollection =
                                {
                                    _NewWorkPlace.CreateParameter("@Prodmonth", SqlDbType.Int, 0,Convert.ToInt32(PlanningSettings.ProdMonth)),
                                    _NewWorkPlace.CreateParameter("@Sectionid_2", SqlDbType.VarChar,20 ,PlanningSettings.MOSectionID),
                                    _NewWorkPlace.CreateParameter("@WPDESCRIPTION", SqlDbType.VarChar,100,WPDesc),
                                    _NewWorkPlace.CreateParameter("@ActivityCode", SqlDbType.Int ,0 ,PlanningSettings.ActivityID ),
                                };

            _NewWorkPlace.ParamCollection = _paramCollection;
            clsDataResult exr = _NewWorkPlace.ExecuteInstruction();

            if (exr.success == false)
            {
                MessageBox.Show(exr.Message);
            }

            if (_NewWorkPlace.ResultsDataTable.Rows.Count != 0)
            {

                CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
                BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
                BMEBL.SetsystemDBTag = systemDBTag;
                BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

                //  DataTable theDates = new DataTable();
                if (BMEBL.get_BeginEndDates(PlanningSettings.MOSectionID, PlanningSettings.ProdMonth) == true)
                {
                    theDates = BMEBL.ResultsDataTable;
                }

                if (_NewWorkPlace.ResultsDataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in _NewWorkPlace.ResultsDataTable.Rows)
                    {
                        tblPlanningData.ImportRow(r);
                    }
                }
            }


        }

        /// <summary>
        /// Run when importing data from previous month
        /// </summary>
        private void ImportPreviousMonthData()
        {
            MWDataManager.clsDataAccess _PrePlanningData = new MWDataManager.clsDataAccess();
            _PrePlanningData.ConnectionString = TConnections.GetConnectionString(systemDBTag,
                                                                                 UserCurrentInfo.Connection);
            _PrePlanningData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _PrePlanningData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _PrePlanningData.SqlStatement = "sp_preplanning_save_previus_month";

            SqlParameter[] _paramCollection =
            {
                    _PrePlanningData.CreateParameter("@ProdMonth",
                                                     SqlDbType.Int,
                                                     0,
                                                     Convert.ToInt32(planningSettings.ProdMonth)),
                    _PrePlanningData.CreateParameter("@Sectionid_2",
                                                     SqlDbType.VarChar,
                                                     20,
                                                     planningSettings.MOSectionID),
                    _PrePlanningData.CreateParameter("@Activity", SqlDbType.Int, 0, planningSettings.ActivityID),
                };


            _PrePlanningData.ParamCollection = _paramCollection;
            var result = _PrePlanningData.ExecuteInstruction();

        }

        public void SaveStopingData()
        {
            tblPlanningData.TableName = "PlanningData";
            tblPlanningData.WriteXml(TGlobalItems.ReportsFolder + "\\PlanningData.xml", XmlWriteMode.IgnoreSchema);
            string CycleData = "NONE";
            string PlanningData = System.IO.File.ReadAllText(TGlobalItems.ReportsFolder + "\\PlanningData.xml");
            PlanningData = PlanningData.Replace("+02:00", "");
            // gest the cycle data in the correct format for the SP
            DataTable newCycleData = new DataTable();
            if (planningCycle.planningCycleData != null && planningCycle.planningCycleData.Count != 0 && tblPlanningData.Rows.Count != 0)
            {
                foreach (var a in planningCycle.planningCycleData)
                {
                    var wpINFO = tblPlanningData.Select("Workplaceid = '" + a.WorkplaceID + "'");
                    if (newCycleData.Columns.Count == 0)
                    {
                        newCycleData = a.GetCycleData(_prodMonth,
                                                      wpINFO[0]["IsCubics"].ToString(),
                                                      _activity,
                                                      wpINFO[0]["SectionID"].ToString())
                            .Copy();
                    }
                    else
                    {
                        newCycleData.Merge(a.GetCycleData(_prodMonth,
                                                          wpINFO[0]["IsCubics"].ToString(),
                                                          _activity,
                                                          wpINFO[0]["SectionID"].ToString()));
                    }
                }

                newCycleData.TableName = "PlanningCycleData";
                newCycleData.WriteXml(TGlobalItems.ReportsFolder + "\\PlanningCycleData.xml", XmlWriteMode.IgnoreSchema);

                CycleData = System.IO.File.ReadAllText(TGlobalItems.ReportsFolder + "\\PlanningCycleData.xml");

                CycleData = CycleData.Replace("+02:00", "");
            }
            theData.ConnectionString = TConnections.GetConnectionString(systemDBTag,
                                                                       UserCurrentInfo.Connection);
            theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.SqlStatement = "sp_Save_Stope_Planning";
            var _paramCollection = new SqlParameter[]
            {
                                theData.CreateParameter("@PlanningData",
                                        SqlDbType.VarChar,
                                        0,
                                        PlanningData),
                                                                theData.CreateParameter("@CycleData",
                                        SqlDbType.VarChar,
                                        0,
                                        CycleData),
            };
            theData.ParamCollection = _paramCollection;
            var theResult = theData.ExecuteInstruction();
            if (theResult.success)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "Planning saved", Color.CornflowerBlue);
            }
            else
            {

            }

        }

        public void AddCyclePlan(string workplaceID, string sectionid, string sectionidMO, string ProdMonth, int Activity, double FL)
        {

            MWDataManager.clsDataAccess _NewCyclePlan = new MWDataManager.clsDataAccess();
            _NewCyclePlan.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);

            _NewCyclePlan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _NewCyclePlan.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _NewWorkPlace.SqlStatement = "spAddPrePlanningWorkPlace";
            _NewCyclePlan.SqlStatement = "sp_LoadPlanningCycleWP";


            SqlParameter[] _paramCollection =
                                {
                                    _NewCyclePlan.CreateParameter("@workplaceID", SqlDbType.VarChar, 12,workplaceID),
                                    _NewCyclePlan.CreateParameter("@sectionid", SqlDbType.VarChar,20 ,sectionid),
                                    _NewCyclePlan.CreateParameter("@sectionidMO", SqlDbType.VarChar,100,sectionidMO),
                                    _NewCyclePlan.CreateParameter("@ProdMonth", SqlDbType.Int ,0 ,ProdMonth ),
                                    _NewCyclePlan.CreateParameter("@Activity", SqlDbType.Int ,0 ,Activity ),
                                    _NewCyclePlan.CreateParameter("@FL", SqlDbType.Int ,0 ,FL ),
                                };

            _NewCyclePlan.ParamCollection = _paramCollection;

            clsDataResult exr = _NewCyclePlan.ExecuteInstruction();
            if (exr.success)
            {
                planningCycle.LoadPlanningCycleData(_NewCyclePlan.ResultsDataTable);
            }

        }




    }
}
