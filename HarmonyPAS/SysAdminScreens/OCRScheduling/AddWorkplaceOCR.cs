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

namespace Mineware.Systems.Production.SysAdminScreens.OCRScheduling
{
    public partial class AddWorkplaceOCR : DevExpress.XtraEditors.XtraForm    
    {
        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        public string _SectionMO;
        public string _Month;
        Procedures procs = new Procedures();

        public AddWorkplaceOCR()
        {
            InitializeComponent();
        }

        private void AddWorkplaceOCR_Load(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManVampWP = new MWDataManager.clsDataAccess();
            _dbManVampWP.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManVampWP.SqlStatement = " select WorkplaceID, Description, 'N' Selected, '' SectionID from WORKPLACE " +
                                        " union Select WorkplaceID, Description, 'N' Selected, '' SectionID from [WORKPLACE_DOORNKOP_SURFACE]" +
                                        " " +
                                        "order by Description ";
            _dbManVampWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManVampWP.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManVampWP.ExecuteInstruction();


            DataTable dt1 = _dbManVampWP.ResultsDataTable;

            DataSet ds1 = new DataSet();

            if (ds1.Tables.Count > 0)
                ds1.Tables.Clear();

            ds1.Tables.Add(dt1);
            gcWorkPlaces.DataSource = ds1.Tables[0];


            gcolWPID.FieldName = "WorkplaceID";
            gcolDESCRIPTION.FieldName = "Description";
            gcolChecked.FieldName = "Selected";
           //gcolSupervisor.FieldName = "SectionID";


            LoadMinerList(_Month, _SectionMO);


            //MWDataManager.clsDataAccess _dbManSec = new MWDataManager.clsDataAccess();
            //_dbManSec.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            //_dbManSec.SqlStatement = "   Select SectionID, SectionID+': ' + Name Name from Section  \r\n" +
            //                          " where Hierarchicalid = '5'  \r\n" +
            //                            " and prodmonth = '"+_Month+"'  \r\n" +
            //                           " and SectionID like '"+ _SectionMO + "%'  \r\n" +
            //                         "order by SectionID ";
            //_dbManSec.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbManSec.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbManSec.ExecuteInstruction();


            //DataTable dt = _dbManSec.ResultsDataTable;

            //DataSet ds = new DataSet();

            //if (ds.Tables.Count > 0)
            //    ds.Tables.Clear();

            //ds.Tables.Add(dt);
            //SecLookUp.DataSource = ds.Tables[0];
            //SecLookUp.DisplayMember = "Name";
            //SecLookUp.ValueMember = "SectionID";




        }


        public void LoadMinerList(string prodMonth, string sectionidMO)
        {
            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;


            _MinerData.SqlStatement = "   Select SectionID,  Name from Section  \r\n" +
                                      " where Hierarchicalid = '5'  \r\n" +
                                        " and prodmonth = '" + _Month + "'  \r\n" +
                                       " and SectionID like '" + procs.ExtractBeforeColon(_SectionMO) + "%'  \r\n" +
                                      "  union  \r\n" +

                                     " Select SectionID, Name from SectionOther  \r\n" +
                                    "  where Hierarchicalid = '3'  \r\n" +
                                     " and prodmonth = '" + _Month + "'  \r\n" +
                                     " and SectionID like '" + procs.ExtractBeforeColon(_SectionMO) + "%'  \r\n" +
                                     "order by SectionID ";
            _MinerData.ExecuteInstruction();
            SecLookUp.DataSource = _MinerData.ResultsDataTable;
            SecLookUp.DisplayMember = "Name";
            SecLookUp.ValueMember = "SectionID";

            SecLookUp.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            string SectionID = "" ;
            string WPID = "";

            for (int i = 0; i < viewWorkplaces.DataRowCount; i++)
            {
                SectionID = viewWorkplaces.GetRowCellValue(i, "SectionID").ToString();
                WPID = viewWorkplaces.GetRowCellValue(i, "WorkplaceID").ToString();               

                if (SectionID != "")
                {
                    MWDataManager.clsDataAccess _dbManWPSTDetail = new MWDataManager.clsDataAccess();
                    _dbManWPSTDetail.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManWPSTDetail.SqlStatement = " Exec [sp_OCR_AddNewWorkplace]  '" + _Month + "','" + SectionID + "','" + WPID + "' \r\n" +
                                                    "  \r\n" +
                                                    "    ";

                    _dbManWPSTDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManWPSTDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManWPSTDetail.ExecuteInstruction();

                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "saved", Color.CornflowerBlue);

                    this.Close();
                }
            }




          

            
        }
    }
}
