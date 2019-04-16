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

namespace Mineware.Systems.Production.SysAdminScreens.TrammingProblems
{
    public partial class ileTrammingProblems : EditFormUserControl
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsTrammingProblemsData _clsTrammingProblemsData = new clsTrammingProblemsData();
        public string theAction = "";
        int _hierID = 0;

        public ileTrammingProblems()
        {
            InitializeComponent();
        }

        private void ileSectionScreen_Load(object sender, EventArgs e)
        {
            _clsTrammingProblemsData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
        }

        private void cmbHierarchicalID_EditValueChanged(object sender, EventArgs e)
        {

        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            _clsTrammingProblemsData.SaveData(txtID.Text, txtProblemCode.Text, theAction);

            
        }

        private void txtProblemCode_Leave(object sender, EventArgs e)
        {
            if (txtProblemCode.Text == "")
            {
                dxProblems.SetError(txtProblemCode, "Section ID cannot be blank");
                txtProblemCode.Focus();
            }

            else
            {
                dxProblems.ClearErrors();
            }
        }
    }
}
