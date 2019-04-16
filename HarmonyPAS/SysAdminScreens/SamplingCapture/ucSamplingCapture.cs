using System;
using System.Collections.Generic;
using System.Linq;
using Mineware.Systems.Global;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Mineware.Systems.ProductionGlobal;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SamplingCapture
{
    public partial class ucSamplingCapture : ucBaseUserControl
    {
        DataTable SamplingCaptureData;
        clsSamplingCaptureData _clsSamplingCaptureData = new clsSamplingCaptureData();

        public ucSamplingCapture()
        {
            InitializeComponent();

        }

        public void loadScreenData()
        {
            _clsSamplingCaptureData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            SamplingCaptureData = _clsSamplingCaptureData.getSamplingData();

            gcSamplingScreen.DataSource = SamplingCaptureData;

        }

        private void gvSamplingScreen_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

        }

        private void ucSamplingCapture_Load(object sender, EventArgs e)
        {
            loadScreenData();

            btnBack.Visible = false;
        }

        //void b_Click(object sender, EventArgs e)
        //{
        //    myEdit.btnUpdate_Click(sender, e as EventArgs);
        //    loadScreenData();
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view;

            //myEdit.theAction = "Edit";

            //var calendarDate = gvSamplingScreen.GetRowCellValue(gvSamplingScreen.FocusedRowHandle, gvSamplingScreen.Columns["CalendarDate"]).ToString();
            //myEdit.dteCalendarDate.EditValue = Convert.ToString(calendarDate);

            //if (calendarDate == "")
            //    myEdit.dteCalendarDate.Enabled = true;

            //else
            //    myEdit.dteCalendarDate.Enabled = false;

            //var workplaceID = gvSamplingScreen.GetRowCellValue(gvSamplingScreen.FocusedRowHandle, gvSamplingScreen.Columns["WorkplaceID"]).ToString();
            //myEdit.txtWorkplaceID.Text = Convert.ToString(workplaceID);

            //var stopeWidth = gvSamplingScreen.GetRowCellValue(gvSamplingScreen.FocusedRowHandle, gvSamplingScreen.Columns["SWidth"]).ToString();
            //myEdit.txtSWidth.Text = Convert.ToString(stopeWidth);

            //var channel = gvSamplingScreen.GetRowCellValue(gvSamplingScreen.FocusedRowHandle, gvSamplingScreen.Columns["Channelwidth"]).ToString();
            //myEdit.txtChannelwidth.Text = Convert.ToString(channel);

            //var hangingWall = gvSamplingScreen.GetRowCellValue(gvSamplingScreen.FocusedRowHandle, gvSamplingScreen.Columns["HangingWall"]).ToString();
            //myEdit.txtHangingWall.Text = Convert.ToString(hangingWall);

            //var footWall = gvSamplingScreen.GetRowCellValue(gvSamplingScreen.FocusedRowHandle, gvSamplingScreen.Columns["Footwall"]).ToString();
            //myEdit.txtFootwall.Text = Convert.ToString(footWall);

            //var cmgt = gvSamplingScreen.GetRowCellValue(gvSamplingScreen.FocusedRowHandle, gvSamplingScreen.Columns["Cmgt"]).ToString();
            //myEdit.txtCmgt.Text = Convert.ToString(cmgt);

            //var rif = gvSamplingScreen.GetRowCellValue(gvSamplingScreen.FocusedRowHandle, gvSamplingScreen.Columns["RIF"]).ToString();
            //myEdit.txtRIF.Text = Convert.ToString(rif);

            //gvSamplingScreen.ShowEditForm();
        }

        private void gvSamplingScreen_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            //SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            ////b.Click -= b_Click;
            //b.Click += b_Click;
        }

        private void gvSamplingScreen_DoubleClick(object sender, EventArgs e)
        {
            string wpID = "";
            DevExpress.XtraGrid.Views.Grid.GridView view;

            var workplaceID = gvSamplingScreen.GetRowCellValue(gvSamplingScreen.FocusedRowHandle, gvSamplingScreen.Columns["WorkplaceID"]).ToString();
            wpID = Convert.ToString(workplaceID);

            _clsSamplingCaptureData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtGroupedSamplingCaptureData = _clsSamplingCaptureData.getGroupedSamplingData(wpID);

            gcSamplingScreen.DataSource = dtGroupedSamplingCaptureData;

            btnBack.Visible = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //gcSamplingScreen.Refresh();
            loadScreenData();

            btnBack.Visible = false;
        }

        private void gvSamplingScreen_ColumnFilterChanged(object sender, EventArgs e)
        {
            btnBack.Visible = true;
        }
    }
}
