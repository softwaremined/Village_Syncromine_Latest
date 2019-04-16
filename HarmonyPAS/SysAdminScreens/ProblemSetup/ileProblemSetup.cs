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
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.ProblemSetup
{
    public partial class ileProblemSetup : DevExpress.XtraEditors.XtraForm
    {
        public TUserCurrentInfo UserCurrentInfo;
        public string theSystemDBTag;
        public int tabIndexForm;
        public int Activity;
        public string TheID;
        public string TheAction;
        public string TheDesc;
        public string TheExplanation;
        public Boolean Drillrig;
        public Boolean LostBlast;
        public Boolean IsCausedLostBlast;
        DataTable DTProblemTypes;
        DataTable DTProblems;
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        clsProblemSetupData _clsProblemSetupData = new clsProblemSetupData();

        public ileProblemSetup()
        {
            InitializeComponent();
        }

        private Mineware.Systems.Production.SysAdminScreens.ProblemSetup.clsProblemSetupData GetPtoblemSetupData()
        {
            var _clsProblemSetupData = new ProblemSetup.clsProblemSetupData();
            _clsProblemSetupData.CurrentUser = UserCurrentInfo;
            _clsProblemSetupData.SystemDBTag = resHarmonyPAS.systemDBTag;
            _clsProblemSetupData.setConnectionString();
            return _clsProblemSetupData;
        }

        public void tabEditTypes()
        {
            pnlTypes.Visible = true;
            pnlDescription.Visible = false;
            pnlNotes.Visible = false;
            pnlTypes.Dock = DockStyle.Top;

            DTProblemTypes = GetPtoblemSetupData().getProblemTypeLinks(txtEnquireID.Text, Activity);
            gcProblemType.DataSource = DTProblemTypes;
            gcProblemType.Visible = true;
            gcNote.Visible = false;

            gcProblemType.Dock = DockStyle.Fill;
        }

        public void tabEditDescription()
        {
            pnlTypes.Visible = false;
            pnlDescription.Visible = true;
            pnlNotes.Visible = false;
            pnlDescription.Dock = DockStyle.Top;


            DTProblems = GetPtoblemSetupData().getProblemLinks(txtProblemID.Text, Activity);
            gcNote.DataSource = DTProblems;
            gcProblemType.Visible = false;
            gcNote.Visible = true;
            chkDrillRig.Checked = Drillrig;
            chkCausedLostBlast.Checked = IsCausedLostBlast;
            gcNote.Dock = DockStyle.Fill;
        }

        public void tabEditNotes()
        {
            pnlTypes.Visible = false;
            pnlDescription.Visible = false;
            pnlNotes.Visible = true;
            pnlNotes.Dock = DockStyle.Fill;
            chkLostBlast.Checked = LostBlast;
            gcProblemType.Visible = false;
            gcNote.Visible = false;
        }

        private void FrmEditProblemSetup_Load(object sender, EventArgs e)
        {
            if (tabIndexForm == 0)
            {
                txtEnquireID.Text = TheID;
                txtDescTypes.Text = TheDesc;
                
                tabEditTypes();
            }
            else if (tabIndexForm == 1)
            {
                txtProblemID.Text = TheID;
                txtDescriptionDesc.Text = TheDesc;
                tabEditDescription();
            }
            else if (tabIndexForm == 2)
            {
                txtNoteID.Text = TheID;
                txtDescNotes.Text = TheDesc;
                txtExplanation.Text = TheExplanation;
                this.Height = 250;
                tabEditNotes();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tabIndexForm == 0)
            {

                txtDescTypes.Focus();
                GetPtoblemSetupData().saveProblemType(txtEnquireID.Text, txtDescTypes.Text, DTProblemTypes, Activity, TheAction);
                GetPtoblemSetupData().getProblemType(Activity);

                
            }

            if (tabIndexForm == 1)
            {
                txtDescriptionDesc.Focus();
                GetPtoblemSetupData().saveProblems(txtProblemID.Text, txtDescriptionDesc.Text, chkDrillRig.Checked, chkCausedLostBlast.Checked, DTProblems, Activity, TheAction);
                GetPtoblemSetupData().getProblemDescription(Activity);
            }

            if (tabIndexForm == 2)
            {
                GetPtoblemSetupData().saveNote(txtNoteID.Text, txtDescNotes.Text, txtExplanation.Text, chkLostBlast.Checked, Activity, TheAction);
                txtDescNotes.Focus();
                GetPtoblemSetupData().getProblemNote(Activity);
            }
            Close();
        }
    }
}
