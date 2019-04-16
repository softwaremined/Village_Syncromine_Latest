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
using Mineware.Systems.ProductionGlobal ;
using DevExpress.XtraGrid.Columns;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SectionScreen
{
    public partial class ileSectionScreen : EditFormUserControl
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsSectionScreenData _clsSectionScreenData = new clsSectionScreenData();
        public string theAction = "";
        int _hierID = 0;

        public ileSectionScreen()
        {
            InitializeComponent();
        }

        private void ileSectionScreen_Load(object sender, EventArgs e)
        {
            _clsSectionScreenData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //LoadEmployees();            
            //txtOccupation.Text = cmbHierarchicalID.Text;
            //GetHierID();
            LoadReportToID();            
            //LoadEmployees();
        }

        private void LoadEmployees()
        {
            _clsSectionScreenData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            DataTable ReportToData = _clsSectionScreenData.getEmployees();
            cmbEmployee.Properties.DataSource = ReportToData;
            cmbEmployee.Properties.ValueMember = "employeeNo";
            cmbEmployee.Properties.DisplayMember = "EmployeeName";
        }

        private void LoadReportToID()
        {
            try
            {
                //setProdMonth();

                string Prodmonth = txtProdMonth.Text;

                int _hierid = 0;
                if (Convert.ToInt32(cmbHierarchicalID.EditValue) > 1)
                    _hierid = Convert.ToInt32(cmbHierarchicalID.EditValue) - 1;
                else
                {
                    _hierid = Convert.ToInt32(cmbHierarchicalID.EditValue);
                }

                _clsSectionScreenData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                DataTable ReportToData = _clsSectionScreenData.getReportToList(txtProdMonth.Text, _hierid);
                cmbReportTo.Properties.DataSource = ReportToData;
                cmbReportTo.Properties.ValueMember = "ReportToSectionID";
                cmbReportTo.Properties.DisplayMember = "ReportToName";


            }
            catch { }
        }

        public void GetHierID()
        {
            if (txtOccupation.Text == "Business Coach")
            {
                _hierID = 1;
            }

            else if (txtOccupation.Text == "Mine Manager")
            {
                _hierID = 2;
            }

            else if (txtOccupation.Text == "Mining Manager")
            {
                _hierID = 3;
            }

            else if (txtOccupation.Text == "Mine Overseer")
            {
                _hierID = 4;
            }

            else if (txtOccupation.Text == "Coach")
            {
                _hierID = 5;
            }

            else if (txtOccupation.Text == "Miner")
            {
                _hierID = 6;
            }

            txtHierarchicalID.Text = _hierID.ToString();
        }

        private void cmbHierarchicalID_EditValueChanged(object sender, EventArgs e)
        {
            //GetHierID();
            //LoadReportToID();

            txtOccupation.Text = cmbHierarchicalID.Text;


        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            //GetHierID();

            _clsSectionScreenData.SaveData(txtProdMonth.Text, txtSectionID.Text, txtName.Text, Convert.ToInt32(txtHierarchicalID.Text), cmbReportTo.EditValue.ToString(), theAction, cmbEmployee.EditValue.ToString());
        }

        private void cmbHierarchicalID_Enter(object sender, EventArgs e)
        {
            _clsSectionScreenData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtHierList = _clsSectionScreenData.getHierList();
            cmbHierarchicalID.Properties.DataSource = dtHierList;
            cmbHierarchicalID.Properties.ValueMember = "ID";
            cmbHierarchicalID.Properties.DisplayMember = "Occupation";
        }

        private void cmbReportTo_Enter(object sender, EventArgs e)
        {

        }

        private void txtSectionID_Leave(object sender, EventArgs e)
        {
            if (txtSectionID.Text == "")
            {
                dxSections.SetError(txtSectionID, "Section ID cannot be blank");
                txtSectionID.Focus();
            }

            else
            {
                dxSections.ClearErrors();
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                dxSections.SetError(txtName, "Name cannot be blank");
                txtName.Focus();
            }

            else
            {
                dxSections.ClearErrors();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            

        }
    }
}
