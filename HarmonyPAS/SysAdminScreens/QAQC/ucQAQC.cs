using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using System.IO;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.QAQC
{
    public partial class ucQAQC : ucBaseUserControl
    {
        clsQAQCData _clsQAQCData = new clsQAQCData();
        DataTable dtQAQC;

        ileCourseBlank myCourseBlankEdit = new ileCourseBlank();
        ileCRM myCRMEdit = new ileCRM();
        ileDuplicate myDuplicateEdit = new ileDuplicate();
        DateTime date = DateTime.Now;
        decimal assayValue = Convert.ToDecimal("0.00");
        decimal reAssayValue = Convert.ToDecimal("0.00");

        public ucQAQC()
        {
            InitializeComponent();
        }

        void LoadScreenData()
        {
            _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            myCourseBlankEdit.UserCurrentInfo = this.UserCurrentInfo;
            myCourseBlankEdit.theSystemDBTag = this.theSystemDBTag;

            myCRMEdit.UserCurrentInfo = this.UserCurrentInfo;
            myCRMEdit.theSystemDBTag = this.theSystemDBTag;

            myDuplicateEdit.UserCurrentInfo = this.UserCurrentInfo;
            myDuplicateEdit.theSystemDBTag = this.theSystemDBTag;

            DataTable dtShaft = _clsQAQCData.GetShaft();
            rpItemShaft.DataSource = dtShaft;
            rpItemShaft.ValueMember = "Name";
            rpItemShaft.DisplayMember = "Name";
        }

        private void ucQAQC_Load(object sender, EventArgs e)
        {
            LoadScreenData();

            panelCourseBlankSample.Visible = false;
            panelStandardCRM.Visible = false;
            panelDuplicateSample.Visible = false;

            rpgBlanks.Visible = false;
            rpgCRM.Visible = false;
            rpgShow.Visible = false;
            panelCourseBlankSample.Visible = false;
            rpgBack.Visible = false;
            rpgSelection.Visible = true;
            biCRM.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            gvCourseBlank.OptionsEditForm.CustomEditFormLayout = myCourseBlankEdit;
            gvStandardCRM.OptionsEditForm.CustomEditFormLayout = myCRMEdit;
            gvDuplicateSample.OptionsEditForm.CustomEditFormLayout = myDuplicateEdit;
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (cmbAMIS.EditValue.ToString() == "Course Blank Sample")
            {
                panelCourseBlankSample.Visible = true;
                panelStandardCRM.Visible = false;
                panelDuplicateSample.Visible = false;

                LoadCourseBlankSampleData();
            }

            else if (cmbAMIS.EditValue.ToString() == "Standard CRM")
            {
                panelCourseBlankSample.Visible = false;
                panelStandardCRM.Visible = true;
                panelDuplicateSample.Visible = false;

                LoadStandardCRMData();
            }

            else if (cmbAMIS.EditValue.ToString() == "Duplicate Sample")
            {
                panelCourseBlankSample.Visible = false;
                panelStandardCRM.Visible = false;
                panelDuplicateSample.Visible = true;

                LoadDuplicateSampleData();
            }

            rpgShow.Visible = false;
        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelCourseBlankSample.Visible = false;
            panelStandardCRM.Visible = false;
            panelDuplicateSample.Visible = false;
            panelCourseBlankSample.Visible = true;
            rpgSelection.Enabled = true;
            rpgBlanks.Visible = false;
            rpgCRM.Visible = false;
            rpgShow.Visible = false;
            panelCourseBlankSample.Visible = false;
            rpgBack.Visible = false;
            //rpgImport.Visible = false;
            //panelAMIS.Visible = false;
            //panelDuplicateSamples.Visible = false;
            biCRM.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            biShaft.EditValue = "";
            cmbAMIS.EditValue = "";

        }

        void LoadCourseBlankSampleData()
        {
            try
            {
                _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                dtQAQC = _clsQAQCData.GetCourseBlankSampleData(biShaft.EditValue.ToString(), cmbAMIS.EditValue.ToString());

                gcCourseBlank.DataSource = dtQAQC;
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red); ;
            }
        }

        void LoadStandardCRMData()
        {
            try
            {
                _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                dtQAQC = _clsQAQCData.GetStandardCRMData(biShaft.EditValue.ToString(), cmbAMIS.EditValue.ToString(), biCRM.EditValue.ToString());

                gcStandardCRM.DataSource = dtQAQC;
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red); ;
            }
        }

        void LoadDuplicateSampleData()
        {
            try
            {
                _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                dtQAQC = _clsQAQCData.GetDuplicateSampleData(biShaft.EditValue.ToString(), cmbAMIS.EditValue.ToString());

                gcDuplicateSample.DataSource = dtQAQC;
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red); ;
            }
        }

        private void gvCourseBlank_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            myCourseBlankEdit.theAction = "Add";
            myCourseBlankEdit.dteDate.Text = DateTime.Now.ToString();
            myCourseBlankEdit.txtTicketNumber.Text = "";
            myCourseBlankEdit.txtAssayValue.Text = "0.00";
            myCourseBlankEdit.txtOutcome.Text = "";
            myCourseBlankEdit.txtReAssayValue.Text = "0.00";
            myCourseBlankEdit.txtOutcome.Text = "";

            DataTable dtCourseBlankSample = _clsQAQCData.GetCourseBlankSample(biShaft.EditValue.ToString());

            if (dtCourseBlankSample.Rows.Count == 0)
            {
                myCourseBlankEdit.labDetectionLimit = Convert.ToDecimal("0.00");
            }

            else if (dtCourseBlankSample.Rows.Count > 0)
            {
                myCourseBlankEdit.labDetectionLimit = Convert.ToDecimal(dtCourseBlankSample.Rows[0]["LabDetectionLimit"].ToString());
            }
        }

        void b_CourseBlankSampleClick(object sender, EventArgs e)
        {
            _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            myCourseBlankEdit.shaft = biShaft.EditValue.ToString();
            myCourseBlankEdit.amis = cmbAMIS.EditValue.ToString();
            myCourseBlankEdit.theAction = "Add";

            myCourseBlankEdit.btnUpdate_Click(sender, e as EventArgs);
            LoadScreenData();
            LoadCourseBlankSampleData();
        }

        private void gvCourseBlank_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            b.Click += b_CourseBlankSampleClick;
        }

        //void ImportCourseBlankSampleData()
        //{
        //    try
        //    {
        //        StreamReader oStreamReader = new StreamReader("CourseBlankData.csv");

        //        DataTable oDataTable = null;
        //        int RowCount = 0;
        //        string[] ColumnNames = null;
        //        string[] oStreamDataValues = null;

        //        while (!oStreamReader.EndOfStream)
        //        {
        //            String oStreamRowData = oStreamReader.ReadLine().Trim();
        //            if (oStreamRowData.Length > 0)
        //            {
        //                oStreamDataValues = oStreamRowData.Split(',');

        //                if (RowCount == 0)
        //                {
        //                    RowCount = 1;
        //                    ColumnNames = oStreamRowData.Split(',');
        //                    oDataTable = new DataTable();

        //                    foreach (string csvcolumn in ColumnNames)
        //                    {
        //                        DataColumn oDataColumn = new DataColumn(csvcolumn, typeof(string));

        //                        oDataColumn.DefaultValue = string.Empty;

        //                        oDataTable.Columns.Add(oDataColumn);
        //                    }
        //                }
        //                else
        //                {
        //                    DataRow oDataRow = oDataTable.NewRow();

        //                    for (int i = 0; i < ColumnNames.Length; i++)
        //                    {
        //                        oDataRow[ColumnNames[i]] = oStreamDataValues[i] == null ? string.Empty : oStreamDataValues[i].ToString();
        //                    }

        //                    oDataTable.Rows.Add(oDataRow);
        //                }
        //            }
        //        }
        //        oStreamReader.Close();

        //        oStreamReader.Dispose();

        //        foreach (DataRow oDataRow in oDataTable.Rows)
        //        {
        //            string RowValues = string.Empty;

        //            foreach (string csvcolumn in ColumnNames)

        //            {
        //                RowValues += csvcolumn + "=" + oDataRow[csvcolumn].ToString() + ";  ";
        //            }
        //            gcCourseBlank.DataSource = oDataTable;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", ex.Message, Color.Red);
        //    }
        //}

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbAMIS.EditValue.ToString() == "Course Blank Sample")
            {

            }

            else if (cmbAMIS.EditValue.ToString() == "CRM Standard")
            {

            }

            else if (cmbAMIS.EditValue.ToString() == "Duplicate Sample")
            {

            }
            //rpgImport.Visible = false;
        }

        private void cmbAMIS_EditValueChanged(object sender, EventArgs e)
        {
            _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtGetCourseBlankSample = _clsQAQCData.GetCourseBlankSample(biShaft.EditValue.ToString());

            if (cmbAMIS.EditValue.ToString() == "Course Blank Sample")
            {
                if (dtGetCourseBlankSample.Rows.Count == 0)
                {
                    txtLabDetectionLimit.EditValue = Convert.ToDecimal("0.00").ToString();
                }

                else if (dtGetCourseBlankSample.Rows.Count != 0)
                {
                    txtLabDetectionLimit.EditValue = Convert.ToDecimal(dtGetCourseBlankSample.Rows[0]["LabDetectionLimit"]).ToString();
                }

                rpgBlanks.Visible = true;
                rpgBack.Visible = true;
                rpgCRM.Visible = false;
                rpgShow.Visible = true;
                biCRM.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            else if (cmbAMIS.EditValue.ToString() == "Standard CRM")
            {
                rpgBlanks.Visible = false;

                biCRM.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                DataTable dtAMIS = _clsQAQCData.GetAMIS();
                rpItemCRM.DataSource = dtAMIS;
                rpItemCRM.ValueMember = "Name";
                rpItemCRM.DisplayMember = "Name";
            }

            else if (cmbAMIS.EditValue.ToString() == "Duplicate Sample")
            {
                rpgBack.Visible = true;
                rpgBlanks.Visible = false;
                rpgCRM.Visible = false;
                rpgShow.Visible = true;

                biCRM.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void biCRM_EditValueChanged(object sender, EventArgs e)
        {
            rpgCRM.Visible = true;
            rpgBack.Visible = true;
            rpgShow.Visible = true;

            DataTable dtGetStandardCRM = _clsQAQCData.GetStandardCRM(biShaft.EditValue.ToString(), biCRM.EditValue.ToString());

            rpItemCRM.DataSource = dtGetStandardCRM;
            rpItemCRM.ValueMember = "CRM";
            rpItemCRM.DisplayMember = "CRM";

            DataTable dtAMIS = _clsQAQCData.GetAMIS();
            rpItemCRM.DataSource = dtAMIS;
            rpItemCRM.ValueMember = "Name";
            rpItemCRM.DisplayMember = "Name";

            if (dtGetStandardCRM.Rows.Count == 0)
            {
                txtMeanValue.EditValue = Convert.ToDecimal("0.00").ToString();
            }

            else if (dtGetStandardCRM.Rows.Count != 0)
            {
                txtMeanValue.EditValue = Convert.ToDecimal(dtGetStandardCRM.Rows[0]["MeanValue"]).ToString();
            }
        }

        void b_StandardCRMClick(object sender, EventArgs e)
        {
            _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            myCRMEdit.shaft = biShaft.EditValue.ToString();
            myCRMEdit.amis = cmbAMIS.EditValue.ToString();
            myCRMEdit.crm = biCRM.EditValue.ToString();
            myCRMEdit.theAction = "Add";

            myCRMEdit.btnUpdate_Click(sender, e as EventArgs);
            LoadScreenData();
            LoadStandardCRMData();
        }

        private void gvStandardCRM_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            b.Click += b_StandardCRMClick;
        }

        private void gvStandardCRM_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            myCRMEdit.theAction = "Add";
            myCRMEdit.crm = biCRM.EditValue.ToString();
            myCRMEdit.dteDate.Text = DateTime.Now.ToString();
            myCRMEdit.txtTicketNumber.Text = "";
            myCRMEdit.txtAssayValue.Text = "0.00";
            myCRMEdit.txtOutcome.Text = "";
            myCRMEdit.txtReAssayValue.Text = "0.00";
            myCRMEdit.txtOutcome.Text = "";

            DataTable dtQAQCData = _clsQAQCData.GetStandardCRM(biShaft.EditValue.ToString(), biCRM.EditValue.ToString());

            if (dtQAQCData.Rows.Count == 0)
            {
                myCRMEdit.meanValue = Convert.ToDecimal("0.00");
            }

            else if (dtQAQCData.Rows.Count > 0)
            {
                myCRMEdit.meanValue = Convert.ToDecimal(dtQAQCData.Rows[0]["MeanValue"].ToString());
            }
        }

        void b_DuplicateSampleClick(object sender, EventArgs e)
        {
            _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            myDuplicateEdit.shaft = biShaft.EditValue.ToString();
            myDuplicateEdit.amis = cmbAMIS.EditValue.ToString();
            //myDuplicateEdit.crm = biCRM.EditValue.ToString();
            myDuplicateEdit.theAction = "Add";

            myDuplicateEdit.btnUpdate_Click(sender, e as EventArgs);
            LoadScreenData();
            LoadDuplicateSampleData();
        }

        private void gvDuplicateSample_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            SimpleButton b = e.Panel.Controls.OfType<PanelControl>().FirstOrDefault().Controls.OfType<SimpleButton>().Select(x => x.Text == GridLocalizer.Active.GetLocalizedString(GridStringId.EditFormUpdateButton) ? x : null).FirstOrDefault();
            b.Click += b_DuplicateSampleClick;
        }

        private void gvDuplicateSample_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            myDuplicateEdit.theAction = "Add";
            //myDuplicateEdit.crm = biCRM.EditValue.ToString();
            myDuplicateEdit.dteDate.Text = DateTime.Now.ToString();
            myDuplicateEdit.txtTicketNumber.Text = "";
            myDuplicateEdit.txtAssayValue.Text = "0.00";
            myDuplicateEdit.txtDuplicateTicketNo.Text = "";
            myDuplicateEdit.txtDuplicateAssay.Text = "0.00";
        }

        private void barButtonImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (cmbAMIS.EditValue.ToString() == "Course Blank Sample")
            {
                panelCourseBlankSample.Visible = true;
                panelStandardCRM.Visible = false;
                panelDuplicateSample.Visible = false;
            }

            else if (cmbAMIS.EditValue.ToString() == "Standard CRM")
            {
                panelCourseBlankSample.Visible = false;
                panelStandardCRM.Visible = true;
                panelDuplicateSample.Visible = false;
            }

            else if (cmbAMIS.EditValue.ToString() == "Duplicate Sample")
            {
                panelCourseBlankSample.Visible = false;
                panelStandardCRM.Visible = false;
                panelDuplicateSample.Visible = true;
            }
        }

        private void barButtonExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
