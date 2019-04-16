using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using DevExpress.XtraGrid.Columns;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SectionScreen
{
    public partial class frmUpdateSection : DevExpress.XtraEditors.XtraForm
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsSectionScreenData _clsSectionScreenData = new clsSectionScreenData();
        public string theAction = "";

        Procedures procs = new Procedures();

        public frmUpdateSection()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (theAction == "EditOther" || theAction == "AddOther")
            {
                _clsSectionScreenData.SaveDataOther(txtProdMonth.Text, txtSectionID.Text, txtName.Text, Convert.ToInt32(procs.ExtractBeforeColon(cmbHierarchicalID2.Text)), procs.ExtractBeforeColon(cmbReportTo2.Text), theAction, procs.ExtractBeforeColon(cmbEmployee.Text));
            }
            else
            {
                _clsSectionScreenData.SaveData(txtProdMonth.Text, txtSectionID.Text, txtName.Text, Convert.ToInt32(procs.ExtractBeforeColon(cmbHierarchicalID2.Text)), procs.ExtractBeforeColon(cmbReportTo2.Text), theAction, procs.ExtractBeforeColon(cmbEmployee.Text));
            }

            this.Close();
        }

        private void LoadEmployees()
        {
            _clsSectionScreenData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            DataTable ReportToData = _clsSectionScreenData.getEmployees();
            cmbEmployee2.Properties.DataSource = ReportToData;
            cmbEmployee2.Properties.ValueMember = "employeeNo";
            cmbEmployee2.Properties.DisplayMember = "EmployeeName";

            foreach (DataRow dr in ReportToData.Rows)
            {
                cmbEmployee.Items.Add(dr["Emp"].ToString());
            }

            //cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            ////cmbEmployee.Aut
            //cmbEmployee.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cmbEmployee.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private void frmUpdateSection_Load(object sender, EventArgs e)
        {        


         

            LoadEmployees();
            if (theAction == "Edit" || theAction == "Add")
            {
                LoadHeirarch();
            }

            if (theAction == "EditOther"|| theAction == "AddOther")
            {
                LoadHeirarchOther();
            }




            if (theAction == "Edit")
            {

                MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();
                theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                theData.SqlStatement = " \r\n" +

                                        "    select * from(   \r\n" +
                          "   select a.*, b.EmployeeNo + ':' + b.EmployeeName Emp, convert(Varchar(5), a.HierarchicalID) + ':' + Description Heir from(   \r\n" +
                          "   select s.*, h.Description  from Section s, HIERARCH h where sectionid = '" + txtSectionID.Text + "'  and prodmonth = '" + txtProdMonth.Text + "'   \r\n" +
                          "   and s.Hierarchicalid = h.HierarchicalID)a   \r\n" +
                          "   left outer join(   \r\n" +
                           "  select * from EmployeeAll) b   \r\n" +
                           "  on a.EmployeeNo = b.EmployeeNo)a   \r\n" +
                         "  left outer join (   \r\n" +
                         "  Select SectionID, Name, SectionID+':' + Name ReportTo from Section   \r\n" +
                         "  where prodmonth = '" + txtProdMonth.Text + "')b   \r\n" +
                         "  on a.ReportToSectionid = b.SectionID   \r\n" +

                             "  ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();

                cmbEmployee2.SelectedText = theData.ResultsDataTable.Rows[0]["EmployeeNo"].ToString();
                cmbEmployee.SelectedText = theData.ResultsDataTable.Rows[0]["Emp"].ToString();
                //cmbHierarchicalID.EditValue = theData.ResultsDataTable.Rows[0]["Hierarchicalid"].ToString();

                cmbHierarchicalID2.SelectedText = theData.ResultsDataTable.Rows[0]["Heir"].ToString();

                int Heir = Convert.ToInt32(procs.ExtractBeforeColon(cmbHierarchicalID2.Text)) - 1;

                MWDataManager.clsDataAccess theData2 = new MWDataManager.clsDataAccess();
                theData2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                theData2.SqlStatement = " select SectionID , Name ReportToName, SectionID+':'+Name ReportTo \r\n " +
                      " from Section where Hierarchicalid = " + Heir + " and \r\n " +
                      " ProdMonth = '" + txtProdMonth.Text + "' order by SectionID ";
                theData2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData2.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData2.ExecuteInstruction();
                cmbReportTo2.Items.Clear();
                foreach (DataRow dr in theData2.ResultsDataTable.Rows)
                {
                    cmbReportTo2.Items.Add(dr["ReportTo"].ToString());
                }

                cmbReportTo2.SelectedText = theData.ResultsDataTable.Rows[0]["ReportTo"].ToString();
            }




            if (theAction == "EditOther")
            {

                MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();
                theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                theData.SqlStatement = " \r\n" +

                                        "    select * from(   \r\n" +
                          "   select a.*, b.EmployeeNo + ':' + b.EmployeeName Emp, convert(Varchar(5), a.HierarchicalID) + ':' + Description Heir from(   \r\n" +
                          "   select s.*, h.Description  from SectionOther s, HIERARCHOTHER h where sectionid = '" + txtSectionID.Text + "'  and prodmonth = '" + txtProdMonth.Text + "'   \r\n" +
                          "   and s.Hierarchicalid = h.HierarchicalID)a   \r\n" +
                          "   left outer join(   \r\n" +
                           "  select * from EmployeeAll) b   \r\n" +
                           "  on a.EmployeeNo = b.EmployeeNo)a   \r\n" +
                         "  left outer join (   \r\n" +
                         "  Select SectionID, Name, SectionID+':' + Name ReportTo from SectionOther   \r\n" +
                         "  where prodmonth = '" + txtProdMonth.Text + "')b   \r\n" +
                         "  on a.ReportToSectionid = b.SectionID   \r\n" +

                             "  ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();

                cmbEmployee2.SelectedText = theData.ResultsDataTable.Rows[0]["EmployeeNo"].ToString();
                cmbEmployee.SelectedText = theData.ResultsDataTable.Rows[0]["Emp"].ToString();
                //cmbHierarchicalID.EditValue = theData.ResultsDataTable.Rows[0]["Hierarchicalid"].ToString();

                cmbHierarchicalID2.SelectedText = theData.ResultsDataTable.Rows[0]["Heir"].ToString();

                int Heir = Convert.ToInt32(procs.ExtractBeforeColon(cmbHierarchicalID2.Text)) - 1;

                MWDataManager.clsDataAccess theData2 = new MWDataManager.clsDataAccess();
                theData2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                theData2.SqlStatement = " select SectionID , Name ReportToName, SectionID+':'+Name ReportTo \r\n " +
                      " from SectionOther where Hierarchicalid = " + Heir + " and \r\n " +
                      " ProdMonth = '" + txtProdMonth.Text + "' order by SectionID ";
                theData2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData2.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData2.ExecuteInstruction();
                cmbReportTo2.Items.Clear();
                foreach (DataRow dr in theData2.ResultsDataTable.Rows)
                {
                    cmbReportTo2.Items.Add(dr["ReportTo"].ToString());
                }

                cmbReportTo2.SelectedText = theData.ResultsDataTable.Rows[0]["ReportTo"].ToString();
            }








        }


        private void LoadHeirarch()
        {
            MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();
            theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            theData.SqlStatement = " select Hierarchicalid ID, Description Occupation, Convert(Varchar(3),Hierarchicalid)+':'+Description Heir " +
                     " from Hierarch order by HierarchicalID ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            //DataTable dtHierList = theData.ResultsDataTable;
            //cmbHierarchicalID.Properties.DataSource = dtHierList;
            //cmbHierarchicalID.Properties.ValueMember = "ID";
            //cmbHierarchicalID.Properties.DisplayMember = "Occupation";

            foreach (DataRow dr in theData.ResultsDataTable.Rows)
            {
                cmbHierarchicalID2.Items.Add(dr["Heir"].ToString());
            }
        }


        private void LoadHeirarchOther()
        {
            MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();
            theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            theData.SqlStatement = " select Hierarchicalid ID, Description Occupation, Convert(Varchar(3),Hierarchicalid)+':'+Description Heir " +
                     " from HierarchOther order by HierarchicalID ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            //DataTable dtHierList = theData.ResultsDataTable;
            //cmbHierarchicalID.Properties.DataSource = dtHierList;
            //cmbHierarchicalID.Properties.ValueMember = "ID";
            //cmbHierarchicalID.Properties.DisplayMember = "Occupation";

            foreach (DataRow dr in theData.ResultsDataTable.Rows)
            {
                cmbHierarchicalID2.Items.Add(dr["Heir"].ToString());
            }
        }

        private void cmbHierarchicalID_EditValueChanged(object sender, EventArgs e)
        {
           



            //cmbReportTo2.SelectedText = theData.ResultsDataTable.Rows[0]["ReportTo"].ToString();
        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();
                theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                theData.SqlStatement = "Select EmployeeNo,EmployeeName,EmployeeNo+':'+EmployeeName Emp  from employeeall where EmployeeName like '%" + searchControl1.Text + "%'";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                cmbEmployee.Items.Clear();
                foreach (DataRow dr in theData.ResultsDataTable.Rows)
                {
                    cmbEmployee.Items.Add(dr["Emp"].ToString());
                }
            }
        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbHierarchicalID2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Hier = Convert.ToInt16(procs.ExtractBeforeColon(cmbHierarchicalID2.Text)) - 1;

            if (theAction == "EditOther"|| theAction == "AddOther")
            {
                MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();
                theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                theData.SqlStatement = " select SectionID , Name ReportToName, SectionID+':'+Name ReportTo \r\n " +
                      " from SectionOther where Hierarchicalid = " + Hier + " and \r\n " +
                      " ProdMonth = '" + txtProdMonth.Text + "' order by SectionID ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                cmbReportTo2.Items.Clear();
                foreach (DataRow dr in theData.ResultsDataTable.Rows)
                {
                    cmbReportTo2.Items.Add(dr["ReportTo"].ToString());
                }
            }
            else
            {
                MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();
                theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                theData.SqlStatement = " select SectionID , Name ReportToName, SectionID+':'+Name ReportTo \r\n " +
                      " from Section where Hierarchicalid = " + Hier + " and \r\n " +
                      " ProdMonth = '" + txtProdMonth.Text + "' order by SectionID ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                cmbReportTo2.Items.Clear();
                foreach (DataRow dr in theData.ResultsDataTable.Rows)
                {
                    cmbReportTo2.Items.Add(dr["ReportTo"].ToString());
                }
            }
        }
    }
}
