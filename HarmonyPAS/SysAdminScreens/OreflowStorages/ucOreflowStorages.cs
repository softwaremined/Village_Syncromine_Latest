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
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.OreflowStorages
{
    public partial class ucOreflowStorages : ucBaseUserControl
    {
        clsOreflowStoragesData _clsOreflowStoragesData = new clsOreflowStoragesData();
        DataTable dtMillingCapture;
        string MillMonth = "";
        int shaftStoragesTonsBegin = 0;
        int shaftStoragesContentBegin = 0;
        int shaftStoragesTonsEnd = 0;
        int shaftStoragesContentEnd = 0;
        int stockPilesTonsBegin = 0;
        int stockPilesContentBegin = 0;
        int stockPilesTonsEnd = 0;
        int stockPilesContentEnd = 0;
        int stockPilesPlantTonsBegin = 0;
        int stockPilesPlantContentBegin = 0;
        int stockPilesPlantTonsEnd = 0;
        int stockPilesPlantContentEnd = 0;
        int railwayBinsTonsBegin = 0;
        int railwayBinsContentBegin = 0;
        int railwayBinsTonsEnd = 0;
        int railwayBinsContentEnd = 0;
        int millBinsTonsBegin = 0;
        int millBinsContentBegin = 0;
        int millBinsTonsEnd = 0;
        int millBinsContentEnd = 0;
        int sludgeTons = 0;
        int sludgeContent = 0;
        int surfaceSourcesTons = 0;
        int surfaceSourcesContent = 0;
        int wasteDumpsTons = 0;
        int wasteDumpsContent = 0;
        int slimesDamTons = 0;
        int slimesDamContent = 0;
        int reefExSortingTons = 0;
        int reefExSortingContent = 0;
        int wasteWashingTons = 0;
        int wasteWashingContent = 0;
        int flushingTons = 0;
        int flushingContent = 0;
        int addSourcesToMillTons = 0;
        int addSourcesToMillContent = 0;
        int totalTonsMilled = 0;
        int totalContentMilled = 0;
        int goldRecovered = 0;
        int reefBallastReclaimedTons = 0;
        int reefBallastReclaimedContent = 0;
        int residue = 0;
        int beltTons = 0;
        decimal beltValue = 0.00M;
        string ugTransferOne = "";
        int ugTOneTons = 0;
        int ugTOneContent = 0;
        int pResidue = 0;
        int pFlushingTons = 0;
        int pFlushingContent = 0;
        int pWasteTonsToMill = 0;
        int pDiscrepency = 0;
        int pMCF = 0;
        int pPRF = 0;
                int bPResidue = 0;
                int bPFlushingTons = 0;
                int bPFlushingContent = 0;
                int bPWasteTonsToMill = 0;
                int bPDiscrepency = 0;
                int bPMCF = 0;
                int bPPRF = 0;

        public ucOreflowStorages()
        {
            InitializeComponent();
        }

        private void ucOreflowStorages_Load(object sender, EventArgs e)
        {
            gcActual.Visible = false;
            gcPlanned.Visible = false;
            gcBusinessPlan.Visible = false;

            LoadScreenData();
        }

        void LoadSurveyStorages()
        {
            DataTable dtSurveyStorages = _clsOreflowStoragesData.GetSurveyStoragesData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())),
                                       lueMill.EditValue.ToString());
            if (dtSurveyStorages.Rows.Count != 0)
            {
                shaftStoragesTonsBegin = Convert.ToInt32(dtSurveyStorages.Rows[0]["ShaftStoragesTonsBegin"]);
                txtShaftStoragesBeginTons.Text = shaftStoragesTonsBegin.ToString();
                shaftStoragesContentBegin = Convert.ToInt32(dtSurveyStorages.Rows[0]["ShaftStoragesContentBegin"]);
                txtShaftStoragesBeginContent.Text = shaftStoragesContentBegin.ToString();
                shaftStoragesTonsEnd = Convert.ToInt32(dtSurveyStorages.Rows[0]["ShaftStoragesTonsEnd"]);
                txtShaftStoragesEndTons.Text = shaftStoragesTonsEnd.ToString();
                shaftStoragesContentEnd = Convert.ToInt32(dtSurveyStorages.Rows[0]["ShaftStoragesContentEnd"]);
                txtShaftStoragesEndContent.Text = shaftStoragesContentEnd.ToString();
                stockPilesTonsBegin = Convert.ToInt32(dtSurveyStorages.Rows[0]["StockPilesShaftTonsBegin"]);
                txtStockPilesBeginTons.Text = stockPilesPlantTonsBegin.ToString();
                stockPilesContentBegin = Convert.ToInt32(dtSurveyStorages.Rows[0]["StockPilesShaftContentBegin"]);
                txtStockPilesBeginContent.Text = stockPilesContentBegin.ToString();
                stockPilesTonsEnd = Convert.ToInt32(dtSurveyStorages.Rows[0]["StockPilesShaftTonsEnd"]);
                txtStockPilesEndTons.Text = stockPilesTonsEnd.ToString();
                stockPilesContentEnd = Convert.ToInt32(dtSurveyStorages.Rows[0]["StockPilesShaftContentEnd"]);
                txtStockPilesEndContent.Text = stockPilesContentEnd.ToString();
                stockPilesPlantTonsBegin = Convert.ToInt32(dtSurveyStorages.Rows[0]["StockPilesPlantTonsBegin"]);
                txtStockpileAtPlantBeginTons.Text = stockPilesPlantTonsBegin.ToString();
                stockPilesPlantContentBegin = Convert.ToInt32(dtSurveyStorages.Rows[0]["StockPilesPlantContentBegin"]);
                txtStockpileAtPlantBeginContent.Text = stockPilesPlantContentBegin.ToString();
                stockPilesPlantTonsEnd = Convert.ToInt32(dtSurveyStorages.Rows[0]["StockPilesPlantTonsEnd"]);
                txtStockpileAtPlantEndTons.Text = stockPilesPlantTonsEnd.ToString();
                stockPilesPlantContentEnd = Convert.ToInt32(dtSurveyStorages.Rows[0]["StockPilesPlantContentEnd"]);
                txtStockpileAtPlantEndContent.Text = stockPilesPlantContentEnd.ToString();
                railwayBinsTonsBegin = Convert.ToInt32(dtSurveyStorages.Rows[0]["RailwayBinsTonsBegin"]);
                txtRailwayBinsBeginTons.Text = railwayBinsTonsBegin.ToString();
                railwayBinsContentBegin = Convert.ToInt32(dtSurveyStorages.Rows[0]["RailwayBinsContentBegin"]);
                txtRailwayBinsBeginContent.Text = railwayBinsContentBegin.ToString();
                railwayBinsTonsEnd = Convert.ToInt32(dtSurveyStorages.Rows[0]["RailwayBinsTonsEnd"]);
                txtRailwayBinsEndTons.Text = railwayBinsTonsEnd.ToString();
                railwayBinsContentEnd = Convert.ToInt32(dtSurveyStorages.Rows[0]["RailwayBinsContentEnd"]);
                txtRailwayBinsEndContent.Text = railwayBinsContentEnd.ToString();
                millBinsTonsBegin = Convert.ToInt32(dtSurveyStorages.Rows[0]["MillBinsTonsBegin"]);
                txtMillBinsBeginTons.Text = millBinsTonsBegin.ToString();
                millBinsContentBegin = Convert.ToInt32(dtSurveyStorages.Rows[0]["MillBinsContentBegin"]);
                txtMillBinsBeginContent.Text = millBinsContentBegin.ToString();
                millBinsTonsEnd = Convert.ToInt32(dtSurveyStorages.Rows[0]["MillBinsTonsEnd"]);
                txtMillBinsEndTons.Text = millBinsTonsEnd.ToString();
                millBinsContentEnd = Convert.ToInt32(dtSurveyStorages.Rows[0]["MillBinsContentEnd"]);
                txtMillBinsEndContent.Text = millBinsContentEnd.ToString();
                sludgeTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["SludgeTons"]);
                txtSludgeTons.Text = sludgeTons.ToString();
                sludgeContent = Convert.ToInt32(dtSurveyStorages.Rows[0]["SludgeContent"]);
                txtSludgeContent.Text = sludgeContent.ToString();
                surfaceSourcesTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["SurfaceSourcesTons"]);
                txtSurfaceSourceTons.Text = surfaceSourcesTons.ToString();
                surfaceSourcesContent = Convert.ToInt32(dtSurveyStorages.Rows[0]["SurfaceSourcesContent"]);
                txtSurfaceSourceContent.Text = surfaceSourcesContent.ToString();
                wasteDumpsTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["WasteDumpsTons"]);
                txtWasteDumpsTons.Text = wasteDumpsTons.ToString();
                wasteDumpsContent = Convert.ToInt32(dtSurveyStorages.Rows[0]["WasteDumpsContent"]);
                txtWasteDumpsContent.Text = wasteDumpsContent.ToString();
                slimesDamTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["SlimesDamTons"]);
                txtSlimesDamsTons.Text = slimesDamTons.ToString();
                slimesDamContent = Convert.ToInt32(dtSurveyStorages.Rows[0]["SlimesDamContent"]);
                txtSlimesDamsContent.Text = slimesDamContent.ToString();
                reefExSortingTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["ReefExSortingTons"]);
                txtReefExSortingTons.Text = reefExSortingTons.ToString();
                reefExSortingContent = Convert.ToInt32(dtSurveyStorages.Rows[0]["ReefExSortingContent"]);
                txtReefExSortingContent.Text = reefExSortingContent.ToString();
                wasteWashingTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["WasteWashingsTons"]);
                txtWasteWashingsTons.Text = wasteWashingTons.ToString();
                wasteWashingContent = Convert.ToInt32(dtSurveyStorages.Rows[0]["WasteWashingsContent"]);
                txtWasteWashingsContent.Text = wasteWashingContent.ToString();
                flushingTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["FlushingTons"]);
                txtFlushingTonsTons.Text = flushingTons.ToString();
                flushingContent = Convert.ToInt32(dtSurveyStorages.Rows[0]["FlushingContent"]);
                txtFlushingTonsContent.Text = flushingContent.ToString();
                addSourcesToMillTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["AddSourcesToMillTons"]);
                txtAddSourcesToMillTons.Text = addSourcesToMillTons.ToString();
                addSourcesToMillContent = Convert.ToInt32(dtSurveyStorages.Rows[0]["AddSourcesToMillContent"]);
                txtAddSourcesToMillContent.Text = addSourcesToMillContent.ToString();
                totalTonsMilled = Convert.ToInt32(dtSurveyStorages.Rows[0]["TotalTonsMilled"]);
                txtTotalTonsMilledTons.Text = totalTonsMilled.ToString();
                totalContentMilled = Convert.ToInt32(dtSurveyStorages.Rows[0]["TotalContentMilled"]);
                txtTotalTonsMilledContent.Text = totalContentMilled.ToString();
                goldRecovered = Convert.ToInt32(dtSurveyStorages.Rows[0]["GoldRecovered"]);
                txtGoldRecovered.Text = goldRecovered.ToString();
                reefBallastReclaimedTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["ReefBallastReclaimedTons"]);
                txtReefBallastReclaimedTons.Text = reefBallastReclaimedTons.ToString();
                reefBallastReclaimedContent = Convert.ToInt32(dtSurveyStorages.Rows[0]["ReefBallastReclaimedContent"]);
                txtReefBallastReclaimedContent.Text = reefBallastReclaimedContent.ToString();
                residue = Convert.ToInt32(dtSurveyStorages.Rows[0]["Residue"]);
                txtResidue.Text = residue.ToString();
                beltTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["BeltTons"]);
                txtBeltTonsWeighted.Text = beltTons.ToString();
                beltValue = Convert.ToDecimal(dtSurveyStorages.Rows[0]["BeltValue"]);
                txtBeltValue.Text = beltValue.ToString();
                ugTransferOne = dtSurveyStorages.Rows[0]["UGTransfer1"].ToString();
                rgUGTransfer.EditValue = ugTransferOne.ToString();
                ugTOneTons = Convert.ToInt32(dtSurveyStorages.Rows[0]["UGT1Tons"]);
                txtUGTransferTons.Text = ugTOneTons.ToString();
                ugTOneContent = Convert.ToInt32(dtSurveyStorages.Rows[0]["UGT1Content"]);
                txtUGTransferContent.Text = ugTOneContent.ToString();
            }

            else if (dtSurveyStorages.Rows.Count == 0)
            {
                txtShaftStoragesBeginTons.Text = "0";
                txtShaftStoragesBeginContent.Text = "0";
                txtShaftStoragesEndTons.Text = "0";
                txtShaftStoragesEndContent.Text = "0";
                txtStockPilesBeginTons.Text = "0";
                txtStockPilesBeginContent.Text = "0";
                txtStockPilesEndTons.Text = "0";
                txtStockPilesEndContent.Text = "0";
                txtStockpileAtPlantBeginTons.Text = "0";
                txtStockpileAtPlantBeginContent.Text = "0";
                txtStockpileAtPlantEndTons.Text = "0";
                txtStockpileAtPlantEndContent.Text = "0";
                txtRailwayBinsBeginTons.Text = "0";
                txtRailwayBinsBeginContent.Text = "0";
                txtRailwayBinsEndTons.Text = "0";
                txtRailwayBinsEndContent.Text = "0";
                txtMillBinsBeginTons.Text = "0";
                txtMillBinsBeginContent.Text = "0";
                txtMillBinsEndTons.Text = "0";
                txtMillBinsEndContent.Text = "0";
                txtSludgeTons.Text = "0";
                txtSludgeContent.Text = "0";
                txtSurfaceSourceTons.Text = "0";
                txtSurfaceSourceContent.Text = "0";
                txtWasteDumpsTons.Text = "0";
                txtWasteDumpsContent.Text = "0";
                txtSlimesDamsTons.Text = "0";
                txtSlimesDamsContent.Text = "0";
                txtReefExSortingTons.Text = "0";
                txtReefExSortingContent.Text = "0";
                txtWasteWashingsTons.Text = "0";
                txtWasteWashingsContent.Text = "0";
                txtFlushingTonsTons.Text = "0";
                txtFlushingTonsContent.Text = "0";
                txtAddSourcesToMillTons.Text = "0";
                txtAddSourcesToMillContent.Text = "0";
                txtTotalTonsMilledTons.Text = "0";
                txtTotalTonsMilledContent.Text = "0";
                txtGoldRecovered.Text = "0";
                txtReefBallastReclaimedTons.Text = "0";
                txtReefBallastReclaimedContent.Text = "0";
                txtResidue.Text = "0";
                txtBeltTonsWeighted.Text = "0";
                txtBeltValue.Text = "0.00";
                rgUGTransfer.EditValue = "";
                txtUGTransferTons.Text = "0";
                txtUGTransferContent.Text = "0";
            }
        }

        void LoadPlanningStoragesData()
        {
            DataTable dtSurveyPlanningStorages = _clsOreflowStoragesData.GetPlanningStoragesData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())),
                           lueMill.EditValue.ToString());
            if (dtSurveyPlanningStorages.Rows.Count != 0)
            {
                pResidue = Convert.ToInt32(dtSurveyPlanningStorages.Rows[0]["Residue"]);
                txtPResidue.Text = pResidue.ToString();

                pFlushingTons = Convert.ToInt32(dtSurveyPlanningStorages.Rows[0]["FlushingTons"]);
                txtPTons.Text = pFlushingTons.ToString();

                pFlushingContent = Convert.ToInt32(dtSurveyPlanningStorages.Rows[0]["FlushingContent"]);
                txtPContent.Text = pFlushingContent.ToString();

                pWasteTonsToMill = Convert.ToInt32(dtSurveyPlanningStorages.Rows[0]["DevWasteTonstoMill"]);
                txtPWasteTonsToMill.Text = pWasteTonsToMill.ToString();

                pDiscrepency = Convert.ToInt32(dtSurveyPlanningStorages.Rows[0]["Discrepency"]);
                txtPDiscrepency.Text = pDiscrepency.ToString();

                pMCF = Convert.ToInt32(dtSurveyPlanningStorages.Rows[0]["MCF"]);
                txtPMCF.Text = pMCF.ToString();

                pPRF = Convert.ToInt32(dtSurveyPlanningStorages.Rows[0]["PRF"]);
                txtPPRF.Text = pPRF.ToString();
            }

            else if (dtSurveyPlanningStorages.Rows.Count == 0)
            {
                txtPTons.Text = "0";
                txtPContent.Text = "0";
                txtPResidue.Text = "0";
                txtPDiscrepency.Text = "0";
                txtPMCF.Text = "0";
                txtPPRF.Text = "0";
                txtPWasteTonsToMill.Text = "0";

            }
        }

        void LoadBusinessPlanningStoragesData()
        {
            DataTable dtSurveyBusinessPlanningStorages = _clsOreflowStoragesData.GetBusinessPlanningStoragesData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())),
                  lueMill.EditValue.ToString());
            if (dtSurveyBusinessPlanningStorages.Rows.Count != 0)
            {
                bPResidue = Convert.ToInt32(dtSurveyBusinessPlanningStorages.Rows[0]["Residue"]);
                txtBPResidue.Text = bPResidue.ToString();

                bPFlushingTons = Convert.ToInt32(dtSurveyBusinessPlanningStorages.Rows[0]["FlushingTons"]);
                txtBPTons.Text = bPFlushingTons.ToString();

                bPFlushingContent = Convert.ToInt32(dtSurveyBusinessPlanningStorages.Rows[0]["FlushingContent"]);
                txtBPContent.Text = bPFlushingContent.ToString();

                bPWasteTonsToMill = Convert.ToInt32(dtSurveyBusinessPlanningStorages.Rows[0]["DevWasteTonstoMill"]);
                txtBPWaste.Text = bPWasteTonsToMill.ToString();

                bPDiscrepency = Convert.ToInt32(dtSurveyBusinessPlanningStorages.Rows[0]["Discrepency"]);
                txtBPDiscrepency.Text = bPDiscrepency.ToString();

                bPMCF = Convert.ToInt32(dtSurveyBusinessPlanningStorages.Rows[0]["MCF"]);
                txtBPMCF.Text = bPMCF.ToString();

                bPPRF = Convert.ToInt32(dtSurveyBusinessPlanningStorages.Rows[0]["PRF"]);
                txtBPPRF.Text = bPPRF.ToString();
            }

            else if (dtSurveyBusinessPlanningStorages.Rows.Count == 0)
            {
                txtBPTons.Text = "0";
                txtBPContent.Text = "0";
                txtBPResidue.Text = "0";
                txtBPDiscrepency.Text = "0";
                txtBPMCF.Text = "0";
                txtBPPRF.Text = "0";
                txtBPWaste.Text = "0";

            }
        }


        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (lueMill.EditValue.ToString() != "")
                {
                    if (rgType.EditValue.ToString() == "Actual")
                    {
                        gcActual.Visible = true;
                        gcPlanned.Visible = false;
                        gcBusinessPlan.Visible = false;

                        LoadSurveyStorages();
                    }

                    else if (rgType.EditValue.ToString() == "Planned")
                    {
                        gcActual.Visible = false;
                        gcPlanned.Visible = true;
                        gcBusinessPlan.Visible = false;

                        LoadPlanningStoragesData();
                    }

                    else if (rgType.EditValue.ToString() == "Business Plan")
                    {
                        gcActual.Visible = false;
                        gcPlanned.Visible = false;
                        gcBusinessPlan.Visible = true;

                        LoadBusinessPlanningStoragesData();
                    }

                    else
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Please select a Type", Color.Red);
                    }

                    rpgSelection.Enabled = false;
                    rpgType.Enabled = false;
                    rpgShow.Enabled = false;
                    rpgSave.Enabled = true;
                }

                else if (lueMill.EditValue.ToString() == "")
                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Please select a Mill", Color.Red);

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
        }

        void LoadScreenData()
        {
            rpgSelection.Enabled = true;
            rpgType.Enabled = true;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;

            try
            {
                _clsOreflowStoragesData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                DataTable dtMillMonth = _clsOreflowStoragesData.GetMillMonth();

                foreach (DataRow r in dtMillMonth.Rows)
                {
                    MillMonth = r["MillMonth"].ToString();
                    lueMillMonth.EditValue = TProductionGlobal.ProdMonthAsDate(MillMonth.ToString()); ;
                }

                DataTable dtMill = _clsOreflowStoragesData.GetMill();
                rpItemMill.DataSource = dtMill;
                rpItemMill.ValueMember = "OreflowID";
                rpItemMill.DisplayMember = "Name";
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
            
        }

        void CheckErrors()
        {
            
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (rgType.EditValue.ToString() == "Actual")
            {
                try
                {
                        _clsOreflowStoragesData.SaveSurveyStoragesData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())),
                                                lueMill.EditValue.ToString(), Convert.ToInt32(txtShaftStoragesBeginTons.Text),
                                                Convert.ToInt32(txtShaftStoragesBeginContent.Text),
                                                Convert.ToInt32(txtShaftStoragesEndTons.Text),
                                                Convert.ToInt32(txtShaftStoragesEndContent.Text),
                                                Convert.ToInt32(txtStockPilesBeginTons.Text),
                                                Convert.ToInt32(txtStockPilesBeginContent.Text),
                                                Convert.ToInt32(txtStockPilesEndTons.Text),
                                                Convert.ToInt32(txtStockPilesEndContent.Text),
                                                Convert.ToInt32(txtStockpileAtPlantBeginTons.Text),
                                                Convert.ToInt32(txtStockpileAtPlantBeginContent.Text),
                                                Convert.ToInt32(txtStockpileAtPlantEndTons.Text),
                                                Convert.ToInt32(txtStockpileAtPlantEndContent.Text),
                                                Convert.ToInt32(txtRailwayBinsBeginTons.Text),
                                                Convert.ToInt32(txtRailwayBinsBeginContent.Text),
                                                Convert.ToInt32(txtRailwayBinsEndTons.Text),
                                                Convert.ToInt32(txtRailwayBinsEndContent.Text),
                                                Convert.ToInt32(txtMillBinsBeginTons.Text),
                                                Convert.ToInt32(txtMillBinsBeginContent.Text),
                                                Convert.ToInt32(txtMillBinsEndTons.Text),
                                                Convert.ToInt32(txtMillBinsEndContent.Text),
                                                Convert.ToInt32(txtSludgeTons.Text),
                                                Convert.ToInt32(txtSludgeContent.Text),
                                                Convert.ToInt32(txtSurfaceSourceTons.Text),
                                                Convert.ToInt32(txtSurfaceSourceContent.Text),
                                                Convert.ToInt32(txtWasteDumpsTons.Text),
                                                Convert.ToInt32(txtWasteDumpsContent.Text),
                                                Convert.ToInt32(txtSlimesDamsTons.Text),
                                                Convert.ToInt32(txtSlimesDamsContent.Text),
                                                Convert.ToInt32(txtReefExSortingTons.Text),
                                                Convert.ToInt32(txtReefExSortingContent.Text),
                                                Convert.ToInt32(txtWasteWashingsTons.Text),
                                                Convert.ToInt32(txtWasteWashingsContent.Text),
                                                Convert.ToInt32(txtFlushingTonsTons.Text),
                                                Convert.ToInt32(txtFlushingTonsContent.Text),
                                                Convert.ToInt32(txtAddSourcesToMillTons.Text),
                                                Convert.ToInt32(txtAddSourcesToMillContent.Text),
                                                Convert.ToInt32(txtTotalTonsMilledTons.Text),
                                                Convert.ToInt32(txtTotalTonsMilledContent.Text),
                                                Convert.ToInt32(txtGoldRecovered.Text),
                                                Convert.ToInt32(txtReefBallastReclaimedTons.Text),
                                                Convert.ToInt32(txtReefBallastReclaimedContent.Text),
                                                Convert.ToInt32(txtResidue.Text),
                                                Convert.ToInt32(txtBeltTonsWeighted.Text),
                                                Convert.ToDecimal(txtBeltValue.Text),
                                                rgUGTransfer.EditValue.ToString(), Convert.ToInt32(txtUGTransferTons.Text),
                                                Convert.ToInt32(txtUGTransferContent.Text));

                        LoadSurveyStorages();
                }
                catch (Exception _exception)
                {
                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
                }


            }

            else if (rgType.EditValue.ToString() == "Planned")
            {
                try
                {
                        _clsOreflowStoragesData.SavePlanningStoragesData(
                                                TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())),
                                                Convert.ToInt32(txtPResidue.Text),
                                                Convert.ToInt32(txtPTons.Text),
                                                Convert.ToInt32(txtPContent.Text), lueMill.EditValue.ToString(),
                                                Convert.ToInt32(txtPWasteTonsToMill.Text),
                                                Convert.ToInt32(txtPDiscrepency.Text),
                                                Convert.ToInt32(txtPMCF.Text),
                                                Convert.ToInt32(txtPPRF.Text));
                        LoadPlanningStoragesData();

                }
                catch (Exception _exception)
                {
                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
                }

            }

            else if (rgType.EditValue.ToString() == "Business Plan")
            {
                try
                {
                        _clsOreflowStoragesData.SaveBusinessPlanningStoragesData(
                        TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(lueMillMonth.EditValue.ToString())),
                        Convert.ToInt32(txtBPResidue.Text),
                        Convert.ToInt32(txtBPTons.Text),
                        Convert.ToInt32(txtBPContent.Text), lueMill.EditValue.ToString(),
                        Convert.ToInt32(txtBPWaste.Text),
                        Convert.ToInt32(txtBPDiscrepency.Text),
                        Convert.ToInt32(txtBPMCF.Text),
                        Convert.ToInt32(txtBPPRF.Text));
                        LoadBusinessPlanningStoragesData();

                }
                catch (Exception _exception)
                {
                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
                }

            }
        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcActual.Visible = false;
            gcPlanned.Visible = false;
            gcBusinessPlan.Visible = false;

            rpgSelection.Enabled = true;
            rpgType.Enabled = false;
            rpgShow.Enabled = true;
            rpgSave.Enabled = false;

            LoadScreenData();
        }

        private void txtBPTons_Leave(object sender, EventArgs e)
        {
            int bpTons;

            if (!int.TryParse(txtBPTons.Text, out bpTons))
            {
                dxOreflowStorages.SetError(txtBPTons, "Input String was not in a correct format");
                txtBPTons.Focus();
                
            }
            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
                
        }

        private void txtBPContent_Leave(object sender, EventArgs e)
        {
            int bpContent;

            if (!int.TryParse(txtBPContent.Text, out bpContent))
            {
                dxOreflowStorages.SetError(txtBPContent, "Input String was not in a correct format");
                txtBPContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtBPResidue_Leave(object sender, EventArgs e)
        {
            int bpResidue;

            if (!int.TryParse(txtBPResidue.Text, out bpResidue))
            {
                dxOreflowStorages.SetError(txtBPResidue, "Input String was not in a correct format");
                txtBPResidue.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtBPDiscrepency_Leave(object sender, EventArgs e)
        {
            int bpDiscrepency;

            if (!int.TryParse(txtBPDiscrepency.Text, out bpDiscrepency))
            {
                dxOreflowStorages.SetError(txtBPDiscrepency, "Input String was not in a correct format");
                txtBPDiscrepency.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtBPMCF_Leave(object sender, EventArgs e)
        {
            int bpMCF;

            if (!int.TryParse(txtBPMCF.Text, out bpMCF))
            {
                dxOreflowStorages.SetError(txtBPMCF, "Input String was not in a correct format");
                txtBPMCF.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtBPPRF_Leave(object sender, EventArgs e)
        {
            int bpPRF;

            if (!int.TryParse(txtBPPRF.Text, out bpPRF))
            {
                dxOreflowStorages.SetError(txtBPPRF, "Input String was not in a correct format");
                txtBPPRF.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtBPWaste_Leave(object sender, EventArgs e)
        {
            int bpWaste;

            if (!int.TryParse(txtBPWaste.Text, out bpWaste))
            {
                dxOreflowStorages.SetError(txtBPWaste, "Input String was not in a correct format");
                txtBPWaste.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtPTons_Leave(object sender, EventArgs e)
        {
            int pTons;

            if (!int.TryParse(txtPTons.Text, out pTons))
            {
                dxOreflowStorages.SetError(txtPTons, "Input String was not in a correct format");
                txtPTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }

        }

        private void txtPContent_Leave(object sender, EventArgs e)
        {
            int pContent;

            if (!int.TryParse(txtPContent.Text, out pContent))
            {
                dxOreflowStorages.SetError(txtPContent, "Input String was not in a correct format");
                txtPContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtPResidue_Leave(object sender, EventArgs e)
        {
            int pResidue;

            if (!int.TryParse(txtPResidue.Text, out pResidue))
            {
                dxOreflowStorages.SetError(txtPResidue, "Input String was not in a correct format");
                txtPResidue.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtPDiscrepency_Leave(object sender, EventArgs e)
        {
            int pDiscrepency;

            if (!int.TryParse(txtPDiscrepency.Text, out pDiscrepency))
            {
                dxOreflowStorages.SetError(txtPDiscrepency, "Input String was not in a correct format");
                txtPDiscrepency.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtPMCF_Leave(object sender, EventArgs e)
        {
            int pMCF;

            if (!int.TryParse(txtPMCF.Text, out pMCF))
            {
                dxOreflowStorages.SetError(txtPMCF, "Input String was not in a correct format");
                txtPMCF.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtPPRF_Leave(object sender, EventArgs e)
        {
            int pPRF;

            if (!int.TryParse(txtPPRF.Text, out pPRF))
            {
                dxOreflowStorages.SetError(txtPPRF, "Input String was not in a correct format");
                txtPPRF.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtPWasteTonsToMill_Leave(object sender, EventArgs e)
        {
            int pWaste;

            if (!int.TryParse(txtPWasteTonsToMill.Text, out pWaste))
            {
                dxOreflowStorages.SetError(txtPWasteTonsToMill, "Input String was not in a correct format");
                txtPWasteTonsToMill.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtShaftStoragesBeginTons_Leave(object sender, EventArgs e)
        {
            int shaftStoragesBeginTons;

            if (!int.TryParse(txtShaftStoragesBeginTons.Text, out shaftStoragesBeginTons))
            {
                dxOreflowStorages.SetError(txtShaftStoragesBeginTons, "Input String was not in a correct format");
                txtShaftStoragesBeginTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtShaftStoragesEndTons_Leave(object sender, EventArgs e)
        {
            int shaftStoragesEndTons;

            if (!int.TryParse(txtShaftStoragesEndTons.Text, out shaftStoragesEndTons))
            {
                dxOreflowStorages.SetError(txtShaftStoragesEndTons, "Input String was not in a correct format");
                txtShaftStoragesEndTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtShaftStoragesBeginContent_Leave(object sender, EventArgs e)
        {
            int shaftStoragesBeginContent;

            if (!int.TryParse(txtShaftStoragesBeginContent.Text, out shaftStoragesBeginContent))
            {
                dxOreflowStorages.SetError(txtShaftStoragesBeginContent, "Input String was not in a correct format");
                txtShaftStoragesBeginContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtShaftStoragesEndContent_Leave(object sender, EventArgs e)
        {
            int shaftStoragesEndContent;

            if (!int.TryParse(txtShaftStoragesEndContent.Text, out shaftStoragesEndContent))
            {
                dxOreflowStorages.SetError(txtShaftStoragesEndContent, "Input String was not in a correct format");
                txtShaftStoragesEndContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtStockPilesBeginTons_Leave(object sender, EventArgs e)
        {
            int stockPilesBeginTons;

            if (!int.TryParse(txtStockPilesBeginTons.Text, out stockPilesBeginTons))
            {
                dxOreflowStorages.SetError(txtStockPilesBeginTons, "Input String was not in a correct format");
                txtStockPilesBeginTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtStockPilesEndTons_Leave(object sender, EventArgs e)
        {
            int stockPilesEndTons;

            if (!int.TryParse(txtStockPilesEndTons.Text, out stockPilesEndTons))
            {
                dxOreflowStorages.SetError(txtStockPilesEndTons, "Input String was not in a correct format");
                txtStockPilesEndTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtStockPilesBeginContent_Leave(object sender, EventArgs e)
        {
            int stockPilesBeginContent;

            if (!int.TryParse(txtStockPilesBeginContent.Text, out stockPilesBeginContent))
            {
                dxOreflowStorages.SetError(txtStockPilesBeginContent, "Input String was not in a correct format");
                txtStockPilesBeginContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtStockPilesEndContent_Leave(object sender, EventArgs e)
        {
            int stockPilesEndContent;

            if (!int.TryParse(txtStockPilesEndContent.Text, out stockPilesEndContent))
            {
                dxOreflowStorages.SetError(txtStockPilesEndContent, "Input String was not in a correct format");
                txtStockPilesEndContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtStockpileAtPlantBeginTons_Leave(object sender, EventArgs e)
        {
            int stockPilesAtPlantBeginTons;

            if (!int.TryParse(txtStockpileAtPlantBeginTons.Text, out stockPilesAtPlantBeginTons))
            {
                dxOreflowStorages.SetError(txtStockpileAtPlantBeginTons, "Input String was not in a correct format");
                txtStockpileAtPlantBeginTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtStockpileAtPlantEndTons_Leave(object sender, EventArgs e)
        {
            int stockPilesAtPlantEndTons;

            if (!int.TryParse(txtStockpileAtPlantEndTons.Text, out stockPilesAtPlantEndTons))
            {
                dxOreflowStorages.SetError(txtStockpileAtPlantEndTons, "Input String was not in a correct format");
                txtStockpileAtPlantEndTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtStockpileAtPlantBeginContent_Leave(object sender, EventArgs e)
        {
            int stockPilesAtPlantBeginContent;

            if (!int.TryParse(txtStockpileAtPlantBeginContent.Text, out stockPilesAtPlantBeginContent))
            {
                dxOreflowStorages.SetError(txtStockpileAtPlantBeginContent, "Input String was not in a correct format");
                txtStockpileAtPlantBeginContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtStockpileAtPlantEndContent_Leave(object sender, EventArgs e)
        {
            int stockPilesAtPlantEndContent;

            if (!int.TryParse(txtStockpileAtPlantEndContent.Text, out stockPilesAtPlantEndContent))
            {
                dxOreflowStorages.SetError(txtStockpileAtPlantEndContent, "Input String was not in a correct format");
                txtStockpileAtPlantEndContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtRailwayBinsBeginTons_Leave(object sender, EventArgs e)
        {
            int railwayBinsBeginTons;

            if (!int.TryParse(txtRailwayBinsBeginTons.Text, out railwayBinsBeginTons))
            {
                dxOreflowStorages.SetError(txtRailwayBinsBeginTons, "Input String was not in a correct format");
                txtRailwayBinsBeginTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtRailwayBinsEndTons_Leave(object sender, EventArgs e)
        {
            int railwayBinsEndTons;

            if (!int.TryParse(txtRailwayBinsEndTons.Text, out railwayBinsEndTons))
            {
                dxOreflowStorages.SetError(txtRailwayBinsEndTons, "Input String was not in a correct format");
                txtRailwayBinsEndTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtRailwayBinsBeginContent_Leave(object sender, EventArgs e)
        {
            int railwayBinsBeginContent;

            if (!int.TryParse(txtRailwayBinsBeginContent.Text, out railwayBinsBeginContent))
            {
                dxOreflowStorages.SetError(txtRailwayBinsBeginContent, "Input String was not in a correct format");
                txtRailwayBinsBeginContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtRailwayBinsEndContent_Leave(object sender, EventArgs e)
        {
            int railwayBinsEndContent;

            if (!int.TryParse(txtRailwayBinsEndContent.Text, out railwayBinsEndContent))
            {
                dxOreflowStorages.SetError(txtRailwayBinsEndContent, "Input String was not in a correct format");
                txtRailwayBinsEndContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtMillBinsBeginTons_Leave(object sender, EventArgs e)
        {
            int railwayBinsBeginTons;

            if (!int.TryParse(txtMillBinsBeginTons.Text, out railwayBinsBeginTons))
            {
                dxOreflowStorages.SetError(txtMillBinsBeginTons, "Input String was not in a correct format");
                txtMillBinsBeginTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtMillBinsEndTons_Leave(object sender, EventArgs e)
        {
            int railwayBinsEndTons;

            if (!int.TryParse(txtMillBinsEndTons.Text, out railwayBinsEndTons))
            {
                dxOreflowStorages.SetError(txtMillBinsEndTons, "Input String was not in a correct format");
                txtMillBinsEndTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtMillBinsBeginContent_Leave(object sender, EventArgs e)
        {
            int railwayBinsBeginContent;

            if (!int.TryParse(txtMillBinsBeginContent.Text, out railwayBinsBeginContent))
            {
                dxOreflowStorages.SetError(txtMillBinsBeginContent, "Input String was not in a correct format");
                txtMillBinsBeginContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtMillBinsEndContent_Leave(object sender, EventArgs e)
        {
            int railwayBinsEndContent;

            if (!int.TryParse(txtMillBinsEndContent.Text, out railwayBinsEndContent))
            {
                dxOreflowStorages.SetError(txtMillBinsEndContent, "Input String was not in a correct format");
                txtMillBinsEndContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtSludgeTons_Leave(object sender, EventArgs e)
        {
            int sludgeTons;

            if (!int.TryParse(txtSludgeTons.Text, out sludgeTons))
            {
                dxOreflowStorages.SetError(txtSludgeTons, "Input String was not in a correct format");
                txtSludgeTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtSludgeContent_Leave(object sender, EventArgs e)
        {
            int sludgeContent;

            if (!int.TryParse(txtSludgeContent.Text, out sludgeContent))
            {
                dxOreflowStorages.SetError(txtSludgeContent, "Input String was not in a correct format");
                txtSludgeContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtSurfaceSourceTons_Leave(object sender, EventArgs e)
        {
            int surfaceSourceTons;

            if (!int.TryParse(txtSurfaceSourceTons.Text, out surfaceSourceTons))
            {
                dxOreflowStorages.SetError(txtSurfaceSourceTons, "Input String was not in a correct format");
                txtSurfaceSourceTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtSurfaceSourceContent_Leave(object sender, EventArgs e)
        {
            int surfaceSourceContent;

            if (!int.TryParse(txtSurfaceSourceContent.Text, out surfaceSourceContent))
            {
                dxOreflowStorages.SetError(txtSurfaceSourceContent, "Input String was not in a correct format");
                txtSurfaceSourceContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtWasteDumpsTons_Leave(object sender, EventArgs e)
        {
            int wasteDumpsTons;

            if (!int.TryParse(txtWasteDumpsTons.Text, out wasteDumpsTons))
            {
                dxOreflowStorages.SetError(txtWasteDumpsTons, "Input String was not in a correct format");
                txtWasteDumpsTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtWasteDumpsContent_Leave(object sender, EventArgs e)
        {
            int wasteDumpsContent;

            if (!int.TryParse(txtWasteDumpsContent.Text, out wasteDumpsContent))
            {
                dxOreflowStorages.SetError(txtWasteDumpsContent, "Input String was not in a correct format");
                txtWasteDumpsContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtSlimesDamsTons_Leave(object sender, EventArgs e)
        {
            int slimesDamsTons;

            if (!int.TryParse(txtSlimesDamsTons.Text, out slimesDamsTons))
            {
                dxOreflowStorages.SetError(txtSlimesDamsTons, "Input String was not in a correct format");
                txtSlimesDamsTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtSlimesDamsContent_Leave(object sender, EventArgs e)
        {
            int slimesDamsContent;

            if (!int.TryParse(txtSlimesDamsContent.Text, out slimesDamsContent))
            {
                dxOreflowStorages.SetError(txtSlimesDamsContent, "Input String was not in a correct format");
                txtSlimesDamsContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtReefExSortingTons_Leave(object sender, EventArgs e)
        {
            int reefExSortingTons;

            if (!int.TryParse(txtReefExSortingTons.Text, out reefExSortingTons))
            {
                dxOreflowStorages.SetError(txtReefExSortingTons, "Input String was not in a correct format");
                txtReefExSortingTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtReefExSortingContent_Leave(object sender, EventArgs e)
        {
            int reefExSortingContent;

            if (!int.TryParse(txtReefExSortingContent.Text, out reefExSortingContent))
            {
                dxOreflowStorages.SetError(txtReefExSortingContent, "Input String was not in a correct format");
                txtReefExSortingContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtWasteWashingsTons_Leave(object sender, EventArgs e)
        {
            int wasteWashingsTons;

            if (!int.TryParse(txtWasteWashingsTons.Text, out wasteWashingsTons))
            {
                dxOreflowStorages.SetError(txtWasteWashingsTons, "Input String was not in a correct format");
                txtWasteWashingsTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtWasteWashingsContent_Leave(object sender, EventArgs e)
        {
            int wasteWashingsContent;

            if (!int.TryParse(txtWasteWashingsContent.Text, out wasteWashingsContent))
            {
                dxOreflowStorages.SetError(txtWasteWashingsContent, "Input String was not in a correct format");
                txtWasteWashingsContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtFlushingTonsTons_Leave(object sender, EventArgs e)
        {
            int flushingTons;

            if (!int.TryParse(txtFlushingTonsTons.Text, out flushingTons))
            {
                dxOreflowStorages.SetError(txtFlushingTonsTons, "Input String was not in a correct format");
                txtFlushingTonsTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtFlushingTonsContent_Leave(object sender, EventArgs e)
        {
            int flushingContent;

            if (!int.TryParse(txtFlushingTonsContent.Text, out flushingContent))
            {
                dxOreflowStorages.SetError(txtFlushingTonsContent, "Input String was not in a correct format");
                txtFlushingTonsContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtAddSourcesToMillTons_Leave(object sender, EventArgs e)
        {
            int addSourcesToMillTons;

            if (!int.TryParse(txtAddSourcesToMillTons.Text, out addSourcesToMillTons))
            {
                dxOreflowStorages.SetError(txtAddSourcesToMillTons, "Input String was not in a correct format");
                txtAddSourcesToMillTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtAddSourcesToMillContent_Leave(object sender, EventArgs e)
        {
            int addSourcesToMillContent;

            if (!int.TryParse(txtAddSourcesToMillContent.Text, out addSourcesToMillContent))
            {
                dxOreflowStorages.SetError(txtAddSourcesToMillContent, "Input String was not in a correct format");
                txtAddSourcesToMillContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtTotalTonsMilledTons_Leave(object sender, EventArgs e)
        {
            int totalMilledTons;

            if (!int.TryParse(txtTotalTonsMilledTons.Text, out totalMilledTons))
            {
                dxOreflowStorages.SetError(txtTotalTonsMilledTons, "Input String was not in a correct format");
                txtTotalTonsMilledTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtTotalTonsMilledContent_Leave(object sender, EventArgs e)
        {
            int totalMilledContent;

            if (!int.TryParse(txtTotalTonsMilledContent.Text, out totalMilledContent))
            {
                dxOreflowStorages.SetError(txtTotalTonsMilledContent, "Input String was not in a correct format");
                txtTotalTonsMilledContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtReefBallastReclaimedTons_Leave(object sender, EventArgs e)
        {
            int reefBallastReclaimedTons;

            if (!int.TryParse(txtReefBallastReclaimedTons.Text, out reefBallastReclaimedTons))
            {
                dxOreflowStorages.SetError(txtReefBallastReclaimedTons, "Input String was not in a correct format");
                txtReefBallastReclaimedTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtReefBallastReclaimedContent_Leave(object sender, EventArgs e)
        {
            int reefBallastReclaimedContent;

            if (!int.TryParse(txtReefBallastReclaimedContent.Text, out reefBallastReclaimedContent))
            {
                dxOreflowStorages.SetError(txtReefBallastReclaimedContent, "Input String was not in a correct format");
                txtReefBallastReclaimedContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtGoldRecovered_Leave(object sender, EventArgs e)
        {
            int goldRecovered;

            if (!int.TryParse(txtGoldRecovered.Text, out goldRecovered))
            {
                dxOreflowStorages.SetError(txtGoldRecovered, "Input String was not in a correct format");
                txtGoldRecovered.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtResidue_Leave(object sender, EventArgs e)
        {
            int residue;

            if (!int.TryParse(txtResidue.Text, out residue))
            {
                dxOreflowStorages.SetError(txtResidue, "Input String was not in a correct format");
                txtResidue.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtBeltTonsWeighted_Leave(object sender, EventArgs e)
        {
            int beltTonsWeighted;

            if (!int.TryParse(txtBeltTonsWeighted.Text, out beltTonsWeighted))
            {
                dxOreflowStorages.SetError(txtBeltTonsWeighted, "Input String was not in a correct format");
                txtBeltTonsWeighted.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtBeltValue_Leave(object sender, EventArgs e)
        {
            decimal beltValue;

            if (!decimal.TryParse(txtBeltValue.Text, out beltValue))
            {
                dxOreflowStorages.SetError(txtBeltValue, "Input String was not in a correct format");
                txtBeltValue.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtUGTransferTons_Leave(object sender, EventArgs e)
        {
            decimal ugTransferTons;

            if (!decimal.TryParse(txtUGTransferTons.Text, out ugTransferTons))
            {
                dxOreflowStorages.SetError(txtUGTransferTons, "Input String was not in a correct format");
                txtUGTransferTons.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }

        private void txtUGTransferContent_Leave(object sender, EventArgs e)
        {
            decimal ugTransferContent;

            if (!decimal.TryParse(txtUGTransferContent.Text, out ugTransferContent))
            {
                dxOreflowStorages.SetError(txtUGTransferContent, "Input String was not in a correct format");
                txtUGTransferContent.Focus();
                
            }

            else
            {
                dxOreflowStorages.ClearErrors();
                
            }
        }
    }
}
