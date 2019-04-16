using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan.DataClass
{
    class clChangeOfPlanData
    {

        private int _requestType = -1;
        private int _ProdMonth = 0;
        private string _WorkplaceID = "";
        private string _WorplaceName = "";
        private string _SectionID = "0";
        private string _SectionID_2 = "0";
        private string _DayCrew = "";
        private string _NightCrew = "";
        private string _AfternoonCrew = "";
        private string _RovingCrew = "";
        private DateTime _StartDate = new DateTime();
        private DateTime _StopDate = new DateTime();
        private string _UserComments = "";
        private string _RequestBy = "";
        private int _SQMOn = 0;
        private int _SQMOff = 0;
        private double _Cube = 0;
        private double _MeterOn = 0;
        private double _MeterOff = 0;
        private string _MiningMethod = "";
        private string _OldWorkplaceID = "";
        private int _activity = -1;
        private int _Facelength = 0;
        private string _DrillRig = "";
        private Boolean _DeleteBookings = false;

        internal void SetRequestType(int requestType)
        {
            _requestType = requestType;
        }
        internal void SetProdMonth(int prodMonth)
        {
            _ProdMonth = prodMonth;
        }
        internal void SetWorkplaceID(string workplaceID)
        {
            _WorkplaceID = workplaceID;
        }
        internal void SetWorplaceName(string worplaceName)
        {
            _WorplaceName = worplaceName;
        }
        internal void SetSectionID(string sectionID)
        {
            _SectionID = sectionID;
        }
        internal void SetSectionID_2(string sectionID_2)
        {
            _SectionID_2 = sectionID_2;
        }
        internal void SetDayCrew(string dayCrew)
        {
            _DayCrew = dayCrew;
        }
        internal void SetNightCrew(string nightCrew)
        {
            _NightCrew = nightCrew;
        }
        internal void SetAfternoonCrew(string afternoonCrew)
        {
            _AfternoonCrew = afternoonCrew;
        }
        internal void SetRovingCrew(string rovingCrew)
        {
            _RovingCrew = rovingCrew;
        }
        internal void SetStartDate(DateTime startDate)
        {
            _StartDate = startDate;
        }
        internal void SetStopDate(DateTime stopDate)
        {
            _StopDate = stopDate;
        }
        internal void SetUserComments(string userComments)
        {
            _UserComments = userComments;
        }
        internal void SetRequestBy(string requestBy)
        {
            _RequestBy = requestBy;
        }
        internal void SetSQMOn(int sQMOn)
        {
            _SQMOn = sQMOn;
        }
        internal void SetSQMOff(int sQMOff)
        {
            _SQMOff = sQMOff;
        }
        internal void SetCube(double cube)
        {
            _Cube = cube;
        }
        internal void SetMeterOn(double meterOn)
        {
            _MeterOn = meterOn;
        }
        internal void SetMeterOff(double meterOff)
        {
            _MeterOff = meterOff;
        }
        internal void SetMiningMethod(string miningMethod)
        {
            _MiningMethod = miningMethod;
        }
        internal void SetDrillRig(string DrillRig)
        {
            _DrillRig = DrillRig;
        }
        internal void SetOldWorkplaceID(string oldWorkplaceID)
        {
            _OldWorkplaceID = oldWorkplaceID;
        }
        internal void SetActivity(int activity)
        {
            _activity = activity;
        }
        internal void SetFacelength(int facelength)
        {
            _Facelength = facelength;
        }

        internal void SetDeleteBookings(Boolean DeleteBookings)
        {
            _DeleteBookings = DeleteBookings;
        }

        public void sendRequest(string theSystemDBTag, string userConnection)
        {
            MWDataManager.clsDataAccess _sendRequest = new MWDataManager.clsDataAccess();
            _sendRequest.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, userConnection);
            _sendRequest.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _sendRequest.queryReturnType = MWDataManager.ReturnType.DataTable;
            _sendRequest.SqlStatement = "sp_RevisedPLanning_Request";



                SqlParameter[] _paramCollectionS = 
                            {
                             _sendRequest.CreateParameter("@requestType", SqlDbType.Int, 0,_requestType ),
                             _sendRequest.CreateParameter("@ProdMonth", SqlDbType.Int, 0,_ProdMonth ),
                             _sendRequest.CreateParameter("@WorkplaceID", SqlDbType.VarChar , 50,_WorkplaceID  ),
                             _sendRequest.CreateParameter("@SectionID", SqlDbType.VarChar, 20,_SectionID),
                             _sendRequest.CreateParameter("@SectionID_2", SqlDbType.VarChar, 20,_SectionID_2),
                             _sendRequest.CreateParameter("@DayCrew",SqlDbType .VarChar ,50,_DayCrew ),
                             _sendRequest.CreateParameter("@NightCrew",SqlDbType .VarChar ,50,_NightCrew ),
                             _sendRequest.CreateParameter("@AfternoonCrew",SqlDbType .VarChar ,50,_AfternoonCrew ),
                             _sendRequest.CreateParameter("@RovingCrew",SqlDbType .VarChar ,50,_RovingCrew ),
                             _sendRequest.CreateParameter("@StartDate", SqlDbType.VarChar, 20,String.Format("{0:yyy/MM/dd}", _StartDate)),
                             _sendRequest.CreateParameter("@StopDate",SqlDbType.VarChar, 20,String.Format("{0:yyy/MM/dd}" ,_StopDate )),
                             _sendRequest.CreateParameter("@UserComments", SqlDbType.VarChar, 600,_UserComments ),
                             _sendRequest.CreateParameter("@RequestBy", SqlDbType.VarChar, 20,TUserInfo.UserID),
                             _sendRequest.CreateParameter("@SQMOn", SqlDbType.Float, 0,_SQMOn),
                             _sendRequest.CreateParameter("@SQMOff", SqlDbType.Float, 0,_SQMOff),
                             _sendRequest.CreateParameter("@Cube", SqlDbType.Float, 0,_Cube),
                             _sendRequest.CreateParameter("@MeterOn", SqlDbType.Float, 0,_MeterOn),
                             _sendRequest.CreateParameter("@MeterOff", SqlDbType.Float, 0,_MeterOff),
                             _sendRequest.CreateParameter("@MiningMethod",SqlDbType .VarChar ,20,_MiningMethod),
                             _sendRequest.CreateParameter("@OldWorkplaceID", SqlDbType .VarChar ,50, _OldWorkplaceID),
                             _sendRequest.CreateParameter("@activity", SqlDbType .Int ,0, _activity),
                             _sendRequest.CreateParameter("@Facelength", SqlDbType.Int, 0,_Facelength ),
                             _sendRequest.CreateParameter("@DrillRig", SqlDbType.VarChar, 150,_DrillRig),
                             _sendRequest.CreateParameter("@DeleteBookings", SqlDbType.Bit, 150,_DeleteBookings)
                            };

                StringBuilder tempstring = new StringBuilder();
                foreach (SqlParameter a in _paramCollectionS)
                {
                    //x += 1;
                    tempstring.AppendLine(a.ParameterName.ToString() + " = " + a.SqlValue.ToString() + ",");
                }

                _sendRequest.ParamCollection = _paramCollectionS;
                clsDataResult DataResults = _sendRequest.ExecuteInstruction();

            if (DataResults.success == false)
            {
                MessageBox.Show(DataResults.Message, "Error sending request!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string getSectionID_2for_Miner(string theSystemDBTag, string userConnection, string MinerSectionID, string Prodmonth)
        {

            string Value = "";
            MWDataManager.clsDataAccess _Query = new MWDataManager.clsDataAccess();
            _Query.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, userConnection);
            _Query.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _Query.queryReturnType = MWDataManager.ReturnType.DataTable;
            _Query.SqlStatement = "Select SectionID_2 from Section_Complete where Prodmonth = " + Prodmonth + " and SectionID = '" + MinerSectionID + "'";
            _Query.ExecuteInstruction();

            if (_Query.ResultsDataTable.Rows.Count != 0 )
            { 
  
                Value = _Query.ResultsDataTable.Rows[0][0].ToString();

            }

            return Value;

        }

    }
}
