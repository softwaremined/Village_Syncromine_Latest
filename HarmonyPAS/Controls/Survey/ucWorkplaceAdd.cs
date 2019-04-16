using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.Survey
{
    public partial class ucWorkplaceAdd : DevExpress.XtraEditors.XtraForm
    {
        public string theConnection;
        clsSurvey _clsSurvey = new clsSurvey();

        private DataTable dtWPLookup;
        private DataTable dtWPAdd;
        private DataTable dtActivity;

        public string ProdMonth;
        public string SectionID;
        public string Activity;

        public string _WorkplaceID;
        public string _Workplace;
        public string _RW;
        public string _Dens;
        public string _SW;
        public string _CW;
        public string _EW;
        public string _EH;
        public string _cmgt;

        public ucWorkplaceAdd()
        {
            InitializeComponent();
        }

        private void ucWorkplaceAdd_Load(object sender, EventArgs e)
        {
            _clsSurvey.theData.ConnectionString = theConnection;
            dtWPLookup = _clsSurvey.get_WPSearch(ProdMonth, SectionID, Activity);
            if (dtWPLookup.Rows.Count != 0)
            {
                gcWorkplace.DataSource = dtWPLookup;
            }
        }

        private void gvWorkplace_DoubleClick(object sender, EventArgs e)
        {
            _WorkplaceID = "";
            if (gvWorkplace.GetRowCellValue(gvWorkplace.FocusedRowHandle, gvWorkplace.Columns["WorkplaceID"]) != null)
            {
                var WorkplaceID = gvWorkplace.GetRowCellValue(gvWorkplace.FocusedRowHandle, gvWorkplace.Columns["WorkplaceID"]);
                _WorkplaceID = WorkplaceID.ToString();
            }
            _Workplace = "";
            if (gvWorkplace.GetRowCellValue(gvWorkplace.FocusedRowHandle, gvWorkplace.Columns["Workplace"]) != null)
            {
                var Workplace = gvWorkplace.GetRowCellValue(gvWorkplace.FocusedRowHandle, gvWorkplace.Columns["Workplace"]);
                _Workplace = Workplace.ToString();
            }
            lbWorkplace.Text = _Workplace;
            lbWorkplaceID.Text = _WorkplaceID;
        }

        private void btnWPSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lblWorkplace.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Please select a Workplace");
            }
            else
            {
                _clsSurvey.theData.ConnectionString = theConnection;
                dtActivity = _clsSurvey.getActivty(lbWorkplaceID.Text);
                string act = dtActivity.Rows[0][0].ToString();
                dtWPAdd = _clsSurvey.Save_WPAdd(ProdMonth, SectionID, lbWorkplaceID.Text, act);

                Close();
            }
        }

        private void btnWPClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void gvWorkplace_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            _WorkplaceID = "";
            if (gvWorkplace.GetRowCellValue(gvWorkplace.FocusedRowHandle, gvWorkplace.Columns["WorkplaceID"]) != null)
            {
                var WorkplaceID = gvWorkplace.GetRowCellValue(gvWorkplace.FocusedRowHandle, gvWorkplace.Columns["WorkplaceID"]);
                _WorkplaceID = WorkplaceID.ToString();
            }
            _Workplace = "";
            if (gvWorkplace.GetRowCellValue(gvWorkplace.FocusedRowHandle, gvWorkplace.Columns["Workplace"]) != null)
            {
                var Workplace = gvWorkplace.GetRowCellValue(gvWorkplace.FocusedRowHandle, gvWorkplace.Columns["Workplace"]);
                _Workplace = Workplace.ToString();
            }
            lbWorkplace.Text = _Workplace;
            lbWorkplaceID.Text = _WorkplaceID;
        }
    }
}
