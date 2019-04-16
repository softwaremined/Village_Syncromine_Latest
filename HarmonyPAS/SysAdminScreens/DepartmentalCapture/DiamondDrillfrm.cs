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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using Mineware.Systems.Global;
using DevExpress.XtraGrid.Views.Base;

using System.Threading;
using System.Globalization;
using Newtonsoft.Json;

using Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPdfViewer;
using DevExpress.Pdf;

using System.IO;
using System.Text.RegularExpressions;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class DiamondDrillfrm : DevExpress.XtraEditors.XtraForm
    {

        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;


        Procedures procs = new Procedures();
        /// <OCR>
        Mineware.Systems.Production.SysAdminScreens.OCRScheduling.clsOCRScheduling _clsOCRScheduling = new Mineware.Systems.Production.SysAdminScreens.OCRScheduling.clsOCRScheduling();

        private Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models.FormsAPI _Forms; //PduPlessis
        private Mineware.Systems.Production.SysAdminScreens.OCRScheduling.Models.PrintedForm _PrintedForm; //PduPlessis
        //List<ListDrop> _items = new List<ListDrop>();
        List<DataRow> list = new List<DataRow>();
        private bool MoveToProdind = false;

        public DiamondDrillfrm()
        {
            InitializeComponent();
        }

        private void DiamondDrillfrm_Load(object sender, EventArgs e)
        {
            ImageDate.Text = Convert.ToInt64(DateTime.Now.ToBinary()).ToString();
            LoadBorehole();

           
        }

        void LoadBorehole()
        {
            // check if edit.
            if (Editlabel.Text == "Y")
            {

                BoreHolePnl.Visible = false;

                MWDataManager.clsDataAccess _dbManID = new MWDataManager.clsDataAccess();
                _dbManID.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

                _dbManID.SqlStatement = "Select * from tbl_GeoScience_Letter where workplace = '" + WPtextBox.Text + "' and holeno = '" + BoreholeIDtxt.Text + "' ";


                _dbManID.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManID.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManID.ExecuteInstruction();


                NoteIDtxt.Text = (Convert.ToInt32(_dbManID.ResultsDataTable.Rows[0][0].ToString())).ToString();

                PegNoCmb.Text = _dbManID.ResultsDataTable.Rows[0]["peg_number"].ToString();
                PegDistTxt.Text = _dbManID.ResultsDataTable.Rows[0]["PegDist"].ToString();
                DirectionTxt.Text = _dbManID.ResultsDataTable.Rows[0]["Direction"].ToString();
                Incltxt.Text = _dbManID.ResultsDataTable.Rows[0]["Incl"].ToString();
                EstLength.Text = _dbManID.ResultsDataTable.Rows[0]["EstLength"].ToString();


                ////get aubis link details

                //MWDataManager.clsDataAccess _dbManNew = new MWDataManager.clsDataAccess();
                //_dbManNew.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

                //_dbManNew.SqlStatement = " select * from [AUBISLINK]..[AUBIS].WPAS_DRILL_LAYOUT_VW " +
                //                            "where uwi = (select nodeid from dbo.tb_GeoScience_PlanLongTerm where workplace = '" + WPtextBox.Text + "' and holeno = '" + BoreholeIDtxt.Text + "')  ";

                //_dbManNew.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManNew.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManNew.ExecuteInstruction();

                //DataTable dt = _dbManNew.ResultsDataTable;

                //foreach (DataRow dr in dt.Rows)
                //{
                //    NDIDTxt.Text = dr["uwi"].ToString();

                //}

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

                _dbMan1.SqlStatement = "Select Note, * from  [dbo].[tbl_Geology_DiamondDrill_Note] where workplace = '" + WPtextBox.Text + "' and holeid = '" + BoreholeIDtxt.Text + "' ";


                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                DataTable dt2 = _dbMan1.ResultsDataTable;

                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        RemBox.Text = dr2["Note"].ToString();

                    }
                }



            }
            else
            {



                MWDataManager.clsDataAccess _dbManID = new MWDataManager.clsDataAccess();
                _dbManID.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

                _dbManID.SqlStatement = "Select MAX(NoteID) from tbl_GeoScience_Letter ";


                _dbManID.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManID.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManID.ExecuteInstruction();

                if (_dbManID.ResultsDataTable.Rows[0][0] == DBNull.Value)
                {
                    _dbManID.ResultsDataTable.Rows[0][0] = "0";
                }

                NoteIDtxt.Text = (Convert.ToInt32(_dbManID.ResultsDataTable.Rows[0][0].ToString()) + 1).ToString();

                ////new get aubis link details

                //MWDataManager.clsDataAccess _dbManNew = new MWDataManager.clsDataAccess();
                //_dbManNew.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

                //_dbManNew.SqlStatement = " select * from [AUBISLINK]..[AUBIS].WPAS_DRILL_LAYOUT_VW " +
                //                            "where uwi = (select nodeid from dbo.tb_GeoScience_PlanLongTerm where workplace = '" + WPtextBox.Text + "' and holeno = '" + BoreholeIDtxt.Text + "')  ";

                //_dbManNew.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManNew.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManNew.ExecuteInstruction();

                //DataTable dt = _dbManNew.ResultsDataTable;

                //foreach (DataRow dr in dt.Rows)
                //{
                //    NDIDTxt.Text = dr["uwi"].ToString();

                //    PegNoCmb.Text = dr["peg_number"].ToString();
                //    PegDistTxt.Text = dr["distance_from_peg"].ToString();
                //    DirectionTxt.Text = dr["Direction"].ToString();
                //    Incltxt.Text = dr["Inclination"].ToString();
                //    EstLength.Text = dr["Estimated_Length"].ToString();
                //}

            }


          

           

            //AddBtn.Visible = false;

        }

        private void SaveBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BoreHolePnl.Visible = true;
            //panel4.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (BoreholeIDtxt.Text == "")
            {
                MessageBox.Show("Please enter a Borehole ID", "Borehole Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (WPtextBox.Text == "")
            {
                MessageBox.Show("Please select a Workplace", "Workplace Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MWDataManager.clsDataAccess _dbManSave = new MWDataManager.clsDataAccess();
            _dbManSave.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManSave.SqlStatement = " delete from tbl_GeoScience_Letter where [NoteID] = '" + NoteIDtxt.Text + "' ";
            _dbManSave.SqlStatement = _dbManSave.SqlStatement + " insert into tb_GeoScience_Letter values('" + NoteIDtxt.Text + "', '" + String.Format("{0:yyyy-MM-dd}", Datedt.Value) + "',   ";
            _dbManSave.SqlStatement = _dbManSave.SqlStatement + " '" + machtextBox.Text + "', '" + WPtextBox.Text + "', '" + BoreholeIDtxt.Text + "' ";

            _dbManSave.SqlStatement = _dbManSave.SqlStatement + ", '" + PegNoCmb.Text + "', '" + PegDistTxt.Text + "', '" + DirectionTxt.Text + "' ";
            _dbManSave.SqlStatement = _dbManSave.SqlStatement + ", '" + Incltxt.Text + "', '" + EstLength.Text + "', '" + NDIDTxt.Text + "' ";

            _dbManSave.SqlStatement = _dbManSave.SqlStatement + "  )";
            _dbManSave.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManSave.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManSave.ExecuteInstruction();



            MWDataManager.clsDataAccess _dbManSave2 = new MWDataManager.clsDataAccess();
            _dbManSave2.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManSave2.SqlStatement = " delete from tbl_Geology_DiamondDrill_Note where [Workplace] = '" + WPtextBox.Text + "' and holeid = '" + BoreholeIDtxt.Text + "' ";
            _dbManSave2.SqlStatement = _dbManSave2.SqlStatement + " insert into tbl_Geology_DiamondDrill_Note values('" + WPtextBox.Text + "', '" + BoreholeIDtxt.Text + "', '" + RemBox.Text + "' )  ";
            _dbManSave2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManSave2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManSave2.ExecuteInstruction();
        }

        private void EditNotesBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BoreHolePnl.Visible = true;
        }

        string MOSecID = "";
        string WPID = "";
        string WPDesc = "";

        private void ChecklistBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ///New
            /// 
            string MOSecID = "Test";
            string WPID = "Test";
            string WPDesc = WPtextBox.Text;
            DevExpress.XtraSplashScreen.IOverlaySplashScreenHandle handle = DevExpress.XtraSplashScreen.SplashScreenManager.ShowOverlayForm(this);

            try
            {

                MoveToProdind = true;
                GetFromInfo("1662", MOSecID, WPID, WPDesc);
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);


                // UpdateSchedule2(SelectedFieldNameDate, SelectedUniqueID);


            }
            catch
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseOverlayForm(handle);
                //MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                        PdfPrinterSettings ps = new PdfPrinterSettings();
                        i.Print(ps);
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

    }
}
