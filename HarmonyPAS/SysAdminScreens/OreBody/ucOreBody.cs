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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.OreBody
{
    public partial class ucOreBody : ucBaseUserControl
    {
        string TheAction = "";
         clsOreBody _clsOreBody = new clsOreBody();
        ileOreBody myEdit = new ileOreBody();
       
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        public ucOreBody()
        {
            InitializeComponent();
        }

        private void ucOreBody_Load(object sender, EventArgs e)
        {
            _clsOreBody.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);          
            //DataTable areas = new DataTable();
            //areas = _clsOreBody.loadOreBody();
            //gcOreBody.DataSource = areas;
            //foreach (DataRow r in _dbMan.ResultsDataTable.Rows)
            //{
            //    int NewRow = dtgrid.Rows.Add();
            //    dtgrid.Rows[NewRow].Cells[0].Value = r["ID"].ToString();
            //    dtgrid.Rows[NewRow].Cells[1].Value = r["Name"].ToString();
            //    if (SysSettings.IsCentralized.ToString() == "1")
            //    {
            //        dtgrid.Rows[NewRow].Cells[2].Value = r["Mine"].ToString();
            //    }
            //}
            //dtgrid.Visible = true;
            //if (dtgrid.Rows.Count > 0)
            //{
            //    this.GridSelectLabel.Text = dtgrid.Rows[0].Cells[0].Value.ToString();
            //}
            loadData();
        }

        public void loadData()
        {
            _clsOreBody.theData.ConnectionString = TConnections.GetConnectionString(this.theSystemDBTag, this.UserCurrentInfo.Connection);
            DataTable areas = new DataTable();
            areas = _clsOreBody.loadOreBody();
            gcOreBody.DataSource = areas;
           
            myEdit.UserCurrentInfo = this.UserCurrentInfo;
            myEdit.theSystemDBTag = this.theSystemDBTag;
            // myEdit.Tag = editProdmonth.EditValue.ToString();
            gvOreBody.OptionsEditForm.CustomEditFormLayout = myEdit;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TheAction = "Edit";
            //string Division = "";
            //Division = gvGrid  .GetRowCellValue(gvGrid .FocusedRowHandle , gvGrid.Columns ["Division"]).ToString();
            //string code = "";
            //code = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Code"]).ToString();
            //ileGrid myEdit = new ileGrid();
            // myEdit.gcGridEdit.DataSource = _clsWorkplaceCodes.loadGridEdit(TheAction, Division, code);
            //myEdit.lkpDivision.Focus();
            //gvGrid.ShowEditForm();

            //ileGridEdit myEdit = new ileGridEdit();
            //myEdit.UserCurrentInfo = this.UserCurrentInfo;
            //myEdit.theSystemDBTag = this.theSystemDBTag;

            //ileOreBody myEdit = new ileOreBody();
            //myEdit.UserCurrentInfo = this.UserCurrentInfo;
            //myEdit.theSystemDBTag = this.theSystemDBTag;
            var Name = gvOreBody  .GetRowCellValue(gvOreBody.FocusedRowHandle, gvOreBody.Columns["Name"]).ToString();
            myEdit.txtName .Text  = Convert.ToString(Name);
            var ID = gvOreBody.GetRowCellValue(gvOreBody.FocusedRowHandle, gvOreBody.Columns["Id"]).ToString();
          //  myEdit.txtGrid.Text = Convert.ToString(code);
            //var Description = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Description"]).ToString();
            //myEdit.txtDescription.Text = Convert.ToString(Description);
            //var CostArea = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["CostArea"]).ToString();
            //myEdit.txtCostarea.Text = Convert.ToString(CostArea);
            //DataTable gridedit = new DataTable();
            //gridedit = _clsOreBody .loadGridEdit(Convert.ToString(ID));
            myEdit.update = "Edit";
            myEdit.edit = Convert.ToString(ID);
            //myEdit.gcOreBodyEdit .DataSource  = gridedit;
            // myEdit.ShowDialog();
            gvOreBody.ShowEditForm();
            //if (myEdit.result == true)
            //{
            //    gvOreBody.CloseEditForm();
            //    loadData();
            //    myEdit.result = false;
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TheAction = "Add";
            gvOreBody.AddNewRow();
            myEdit.txtName.Text ="";
        
            DataTable gridedit = new DataTable();
            gridedit = _clsOreBody.loadGridEdit("");
            myEdit.update = "Add";
            myEdit.edit ="";
            myEdit.gcOreBodyEdit.DataSource = gridedit;
       
            gvOreBody.ShowEditForm();
         
        }

        private void gvOreBody_ShowingPopupEditForm(object sender, ShowingPopupEditFormEventArgs e)
        {
            //foreach (var button in e.EditForm.Controls.OfType<EditFormContainer>()
            //            .SelectMany(control => Enumerable.Cast<Control>(control.Controls)).OfType<PanelControl>()
            //            .SelectMany(nestedControl => Enumerable.Cast<Control>(nestedControl.Controls)).OfType<SimpleButton>())
            //{
            //    switch (button.Text)
            //    {
            //        case "Update":
            //            // Hide this button for the form will be read-only.
            //            button.Visible = false;
            //            break;

            //        case "Cancel":
            //            // Replace the Cancel button text with a localized version of the word Continue.
            //            button.Text = "Continue";
            //            break;
            //    }
            //}
        }



        public  void gvOreBody_RowUpdated(object sender, RowObjectEventArgs e)
        {
           // gvOreBody.CloseEditForm();
           // gvOreBody_ValidateRow(sender, e as ValidateRowEventArgs);
            //DataTable gridedit = new DataTable();
            //// DataTable dt = new DataTable();
            //var ID = gvOreBody.GetRowCellValue(gvOreBody.FocusedRowHandle, gvOreBody.Columns["Id"]).ToString();
            //foreach (GridColumn column in  myEdit . gvOreBodyEdit.VisibleColumns)
            //{
            //    gridedit.Columns.Add(column.FieldName, column.ColumnType);
            //}
            //for (int i = 0; i < myEdit.gvOreBodyEdit.DataRowCount; i++)
            //{
            //    DataRow row = gridedit.NewRow();
            //    foreach (GridColumn column in myEdit.gvOreBodyEdit.VisibleColumns)
            //    {
            //        row[column.FieldName] = myEdit.gvOreBodyEdit.GetRowCellValue(i, column);
            //    }
            //    gridedit.Rows.Add(row);
            //}
            //if (myEdit . txtName.Text == "")
            //{
            //    _sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Common Area", ButtonTypes.OK, MessageDisplayType.Small);
            //    return;
            //}
            //_clsOreBody.SaveOreBody(myEdit.txtName.Text, Convert.ToString(ID), TheAction, gridedit);
            //Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Ore Body data Saved", Color.CornflowerBlue);

        }

        private void GvOreBody_LostFocus(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void gvOreBody_DoubleClick(object sender, EventArgs e)
        {
            TheAction = "Edit";
            //string Division = "";
            //Division = gvGrid  .GetRowCellValue(gvGrid .FocusedRowHandle , gvGrid.Columns ["Division"]).ToString();
            //string code = "";
            //code = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Code"]).ToString();
            //ileGrid myEdit = new ileGrid();
            // myEdit.gcGridEdit.DataSource = _clsWorkplaceCodes.loadGridEdit(TheAction, Division, code);
            //myEdit.lkpDivision.Focus();
            //gvGrid.ShowEditForm();

            //ileGridEdit myEdit = new ileGridEdit();
            //myEdit.UserCurrentInfo = this.UserCurrentInfo;
            //myEdit.theSystemDBTag = this.theSystemDBTag;

            //ileOreBody myEdit = new ileOreBody();
            //myEdit.UserCurrentInfo = this.UserCurrentInfo;
            //myEdit.theSystemDBTag = this.theSystemDBTag;
            var Name = gvOreBody.GetRowCellValue(gvOreBody.FocusedRowHandle, gvOreBody.Columns["Name"]).ToString();
            myEdit.txtName.Text = Convert.ToString(Name);
            var ID = gvOreBody.GetRowCellValue(gvOreBody.FocusedRowHandle, gvOreBody.Columns["Id"]).ToString();
            //  myEdit.txtGrid.Text = Convert.ToString(code);
            //var Description = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Description"]).ToString();
            //myEdit.txtDescription.Text = Convert.ToString(Description);
            //var CostArea = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["CostArea"]).ToString();
            //myEdit.txtCostarea.Text = Convert.ToString(CostArea);
            DataTable gridedit = new DataTable();
            gridedit = _clsOreBody.loadGridEdit(Convert.ToString(ID));
            myEdit.update = "Edit";
            myEdit.edit = Convert.ToString(ID);
            myEdit.gcOreBodyEdit.DataSource = gridedit;
            // myEdit.ShowDialog();
            gvOreBody.ShowEditForm();
            //if (myEdit.result == true)
            //{
            //    gvOreBody.CloseEditForm();
            //    loadData();
            //    myEdit.result = false;
            //}
        }

        public void closeedit()
        {
            //gvOreBody.UpdateCurrentRow();
            //gvOreBody.CloseEditForm();
            //gvOreBody.CloseEditor();
            //this.ParentForm.Close();
           // myEdit.Dispose();
            //InitializeComponent();
            //ileOreBody myEdit = new ileOreBody();
        
           // loadData();
        }

        private void gvOreBody_EditFormPrepared(object sender, EditFormPreparedEventArgs e)
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

        private void gvOreBody_ValidateRow(object sender, ValidateRowEventArgs e)
        {
           
        }

        private void gvOreBody_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = gvOreBody.GetRowCellValue(gvOreBody.FocusedRowHandle, gvOreBody.Columns["Id"]).ToString();
           string ID = Convert.ToString(id );

            if (ID == "")
            {
                MessageBox.Show("Please select a record to be deleted.", "No record selected", MessageBoxButtons.OK);
            }
            else if (MessageBox.Show("Are you sure you want to delete record", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.SqlStatement = "DELETE FROM CommonAreas WHERE ID = '" + ID + "'";
                _dbMan.SqlStatement += " DELETE FROM CommonAreaSubSections WHERE CommonArea = '" + ID + "'";

                _dbMan.ExecuteInstruction();
                loadData();
            }
        }

        private void gcOreBody_Click(object sender, EventArgs e)
        {

        }
    }
}
