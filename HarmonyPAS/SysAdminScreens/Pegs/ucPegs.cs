using System;
using System.Data;
using Mineware.Systems.Global;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Pegs
{
    public partial class ucPegs : ucBaseUserControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        clsPegs _clsPegs = new clsPegs();
        ilePegs myEdit = new ilePegs();
        private DataTable dtPegs;
        public DataTable dtWPPegs;
        private DataTable dtPegsExists;

        private string _WP, _WorkplaceID, _Description, _PegID, _theValue, _Letter1, _Letter2, _Letter3;

        private bool doFocus;
        private bool doForm;
        private bool donotdoErrors;
        private bool result = false;
        //private int rowIndex;
        private int rowIndex;
        //private int rowIndex1;

        public ucPegs()
        {
            InitializeComponent();
        }
        private void ucPegs_Load(object sender, EventArgs e)
        {
            _WP = "";
            rowIndex = 7;
            loadScreenData();
        }
        private void loadScreenData()
        {
            donotdoErrors = false;
            doFocus = false;
            doForm = true;
            _clsPegs.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtPegs = _clsPegs.get_Pegs();
            /*if (_WP == "")
                dtPegs = _clsPegs.get_Pegs("");
            else
                dtPegs = _clsPegs.get_Pegs("A");*/
            gcPegs.DataSource = dtPegs;
            //DataRow[] EditedRow = dtPegs.Select(" WorkplaceID = '" + _WP + "' ");
            //if (_WP != "")
            //{

            //    gcPegs.Refresh();
            // }

            //int rowHandle = gvPegs.LocateByValue("col_WorkplaceID", _WP);
            //if (rowHandle > 0)
            gvPegs.RefreshData();
                gvPegs.FocusedRowHandle = rowIndex;
            //_WP = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_WorkplaceID != null)
            {
                if (_WorkplaceID != "")
                {
                    doForm = false;
                    gvPegs.AddNewRow();
                    _clsPegs.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    dtWPPegs = _clsPegs.get_WPPegs(_WorkplaceID);
                    myEdit.gcWPPegs.DataSource = dtWPPegs;
                    myEdit.txtPegID.Text = "";
                    myEdit.txttheValue.Text = "0.00";
                    myEdit.txtLetter1.Text = "";
                    myEdit.txtLetter2.Text = "";
                    myEdit.txtLetter3.Text = "";
                    myEdit.UserCurrentInfo = this.UserCurrentInfo;
                    myEdit.theSystemDBTag = this.theSystemDBTag;
                    gvPegs.OptionsEditForm.CustomEditFormLayout = myEdit;
                    gvPegs.ShowEditForm();
                }
                else
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "Pegs", "Please select a Workplace ", ButtonTypes.OK, MessageDisplayType.Small);
                }
            }
            else
            {
                _sysMessagesClass.viewMessage(MessageType.Info, "Pegs", "Please select a Workplace ", ButtonTypes.OK, MessageDisplayType.Small);
            }
        }

        private void gvPegs_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            object row = view.GetRow(e.RowHandle);
            //if (e.RowHandle > 0)
                //rowIndex = gvPegs.FocusedRowHandle;
                //rowIndex = Convert.ToInt32(row.ToString());

            (row as DataRowView)["WorkplaceID"] = _WorkplaceID;
            (row as DataRowView)["Description"] = _Description;
            (row as DataRowView)["PegID"] = "";
            (row as DataRowView)["theValue"] = 0;
            (row as DataRowView)["Letter1"] = "";
            (row as DataRowView)["Letter2"] = "";
            (row as DataRowView)["Letter3"] = "";
        }

        private void gvPegs_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //GridView view;
            //view = sender as GridView;
            //object row = gvPegs.GetRow(e.RowHandle);
            //object row  = gvPegs.GetRow(e.RowHandle);
            //object rowIndex = 
            //rowIndex1 = e.RowHandle;
            rowIndex = gvPegs.FocusedRowHandle;
            _WorkplaceID = "";
            if (gvPegs.GetRowCellValue(gvPegs.FocusedRowHandle, gvPegs.Columns["WorkplaceID"]) != null)
            {
                var WorkplaceID = gvPegs.GetRowCellValue(gvPegs.FocusedRowHandle, gvPegs.Columns["WorkplaceID"]);
                _WorkplaceID = WorkplaceID.ToString();
                _WP = WorkplaceID.ToString();
            }
            _Description = "";
            if (gvPegs.GetRowCellValue(gvPegs.FocusedRowHandle, gvPegs.Columns["Description"]) != null)
            {
                var Description = gvPegs.GetRowCellValue(gvPegs.FocusedRowHandle, gvPegs.Columns["Description"]);
                _Description = Description.ToString();
            }
        }

        private void gcPegs_Click(object sender, EventArgs e)
        {

        }

        private void gvPegs_Click(object sender, EventArgs e)
        {
            //rowIndex = gvPegs.FocusedRowHandle;
        }

        private void gvPegs_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            bool theSave = _clsPegs.saveData(dtPegs);
            if (theSave == true)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Peg was Added ", Color.CornflowerBlue);
                //donotdoForm1 = true;
                //dofocus = true;
                doFocus = true;
            }
        }
        private void gvPegs_GotFocus(object sender, EventArgs e)
        {
            if (doFocus == true)
            {
                loadScreenData();
                gvPegs.FocusedRowHandle = rowIndex;
            }
        }
        

        private void gvPegs_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string theValue = gvPegs.GetRowCellValue(e.RowHandle, col_PegID).ToString();
            string theValue1 = gvPegs.GetRowCellValue(e.RowHandle, col_WorkplaceID).ToString();
            if (theValue == "")
            {
                e.Valid = false;
            }
            else
            {
                _clsPegs.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtPegsExists = _clsPegs.find_WPPeg(theValue1, theValue);
                if (dtPegsExists.Rows.Count > 0)
                {
                    e.Valid = false;
                }
            }
            theValue = gvPegs.GetRowCellValue(e.RowHandle, col_Value).ToString();
            if (theValue == "")
            {
                e.Valid = false;
            }
        }

        private void gvPegs_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void gvPegs_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            if (e.Panel.Parent as Form != null)
            {
                (e.Panel.Parent as Form).StartPosition = FormStartPosition.CenterScreen;
            }
            e.Panel.Controls[1].Controls[1].Click += Form1_Click;
            //if (donotdoErrors == true)
            //    myEdit.Check_Errors();
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            if (doForm == false)
            {
                myEdit.dxPegsErrorSetup.ClearErrors();
                result = false;

                if (myEdit.txtPegID.Text == "")
                {
                    myEdit.dxPegsErrorSetup.SetError(myEdit.txtPegID, "Please enter a Peg ID");
                    result = true;
                }
                else
                {
                    dtPegsExists = _clsPegs.find_WPPeg(myEdit.txtWorkplaceID.Text, myEdit.txtPegID.Text);
                    if (dtPegsExists.Rows.Count > 0)
                    {
                        myEdit.dxPegsErrorSetup.SetError(myEdit.txtPegID, "This Peg ID already exists for Workpace ID "+ myEdit.txtWorkplaceID.Text);
                        result = true;
                    }
                }
                if (myEdit.txttheValue.Text == "")
                {
                    myEdit.dxPegsErrorSetup.SetError(myEdit.txttheValue, "Please enter a Peg Value");
                    result = true;
                }
                if (result == true)
                {
                    gvPegs.ShowEditForm();
                }
            }
        }
    }
}
