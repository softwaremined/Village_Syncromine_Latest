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
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.QAQC
{
    public partial class ileDuplicate : EditFormUserControl
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsQAQCData _clsQAQCData = new clsQAQCData();
        public string shaft = "";
        public string amis = "";
        public string theAction = "";

        public ileDuplicate()
        {
            InitializeComponent();
        }

        private void ileDuplicate_Load(object sender, EventArgs e)
        {
            _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            _clsQAQCData.SaveDuplicateSampleData(shaft, amis, txtTicketNumber.Text, Convert.ToDecimal(txtAssayValue.Text), Convert.ToDateTime(dteDate.EditValue.ToString()),
                                        txtDuplicateTicketNo.Text, Convert.ToDecimal(txtDuplicateAssay.Text), theAction);
        }

    }
}
