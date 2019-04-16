using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using System.IO;
using System.Text.RegularExpressions;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.BusinessPlanImport
{
    public partial class ucBusinessPlanImport : ucBaseUserControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        clsBusinessPlanImport _clsBusPlanImp = new clsBusinessPlanImport();
        OpenFileDialog f = new OpenFileDialog();
        SaveFileDialog f_err = new SaveFileDialog();

        protected DataTable tblXMReport;
        protected DataTable tblXMReport1;

        private DataTable dtBusLock;
        private DataTable dtOpsPlan;
        private DataTable dtTempBuss;
        private DataTable dtDeleteTempBuss;
        private DataTable dtDeleteBuss;
        private DataTable dtDelete;
        private DataTable dtSection;
        private DataTable dtWorkplace;
        private DataTable dtSaveBussPlan;

        private string strWorkplaceID;
        private string strSectionID;
        private string strProdMonth;

        private string strWorkplaceID1;
        private string strSectionID1;
        private string strProdMonth1;

        private string stpReefSqmLedge;
        private string stpOSSqmLedge;
        private string stpOSFSqmLedge;
        private string stpReefSqmStope;
        private string stpOSSqmStope;
        private string stpOSFSqmStope;
        private string stpSWFault;
        private string stpSW;
        private string stpFLReef;
        private string stpFLOS;
        private string stpDens;
        private string stpCmgt;
        private string stpCW;
        private string stpCubics;

        private string devAdvReef;
        private string devAdvWaste;
        private string devHeight;
        private string devWidth;
        private string devDens;
        private string devCmgt;
        private string devCubics;
        private string devIndicator;
        private string devMinigMethod;

        //private bool FoundErr;

        private bool ErrorFound;
        private string ErrorMsg;

        private string[] strFileLines;
        //string[] strFileLines;
        private string errorFilePathName;

        private int nProdMonthStart = 0;
        private int nProdMonthEnd = 0;

        private string strRegexOnlyNumbers = @"^[\d.]+$";   //@"^[0-9] + ([.])?$";
        private string strRegexDecimal1Place = @"^[0-9]*(\.\d{0,1})?$";
        private string strRegexDecimal10Place = @"^[0-9]*(\.\d{0,10})?$";
        private string strRegexDecimal2Places = @"^[0-9]*(\.\d{0,2})?$";
        private string strRegexInt = @"^(\+|-)?\d+$";

        string[] strColumnLettersArray = @"A B C D E F G H I J K L M N O P Q R S T U V W X Y Z 
                    AA AB AC AD AE AF AG AH AI AJ AK AL AM AN AO AP AQ AR AS AT AU AV AW AX AY AZ 
                    BA BB BC BD BE BF BG BH BI BJ BK BL BM BN BO BP BQ BR BS BT BU BV BW BX BY BZ 
                    CA CB CC CD CE CF CG CH CI CJ CK CL CM CN CO CP CQ CR CS CT CU CV CW CX CY CZ 
                    DA DB DC DD DE DF DG DH DI DJ DK DL DM DN DO DP DQ DR DS DT DU DV DW DX DY DZ 
                    EA EB EC ED EE EF EG EH EI EJ EK EL EM EN EO EP EQ ER ES ET EU EV EW EX EY EZ 
                    FA FB FC FD FE FF FG FH FI FJ FK FL FM FN FO FP FQ FR FS FT FU FV FW FX FY FZ 
                    GA GB GC GD GE GF GG GH GI GJ GK GL GM GN GO GP GQ GR GS GT GU GV GW GX GY GZ 
                    HA HB HC HD HE HF HG HH HI HJ HK HL HM HN HO HP HQ HR HS HT HU HV HW HX HY HZ".Split(' ');

        private int nCount;
        private int nStart;

        public ucBusinessPlanImport()
        {
            InitializeComponent();
        }
        private void ucBusinessPlanImport_Load(object sender, EventArgs e)
        {
            barActivity.EditValue = "Stoping";
        }
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dispose();
        }
        private void btnBrowse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f.InitialDirectory = Application.ExecutablePath;

            if (f.ShowDialog() == DialogResult.OK)
                if (f.FileName != null && f.CheckFileExists == true)
                {
                    if (Path.GetExtension(f.FileName) != ".csv")
                    {
                        _sysMessagesClass.viewMessage(MessageType.Info, "Business Plan Import", "The file you have chosen does not seem to be a CSV file. Please try again. ", ButtonTypes.OK, MessageDisplayType.Small);
                        btnImportFile.Enabled = false;
                    }
                    else
                    {
                        string inputPath = f.FileName;
                        barFileName.EditValue = f.FileName;
                        btnImportFile.Enabled = true;
                    }                   
                }
        }
        private void ValidDate_File()
        {
            /*ErrorFound = false;
            if (barFileName.EditValue == null)
            {
                ErrorMsg = "Please browse for a file to Import first. ";
                ErrorFound = true;
            }
            else
            {
                if (barFileName.EditValue.ToString() == "")
                {
                    ErrorMsg = "Please browse for a file to Import first. ";
                    ErrorFound = true;
                }
            }
            if (ErrorFound == false)
            {
                if (Path.GetExtension(f.FileName) != ".csv")
                {
                    ErrorMsg = "The file you have chosen does not seem to be a CSV file. Please try again. ";
                    btnImportFile.Enabled = false;
                    ErrorFound = true;
                }
            }
            if (ErrorFound == false)
            {
                try
                {
                    strFileLines = File.ReadAllLines(barFileName.EditValue.ToString());
                }
                catch
                {
                    ErrorMsg = "There was a error trying to open the file. Please make sure the file is not open somewhere else (Excel) and try again.";
                    ErrorFound = true;
                }
            }*/
        }
        private void barImportFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool ErrorFound = false;
            if ((barActivity.EditValue.ToString() != "Stoping") &&
               (barActivity.EditValue.ToString() != "Development"))
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "Please select either Stoping or Development", ErrorMsg, ButtonTypes.OK, MessageDisplayType.Small);
                ErrorFound = true;
            }
            if (ErrorFound == false)
            {
                if (barFileName.EditValue == null)
                {
                    _sysMessagesClass.viewMessage(MessageType.Error, "Business Plan Import", "Please browse for a file to Import first", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrorFound = true;
                }
            }
            if (ErrorFound == false)
            {
                if (barFileName.EditValue.ToString() == "")
                { 
                    _sysMessagesClass.viewMessage(MessageType.Error, "Business Plan Import", "Please browse for a file to Import first", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrorFound = true;
                }
            }
            if (ErrorFound == false)
            {
                DialogResult theResult;
                theResult = MessageBox.Show("Are you sure you want to Import this File : " + barFileName.EditValue.ToString() + " ? ", "Create Audit", MessageBoxButtons.YesNo);
                if (theResult == DialogResult.Yes)
                {
                    /*ValidDate_File();
                    if (ErrorFound == true)
                        _sysMessagesClass.viewMessage(MessageType.Info, "Business Plan Import", ErrorMsg, ButtonTypes.OK, MessageDisplayType.Small);
                    else
                    {*/
                    //Do_ImportClick_MO();*/
                    Do_ImportClick_WP();
                    // }
                }
            }
        }
        private void Do_ImportClick_MO()
        { 
            bool bFirst = true;
            string strMissingWorkplaces = "";
            string strMissingSections = "";
            string strErrorMessage = "";
            int nProdMonthStart = 0;
            int nProdMonthEnd = 0;
            bool bDoesHaveErrors = false;
            int nCount = 0;
            int nActivity = 0;
            List<string> lstQueries = new List<string>();
            string nProdMonthStart1 = "";
            string nProdMonthEnd1 = "";

            string strRegexDecimal1Place = @"^[0-9]*(\.\d{0,1})?$";
            string strRegexDecimal2Places = @"^[0-9]*(\.\d{0,2})?$";
            string strRegexInt = @"^(\+|-)?\d+$";


            string[] strColumnLettersArray = @"A B C D E F G H I J K L M N O P Q R S T U V W X Y Z 
AA AB AC AD AE AF AG AH AI AJ AK AL AM AN AO AP AQ AR AS AT AU AV AW AX AY AZ 
BA BB BC BD BE BF BG BH BI BJ BK BL BM BN BO BP BQ BR BS BT BU BV BW BX BY BZ 
CA CB CC CD CE CF CG CH CI CJ CK CL CM CN CO CP CQ CR CS CT CU CV CW CX CY CZ 
DA DB DC DD DE DF DG DH DI DJ DK DL DM DN DO DP DQ DR DS DT DU DV DW DX DY DZ 
EA EB EC ED EE EF EG EH EI EJ EK EL EM EN EO EP EQ ER ES ET EU EV EW EX EY EZ 
FA FB FC FD FE FF FG FH FI FJ FK FL FM FN FO FP FQ FR FS FT FU FV FW FX FY FZ 
GA GB GC GD GE GF GG GH GI GJ GK GL GM GN GO GP GQ GR GS GT GU GV GW GX GY GZ 
HA HB HC HD HE HF HG HH HI HJ HK HL HM HN HO HP HQ HR HS HT HU HV HW HX HY HZ".Split(' ');

            MWDataManager.clsDataAccess _MWDBMan = new MWDataManager.clsDataAccess();
            StringBuilder sb = new StringBuilder();

            tblXMReport = new DataTable();

            tblXMReport.Columns.Add("VERSION_DSC");
            tblXMReport.Columns.Add("Problem");
            tblXMReport.Columns.Add("PROJECT_NO");
            tblXMReport.Columns.Add("ACCOUNT_NO");
            tblXMReport.Columns.Add("Prodmonth");

            tblXMReport.Columns.Add("SOURCE_IND");
            tblXMReport.Columns.Add("BUD_VAL_TYPE");
            tblXMReport.Columns.Add("BUD_VAL");
            tblXMReport.Columns.Add("Line");
           // dtDelete = _clsBusPlanImp.delete_Plan();

           // sb.Clear();
           // //MWDataManager.clsDataAccess _MWDBMan = new MWDataManager.clsDataAccess();
           //// StringBuilder sb = new StringBuilder();
           // sb.AppendLine("delete from tempBusPlanImport");
           // _MWDBMan.SqlStatement = sb.ToString();
           // _MWDBMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
           // _MWDBMan.queryReturnType = MWDataManager.ReturnType.DataTable;
           // _MWDBMan.ExecuteInstruction();
           // //_clsBusPlanImp.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
           // //bool theSaveSIC;
           // //theSaveSIC = _clsBusPlanImp.delete_TempBus();
           // //dtDeleteTempBuss = _clsBusPlanImp.delete_TempBus();// _clsBusPlanImp.delete_TempBus();

            foreach (string strCurrentLine in strFileLines)
            {
                if (bFirst)
                {
                    bFirst = false;
                    continue;
                }
                string strWorkplaceID = strCurrentLine.Split(',')[0];
                string strSectionID = strCurrentLine.Split(',')[1];
                string strProd1 = strCurrentLine.Split(',')[7];
                string strProd = strProd1.Replace("\"", "");

                string strProdMonth = "";

                if (strProd.Substring(4, 3) == "Jan")
                {
                    strProdMonth = strProd.Substring(0, 4) + "01";
                }
                if (strProd.Substring(4, 3) == "Feb")
                {
                    strProdMonth = strProd.Substring(0, 4) + "02";
                }
                if (strProd.Substring(4, 3) == "Mar")
                {
                    strProdMonth = strProd.Substring(0, 4) + "03";
                }
                if (strProd.Substring(4, 3) == "Apr")
                {
                    strProdMonth = strProd.Substring(0, 4) + "04";
                }
                if (strProd.Substring(4, 3) == "May")
                {
                    strProdMonth = strProd.Substring(0, 4) + "05";
                }
                if (strProd.Substring(4, 3) == "Jun")
                {
                    strProdMonth = strProd.Substring(0, 4) + "06";
                }
                if (strProd.Substring(4, 3) == "Jul")
                {
                    strProdMonth = strProd.Substring(0, 4) + "07";
                }
                if (strProd.Substring(4, 3) == "Aug")
                {
                    strProdMonth = strProd.Substring(0, 4) + "08";
                }
                if (strProd.Substring(4, 3) == "Sep")
                {
                    strProdMonth = strProd.Substring(0, 4) + "09";
                }
                if (strProd.Substring(4, 3) == "Oct")
                {
                    strProdMonth = strProd.Substring(0, 4) + "10";
                }
                if (strProd.Substring(4, 3) == "Nov")
                {
                    strProdMonth = strProd.Substring(0, 4) + "11";
                }
                if (strProd.Substring(4, 3) == "Dec")
                {
                    strProdMonth = strProd.Substring(0, 4) + "12";
                }

                if (!string.IsNullOrEmpty(strProdMonth))
                {
                    if (nProdMonthStart == 0)
                        nProdMonthStart = Convert.ToInt32(strProdMonth);

                    if (Convert.ToInt32(strProdMonth) < nProdMonthStart)
                        nProdMonthStart = Convert.ToInt32(strProdMonth);

                    if (nProdMonthEnd == 0
                        || Convert.ToInt32(strProdMonth) > nProdMonthEnd)
                        nProdMonthEnd = Convert.ToInt32(strProdMonth);
                }
            }

            DateTime? dtStartMonth = new DateTime(Convert.ToInt32(nProdMonthStart.ToString().Substring(0, 4)),
                Convert.ToInt32(nProdMonthStart.ToString().Substring(4, 2)),
                1);

            bFirst = true;
            int nYear = Convert.ToInt32(nProdMonthEnd.ToString().Substring(0, 4));

            _clsBusPlanImp.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtBusLock = _clsBusPlanImp.get_BussPlanLock(nYear, 0);

            if (dtBusLock.Rows.Count != 0)
            {
                if (Convert.ToBoolean(dtBusLock.Rows[0]["IsLocked"]))
                {
                    MessageBox.Show("The Business Plan you are trying to import is locked, which means no futher imports are possible for this Year.",
                        "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (barActivity.EditValue.ToString() == "Production")
            {
                foreach (string strCurrentLine in strFileLines)
                {
                    nCount++;

                    string strSectionID1 = strCurrentLine.Split(',')[3];

                    string strProd1 = strCurrentLine.Split(',')[7];
                    string strProd = strProd1.Replace("\"", "");

                    string strProdMonth1 = "";

                    if (strProd.Substring(4, 3) == "Jan")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "01";
                    }
                    if (strProd.Substring(4, 3) == "Feb")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "02";
                    }
                    if (strProd.Substring(4, 3) == "Mar")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "03";
                    }
                    if (strProd.Substring(4, 3) == "Apr")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "04";
                    }
                    if (strProd.Substring(4, 3) == "May")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "05";
                    }
                    if (strProd.Substring(4, 3) == "Jun")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "06";
                    }
                    if (strProd.Substring(4, 3) == "Jul")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "07";
                    }
                    if (strProd.Substring(4, 3) == "Aug")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "08";
                    }
                    if (strProd.Substring(4, 3) == "Sep")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "09";
                    }
                    if (strProd.Substring(4, 3) == "Oct")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "10";
                    }
                    if (strProd.Substring(4, 3) == "Nov")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "11";
                    }
                    if (strProd.Substring(4, 3) == "Dec")
                    {
                        strProdMonth1 = strProd.Substring(0, 4) + "12";
                    }

                    dtOpsPlan = _clsBusPlanImp.get_OpsPlan(strProdMonth1, strSectionID1.Replace("\"", ""));

                    if (dtOpsPlan.Rows.Count == 0)
                    {

                        int nStart = 0;
                        string strWorkplaceID = strCurrentLine.Split(',')[4];

                        string VERSION_DSC = strCurrentLine.Split(',')[0];
                        string VERSION_DSC1 = VERSION_DSC.Replace("\"", "");
                        string COMPANY_NAME = strCurrentLine.Split(',')[1];
                        string COMPANY_NAME1 = COMPANY_NAME.Replace("\"", "");
                        string ACCOUNT_NO = strCurrentLine.Split(',')[2];
                        string ACCOUNT_NO1 = ACCOUNT_NO.Replace("\"", "");

                        string PROJECT_NO = strCurrentLine.Split(',')[3];
                        string PROJECT_NO1 = PROJECT_NO.Replace("\"", "");

                        string PROJECT_TASK = strCurrentLine.Split(',')[4];
                        string PROJECT_TASK1 = PROJECT_TASK.Replace("\"", "");

                        string RESP_CENTER = strCurrentLine.Split(',')[5];
                        string RESP_CENTER1 = RESP_CENTER.Replace("\"", "");

                        string OCCUPATION_CD = strCurrentLine.Split(',')[6];
                        string OCCUPATION_CD1 = OCCUPATION_CD.Replace("\"", "");

                        string FISCAL_PERIOD = strCurrentLine.Split(',')[7];
                        string FISCAL_PERIOD1 = FISCAL_PERIOD.Replace("\"", "");

                        string SOURCE_IND = strCurrentLine.Split(',')[8];
                        string SOURCE_IND1 = SOURCE_IND.Replace("\"", "");

                        string BUD_VAL_TYPE = strCurrentLine.Split(',')[9];
                        string BUD_VAL_TYPE1 = BUD_VAL_TYPE.Replace("\"", "");


                        string BUD_VAL = strCurrentLine.Split(',')[10];
                        string BUD_VAL1 = BUD_VAL.Replace("\"", "");

                        string POST_ALLOC_BUD_VAL = strCurrentLine.Split(',')[11];
                        string POST_ALLOC_BUD_VAL1 = POST_ALLOC_BUD_VAL.Replace("\"", "");

                        dtOpsPlan = _clsBusPlanImp.get_OpsPlan(strSectionID1.Replace("\"", ""));

                        bDoesHaveErrors = true;
                        AddErrorToXMLTable(VERSION_DSC1, "Opsplanklink not found for Fiscal_Period.", strWorkplaceID,
                            strSectionID1, strProdMonth1, SOURCE_IND1, BUD_VAL_TYPE1, strCurrentLine.Split(',')[nStart + 11],
                            nCount);
                    }
                    else
                    {

                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "01")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Jan";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "02")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Feb";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "03")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Mar";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "04")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Apr";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "05")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "May";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "06")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Jun";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "07")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Jul";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "08")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Aug";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "09")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Sep";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "10")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Oct";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "11")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Nov";
                        }
                        if (Convert.ToString(nProdMonthStart).Substring(4, 2) == "12")
                        {
                            nProdMonthStart1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Dec";
                        }

                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "01")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthStart).Substring(0, 4) + "Jan";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "02")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "Feb";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "03")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "Mar";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "04")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "Apr";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "05")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "May";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "06")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "Jun";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "07")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "Jul";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "08")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "Aug";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "09")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "Sep";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "10")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "Oct";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "11")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "Nov";
                        }
                        if (Convert.ToString(nProdMonthEnd).Substring(4, 2) == "12")
                        {
                            nProdMonthEnd1 = Convert.ToString(nProdMonthEnd).Substring(0, 4) + "Dec";
                        }


                        string VERSION_DSC = strCurrentLine.Split(',')[0];
                        string VERSION_DSC1 = VERSION_DSC.Replace("\"", "");
                        string COMPANY_NAME = strCurrentLine.Split(',')[1];
                        string COMPANY_NAME1 = COMPANY_NAME.Replace("\"", "");
                        string ACCOUNT_NO = strCurrentLine.Split(',')[2];
                        string ACCOUNT_NO1 = ACCOUNT_NO.Replace("\"", "");

                        string PROJECT_NO = strCurrentLine.Split(',')[3];
                        string PROJECT_NO1 = PROJECT_NO.Replace("\"", "");

                        string PROJECT_TASK = strCurrentLine.Split(',')[4];
                        string PROJECT_TASK1 = PROJECT_TASK.Replace("\"", "");

                        string RESP_CENTER = strCurrentLine.Split(',')[5];
                        string RESP_CENTER1 = RESP_CENTER.Replace("\"", "");

                        string OCCUPATION_CD = strCurrentLine.Split(',')[6];
                        string OCCUPATION_CD1 = OCCUPATION_CD.Replace("\"", "");

                        string FISCAL_PERIOD = strCurrentLine.Split(',')[7];
                        string FISCAL_PERIOD1 = FISCAL_PERIOD.Replace("\"", "");

                        string SOURCE_IND = strCurrentLine.Split(',')[8];
                        string SOURCE_IND1 = SOURCE_IND.Replace("\"", "");

                        string BUD_VAL_TYPE = strCurrentLine.Split(',')[9];
                        string BUD_VAL_TYPE1 = BUD_VAL_TYPE.Replace("\"", "");
                        if (BUD_VAL_TYPE1 == "Kg's")
                        {
                            BUD_VAL_TYPE1 = "Kg";
                        }

                        string BUD_VAL = strCurrentLine.Split(',')[10];
                        string BUD_VAL1 = BUD_VAL.Replace("\"", "");

                        string POST_ALLOC_BUD_VAL = strCurrentLine.Split(',')[11];
                        string POST_ALLOC_BUD_VAL1 = POST_ALLOC_BUD_VAL.Replace("\"", "");
                        bool savedData = _clsBusPlanImp.save_BusPlan
                            (VERSION_DSC1, COMPANY_NAME1, ACCOUNT_NO1,
                             PROJECT_NO1, PROJECT_TASK1, RESP_CENTER1, OCCUPATION_CD1, FISCAL_PERIOD1,
                             SOURCE_IND1, BUD_VAL_TYPE1, BUD_VAL1, POST_ALLOC_BUD_VAL1);

                    }
                }
            }

            if (barActivity.EditValue.ToString() == "Production")
            {
                if (tblXMReport.Rows.Count > 0)
                {
                    _clsBusPlanImp.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    dtDeleteBuss = _clsBusPlanImp.save_DeleteOpsPlan(nProdMonthStart1, nProdMonthEnd1);
                    MessageBox.Show("The Business Plan you are trying to import has incorrect Project_No. Please correct the Project_No.",
                         "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (tblXMReport.Rows.Count != 0)
            {
                Save_ErrorFile();
            }
            else
            {
                sb.Clear();

                #region Delete existing entry

                sb.AppendLine("delete from BusinessPlan_Locks");
                sb.AppendLine("where Year = " + nYear);
                sb.AppendLine("and Activity = " + nActivity);


                #endregion

                sb.AppendLine("INSERT INTO BusinessPlan_Locks");
                sb.AppendLine("           ([Year]");
                sb.AppendLine("           ,[ProdMonthStart]");
                sb.AppendLine("           ,[ProdMonthEnd]");
                sb.AppendLine("           ,[IsLocked]");
                sb.AppendLine("           ,[Activity])");
                sb.AppendLine("     VALUES");
                sb.AppendLine("           (" + nYear);
                sb.AppendLine("           ," + nProdMonthStart);
                sb.AppendLine("           ," + nProdMonthEnd);
                sb.AppendLine("           ,'false'");
                sb.AppendLine("           ," + nActivity + ")");

                _MWDBMan.SqlStatement = sb.ToString();
                _MWDBMan.ExecuteInstruction();

                MessageBox.Show("Business Plan successfully imported.", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                _MWDBMan.SqlStatement = "select * from TM1Import where BUD_VAL_TYPE='Equipped Nr of Panels'";
                _MWDBMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _MWDBMan.ExecuteInstruction();
                if (_MWDBMan.ResultsDataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No Reclamation Panel fields found", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    _MWDBMan.SqlStatement = "Delete from TM1Import where BUD_VAL_TYPE='Equipped Nr of Panels'";
                    _MWDBMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _MWDBMan.ExecuteInstruction();
                    MessageBox.Show("Reclamation Panel fields excluded", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
        }
        private void Do_ImportClick_WP()
        {
            #region Variables

            bool bFirst = true;
            int nActivity = 0;
            List<string> lstQueries = new List<string>();

            #region Report Column Initiation

            tblXMReport1 = new DataTable();

            tblXMReport1.Columns.Add("WORKPLACEID");
            tblXMReport1.Columns.Add("SECTIONID");
            tblXMReport1.Columns.Add("PRODMONTH");
            tblXMReport1.Columns.Add("PROBLEM");
            tblXMReport1.Columns.Add("FIELD");
            tblXMReport1.Columns.Add("VALUE");
            tblXMReport1.Columns.Add("LINE");
            tblXMReport1.Columns.Add("SOURCE_IND");

            #endregion

            #endregion

            #region Validations

            try
            {
                strFileLines = File.ReadAllLines(barFileName.EditValue.ToString());
            }
            catch
            {
                ErrorMsg = "There was a error trying to open the file. Please make sure the file is not open somewhere else (Excel) and try again.";
                _sysMessagesClass.viewMessage(MessageType.Info, "Business Plan Import", ErrorMsg, ButtonTypes.OK, MessageDisplayType.Small);
                return;
            }

            #region Workplaces + Sections
            _clsBusPlanImp.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtDeleteTempBuss = _clsBusPlanImp.save_DeleteTempBussPlan();

            nCount = 1;
            foreach (string strCurrentLine in strFileLines)
            {
                if (bFirst)
                {
                    bFirst = false;
                    continue;
                }
                strWorkplaceID = strCurrentLine.Split(',')[0];
                strSectionID = strCurrentLine.Split(',')[1];
                strProdMonth = strCurrentLine.Split(',')[2];

                if (!string.IsNullOrEmpty(strProdMonth))
                {
                    if (nProdMonthStart == 0)
                        nProdMonthStart = Convert.ToInt32(strProdMonth);

                    if (Convert.ToInt32(strProdMonth) < nProdMonthStart)
                        nProdMonthStart = Convert.ToInt32(strProdMonth);

                    if (nProdMonthEnd == 0
                        || Convert.ToInt32(strProdMonth) > nProdMonthEnd)
                        nProdMonthEnd = Convert.ToInt32(strProdMonth);
                }
                if (strProdMonth.Length != 6)
                {
                    ErrorFound = true;
                    AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "ProdMonth",
                        strProdMonth, nCount.ToString(), "1");
                }
                //add sections that doesn't exist in Section Table into temp table
                //Its only to write one error record to the file
                dtSection = _clsBusPlanImp.get_Section(strProdMonth, strSectionID);
                if (dtSection.Rows.Count == 0)
                {
                    dtSection = _clsBusPlanImp.get_SectionInTempBuss(strProdMonth, strSectionID);

                    if (dtSection.Rows.Count == 0)
                    {
                        dtSection = _clsBusPlanImp.save_SectionInTempBuss(strProdMonth, strSectionID);
                        ErrorFound = true;
                        AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Section Not Found ", "SectionID",
                        strSectionID, nCount.ToString(), "1");
                    }
                }

                dtWorkplace = _clsBusPlanImp.get_Workplace(strWorkplaceID);

                if (dtWorkplace.Rows.Count == 0)
                {
                    dtWorkplace = _clsBusPlanImp.get_WorkplaceInTempBuss(strWorkplaceID);

                    if (dtWorkplace.Rows.Count == 0)
                    {
                        dtWorkplace = _clsBusPlanImp.save_WorkplaceInTempBuss(strProdMonth, strSectionID, strWorkplaceID);
                        ErrorFound = true;
                        AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Workplace Not Found ", "WorkplaceID",
                                strWorkplaceID, nCount.ToString(), "1");
                    }
                }
                nCount = nCount + 1;
            }

            #endregion



            #endregion

            //  #region Import

            DateTime? dtStartMonth = new DateTime(Convert.ToInt32(nProdMonthStart.ToString().Substring(0, 4)),
                Convert.ToInt32(nProdMonthStart.ToString().Substring(4, 2)),
                1);

            bFirst = true;


            #region
            if (barActivity.EditValue.ToString() == "Stoping")
                nActivity = 0;
            else
                nActivity = 1;
            dtDelete = _clsBusPlanImp.save_DeleteBussPlan(nActivity, nProdMonthStart.ToString(),
                                                            nProdMonthEnd.ToString());

            #endregion

            int nYear = Convert.ToInt32(nProdMonthEnd.ToString().Substring(0, 4));
            dtBusLock = _clsBusPlanImp.get_BussPlanLock(nYear, 0);

            if (dtBusLock.Rows.Count != 0)
            {
                if (Convert.ToBoolean(dtBusLock.Rows[0]["IsLocked"]))
                {
                    MessageBox.Show("The Business Plan you are trying to import is locked, which means no futher imports are possible for this Year.",
                        "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Mineware.Systems.ProductionGlobal.frmProgress theProgress = new Mineware.Systems.ProductionGlobal.frmProgress();
            theProgress.SetCaption("Importing Data");
            theProgress.SetProgresMax(nCount);
            theProgress.Show();
            int thePos = 1;
            foreach (string strCurrentLine in strFileLines)
            {
                if (bFirst)
                {
                    bFirst = false;
                    continue;
                }
                theProgress.SetProgressPosition(thePos);
                thePos = thePos + 1;
                strWorkplaceID = strCurrentLine.Split(',')[0];
                strSectionID = strCurrentLine.Split(',')[1];
                strProdMonth = strCurrentLine.Split(',')[2];

                dtTempBuss = _clsBusPlanImp.get_RecordInTempBuss(strProdMonth, strSectionID, strWorkplaceID);

                if (dtTempBuss.Rows.Count == 0)
                {
                    if (barActivity.EditValue.ToString() == "Stoping")
                    {
                        stpReefSqmLedge = strCurrentLine.Split(',')[3];
                        if (stpReefSqmLedge == "")
                            stpReefSqmLedge = "0";
                        stpOSSqmLedge = strCurrentLine.Split(',')[4];
                        if (stpOSSqmLedge == "")
                            stpOSSqmLedge = "0";
                        stpOSFSqmLedge = strCurrentLine.Split(',')[5];
                        if (stpOSFSqmLedge == "")
                            stpOSFSqmLedge = "0";
                        stpReefSqmStope = strCurrentLine.Split(',')[6];
                        if (stpReefSqmStope == "")
                            stpReefSqmStope = "0";
                        stpOSSqmStope = strCurrentLine.Split(',')[7];
                        if (stpOSSqmStope == "")
                            stpOSSqmStope = "0";
                        stpOSFSqmStope = strCurrentLine.Split(',')[8];
                        if (stpOSFSqmStope == "")
                            stpOSFSqmStope = "0";
                        stpSWFault = strCurrentLine.Split(',')[9];
                        if (stpSWFault == "")
                            stpSWFault = "0";
                        stpSW = strCurrentLine.Split(',')[10];
                        if (stpSW == "")
                            stpSW = "0";
                        stpFLReef = strCurrentLine.Split(',')[11];
                        if (stpFLReef == "")
                            stpFLReef = "0";
                        stpFLOS = strCurrentLine.Split(',')[12];
                        if (stpFLOS == "")
                            stpFLOS = "0";
                        stpDens = strCurrentLine.Split(',')[13];
                        if (stpDens == "")
                            stpDens = "0";
                        stpCmgt = strCurrentLine.Split(',')[14];
                        if (stpCmgt == "")
                            stpCmgt = "0";
                        stpCW = strCurrentLine.Split(',')[15];
                        if (stpCW == "")
                            stpCW = "0";
                        stpCubics = strCurrentLine.Split(',')[16];
                        if (stpCubics == "")
                            stpCubics = "0";

                        ErrorFound = false;
                        Validate_Stoping();
                        double stpFL = Convert.ToDouble(stpFLReef) + Convert.ToDouble(stpFLOS);
                        double stpSqmStope = Convert.ToDouble(stpReefSqmStope) + Convert.ToDouble(stpOSSqmStope) +
                                            Convert.ToDouble(stpOSFSqmStope);
                        double stpSqmLedge = Convert.ToDouble(stpReefSqmLedge) + Convert.ToDouble(stpOSSqmLedge) +
                                            Convert.ToDouble(stpOSFSqmLedge);
                        double stpSqmWaste = Convert.ToDouble(stpOSSqmLedge) + Convert.ToDouble(stpOSFSqmLedge) +
                                            Convert.ToDouble(stpOSSqmStope) + Convert.ToDouble(stpOSFSqmStope);
                        double stpSqm = Convert.ToDouble(stpReefSqmStope) + Convert.ToDouble(stpOSSqmStope) +
                                        Convert.ToDouble(stpOSFSqmStope) +
                                        Convert.ToDouble(stpReefSqmLedge) + Convert.ToDouble(stpOSSqmLedge) +
                                        Convert.ToDouble(stpOSFSqmLedge);
                        double stpSqmWasteLedge = Convert.ToDouble(stpOSSqmLedge) + Convert.ToDouble(stpOSFSqmLedge);
                        double stpSqmWasteStope = Convert.ToDouble(stpOSSqmStope) + Convert.ToDouble(stpOSFSqmStope);

                        double stpOSTonsLedge = Convert.ToDouble(stpOSSqmLedge) * Convert.ToDouble(stpSW) / 100 *
                                                Convert.ToDouble(stpDens);
                        double stpOSTonsStope = Convert.ToDouble(stpOSSqmStope) * Convert.ToDouble(stpSW) / 100 *
                                                Convert.ToDouble(stpDens);
                        double stpOSFTonsLedge = Convert.ToDouble(stpOSFSqmLedge) * Convert.ToDouble(stpSW) / 100 *
                                                    Convert.ToDouble(stpDens);
                        double stpOSFTonsStope = Convert.ToDouble(stpOSFSqmStope) * Convert.ToDouble(stpSW) / 100 *
                                                    Convert.ToDouble(stpDens);
                        double stpReefTonsLedge = Convert.ToDouble(stpReefSqmLedge) * Convert.ToDouble(stpSW) / 100 *
                                                    Convert.ToDouble(stpDens);
                        double stpReefTonsStope = Convert.ToDouble(stpReefSqmStope) * Convert.ToDouble(stpSW) / 100 *
                                                    Convert.ToDouble(stpDens);
                        double stpWasteTonsLedge = (Convert.ToDouble(stpOSSqmLedge) + Convert.ToDouble(stpOSFSqmLedge)) *
                                                    Convert.ToDouble(stpSW) / 100 * Convert.ToDouble(stpDens);
                        double stpWasteTonsStope = (Convert.ToDouble(stpOSSqmStope) + Convert.ToDouble(stpOSFSqmStope)) *
                                                    Convert.ToDouble(stpSW) / 100 * Convert.ToDouble(stpDens);
                        double stpTonsOS = (Convert.ToDouble(stpOSSqmStope) + Convert.ToDouble(stpOSSqmLedge)) *
                                            Convert.ToDouble(stpSW) / 100 * Convert.ToDouble(stpDens);
                        double stpTonsOSF = (Convert.ToDouble(stpOSFSqmStope) + Convert.ToDouble(stpOSFSqmLedge)) *
                                                Convert.ToDouble(stpSW) / 100 * Convert.ToDouble(stpDens);
                        double stpTonsReef = (Convert.ToDouble(stpReefSqmStope) + Convert.ToDouble(stpReefSqmLedge)) *
                                                Convert.ToDouble(stpSW) / 100 * Convert.ToDouble(stpDens);
                        double stpTonsWaste = (Convert.ToDouble(stpOSFSqmStope) + Convert.ToDouble(stpOSFSqmLedge) +
                                                Convert.ToDouble(stpOSSqmStope) + Convert.ToDouble(stpOSSqmLedge)) *
                                                Convert.ToDouble(stpSW) / 100 * Convert.ToDouble(stpDens);
                        double stpTonsStope = (Convert.ToDouble(stpReefSqmStope) + Convert.ToDouble(stpOSSqmStope) +
                                                Convert.ToDouble(stpOSFSqmStope)) *
                                                Convert.ToDouble(stpSW) / 100 * Convert.ToDouble(stpDens);
                        double stpTonsLedge = (Convert.ToDouble(stpReefSqmLedge) + Convert.ToDouble(stpOSSqmLedge) +
                                                Convert.ToDouble(stpOSFSqmLedge)) *
                                                Convert.ToDouble(stpSW) / 100 * Convert.ToDouble(stpDens);
                        double stpTons = (Convert.ToDouble(stpReefSqmLedge) + Convert.ToDouble(stpOSSqmLedge) +
                                            Convert.ToDouble(stpOSFSqmLedge) + Convert.ToDouble(stpReefSqmStope) +
                                            Convert.ToDouble(stpOSSqmStope) + Convert.ToDouble(stpOSFSqmStope)) *
                                            Convert.ToDouble(stpSW) / 100 * Convert.ToDouble(stpDens);
                        double stpContent = (Convert.ToDouble(stpReefSqmStope) + Convert.ToDouble(stpReefSqmLedge)) *
                            Convert.ToDouble(stpCmgt) * Convert.ToDouble(stpDens) / 100;
                        double stpContentLedge = Convert.ToDouble(stpReefSqmLedge) *
                                                    Convert.ToDouble(stpCmgt) * Convert.ToDouble(stpDens) / 100;
                        double stpContentStope = Convert.ToDouble(stpReefSqmStope) *
                                                    Convert.ToDouble(stpCmgt) * Convert.ToDouble(stpDens) / 100;
                        if (ErrorFound == false)
                        {
                            bool _saverecord  = _clsBusPlanImp.save_Stoping(strWorkplaceID, strSectionID, strProdMonth,
                                  stpReefSqmLedge, stpOSSqmLedge, stpOSFSqmLedge,
                                  stpReefSqmStope, stpOSSqmStope, stpOSFSqmStope,
                                  stpSqmWasteLedge.ToString(), stpSqmWasteStope.ToString(),
                                  stpSqmWaste.ToString(), stpSqmLedge.ToString(), stpSqmStope.ToString(), stpSqm.ToString(),
                                  stpSWFault, stpSW,
                                  stpFLReef, stpFLOS, stpFL.ToString(),
                                  stpDens, stpCmgt, stpCW,
                                  stpCubics,
                                  stpReefTonsLedge.ToString(), stpOSTonsLedge.ToString(), stpOSFTonsLedge.ToString(),
                                  stpReefTonsStope.ToString(), stpOSTonsStope.ToString(), stpOSFTonsStope.ToString(),
                                  stpWasteTonsStope.ToString(), stpWasteTonsLedge.ToString(),
                                  stpTonsOS.ToString(), stpTonsOSF.ToString(),
                                  stpTonsReef.ToString(), stpTonsWaste.ToString(),
                                  stpTonsLedge.ToString(), stpTonsStope.ToString(), stpTons.ToString(),
                                  stpContent.ToString(), stpContentLedge.ToString(), stpContentStope.ToString());
                            if (_saverecord == false)
                                Global.sysNotification.TsysNotification.showNotification("Data Saved", "Stoping Record not Saved - SQL Error", Color.CornflowerBlue);
                        }
                    }
                    if (barActivity.EditValue.ToString() == "Development")
                    {
                        devAdvReef = strCurrentLine.Split(',')[3];
                        if (devAdvReef == "")
                            devAdvReef = "0";
                        devAdvWaste = strCurrentLine.Split(',')[4];
                        if (devAdvWaste == "")
                            stpFLReef = "0";
                        devHeight = strCurrentLine.Split(',')[5];
                        if (devHeight == "")
                            devHeight = "0";
                        devWidth = strCurrentLine.Split(',')[6];
                        if (devWidth == "")
                            devWidth = "0";
                        devDens = strCurrentLine.Split(',')[7];
                        if (devDens == "")
                            devDens = "0";
                        devCmgt = strCurrentLine.Split(',')[8];
                        if (devCmgt == "")
                            devCmgt = "0";
                        devCubics = strCurrentLine.Split(',')[9];
                        if (devCubics == "")
                            devCubics = "0";
                        devIndicator = strCurrentLine.Split(',')[10];
                        devMinigMethod = strCurrentLine.Split(',')[11];

                        ErrorFound = false;
                        Validate_Development();

                        double devAdv = Convert.ToDouble(devAdvReef) + Convert.ToDouble(devAdvWaste);
                        double devTonsReef = Convert.ToDouble(devAdvReef) * Convert.ToDouble(devWidth) *
                                                Convert.ToDouble(devHeight) * Convert.ToDouble(devDens);
                        double devTonsWaste = Convert.ToDouble(devAdvWaste) * Convert.ToDouble(devWidth) *
                                                Convert.ToDouble(devHeight) * Convert.ToDouble(devDens);
                        double devTons = devAdv * Convert.ToDouble(devWidth) *
                                            Convert.ToDouble(devHeight) * Convert.ToDouble(devDens);
                        double devContent = Convert.ToDouble(devAdvReef) * Convert.ToDouble(devWidth) *
                                            Convert.ToDouble(devCmgt) * Convert.ToDouble(devDens) / 100;

                        if (ErrorFound == false)
                        {
                            bool _saverecord = _clsBusPlanImp.save_Development(
                                strWorkplaceID, strSectionID, strProdMonth,
                                devAdvReef, devAdvWaste, devHeight, devWidth,
                                devDens, devCmgt, devCubics,
                                devIndicator, devMinigMethod,
                                devAdv.ToString(), devContent.ToString(),
                                devTons.ToString(), devTonsReef.ToString(), devTonsWaste.ToString());
                            if (_saverecord == false)
                                Global.sysNotification.TsysNotification.showNotification("Data Saved", "Development Record not Saved - SQL Error", Color.CornflowerBlue);
                        }
                    }
                }
                nCount++;
            }
            theProgress.Close();
            if (tblXMReport1.Rows.Count != 0)
            {
                Save_ErrorFile();
            }
            else
            {
                bool _thesave = _clsBusPlanImp.save_BusPlanLocks(nYear.ToString(), nActivity.ToString(),
                                        nProdMonthStart.ToString(), nProdMonthEnd.ToString());
                barFileName.EditValue = "";
                MessageBox.Show("Business Plan successfully imported.", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
            }

            //Close();
            // }
        }

        private void Validate_Stoping()
        {
            if (!Regex.IsMatch(stpReefSqmLedge, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "ReefSqmLedge",
                    stpReefSqmLedge, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpOSSqmLedge, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "OSSqmLedge",
                    stpOSSqmLedge, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpOSFSqmLedge, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "OSFSqmLedge",
                    stpOSFSqmLedge, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpReefSqmStope, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "ReefSqmStope",
                    stpReefSqmStope, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpOSSqmStope, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "OSSqmStope",
                    stpOSSqmStope, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpOSFSqmStope, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "OSFSqmStope",
                    stpOSFSqmStope, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpSWFault, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "SWFault",
                    stpSWFault, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpSW, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "SW",
                    stpSW, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpFLReef, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "FLReef",
                    stpFLReef, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpFLOS, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "FLOS",
                    stpFLOS, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpDens, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "Density",
                    stpDens, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpCmgt, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "Cmgt",
                    stpCmgt, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpCW, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "CW",
                    stpCW, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(stpCubics, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "Cubics",
                    stpCubics, nCount.ToString(), "1");
            }
        }
        private void Validate_Development()
        {
            if (!Regex.IsMatch(devAdvReef, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "AdvReef",
                    devAdvReef, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(devAdvWaste, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "AdvWaste",
                    devAdvWaste, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(devHeight, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "Height",
                    devHeight, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(devWidth, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "Width",
                    devWidth, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(devDens, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "Density",
                    devDens, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(devCmgt, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "Cmgt",
                    devCmgt, nCount.ToString(), "1"); ;
            }
            if (!Regex.IsMatch(devCubics, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "Cubics",
                    devCubics, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(devIndicator, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "IndicatorID",
                    devIndicator, nCount.ToString(), "1");
            }
            if (!Regex.IsMatch(devMinigMethod, strRegexOnlyNumbers))
            {
                ErrorFound = true;
                AddErrorToXMLTable1(strWorkplaceID, strSectionID, strProdMonth, "Incorrect Format", "MinigMethodID",
                    devMinigMethod, nCount.ToString(), "1");
            }
        }

        private void AddErrorToXMLTable(string strVariable, string strProblem, string strWorkplaceID,
           string strSectionID, string strProdmonth, string nCodeLine, string strColumn, string strValue,
            int nLine)
        {
            DataRow drNew = tblXMReport.NewRow();

            drNew["VERSION_DSC"] = strVariable;
            drNew["Problem"] = strProblem;
            drNew["PROJECT_NO"] = strWorkplaceID;
            drNew["ACCOUNT_NO"] = strSectionID;
            drNew["Prodmonth"] = strProdmonth;

            drNew["SOURCE_IND"] = nCodeLine;
            drNew["BUD_VAL_TYPE"] = strColumn;
            drNew["BUD_VAL"] = strValue;
            drNew["Line"] = nLine;

            tblXMReport.Rows.Add(drNew);
            tblXMReport.AcceptChanges();
        }
        private void AddErrorToXMLTable1(string _workplaceid, string _sectionid, string _prodmonth,
           string _problem, string _field, string _value, string _line, string _source)
        {
            DataRow drNew = tblXMReport1.NewRow();

            drNew["WORKPLACEID"] = strWorkplaceID;
            drNew["SECTIONID"] = strSectionID;
            drNew["PRODMONTH"] = strProdMonth;
            drNew["PROBLEM"] = _problem;
            drNew["FIELD"] = _field;
            drNew["VALUE"] = _value;
            drNew["LINE"] = _line;
            drNew["SOURCE_IND"] = _source;
            tblXMReport1.Rows.Add(drNew);
            tblXMReport1.AcceptChanges();
        }

        private void Save_ErrorFile()
        {
            MessageBox.Show("Business Plan not imported.\r\nPlease choose where you want to save the report.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);            

            f_err.FileName = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + "_Report.xls";
            errorFilePathName = Path.GetDirectoryName(f.FileName) + "\\" + f_err.FileName;
            barFileNameError.EditValue = f_err.FileName;

            if (f_err.ShowDialog() == DialogResult.OK)
            {
                errorFilePathName = f_err.FileName;
                saveFileDialogXMLReport_FileOk(null, null);
                f.FileName = "";
                f_err.FileName = "";
                barFileName.EditValue = "";
                barFileNameError.EditValue = "";
                //Global.sysNotification.TsysNotification.showNotification("Data Imported", "Business Plan Import", Color.CornflowerBlue);
            }
        }
        private void saveFileDialogXMLReport_FileOk(object sender, CancelEventArgs e)
        {
            DataView dvReport = new DataView(tblXMReport1);
            dvReport.Sort = "WORKPLACEID";

            dvReport.ToTable("Report").WriteXml(errorFilePathName);
        }
    }
}
