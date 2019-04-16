using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning.SundryMiningMeasurements;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Planning.SundryMiningMeasurements
{
    public partial class ucSundryMiningMeasurements : Mineware.Systems.Global.ucBaseUserControl
    {
         DataTable dt = new DataTable();
        public ucSundryMiningMeasurements()
        {
            InitializeComponent();
        }

        private void MainGrid_Click(object sender, EventArgs e)
        {

        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rpReplanning.Visible = true ;
            rpPreplanning.Visible = false;
            MainGrid.Visible = true ;
            panelControl1.Visible = true;
            LoadOrgUnits(prodmonth1.EditValue.ToString(), section.EditValue.ToString());
            LoadMinerList(prodmonth1.EditValue.ToString(), section.EditValue.ToString());

            editProdmonth.Text = prodmonth1.EditValue.ToString();
            MWDataManager.clsDataAccess _MOSection = new MWDataManager.clsDataAccess();
            _MOSection.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MOSection.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MOSection.queryReturnType = MWDataManager.ReturnType.DataTable;
            _MOSection.SqlStatement = "SELECT DISTINCT NAME_2,Name_2 FROM dbo.SECTION_COMPLETE " +
                                      "WHERE PRODMONTH = '" + prodmonth1.EditValue.ToString() + "' AND " +
                                      "SECTIONID_2 = '" + section .EditValue .ToString () + "'";
            _MOSection.ExecuteInstruction();
            editMoSectionID.Text = section.EditValue.ToString();
            foreach (DataRow r in _MOSection.ResultsDataTable.Rows)
            {             
                editMoSection.Text = r["Name_2"].ToString();
            }

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.SqlStatement = "[SP_SundryMiningMeasurements]";
            int prodmonth = Convert.ToInt32(prodmonth1.EditValue);
            string desc = section.EditValue.ToString();
            int activ = Convert.ToInt32(activity.EditValue);

            SqlParameter[] _paramCollection = 
            {
                _dbMan.CreateParameter("@prodmonth", SqlDbType.Int, 0,Convert.ToInt32(prodmonth )),
                _dbMan.CreateParameter("@sectionid", SqlDbType.VarChar,50 ,section.EditValue.ToString()),                                   
            };

            _dbMan.ParamCollection = _paramCollection;
            _dbMan.ExecuteInstruction();
           
            dt = _dbMan.ResultsDataTable;
            MainGrid.DataSource = dt;           
        }

        private void setSections()
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

            if (BMEBL.get_Sections(Convert.ToString(prodmonth1.EditValue), TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID.ToString()) == true)
            {
                editSection.DataSource = BMEBL.ResultsDataTable;
                editSection.DisplayMember = "Name";
                editSection.ValueMember = "SectionID";
            }
        }

        private void LoadSundryMiningDescription()
        {
            CPMBusinessLayer.clsBusinessLayer bl = new CPMBusinessLayer.clsBusinessLayer();
            bl.SetsystemDBTag = this.theSystemDBTag;
            bl.SetUserCurrentInfo = this.UserCurrentInfo;
            SundryMining.DataSource = bl.getSundryMiningActivity(2);
            SundryMining.DisplayMember = "SMDescription";
            SundryMining.ValueMember = "SMDescription";
        }

        private void LoadMiningMethod()
        {
            CPMBusinessLayer.clsBusinessLayer bl = new CPMBusinessLayer.clsBusinessLayer();
            bl.SetsystemDBTag = this.theSystemDBTag;
            bl.SetUserCurrentInfo = this.UserCurrentInfo;
            reActivity.DataSource = bl.getMiningMethods(2);         
            reActivity.DisplayMember = "Description";
            reActivity.ValueMember = "TargetID";
        }

        public void LoadOrgUnits(string prodMonth, string sectionidMO)
        {
            MWDataManager.clsDataAccess _OrgUnitsData = new MWDataManager.clsDataAccess();
            _OrgUnitsData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _OrgUnitsData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _OrgUnitsData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _OrgUnitsData.SqlStatement = "SELECT 1 thepos,'' Crew_Org " +
                                         " UNION " +
                                         " SELECT 2 thepos,'Contractor' Crew_Org" +
                                         " UNION " +
                                         "SELECT 3 thepos,Crew_Org FROM " + TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Bonus_Database + "dbo.Production_Orgunit_View  WHERE MO_SectionID = '" + sectionidMO + "' ";
            _OrgUnitsData.ExecuteInstruction();
            reOrgDaySelection.DataSource = _OrgUnitsData.ResultsDataTable;
            reOrgDaySelection.DisplayMember = "Crew_Org";
            reOrgDaySelection.ValueMember = "Crew_Org";

            reOrgNightSelection.DataSource = _OrgUnitsData.ResultsDataTable;
            reOrgNightSelection.DisplayMember = "Crew_Org";
            reOrgNightSelection.ValueMember = "Crew_Org";
        }

        public void LoadMinerList(string prodMonth, string sectionidMO)
        {
            MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
            _MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;

            _MinerData.SqlStatement = "select distinct a.SECTIONID, Name from section_complete a inner join SECCAL b on " +
                                      "A.PRODMONTH = B.Prodmonth AND " +
                                      "A.SECTIONID_1 = B.SectionID " +
                                      "where A.prodmonth = '" + prodMonth + "' and a.SECTIONID_2 = '" + sectionidMO + "' ORDER BY a.Name";

            _MinerData.ExecuteInstruction();
            reMinerSelection.DataSource = _MinerData.ResultsDataTable;
            reMinerSelection.DisplayMember = "Name";
            reMinerSelection.ValueMember = "SECTIONID";
        }        

        private void editProductionmonth_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit theEdit = sender as DevExpress.XtraEditors.SpinEdit;
            SetProdmonth theNewMonth = new SetProdmonth();
            decimal theProdMonth;
            theProdMonth = Convert.ToDecimal(theEdit.EditValue.ToString());
            theNewMonth.getNewProdmonth(theProdMonth);
            if (theNewMonth.getProdmonth.ToString() != "-1")
                prodmonth1.EditValue = theNewMonth.getProdmonth.ToString();             
        }

        private void ucSundryMiningMeasurements_Load(object sender, EventArgs e)
        {
            prodmonth1.EditValue = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth;
            setSections();
            LoadSundryMiningDescription();
            LoadMiningMethod();
            rpPreplanning.Visible = true;
            rpReplanning.Visible = false;         
            MainGrid.Visible = false;
            panelControl1.Visible = false ;
        }
          

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewSundryMiningMeasurements.FocusedRowHandle = viewSundryMiningMeasurements.GetVisibleRowHandle(0);           

            foreach (DataRow r in dt.Rows )
            {
                //if (Convert.ToBoolean(r["hasChanged"].ToString()) == true)
                //{
                    MWDataManager.clsDataAccess _MinerData = new MWDataManager.clsDataAccess();
                    _MinerData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _MinerData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _MinerData.queryReturnType = MWDataManager.ReturnType.DataTable;

                    _MinerData.SqlStatement = "  delete from SUNDRYMINING_MEASUREMENT where prodmonth=" + Convert.ToInt32(prodmonth1.EditValue) + 
                                              " and sectionid='" + r["SectionID"].ToString() + "'"+
                                              " and workplaceid='" + r["WorkplaceID"].ToString() +"'"+
                                              " and smid=" + Convert.ToInt32(r["SMID"]) +
                                              " and OrgunitDay= '" + r["OrgunitDay"].ToString() + "'";
                    _MinerData.ExecuteInstruction();

                    _MinerData.SqlStatement = "insert into SUNDRYMINING_MEASUREMENT values(" + Convert.ToInt32(prodmonth1.EditValue) + ",'" 
                                               + r["SectionID"].ToString() + "','" 
                                               + r["WorkplaceID"].ToString() + "',2," 
                                               + Convert.ToInt32(r["SMID"]) + ",'" 
                                               + r["OrgunitDay"].ToString() + "',"
                                               + Convert.ToDecimal(r["MesuredUnits"]) + ","
                                               + Convert.ToDecimal(r["MeasuredMeters"]) + ")";
                    _MinerData.ExecuteInstruction();
                //}
            }

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Sundry Mining Measurement Data was saved successfully", Color.CornflowerBlue);

            MainGrid.Visible = false;
            panelControl1.Visible = false;
            rpPreplanning.Visible = true;
            rpReplanning.Visible = false;
        }

        private void viewSundryMiningMeasurements_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view;
            view = sender as GridView;
            object row = view.GetRow(view.FocusedRowHandle); // get the current row 
            (row as DataRowView)["hasChanged"] = 1;
        }

        private void rpCopyGrid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            viewSundryMiningMeasurements.SelectAll();
            viewSundryMiningMeasurements.CopyToClipboard();            
        }

        private void btnCancelReplanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MainGrid.Visible = false;
            panelControl1.Visible = false;
            rpPreplanning.Visible = true;
            rpReplanning.Visible = false;
        }
    }
}
