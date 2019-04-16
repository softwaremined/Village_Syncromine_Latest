using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.BookingsABS
{
    public partial class ucBookingsABSChoice : DevExpress.XtraEditors.XtraForm
    {
        public string TheSection = "", TheWorkpalce = "";
        public string TheColorA, TheColorB, TheColorS, TheABSCode = "";
        public DateTime TheDate;
        public int TheActivity = 0;

        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;


        public int x = 0;
        public int y = 0;
        public int Row = 0;
        public ucBookingsABSChoice()
        {
            InitializeComponent();
        }

        public string ExtractAfterColon(string TheString)
        {
            string AfterColon;

            int index = TheString.IndexOf(":"); // Kry die postion van die :

            AfterColon = TheString.Substring(index + 1); // kry alles na :

            return AfterColon;
        }

        private void ucBookingsABSChoice_Load(object sender, EventArgs e)
        {
            //DesktopLocation = new Point(x, y);
            lblWP.Text = ExtractAfterColon(TheWorkpalce);

            DateEdit.EditValue = System.DateTime.Today;

            WPEditItem.EditValue = ExtractAfterColon(TheWorkpalce);


            btnA.BackColor = Color.FromArgb(Convert.ToInt32(TheColorA));
            btnB.BackColor = Color.FromArgb(Convert.ToInt32(TheColorB));
            btnS.BackColor = Color.FromArgb(Convert.ToInt32(TheColorS));


            MWDataManager.clsDataAccess _dbManNeilTemp = new MWDataManager.clsDataAccess();
            _dbManNeilTemp.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManNeilTemp.SqlStatement = "select * from PLANNINGTemperature where workplaceid = '" + lblWorkplaceid.Text + "' and CalendarDate =  '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' ";

            _dbManNeilTemp.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManNeilTemp.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManNeilTemp.ExecuteInstruction();

            DataTable dtNeilTemp = _dbManNeilTemp.ResultsDataTable;

            foreach (DataRow dr in dtNeilTemp.Rows)
            {
                TxtTemp.Text = dr["Tmp"].ToString();
            }

            if (ActLbl.Text != "1")
            {
                MWDataManager.clsDataAccess _dbManDian = new MWDataManager.clsDataAccess();
                _dbManDian.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbManDian.SqlStatement = "select * from PLANNINGOther where workplaceid =  '" + lblWorkplaceid.Text + "' and calendardate = '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' " +
                    "and sectionid = '" + MoSecLbl.Text + "' and activity =  '" + ActLbl.Text + "' ";

                _dbManDian.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManDian.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManDian.ExecuteInstruction();

                DataTable dt = _dbManDian.ResultsDataTable;


                if (dt.Rows.Count > 0)
                {
                    SupTypeCombo.SelectedItem = dt.Rows[0]["supporttype"].ToString();

                    SupTopTxt.Text = dt.Rows[0]["suptop"].ToString();
                    SupMidTxt.Text = dt.Rows[0]["supmid"].ToString();
                    SupBotTxt.Text = dt.Rows[0]["supbot"].ToString();


                    SwpTxt.Text = dt.Rows[0]["sweepdist"].ToString();

                    swtextEdit1.Text = dt.Rows[0]["swtop"].ToString();
                    swtextEdit2.Text = dt.Rows[0]["swmid"].ToString();
                    swtextEdit3.Text = dt.Rows[0]["swBottom"].ToString();

                }
            }
            else
            {
                MWDataManager.clsDataAccess _dbManother = new MWDataManager.clsDataAccess();
                _dbManother.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbManother.SqlStatement = "select * from PLANNINGOtherDev where workplaceid = '" + lblWorkplaceid.Text + "' and CalendarDate =  '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' ";
                
                _dbManother.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManother.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManother.ExecuteInstruction();

                DataTable nother = _dbManother.ResultsDataTable;

                foreach (DataRow dr in nother.Rows)
                {
                    VentPipesTxt.Text = dr["VentPipes"].ToString();
                    PermSupTxt.Text = dr["PermSupport"].ToString();
                }
            }

           


            if (lblABSNotes.Text != "ABSNotes")
            {
                ABSNotestxt.Text = lblABSNotes.Text;
            }

            if (lblStatus.Text == "Safe")
                btnA_Click(null,null);
            if (lblStatus.Text == "UnSafe")
                btnB_Click(null, null);
            if (lblStatus.Text == "No Vis.")
                btnS_Click(null, null);

            LoadIncidents();

            if (ActLbl.Text == "1")
            {
                label13.Visible = false;
                label14.Visible = false;
                SwpTxt.Visible = false;
                label15.Visible = false;
                alabel17.Visible = false;

                pnlDev.Visible = true;
                pnlDev.BringToFront();
            }
            else
            {
                pnlDev.Visible = false;
                pnlDev.SendToBack();
            }

            if (lblFrmType.Text == "Vamping")
            {
                label13.Visible = false;
                label14.Visible = false;
                SwpTxt.Visible = false;
                label15.Visible = false;
                alabel17.Visible = false;

                pnlDev.Visible = false;
                //panel1.Visible = false;
                label3.Visible = false;
                label16.Visible = false;
                label7.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                alabel18.Visible = false;
                alabel23.Visible = false;
                alabel22.Visible = false;
                label8.Visible = false;
                label11.Visible = false;
                label12.Visible = false;

                VentPipesTxt.Visible = false;
                PermSupTxt.Visible = false;

                SupTypeCombo.Visible = false;

                SupTopTxt.Visible = false;
                SupMidTxt.Visible = false;
                SupBotTxt.Visible = false;


                SwpTxt.Visible = false;

                swtextEdit1.Visible = false;
                swtextEdit2.Visible = false;
                swtextEdit3.Visible = false;

            }
        }

        private void LoadIncidents()
        {
            MWDataManager.clsDataAccess _dbManNeilTemp = new MWDataManager.clsDataAccess();
            _dbManNeilTemp.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManNeilTemp.SqlStatement = "select case when  a.App_Origin = 'Pivot Action' and  b.action_closed_by is not null then 'Requested'    \r\n" +
" when a.App_Origin = 'Normal Action'  then 'NA'    \r\n" +
" else 'Open' end as RequestStatus    \r\n" +
" ,b.action_closed_by RequestedBy, a.*from    \r\n" +
" (select *, datediff(DAY, Start_Date, getdate()) DaysOpen, isnull([Mineware_Action_ID], '') + convert(varchar(300), isnull( [Pivot_Action_ID], ''))  ActionID    \r\n" +
" , case when Application_Origin like 'Pivot%' then 'Pivot Action' else 'Normal Action' end as App_Origin  from tbl_Incidents    \r\n" +
" where workplace = '" + lblWP.Text + "' and (Action_Status = 'Open' or Action_Status = 'New Action') ) a     \r\n" +
" left outer join     \r\n" +
" (Select * from tbl_Incidents_Close_Request where workplace = '" + lblWP.Text + "') b on a.Pivot_Action_ID = b.Pivot_Action_ID    \r\n" +
" order by Hazard, ActionDate ";

            //_dbManNeilTemp.SqlStatement = "select *,datediff(DAY, Start_Date, getdate()) DaysOpen,isnull([Mineware_Action_ID],'') + convert(varchar(300),isnull( [Pivot_Action_ID], ''))  ActionID \r\n"+
            //                              " , case when Application_Origin like 'Pivot%' then 'Pivot Action' else 'Normal Action' end as App_Origin  from tbl_Incidents  \r\n" +
            //                              " where workplace = '" + lblWP.Text + "' and Action_Status = 'Open' and Operation = 'RE' \r\n" +
            //                              "  order by Hazard,ActionDate ";

            _dbManNeilTemp.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManNeilTemp.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManNeilTemp.ExecuteInstruction();

            DataTable dt = _dbManNeilTemp.ResultsDataTable;

            DataSet ds = new DataSet();
            ds.Tables.Clear();
            ds.Tables.Add(dt);

            gcAction.DataSource = ds.Tables[0];

            colActionDate.FieldName = "Start_Date";
            colActionTitle.FieldName = "Action_Title";
            colResPerson.FieldName = "Responsible_Person";
            colActionID.FieldName = "ActionID";
            colHazard.FieldName = "Hazard";
            colDaysOpen.FieldName = "DaysOpen";
            colActionType.FieldName = "App_Origin";
            colPivotID.FieldName = "Pivot_Action_ID";

            colRequestedForClosed.FieldName = "RequestStatus";
        }

        private void Closebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Savebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (lblFrmType.Text == "Vamping")
            {
                if (lblStatus.Text == "Status")
                {
                    MessageBox.Show("Please select if the workplace is Safe or Unsafe");
                    return;
                }

                // do checks 
                if (TxtTemp.Text == "0.0")
                {
                    MessageBox.Show("Missing Data", "Please add a temperature reading", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TxtTemp.Focus();
                    return;
                }

                // do temp
                MWDataManager.clsDataAccess _dbManNeil = new MWDataManager.clsDataAccess();
                _dbManNeil.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbManNeil.SqlStatement = " ";
                _dbManNeil.SqlStatement = _dbManNeil.SqlStatement + "Delete from PLANNINGTemperature where workplaceid =  '" + lblWorkplaceid.Text + "' and calendardate = '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' ";
                //    "and sectionid = '" + MSecLbl.Text + "' and activity =  '" + MActLbl.Text + "' " +

                if (TxtTemp.Text != "0")
                    _dbManNeil.SqlStatement = _dbManNeil.SqlStatement + "insert into PLANNINGTemperature values ('" + lblWorkplaceid.Text + "', '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "', '" + TxtTemp.Text + "') ";

                _dbManNeil.SqlStatement = _dbManNeil.SqlStatement + "  ";
                _dbManNeil.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManNeil.queryReturnType = MWDataManager.ReturnType.longNumber;
                _dbManNeil.ExecuteInstruction();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbMan.SqlStatement = " " +
                        "Delete from PLANNINGOther where workplaceid =  '" + lblWorkplaceid.Text + "' and calendardate = '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' " +
                        "and sectionid = '" + MoSecLbl.Text + "' and activity =  '" + ActLbl.Text + "' " +


                        "insert into PLANNINGOther  \r\n" +
                        "(WorkplaceID,SectionID,Activity,CalendarDate,  \r\n" +
                        " SupportType,SupTop,SupMid,SupBot,  \r\n" +
                        " SweepDist,SWTop,SWMid,SWBottom  \r\n" +
                        " )   \r\n" +
                        "values ('" + lblWorkplaceid.Text + "', '" + MoSecLbl.Text + "', '" + ActLbl.Text + "', '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' " +
                        ", '" + SupTypeCombo.Text + "' , '" + SupTopTxt.Text + "', '" + SupMidTxt.Text + "', '" + SupBotTxt.Text + "',  " +

                        " '" + SwpTxt.Text + "' ,'" + swtextEdit1.Text + "','" + swtextEdit2.Text + "','" + swtextEdit3.Text + "' )  ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
                _dbMan.ExecuteInstruction();


                if (lblStatus.Text == "Safe")
                {
                    TheABSCode = "Safe";
                }

                if (lblStatus.Text == "UnSafe")
                {
                    TheABSCode = "UnSafe";
                }

                if (lblStatus.Text == "No Vis.")
                {
                    TheABSCode = "No Vis.";
                }

            }
            else
            {
                if (ActLbl.Text != "1")
                {
                    if (lblStatus.Text == "Status")
                    {
                        MessageBox.Show("Please select if the workplace is Safe or Unsafe");
                        return;
                    }

                    // do checks 
                    if (TxtTemp.Text == "0.0")
                    {
                        MessageBox.Show("Missing Data", "Please add a temperature reading", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        TxtTemp.Focus();
                        return;
                    }

                    if (SupTopTxt.Text == "")
                    {
                        MessageBox.Show("You Must Enter a support top", "Enter Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    if (SupMidTxt.Text == "")
                    {
                        MessageBox.Show("You Must Enter a support mid", "Enter Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (SupBotTxt.Text == "")
                    {
                        MessageBox.Show("You Must Enter a support bottom", "Enter Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (SwpTxt.Text == "")
                    {
                        MessageBox.Show("You Must Enter a sweeping distance", "Enter Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    if (SupTopTxt.Text == "0.0")
                    {
                        MessageBox.Show("You Must Enter a support top not 0", "Enter Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    if (SupMidTxt.Text == "0.0")
                    {
                        MessageBox.Show("You Must Enter a support mid not 0", "Enter Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (SupBotTxt.Text == "0.0")
                    {
                        MessageBox.Show("You Must Enter a support bottom not 0", "Enter Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    if (SwpTxt.Text == "0.0")
                    {
                        MessageBox.Show("You Must Enter a sweeping distance not 0", "Enter Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    // do temp
                    MWDataManager.clsDataAccess _dbManNeil = new MWDataManager.clsDataAccess();
                    _dbManNeil.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManNeil.SqlStatement = " ";
                    _dbManNeil.SqlStatement = _dbManNeil.SqlStatement + "Delete from PLANNINGTemperature where workplaceid =  '" + lblWorkplaceid.Text + "' and calendardate = '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' ";
                    //    "and sectionid = '" + MSecLbl.Text + "' and activity =  '" + MActLbl.Text + "' " +

                    if (TxtTemp.Text != "0")
                        _dbManNeil.SqlStatement = _dbManNeil.SqlStatement + "insert into PLANNINGTemperature values ('" + lblWorkplaceid.Text + "', '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "', '" + TxtTemp.Text + "') ";

                    _dbManNeil.SqlStatement = _dbManNeil.SqlStatement + "  ";
                    _dbManNeil.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManNeil.queryReturnType = MWDataManager.ReturnType.longNumber;
                    _dbManNeil.ExecuteInstruction();


                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbMan.SqlStatement = " " +
                            "Delete from PLANNINGOther where workplaceid =  '" + lblWorkplaceid.Text + "' and calendardate = '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' " +
                            "and sectionid = '" + MoSecLbl.Text + "' and activity =  '" + ActLbl.Text + "' " +


                            "insert into PLANNINGOther  \r\n" +
                            "(WorkplaceID,SectionID,Activity,CalendarDate,  \r\n" +
                            " SupportType,SupTop,SupMid,SupBot,  \r\n" +
                            " SweepDist,SWTop,SWMid,SWBottom  \r\n" +
                            " )   \r\n" +
                            "values ('" + lblWorkplaceid.Text + "', '" + MoSecLbl.Text + "', '" + ActLbl.Text + "', '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' " +
                            ", '" + SupTypeCombo.Text + "' , '" + SupTopTxt.Text + "', '" + SupMidTxt.Text + "', '" + SupBotTxt.Text + "',  " +

                            " '" + SwpTxt.Text + "' ,'" + swtextEdit1.Text + "','" + swtextEdit2.Text + "','" + swtextEdit3.Text + "' )  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
                    _dbMan.ExecuteInstruction();



                }
                else
                {
                    ///////////////////////DEVELOPEMENT//////////////////////////////
                    if (VentPipesTxt.Visible == true)
                    {
                        if (VentPipesTxt.Text == "0.0")
                        {
                            MessageBox.Show("Please add a Vent Pipe dist.", "Unselected Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    if (PermSupTxt.Visible == true)
                    {
                        //if (PermSupTxt.Text == "0.0")
                        //{
                        //    MessageBox.Show("Please add a Permanent Support dist.", "Unselected Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    return;
                        //}
                    }

                    // save other
                    MWDataManager.clsDataAccess _dbManOther = new MWDataManager.clsDataAccess();
                    _dbManOther.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManOther.SqlStatement = " ";
                    _dbManOther.SqlStatement = _dbManOther.SqlStatement + "Delete from PLANNINGOtherDev where workplaceid =  '" + lblWorkplaceid.Text + "' and calendardate = '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' ";

                    _dbManOther.SqlStatement = _dbManOther.SqlStatement + "insert into PLANNINGOtherDev values ('" + lblWorkplaceid.Text + "', '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "', '" + VentPipesTxt.Text + "', '" + PermSupTxt.Text + "') ";
                    _dbManOther.SqlStatement = _dbManOther.SqlStatement + "  ";

                    _dbManOther.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManOther.queryReturnType = MWDataManager.ReturnType.longNumber;
                    _dbManOther.ExecuteInstruction();

                    // do temp
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbMan.SqlStatement = " ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + "Delete from PLANNINGTemperature where workplaceid =  '" + lblWorkplaceid.Text + "' and calendardate = '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' ";

                    if (TxtTemp.Text != "0")
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "insert into PLANNINGTemperature values ('" + lblWorkplaceid.Text + "', '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "', '" + TxtTemp.Text + "') ";

                    _dbMan.SqlStatement = _dbMan.SqlStatement + "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
                    _dbMan.ExecuteInstruction();
                }




                TheActivity = Convert.ToInt32(ActLbl.Text);
                TheSection = MoSecLbl.Text;

                if (lblStatus.Text == "Safe")
                {
                    TheABSCode = "Safe";
                }

                if (lblStatus.Text == "UnSafe")
                {
                    TheABSCode = "UnSafe";
                }

                if (lblStatus.Text == "No Vis.")
                {
                    TheABSCode = "No Vis.";
                }
            }

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Data saved", Color.CornflowerBlue);
            this.Close();            
        }

        private void gvAction_DoubleClick(object sender, EventArgs e)
        {
            if (gvAction.FocusedRowHandle >= 0)
            {
                frmCloseActions frm = new frmCloseActions();
                frm.lblActionID.Text = gvAction.GetRowCellValue(gvAction.FocusedRowHandle, gvAction.Columns["ActionID"]).ToString();
                frm.lblActionType.Text = gvAction.GetRowCellValue(gvAction.FocusedRowHandle, gvAction.Columns["App_Origin"]).ToString();

                frm.lblPivotActionID.Text = gvAction.GetRowCellValue(gvAction.FocusedRowHandle, gvAction.Columns["Pivot_Action_ID"]).ToString();
                frm.lblWorkplace.Text = lblWP.Text;
                frm._theSystemDBTag = _theSystemDBTag;
                frm._UserCurrentInfoConnection = _UserCurrentInfoConnection;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                LoadIncidents();
            }           
        }

        private void gvAction_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int daysgone = Convert.ToInt32(gvAction.GetRowCellValue(e.RowHandle, gvAction.Columns["DaysOpen"]).ToString() );

            if (gvAction.GetRowCellValue(e.RowHandle,gvAction.Columns["Hazard"]).ToString() == "A")
            {
                e.Appearance.ForeColor = Color.Tomato;
            }

            if (daysgone > 7)
            {
                e.Appearance.ForeColor = Color.Tomato;
            }

            if (e.Column.FieldName == "RequestStatus")
            {
                if (e.CellValue.ToString() == "NA")
                {
                    e.Appearance.ForeColor = Color.Gainsboro;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

        }

        private void RCRockEngineering_Click(object sender, EventArgs e)
        {

        }

        private void gvAction_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvAction.FocusedColumn.FieldName == "RequestStatus")
            {
                if (gvAction.GetRowCellValue(gvAction.FocusedRowHandle, gvAction.FocusedColumn.FieldName).ToString() == "NA")
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Safe";
            pbxSafe.Image = pbxChecked.Image;

            pbxNotVisited.Visible = false;

            ABSNotestxt.Visible = false;
            label1.Visible = false;
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "UnSafe";
            pbxSafe.Image = pbxCross.Image;

            pbxNotVisited.Visible = false;


            ABSNotestxt.Visible = true;
            label1.Visible = true;
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "No Vis.";

            pbxNotVisited.Visible = true;

            ABSNotestxt.Visible = false;
            label1.Visible = false;

            // do temp
            //MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            //_dbMan.SqlStatement = " ";
            //_dbMan.SqlStatement = _dbMan.SqlStatement + "Delete from PLANNINGTemperature where workplaceid =  '" + lblWorkplaceid.Text + "' and calendardate = '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "' ";
            ////    "and sectionid = '" + MSecLbl.Text + "' and activity =  '" + MActLbl.Text + "' " +

            //if (TxtTemp.Text != "0")
            //    _dbMan.SqlStatement = _dbMan.SqlStatement + "insert into PLANNINGTemperature values ('" + lblWorkplaceid.Text + "', '" + String.Format("{0:yyyy-MM-dd}", OtherdateTimePicker1.Value) + "', '" + TxtTemp.Text + "') ";

            //_dbMan.SqlStatement = _dbMan.SqlStatement + "  ";
            //_dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
            //_dbMan.ExecuteInstruction();

            TheActivity = Convert.ToInt32(ActLbl.Text);
            TheSection = MoSecLbl.Text;

            if (lblStatus.Text == "Safe")
            {
                TheABSCode = "Safe";
            }

            if (lblStatus.Text == "UnSafe")
            {
                TheABSCode = "UnSafe";
            }

            if (lblStatus.Text == "No Vis.")
            {
                TheABSCode = "No Vis.";
            }

            //Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Data saved", Color.CornflowerBlue);
            //this.Close();

        }

        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            TheABSCode = "";
        }
    }
}
