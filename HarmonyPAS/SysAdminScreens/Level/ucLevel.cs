using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using System.Reflection;
using DevExpress.XtraGrid.EditForm.Helpers;
using System.Diagnostics;
using DevExpress.XtraGrid.Localization;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Production.SysAdminScreens.Level
{
    public partial class ucLevel : ucBaseUserControl
    {
        clsLevel _clsLevel = new clsLevel();
        DataTable dt = new DataTable();
        ileLevel myEdit = new ileLevel();
        string TheAction = "";

        public ucLevel()
        {
            InitializeComponent();
        }

        public void loaddata()
        {
            _clsLevel.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dt = _clsLevel.loadGrid();
            gcLevel.DataSource = dt;

            myEdit.UserCurrentInfo = this.UserCurrentInfo;
            myEdit.theSystemDBTag = this.theSystemDBTag;
            //// myEdit.Tag = editProdmonth.EditValue.ToString();
            gvLevel.OptionsEditForm.CustomEditFormLayout = myEdit;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
           
            TheAction = "Edit";
          
            var Division = gvLevel.GetRowCellValue(gvLevel.FocusedRowHandle, gvLevel.Columns["Division"]).ToString();
            myEdit.cmbLevelDivison .EditValue = Convert.ToString(Division);
            myEdit.cmbLevelDivison.Enabled = false;
            var oreflowid = gvLevel.GetRowCellValue(gvLevel.FocusedRowHandle, gvLevel.Columns["OreFlowID"]).ToString();
            myEdit.OreFlowIDtxt .Text  = Convert.ToString(oreflowid);
            myEdit.OreFlowIDtxt.Enabled = false;
            var levelno = gvLevel.GetRowCellValue(gvLevel.FocusedRowHandle, gvLevel.Columns["LevelNumber"]).ToString();
            myEdit.oreLeveltxt .Text  = Convert.ToString(levelno);
            var lvl = gvLevel.GetRowCellValue(gvLevel.FocusedRowHandle, gvLevel.Columns["lvl"]).ToString();         
            DataTable edit = new DataTable();
            if (Convert.ToString(oreflowid) != "")
            {
                edit = _clsLevel.loadIfEditId(Convert.ToString(oreflowid), Convert.ToString(Division));
                if (edit.Rows.Count > 0)
                {
                    myEdit.oreLeveltxt.Text = edit.Rows[0]["levelnumber"].ToString();
                    if (edit.Rows[0]["ReefID"].ToString() == "")
                    {
                        myEdit.LVLReefTypeCb.EditValue = "";
                    }
                    else
                    {
                        myEdit.LVLReefTypeCb.EditValue = edit.Rows[0]["ReefID"].ToString();
                    }
                    myEdit.OreInActivecbx.Checked = false;

                    if (edit.Rows[0]["inactive"].ToString().Trim() == "Y")
                        myEdit.OreInActivecbx.Checked = true;

                    if (edit.Rows[0]["ParentOreFlowID"].ToString() == "")
                    {
                    }
                    else
                    {
                        myEdit.LVLOPassCb.EditValue = edit.Rows[0]["Ore"].ToString(); //SubA.Rows[0]["ParentOreFlowID"].ToString() + ":" + SubA.Rows[0]["lvlname"].ToString();
                        myEdit.orepass = edit.Rows[0]["Ore"].ToString();
                        myEdit.EditID = edit.Rows[0]["lvlname"].ToString();                   
                        if (edit.Rows[0]["lvlname"].ToString() != "")
                        {
                            myEdit.OreFlowDesctxt.Text = myEdit.oreLeveltxt.Text + " Level to " + edit.Rows[0]["lvlname"].ToString();
                        }

                    }
                  
                    if (edit.Rows[0]["HopperFactor"].ToString() != "")
                        myEdit.OreHopperFactortxt.Text = edit.Rows[0]["HopperFactor"].ToString();
                    else
                        myEdit.OreHopperFactortxt.Text = "0";

                    myEdit.OreCrossTramcbx.Checked = false;

                    if (edit.Rows[0]["CrossTram"].ToString().Trim() == "Y")
                        myEdit.OreCrossTramcbx.Checked = true;


                    myEdit.txtCostAreaNumber.Text = edit.Rows[0]["CostArea"].ToString();
                }
            }
            else
            {
                myEdit.cmbLevelDivison.Enabled = true;
                myEdit.OreFlowIDtxt.Enabled = true;
            }

            DataTable gridedit = new DataTable();
            gridedit = _clsLevel.loadGridEdit(TheAction, Convert.ToString(Division), Convert.ToString(oreflowid));
            myEdit.division  = Convert.ToString(Division);
            //myEdit.EditID = Convert.ToString(oreflowid);
         //  myEdit .orepass = Convert.ToString(lvl );
          //  myEdit.LVLOPassCb.EditValue  = Convert.ToString(lvl);
            myEdit.gcLevelEdit .DataSource  = gridedit;
            myEdit.theAction = TheAction;
            // myEdit.ShowDialog();
            //  loaddata();
            gvLevel.ShowEditForm();
        }

        private void ucLevel_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        private void gvLevel_EditFormPrepared(object sender, EditFormPreparedEventArgs e)
        {
            SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            b.Click -= b_Click;
            b.Click += b_Click;


        }

        void b_Click(object sender, EventArgs e)
        {
            // System.Diagnostics.Debugger.Break();
            myEdit.btnUpdate_Click(sender, e as EventArgs);
            loaddata();
        }

        private void gvLevel_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;

            TheAction = "Edit";

            var Division = gvLevel.GetRowCellValue(gvLevel.FocusedRowHandle, gvLevel.Columns["Division"]).ToString();
            myEdit.cmbLevelDivison.EditValue = Convert.ToString(Division);
            myEdit.cmbLevelDivison.Enabled = false;
            var oreflowid = gvLevel.GetRowCellValue(gvLevel.FocusedRowHandle, gvLevel.Columns["OreFlowID"]).ToString();
            myEdit.OreFlowIDtxt.Text = Convert.ToString(oreflowid);
            myEdit.OreFlowIDtxt.Enabled = false;
            var levelno = gvLevel.GetRowCellValue(gvLevel.FocusedRowHandle, gvLevel.Columns["LevelNumber"]).ToString();
            myEdit.oreLeveltxt.Text = Convert.ToString(levelno);
            var lvl = gvLevel.GetRowCellValue(gvLevel.FocusedRowHandle, gvLevel.Columns["lvl"]).ToString();
            //myEdit.txtCostarea.Text = Convert.ToString(CostArea);
            DataTable edit = new DataTable();
            if (Convert.ToString(oreflowid) != "")
            {
                edit = _clsLevel.loadIfEditId(Convert.ToString(oreflowid), Convert.ToString(Division));
                if (edit.Rows.Count > 0)
                {
                    myEdit.oreLeveltxt.Text = edit.Rows[0]["levelnumber"].ToString();
                    if (edit.Rows[0]["ReefID"].ToString() == "")
                    {
                        myEdit.LVLReefTypeCb.EditValue = "";
                    }
                    else
                    {
                        myEdit.LVLReefTypeCb.EditValue = edit.Rows[0]["ReefID"].ToString();
                    }
                    myEdit.OreInActivecbx.Checked = false;

                    if (edit.Rows[0]["inactive"].ToString().Trim() == "Y")
                        myEdit.OreInActivecbx.Checked = true;

                    if (edit.Rows[0]["ParentOreFlowID"].ToString() == "")
                    {
                    }
                    else
                    {
                        myEdit.LVLOPassCb.EditValue = edit.Rows[0]["Ore"].ToString(); //SubA.Rows[0]["ParentOreFlowID"].ToString() + ":" + SubA.Rows[0]["lvlname"].ToString();
                        myEdit.orepass = edit.Rows[0]["Ore"].ToString();
                        myEdit.EditID = edit.Rows[0]["lvlname"].ToString();
                        // myEdit.orepass = edit.Rows[0]["lvlname"].ToString();
                        if (edit.Rows[0]["lvlname"].ToString() != "")
                        {
                            myEdit.OreFlowDesctxt.Text = myEdit.oreLeveltxt.Text + " Level to " + edit.Rows[0]["lvlname"].ToString();
                        }

                    }
   
                    if (edit.Rows[0]["HopperFactor"].ToString() != "")
                        myEdit.OreHopperFactortxt.Text = edit.Rows[0]["HopperFactor"].ToString();
                    else
                        myEdit.OreHopperFactortxt.Text = "0";

                    myEdit.OreCrossTramcbx.Checked = false;

                    if (edit.Rows[0]["CrossTram"].ToString().Trim() == "Y")
                        myEdit.OreCrossTramcbx.Checked = true;


                    myEdit.txtCostAreaNumber.Text = edit.Rows[0]["CostArea"].ToString();
                }
            }
            else
            {
                myEdit.cmbLevelDivison.Enabled = true;
                myEdit.OreFlowIDtxt.Enabled = true;
            }

            DataTable gridedit = new DataTable();
            gridedit = _clsLevel.loadGridEdit(TheAction, Convert.ToString(Division), Convert.ToString(oreflowid));
            myEdit.division = Convert.ToString(Division);        
            myEdit.gcLevelEdit.DataSource = gridedit;
            myEdit.theAction = TheAction;         
            gvLevel.ShowEditForm();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gvLevel.AddNewRow();
            gvLevel.InitNewRow += gvLevel_InitNewRow;          
            gvLevel.ShowEditForm();
        }

        private void GvLevel_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void gvLevel_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            TheAction = "";
            myEdit.cmbLevelDivison.EditValue = "";
            myEdit.LVLReefTypeCb.EditValue = "";
            myEdit.OreFlowIDtxt.Text = "";
            myEdit.OreFlowDesctxt.Enabled = false;
            myEdit.oreLeveltxt.Text = "";
            DataTable gridedit = new DataTable();
            gridedit = _clsLevel.loadGridEdit(TheAction, "", "");
            myEdit.division = "";
            myEdit.OreInActivecbx.Checked  = false;
            myEdit.OreCrossTramcbx.Checked = false;
            myEdit.OreHopperFactortxt.Text = "";      
            myEdit.gcLevelEdit.DataSource = gridedit;
            myEdit.theAction = TheAction;
            myEdit.cmbLevelDivison.Enabled = true;
            myEdit.OreFlowIDtxt.Enabled = true;
        }
    }
}
