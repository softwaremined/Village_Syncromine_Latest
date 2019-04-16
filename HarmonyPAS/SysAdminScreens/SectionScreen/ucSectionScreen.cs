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

    public partial class ucSectionScreen : ucBaseUserControl
    {
        clsSectionScreenData _clsSectionScreenData = new clsSectionScreenData();
        ileSectionScreen myEdit = new ileSectionScreen();
        DataTable SectionScreenData;
        string TheAction = "";

        public ucSectionScreen()
        {
            InitializeComponent();
        }

        public void loadScreenData()
        {
            _clsSectionScreenData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            SectionScreenData = _clsSectionScreenData.getSectionScreenList(editProdmonth.Text);

            gcSectionScreen.DataSource = SectionScreenData;

            myEdit.UserCurrentInfo = this.UserCurrentInfo;
            myEdit.theSystemDBTag = this.theSystemDBTag;
            myEdit.Tag = editProdmonth.EditValue.ToString();
            gvSectionScreen.OptionsEditForm.CustomEditFormLayout = myEdit;
            _clsSectionScreenData.updateSecCal(editProdmonth.Text);

        }

        public void setProdMonth()
        {
            int ProdMonth;

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("SELECT DISTINCT TOP 1 [Prodmonth] ProdMonth  FROM [SECCAL] WHERE BeginDate <= CAST(GETDATE()  AS DATE) and EndDate >= CAST(GETDATE()  AS DATE)");

           _dbMan.SqlStatement =    "select case when pm1 is null then prodmonth else pm1 end as prodmonth  from (select 'a' lbl, (year(getdate())*100) + month(getdate()) prodmonth ) a \r\n" +
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

        public void EditSection()
        {
            // DevExpress.XtraGrid.Views.Grid.GridView view;

            // myEdit.theAction = "Edit";

            // var sectionID = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["SectionID"]).ToString();

            // myEdit.txtProdMonth.Text = Convert.ToDateTime(editProdmonth.EditValue).ToString("yyyyMM");

            // myEdit.txtSectionID.Text = Convert.ToString(sectionID);
            // myEdit.txtSectionID.Enabled = false;

            // var name = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["Name"]).ToString();
            // myEdit.txtName.Text = Convert.ToString(name);

            // var hierarchicalID = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["HierarchicalID"]).ToString();
            // myEdit.txtHierarchicalID.Text = Convert.ToString(hierarchicalID);


            // var occupation = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["Occupation"]).ToString();
            // myEdit.txtOccupation.Text = Convert.ToString(occupation);

            // var IndustryNo = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["IndustryNo"]).ToString();
            // //myEdit.txtHierarchicalID.Text = Convert.ToString(hierarchicalID);
            //// myEdit.cmbEmployee.EditValue = IndustryNo;

            // DataTable dtHierName = new DataTable();
            // DataTable dtEmployee = new DataTable();
            // DataTable dtReportTo = new DataTable();


            // if (Convert.ToString(sectionID) != "")
            // {
            //     dtHierName = _clsSectionScreenData.getHierName(editProdmonth.Text, sectionID);
            //     dtEmployee = _clsSectionScreenData.getEmployee(editProdmonth.Text, sectionID);

            //     dtReportTo = _clsSectionScreenData.getReportToList(editProdmonth.Text, Convert.ToInt32(hierarchicalID));

            //     if (dtHierName.Rows.Count > 0)
            //     {
            //         string _occupation = "";
            //         //myEdit.cmbHierarchicalID.Properties.DataSource = null;
            //         myEdit.cmbHierarchicalID.Properties.DataSource = dtHierName;
            //         myEdit.cmbHierarchicalID.Properties.ValueMember = "ID";
            //         myEdit.cmbHierarchicalID.Properties.DisplayMember = "Occupation";

            //         _occupation = dtHierName.Rows[0]["ID"].ToString();

            //         myEdit.cmbHierarchicalID.EditValue = _occupation.ToString();

            //     }

            //     if (dtReportTo.Rows.Count > 0)
            //     {
            //         string _reportTo = "";
            //         //myEdit.cmbReportTo.Properties.DataSource = null;
            //          myEdit.cmbReportTo.Properties.DataSource = dtReportTo;
            //         myEdit.cmbReportTo.Properties.ValueMember = "ReportToSectionID";
            //         myEdit.cmbReportTo.Properties.DisplayMember = "ReportToName";

            //         _reportTo = dtReportTo.Rows[0]["ReportToSectionID"].ToString();

            //         myEdit.cmbReportTo.EditValue = _reportTo.ToString();
            //     }

            //     if (dtEmployee.Rows.Count > 0)
            //     {
            //         string _Employee = "";
            //         //myEdit.cmbEmployee.Properties.DataSource = null;
            //         myEdit.cmbEmployee.Properties.DataSource = dtEmployee;
            //         myEdit.cmbEmployee.Properties.ValueMember = "EmployeeNo";
            //         myEdit.cmbEmployee.Properties.DisplayMember = "EmployeeName";

            //         try
            //         {
            //             _Employee = dtEmployee.Rows[0]["EmployeeNo"].ToString();
            //         }
            //         catch
            //         {
            //             _Employee = "";
            //         }

            //         myEdit.cmbEmployee.EditValue = _Employee.ToString();

            //     }
            //     //else
            //     //{
            //     //    dtEmployee = _clsSectionScreenData.getEmployeeAll(editProdmonth.Text, sectionID);
            //     //    myEdit.cmbEmployee.Properties.DataSource = dtEmployee;
            //     //    myEdit.cmbEmployee.Properties.ValueMember = "EmployeeNo";
            //     //    myEdit.cmbEmployee.Properties.DisplayMember = "EmployeeName";
            //     //    myEdit.cmbEmployee.EditValue = null;
            //     //}

            //     myEdit.cmbHierarchicalID.EditValue = hierarchicalID;

            // }

            // gvSectionScreen.ShowEditForm();


           
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            //EditSection();
            frmUpdateSection frm = new frmUpdateSection();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.theSystemDBTag = theSystemDBTag;
            frm.UserCurrentInfo = UserCurrentInfo;

            frm.theAction = "Edit";

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

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            //gvSectionScreen.AddNewRow();
            //gvSectionScreen.InitNewRow += gvSectionScreen_InitNewRow;
            //gvSectionScreen.ShowEditForm();

            frmUpdateSection frm = new frmUpdateSection();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.theSystemDBTag = theSystemDBTag;
            frm.UserCurrentInfo = UserCurrentInfo;

            frm.theAction = "Add";
            frm.txtProdMonth.Text = Convert.ToDateTime(editProdmonth.EditValue).ToString("yyyyMM");

            frm.ShowDialog();

            loadScreenData();
        }

        private void ucSectionScreen_Load(object sender, EventArgs e)
        {
            setProdMonth();
            loadScreenData();

        }

        private void editProdmonth_EditValueChanged_1(object sender, EventArgs e)
        {
            loadScreenData();
        }

        void b_Click(object sender, EventArgs e)
        {
            myEdit.btnUpdate_Click(sender, e as EventArgs);
            loadScreenData();
        }

        private void gvSectionScreen_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            //SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            ////b.Click -= b_Click;
            //b.Click += b_Click;
        }

        private void gvSectionScreen_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view;
            //myEdit.theAction = "Add";

            //myEdit.txtProdMonth.Text = editProdmonth.Text;
            //myEdit.txtSectionID.Text = "";
            //myEdit.txtSectionID.Enabled = true;
            //myEdit.txtName.Text = "";
            //myEdit.txtName.Visible = true;
            //myEdit.cmbHierarchicalID.EditValue = null;
            //myEdit.cmbReportTo.EditValue = null;

        }

        private void gvSectionScreen_DoubleClick(object sender, EventArgs e)
        {
            // EditSection();
            //return;

            frmUpdateSection frm = new frmUpdateSection();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.theSystemDBTag = theSystemDBTag;
            frm.UserCurrentInfo = UserCurrentInfo;

            frm.theAction = "Edit";

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

        private void gvSectionScreen_ShowingPopupEditForm(object sender, DevExpress.XtraGrid.Views.Grid.ShowingPopupEditFormEventArgs e)
        {
            //foreach (var button in e.EditForm.Controls.OfType<EditFormContainer>()
            //            .SelectMany(control => Enumerable.Cast<Control>(control.Controls)).OfType<PanelControl>()
            //            .SelectMany(nestedControl => Enumerable.Cast<Control>(nestedControl.Controls)).OfType<SimpleButton>())
            //{
            //    switch (button.Text)
            //    {
            //        case "Update":
            //            // Hide this button for the form will be read-only.
            //            button.Visible = false;
            //            break;

            //        case "Cancel":
            //            // Replace the Cancel button text with a localized version of the word Continue.
            //            button.Text = "Continue";
            //            break;
            //    }
            //}
        }

        private void gvSectionScreen_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void gcSectionScreen_Click(object sender, EventArgs e)
        {

        }

        private void mwButton1_Click(object sender, EventArgs e)
        {
          

            var sectionID = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["SectionID"]).ToString();
            string prodmonth = Convert.ToDateTime(editProdmonth.EditValue).ToString("yyyyMM");
            string sectionid = Convert.ToString(sectionID);          
            var name = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["Name"]).ToString();

            MWDataManager.clsDataAccess _dbMan11 = new MWDataManager.clsDataAccess();
            _dbMan11.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan11.SqlStatement = " Select * from planmonth where prodmonth = '"+ prodmonth + "' ";

            _dbMan11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan11.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan11.ExecuteInstruction();

            if (_dbMan11.ResultsDataTable.Rows.Count > 0)
            {
                MessageBox.Show("Section cant be deleted because there is planning in for '"+ prodmonth + "'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //WorkplaceIDTxt.Focus();
                return;
            }
            else
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " delete  from section where sectionid = '"+ sectionID + "'  and prodmonth = '" + prodmonth + "' ";

                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                loadScreenData();
            }




        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
