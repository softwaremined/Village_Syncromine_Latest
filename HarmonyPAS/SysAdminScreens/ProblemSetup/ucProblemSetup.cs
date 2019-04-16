using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.Global;
using System.Data.SqlClient;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.ProblemSetup
{
    public partial class ucProblemSetup : Mineware.Systems.Global.ucBaseUserControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        DataTable ProblemSetupData;
        public static int tabIndex;

        clsProblemSetupData _clsProblemSetupData = new clsProblemSetupData();

        public ucProblemSetup()
        {
            InitializeComponent();
        }

        public void loadScreenData()
        {
            _clsProblemSetupData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            ProblemSetupData = _clsProblemSetupData.getProblemType(rgActivity.SelectedIndex);
            gcProblemTypes.DataSource = ProblemSetupData;

            ProblemSetupData = _clsProblemSetupData.getProblemDescription(rgActivity.SelectedIndex);
            gcProblemDescription.DataSource = ProblemSetupData;

            ProblemSetupData = _clsProblemSetupData.getProblemNote(rgActivity.SelectedIndex);
            gcProblemNotes.DataSource = ProblemSetupData;
        }

        

        private void ucCPMProblemSetup_Load(object sender, EventArgs e)
        {
            loadScreenData();
        }

        private void rgActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgActivity.SelectedIndex == 0)
            {
                loadScreenData();
            }
            else
            {
                loadScreenData();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            //string test = xtraTabControl1.Name

            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                var editForm = new ileProblemSetup();
                editForm.UserCurrentInfo = UserCurrentInfo;
                editForm.TheAction = "E";
                editForm.TheID = gvProblemTypes.GetRowCellValue(gvProblemTypes.FocusedRowHandle, gcEnquirersID).ToString();
                editForm.TheDesc = gvProblemTypes.GetRowCellValue(gvProblemTypes.FocusedRowHandle, gcDesc).ToString();
                editForm.Activity = rgActivity.SelectedIndex;
                editForm.txtEnquireID.Enabled = false;
                editForm.tabIndexForm = 0;
                editForm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                var editForm = new ileProblemSetup();
                editForm.UserCurrentInfo = UserCurrentInfo;
                editForm.TheAction = "E";
                editForm.TheID = gvProblemDescription.GetRowCellValue(gvProblemDescription.FocusedRowHandle, gcProblemID).ToString();
                editForm.TheDesc = gvProblemDescription.GetRowCellValue(gvProblemDescription.FocusedRowHandle, gcDescProblemID).ToString();
                editForm.Drillrig = Convert.ToBoolean(gvProblemDescription.GetRowCellValue(gvProblemDescription.FocusedRowHandle, gcDrillrig).ToString());
                editForm.IsCausedLostBlast = Convert.ToBoolean(gvProblemDescription.GetRowCellValue(gvProblemDescription.FocusedRowHandle, gcCausedLostBlast).ToString());
                editForm.Activity = rgActivity.SelectedIndex;
                editForm.txtProblemID.Enabled = false;
                editForm.tabIndexForm = 1;
                editForm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                var editForm = new ileProblemSetup();
                editForm.UserCurrentInfo = UserCurrentInfo;
                editForm.TheAction = "E";
                editForm.TheID = gvProblemNotes.GetRowCellValue(gvProblemNotes.FocusedRowHandle, gcNoteID).ToString();
                editForm.TheDesc = gvProblemNotes.GetRowCellValue(gvProblemNotes.FocusedRowHandle, gcDescNoteID).ToString();
                editForm.TheExplanation = gvProblemNotes.GetRowCellValue(gvProblemNotes.FocusedRowHandle, gcExplanation).ToString();
                editForm.LostBlast = Convert.ToBoolean(gvProblemNotes.GetRowCellValue(gvProblemNotes.FocusedRowHandle, gcNotLostBlast).ToString());
                editForm.Activity = rgActivity.SelectedIndex;
                editForm.txtNoteID.Enabled = false;
                editForm.tabIndexForm = 2;
                editForm.ShowDialog();
            }
            loadScreenData();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                var editForm = new ileProblemSetup();
                editForm.UserCurrentInfo = UserCurrentInfo;
                editForm.TheAction = "A";
                editForm.TheID = "";
                editForm.TheDesc = "";
                editForm.Activity = rgActivity.SelectedIndex;
                editForm.txtEnquireID.Enabled = true;
                editForm.tabIndexForm = 0;
                editForm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                var editForm = new ileProblemSetup();
                editForm.UserCurrentInfo = UserCurrentInfo;
                editForm.TheAction = "A";
                editForm.TheID = "";
                editForm.TheDesc = "";
                editForm.Drillrig = false;
                editForm.IsCausedLostBlast = false;
                editForm.Activity = rgActivity.SelectedIndex;
                editForm.txtProblemID.Enabled = true;
                editForm.tabIndexForm = 1;
                editForm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                var editForm = new ileProblemSetup();
                editForm.UserCurrentInfo = UserCurrentInfo;
                editForm.TheAction = "A";
                editForm.TheID = "";
                editForm.TheDesc = "";
                editForm.TheExplanation = "";
                editForm.LostBlast = false;
                editForm.Activity = rgActivity.SelectedIndex;
                editForm.txtNoteID.Enabled = true;
                editForm.tabIndexForm = 2;
                editForm.ShowDialog();
            }
            loadScreenData();
        }
    }
}
