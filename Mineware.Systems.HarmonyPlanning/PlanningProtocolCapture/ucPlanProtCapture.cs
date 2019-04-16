using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System.Threading;
using Mineware.Systems.Planning;
using Mineware.Systems.Planning.PlanningProtocolCapture;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PlanningProtocolCapture
{

    public partial class ucPlanProtCapture : ucBaseUserControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        string captureOption = "-1";
        bool hasChnaged = false;
        private Thread theLoadProtocolThread;
        ucHRPlanning ucp;
        ucPlanProtDataView ucPlanProtDataView;
        ucUnapproveWorkplaceList ucUnapproveWPList;

        public ucPlanProtCapture()
        {
            InitializeComponent();
            //barProdMonth.EditValue = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth;

            //if (TUserInfo.theSecurityLevel("PPULV") == 2)
            //{
            //    btnUnlockData.Enabled = true;
            //}
            //else { btnUnlockData.Enabled = false; }


            //CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            //BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            //BMEBL.SetsystemDBTag = this.theSystemDBTag;
            //BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            //if (BMEBL.get_Activity() == true)
            //{
            //    //  this.tscbShaft.Items.Add("MINE");
            //    editActitivity.DataSource = BMEBL.ResultsDataTable;
            //    editActitivity.DisplayMember = "Desc";
            //    editActitivity.ValueMember = "Code";

            //}


            //LoadSections();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucPlanProtDataView.printReport();
        }

        private void barEditItem1_ShowingEditor(object sender, DevExpress.XtraBars.ItemCancelEventArgs e)
        {
            LoadTemplates();
        }

        private void barProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            LoadSections();
        }

        private void barTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barWorkPlace_ShowingEditor(object sender, DevExpress.XtraBars.ItemCancelEventArgs e)
        {
            if (barActitivity.EditValue != null)
            {
                int activity = Convert.ToInt32(barActitivity.EditValue.ToString());
                MWDataManager.clsDataAccess WPData = new MWDataManager.clsDataAccess();
                WPData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                string Activity;
                if (activity == 0)
                {
                    Activity = "S";
                }
                else { Activity = "D"; }
                WPData.SqlStatement = "SELECT WP.WORKPLACEID,WP.DESCRIPTION FROM dbo.WORKPLACE WP " +
                                      "WHERE WP.Activity = " + activity + " AND " +
                                      "(WP.DELETED <> 'Y' OR " +
                                      "WP.DELETED IS NULL)";
                WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
                WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                WPData.ExecuteInstruction();


                editWorkPlace.DataSource = WPData.ResultsDataTable;
                editWorkPlace.DisplayMember = "DESCRIPTION";
                editWorkPlace.ValueMember = "WORKPLACEID";
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("Select the Activity.");
            }
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            // ucp.countcheck();

            DialogResult result;

            result = MessageBox.Show("Are you sure you want to cancel the current Planning Protocol? All changes will be lost.", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                rpSettings.Visible = true;
                rpCapture.Visible = false;
                ucPlanProtDataView.Parent = null;

                //   ucPreplanning.Dispose();
            }

        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Close();
        }

        private void btnLockData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucPlanProtDataView.saveData();
            ucUnapproveWPList = new ucUnapproveWorkplaceList { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
            ucUnapproveWPList.loadTemplateData(Convert.ToInt32(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProdMonth.EditValue.ToString()))),
                                                  barSetion.EditValue.ToString(),
                                                   Convert.ToInt32(barTemplate.EditValue.ToString()),

                                                   Convert.ToInt32(barActitivity.EditValue.ToString())
                                                  );
            if (ucUnapproveWPList.abc.Rows.Count == 0)
            {
                ucPlanProtDataView.DoApproveData("APPROVE");
            }
            else
            {
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            // ucUnapproveWPList.ShowDialog();
            ucPlanProtDataView.saveData();
            rpSettings.Visible = true;
            rpCapture.Visible = false;
            ucPlanProtDataView.Parent = null;

        }


        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Boolean canContinue = false;
            if (captureOption != "-1")
            {
                canContinue = true;

            }
            else { canContinue = false; MessageBox.Show("You need to select a Capture Option"); return; }


            if (barSetion.EditValue != null)
            {
                canContinue = true;

            }
            else { canContinue = false; MessageBox.Show("You need to select a Section"); return; }

            if (barActitivity.EditValue != null)
            {
                canContinue = true;

            }
            else { canContinue = false; MessageBox.Show("You need to select an Activity"); return; }

            if (barTemplate.EditValue != null)
            {
                canContinue = true;

            }
            else { canContinue = false; MessageBox.Show("You need to select a Template"); return; }



            if (canContinue == true)
            {

                //theLoadProtocolThread = new Thread(new ParameterizedThreadStart(LoadTheData));
                string workplace;
                if (barWorkPlace.EditValue == null)
                {
                    workplace = "";
                }
                else
                {
                    workplace = barWorkPlace.EditValue.ToString();
                }


                LoadTheData(barTemplate.EditValue.ToString(), barSetion.EditValue.ToString(), TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProdMonth.EditValue.ToString())), barActitivity.EditValue.ToString(), workplace);
                rpSettings.Visible = false;
                rpCapture.Visible = true;

            }


        }

        private void btnUnlockData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucPlanProtDataView.DoApproveData("UNAPPROVE");
        }

        private void editActitivity_EditValueChanged(object sender, EventArgs e)
        {
            barTemplate.EditValue = null;
        }

        private void editActivity_EditValueChanged(object sender, EventArgs e)
        {
            LoadTemplates();
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit theEdit = sender as DevExpress.XtraEditors.SpinEdit;
            SetProdmonth theNewMonth = new SetProdmonth();
            decimal theProdMonth;
            theProdMonth = Convert.ToDecimal(theEdit.EditValue.ToString());
            theNewMonth.getNewProdmonth(theProdMonth);
            if (theNewMonth.getProdmonth.ToString() != "-1")
                barProdMonth.EditValue = theNewMonth.getProdmonth.ToString();
            LoadSections();
        }

        private void editTmplates_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frmPlanProtCpature_Load(object sender, EventArgs e)
        {
            updateSecurity();
            barCaptureOptions.EditValue = "1";
            captureOption = "1";
            barProdMonth.EditValue = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());

            btnUnlockData.Enabled = true;



            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            if (BMEBL.get_ReviseActivity() == true)
            {
                editActitivity.DataSource = BMEBL.ResultsDataTable;
                editActitivity.DisplayMember = "Desc";
                editActitivity.ValueMember = "Code";

            }


            LoadSections();

        }

        private void LoadSections()
        {

            DataTable _SectionResult = new DataTable();
            GetSectionsAndName theData = new GetSectionsAndName { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };

            List<string> sections = TProductionGlobal.getUserInfo(UserCurrentInfo.Connection).PlanBookSections;
            foreach (string item in sections)
            {
                MWDataManager.clsDataAccess temp = theData.theSectionsAndName(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProdMonth.EditValue.ToString())), item, TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID);
                temp.ExecuteInstruction();
                if (temp.ResultsDataTable.Rows.Count != 0)
                {
                    if (_SectionResult.Columns.Count == 0)
                    {
                        _SectionResult = temp.ResultsDataTable.Clone();
                    }
                    _SectionResult.Merge(temp.ResultsDataTable);
                }

            }

            editListSections.DataSource = _SectionResult;
            editListSections.ValueMember = "SectionID";
            editListSections.DisplayMember = "Name";

        }

        private void LoadTemplates()
        {
            MWDataManager.clsDataAccess _Templates = new MWDataManager.clsDataAccess();
            _Templates.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            if ((barActitivity.EditValue != null))
            {
                if (barActitivity.EditValue.ToString() == "1")
                {

                    _Templates.SqlStatement = "SELECT PPT.TemplateID,CASE WHEN PPPA.AccessLevel = 1 THEN  TemplateName + ' - FULL ACCESS' ELSE  TemplateName + ' - READ ONLY ACCESS'  END  TemplateName   FROM PlanProt_Template PPT " +
                                            "INNER JOIN dbo.PlanProt_ProfileAccess PPPA ON " +
                                            "PPT.TemplateID = PPPA.TemplateID " +
                                            "WHERE Activity = 1 AND " +
                                            "      DepartmentID = '" + UserCurrentInfo.DepartmentID + "'";
                }
                else
                {

                    _Templates.SqlStatement = "SELECT PPT.TemplateID,CASE WHEN PPPA.AccessLevel = 1 THEN  TemplateName + ' - FULL ACCESS' ELSE  TemplateName + ' - READ ONLY ACCESS'  END  TemplateName   FROM PlanProt_Template PPT " +
                            "INNER JOIN dbo.PlanProt_ProfileAccess PPPA ON " +
                            "PPT.TemplateID = PPPA.TemplateID " +
                            "WHERE Activity = 0 AND " +
                            "      DepartmentID = '" + UserCurrentInfo.DepartmentID + "'";
                }
                _Templates.queryReturnType = MWDataManager.ReturnType.DataTable;
                _Templates.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _Templates.ExecuteInstruction();

                editTmplates.DataSource = _Templates.ResultsDataTable;
                editTmplates.DisplayMember = "TemplateName";
                editTmplates.ValueMember = "TemplateID";
            }


        }

        private void LoadTheData(string Template, string Section, string ProdMonth, string Activity, string WorkPlace)
        {
            bool Readonly = false;
            MWDataManager.clsDataAccess _Access = new MWDataManager.clsDataAccess();
            _Access.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _Access.SqlStatement = "SELECT * FROM dbo.PlanProt_ProfileAccess PPPA WHERE  PPPA.TemplateID = " + Template + " AND   DepartmentID = '" + TUserInfo.DepartmentID + "'";
            _Access.queryReturnType = MWDataManager.ReturnType.DataTable;
            _Access.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _Access.ExecuteInstruction();

            // Setup security to allow lock and unlock of data
            foreach (DataRow r in _Access.ResultsDataTable.Rows)
            {
                if (r["AccessLevel"].ToString() != "True")
                {
                    Readonly = true;
                    
                }
                else
                {
                    Readonly = false;
                    
                }// change back  btnLockData.enabled to false
            }

            // Setup secutity to allow lock of data
            _Access.SqlStatement = "SELECT DISTINCT Name_4,Name_3 ,Name_2 FROM dbo.SECTION_COMPLETE WHERE SECTIONID_2 = '" + barSetion.EditValue.ToString() + "' AND PRODMONTH = " + ProdMonth;
            _Access.ExecuteInstruction();
            string theUnit = "";
            string theShaft = "";
            string theSection = "";


            foreach (DataRow r in _Access.ResultsDataTable.Rows)
            {
                theUnit = r["Name_4"].ToString(); // the new unit
                theShaft = r["Name_3"].ToString(); // the new shaft
                theSection = r["Name_2"].ToString(); // the new section
            }

            _Access.SqlStatement = "SELECT * FROM dbo.PlanProt_ApproveUsers WHERE ((Unit = '" + theUnit + "' and Section = 'NONE' and Section = 'NONE') OR (Unit = '" + theUnit + "' and Shaft = '" + theShaft + "' and Section = 'NONE') OR (Unit = '" + theUnit + "' and Shaft = '" + theShaft + "'  and Section = '" + theSection + "'))  AND TemplateID = " + Template;
            _Access.ExecuteInstruction();

            btnLockData.Enabled = true;
            foreach (DataRow r in _Access.ResultsDataTable.Rows)
            {
                if (TUserInfo.UserID == r["User1"].ToString() || TUserInfo.UserID == r["User2"].ToString())
                {
                    btnLockData.Enabled = true;
                    break;
                }
                else { btnLockData.Enabled = false; }//change back btnLockData.Enabled to false
            }






            string WorkplacesID;
            if (WorkPlace == "")
            { WorkplacesID = "0"; }
            else { WorkplacesID = WorkPlace; }
            ucPlanProtDataView = new ucPlanProtDataView { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
            ucPlanProtDataView.Dock = DockStyle.Fill;
            ucPlanProtDataView.Parent = clientPanel;
            ucPlanProtDataView.loadTemplateData(Convert.ToInt32(ProdMonth),
                                                Section,
                                                Convert.ToInt32(Template),
                                                WorkplacesID,
                                                Convert.ToInt32(Activity),
                                                captureOption, Readonly);


            if (ucPlanProtDataView.mainData.Rows.Count == 0)
            {
                ucPlanProtDataView.Visible = false;
                rpCapture.Visible = false;
                rpSettings.Visible = true;

                _sysMessagesClass.viewMessage(Global.MessageType.Info, "ERROR LOADING DATA", this.theSystemTag, "Template", "loadTemplateData", "No Data available for selected settings", Global.ButtonTypes.OK, Global.MessageDisplayType.FullScreen);

                // MessageBox.Show("No Data available for selected settings", "", MessageBoxButtons.OK);
                // frmPlanProtCpature abc = new frmPlanProtCpature();
                //abc.Show();




            }
        }



        private void radioEditCaptureOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DevExpress.XtraEditors.RadioGroup tempGroup;
            //tempGroup = sender as DevExpress.XtraEditors.RadioGroup;
            //if (tempGroup.SelectedIndex == 0)
            //{
            //    barWorkPlace.Enabled = false;
            //}
            //else { barWorkPlace.Enabled = true; }

        }

        private void radioEditCaptureOptions_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.RadioGroup tempGroup;
            tempGroup = sender as DevExpress.XtraEditors.RadioGroup;
            if (tempGroup.Text == "1")
            {
                barWorkPlace.Enabled = false;
                captureOption = tempGroup.Text;
            }
            else { barWorkPlace.Enabled = true; captureOption = tempGroup.Text; }

        }

        private void updateSecurity()
        {
            if (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.HarmonyPasMenuStructure.miCapturePlanningProtocol_HPASCapturePlanningProtocol_MinewareSystemsHarmonyPAS.ItemID) == 0 ||
                TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.HarmonyPasMenuStructure.miCapturePlanningProtocol_HPASCapturePlanningProtocol_MinewareSystemsHarmonyPAS.ItemID) == 1)
            {
                btnSave.Enabled = false;
                btnUnlockData.Enabled = false;
                btnLockData.Enabled = false;
            }
            else
            {
                switch (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.HarmonyPasMenuStructure.miUnlockPlanningProtcol_HPASPlanProtUnlock_MinewareSystemsHarmonyPAS.ItemID))
                {
                    case 0:
                        btnUnlockData.Enabled = false;
                        break;
                    case 1:
                        btnUnlockData.Enabled = false;
                        break;
                    case 2:
                        btnUnlockData.Enabled = true;
                        break;
                }
            }
        }

        public override void setSecurity()
        {
            updateSecurity();
        }
        //public string theSystemDBTag;
        //public TUserCurrentInfo UserCurrentInfo;

        public class ThreadParameter
        {
            public string Activity { get; set; }
            public string ProdMonth { get; set; }
            public string Section { get; set; }
            public string Template { get; set; }
        }





    }
}