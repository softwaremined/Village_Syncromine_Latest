using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.Global;

namespace Mineware.Systems.Production.Controls.BookingsABS
{
    public partial class ucBookingsABSPlanStop : DevExpress.XtraEditors.XtraForm
    {

        clsBookingsABS _clsBookingsABS = new clsBookingsABS();
        //public TUserCurrentInfo CurrentUser = new TUserCurrentInfo();
        public string theConnection;
        DataTable dt_1 = new DataTable();
        DataTable dt_PlannedStoppages = new DataTable();
      
        DataTable explanation = new DataTable();
        public string TheSection = "", TheWorkpalce = "", NoteID = "", ProblemID = "", SBossNotes = "", ProblemDesc = "";

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lbNoteID.Text == "")
                MessageBox.Show("Plese select a Planned Stoppage.", "Unselected Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                this.Close();
        }

        private void gv10_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //GridView view = sender as GridView;
            //ProblemID = ExtractBeforeColon(view.FocusedValue.ToString());
            //ProblemDesc = ExtractBeforeColon(view.Columns[0].Caption.ToString());
            //string _noteid;
            //if (view.GetRowCellValue(view.FocusedRowHandle, view.Columns["PS"]) != null)
            //{
            //    var noteid = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["PS"]);
            //    lbNoteID.Text = noteid.ToString();
            //}
        }

        public string ExtractAfterColon(string TheString)
        {
            string AfterColon;

            int index = TheString.IndexOf(":"); // Kry die postion van die :

            AfterColon = TheString.Substring(index + 1); // kry alles na :

            return AfterColon;
        }

        public string ExtractBeforeColon(string TheString)
        {
            if (TheString != "")
            {
                string BeforeColon;

                int index = TheString.IndexOf(":");

                BeforeColon = TheString.Substring(0, index);

                return BeforeColon;
            }
            else
            {
                return "";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterInfo();
        }

        public void FilterInfo()
        {
           
            gv10.Columns[0].FilterInfo = new ColumnFilterInfo("[PS] LIKE '%" + txtSearch.Text + "%'");
           
        }

        private void gv10_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            ProblemID = ExtractBeforeColon(gv10.FocusedValue.ToString());
            ProblemDesc = ExtractAfterColon(gv10.FocusedValue.ToString());
            string _noteid;
            if (gv10.GetRowCellValue(gv10.FocusedRowHandle, gv10.Columns["PS"]) != null)
            {
                var noteid = gv10.GetRowCellValue(gv10.FocusedRowHandle, gv10.Columns["PS"]);
                lbNoteID.Text = noteid.ToString();
            }
        }

        DataTable dtPS = new DataTable();

        public DateTime TheDate;



        public int TheActivity = 0;

        public ucBookingsABSPlanStop()
        {
            InitializeComponent();
        }

        private void ucBookingsABSPlanStop_Load(object sender, EventArgs e)
        {
            lbSection.Text = TheSection;
            lbWorkplace.Text = TheWorkpalce;
            lbDate.Text = TheDate.ToString("yyyy-MM-dd");

           // txtSBossNotes.Text = SBossNotes;
            txtSearch.Text = NoteID;
            lbNoteID.Text = ProblemDesc;


            gc10.Visible = false;
           
            _clsBookingsABS.theData.ConnectionString = theConnection;
            dt_PlannedStoppages = _clsBookingsABS.get_PlannedStoppages(TheActivity);

            dtPS = dt_PlannedStoppages; 
            gc10Notes.Caption = "Planned Stoppages";
            gc10.DataSource = dtPS;
            gc10.Visible = true;
          
            txtSearch.Text = "";
        }
    }
}
