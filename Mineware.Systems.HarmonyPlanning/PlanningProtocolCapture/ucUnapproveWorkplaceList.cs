using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Data;
using DevExpress.XtraPivotGrid.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors.Repository;
using FastReport;
using DevExpress.Utils;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Menu;
using DevExpress.Data.Filtering;
using DevExpress.Utils.Win;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning.PlanningProtocolCapture;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PlanningProtocolCapture
{
    public partial class ucUnapproveWorkplaceList : Form
    {
        ucPlanProtCapture frmplanprotcapture;
        ucPlanProtDataView ucplantrotdataview=new ucPlanProtDataView();
        private MWDataManager.clsDataAccess _RequestList = new MWDataManager.clsDataAccess();
        public DataTable abc = new DataTable();
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        public ucUnapproveWorkplaceList()
        {
            InitializeComponent();
          // loadTemplateData( ProdMonth,  section,  TemplateID,  WorkPlaceID,  ActivityType,  captureOption,  Readonly);
           // loadTemplateData();
        }

        //public void loadRequestList()
        //{

          
        //}

        public void loadTemplateData(int ProdMonth, string section, int TemplateID, int ActivityType)
        {
            gcRequestList.Refresh();
            gvRequestList.RefreshData();
        
           // GlobalVar.ActivityType = ActivityType;
           //// GlobalVar.captureOption = captureOption;
           // GlobalVar.ProdMonth = ProdMonth;
           //// GlobalVar.Readonly = Readonly;
           // GlobalVar.section = section;
           // GlobalVar.TemplateID = TemplateID;
          //  GlobalVar.WorkPlaceID = WorkPlaceID;

            
            MWDataManager.clsDataAccess _RequestList = new MWDataManager.clsDataAccess();
            _RequestList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _RequestList.SqlStatement = "sp_PlanProt_WorkplaceListTobeApproved";
            _RequestList.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

            //_RequestList.ConnectionString = TUserInfo.ConnectionString;
            //_RequestList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_RequestList.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_RequestList.SqlStatement = "spUNAPPROVALWorkplaceLIST '";// + TUserInfo.UserID + "'";
            //_RequestList.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
           // _RequestList.ExecuteInstruction();

            SqlParameter[] _paramCollection = 
             {
              _RequestList.CreateParameter("@ProdMonth", SqlDbType.Int, 7,    ProdMonth),
              _RequestList.CreateParameter("@SectionID ", SqlDbType.VarChar, 10, section),
              _RequestList.CreateParameter("@TemplateID", SqlDbType.Int, 0,TemplateID),
           
              _RequestList.CreateParameter("@ActivityType", SqlDbType.Int, 0, ActivityType),
            //  _RequestList.CreateParameter("@ActivityType", SqlDbType.VarChar , 50,captureOption),

              // _RequestList.CreateParameter("@WorkplaceID", SqlDbType.Int, 0,WorkPlaceID),

             };

            _RequestList.ParamCollection = _paramCollection;
            _RequestList.queryReturnType = MWDataManager.ReturnType.DataTable;
            _RequestList.ExecuteInstruction();

            if (_RequestList.ResultsDataTable.DefaultView.Count == 0)
            {
                //if(_RequestList .ResultsDataTable .Columns
                Visible = false;
               // ucplantrotdataview.DoApproveData("APPROVE");
            }
            else
            {
                gcRequestList.DataSource = _RequestList.ResultsDataTable;
                abc = _RequestList.ResultsDataTable;
                ShowDialog();
                frmLockUnlockData frmunlc = new frmLockUnlockData();
                frmunlc.Visible = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
