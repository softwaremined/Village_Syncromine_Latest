using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;
using System.Threading;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global;
using DevExpress.XtraEditors;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.RevisedPlanning_Audit_Report
{
    public partial class ucRevisedPlanningAuditReport : ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        private RevisedPlanningAuditProperties reportSettings = new RevisedPlanningAuditProperties();
        private Report theReport = new Report();

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgRevisedPlanningAudit;
        private Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth riFromProdmonth;
        private Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth riToProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpSection;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpRevisedPlanningType;

        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iFromProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iToProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSection;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iRevisedPlanningTypes;

        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pgRevisedPlanningAudit = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riFromProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.riToProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.rpSection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rpRevisedPlanningType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.iFromProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iToProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iRevisedPlanningTypes = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgRevisedPlanningAudit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riToProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpRevisedPlanningType)).BeginInit();
            this.SuspendLayout();
            // 
            // pgRevisedPlanningAudit
            // 
            this.pgRevisedPlanningAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgRevisedPlanningAudit.Location = new System.Drawing.Point(0, 0);
            this.pgRevisedPlanningAudit.Name = "pgRevisedPlanningAudit";
            this.pgRevisedPlanningAudit.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgRevisedPlanningAudit.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riFromProdmonth,
            this.riToProdmonth,
            this.rpSection,
            this.rpRevisedPlanningType});
            this.pgRevisedPlanningAudit.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iFromProdmonth,
            this.iToProdmonth,
            this.iSection,
            this.iRevisedPlanningTypes});
            this.pgRevisedPlanningAudit.Size = new System.Drawing.Size(414, 360);
            this.pgRevisedPlanningAudit.TabIndex = 0;
            // 
            // riFromProdmonth
            // 
            this.riFromProdmonth.AutoHeight = false;
            this.riFromProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject6, "", null, null, true)});
            this.riFromProdmonth.Mask.EditMask = "yyyyMM";
            this.riFromProdmonth.Mask.IgnoreMaskBlank = false;
            this.riFromProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riFromProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riFromProdmonth.Name = "riFromProdmonth";
            // 
            // riToProdmonth
            // 
            this.riToProdmonth.AutoHeight = false;
            this.riToProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject7, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject8, "", null, null, true)});
            this.riToProdmonth.Mask.EditMask = "yyyyMM";
            this.riToProdmonth.Mask.IgnoreMaskBlank = false;
            this.riToProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riToProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riToProdmonth.Name = "riToProdmonth";
            // 
            // rpSection
            // 
            this.rpSection.AutoHeight = false;
            this.rpSection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpSection.Name = "rpSection";
            this.rpSection.NullText = "";
            // 
            // rpRevisedPlanningType
            // 
            this.rpRevisedPlanningType.AutoHeight = false;
            this.rpRevisedPlanningType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpRevisedPlanningType.Name = "rpRevisedPlanningType";
            this.rpRevisedPlanningType.NullText = "";
            // 
            // iFromProdmonth
            // 
            this.iFromProdmonth.Height = 20;
            this.iFromProdmonth.IsChildRowsLoaded = true;
            this.iFromProdmonth.Name = "iFromProdmonth";
            this.iFromProdmonth.Properties.Caption = "From Production Month";
            this.iFromProdmonth.Properties.FieldName = "FromProdmonth";
            this.iFromProdmonth.Properties.RowEdit = this.riFromProdmonth;
            // 
            // iToProdmonth
            // 
            this.iToProdmonth.Height = 20;
            this.iToProdmonth.IsChildRowsLoaded = true;
            this.iToProdmonth.Name = "iToProdmonth";
            this.iToProdmonth.Properties.Caption = "To Production Month";
            this.iToProdmonth.Properties.FieldName = "ToProdmonth";
            this.iToProdmonth.Properties.RowEdit = this.riToProdmonth;
            // 
            // iSection
            // 
            this.iSection.Name = "iSection";
            this.iSection.Properties.Caption = "Section";
            this.iSection.Properties.FieldName = "sectionid";
            this.iSection.Properties.RowEdit = this.rpSection;
            // 
            // iRevisedPlanningTypes
            // 
            this.iRevisedPlanningTypes.Name = "iRevisedPlanningTypes";
            this.iRevisedPlanningTypes.Properties.Caption = "Revised Planning Type";
            this.iRevisedPlanningTypes.Properties.FieldName = "RevisedPlanningType";
            this.iRevisedPlanningTypes.Properties.RowEdit = this.rpRevisedPlanningType;
            // 
            // ucRevisedPlanningAuditReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.White;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgRevisedPlanningAudit);
            this.Name = "ucRevisedPlanningAuditReport";
            this.Size = new System.Drawing.Size(414, 360);
            this.Load += new System.EventHandler(this.ucRevisedPlanningAuditReport_Load);
            this.Controls.SetChildIndex(this.pgRevisedPlanningAudit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgRevisedPlanningAudit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riToProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpRevisedPlanningType)).EndInit();
            this.ResumeLayout(false);
        }

        public ucRevisedPlanningAuditReport()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        public override bool prepareReport()
        {
            bool theResult;

            theReportThread = new Thread(new ParameterizedThreadStart(createReport));
            theReportThread.SetApartmentState(ApartmentState.STA);
            theReportThread.Start(reportSettings);
            theResult = true;
            return theResult;
        }

        public void createReport(Object theReportSettings)
        {
            MWDataManager.clsDataAccess _sect = new MWDataManager.clsDataAccess();
            _sect.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _sect.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _sect.queryReturnType = MWDataManager.ReturnType.DataTable;
            _sect.ResultsTableName = "sect";
            _sect.SqlStatement = "select top 1 name from section where sectionid='" + reportSettings.sectionid + "'";
            _sect.ExecuteInstruction();

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "sp_StatusDetails";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _dbMan.ExecuteInstruction();

             MWDataManager.clsDataAccess _RevisedStatus = new MWDataManager.clsDataAccess();
            _RevisedStatus.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _RevisedStatus.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure ;
            _RevisedStatus.queryReturnType = MWDataManager.ReturnType.DataTable;
            _RevisedStatus.ResultsTableName = "RevisedStatus";
            _RevisedStatus.SqlStatement = "sp_RevisedPlanningAudit";           
           
            try
            {
                SqlParameter[] _paramCollection2 = 
                {
                    _RevisedStatus.CreateParameter("@Prodmonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsInt(reportSettings .FromProdmonth)),
                    _RevisedStatus.CreateParameter("@ToProdmonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsInt(reportSettings .ToProdmonth)),                             
                    _RevisedStatus.CreateParameter("@Section", SqlDbType.VarChar, 30,reportSettings .sectionid  ),      
                    _RevisedStatus.CreateParameter("@RevisedType", SqlDbType.VarChar, 50,reportSettings .RevisedPlanningType ),                           
                };

                _RevisedStatus.ParamCollection = _paramCollection2;
                _RevisedStatus.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            DataTable changetype = new DataTable();
                
            changetype = _RevisedStatus.ResultsDataTable.DefaultView.ToTable(true, "ChangeType");
            changetype.TableName = "ChangeType";
   
             MWDataManager.clsDataAccess _RevisedStatusDetails = new MWDataManager.clsDataAccess();
             _RevisedStatusDetails.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
             _RevisedStatusDetails.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
             _RevisedStatusDetails.queryReturnType = MWDataManager.ReturnType.DataTable;
             _RevisedStatusDetails.ResultsTableName = "RevisedStatusDetails";
             _RevisedStatusDetails.SqlStatement = "sp_RevisedPlanningAudit_StatusDetail";

             try
             {
                 SqlParameter[] _paramCollection8 = 
                    {
                        _RevisedStatus.CreateParameter("@Prodmonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsInt(reportSettings .FromProdmonth)),
                        _RevisedStatus.CreateParameter("@ToProdmonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsInt(reportSettings .ToProdmonth)),
                    };

                 _RevisedStatusDetails.ParamCollection = _paramCollection8;
                 _RevisedStatusDetails.ExecuteInstruction();
             }
             catch (Exception _exception)
             {
                 throw new ApplicationException(_exception.Message, _exception);
             }

             MWDataManager.clsDataAccess _RevisedSummarybet = new MWDataManager.clsDataAccess();
             _RevisedSummarybet.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
             _RevisedSummarybet.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure  ;
             _RevisedSummarybet.queryReturnType = MWDataManager.ReturnType.DataTable;
             _RevisedSummarybet.ResultsTableName = "RevisedSummarybet";
             _RevisedSummarybet.SqlStatement = "sp_Revised_Audit_Summary";

             try
             {
                 SqlParameter[] _paramCollection4 = 
                    {
                        _RevisedStatus.CreateParameter("@Prodmonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsInt(reportSettings .FromProdmonth)),
                        _RevisedStatus.CreateParameter("@ToProdmonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsInt(reportSettings .ToProdmonth)),                             
                        _RevisedSummarybet.CreateParameter("@Section", SqlDbType.VarChar, 30,reportSettings .sectionid  ),      
                        _RevisedSummarybet.CreateParameter("@between", SqlDbType.VarChar, 50,"0" ),                           
                    };

                 _RevisedSummarybet.ParamCollection = _paramCollection4;
                 _RevisedSummarybet.ExecuteInstruction();
             }
             catch (Exception _exception)
             {
                 throw new ApplicationException(_exception.Message, _exception);
             }

             MWDataManager.clsDataAccess _RevisedSummarynotbet = new MWDataManager.clsDataAccess();
             _RevisedSummarynotbet.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
             _RevisedSummarynotbet.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
             _RevisedSummarynotbet.queryReturnType = MWDataManager.ReturnType.DataTable;
             _RevisedSummarynotbet.ResultsTableName = "RevisedSummarynotbet";
             _RevisedSummarynotbet.SqlStatement = "sp_Revised_Audit_Summary";

             try
             {

                 SqlParameter[] _paramCollection3 = 
                    {
                        _RevisedStatus.CreateParameter("@Prodmonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsInt(reportSettings .FromProdmonth)),
                        _RevisedStatus.CreateParameter("@ToProdmonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsInt(reportSettings .ToProdmonth)),
                        _RevisedSummarynotbet.CreateParameter("@Section", SqlDbType.VarChar, 30,reportSettings .sectionid  ),      
                        _RevisedSummarynotbet.CreateParameter("@between", SqlDbType.VarChar, 50,"1" ),                           
                    };

                 _RevisedSummarynotbet.ParamCollection = _paramCollection3;
                 _RevisedSummarynotbet.ExecuteInstruction();
             }
             catch (Exception _exception)
             {
                 throw new ApplicationException(_exception.Message, _exception);
             }

             DataSet RevisedAudit = new DataSet();
             RevisedAudit.Tables.Add(_sect.ResultsDataTable);
             RevisedAudit.Tables.Add(_RevisedStatus.ResultsDataTable);
             RevisedAudit.Tables.Add(_RevisedStatusDetails.ResultsDataTable);
             RevisedAudit.Tables.Add(_RevisedSummarybet.ResultsDataTable);
             RevisedAudit.Tables.Add(_RevisedSummarynotbet.ResultsDataTable);
             RevisedAudit.Tables.Add(changetype);
             theReport.RegisterData(RevisedAudit);
             theReport.Load(TGlobalItems.ReportsFolder + "\\Revised_Audit_Report.frx");

             theReport.SetParameterValue("logo", TGlobalItems.ClientLogo);
             theReport.SetParameterValue("logoMineware", TGlobalItems.CompanyLogo);
             theReport.SetParameterValue("type", reportSettings .RevisedPlanningType );

            //theReport.Design();


            if (TParameters.DesignReport)
                 theReport.Design();
             theReport.Prepare();

             ActiveReport.SetReport = theReport;
             ActiveReport.isDone = true;
        }

        private void ucRevisedPlanningAuditReport_Load(object sender, EventArgs e)
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            reportSettings.FromProdmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            reportSettings.ToProdmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString()); 
            
            DataTable section = new DataTable();

            if (BMEBL.GetPlanSectionsAndNameADO(TProductionGlobal.ProdMonthAsString(reportSettings.FromProdmonth)) == true)
            {  
                section = BMEBL.ResultsDataTable;
                rpSection.DataSource = BMEBL.ResultsDataTable;
                rpSection.DisplayMember = "NAME";
                rpSection.ValueMember = "sectionid";
            }
            reportSettings.sectionid = section.Rows[0]["sectionid"].ToString();
            reportSettings .RevisedPlanningType ="All";

            MWDataManager.clsDataAccess _RevisedOptions = new MWDataManager.clsDataAccess();

            _RevisedOptions.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _RevisedOptions.SqlStatement = "SELECT 0 Code, 'All' [RevisedPlanningType] " +
                                           "UNION " +
                                           "SELECT 1 Code, 'Stop Workplace' [RevisedPlanningType] " +
                                           "union " +
                                           "select 2 Code, 'New Workplace' [RevisedPlanningType] " +
                                           "union " +
                                           "select 3 Code, 'Crew Miner Changes' [RevisedPlanningType] " +
                                           "union " +
                                           "select 4 Code, 'Call Changes' [RevisedPlanningType] " +
                                           "union " +
                                           "select 5 Code, 'Move Planning' [RevisedPlanningType] " +
                                           "union " +
                                           "select 6 Code, 'Start Workplace' [RevisedPlanningType] " +
                                           "union " +
                                           "select 7 Code, 'Mining Method Change' [RevisedPlanningType] " +
                                           "union " +
                                           "select 8 Code, 'Drill Rig Change' [RevisedPlanningType] " +
                                           "union " +
                                           "select 9 Code, 'Delete Planning' [RevisedPlanningType] ";

            _RevisedOptions.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;

            _RevisedOptions.queryReturnType = MWDataManager.ReturnType.DataTable;
            _RevisedOptions.ExecuteInstruction();
            rpRevisedPlanningType.DataSource = _RevisedOptions.ResultsDataTable;
            rpRevisedPlanningType.DisplayMember = "RevisedPlanningType";
            rpRevisedPlanningType.ValueMember = "RevisedPlanningType";
            pgRevisedPlanningAudit.SelectedObject = reportSettings;
        }
    }
}
