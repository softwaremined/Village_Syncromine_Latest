using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.GlobalConnect;
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


namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{

    public partial class SamplingChecklistAdditInfo : DevExpress.XtraEditors.XtraForm
    {

        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;
        Procedures procs = new Procedures();

        Mineware.Systems.Production.SysAdminScreens.OCRScheduling.clsOCRScheduling _clsOCRScheduling = new Mineware.Systems.Production.SysAdminScreens.OCRScheduling.clsOCRScheduling();
        private Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models.FormsAPI _Forms; //PduPlessis
        private Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models.PrintedForm _PrintedForm; //PduPlessis
        //List<ListDrop> _items = new List<ListDrop>();
        List<DataRow> list = new List<DataRow>();
        private bool MoveToProdind = false;

        public SamplingChecklistAdditInfo()
        {
            InitializeComponent();
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void SamplingChecklistAdditInfo_Load(object sender, EventArgs e)
        {
            ///Load Workplaces
            MWDataManager.clsDataAccess _dbManSec = new MWDataManager.clsDataAccess();
            _dbManSec.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManSec.SqlStatement = " select WorkplaceID, Description from Workplace  " +
                                      " order by WorkplaceID";
            _dbManSec.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManSec.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManSec.ExecuteInstruction();




            if (_dbManSec.ResultsDataTable.Rows.Count > 0)
            {
                Addit1.Properties.DataSource = _dbManSec.ResultsDataTable;
                Addit1.Properties.DisplayMember = "Description";
                Addit1.Properties.ValueMember = "Description";

                Addit2.Properties.DataSource = _dbManSec.ResultsDataTable;
                Addit2.Properties.DisplayMember = "Description";
                Addit2.Properties.ValueMember = "Description";


            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ///New
            DevExpress.XtraSplashScreen.IOverlaySplashScreenHandle handle = DevExpress.XtraSplashScreen.SplashScreenManager.ShowOverlayForm(this);

            try
            {

                MoveToProdind = true;
                if (SysSettings.Banner == "Doornkop Mine")
                {
                    GetFromInfo("1273", label5.Text, label4.Text, label6.Text);
                }
                if (SysSettings.Banner == "Masimong Mine")
                {
                    GetFromInfo("1273", label5.Text, label4.Text, label6.Text);
                }
                if (SysSettings.Banner == "Joel Mine")
                {
                    GetFromInfo("1261", label5.Text, label4.Text, label6.Text);
                }
                if (SysSettings.Banner == "Kusasalethu Mine")
                {
                    GetFromInfo("1273", label5.Text, label4.Text, label6.Text);
                }
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);


                // UpdateSchedule2(SelectedFieldNameDate, SelectedUniqueID);


            }
            catch
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);
                //MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            ///REI
            ///--All minuses
            ///
            ///REM
            ///--All minuses
            ///
            ///

        }



        private void GetFromInfo(string FormsID, string MOSecID, string WPID, string WPDesc)
        {
            try
            {
                string GetFormInfoURL = string.Format(@"/api/Forms/GetFormInfo/");
                var client = new Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models.ClientConnect();
                var param = new Dictionary<string, string>();
                param.Add("FormID", FormsID);

                var response = System.Threading.Tasks.Task.Run(() => client.GetWithParameters(GetFormInfoURL, param)).Result;

                _Forms = JsonConvert.DeserializeObject<Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models.FormsAPI>(response);

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
                MoveToProd();
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
                    }

                }

            }
            catch (Exception error)
            {

            }
        }

        //try
        //{

        //    //Write Checklist id to textfile


        //    string path = @"C:\Mineware\Syncromine\OCRForm.Txt";


        //    using (StreamWriter writetext = new StreamWriter(path))
        //    {
        //        writetext.WriteLine("1273");//Sampling Work Order
        //    }


        //    ///
        //    Process Shec = new Process();
        //    Shec.StartInfo.FileName = @"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe";
        //    Shec.StartInfo.Arguments = label4.Text+" "+Addit1.EditValue.ToString().Replace(" ","_") + " " + Addit2.EditValue.ToString().Replace(" ", "_") + " " +CommTxt.Text;
        //    Shec.StartInfo.UseShellExecute = false;
        //    Shec.StartInfo.CreateNoWindow = true;
        //    Shec.StartInfo.RedirectStandardOutput = true;
        //    Shec.StartInfo.Verb = "runas";
        //    Shec.Start();
        //    Shec.WaitForExit();
        //    // Shec.WaitForExit();




        //}
        //catch
        //{
        //    MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    return;
        //}
    
    }
}
