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
    public partial class ucBookingsABSProblems : DevExpress.XtraEditors.XtraForm
    {
        clsBookingsABS _clsBookingsABS = new clsBookingsABS();
        //public TUserCurrentInfo CurrentUser = new TUserCurrentInfo();
        public string theConnection;
        DataTable dt_Codes = new DataTable();
        DataTable dt_ProblemList = new DataTable();
        DataTable dt_1 = new DataTable();
        DataTable dt_2 = new DataTable();
        DataTable dt_3 = new DataTable();
        DataTable dt_4 = new DataTable();
        DataTable dt_5 = new DataTable();
        DataTable dt_6 = new DataTable();
        DataTable dt_7 = new DataTable();
        DataTable dt_8 = new DataTable();
        DataTable dt_9 = new DataTable();
        DataTable dt_10 = new DataTable();
        DataTable dt_11 = new DataTable();
        DataTable dt_12 = new DataTable();
        DataTable explanation = new DataTable();
        public string TheSection = "", TheWorkpalce = "", NoteID = "", ProblemID = "", SBossNotes = "", ProblemDesc="";

        

        public DateTime TheDate;

        

        public int TheActivity = 0;
        public ucBookingsABSProblems()
        {
            InitializeComponent();
        }
        private void ucBookingsABSProblems_Load(object sender, EventArgs e)
        {
            lbSection.Text = TheSection;
            lbWorkplace.Text = TheWorkpalce;
            lbDate.Text = TheDate.ToString("yyyy-MM-dd");

            txtSBossNotes.Text = SBossNotes;
            txtSearch.Text = NoteID;
            lbNoteID.Text = ProblemDesc;

            gc1.Visible = false;
            gc2.Visible = false;
            gc3.Visible = false;
            gc4.Visible = false;
            gc5.Visible = false;
            gc6.Visible = false;
            gc7.Visible = false;
            gc8.Visible = false;
            gc9.Visible = false;
            gc10.Visible = false;
            gc11.Visible = false;
            gc12.Visible = false;

            _clsBookingsABS.theData.ConnectionString = theConnection;
            dt_Codes = _clsBookingsABS.get_Problems_Types(TheActivity);            
            int x = 0;
            if (dt_Codes.Rows.Count == 0)
            {
                MessageBox.Show("There are no Problem Types in the Systen.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                foreach (DataRow r in dt_Codes.Rows)
                {
                    x += 1;
            
                    dt_ProblemList = _clsBookingsABS.get_Problems_Groups(TheActivity, ExtractBeforeColon(r["ProblemType"].ToString()));

                    if (x == 1)
                    {
                        dt_1 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc1Notes.Caption = r["ProblemType"].ToString();
                        gc1.DataSource = dt_1;
                        gc1.Visible = true;
                    }

                    if (x == 2)
                    {
                        dt_2 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc2Notes.Caption = r["ProblemType"].ToString();
                        gc2.DataSource = dt_2;
                        gc2.Visible = true;
                    }

                    if (x == 3)
                    {
                        dt_3 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc3Notes.Caption = r["ProblemType"].ToString();
                        gc3.DataSource = dt_3;
                        gc3.Visible = true;
                    }

                    if (x == 4)
                    {
                        dt_4 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc4Notes.Caption = r["ProblemType"].ToString();
                        gc4.DataSource = dt_4;
                        gc4.Visible = true;
                    }

                    if (x == 5)
                    {
                        dt_5 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc5Notes.Caption = r["ProblemType"].ToString();
                        gc5.DataSource = dt_5;
                        gc5.Visible = true;
                    }

                    if (x == 6)
                    {
                        dt_6 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc6Notes.Caption = r["ProblemType"].ToString();
                        gc6.DataSource = dt_6;
                        gc6.Visible = true;
                    }

                    if (x == 7)
                    {
                        dt_7 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc7Notes.Caption = r["ProblemType"].ToString();
                        gc7.DataSource = dt_7;
                        gc7.Visible = true;
                    }

                    if (x == 8)
                    {
                        dt_8 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc8Notes.Caption = r["ProblemType"].ToString();
                        gc8.DataSource = dt_8;
                        gc8.Visible = true;
                    }

                    if (x == 9)
                    {
                        dt_9 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc9Notes.Caption = r["ProblemType"].ToString();
                        gc9.DataSource = dt_9;
                        gc9.Visible = true;
                    }

                    if (x == 10)
                    {
                        dt_10 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc10Notes.Caption = r["ProblemType"].ToString();
                        gc10.DataSource = dt_10;
                        gc10.Visible = true;
                       // gv10.Appearance.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow;
                    }

                    if (x == 11)
                    {
                        dt_11 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc11Notes.Caption = r["ProblemType"].ToString();
                        gc11.DataSource = dt_11;
                        gc11.Visible = true;
                    }

                    if (x == 12)
                    {
                        dt_12 = dt_ProblemList; //_Problem_List.ResultsDataTable;
                        gc12Notes.Caption = r["ProblemType"].ToString();
                        gc12.DataSource = dt_12;
                        gc12.Visible = true;
                    }
                }
            }
            txtSearch.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lbNoteID.Text == "")
            {
                MessageBox.Show("Please select a Problem.", "Unselected Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (txtSBossNotes.Text == "")
            {
                MessageBox.Show("Please provide a note.", "No Notes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Close();
            }
        }

        private void txtSBossNotes_TextChanged(object sender, EventArgs e)
        {
            SBossNotes = txtSBossNotes.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NoteID = "";
            ProblemID = "";
            SBossNotes = "";
            this.Close();
        }

        private void gv1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            GridView view = sender as GridView;
            ProblemID = ExtractBeforeColon(view.FocusedValue.ToString());
            ProblemDesc = ExtractBeforeColon(view.Columns[0].Caption.ToString());
            string _noteid;
            if (view.GetRowCellValue(view.FocusedRowHandle, view.Columns["ProblemNote"]) != null)
            {
                var noteid = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["ProblemNote"]);
                lbNoteID.Text = noteid.ToString();
            }
        }

        public void FilterInfo()
        {
            gv1.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv2.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv3.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv4.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv5.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv6.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv7.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv8.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv9.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv10.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv11.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
            gv12.Columns[0].FilterInfo = new ColumnFilterInfo("[ProblemNote] LIKE '%" + txtSearch.Text + "%'");
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterInfo();
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
    }
}
