using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.Production.Controls.BookingsABS;
using Mineware.Systems.GlobalExtensions;
using System.Drawing;

namespace Mineware.Systems.Production.Controls
{
	public partial class ReconBookingForm : XtraForm
	{
		public TUserCurrentInfo UserCurrentInfo = new TUserCurrentInfo();
		private List<ReconBookingModel> _reconResult;
       // private readonly sysMessagesClass _sysMessagesClass = new sysMessagesClass();

        public ReconBookingForm()
		{
			InitializeComponent();
		}

		public string Section { get; set; }
		public string SectionId { get; set; }
		public string Prodmonth { get; set; }
		public string Calendardate { get; set; }
		public string ConnectionString { get; set; }

        public string result { get; set; }

        private void frmReconBooking_Load(object sender, EventArgs e)
		{
			txtProdMonth.Text = Prodmonth;
			txtSection.Text = Section;
			txtCalendarDate.Text = Calendardate;
            btnClose.Visible = false;
            if (UserCurrentInfo.SuperUser == true|| UserCurrentInfo.DepartmentID == 1|| UserCurrentInfo.DepartmentID == 6 || UserCurrentInfo.DepartmentID == 15)
            {
              
                btnClose.Visible = true;
               
            }
            



            LoadData();
		}
        string loaded = "N";
		private void LoadData()
	    {
			var booking = new clsBookingsABS();
			booking.setConnectionString(ConnectionString);


            if (loaded == "N")
            {
                //_reconResult.Clear();
                //gridgridControl1.Data();
                _reconResult = booking.LoadBookingRecon(Prodmonth, SectionId, Calendardate).ToList();
                gridControl1.DataSource = _reconResult;
                loaded = "Y";

                foreach (var recon in _reconResult)
                {
                    if (recon.Activity == "Stoping")
                    {
                        gcReconAdvance.Visible = false;
                        gcProgAdvance.Visible = false;
                        gcProgFaceLength.Visible = true;
                        gcReconFaceLength.Visible = true;
                    }
                    else
                    {
                        gcProgFaceLength.Visible = false;
                        gcReconFaceLength.Visible = false;
                        //gcReconAdvance.Visible = true;
                        //gcProgAdvance.Visible = true;

                        //gcProgFaceLength.Visible = false;
                        //gcReconFaceLength.Visible = false;
                    }

                }
            }

            
            
        }
        

		private void btnSave_Click(object sender, EventArgs e)
		{
            btnSave.Focus();
           // gridView1.PostEditor();

            var booking = new clsBookingsABS();
			booking.setConnectionString(ConnectionString);

         

            string message = "";
            string message2 = "";
            string Validation = "Y";

            foreach (var recon in _reconResult)
            {
                if (recon.MOComment == "" && recon.MOFC < recon.MonthPlan)
                {
                    message = "Please provide a reason why you are forecasting less for " + recon.Workplace;
                    MessageBox.Show(message, "Unsuccessful", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    //LoadData(); 
                    Validation = "N";
                    return;
                    // recon.ReconFaceLength = recon.ProgressiveFaceLength;
                }
            }

            foreach (var recon in _reconResult)
            {
                decimal reconsqm = 0;


                //Validation = "Y";
                string aa = "";
                if (recon.MOComment == null)
                {
                    aa = "";
                    recon.MOComment = "";
                }
                else
                {
                    aa = recon.MOComment;
                    recon.MOComment = aa;

                }

                //if (recon.MOComment == "" && recon.MOFC < recon.MonthPlan)
                //{
                //    message = "Please provide a reason why you are forecasting less for " + recon.Workplace;
                //    MessageBox.Show(message, "Unsuccessful", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                //    //LoadData(); 
                //    Validation = "N";                    
                //    return;
                //   // recon.ReconFaceLength = recon.ProgressiveFaceLength;



                //}

                reconsqm = recon.ReconFaceLength - recon.ProgressiveFaceLength;

                ///Save bookings after validation
                if (Validation == "Y")
                {
                    
                    if (reconsqm < 0)
                    {
                        //reconsqm = recon.ProgressiveFaceLength - recon.ReconFaceLength;
                    }
                    else if( reconsqm > 0 )
                    {
                        //reconsqm = recon.ReconFaceLength - recon.ProgressiveFaceLength;
                    }
                   
                    if (reconsqm != 0)
                    {
                        //recon.ReconFaceLength = recon.ReconFaceLength - reconsqm;
                    }
                    else
                    {
                        //recon.ReconFaceLength = recon.ReconFaceLength;
                    }

                    recon.ReconFaceLength =  reconsqm;

                    if (recon.UserId == null)
                    {
                        recon.UserId = "";
                    }
                    else { recon.UserId = recon.UserId; }
                    if (recon.MOComment == null)
                    {
                        recon.MOComment = "";
                    }
                   // recon.ReconFaceLength = recon.ReconFaceLength + reconsqm;
                    recon.ReconFaceLength =  reconsqm;

                    if (ValidateRecon())
                    {
                        // if (ValidateRecon.)
                        recon.Approved = BlastBar;

                    }

                }
                else
                {
                    if (reconsqm < 0)
                    {
                        reconsqm = recon.ProgressiveFaceLength - recon.ReconFaceLength;
                    }
                    else if (reconsqm > 0)
                    {
                        reconsqm = recon.ReconFaceLength - recon.ProgressiveFaceLength;
                    }

                    if (reconsqm != 0)
                    {
                        recon.ReconFaceLength = recon.ReconFaceLength - reconsqm;
                    }
                    else
                    {
                        //recon.ReconFaceLength = recon.ReconFaceLength;
                    }

                    //recon.ReconFaceLength = reconsqm;


                    recon.ReconFaceLength = recon.ProgressiveFaceLength + reconsqm;


                }
               

            }

            if (Validation == "Y")
            {

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Successful", "Recon Updated Succesfully", Color.CornflowerBlue);
                booking.SaveBookingRecon(_reconResult);
                //_reconResult.Clear();
                this.Close();
            }

            //if (Validation == "Y")
            //{

            //   // Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Successful", "Recon Updated Succesfully", Color.CornflowerBlue);
            //    booking.SaveBookingRecon(_reconResult);
            //    //_reconResult.Clear();
            //    //this.Close();
            //}





        }

        bool BlastBar = false;

		private void frmReconBooking_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				e.SuppressKeyPress = true;
			}
		}

		private bool ValidateRecon()
		{
			var invalid = _reconResult.Any(x => !x.Approved);
			if (invalid)
			{
                BlastBar = false;
			}
            else
            {
                BlastBar = true;
            }

			return !invalid;
		}

		private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
		{
			var view = sender as GridView;

			var row = view.GetRow(view.FocusedRowHandle);

			var actTypeId = (row as ReconBookingModel).ActivityCode;
			e.Cancel = true;
			if (view.FocusedColumn.FieldName == "ReconFaceLength" && actTypeId == 0)
			{
				e.Cancel = false;
			}

			if (view.FocusedColumn.FieldName == "ReconAdvance" && actTypeId == 1)
			{
				e.Cancel = false;
			}

			if (view.FocusedColumn.FieldName == "ReconCubics" && (actTypeId == 0 || actTypeId == 1))
			{
				e.Cancel = false;
			}

			if (view.FocusedColumn.FieldName == "Approved")
			{
				e.Cancel = false;
			}

            if (view.FocusedColumn.FieldName == "MOFC")
            {
                e.Cancel = false;
            }

            if (view.FocusedColumn.FieldName == "MOComment")
            {
                e.Cancel = false;
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            result = "False";
            this.Close();
        }
    }
}