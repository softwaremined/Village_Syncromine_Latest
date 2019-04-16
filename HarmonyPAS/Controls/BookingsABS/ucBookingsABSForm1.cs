using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using DevExpress.XtraEditors;
using System.IO;

namespace Mineware.Systems.Production.Controls.BookingsABS
{
    public partial class ucBookingsABSForm1 : DevExpress.XtraEditors.XtraForm
    {
        public string TheABSPicNo = "", TheABSNotes = "", TheABSCodeDisplay = "", TheABSCode = "", TheABSPrec = "";
        public string TheSection = "", TheWorkpalce = "";
        public DateTime TheDate;
        public int TheActivity = 0;
        public ucBookingsABSForm1()
        {
            InitializeComponent();
        }
        private void ucBookingsABSForm1_Load(object sender, EventArgs e)
        {
            lblWPS.Text = TheWorkpalce;
            lblWPB.Text = TheWorkpalce;
            lblAct.Text = Convert.ToString(TheActivity);
            txtABSNote.Text = TheABSNotes;
            lblWhichPic.Text = TheABSPicNo;
            if (TheABSPrec == "P")
                PrecPicBox25.Image = picCheck.Image;
            lblCat.Text = TheABSCode;
            Date1.Value = DateTime.Now;
            loadPic();
            DesktopLocation = new Point(100, 10);
            if (lblCat.Text == "S")
            {
                pictureBox4.Image = picCross.Image;
                pictureBox5.Image = picCross.Image;
                pictureBox6.Image = picCheck.Image;

                pnlS.Location = new Point(10, 394);
                pnlS.Size = new Size(443, 235);
                pnlS.Visible = true;
                pnlS.BringToFront();
            }

            if (lblCat.Text == "B")
            {
                pictureBox4.Image = picCross.Image;
                pictureBox5.Image = picCheck.Image;
                pictureBox6.Image = picCross.Image;
                pnlB.Location = new Point(10, 394);
                pnlB.Size = new Size(443, 235);
                pnlB.Visible = true;
                pnlB.BringToFront();
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            //if (lblAct.Text == "0")
            //{
            if (lblWhichPic.Text == "")
            {
                MessageBox.Show("An ABS Classifiction has not been selected.", "Unselected Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (lblCat.Text == "S")
            {
                if (Convert.ToInt64(lblWhichPic.Text) > 11)
                {
                    MessageBox.Show("An ABS Classifiction has not been selected.", "Unselected Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
            {
                if (Convert.ToInt64(lblWhichPic.Text) < 12)
                {
                    MessageBox.Show("An ABS Classifiction has not been selected.", "Unselected Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            if (pictureBox6.Image == picCheck.Image)
            {
                if (txtABSNote.Text == "")
                {
                    MessageBox.Show("When ABS is set to S notes are required.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtABSNote.Focus();
                    return;
                }
            }

            TheABSNotes = txtABSNote.Text.ToString();
            TheABSPicNo = lblWhichPic.Text.ToString();
            if (pictureBox6.Image == picCheck.Image)
            {
                TheABSCode = "S";
                TheABSCodeDisplay = "S";
            }
            if (pictureBox5.Image == picCheck.Image)
            {
                TheABSCode = "B";
                TheABSCodeDisplay = "B";
            }
            if (PrecPicBox25.Image == picCheck.Image)
            {
                TheABSPrec = "P";
                TheABSCodeDisplay = "P";
            }
            Close();
            //}
            /*if (lblAct.Text == "1")
            {
                if (lblWhichPic.Text == "")
                {
                    MessageBox.Show("An ABS Classifiction has not been selected.", "Unselected Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (Catlabel35.Text == "S")
                {
                    if (Convert.ToInt64(PicLbl.Text) > 11)
                    {
                        MessageBox.Show("A ABS Classifiction has not been selected.", "Unselected Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    if (Convert.ToInt64(PicLbl.Text) < 12)
                    {
                        MessageBox.Show("A ABS Classifiction has not been selected.", "Unselected Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (pictureBox6.Image == picCheck.Image)
                {
                    if (Commentstxt.Text == "")
                    {
                        MessageBox.Show("When ABS is set to S notes are required.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Commentstxt.Focus();
                        return;
                    }
                }
                _ucBookingsABSProp.GridDevBook.Rows[Convert.ToInt32(Rowlabel.Text)].Cells[27].Value = Commentstxt.Text.ToString();
                _ucBookingsABSProp.GridDevBook.Rows[Convert.ToInt32(Rowlabel.Text)].Cells[28].Value = PicLbl.Text.ToString();

                if (pictureBox6.Image == picCheck.Image)
                {
                    _ucBookingsABSProp.GridDevBook.Rows[Convert.ToInt32(Rowlabel.Text)].Cells[29].Value = "S";
                    _ucBookingsABSProp.GridDevBook.Rows[Convert.ToInt32(Rowlabel.Text)].Cells[30].Value = "S";
                }
                else
                {
                    _ucBookingsABSProp.GridDevBook.Rows[Convert.ToInt32(Rowlabel.Text)].Cells[29].Value = "B";
                    _ucBookingsABSProp.GridDevBook.Rows[Convert.ToInt32(Rowlabel.Text)].Cells[30].Value = "B";
                }

                if (PrecPicBox25.Image == picCheck.Image)
                {
                    _ucBookingsABSProp.GridDevBook.Rows[Convert.ToInt32(Rowlabel.Text)].Cells[30].Value = "P";
                }

                Close();

            }*/

        }
        public void loadPic()
        {
            pictureBox7.Image = picCross.Image;
            pictureBox8.Image = picCross.Image;
            pictureBox9.Image = picCross.Image;
            pictureBox10.Image = picCross.Image;
            pictureBox11.Image = picCross.Image;
            pictureBox12.Image = picCross.Image;
            pictureBox13.Image = picCross.Image;
            pictureBox14.Image = picCross.Image;
            pictureBox15.Image = picCross.Image;
            pictureBox16.Image = picCross.Image;
            pictureBox17.Image = picCross.Image;

            pictureBox18.Image = picCross.Image;
            pictureBox19.Image = picCross.Image;
            pictureBox20.Image = picCross.Image;
            pictureBox21.Image = picCross.Image;
            pictureBox22.Image = picCross.Image;
            pictureBox23.Image = picCross.Image;
            pictureBox24.Image = picCross.Image;

            if (lblWhichPic.Text == "1")
                pictureBox7.Image = picCheck.Image;
            if (lblWhichPic.Text == "2")
                pictureBox8.Image = picCheck.Image;
            if (lblWhichPic.Text == "3")
                pictureBox9.Image = picCheck.Image;
            if (lblWhichPic.Text == "4")
                pictureBox10.Image = picCheck.Image;
            if (lblWhichPic.Text == "5")
                pictureBox11.Image = picCheck.Image;
            if (lblWhichPic.Text == "6")
                pictureBox12.Image = picCheck.Image;
            if (lblWhichPic.Text == "7")
                pictureBox13.Image = picCheck.Image;
            if (lblWhichPic.Text == "8")
                pictureBox14.Image = picCheck.Image;
            if (lblWhichPic.Text == "9")
                pictureBox15.Image = picCheck.Image;
            if (lblWhichPic.Text == "10")
                pictureBox16.Image = picCheck.Image;
            if (lblWhichPic.Text == "11")
                pictureBox17.Image = picCheck.Image;

            if (lblWhichPic.Text == "12")
                pictureBox18.Image = picCheck.Image;
            if (lblWhichPic.Text == "13")
                pictureBox19.Image = picCheck.Image;
            if (lblWhichPic.Text == "14")
                pictureBox20.Image = picCheck.Image;
            if (lblWhichPic.Text == "15")
                pictureBox21.Image = picCheck.Image;
            if (lblWhichPic.Text == "16")
                pictureBox22.Image = picCheck.Image;
            if (lblWhichPic.Text == "17")
                pictureBox23.Image = picCheck.Image;
            if (lblWhichPic.Text == "18")
                pictureBox24.Image = picCheck.Image;

        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "1";
            loadPic();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "2";
            loadPic();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "3";
            loadPic();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "4";
            loadPic();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "5";
            loadPic();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "6";
            loadPic();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "7";
            loadPic();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "8";
            loadPic();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "9";
            loadPic();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "10";
            loadPic();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "11";
            loadPic();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "12";
            loadPic();
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "13";
            loadPic();
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "14";
            loadPic();
        }

        private void pnlS_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlB_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PrecPicBox25_Click(object sender, EventArgs e)
        {
            if (lblABSPrec.Text == "Y")
            {
                lblABSPrec.Text = "N";
            }
            else
            {
                lblABSPrec.Text = "Y";
            }


            if (lblABSPrec.Text == "Y")
            {
                PrecPicBox25.Image = picCheck.Image;
            }
            else
            {
                PrecPicBox25.Image = picCross.Image;
            }
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "15";
            loadPic();
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "16";
            loadPic();
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "17";
            loadPic();
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            lblWhichPic.Text = "18";
            loadPic();
        }
    }
}
