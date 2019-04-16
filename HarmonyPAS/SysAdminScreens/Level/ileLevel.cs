using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal ;
using DevExpress.XtraGrid.Columns;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Level
{
    public partial class ileLevel : EditFormUserControl
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsLevel _clsLevel = new clsLevel();
       public  string EditID = "";
        public string theAction = "";
        public string division = "";
        public string orepass = "";
        
        public ileLevel()
        {
            InitializeComponent();
        }

        private void ileLevel_Load(object sender, EventArgs e)
        {
            _clsLevel.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);


            cmbLevelDivison.Properties.DataSource = _clsLevel.loadSubDivison();
            cmbLevelDivison.Properties.DisplayMember = "DivisionCodeDescription";
            cmbLevelDivison.Properties.ValueMember = "DivisionCode";
            //this.cmbLevelDivison.Text = "";

            Load_LevelOrePass();

            LVLReefTypeCb.Properties.DataSource = _clsLevel.loadReef();
            LVLReefTypeCb.Properties.DisplayMember = "Reef";
            LVLReefTypeCb.Properties.ValueMember = "ReefID";

         
        }


        public void Load_LevelOrePass()
        {
            _clsLevel.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            LVLOPassCb.Properties.DataSource = _clsLevel.Levelorepass(cmbLevelDivison .EditValue .ToString ());
            LVLOPassCb.Properties.DisplayMember = "Orepass";
            LVLOPassCb.Properties.ValueMember = "Ore";

            LVLOPassCb.EditValue = orepass;
        }
        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        public string ExtractAfterColon(string TheString)
        {
            string AfterColon;

            int index = TheString.IndexOf(":"); // Kry die postion van die :

            AfterColon = TheString.Substring(index + 1); // kry alles na :

            return AfterColon;
        }

        public string ExtractBeforeColon(string TheString)
        {
            if (TheString != "")
            {
                string BeforeColon;

                int index = TheString.IndexOf(":");

                BeforeColon = TheString.Substring(0, index);

                return BeforeColon;
            }
            else
            {
                return "";
            }
        }

        private void oreLeveltxt_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void cmbLevelDivison_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbLevelDivison.ItemIndex  != -1)
            {
                if (TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).IsCentralizedDatabase.ToString ()== "1")
                {
                    //  GridMine = "";
                    //MWDataManager.clsDataAccess _dbManA = new MWDataManager.clsDataAccess();
                    //  _dbManA.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                    //_dbManA.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    //_dbManA.queryReturnType = MWDataManager.ReturnType.DataTable;
                    
                    //_dbManA.SqlStatement = " select mine from Code_WPDivision where DivisionCode =  '" + cmbLevelDivison.EditValue .ToString () + "' ";
                    //_dbManA.ExecuteInstruction();
                    //DataTable dtA = _dbManA.ResultsDataTable;
                   // if (dtA.Rows.Count > 0)
                       // GridMine = dtA.Rows[0]["mine"].ToString();
                }
            }
            Load_LevelOrePass();
        }

        private void LVLOPassCb_EditValueChanged(object sender, EventArgs e)
        {
            if(theAction =="")
            {
                OreFlowDesctxt.Text = oreLeveltxt.Text + " Level to " + ExtractAfterColon(LVLOPassCb.Text); //edit.Rows[0]["lvlname"].ToString();
            }
        }

        private void LVLOPassCb_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
           
        }

        private void LVLOPassCb_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
        
        }

        private void oreLeveltxt_EditValueChanged(object sender, EventArgs e)
        {
            OreFlowDesctxt.Text = oreLeveltxt.Text;
           
            //OreFlowDesctxt.Text = oreLeveltxt.Text + " Level " + procs.ExtractAfterColon(LVLReefTypeCb.Text);
            if (LVLOPassCb.ItemIndex != -1)
                OreFlowDesctxt.Text = oreLeveltxt.Text + " Level to " + EditID ;
        }

        public  void btnUpdate_Click(object sender, EventArgs e)
        {
        
            _clsLevel.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
         
            DataTable gridedit = new DataTable();
         
            foreach (GridColumn column in gvLevelEdit  .VisibleColumns)
            {
                gridedit.Columns.Add(column.FieldName, column.ColumnType);
            }
            for (int i = 0; i < gvLevelEdit.DataRowCount; i++)
            {
                DataRow row = gridedit.NewRow();
                foreach (GridColumn column in gvLevelEdit.VisibleColumns)
                {
                    row[column.FieldName] = gvLevelEdit.GetRowCellValue(i, column);
                }
                gridedit.Rows.Add(row);
            }

            _clsLevel.saveData(cmbLevelDivison.EditValue.ToString(), LVLOPassCb.EditValue.ToString(), LVLReefTypeCb.EditValue.ToString(), Convert.ToBoolean(OreInActivecbx.EditValue), Convert.ToBoolean(OreCrossTramcbx.EditValue), theAction , OreFlowIDtxt.Text, OreFlowDesctxt.Text, oreLeveltxt.Text, txtCostAreaNumber.Text, OreHopperFactortxt.Text, gridedit);
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Level data Saved", Color.CornflowerBlue);
        }


    
    }
}
