using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.AMISSetup
{
    public partial class ucAMISSetup : ucBaseUserControl
    {
        clsAMISSetupData _clsAMISSetupData = new clsAMISSetupData();
        ileCourseBlankSample myCourseBlankSampleEdit = new ileCourseBlankSample();
        ileStandardCRM myStandardCRMEdit = new ileStandardCRM();

        public ucAMISSetup()
        {
            InitializeComponent();
        }

        void LoadScreenData()
        {
            _clsAMISSetupData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtShaft = _clsAMISSetupData.GetShaft();
            repositoryItemShaft.DataSource = dtShaft;
            repositoryItemShaft.ValueMember = "Name";
            repositoryItemShaft.DisplayMember = "Name";

            myCourseBlankSampleEdit.UserCurrentInfo = this.UserCurrentInfo;
            myCourseBlankSampleEdit.theSystemDBTag = this.theSystemDBTag;
            gvCourseBlankSample.OptionsEditForm.CustomEditFormLayout = myCourseBlankSampleEdit;

            myStandardCRMEdit.UserCurrentInfo = this.UserCurrentInfo;
            myStandardCRMEdit.theSystemDBTag = this.theSystemDBTag;
            gvStandardCRM.OptionsEditForm.CustomEditFormLayout = myStandardCRMEdit;
        }

        private void ucAMISSetup_Load(object sender, EventArgs e)
        {
            panelCourseBlankSample.Visible = false;
            panelStandardCRM.Visible = false;
            panelDuplicateSample.Visible = false;

            LoadScreenData();

            //LoadStandardCRMData();
        }

        void LoadCourseBlankSampleData()
        {
            try
            {
                _clsAMISSetupData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                DataTable dtCourseBlankSampleData = _clsAMISSetupData.GetCourseBlankSampleData(barEditItemShaft.EditValue.ToString());

                if (dtCourseBlankSampleData.Rows.Count != 0)
                {
                    gcCourseBlankSample.DataSource = dtCourseBlankSampleData;
                    
                }
                else
                {
                    //Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "There's no data for your selection", Color.Red);
                    gcCourseBlankSample.DataSource = dtCourseBlankSampleData;
                }

            }
            catch (Exception ex)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", ex.Message, Color.Red);
            }

        }

        void LoadStandardCRMData()
        {
            try
            {
                _clsAMISSetupData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                DataTable dtStandardCRMData = _clsAMISSetupData.GetStandardCRMData(barEditItemShaft.EditValue.ToString());

                if (dtStandardCRMData.Rows.Count != 0)
                {
                    gcStandardCRM.DataSource = dtStandardCRMData;
                }
                else
                {
                    //Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "There's no data for your selection", Color.Red);
                    gcStandardCRM.DataSource = dtStandardCRMData;
                }

            }
            catch (Exception ex)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", ex.Message, Color.Red);
            }
            
        }

        void LoadDuplicateSampleData()
        {
            try
            {
                _clsAMISSetupData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                DataTable dtDuplicateSampleData = _clsAMISSetupData.GetDuplicateSampleData(barEditItemShaft.EditValue.ToString());

                if (dtDuplicateSampleData.Rows.Count != 0)
                {
                    gcDuplicateSample.DataSource = dtDuplicateSampleData;
                }
                else
                {
                    //Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "There's no data for your selection", Color.Red);
                    gcDuplicateSample.DataSource = dtDuplicateSampleData;
                }

            }
            catch (Exception ex)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", ex.Message, Color.Red);
            }

        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //LoadStandardCRMData();
        }

        private void gvAMIS_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            myStandardCRMEdit.theAction = "Add";

            myStandardCRMEdit.txtCRM.Text = "";
            myStandardCRMEdit.txtMeanValue.Text = "0.00";
            myStandardCRMEdit.txtStdDev.Text = "0.00";
        }

        private void buttonShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barEditItemShaft.EditValue.ToString() == null)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Please select a Shaft", Color.Red);
            }

            if (cmbAMIS.EditValue == null)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Please select an AMIS", Color.Red);
            }

            else
            {
                if (cmbAMIS.EditValue.ToString() == "Course Blank Sample")
                {
                    LoadCourseBlankSampleData();

                    panelCourseBlankSample.Visible = true;
                    panelStandardCRM.Visible = false;
                    panelDuplicateSample.Visible = false;
                }

                else if (cmbAMIS.EditValue.ToString() == "Standard CRM")
                {
                    LoadStandardCRMData();

                    panelCourseBlankSample.Visible = false;
                    panelStandardCRM.Visible = true;
                    panelDuplicateSample.Visible = false;
                }

                else if (cmbAMIS.EditValue.ToString() == "Duplicate Sample")
                {
                    LoadDuplicateSampleData();

                    panelCourseBlankSample.Visible = false;
                    panelStandardCRM.Visible = false;
                    panelDuplicateSample.Visible = true;
                }
            }

        }

        private void buttonBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelCourseBlankSample.Visible = false;
            panelStandardCRM.Visible = false;
            panelDuplicateSample.Visible = false;

            LoadScreenData();
        }

        void b_StandardCRMClick(object sender, EventArgs e)
        {
            _clsAMISSetupData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            myStandardCRMEdit.shaft = barEditItemShaft.EditValue.ToString();
            myStandardCRMEdit.btnUpdate_Click(sender, e as EventArgs);

            LoadStandardCRMData();
        }

        private void gvStandardCRM_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            //b.Click -= b_Click;
            b.Click += b_StandardCRMClick;
        }

        private void gvStandardCRM_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            myStandardCRMEdit.theAction = "Add";

            myStandardCRMEdit.txtCRM.Text = "";
            myStandardCRMEdit.txtMeanValue.Text = "0.00";
            myStandardCRMEdit.txtStdDev.Text = "0.00";
        }

        void b_CourseBlankSampleClick(object sender, EventArgs e)
        {
            _clsAMISSetupData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            myCourseBlankSampleEdit.shaft = barEditItemShaft.EditValue.ToString();
            myCourseBlankSampleEdit.btnUpdate_Click(sender, e as EventArgs);

            LoadCourseBlankSampleData();
        }

        private void gvCourseBlankSample_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            //b.Click -= b_Click;
            b.Click += b_CourseBlankSampleClick;
        }

        private void gvCourseBlankSample_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            myCourseBlankSampleEdit.theAction = "Add";
            myCourseBlankSampleEdit.txtLabDetectionLimit.Text = "0.00";
        }

        void b_DuplicateSampleClick(object sender, EventArgs e)
        {
            _clsAMISSetupData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            LoadDuplicateSampleData();
        }

        private void gvDuplicateSample_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            //b.Click -= b_Click;
            b.Click += b_DuplicateSampleClick;
        }

        private void gvDuplicateSample_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
        }
    }
}
