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
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.TrammingBoxholes
{
    public partial class ileTrammingBoxholes : EditFormUserControl
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsTrammingBoxholesData _clsTrammingBoxholesData = new clsTrammingBoxholesData();
       // ucTrammingBoxholes _ucTramming = new ucBaseUs
        public string theAction = "";
        

        public ileTrammingBoxholes()
        {
            InitializeComponent();
         
        }
       

        private void ileTrammingBoxholes_Load(object sender, EventArgs e)
        {
            _clsTrammingBoxholesData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //LoadLevel();
            


        }

        private void chkInactive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkInactive_CheckStateChanged(object sender, EventArgs e)
        {

        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            
            _clsTrammingBoxholesData.SaveData(txtOreFlowID.Text, txtBoxholeDescription.Text, txtLevelNumber.Text, 
                Convert.ToInt32(lueReefType.EditValue.ToString()), lueInactive.EditValue.ToString(), lueSectionID.EditValue.ToString(),
                Convert.ToDecimal(txtDistance.Text), theAction);

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Tramming Boxholes Data was saved", Color.CornflowerBlue);

        }

        private void lueReefType_Enter(object sender, EventArgs e)
        {
            _clsTrammingBoxholesData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtReefTypes = _clsTrammingBoxholesData.LoadReefTypes();
            lueReefType.Properties.DataSource = dtReefTypes;
            lueReefType.Properties.ValueMember = "ReefID";
            lueReefType.Properties.DisplayMember = "Description";
        }

        private void lueInactive_Enter(object sender, EventArgs e)
        {
            _clsTrammingBoxholesData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtInactive = _clsTrammingBoxholesData.LoadInact();
            lueInactive.Properties.DataSource = dtInactive;
            lueInactive.Properties.ValueMember = "Inactive";
            lueInactive.Properties.DisplayMember = "Inactive";
        }

        private void lueSectionID_Enter(object sender, EventArgs e)
        {
            _clsTrammingBoxholesData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtSections = _clsTrammingBoxholesData.LoadSections();
            lueSectionID.Properties.DataSource = dtSections;
            lueSectionID.Properties.ValueMember = "SectionID";
            lueSectionID.Properties.DisplayMember = "SectionID";
        }

        private void lueMineOverseer_Enter(object sender, EventArgs e)
        {
            _clsTrammingBoxholesData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtMONames = _clsTrammingBoxholesData.LoadMONames();
            lueMineOverseer.Properties.DataSource = dtMONames;
            lueMineOverseer.Properties.ValueMember = "Name";
            lueMineOverseer.Properties.DisplayMember = "Name";
        }

        public ileTrammingBoxholes(SimpleButton btnUpdate)
        {
            this.btnUpdate = btnUpdate;
            var temp = this.btnUpdate.GetContainerControl();
           
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("container control", temp.ToString() , Color.CornflowerBlue);

        }

        private void txtDistance_Leave(object sender, EventArgs e)
        {
            decimal distance;

            if (!decimal.TryParse(txtDistance.Text, out distance))
            {
                dxBoxholes.SetError(txtDistance, "Input String was not in a correct format");
                txtDistance.Focus();

            }

            else
            {
                dxBoxholes.ClearErrors();

            }
        }

        private void lueLevelNumber_Enter(object sender, EventArgs e)
        {
            _clsTrammingBoxholesData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            DataTable dtLevels = _clsTrammingBoxholesData.LoadLevels();
            lueLevelNumber.Properties.DataSource = dtLevels;
            lueLevelNumber.Properties.ValueMember = "LevelNumber";
            lueLevelNumber.Properties.DisplayMember = "LevelNumber";
        }

        private void lueLevelNumber_EditValueChanged(object sender, EventArgs e)
        {
            txtLevelNumber.Text = lueLevelNumber.EditValue.ToString();
        }

        private void btnUpdate_Leave(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Validated(object sender, EventArgs e)
        {
        
        }
    }
}
