using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using Mineware.Systems.Global;
using DevExpress.XtraScheduler.Commands;
using Mineware.Systems.GlobalConnect;
using FastReport;
using FastReport.Export.Pdf;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;

//using System.ComponentModel
//using System.IO;
using System.Text.RegularExpressions;

using System.Threading;
using System.Globalization;
using System.IO;
using System.Drawing.Printing;
using Mineware.Systems.Production.SysAdminScreens.OCRScheduling;

using System.Text;
using Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPdfViewer;
using DevExpress.Pdf;
using Newtonsoft.Json;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class ucMinersReport : Mineware.Systems.Global.ucBaseUserControl
    {
        Report MorningReportReturn = new Report();
        Report MorningReport = new Report();
        Report SiesRep = new Report();

        public string MoringRepSec = "";
        public string MoringRepWP = "";
        public string MoringRepSeis = "";
        public string MoringRepRisk = "";

        string s;
        string newwp = "";
        string ImageDir = "";
        Procedures procs = new Procedures();
        string Param = "";

        //OCR
        bool printPreview = false;
        clsOCRScheduling _clsOCRScheduling = new clsOCRScheduling();
        //Procedures procs = new Procedures();
        private BackgroundWorker MovetoProd = new BackgroundWorker();
        private Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models.FormsAPI _Forms; //PduPlessis
        private Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models.PrintedForm _PrintedForm; //PduPlessis
        //List<ListDrop> _items = new List<ListDrop>();
        List<DataRow> list = new List<DataRow>();
        private bool MoveToProdind = false;

        public ucMinersReport()
        {
            InitializeComponent();
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

        private void ucMinersReport_Load(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManWPST2 = new MWDataManager.clsDataAccess();
            _dbManWPST2.ConnectionString = TConnections.GetConnectionString("DBHARMONYPAS", "Doornkop");
            _dbManWPST2.SqlStatement = "Select * from sysset  \r\n";
            _dbManWPST2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST2.ExecuteInstruction();

            if (_dbManWPST2.ResultsDataTable.Rows.Count > 0)
            {
                ImageDir = _dbManWPST2.ResultsDataTable.Rows[0]["REPDIR"].ToString();
            }


            radioGroup1_SelectedIndexChanged(null, null);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //day
            if (radioGroup1.SelectedIndex == 0)
            {
                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement = "select s2.sectionid + ':'+ s2.name moname, max(s2.EmployeeNo) mono, " +
                                       "s.sectionid + ':'+ s.name minname, max(s.EmployeeNo) minno " +

                                       "from planning p, section s, section s1, section s2 where calendardate = convert(varchar(15),getdate(),102) " +
                                       "and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth " +
                                       "and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth " +
                                       "and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth " +

                                       "group by  s2.sectionid  ,  s.sectionid , s.name , s2.Name   ";
                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ExecuteInstruction();

                DataTable dtLToOp = _dbMan2.ResultsDataTable;

                DataSet dsLToOp = new DataSet();

                if (dsLToOp.Tables.Count > 0)
                    dsLToOp.Tables.Clear();

                dsLToOp.Tables.Add(dtLToOp);


                MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan3.SqlStatement = "select distinct * from (select \r\n" +
                                       "s.sectionid + ':'+ s.name minname, w.description, s.SectionID      \r\n" +

                                       "from planning p, section s, section s1, section s2, workplace w where p.workplaceid = w.workplaceid and calendardate = convert(varchar(15),getdate(),102) \r\n" +
                                       "and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth \r\n" +
                                       "and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth \r\n" +
                                       "and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth \r\n" +

                                       "group by  s2.sectionid , s2.name , s2.EmployeeNo ,  \r\n" +
                                       "s.sectionid , s.name , s.EmployeeNo, w.description    \r\n" +

                                       ") a left outer join (select wpdescription, max(convert(varchar(20),calendardate, 8)) ss  from [dbo].[tbl_MorningReport_Print] where  \r\n" +
                                       "convert(varchar(15),calendardate,106) = convert(varchar(15),getdate(),106) group by wpdescription) b on a.description = b.wpdescription  \r\n" +


                                       "left outer join (select a.wpdescription, convert(decimal(18,1),a.risk) Seismic from  tbl_LicenceToOperate_Seismic a,  \r\n" +
                                       "(select wpdescription wp1, max(thedate) dd from  tbl_LicenceToOperate_Seismic group by wpdescription) b  \r\n" +
                                       "where a.wpdescription = b.wp1 and a.thedate = b.dd ) ac on a.description = ac.wpdescription  \r\n" +

                                       "left outer join (Select wpdescription,max(risk)risk from (   \r\n"+
" select a.wpdescription, convert(decimal(18, 0), a.resrisk) risk from[dbo].[tbl_LicenceToOperate_Risk] a,  \r\n"+
     " (select wpdescription wp1, max(thedate) dd from[tbl_LicenceToOperate_Risk] group by wpdescription) b  \r\n"+
" where a.wpdescription = b.wp1 and a.thedate = b.dd  )a group by wpdescription ) ad on a.description = ad.wpdescription  \r\n" +

                                       "order by SectionID, description  ";


                _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan3.ExecuteInstruction();


                DataTable dtDetail = _dbMan3.ResultsDataTable;

                dsLToOp.Tables.Add(dtDetail);

                dsLToOp.Relations.Clear();

                DataColumn keyColumn1 = dsLToOp.Tables[0].Columns[2];
                DataColumn foreignKeyColumn1 = dsLToOp.Tables[1].Columns[0];
                dsLToOp.Relations.Add("CategoriesProducts", keyColumn1, foreignKeyColumn1);


                gridControl4.DataSource = dsLToOp.Tables[0];
                gridControl4.LevelTree.Nodes.Add("CategoriesProducts", bandedGridView6);
                bandedGridView6.ViewCaption = "Workplace Detail";
                WPSub.FieldName = "description";
                WPSub.Width = 200;

                SeisCol.FieldName = "Seismic";
                RiskCol.FieldName = "risk";

                TimeSub.FieldName = "ss";


                ColMainMOSection.FieldName = "minname";
                ColMainContrator.FieldName = "moname";



                MWDataManager.clsDataAccess _dbMan3zz = new MWDataManager.clsDataAccess();
                _dbMan3zz.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan3zz.SqlStatement = "select  '" + SysSettings.Banner + "' banner,  *, isnull(Day14+Day13+Day12+Day10+Day9+Day8+Day7 " +
                                        "+Day6+Day5+Day4+Day3+Day2+Day1,0) num " +
                                         "from vw_MinersReport_Returned ";
                _dbMan3zz.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan3zz.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan3zz.ResultsTableName = "Data";
                _dbMan3zz.ExecuteInstruction();

                DataSet DsRep1 = new DataSet();
                DsRep1.Tables.Add(_dbMan3zz.ResultsDataTable);

                MorningReportReturn.RegisterData(DsRep1);


                MorningReportReturn.Load(TGlobalItems.ReportsFolder + "\\MinersReportReturned.frx");

               // MorningReportReturn.Design();

                PC9.OutlineVisible = true;

                PC9.Clear();
                MorningReportReturn.Prepare();
                MorningReportReturn.Preview = PC9;
                MorningReportReturn.ShowPrepared();
            }

            //night
            if (radioGroup1.SelectedIndex == 1)
            {
                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement =  "select s2.sectionid + ':'+ max(s2.name) moname, max(s2.EmployeeNo) mono, " +
                                       "s.sectionid + ':'+ s.name minname, s.EmployeeNo minno " +

                                       "from planning p, section s, section s1, section s2 where calendardate = convert(varchar(15),getdate(),102) " +
                                       "and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth " +
                                       "and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth " +
                                       "and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth " +

                                       "group by  s2.sectionid  ,  s.sectionid , s.name , s.EmployeeNo   ";
                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ExecuteInstruction();

                DataTable dtLToOp = _dbMan2.ResultsDataTable;

                DataSet dsLToOp = new DataSet();

                if (dsLToOp.Tables.Count > 0)
                    dsLToOp.Tables.Clear();

                dsLToOp.Tables.Add(dtLToOp);


                MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan3.SqlStatement = "select * from (select " +
                                       "s.sectionid + ':'+ s.name minname, w.description, s.SectionID  \r\n" +

                                       "from planning p, section s, section s1, section s2, workplace w where p.workplaceid = w.workplaceid and calendardate = convert(varchar(15),getdate(),102) \r\n" +
                                       "and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth \r\n" +
                                       "and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth \r\n" +
                                       "and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth \r\n" +

                                       "group by  s2.sectionid , s2.name , s2.EmployeeNo ,  \r\n" +
                                       "s.sectionid , s.name , s.EmployeeNo, w.description    \r\n" +

                                       ") a left outer join (select wpdescription, max(convert(varchar(20),calendardate, 8)) ss  from [dbo].[tbl_MorningReport_Print_NS] where  \r\n" +
                                       "convert(varchar(15),calendardate,106) = convert(varchar(15),getdate(),106) group by wpdescription) b on a.description = b.wpdescription  \r\n" +


                                       "left outer join (select a.wpdescription, convert(decimal(18,1),a.risk) Seismic from  tbl_LicenceToOperate_Seismic a,  \r\n" +
                                       "(select wpdescription wp1, max(thedate) dd from  tbl_LicenceToOperate_Seismic group by wpdescription) b  \r\n" +
                                       "where a.wpdescription = b.wp1 and a.thedate = b.dd ) ac on a.description = ac.wpdescription  \r\n" +

                                       "left outer join (Select wpdescription,max(risk)risk from (   \r\n" +
" select a.wpdescription, convert(decimal(18, 0), a.resrisk) risk from[dbo].[tbl_LicenceToOperate_Risk] a,  \r\n" +
     " (select wpdescription wp1, max(thedate) dd from[tbl_LicenceToOperate_Risk] group by wpdescription) b  \r\n" +
" where a.wpdescription = b.wp1 and a.thedate = b.dd  )a group by wpdescription ) ad on a.description = ad.wpdescription  \r\n" +

                                       //"left outer join (select a.wpdescription, convert(decimal(18,0),a.resrisk) risk from [dbo].[tbl_LicenceToOperate_Risk] a,  \r\n" +
                                       //"(select wpdescription wp1, max(thedate) dd from  [tbl_LicenceToOperate_Risk] group by wpdescription) b  \r\n" +
                                       //"where a.wpdescription = b.wp1 and a.thedate = b.dd ) ad on a.description = ad.wpdescription  \r\n" +

                                       "order by SectionID, description  ";


                _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan3.ExecuteInstruction();


                DataTable dtDetail = _dbMan3.ResultsDataTable;

                dsLToOp.Tables.Add(dtDetail);

                dsLToOp.Relations.Clear();

                DataColumn keyColumn1 = dsLToOp.Tables[0].Columns[2];
                DataColumn foreignKeyColumn1 = dsLToOp.Tables[1].Columns[0];
                dsLToOp.Relations.Add("CategoriesProducts", keyColumn1, foreignKeyColumn1);


                gridControl4.DataSource = dsLToOp.Tables[0];
                gridControl4.LevelTree.Nodes.Add("CategoriesProducts", bandedGridView6);
                bandedGridView6.ViewCaption = "Workplace Detail";
                WPSub.FieldName = "description";
                WPSub.Width = 200;

                SeisCol.FieldName = "Seismic";
                RiskCol.FieldName = "risk";

                TimeSub.FieldName = "ss";


                ColMainMOSection.FieldName = "minname";
                ColMainContrator.FieldName = "moname";



                MWDataManager.clsDataAccess _dbMan3zz = new MWDataManager.clsDataAccess();
                _dbMan3zz.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan3zz.SqlStatement = "select  '" + SysSettings.Banner + "' banner,  *, isnull(Day14+Day13+Day12+Day10+Day9+Day8+Day7 " +
                                        "+Day6+Day5+Day4+Day3+Day2+Day1,0) num " +
                                         "from vw_MinersReport_Returned ";
                _dbMan3zz.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan3zz.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan3zz.ResultsTableName = "Data";
                _dbMan3zz.ExecuteInstruction();

                DataSet DsRep1 = new DataSet();
                DsRep1.Tables.Add(_dbMan3zz.ResultsDataTable);

                MorningReportReturn.RegisterData(DsRep1);


                MorningReportReturn.Load(TGlobalItems.ReportsFolder + "\\MinersReportReturned.frx");

                //  MorningReportReturn.Design();

                PC9.OutlineVisible = true;

                PC9.Clear();
                MorningReportReturn.Prepare();
                MorningReportReturn.Preview = PC9;
                MorningReportReturn.ShowPrepared();
            }
        }

        private void bandedGridView4_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            MoringRepSec = bandedGridView4.GetRowCellValue(e.RowHandle, bandedGridView4.Columns[1]).ToString();
            MoringRepSec = ExtractBeforeColon(MoringRepSec);
        }

        private void btnPrintOnly_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            printPreview = true;
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "select s.sectionid, w.workplaceid, description, convert(decimal(18,0),p.activity) activity,CASE WHEN LEN(s.sectionid)>4 THEN LEFT(s.sectionid,4) ELSE S.Sectionid END AS MO_Section " +

                                   "from planning p, section s, section s1, section s2, workplace w  where p.workplaceid = w.workplaceid and calendardate = convert(varchar(15),getdate(),102) " +
                                   "and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth " +
                                   "and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth " +
                                   "and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth  " +

                                   "group by  s.sectionid, w.workplaceid, description, p.activity order by s.sectionid, description  ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dt = _dbMan.ResultsDataTable;



            foreach (DataRow dr in dt.Rows)
            {
                newwp = dr["description"].ToString();

                string activity = dr["activity"].ToString();

                Cursor = Cursors.WaitCursor;

                // do barcode
                string barcode = "";              
                barcode = "2";       

                if (radioGroup1.SelectedIndex == 1)
                {

                    barcode = "B";
                  
                }

                // workplace
                barcode = barcode + dr["workplaceid"].ToString();
                barcode = barcode + String.Format("{0:ddMMyyyy}", DateTime.Now);

              
                clsWorknoteData ReportData = new clsWorknoteData();
                ReportData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //Data for Barcode
                DataSet ReportDatasetReportBar = new DataSet();
                ReportDatasetReportBar.Tables.Add(ReportData.PrintOnlyALL(newwp, barcode, dr["workplaceid"].ToString()));
                //string activity = "";
                try
                {
                    activity = ReportDatasetReportBar.Tables[0].Rows[0]["activity"].ToString();
                }
                catch { }

                //Data for DS
                DataSet ReportDatasetReport2 = new DataSet();
                ReportDatasetReport2.Tables.Add(ReportData.WorkplaceGridDS(newwp, "D"));
                //Data for NS
                DataSet ReportDatasetReport2NS = new DataSet();
                ReportDatasetReport2.Tables.Add(ReportData.WorkplaceGridDS(newwp, "N"));
                //Data for CallCentre
                DataSet ReportDatasetReport3 = new DataSet();
                ReportDatasetReport3.Tables.Add(ReportData.CallCentre(newwp));
                //Data for Seismic
                DataSet ReportDatasetReport4 = new DataSet();
                ReportDatasetReport4.Tables.Add(ReportData.Seismic(newwp));
                decimal Seis = 0;
                try
                {
                    if (ReportDatasetReport4.Tables[0].Rows[0]["Seismic"].ToString() != "")
                    {

                        Seis = Convert.ToDecimal(ReportDatasetReport4.Tables[0].Rows[0]["Seismic"].ToString());
                    }
                }
                catch
                { }
                //Data for Seismic
                DataSet ReportDatasetReport5 = new DataSet();
                ReportDatasetReport5.Tables.Add(ReportData.Incident(newwp));
                //Data For Rock Eng Walkabouts
                DataSet ReportDatasetReport6 = new DataSet();
                ReportDatasetReport6.Tables.Add(ReportData.RockEngData(newwp));

                //TODO: Check below code nessesity 
                #region CheckCode

                string aa = "";
                try
                {
                    if (ReportDatasetReport6.Tables[0].Rows.Count > 0)
                    {

                        if (ReportDatasetReport6.Tables[0].Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (ReportDatasetReport6.Tables[0].Rows[0]["picture"].ToString() != "")
                            {
                                //PicBox.Image = procs.Base64ToImage(_dbManRMDetail.ResultsDataTable.Rows[0]["picture"].ToString(), MoringRepWP);
                                //PicBox.Image.Save(Application.StartupPath + "\\" + "Neil.bmp");
                                //aa = Application.StartupPath + "\\" + "Neil.bmp";
                            }


                        }
                    }
                }
                catch
                { }
                #endregion

                //Data For Rock Eng Walkabouts
                DataSet ReportDatasetReport6Pic = new DataSet();
                ReportDatasetReport6Pic.Tables.Add(ReportData.RockEngPicture(newwp));
                //Data For License to Operate
                DataSet ReportDatasetReportWPD1 = new DataSet();
                ReportDatasetReportWPD1.Tables.Add(ReportData.LicenseToOperate(newwp));
                //Data For PlanBook
                DataSet ReportDatasetReportWPPlanBook = new DataSet();
                ReportDatasetReportWPPlanBook.Tables.Add(ReportData.WPPlanBook(newwp));
                //Data For Temp LTO
                DataSet ReportDatasetReportTemp = new DataSet();
                ReportDatasetReportTemp.Tables.Add(ReportData.Temp_LTO(newwp));


                //TODO:Check Code reason
                #region CheckCode_2


                string tmp = "0";


                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day22"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day22"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day22"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day22"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day21"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day21"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day21"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day21"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day20"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day20"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day20"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day20"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day19"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day19"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day19"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day19"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day18"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day18"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day18"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day18"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day17"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day17"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day17"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day17"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day16"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day16"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day16"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day16"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day15"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day15"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day15"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day15"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day14"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day14"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day14"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day14"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day13"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day13"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day13"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day13"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day12"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day12"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day12"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day12"].ToString();



                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day11"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day11"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day11"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day11"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day10"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day10"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day10"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day10"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day9"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day9"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day9"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day9"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day8"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day8"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day8"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day8"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day7"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day7"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day7"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day7"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day6"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day6"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day6"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day6"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day5"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day5"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day5"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day5"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day4"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day4"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day4"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day4"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day3"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day3"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day3"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day3"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day2"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day2"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day2"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day2"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day1"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day1"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day1"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day1"].ToString();


                #endregion


                //Data For Employee Details
                DataSet ReportDatasetReportEmp = new DataSet();
                ReportDatasetReportEmp.Tables.Add(ReportData.EmployeeDetail(newwp));

                //TODO:Remove
                String dirqa = "http://10.1.1.113/api/resource/GetEmployeeImage?EmployeeNumber=";

                //Data For MONotes
                DataSet ReportDatasetReportMonote = new DataSet();
                ReportDatasetReportMonote.Tables.Add(ReportData.MONotes(newwp));

                //Data For Seismic the last 10 days
                DataSet ReportDatasetSeis = new DataSet();
                ReportDatasetSeis.Tables.Add(ReportData.Seismic_10Days(newwp));

                //Data For Radiation
                DataSet ReportDatasetRadiation = new DataSet();
                ReportDatasetRadiation.Tables.Add(ReportData.Radiation(newwp));


                MorningReport.RegisterData(ReportDatasetSeis);
                MorningReport.RegisterData(ReportDatasetRadiation);
                MorningReport.RegisterData(ReportDatasetReport2);
                MorningReport.RegisterData(ReportDatasetReport2NS);
                MorningReport.RegisterData(ReportDatasetReport3);
                MorningReport.RegisterData(ReportDatasetReport4);
                MorningReport.RegisterData(ReportDatasetReport5);
                MorningReport.RegisterData(ReportDatasetReport6);
                MorningReport.RegisterData(ReportDatasetReport6Pic);
                MorningReport.RegisterData(ReportDatasetReportWPD1);
                MorningReport.RegisterData(ReportDatasetReportTemp);
                MorningReport.RegisterData(ReportDatasetReportEmp);
                MorningReport.RegisterData(ReportDatasetReportWPPlanBook);
                MorningReport.RegisterData(ReportDatasetReportBar);
                MorningReport.RegisterData(ReportDatasetReportMonote);


                if (radioGroup1.SelectedIndex == 0)
                {
                    MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
                    _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManInsert.SqlStatement = "insert into [dbo].[tbl_MorningReport_Print] values('" + newwp + "', getdate(), '" + TUserInfo.UserID + "' )";
                    _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManInsert.ExecuteInstruction();
                }

                if (radioGroup1.SelectedIndex == 1)
                {
                    MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
                    _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManInsert.SqlStatement = "insert into [dbo].[tbl_MorningReport_Print_NS] values('" + newwp + "', getdate(), '" + TUserInfo.UserID + "' )";
                    _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManInsert.ExecuteInstruction();
                }


                
                    if (activity == "1")
                    {
                        if (radioGroup1.SelectedIndex == 0)
                        {
                            MorningReport.Load(TGlobalItems.ReportsFolder + "\\NewMorningDevReport.frx");
                        }

                        if (radioGroup1.SelectedIndex == 1)  
                        {
                            MorningReport.Load(TGlobalItems.ReportsFolder + "\\NewMorningDevReportNS.frx");
                        }
                    }
                    else
                    {
                        if (radioGroup1.SelectedIndex == 0)
                        {
                            MorningReport.Load(TGlobalItems.ReportsFolder + "\\NewMorningReport.frx");

                       // MorningReport.Design();
                        }

                        if (radioGroup1.SelectedIndex == 1)
                        {
                            MorningReport.Load(TGlobalItems.ReportsFolder + "\\NewMorningReportNS.frx");
                        }
                    }

                MorningReport.PrintSettings.Duplex = Duplex.Vertical;
                MorningReport.PrintSettings.ShowDialog = false;
                try
                {
MorningReport.Print();
                }
                catch
                { Exception es; }
                


                if (Seis >= 5)
                {                 
                        string formid = "";
                    DataTable Seismic = new DataTable();
                    Seismic = ReportData.SeismicChecklist();


                    if (Seismic.Rows.Count > 0)
                    {
                        formid = Seismic.Rows[0][0].ToString();

                        //if (SysSettings.Banner == "Doornkop Mine")
                        //{
                        //    formid = "1464";//Seismic Checklist
                        //}
                        //if (SysSettings.Banner == "Joel Mine")
                        //{
                        //    formid = "1482";//Seismic Checklist
                        //}
                        //if (SysSettings.Banner == "Masimong Mine")
                        //{
                        //    formid = "1700";//Seismic Checklist
                        //}


                        DevExpress.XtraSplashScreen.IOverlaySplashScreenHandle handle = DevExpress.XtraSplashScreen.SplashScreenManager.ShowOverlayForm(this);

                        try
                        {

                            MoveToProdind = true;
                            try
                            {
                                MoringRepSec = dr["MO_Section"].ToString(); ;
                            }
                            catch { MoringRepSec = ""; }
                            GetFromInfo(formid);

                            DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);


                        }
                        catch
                        {
                            DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);
                            //return;
                        }

                    }

                }

            }

            radioGroup1_SelectedIndexChanged(null, null);

            Cursor = Cursors.Default;
            printPreview = false;
        }


        private void GetFromInfo(string FormsID)
        {
            try
            {
                string GetFormInfoURL = string.Format(@"/api/Forms/GetFormInfo/");
                var client = new OCRScheduling.Models.ClientConnect();
                var param = new Dictionary<string, string>();
                param.Add("FormID", FormsID);

                var response = System.Threading.Tasks.Task.Run(() => client.GetWithParameters(GetFormInfoURL, param)).Result;

                _Forms = JsonConvert.DeserializeObject<OCRScheduling.Models.FormsAPI>(response);

                _Forms.UniqueDataStructure.TableName = _Forms.TableName;

                if (MoveToProdind == true)
                {
                    _Forms.UniqueDataStructure.Rows[0].Delete();

                    DataRow row;
                    row = _Forms.UniqueDataStructure.NewRow();

                    row["MOSectionID"] = MoringRepSec;
                    if (printPreview)
                    {
                        row["Workplaceid"] = WorkplaceID;
                        row["Description"] = newwp;
                    }
                    else
                    {
                        row["Workplaceid"] = WorkplaceID;
                        row["Description"] = MoringRepWP;
                    }
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
                MovetoProd.DoWork += MovetoProd_DoWork;
                MovetoProd.RunWorkerAsync();
                //MovetoProd.RunWorkerCompleted += MovetoProd_RunWorkerCompleted;
            }

        }

        private void MovetoProd_DoWork(object sender, DoWorkEventArgs e)
        {
            MoveToProd();
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
                        if (!printPreview)
                        {
                            jj.Controls.Add(i);
                            jj.Width = 600;
                            jj.Height = 800;

                            jj.ShowIcon = false;
                            jj.Text = "OCR - DOCUMENT PREVIEW";
                            i.CreateRibbon();
                            jj.ShowDialog();

                        }
                        else
                        {
                            PdfPrinterSettings ps = new PdfPrinterSettings();
                            i.Print(ps);
                        }
                        
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

        private void btnPrintExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnExportOnly_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void bandedGridView6_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                
                printPreview = false;
                newwp = MoringRepWP;


                if (MoringRepWP.Length < 5)
                    return;

                // do barcode
                string barcode = "";
                string barcode1 = "";
                barcode = "2";

                if (radioGroup1.SelectedIndex == 1)
                {
                    barcode = "B";

                }

                barcode1 = String.Format("{0:ddMMyyyy}", DateTime.Now);           

                Cursor = Cursors.WaitCursor;
                
                clsWorknoteData ReportData = new clsWorknoteData();
                ReportData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //Data for Barcode
                DataSet ReportDatasetReportBar = new DataSet();
                ReportDatasetReportBar.Tables.Add(ReportData.PrintOnlyWP(newwp,barcode,barcode1));
                string activity = "";
                try
                {
                    activity = ReportDatasetReportBar.Tables[0].Rows[0]["activity"].ToString();
                }
                catch { }

                //Data for DS
                DataSet ReportDatasetReport2 = new DataSet();
                ReportDatasetReport2.Tables.Add(ReportData.WorkplaceGridDS(newwp, "D"));
                //Data for NS
                DataSet ReportDatasetReport2NS = new DataSet();
                ReportDatasetReport2.Tables.Add(ReportData.WorkplaceGridDS(newwp, "N"));
                //Data for CallCentre
                DataSet ReportDatasetReport3 = new DataSet();
                ReportDatasetReport3.Tables.Add(ReportData.CallCentre(newwp));
                //Data for Seismic
                DataSet ReportDatasetReport4 = new DataSet();
                ReportDatasetReport4.Tables.Add(ReportData.Seismic(newwp));
                decimal Seis = 0;
                try
                {
                    if (ReportDatasetReport4.Tables[0].Rows[0]["Seismic"].ToString() != "")
                    {

                        Seis = Convert.ToDecimal(ReportDatasetReport4.Tables[0].Rows[0]["Seismic"].ToString());
                    }
                }
                catch
                { }
                //Data for Seismic
                DataSet ReportDatasetReport5 = new DataSet();
                ReportDatasetReport5.Tables.Add(ReportData.Incident(newwp));
                //Data For Rock Eng Walkabouts
                DataSet ReportDatasetReport6 = new DataSet();
                ReportDatasetReport6.Tables.Add(ReportData.RockEngData(newwp));

                //TODO: Check below code nessesity 
                #region CheckCode

                string aa = "";
                try
                {
                    if (ReportDatasetReport6.Tables[0].Rows.Count > 0)
                    {

                        if (ReportDatasetReport6.Tables[0].Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (ReportDatasetReport6.Tables[0].Rows[0]["picture"].ToString() != "")
                            {
                                //PicBox.Image = procs.Base64ToImage(_dbManRMDetail.ResultsDataTable.Rows[0]["picture"].ToString(), MoringRepWP);
                                //PicBox.Image.Save(Application.StartupPath + "\\" + "Neil.bmp");
                                //aa = Application.StartupPath + "\\" + "Neil.bmp";
                            }


                        }
                    }
                }
                catch
                { }
                #endregion

                //Data For Rock Eng Walkabouts
                DataSet ReportDatasetReport6Pic = new DataSet();
                ReportDatasetReport6Pic.Tables.Add(ReportData.RockEngPicture(newwp));
                //Data For License to Operate
                DataSet ReportDatasetReportWPD1 = new DataSet();
                ReportDatasetReportWPD1.Tables.Add(ReportData.LicenseToOperate(newwp));
                //Data For PlanBook
                DataSet ReportDatasetReportWPPlanBook = new DataSet();
                ReportDatasetReportWPPlanBook.Tables.Add(ReportData.WPPlanBook(newwp));
                //Data For Temp LTO
                DataSet ReportDatasetReportTemp = new DataSet();
                ReportDatasetReportTemp.Tables.Add(ReportData.Temp_LTO(newwp));


                //TODO:Check Code reason
                #region CheckCode_2


                string tmp = "0";


                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day22"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day22"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day22"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day22"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day21"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day21"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day21"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day21"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day20"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day20"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day20"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day20"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day19"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day19"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day19"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day19"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day18"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day18"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day18"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day18"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day17"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day17"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day17"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day17"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day16"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day16"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day16"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day16"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day15"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day15"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day15"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day15"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day14"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day14"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day14"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day14"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day13"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day13"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day13"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day13"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day12"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day12"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day12"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day12"].ToString();



                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day11"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day11"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day11"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day11"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day10"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day10"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day10"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day10"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day9"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day9"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day9"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day9"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day8"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day8"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day8"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day8"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day7"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day7"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day7"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day7"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day6"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day6"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day6"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day6"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day5"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day5"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day5"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day5"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day4"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day4"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day4"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day4"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day3"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day3"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day3"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day3"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day2"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day2"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day2"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day2"].ToString();

                //if (ReportDatasetReportTemp.Tables[0].Rows[1]["day1"].ToString() != "")
                //    if (ReportDatasetReportTemp.Tables[0].Rows[1]["day1"].ToString() != "OFF")
                //        if (ReportDatasetReportTemp.Tables[0].Rows[1]["day1"].ToString() != "0")
                //            tmp = ReportDatasetReportTemp.Tables[0].Rows[1]["day1"].ToString();


                #endregion


                //Data For Employee Details
                DataSet ReportDatasetReportEmp = new DataSet();
                ReportDatasetReportEmp.Tables.Add(ReportData.EmployeeDetail(newwp));

                //TODO:Remove
                String dirqa = "http://10.1.1.113/api/resource/GetEmployeeImage?EmployeeNumber=";

                //Data For MONotes
                DataSet ReportDatasetReportMonote = new DataSet();
                ReportDatasetReportMonote.Tables.Add(ReportData.MONotes(newwp));

                //Data For Seismic the last 10 days
                DataSet ReportDatasetSeis = new DataSet();
                ReportDatasetSeis.Tables.Add(ReportData.Seismic_10Days(newwp));

                //Data For Radiation
                DataSet ReportDatasetRadiation = new DataSet();
                ReportDatasetRadiation.Tables.Add(ReportData.Radiation(newwp));


                MorningReport.RegisterData(ReportDatasetSeis);
                MorningReport.RegisterData(ReportDatasetRadiation);
                MorningReport.RegisterData(ReportDatasetReport2);
                MorningReport.RegisterData(ReportDatasetReport2NS);
                MorningReport.RegisterData(ReportDatasetReport3);
                MorningReport.RegisterData(ReportDatasetReport4);
                MorningReport.RegisterData(ReportDatasetReport5);
                MorningReport.RegisterData(ReportDatasetReport6);
                MorningReport.RegisterData(ReportDatasetReport6Pic);
                MorningReport.RegisterData(ReportDatasetReportWPD1);
                MorningReport.RegisterData(ReportDatasetReportTemp);
                MorningReport.RegisterData(ReportDatasetReportEmp);
                MorningReport.RegisterData(ReportDatasetReportWPPlanBook);
                MorningReport.RegisterData(ReportDatasetReportBar);
                MorningReport.RegisterData(ReportDatasetReportMonote);


                if (radioGroup1.SelectedIndex == 0)
                {
                    MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
                    _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManInsert.SqlStatement = "insert into [dbo].[tbl_MorningReport_Print] values('" + MoringRepWP + "', getdate(), '" + TUserInfo.UserID + "' )";
                    _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManInsert.ExecuteInstruction();
                }

                if (radioGroup1.SelectedIndex == 1)
                {
                    MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
                    _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManInsert.SqlStatement = "insert into [dbo].[tbl_MorningReport_Print_NS] values('" + MoringRepWP + "', getdate(), '" + TUserInfo.UserID + "' )";
                    _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManInsert.ExecuteInstruction();
                }





                if (activity == "1")
                {
                    if (radioGroup1.SelectedIndex == 0)
                    {
                        MorningReport.Load(TGlobalItems.ReportsFolder + "\\NewMorningDevReport.frx");
                    }
                    //ns
                    if (radioGroup1.SelectedIndex == 1)
                    {
                        MorningReport.Load(TGlobalItems.ReportsFolder + "\\NewMorningDevReportNS.frx");
                    }
                }
                else
                {
                    if (radioGroup1.SelectedIndex == 0)
                    {
                        MorningReport.Load(TGlobalItems.ReportsFolder + "\\NewMorningReport.frx");
                    }
                    //ns
                    if (radioGroup1.SelectedIndex == 1)
                    {
                        MorningReport.Load(TGlobalItems.ReportsFolder + "\\NewMorningReportNS.frx");
                    }
                }


                MorningReport.Load(TGlobalItems.ReportsFolder + "\\NewMorningReport.frx");
                //MorningReport.Design();

                string dd = DateTime.Now.ToString();
                dd = dd.Substring(0, 10);

                string filename = MoringRepWP + "-" + dd;
                //filename = filename.Substring(0,

                string imageDir = " ";

                if (radioGroup1.SelectedIndex == 0)
                {
                    imageDir = ImageDir + @"\Morning" + @"\" + filename + ".pdf";
                }

                if (radioGroup1.SelectedIndex == 1)
                {
                    imageDir = ImageDir + @"\Night" + @"\" + filename + ".pdf";
                }


                //MorningReport.Design();
                PDFExport pdf = new PDFExport();
                try
                {
                    MorningReport.Prepare();
                    //MorningReport.Export(pdf, imageDir);
                }
                catch
                {
                    Exception ee;
                }

                MorningReport.Show(false); ;


                if (Seis >= Convert.ToDecimal("5"))
                {

                    DataTable Seismic = new DataTable();
                    Seismic= ReportData.SeismicChecklist();

                    string formid = "";

                    if (Seismic.Rows.Count>0)
                    {
                        formid = Seismic.Rows[0][0].ToString();
                        //if (SysSettings.Banner == "Doornkop Mine")
                        //{
                        //    formid = "1464";//Seismic Checklist
                        //}
                        //if (SysSettings.Banner == "Joel Mine")
                        //{
                        //    formid = "1482";//Seismic Checklist
                        //}
                        //if (SysSettings.Banner == "Masimong Mine")
                        //{
                        //    formid = "1700";//Seismic Checklist
                        //}

                        try
                        {
                            
                            MoveToProdind = true;
                            GetFromInfo(formid);

                        }
                        catch
                        {
                            //return;
                        }
                    }


                    
                }
                //}


                radioGroup1_SelectedIndexChanged(null, null);
                Cursor = Cursors.Default;

            }
            catch
            {
                Cursor = Cursors.Default;
            }
            finally
            {
               
            }
            
        }

        string WorkplaceID = "";
        string MOSec = "";

        private void bandedGridView6_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView view = sender as GridView;

            string test = e.Column.FieldName;
           
            try
            {
                if (e.RowHandle >= 0)
                    MoringRepWP = view.GetRowCellValue(e.RowHandle, view.Columns["description"]).ToString();
            }
            catch (Exception)
            {
                return;
            }

            int x = e.Column.AbsoluteIndex;
            x = 0;
            var rowHandleToSelect = e.RowHandle == 1 ? 2 : 1;
            this.BeginInvoke(new MethodInvoker(() => bandedGridView6.SelectRow(rowHandleToSelect)));
            Trace.WriteLine(string.Format("At end of RowClick on row {0}, selected rows: {1}", e.RowHandle, string.Join(", ", ((GridView)sender).GetSelectedRows())));

            //string aa = e.Column.Caption;
            //string Act = "";

            //MWDataManager.clsDataAccess _dbManWPSTDetail22 = new MWDataManager.clsDataAccess();
            //_dbManWPSTDetail22.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbManWPSTDetail22.SqlStatement = " exec [sp_OCR_WorkplaceDetailPrint]  '" + MoringRepWP + "' \r\n" +

            //" ";
            //_dbManWPSTDetail22.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbManWPSTDetail22.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbManWPSTDetail22.ExecuteInstruction();
          
           
        }

        private void btnMONote_Click(object sender, EventArgs e)
        {
            frmMONote frm = new frmMONote();
            frm._theSystemDBTag = theSystemDBTag;
            frm._UserCurrentInfoConnection = UserCurrentInfo.Connection;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void gridControl4_Click(object sender, EventArgs e)
        {

        }

        private void RCRockEngineering_Click(object sender, EventArgs e)
        {

        }
    }
}
