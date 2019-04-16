using Mineware.Systems.Global;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Pegs
{
    public partial class ilePegs : EditFormUserControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        clsPegs _clsPegs = new clsPegs();
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        private string _WPID, _WPPegID, _WPtheValue;
        private DataTable dtBookings;
        private DataTable dtDelete;
        private DataTable dtWPPegs;
        private DataTable dtPegsExists;
        public ilePegs()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            _clsPegs.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtBookings = _clsPegs.get_Bookings(_WPID, _WPPegID);
            if (dtBookings.Rows.Count > 0)
                _sysMessagesClass.viewMessage(MessageType.Info, "Peg Delete", "A Booking has been done - Can not delete the Peg ", ButtonTypes.OK, MessageDisplayType.Small);
            else
            {
                dtBookings = _clsPegs.get_Bookings(_WPID, _WPPegID, _WPtheValue);
                if (dtBookings.Rows.Count > 0)
                    _sysMessagesClass.viewMessage(MessageType.Info, "Peg Delete", "A Booking has been done - Can not delete the Peg ", ButtonTypes.OK, MessageDisplayType.Small);
                else
                {
                    DialogResult theResult;
                    theResult = MessageBox.Show("Are you sure you want to Delete the Peg? ", "Create Audit", MessageBoxButtons.YesNo);
                    if (theResult == DialogResult.Yes)
                    {
                        dtDelete = _clsPegs.get_WPPegDelete(_WPID, _WPPegID);
                        dtWPPegs = _clsPegs.get_WPPegs(_WPID);
                        gcWPPegs.DataSource = dtWPPegs;
                    }
                }
            }
        }

        private void gvWPPegs_RowClick(object sender, RowClickEventArgs e)
        {
            _WPID = "";
            if (gvWPPegs.GetRowCellValue(gvWPPegs.FocusedRowHandle, gvWPPegs.Columns["WPID"]) != null)
            {
                var WPID = gvWPPegs.GetRowCellValue(gvWPPegs.FocusedRowHandle, gvWPPegs.Columns["WPID"]);
                _WPID = WPID.ToString();
            }
            _WPPegID = "";
            if (gvWPPegs.GetRowCellValue(gvWPPegs.FocusedRowHandle, gvWPPegs.Columns["WPPegID"]) != null)
            {
                var WPPegID = gvWPPegs.GetRowCellValue(gvWPPegs.FocusedRowHandle, gvWPPegs.Columns["WPPegID"]);
                _WPPegID = WPPegID.ToString();
            }
            _WPtheValue = "";
            if (gvWPPegs.GetRowCellValue(gvWPPegs.FocusedRowHandle, gvWPPegs.Columns["WPtheValue"]) != null)
            {
                var WPtheValue = gvWPPegs.GetRowCellValue(gvWPPegs.FocusedRowHandle, gvWPPegs.Columns["WPtheValue"]);
                _WPtheValue = WPtheValue.ToString();
            }
        }

        private void txtPegID_Enter(object sender, System.EventArgs e)
        {
            Check_Errors();
        }

        public void Check_Errors()
        {
            dxPegsErrorSetup.ClearErrors();
            _clsPegs.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (txtPegID.Text == "")
            {
                dxPegsErrorSetup.SetError(txtPegID, "Please enter a Peg ID");
            }
            else
            {
                dtPegsExists = _clsPegs.find_WPPeg(txtWorkplaceID.Text, txtPegID.Text);
                if (dtPegsExists.Rows.Count > 0)
                {
                    dxPegsErrorSetup.SetError(txtPegID, "This Peg ID already exists for Workpace ID " + txtWorkplaceID.Text);
                }
            }
            if (txttheValue.Text == "")
            {
                dxPegsErrorSetup.SetError(txttheValue, "Please enter a Peg Value");
            }           
        }
    }
}
