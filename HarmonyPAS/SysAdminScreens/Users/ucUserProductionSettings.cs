using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Production.SysAdminScreens.Users
{
    public partial class ucUserProductionSettings : ucBaseUserControl
    {
        public ScreenStatus theScreenStatus;
        public string currentConnection;        
        private clsUserProductionSettings dlUsers = new clsUserProductionSettings();
        private DataTable userSectionsBookPlan = new DataTable();
        private DataTable userSectionsReports = new DataTable();
        private DataTable userInfo = new DataTable();
        public ucUserProductionSettings()
        {
            InitializeComponent();
            SaveRequest += new SaveRequestEventHandler(saveMyData);
            LoadUserModulesRequest += new LoadUserModulesEventHandler(LoadUserInfo);
        }



        protected virtual void LoadUserInfo(object sender, LoadUserModulesRequestEventArgs e)
        {
            UserCurrentInfo.Connection = currentConnection;
            dlUsers.UserCurrentInfo = UserCurrentInfo;
            userInfo = dlUsers.getUserInfo(UserID);

            userSectionsBookPlan = dlUsers.getUserSections(UserID, "P");
            tlSectionsBook.DataSource = userSectionsBookPlan;
            tlSectionsBook.ParentFieldName = "ReportToID";
            tlSectionsBook.KeyFieldName = "SectionID";

            userSectionsReports = dlUsers.getUserSections(UserID, "R");
            tlSectionsReport.DataSource = userSectionsReports;
            tlSectionsReport.ParentFieldName = "ReportToID";
            tlSectionsReport.KeyFieldName = "SectionID";

            rgBackDatedBookings.DataBindings.Add(new Binding("EditValue", userInfo, "BackDateBooking"));
            seDaysBackBook.DataBindings.Add(new Binding("EditValue", userInfo, "DaysBackdate"));
            ceWPProduction.DataBindings.Add(new Binding("EditValue", userInfo, "WPProduction"));
            ceWPSurface.DataBindings.Add(new Binding("EditValue", userInfo, "WPSurface"));
            ceWPUnderGround.DataBindings.Add(new Binding("EditValue", userInfo, "WPUnderGround"));
            ceWPEditName.DataBindings.Add(new Binding("EditValue", userInfo, "WPEditName"));
            ceWPEditAttributes.DataBindings.Add(new Binding("EditValue", userInfo, "WPEditAttribute"));
            ceWPClassify.DataBindings.Add(new Binding("EditValue", userInfo, "WPClassify"));

            if (theScreenStatus == Global.ScreenStatus.Edit)
                userInfo.NewRow();
        }
        //protected void LoadUserInfo()
        //{
        //    UserCurrentInfo.Connection = currentConnection;
        //    dlUsers.UserCurrentInfo = UserCurrentInfo;
        //    userInfo = dlUsers.getUserInfo(UserID);

        //    rgBackDatedBookings.DataBindings.Add(new Binding("EditValue", userInfo, "BackDateBooking"));
        //    ceWPProduction.DataBindings.Add(new Binding("EditValue", userInfo, "WPProduction"));
        //    ceWPSurface.DataBindings.Add(new Binding("EditValue", userInfo, "WPSurface"));
        //    ceWPUnderGround.DataBindings.Add(new Binding("EditValue", userInfo, "WPUnderGround"));
        //    ceWPEditName.DataBindings.Add(new Binding("EditValue", userInfo, "WPEditName"));
        //    ceWPEditAttributes.DataBindings.Add(new Binding("EditValue", userInfo, "WPEditAttribute"));
        //    ceWPClassify.DataBindings.Add(new Binding("EditValue", userInfo, "WPClassify"));

        //    if (theScreenStatus == Global.ScreenStatus.Edit)
        //        userInfo.NewRow();
        //}
        protected virtual void saveMyData(object sender, SaveRequestEventArgs e)
        {
            if (e.TheUserID != "")
            {
                if (testConditions())
                {
                    this.hasSaved = SaveUserData();
                    this.hasSaved = true;
                }
                else
                {
                    this.hasSaved = false;
                    MessageBox.Show("Back Date Days may not be 0", "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
            else
            {
                this.hasSaved = false;
            }
        }

        private bool SaveUserData()
        {
            bool theResult = false;
            bool ResultUserSettings = false;
            bool ResultPSection = true;
            bool ResultRSection = true;

            if (ceWPSurface.Tag.ToString() == "1" || ceWPProduction.Tag.ToString() == "1" ||
               ceWPEditAttributes.Tag.ToString() == "1" || ceWPEditName.Tag.ToString() == "1" ||
               ceWPUnderGround.Tag.ToString() == "1" || 
               seDaysBackBook.Tag.ToString() == "1" || rgBackDatedBookings.Tag.ToString() == "1" ||
                ceWPClassify .Tag .ToString () == "1")
            {
                ResultUserSettings = dlUsers.SaveUserSettings(userInfo, UserID);
                ResultPSection = dlUsers.SaveUserSections(userSectionsBookPlan, UserID, "P");
                ResultRSection = dlUsers.SaveUserSections(userSectionsReports, UserID, "R");
                if (ResultUserSettings == true && ResultPSection == true && ResultRSection == true)
                    theResult = true;
                else
                    theResult = false;
            }
            else theResult = true;


            return theResult;
        }

        private bool testConditions()
        {
            int errorCount = 0;
            DataRow[] userSectionsBookPlanCount = userSectionsBookPlan.Select("IsLinked = 1");
            if (userSectionsBookPlanCount.Count() == 0)
            {
                //lciUseProfileError.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciBackDateBookings.OptionsToolTip.ToolTip = "Select a Planning/Booking section";
                lciBackDateBookings.Image = TGlobalItems.notificationImageError;

                errorCount++;
                //   setBorderEditBox(lueDepartment);
            }
            else
            {
                lciBackDateBookings.OptionsToolTip.ToolTip = "";
                lciBackDateBookings.Image = null;
            }

            DataRow[] userSectionsReportsCount = userSectionsReports.Select("IsLinked = 1");
            if (userSectionsReportsCount.Count() == 0)
            {
                //lciUseProfileError.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciReportsSections.OptionsToolTip.ToolTip = "Select a Reporting section";
                lciReportsSections.Image = TGlobalItems.notificationImageError;

                errorCount++;
                //   setBorderEditBox(lueDepartment);
            }
            else
            {
                lciReportsSections.OptionsToolTip.ToolTip = "";
                lciReportsSections.Image = null;
            }
            bool theResult;
            if (rgBackDatedBookings.SelectedIndex == 1)
            {
                if (Convert.ToInt32(seDaysBackBook.Text) == 0)
                    theResult = false;
                else
                    theResult = true;
            }
            else
            {
                theResult = true;
            }

            return theResult;
        }

        private void ucUserProductionSettings_Load(object sender, EventArgs e)
        {
            //LoadUserInfo();
        }

        private void rgBackDatedBookings_EditValueChanged(object sender, EventArgs e)
        {
            if (rgBackDatedBookings.EditValue.ToString() == "1")
            {
                seDaysBackBook.Enabled = true;
            }
            else seDaysBackBook.Enabled = false;
            rgBackDatedBookings.Tag = 1;
        }
        private void seDaysBackBook_EditValueChanged(object sender, EventArgs e)
        {
            seDaysBackBook.Tag = 1;
        }


        private void ceWPProduction_CheckedChanged(object sender, EventArgs e)
        {
            ceWPProduction.Tag = 1;
        }

        private void ceWPSurface_CheckedChanged(object sender, EventArgs e)
        {
            ceWPSurface.Tag = 1;
        }

        private void ceWPUnderGround_CheckedChanged(object sender, EventArgs e)
        {
            ceWPUnderGround.Tag = 1;
        }

        private void ceWPEditName_CheckedChanged(object sender, EventArgs e)
        {
            ceWPEditName.Tag = 1;
        }

        private void ceWPEditAttributes_CheckedChanged(object sender, EventArgs e)
        {
            ceWPEditAttributes.Tag = 1;
        }

        private void ceWPClassify_CheckedChanged(object sender, EventArgs e)
        {
            ceWPClassify.Tag = 1;
        }

        private void tlSectionsReport_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            Object DataRow = tlSectionsReport.GetDataRecordByNode(e.Node);
            DataRowView drv = DataRow as DataRowView;
            if (drv != null)
                drv.Row["Updated"] = 1;
        }

        private void tlSectionsBook_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            Object DataRow = tlSectionsBook.GetDataRecordByNode(e.Node);
            DataRowView drv = DataRow as DataRowView;
            if (drv != null)
                drv.Row["Updated"] = 1;
        }
    }
}
