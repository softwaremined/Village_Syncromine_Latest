using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using Mineware.Systems.Global;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Mineware.Systems.ProductionGlobal;
using System.Text;
using System.Collections;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.TrammingBoxholes
{

    public partial class ucTrammingBoxholes : ucBaseUserControl
    {
        clsTrammingBoxholesData _clsTrammingBoxholesData = new clsTrammingBoxholesData();
        ileTrammingBoxholes myEdit = new ileTrammingBoxholes();
        DataTable dtBoxholesData;
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        public ucTrammingBoxholes()
        {
            InitializeComponent();
        }

        public void loadScreenData()
        {
            _clsTrammingBoxholesData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            dtBoxholesData = _clsTrammingBoxholesData.LoadBoxholes();

            gcTrammingBoxholes.DataSource = dtBoxholesData;

            myEdit.UserCurrentInfo = this.UserCurrentInfo;
            myEdit.theSystemDBTag = this.theSystemDBTag;
            myEdit.Tag = myEdit.txtOreFlowID.Text;
            gvTrammingBoxholes.OptionsEditForm.CustomEditFormLayout = myEdit;
        }

        private void EditBoxholes()
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;

            myEdit.theAction = "Edit";

            var oreflowID = gvTrammingBoxholes.GetRowCellValue(gvTrammingBoxholes.FocusedRowHandle, gvTrammingBoxholes.Columns["OreFlowID"]).ToString();
            myEdit.txtOreFlowID.Text = Convert.ToString(oreflowID);
            myEdit.txtOreFlowID.Enabled = false;

            var name = gvTrammingBoxholes.GetRowCellValue(gvTrammingBoxholes.FocusedRowHandle, gvTrammingBoxholes.Columns["Name"]).ToString();
            myEdit.txtBoxholeDescription.Text = Convert.ToString(name);

            var distance = gvTrammingBoxholes.GetRowCellValue(gvTrammingBoxholes.FocusedRowHandle, gvTrammingBoxholes.Columns["BoxDistance"]).ToString();
            myEdit.txtDistance.Text = Convert.ToString(distance);

            var lvl = gvTrammingBoxholes.GetRowCellValue(gvTrammingBoxholes.FocusedRowHandle, gvTrammingBoxholes.Columns["lvl"]).ToString();

            if (lvl != "")
            {
                string _lvl = lvl;

                string _level = _lvl.Substring(0, _lvl.IndexOf(" "));

                myEdit.txtLevelNumber.Text = _level;
            }

            DataTable dtInAct = new DataTable();
            DataTable dtSectionID = new DataTable();
            DataTable dtReefType = new DataTable();
            DataTable dtLevel = new DataTable();
            DataTable dtMO = new DataTable();

            if (Convert.ToString(oreflowID) != "")
            {
                dtInAct = _clsTrammingBoxholesData.GetInAct(oreflowID);
                dtSectionID = _clsTrammingBoxholesData.GetSectionID(oreflowID);
                dtReefType = _clsTrammingBoxholesData.GetReefType(oreflowID);
                dtLevel = _clsTrammingBoxholesData.GetLevel(oreflowID);
                dtMO = _clsTrammingBoxholesData.LoadMONames();

                if (dtInAct.Rows.Count > 0)
                {
                    string _inAct = "";

                    myEdit.lueInactive.Properties.DataSource = dtInAct;
                    myEdit.lueInactive.Properties.ValueMember = "Inactive";
                    myEdit.lueInactive.Properties.DisplayMember = "Inactive";

                    _inAct = dtInAct.Rows[0]["Inactive"].ToString();

                    myEdit.lueInactive.EditValue = _inAct.ToString();

                }

                if (dtSectionID.Rows.Count > 0)
                {
                    string _sectionID = "";

                    myEdit.lueSectionID.Properties.DataSource = dtSectionID;
                    myEdit.lueSectionID.Properties.ValueMember = "SectionID";
                    myEdit.lueSectionID.Properties.DisplayMember = "SectionID";

                    _sectionID = dtSectionID.Rows[0]["SectionID"].ToString();

                    myEdit.lueSectionID.EditValue = _sectionID.ToString();

                }

                if (dtReefType.Rows.Count > 0)
                {
                    int _reefType = 0;

                    myEdit.lueReefType.Properties.DataSource = dtReefType;
                    myEdit.lueReefType.Properties.ValueMember = "ReefType";
                    myEdit.lueReefType.Properties.DisplayMember = "ReefType";

                    if (dtReefType.Rows[0]["ReefType"].ToString() != "")
                    {
                        _reefType = Convert.ToInt32(dtReefType.Rows[0]["ReefType"].ToString());
                    }


                    myEdit.lueReefType.EditValue = _reefType.ToString();

                }

                if (dtMO.Rows.Count > 0)
                {
                    string _MO = "";

                    myEdit.lueMineOverseer.Properties.DataSource = dtMO;
                    myEdit.lueMineOverseer.Properties.ValueMember = "Name";
                    myEdit.lueMineOverseer.Properties.DisplayMember = "Name";

                    _MO = dtMO.Rows[0]["Name"].ToString();

                    myEdit.lueMineOverseer.EditValue = _MO.ToString();

                }

            }

            gvTrammingBoxholes.ShowEditForm();
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            EditBoxholes();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            gvTrammingBoxholes.AddNewRow();
            gvTrammingBoxholes.InitNewRow += gvTrammingBoxholes_InitNewRow;
            gvTrammingBoxholes.ShowEditForm();
        }

        private void gvOrgStructure_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void editProdmonth_EditValueChanged(object sender, System.EventArgs e)
        {
            loadScreenData();
        }

        private void editProdmonth_EditValueChanged_1(object sender, EventArgs e)
        {
            loadScreenData();
        }

        private void ucTrammingBoxholes_Load(object sender, EventArgs e)
        {
            loadScreenData();
        }

        private void gvTrammingBoxholes_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            myEdit.theAction = "Add";

           
                _clsTrammingBoxholesData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                DataTable dtBHID = _clsTrammingBoxholesData.LoadBHID();
               // txtOreFlowID.Text = dtBHID.Rows[0][0].ToString();
           

            myEdit.txtOreFlowID.Text = dtBHID.Rows[0][0].ToString();
            myEdit.txtOreFlowID.Enabled = false;
            myEdit.txtBoxholeDescription.Text = "";
            myEdit.txtLevelNumber.Text = "";
            myEdit.lueReefType.EditValue = "";
            myEdit.lueInactive.EditValue = "";
            myEdit.lueSectionID.EditValue = "";
            myEdit.lueMineOverseer.EditValue = "";
            myEdit.txtDistance.EditValue = 0.00;
        }

        void b_Click(object sender, EventArgs e)
        {
            myEdit.btnUpdate_Click(sender, e as EventArgs);
            loadScreenData();
        }

        private void gvTrammingBoxholes_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            //b.Click -= b_Click;
            b.Click += b_Click;
        }

        private void gvTrammingBoxholes_DoubleClick(object sender, EventArgs e)
        {
            EditBoxholes();
        }

        private void mwButton1_Click(object sender, EventArgs e)
        {
            loadScreenData();
        }
    }
}
