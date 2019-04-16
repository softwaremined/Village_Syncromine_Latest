using System;
using System.Linq;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Reports.SurveyReport
{
    public class clsSurveyReportSettings
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        public string _DBTag;
        public string DBTag { get { return _DBTag; } set { _DBTag = value; } }
        public string _UserCurrentInfo;
        public string UserCurrentInfo { get { return _UserCurrentInfo; } set { _UserCurrentInfo = value; } }

        private string theFromMonth;
        private string theToMonth;
        private string theHierID;

        private DateTime _FromMonth;
        public DateTime FromMonth { get { return _FromMonth; } set { _FromMonth = value; } }
        private DateTime _ToMonth;
        public DateTime ToMonth { get { return _ToMonth; } set { _ToMonth = value; } }

        private string _ReefDesc;
        public string ReefDesc { get { return _ReefDesc; } set { _ReefDesc = value; } }
        private string _ShaftDesc;
        public string ShaftDesc { get { return _ShaftDesc; } set { _ShaftDesc = value; } }
        private string _SignOffsDesc;
        public string SignOffsDesc { get { return _SignOffsDesc; } set { _SignOffsDesc = value; } }
        private string _CleanTypeDesc;
        public string CleanTypeDesc { get { return _CleanTypeDesc; } set { _CleanTypeDesc = value; } }

        private bool _Reclamation;
        public bool Reclamation { get { return _Reclamation; } set { _Reclamation = value; } }
        private bool _HighLight;
        public bool HighLight { get { return _HighLight; } set { _HighLight = value; } }

        private bool _Paid;
        private bool _Unpaid;
        private bool _PaidUnpaid;
        public bool Paid
        {
            get
            {
                return _Paid;
            }
            set
            {
                if (_Paid != true && value == true)
                {
                    _Paid = value;
                    _Unpaid = false;
                    _PaidUnpaid = false;
                }
            }
        }
        
        public bool Unpaid
        {
            get
            {
                return _Unpaid;
            }
            set
            {
                if (_Unpaid != true && value == true)
                {
                    _Unpaid = value;
                    _Paid = false;
                    _PaidUnpaid = false;
                }
            }
        }
        public bool PaidUnpaid
        {
            get
            {
                return _PaidUnpaid;
            }
            set
            {
                if (_PaidUnpaid != true && value == true)
                {
                    _PaidUnpaid = value;
                    _Paid = false;
                    _Unpaid = false;
                }
            }
        }

        private bool _ReportTypeStp;
        private bool _ReportTypeDev;
        private bool _ReportTypeSweeps;
        private bool _ReportTypeTM;

        public bool ReportTypeStp
        {
            get
            {
                return _ReportTypeStp;
            }
            set
            {
                if (_ReportTypeStp != true && value == true)
                {
                    _ReportTypeStp = value;
                    _ReportTypeDev = false;
                    _ReportTypeSweeps = false;
                    _ReportTypeTM = false;
                }
            }
        }

        public bool ReportTypeDev
        {
            get
            {
                return _ReportTypeDev;
            }
            set
            {
                if (_ReportTypeDev != true && value == true)
                {
                    _ReportTypeDev = value;
                    _ReportTypeStp = false;
                    _ReportTypeSweeps = false;
                    _ReportTypeTM = false;
                }
            }
        }
        public bool ReportTypeSweeps
        {
            get
            {
                return _ReportTypeSweeps;
            }
            set
            {
                if (_ReportTypeSweeps != true && value == true)
                {
                    _ReportTypeSweeps = value;
                    _ReportTypeStp = false;
                    _ReportTypeDev = false;
                    _ReportTypeTM = false;
                }
            }
        }
        public bool ReportTypeTM
        {
            get
            {
                return _ReportTypeTM;
            }
            set
            {
                if (_ReportTypeTM != true && value == true)
                {
                    _ReportTypeTM = value;
                    _ReportTypeStp = false;
                    _ReportTypeDev = false;
                    _ReportTypeSweeps = false;
                }
            }
        }
        private bool _DisplayID;
        private bool _DisplayName;
        private bool _DisplayIDName;
        public bool DisplayID
        {
            get
            {
                return _DisplayID;
            }
            set
            {
                if (_DisplayID != true && value == true)
                {
                    _DisplayID = value;
                    _DisplayName = false;
                    _DisplayIDName = false;
                }
            }
        }
        public bool DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                if (_DisplayName != true && value == true)
                {
                    _DisplayName = value;
                    _DisplayID = false;
                    _DisplayIDName = false;
                }
            }
        }
        public bool DisplayIDName
        {
            get
            {
                return _DisplayIDName;
            }
            set
            {
                if (_DisplayIDName != true && value == true)
                {
                    _DisplayIDName = value;
                    _DisplayID = false;
                    _DisplayName = false;
                }
            }
        }

        private bool _LvlGM;
        private bool _LvlMM;
        private bool _LvlMNM;
        private bool _LvlMO;
        private bool _LvlSB;
        private bool _LvlMiner;
        private bool _LvlWP;

        public bool LvlGM { get { return _LvlGM; } set { _LvlGM = value; } }
        public bool LvlMM { get { return _LvlMM; } set { _LvlMM = value; } }
        public bool LvlMNM { get { return _LvlMNM; } set { _LvlMNM = value; } }
        public bool LvlMO { get { return _LvlMO; } set { _LvlMO = value; } }
        public bool LvlSB { get { return _LvlSB; } set { _LvlSB = value; } }
        public bool LvlMiner { get { return _LvlMiner; } set { _LvlMiner = value; } }
        public bool LvlWP { get { return _LvlWP; } set { _LvlWP = value; } }

        private string _SectionID;
        public string SectionID
        {
            get
            {
                return _SectionID;
            }
            set
            {
                _SectionID = value;

                if (Convert.ToInt32(_FromMonth.Month.ToString()) < 10)
                {
                    theFromMonth = _FromMonth.Year.ToString() + "0" + _FromMonth.Month.ToString();
                }
                else
                {
                    theFromMonth = _FromMonth.Year.ToString() + _FromMonth.Month.ToString();
                }
                if (Convert.ToInt32(_ToMonth.Month.ToString()) < 10)
                {
                    theToMonth = _ToMonth.Year.ToString() + "0" + _ToMonth.Month.ToString();
                }
                else
                {
                    theToMonth = _ToMonth.Year.ToString() + _ToMonth.Month.ToString();
                }

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(DBTag, UserCurrentInfo);
                _dbMan.SqlStatement = " select HierarchicalID, Name from Section where " +
                                        " ProdMonth >= '" + theFromMonth + "' and " +
                                        " ProdMonth <= '" + theToMonth + "' and " +
                                        " sectionID  = '" + _SectionID + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                clsDataResult errorMsg = _dbMan.ExecuteInstruction();
                if (errorMsg.success == false)
                {
                    _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSurveyReportSettings", "Section", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                }
                else
                {
                    theHierID = "1";
                    if (_dbMan.ResultsDataTable.Rows.Count > 0)
                    {
                        theHierID = _dbMan.ResultsDataTable.Rows[0]["HierarchicalID"].ToString();
                    }


                    _LvlGM = true;
                    _LvlMM = true;
                    _LvlMNM = true;
                    _LvlMO = true;
                    _LvlSB = true;
                    _LvlMiner = true;
                    _LvlWP = true;


                    if (theHierID == "2")
                    {
                        _LvlGM = false;
                    }
                    if (theHierID == "3")
                    {
                        _LvlGM = false;
                        _LvlMM = false;
                    }
                    if (theHierID == "4")
                    {
                        _LvlGM = false;
                        _LvlMM = false;
                        _LvlMNM = false;
                    }
                    if (theHierID == "5")
                    {
                        _LvlGM = false;
                        _LvlMM = false;
                        _LvlMNM = false;
                        _LvlMO = false;
                    }

                    if (theHierID == "6")
                    {
                        _LvlGM = false;
                        _LvlMM = false;
                        _LvlMNM = false;
                        _LvlMO = false;
                        _LvlSB = false;
                    }

                    if (theHierID == "7")
                    {
                        _LvlGM = false;
                        _LvlMM = false;
                        _LvlMNM = false;
                        _LvlMO = false;
                        _LvlSB = false;
                        _LvlMiner = false;
                    }
                }
            }
        }
    }
}
