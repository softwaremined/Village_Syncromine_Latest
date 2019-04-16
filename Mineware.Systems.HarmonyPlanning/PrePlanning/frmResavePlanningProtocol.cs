using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using FastReport;
using System.Threading;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System.IO;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning
{
    public partial class frmResavePlanningProtocol : DevExpress.XtraEditors.XtraForm
    {
        MWDataManager.clsDataAccess _WorkplaceList = new MWDataManager.clsDataAccess();
        DataTable theProgressData = new DataTable();
        public string theSystemDBTag = "";
        public TUserCurrentInfo UserCurrentInfo;
        public frmResavePlanningProtocol()
        {
            InitializeComponent();


        }

        private void DataStructure()
        {

            theProgressData.Columns.Add("theSection", typeof(string));
            theProgressData.Columns.Add("theProdmonth", typeof(string));
            theProgressData.Columns.Add("theActivity", typeof(string));
            theProgressData.Columns.Add("theProgress", typeof(double));
            gridControl2.DataSource = theProgressData;

        }

        public void updateSections(string prodMonth,string activity)
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            if (BMEBL.get_SectionsReportsNotSaved(prodMonth, activity) == true)
            {
                //  this.tscbShaft.Items.Add("MINE");
                lcSections.DataSource = BMEBL.ResultsDataTable;
                lcSections.DisplayMember = "NAME";
                lcSections.ValueMember = "SECTIONID";

            }

        }

        private void ucProdmonthEdit1_EditValueHasChanged(object sender, EventArgs e)
        {
           
        }

        private void lcSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                
                _WorkplaceList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _WorkplaceList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement; 
                _WorkplaceList.queryReturnType = MWDataManager.ReturnType.DataTable;

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Update AT set[FileName] = Cast(pp.ProdMonth As VarChar(7)) + '_' + Cast(pp.Workplaceid As VarChar(7)) + '.fpx' ");
                sb.AppendLine(" ");
                sb.AppendLine("FROM PLANMONTH PP ");
                sb.AppendLine("LEFT JOIN PLANPROT_APPROVEDTEMPLATE AT ON ");
                sb.AppendLine("PP.Prodmonth = AT.Prodmonth and ");
                sb.AppendLine("PP.[Workplaceid] = AT.[Workplaceid] and ");
                sb.AppendLine("PP.[Activity] = AT.[Activity] ");
                sb.AppendLine("WHERE PP.Prodmonth = " + TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(mwProdmonthEdit1.EditValue)) + " and ");
                sb.AppendLine("	  Locked = 1 and");
                sb.AppendLine("	  pp.PlanCode = 'MP' and [FileName] is null");

                _WorkplaceList.SqlStatement = sb.ToString();
                _WorkplaceList.ExecuteInstruction();

                sb.Clear();
                sb.AppendLine("Insert Into PLANPROT_APPROVEDTEMPLATE ");
                sb.AppendLine("Select pp.Prodmonth, pp.WorkplaceID, pp.Sectionid_2, pp.Activity,  [FileName] = Cast(pp.ProdMonth As VarChar(7)) + '_' + Cast(pp.Workplaceid As VarChar(7)) + '.fpx', GetDate()");
                sb.AppendLine("FROM PLANMONTH PP ");
                sb.AppendLine("LEFT JOIN PLANPROT_APPROVEDTEMPLATE AT ON ");
                sb.AppendLine("PP.Prodmonth = AT.Prodmonth and ");
                sb.AppendLine("PP.[Workplaceid] = AT.[Workplaceid] and ");
                sb.AppendLine("PP.[Activity] = AT.[Activity] ");
                sb.AppendLine("WHERE PP.Prodmonth = " + TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(mwProdmonthEdit1.EditValue)) + " and ");
                sb.AppendLine("	  Locked = 1 and");
                sb.AppendLine("	  pp.PlanCode = 'MP' and [FileName] is null");

                _WorkplaceList.SqlStatement = sb.ToString();
                _WorkplaceList.ExecuteInstruction();

                //sb.Clear();
                //sb.AppendLine("update a set Locked = 1 ");
                //sb.AppendLine("from");
                //sb.AppendLine("planmonth a inner");
                //sb.AppendLine("join");
                //sb.AppendLine("(select Prodmonth,");
                //sb.AppendLine("SectionID,");
                //sb.AppendLine("WorkplaceID,");
                //sb.AppendLine("Activity,");
                //sb.AppendLine("PlanCode,");
                //sb.AppendLine("Count(Prodmonth) Shifts");
                //sb.AppendLine(" from planning");
                //sb.AppendLine(" group by Prodmonth,");
                //sb.AppendLine("SectionID,");
                //sb.AppendLine("WorkplaceID,");
                //sb.AppendLine("Activity,");
                //sb.AppendLine("PlanCode) b on");
                //sb.AppendLine("a.Prodmonth = b.Prodmonth and");
                //sb.AppendLine("a.SectionID = b.SectionID and");
                //sb.AppendLine("a.WorkplaceID = b.WorkplaceID and");
                //sb.AppendLine("a.Activity = b.Activity and");
                //sb.AppendLine("a.PlanCode = b.PlanCode");
                //sb.AppendLine("where a.Prodmonth = " + TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(mwProdmonthEdit1.EditValue)) + " and");
                //sb.AppendLine("a.Plancode = 'MP'");
                //sb.AppendLine("and b.Shifts >= 20");
                //sb.AppendLine("and a.Locked = 0");

                //_WorkplaceList.SqlStatement = sb.ToString();
                //_WorkplaceList.ExecuteInstruction();

                sb.Clear();

                sb.AppendLine("SELECT * FROM");
                sb.AppendLine("( SELECT PP.Prodmonth,  PP.[Workplaceid],  PP.[Activity], [FileName],dbo.fn_FileExists('" + TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).PlanProtSaveDir + "\\' + [FileName]) FileExsits "); 
                sb.AppendLine("FROM PLANMONTH  PP   "); 
                sb.AppendLine("LEFT JOIN PLANPROT_APPROVEDTEMPLATE AT ON   "); 
                sb.AppendLine("PP.Prodmonth = AT.Prodmonth and   "); 
                sb.AppendLine("PP.[Workplaceid] = AT.[Workplaceid] and   "); 
                sb.AppendLine("PP.[Activity] = AT.[Activity]   ");
                sb.AppendLine("WHERE PP.Prodmonth = " + TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(mwProdmonthEdit1.EditValue)) + " and ");
                sb.AppendLine("      PP.sectionID_2 = '" + lcSections.SelectedValue + "' and  ");
                sb.AppendLine("	  PP.[Activity] = " + editActivity.EditValue.ToString() + " and Locked = 1 and"); 
                sb.AppendLine("	  pp.PlanCode = 'MP' ) data   "); 
                sb.AppendLine("WHERE FileExsits = 0 ");
                _WorkplaceList.SqlStatement = sb.ToString();
                _WorkplaceList.ExecuteInstruction();

                gridControl1.DataSource = _WorkplaceList.ResultsDataTable;
            }
            catch (Exception ex)
            {

               // MessageBox.Show(ex.Message);
            }

        }
        private void resavePP(Object _theData)
        {
           // //TODO: Remember to add report
           // DataTable theData = _theData as DataTable;
           // //Mineware.Systems.Production.Reports.Planning_Protocol_Report.ucPlanningProtocolReport theReportLoader = new Mineware.Systems.Production.Reports.Planning_Protocol_Report.ucPlanningProtocolReport() { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
           // //FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
           //// FastReport.Export.Image.ImageExport export = new FastReport.Export.Image.ImageExport();
           // //  export = FastReport.Export.Pdf.PDFExport;

           // //export.JpegQuality = 5000;
           // //export.Resolution = 600;
           // int totalRows = theData.Rows.Count;
           // int doneCount = 0;

            

           // foreach (DataRow r in theData.Rows)
           // {
           //     MWDataManager.clsDataAccess _Update = new MWDataManager.clsDataAccess();
           //     _Update.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
           //     _Update.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
           //     _Update.queryReturnType = MWDataManager.ReturnType.longNumber;
           //     _Update.SqlStatement = "UPDATE planmonth SET Locked = 0 WHERE Prodmonth = '" + r["Prodmonth"] + "' and WorkPlaceID = '" + r["WorkPlaceID"] + "' and  PlanCode = 'MP' ";
           //     _Update.ExecuteInstruction();

           //     try
           //     {
           //         string activity = "";
           //         HarmonyPASReports.Planning_Protocol_Report.PlanningProtocolReportProperties reportSettings = new Reports.Planning_Protocol_Report.PlanningProtocolReportProperties();



           //         //reportSettings .Code = activity ;
           //         HarmonyPASReports.Planning_Protocol_Report.ucPlanningProtocolReport theReportLoader = new Reports.Planning_Protocol_Report.ucPlanningProtocolReport { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };


           //         //   TODO:Remember to add planning protocol report exsport
           //         // ReportsInterface.ucPlanningProtocolReport theReportLoader = new ReportsInterface.ucPlanningProtocolReport();
           //         Report approvedReport = new Report();
           //         //FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
           //         FastReport.Export.Image.ImageExport export = new FastReport.Export.Image.ImageExport();
           //         //  export = FastReport.Export.Pdf.PDFExport;
           //         reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(r["Prodmonth"].ToString()); 
           //         reportSettings.WORKPLACEID = r["WorkPlaceID"].ToString();
           //         reportSettings.SECTIONID_2 = "NONE";
           //         reportSettings.Code = r["Activity"].ToString();
           //         reportSettings.PPD = "Dynamic";
           //         reportSettings.Print = "Workplace";

           //         theReportLoader.RunCheck = false;

           //         approvedReport = theReportLoader.approveReport(reportSettings, UserCurrentInfo.Connection);

           //         // approvedReport = theReportLoader.approveReport(r["WorkPlaceID"].ToString(), Convert.ToInt32(r["Prodmonth"].ToString()), r["Activitycode"].ToString());
           //         //   approvedReport.Prepare();

           //         //  approvedReport.Export(export, String.Format("{0}\\Planning_Minutes\\{1}_{2}.pdf", TSystemSettings.ReportDir, r["Prodmonth"], r["WorkPlaceID"]));

           //         //  approvedReport.SavePrepared(String.Format("{0}\\Planning_Minutes\\{1}_{2}.fpx", TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).PlanProtLocReportDir, r["Prodmonth"], r["WorkPlaceID"]));


           //         approvedReport.SavePrepared(String.Format("{0}\\{1}_{2}.fpx", TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).PlanProtSaveDir, r["Prodmonth"], r["WorkPlaceID"]));
           //         //reportSettings .Code = activity ;

           //         string ThePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
           //         string path = string.Format(@"{0}\Success_{1}_{2}.txt", ThePath, r["Prodmonth"], r["WorkPlaceID"]);

           //         FileStream fs = File.Create(path);
           //         string tmp = r["Prodmonth"].ToString() + " - " + r["WorkPlaceID"].ToString();
           //         Byte[] info = new UTF8Encoding(true).GetBytes(tmp);
           //         fs.Write(info, 0, info.Length);

           //         tmp += "\n";
           //         tmp = System.DateTime.Now.ToShortDateString() + "   " + System.DateTime.Now.ToShortTimeString();
           //         tmp += "\n";

           //         info = new UTF8Encoding(true).GetBytes(tmp);

           //         fs.Write(info, 0, info.Length);

           //     }
           //     catch (Exception ex)
           //     {
           //         string ThePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
           //         string path = string.Format(@"{0}\Error_{1}_{2}.txt", ThePath, r["Prodmonth"], r["WorkPlaceID"]);

           //         FileStream fs = File.Create(path);
           //         string tmp = r["Prodmonth"].ToString() + " - " + r["WorkPlaceID"].ToString();
           //         Byte[] info = new UTF8Encoding(true).GetBytes(tmp);
           //         fs.Write(info, 0, info.Length);

           //         tmp = System.DateTime.Now.ToShortDateString() + "   " + System.DateTime.Now.ToShortTimeString();
           //         tmp = ex.Message;
           //         tmp += "\n";
           //         tmp += "\n";
           //         tmp += ex.StackTrace;

           //         info = new UTF8Encoding(true).GetBytes(tmp);
           //         fs.Write(info, 0, info.Length);

           //     }
           //     finally
           //     {
           //         _Update.SqlStatement = "UPDATE planmonth SET Locked = 1 WHERE Prodmonth = '" + r["Prodmonth"] + "' and WorkPlaceID = '" + r["WorkPlaceID"] + "' and  PlanCode = 'MP'";
           //         _Update.ExecuteInstruction();

           //     }
           //     doneCount++;
           //     DoProgress(Convert.ToInt16(theData.TableName), totalRows, doneCount);
           //     Application.DoEvents();
           //     //gridView2.SetRowCellValue(Convert.ToInt16(theData.TableName), "theProgress", doneCount / totalRows);
           //     //_Update.SqlStatement = "UPDATE planmonth SET Locked = 1 WHERE Prodmonth = '" + r["Prodmonth"] + "' and WorkPlaceID = '" + r["WorkPlaceID"] + "' and  PlanCode = 'MP'";
           //     //_Update.ExecuteInstruction();
           // }
        }

        public void DoProgress(int rowNumber, int total, int count)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate()
                {
                    DoProgress(rowNumber,total,count);
                });
            else
            {
                theProgressData.Rows[rowNumber].SetField("theProgress", (count / total) * 100);

            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Thread theReportThread;
            //Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            theReportThread = new Thread(new ParameterizedThreadStart(resavePP));
            theReportThread.SetApartmentState(ApartmentState.STA);
            theProgressData.Rows.Add(lcSections.SelectedValue.ToString(), TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(mwProdmonthEdit1.EditValue)), editActivity.EditValue.ToString(), 0);
            DataTable theData = _WorkplaceList.ResultsDataTable.Clone();
            for (int i = 0; i <= _WorkplaceList.ResultsDataTable.Rows.Count - 1; ++i)
            {
                theData.ImportRow(_WorkplaceList.ResultsDataTable.Rows[i]);
            }
            theData.TableName = Convert.ToString(theProgressData.Rows.Count - 1);
            
            theReportThread.Start(theData);

        }

        private void frmResavePlanningProtocol_Load(object sender, EventArgs e)
        {
            //string ThePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            //string path = string.Format(@"{0}\{1}_{2}.txt", ThePath, r["Prodmonth"], r["WorkPlaceID"]);

            //MessageBox.Show(ThePath);

            mwProdmonthEdit1.EditValue = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());

            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;

            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            if (BMEBL.get_Activity() == true)
            {
                //  this.tscbShaft.Items.Add("MINE");
                editActivity.Properties.DataSource = BMEBL.ResultsDataTable;
                editActivity.Properties.DisplayMember = "Desc";
                editActivity.Properties.ValueMember = "Code";
                editActivity.ItemIndex = 0;

            }

            updateSections(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(mwProdmonthEdit1.EditValue)), editActivity.EditValue.ToString());
            DataStructure();
        }

        private void editActivity_EditValueChanged(object sender, EventArgs e)
        {
            if (editActivity.EditValue != null)
            {
                updateSections(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(mwProdmonthEdit1.EditValue)), editActivity.EditValue.ToString());
            }
        }

        private void mwProdmonthEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (editActivity.EditValue != null)
            {
                updateSections(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(mwProdmonthEdit1.EditValue)), editActivity.EditValue.ToString());
            }
        }

        
    }
}