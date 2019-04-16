using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using System.Collections;
using MWDataManager;
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;
using FastReport;
using Mineware.Systems.Production.SysAdminScreens.SetupCycles;


using System.Diagnostics;
//using System.ComponentModel
using System.IO;
using System.Text.RegularExpressions;

using System.Threading;
using System.Globalization;



using Newtonsoft.Json;

using Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPdfViewer;
using DevExpress.Pdf;


namespace Mineware.Systems.Production.SysAdminScreens.Dashboards
{
    public partial class ucMainDashboard : Mineware.Systems.Global.ucBaseUserControl
    {

        Report WPStopRep = new Report();
        Report WPStopRep1 = new Report();
        Report WPStopRep2 = new Report();
        Report WPStopRep3 = new Report();
        Report WPStopStartRep = new Report();

        Report WPLicToOp = new Report();
        Report CallCentreApp = new Report();
        Report TempRep = new Report();
        Report WPDevRep = new Report();
        Report MajHazRep = new Report();
        Report ProdRep = new Report();

        string wpaa = "";
        string col1 = "";
        string vvv = "";
        string acttype1 = "";

        string wp = "";
        string wpid = "";
        string crewid = "";
        string ADate = "";
        string BDate = "";
        string LastDateBook = "";
        string col = "";
        string stopdoc = "";
        string startdoc = "";
        string stopCat = "";
        string Act = "";

        string mo = "";

        string ExtBreak = "";

        Procedures procs = new Procedures();

        private BackgroundWorker MovetoProd = new BackgroundWorker();

        /// <OCR>
        OCRScheduling.clsOCRScheduling _clsOCRScheduling = new OCRScheduling.clsOCRScheduling();


        private bool MoveToProdind = false;
        private FormsAPI _Forms; //PduPlessis
        private PrintedForm _PrintedForm; //PduPlessis
        //List<ListDrop> _items = new List<ListDrop>();
        List<DataRow> list = new List<DataRow>();
        //private bool MoveToProdind = false;
        /// </summary>

        public ucMainDashboard()
        {
            InitializeComponent();
        }

        private void MovetoProd_DoWork(object sender, DoWorkEventArgs e)
        {
            MoveToProd();
        }


        private void ucMainDashboard_Load(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManCheck = new MWDataManager.clsDataAccess();
            _dbManCheck.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManCheck.SqlStatement = "select CheckMeas,Banner from SysSet ";
            _dbManCheck.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManCheck.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _dbManLosses.ResultsTableName = "Days";  //get table name
            _dbManCheck.ExecuteInstruction();



            SysSettings.CheckMeas = _dbManCheck.ResultsDataTable.Rows[0][0].ToString();
            SysSettings.Banner = _dbManCheck.ResultsDataTable.Rows[0][1].ToString();

            //SysSettings. = _dbManCheck.ResultsDataTable.Rows[0][1].ToString();
            //TProductionGlobal.Conn

            xtraTabControl1.TabPages[0].Text = "      LTO         ";
            xtraTabControl1.TabPages[1].Text = "  Stop Start Docs ";


            MWDataManager.clsDataAccess _dbManNNEIL1 = new MWDataManager.clsDataAccess();
            _dbManNNEIL1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManNNEIL1.SqlStatement = "select * from sysset";
            _dbManNNEIL1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManNNEIL1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManNNEIL1.ExecuteInstruction();
            DataTable dtNeil = _dbManNNEIL1.ResultsDataTable;

            foreach (DataRow dr in dtNeil.Rows)
            {
                LowLbl.Text = dr["A_Color"].ToString();
                MedLbl.Text = dr["S_Color"].ToString();
                HighLbl.Text = dr["B_Color"].ToString();
            }

            MWDataManager.clsDataAccess _dbManExten = new MWDataManager.clsDataAccess();
            _dbManExten.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManExten.SqlStatement = "select * from SYSSET_Extended";
            _dbManExten.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManExten.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManExten.ExecuteInstruction();
            DataTable dtexten = _dbManExten.ResultsDataTable;

            foreach (DataRow dr in dtexten.Rows)
            {
                LTOOrangelabel.Text = dr["LTOOrange"].ToString();
                LTORedlabel.Text = dr["LTORed"].ToString();
            }


            LoadLicenceToOperate();
            LoadStopStart();
            //Load
        }

        private void LoadStopStart()
        {
            // do 

            MWDataManager.clsDataAccess _dbManWPST = new MWDataManager.clsDataAccess();
            _dbManWPST.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (label17.Text == "Workplace Stop - Start Procedure")
                _dbManWPST.SqlStatement = " select Distinct a.*, w.activity from [tbl_WPStopStartDocSum] a, workplace w where a.description = w.description order by mo, DaysSinceBook desc ";
            if (label17.Text != "Workplace Stop - Start Procedure")
                _dbManWPST.SqlStatement = "SELECT DISTINCT " +
 "        a.mo " +
"		,a.description " +
"		,a.lastplan " +
"		,a.lastbook " +
"		,a.DaysSinceBook " +
"		,a.Color " +
"		,a.sit1 " +
"		,a.aa " +
"		,a.sstart " +
"		,MAX(a.top20) top20 " +
"		,a.newwp, " +
"       w.activity " +
"FROM[tbl_ExtendedBreakSum] a, " +
"     workplace w " +
"WHERE a.description = w.description " +
"GROUP BY  a.mo " +
"		,a.description " +
"		,a.lastplan " +
"		,a.lastbook " +
"		,a.DaysSinceBook " +
"		,a.Color " +
"		,a.sit1 " +
"		,a.aa " +
"		,a.sstart " +
"		,a.newwp, " +
"       w.activity " +
"ORDER BY A.mo,  " +
         "a.DaysSinceBook DESC; ";
            //_dbManWPST.SqlStatement = " select Distinct a.*, w.activity from [tbl_ExtendedBreakSum] a, workplace w where a.description = w.description order by mo, DaysSinceBook desc "; //and aa+sstart ";//not in ('Actions ClosedActions Closed', 'OKActions Closed', 'OKOK', 'Actions ClosedOK') order by mo, DaysSinceBook desc ";           



            _dbManWPST.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST.ExecuteInstruction();

            DataTable dt = _dbManWPST.ResultsDataTable;

            DataSet ds = new DataSet();

            if (ds.Tables.Count > 0)
                ds.Tables.Clear();

            ds.Tables.Add(dt);


            gridControl6.DataSource = ds.Tables[0];

            col1SecID.FieldName = "mo";
            col1Wp.FieldName = "description";

            col1Situation.FieldName = "sit1";
            ColExtBreak.FieldName = "sit1";

            col1Date.FieldName = "lastbook";
            if (label17.Text == "Workplace Stop - Start Procedure")
            {
                col1Days.Visible = true;
                col1Date.Caption = "Last Book Date";
            }
            else
            {
                col1Days.Visible = false;
                col1Date.Caption = "Ext Break Start";

            }


            col1Days.FieldName = "DaysSinceBook";

            Col1Top20.FieldName = "top20";
            NewWPCol.FieldName = "newwp";

            //Col1WPStartDoc.FieldName = "aa";

            col1WPStopDoc.FieldName = "aa";
            Col1WPStartDoc1.FieldName = "sstart";
            WPAct.FieldName = "activity";

            // if (clsUserInfo.UserID != "MINEWARE")
            //   col1WPStopDoc.Visible = false;

            gridControl6.Dock = DockStyle.Fill;
            gridControl6.Visible = true;
        }

        private void LoadLicenceToOperate()
        {
            // do col set up
            MWDataManager.clsDataAccess _dbManLToOp1 = new MWDataManager.clsDataAccess();
            _dbManLToOp1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManLToOp1.SqlStatement = " select * from [dbo].[tbl_LicenceToOperate_SetUp] ";
            _dbManLToOp1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManLToOp1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManLToOp1.ExecuteInstruction();


            LOCompA.Visible = true;
            LOCallCent.Visible = true;

            LOCat1.Visible = true;
            LOCat2.Visible = true;
            LOCat3.Visible = true;
            LOCat4.Visible = true;
            LOCat5.Visible = true;
            LOCat6.Visible = true;
            LOCat7.Visible = true;
            LOCat8.Visible = true;


            if (_dbManLToOp1.ResultsDataTable.Rows.Count > 0)
            {
                if (_dbManLToOp1.ResultsDataTable.Rows[0]["Cat1"].ToString() == "N")
                {
                    LOCompA.Visible = false;
                    LOCompAD.Visible = false;
                }
                if (_dbManLToOp1.ResultsDataTable.Rows[0]["Cat2"].ToString() == "N")
                {
                    LOCallCent.Visible = false;
                    LOCallCentD.Visible = false;
                }
                if (_dbManLToOp1.ResultsDataTable.Rows[0]["Cat3"].ToString() == "N")
                {
                    LOCat1.Visible = false;
                    LOCat1D.Visible = false;
                }
                if (_dbManLToOp1.ResultsDataTable.Rows[0]["Cat4"].ToString() == "N")
                {
                    LOCat2.Visible = false;
                    LOCat2D.Visible = false;
                }
                if (_dbManLToOp1.ResultsDataTable.Rows[0]["Cat5"].ToString() == "N")
                {
                    LOCat3.Visible = false;
                    LOCat3D.Visible = false;
                }
                if (_dbManLToOp1.ResultsDataTable.Rows[0]["Cat6"].ToString() == "N")
                {
                    LOCat4.Visible = false;
                    LOCat4D.Visible = false;
                }
                if (_dbManLToOp1.ResultsDataTable.Rows[0]["Cat7"].ToString() == "N")
                {
                    LOCat5.Visible = false;
                    LOCat5D.Visible = false;
                }
                if (_dbManLToOp1.ResultsDataTable.Rows[0]["Cat8"].ToString() == "N")
                {
                    LOCat6.Visible = false;
                    LOCat6D.Visible = false;
                }
                if (_dbManLToOp1.ResultsDataTable.Rows[0]["Cat9"].ToString() == "N")
                {
                    LOCat7.Visible = false;
                    LOCat7D.Visible = false;
                }
                if (_dbManLToOp1.ResultsDataTable.Rows[0]["Cat10"].ToString() == "N")
                {
                    LOCat8.Visible = false;
                    LOCat8D.Visible = false;
                }

            }

            if (TUserInfo.UserID == "CSHEPPARD" || TUserInfo.UserID == "SHAUN#1" || TUserInfo.UserID == "mineware" || TUserInfo.UserID == "Chris")
            {
                LOCompA.Visible = true;
                LOCallCent.Visible = true;

                LOCat1.Visible = true;
                LOCat2.Visible = true;
                LOCat3.Visible = true;
                LOCat4.Visible = true;
                LOCat5.Visible = true;
                LOCat6.Visible = true;
                LOCat7.Visible = true;
                LOCat8.Visible = true;


                LOCompAD.Visible = true;
                LOCallCentD.Visible = true;

                LOCat1D.Visible = true;
                LOCat2D.Visible = true;
                LOCat3D.Visible = true;
                LOCat4D.Visible = true;
                LOCat5D.Visible = true;
                LOCat6D.Visible = true;
                LOCat7D.Visible = true;
                LOCat8D.Visible = true;

            }


            // gridBand7.Columns.Clear();

            MWDataManager.clsDataAccess _dbManLToOp = new MWDataManager.clsDataAccess();
            _dbManLToOp.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManLToOp.SqlStatement = " select WPDescription1+orgdescription aa,wpdescription1 wpdescription, aq.*, isnull(convert(varchar(20),rate),'') rate from (  \r\n" +
                                        "select max(substring(section,1,4)) section, wpdescription wpdescription1, orgdescription,  isnull(max(ColCritSkill),'') ColCritSkill,  isnull(max(ColRiskProfile),'') ColRiskProfile \r\n " +
                                        ",  isnull(max(ColWPDev),'') ColWPDev,  isnull(max(ColMajHazards),'') ColMajHazards,  isnull(max(ColMajHazardsOCR),'') ColMajHazardsOCR,  isnull(max(ColWPEnviro),'') ColWPEnviro,  isnull(max(ColProd),'') ColProd, sum(num) num  \r\n" +
                                        ",  isnull(max(ColGeol),'') ColGeol,  isnull(max(ColRock),'') ColRock,  isnull(max(ColCallCent),'') ColCallCent,  isnull(max(ColCompA),'') ColCompA,  isnull(max(ColSC),'') ColSC  \r\n" +
                                        " from   \r\n" +
                                        "(  \r\n" +
                                        "select section, wpdescription, orgdescription,   \r\n" +
                                        "case when a.majcat = '1) Critical Skills' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '1) Critical Skills' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColCritSkill,  \r\n" +

                                        "case when a.majcat = '2) Workplace Risk Profiles' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '2) Workplace Risk Profiles' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColRiskProfile,  \r\n" +

                                        "case when a.majcat = '3) Workplace Deviations' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '3) Workplace Deviations' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColWPDev,  \r\n" +

                                        "case when a.majcat = '4) Major Hazards / Critical Controls Deviations' and a.subcat = '1) Critical Controls - A Hazards' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '4) Major Hazards / Critical Controls Deviations' and a.subcat = '1) Critical Controls - A Hazards' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColMajHazards,  \r\n" +

                                           "case when a.majcat = '4) Major Hazards / Critical Controls Deviations' and a.subcat = '2) Repetitive Deficiencies (categories)' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '4) Major Hazards / Critical Controls Deviations' and a.subcat = '2) Repetitive Deficiencies (categories)' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColMajHazardsOCR,  \r\n" +

                                        "case when a.majcat = '5) Workplace Enviromental Conditions' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '5) Workplace Enviromental Conditions' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColWPEnviro,  \r\n" +

                                        "case when a.majcat = '6) Production' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '6) Production' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColProd,  \r\n" +

                                        "case when day30 is not null  and day30 <> '' and day30 <> 'Off' then Weight else 0 end as num,  \r\n" +

                                        "case when a.majcat = '7) Geology Mapping' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '7) Geology Mapping' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColGeol,  \r\n" +

                                         "case when a.majcat = '8) Rock Engineering Visits' and day30 = 'Red' then 'Red' \r\n " +
                                        "when a.majcat = '8) Rock Engineering Visits' and day30 = 'Orange' then 'Orange' \r\n " +
                                        "end as ColRock,  \r\n" +

                                         "case when a.majcat = '9) Call Center Application' and day30 = 'Red' then 'Red' \r\n " +
                                        "when a.majcat = '9) Call Center Application' and day30 = 'Orange' then 'Orange' \r\n " +
                                        "end as ColCallCent,  \r\n" +


                                        "case when a.majcat = '10) CompA' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '10) CompA' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColCompA  \r\n" +

                                        ", case when a.majcat = '8a) ISSI_Seismic' and day30 = 'Red' then 'Red' when a.majcat = '8a) ISSI_Seismic' and day30 = 'Orange' then 'Orange' end as ColSC   \r\n" +


                                        " from  (  \r\n" +
                                        " Select a.*from[dbo].[tbl_LicenceToOperateSum] a ,WORKPLACE b  \r\n" +
                                        " where a.WPid = b.WorkplaceID and(b.Activity = '0' or b.Activity = '8')) a  \r\n" +
                                        "left outer join  tbl_LicenceToOperateWeighting b on a.majcat = b.majcat and a.subcat = b.subcat  ) a group by wpdescription, orgdescription) aq   \r\n" +
                                        " left outer join (  \r\n" +
                                         " select s.wpdescription wpsc, max(risk) rate from [dbo].[tbl_LicenceToOperate_Seismic] s, (select wpdescription, max(thedate) dd from  [dbo].[tbl_LicenceToOperate_Seismic] group by wpdescription) b  \r\n" +
                                         " where s.wpdescription = b.wpdescription and s.thedate = b.dd group by s.wpdescription  \r\n" +
                                         " ) sc on aq.wpdescription1 = sc.wpsc  \r\n" +
                                         " where section not like 'REZ%' and section not like 'REP%' order by num desc  \r\n";
            _dbManLToOp.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManLToOp.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManLToOp.ExecuteInstruction();

            DataTable dtLToOp = _dbManLToOp.ResultsDataTable;

            DataSet dsLToOp = new DataSet();

            if (dsLToOp.Tables.Count > 0)
                dsLToOp.Tables.Clear();

            dsLToOp.Tables.Add(dtLToOp);




            MWDataManager.clsDataAccess _dbManWPSTDetail = new MWDataManager.clsDataAccess();
            _dbManWPSTDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPSTDetail.SqlStatement = "   select * from ( select WPDescription+orgdescription aa, WPDescription, majcat, subcat  \r\n" +

                                        " , max(day1) day1, max(day2) day2, max(day3) day3, max(day4) day4, max(day5) day5 \r\n" +
                                        " , max(day6) day6, max(day7) day7, max(day8) day8, max(day9) day9, max(day10) day10 \r\n" +

                                        " , max(day11) day11, max(day12) day12, max(day13) day13, max(day14) day14, max(day15) day15 \r\n" +
                                        " , max(day16) day16, max(day17) day17, max(day18) day18, max(day19) day19, max(day20) day20 \r\n" +

                                        " , max(day21) day21, max(day22) day22, max(day23) day23, max(day24) day24, max(day25) day25 \r\n" +
                                        " , max(day26) day26, max(day27) day27, max(day28) day28, max(day29) day29, max(day30) day30 \r\n" +
                                        "  from [dbo].[tbl_LicenceToOperateSum] a ,WORKPLACE b \r\n" +
                                        " where a.WPid = b.WorkplaceID and(b.Activity = '0' or b.Activity = '8') and section not like 'REZ%' and section not like 'REP%'   group by wpdescription, orgdescription, majcat, subcat \r\n" +

                                        "  ) a where  \r\n" +
                                        "  replace(isnull(day1,''),'Off','')+ replace(isnull(day2,''),'Off','')+ replace(isnull(day3,''),'Off','') + \r\n" +
                                        "  replace(isnull(day4,''),'Off','')+ replace(isnull(day5,''),'Off','')+ replace(isnull(day6,''),'Off','') + \r\n" +
                                        "  replace(isnull(day7,''),'Off','')+ replace(isnull(day8,''),'Off','')+ replace(isnull(day9,''),'Off','') +  replace(isnull(day10,''),'Off','') + \r\n" +

                                        " replace(isnull(day11,''),'Off','')+ replace(isnull(day12,''),'Off','')+ replace(isnull(day13,''),'Off','') + \r\n" +
                                        " replace(isnull(day14,''),'Off','')+ replace(isnull(day15,''),'Off','')+ replace(isnull(day16,''),'Off','') + \r\n" +
                                        " replace(isnull(day17,''),'Off','')+ replace(isnull(day18,''),'Off','')+ replace(isnull(day19,''),'Off','') +  replace(isnull(day20,''),'Off','') + \r\n" +

                                        " replace(isnull(day21,''),'Off','')+ replace(isnull(day22,''),'Off','')+ replace(isnull(day23,''),'Off','') + \r\n" +
                                        " replace(isnull(day24,''),'Off','')+ replace(isnull(day25,''),'Off','')+ replace(isnull(day26,''),'Off','') + \r\n" +
                                        " replace(isnull(day27,''),'Off','')+ replace(isnull(day28,''),'Off','')+ replace(isnull(day29,''),'Off','') +  replace(isnull(day30,''),'Off','') 	 <> ''  \r\n" +

                                         "  order by wpdescription, aa, majcat, subcat  ";

            _dbManWPSTDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPSTDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPSTDetail.ExecuteInstruction();

            DataTable dtDetail = _dbManWPSTDetail.ResultsDataTable;

            dsLToOp.Tables.Add(dtDetail);

            dsLToOp.Relations.Clear();

            DataColumn keyColumn1 = dsLToOp.Tables[0].Columns[0];
            DataColumn foreignKeyColumn1 = dsLToOp.Tables[1].Columns[0];
            dsLToOp.Relations.Add("CategoriesProducts", keyColumn1, foreignKeyColumn1);


            gridControl1.DataSource = dsLToOp.Tables[0];
            gridControl1.LevelTree.Nodes.Add("CategoriesProducts", bandedGridView2);
            bandedGridView2.ViewCaption = "Workplace Detail";

            gridControl1.DataSource = dsLToOp.Tables[0];

            Group.Width = 220;
            SubGroup.Width = 220;

            int x = 29;

            Group.FieldName = "majcat";
            SubGroup.FieldName = "subcat";


            bandedGridColumn1.FieldName = "day1";
            bandedGridColumn1.Width = 40;
            bandedGridColumn1.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn2.FieldName = "day2";
            bandedGridColumn2.Width = 40;
            bandedGridColumn2.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn3.FieldName = "day3";
            bandedGridColumn3.Width = 40;
            bandedGridColumn3.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn4.FieldName = "day4";
            bandedGridColumn4.Width = 40;
            bandedGridColumn4.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // bandedGridColumn4.Visible = false;

            x = x - 1;
            bandedGridColumn5.FieldName = "day5";
            bandedGridColumn5.Width = 40;
            bandedGridColumn5.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn6.FieldName = "day6";
            bandedGridColumn6.Width = 40;
            bandedGridColumn6.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn7.FieldName = "day7";
            bandedGridColumn7.Width = 40;
            bandedGridColumn7.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn8.FieldName = "day8";
            bandedGridColumn8.Width = 40;
            bandedGridColumn8.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn9.FieldName = "day9";
            bandedGridColumn9.Width = 40;
            bandedGridColumn9.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn10.FieldName = "day10";
            bandedGridColumn10.Width = 40;
            bandedGridColumn10.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn11.FieldName = "day11";
            bandedGridColumn11.Width = 40;
            bandedGridColumn11.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn12.FieldName = "day12";
            bandedGridColumn12.Width = 40;
            bandedGridColumn12.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn12.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn13.FieldName = "day13";
            bandedGridColumn13.Width = 40;
            bandedGridColumn13.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn13.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn14.FieldName = "day14";
            bandedGridColumn14.Width = 40;
            bandedGridColumn14.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn14.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn15.FieldName = "day15";
            bandedGridColumn15.Width = 40;
            bandedGridColumn15.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn15.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn16.FieldName = "day16";
            bandedGridColumn16.Width = 40;
            bandedGridColumn16.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn16.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn17.FieldName = "day17";
            bandedGridColumn17.Width = 40;
            bandedGridColumn17.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn17.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn18.FieldName = "day18";
            bandedGridColumn18.Width = 40;
            bandedGridColumn18.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn18.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn19.FieldName = "day19";
            bandedGridColumn19.Width = 40;
            bandedGridColumn19.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn19.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn20.FieldName = "day20";
            bandedGridColumn20.Width = 40;
            bandedGridColumn20.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn20.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn21.FieldName = "day21";
            bandedGridColumn21.Width = 40;
            bandedGridColumn21.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn21.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn22.FieldName = "day22";
            bandedGridColumn22.Width = 40;
            bandedGridColumn22.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn22.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn23.FieldName = "day23";
            bandedGridColumn23.Width = 40;
            bandedGridColumn23.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn23.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn24.FieldName = "day24";
            bandedGridColumn24.Width = 40;
            bandedGridColumn24.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn24.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn25.FieldName = "day25";
            bandedGridColumn25.Width = 40;
            bandedGridColumn25.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn25.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn26.FieldName = "day26";
            bandedGridColumn26.Width = 40;
            bandedGridColumn26.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn26.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn27.FieldName = "day27";
            bandedGridColumn27.Width = 40;
            bandedGridColumn27.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn27.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn28.FieldName = "day28";
            bandedGridColumn28.Width = 40;
            bandedGridColumn28.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn28.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn29.FieldName = "day29";
            bandedGridColumn29.Width = 40;
            bandedGridColumn29.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn29.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn30.FieldName = "day30";
            bandedGridColumn30.Width = 40;
            bandedGridColumn30.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn30.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;






            // gridControl1.DataSource = dsLToOp.Tables[0];

            LOPSection.FieldName = "section";
            LOPWorkplace.FieldName = "wpdescription";

            LOOrgunit.FieldName = "orgdescription";
            LOCat1.FieldName = "ColCritSkill";
            LOCat2.FieldName = "ColRiskProfile";
            LOCat3.FieldName = "ColWPDev";
            LOCat4.FieldName = "ColMajHazardsOCR";
            LOCat5.FieldName = "ColWPEnviro";
            LOCat6.FieldName = "ColProd";

            LOWeighting.FieldName = "num";

            LOCat7.FieldName = "ColRock";
            LOCat8.FieldName = "ColGeol";
            LOCallCent.FieldName = "ColCallCent";
            LOCompA.FieldName = "ColCompA";

            LOSeismic.FieldName = "ColSC";
            Loss.FieldName = "rate";


            DevExpress.XtraGrid.Columns.GridColumn column = bandedGridColumn30;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo info = bandedGridView2.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            bandedGridView2.LeftCoord = info.GetColumnLeftCoord(column);


            gridControl1.Visible = true;


            MWDataManager.clsDataAccess _dbManLToOpD = new MWDataManager.clsDataAccess();
            _dbManLToOpD.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManLToOpD.SqlStatement = " select WPDescription1+orgdescription aa,wpdescription1 wpdescription, aq.*, isnull(convert(varchar(20),rate),'') rate from (  \r\n" +
                                        "select max(substring(section,1,4)) section, wpdescription wpdescription1, orgdescription,  isnull(max(ColCritSkill),'') ColCritSkill,  isnull(max(ColRiskProfile),'') ColRiskProfile \r\n " +
                                        ",  isnull(max(ColWPDev),'') ColWPDev,  isnull(max(ColMajHazards),'') ColMajHazards,  isnull(max(ColWPEnviro),'') ColWPEnviro,  isnull(max(ColProd),'') ColProd, sum(num) num  \r\n" +
                                        ",  isnull(max(ColGeol),'') ColGeol,  isnull(max(ColRock),'') ColRock,  isnull(max(ColCallCent),'') ColCallCent,  isnull(max(ColCompA),'') ColCompA,  isnull(max(ColSC),'') ColSC  \r\n" +
                                        " from   \r\n" +
                                        "(  \r\n" +
                                        "select section, wpdescription, orgdescription,   \r\n" +
                                        "case when a.majcat = '1) Critical Skills' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '1) Critical Skills' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColCritSkill,  \r\n" +

                                        "case when a.majcat = '2) Workplace Risk Profiles' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '2) Workplace Risk Profiles' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColRiskProfile,  \r\n" +

                                        "case when a.majcat = '3) Workplace Deviations' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '3) Workplace Deviations' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColWPDev,  \r\n" +

                                        "case when a.majcat = '4) Major Hazards / Critical Controls Deviations' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '4) Major Hazards / Critical Controls Deviations' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColMajHazards,  \r\n" +

                                        "case when a.majcat = '5) Workplace Enviromental Conditions' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '5) Workplace Enviromental Conditions' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColWPEnviro,  \r\n" +

                                        "case when a.majcat = '6) Production' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '6) Production' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColProd,  \r\n" +

                                        "case when day30 is not null  and day30 <> '' then Weight else 0 end as num,  \r\n" +

                                        "case when a.majcat = '7) Geology Mapping' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '7) Geology Mapping' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColGeol,  \r\n" +

                                         "case when a.majcat = '8) Rock Engineering Visits' and day30 = 'Red' then 'Red' \r\n " +
                                        "when a.majcat = '8) Rock Engineering Visits' and day30 = 'Orange' then 'Orange' \r\n " +
                                        "end as ColRock,  \r\n" +

                                         "case when a.majcat = '9) Call Center Application' and day30 = 'Red' then 'Red' \r\n " +
                                        "when a.majcat = '9) Call Center Application' and day30 = 'Orange' then 'Orange' \r\n " +
                                        "end as ColCallCent,  \r\n" +


                                        "case when a.majcat = '10) CompA' and day30 = 'Red' then 'Red'  \r\n" +
                                        "when a.majcat = '10) CompA' and day30 = 'Orange' then 'Orange'  \r\n" +
                                        "end as ColCompA  \r\n" +

                                        ", case when a.majcat = '8a) ISSI_Seismic' and day30 = 'Red' then 'Red' when a.majcat = '8a) ISSI_Seismic' and day30 = 'Orange' then 'Orange' end as ColSC   \r\n" +


                                        " from   \r\n" +
                                        "  (   \r\n" +
                                        " Select a.* from[dbo].[tbl_LicenceToOperateSum] a, WORKPLACE b   \r\n" +
                                        " where a.WPid = b.WorkplaceID and(b.Activity = '1')) a   \r\n" +
                                        "left outer join  tbl_LicenceToOperateWeighting b on a.majcat = b.majcat and a.subcat = b.subcat ) a group by wpdescription, orgdescription) aq   \r\n" +
                                        " left outer join (  \r\n" +
                                         " select s.wpdescription wpsc, max(risk) rate from [dbo].[tbl_LicenceToOperate_Seismic] s, (select wpdescription, max(thedate) dd from  [dbo].[tbl_LicenceToOperate_Seismic] group by wpdescription) b  \r\n" +
                                         " where s.wpdescription = b.wpdescription and s.thedate = b.dd group by s.wpdescription  \r\n" +
                                         " ) sc on aq.wpdescription1 = sc.wpsc  \r\n" +
                                         " order by num desc  \r\n";
            _dbManLToOpD.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManLToOpD.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManLToOpD.ExecuteInstruction();

            DataTable dtLToOpD = _dbManLToOpD.ResultsDataTable;

            DataSet dsLToOpD = new DataSet();

            if (dsLToOpD.Tables.Count > 0)
                dsLToOpD.Tables.Clear();

            dsLToOpD.Tables.Add(dtLToOpD);



            MWDataManager.clsDataAccess _dbManWPSTDetailD = new MWDataManager.clsDataAccess();
            _dbManWPSTDetailD.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPSTDetailD.SqlStatement = "   select * from ( select WPDescription+orgdescription aa, WPDescription, majcat, subcat  " +

                                        " , max(day1) day1, max(day2) day2, max(day3) day3, max(day4) day4, max(day5) day5 " +
                                        " , max(day6) day6, max(day7) day7, max(day8) day8, max(day9) day9, max(day10) day10 " +

                                        " , max(day11) day11, max(day12) day12, max(day13) day13, max(day14) day14, max(day15) day15 " +
                                        " , max(day16) day16, max(day17) day17, max(day18) day18, max(day19) day19, max(day20) day20 " +

                                        " , max(day21) day21, max(day22) day22, max(day23) day23, max(day24) day24, max(day25) day25 " +
                                        " , max(day26) day26, max(day27) day27, max(day28) day28, max(day29) day29, max(day30) day30 " +
                                        "  from [dbo].[tbl_LicenceToOperateSum] a ,WORKPLACE b  \r\n" +
                                        "where a.WPid = b.WorkplaceID and(b.Activity = '1') group by wpdescription, orgdescription, majcat, subcat " +

                                        " ) a where  " +
                                        "  replace(isnull(day1,''),'Off','')+ replace(isnull(day2,''),'Off','')+ replace(isnull(day3,''),'Off','') + " +
                                        "  replace(isnull(day4,''),'Off','')+ replace(isnull(day5,''),'Off','')+ replace(isnull(day6,''),'Off','') + " +
                                        "  replace(isnull(day7,''),'Off','')+ replace(isnull(day8,''),'Off','')+ replace(isnull(day9,''),'Off','') +  replace(isnull(day10,''),'Off','') + " +

                                        " replace(isnull(day11,''),'Off','')+ replace(isnull(day12,''),'Off','')+ replace(isnull(day13,''),'Off','') + " +
                                        " replace(isnull(day14,''),'Off','')+ replace(isnull(day15,''),'Off','')+ replace(isnull(day16,''),'Off','') + " +
                                        " replace(isnull(day17,''),'Off','')+ replace(isnull(day18,''),'Off','')+ replace(isnull(day19,''),'Off','') +  replace(isnull(day20,''),'Off','') + " +

                                        " replace(isnull(day21,''),'Off','')+ replace(isnull(day22,''),'Off','')+ replace(isnull(day23,''),'Off','') + " +
                                        " replace(isnull(day24,''),'Off','')+ replace(isnull(day25,''),'Off','')+ replace(isnull(day26,''),'Off','') + " +
                                        " replace(isnull(day27,''),'Off','')+ replace(isnull(day28,''),'Off','')+ replace(isnull(day29,''),'Off','') +  replace(isnull(day30,''),'Off','') 	 <> ''  " +

                                         "  order by wpdescription, aa, majcat, subcat  ";

            _dbManWPSTDetailD.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPSTDetailD.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPSTDetailD.ExecuteInstruction();

            DataTable dtDetailD = _dbManWPSTDetailD.ResultsDataTable;

            dsLToOpD.Tables.Add(dtDetailD);

            dsLToOpD.Relations.Clear();

            DataColumn keyColumn1D = dsLToOpD.Tables[0].Columns[0];
            DataColumn foreignKeyColumn1D = dsLToOpD.Tables[1].Columns[0];
            dsLToOpD.Relations.Add("CategoriesProductsD", keyColumn1D, foreignKeyColumn1D);


            gridControl2.DataSource = dsLToOp.Tables[0];
            gridControl2.LevelTree.Nodes.Add("CategoriesProductsD", bandedGridView3);
            bandedGridView3.ViewCaption = "Workplace Detail";

            gridControl2.DataSource = dsLToOpD.Tables[0];


            GroupD.Width = 220;
            SubGroupD.Width = 220;

            x = 29;

            GroupD.FieldName = "majcat";
            SubGroupD.FieldName = "subcat";


            bandedGridColumn1D.FieldName = "day1";
            bandedGridColumn1D.Width = 40;
            bandedGridColumn1D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn1D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn2D.FieldName = "day2";
            bandedGridColumn2D.Width = 40;
            bandedGridColumn2D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn2D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn3D.FieldName = "day3";
            bandedGridColumn3D.Width = 40;
            bandedGridColumn3D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn3D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn4D.FieldName = "day4";
            bandedGridColumn4D.Width = 40;
            bandedGridColumn4D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn4D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // bandedGridColumn4.Visible = false;

            x = x - 1;
            bandedGridColumn5D.FieldName = "day5";
            bandedGridColumn5D.Width = 40;
            bandedGridColumn5D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn5D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn6D.FieldName = "day6";
            bandedGridColumn6D.Width = 40;
            bandedGridColumn6D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn6D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn7D.FieldName = "day7";
            bandedGridColumn7D.Width = 40;
            bandedGridColumn7D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn7D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn8D.FieldName = "day8";
            bandedGridColumn8D.Width = 40;
            bandedGridColumn8D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn8D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn9D.FieldName = "day9";
            bandedGridColumn9D.Width = 40;
            bandedGridColumn9D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn9D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn10D.FieldName = "day10";
            bandedGridColumn10D.Width = 40;
            bandedGridColumn10D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn10D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn11D.FieldName = "day11";
            bandedGridColumn11D.Width = 40;
            bandedGridColumn11D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn11D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn12D.FieldName = "day12";
            bandedGridColumn12D.Width = 40;
            bandedGridColumn12D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn12D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn13D.FieldName = "day13";
            bandedGridColumn13D.Width = 40;
            bandedGridColumn13D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn13D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn14D.FieldName = "day14";
            bandedGridColumn14D.Width = 40;
            bandedGridColumn14D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn14D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn15D.FieldName = "day15";
            bandedGridColumn15D.Width = 40;
            bandedGridColumn15D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn15D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn16D.FieldName = "day16";
            bandedGridColumn16D.Width = 40;
            bandedGridColumn16D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn16D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn17D.FieldName = "day17";
            bandedGridColumn17D.Width = 40;
            bandedGridColumn17D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn17D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn18D.FieldName = "day18";
            bandedGridColumn18D.Width = 40;
            bandedGridColumn18D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn18D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn19D.FieldName = "day19";
            bandedGridColumn19D.Width = 40;
            bandedGridColumn19D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn19D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn20D.FieldName = "day20";
            bandedGridColumn20D.Width = 40;
            bandedGridColumn20D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn20D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn21D.FieldName = "day21";
            bandedGridColumn21D.Width = 40;
            bandedGridColumn21D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn21D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn22D.FieldName = "day22";
            bandedGridColumn22D.Width = 40;
            bandedGridColumn22D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn22D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn23D.FieldName = "day23";
            bandedGridColumn23D.Width = 40;
            bandedGridColumn23D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn23D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn24D.FieldName = "day24";
            bandedGridColumn24D.Width = 40;
            bandedGridColumn24D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn24D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn25D.FieldName = "day25";
            bandedGridColumn25D.Width = 40;
            bandedGridColumn25D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn25D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn26D.FieldName = "day26";
            bandedGridColumn26D.Width = 40;
            bandedGridColumn26D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn26D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn27D.FieldName = "day27";
            bandedGridColumn27D.Width = 40;
            bandedGridColumn27D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn27D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn28D.FieldName = "day28";
            bandedGridColumn28D.Width = 40;
            bandedGridColumn28D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn28D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn29D.FieldName = "day29";
            bandedGridColumn29D.Width = 40;
            bandedGridColumn29D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn29D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            x = x - 1;
            bandedGridColumn30D.FieldName = "day30";
            bandedGridColumn30D.Width = 40;
            bandedGridColumn30D.Caption = DateTime.Now.AddDays(-x).ToString("dd MMM");
            bandedGridColumn30D.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;






            // gridControl1.DataSource = dsLToOp.Tables[0];

            LOPSectionD.FieldName = "section";
            LOPWorkplaceD.FieldName = "wpdescription";

            LOOrgunitD.FieldName = "orgdescription";
            LOCat1D.FieldName = "ColCritSkill";
            LOCat2D.FieldName = "ColRiskProfile";
            LOCat3D.FieldName = "ColWPDev";
            LOCat4D.FieldName = "ColMajHazards";
            LOCat5D.FieldName = "ColWPEnviro";
            LOCat6D.FieldName = "ColProd";

            LOWeightingD.FieldName = "num";

            LOCat7D.FieldName = "ColRock";
            LOCat8D.FieldName = "ColGeol";
            LOCallCentD.FieldName = "ColCallCent";
            LOCompAD.FieldName = "ColCompA";

            LOSeismicD.FieldName = "ColSC";
            LossD.FieldName = "rate";


            DevExpress.XtraGrid.Columns.GridColumn columnD = bandedGridColumn30D;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo infoD = bandedGridView3.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            bandedGridView3.LeftCoord = info.GetColumnLeftCoord(column);


            gridControl2.Visible = true;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;


            string Shaft = "";

            //if (SysSettings.Banner == "Great Noligwa")
            //    Shaft = "36";
            //if (SysSettings.Banner == "Moab Khotsong")
            Shaft = "47";
            //if (SysSettings.Banner == "Kopanang")
            //Shaft = "37";

            //if (SysSettings.Banner == "Mponeng")
            //    Shaft = "42";
            //if (SysSettings.Banner == "Savuka")
            //    Shaft = "43";
            //if (SysSettings.Banner == "Tau Tona")
            //Shaft = "44";


            if (col1 == "Workplace                                                         (DQlik to show Report)")
            {

                ///////////////////////1.Comp A//////////////////////////////////////////////////////

                MWDataManager.clsDataAccess _dbManCompA = new MWDataManager.clsDataAccess();
                _dbManCompA.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManCompA.SqlStatement = "exec [dbo].sp_LicenceToOperate_CompA '" + wpaa + "'";

                _dbManCompA.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManCompA.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManCompA.ResultsTableName = "DetailsCompA";
                _dbManCompA.ExecuteInstruction();

                DataSet ReportDatasetCompADetail = new DataSet();
                ReportDatasetCompADetail.Tables.Add(_dbManCompA.ResultsDataTable);


                //TempRep.RegisterData(ReportDatasetReport1);



                MWDataManager.clsDataAccess _dbManCompA1 = new MWDataManager.clsDataAccess();
                _dbManCompA1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbManCompA1.SqlStatement = "select * from [ZAWW2K16SQL01].[AngloQlikView].[dbo].[vw_PaperlessActionManagerPrint_AllActions] ";
                _dbManCompA1.SqlStatement = "select Start_Date DateSubmitted,criticalControl SubTaskName ,Action_Parent_Type  StepName, Action_Status ActionStatus from tbl_Incidents   \r\n" +
                "where[Start_Date] > getdate() - 30 and Action_Parent_Type like '%CompA%'  and workplace = '" + wpaa + "' and Action_Status <> 'Not A Deficiency'and Action_Status <> 'Not Applicable'  ";
                _dbManCompA1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManCompA1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManCompA1.ResultsTableName = "PaperlessCompA";
                _dbManCompA1.ExecuteInstruction();

                DataSet ReportDatasetCompADetailA = new DataSet();
                ReportDatasetCompADetailA.Tables.Add(_dbManCompA1.ResultsDataTable);


                //TempRep.RegisterData(ReportDatasetReport2);



                MWDataManager.clsDataAccess _dbManCompA2 = new MWDataManager.clsDataAccess();
                _dbManCompA2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManCompA2.SqlStatement = "select '" + SysSettings.Banner + "' mine, '" + wpaa + "' workplace ";
                _dbManCompA2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManCompA2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManCompA2.ResultsTableName = "HeaderCompA";
                _dbManCompA2.ExecuteInstruction();

                DataSet ReportDatasetCompADetailB = new DataSet();
                ReportDatasetCompADetailB.Tables.Add(_dbManCompA2.ResultsDataTable);


                //TempRep.RegisterData(ReportDatasetReport3);



                //////////////////////////////////////////////////////////////////////////////////////

                ///////////////////////2.Call Centre Application//////////////////////////////////////          





                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select * from  tbl_Call_Center where worktypecat = 'STP' \r\n " +
                                        " and shaftid = '" + Shaft + "' and answerdate = CONVERT(VARCHAR(10),getdate(), 20) \r\n " +
                                        " and wpequipno = '" + wpaa + "' \r\n " +
                                        " and ragind = 'R' \r\n " +
                                        " \r\n " +
                                        " \r\n " +
                                        " \r\n " +
                                        " \r\n " +
                                        " \r\n " +
                                        " \r\n ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "Data1";
                _dbMan1.ExecuteInstruction();

                string visible1 = "N";

                if (_dbMan1.ResultsDataTable.Rows.Count > 0)
                {
                    visible1 = "Y";
                }

                DataSet ReportDatasetReport1 = new DataSet();
                ReportDatasetReport1.Tables.Add(_dbMan1.ResultsDataTable);

                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement = "  " +
                                     "  select MAX(col1) col1, MAX(col2) col2, MAX(col3) col3,MAX(col4) col4,MAX(col5) col5, \r\n " +
                                            " MAX(col6) col6, MAX(col7) col7, MAX(col8) col8 , MAX(col9) col9 , MAX(col10) col10, \r\n " +
                                            " MAX(col11) col11,MAX(col12) col12,MAX(col13) col13,MAX(col14) col14,MAX(col15) col15, \r\n " +
                                            " MAX(col16) col16,MAX(col17) col17,MAX(col18) col18,MAX(col19) col19,MAX(col20) col20, \r\n " +
                                            " MAX(col21) col21,MAX(col22) col22,MAX(col23) col23,MAX(col24) col24,MAX(col25) col25, \r\n " +
                                            " MAX(col26) col26,MAX(col27) col27,MAX(col28) col28,MAX(col29) col29,MAX(col30) col30, \r\n " +
                                            " MAX(col31) col31 \r\n " +

                                            " from(select  \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate(), 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate(), 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col1, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-1, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-1, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col2, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-2, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-2, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col3, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-3, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-3, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col4, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-4, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-4, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col5, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-5, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-5, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col6, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-6, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-6, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col7, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-7, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-7, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col8, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-8, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-8, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col9, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-9, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-9, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col10, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-10, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-10, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col11, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-11, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-11, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col12, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-12, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-12, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col13, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-13, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-13, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col14, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-14, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-14, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col15, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-15, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-15, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col16, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-16, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-16, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col17, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-17, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-17, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col18, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-18, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-18, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col19, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-19, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-19, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col20, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-20, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-20, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col21, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-21, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-21, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col22, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-22, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-22, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col23, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-23, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-23, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col24, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-24, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-24, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col25, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-25, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-25, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col26, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-26, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-26, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col27, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-27, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-27, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col28, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-28, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-28, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col29, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-29, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-29, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col30, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-30, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-30, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col31, \r\n " +


                                            " * from ( \r\n " +
                                             "Select calendardate, max(wd) wd from(   \r\n" +
                                              "select w.description, a.* from vw_Plansect_workDay_Combined a   \r\n" +


                                              "left outer join WORKPLACE w  on a.workplaceid = w.WorkplaceID) a   \r\n" +
                                              " where calendardate > getdate() - 30   \r\n" +
                                              "and calendardate < getdate()   \r\n" +
                                            " and description =  '" + wpaa + "' group by calendardate) a \r\n " +
                                            " left outer join \r\n " +
                                            " (select answerdate from  tbl_Call_Center where worktypecat <> 'DEV1' \r\n " +
                                            //" and shaftid = '" + Shaft + "'
                                            "and answerdate >= CONVERT(VARCHAR(10),getdate()-30, 20) \r\n " +
                                            " and wpequipno = '" + wpaa + "' and ShiftCode = 'D' \r\n " +
                                            " group by answerdate) b on a.calendardate = b.answerdate )a \r\n " +
                                        "  ";
                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ResultsTableName = "Data2";
                _dbMan2.ExecuteInstruction();

                DataSet ReportDatasetReport2 = new DataSet();
                ReportDatasetReport2.Tables.Add(_dbMan2.ResultsDataTable);


                //////N/Shift

                MWDataManager.clsDataAccess _dbMan2NS = new MWDataManager.clsDataAccess();
                _dbMan2NS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2NS.SqlStatement = "  " +
                                     "  select MAX(col1) col1, MAX(col2) col2, MAX(col3) col3,MAX(col4) col4,MAX(col5) col5, \r\n " +
                                            " MAX(col6) col6, MAX(col7) col7, MAX(col8) col8 , MAX(col9) col9 , MAX(col10) col10, \r\n " +
                                            " MAX(col11) col11,MAX(col12) col12,MAX(col13) col13,MAX(col14) col14,MAX(col15) col15, \r\n " +
                                            " MAX(col16) col16,MAX(col17) col17,MAX(col18) col18,MAX(col19) col19,MAX(col20) col20, \r\n " +
                                            " MAX(col21) col21,MAX(col22) col22,MAX(col23) col23,MAX(col24) col24,MAX(col25) col25, \r\n " +
                                            " MAX(col26) col26,MAX(col27) col27,MAX(col28) col28,MAX(col29) col29,MAX(col30) col30, \r\n " +
                                            " MAX(col31) col31 \r\n " +

                                            " from(select  \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate(), 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate(), 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col1, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-1, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-1, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col2, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-2, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-2, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col3, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-3, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-3, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col4, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-4, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-4, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col5, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-5, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-5, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col6, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-6, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-6, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col7, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-7, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-7, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col8, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-8, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-8, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col9, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-9, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-9, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col10, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-10, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-10, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col11, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-11, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-11, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col12, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-12, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-12, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col13, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-13, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-13, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col14, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-14, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-14, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col15, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-15, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-15, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col16, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-16, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-16, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col17, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-17, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-17, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col18, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-18, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-18, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col19, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-19, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-19, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col20, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-20, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-20, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col21, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-21, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-21, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col22, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-22, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-22, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col23, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-23, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-23, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col24, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-24, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-24, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col25, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-25, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-25, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col26, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-26, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-26, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col27, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-27, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-27, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col28, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-28, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-28, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col29, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-29, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-29, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col30, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-30, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-30, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col31, \r\n " +


                                            " * from ( \r\n " +
                                             "Select calendardate, max(wd) wd from(   \r\n" +
  "select w.description, a.* from vw_Plansect_workDay_Combined a   \r\n" +


  "left outer join WORKPLACE w  on a.workplaceid = w.WorkplaceID) a   \r\n" +
  " where calendardate > getdate() - 30   \r\n" +
  "and calendardate < getdate()   \r\n" +
                                            " and description =  '" + wpaa + "' group by calendardate) a \r\n " +
                                            " left outer join \r\n " +
                                            " (select answerdate from  tbl_Call_Center where worktypecat <> 'DEV1' \r\n " +
                //" and shaftid = '" + Shaft + "' 
                "and answerdate >= CONVERT(VARCHAR(10),getdate()-30, 20) \r\n " +
                                            " and wpequipno = '" + wpaa + "' and ShiftCode = 'N' \r\n " +
                                            " group by answerdate) b on a.calendardate = b.answerdate )a \r\n " +
                                        "  ";
                _dbMan2NS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2NS.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2NS.ResultsTableName = "DataNS";
                _dbMan2NS.ExecuteInstruction();

                DataSet ReportDatasetReport2NS = new DataSet();
                ReportDatasetReport2NS.Tables.Add(_dbMan2NS.ResultsDataTable);


                MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan3.SqlStatement = "  \r\n " +
                                        "  select Top 10 QuestionDescr, num, colOrder from( select \r\n " +
                                        "  QuestionDescr, count(QuestionDescr) num, 'a' colOrder from  tbl_Call_Center where worktypecat = 'STP' \r\n " +
                                        " and shaftid = '" + Shaft + "' and answerdate >= CONVERT(VARCHAR(10),getdate()-30, 20) \r\n " +
                                        " and wpequipno = '" + wpaa + "' \r\n " +
                                        " and ragind = 'R' group by QuestionDescr \r\n " +
                                        " union  \r\n " +
                                        " select ' ',0,'z1' \r\n " +
                                        " union \r\n " +
                                        " select '  ',0,'z2' \r\n " +
                                        " union  \r\n " +
                                        " select '   ',0,'z3' \r\n " +
                                        " union  \r\n " +
                                        " select '    ',0,'z4' \r\n " +
                                        " union  \r\n " +
                                        " select '     ',0,'z5' \r\n " +
                                        " union \r\n " +
                                        " select '      ',0,'z6' \r\n " +
                                        " union  \r\n " +
                                        " select '       ',0,'z7' \r\n " +
                                        " union  \r\n " +
                                        " select '        ',0,'z8' \r\n " +
                                        " union  \r\n " +
                                        " select '         ',0,'z9' \r\n " +
                                        " union \r\n " +
                                        " select '          ',0,'z10')a \r\n " +
                                        " Order by colOrder, num desc \r\n " +
                                        " \r\n ";
                _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan3.ResultsTableName = "Data3";
                _dbMan3.ExecuteInstruction();

                DataSet ReportDatasetReport3 = new DataSet();
                ReportDatasetReport3.Tables.Add(_dbMan3.ResultsDataTable);

                MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
                _dbManDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManDate.SqlStatement = "select getdate()" +

                                            "  ";
                _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManDate.ResultsTableName = "Date";
                _dbManDate.ExecuteInstruction();

                DataSet ReportDatasetReport4 = new DataSet();
                ReportDatasetReport4.Tables.Add(_dbManDate.ResultsDataTable);

                MWDataManager.clsDataAccess _dbManGenData = new MWDataManager.clsDataAccess();
                _dbManGenData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManGenData.SqlStatement = " select '" + wpaa + "' WP,  '" + SysSettings.Banner + "' mine, '" + visible1 + "' mine ";
                _dbManGenData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGenData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGenData.ResultsTableName = "GenData";
                _dbManGenData.ExecuteInstruction();

                DataSet ReportDatasetReport5 = new DataSet();
                ReportDatasetReport5.Tables.Add(_dbManGenData.ResultsDataTable);



                //////WPLicToOpCall.frx

                /////////////////////////////////////////////////////////////////////////////


                //////////////////////3.Critical Skills////////////////////////////////////// 


                MWDataManager.clsDataAccess _dbManWPData = new MWDataManager.clsDataAccess();
                _dbManWPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWPData.SqlStatement = "select  top(1) EmployeeNo  miner,pm.OrgUnitDay+'                                         ' org, w.Description " +
                                            "from planning p, workplace w, planmonth pm, section s " +
                                            "where p.calendardate = CONVERT(VARCHAR(10),getdate(), 20) " +
                                            "and p.workplaceid = w.workplaceid and p.prodmonth = pm.prodmonth " +
                                            "and p.workplaceid = pm.workplaceid and p.activity = pm.activity and p.sectionid = pm.sectionid " +
                                            "and p.prodmonth = s.prodmonth and p.sectionid = s.sectionid " +
                                            "and w.description = '" + wpaa + "'  order by calendardate desc, pm.OrgUnitDay desc ";
                _dbManWPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPData.ResultsTableName = "RawData";
                _dbManWPData.ExecuteInstruction();


                string miner = "";
                string org = "";
                string wp = "";

                if (_dbManWPData.ResultsDataTable.Rows.Count > 0)
                {

                    miner = _dbManWPData.ResultsDataTable.Rows[0]["miner"].ToString();
                    org = _dbManWPData.ResultsDataTable.Rows[0]["org"].ToString().Substring(0, 10);
                    wp = _dbManWPData.ResultsDataTable.Rows[0]["description"].ToString();
                }


                MWDataManager.clsDataAccess _dbManDateCS = new MWDataManager.clsDataAccess();
                _dbManDateCS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManDateCS.SqlStatement = "select getdate() , '" + miner + "' mm, '" + org + "'  org " +

                                            "  ";
                _dbManDateCS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManDateCS.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManDateCS.ResultsTableName = "DateCS";
                _dbManDateCS.ExecuteInstruction();

                DataSet ReportDatasetReportCS = new DataSet();
                ReportDatasetReportCS.Tables.Add(_dbManDateCS.ResultsDataTable);


                MWDataManager.clsDataAccess _dbManMinerMain = new MWDataManager.clsDataAccess();
                _dbManMinerMain.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManMinerMain.SqlStatement = " " +

                   " select a.*, case when ug is null then 'N' else 'Y' end as UG from (select 'a' a, reader_description, max(clock_time) cc " +
                    " from [tbl_LicenceToOperate_Labour_Import]  " +
                    " where emp_Empno = '" + miner + "'   and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20)  " +
                    " group by reader_description) a  left outer join " +
                    " (select 'a' a, max(clock_time) ug from [tbl_LicenceToOperate_Labour_Import]  " +
                    " where emp_Empno = '" + miner + "'   and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20)  " +
                    " and reader_code like '%UG%') b on a.a = b.a " +

                    //"  and reader_description like '%Underground%') b on a.a = b.a " +


                    " order by cc   ";

                _dbManMinerMain.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManMinerMain.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManMinerMain.ResultsTableName = "MinerToDay";
                _dbManMinerMain.ExecuteInstruction();

                // DataSet ReportDatasetReport = new DataSet();
                ReportDatasetReportCS.Tables.Add(_dbManMinerMain.ResultsDataTable);

                ////Last Clocking Miner

                MWDataManager.clsDataAccess _dbManMinerMainLastClock = new MWDataManager.clsDataAccess();
                _dbManMinerMainLastClock.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManMinerMainLastClock.SqlStatement = " " +

                   "  select top(1)  clock_time, READER_DESCRIPTION, emp_empno  from [tbl_LicenceToOperate_Labour_Import]    " +
                    " where emp_Empno = '" + miner + "'   " +
                    " and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20) order by clock_time desc  ";

                _dbManMinerMainLastClock.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManMinerMainLastClock.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManMinerMainLastClock.ResultsTableName = "MinerToDayLastClock";
                _dbManMinerMainLastClock.ExecuteInstruction();


                ReportDatasetReportCS.Tables.Add(_dbManMinerMainLastClock.ResultsDataTable);

                // miner histrory
                MWDataManager.clsDataAccess _dbManMinerHist = new MWDataManager.clsDataAccess();
                _dbManMinerHist.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManMinerHist.SqlStatement = " select emp_empno, max(emp_name) name \r\n" +
                                               " ,max(Day40) Day40, max(Day39) Day39, max(Day38) Day38, max(Day37) Day37, max(Day36) Day36, max(Day35) Day35 \r\n" +
                                               " ,max(Day34) Day34, max(Day33) Day33, max(Day32) Day32, max(Day31) Day31, max(Day30) Day30, max(Day29) Day29 \r\n" +

                                               " ,max(Day28) Day28, max(Day27) Day27, max(Day26) Day26, max(Day25) Day25 \r\n" +
                                               " ,max(Day24) Day24, max(Day23) Day23, max(Day22) Day22, max(Day21) Day21, max(Day20) Day20, max(Day19) Day19 \r\n" +

                                               " ,max(Day18) Day18, max(Day17) Day17, max(Day16) Day16, max(Day15) Day15 \r\n" +
                                               " ,max(Day14) Day14, max(Day13) Day13, max(Day12) Day12, max(Day11) Day11, max(Day10) Day10 \r\n" +

                                               "  from (SELECT emp_empno, emp_name, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate(), 20) then day2 else '' end as Day40, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-1, 20) then day2 else '' end as Day39, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-2, 20) then day2 else '' end as Day38, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-3, 20) then day2 else '' end as Day37, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-4, 20) then day2 else '' end as Day36, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-5, 20) then day2 else '' end as Day35, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-6, 20) then day2 else '' end as Day34, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-7, 20) then day2 else '' end as Day33, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-8, 20) then day2 else '' end as Day32, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-9, 20) then day2 else '' end as Day31, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-10, 20) then day2 else '' end as Day30, \r\n" +

                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-11, 20) then day2 else '' end as Day29, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-12, 20) then day2 else '' end as Day28, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-13, 20) then day2 else '' end as Day27, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-14, 20) then day2 else '' end as Day26, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-15, 20) then day2 else '' end as Day25, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-16, 20) then day2 else '' end as Day24, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-17, 20) then day2 else '' end as Day23, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-18, 20) then day2 else '' end as Day22, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-19, 20) then day2 else '' end as Day21, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-20, 20) then day2 else '' end as Day20, \r\n" +

                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-21, 20) then day2 else '' end as Day19, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-22, 20) then day2 else '' end as Day18, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-23, 20) then day2 else '' end as Day17, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-24, 20) then day2 else '' end as Day16, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-25, 20) then day2 else '' end as Day15, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-26, 20) then day2 else '' end as Day14, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-27, 20) then day2 else '' end as Day13, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-28, 20) then day2 else '' end as Day12, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-29, 20) then day2 else '' end as Day11, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-30, 20) then day2 else '' end as Day10 \r\n" +
                                               "  from  tbl_Attendance \r\n" +
                                               "  where   \r\n" +
                                               "  convert(varchar(20),attendance_date)+emp_empno in  \r\n" +
                                               " (select convert(varchar(20),attendance_date)+emp_empno from  \r\n" +
                                               " tbl_Attendance where emp_Empno ='" + miner + "' ) \r\n" +
                                               " and emp_empno in (select distinct(emp_empno) a from  [tbl_LicenceToOperate_Labour_Import] \r\n" +
                                               " where emp_Empno = '" + miner + "' \r\n" +
                                               " and CONVERT(VARCHAR(10),clock_time, 20) >= CONVERT(VARCHAR(10),getdate()-30, 20))) a \r\n" +
                                               " group by emp_empno \r\n " +

                                               " order by emp_empno desc \r\n";
                _dbManMinerHist.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManMinerHist.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManMinerHist.ResultsTableName = "MinerHist";
                _dbManMinerHist.ExecuteInstruction();

                string Minerpf = "";

                if (_dbManMinerHist.ResultsDataTable.Rows.Count > 0)
                    Minerpf = _dbManMinerHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();

                DataSet ReportDatasetReportCS1 = new DataSet();
                ReportDatasetReportCS1.Tables.Add(_dbManMinerHist.ResultsDataTable);





                // teamleader main
                MWDataManager.clsDataAccess _dbManTeamMain = new MWDataManager.clsDataAccess();
                _dbManTeamMain.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManTeamMain.SqlStatement = " \r\n" +

                                                "   declare @minerTL varchar(20) \r\n" +

                                                " set @minerTL = (select  max(clock_time) ug from [tbl_LicenceToOperate_Labour_Import]  \r\n" +
                                                " where gang_number = '" + org + "' and wage_Description like '%TEAM%'  \r\n" +
                                                " and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20)  \r\n" +
                                               //" and reader_description like '%Underground%') \r\n" +

                                               "  and reader_code like '%UG%'  and  (reader_description like '%in%')) " +

                                                " select a.*, case when @minerTL is null then 'N' else 'Y' end as UG from (select 'a' a,'" + org + "' oo, reader_description, max(clock_time) cc from  [dbo].[tbl_LicenceToOperate_Labour_Import]  \r\n" +
                                                " where gang_number = '" + org + "' and wage_Description like '%TEAM%'  \r\n" +
                                                " and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20)  \r\n" +
                                                " group by reader_description ) a   \r\n" +
                                                " order by cc   \r\n";

                _dbManTeamMain.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManTeamMain.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManTeamMain.ResultsTableName = "TeamToDay";
                _dbManTeamMain.ExecuteInstruction();

                DataSet ReportDatasetReportCS2 = new DataSet();
                ReportDatasetReportCS2.Tables.Add(_dbManTeamMain.ResultsDataTable);

                ////Last Clocking TL

                MWDataManager.clsDataAccess _dbManTLMainLastClock = new MWDataManager.clsDataAccess();
                _dbManTLMainLastClock.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManTLMainLastClock.SqlStatement = " " +

                   "  select top(1) clock_time, READER_DESCRIPTION, emp_empno  from [tbl_LicenceToOperate_Labour_Import]   " +
                    " where gang_number = '" + org + "' and wage_Description like '%TEAM%'   " +
                    " and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20) order by clock_time desc  ";

                _dbManTLMainLastClock.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManTLMainLastClock.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManTLMainLastClock.ResultsTableName = "TLToDayLastClock";
                _dbManTLMainLastClock.ExecuteInstruction();

                ReportDatasetReportCS.Tables.Add(_dbManTLMainLastClock.ResultsDataTable);


                // teamleader main
                MWDataManager.clsDataAccess _dbManTeamHist = new MWDataManager.clsDataAccess();
                _dbManTeamHist.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManTeamHist.SqlStatement = " select  '" + org + "'  oo, emp_empno, max(emp_name) name  \r\n" +
                                               " ,max(Day40) Day40, max(Day39) Day39, max(Day38) Day38, max(Day37) Day37, max(Day36) Day36, max(Day35) Day35 \r\n" +
                                               " ,max(Day34) Day34, max(Day33) Day33, max(Day32) Day32, max(Day31) Day31, max(Day30) Day30, max(Day29) Day29 \r\n" +

                                               " ,max(Day28) Day28, max(Day27) Day27, max(Day26) Day26, max(Day25) Day25 \r\n" +
                                               " ,max(Day24) Day24, max(Day23) Day23, max(Day22) Day22, max(Day21) Day21, max(Day20) Day20, max(Day19) Day19 \r\n" +

                                               " ,max(Day18) Day18, max(Day17) Day17, max(Day16) Day16, max(Day15) Day15, \r\n" +
                                               " max(Day14) Day14, max(Day13) Day13, max(Day12) Day12, max(Day11) Day11, max(Day10) Day10 \r\n" +

                                               "  from (SELECT emp_empno, emp_name, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate(), 20) then day2 else '' end as Day40, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-1, 20) then day2 else '' end as Day39, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-2, 20) then day2 else '' end as Day38, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-3, 20) then day2 else '' end as Day37, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-4, 20) then day2 else '' end as Day36, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-5, 20) then day2 else '' end as Day35, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-6, 20) then day2 else '' end as Day34, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-7, 20) then day2 else '' end as Day33, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-8, 20) then day2 else '' end as Day32, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-9, 20) then day2 else '' end as Day31, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-10, 20) then day2 else '' end as Day30, \r\n" +

                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-11, 20) then day2 else '' end as Day29, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-12, 20) then day2 else '' end as Day28, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-13, 20) then day2 else '' end as Day27, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-14, 20) then day2 else '' end as Day26, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-15, 20) then day2 else '' end as Day25, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-16, 20) then day2 else '' end as Day24, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-17, 20) then day2 else '' end as Day23, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-18, 20) then day2 else '' end as Day22, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-19, 20) then day2 else '' end as Day21, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-20, 20) then day2 else '' end as Day20, \r\n" +

                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-21, 20) then day2 else '' end as Day19, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-22, 20) then day2 else '' end as Day18, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-23, 20) then day2 else '' end as Day17, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-24, 20) then day2 else '' end as Day16, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-25, 20) then day2 else '' end as Day15, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-26, 20) then day2 else '' end as Day14, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-27, 20) then day2 else '' end as Day13, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-28, 20) then day2 else '' end as Day12, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-29, 20) then day2 else '' end as Day11, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-30, 20) then day2 else '' end as Day10 \r\n" +
                                               "  from  tbl_Attendance \r\n" +
                                               "  where   \r\n" +
                                               "  convert(varchar(20),attendance_date)+emp_empno in  \r\n" +
                                               " (select convert(varchar(20),attendance_date)+emp_empno from   \r\n" +
                                               " tbl_Attendance where gang_number = '" + org + "'  ) \r\n" +
                                               " and emp_empno in (select distinct(emp_empno) a from  [tbl_LicenceToOperate_Labour_Import] \r\n" +
                                               " where gang_number = '" + org + "' and wage_Description like '%STOPE TEAM%' \r\n" +
                                               " and CONVERT(VARCHAR(10),clock_time, 20) >= CONVERT(VARCHAR(10),getdate()-30, 20))) a \r\n" +
                                               " group by emp_empno \r\n" +

                                               " order by emp_empno desc \r\n";

                _dbManTeamHist.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManTeamHist.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManTeamHist.ResultsTableName = "TeamHist";
                _dbManTeamHist.ExecuteInstruction();

                DataSet ReportDatasetReportCS3 = new DataSet();
                ReportDatasetReportCS3.Tables.Add(_dbManTeamHist.ResultsDataTable);


                string Team = "";

                if (_dbManTeamHist.ResultsDataTable.Rows.Count > 0)
                    Team = _dbManTeamHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();

                //Commented out
                string userPath = @"\\\\afzavrdat01\\vr\\LMSNew\\Images";
                string minpic = "";
                string tlpic = "";
                string pic = "";


                string min = "";
                string tl = "";

                //EmployeeImages
                if (_dbManMinerMainLastClock.ResultsDataTable.Rows.Count > 0)
                {
                    //pic = _dbManMinerMainLastClock.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                    //minpic = userPath + @"\" + pic + ".jpg";

                    //min = _dbManMinerMainLastClock.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                }


                if (_dbManMinerHist.ResultsDataTable.Rows.Count > 0)
                {
                    //pic = _dbManMinerHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                    //minpic = userPath + @"\" + pic + ".jpg";

                    //min = _dbManMinerHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                }



                if (_dbManTLMainLastClock.ResultsDataTable.Rows.Count > 0)
                {
                    //pic = _dbManTLMainLastClock.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                    //tlpic = userPath + @"\" + pic + ".jpg";
                    //tl = _dbManTLMainLastClock.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                }

                if (_dbManTeamHist.ResultsDataTable.Rows.Count > 0)
                {
                    //pic = _dbManTeamHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                    //tlpic = userPath + @"\" + pic + ".jpg";
                    //tl = _dbManTeamHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                }

                //if (VerifyGrid.CurrentRow == null)
                //{
                //    MessageBox.Show("Please select a incident", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}



                MWDataManager.clsDataAccess _dbManGenDataCS = new MWDataManager.clsDataAccess();
                _dbManGenDataCS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManGenDataCS.SqlStatement = " select *, b.employeename mmname, c.employeename tlname from ( select '" + minpic + "' minerpic, '" + tlpic + "' tlpic, '" + SysSettings.Banner + "' mine,  '" + Team + "' TeamCompNo,  '" + Minerpf + "' MinerCompNo , '" + wp + "' wp, '" + min + "' mm, '" + tl + "' tt ";
                _dbManGenDataCS.SqlStatement = _dbManGenDataCS.SqlStatement + "  ) a  left outer join employeeall b on a.mm = b.employeeno   left outer join employeeall c on a.tt = c.employeeno ";


                _dbManGenDataCS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGenDataCS.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGenDataCS.ResultsTableName = "GenDataCS";
                _dbManGenDataCS.ExecuteInstruction();

                DataSet ReportDatasetReportCS4 = new DataSet();
                ReportDatasetReportCS4.Tables.Add(_dbManGenDataCS.ResultsDataTable);




                //////WPLicToOpLab.frx

                /////////////////////////////////////////////////////////////////////////////



                //////////////4.Workplace Risk Profiles/////////////////




                MWDataManager.clsDataAccess _dbManWPST2RP = new MWDataManager.clsDataAccess();
                _dbManWPST2RP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                _dbManWPST2RP.SqlStatement = "select top (20) * from ( select 'z' bb, Action_Status Actionstatus, Action_Title Action, [Start_Date] datesubmitted, datediff(day,[Start_Date],getdate()) ss, Hazard, Action_Close_Date DateActionClosed  from tbl_Incidents       \r\n" +
                                            "where workplace = '" + wpaa + "'     \r\n" +
                                            "--and disciplinename = 'RMS'     \r\n" +
                                            "and Start_Date = (     \r\n" +
                                            "select max([Start_Date]) dd from tbl_Incidents     \r\n" +
                                            "where workplace = '" + wpaa + "'     \r\n" +
                                            "--and disciplinename = 'RMS'     \r\n" +
                                            ") group by Action_Title, Action_Status, [Start_Date],Hazard, Action_Close_Date      \r\n" +
                                            " union all \r\n" +
                                            " select 'a' , '', '', null, '', '' , '' \r\n" +
                                            " union all \r\n" +
                                            " select 'b ', '', '', null, '', '' , '' \r\n" +
                                            " union all \r\n" +
                                            " select 'c  ' , '', '', null, '', '' , '' \r\n" +
                                            " union \r\n" +
                                            " select 'd   ' , '', '', null, '', '' , '' \r\n" +
                                            " union \r\n" +
                                            " select 'e    ' , '', '', null, '', '' , '' \r\n" +
                                            " union \r\n" +
                                            " select 'f     ' , '', '', null, '', '' , '' \r\n" +
                                            " union \r\n" +
                                            " select 'g     ' , '', '', null, '', '' , '' \r\n" +
                                            " union \r\n" +
                                            " select 'h     ' , '', '', null, '', '' , '' \r\n" +
                                            " union \r\n" +
                                            " select 'i     ' , '', '', null, '', '' , '' \r\n" +
                                            " union \r\n" +
                                            " select 'j     ' , '', '', null, '', '' , '' \r\n" +
                                            " )a \r\n" +
                                            " order  by bb  \r\n";
                //_dbManWPST2RP.SqlStatement = "select top (20) * from ( select 'z' bb, Action_Status, Action_Title, [Start_Date], datediff(day,[Start_Date],getdate()) ss, Hazard, Action_Close_Date   from tbl_Incidents  \r\n" +
                //                           "where workplace = '" + wpaa + "' and disciplinename = 'RMS'  \r\n" +
                //                           "and Start_Date = (  \r\n" +

                //                           "select max([Start_Date]) dd from tbl_Incidents    \r\n" +
                //                           "where workplace = '" + wpaa + "' and disciplinename = 'RMS') group by Action_Title, Action_Status, [Start_Date],Hazard, Action_Close_Date   \r\n" +
                //                           " union all \r\n" +
                //                           " select 'a' , '', '', null, '', '' , '' \r\n" +
                //                           " union all \r\n" +
                //                           " select 'b ', '', '', null, '', '' , '' \r\n" +
                //                           " union all \r\n" +
                //                           " select 'c  ' , '', '', null, '', '' , '' \r\n" +
                //                           " union \r\n" +
                //                           " select 'd   ' , '', '', null, '', '' , '' \r\n" +
                //                           " union \r\n" +
                //                           " select 'e    ' , '', '', null, '', '' , '' \r\n" +
                //                           " union \r\n" +
                //                           " select 'f     ' , '', '', null, '', '' , '' \r\n" +
                //                           " union \r\n" +
                //                           " select 'g     ' , '', '', null, '', '' , '' \r\n" +
                //                           " union \r\n" +
                //                           " select 'h     ' , '', '', null, '', '' , '' \r\n" +
                //                           " union \r\n" +
                //                           " select 'i     ' , '', '', null, '', '' , '' \r\n" +
                //                           " union \r\n" +
                //                           " select 'j     ' , '', '', null, '', '' , '' \r\n" +
                //                           " )a \r\n" +
                //                           " order  by bb  \r\n";
                _dbManWPST2RP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPST2RP.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPST2RP.ResultsTableName = "HazardsRP";
                _dbManWPST2RP.ExecuteInstruction();




                DataSet dsABS1 = new DataSet();
                dsABS1.Tables.Add(_dbManWPST2RP.ResultsDataTable);



                MWDataManager.clsDataAccess _dbManVentDetailRP = new MWDataManager.clsDataAccess();
                _dbManVentDetailRP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetailRP.SqlStatement = "exec sp_LicenceToOperate_TempReadingWpRiskProf '" + wpaa + "'";

                _dbManVentDetailRP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetailRP.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetailRP.ResultsTableName = "TempDetailsRP";
                _dbManVentDetailRP.ExecuteInstruction();

                DataSet ReportDatasetReportRP1 = new DataSet();
                ReportDatasetReportRP1.Tables.Add(_dbManVentDetailRP.ResultsDataTable);






                MWDataManager.clsDataAccess _dbManHeaderRP = new MWDataManager.clsDataAccess();
                _dbManHeaderRP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHeaderRP.SqlStatement = "select '" + SysSettings.Banner + "' mine, '" + wpaa + "' workplace ";
                _dbManHeaderRP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHeaderRP.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHeaderRP.ResultsTableName = "HeaderRP";
                _dbManHeaderRP.ExecuteInstruction();

                DataSet ReportDatasetReportRP3 = new DataSet();
                ReportDatasetReportRP3.Tables.Add(_dbManHeaderRP.ResultsDataTable);






                //////WPLicToOpWPRiskProf.frx

                ////////////////////////////////////


                ///////////////Workplace Deviations///////////////////


                MWDataManager.clsDataAccess _dbManVentDetailWPD = new MWDataManager.clsDataAccess();
                _dbManVentDetailWPD.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetailWPD.SqlStatement = "exec [sp_LicenceToOperate_3)WorkplaceDeviations] '" + wpaa + "'";

                _dbManVentDetailWPD.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetailWPD.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetailWPD.ResultsTableName = "TempDetailsWPD";
                _dbManVentDetailWPD.ExecuteInstruction();

                DataSet ReportDatasetReportWPD1 = new DataSet();
                ReportDatasetReportWPD1.Tables.Add(_dbManVentDetailWPD.ResultsDataTable);




                MWDataManager.clsDataAccess _dbManVentDetailGraphWPD = new MWDataManager.clsDataAccess();
                _dbManVentDetailGraphWPD.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetailGraphWPD.SqlStatement = "exec [sp_LicenceToOperate_3)WorkplaceDeviationsGraph] '" + wpaa + "'";

                _dbManVentDetailGraphWPD.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetailGraphWPD.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetailGraphWPD.ResultsTableName = "TempDetailsGraphWPD";
                _dbManVentDetailGraphWPD.ExecuteInstruction();

                DataSet ReportDatasetReportWPD2 = new DataSet();
                ReportDatasetReportWPD2.Tables.Add(_dbManVentDetailGraphWPD.ResultsDataTable);



                MWDataManager.clsDataAccess _dbManHeaderWPD = new MWDataManager.clsDataAccess();
                _dbManHeaderWPD.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHeaderWPD.SqlStatement = "select '" + SysSettings.Banner + "' mine, '" + wpaa + "' workplace ";
                _dbManHeaderWPD.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHeaderWPD.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHeaderWPD.ResultsTableName = "HeaderWPD";
                _dbManHeaderWPD.ExecuteInstruction();

                DataSet ReportDatasetReportWPD3 = new DataSet();
                ReportDatasetReportWPD3.Tables.Add(_dbManHeaderWPD.ResultsDataTable);



                //////////WPLicToOpWPDev.frx
                /////////////////////////////////////


                //////////////////Major Hazards / Critical Controls Deviations//////////////////



                MWDataManager.clsDataAccess _dbManVentDetailMH = new MWDataManager.clsDataAccess();
                _dbManVentDetailMH.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetailMH.SqlStatement = "exec [dbo].[sp_LicenceToOperate_MajHazards] '" + wpaa + "'";

                _dbManVentDetailMH.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetailMH.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetailMH.ResultsTableName = "TempDetailsMH";
                _dbManVentDetailMH.ExecuteInstruction();

                DataSet ReportDatasetReportMH1 = new DataSet();
                ReportDatasetReportMH1.Tables.Add(_dbManVentDetailMH.ResultsDataTable);

                //Here

                MWDataManager.clsDataAccess _dbManHeaderMH = new MWDataManager.clsDataAccess();
                _dbManHeaderMH.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHeaderMH.SqlStatement = "select '" + SysSettings.Banner + "' mine, '" + wpaa + "' workplace ";
                _dbManHeaderMH.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHeaderMH.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHeaderMH.ResultsTableName = "HeaderMH";
                _dbManHeaderMH.ExecuteInstruction();

                DataSet ReportDatasetReportMH3 = new DataSet();
                ReportDatasetReportMH3.Tables.Add(_dbManHeaderMH.ResultsDataTable);



                ///////////WPLicToOpMajHazard.frx
                ///////////////////////////


                //////////////////////////Workplace Environmental Conditions//////////////////////////////////////


                MWDataManager.clsDataAccess _dbManVentDetailWEC = new MWDataManager.clsDataAccess();
                _dbManVentDetailWEC.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetailWEC.SqlStatement = "exec [dbo].[sp_LicenceToOperate_TempReading] '" + wpaa + "'";

                _dbManVentDetailWEC.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetailWEC.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetailWEC.ResultsTableName = "TempDetailsWEC";
                _dbManVentDetailWEC.ExecuteInstruction();

                DataSet ReportDatasetReportWEC1 = new DataSet();
                ReportDatasetReportWEC1.Tables.Add(_dbManVentDetailWEC.ResultsDataTable);


                MWDataManager.clsDataAccess _dbManVentPPWEC = new MWDataManager.clsDataAccess();
                _dbManVentPPWEC.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentPPWEC.SqlStatement = "select Start_Date Datesubmitted,criticalControl subtaskname,Action_Parent_Type stepname,action_status Actionstatus from tbl_Incidents ";
                _dbManVentPPWEC.SqlStatement = _dbManVentPPWEC.SqlStatement + " where Start_Date > getdate()-30 and Action_Parent_Type like '%tempe%' and workplace = '" + wpaa + "' and action_status not in ( 'Not A Deficiency', 'Not Applicable' )    ";
                _dbManVentPPWEC.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentPPWEC.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentPPWEC.ResultsTableName = "PaperlessWEC";
                _dbManVentPPWEC.ExecuteInstruction();

                DataSet ReportDatasetReportWEC2 = new DataSet();
                ReportDatasetReportWEC2.Tables.Add(_dbManVentPPWEC.ResultsDataTable);


                MWDataManager.clsDataAccess _dbManHeaderWEC = new MWDataManager.clsDataAccess();
                _dbManHeaderWEC.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHeaderWEC.SqlStatement = "select '" + SysSettings.Banner + "' mine, '" + wpaa + "' workplace ";
                _dbManHeaderWEC.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHeaderWEC.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHeaderWEC.ResultsTableName = "HeaderWEC";
                _dbManHeaderWEC.ExecuteInstruction();

                DataSet ReportDatasetReportWEC3 = new DataSet();
                ReportDatasetReportWEC3.Tables.Add(_dbManHeaderWEC.ResultsDataTable);




                /////////////WPLicToOpTemp.frx
                ///////////////////////////////////////////////////////////////////


                //////////////////Production////////////////////////////


                MWDataManager.clsDataAccess _dbManWPDetailProd = new MWDataManager.clsDataAccess();
                _dbManWPDetailProd.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //if (SysSettings.Banner == "Moab Khotsong")
                //{
                if (wpaa.Substring(0, 2) == "61" || wpaa.Substring(0, 2) == "64" || wpaa.Substring(0, 2) == "68" || wpaa.Substring(0, 2) == "70" || wpaa.Substring(0, 2) == "73" || wpaa.Substring(0, 2) == "74" || wpaa.Substring(0, 2) == "76")
                    _dbManWPDetailProd.SqlStatement = " select '" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, *, (select distsupred1 from sysset) distsupred, (select distsuporange1 from sysset) distsupOrange, (select distswpred1 from sysset) distswpred, (select distswporange1 from sysset) distswpOrange from workplace where description = '" + wpaa + "' ";
                else
                    _dbManWPDetailProd.SqlStatement = " select '" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, *, (select A_Color from sysset) distsupred, (select B_Color from sysset) distsupOrange, (select A_Color from sysset) distswpred, (select B_Color from sysset) distswpOrange from workplace where description = '" + wpaa + "' ";

                //_dbManWPDetail.SqlStatement = " select 'Backfill' SweepLbl,'" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, *, (select A_Color from sysset) distsupred, (select B_Color from sysset) distsupOrange, (select A_Color from sysset) distswpred, (select B_Color from sysset) distswpOrange from workplace where description = '" + wpaa + "' ";
                //}
                //else
                //    _dbManWPDetailProd.SqlStatement = " select '" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, *, (select distsupred from sysset) distsupred, (select distsuporange from sysset) distsupOrange, (select distswpred from sysset) distswpred, (select distswporange from sysset) distswpOrange from workplace where description = '" + wpaa + "' ";



                _dbManWPDetailProd.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPDetailProd.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPDetailProd.ResultsTableName = "WorkplaceDetailsProd";
                _dbManWPDetailProd.ExecuteInstruction();


                //MWDataManager.clsDataAccess _dbManWPDetailProd = new MWDataManager.clsDataAccess();
                //_dbManWPDetailProd.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbManWPDetailProd.SqlStatement = " select '" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, * from workplace where description = '" + wpaa + "' " +

                //                                "  ";

                //_dbManWPDetailProd.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManWPDetailProd.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManWPDetailProd.ResultsTableName = "WorkplaceDetailsProd";
                //_dbManWPDetailProd.ExecuteInstruction();


                DataSet ReportDatasetReportProd = new DataSet();
                ReportDatasetReportProd.Tables.Add(_dbManWPDetailProd.ResultsDataTable);

                if (_dbManWPDetailProd.ResultsDataTable.Rows.Count < 1)
                {
                    //  MessageBox.Show("No Data for selected criteria");
                    //  return;
                }
                else
                {
                    wpid = _dbManWPDetailProd.ResultsDataTable.Rows[0]["workplaceid"].ToString();
                }


                MWDataManager.clsDataAccess _dbManPSDetailProd = new MWDataManager.clsDataAccess();
                _dbManPSDetailProd.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManPSDetailProd.SqlStatement = " select pr.description, count(p.ProblemID) aa from planning p, CODE_PROBLEM pr     \r\n" +
                                                "  where p.problemid = pr.problemid and p.Activity = pr.Activity  \r\n" +
                                                "  and p.workplaceid = '" + wpid + "' and calendardate > getdate() - 30  group by pr.description    " +
                                                "  ";

                _dbManPSDetailProd.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManPSDetailProd.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManPSDetailProd.ResultsTableName = "Planned Stoppage Graph";
                _dbManPSDetailProd.ExecuteInstruction();

                DataSet ReportDatasetReportPSProd = new DataSet();
                ReportDatasetReportPSProd.Tables.Add(_dbManPSDetailProd.ResultsDataTable);


                MWDataManager.clsDataAccess _dbManProbDetailProd = new MWDataManager.clsDataAccess();
                _dbManProbDetailProd.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManProbDetailProd.SqlStatement = " select c.description, count(description) aa  from planning p, Code_Cycle c   " +
                                                " where p.BookCode = c.CycleCode   COLLATE Latin1_General_CI_AS and  workplaceid = '" + wpid + "' and calendardate > getdate() - 30 " +
                                                " group by c.description " +
                                                "  ";

                _dbManProbDetailProd.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManProbDetailProd.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManProbDetailProd.ResultsTableName = "Problem GraphProd";
                _dbManProbDetailProd.ExecuteInstruction();

                DataSet ReportDatasetReportProbProd = new DataSet();
                ReportDatasetReportProbProd.Tables.Add(_dbManProbDetailProd.ResultsDataTable);

                /////New////////

                MWDataManager.clsDataAccess _dbManVentDetailProd = new MWDataManager.clsDataAccess();
                _dbManVentDetailProd.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetailProd.SqlStatement = "exec [dbo].[sp_LicenceToOperate_TempReadingProd] '" + wpaa + "'";

                _dbManVentDetailProd.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetailProd.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetailProd.ResultsTableName = "TempDetailsProd";
                _dbManVentDetailProd.ExecuteInstruction();

                DataSet ReportDatasetReportProd1 = new DataSet();
                ReportDatasetReportProd1.Tables.Add(_dbManVentDetailProd.ResultsDataTable);



                ///////////////////WPLicToOpProd.frx
                /////////////////////////////////////////////////////////


                ////////////////////////////Rock Engineer Dept.////////////////////////////




                MWDataManager.clsDataAccess _dbManRMDetailRE = new MWDataManager.clsDataAccess();
                _dbManRMDetailRE.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManRMDetailRE.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] a, workplace w " +
                                                "where a.workplace = w.description and workplace  = '" + wpaa + "' " +
                                                "and captdate = (select  " +
                                                "max(captdate) from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + wpaa + "' ) ";
                _dbManRMDetailRE.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManRMDetailRE.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManRMDetailRE.ResultsTableName = "RMDetailsRE";
                _dbManRMDetailRE.ExecuteInstruction();





                string WPLbl = "00";
                string WkLbl = "00";
                string WkLbl2 = "00";
                string RRLbl = "0";
                //string RRLbl = "";

                if (_dbManRMDetailRE.ResultsDataTable.Rows.Count > 0)
                {

                    WPLbl = wpaa;
                    WkLbl = _dbManRMDetailRE.ResultsDataTable.Rows[0]["captweek"].ToString();
                    WkLbl2 = _dbManRMDetailRE.ResultsDataTable.Rows[0]["captweek"].ToString();
                    RRLbl = _dbManRMDetailRE.ResultsDataTable.Rows[0]["riskrating"].ToString(); //"0"

                }

                //EditLbl.Text = "1";


                MWDataManager.clsDataAccess _dbManWPST2RE = new MWDataManager.clsDataAccess();
                _dbManWPST2RE.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWPST2RE.SqlStatement = "select top (20) * from ( select 'z' bb,Action_Status ActionStatus,Action_Title action,[Start_Date] datesubmitted, datediff(day,[Start_Date],getdate()) ss  from tbl_Incidents  \r\n" +
                                            "where workplace = '" + WPLbl + "' \r\n" +
                                            " --and disciplinename = 'RMS' \r\n" +
                                            " and hazard = 'A'  \r\n" +
                                            "and [Start_Date] = (  \r\n" +

                                            "select max([Start_Date]) dd from tbl_Incidents    \r\n" +
                                            "where workplace = '" + WPLbl + "'  \r\n" +
                                            "--and disciplinename = 'RMS' \r\n" +
                                            " and hazard = 'A') group by Action_Title, Action_Status, [Start_Date]  \r\n" +
                                            " union all \r\n" +
                                            " select 'a' , '', '', null, '' \r\n" +
                                            " union all \r\n" +
                                            " select 'b ', '', '', null, '' \r\n" +
                                            " union all \r\n" +
                                            " select 'c  ' , '', '', null, '' \r\n" +
                                            " union \r\n" +
                                            " select 'd   ' , '', '', null, '' \r\n" +
                                            " union \r\n" +
                                            " select 'e    ' , '', '', null, '' \r\n" +
                                            " union \r\n" +
                                            " select 'f     ' , '', '', null, '' \r\n" +
                                            " union \r\n" +
                                            " select 'g     ' , '', '', null, '' \r\n" +
                                            " union \r\n" +
                                            " select 'h     ' , '', '', null, '' \r\n" +
                                            " union \r\n" +
                                            " select 'i     ' , '', '', null, '' \r\n" +
                                            " union \r\n" +
                                            " select 'j     ' , '', '', null, '' \r\n" +
                                            " )a \r\n" +
                                            " order  by bb  \r\n";
                _dbManWPST2RE.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPST2RE.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPST2RE.ResultsTableName = "Table2RE";
                _dbManWPST2RE.ExecuteInstruction();




                DataSet dsABS1RE = new DataSet();
                dsABS1RE.Tables.Add(_dbManWPST2RE.ResultsDataTable);

                string aa = Application.StartupPath + "\\" + "Neil.bmp";

                MWDataManager.clsDataAccess _dbManRE = new MWDataManager.clsDataAccess();
                _dbManRE.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManRE.SqlStatement = " select '" + SysSettings.Banner + "' banner, '" + RRLbl + "' rr,  * from [dbo].[tbl_DPT_RockMechInspection] where workplace = '" + WPLbl + "' and captweek = convert(decimal(18,0),'" + WkLbl + "') ";
                _dbManRE.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManRE.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManRE.ResultsTableName = "DevSummary";
                _dbManRE.ExecuteInstruction();


                if (_dbManRE.ResultsDataTable.Rows.Count > 0)
                {

                    if (_dbManRE.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                    {

                        if (_dbManRE.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                        {
                            // PicBox.Image = Base64ToImage(_dbManRE.ResultsDataTable.Rows[0]["picture"].ToString(), WPLbl);
                        }

                        PicBox.Image.Save(Application.StartupPath + "\\" + "Neil.bmp");

                    }
                }


                MWDataManager.clsDataAccess _dbManImageRE = new MWDataManager.clsDataAccess();
                _dbManImageRE.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                _dbManImageRE.SqlStatement = "Select picture, '" + aa + "' pp from [dbo].[tbl_DPT_RockMechInspection] where workplace = '" + WPLbl + "' and captweek = '" + WkLbl + "' ";

                //MineWarePics$\"+BusUnit+"\\RockEng


                _dbManImageRE.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManImageRE.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManImageRE.ResultsTableName = "ImageRE";
                _dbManImageRE.ExecuteInstruction();


                MWDataManager.clsDataAccess _dbManWPST21 = new MWDataManager.clsDataAccess();
                _dbManWPST21.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWPST21.SqlStatement = "select * from  tbl_LicenceToOperate_Seismic where wpdescription = '" + WPLbl + "' order by thedate desc";

                _dbManWPST21.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPST21.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPST21.ResultsTableName = "Graphree";
                _dbManWPST21.ExecuteInstruction();

                DataSet dsABS111 = new DataSet();
                dsABS111.Tables.Add(_dbManWPST21.ResultsDataTable);


                DataSet ReportDatasetReportRE = new DataSet();
                ReportDatasetReportRE.Tables.Add(_dbManRE.ResultsDataTable);



                DataSet ReportDatasetReportImageRE = new DataSet();
                ReportDatasetReportImageRE.Tables.Add(_dbManImageRE.ResultsDataTable);


                /////////////////////////////
                ////////////////////////////////////////////////////////

                ////////////////////////////GeoScience Dept.////////////////////////////////////////////////////////




                MWDataManager.clsDataAccess _dbManRMDetailG = new MWDataManager.clsDataAccess();
                _dbManRMDetailG.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManRMDetailG.SqlStatement = "select * from [dbo].[tbl_DPT_GeoScienceInspection] a, workplace w " +
                                                "where a.workplace = w.description and workplace  = '" + wpaa + "' " +
                                                "and captdate = (select  " +
                                                "max(captdate) from [dbo].[tbl_DPT_GeoScienceInspection] where workplace  = '" + wpaa + "' ) ";
                _dbManRMDetailG.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManRMDetailG.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManRMDetailG.ResultsTableName = "RMDetailsG";
                _dbManRMDetailG.ExecuteInstruction();

                string WPLblG = "0";
                string WkLblG = "0";
                string WkLbl2G = "0";
                string RRLblG = "0";


                if (_dbManRMDetailG.ResultsDataTable.Rows.Count > 0)
                {
                    WPLblG = wpaa;
                    WkLblG = _dbManRMDetailG.ResultsDataTable.Rows[0]["actweek"].ToString();
                    WkLbl2G = _dbManRMDetailG.ResultsDataTable.Rows[0]["captweek"].ToString();
                    RRLblG = _dbManRMDetailG.ResultsDataTable.Rows[0]["riskrating"].ToString(); //"0";
                    //FPMessagefrm.EditLbl.Text = "1";
                }




                MWDataManager.clsDataAccess _dbManWPST2G = new MWDataManager.clsDataAccess();
                _dbManWPST2G.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWPST2G.SqlStatement = "select top (20) * from ( select 'z' bb,Action_Status ActionStatus,Action_Title action,Start_Date datesubmitted, datediff(day,Start_Date,getdate()) ss  from tbl_Incidents  \r\n" +
                                        "where workplace = '" + WPLblG + "'  \r\n" +
                                        "--and disciplinename = 'RMS' \r\n" +
                                        " and hazard = 'A'  \r\n" +
                                        "and Start_Date = (  \r\n" +

                                        "select max(Start_Date) dd from tbl_Incidents   \r\n" +
                                        "where workplace = '" + WPLblG + "'  \r\n" +
                                        "--and disciplinename = 'RMS' \r\n" +
                                        " and hazard = 'A') group by Action_Title, Action_Status, Start_Date  \r\n" +
                                        " union all \r\n" +
                                        " select 'a' , '', '', null, '' \r\n" +
                                        " union all \r\n" +
                                        " select 'b ', '', '', null, '' \r\n" +
                                        " union all \r\n" +
                                        " select 'c  ' , '', '', null, '' \r\n" +
                                        " union \r\n" +
                                        " select 'd   ' , '', '', null, '' \r\n" +
                                        " union \r\n" +
                                        " select 'e    ' , '', '', null, '' \r\n" +
                                        " union \r\n" +
                                        " select 'f     ' , '', '', null, '' \r\n" +
                                        " union \r\n" +
                                        " select 'g     ' , '', '', null, '' \r\n" +
                                        " union \r\n" +
                                        " select 'h     ' , '', '', null, '' \r\n" +
                                        " union \r\n" +
                                        " select 'i     ' , '', '', null, '' \r\n" +
                                        " union \r\n" +
                                        " select 'j     ' , '', '', null, '' \r\n" +
                                        " )a \r\n" +
                                        " order  by bb  \r\n";
                _dbManWPST2G.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPST2G.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPST2G.ResultsTableName = "Table2G";
                _dbManWPST2G.ExecuteInstruction();




                DataSet dsABS1G = new DataSet();
                dsABS1G.Tables.Add(_dbManWPST2G.ResultsDataTable);



                MWDataManager.clsDataAccess _dbManG = new MWDataManager.clsDataAccess();
                _dbManG.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManG.SqlStatement = " select '" + SysSettings.Banner + "' banner, '" + RRLblG + "' rr,  * from [dbo].[tbl_DPT_GeoScienceInspection] where workplace = '" + WPLblG + "' and captweek = '" + WkLbl2G + "' ";
                _dbManG.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManG.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManG.ResultsTableName = "DevSummaryG";
                _dbManG.ExecuteInstruction();





                MWDataManager.clsDataAccess _dbManImageG = new MWDataManager.clsDataAccess();
                _dbManImageG.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                _dbManImageG.SqlStatement = "Select picture from [dbo].[tbl_DPT_GeoScienceInspection] where workplace = '" + WPLblG + "' and captweek = '" + WkLbl2G + "' ";

                //MineWarePics$\"+BusUnit+"\\RockEng


                _dbManImageG.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManImageG.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManImageG.ResultsTableName = "ImageG";
                _dbManImageG.ExecuteInstruction();


                DataSet ReportDatasetReportG = new DataSet();
                ReportDatasetReportG.Tables.Add(_dbManG.ResultsDataTable);



                DataSet ReportDatasetReportImageG = new DataSet();
                ReportDatasetReportImageG.Tables.Add(_dbManImageG.ResultsDataTable);







                ////////////////
                ////////////////////////////////////////////////////////////////////////////////////
                // CompA
                CallCentreApp.RegisterData(ReportDatasetCompADetail);
                CallCentreApp.RegisterData(ReportDatasetCompADetailA);
                CallCentreApp.RegisterData(ReportDatasetCompADetailB);

                ///CallCentreApp
                CallCentreApp.RegisterData(ReportDatasetReport2NS);
                CallCentreApp.RegisterData(ReportDatasetReport1);
                CallCentreApp.RegisterData(ReportDatasetReport2);
                CallCentreApp.RegisterData(ReportDatasetReport3);
                CallCentreApp.RegisterData(ReportDatasetReport4);
                CallCentreApp.RegisterData(ReportDatasetReport5);

                ///CriticalSkills
                CallCentreApp.RegisterData(ReportDatasetReportCS);
                CallCentreApp.RegisterData(ReportDatasetReportCS1);
                CallCentreApp.RegisterData(ReportDatasetReportCS2);
                CallCentreApp.RegisterData(ReportDatasetReportCS3);
                CallCentreApp.RegisterData(ReportDatasetReportCS4);


                ///Risk Profiles
                CallCentreApp.RegisterData(dsABS1);
                CallCentreApp.RegisterData(ReportDatasetReportRP1);
                CallCentreApp.RegisterData(ReportDatasetReportRP3);

                ///Wp Deviations
                CallCentreApp.RegisterData(ReportDatasetReportWPD3);
                CallCentreApp.RegisterData(ReportDatasetReportWPD2);
                CallCentreApp.RegisterData(ReportDatasetReportWPD1);

                //Major Hazards
                CallCentreApp.RegisterData(ReportDatasetReportMH1);
                CallCentreApp.RegisterData(ReportDatasetReportMH3);

                ////WP Environmental Conditions
                CallCentreApp.RegisterData(ReportDatasetReportWEC1);
                CallCentreApp.RegisterData(ReportDatasetReportWEC2);
                CallCentreApp.RegisterData(ReportDatasetReportWEC3);

                /////////////Production
                CallCentreApp.RegisterData(ReportDatasetReportProd1);
                CallCentreApp.RegisterData(ReportDatasetReportProd);
                CallCentreApp.RegisterData(ReportDatasetReportPSProd);
                CallCentreApp.RegisterData(ReportDatasetReportProbProd);



                ///Rock Engineering
                CallCentreApp.RegisterData(ReportDatasetReportImageRE);
                CallCentreApp.RegisterData(ReportDatasetReportRE);
                CallCentreApp.RegisterData(dsABS1RE);
                CallCentreApp.RegisterData(dsABS111);

                ///GeoInp
                CallCentreApp.RegisterData(dsABS1G);
                CallCentreApp.RegisterData(ReportDatasetReportG);
                CallCentreApp.RegisterData(ReportDatasetReportImageG);

                CallCentreApp.Load(TGlobalItems.ReportsFolder + "\\AllReports.frx");

                //CallCentreApp.Design();
                CallCentreApp.Show();




            }


            if (col1 == "Production" || col1 == "Mining Input")
            {
                MWDataManager.clsDataAccess _dbManWPDetail = new MWDataManager.clsDataAccess();
                _dbManWPDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                ////if (SysSettings.Banner == "Moab Khotsong")
                ////{
                if (wpaa.Substring(0, 2) == "61" || wpaa.Substring(0, 2) == "64" || wpaa.Substring(0, 2) == "68" || wpaa.Substring(0, 2) == "70" || wpaa.Substring(0, 2) == "73" || wpaa.Substring(0, 2) == "74" || wpaa.Substring(0, 2) == "76")
                    _dbManWPDetail.SqlStatement = " select 'Sweeps' SweepLbl, '" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, *, (select A_Color from sysset) distsupred, (select B_Color from sysset) distsupOrange, (select A_Color from sysset) distswpred, (select B_Color from sysset) distswpOrange from workplace where description = '" + wpaa + "' ";
                else
                    _dbManWPDetail.SqlStatement = " select 'Backfill' SweepLbl,'" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, *, (select A_Color from sysset) distsupred, (select B_Color from sysset) distsupOrange, (select A_Color from sysset) distswpred, (select B_Color from sysset) distswpOrange from workplace where description = '" + wpaa + "' ";

                //if (wpaa.Substring(0, 2) == "61" || wpaa.Substring(0, 2) == "64" || wpaa.Substring(0, 2) == "68" || wpaa.Substring(0, 2) == "70" || wpaa.Substring(0, 2) == "73" || wpaa.Substring(0, 2) == "74" || wpaa.Substring(0, 2) == "76")
                //    _dbManWPDetail.SqlStatement = " select 'Sweeps' SweepLbl, '" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, *, (select distsupred1 from sysset) distsupred, (select distsuporange1 from sysset) distsupOrange, (select distswpred1 from sysset) distswpred, (select distswporange1 from sysset) distswpOrange from workplace where description = '" + wpaa + "' ";
                //else
                //    _dbManWPDetail.SqlStatement = " select 'Backfill' SweepLbl,'" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, *, (select distsupred from sysset) distsupred, (select distsuporange from sysset) distsupOrange, (select distswpred from sysset) distswpred, (select distswporange from sysset) distswpOrange from workplace where description = '" + wpaa + "' ";

                ////}
                ////else
                ////{
                ////    if (SysSettings.Banner == "Mponeng")
                ////        _dbManWPDetail.SqlStatement = " select 'Backfill' SweepLbl,'" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, *, (select distsupred from sysset) distsupred, (select distsuporange from sysset) distsupOrange, (select distswpred from sysset) distswpred, (select distswporange from sysset) distswpOrange from workplace where description = '" + wpaa + "' ";
                ////    else
                ////        _dbManWPDetail.SqlStatement = " select 'Sweeps' SweepLbl,'" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, *, (select distsupred from sysset) distsupred, (select distsuporange from sysset) distsupOrange, (select distswpred from sysset) distswpred, (select distswporange from sysset) distswpOrange from workplace where description = '" + wpaa + "' ";
                ////}

                _dbManWPDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPDetail.ResultsTableName = "WorkplaceDetails";
                _dbManWPDetail.ExecuteInstruction();


                DataSet ReportDatasetReport = new DataSet();
                ReportDatasetReport.Tables.Add(_dbManWPDetail.ResultsDataTable);

                if (_dbManWPDetail.ResultsDataTable.Rows.Count < 1)
                {
                    //  MessageBox.Show("No Data for selected criteria");
                    //  return;
                }
                else
                {
                    wpid = _dbManWPDetail.ResultsDataTable.Rows[0]["workplaceid"].ToString();
                }


                MWDataManager.clsDataAccess _dbManPSDetail = new MWDataManager.clsDataAccess();
                _dbManPSDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManPSDetail.SqlStatement = " select pr.description, count(p.ProblemID) aa from planning p, CODE_PROBLEM pr     \r\n" +
                                                "  where p.problemid = pr.problemid and p.Activity = pr.Activity  \r\n" +
                                                "  and p.workplaceid = '" + wpid + "' and calendardate > getdate() - 30  group by pr.description    " +
                                                "  ";
                //_dbManPSDetail.SqlStatement = " select p.description, count(description) aa from problembook pb, problem p  \r\n" +
                //                                " where pb.problemid = p.problemid and  workplaceid = '" + wpid + "' and calendardate > getdate() - 30 " +
                //                                " group by p.description " +
                //                                "  ";

                _dbManPSDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManPSDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManPSDetail.ResultsTableName = "Planned Stoppage Graph";
                _dbManPSDetail.ExecuteInstruction();

                DataSet ReportDatasetReportPS = new DataSet();
                ReportDatasetReportPS.Tables.Add(_dbManPSDetail.ResultsDataTable);


                MWDataManager.clsDataAccess _dbManProbDetail = new MWDataManager.clsDataAccess();
                _dbManProbDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManProbDetail.SqlStatement = " select c.description, count(description) aa  from planning p, CODE_CYCLE c   " +
                                                " where p.BookCode = c.CycleCode  COLLATE Latin1_General_CI_AS and  workplaceid = '" + wpid + "' and calendardate > getdate() - 30 " +
                                                " group by c.description " +
                                                "  ";

                _dbManProbDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManProbDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManProbDetail.ResultsTableName = "Problem Graph";
                _dbManProbDetail.ExecuteInstruction();

                DataSet ReportDatasetReportProb = new DataSet();
                ReportDatasetReportProb.Tables.Add(_dbManProbDetail.ResultsDataTable);

                /////New////////

                MWDataManager.clsDataAccess _dbManVentDetail = new MWDataManager.clsDataAccess();
                _dbManVentDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetail.SqlStatement = "exec [dbo].[sp_LicenceToOperate_TempReadingProd] '" + wpaa + "'";

                _dbManVentDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetail.ResultsTableName = "TempDetails";
                _dbManVentDetail.ExecuteInstruction();

                DataSet ReportDatasetReport1 = new DataSet();
                ReportDatasetReport1.Tables.Add(_dbManVentDetail.ResultsDataTable);


                ProdRep.RegisterData(ReportDatasetReport1);

                /////////////

                ProdRep.RegisterData(ReportDatasetReport);
                ProdRep.RegisterData(ReportDatasetReportPS);
                ProdRep.RegisterData(ReportDatasetReportProb);

                if (tabControl2.SelectedIndex == 1)
                {
                    ProdRep.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpProdDev.frx");
                }
                else
                {
                    ProdRep.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpProd.frx");
                }

                //if (xtraTabControl1.SelectedTabPage == xtraTabControl1.TabPages[1])
                //{
                //    ProdRep.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpProdDev.frx");
                //}
                //else
                //{
                //    ProdRep.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpProd.frx");
                //}

                //ProdRep.Design();
                ProdRep.Show();
            }


            if (col1 == "Safe Declaration")
            {
                MWDataManager.clsDataAccess _dbManVentDetail = new MWDataManager.clsDataAccess();
                _dbManVentDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetail.SqlStatement = "exec [dbo].sp_LicenceToOperate_CompA '" + wpaa + "'";

                _dbManVentDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetail.ResultsTableName = "TempDetails";
                _dbManVentDetail.ExecuteInstruction();

                DataSet ReportDatasetReport1 = new DataSet();
                ReportDatasetReport1.Tables.Add(_dbManVentDetail.ResultsDataTable);


                TempRep.RegisterData(ReportDatasetReport1);



                MWDataManager.clsDataAccess _dbManVentPP = new MWDataManager.clsDataAccess();
                _dbManVentPP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentPP.SqlStatement = "select Start_Date Datesubmitted,criticalControl subtaskname,Action_Parent_Type stepname,action_status Actionstatus from tbl_Incidents";
                _dbManVentPP.SqlStatement = _dbManVentPP.SqlStatement + " where Start_Date > getdate()-30 and Action_Parent_Type like '%Safe Declaration%'  and workplace = '" + wpaa + "' and action_status <> 'Not A Deficiency'and action_status <> 'Not Applicable'  ";
                _dbManVentPP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentPP.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentPP.ResultsTableName = "Paperless";
                _dbManVentPP.ExecuteInstruction();

                DataSet ReportDatasetReport2 = new DataSet();
                ReportDatasetReport2.Tables.Add(_dbManVentPP.ResultsDataTable);


                TempRep.RegisterData(ReportDatasetReport2);



                MWDataManager.clsDataAccess _dbManHeader = new MWDataManager.clsDataAccess();
                _dbManHeader.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHeader.SqlStatement = "select '" + SysSettings.Banner + "' mine, '" + wpaa + "' workplace ";
                _dbManHeader.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHeader.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHeader.ResultsTableName = "Header";
                _dbManHeader.ExecuteInstruction();

                DataSet ReportDatasetReport3 = new DataSet();
                ReportDatasetReport3.Tables.Add(_dbManHeader.ResultsDataTable);


                TempRep.RegisterData(ReportDatasetReport3);





                TempRep.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpCompA.frx");

                //TempRep.Design();
                TempRep.Show();



            }

            if (col1 == "Workplace Risk Profiles")
            {
                Cursor = Cursors.WaitCursor;
                ////Hazards

                //MWDataManager.clsDataAccess _dbManWPST2 = new MWDataManager.clsDataAccess();
                //_dbManWPST2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbManWPST2.SqlStatement = "select top (20) *,   (select  min(datediff(day,Start_Date,getdate())) dd from tbl_Incidents  \r\n"+
                //"where workplace = '192 N5 E 4'  \r\n"+
                //"and (CriticalControl + Action_Parent_Type like 'RMSSafe%' or Action_Parent_Type like 'MP-MPI%' or Action_Parent_Type like 'KP-SI%') and Action_Status <> 'Not A Deficiency'  ) dd \r\n" + 
                //"from ( select 'z' bb, Action_Status, Action_Title,Start_Date datesubmitted, datediff(day,Start_Date,getdate()) ss, Hazard,Action_Close_Date DateActionClosed   from tbl_Incidents \r\n"+
                //" where workplace = '192 N5 E 4'  \r\n"+
                //"and(CriticalControl + Action_Parent_Type like 'RMSSafe%' or Action_Parent_Type like 'MP-MPI%' or Action_Parent_Type like 'KP-SI%') and Action_Status <> 'Not A Deficiency' \r\n" +
                //" and Start_Date = ( select max(Start_Date) dd from tbl_Incidents   \r\n"+
                //" where workplace = '192 N5 E 4'  \r\n" +
                //"and(CriticalControl + Action_Parent_Type like 'RMSSafe%' or Action_Parent_Type like 'MP-MPI%' or Action_Parent_Type like 'KP-SI%') \r\n" +
                //" and Action_Status <> 'Not A Deficiency' ) group by Action_Title, Action_Status, Start_Date,Hazard, Action_Close_Date  \r\n" +

                ////_dbManWPST2.SqlStatement = "select top (20) *,   (select  min(datediff(day,Start_Date,getdate())) dd from tbl_Incidents " +
                ////                             " where workplace = '" + wpaa + "'  " +

                ////                             // and disciplinename = 'RMS'  and taskname like '%Safe%'
                ////                             " and (disciplinename + taskname like 'RMSSafe%' or taskname like 'MP-MPI%' or taskname like 'KP-SI%') and actionstatus <> 'Not A Deficiency' " +


                ////                             " ) dd  from ( select 'z' bb, ActionStatus, action,Start_Date datesubmitted, datediff(day,Start_Date,getdate()) ss, Hazard, DateActionClosed   from tbl_Incidents \r\n" +
                ////                            "where workplace = '" + wpaa + "' " +
                ////                            " and (disciplinename + taskname like 'RMSSafe%' or taskname like 'MP-MPI%' or taskname like 'KP-SI%') and actionstatus <> 'Not A Deficiency' " +
                //////and disciplinename = 'RMS'  and taskname like '%Safe%' \r\n" +
                ////                            "and datesubmitted = (  \r\n" +

                ////                            "select max(datesubmitted) dd from tbl_Incidents    \r\n" +
                ////                            "where workplace = '" + wpaa + "' " +
                //////and disciplinename = 'RMS'  and taskname like '%Safe%'
                ////                           " and (disciplinename + taskname like 'RMSSafe%' or taskname like 'MP-MPI%' or taskname like 'KP-SI%') and actionstatus <> 'Not A Deficiency' " +

                ////                            ") group by Action, ActionStatus, DateSubmitted,Hazard, DateActionClosed   \r\n" +


                //" union all \r\n" +
                //                            " select 'a' , '', '', null, '', '' , '' \r\n" +
                //                            " union all \r\n" +
                //                            " select 'b ', '', '', null, '', '' , '' \r\n" +
                //                            " union all \r\n" +
                //                            " select 'c  ' , '', '', null, '', '' , '' \r\n" +
                //                            " union \r\n" +
                //                            " select 'd   ' , '', '', null, '', '' , '' \r\n" +
                //                            " union \r\n" +
                //                            " select 'e    ' , '', '', null, '', '' , '' \r\n" +
                //                            " union \r\n" +
                //                            " select 'f     ' , '', '', null, '', '' , '' \r\n" +
                //                            " union \r\n" +
                //                            " select 'g     ' , '', '', null, '', '' , '' \r\n" +
                //                            " union \r\n" +
                //                            " select 'h     ' , '', '', null, '', '' , '' \r\n" +
                //                            " union \r\n" +
                //                            " select 'i     ' , '', '', null, '', '' , '' \r\n" +
                //                            " union \r\n" +
                //                            " select 'j     ' , '', '', null, '', '' , '' \r\n" +
                //                            " )a \r\n" +
                //                            " order  by bb  \r\n";
                //_dbManWPST2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManWPST2.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManWPST2.ResultsTableName = "Hazards";
                //_dbManWPST2.ExecuteInstruction();


                MWDataManager.clsDataAccess _dbManWPST2 = new MWDataManager.clsDataAccess();
                _dbManWPST2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWPST2.SqlStatement = "  \r\n" +
                " select top (20) *, (select  min(datediff(day, Start_Date, getdate())) dd from[dbo].[tbl_Incidents_PivotTemp] \r\n" +

                                        " where workplace = '" + wpaa + "' \r\n" +

                                        " and dept = 'Safety'  ) dd \r\n" +

                                        " from(select 'z' bb, Action_Status, Action_Title, Start_Date datesubmitted, datediff(day, Start_Date, getdate()) ss, Hazard, Action_Close_Date DateActionClosed   from[dbo].[tbl_Incidents_PivotTemp] \r\n" +

                                        "  where workplace = '" + wpaa + "' \r\n" +

                                        " and dept = 'Safety' \r\n" +

                                        "  and Start_Date = (select max(Start_Date) dd from tbl_Incidents \r\n" +

                                        "  where workplace = '" + wpaa + "' \r\n" +

                                        " and dept = 'Safety') group by Action_Title, Action_Status, Start_Date,Hazard, Action_Close_Date \r\n" +

                                        " union all \r\n" +

                                        " select 'a' , '', '', null, '', '' , '' \r\n" +

                                        "  union all \r\n" +

                                        "  select 'b ', '', '', null, '', '' , '' \r\n" +

                                        "  union all \r\n" +

                                        "  select 'c  ' , '', '', null, '', '' , '' \r\n" +

                                        "  union \r\n" +

                                        "  select 'd   ' , '', '', null, '', '' , '' \r\n" +

                                        "  union \r\n" +

                                        "  select 'e    ' , '', '', null, '', '' , '' \r\n" +

                                        "  union \r\n" +

                                        "  select 'f     ' , '', '', null, '', '' , '' \r\n" +

                                        "  union \r\n" +

                                        "  select 'g     ' , '', '', null, '', '' , '' \r\n" +

                                        "  union \r\n" +

                                        "  select 'h     ' , '', '', null, '', '' , '' \r\n" +

                                        "  union \r\n" +

                                        "  select 'i     ' , '', '', null, '', '' , '' \r\n" +

                                         " union \r\n" +

                                         " select 'j     ' , '', '', null, '', '' , '' \r\n" +

                                        "  )a \r\n" +



                                                                " order  by bb  \r\n";
                _dbManWPST2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPST2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPST2.ResultsTableName = "Hazards";
                _dbManWPST2.ExecuteInstruction();




                DataSet dsABS1 = new DataSet();
                dsABS1.Tables.Add(_dbManWPST2.ResultsDataTable);

                TempRep.RegisterData(dsABS1);

                MWDataManager.clsDataAccess _dbManVentDetail = new MWDataManager.clsDataAccess();
                _dbManVentDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetail.SqlStatement = "exec sp_LicenceToOperate_TempReadingWpRiskProf '" + wpaa + "'";

                _dbManVentDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetail.ResultsTableName = "TempDetails";
                _dbManVentDetail.ExecuteInstruction();

                DataSet ReportDatasetReport1 = new DataSet();
                ReportDatasetReport1.Tables.Add(_dbManVentDetail.ResultsDataTable);


                TempRep.RegisterData(ReportDatasetReport1);



                MWDataManager.clsDataAccess _dbManHeader = new MWDataManager.clsDataAccess();
                _dbManHeader.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHeader.SqlStatement = "select '" + SysSettings.Banner + "' mine, '" + wpaa + "' workplace ";
                _dbManHeader.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHeader.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHeader.ResultsTableName = "Header";
                _dbManHeader.ExecuteInstruction();

                DataSet ReportDatasetReport3 = new DataSet();
                ReportDatasetReport3.Tables.Add(_dbManHeader.ResultsDataTable);


                TempRep.RegisterData(ReportDatasetReport3);





                TempRep.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpWPRiskProf.frx");

                // TempRep.Design();
                TempRep.Show();

                Cursor = Cursors.Default;
            }



            if (col1 == "S.U.E. Control Failure")
            {
                MWDataManager.clsDataAccess _dbManVentDetail = new MWDataManager.clsDataAccess();
                _dbManVentDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetail.SqlStatement = "exec [dbo].[sp_LicenceToOperate_MajHazards] '" + wpaa + "'";

                _dbManVentDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetail.ResultsTableName = "TempDetails";
                _dbManVentDetail.ExecuteInstruction();

                DataSet ReportDatasetReport1 = new DataSet();
                ReportDatasetReport1.Tables.Add(_dbManVentDetail.ResultsDataTable);

                MajHazRep.RegisterData(ReportDatasetReport1);

                MWDataManager.clsDataAccess _dbManHeader = new MWDataManager.clsDataAccess();
                _dbManHeader.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHeader.SqlStatement = "select '" + SysSettings.Banner + "' mine, '" + wpaa + "' workplace ";
                _dbManHeader.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHeader.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHeader.ResultsTableName = "Header";
                _dbManHeader.ExecuteInstruction();

                DataSet ReportDatasetReport3 = new DataSet();
                ReportDatasetReport3.Tables.Add(_dbManHeader.ResultsDataTable);


                MajHazRep.RegisterData(ReportDatasetReport3);





                MajHazRep.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpMajHazard.frx");

                // MajHazRep.Design();
                MajHazRep.Show();


            }

            if (col1 == "Workplace Deviations")
            {
                MWDataManager.clsDataAccess _dbManVentDetail = new MWDataManager.clsDataAccess();
                _dbManVentDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetail.SqlStatement = "exec [sp_LicenceToOperate_3)WorkplaceDeviations] '" + wpaa + "'";

                _dbManVentDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetail.ResultsTableName = "TempDetails";
                _dbManVentDetail.ExecuteInstruction();

                DataSet ReportDatasetReport1 = new DataSet();
                ReportDatasetReport1.Tables.Add(_dbManVentDetail.ResultsDataTable);

                WPDevRep.RegisterData(ReportDatasetReport1);


                MWDataManager.clsDataAccess _dbManVentDetailGraph = new MWDataManager.clsDataAccess();
                _dbManVentDetailGraph.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetailGraph.SqlStatement = "exec [sp_LicenceToOperate_3)WorkplaceDeviationsGraph] '" + wpaa + "'";

                _dbManVentDetailGraph.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetailGraph.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetailGraph.ResultsTableName = "TempDetailsGraph";
                _dbManVentDetailGraph.ExecuteInstruction();

                DataSet ReportDatasetReport2 = new DataSet();
                ReportDatasetReport2.Tables.Add(_dbManVentDetailGraph.ResultsDataTable);

                WPDevRep.RegisterData(ReportDatasetReport2);

                MWDataManager.clsDataAccess _dbManHeader = new MWDataManager.clsDataAccess();
                _dbManHeader.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHeader.SqlStatement = "select '" + SysSettings.Banner + "' mine, '" + wpaa + "' workplace ";
                _dbManHeader.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHeader.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHeader.ResultsTableName = "Header";
                _dbManHeader.ExecuteInstruction();

                DataSet ReportDatasetReport3 = new DataSet();
                ReportDatasetReport3.Tables.Add(_dbManHeader.ResultsDataTable);


                WPDevRep.RegisterData(ReportDatasetReport3);


                WPDevRep.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpWPDev.frx");

                //WPDevRep.Design();
                WPDevRep.Show();



            }

            if (col1 == "Workplace Environmental Conditions")
            {
                MWDataManager.clsDataAccess _dbManVentDetail = new MWDataManager.clsDataAccess();
                _dbManVentDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentDetail.SqlStatement = "exec [dbo].[sp_LicenceToOperate_TempReading] '" + wpaa + "'";

                _dbManVentDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentDetail.ResultsTableName = "TempDetails";
                _dbManVentDetail.ExecuteInstruction();

                DataSet ReportDatasetReport1 = new DataSet();
                ReportDatasetReport1.Tables.Add(_dbManVentDetail.ResultsDataTable);


                TempRep.RegisterData(ReportDatasetReport1);



                MWDataManager.clsDataAccess _dbManVentPP = new MWDataManager.clsDataAccess();
                _dbManVentPP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManVentPP.SqlStatement = "select Start_Date Datesubmitted,criticalControl subtaskname,Action_Parent_Type stepname,action_status Actionstatus from tbl_Incidents ";
                _dbManVentPP.SqlStatement = _dbManVentPP.SqlStatement + " where Start_Date > getdate()-30 and Action_Parent_Type like '%tempe%' and workplace = '" + wpaa + "' and action_status not in ( 'Not A Deficiency', 'Not Applicable' )     ";
                _dbManVentPP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVentPP.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVentPP.ResultsTableName = "Paperless";
                _dbManVentPP.ExecuteInstruction();

                DataSet ReportDatasetReport2 = new DataSet();
                ReportDatasetReport2.Tables.Add(_dbManVentPP.ResultsDataTable);


                TempRep.RegisterData(ReportDatasetReport2);



                MWDataManager.clsDataAccess _dbManHeader = new MWDataManager.clsDataAccess();
                _dbManHeader.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHeader.SqlStatement = "select '" + SysSettings.Banner + "' mine, '" + wpaa + "' workplace ";
                _dbManHeader.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHeader.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHeader.ResultsTableName = "Header";
                _dbManHeader.ExecuteInstruction();

                DataSet ReportDatasetReport3 = new DataSet();
                ReportDatasetReport3.Tables.Add(_dbManHeader.ResultsDataTable);


                TempRep.RegisterData(ReportDatasetReport3);

                TempRep.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpTemp.frx");

                //TempRep.Design();
                TempRep.Show();



            }

            if (col1 == "GeoScience Dept.")
            {
                if (vvv == "")
                {
                    MessageBox.Show("No Data available", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {

                    MWDataManager.clsDataAccess _dbManRMDetail = new MWDataManager.clsDataAccess();
                    _dbManRMDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManRMDetail.SqlStatement = "select substring(convert(varchar(10),captweek+100),2,3) aa, * from [dbo].[tbl_DPT_GeoScienceInspection] a, workplace w " +
                                                    "where a.workplace = w.description and workplace  = '" + wpaa + "' " +
                                                    "and captdate = (select  " +
                                                    "max(captdate) from [dbo].[tbl_DPT_GeoScienceInspection] where workplace  = '" + wpaa + "' ) ";
                    _dbManRMDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManRMDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManRMDetail.ResultsTableName = "RMDetails";
                    _dbManRMDetail.ExecuteInstruction();



                    GeoInspFrm FPMessagefrm = new GeoInspFrm();
                    FPMessagefrm.WPLbl.Text = wpaa;
                    FPMessagefrm.WkLbl.Text = _dbManRMDetail.ResultsDataTable.Rows[0]["aa"].ToString();
                    FPMessagefrm.WkLbl2.Text = "wk-" + _dbManRMDetail.ResultsDataTable.Rows[0]["aa"].ToString();
                    FPMessagefrm.RRLbl.Text = _dbManRMDetail.ResultsDataTable.Rows[0]["riskrating"].ToString(); //"0";
                    FPMessagefrm.EditLbl.Text = "1";
                    FPMessagefrm._theSystemDBTag = theSystemDBTag;
                    FPMessagefrm._UserCurrentInfoConnection = UserCurrentInfo.Connection;

                    FPMessagefrm.ShowDialog();

                }


            }

            if (col1 == "Call Centre Application")
            {


                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select * from  tbl_Call_Center where worktypecat <> 'DEV1' \r\n " +
                                        // " and shaftid = '" + Shaft + "' 
                                        "and answerdate = CONVERT(VARCHAR(10),getdate(), 20) \r\n " +
                                        " and wpequipno = '" + wpaa + "' \r\n " +
                                        " and ragind = 'R' \r\n " +
                                        " \r\n " +
                                        " \r\n " +
                                        " \r\n " +
                                        " \r\n " +
                                        " \r\n " +
                                        " \r\n ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "Data1";
                _dbMan1.ExecuteInstruction();

                string visible1 = "N";

                if (_dbMan1.ResultsDataTable.Rows.Count > 0)
                {
                    visible1 = "Y";
                }

                DataSet ReportDatasetReport1 = new DataSet();
                ReportDatasetReport1.Tables.Add(_dbMan1.ResultsDataTable);

                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement = "  " +
                                     "  select MAX(col1) col1, MAX(col2) col2, MAX(col3) col3,MAX(col4) col4,MAX(col5) col5, \r\n " +
                                            " MAX(col6) col6, MAX(col7) col7, MAX(col8) col8 , MAX(col9) col9 , MAX(col10) col10, \r\n " +
                                            " MAX(col11) col11,MAX(col12) col12,MAX(col13) col13,MAX(col14) col14,MAX(col15) col15, \r\n " +
                                            " MAX(col16) col16,MAX(col17) col17,MAX(col18) col18,MAX(col19) col19,MAX(col20) col20, \r\n " +
                                            " MAX(col21) col21,MAX(col22) col22,MAX(col23) col23,MAX(col24) col24,MAX(col25) col25, \r\n " +
                                            " MAX(col26) col26,MAX(col27) col27,MAX(col28) col28,MAX(col29) col29,MAX(col30) col30, \r\n " +
                                            " MAX(col31) col31 \r\n " +

                                            " from(select  \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate(), 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate(), 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col1, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-1, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-1, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col2, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-2, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-2, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col3, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-3, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-3, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col4, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-4, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-4, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col5, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-5, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-5, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col6, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-6, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-6, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col7, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-7, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-7, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col8, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-8, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-8, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col9, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-9, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-9, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col10, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-10, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-10, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col11, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-11, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-11, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col12, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-12, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-12, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col13, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-13, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-13, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col14, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-14, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-14, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col15, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-15, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-15, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col16, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-16, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-16, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col17, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-17, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-17, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col18, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-18, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-18, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col19, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-19, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-19, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col20, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-20, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-20, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col21, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-21, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-21, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col22, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-22, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-22, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col23, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-23, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-23, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col24, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-24, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-24, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col25, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-25, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-25, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col26, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-26, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-26, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col27, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-27, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-27, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col28, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-28, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-28, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col29, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-29, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-29, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col30, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-30, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-30, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col31, \r\n " +


                                            " * from ( \r\n " +
                                             "Select calendardate, max(wd) wd from(   \r\n" +
                                              "select w.description, a.* from vw_Plansect_workDay_Combined a   \r\n" +


                                              "left outer join WORKPLACE w  on a.workplaceid = w.WorkplaceID) a   \r\n" +
                                              " where calendardate > getdate() - 30   \r\n" +
                                              "and calendardate < getdate()   \r\n" +
                                            " and description =  '" + wpaa + "' group by calendardate) a \r\n " +
                                            " left outer join \r\n " +
                                            " (select answerdate from  tbl_Call_Center where worktypecat <> 'DEV1' \r\n " +
                                            //" and shaftid = '" + Shaft + "'
                                            "and answerdate >= CONVERT(VARCHAR(10),getdate()-30, 20) \r\n " +
                                            " and wpequipno = '" + wpaa + "' and ShiftCode = 'D' \r\n " +
                                            " group by answerdate) b on a.calendardate = b.answerdate )a \r\n " +
                                        "  ";
                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ResultsTableName = "Data2";
                _dbMan2.ExecuteInstruction();

                DataSet ReportDatasetReport2 = new DataSet();
                ReportDatasetReport2.Tables.Add(_dbMan2.ResultsDataTable);

                //////N/Shift

                MWDataManager.clsDataAccess _dbMan2NS = new MWDataManager.clsDataAccess();
                _dbMan2NS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2NS.SqlStatement = "  " +
                                     "  select MAX(col1) col1, MAX(col2) col2, MAX(col3) col3,MAX(col4) col4,MAX(col5) col5, \r\n " +
                                            " MAX(col6) col6, MAX(col7) col7, MAX(col8) col8 , MAX(col9) col9 , MAX(col10) col10, \r\n " +
                                            " MAX(col11) col11,MAX(col12) col12,MAX(col13) col13,MAX(col14) col14,MAX(col15) col15, \r\n " +
                                            " MAX(col16) col16,MAX(col17) col17,MAX(col18) col18,MAX(col19) col19,MAX(col20) col20, \r\n " +
                                            " MAX(col21) col21,MAX(col22) col22,MAX(col23) col23,MAX(col24) col24,MAX(col25) col25, \r\n " +
                                            " MAX(col26) col26,MAX(col27) col27,MAX(col28) col28,MAX(col29) col29,MAX(col30) col30, \r\n " +
                                            " MAX(col31) col31 \r\n " +

                                            " from(select  \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate(), 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate(), 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col1, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-1, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-1, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col2, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-2, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-2, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col3, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-3, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-3, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col4, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-4, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-4, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col5, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-5, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-5, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col6, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-6, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-6, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col7, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-7, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-7, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col8, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-8, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-8, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col9, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-9, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-9, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col10, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-10, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-10, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col11, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-11, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-11, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col12, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-12, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-12, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col13, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-13, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-13, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col14, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-14, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-14, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col15, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-15, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-15, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col16, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-16, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-16, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col17, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-17, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-17, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col18, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-18, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-18, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col19, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-19, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-19, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col20, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-20, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-20, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col21, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-21, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-21, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col22, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-22, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-22, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col23, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-23, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-23, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col24, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-24, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-24, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col25, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-25, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-25, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col26, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-26, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-26, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col27, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-27, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-27, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col28, \r\n " +

                                            " case  \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-28, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-28, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else ''  \r\n " +
                                            " end as col29, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-29, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-29, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col30, \r\n " +

                                            " case \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-30, 20) and wd = 'N' then 'OFF' \r\n " +
                                            " when calendardate = CONVERT(VARCHAR(10),getdate()-30, 20) and wd = 'Y' And AnswerDate IS not null then 'OK' \r\n " +
                                            " else '' \r\n " +
                                            " end as col31, \r\n " +


                                            " * from ( \r\n " +
                                             "Select calendardate, max(wd) wd from(   \r\n" +
  "select w.description, a.* from vw_Plansect_workDay_Combined a   \r\n" +


  "left outer join WORKPLACE w  on a.workplaceid = w.WorkplaceID) a   \r\n" +
  " where calendardate > getdate() - 30   \r\n" +
  "and calendardate < getdate()   \r\n" +
                                            " and description =  '" + wpaa + "' group by calendardate) a \r\n " +
                                            " left outer join \r\n " +
                                            " (select answerdate from  tbl_Call_Center where worktypecat <> 'DEV1' \r\n " +
                //" and shaftid = '" + Shaft + "' 
                "and answerdate >= CONVERT(VARCHAR(10),getdate()-30, 20) \r\n " +
                                            " and wpequipno = '" + wpaa + "' and ShiftCode = 'N' \r\n " +
                                            " group by answerdate) b on a.calendardate = b.answerdate )a \r\n " +
                                        "  ";
                _dbMan2NS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2NS.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2NS.ResultsTableName = "DataNS";
                _dbMan2NS.ExecuteInstruction();

                DataSet ReportDatasetReport2NS = new DataSet();
                ReportDatasetReport2NS.Tables.Add(_dbMan2NS.ResultsDataTable);




                MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan3.SqlStatement = "  \r\n " +
                                        "  select Top 10 QuestionDescr, num, colOrder from( select \r\n " +
                                        "  QuestionDescr, count(QuestionDescr) num, 'a' colOrder from  tbl_Call_Center where worktypecat <> 'STP1' \r\n " +
                //" and shaftid = '" + Shaft + "' 
                "and answerdate >= CONVERT(VARCHAR(10),getdate()-30, 20) \r\n " +
                                        " and wpequipno = '" + wpaa + "' \r\n " +
                                        " and ragind = 'R' group by QuestionDescr \r\n " +
                                        " union  \r\n " +
                                        " select ' ',0,'z1' \r\n " +
                                        " union \r\n " +
                                        " select '  ',0,'z2' \r\n " +
                                        " union  \r\n " +
                                        " select '   ',0,'z3' \r\n " +
                                        " union  \r\n " +
                                        " select '    ',0,'z4' \r\n " +
                                        " union  \r\n " +
                                        " select '     ',0,'z5' \r\n " +
                                        " union \r\n " +
                                        " select '      ',0,'z6' \r\n " +
                                        " union  \r\n " +
                                        " select '       ',0,'z7' \r\n " +
                                        " union  \r\n " +
                                        " select '        ',0,'z8' \r\n " +
                                        " union  \r\n " +
                                        " select '         ',0,'z9' \r\n " +
                                        " union \r\n " +
                                        " select '          ',0,'z10')a \r\n " +
                                        " Order by colOrder, num desc \r\n " +
                                        " \r\n ";
                _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan3.ResultsTableName = "Data3";
                _dbMan3.ExecuteInstruction();




                DataSet ReportDatasetReport3 = new DataSet();
                ReportDatasetReport3.Tables.Add(_dbMan3.ResultsDataTable);

                MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
                _dbManDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManDate.SqlStatement = "select getdate()" +

                                            "  ";
                _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManDate.ResultsTableName = "Date";
                _dbManDate.ExecuteInstruction();

                DataSet ReportDatasetReport4 = new DataSet();
                ReportDatasetReport4.Tables.Add(_dbManDate.ResultsDataTable);

                MWDataManager.clsDataAccess _dbManGenData = new MWDataManager.clsDataAccess();
                _dbManGenData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManGenData.SqlStatement = " select '" + wpaa + "' WP,  '" + SysSettings.Banner + "' mine, '" + visible1 + "' mine ";
                _dbManGenData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGenData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGenData.ResultsTableName = "GenData";
                _dbManGenData.ExecuteInstruction();

                DataSet ReportDatasetReport5 = new DataSet();
                ReportDatasetReport5.Tables.Add(_dbManGenData.ResultsDataTable);


                CallCentreApp.RegisterData(ReportDatasetReport1);
                CallCentreApp.RegisterData(ReportDatasetReport2);
                CallCentreApp.RegisterData(ReportDatasetReport3);
                CallCentreApp.RegisterData(ReportDatasetReport4);
                CallCentreApp.RegisterData(ReportDatasetReport5);

                CallCentreApp.RegisterData(ReportDatasetReport2NS);


                CallCentreApp.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpCall.frx");

                //CallCentreApp.Design();
                CallCentreApp.Show();
            }

            if (col1 == "Critical Skills")
            {
                MWDataManager.clsDataAccess _dbManWPData = new MWDataManager.clsDataAccess();
                _dbManWPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWPData.SqlStatement = "select  top(1) EmployeeNo  miner,pm.OrgUnitDay+'                                         ' org, w.Description  \r\n" +
                                            "from planning p, workplace w, planmonth pm, section s  \r\n" +
                                            "where p.calendardate = CONVERT(VARCHAR(10),getdate(), 20)  \r\n" +
                                            "and p.workplaceid = w.workplaceid and p.prodmonth = pm.prodmonth  \r\n" +
                                            "and p.workplaceid = pm.workplaceid and p.activity = pm.activity and p.sectionid = pm.sectionid  \r\n" +
                                            "and p.prodmonth = s.prodmonth and p.sectionid = s.sectionid  \r\n" +
                                            "and w.description = '" + wpaa + "'  order by calendardate desc, pm.OrgUnitDay desc ";
                _dbManWPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPData.ResultsTableName = "RawData";
                _dbManWPData.ExecuteInstruction();


                string miner = "";
                string org = "";
                string wp = "";

                if (_dbManWPData.ResultsDataTable.Rows.Count > 0)
                {

                    miner = _dbManWPData.ResultsDataTable.Rows[0]["miner"].ToString();
                    org = _dbManWPData.ResultsDataTable.Rows[0]["org"].ToString().Substring(0, 10);
                    wp = _dbManWPData.ResultsDataTable.Rows[0]["description"].ToString();
                }



                string clocking = "";
                if (SysSettings.Banner == "Joel Mine")
                {
                    clocking = "UNDERGROUND";
                }
                if (SysSettings.Banner == "Doornkop Mine")
                {
                    clocking = "SKYWALK";
                }

                MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
                _dbManDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManDate.SqlStatement = "select getdate() , '" + miner + "' mm, '" + org + "'  org " +

                                            "  ";
                _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManDate.ResultsTableName = "Date";
                _dbManDate.ExecuteInstruction();

                DataSet ReportDatasetReport = new DataSet();
                ReportDatasetReport.Tables.Add(_dbManDate.ResultsDataTable);


                MWDataManager.clsDataAccess _dbManMinerMain = new MWDataManager.clsDataAccess();
                _dbManMinerMain.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManMinerMain.SqlStatement = " " +

                   " select a.*, case when ug is null then 'N' else 'Y' end as UG from (select 'a' a, reader_description, max(clock_time) cc \r\n" +
                    " from [tbl_LicenceToOperate_Labour_Import]   \r\n" +
                    " where emp_Empno = '" + miner + "'   and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20)   \r\n" +
                    " group by reader_description) a  left outer join  \r\n" +
                    " (select 'a' a, max(clock_time) ug from [tbl_LicenceToOperate_Labour_Import]   \r\n" +
                    " where emp_Empno = '" + miner + "'   and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20)   \r\n" +


                    " and (   reader_description like '%" + clocking + "%')) b on a.a = b.a " +

                    //"  and reader_description like '%Underground%') b on a.a = b.a " +


                    " order by cc   ";

                _dbManMinerMain.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManMinerMain.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManMinerMain.ResultsTableName = "MinerToDay";
                _dbManMinerMain.ExecuteInstruction();

                // DataSet ReportDatasetReport = new DataSet();
                ReportDatasetReport.Tables.Add(_dbManMinerMain.ResultsDataTable);

                ////Last Clocking Miner

                MWDataManager.clsDataAccess _dbManMinerMainLastClock = new MWDataManager.clsDataAccess();
                _dbManMinerMainLastClock.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManMinerMainLastClock.SqlStatement = " " +

                   "  select top(1)  clock_time, READER_DESCRIPTION, emp_empno  from [tbl_LicenceToOperate_Labour_Import]    " +
                    " where emp_Empno = '" + miner + "'   " +
                    " and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20) order by clock_time desc  ";

                _dbManMinerMainLastClock.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManMinerMainLastClock.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManMinerMainLastClock.ResultsTableName = "MinerToDayLastClock";
                _dbManMinerMainLastClock.ExecuteInstruction();


                ReportDatasetReport.Tables.Add(_dbManMinerMainLastClock.ResultsDataTable);

                // miner histrory
                MWDataManager.clsDataAccess _dbManMinerHist = new MWDataManager.clsDataAccess();
                _dbManMinerHist.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManMinerHist.SqlStatement = " select emp_empno, max(emp_name) name \r\n" +
                                               " ,max(Day40) Day40, max(Day39) Day39, max(Day38) Day38, max(Day37) Day37, max(Day36) Day36, max(Day35) Day35 \r\n" +
                                               " ,max(Day34) Day34, max(Day33) Day33, max(Day32) Day32, max(Day31) Day31, max(Day30) Day30, max(Day29) Day29 \r\n" +

                                               " ,max(Day28) Day28, max(Day27) Day27, max(Day26) Day26, max(Day25) Day25 \r\n" +
                                               " ,max(Day24) Day24, max(Day23) Day23, max(Day22) Day22, max(Day21) Day21, max(Day20) Day20, max(Day19) Day19 \r\n" +

                                               " ,max(Day18) Day18, max(Day17) Day17, max(Day16) Day16, max(Day15) Day15 \r\n" +
                                               " ,max(Day14) Day14, max(Day13) Day13, max(Day12) Day12, max(Day11) Day11, max(Day10) Day10 \r\n" +

                                               "  from (SELECT emp_empno, emp_name, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate(), 20) then day2 else '' end as Day40, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-1, 20) then day2 else '' end as Day39, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-2, 20) then day2 else '' end as Day38, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-3, 20) then day2 else '' end as Day37, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-4, 20) then day2 else '' end as Day36, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-5, 20) then day2 else '' end as Day35, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-6, 20) then day2 else '' end as Day34, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-7, 20) then day2 else '' end as Day33, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-8, 20) then day2 else '' end as Day32, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-9, 20) then day2 else '' end as Day31, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-10, 20) then day2 else '' end as Day30, \r\n" +

                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-11, 20) then day2 else '' end as Day29, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-12, 20) then day2 else '' end as Day28, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-13, 20) then day2 else '' end as Day27, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-14, 20) then day2 else '' end as Day26, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-15, 20) then day2 else '' end as Day25, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-16, 20) then day2 else '' end as Day24, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-17, 20) then day2 else '' end as Day23, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-18, 20) then day2 else '' end as Day22, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-19, 20) then day2 else '' end as Day21, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-20, 20) then day2 else '' end as Day20, \r\n" +

                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-21, 20) then day2 else '' end as Day19, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-22, 20) then day2 else '' end as Day18, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-23, 20) then day2 else '' end as Day17, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-24, 20) then day2 else '' end as Day16, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-25, 20) then day2 else '' end as Day15, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-26, 20) then day2 else '' end as Day14, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-27, 20) then day2 else '' end as Day13, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-28, 20) then day2 else '' end as Day12, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-29, 20) then day2 else '' end as Day11, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-30, 20) then day2 else '' end as Day10 \r\n" +
                                               "  from  tbl_Attendance \r\n" +
                                               "  where   \r\n" +
                                               "  convert(varchar(20),attendance_date)+emp_empno in  \r\n" +
                                               " (select convert(varchar(20),attendance_date)+emp_empno from  \r\n" +
                                               " tbl_Attendance where emp_Empno ='" + miner + "' ) \r\n" +
                                               " and emp_empno in (select distinct(emp_empno) a from  [tbl_LicenceToOperate_Labour_Import] \r\n" +
                                               " where emp_Empno = '" + miner + "' \r\n" +
                                               " and CONVERT(VARCHAR(10),clock_time, 20) >= CONVERT(VARCHAR(10),getdate()-30, 20))) a \r\n" +
                                               " group by emp_empno \r\n " +

                                               " order by emp_empno desc \r\n";
                _dbManMinerHist.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManMinerHist.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManMinerHist.ResultsTableName = "MinerHist";
                _dbManMinerHist.ExecuteInstruction();

                string Minerpf = "";

                if (_dbManMinerHist.ResultsDataTable.Rows.Count > 0)
                    Minerpf = _dbManMinerHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();

                DataSet ReportDatasetReport1 = new DataSet();
                ReportDatasetReport1.Tables.Add(_dbManMinerHist.ResultsDataTable);



                // teamleader main
                MWDataManager.clsDataAccess _dbManTeamMain = new MWDataManager.clsDataAccess();
                _dbManTeamMain.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManTeamMain.SqlStatement = " \r\n" +

                                                "   declare @minerTL varchar(20) \r\n" +

                                                " set @minerTL = (select max(ug) ug from (select  max(clock_time) ug from [tbl_LicenceToOperate_Labour_Import]  \r\n" +
                                                " where gang_number = '" + org + "' and wage_Description like '%TEAM%'  \r\n" +
                                                " and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20)  \r\n" +
                                               //" and reader_description like '%Underground%') \r\n" +

                                               "  and   reader_description like '%" + clocking + "%'" +


                                               "   union " +
                                                "  select  max(clock_time) ug from [tbl_LicenceToOperate_Labour_Import]  " +
                                               "  where gang_number = '" + org + "' and wage_Description like '%TEAM%'   " +
                                               "  and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20)   " +
                                               "  and reader_description like '%Staircase%') a) " +



                                                " select a.*, case when @minerTL is null then 'N' else 'Y' end as UG from (select 'a' a,'" + org + "' oo, reader_description, max(clock_time) cc from  [dbo].[tbl_LicenceToOperate_Labour_Import]  \r\n" +
                                                " where gang_number = '" + org + "' and wage_Description like '%TEAM%'  \r\n" +
                                                " and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20)  \r\n" +
                                                " group by reader_description ) a   \r\n" +
                                                " order by cc   \r\n";

                _dbManTeamMain.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManTeamMain.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManTeamMain.ResultsTableName = "TeamToDay";
                _dbManTeamMain.ExecuteInstruction();

                DataSet ReportDatasetReport2 = new DataSet();
                ReportDatasetReport2.Tables.Add(_dbManTeamMain.ResultsDataTable);

                ////Last Clocking TL

                MWDataManager.clsDataAccess _dbManTLMainLastClock = new MWDataManager.clsDataAccess();
                _dbManTLMainLastClock.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManTLMainLastClock.SqlStatement = " " +

                   "  select top(1) clock_time, READER_DESCRIPTION, emp_empno  from [tbl_LicenceToOperate_Labour_Import]   " +
                    " where gang_number = '" + org + "' and wage_Description like '%TEAM%'   " +
                    " and CONVERT(VARCHAR(10),clock_time, 20) = CONVERT(VARCHAR(10),getdate(), 20) order by clock_time desc  ";

                _dbManTLMainLastClock.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManTLMainLastClock.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManTLMainLastClock.ResultsTableName = "TLToDayLastClock";
                _dbManTLMainLastClock.ExecuteInstruction();

                ReportDatasetReport.Tables.Add(_dbManTLMainLastClock.ResultsDataTable);


                // teamleader main
                MWDataManager.clsDataAccess _dbManTeamHist = new MWDataManager.clsDataAccess();
                _dbManTeamHist.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManTeamHist.SqlStatement = " select  '" + org + "'  oo, emp_empno, max(emp_name) name  \r\n" +
                                               " ,max(Day40) Day40, max(Day39) Day39, max(Day38) Day38, max(Day37) Day37, max(Day36) Day36, max(Day35) Day35 \r\n" +
                                               " ,max(Day34) Day34, max(Day33) Day33, max(Day32) Day32, max(Day31) Day31, max(Day30) Day30, max(Day29) Day29 \r\n" +

                                               " ,max(Day28) Day28, max(Day27) Day27, max(Day26) Day26, max(Day25) Day25 \r\n" +
                                               " ,max(Day24) Day24, max(Day23) Day23, max(Day22) Day22, max(Day21) Day21, max(Day20) Day20, max(Day19) Day19 \r\n" +

                                               " ,max(Day18) Day18, max(Day17) Day17, max(Day16) Day16, max(Day15) Day15, \r\n" +
                                               " max(Day14) Day14, max(Day13) Day13, max(Day12) Day12, max(Day11) Day11, max(Day10) Day10 \r\n" +

                                               "  from (SELECT emp_empno, emp_name, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate(), 20) then day2 else '' end as Day40, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-1, 20) then day2 else '' end as Day39, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-2, 20) then day2 else '' end as Day38, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-3, 20) then day2 else '' end as Day37, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-4, 20) then day2 else '' end as Day36, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-5, 20) then day2 else '' end as Day35, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-6, 20) then day2 else '' end as Day34, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-7, 20) then day2 else '' end as Day33, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-8, 20) then day2 else '' end as Day32, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-9, 20) then day2 else '' end as Day31, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-10, 20) then day2 else '' end as Day30, \r\n" +

                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-11, 20) then day2 else '' end as Day29, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-12, 20) then day2 else '' end as Day28, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-13, 20) then day2 else '' end as Day27, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-14, 20) then day2 else '' end as Day26, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-15, 20) then day2 else '' end as Day25, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-16, 20) then day2 else '' end as Day24, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-17, 20) then day2 else '' end as Day23, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-18, 20) then day2 else '' end as Day22, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-19, 20) then day2 else '' end as Day21, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-20, 20) then day2 else '' end as Day20, \r\n" +

                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-21, 20) then day2 else '' end as Day19, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-22, 20) then day2 else '' end as Day18, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-23, 20) then day2 else '' end as Day17, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-24, 20) then day2 else '' end as Day16, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-25, 20) then day2 else '' end as Day15, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-26, 20) then day2 else '' end as Day14, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-27, 20) then day2 else '' end as Day13, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-28, 20) then day2 else '' end as Day12, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-29, 20) then day2 else '' end as Day11, \r\n" +
                                               " case when attendance_date = CONVERT(VARCHAR(10),getdate()-30, 20) then day2 else '' end as Day10 \r\n" +
                                               "  from  tbl_Attendance \r\n" +
                                               "  where   \r\n" +
                                               "  convert(varchar(20),attendance_date)+emp_empno in  \r\n" +
                                               " (select convert(varchar(20),attendance_date)+emp_empno from   \r\n" +
                                               " tbl_Attendance where gang_number = '" + org + "'  ) \r\n" +
                                               " and emp_empno in (select distinct(emp_empno) a from  [tbl_LicenceToOperate_Labour_Import] \r\n" +
                                               " where gang_number = '" + org + "' and wage_Description like '%STOPE TEAM%' \r\n" +
                                               " and CONVERT(VARCHAR(10),clock_time, 20) >= CONVERT(VARCHAR(10),getdate()-30, 20))) a \r\n" +
                                               " group by emp_empno \r\n" +

                                               " order by emp_empno desc \r\n";

                _dbManTeamHist.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManTeamHist.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManTeamHist.ResultsTableName = "TeamHist";
                _dbManTeamHist.ExecuteInstruction();

                DataSet ReportDatasetReport3 = new DataSet();
                ReportDatasetReport3.Tables.Add(_dbManTeamHist.ResultsDataTable);


                string Team = "";

                if (_dbManTeamHist.ResultsDataTable.Rows.Count > 0)
                    Team = _dbManTeamHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();


                string userPath = @"http://10.1.1.113/api/resource/GetEmployeeImage?EmployeeNumber=";
                string minpic = "";
                string tlpic = "";
                string pic = "";


                string min = "";
                string tl = "";

                if (_dbManMinerMainLastClock.ResultsDataTable.Rows.Count > 0)
                {
                    pic = _dbManMinerMainLastClock.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                    minpic = userPath + pic;

                    min = _dbManMinerMainLastClock.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                }


                if (_dbManMinerHist.ResultsDataTable.Rows.Count > 0)
                {
                    pic = _dbManMinerHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                    minpic = userPath + pic;

                    min = _dbManMinerHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                }



                if (_dbManTLMainLastClock.ResultsDataTable.Rows.Count > 0)
                {
                    pic = _dbManTLMainLastClock.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                    tlpic = userPath + pic;
                    tl = _dbManTLMainLastClock.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                }

                if (_dbManTeamHist.ResultsDataTable.Rows.Count > 0)
                {
                    pic = _dbManTeamHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                    tlpic = userPath + pic;
                    tl = _dbManTeamHist.ResultsDataTable.Rows[0]["emp_empno"].ToString();
                }

                //if (VerifyGrid.CurrentRow == null)
                //{
                //    MessageBox.Show("Please select a incident", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}



                MWDataManager.clsDataAccess _dbManGenData = new MWDataManager.clsDataAccess();
                _dbManGenData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManGenData.SqlStatement = " select *, b.employeename mmname, c.employeename tlname from ( select '" + minpic + "' minerpic, '" + tlpic + "' tlpic, '" + SysSettings.Banner + "' mine,  '" + Team + "' TeamCompNo,  '" + Minerpf + "' MinerCompNo , '" + wp + "' wp, '" + min + "' mm, '" + tl + "' tt ";
                _dbManGenData.SqlStatement = _dbManGenData.SqlStatement + "  ) a  left outer join employeeall b on a.mm = b.employeeno   left outer join employeeall c on a.tt = c.employeeno ";


                _dbManGenData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGenData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGenData.ResultsTableName = "GenData";
                _dbManGenData.ExecuteInstruction();

                DataSet ReportDatasetReport4 = new DataSet();
                ReportDatasetReport4.Tables.Add(_dbManGenData.ResultsDataTable);


                WPLicToOp.RegisterData(ReportDatasetReport);
                WPLicToOp.RegisterData(ReportDatasetReport1);
                WPLicToOp.RegisterData(ReportDatasetReport2);
                WPLicToOp.RegisterData(ReportDatasetReport3);
                WPLicToOp.RegisterData(ReportDatasetReport4);


                WPLicToOp.Load(TGlobalItems.ReportsFolder + "\\WPLicToOpLab.frx");

                //WPLicToOp.Design();
                WPLicToOp.Show();

            }




            if (col1 == "Rock Engineer Dept.")
            {
                if (wpaa == "")
                {
                    MessageBox.Show("No Data available", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {

                    MWDataManager.clsDataAccess _dbManRMDetail = new MWDataManager.clsDataAccess();
                    _dbManRMDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManRMDetail.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] a, workplace w " +
                                                    "where a.workplace = w.description and workplace  = '" + wpaa + "' " +
                                                    "and captdate = (select  " +
                                                    "max(captdate) from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + wpaa + "' ) ";
                    _dbManRMDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManRMDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManRMDetail.ResultsTableName = "RMDetails";
                    _dbManRMDetail.ExecuteInstruction();

                    if (_dbManRMDetail.ResultsDataTable.Rows.Count < 1)
                    {
                        MessageBox.Show("No Data available", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    RockEngFrm FPMessagefrm = new RockEngFrm();
                    FPMessagefrm.WPLbl.Text = wpaa;
                    FPMessagefrm.ActType = acttype1;
                    FPMessagefrm.WkLbl.Text = _dbManRMDetail.ResultsDataTable.Rows[0]["captweek"].ToString();
                    FPMessagefrm.WkLbl2.Text = _dbManRMDetail.ResultsDataTable.Rows[0]["captweek"].ToString();
                    FPMessagefrm.RRLbl.Text = _dbManRMDetail.ResultsDataTable.Rows[0]["riskrating"].ToString(); //"0";
                    FPMessagefrm.EditLbl.Text = "1";
                    FPMessagefrm._theSystemDBTag = theSystemDBTag;
                    FPMessagefrm._UserCurrentInfoConnection = UserCurrentInfo.Connection;

                    FPMessagefrm.ShowDialog();

                }


            }


            if (col1 == "Workplace                                                         (DQlik to show Report) aaaaaaadelete")
            {

                //                select s.pfnumber from planning p, section s where p.prodmonth = s.prodmonth and p.sectionid = s.sectionid and  p.workplaceid = 'S06231'
                //and calendardate = (select max(calendardate) dd from planning where workplaceid = 'S06231' and calendardate < getdate())



                MWDataManager.clsDataAccess _dbManWPDetail = new MWDataManager.clsDataAccess();
                _dbManWPDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWPDetail.SqlStatement = " select '" + SysSettings.Banner + "' Baner, '" + clsUserInfo.UserName + "' username, * from workplace where description = '" + wpaa + "' " +

                                                "  ";

                _dbManWPDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPDetail.ResultsTableName = "WorkplaceDetails";
                _dbManWPDetail.ExecuteInstruction();


                DataSet ReportDatasetReport = new DataSet();
                ReportDatasetReport.Tables.Add(_dbManWPDetail.ResultsDataTable);

                if (_dbManWPDetail.ResultsDataTable.Rows.Count < 1)
                {
                    //  MessageBox.Show("No Data for selected criteria");
                    //  return;
                }
                else
                {

                    wpid = _dbManWPDetail.ResultsDataTable.Rows[0]["workplaceid"].ToString();
                }

                MWDataManager.clsDataAccess _dbManSection = new MWDataManager.clsDataAccess();
                _dbManSection.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManSection.SqlStatement = " select s.pfnumber from planning p, section s where p.prodmonth = s.prodmonth and p.sectionid = s.sectionid and  p.workplaceid = '" + wpid + "' " +
                                                " and calendardate = (select max(calendardate) dd from planning where workplaceid = '" + wpid + "' and calendardate < getdate()) ";
                _dbManSection.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSection.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManSection.ResultsTableName = "ccc";
                _dbManSection.ExecuteInstruction();
                string userPath = @"\\\\afzavrdat01\\vr\\LMSNew\\Images";
                string pic = "";

                string miner = "";
                if (_dbManSection.ResultsDataTable.Rows.Count > 0)
                {
                    pic = _dbManSection.ResultsDataTable.Rows[0]["pfnumber"].ToString();
                    miner = userPath + @"\" + pic + ".jpg";
                }

                MWDataManager.clsDataAccess _dbManCrewDetail = new MWDataManager.clsDataAccess();
                _dbManCrewDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManCrewDetail.SqlStatement = " select substring(max(orgunitds),1,15) crew, max(orgunitds) xxx, '" + miner + "' miner from planning where workplaceid =  '" + wpid + "' " +

                                                " and calendardate = (select max(calendardate) from planning where workplaceid = '" + wpid + "') ";

                _dbManCrewDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManCrewDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManCrewDetail.ResultsTableName = "Crew Details";
                _dbManCrewDetail.ExecuteInstruction();

                DataSet ReportDatasetReportCrew = new DataSet();
                ReportDatasetReportCrew.Tables.Add(_dbManCrewDetail.ResultsDataTable);

                if (_dbManCrewDetail.ResultsDataTable.Rows.Count < 1)
                {
                    // MessageBox.Show("No Data for selected criteria");
                    // return;
                }
                else
                {

                    crewid = _dbManCrewDetail.ResultsDataTable.Rows[0]["crew"].ToString();
                }



                MWDataManager.clsDataAccess _dbManMinerDetail = new MWDataManager.clsDataAccess();
                _dbManMinerDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManMinerDetail.SqlStatement = " SELECT * from  MW_PassStageDB.dbo.ATTENDANCE where " +
                                                "attendance_date <= getdate()-1 and attendance_date > getdate()-2 " +

                                                " and emp_empno = '" + pic + "' ";

                _dbManMinerDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManMinerDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManMinerDetail.ResultsTableName = "Miner Details";
                _dbManMinerDetail.ExecuteInstruction();

                DataSet ReportDatasetReportMiner = new DataSet();
                ReportDatasetReportMiner.Tables.Add(_dbManMinerDetail.ResultsDataTable);



                MWDataManager.clsDataAccess _dbManPSDetail = new MWDataManager.clsDataAccess();
                _dbManPSDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManPSDetail.SqlStatement = " select pr.description, count(p.ProblemID) aa from planning p, CODE_PROBLEM pr     \r\n" +
                                                "  where p.problemid = pr.problemid and p.Activity = pr.Activity  \r\n" +
                                                "  and p.workplaceid = '" + wpid + "' and calendardate > getdate() - 30  group by pr.description    " +
                                                "  ";
                //_dbManPSDetail.SqlStatement = " select p.description, count(description) aa from problembook pb, problem p  " +
                //                                " where pb.problemid = p.problemid and  workplaceid = '" + wpid + "' and calendardate > getdate() - 30 " +
                //                                " group by p.description " +
                //                                "  ";

                _dbManPSDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManPSDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManPSDetail.ResultsTableName = "Planned Stoppage Graph";
                _dbManPSDetail.ExecuteInstruction();

                DataSet ReportDatasetReportPS = new DataSet();
                ReportDatasetReportPS.Tables.Add(_dbManPSDetail.ResultsDataTable);


                MWDataManager.clsDataAccess _dbManProbDetail = new MWDataManager.clsDataAccess();
                _dbManProbDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManProbDetail.SqlStatement = " select c.description, count(description) aa  from planning p, Code_Cycle c   " +
                                                " where p.BookCode = c.CycleCode   COLLATE Latin1_General_CI_AS and  workplaceid = '" + wpid + "' and calendardate > getdate() - 30 " +
                                                " group by c.description " +
                                                "  ";

                _dbManProbDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManProbDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManProbDetail.ResultsTableName = "Problem Graph";
                _dbManProbDetail.ExecuteInstruction();

                DataSet ReportDatasetReportProb = new DataSet();
                ReportDatasetReportProb.Tables.Add(_dbManProbDetail.ResultsDataTable);


                MWDataManager.clsDataAccess _dbManSPDetail = new MWDataManager.clsDataAccess();
                _dbManSPDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManSPDetail.SqlStatement = " select userid, CONVERT(VARCHAR(11),thedate,20) dd from [dbo].[tbl_WPStopDoc] where workplace = '" + wp + "'   " +
                                                " and thedate  = (select max(thedate) from [dbo].[tbl_WPStopDoc] where workplace = '" + wp + "' " +
                                                " and lastbookdate = '" + LastDateBook + "') " +
                                                " group by  userid, CONVERT(VARCHAR(11),thedate,20) ";

                _dbManSPDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSPDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManSPDetail.ResultsTableName = "Stop Procedure";
                _dbManSPDetail.ExecuteInstruction();

                DataSet ReportDatasetReportSP = new DataSet();
                ReportDatasetReportSP.Tables.Add(_dbManSPDetail.ResultsDataTable);

                if (_dbManSPDetail.ResultsDataTable.Rows.Count < 1)
                {
                    //  MessageBox.Show("No Data for selected criteria");
                    //  return;
                }
                else
                {

                    ADate = _dbManSPDetail.ResultsDataTable.Rows[0]["dd"].ToString();
                }



                MWDataManager.clsDataAccess _dbManFailDetail = new MWDataManager.clsDataAccess();
                _dbManFailDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManFailDetail.SqlStatement = " select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                " where datesubmitted  >= getdate()-30 " +
                                                " and workplace = '" + wpaa + "' and answer = 'No' and hazard = 'A' " +


                                                // //" union " + 

                                                // //GN Moab
                                                // "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                // " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                // " where taskname = 'MK-IAPI-Extended Break Close Down Checklist' and datesubmitted  = '" + LastDateBook + "' " +
                                                // " and workplace = '" + wp + "' and answer = 'No' " +


                                                // //Kop

                                                //  "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                // " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                // " where taskname = 'KP-IAPI-December Break Shut Down Checklist' and datesubmitted  = '" + LastDateBook + "' " +
                                                // " and workplace = '" + wp + "' and answer = 'No' " +



                                                // //Mponeng

                                                //"  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                // " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                // " where taskname = 'MP-IAPI-Christmas Break Shutdown 2015' and datesubmitted  = '" + LastDateBook + "' " +
                                                // " and workplace = '" + wp + "' and answer = 'No' " +

                                                // //Tau Tona Sav
                                                //   "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                // " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                // " where taskname = 'TT-IAPI-Extended Break Development Shut-Down' and datesubmitted  = '" + LastDateBook + "' " +
                                                // " and workplace = '" + wp + "' and answer = 'No' " +

                                                //   "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                // " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                // " where taskname = 'TT-IAPI-Extended Break Stoping Shut-Down' and datesubmitted  = '" + LastDateBook + "' " +
                                                // " and workplace = '" + wp + "' and answer = 'No' " +



                                                " union select '' stepname, ''action, ''hazard,  ''majorhazard, ''criticalcontrol, ''datesubmitted    , ''dateactionclosed, ''fixedby  order by datesubmitted ";
                _dbManFailDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManFailDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManFailDetail.ResultsTableName = "Failures";
                _dbManFailDetail.ExecuteInstruction();

                DataSet ReportDatasetReportFail = new DataSet();
                ReportDatasetReportFail.Tables.Add(_dbManFailDetail.ResultsDataTable);



                MWDataManager.clsDataAccess _dbManStDetail = new MWDataManager.clsDataAccess();
                _dbManStDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManStDetail.SqlStatement = " select userid, CONVERT(VARCHAR(11),thedate,20) dd from [dbo].[tbl_WPStartDoc] where workplace = '" + wp + "'   " +
                                                " and thedate  = (select max(thedate) from [dbo].[tbl_WPStartDoc] where workplace = '" + wp + "' " +
                                                " and lastbookdate = '" + LastDateBook + "') " +
                                                " group by  userid, CONVERT(VARCHAR(11),thedate,20) ";

                _dbManStDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManStDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManStDetail.ResultsTableName = "Start Procedure";
                _dbManStDetail.ExecuteInstruction();

                DataSet ReportDatasetReportSttt = new DataSet();
                ReportDatasetReportSttt.Tables.Add(_dbManStDetail.ResultsDataTable);

                if (_dbManSPDetail.ResultsDataTable.Rows.Count < 1)
                {
                    //  MessageBox.Show("No Data for selected criteria");
                    //  return;
                }
                else
                {

                    BDate = _dbManSPDetail.ResultsDataTable.Rows[0]["dd"].ToString();
                }




                MWDataManager.clsDataAccess _dbManStartDetail = new MWDataManager.clsDataAccess();
                _dbManStartDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManStartDetail.SqlStatement = " select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                " where [activityname] like 'Start Procedure' and CONVERT(VARCHAR(11),datesubmitted,106)  = '" + BDate + "' " +
                                                " and workplace = '" + wp + "' and answer = 'No' " +



                                                //GN Moab
                                                "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                " where taskname = 'MK-IAPI-Extended Break Start Up Checklist' and CONVERT(VARCHAR(11),datesubmitted,106)  = '" + LastDateBook + "' " +
                                                " and workplace = '" + wp + "' and answer = 'No' " +


                                                 //Kop

                                                 "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                " where taskname = 'KP-IAPI-December Break Start Up Checklist' and CONVERT(VARCHAR(11),datesubmitted,106)  = '" + LastDateBook + "' " +
                                                " and workplace = '" + wp + "' and answer = 'No' " +



                                               //Mponeng

                                               "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                " where taskname = 'MP-IAPI-2016 Day Shift Miners Extended Break Start Up' and CONVERT(VARCHAR(11),datesubmitted,106)  = '" + LastDateBook + "' " +
                                                " and workplace = '" + wp + "' and answer = 'No' " +

                                                "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                " where taskname = 'MP-IAPI-Christmas Break Start Up 2016' and CONVERT(VARCHAR(11),datesubmitted,106)  = '" + LastDateBook + "' " +
                                                " and workplace = '" + wp + "' and answer = 'No' " +

                                                "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                " where taskname = 'MP-IAPI-2016 Night Shift Miners Extended Break Start Up' and CONVERT(VARCHAR(11),datesubmitted,106)  = '" + LastDateBook + "' " +
                                                " and workplace = '" + wp + "' and answer = 'No' " +



                                                  //Tau Tona Sav
                                                  "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                " where taskname = 'TT-IAPI-Extended Break Stoping Start-Up' and CONVERT(VARCHAR(11),datesubmitted,106)  = '" + LastDateBook + "' " +
                                                " and workplace = '" + wp + "' and answer = 'No' " +


                                                  "  union select stepname, action, hazard,  majorhazard, criticalcontrol, datesubmitted   " +
                                                " , dateactionclosed, fixedby from [MW_PassStageDB].[dbo].[vw_PaperlessActionManagerPrint_AllActions]  " +
                                                " where taskname = 'TT-IAPI-Extended Break Development Start-Up' and CONVERT(VARCHAR(11),datesubmitted,106)  = '" + LastDateBook + "' " +
                                                " and workplace = '" + wp + "' and answer = 'No' " +






                " union select '' stepname, ''action, ''hazard,  ''majorhazard, ''criticalcontrol, ''datesubmitted    , ''dateactionclosed, ''fixedby ";
                _dbManStartDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManStartDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManStartDetail.ResultsTableName = "StartDeviation";
                _dbManStartDetail.ExecuteInstruction();

                DataSet ReportDatasetReportStart = new DataSet();
                ReportDatasetReportStart.Tables.Add(_dbManStartDetail.ResultsDataTable);






                MWDataManager.clsDataAccess _dbManLabourDetail = new MWDataManager.clsDataAccess();
                _dbManLabourDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLabourDetail.SqlStatement = " select thedate, convert(decimal(18,2),avg(ds_ug_dur_min))/60 tt   " +
                                                " from [tb_ClockingHistory] where gang = '" + crewid + "' and thedate > getdate()-30  " +
                                                " group by thedate order by thedate " +
                                                "  ";

                _dbManLabourDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLabourDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLabourDetail.ResultsTableName = "Labour";
                _dbManLabourDetail.ExecuteInstruction();

                DataSet ReportDatasetReportLabour = new DataSet();
                ReportDatasetReportLabour.Tables.Add(_dbManLabourDetail.ResultsDataTable);

                MWDataManager.clsDataAccess _dbManLabourDetail2 = new MWDataManager.clsDataAccess();
                _dbManLabourDetail2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLabourDetail2.SqlStatement = " select *,  hr+':'+mina tt from (select empno,emp_name, occupation, wage_code, convert(decimal(18,2),avg(ds_ug_dur_min))/60 tt11,   " +

                                                " case when floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60) > 9 then convert(varchar(10),floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60)) else   " +
                                                "  '0'+ convert(varchar(10),floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60)) end as hr,  " +


                                                "  case when floor(((convert(decimal(18,2),avg(ds_ug_dur_min))/60)-floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60))*60) > 9  " +
                                                " then " +
                                                "  convert(varchar(10),floor(((convert(decimal(18,2),avg(ds_ug_dur_min))/60)-floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60))*60))  " +
                                                "  else " +
                                                "  '0'+ convert(varchar(10),floor(((convert(decimal(18,2),avg(ds_ug_dur_min))/60)-floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60))*60))  " +
                                                "  end as  " +
                                                 "  mina   " +

                                                " from  [tb_ClockingHistory] where gang = '" + crewid + "' and thedate > getdate()-30 " +
                                                " group by empno,emp_name, occupation, wage_code) a order by wage_code desc " +
                                                "  ";

                _dbManLabourDetail2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLabourDetail2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLabourDetail2.ResultsTableName = "LabourDetail";
                _dbManLabourDetail2.ExecuteInstruction();

                DataSet ReportDatasetReportLabour2 = new DataSet();
                ReportDatasetReportLabour2.Tables.Add(_dbManLabourDetail2.ResultsDataTable);


                WPLicToOp.RegisterData(ReportDatasetReport);
                WPLicToOp.RegisterData(ReportDatasetReportCrew);
                WPLicToOp.RegisterData(ReportDatasetReportMiner);
                WPLicToOp.RegisterData(ReportDatasetReportPS);
                WPLicToOp.RegisterData(ReportDatasetReportProb);
                WPLicToOp.RegisterData(ReportDatasetReportSP);
                WPLicToOp.RegisterData(ReportDatasetReportFail);
                WPLicToOp.RegisterData(ReportDatasetReportLabour);
                WPLicToOp.RegisterData(ReportDatasetReportLabour2);
                WPLicToOp.RegisterData(ReportDatasetReportStart);
                WPLicToOp.RegisterData(ReportDatasetReportSttt);



                WPLicToOp.Load(TGlobalItems.ReportsFolder + "\\WPLicToOp.frx");

                // WPLicToOp.Design();
                WPLicToOp.Show();
            }
            Cursor = Cursors.Default;
        }

        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View = sender as GridView;

            string ss = "";

            ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();

            if (ss == "Orange")
            {
                e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(MedLbl.Text));
                e.Appearance.ForeColor = Color.FromArgb(Convert.ToInt32(MedLbl.Text));

            }

            if (ss == "Red")
            {
                e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(HighLbl.Text));
                e.Appearance.ForeColor = Color.FromArgb(Convert.ToInt32(HighLbl.Text));
            }

            if (e.Column.AbsoluteIndex == 14)
            {
                e.DisplayText = "     " + View.GetRowCellValue(e.RowHandle, "rate").ToString();
                e.Appearance.ForeColor = Color.DarkGray;

            }


            string ss1 = "";

            ss1 = View.GetRowCellValue(e.RowHandle, "num").ToString();


            if (e.Column.AbsoluteIndex == 1)
            {
                if (Convert.ToDecimal(ss1) >= Convert.ToDecimal(LTOOrangelabel.Text))
                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(MedLbl.Text));

                if (Convert.ToDecimal(ss1) >= Convert.ToDecimal(LTORedlabel.Text))
                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(HighLbl.Text));

            }
        }

        private void bandedGridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            wpaa = bandedGridView1.GetRowCellValue(e.RowHandle, bandedGridView1.Columns[1]).ToString();
            col1 = e.Column.ToString();

            if (col1 == "Rock Engineer Dept.")
                vvv = bandedGridView1.GetRowCellValue(e.RowHandle, bandedGridView1.Columns[10]).ToString();

            if (col1 == "GeoScience Dept.")
                vvv = bandedGridView1.GetRowCellValue(e.RowHandle, bandedGridView1.Columns[11]).ToString();

            acttype1 = "Stp";
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private void bandedGridView4_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View = sender as GridView;

            string ss = "";

            ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();

            if (ss == "Orange")
            {
                e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(MedLbl.Text));
                e.Appearance.ForeColor = Color.FromArgb(Convert.ToInt32(MedLbl.Text));

            }

            if (ss == "Red")
            {
                e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(HighLbl.Text));
                e.Appearance.ForeColor = Color.FromArgb(Convert.ToInt32(HighLbl.Text));
            }

            if (e.Column.AbsoluteIndex == 14)
            {
                e.DisplayText = "     " + View.GetRowCellValue(e.RowHandle, "rate").ToString();
                e.Appearance.ForeColor = Color.DarkGray;

            }

            string ss1 = "";

            ss1 = View.GetRowCellValue(e.RowHandle, "num").ToString();


            if (e.Column.AbsoluteIndex == 1)
            {
                if (Convert.ToDecimal(ss1) >= Convert.ToDecimal(LTOOrangelabel.Text))
                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(MedLbl.Text));

                if (Convert.ToDecimal(ss1) >= Convert.ToDecimal(LTORedlabel.Text))
                    e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(HighLbl.Text));

            }

        }

        private void bandedGridView4_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            wpaa = bandedGridView4.GetRowCellValue(e.RowHandle, bandedGridView4.Columns[1]).ToString();
            col1 = e.Column.ToString();

            if (col1 == "Rock Engineer Dept.")
                vvv = bandedGridView4.GetRowCellValue(e.RowHandle, bandedGridView4.Columns[10]).ToString();

            if (col1 == "GeoScience Dept.")
                vvv = bandedGridView4.GetRowCellValue(e.RowHandle, bandedGridView4.Columns[11]).ToString();

            acttype1 = "Dev";
        }




        private void gridControl6_DoubleClick(object sender, EventArgs e)
        {
            //if (clsUserInfo.plan != "Y")
            //    return;

            int numrep = 1;




            if (col == "Workplace                                                         (DQlik to show Report)")
            {

                MWDataManager.clsDataAccess _dbManWPDetail = new MWDataManager.clsDataAccess();
                _dbManWPDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWPDetail.SqlStatement = " select '" + SysSettings.Banner + "' Baner, '" + TUserInfo.Name + "' username, * from workplace where description = '" + wp + "' " +

                                                "  ";

                _dbManWPDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWPDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWPDetail.ResultsTableName = "WorkplaceDetails";
                _dbManWPDetail.ExecuteInstruction();


                DataSet ReportDatasetReport = new DataSet();
                ReportDatasetReport.Tables.Add(_dbManWPDetail.ResultsDataTable);

                if (_dbManWPDetail.ResultsDataTable.Rows.Count < 1)
                {
                    //  MessageBox.Show("No Data for selected criteria");
                    //  return;
                }
                else
                {

                    wpid = _dbManWPDetail.ResultsDataTable.Rows[0]["workplaceid"].ToString();
                }



                MWDataManager.clsDataAccess _dbManCrewDetail = new MWDataManager.clsDataAccess();
                _dbManCrewDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManCrewDetail.SqlStatement = " select substring(max(OrgUnitDay),1,15) crew, max(OrgUnitDay) xxx from PLANMONTH pm,PLANNING p \r\n" +
                                                " where p.Prodmonth = pm.Prodmonth and p.WorkplaceID = pm.Workplaceid and p.SectionID = pm.Sectionid    \r\n" +
                                                " and pm.Workplaceid = '" + wpid + "'   \r\n" +
                                                " and p.calendardate = (select max(calendardate) from planning where workplaceid = '" + wpid + "')  ";

                //_dbManCrewDetail.SqlStatement = " select substring(max(OrgUnitDay),1,15) crew, max(OrgUnitDay) xxx from planning where workplaceid =  '" + wpid + "' " +

                //                                " and calendardate = (select max(calendardate) from planning where workplaceid = '" + wpid + "') ";

                _dbManCrewDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManCrewDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManCrewDetail.ResultsTableName = "Crew Details";
                _dbManCrewDetail.ExecuteInstruction();

                DataSet ReportDatasetReportCrew = new DataSet();
                ReportDatasetReportCrew.Tables.Add(_dbManCrewDetail.ResultsDataTable);

                if (_dbManCrewDetail.ResultsDataTable.Rows.Count < 1)
                {
                    // MessageBox.Show("No Data for selected criteria");
                    // return;
                }
                else
                {

                    crewid = _dbManCrewDetail.ResultsDataTable.Rows[0]["crew"].ToString();
                }




                MWDataManager.clsDataAccess _dbManPSDetail = new MWDataManager.clsDataAccess();
                _dbManPSDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManPSDetail.SqlStatement = " select pr.description, count(p.ProblemID) aa from planning p, CODE_PROBLEM pr     \r\n" +
                                                "  where p.problemid = pr.problemid and p.Activity = pr.Activity  \r\n" +
                                                "  and p.workplaceid = '" + wpid + "' and calendardate > getdate() - 30  group by pr.description    " +
                                                "  ";
                //_dbManPSDetail.SqlStatement = " select p.description, count(description) aa from problembook pb, problem p  " +
                //                                " where pb.problemid = p.problemid and  workplaceid = '" + wpid + "' and calendardate > getdate() - 30 " +
                //                                " group by p.description " +
                //                                "  ";

                _dbManPSDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManPSDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManPSDetail.ResultsTableName = "Planned Stoppage Graph";
                _dbManPSDetail.ExecuteInstruction();

                DataSet ReportDatasetReportPS = new DataSet();
                ReportDatasetReportPS.Tables.Add(_dbManPSDetail.ResultsDataTable);


                MWDataManager.clsDataAccess _dbManProbDetail = new MWDataManager.clsDataAccess();
                _dbManProbDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManProbDetail.SqlStatement = " select c.description, count(description) aa  from planning p, CODE_CYCLE c   " +
                                                " where p.BookCode = c.CycleCode  COLLATE Latin1_General_CI_AS and  workplaceid = '" + wpid + "' and calendardate > getdate() - 30 " +
                                                " group by c.description " +
                                                "  ";
                //_dbManProbDetail.SqlStatement = " select c.description, count(description) aa  from planning p, CODE_CYCLE c   " +
                //                                " where p.bookprob = c.code  COLLATE Latin1_General_CI_AS and  workplaceid = '" + wpid + "' and calendardate > getdate() - 30 " +
                //                                " group by c.description " +
                //                                "  ";

                _dbManProbDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManProbDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManProbDetail.ResultsTableName = "Problem Graph";
                _dbManProbDetail.ExecuteInstruction();

                DataSet ReportDatasetReportProb = new DataSet();
                ReportDatasetReportProb.Tables.Add(_dbManProbDetail.ResultsDataTable);


                MWDataManager.clsDataAccess _dbManSPDetail = new MWDataManager.clsDataAccess();
                _dbManSPDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManSPDetail.SqlStatement = " select userid, CONVERT(VARCHAR(11),thedate,20) dd from [dbo].[tbl_WPStopDoc] where workplace = '" + wp + "'   " +
                                                " and thedate  = (select max(thedate) from [dbo].[tbl_WPStopDoc] where workplace = '" + wp + "' " +
                                                " and lastbookdate = '" + LastDateBook + "') " +
                                                " group by  userid, CONVERT(VARCHAR(11),thedate,20) ";

                _dbManSPDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSPDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManSPDetail.ResultsTableName = "Stop Procedure";
                _dbManSPDetail.ExecuteInstruction();

                DataSet ReportDatasetReportSP = new DataSet();
                ReportDatasetReportSP.Tables.Add(_dbManSPDetail.ResultsDataTable);

                if (_dbManSPDetail.ResultsDataTable.Rows.Count < 1)
                {
                    //  MessageBox.Show("No Data for selected criteria");
                    //  return;
                }
                else
                {

                    ADate = _dbManSPDetail.ResultsDataTable.Rows[0]["dd"].ToString();
                }



                MWDataManager.clsDataAccess _dbManFailDetail = new MWDataManager.clsDataAccess();
                _dbManFailDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManFailDetail.SqlStatement = " select Action_Title stepname,Action_Title action, hazard,''  majorhazard, criticalcontrol,Start_Date datesubmitted      \r\n" +
                                                " ,Complete_Date dateactionclosed, Action_Closed_By fixedby from tbl_Incidents   \r\n" +
                                                " where(Action_Parent_Type like 'Stop Procedure' or Action_Parent_Type like '%Stopping%')  \r\n" +
                                               //and datesubmitted  = '" + ADate + "' " +
                                               " and workplace = '" + wp + "' \r\n" +
                                               "--and answer = 'No' \r\n" +



                                               //GN Moab
                                               " union  select Action_Title stepname,Action_Title action, hazard,''  majorhazard, criticalcontrol,Start_Date datesubmitted  \r\n" +
                                                " ,Complete_Date dateactionclosed, Action_Closed_By fixedby from tbl_Incidents  \r\n" +
                                                "  where Action_Parent_Type = 'MK-IAPI-Extended Break Close Down Checklist' and Start_Date = '" + LastDateBook + "'  \r\n" +
                                               " and workplace = '" + wp + "'  \r\n" +
                                               "--and answer = 'No' \r\n" +


                                               " union select '' stepname, ''action, ''hazard,  ''majorhazard, ''criticalcontrol, ''datesubmitted    , ''dateactionclosed, ''fixedby ";


                //_dbManFailDetail.SqlStatement = " select stepname,Action_Title action, hazard,  majorhazard, criticalcontrol,Start_Date datesubmitted   \r\n" +
                //                                " ,Complete_Date dateactionclosed,Action_Closed_By fixedby from tbl_Incidents  \r\n" +
                //                                " where ([activityname] like 'Stop Procedure' or [activityname] like '%Stopping%') \r\n" +
                ////and datesubmitted  = '" + ADate + "' " +
                //                                " and workplace = '" + wp + "' and answer = 'No' \r\n" +



                //                                //GN Moab
                //                                "  union select stepname,Action_Title action, hazard,  majorhazard, criticalcontrol,Start_Date datesubmitted   \r\n" +
                //                                " ,Complete_Date dateactionclosed,Action_Closed_By fixedby from tbl_Incidents  \r\n" +
                //                                " where taskname = 'MK-IAPI-Extended Break Close Down Checklist' and Start_Date  = '" + LastDateBook + "' \r\n" +
                //                                " and workplace = '" + wp + "' and answer = 'No' \r\n" +                                               



                //                                " union select '' stepname, ''action, ''hazard,  ''majorhazard, ''criticalcontrol, ''datesubmitted    , ''dateactionclosed, ''fixedby ";
                _dbManFailDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManFailDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManFailDetail.ResultsTableName = "Failures";
                _dbManFailDetail.ExecuteInstruction();

                DataSet ReportDatasetReportFail = new DataSet();
                ReportDatasetReportFail.Tables.Add(_dbManFailDetail.ResultsDataTable);



                MWDataManager.clsDataAccess _dbManStDetail = new MWDataManager.clsDataAccess();
                _dbManStDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManStDetail.SqlStatement = " select userid, CONVERT(VARCHAR(11),thedate,20) dd from [dbo].[tbl_WPStartDoc] where workplace = '" + wp + "'   \r\n" +
                                                " and thedate  = (select max(thedate) from [dbo].[tbl_WPStartDoc] where workplace = '" + wp + "' \r\n" +
                                                " and lastbookdate = '" + LastDateBook + "') \r\n" +
                                                " group by  userid, CONVERT(VARCHAR(11),thedate,20) ";

                _dbManStDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManStDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManStDetail.ResultsTableName = "Start Procedure";
                _dbManStDetail.ExecuteInstruction();

                DataSet ReportDatasetReportSttt = new DataSet();
                ReportDatasetReportSttt.Tables.Add(_dbManStDetail.ResultsDataTable);

                if (_dbManSPDetail.ResultsDataTable.Rows.Count < 1)
                {
                    //  MessageBox.Show("No Data for selected criteria");
                    //  return;
                }
                else
                {

                    BDate = _dbManSPDetail.ResultsDataTable.Rows[0]["dd"].ToString();
                }




                MWDataManager.clsDataAccess _dbManStartDetail = new MWDataManager.clsDataAccess();
                _dbManStartDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManStartDetail.SqlStatement = " select Action_Title stepname,Action_Title action, hazard,''  majorhazard, criticalcontrol,Start_Date datesubmitted     \r\n" +
                                                " ,Complete_Date dateactionclosed, Action_Closed_By fixedby from tbl_Incidents  \r\n" +
                                                " where(Action_Parent_Type like 'Start Procedure' or Action_Parent_Type like '%Start U%' or Action_Parent_Type like 'Start-U%')  \r\n" +
                //and CONVERT(VARCHAR(11),datesubmitted,106)  = '" + BDate + "' " +
                                                " and workplace = '" + wp + "'  \r\n" +
                                                "--and answer = 'No'   \r\n" +



                                                //GN Moab
                                                "union select Action_Title stepname,Action_Title action, hazard,''  majorhazard, criticalcontrol,Start_Date datesubmitted     \r\n" +
                                                " ,Complete_Date dateactionclosed, Action_Closed_By fixedby from tbl_Incidents  \r\n" +
                                                "  where Action_Parent_Type = 'MK-IAPI-Extended Break Start Up Checklist' and CONVERT(VARCHAR(11), Start_Date, 106)  = '" + LastDateBook + "' \r\n" +
                                                " and workplace = '" + wp + "'  \r\n" +
                                                "--and answer = 'No' \r\n" +


                " union select '' stepname, ''action, ''hazard,  ''majorhazard, ''criticalcontrol, ''datesubmitted    , ''dateactionclosed, ''fixedby ";


                //_dbManStartDetail.SqlStatement = " select stepname,Action_Title action, hazard,  majorhazard, criticalcontrol,Start_Date datesubmitted   \r\n" +
                //                                " ,Complete_Date dateactionclosed,Action_Closed_By fixedby from tbl_Incidents  \r\n" +
                //                                " where ([activityname] like 'Start Procedure' or [activityname] like '%Start U%' or [activityname] like 'Start-U%') \r\n" +
                ////and CONVERT(VARCHAR(11),datesubmitted,106)  = '" + BDate + "' " +
                //                                " and workplace = '" + wp + "'  \r\n" +
                //                                "--and answer = 'No' " +



                //                                //GN Moab
                //                                "  union select stepname,Action_Title action, hazard,  majorhazard, criticalcontrol,Start_Date datesubmitted   \r\n" +
                //                                " ,Complete_Date dateactionclosed,Action_Closed_By fixedby from tbl_Incidents  \r\n" +
                //                                " where taskname = 'MK-IAPI-Extended Break Start Up Checklist' and CONVERT(VARCHAR(11),Start_Date,106)  = '" + LastDateBook + "' \r\n" +
                //                                " and workplace = '" + wp + "'  \r\n" +
                //                                "--and answer = 'No' \r\n" +


                //" union select '' stepname, ''action, ''hazard,  ''majorhazard, ''criticalcontrol, ''datesubmitted    , ''dateactionclosed, ''fixedby ";
                _dbManStartDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManStartDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManStartDetail.ResultsTableName = "StartDeviation";
                _dbManStartDetail.ExecuteInstruction();

                DataSet ReportDatasetReportStart = new DataSet();
                ReportDatasetReportStart.Tables.Add(_dbManStartDetail.ResultsDataTable);






                MWDataManager.clsDataAccess _dbManLabourDetail = new MWDataManager.clsDataAccess();
                _dbManLabourDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLabourDetail.SqlStatement = " select thedate, convert(decimal(18,2),avg(ds_ug_dur_min))/60 tt   " +
                                                " from [tb_ClockingHistory] where gang = '" + crewid + "' and thedate > getdate()-30  " +
                                                " group by thedate order by thedate " +
                                                "  ";

                _dbManLabourDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLabourDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLabourDetail.ResultsTableName = "Labour";
                _dbManLabourDetail.ExecuteInstruction();

                DataSet ReportDatasetReportLabour = new DataSet();
                ReportDatasetReportLabour.Tables.Add(_dbManLabourDetail.ResultsDataTable);

                MWDataManager.clsDataAccess _dbManLabourDetail2 = new MWDataManager.clsDataAccess();
                _dbManLabourDetail2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManLabourDetail2.SqlStatement = " select *,  hr+':'+mina tt from (select empno,emp_name, occupation, wage_code, convert(decimal(18,2),avg(ds_ug_dur_min))/60 tt11,   " +

                                                " case when floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60) > 9 then convert(varchar(10),floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60)) else   " +
                                                "  '0'+ convert(varchar(10),floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60)) end as hr,  " +


                                                "  case when floor(((convert(decimal(18,2),avg(ds_ug_dur_min))/60)-floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60))*60) > 9  " +
                                                " then " +
                                                "  convert(varchar(10),floor(((convert(decimal(18,2),avg(ds_ug_dur_min))/60)-floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60))*60))  " +
                                                "  else " +
                                                "  '0'+ convert(varchar(10),floor(((convert(decimal(18,2),avg(ds_ug_dur_min))/60)-floor(convert(decimal(18,2),avg(ds_ug_dur_min))/60))*60))  " +
                                                "  end as  " +
                                                 "  mina   " +

                                                " from  [tb_ClockingHistory] where gang = '" + crewid + "' and thedate > getdate()-30 " +
                                                " group by empno,emp_name, occupation, wage_code) a order by wage_code desc " +
                                                "  ";

                _dbManLabourDetail2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLabourDetail2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLabourDetail2.ResultsTableName = "LabourDetail";
                _dbManLabourDetail2.ExecuteInstruction();

                DataSet ReportDatasetReportLabour2 = new DataSet();
                ReportDatasetReportLabour2.Tables.Add(_dbManLabourDetail2.ResultsDataTable);


                WPStopStartRep.RegisterData(ReportDatasetReport);
                WPStopStartRep.RegisterData(ReportDatasetReportCrew);
                WPStopStartRep.RegisterData(ReportDatasetReportPS);
                WPStopStartRep.RegisterData(ReportDatasetReportProb);
                WPStopStartRep.RegisterData(ReportDatasetReportSP);
                WPStopStartRep.RegisterData(ReportDatasetReportFail);
                WPStopStartRep.RegisterData(ReportDatasetReportLabour);
                WPStopStartRep.RegisterData(ReportDatasetReportLabour2);
                WPStopStartRep.RegisterData(ReportDatasetReportStart);
                WPStopStartRep.RegisterData(ReportDatasetReportSttt);



                WPStopStartRep.Load(TGlobalItems.ReportsFolder + "\\WPStopStartRep.frx");

                //WPStopStartRep.Design();
                WPStopStartRep.Show();

                //FastReport.Utils.XmlItem item = FastReport.Utils.Config.Root.FindItem("Forms").FindItem("PreviewForm");
                //WPStopStartRep.Show(false);

            }




            if (col == "WP Stop Doc. (DQlik Print) ")
            {

                if (stopdoc == "Not Req")
                {
                    MessageBox.Show("Stop Documentation .....                                                                                 Must required as workplace was not ever booked.", "Stop Documentation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }

                if (stopdoc != "No" && stopdoc != "Print")
                {
                    MessageBox.Show("Stop Documentation .....                                                                                 Stop Procedure has already started.", "Stop Documentation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }


                if (label17.Text == "Workplace Stop - Start Procedure")
                {

                    MWDataManager.clsDataAccess _dbManWPSTDetail = new MWDataManager.clsDataAccess();
                    _dbManWPSTDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManWPSTDetail.SqlStatement = " insert into  tbl_WPStopDoc " +

                                                    " Values( '" + wp + "', '" + LastDateBook + "', '" + clsUserInfo.UserID + "', getdate()) " +

                                                    " exec  [dbo].[sp_WPStopStartDocSumPrint] '" + wp + "' ";

                    _dbManWPSTDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManWPSTDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManWPSTDetail.ExecuteInstruction();


                    //if (SysSettings.Banner == "Moab Khotsong" || SysSettings.Banner == "Great Noligwa")
                    //{
                    if (stopCat != "04 - 14")
                    {
                        MWDataManager.clsDataAccess _dbManWPSTDetail11 = new MWDataManager.clsDataAccess();
                        _dbManWPSTDetail11.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManWPSTDetail11.SqlStatement = " insert into  [tbl_WPStopDocFinal] " +

                                                        " Values( '" + wp + "', '" + LastDateBook + "', '" + clsUserInfo.UserID + "', getdate()) " +

                                                        " exec  [dbo].[sp_WPStopStartDocSumPrint] '" + wp + "' ";

                        _dbManWPSTDetail11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManWPSTDetail11.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManWPSTDetail11.ExecuteInstruction();

                    }
                    //}


                    //Report
                    MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                    _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan2.SqlStatement = "  ";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select max(description) Description, max(date) date, max(sbno) sbno, max(sbname) sbname, max(mono) mono, max(moname) moname, max(section) section, '" + SysSettings.Banner + "' bbbbb from(  \r\n";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select Description +'                '  Description, convert(varchar(12),getdate(),103) date, \r\n";





                    //if (SysSettings.Banner == "Great Noligwa")
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'GN'+s1.reporttosectionid+'             ' section \r\n";
                    //if (SysSettings.Banner == "Moab Khotsong")
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.EmployeeNo,'')  +'             ' sbno, s1.name sbname, 'MK'+s1.reporttosectionid+'             ' section \r\n";




                    //if (SysSettings.Banner == "Kopanang")
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'KP'+s1.reporttosectionid+'             ' section \r\n";

                    //if (SysSettings.Banner == "Mponeng")
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'MP'+s1.reporttosectionid+'             ' section \r\n";
                    //if (SysSettings.Banner == "Tau Tona")
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'TT'+s1.reporttosectionid+'             ' section \r\n";
                    //if (SysSettings.Banner == "Savuka")
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'SV'+s1.reporttosectionid+'             ' section \r\n";






                    //  dbMan2.SqlStatement = _dbMan2.SqlStatement +" s1.pfnumber  +'             ' sbno, s1.name sbname, 'MP'+s1.reporttosectionid+'             ' section \r\n" +

                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " ,s2.EmployeeNo  +'             ' mono, s2.name moname \r\n";

                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " from planning p, workplace w, section s, section s1, section s2 \r\n";
                    //  " ,( select startdate from [dbo].[tbl_ExtendedBreakSetup] \r\n" +
                    //" where description= 'X-MAS 2015') z \r\n" +

                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " where p.workplaceid = w.workplaceid and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth \r\n";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth \r\n";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth \r\n";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and calendardate = (select max(calendardate) from planning p, workplace w where p.workplaceid = w.workplaceid and w.description = '" + wp + "')  \r\n";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and w.description = '" + wp + "') a \r\n";
                    _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan2.ResultsTableName = "Report";
                    _dbMan2.ExecuteInstruction();

                    DataSet ReportDatasetReport = new DataSet();
                    ReportDatasetReport.Tables.Add(_dbMan2.ResultsDataTable);

                    if (_dbMan2.ResultsDataTable.Rows.Count < 1)
                    {
                        MessageBox.Show("No Data for selected criteria");
                        return;
                    }


                    WPStopRep.RegisterData(ReportDatasetReport);

                    //if (SysSettings.Banner == "Tau Tona" || SysSettings.Banner == "Savuka")
                    //{
                    //    if (Act == "1")
                    //        WPStopRep.Load("StopTTDev.frx");
                    //    else
                    //        WPStopRep.Load("StopTT.frx");

                    //}
                    //else
                    //{
                    //if (SysSettings.Banner == "Kopanang")
                    //{
                    //    if (Act == "1")
                    //    {
                    //        //WPStopRep.Load("StopStartDevStopKP.frx");
                    //        if (stopCat == "04 - 30")
                    //            WPStopRep.Load("StopStartDevStopKP.frx");
                    //        else
                    //            WPStopRep.Load("StopStartDevStopKP.frx");


                    //    }
                    //    else
                    //    {
                    //        // WPStopRep.Load("StopStartStopingStopKP.frx");
                    //        if (stopCat == "04 - 30")
                    //            WPStopRep.Load("StopStartStopingStopKP.frx");
                    //        else
                    //            WPStopRep.Load("StopStartStopingStopKP.frx");


                    //    }
                    //}
                    //else
                    //{
                    //if (SysSettings.Banner == "Moab Khotsong" || SysSettings.Banner == "Great Noligwa")
                    //{
                    string IDStopDev = "";
                    if (SysSettings.Banner == "Doornkop Mine")
                        IDStopDev = "1603";
                    if (SysSettings.Banner == "Joel Mine")
                        IDStopDev = "1606";
                    if (SysSettings.Banner == "Masimong Mine")
                        IDStopDev = "1724";

                    if (Act == "1")
                    {
                        if (stopCat == "04 - 14")
                        {
                            //WPStopRep.Load(TGlobalItems.ReportsFolder + "\\StopDevMK(4 - 14).frx");
                        }
                        else
                        {
                            try
                            {

                                ///New
                                DevExpress.XtraSplashScreen.IOverlaySplashScreenHandle handle = DevExpress.XtraSplashScreen.SplashScreenManager.ShowOverlayForm(this);

                                try
                                {

                                    MoveToProdind = true;
                                    GetFromInfo(IDStopDev, MOSecID, WPID, WPDesc);
                                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);


                                    // UpdateSchedule2(SelectedFieldNameDate, SelectedUniqueID);


                                }
                                catch
                                {
                                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);
                                    //MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                ///////////////

                                // //Write Checklist id to textfile







                            }
                            catch
                            {
                                MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            LoadStopStart();
                        }
                        //WPStopRep.Load(TGlobalItems.ReportsFolder + "\\StopDevMK(14Plus).frx");

                    }
                    else
                    {
                        if (stopCat == "04 - 14")
                        {
                            //WPStopRep.Load(TGlobalItems.ReportsFolder + "\\StopStpMK(4 - 14).frx");
                        }

                        else
                        {
                            //  WPStopRep.Load(TGlobalItems.ReportsFolder + "\\StopStpMK(14Plus).frx");



                            try
                            {

                                //Write Checklist id to textfile


                                //string path = @"C:\Mineware\Syncromine\OCRForm.Txt";


                                //using (StreamWriter writetext = new StreamWriter(path))
                                //{
                                //    writetext.WriteLine("1467");//Stoping Doornkop StopDoc Stoping
                                //}




                                /////
                                //Process Shec = new Process();
                                //Shec.StartInfo.FileName = @"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe";
                                //Shec.StartInfo.Arguments = Param;
                                //Shec.StartInfo.UseShellExecute = false;
                                //Shec.StartInfo.CreateNoWindow = true;
                                //Shec.StartInfo.RedirectStandardOutput = true;
                                //Shec.StartInfo.Verb = "runas";
                                //Shec.Start();
                                //Shec.WaitForExit();

                                string IDStopStope = "";
                                if (SysSettings.Banner == "Doornkop Mine")
                                    IDStopStope = "1467";
                                if (SysSettings.Banner == "Joel Mine")
                                    IDStopStope = "1484";
                                if (SysSettings.Banner == "Masimong Mine")
                                    IDStopStope = "1727";


                                ///New
                                DevExpress.XtraSplashScreen.IOverlaySplashScreenHandle handle = DevExpress.XtraSplashScreen.SplashScreenManager.ShowOverlayForm(this);

                                try
                                {

                                    MoveToProdind = true;
                                    GetFromInfo(IDStopStope, MOSecID, WPID, WPDesc);
                                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);


                                    // UpdateSchedule2(SelectedFieldNameDate, SelectedUniqueID);


                                }
                                catch
                                {
                                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);
                                    //MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                ///////////////




                            }
                            catch
                            {
                                MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            LoadStopStart();
                        }

                    }

                    //  WPStopRep.Load("StopStartMoabCheckList.frx");
                    // WPStopRep3.Load("StopStartMoabCheckList Pg1.frx");
                    //}
                    //else
                    //{
                    //    if (Act == "1")
                    //        WPStopRep.Load("StopDocDev.frx");
                    //    else
                    //        WPStopRep.Load("StopDoc.frx");
                    //}
                    //}
                    //}
                }
                else
                {

                    MWDataManager.clsDataAccess _dbManWPSTDetail = new MWDataManager.clsDataAccess();
                    _dbManWPSTDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManWPSTDetail.SqlStatement = " insert into  tbl_ExtBreakStopDoc " +

                                                    " Values( '" + wp + "', '" + LastDateBook + "', '" + clsUserInfo.UserID + "', getdate()) " +

                                                    " exec  [dbo].[sp_ExtendedBreakPrint]  ";

                    _dbManWPSTDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManWPSTDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManWPSTDetail.ExecuteInstruction();


                    string IDExtBreakStop = "";
                    if (SysSettings.Banner == "Doornkop Mine")
                        IDExtBreakStop = "1468";
                    if (SysSettings.Banner == "Joel Mine")
                        IDExtBreakStop = "1486";
                    if (SysSettings.Banner == "Masimong Mine")
                        IDExtBreakStop = "1730";

                    ///New
                    DevExpress.XtraSplashScreen.IOverlaySplashScreenHandle handle = DevExpress.XtraSplashScreen.SplashScreenManager.ShowOverlayForm(this);

                    try
                    {

                        MoveToProdind = true;
                        GetFromInfo(IDExtBreakStop, MOSecID, WPID, WPDesc);
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);


                        // UpdateSchedule2(SelectedFieldNameDate, SelectedUniqueID);


                    }
                    catch
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);
                        //MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }




                    LoadStopStart();
                    //WPStopRep.
                }




            }




            if (col == "WP Start Doc.  (DQlik Print) ")
            {



                //if (stopdoc == "No")
                //{
                //    MessageBox.Show("Stop Documentation .....                                                                                 Must be printed and complete before a start Procedure can be started.", "Stop Documentation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                   
                //    return;

                //}

                //if (stopdoc == "Print")
                //{
                //    MessageBox.Show("Stop Documentation .....                                                                                 Must completed before a start Procedure can be started.", "Stop Documentation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;

                //}

                //if (stopdoc == "Actions Open")
                //{
                //    MessageBox.Show("Stop Documentation .....                                                                                 Outsatanding Actions must completed before a start Procedure can be started.", "Stop Documentation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;

                //}


                //if (startdoc == "Actions Open")
                //{
                //    MessageBox.Show("Start Documentation .....                                                                                 Start Procedure has already started.", "Start Documentation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;

                //}





                if (label17.Text == "Workplace Stop - Start Procedure")
                {

                    MWDataManager.clsDataAccess _dbManWPSTDetail = new MWDataManager.clsDataAccess();
                    _dbManWPSTDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManWPSTDetail.SqlStatement = " insert into  tbl_WPStartDoc " +

                                                    " Values( '" + wp + "', '" + LastDateBook + "', '" + clsUserInfo.UserID + "', getdate()) " +
                                                    " exec  [dbo].[sp_WPStopStartDocSumPrint] '" + wp + "' ; update [tbl_WPStopStartDocSum] set sstart = 'Print' where  description+lastbook in ( " +
                                                    " select workplace+lastbookdate from[dbo].[tbl_WPStartDoc]) and sstart = 'No'";

                    _dbManWPSTDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManWPSTDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManWPSTDetail.ExecuteInstruction();

                    if (stopCat != "04 - 14")
                    {
                        MWDataManager.clsDataAccess _dbManWPSTDetail11 = new MWDataManager.clsDataAccess();
                        _dbManWPSTDetail11.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManWPSTDetail11.SqlStatement = " insert into  [tbl_WPStartDocFinal] " +

                                                        " Values( '" + wp + "', '" + LastDateBook + "', '" + clsUserInfo.UserID + "', getdate()) " +

                                                        " exec  [dbo].[sp_WPStopStartDocSumPrint] '" + wp + "' ";

                        _dbManWPSTDetail11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManWPSTDetail11.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManWPSTDetail11.ExecuteInstruction();

                    }



                    //// load start Doc
                    ////Report
                    //MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                    //_dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan2.SqlStatement = "  " +
                    //                        " select max(description) Description, max(date) date, max(sbno) sbno, max(sbname) sbname, max(mono) mono, max(moname) moname, max(section) section from(  \r\n";
                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " select Description +'                '  Description, convert(varchar(12),getdate(),103) date, \r\n";

                    ////if (SysSettings.Banner == "Great Noligwa")
                    ////    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'GN'+s1.reporttosectionid+'             ' section \r\n";
                    ////if (SysSettings.Banner == "Moab Khotsong")
                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.EmployeeNo,'')  +'             ' sbno, s1.name sbname,  case when stopingcode = 'GN'  then 'GN'+s1.reporttosectionid+'             ' else  'MK'+s1.reporttosectionid+'             ' end as section \r\n";
                    ////if (SysSettings.Banner == "Kopanang")
                    ////    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'KP'+s1.reporttosectionid+'             ' section \r\n";

                    ////if (SysSettings.Banner == "Mponeng")
                    ////    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'MP'+s1.reporttosectionid+'             ' section \r\n";
                    ////if (SysSettings.Banner == "Tau Tona")
                    ////    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'TT'+s1.reporttosectionid+'             ' section \r\n";
                    ////if (SysSettings.Banner == "Savuka")
                    ////    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'SV'+s1.reporttosectionid+'             ' section \r\n";



                    //// _dbMan2.SqlStatement = _dbMan2.SqlStatement + " s1.pfnumber  +'             ' sbno, s1.name sbname, 'MP'+s1.reporttosectionid+'             ' section \r\n";
                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " ,isnull(s2.EmployeeNo,'')  +'             ' mono, s2.name moname \r\n";
                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " from planning p, workplace w, section s, section s1, section s2 \r\n";
                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " where p.workplaceid = w.workplaceid and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth \r\n";
                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth \r\n";

                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth \r\n";
                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " and calendardate = (select max(calendardate) from planning p, workplace w where p.workplaceid = w.workplaceid and w.description = '" + wp + "') \r\n";
                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " and w.description = '" + wp + "') a \r\n";



                    //_dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    //_dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    //_dbMan2.ResultsTableName = "Report";
                    //_dbMan2.ExecuteInstruction();

                    //DataSet ReportDatasetReport = new DataSet();
                    //ReportDatasetReport.Tables.Add(_dbMan2.ResultsDataTable);

                    //if (_dbMan2.ResultsDataTable.Rows.Count < 1)
                    //{
                    //    MessageBox.Show("No Data for selected criteria");
                    //    return;
                    //}


                    //WPStopRep.RegisterData(ReportDatasetReport);

                    if (Act != "1")
                    {

                        //WPStopRep.Load(TGlobalItems.ReportsFolder + "\\StopStartMoabCheckList.frx");

                        try
                        {

                            //Write Checklist id to textfile


                            //string path = @"C:\Mineware\Syncromine\OCRForm.Txt";


                            //using (StreamWriter writetext = new StreamWriter(path))
                            //{
                            //    writetext.WriteLine("1466");//Stoping Doornkop StartDoc Stoping
                            //}




                            /////
                            //Process Shec = new Process();
                            //Shec.StartInfo.FileName = @"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe";
                            //Shec.StartInfo.Arguments = Param;
                            //Shec.StartInfo.UseShellExecute = false;
                            //Shec.StartInfo.CreateNoWindow = true;
                            //Shec.StartInfo.RedirectStandardOutput = true;
                            //Shec.StartInfo.Verb = "runas";
                            //Shec.Start();
                            //Shec.WaitForExit();

                            string IDStopeStartUps = "";
                            if (SysSettings.Banner == "Doornkop Mine")
                                IDStopeStartUps = "1466";
                            if (SysSettings.Banner == "Joel Mine")
                                IDStopeStartUps = "1483";
                            if (SysSettings.Banner == "Masimong Mine")
                                IDStopeStartUps = "1725";

                            ///New
                            DevExpress.XtraSplashScreen.IOverlaySplashScreenHandle handle = DevExpress.XtraSplashScreen.SplashScreenManager.ShowOverlayForm(this);

                            try
                            {

                                MoveToProdind = true;
                                GetFromInfo(IDStopeStartUps, MOSecID, WPID, WPDesc);
                                DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);


                                // UpdateSchedule2(SelectedFieldNameDate, SelectedUniqueID);


                            }
                            catch
                            {
                                DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);
                                //MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            ///////////////




                        }
                        catch
                        {
                            MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        WPStopRep3.Load(TGlobalItems.ReportsFolder + "\\StopStartMoabCheckList Pg1.frx");

                        LoadStopStart();
                    }


                    if (Act == "1")
                    {

                        if (stopCat == "10 Above")
                        {
                            //WPStopRep.Load(TGlobalItems.ReportsFolder + "\\StopStartMoabCheckList.frx");

                            try
                            {

                                ////Write Checklist id to textfile


                                //string path = @"C:\Mineware\Syncromine\OCRForm.Txt";


                                //using (StreamWriter writetext = new StreamWriter(path))
                                //{
                                //    writetext.WriteLine("1604");//Dev Doornkop StartDoc 
                                //}




                                /////
                                //Process Shec = new Process();
                                //Shec.StartInfo.FileName = @"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe";
                                //Shec.StartInfo.Arguments = Param;
                                //Shec.StartInfo.UseShellExecute = false;
                                //Shec.StartInfo.CreateNoWindow = true;
                                //Shec.StartInfo.RedirectStandardOutput = true;
                                //Shec.StartInfo.Verb = "runas";
                                //Shec.Start();
                                //Shec.WaitForExit();

                                string IDDevStartUps = "";
                                if (SysSettings.Banner == "Doornkop Mine")
                                    IDDevStartUps = "1604";
                                if (SysSettings.Banner == "Joel Mine")
                                    IDDevStartUps = "1607";
                                if (SysSettings.Banner == "Masimong Mine")
                                    IDDevStartUps = "1726";
                                ///New
                                DevExpress.XtraSplashScreen.IOverlaySplashScreenHandle handle = DevExpress.XtraSplashScreen.SplashScreenManager.ShowOverlayForm(this);

                                try
                                {

                                    MoveToProdind = true;
                                    GetFromInfo(IDDevStartUps, MOSecID, WPID, WPDesc);
                                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);


                                    // UpdateSchedule2(SelectedFieldNameDate, SelectedUniqueID);


                                }
                                catch
                                {
                                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);
                                    //MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                ///////////////




                            }
                            catch
                            {
                                MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            WPStopRep3.Load(TGlobalItems.ReportsFolder + "\\StopStartMoabCheckList Pg1.frx");

                            LoadStopStart();
                        }





                    }
                }
                else
                {
                    MWDataManager.clsDataAccess _dbManWPSTDetail = new MWDataManager.clsDataAccess();
                    _dbManWPSTDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManWPSTDetail.SqlStatement = " insert into  tbl_ExtBreakStartDoc " +

                                                    " Values( '" + wp + "', '" + LastDateBook + "', '" + clsUserInfo.UserID + "', getdate()) " +

                                                    " exec  [dbo].[sp_ExtendedBreakPrint]  ";

                    _dbManWPSTDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManWPSTDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManWPSTDetail.ExecuteInstruction();

                    string IDDevStartUpsShutDownDev = "";
                    if (SysSettings.Banner == "Doornkop Mine")
                        IDDevStartUpsShutDownDev = "1472";
                    if (SysSettings.Banner == "Joel Mine")
                        IDDevStartUpsShutDownDev = "1485";
                    if (SysSettings.Banner == "Masimong Mine")
                        IDDevStartUpsShutDownDev = "1729";

                    ///New
                    DevExpress.XtraSplashScreen.IOverlaySplashScreenHandle handle = DevExpress.XtraSplashScreen.SplashScreenManager.ShowOverlayForm(this);

                    try
                    {

                        MoveToProdind = true;
                        GetFromInfo(IDDevStartUpsShutDownDev, MOSecID, WPID, WPDesc);
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);


                        // UpdateSchedule2(SelectedFieldNameDate, SelectedUniqueID);


                    }
                    catch
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);
                        //MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ///////////////


                    //MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                    //_dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan2.SqlStatement = " select * from ( ";
                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " select max(description) Description, max(startdate) date, max(sbno) sbno, max(sbname) sbname, max(mono) mono, max(moname) moname, max(section) section from(  \r\n";
                    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + " select Description +'                '  Description, convert(varchar(12),getdate(),103) date, convert(varchar(12),startdate,103) startdate, \r\n";


                    //if (SysSettings.Banner == "Great Noligwa")
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'GN'+s1.reporttosectionid+'             ' section \r\n";
                    //    if (SysSettings.Banner == "Moab Khotsong")
                    //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.EmployeeNo,'')  +'             ' sbno, s1.name sbname,  case when stopingcode = 'GN'  then 'GN'+s1.reporttosectionid+'             ' else  'MK'+s1.reporttosectionid+'             ' end as section \r\n";
                    //    //if (SysSettings.Banner == "Kopanang")
                    //    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'KP'+s1.reporttosectionid+'             ' section \r\n";

                    //    //if (SysSettings.Banner == "Mponeng")
                    //    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'MP'+s1.reporttosectionid+'             ' section \r\n";
                    //    //if (SysSettings.Banner == "Tau Tona")
                    //    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'TT'+s1.reporttosectionid+'             ' section \r\n";
                    //    //if (SysSettings.Banner == "Savuka")
                    //    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.pfnumber,'')  +'             ' sbno, s1.name sbname, 'SV'+s1.reporttosectionid+'             ' section \r\n";



                    //    //_dbMan2.SqlStatement = _dbMan2.SqlStatement + "" s1.pfnumber  +'             ' sbno, s1.name sbname, 'MP'+s1.reporttosectionid+'             ' section \r\n" +

                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + ",isnull(s2.EmployeeNo,'')  +'             ' mono, s2.name moname \r\n";

                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " from planning p, workplace w, section s, section s1, section s2 \r\n";

                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " ,( select enddate startdate from [dbo].[tbl_ExtendedBreakSetup] \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " where description= '" + ExtBreak + "') z \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " where p.workplaceid = w.workplaceid and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and calendardate = (select max(calendardate) from planning p, workplace w where p.workplaceid = w.workplaceid and w.description = '" + wp + "')  \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and w.description = '" + wp + "'  and substring(p.sectionid,1,4)  = '" + mo + "') a \r\n";


                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " union select max(description) Description, max(startdate) date, max(sbno) sbno, max(sbname) sbname, max(mono) mono, max(moname) moname, max(section) section from(  \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select Description +'             '  Description, convert(varchar(12),getdate(),103) date, convert(varchar(12),startdate,103) startdate, \r\n";


                    //    //if (SysSettings.Banner == "Great Noligwa")
                    //    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + "  s1.pfnumber  +'             ' sbno, s1.name sbname, 'GN'+s1.reporttosectionid+'             ' section \r\n";
                    //    //if (SysSettings.Banner == "Moab Khotsong")
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " isnull(s1.EmployeeNo,'')  +'             ' sbno, s1.name sbname, 'MK'+s1.reporttosectionid+'             ' section \r\n";
                    //    //if (SysSettings.Banner == "Kopanang")
                    //    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " s1.pfnumber  +'             ' sbno, s1.name sbname, 'KP'+s1.reporttosectionid+'             ' section \r\n";

                    //    //if (SysSettings.Banner == "Mponeng")
                    //    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + "  s1.pfnumber  +'             ' sbno, s1.name sbname, 'MP'+s1.reporttosectionid+'             ' section \r\n";
                    //    //if (SysSettings.Banner == "Tau Tona")
                    //    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " s1.pfnumber  +'             ' sbno, s1.name sbname, 'TT'+s1.reporttosectionid+'             ' section \r\n";
                    //    //if (SysSettings.Banner == "Savuka")
                    //    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " s1.pfnumber  +'             ' sbno, s1.name sbname, 'SV'+s1.reporttosectionid+'             ' section \r\n";


                    //    // _dbMan2.SqlStatement = _dbMan2.SqlStatement + " s1.pfnumber  +'             ' sbno, s1.name sbname, 'MP'+s1.reporttosectionid+'             ' section \r\n";

                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " ,isnull(s2.EmployeeNo,'')  +'             ' mono, s2.name moname \r\n";

                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " from planning_vamping p, workplace w, section s, section s1, section s2 \r\n";


                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " ,( select enddate startdate from [dbo].[tbl_ExtendedBreakSetup] \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " where description= '" + ExtBreak + "') z \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " where p.workplaceid = w.workplaceid and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and calendardate = (select max(calendardate) from planning_vamping p, workplace w where p.workplaceid = w.workplaceid and w.description = '" + wp + "')  \r\n";
                    //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and w.description = '" + wp + "' and substring(p.sectionid,1,4)  = '" + mo + "') a) a where description is not null \r\n";
                    //    _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    //    _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    //    _dbMan2.ResultsTableName = "Report";
                    //    _dbMan2.ExecuteInstruction();

                    //    DataSet ReportDatasetReport = new DataSet();
                    //    ReportDatasetReport.Tables.Add(_dbMan2.ResultsDataTable);

                    //    if (_dbMan2.ResultsDataTable.Rows.Count < 1)
                    //    {
                    //        MessageBox.Show("No Data for selected criteria");
                    //        return;
                    //    }


                    //    //WPStopRep.Delete();
                    //    //WPStopRep1.Delete();
                    //    //WPStopRep2.Delete();


                    //    //Report WPStopRep = new Report();
                    //    //Report WPStopRep1 = new Report();
                    //    //Report WPStopRep2 = new Report();



                    //    numrep = 1;
                    //    //if (SysSettings.Banner == "Mponeng")
                    //    //{
                    //    //    if (Act == "1")
                    //    //        WPStopRep.Load("ExtBreakStartUpMP.frx");
                    //    //    else
                    //    //        WPStopRep.Load("ExtBreakStartUpMP.frx");
                    //    //    //WPStopRep1.Load("ExtBreakStartUpNSMinersMP.frx");
                    //    //    //WPStopRep2.Load("ExtBreakStartUpDSMinersMP.frx");

                    //    //    WPStopRep.RegisterData(ReportDatasetReport);
                    //    //    //WPStopRep1.RegisterData(ReportDatasetReport);
                    //    //    // WPStopRep2.RegisterData(ReportDatasetReport);
                    //    //    numrep = 1;
                    //    //}


                    //    //if (SysSettings.Banner == "Tau Tona")
                    //    //{
                    //    //    if (Act == "1")
                    //    //        WPStopRep.Load("ExtBreakStartUpDevTT.frx");
                    //    //    if (Act != "1")
                    //    //        WPStopRep.Load("ExtBreakStartUpStopeTT.frx");

                    //    //    WPStopRep.RegisterData(ReportDatasetReport);

                    //    //    numrep = 1;
                    //    //}


                    //    //if (SysSettings.Banner == "Savuka")
                    //    //{
                    //    //    if (Act == "1")
                    //    //        WPStopRep.Load("ExtBreakStartUpDevTT.frx");
                    //    //    if (Act != "1")
                    //    //        WPStopRep.Load("ExtBreakStartUpStopeTT.frx");

                    //    //    WPStopRep.RegisterData(ReportDatasetReport);

                    //    //    numrep = 1;
                    //    //}



                    //    //if (SysSettings.Banner == "Moab Khotsong")
                    //    //{

                    //    //W/PStopRep.Load("StopStartMoabCheckList.frx");

                    //    if (Act == "1")
                    //        WPStopRep.Load(TGlobalItems.ReportsFolder + "\\ExtBreakStartUpMK.frx");
                    //    if (Act != "1")
                    //        WPStopRep.Load(TGlobalItems.ReportsFolder + "\\ExtBreakStartUpMK.frx");


                    //    WPStopRep.RegisterData(ReportDatasetReport);
                    //    numrep = 1;

                    //    //}

                    //    //if (SysSettings.Banner == "Great Noligwa")
                    //    //{
                    //    //if (Act == "1")
                    //    //    WPStopRep.Load("ExtBreakStartUpMK.frx");
                    //    //if (Act != "1")
                    //    //    WPStopRep.Load("ExtBreakStartUpMK.frx");



                    //    ////WPStopRep.Load("StopStartMoabCheckList.frx");
                    //    //WPStopRep.RegisterData(ReportDatasetReport);

                    //    //numrep = 1;

                    //    //}


                    //    //if (SysSettings.Banner == "Kopanang")
                    //    //{

                    //    //    // WPStopRep.Load("ExtBreakStartUpKP.frx");

                    //    //    if (Act == "1")
                    //    //        WPStopRep.Load("ExtBreakStartUpKPDev.frx");
                    //    //    else
                    //    //        WPStopRep.Load("ExtBreakStartUpKPStp.frx");

                    //    //    WPStopRep.RegisterData(ReportDatasetReport);

                    //    //    numrep = 1;

                    //    //}

                    //}


                    //// WPStopRep.Design();

                    //FastReport.Utils.XmlItem item = FastReport.Utils.Config.Root.FindItem("Forms").FindItem("PreviewForm");
                    //item.SetProp("Normal", "0");

                    //// WPStopRep.Design();
                    //WPStopRep.Show(false);

                    ////if (SysSettings.Banner == "Moab Khotsong" || SysSettings.Banner == "Great Noligwa")
                    ////{
                    //if (label17.Text == "Workplace Stop - Start Procedure")
                    //    WPStopRep3.Show(null);

                    ////}


                    //WPStopRep.Report.Preview.BringToFront();
                    //if (numrep > 1)
                    //{
                    //    WPStopRep1.Show(false);
                    //    WPStopRep1.Report.Preview.BringToFront();
                    //}
                    //if (numrep > 2)
                    //{
                    //    WPStopRep2.Show(false);
                    //    WPStopRep2.Report.Preview.BringToFront();
                    //}

                    //LoadStopStart();
                    LoadStopStart();
                }
            }




        }


        private void GetFromInfo(string FormsID, string MOSecID, string WPID, string WPDesc)
        {
            try
            {
                string GetFormInfoURL = string.Format(@"/api/Forms/GetFormInfo/");
                var client = new ClientConnect();
                var param = new Dictionary<string, string>();
                param.Add("FormID", FormsID);

                var response = System.Threading.Tasks.Task.Run(() => client.GetWithParameters(GetFormInfoURL, param)).Result;

                _Forms = JsonConvert.DeserializeObject<FormsAPI>(response);

                _Forms.UniqueDataStructure.TableName = _Forms.TableName;

                if (MoveToProdind == true)
                {
                    _Forms.UniqueDataStructure.Rows[0].Delete();

                    DataRow row;
                    row = _Forms.UniqueDataStructure.NewRow();

                    row["MOSectionID"] = MOSecID;
                    row["Workplaceid"] = WPID;
                    row["Description"] = WPDesc;
                    row["CaptureDate"] = DateTime.Now.ToString("ddMMyyyy");

                    _Forms.UniqueDataStructure.Rows.Add(row);
                }
                else
                {
                    _Forms.UniqueDataStructure.Rows[0].Delete();

                    DataRow row;
                    row = _Forms.UniqueDataStructure.NewRow();

                    row["MOSectionID"] = "EXAMPLE";
                    row["Workplaceid"] = "EXAMPLE";
                    row["Description"] = "EXAMPLE";
                    row["CaptureDate"] = DateTime.Now.ToString("ddMMyyyy");

                    _Forms.UniqueDataStructure.Rows.Add(row);
                }



            }
            catch
            {

            }

            GetReport(FormsID);
            if (MoveToProdind == true)
            {
                //MoveToProd();
                MovetoProd.DoWork += MovetoProd_DoWork;
                MovetoProd.RunWorkerAsync();
            }

        }


        private void GetReport(string FormsID)
        {
            string GetReportRL = string.Format(@"/api/Report/GetReport/");
            var client = new ClientConnect();
            var param = new Dictionary<string, string>();
            var header = new Dictionary<string, string>();
            param.Add("FormsID", FormsID);
            _Forms.UniqueDataStructure.AcceptChanges();
            DataSet TheData = new DataSet();
            TheData.Tables.Clear();
            TheData.Tables.Add(_Forms.UniqueDataStructure);
            string JSOResult;
            JSOResult = JsonConvert.SerializeObject(TheData, Formatting.Indented);
            try
            {
                //this.pdfViewer1.LoadDocument(@"..\..\Report.pdf"

                var response = Task.Run(() => client.PostWithBodyAndParameters(GetReportRL, param, JSOResult)).Result;
                _PrintedForm = JsonConvert.DeserializeObject<PrintedForm>(response);
                string txtPDF = _PrintedForm.PDFLocation;

                if (File.Exists(@_PrintedForm.PDFLocation))
                {
                    if (MoveToProdind)
                    {
                        //Process.Start("explorer.exe", _PrintedForm.PDFLocation);
                        PdfViewer i = new PdfViewer();
                        DevExpress.XtraEditors.XtraForm jj = new DevExpress.XtraEditors.XtraForm();
                        i.LoadDocument(@_PrintedForm.PDFLocation);
                        i.Dock = DockStyle.Fill;
                        i.ZoomMode = PdfZoomMode.FitToWidth;
                        i.NavigationPanePageVisibility = PdfNavigationPanePageVisibility.None;
                        jj.Controls.Add(i);
                        jj.Width = 600;
                        jj.Height = 800;
                        jj.Show();
                        jj.ShowIcon = false;
                        jj.Text = "OCR - DOCUMENT PREVIEW";
                        i.CreateRibbon();
                        jj.ShowDialog();
                        // PdfPrinterSettings ps = new PdfPrinterSettings();
                        // i.Print(ps);
                    }
                    else
                    {
                        PdfViewer i = new PdfViewer();
                        DevExpress.XtraEditors.XtraForm jj = new DevExpress.XtraEditors.XtraForm();
                        i.LoadDocument(@_PrintedForm.PDFLocation);
                        i.Dock = DockStyle.Fill;
                        i.ZoomMode = PdfZoomMode.FitToWidth;
                        i.NavigationPanePageVisibility = PdfNavigationPanePageVisibility.None;
                        jj.Controls.Add(i);
                        jj.Width = 600;
                        jj.Height = 800;
                        jj.Show();
                        jj.ShowIcon = false;
                        jj.Text = "CHECKLIST EXAMPLE - CANNOT BE PRINTED";
                        jj.ShowDialog();
                    }

                }

            }
            catch (Exception error)
            {

            }
        }

        private void MoveToProd()
        {
            string GetFormInfoURL = string.Format(@"/api/Report/PrintReport/");
            foreach (string s in _PrintedForm.PrintedFromID)
            {

                var client = new ClientConnect();
                var param = new Dictionary<string, string>();
                param.Add("PrintedFromID", s);
                param.Add("PrintedByName", "Dolf");

                var response = Task.Run(() => client.GetWithParameters(GetFormInfoURL, param)).Result;
            }
        }

        private void bandedGridView11_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View = sender as GridView;
            string ss = "";

            if (e.Column == View.Columns[7])
            {
                if (!View.GetRowCellValue(e.RowHandle, e.Column).Equals(DBNull.Value))
                {
                    ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                    if (View.GetRowCellValue(e.RowHandle, e.Column).ToString() == "Top20")
                    {
                        e.Graphics.DrawImage(pictureBox6.Image, e.Bounds.X + 2, e.Bounds.Y - 19, 40, 52);
                        e.Handled = false;
                    }
                }
            }


            if (e.Column == View.Columns[8])
            {
                if (!View.GetRowCellValue(e.RowHandle, e.Column).Equals(DBNull.Value))
                {
                    ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                    if (View.GetRowCellValue(e.RowHandle, e.Column).ToString() == "CRed")
                    {
                        e.Graphics.DrawImage(pictureBox7.Image, e.Bounds.X - 8, e.Bounds.Y - 6, 55, 30);
                        e.Handled = false;
                    }
                }
            }


            if (e.Column == View.Columns[8])
            {
                if (!View.GetRowCellValue(e.RowHandle, e.Column).Equals(DBNull.Value))
                {
                    ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                    if (View.GetRowCellValue(e.RowHandle, e.Column).ToString() == "BYel")
                    {
                        e.Graphics.DrawImage(pictureBox8.Image, e.Bounds.X - 8, e.Bounds.Y - 6, 55, 30);
                        e.Handled = false;
                    }
                }
            }

            //if (e.Column.AbsoluteIndex == 3)
            //{
            //    e.Graphics.DrawImage(pictureBox1.Image, e.Bounds.X-40, e.Bounds.Y-19, 40, 50);
            //    e.Handled = false;
            //}
        }

        string MOSecID = "";
        string WPID = "";
        string WPDesc = "";

        private void bandedGridView11_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            wp = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[1]).ToString();
            LastDateBook = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[2]).ToString();
            col = e.Column.ToString();
            stopdoc = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[4]).ToString();
            startdoc = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[6]).ToString();

            stopCat = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[5]).ToString();

            Act = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[9]).ToString();

            ExtBreak = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[10]).ToString();

            mo = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[0]).ToString();
            mo = mo + "              ";
            mo = mo.Substring(0, 4);

            mo = mo;





            Cursor = Cursors.WaitCursor;
            ///// new
            MWDataManager.clsDataAccess _dbManWPSTDetail22 = new MWDataManager.clsDataAccess();
            _dbManWPSTDetail22.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPSTDetail22.SqlStatement = " exec [sp_OCR_WorkplaceDetailPrint]  '" + wp + "' \r\n" +

            " ";
            _dbManWPSTDetail22.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPSTDetail22.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPSTDetail22.ExecuteInstruction();

            MOSecID = _dbManWPSTDetail22.ResultsDataTable.Rows[0]["MOSectionID"].ToString();
            WPID = _dbManWPSTDetail22.ResultsDataTable.Rows[0]["Workplaceid"].ToString();
            WPDesc = _dbManWPSTDetail22.ResultsDataTable.Rows[0]["Description"].ToString();



            Cursor = Cursors.Default;
        }

        string Param = "";


        private void button56_Click(object sender, EventArgs e)
        {
            LoadStopStart();
        }

        private void bandedGridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View1 = sender as GridView;

            string ss1 = "";
            //if (e.Column == View.Columns[5])
            //{
            ss1 = View1.GetRowCellValue(e.RowHandle, e.Column).ToString();

            if (ss1 == "Orange")
            {

                e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(MedLbl.Text));
                e.Appearance.ForeColor = Color.FromArgb(Convert.ToInt32(MedLbl.Text));

            }
            if (ss1 == "Red")
            {
                e.Appearance.BackColor = Color.FromArgb(Convert.ToInt32(HighLbl.Text));
                e.Appearance.ForeColor = Color.FromArgb(Convert.ToInt32(HighLbl.Text));
            }

            if (ss1 == "Off")
            {
                e.Appearance.BackColor = Color.Gainsboro;
                e.Appearance.ForeColor = Color.Gainsboro;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (simpleButton3.Text != "Show Outstanding Extended Breaks")
            {

                simpleButton3.Text = "Show Outstanding Extended Breaks";
                label17.Text = "Workplace Stop - Start Procedure";

                //if (SysSettings.Banner == "Moab Khotsong" || SysSettings.Banner == "Great Noligwa")
                //    Col1WPStartDoc1.Visible = false;

                col1Situation.Visible = true;
                ColExtBreak.Visible = false;
            }

            else
            {
                simpleButton3.Text = "Show Outstanding Workplace Stoppages";
                label17.Text = "Extended Break Documentation";
                col1Situation.Visible = false;
                ColExtBreak.Visible = true;
                Col1WPStartDoc1.Visible = true;
            }


            LoadStopStart();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (simpleButton3.Text != "Show Outstanding Extended Breaks")
            {
                barButtonItem5.Caption = "Show Outstanding Extended Breaks";

                simpleButton3.Text = "Show Outstanding Extended Breaks";
                label17.Text = "Workplace Stop - Start Procedure";

                //if (SysSettings.Banner == "Moab Khotsong" || SysSettings.Banner == "Great Noligwa")
                //    Col1WPStartDoc1.Visible = false;

                col1Situation.Visible = true;
                ColExtBreak.Visible = false;
            }

            else
            {
                barButtonItem5.Caption = "Show Stop - Start Procedure";
                simpleButton3.Text = "Show Outstanding Workplace Stoppages";
                label17.Text = "Extended Break Documentation";
                col1Situation.Visible = false;
                ColExtBreak.Visible = true;
                Col1WPStartDoc1.Visible = true;
            }


            LoadStopStart();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ucMainDashboard_Load(null, null);
            Cursor = Cursors.Default;
        }
    }
}