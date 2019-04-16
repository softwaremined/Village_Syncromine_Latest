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

namespace Mineware.Systems.Production.SysAdminScreens.TrammingProblems
{

    public partial class ucTrammingProblems : ucBaseUserControl
    {
        clsTrammingProblemsData _clsSectionScreenData = new clsTrammingProblemsData();
        ileTrammingProblems myEdit = new ileTrammingProblems();
        DataTable SectionScreenData;
        string TheAction = "";

        public ucTrammingProblems()
        {
            InitializeComponent();
            //loadScreenData();
        }

        public void loadScreenData()
        {
            _clsSectionScreenData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            SectionScreenData = _clsSectionScreenData.GetTrammingProblems();

            gcSectionScreen.DataSource = SectionScreenData;

            myEdit.UserCurrentInfo = this.UserCurrentInfo;
            myEdit.theSystemDBTag = this.theSystemDBTag;
            gvSectionScreen.OptionsEditForm.CustomEditFormLayout = myEdit;
        }

        public void EditProblems()
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;

            myEdit.theAction = "Edit";

            var id = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["ID"]).ToString();
            myEdit.txtID.Text = Convert.ToString(id);

            var problem = gvSectionScreen.GetRowCellValue(gvSectionScreen.FocusedRowHandle, gvSectionScreen.Columns["ProblemCode"]).ToString();
            myEdit.txtProblemCode.Text = Convert.ToString(problem);
            

            gvSectionScreen.ShowEditForm();

            //loadScreenData();
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            EditProblems();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            gvSectionScreen.AddNewRow();
            gvSectionScreen.InitNewRow += gvSectionScreen_InitNewRow;
            gvSectionScreen.ShowEditForm();

            //loadScreenData();


        }

        private void ucSectionScreen_Load(object sender, EventArgs e)
        {
            loadScreenData();

        }

        private void editProdmonth_EditValueChanged_1(object sender, EventArgs e)
        {
            loadScreenData();
        }

        void b_Click(object sender, EventArgs e)
        {
            myEdit.btnUpdate_Click(sender, e as EventArgs);
           // loadScreenData();
        }

        private void gvSectionScreen_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            //b.Click -= b_Click;
            b.Click += b_Click;
            //loadScreenData();
        }

        private void gvSectionScreen_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            myEdit.theAction = "Add";

            myEdit.txtProblemCode.Text = "";
        }

        private void gvSectionScreen_DoubleClick(object sender, EventArgs e)
        {
            EditProblems();
        }

        private void mwButton1_Click(object sender, EventArgs e)
        {
            loadScreenData();
        }
    }
}
