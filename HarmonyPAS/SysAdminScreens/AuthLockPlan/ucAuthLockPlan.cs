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
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.AuthLockPlan
{
    public partial class ucAuthLockPlan : Mineware.Systems.Global.ucBaseUserControl
    {
        decimal theNewMonth = 0;
        DataTable dt = new DataTable();
        TUserProduction theUser = new TUserProduction();
        clsAuthLockPlanData _clsAuthLockPlanData = new clsAuthLockPlanData();
        string TheTopEndText;

        public ucAuthLockPlan()
        {
            InitializeComponent();
            updateSecurity();
        }

        public override void setSecurity()
        {
            updateSecurity();
        }

        private void updateSecurity()
        {
            if (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS.ItemID) == 2)
            {
                btnSave.Enabled = true;
                gcLocked.OptionsColumn.AllowEdit = true;
                gcLocked.OptionsColumn.ReadOnly = false;
                gcAuthorize.OptionsColumn.AllowEdit = true;
                gcAuthorize.OptionsColumn.ReadOnly = false;
            }
            else
            {
                btnSave.Enabled = false;
                gcLocked.OptionsColumn.AllowEdit = false;
                gcLocked.OptionsColumn.ReadOnly = true;
                gcAuthorize.OptionsColumn.AllowEdit = false;
                gcAuthorize.OptionsColumn.ReadOnly = true;
            }
        }

        public void updateSections(int prodMonth)
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

            if (BMEBL.get_Sections(Convert.ToString(prodMonth), TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID.ToString()) == true)
            {
                editMineoverseer1.DataSource = BMEBL.ResultsDataTable;
                editMineoverseer1.DisplayMember = "Name";
                editMineoverseer1.ValueMember = "Name";
            }
        }

        private void gcTopend_ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit editor = (sender as DevExpress.XtraEditors.CheckEdit);
            TheTopEndText = editor.Properties.GetDisplayText(editor.EditValue);

            if (activity.EditValue.ToString() == "1")
            {
                string wpDesc = Development.GetRowCellValue(Development.FocusedRowHandle, Development.Columns["description"]).ToString();

                string workplcid;
                string prmonth;
                dt.AcceptChanges();
                DataRow[] df = dt.Select("description='" + wpDesc + "'");
                prmonth = df[0]["prodmonth"].ToString();
                workplcid = df[0]["workplaceid"].ToString();

                if (TheTopEndText == "True")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.SqlStatement = "update PLANMONTH SET Topend = 'Y' WHERE prodmonth=" + prmonth + " and workplaceid='" + workplcid + "' and Plancode ='LP' ";

                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();
                }
                else
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.SqlStatement = "update PLANMONTH SET Topend = 'N' WHERE prodmonth=" + prmonth + " and workplaceid='" + workplcid + "' and Plancode ='LP' ";

                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();
                }
            }
        }

        private void gcLKD_ColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            var varplan = Sundry.GetRowCellValue(Sundry.FocusedRowHandle, Sundry.Columns["VarPlan"]);
            string VARPLAN = Convert.ToString(varplan);
            var lockplan = Sundry.GetRowCellValue(Sundry.FocusedRowHandle, Sundry.Columns["LockPlan"]);
            string LOCKPLAN = Convert.ToString(lockplan);

            if (LOCKPLAN == "0.00")
            {
                Sundry.SetRowCellValue(Sundry.FocusedRowHandle, Sundry.Columns["LockPlan"], VARPLAN);
                Sundry.SetRowCellValue(Sundry.FocusedRowHandle, Sundry.Columns["VarPlan"], lockplan);
            }
        }

        private void ucAuthLockPlan_Load(object sender, EventArgs e)
        {
          
            switch (  TUserInfo .theSecurityLevel (ProductionGlobal.TProductionGlobal.WPASMenuStructure.miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS.ItemID ))
            {
                case 0:
                case 1:  
                    gcAuthorize.OptionsColumn.AllowEdit = false;
                    gcAuthorize.OptionsColumn.ReadOnly = true;
                    gcDevAuth.OptionsColumn.AllowEdit = false;
                    gcDevAuth.OptionsColumn.ReadOnly = true;
                    gcSweepVampAuth.OptionsColumn.AllowEdit = false;
                    gcSweepVampAuth.OptionsColumn.ReadOnly = true;
                    gcSundryAuth.OptionsColumn.AllowEdit = false;
                    gcSundryAuth.OptionsColumn.ReadOnly = true;
                    break;
                case 2:
                    gcAuthorize.OptionsColumn.AllowEdit = true;
                    gcAuthorize.OptionsColumn.ReadOnly = false;
                    gcDevAuth.OptionsColumn.AllowEdit = true;
                    gcDevAuth.OptionsColumn.ReadOnly = false;
                    gcSweepVampAuth.OptionsColumn.AllowEdit = true;
                    gcSweepVampAuth.OptionsColumn.ReadOnly = false;
                    gcSundryAuth.OptionsColumn.AllowEdit = true;
                    gcSundryAuth.OptionsColumn.ReadOnly = false;
                    break;
            }

            switch (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS.ItemID ))
            {
                case 0:
                case 1: 
                    gcLocked.OptionsColumn.AllowEdit = false;
                    gcLock.OptionsColumn.ReadOnly = true;
                    gcDevLocked.OptionsColumn.AllowEdit = false;
                    gcDevLocked.OptionsColumn.ReadOnly = true;
                    gcSweepCampLock.OptionsColumn.AllowEdit = false;
                    gcSweepCampLock.OptionsColumn.ReadOnly = true;
                    gcLKD.OptionsColumn.AllowEdit = false;
                    gcLKD.OptionsColumn.ReadOnly = true;
                    break;
                case 2:
                    gcLocked.OptionsColumn.AllowEdit = true;
                    gcLock.OptionsColumn.ReadOnly = false;
                    gcDevLocked.OptionsColumn.AllowEdit = true;
                    gcDevLocked.OptionsColumn.ReadOnly = false;
                    gcSweepCampLock.OptionsColumn.AllowEdit = true;
                    gcSweepCampLock.OptionsColumn.ReadOnly = false;
                    gcLKD.OptionsColumn.AllowEdit = true;
                    gcLKD.OptionsColumn.ReadOnly = false;

                    break;
            }

            Stopping.BestFitColumns();
            //CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            //BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;

            //BMEBL.SetsystemDBTag = this.theSystemDBTag;
            //BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

            _clsAuthLockPlanData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            updateSections(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth);

            DataTable dtActivity = _clsAuthLockPlanData.GetActivity();
            editActivity.DataSource = dtActivity;
            editActivity.ValueMember = "Code";
            editActivity.DisplayMember = "Desc";

            productionmonth1.EditValue = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            gcTopend.ColumnEdit.EditValueChanged += gcTopend_ColumnEdit_EditValueChanged;
            gcLKD.ColumnEdit.EditValueChanged += gcLKD_ColumnEdit_EditValueChanged;       
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gcLockPlan.Visible = false ;
            simpleButton2.Visible = false;
            simpleButton3.Visible = false;
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).EnableUranium == true)
            {
                gridBand2.Visible = true;
                gridBand10.Visible = true;
                gridBand11.Visible = true;
            }
            else
            {
                gridBand2.Visible = false ;
                gridBand10.Visible = false;
                gridBand11.Visible = false;
            }
            editProdmonth.Text = TProductionGlobal.ProdMonthAsInt((DateTime)productionmonth1.EditValue).ToString();

            try
            {
                if (activity.EditValue.ToString() == "0")
                {
                    editAct.Text = "Stoping";
                }
                if (activity.EditValue.ToString() == "1")
                {
                    editAct.Text = "Development";
                }
                if (activity.EditValue.ToString() == "2")
                {
                    editAct.Text = "Sundry";
                }
                if (activity.EditValue.ToString() == "8")
                {
                    editAct.Text = "Sweeps & Vamps";
                }
                panelControl1.Visible = true;

                    editMoSectionID.Text = mineoverseer1.EditValue.ToString();

                gcLockPlan.Visible = true;
                rpReplanning.Visible = true;
                rpPreplanning.Visible = false;
                gcLockPlan.DataSource = "";
                dt.Clear();

                if (activity.EditValue.ToString() == "0")
                {
                    gcLockPlan.MainView = Stopping;


                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.SqlStatement = "[sp_AuthLockPlan]";
                    int prodmonth = TProductionGlobal.ProdMonthAsInt((DateTime)productionmonth1.EditValue);
                    string desc = mineoverseer1.EditValue.ToString();
                    int activ = Convert.ToInt32(activity.EditValue);

                    SqlParameter[] _paramCollection =
                                        {
                                        _dbMan.CreateParameter("@prodmonth", SqlDbType.Int, 0,TProductionGlobal.ProdMonthAsInt((DateTime)productionmonth1 .EditValue )),
                                        _dbMan.CreateParameter("@description", SqlDbType.VarChar,100 ,mineoverseer1 .EditValue.ToString ()),
                                        _dbMan.CreateParameter("@activity", SqlDbType.Int ,0 ,Convert.ToInt32(activity .EditValue) ),
                                    };

                    _dbMan.ParamCollection = _paramCollection;
                    _dbMan.ExecuteInstruction();

                    dt = _dbMan.ResultsDataTable;

                    gcLockPlan.DataSource = dt;
                }
                else if (activity.EditValue.ToString() == "1")
                {
                    gcLockPlan.MainView = Development;

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                    _dbMan.SqlStatement = "sp_AuthLockPlan";
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    int prodmonth = TProductionGlobal.ProdMonthAsInt((DateTime)productionmonth1.EditValue);
                    string desc = mineoverseer1.EditValue.ToString();
                    int activ = Convert.ToInt32(activity.EditValue);

                    SqlParameter[] _paramCollection =
                                    {
                                    _dbMan.CreateParameter("@prodmonth", SqlDbType.Int, 0,TProductionGlobal.ProdMonthAsInt((DateTime)productionmonth1 .EditValue )),
                                    _dbMan.CreateParameter("@description", SqlDbType.VarChar,100 ,mineoverseer1 .EditValue.ToString ()),
                                    _dbMan.CreateParameter("@activity", SqlDbType.Int ,0 ,Convert.ToInt32(activity .EditValue) ),
                                };

                    _dbMan.ParamCollection = _paramCollection;
                    _dbMan.ExecuteInstruction();
                    dt = _dbMan.ResultsDataTable;
                    gcLockPlan.DataSource = dt;
                }
                else if (activity.EditValue.ToString() == "2")
                {
                    gcLockPlan.MainView = Sundry;

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.SqlStatement = "[sp_AuthLockPlan]";
                    int prodmonth = TProductionGlobal.ProdMonthAsInt((DateTime)productionmonth1.EditValue);
                    string desc = mineoverseer1.EditValue.ToString();
                    int activ = Convert.ToInt32(activity.EditValue);

                    SqlParameter[] _paramCollection =
                                    {
                                    _dbMan.CreateParameter("@prodmonth", SqlDbType.Int, 0,TProductionGlobal.ProdMonthAsInt((DateTime)productionmonth1 .EditValue )),
                                    _dbMan.CreateParameter("@description", SqlDbType.VarChar,100 ,mineoverseer1 .EditValue.ToString ()),
                                    _dbMan.CreateParameter("@activity", SqlDbType.Int ,0 ,Convert.ToInt32(activity .EditValue) ),
                                };

                    _dbMan.ParamCollection = _paramCollection;
                    _dbMan.ExecuteInstruction();
                    dt = _dbMan.ResultsDataTable;
                    gcLockPlan.DataSource = dt;
                }
                else if (activity.EditValue.ToString() == "8")
                {
                    gcLockPlan.MainView = SweepsVamps;

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.SqlStatement = "[sp_AuthLockPlan]";
                    int prodmonth = TProductionGlobal.ProdMonthAsInt((DateTime)productionmonth1.EditValue);
                    string desc = mineoverseer1.EditValue.ToString();
                    int activ = Convert.ToInt32(activity.EditValue);

                    SqlParameter[] _paramCollection =
                                    {
                                    _dbMan.CreateParameter("@prodmonth", SqlDbType.Int, 0,TProductionGlobal.ProdMonthAsInt((DateTime)productionmonth1 .EditValue )),
                                    _dbMan.CreateParameter("@description", SqlDbType.VarChar,100 ,mineoverseer1 .EditValue.ToString ()),
                                    _dbMan.CreateParameter("@activity", SqlDbType.Int ,0 ,Convert.ToInt32(activity .EditValue) ),
                                };

                    _dbMan.ParamCollection = _paramCollection;
                    _dbMan.ExecuteInstruction();
                    dt = _dbMan.ResultsDataTable;
                    gcLockPlan.DataSource = dt;
                }
            }
            catch (Exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Invalid Selection", Color.Red);
            }
                                       
        }

        private void btnCancelReplanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcLockPlan.Visible = false ;
            rpPreplanning.Visible = true;
            rpReplanning.Visible = false;
            panelControl1.Visible = false;
        }

        private void productionmonth_EditValueChanged(object sender, EventArgs e)
        {
            mineoverseer1 .EditValue  = null;
            //updateSections(productionmonth1.EditValue);

            //if (productionmonth1.EditValue != null)
            //{
            //    PlanningClass.ProdMonth = TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(productionmonth1.EditValue.ToString()));
            //    setSections();
            //}
        }

        private void editProductionmonth_ValueChanged(object sender, EventArgs e)
        {
            decimal month = theNewMonth;
            String PMonth = month.ToString();
            PMonth.Substring(4, 2);
            if (Convert.ToInt32(PMonth.Substring(4, 2)) > 12)
            {
                int M = Convert.ToInt32(PMonth.Substring(0, 4));
                M++;
                PMonth = M.ToString();
                PMonth = PMonth + "01";
                spinEdit1.EditValue = Convert.ToInt32(PMonth);
            }
            else
            {
                if (Convert.ToInt32(PMonth.Substring(4, 2)) < 1)
                {
                    int M = Convert.ToInt32(PMonth.Substring(0, 4));
                    M--;
                    PMonth = M.ToString();
                    PMonth = PMonth + "12";
                    spinEdit1.EditValue = Convert.ToDecimal(PMonth);
                }
            } 
        }

        private void editProductionmonth_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit theEdit = sender as DevExpress.XtraEditors.SpinEdit;
            SetProdmonth theNewMonth = new SetProdmonth();
            decimal theProdMonth;
            theProdMonth = Convert.ToDecimal(theEdit.EditValue.ToString());
            theNewMonth.getNewProdmonth(theProdMonth);
            if (theNewMonth.getProdmonth.ToString() != "-1")
                productionmonth1.EditValue = theNewMonth.getProdmonth.ToString();            
        }

        private void editProductionmonth_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            theNewMonth = Convert.ToInt32(e.NewValue.ToString());
        }
            
        private void rpCopyGrid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Convert.ToInt32(activity.EditValue) == 0)
            {
                Stopping.OptionsSelection.MultiSelect = true;
                Stopping.SelectAll();
                Stopping.CopyToClipboard();
                Stopping.OptionsSelection.MultiSelect = false;
            }
            if (Convert.ToInt32(activity.EditValue) == 1)
            {
                Development.OptionsSelection.MultiSelect = true;
                Development.SelectAll();
                Development.CopyToClipboard();
                Development.OptionsSelection.MultiSelect = true;
            }
            if (Convert.ToInt32(activity.EditValue) == 2)
            {
                Sundry.OptionsSelection.MultiSelect = true;
                Sundry.SelectAll();
                Sundry.CopyToClipboard();
                Sundry.OptionsSelection.MultiSelect = true;
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (activity.EditValue.ToString() == "0")
            {
                Stopping.PostEditor();
                Stopping.UpdateCurrentRow();
                foreach (DataRow r in dt.Rows)
                {
                    string auth;
                    if (r["Auth"].ToString() != "")
                    {
                        if (Convert.ToBoolean(r["Auth"].ToString()) == true)
                        {
                            auth = "Y";
                        }
                        else
                        {
                            auth = "N";
                        }
                    }

                    else
                    {
                        auth = "N";
                    }
                    if (r.RowState == DataRowState.Modified)
                    {
                        MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                        _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        sb.AppendLine("update PLANMONTH SET Locked='" + r["locked"].ToString() + "',");                     
                        sb.AppendLine("                     Auth = '" + auth + "'");
   
                        sb.AppendLine("  WHERE prodmonth=" + r["prodmonth"].ToString() + " and workplaceid='" + r["workplaceid"].ToString() + "' and Plancode ='LP' and sectionid='" + r["Sectionid"].ToString() + "' ");
                        _dbMan.SqlStatement = sb.ToString();
                        _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbMan.ExecuteInstruction();
                        TUserInfo.ActionLog(resWPAS.systemTag, "LOCK PLAN STOPE", UserCurrentInfo.UserID + " : " + r["workplaceid"].ToString() + " LOCKED: " + r["locked"].ToString(), UserCurrentInfo.Connection);
                        TUserInfo.ActionLog(resWPAS.systemTag, "LOCK PLAN STOPE", UserCurrentInfo.UserID + " : " + r["workplaceid"].ToString() + " AUTH: " + r["Auth"].ToString(), UserCurrentInfo.Connection);
                    }
                }
            }            

            if (activity.EditValue.ToString() == "1")
            {
                Development.PostEditor();
               
                Development.UpdateCurrentRow();
                foreach (DataRow r in dt.Rows)
                {
                    string auth;
                    if (r["Auth"].ToString() == "" || Convert.ToBoolean(r["Auth"].ToString()) == false)
                    {
                        auth = "N";
                    }
                    else
                    {
                        auth = "Y";
                    }
                    bool locked;
                    var descript = Development.GetRowCellValue(Development.FocusedRowHandle, Development.Columns["description"]);
                    string DESCRIPT = Convert.ToString(descript);
                    var LOCKED = Development.GetRowCellValue(Development.FocusedRowHandle, Development.Columns["locked"]);
                    if (LOCKED == null || LOCKED is DBNull || LOCKED == "")
                    {
                        LOCKED = "False";
                        locked = Convert.ToBoolean(LOCKED);
                    }
                    else
                    {
                        locked = Convert.ToBoolean(LOCKED);
                    }

                    bool Lock;
                    if (r["locked"].ToString() == "")
                    {
                        Lock = false;
                    }
                    else { Lock = Convert.ToBoolean(r["locked"].ToString()); }
                    var ab10 = Development.GetRowCellValue(Development.FocusedRowHandle, Development.Columns["Topend"]);
                    string ab101 = Convert.ToString(ab10);
                    string workplcid;
                    string prmonth;
                    dt.AcceptChanges();
                    DataRow[] df = dt.Select("description='" + DESCRIPT + "'");
                    prmonth = df[0]["prodmonth"].ToString();
                    workplcid = df[0]["workplaceid"].ToString();

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.SqlStatement = "";
                    //_dbMan.SqlStatement = "update PLANMONTH SET  Auth = '" + auth + "', cmkgt = " + r["Lcmkgt"].ToString() + ", Locked=" + Convert.ToInt16(Lock).ToString() + ",metresadvance=" + r["lM"].ToString() + ",ReefAdv=" + r["Lock_On_M"].ToString() + ",WasteAdv=" + r["Lock_Off_M"].ToString() + " WHERE prodmonth=" + prmonth + " and workplaceid='" + r["Workplaceid"].ToString() + "'  and Plancode ='LP'  and sectionid='" + r["Sectionid"].ToString() + "'";
                    _dbMan.SqlStatement = "update PLANMONTH SET  Auth = '" + auth + "',Locked=" + Convert.ToInt16(Lock).ToString() + "  WHERE prodmonth=" + prmonth + " and workplaceid='" + r["Workplaceid"].ToString() + "'  and Plancode ='LP'  and sectionid='" + r["Sectionid"].ToString() + "'";
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();
                    rpReplanning.Visible = true;
                    rpPreplanning.Visible = false;
                    gcLockPlan.Visible = true;
                    TUserInfo.ActionLog(resWPAS.systemTag, "LOCK PLAN STOPE", UserCurrentInfo.UserID + " : " + r["workplaceid"].ToString() + " LOCKED: " + r["locked"].ToString(), UserCurrentInfo.Connection);
                    TUserInfo.ActionLog(resWPAS.systemTag, "LOCK PLAN STOPE", UserCurrentInfo.UserID + " : " + r["workplaceid"].ToString() + " AUTH: " + r["Auth"].ToString(), UserCurrentInfo.Connection);
                  
                }

                gcLockPlan.Visible = false;
                rpPreplanning.Visible = true;
                rpReplanning.Visible = false;
                panelControl1.Visible = false;
                //Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Development Lock Plan Data was saved successfully", Color.CornflowerBlue);
            }

            if (activity.EditValue.ToString() == "2")
            {
                Sundry.PostEditor();
                Sundry.UpdateCurrentRow();

                foreach (DataRow r in dt.Rows)
                {
                    bool Auth;
                    if (r["Auth"].ToString() == "")
                    {
                        Auth = false;
                    }
                    else { Auth = Convert.ToBoolean(r["Auth"].ToString()); }
                   
                    bool Lock;
                    if (r["Locked"].ToString() == "")
                    {
                        Lock = false;
                    }
                    else { Lock = Convert.ToBoolean(r["Locked"].ToString()); }

                    if (Convert.ToBoolean(r["hasChanged"].ToString()) == true) // only save data that changed
                    {
                        MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                        _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbMan1.SqlStatement = "";
                        _dbMan1.SqlStatement = "select * from PLANMONTH_SUNDRYMINING WHERE prodmonth=" + r["prodmonth"].ToString() + " and workplaceid='" + r["Workplaceid"].ToString() + "'  and Plancode ='LP' and SMID='" + r["SMID"].ToString() + "' and sectionid='" + r["Sectionid"].ToString() + "'";

                        _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbMan1.ExecuteInstruction();
                        DataTable SNDRY = new DataTable();
                        SNDRY.Clear();
                        SNDRY = _dbMan1.ResultsDataTable;
                        if (SNDRY.Rows.Count == 0)
                        {
                            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            _dbMan1.SqlStatement = "";
                            _dbMan.SqlStatement = " insert into PLANMONTH_SUNDRYMINING values(" + r["prodmonth"].ToString() + ",'" + r["SectionID"].ToString() + "','" + r["Workplaceid"].ToString() + "',2,'" + r["SMID"].ToString() + "','LP'," +
                                                  "   '" + r["OrgunitDay"].ToString() + "', " +
                                                  "    '" + r["OrgunitNight"].ToString() + "', '" + Convert.ToInt32(r["Units"]) + "','','',0,0.000," + Convert.ToInt16(Lock).ToString() + "," + Convert.ToInt16(Auth).ToString() + ",'') ";
                            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _dbMan.ExecuteInstruction();
                            rpReplanning.Visible = true;
                            rpPreplanning.Visible = false;
                            gcLockPlan.Visible = true;
                        }
                        else
                        {
                            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            _dbMan.SqlStatement = "";
                            _dbMan.SqlStatement = "update PLANMONTH_SUNDRYMINING SET Locked= " + Convert.ToInt16(Lock).ToString() + " , Auth= " + Convert.ToInt16(Auth).ToString() + " WHERE prodmonth=" + r["prodmonth"].ToString() + " and workplaceid='" + r["Workplaceid"].ToString() + "'  and Plancode ='LP' AND SMID= '" + r["SMID"].ToString() + "' and sectionid='" + r["Sectionid"].ToString() + "'";

                            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _dbMan.ExecuteInstruction();
                            rpReplanning.Visible = true;
                            rpPreplanning.Visible = false;
                            gcLockPlan.Visible = true;
                            panelControl1.Visible = true;
                        }
                    }
                }
            }


            if (activity.EditValue.ToString() == "8")
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (Convert.ToBoolean(r["hasChanged"].ToString()) == true) // only save data that changed
                    {
                        // test if WP was saved
                        MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                        _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbMan1.SqlStatement = "";
                        _dbMan1.SqlStatement = "select * from PlanMonth_Oldgold WHERE prodmonth=" + r["prodmonth"].ToString() + " and workplaceid='" + r["workplaceid"].ToString() + "'  and Plancode ='LP' AND OGID= '" + r["OGID"].ToString() + "' and sectionid='" + r["Sectionid"].ToString() + "'";

                        _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbMan1.ExecuteInstruction();

                        DataTable oldgold = new DataTable();
                        oldgold.Clear();
                        oldgold = _dbMan1.ResultsDataTable;
                        SweepsVamps.PostEditor();
                        SweepsVamps.UpdateCurrentRow();
                        bool Auth; 
                        if (r["Auth"].ToString() == "")
                        {
                            Auth = false;
                        }
                        else { Auth = Convert.ToBoolean(r["Auth"].ToString()); }

                        bool Lock;
                        if (r["Locked"].ToString() == "")
                        {
                            Lock = false;
                        }
                        else { Lock = Convert.ToBoolean(r["Locked"].ToString()); }

                        if (oldgold.Rows.Count == 0) // add
                        {

                            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            _dbMan.SqlStatement = "";
                            _dbMan.SqlStatement = " insert into PLANMONTH_OLDGOLD values(" + r["prodmonth"].ToString() + ",'" + r["SectionID"].ToString() + "','" + r["workplaceid"].ToString() + "',8,'LP','" + r["OGID"].ToString() + "'," +
                                                 " '" + Convert.ToInt32(r["Units"]) + "',0,0.00,0,'" + r["OrgunitDay"].ToString() + "','" + r["OrgunitAfterNoon"].ToString() + "', " +
                                                 " '" + r["OrgunitNight"].ToString() + "',0," + Convert.ToInt16(Lock).ToString() + "," + Convert.ToInt16(Auth).ToString() + ",'') ";
                            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _dbMan.ExecuteInstruction();
                        }
                        else // update
                        {
                            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            _dbMan.SqlStatement = "";
                            _dbMan.SqlStatement = "update PlanMonth_Oldgold SET Locked= " + Convert.ToInt16(Lock).ToString() + " , Auth= " + Convert.ToInt16(Auth).ToString() + " WHERE prodmonth=" + r["prodmonth"].ToString() + " and workplaceid='" + r["workplaceid"].ToString() + "'  and Plancode ='LP' AND OGID= '" + r["OGID"].ToString() + "' and sectionid='" + r["Sectionid"].ToString() + "'";

                            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _dbMan.ExecuteInstruction();
                        }
                    }                  
                }
            }
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Success", "Data Saved", System.Drawing.Color.CornflowerBlue);
            ribbonMenu.Minimized = false;
        }

        private void Sundry_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view;
            view = sender as GridView;
            object row = view.GetRow(view.FocusedRowHandle); // get the current row 
            (row as DataRowView)["hasChanged"] = 1;
        }

        private void SweepsVamps_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view;
            view = sender as GridView;
            object row = view.GetRow(view.FocusedRowHandle); // get the current row 
            (row as DataRowView)["hasChanged"] = 1;
        }

        private void Stopping_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name.ToString() == "gcLocked")
            {
                DataRow dr = Stopping.GetFocusedDataRow();
                dr["locked"] = e.Value;
                if (Convert.ToBoolean(e.Value))
                {
                    dr["Lkg"] = dr["Pkg"];
                    dr["Diffkg"] = 0;
                    dr["LCubics"] = dr["PCubics"];
                    dr["DiffCubics"] = 0;
                    dr["Lcmgt"] = dr["Pcmgt"];
                    dr["LSQM"] = dr["PSQM"];
                    dr["LOnSQM"] = dr["POnSQM"];
                    dr["LOffSQM"] = dr["POffSQM"];
                    dr["DiffSQM"] = 0;
                    dr["LUkg"] = dr["PUkg"];
                    dr["DiffUkg"] = 0;
                }
                else
                {
                    dr["Lkg"] = 0;
                    dr["Diffkg"] = Convert.ToDecimal(dr["Pkg"].ToString()) - Convert.ToDecimal(dr["Lkg"].ToString());
                    dr["LCubics"] = 0;
                    dr["DiffCubics"] = Convert.ToDecimal(dr["PCubics"].ToString()) - Convert.ToDecimal(dr["LCubics"].ToString());
                    dr["Lcmgt"] = 0;
                    dr["LSQM"] = 0;
                    dr["LOnSQM"] = 0;
                    dr["LOffSQM"] = 0;
                    dr["DiffSQM"] = Convert.ToDecimal(dr["PSQM"].ToString()) - Convert.ToDecimal(dr["LSQM"].ToString());
                    dr["LUkg"] = 0;
                    dr["DiffUkg"] = Convert.ToDecimal(dr["PUkg"].ToString()) - Convert.ToDecimal(dr["LUkg"].ToString());
                }
            }            
        }

        private void Development_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name.ToString() == "gcDevLocked")
            {
                DataRow dr = Development.GetFocusedDataRow();
                dr["locked"] = e.Value;
                if (Convert.ToBoolean(e.Value))
                {
                    //dr["Lkg"] = dr["Pkg"];
                    //dr["Diffkg"] = 0;
                    dr["Lock_On_M"] = dr["Plan_On_M"];
                    dr["diffm3"] = 0;
                   // dr["Lcmgt"] = dr["Pcmgt"];
                    //dr["LSQM"] = dr["PSQM"];
                    dr["lM"] = dr["metresadvance"];
                    dr["Lock_Off_M"] = dr["Plan_Off_M"];
                    dr["diffoffm"] = 0;
                    dr["diffm2"] = 0;
                    dr["LUkg"] = dr["PUkg"];
                    dr["DiffUkg"] = 0;
                }
                else
                {
                    //dr["Lkg"] = 0;
                    //dr["Diffkg"] = Convert.ToDecimal(dr["Pkg"].ToString()) - Convert.ToDecimal(dr["Lkg"].ToString());
                    dr["Lock_On_M"] = 0;
                    dr["diffm3"] = Convert.ToDecimal(dr["Plan_On_M"].ToString()) - Convert.ToDecimal(dr["Lock_On_M"].ToString());
                  //  dr["Lcmgt"] = 0;
                    //dr["LSQM"] = 0;
                    dr["lM"] = 0;
                    dr["Lock_Off_M"] = 0;
                    dr["diffoffm"] = Convert.ToDecimal(dr["Plan_Off_M"].ToString()) - Convert.ToDecimal(dr["Lock_Off_M"].ToString());
                    dr["diffm2"] = Convert.ToDecimal(dr["metresadvance"].ToString()) - Convert.ToDecimal(dr["lM"].ToString());
                    dr["LUkg"] = 0;
                    dr["DiffUkg"] = Convert.ToDecimal(dr["PUkg"].ToString()) - Convert.ToDecimal(dr["LUkg"].ToString());
                }
            }
        }

        private void btnCancelled_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl1.Visible = false;
        }

        private void btnSendRequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ribbonMenu_Click(object sender, EventArgs e)
        {

        }
    }
}
