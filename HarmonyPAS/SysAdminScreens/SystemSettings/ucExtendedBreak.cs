using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    public partial class ucExtendedBreak : ucBaseUserControl
    {
        public ucExtendedBreak()
        {
            InitializeComponent();
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAddExtendedBreak frm = new frmAddExtendedBreak();
            frm._theSystemDBTag = theSystemDBTag;
            frm._UserCurrentInfoConnection = UserCurrentInfo.Connection;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            loadExtendedBreak();
        }

        void loadExtendedBreak()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " select * from [tbl_ExtendedBreakSetup] " +
                                    " " +
                                    "order by InitiateDate desc";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dt = _dbMan.ResultsDataTable;

            DataSet ds = new DataSet();

            ds.Tables.Clear();
            ds.Tables.Add(dt);

            gcExtendedBreaks.DataSource = ds.Tables[0];

            colSection.FieldName = "Section";
            colInitiateDate.FieldName = "InitiateDate";
            colDaysBefore.FieldName = "DaysBefore";
            colStartDate.FieldName = "StartDate";
            colEndDate.FieldName = "EndDate";
            colDescription.FieldName = "Description";
        }

        private void Closebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void ucExtendedBreak_Load(object sender, EventArgs e)
        {
            loadExtendedBreak();
        }
    }
}
