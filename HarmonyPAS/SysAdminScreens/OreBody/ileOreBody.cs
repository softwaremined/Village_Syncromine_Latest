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
using DevExpress.XtraGrid.Columns;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.OreBody
{
    public partial class ileOreBody : EditFormUserControl 
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsOreBody _clsOreBody = new clsOreBody();
       // ucOreBody _ucOreBody = new ucOreBody();
        public string update = "";
        public string edit = "";
      public   bool result = false;
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        public ileOreBody()
        {
            InitializeComponent();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        public  void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _clsOreBody.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //BindingSource bs = (BindingSource)gvGridEdit.DataSource;
                DataTable gridedit = new DataTable();
                // DataTable dt = new DataTable();
                foreach (GridColumn column in gvOreBodyEdit.VisibleColumns)
                {
                    gridedit.Columns.Add(column.FieldName, column.ColumnType);
                }

                for (int i = 0; i < gvOreBodyEdit.DataRowCount; i++)
                {
                    DataRow row = gridedit.NewRow();
                    foreach (GridColumn column in gvOreBodyEdit.VisibleColumns)
                    {
                        row[column.FieldName] = gvOreBodyEdit.GetRowCellValue(i, column);
                    }
                    gridedit.Rows.Add(row);
                }

                if (txtName.Text == "")
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Common Area", ButtonTypes.OK, MessageDisplayType.Small);
                    return;
                }
                _clsOreBody.SaveOreBody(txtName.Text, edit, update, gridedit);
                result = true;
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Ore Body data Saved", Color.CornflowerBlue);




                // this.Dispose();
                // this.InitializeComponent();
                //_ucOreBody.loadData();
                //_ucOreBody.closeedit();
                //var parent = this.Parent;
                //while (parent != null && this.Parent as DataGridView  == null)
                //{
                //    parent = parent.Parent;
                //}
                //DataGridView grid = this.Parent as DataGridView;
                //grid.CancelRowEdit();
                //  _ucOreBody.gvOreBody_RowUpdated(sender,e as DevExpress.XtraGrid.Views.Base.RowObjectEventArgs );

               // this.ParentForm.Close();
                //this.Dispose();
              //  return;
                //_ucOreBody.loadData();
                ///return true;
               // ucOreBody _ucOreBody = new ucOreBody { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
               // _ucOreBody.gvOreBody.Focus();
            }
            catch (Exception x)
            {

            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //  this.Close();
           
           
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
          
        }

        private void ileOreBody_Leave(object sender, EventArgs e)
        {
          
        }
    }
}
