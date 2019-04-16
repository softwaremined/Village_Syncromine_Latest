using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using FastReport;
using System.IO;

namespace Mineware.Systems.Reports.Planning_Protocol_Report
{
    public partial class frmLockedPLanning :Mineware.Systems.Global.ucBaseUserControl
    {
        Report theReport;
        string Prodmonth;
        string SectionID_2;
        string ActivityCode;
        DataTable theFileList;
        public frmLockedPLanning()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public void loadList(DataTable theList)
        {
            try
            {
                lbWorkplace.DataSource = theList;
                lbWorkplace.ValueMember = "WorkplaceID";
                lbWorkplace.DisplayMember = "WorkplaceDesc";

            }
            catch (Exception)
            {
                
     
            }


        }

        public void setFileList(DataTable fileList)
        {
            theFileList = fileList;
        }

        private void loadReport(string theWorkplaceID)
        {

            theReport = new Report();
            DataRow[] theData = theFileList.Select("WorkplaceID = '" + theWorkplaceID + "'");

            string tempFolder = System.Environment.GetEnvironmentVariable("TEMP");
            foreach (DataRow r in theData)
            {


                if (File.Exists(String.Format("{0}\\{1}", tempFolder, r["FileName"])))
                {
                    theReport.Clear();

                    theReport.LoadPrepared(String.Format("{0}\\{1}", tempFolder, r["FileName"]));
                    //  theReport.Prepare();
                    //theReport.Load(TSystemSettings.ReportDir + "\\Planning_Minutes\\" + r["FileName"].ToString());
                    theReport.Preview = previewReport;
                    theReport.ShowPrepared();
                    previewReport.OutlineVisible = false;
                }
                else { MessageBox.Show("The planning report is not avaliable. WP : " + r["WorkplaceID"].ToString()); }
            }
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadReport(lbWorkplace.SelectedValue.ToString());
        }

        private void frmLockedPLanning_Load(object sender, EventArgs e)
        {
            //loadReport(lbWorkplace.SelectedValue.ToString());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            theReport.PrintSettings.ShowDialog = true;
            theReport.PrintPrepared();
            FastReport.PageRange printPages = theReport.PrintSettings.PageRange;
            PrintSettings PS = new PrintSettings();
            PS.PaperSource = theReport.PrintSettings.PaperSource;
            PS.PrintMode = theReport.PrintSettings.PrintMode;
            PS.Printer = theReport.PrintSettings.Printer;
            PS.PrintOnSheetHeight = theReport.PrintSettings.PrintOnSheetHeight;
            PS.PrintOnSheetWidth = theReport.PrintSettings.PrintOnSheetWidth;
            PS.PrintOnSheetRawPaperSize = theReport.PrintSettings.PrintOnSheetRawPaperSize;
            string thePrinter = theReport.PrintSettings.Printer;
            if (printPages == PageRange.All)
            {
                theReport.PrintSettings.ShowDialog = false;
                int selectedIndex = lbWorkplace.SelectedIndex;

                for (int i = 0; i < lbWorkplace.ItemCount; i++)
                {
                    if (selectedIndex != i)
                    {
                        lbWorkplace.SelectedIndex = i;
                        theReport.PrintSettings.PaperSource = PS.PaperSource;
                        theReport.PrintSettings.PrintMode = PS.PrintMode;
                        theReport.PrintSettings.PrintOnSheetHeight = PS.PrintOnSheetHeight;
                        theReport.PrintSettings.PrintOnSheetWidth = PS.PrintOnSheetWidth;
                        theReport.PrintSettings.PrintOnSheetRawPaperSize = PS.PrintOnSheetRawPaperSize;
                        theReport.PrintSettings.ShowDialog = false;
                        theReport.PrintSettings.Printer = PS.Printer;
                        theReport.PrintPrepared();
                    }
                }
            }
        }


    }
}