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
using Mineware.Systems.ProductionGlobal;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars;
using DevExpress.Utils;
using DevExpress.XtraNavBar;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Localization;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Production.SysAdminScreens.Workplaces
{
    public partial class ucWorkplaces : ucBaseUserControl
    {
        clsWorkplaces _clsWorkplaces = new clsWorkplaces();
        ileWorkplaceEdit myEdit = new ileWorkplaceEdit();
        DataTable WorkplaceDt = new DataTable();
        string WPID = "";
        bool theAllWP;
        int type = 0;
        public ucWorkplaces()
        {
            InitializeComponent();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void ucWorkplaces_Load(object sender, EventArgs e)
        {
            // _clsWorkplaces.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
           
            loadData();
            TUserProduction up = new TUserProduction();
            up.SetUserInfo(UserCurrentInfo.UserID, this.theSystemDBTag, this.UserCurrentInfo.Connection);
            if ((up.WPProduction == "Y") ||
                   (up.WPSurface == "Y") ||
                   (up.WPUnderGround == "Y"))
            {
                btnEdit.Enabled = true;
                btnAdd.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnAdd.Enabled = false ;
            }
            if ((up.WPClassify == "Y") ||
                (up.WPEditAttribute == "Y") ||
                (up.WPEditName == "Y"))
            {
              btnEdit  .Enabled = true;
             btnAdd    .Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnAdd.Enabled = false;
            }




            if (up.WPProduction == "N") 
            {
               
                    rdbtnWPAddStopDevNon.Properties.Items[0].Enabled = false;
                rdbtnWPAddStopDevNon.Properties.Items[1].Enabled = false;
            }
            else
            {
                rdbtnWPAddStopDevNon.Properties.Items[0].Enabled = true;
                rdbtnWPAddStopDevNon.Properties.Items[1].Enabled = true;

            }
            if (up.WPUnderGround  == "N")
            {
                rdbtnWPAddStopDevNon.Properties.Items[2].Enabled = false;
            }
            else
            {
                rdbtnWPAddStopDevNon.Properties.Items[2].Enabled = true;

            }
            if (up.WPSurface   == "N")
            {
                rdbtnWPAddSurface.Enabled = false;
            }
            else
            {
                rdbtnWPAddSurface.Enabled = true;

            }
        

        }

        public void loadData()
        {
            _clsWorkplaces.theData.ConnectionString = TConnections.GetConnectionString(this.theSystemDBTag, this.UserCurrentInfo.Connection);
            DataTable dtSurfaceWP = new DataTable();
            dtSurfaceWP = _clsWorkplaces.Load_cmbWPSearchSurfaceWP();
            if (dtSurfaceWP.Rows.Count > 0)
            {
                cmbWPSearchSurfaceWP.Properties.DataSource = dtSurfaceWP;
                cmbWPSearchSurfaceWP.Properties.DisplayMember = "TypeCode";
                cmbWPSearchSurfaceWP.Properties.ValueMember = "TypeCode";

            }
            cmbWPSearchSurfaceWP.ItemIndex = -1;
            //  cmbWPSearchSurfaceWP.Text = "-Not Applicable-";
            cmbWPSearchSurfaceWP.Properties.NullText = "-Not Applicable-";



            DataTable dtUnNonWP = new DataTable();
            dtUnNonWP = _clsWorkplaces.Load_cmbWPSearchUnNonWP();
            if (dtUnNonWP.Rows.Count > 0)
            {
                // foreach (DataRow r in dtUnNonWP.Rows)
                cmbWPSearchUnNonWP.Properties.DataSource = dtUnNonWP;
                cmbWPSearchUnNonWP.Properties.DisplayMember = "TypeCode";
                cmbWPSearchUnNonWP.Properties.ValueMember = "TypeCode";

            }
            cmbWPSearchUnNonWP.ItemIndex = -1;
            // cmbWPSearchUnNonWP.Text = "-Not Applicable-";
            cmbWPSearchUnNonWP.Properties.NullText = "-Not Applicable-";

            DataTable dtUnProdWP = new DataTable();
            dtUnProdWP = _clsWorkplaces.Load_cmbWPSearchUnProdWP();
            if (dtUnProdWP.Rows.Count > 0)
            {
                cmbWPSearchUnProdWP.Properties.DataSource = dtUnProdWP;
                cmbWPSearchUnProdWP.Properties.DisplayMember = "TypeCode";
                cmbWPSearchUnProdWP.Properties.ValueMember = "TypeCode";
            }
            cmbWPSearchUnProdWP.ItemIndex = -1;
            //  cmbWPSearchUnProdWP.Text = "-Not Applicable-";
            cmbWPSearchUnProdWP.Properties.NullText = "-Not Applicable-";



            DataTable dtWPStatus = new DataTable();
            dtWPStatus = _clsWorkplaces.Load_cmbWPSearchStatus();
            if (dtWPStatus.Rows.Count > 0)
            {
                cmbWPSearchStatus.Properties.DataSource = dtWPStatus;
                cmbWPSearchStatus.Properties.DisplayMember = "WPStatus";
                cmbWPSearchStatus.Properties.ValueMember = "WPStatus";
            }
            theAllWP = false;

            cmbWPSearchStatus.ItemIndex = 0;

            cmbWPSearchSurfaceWP.ItemIndex = 0;
            cmbWPSearchUnNonWP.ItemIndex = 0;
            cmbWPSearchUnProdWP.ItemIndex = 0;

            theAllWP = true;

            WorkplaceDt.Clear();
            WorkplaceDt = _clsWorkplaces.LoadWorkplace("");
            gcWorkplaces.DataSource = WorkplaceDt;

            rdbtnWPAddStopDevNon.SelectedIndex = -1;
            rdbtnWPAddSurface.SelectedIndex = -1;


            myEdit.UserCurrentInfo = this.UserCurrentInfo;
            myEdit.theSystemDBTag = this.theSystemDBTag;
            // myEdit.Tag = editProdmonth.EditValue.ToString();
            gvWorkplaces.OptionsEditForm.CustomEditFormLayout = myEdit;
        }

        private void cmbWPSearchStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (theAllWP == true)
            {
                if (cmbWPSearchUnProdWP.ItemIndex != -1)
                {
                    cmbWPSearchUnNonWP.ItemIndex = -1;
                    cmbWPSearchSurfaceWP.ItemIndex = -1;
                    //cmbWPSearchUnNonWP.Text   = "-Not Applicable-";
                    //cmbWPSearchSurfaceWP.Text = "-Not Applicable-";
                    cmbWPSearchUnNonWP .Properties .NullText = "-Not Applicable-";
                    cmbWPSearchSurfaceWP.Properties.NullText = "-Not Applicable-";
                    Load_Workplaces_New();
                }
            }
        }

        private void Load_Workplaces_New()
        {
            string theStatus = "";
            if (cmbWPSearchStatus.ItemIndex != -1)
            {
                if (cmbWPSearchStatus.EditValue.ToString() != "<<<ALL>>>")
                {
                    
                    if (ExtractBeforeColon(cmbWPSearchStatus.EditValue.ToString()) == "A")
                        theStatus = "A";
                    else
                        if (ExtractBeforeColon(cmbWPSearchStatus.EditValue.ToString()) == "I")
                        theStatus = "I";
                    else
                            if (ExtractBeforeColon(cmbWPSearchStatus.EditValue.ToString()) == "P")
                        theStatus = "P";
                }
            }
            string theDescription = "";
            string theWPType = "";
            if (cmbWPSearchUnProdWP.ItemIndex != -1)
            {
                if (cmbWPSearchUnProdWP.EditValue.ToString() == "<<<ALL>>>")
                    theDescription = "('D','S')";
                else
                    theWPType = ExtractBeforeColon(cmbWPSearchUnProdWP.EditValue.ToString());
            }
            if (cmbWPSearchUnNonWP.ItemIndex != -1)
            {
                if (cmbWPSearchUnNonWP.EditValue.ToString() == "<<<ALL>>>")
                    theDescription = "('OUG')";
                else
                    theWPType = ExtractBeforeColon(cmbWPSearchUnNonWP.EditValue.ToString());
            }
            if (cmbWPSearchSurfaceWP.ItemIndex != -1)
            {
                if (cmbWPSearchSurfaceWP.EditValue.ToString() == "<<<ALL>>>")
                    theDescription = "('SU')";
                else
                    theWPType = ExtractBeforeColon(cmbWPSearchSurfaceWP.EditValue.ToString());
            }
            if ((cmbWPSearchUnProdWP.EditValue.ToString() == "<<<ALL>>>") &
                (cmbWPSearchUnNonWP.EditValue.ToString() == "<<<ALL>>>") &
                (cmbWPSearchSurfaceWP.EditValue.ToString() == "<<<ALL>>>"))
            {
                theDescription = "";
                theWPType = "";
            }



            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " select distinct(w.WorkplaceID) aa, w.*, e.description Endtype, r.description Reef, o.name Level1 from workplace w \r\n " +
                                    "left outer join endtype e on  w.endtypeid = e.endtypeid \r\n " +
                                    "left outer join reef r on  w.reefid = r.reefid \r\n " +
                                    "left outer join oreflowentities o on  w.oreflowid = o.oreflowid \r\n ";
            //if (SysSettings.IsCentralized.ToString() == "1")
            //{
            //    _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.mine = o.mine2";
            //}

            if ((theDescription != "") || (theWPType != ""))
            {
                _dbMan.SqlStatement = _dbMan.SqlStatement + " left outer join WPType_Setup sc on w.TypeCode = sc.TypeCode \r\n " +
                                        " inner join Code_WPType ct on ct.TypeCode = sc.TypeCode and ct.Selected = 'Y' \r\n ";
            }

            bool theUseAnd = false;
            bool theWhere = false;
            //if (SysSettings.IsCentralized.ToString() == "1")
            //{
            //    _dbMan.SqlStatement = _dbMan.SqlStatement + " where w.DivisionCode='" + ddlWorkplaceDivision.SelectedValue.ToString() + "' ";
            //    theUseAnd = true;
            //    theWhere = true;
            //}
            if ((theWhere == false) &
                ((txtWPSearchText.Text != "") ||
                (theDescription != "") ||
                (theWPType != "") ||
                (theStatus != "")))
            {
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where ";
                theWhere = true;
            }
            if (theDescription != "")
            {
                if (theUseAnd == false)
                {
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " sc.SetupCode in " + theDescription + " \r\n ";
                    theUseAnd = true;
                }
                else
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and sc.SetupCode in " + theDescription + " \r\n ";
            }
            if (theWPType != "")
            {
                if (theUseAnd == false)
                {
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " w.TypeCode = '" + theWPType + "' \r\n ";
                    theUseAnd = true;
                }
                else
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.TypeCode = '" + theWPType + "' \r\n ";
            }
            if (theStatus != "")
            {
                if (theUseAnd == false)
                {
                    if (theStatus == "A")
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " (w.Inactive = 'N' or WPStatus = 'P') \r\n ";
                    else
                        if (theStatus == "I")
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " w.Inactive = 'Y'  \r\n ";
                    else
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " w.WPStatus = 'P'  \r\n ";
                    theUseAnd = true;
                }
                else
                {
                    if (theStatus == "A")
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " and (w.Inactive = 'N' or WPStatus = 'P') \r\n ";
                    else
                        if (theStatus == "I")
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.Inactive = 'Y' \r\n ";
                    else
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.WPStatus = 'P' \r\n ";
                    theUseAnd = true;
                }
            }

            if (txtWPSearchText.Text != "")
            {
                if (rdgrpWPSearchIDName.SelectedIndex == 0)
                {
                    if (theUseAnd == false)
                    {
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " w.WorkplaceID like '" + txtWPSearchText.Text + "%' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "%'\r\n ";
                        //  _dbMan.SqlStatement = _dbMan.SqlStatement + " w.WorkplaceID like '" + txtWPSearchText.Text + "' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "'\r\n ";
                        theUseAnd = true;
                    }
                    else
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.WorkplaceID like '" + txtWPSearchText.Text + "%' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "%'\r\n ";
                    //_dbMan.SqlStatement = _dbMan.SqlStatement + " and w.WorkplaceID like '" + txtWPSearchText.Text + "' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "'\r\n ";
                }
                if (rdgrpWPSearchIDName.SelectedIndex == 1)
                {
                    if (theUseAnd == false)
                    {
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " w.Description like '" + txtWPSearchText.Text + "%' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "%'\r\n ";
                        //  _dbMan.SqlStatement = _dbMan.SqlStatement + " w.Description like '" + txtWPSearchText.Text + "' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "'\r\n ";
                        theUseAnd = true;
                    }
                    else
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.Description like '" + txtWPSearchText.Text + "%' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "%'\r\n ";
                    // _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.Description like '" + txtWPSearchText.Text + "' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "'\r\n ";
                }
            }
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            WorkplaceDt.Clear();
            WorkplaceDt = _dbMan.ResultsDataTable;
            gcWorkplaces.DataSource = WorkplaceDt;
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

        private void cmbWPSearchUnNonWP_EditValueChanged(object sender, EventArgs e)
        {
            if (theAllWP == true)
            {
                if (cmbWPSearchUnNonWP.ItemIndex  != -1)
                {
                    cmbWPSearchUnProdWP.ItemIndex = -1;
                    cmbWPSearchSurfaceWP.ItemIndex = -1;
                    //cmbWPSearchUnProdWP.Text = "-Not Applicable-";
                    //cmbWPSearchSurfaceWP.Text = "-Not Applicable-";
                    cmbWPSearchUnNonWP.Properties.NullText = "-Not Applicable-";
                    cmbWPSearchSurfaceWP.Properties.NullText = "-Not Applicable-";
                    Load_Workplaces_New();
                }
            }
        }

        private void cmbWPSearchSurfaceWP_EditValueChanged(object sender, EventArgs e)
        {
            if (theAllWP == true)
            {
                if (cmbWPSearchSurfaceWP.ItemIndex != -1)
                {
                    cmbWPSearchUnProdWP.ItemIndex = -1;
                    cmbWPSearchUnNonWP.ItemIndex = -1;
                    cmbWPSearchUnProdWP.Text = "-Not Applicable-";
                    cmbWPSearchUnNonWP.Text = "-Not Applicable-";
                    Load_Workplaces_New();
                }
            }
        }

        private void cmbWPSearchUnProdWP_EditValueChanged(object sender, EventArgs e)
        {
            if (theAllWP == true)
            {
                if (cmbWPSearchUnProdWP.ItemIndex != -1)
                {
                    cmbWPSearchUnNonWP.ItemIndex = -1;
                    cmbWPSearchSurfaceWP.ItemIndex = -1;
                    cmbWPSearchUnNonWP.Text = "-Not Applicable-";
                    cmbWPSearchSurfaceWP.Text = "-Not Applicable-";
                    Load_Workplaces_New();
                }
            }
        }

        private void txtWPSearchText_TextChanged(object sender, EventArgs e)
        {
            Load_Workplaces_New();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //  ileWorkplaceEdit _frmWorkplaceEdit = new Workplaces.ileWorkplaceEdit();

            //myEdit.UserCurrentInfo = this.UserCurrentInfo;
            //myEdit.theSystemDBTag = this.theSystemDBTag;
            myEdit.edit = "A";
            gvWorkplaces.AddNewRow();
            if (rdbtnWPAddStopDevNon.SelectedIndex == 0)
            {
                rdbtnWPAddSurface.SelectedIndex = -1;
                type = 0;
            }
            if (rdbtnWPAddStopDevNon.SelectedIndex == 1)
            {
                rdbtnWPAddSurface.SelectedIndex = -1;
                type = 1;
            }
            if (rdbtnWPAddStopDevNon.SelectedIndex == 2)
            {
                rdbtnWPAddSurface.SelectedIndex = -1;
                type = 3;
            }
            if (rdbtnWPAddSurface .SelectedIndex == 0)
            {
                rdbtnWPAddStopDevNon.SelectedIndex = -1;
                type = 2;
            }
            myEdit.DefaultAdvEdit = "0.00";
            if (rdbtnWPAddStopDevNon.SelectedIndex == -1 && (rdbtnWPAddSurface.SelectedIndex == -1))
            {
                MessageBox.Show("Please select a Type", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                loadData();
                return;
            }
            myEdit.LoadWorkplace("A","", type);
            gvWorkplaces.ShowEditForm();
            // _frmWorkplaceEdit.ShowDialog();
           


        }

        private void rdbtnWPAddStopDevNon_MouseUp(object sender, MouseEventArgs e)
        {
            RadioGroup radioGroup = sender as RadioGroup;
            RadioGroupViewInfo viewInfo = radioGroup.GetViewInfo() as RadioGroupViewInfo;
            if (viewInfo.GetItemIndexByPoint(e.Location) == viewInfo.SelectedIndex)
            {
                radioGroup.SelectedIndex = -1;
                viewInfo.HitIndex = -1;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string wptype = "";
           
            WPID = gvWorkplaces.GetRowCellValue(gvWorkplaces.FocusedRowHandle, gvWorkplaces.Columns["WorkplaceID"]).ToString();
            String typecode = "";
            typecode = gvWorkplaces.GetRowCellValue(gvWorkplaces.FocusedRowHandle, gvWorkplaces.Columns["TypeCode"]).ToString();
          //  String DefaultAdvEdit = "";
           // DefaultAdvEdit = gvWorkplaces.GetRowCellValue(gvWorkplaces.FocusedRowHandle, gvWorkplaces.Columns["defaultadv"]).ToString();
            myEdit.DefaultAdvEdit = "0.00";
            myEdit.workplacetype = "D";
            //if (TSysSettings.IsCentralizedDatabase .ToString () == "1")
                //  myEdit.theDiv = ddlWorkplaceDivision.SelectedValue.ToString();

               // #region Workplaces

                // if (Typelabel.Text == "Workplaces")
                //  {
                myEdit.Edit = "E";


                //MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                //_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbMan.SqlStatement = " select * FROM Planning WHERE WorkplaceID = '" + WPID + "' ";
                //_dbMan.ExecuteInstruction();
                //DataTable dt = _dbMan.ResultsDataTable;
                //if (dt.Rows.Count > 0) myEdit.Locked = "1"; else myEdit.Locked = "0";

                MWDataManager.clsDataAccess _dbManA = new MWDataManager.clsDataAccess();
            _dbManA.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManA.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManA.queryReturnType = MWDataManager.ReturnType.DataTable;

                _dbManA.SqlStatement = " select * FROM WPType_Setup WHERE TypeCode = '" + typecode + "' ";
                _dbManA.ExecuteInstruction();
                DataTable dtA = _dbManA.ResultsDataTable;
            if (dtA.Rows.Count > 0)
            {
                myEdit.workplacetype = dtA.Rows[0]["SetupCode"].ToString();
                wptype = dtA.Rows[0]["SetupCode"].ToString();
                if(wptype =="S")
                {
                    type = 0;
                }
                if (wptype == "D")
                {
                    type = 1;
                }
                if (wptype == "OUG")
                {
                    type = 3;
                }
                if (wptype == "SU")
                {
                    type = 2;
                }

            }
            else
            {
                MessageBox.Show("WPType not setup.", "WP Setup not done", MessageBoxButtons.OK);
                return;
            }
            //}

            //  string wpid = "";

            // gvWorkplaces.AddNewRow();
            myEdit.LoadWorkplace("E", WPID, type);
            gvWorkplaces.ShowEditForm();
        }

        private void gvWorkplaces_EditFormPrepared(object sender, EditFormPreparedEventArgs e)
        {

            SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            b.Click -= b_Click;
            b.Click += b_Click;


        }

        void b_Click(object sender, EventArgs e)
        {
            // System.Diagnostics.Debugger.Break();
            myEdit.btnUpdate_Click(sender, e as EventArgs);
            loadData();
        }

        private void gvWorkplaces_DoubleClick(object sender, EventArgs e)
        {
            
            string wptype = "";

            WPID = gvWorkplaces.GetRowCellValue(gvWorkplaces.FocusedRowHandle, gvWorkplaces.Columns["WorkplaceID"]).ToString();
            String typecode = "";
            typecode = gvWorkplaces.GetRowCellValue(gvWorkplaces.FocusedRowHandle, gvWorkplaces.Columns["TypeCode"]).ToString();
            //  String DefaultAdvEdit = "";
            // DefaultAdvEdit = gvWorkplaces.GetRowCellValue(gvWorkplaces.FocusedRowHandle, gvWorkplaces.Columns["defaultadv"]).ToString();
            myEdit.DefaultAdvEdit = "0.00";
            myEdit.workplacetype = "D";
            //if (TSysSettings.IsCentralizedDatabase .ToString () == "1")
            //  myEdit.theDiv = ddlWorkplaceDivision.SelectedValue.ToString();

            // #region Workplaces

            // if (Typelabel.Text == "Workplaces")
            //  {
            myEdit.Edit = "E";


            //MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbMan.SqlStatement = " select * FROM Planning WHERE WorkplaceID = '" + WPID + "' ";
            //_dbMan.ExecuteInstruction();
            //DataTable dt = _dbMan.ResultsDataTable;
            //if (dt.Rows.Count > 0) myEdit.Locked = "1"; else myEdit.Locked = "0";

            MWDataManager.clsDataAccess _dbManA = new MWDataManager.clsDataAccess();
            _dbManA.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManA.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManA.queryReturnType = MWDataManager.ReturnType.DataTable;

            _dbManA.SqlStatement = " select * FROM WPType_Setup WHERE TypeCode = '" + typecode + "' ";
            _dbManA.ExecuteInstruction();
            DataTable dtA = _dbManA.ResultsDataTable;
            if (dtA.Rows.Count > 0)
            {
                myEdit.workplacetype = dtA.Rows[0]["SetupCode"].ToString();
                wptype = dtA.Rows[0]["SetupCode"].ToString();
                if (wptype == "S")
                {
                    type = 0;
                }
                if (wptype == "D")
                {
                    type = 1;
                }
                if (wptype == "OUG")
                {
                    type = 3;
                }
                if (wptype == "SU")
                {
                    type = 2;
                }

            }
            else
            {
                MessageBox.Show("WPType not setup.", "WP Setup not done", MessageBoxButtons.OK);
                return;
            }
            //}

            //  string wpid = "";

            // gvWorkplaces.AddNewRow();
            myEdit.LoadWorkplace("E", WPID, type);
            TUserProduction up = new TUserProduction();
            up.SetUserInfo(UserCurrentInfo.UserID, this.theSystemDBTag, this.UserCurrentInfo.Connection);

            object abc = gvWorkplaces.GetRowCellValue(gvWorkplaces.FocusedRowHandle, gvWorkplaces.Columns["Activity"]);
            int act = Convert.ToInt32(abc);

            if (Convert.ToInt32(act) == 0 || Convert.ToInt32(act) == 1)
            {
                if ((up.WPProduction == "Y"))

                {

                    gvWorkplaces.ShowEditForm();
                }
                else
                {
                  
                }

            }
            if (Convert.ToInt32(act) == 10)
            {
                if ((up.WPSurface == "Y"))
                {
                    gvWorkplaces.ShowEditForm();
                }
                else
                {

                }
            }



            if (Convert.ToInt32(act) == 11)
            {
                if ((up.WPUnderGround == "Y"))
                {
                    gvWorkplaces.ShowEditForm();
                }
                else
                {
                   
                }
            }
            // gvWorkplaces.ShowEditForm();
        }

        private void rdbtnWPAddSurface_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdbtnWPAddStopDevNon.SelectedIndex = -1;
            if (rdbtnWPAddSurface.SelectedIndex == 0)
            {
                WorkplaceDt.Clear();
                WorkplaceDt = _clsWorkplaces.LoadWorkplace("SU");
                gcWorkplaces.DataSource = WorkplaceDt;
             //   _clsWorkplaces.LoadWorkplace("SU");
            }
        }

        private void rdbtnWPAddStopDevNon_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdbtnWPAddSurface.SelectedIndex = -1;
            if(rdbtnWPAddStopDevNon .SelectedIndex == 0)
            {
                WorkplaceDt.Clear();
                WorkplaceDt = _clsWorkplaces.LoadWorkplace("S");
                gcWorkplaces.DataSource = WorkplaceDt;
            }
            if (rdbtnWPAddStopDevNon.SelectedIndex == 1)
            {
                WorkplaceDt.Clear();
                WorkplaceDt = _clsWorkplaces.LoadWorkplace("D");
                gcWorkplaces.DataSource = WorkplaceDt;
            }
            if (rdbtnWPAddStopDevNon.SelectedIndex == 2)
            {
                WorkplaceDt.Clear();
                WorkplaceDt = _clsWorkplaces.LoadWorkplace("OUG");
                gcWorkplaces.DataSource = WorkplaceDt;
            }
        }

        private void gvWorkplaces_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
           
        }

        private void gvWorkplaces_ShowingEditor(object sender, CancelEventArgs e)
        {
            TUserProduction up = new TUserProduction();
            up.SetUserInfo(UserCurrentInfo.UserID, this.theSystemDBTag, this.UserCurrentInfo.Connection);
           
                object abc = gvWorkplaces.GetRowCellValue(gvWorkplaces.FocusedRowHandle, gvWorkplaces.Columns["Activity"]);
                int act = Convert.ToInt32 (abc);
           
                if (Convert.ToInt32(act) == 0 || Convert.ToInt32(act) == 1)
            { 
                if ((up.WPProduction == "Y"))

                {
                    
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }

            }

           
            
          
            if (Convert.ToInt32(act) == 10 )
             {
                if ((up.WPSurface == "Y"))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }

           
               
          if (Convert.ToInt32(act) == 11)
           {
                if ((up.WPUnderGround == "Y"))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
         
        }

        private void gvWorkplaces_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                return;
                //e.Handled = true;
            }
        }
    }
}
