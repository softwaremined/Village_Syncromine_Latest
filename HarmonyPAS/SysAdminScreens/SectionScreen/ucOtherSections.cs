using System;
using System.Collections.Generic;
using System.Linq;
using Mineware.Systems.Global;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Mineware.Systems.ProductionGlobal;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SectionScreen
{
    public partial class ucOtherSections : ucBaseUserControl
    {

        clsSectionScreenData _clsSectionScreenData = new clsSectionScreenData();
        ileSectionScreen myEdit = new ileSectionScreen();
        DataTable SectionScreenData;
        string TheAction = "";

        public ucOtherSections()
        {
            InitializeComponent();
        }

        public void setProdMonth()
        {
            int ProdMonth;

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("SELECT DISTINCT TOP 1 [Prodmonth] ProdMonth  FROM [SECCAL] WHERE BeginDate <= CAST(GETDATE()  AS DATE) and EndDate >= CAST(GETDATE()  AS DATE)");

            _dbMan.SqlStatement = "select case when pm1 is null then prodmonth else pm1 end as prodmonth  from (select 'a' lbl, (year(getdate())*100) + month(getdate()) prodmonth ) a \r\n" +
 "left outer join   \r\n" +
 "(SELECT DISTINCT TOP 1[Prodmonth] pm1, 'a' lbl  \r\n" +

 "FROM[SECCAL] WHERE BeginDate <= CAST(GETDATE()  AS DATE) and EndDate >= CAST(GETDATE()  AS DATE)) b   \r\n" +
 "on a.lbl = b.lbl ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable SubB = _dbMan.ResultsDataTable;

            ProdMonth = Convert.ToInt32(SubB.Rows[0]["ProdMonth"].ToString());

            editProdmonth.EditValue = TProductionGlobal.ProdMonthAsDate(ProdMonth.ToString());
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mwButton1_Click(object sender, EventArgs e)
        {
            var sectionID = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["SectionID"]).ToString();
            string prodmonth = Convert.ToDateTime(editProdmonth.EditValue).ToString("yyyyMM");
            string sectionid = Convert.ToString(sectionID);
            var name = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["Name"]).ToString();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " delete  from sectionOther where sectionid = '" + sectionID + "'  and prodmonth = '" + prodmonth + "' ";

                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                loadScreenData();
            
        }

        private void gcSectionScreen_Click(object sender, EventArgs e)
        {

        }

        public void loadScreenData()
        {
            //editProdmonth.EditValue = TProductionGlobal.ProdMonthAsDate(SysSettings.ProdMonth.ToString());
            _clsSectionScreenData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            SectionScreenData = _clsSectionScreenData.getSectionScreenListOther(editProdmonth.Text);

            gcSectionScreen.DataSource = SectionScreenData;

            myEdit.UserCurrentInfo = this.UserCurrentInfo;
            myEdit.theSystemDBTag = this.theSystemDBTag;
            myEdit.Tag = editProdmonth.EditValue.ToString();
            gvSectionScreen.OptionsEditForm.CustomEditFormLayout = myEdit;
            _clsSectionScreenData.updateSecCal(editProdmonth.Text);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmUpdateSection frm = new frmUpdateSection();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.theSystemDBTag = theSystemDBTag;
            frm.UserCurrentInfo = UserCurrentInfo;

            frm.theAction = "AddOther";
            frm.txtProdMonth.Text = Convert.ToDateTime(editProdmonth.EditValue).ToString("yyyyMM");

            frm.ShowDialog();

            loadScreenData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmUpdateSection frm = new frmUpdateSection();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.theSystemDBTag = theSystemDBTag;
            frm.UserCurrentInfo = UserCurrentInfo;

            frm.theAction = "EditOther";

            var sectionID = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["SectionID"]).ToString();

            frm.txtProdMonth.Text = Convert.ToDateTime(editProdmonth.EditValue).ToString("yyyyMM");

            frm.txtSectionID.Text = Convert.ToString(sectionID);
            frm.txtSectionID.Enabled = false;

            var name = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["Name"]).ToString();
            frm.txtName.Text = Convert.ToString(name);

            //var hierarchicalID = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["HierarchicalID"]).ToString();
            //frm.txtHierarchicalID.Text = Convert.ToString(hierarchicalID);


            frm.ShowDialog();


            loadScreenData();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucOtherSections_Load(object sender, EventArgs e)
        {
            setProdMonth();
            loadScreenData();
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            loadScreenData();
        }

        private void gvSectionScreen_DoubleClick(object sender, EventArgs e)
        {
            frmUpdateSection frm = new frmUpdateSection();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.theSystemDBTag = theSystemDBTag;
            frm.UserCurrentInfo = UserCurrentInfo;

            frm.theAction = "EditOther";

            var sectionID = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["SectionID"]).ToString();

            frm.txtProdMonth.Text = Convert.ToDateTime(editProdmonth.EditValue).ToString("yyyyMM");

            frm.txtSectionID.Text = Convert.ToString(sectionID);
            frm.txtSectionID.Enabled = false;

            var name = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["Name"]).ToString();
            frm.txtName.Text = Convert.ToString(name);

            //var hierarchicalID = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["HierarchicalID"]).ToString();
            //frm.txtHierarchicalID.Text = Convert.ToString(hierarchicalID);


            frm.ShowDialog();
        }
    }
}
