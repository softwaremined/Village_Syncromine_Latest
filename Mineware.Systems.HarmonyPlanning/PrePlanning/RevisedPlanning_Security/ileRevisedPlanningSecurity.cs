using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using DevExpress.XtraEditors.Repository;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning;
using Mineware.Systems.Planning.PrePlanning;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Planning.PrePlanning.RevisedPlanning_Security
{
    public partial class ileRevisedPlanningSecurity : EditFormUserControl 
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        public RevisedSecurityDB RSDB;
        string hasChanged = "";
        string department = "";
        string section = "";
        string sectionID = "";
        string UserName = "";
        DataTable dt = new DataTable();
        ucRevisedPlanningSecurity ucr = new ucRevisedPlanningSecurity();
          bool update = false;
        public ileRevisedPlanningSecurity()
        {
            InitializeComponent();
        }

        private void ileRevisedPlanningSecurity_Load(object sender, EventArgs e)
        {
            RSDB.UserCurrentInfo = UserCurrentInfo;
            RSDB.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);



            editSection.Properties.DataSource = RSDB.GetSections(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth);
            editSection.Properties.DisplayMember = "Name";
            editSection.Properties.ValueMember = "ID";


            editUser.Properties.DataSource = RSDB.GetUserList(department);
            editUser.Properties.ValueMember = "UserID";
            editUser.Properties.DisplayMember = "UserName";


            editSecurity.Properties.DataSource = RSDB.GetSecurityType();
            editSecurity.Properties.ValueMember = "SecurityType";
            editSecurity.Properties.DisplayMember = "Description";

            //info(hasChanged);

        }

        public void info(string changed, string dept, string SECTION, string USER, string sectype)
        {
            hasChanged = changed;
            department = dept;
            //editUser.EditValue = USER;
            //editSection.EditValue = SECTION;
          //  editSection.Properties.NullText = SECTION;
      
                if (sectype == "1")
                {

            //        layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //     
                }

                else
                {
                    if (sectype == "2")
                    {
                   
                        layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                      
                    }
                }
          //  }
                if (hasChanged == "2")
                {
                    editUser.Enabled = false ;
                }
               else
                {
                    editUser.Enabled = true ;
                }

            RSDB.UserCurrentInfo = UserCurrentInfo;
            RSDB.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable SectionList = RSDB.GetSections(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth);
            DataTable UserList = RSDB.GetUserList(department);
            DataTable SecurityType = RSDB.GetSecurityType();

            editSection.Properties.DataSource = SectionList;
            editSection.Properties.DisplayMember = "Name";
            editSection.Properties.ValueMember = "ID";
            //editSection.EditValue = SectionList.Rows[0][0];

            editUser.Properties.DataSource = UserList;
            editUser.Properties.ValueMember = "UserID";
            editUser.Properties.DisplayMember = "UserName";
            //editUser.EditValue = UserList.Rows[0][0];

            editSecurity.Properties.DataSource = SecurityType;
            editSecurity.Properties.ValueMember = "SecurityType";
            editSecurity.Properties.DisplayMember = "Description";
            //editSecurity.EditValue = SecurityType.Rows[0][0];

        }

        private void chkCrewChange_CheckedChanged(object sender, EventArgs e)
        {
            //if (sectionID == "")
            //{
            //    sectionID = editSection.EditValue.ToString();
            //}
            //if (editUser.EditValue == null)
            //{ }
            //else
            //{
            //    if (chkCrewChange.Checked == true && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //    {
            //        RSDB.savedata(3, "1", editUser.EditValue.ToString(), department, sectionID);
            //        ucr.gvUserDataView.RefreshData();
            //    }
            //    else
            //    {
            //        if (chkCrewChange.Checked == false && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //        {
            //            RSDB.savedata(3, "0", editUser.EditValue.ToString(), department, sectionID);
            //            ucr.gvUserDataView.RefreshData();
            //        }
            //    }
            //}
        }

        private void chkCallChange_CheckedChanged(object sender, EventArgs e)
        {
            //if (sectionID == "")
            //{
            //    sectionID = editSection.EditValue.ToString();
            //}
            //if (editUser.EditValue == null)
            //{ }
            //else
            //{
            //    if (chkCallChange.Checked == true && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //    {
            //        RSDB.savedata(4, "1", editUser.EditValue.ToString(), department, sectionID);
            //        ucr.gvUserDataView.RefreshData();
            //    }
            //    else
            //    {
            //        if (chkCallChange.Checked == false && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //        {
            //            RSDB.savedata(4, "0", editUser.EditValue.ToString(), department, sectionID);
            //            ucr.gvUserDataView.RefreshData();
            //        }
            //    }
            //}
        }

        private void chkCallChange_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            //if (e.Value.Equals (true ) )
            //{
            //    e.CheckState = CheckState.Checked ;
            //    e.Handled = true;
            //}
            //else
            //    e.CheckState = CheckState.Unchecked ;
            //e.Handled = true;
        }

        private void chkCrewChange_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            //if (e.Value.Equals(true))
            //{
            //    e.CheckState = CheckState.Checked;
            //    e.Handled = true;
            //}
            //else
            //    e.CheckState = CheckState.Unchecked;
            //e.Handled = true;
        }

        private void chkStopWPChange_CheckedChanged(object sender, EventArgs e)
        {
            //if (editSection.EditValue.ToString() == "")
            //{
            //    sectionID = editSection.EditValue.ToString();
            //}
            //if (editUser.EditValue == null)
            //{ }
            //else
            //{
            //    if (chkStopWPChange.Checked == true && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //    {
            //        RSDB.savedata(1, "1", editUser.EditValue.ToString(), department, sectionID);
            //        ucr.gvUserDataView.RefreshData();
            //    }
            //    else
            //    {
            //        if (chkStopWPChange.Checked == false && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //        {
            //            RSDB.savedata(1, "0", editUser.EditValue.ToString(), department, sectionID);
            //            ucr.gvUserDataView.RefreshData();
            //        }
            //    }
            //}
        }

        private void chkNewWPChange_CheckedChanged(object sender, EventArgs e)
        {
            //if (editSection.EditValue.ToString() == "")
            //{
            //    sectionID = editSection.EditValue.ToString();
            //}
            //if (editUser.EditValue == null)
            //{ }
            //else
            //{
            //    if (editUser.EditValue.ToString() == "" && editUser.EditValue != null)
            //    {
            //        // MessageBox.Show("Please select the User", "", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        if (chkNewWPChange.Checked == true && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //        {
            //            RSDB.savedata(2, "1", editUser.EditValue.ToString(), department, sectionID);
            //            ucr.gvUserDataView.RefreshData();
            //        }
            //        else
            //        {
            //            if (chkNewWPChange.Checked == false && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //            {
            //                RSDB.savedata(2, "0", editUser.EditValue.ToString(), department, sectionID);
            //                ucr.gvUserDataView.RefreshData();
            //            }
            //        }
            //    }
            //}
        }

        private void chkWPMove_CheckedChanged(object sender, EventArgs e)
        {
            //if (editSection.EditValue.ToString() == "")
            //{
            //    sectionID = editSection.EditValue.ToString();
            //}
            //if (editUser.EditValue == null)
            //{ }
            //else
            //{
            //    if (editUser.EditValue.ToString() == "" && editUser.EditValue != null)
            //    {
            //        // MessageBox.Show("Please select the User", "", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        if (chkWPMove.Checked == true && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //        {
            //            RSDB.savedata(5, "1", editUser.EditValue.ToString(), department, sectionID);
            //            ucr.gvUserDataView.RefreshData();
            //        }
            //        else
            //        {
            //            if (chkWPMove.Checked == false && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //            {
            //                RSDB.savedata(5, "0", editUser.EditValue.ToString(), department, sectionID);
            //                ucr.gvUserDataView.RefreshData();
            //            }
            //        }
            //    }
            //}
        }

        private void chkStartWPChange_CheckedChanged(object sender, EventArgs e)
        {
            //if (editSection.EditValue.ToString() == "")
            //{
            //    sectionID = editSection.EditValue.ToString();
            //}
            //if (editUser.EditValue == null)
            //{ }
            //else
            //{
            //    if (editUser.EditValue.ToString() == "" && editUser.EditValue != null)
            //    {
            //        //  MessageBox.Show("Please select the User", "", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        if (chkStartWPChange.Checked == true && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //        {
            //            RSDB.savedata(6, "1", editUser.EditValue.ToString(), department, sectionID);
            //            ucr.gvUserDataView.RefreshData();
            //        }
            //        else
            //        {
            //            if (chkStartWPChange.Checked == false && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //            {
            //                RSDB.savedata(6, "0", editUser.EditValue.ToString(), department, sectionID);
            //                ucr.gvUserDataView.RefreshData();
            //            }
            //        }
            //    }
            //}
        }

        private void chkMiningMethodChange_CheckedChanged(object sender, EventArgs e)
        {

            //if (editSection.EditValue.ToString() == "")
            //{
            //    sectionID = editSection.EditValue.ToString();
            //}
            //if (editUser.EditValue == null)
            //{ }
            //else
            //{
            //    if (editUser.EditValue.ToString() == "" && editUser.EditValue != null)
            //    {
            //        //  MessageBox.Show("Please select the User", "", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        if (chkMiningMethodChange.Checked == true && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //        {
            //            RSDB.savedata(7, "1", editUser.EditValue.ToString(), department, sectionID);
            //            ucr.gvUserDataView.RefreshData();
            //        }
            //        else
            //        {
            //            if (chkMiningMethodChange.Checked == false && editUser.EditValue.ToString() != "" && editUser.EditValue != null)
            //            {
            //                RSDB.savedata(7, "0", editUser.EditValue.ToString(), department, sectionID);
            //                ucr.gvUserDataView.RefreshData();
            //            }
            //        }
            //    }
            //}
        }

        private void editSecurity_EditValueChanged(object sender, EventArgs e)
        {
            if (editSecurity.EditValue.ToString() == "2")
            {
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;  
            }
            else
            {
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;         
            }
        }

        private void chkCrewChange_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void chkCallChange_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void chkStopWPChange_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void chkNewWPChange_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void chkWPMove_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void chkStartWPChange_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void chkMiningMethodChange_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void chkCrewChange_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void editUser_EditValueChanged(object sender, EventArgs e)
        {
            RSDB.UserName = editUser.Text;
        }
    }
}
