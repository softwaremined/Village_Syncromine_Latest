//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using DevExpress.XtraEditors;
//using System.Windows.Forms;
//using Mineware.Systems.Global.ReportsControls;
//using Mineware.Systems.Global.sysMessages;
//using FastReport;
//using Mineware.Systems.Global;
////using Mineware.Systems.Production;
//using System.Threading;
//using Mineware.Systems.ProductionGlobal;
//using System.Text;
//using Mineware.Systems.GlobalConnect;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using FastReport;
using System.Threading;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.Global.ReportsControls;


namespace Mineware.Systems.Reports.PlanningReport
{
    public partial class ucPlanningReport : ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        public string checking;
        DataTable dtSections = new DataTable();

        Report theReport = new Report();
        private bool ErrFound;
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();


        private PlanningReportSettingsProperties reportSettings = new PlanningReportSettingsProperties();


        private DevExpress.XtraVerticalGrid.PropertyGridControl pgPlanningReportNewStyle;
        private Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth riProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpSectionID;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSection;
        //private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSection;

        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }

        public ucPlanningReport()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            this.pgPlanningReportNewStyle = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.rpSectionID = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.rpSelection = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.rpPlanType = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.rpReportType = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.rpPlanningGroup = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.rpPlanningGroupType = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSelection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iPlanType = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.IPlanTypeRadio = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.IPlanningGroupRadio = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.IplanningTypeGroup = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.WarGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pgPlanningReportNewStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSectionID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPlanType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpReportType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPlanningGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPlanningGroupType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WarGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pgPlanningReportNewStyle
            // 
            this.pgPlanningReportNewStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgPlanningReportNewStyle.Location = new System.Drawing.Point(0, 0);
            this.pgPlanningReportNewStyle.Name = "pgPlanningReportNewStyle";
            this.pgPlanningReportNewStyle.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgPlanningReportNewStyle.RecordWidth = 136;
            this.pgPlanningReportNewStyle.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpSectionID,
            this.riProdmonth,
            this.rpSelection,
            this.rpPlanType,
            this.rpReportType,
            this.rpPlanningGroup,
            this.rpPlanningGroupType});
            this.pgPlanningReportNewStyle.RowHeaderWidth = 64;
            this.pgPlanningReportNewStyle.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdmonth,
            this.iSection,
            this.iSelection,
            this.iPlanType,
            this.IPlanTypeRadio,
            this.IPlanningGroupRadio,
            this.IplanningTypeGroup});
            this.pgPlanningReportNewStyle.Size = new System.Drawing.Size(241, 311);
            this.pgPlanningReportNewStyle.TabIndex = 4;
            this.pgPlanningReportNewStyle.Click += new System.EventHandler(this.pgPlanningReportNewStyle_Click);
            // 
            // rpSectionID
            // 
            this.rpSectionID.AutoHeight = false;
            this.rpSectionID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpSectionID.Name = "rpSectionID";
            this.rpSectionID.NullText = "";
            // 
            // riProdmonth
            // 
            this.riProdmonth.AutoHeight = false;
            this.riProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2)});
            this.riProdmonth.Mask.EditMask = "yyyyMM";
            this.riProdmonth.Mask.IgnoreMaskBlank = false;
            this.riProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riProdmonth.Name = "riProdmonth";
            // 
            // rpSelection
            // 
            this.rpSelection.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Total Mine", "Total Mine"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Detail", "Detail")});
            this.rpSelection.Name = "rpSelection";
            this.rpSelection.EditValueChanged += new System.EventHandler(this.rpSelection_EditValueChanged);
            // 
            // rpPlanType
            // 
            this.rpPlanType.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Dynamic", "Dynamic"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Locked", "Locked")});
            this.rpPlanType.Name = "rpPlanType";
            // 
            // rpReportType
            // 
            this.rpReportType.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "AGA Shift Boss Planning"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Planning Style 2(new)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "Execution Schedule")});
            this.rpReportType.Name = "rpReportType";
            // 
            // rpPlanningGroup
            // 
            this.rpPlanningGroup.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "Total Mine"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Detial")});
            this.rpPlanningGroup.Name = "rpPlanningGroup";
            // 
            // rpPlanningGroupType
            // 
            this.rpPlanningGroupType.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "Dynamic"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Locked")});
            this.rpPlanningGroupType.Name = "rpPlanningGroupType";
            // 
            // iProdmonth
            // 
            this.iProdmonth.IsChildRowsLoaded = true;
            this.iProdmonth.Name = "iProdmonth";
            this.iProdmonth.Properties.Caption = "ProdMonth";
            this.iProdmonth.Properties.FieldName = "Prodmonth";
            this.iProdmonth.Properties.RowEdit = this.riProdmonth;
            // 
            // iSection
            // 
            this.iSection.IsChildRowsLoaded = true;
            this.iSection.Name = "iSection";
            this.iSection.Properties.Caption = "Section";
            this.iSection.Properties.FieldName = "SectionID";
            this.iSection.Properties.RowEdit = this.rpSectionID;
            // 
            // iSelection
            // 
            this.iSelection.Height = 18;
            this.iSelection.IsChildRowsLoaded = true;
            this.iSelection.Name = "iSelection";
            this.iSelection.Properties.Caption = "Selection";
            this.iSelection.Properties.FieldName = "Selection";
            this.iSelection.Properties.RowEdit = this.rpSelection;
            this.iSelection.Visible = false;
            // 
            // iPlanType
            // 
            this.iPlanType.IsChildRowsLoaded = true;
            this.iPlanType.Name = "iPlanType";
            this.iPlanType.Properties.Caption = "Plan Type";
            this.iPlanType.Properties.FieldName = "PlanType";
            this.iPlanType.Properties.RowEdit = this.rpPlanType;
            this.iPlanType.Visible = false;
            // 
            // IPlanTypeRadio
            // 
            this.IPlanTypeRadio.Height = 56;
            this.IPlanTypeRadio.Name = "IPlanTypeRadio";
            this.IPlanTypeRadio.Properties.Caption = "Report Type";
            this.IPlanTypeRadio.Properties.FieldName = "ReportType";
            this.IPlanTypeRadio.Properties.RowEdit = this.rpReportType;
            this.IPlanTypeRadio.Visible = false;
            // 
            // IPlanningGroupRadio
            // 
            this.IPlanningGroupRadio.Name = "IPlanningGroupRadio";
            this.IPlanningGroupRadio.Properties.Caption = " ";
            this.IPlanningGroupRadio.Properties.FieldName = "PlanningGroup";
            this.IPlanningGroupRadio.Properties.RowEdit = this.rpPlanningGroup;
            this.IPlanningGroupRadio.Visible = false;
            // 
            // IplanningTypeGroup
            // 
            this.IplanningTypeGroup.Name = "IplanningTypeGroup";
            this.IplanningTypeGroup.Properties.Caption = " ";
            this.IplanningTypeGroup.Properties.FieldName = "PlanningTypeGroup";
            this.IplanningTypeGroup.Properties.RowEdit = this.rpPlanningGroupType;
            this.IplanningTypeGroup.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 157);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(33, 25);
            this.textBox1.TabIndex = 53;
            this.textBox1.Visible = false;
            // 
            // WarGrid
            // 
            this.WarGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WarGrid.Location = new System.Drawing.Point(171, 226);
            this.WarGrid.Margin = new System.Windows.Forms.Padding(4);
            this.WarGrid.Name = "WarGrid";
            this.WarGrid.Size = new System.Drawing.Size(81, 18);
            this.WarGrid.TabIndex = 59;
            this.WarGrid.Visible = false;
            // 
            // ucPlanningReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.Controls.Add(this.WarGrid);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pgPlanningReportNewStyle);
            this.Name = "ucPlanningReport";
            this.Size = new System.Drawing.Size(241, 311);
            this.Load += new System.EventHandler(this.ucMODailyProduction_Load);
            this.Controls.SetChildIndex(this.pgPlanningReportNewStyle, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.WarGrid, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgPlanningReportNewStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSectionID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPlanType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpReportType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPlanningGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPlanningGroupType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WarGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        Procedures procs = new Procedures();
        private void LoadSections()
        {

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " Select SectionID, SectionID + ' : ' + Name Name " +
                                  "from Section s where s.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'";

            _dbMan.SqlStatement += " and Hierarchicalid  = 4 ";
            _dbMan.SqlStatement += " order by SectionID asc  ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            dtSections = _dbMan.ResultsDataTable;
            rpSectionID.DataSource = dtSections;
            rpSectionID.DisplayMember = "Name";
            rpSectionID.ValueMember = "SectionID";



            // this.gbSectionID.Text = "Section ID";
        }

        private void ucMODailyProduction_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            LoadSections();
            reportSettings.SectionID = dtSections.Rows[0][0].ToString();
            reportSettings.Selection = "Total Mine";
            reportSettings.PlanType = "Dynamic";
            reportSettings.ReportType = "0";
            reportSettings.PlanningGroup = "0";
            reportSettings.PlanningTypeGroup = "0";
            pgPlanningReportNewStyle.SelectedObject = reportSettings;
        }

        public override bool prepareReport()
        {
            bool theResult = false;

            if (CheckForErrors() == false)
            {
                theReportThread = new Thread(new ParameterizedThreadStart(createReport));
                theReportThread.SetApartmentState(ApartmentState.STA);
                theReportThread.Start(reportSettings);
                theResult = true;
            }
            return theResult;
        }

        private Boolean CheckForErrors()
        {
            Boolean haserror = false;
            if (reportSettings.SectionID == null)
            {
                _sysMessagesClass.viewMessage(MessageType.Info, "Planning Report", "Please select a Section", ButtonTypes.OK, MessageDisplayType.Small);
                haserror = true;
            }

            return haserror;
        }

        private void createReport(Object theReportSettings)
        {
            string test = reportSettings.ReportType;

            //PlanTyperadioGroup.SelectedIndex is reportSettings.ReportType
            //PlanningGroup.SelectedIndex is reportSettings.PlanningGroup
            //PlanningTypeGroup.SelectedIndex is reportSettings.PlanningTypeGroup

            if (reportSettings.ReportType == "0")
            {
                LoadPlanningAnglo();
            }
            if (reportSettings.ReportType == "1")
            {

                if (reportSettings.PlanningGroup == "0")
                {
                    if (reportSettings.PlanningTypeGroup == "0")
                    {
                        LoadTotalPlanningReportDynamic();
                    }
                    else
                    {
                        LoadTotalPlanningReportLocked();
                    }
                }
                else
                {
                    if (reportSettings.PlanningTypeGroup == "0")
                    {
                        LoadDetailPlanningReportDynamic();
                    }
                    else
                    {
                       LoadDetailPlanningReportLocked();
                    }
                }
            }

           
            
        }

        void LoadPlanningAnglo()
        {
            string prodmonth = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);

            MWDataManager.clsDataAccess _dbManBanner = new MWDataManager.clsDataAccess();
            _dbManBanner.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManBanner.SqlStatement = " select '" + SysSettings.Banner + "', '" + reportSettings.SectionID + "', '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + reportSettings.SectionID + "'  ";

            _dbManBanner.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManBanner.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManBanner.ResultsTableName = "Banner";
            _dbManBanner.ExecuteInstruction();


            DataSet DsBanner = new DataSet();
            DsBanner.Tables.Add(_dbManBanner.ResultsDataTable);

            theReport.RegisterData(DsBanner);

            theReport.Load(TGlobalItems.ReportsFolder + "\\Planning.frx");


            MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
            _dbManDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " select *, DATEPART(ISOWK,BeginDate) ww  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " , DATEPART(ISOWK,BeginDate+7) ww1   ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+14) ww2  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+21) ww3  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+28) ww4  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+35) ww5  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+42) ww6  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+49) ww7  ";






            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " from (select Min(s.BeginDate) BeginDate,MAX(s.EndDate) EndDate from SECCAL s  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " left outer join Section sc ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " on s.Sectionid = sc.SectionID ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " where s.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and sc.ReportToSectionid = '" + reportSettings.SectionID + "') a ";
            _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDate.ResultsTableName = "sysset";
            _dbManDate.ExecuteInstruction();           

            //if (_dbManDate.ResultsDataTable.Rows[0][0].ToString() == 0)
            //{
                
            //}

            TimeSpan Span;
            DateTime BeginDate;
            DateTime EndDate;
            DataTable CalDate = new DataTable();
            int Day = 0;
            int Week = 0;
            int Weekno = 0;

            try
            {
                BeginDate = Convert.ToDateTime(_dbManDate.ResultsDataTable.Rows[0][0].ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("No Section Calendars found for section : " + reportSettings.SectionID + " ");
                return;
            }

            BeginDate = Convert.ToDateTime(_dbManDate.ResultsDataTable.Rows[0][0].ToString());
            EndDate = Convert.ToDateTime(_dbManDate.ResultsDataTable.Rows[0][1].ToString());
            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][2].ToString());

            CalDate.Rows.Add();

            for (int x = 0; x <= 45; x++)
            {
                if (BeginDate.AddDays(Day) <= EndDate)
                {

                    CalDate.Columns.Add();
                    CalDate.Rows[0][x] = BeginDate.AddDays(Day).ToString("dd MMM ddd");
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Mon")
                    {
                        CalDate.Rows[0][x] = CalDate.Rows[0][x] + "                W";
                    }
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Tue")
                    {
                        CalDate.Rows[0][x] = CalDate.Rows[0][x] + "                E";
                    }
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Wed")
                    {
                        CalDate.Rows[0][x] = CalDate.Rows[0][x] + "                E";
                    }
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Thu")
                    {
                        CalDate.Rows[0][x] = CalDate.Rows[0][x] + "                K";
                    }
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Fri")
                    {
                        CalDate.Rows[0][x] = CalDate.Rows[0][x] + "               -";
                    }
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Sat")
                    {
                        // do first wwk
                        if (Weekno == 0)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][2].ToString());

                        if (Weekno == 1)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][3].ToString());

                        if (Weekno == 2)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][4].ToString());

                        if (Weekno == 3)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][5].ToString());

                        if (Weekno == 4)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][6].ToString());
                        if (Weekno == 5)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][7].ToString());
                        if (Weekno == 6)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][8].ToString());


                        // CalDate.Rows[0][x] = CalDate.Rows[0][x] + "        ";
                        if (Week >= 5000)
                        {
                            //Week = 1;

                            if (Week > 9)
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString().Substring(0, 1);

                            }
                            else
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + " " + "         0";

                            }
                        }
                        else
                        {
                            if (Week > 9)
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString().Substring(0, 1);

                            }
                            else
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + " " + "         0";

                            }
                        }



                    }

                    if (BeginDate.AddDays(Day).ToString("ddd") == "Sun")
                    {
                        if (Weekno == 0)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][2].ToString());

                        if (Weekno == 1)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][3].ToString());

                        if (Weekno == 2)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][4].ToString());

                        if (Weekno == 3)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][5].ToString());

                        if (Weekno == 4)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][6].ToString());
                        if (Weekno == 5)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][7].ToString());
                        if (Weekno == 6)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][8].ToString());

                        // CalDate.Rows[0][x] = CalDate.Rows[0][x] + "        ";
                        if (Week >= 54000)
                        {
                            // Week = 1;

                            if (Week > 9)
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString().Substring(1, 1);
                                //Week = Week + 1;

                            }
                            else
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString();
                                //Week = Week + 1;

                            }
                        }
                        else
                        {
                            if (Week > 9)
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString().Substring(1, 1);
                                // Week = Week + 1;

                            }
                            else
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString();
                                //Week = Week + 1;

                            }
                        }

                        Weekno = Weekno + 1;

                    }


                    // Weekno = Weekno + 1;

                    Day = Day + 1;


                }
                else
                {
                    CalDate.Columns.Add();
                    CalDate.Rows[0][x] = "";
                }

            }

            CalDate.Columns.Add();
            CalDate.Rows[0][CalDate.Columns.Count - 1] = Day.ToString();

            CalDate.TableName = "CalDates";
            DataSet DsCalDate = new DataSet();
            DsCalDate.Tables.Add(CalDate);

            theReport.RegisterData(DsCalDate);


            MWDataManager.clsDataAccess _dbManPlan = new MWDataManager.clsDataAccess();
            _dbManPlan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

             _dbManPlan.SqlStatement = "sp_Report_Planning";
            _dbManPlan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _dbManPlan.ResultsTableName = "Planning";
            SqlParameter[] _paramCollection =
                {
                    _dbManPlan.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, prodmonth),
                    _dbManPlan.CreateParameter("@SectionID", SqlDbType.VarChar, 20, reportSettings.SectionID.ToString()),
                 };

            _dbManPlan.ParamCollection = _paramCollection;
            _dbManPlan.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = _dbManPlan.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", theSystemDBTag, "ucPlanningeport", "LoadPlanningAnglo", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);

            }
            else
            {
                if (_dbManPlan.ResultsDataTable.Rows.Count > 0)
                {

                    DataSet DsPlanning = new DataSet();
                    DsPlanning.Tables.Add(_dbManPlan.ResultsDataTable);
                    theReport.RegisterData(DsPlanning);

                    theReport.Load(TGlobalItems.ReportsFolder + "\\Planning.frx");

                    //theReport.Design();

                    if (TParameters.DesignReport)
                    {
                        theReport.Design();
                    }

                    theReport.Prepare();
                    ActiveReport.SetReport = theReport;
                    ActiveReport.isDone = true;
                    // return;
                }
                else
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "ucPlanningReport", "No record found", ButtonTypes.OK, MessageDisplayType.Small);
                }



                //            _dbManPlan.SqlStatement = _dbManPlan.SqlStatement + "declare @Start datetime      \r\n"+  
                //"set @Start = (select      \r\n"+
                //"min(calendardate) ss from Planning v, section s , section s1, section s2      \r\n"+
                //"where v.Sectionid = s.SectionID and v.Prodmonth = s.Prodmonth      \r\n"+
                //" and s.ReportToSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n"+
                //" and s1.ReportToSectionid = s2.SectionID and s1.Prodmonth = s2.Prodmonth      \r\n"+
                //" and v.Prodmonth = '"+prodmonth+"' and s2.SectionID = '"+reportSettings.SectionID+"')         \r\n\r\n"+

                //"declare @VampingStart datetime      \r\n" +
                //"set @VampingStart = (select      \r\n" +
                //"min(calendardate) ss from Planning_vamping v, section s , section s1, section s2      \r\n" +
                //"where v.Sectionid = s.SectionID and v.Prodmonth = s.Prodmonth      \r\n" +
                //" and s.ReportToSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
                //" and s1.ReportToSectionid = s2.SectionID and s1.Prodmonth = s2.Prodmonth      \r\n" +
                //" and v.Prodmonth = '" + prodmonth + "' and s2.SectionID = '" + reportSettings.SectionID + "')         \r\n\r\n" +

                //" declare @prev integer      \r\n" +
                //" set @prev = (select max(prodmonth) aaaa from vw_planmonth where prodmonth < '" + prodmonth + "')        \r\n\r\n" +

                //" select  case when newptt is null then 'Red' when newptt is not null and newptt < @prev then 'orange' else '' end as newwpflag, *from      \r\n" +
                //"  (select '1' Line, s1.Sectionid sbid, s1.Name sbname, s.Sectionid minid, s.Name minname, pm.workplaceid, case when pm.activity = 9 then w.description + ' Ledge' else w.description end as description,      \r\n" +
                //"     case when pm.Activity = 9 then 'Ledge'      \r\n" +
                //"    when pm.Activity = 0 then 'Stope'      \r\n" +
                //"    when pm.Activity = 1 then 'Dev' End as Act,      \r\n" +
                //" case when pm.activity = 1 then MetresAdvance else 0 end as DevTotAdv,      \r\n" +
                //" case when pm.activity = 1 and w.ReefWaste = 'W' then MetresAdvance else 0 end as DevWasteAdv,      \r\n" +
                //"  case when pm.activity = 1 then Tons else 0 end as DevTons,      \r\n" +
                //"  case when  pm.Activity = 1 then kg else 0 end as DevCont,      \r\n" +
                //" case when pm.activity <> 1 then Tons else 0 end as StpTons,      \r\n" +
                //"  case when pm.activity <> 1 then kg else 0 end as StpCont,      \r\n" +
                //"     case when pm.Activity <> 1 then 'a.Stoping' else 'b.Development' end as MainGroup,      \r\n" +
                //"      case when pm.Activity <> 1 then pm.FL else 0 end AS FL, 	 case when pm.Activity <> 1 then pm.Sqm else 0 end as plansqm, 	case when pm.Activity <> 1 then	cyclesqm else pm.MonthlyTotalSQM end as cyclesqm,  cr.CrewName orgunitds, 'Std Cycle' aa, pm.MetresAdvance Metresadvance, tons, content,pm.WasteSqm  offreefsqm, 0 Budget, pm.activity,      \r\n" +
                //"    case when pm.activity <> 1 then pm.extrabudget else 0 end as extrabudget, case when pm.activity = 1 then pm.extrabudget else 0 end as extrabudget1, SUBSTRING(DefaultCycle, 1, 4) aa1,      \r\n" +
                //"      SUBSTRING(DefaultCycle, 5, 4) aa2, SUBSTRING(DefaultCycle, 9, 4) aa3, SUBSTRING(DefaultCycle, 13, 4) aa4,      \r\n" +
                //"     SUBSTRING(DefaultCycle, 17, 4) aa5, SUBSTRING(DefaultCycle, 21, 4) aa6, SUBSTRING(DefaultCycle, 25, 4) aa7, SUBSTRING(DefaultCycle, 29, 4) aa8,      \r\n" +
                //"       SUBSTRING(DefaultCycle, 33, 4) aa9, SUBSTRING(DefaultCycle, 37, 4) aa10, SUBSTRING(DefaultCycle, 41, 4) aa11, SUBSTRING(DefaultCycle, 45, 4) aa12,      \r\n" +
                //"      SUBSTRING(DefaultCycle, 49, 4) aa13, SUBSTRING(DefaultCycle, 53, 4) aa14, SUBSTRING(DefaultCycle, 57, 4) aa15, SUBSTRING(DefaultCycle, 61, 4) aa16,      \r\n" +
                //"      SUBSTRING(DefaultCycle, 65, 4) aa17, SUBSTRING(DefaultCycle, 69, 4) aa18, SUBSTRING(DefaultCycle, 73, 4) aa19, SUBSTRING(DefaultCycle, 77, 4) aa20,      \r\n" +
                //"       SUBSTRING(DefaultCycle, 81, 4) aa21, SUBSTRING(DefaultCycle, 85, 4) aa22, SUBSTRING(DefaultCycle, 89, 4) aa23, SUBSTRING(DefaultCycle, 93, 4) aa24,      \r\n" +
                //"       SUBSTRING(DefaultCycle, 97, 4) aa25, SUBSTRING(DefaultCycle, 101, 4) aa26, SUBSTRING(DefaultCycle, 105, 4) aa27, SUBSTRING(DefaultCycle, 109, 4) aa28,      \r\n" +
                //"       SUBSTRING(DefaultCycle, 113, 4) aa29, SUBSTRING(DefaultCycle, 117, 4) aa30, SUBSTRING(DefaultCycle, 121, 4) aa31, SUBSTRING(DefaultCycle, 125, 4) aa32,      \r\n" +
                //"       SUBSTRING(DefaultCycle, 129, 4) aa33, SUBSTRING(DefaultCycle, 133, 4) aa34, SUBSTRING(DefaultCycle, 137, 4) aa35, SUBSTRING(DefaultCycle, 141, 4) aa36,      \r\n" +
                //"       SUBSTRING(DefaultCycle, 145, 4) aa37, SUBSTRING(DefaultCycle, 149, 4) aa38, SUBSTRING(DefaultCycle, 153, 4) aa39, SUBSTRING(DefaultCycle, 157, 4) aa40,      \r\n" +
                //"       SUBSTRING(DefaultCycle, 161, 4) aa41, SUBSTRING(DefaultCycle, 165, 4) aa42, SUBSTRING(DefaultCycle, 169, 4) aa43, SUBSTRING(DefaultCycle, 173, 4) aa44,      \r\n" +
                //"       SUBSTRING(DefaultCycle, 177, 4) aa45, s1.ReportToSectionid+':'+s2.Name ReportToSectionid, '' Mprass, '' wpexternalid, '' Surv, pmold1 newptt       from vw_PlanMonth pm, (select w.*, pmold1 from Workplace w left outer join(select workplaceid wz, max(prodmonth) pmold1 from vw_PlanMonth      where prodmonth < '" + prodmonth + "' group by workplaceid) newwp on  w.workplaceid = newwp.wz) w, Section s, Section s1,Section s2, Workplace wt,crew cr where pm.workplaceid = w.workplaceid \r\n" +
                //" -------and w.GMSIWPID = wt.GMSIWPID      \r\n" +
                //"       and pm.Sectionid = s.SectionID and pm.Prodmonth = s.Prodmonth      \r\n" +
                //"       and s.reporttoSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
                //" and s1.reporttoSectionid = s2.SectionID and s1.Prodmonth = s2.Prodmonth   \r\n" +
                //"       and pm.prodmonth = '" + prodmonth + "'      and s1.ReportToSectionid = '"+reportSettings.SectionID+ "' and pm.Plancode = 'MP'  and pm.OrgUnitDay = cr.GangNo) a      \r\n" +
                //"       left outer join      \r\n" +
                //"       (select '2' Line, s1.Sectionid sbid, s1.Name sbname, s.Sectionid minid, s.Name minname, pm.workplaceid, w.description, pm.activity,      \r\n" +
                //"       case when pm.Activity = 9 then 'Ledge'      \r\n" +
                //"     when pm.Activity = 0 then 'Stope'      \r\n" +
                //"     when pm.Activity = 1 then 'Dev' End as Act,      \r\n" +
                //"      case when pm.Activity <> 1 then 'a.Stoping' else 'b.Development' end as MainGroup,      \r\n" +
                //"      pm.FL, pm.SqmTotal plansqm, cyclesqm, orgunitds, 'Crew Cycle' aa,      \r\n" +
                //"       SUBSTRING(MOCycle, 1, 4) a1,      \r\n" +
                //"       SUBSTRING(MOCycle, 5, 4) a2, SUBSTRING(MOCycle, 9, 4) a3, SUBSTRING(MOCycle, 13, 4) a4,      \r\n" +
                //"       SUBSTRING(MOCycle, 17, 4) a5, SUBSTRING(MOCycle, 21, 4) a6, SUBSTRING(MOCycle, 25, 4) a7, SUBSTRING(MOCycle, 29, 4) a8,      \r\n" +
                //"       SUBSTRING(MOCycle, 33, 4) a9, SUBSTRING(MOCycle, 37, 4) a10, SUBSTRING(MOCycle, 41, 4) a11, SUBSTRING(MOCycle, 45, 4) a12,      \r\n" +
                //"       SUBSTRING(MOCycle, 49, 4) a13, SUBSTRING(MOCycle, 53, 4) a14, SUBSTRING(MOCycle, 57, 4) a15, SUBSTRING(MOCycle, 61, 4) a16,      \r\n" +
                //"       SUBSTRING(MOCycle, 65, 4) a17, SUBSTRING(MOCycle, 69, 4) a18, SUBSTRING(MOCycle, 73, 4) a19, SUBSTRING(MOCycle, 77, 4) a20,      \r\n" +
                //"       SUBSTRING(MOCycle, 81, 4) a21, SUBSTRING(MOCycle, 85, 4) a22, SUBSTRING(MOCycle, 89, 4) a23, SUBSTRING(MOCycle, 93, 4) a24,      \r\n" +
                //"       SUBSTRING(MOCycle, 97, 4) a25, SUBSTRING(MOCycle, 101, 4) a26, SUBSTRING(MOCycle, 105, 4) a27, SUBSTRING(MOCycle, 109, 4) a28,      \r\n" +
                //"       SUBSTRING(MOCycle, 113, 4) a29, SUBSTRING(MOCycle, 117, 4) a30, SUBSTRING(MOCycle, 121, 4) a31, SUBSTRING(MOCycle, 125, 4) a32,      \r\n" +
                //"       SUBSTRING(MOCycle, 129, 4) a33, SUBSTRING(MOCycle, 133, 4) a34, SUBSTRING(MOCycle, 137, 4) a35, SUBSTRING(MOCycle, 141, 4) a36,      \r\n" +
                //"       SUBSTRING(MOCycle, 145, 4) a37, SUBSTRING(MOCycle, 149, 4) a38, SUBSTRING(MOCycle, 153, 4) a39, SUBSTRING(MOCycle, 157, 4) a40,      \r\n" +
                //"       SUBSTRING(MOCycle, 161, 4) a41, SUBSTRING(MOCycle, 165, 4) a42, SUBSTRING(MOCycle, 169, 4) a43, SUBSTRING(MOCycle, 173, 4) a44,      \r\n" +
                //"       SUBSTRING(MOCycle, 177, 4) a45, s1.ReportToSectionid , pm.MonthlyTotalSQM MonthSqm      from vw_PlanMonth pm, Workplace w, section s, section s1 where pm.workplaceid = w.workplaceid      \r\n" +
                //"       and pm.Sectionid = s.SectionID and pm.Prodmonth = s.Prodmonth      \r\n" +
                //"       and s.reporttoSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
                //"       and pm.prodmonth = '" + prodmonth + "'  and pm.Plancode = 'MP'    \r\n" +
                //"       and s1.ReportToSectionid = '"+reportSettings.SectionID+"') b on a.workplaceid = b.workplaceid and a.activity = b.activity and a.minid = b.minid      \r\n" +
                //"        left outer join      \r\n" +
                //"       (select '3' Line, s1.Sectionid sbid, s1.Name sbname, s.Sectionid minid, s.Name minname, pm.workplaceid, w.description,      \r\n" +
                //"      case when pm.Activity = 9 then 'Ledge'      \r\n" +
                //"     when pm.Activity = 0 then 'Stope'      \r\n" +
                //"     when pm.Activity = 1 then 'Dev' End as Act,      \r\n" +
                //"       case when pm.Activity <> 1 then 'a.Stoping' else 'b.Development' end as MainGroup,      \r\n" +
                //"       pm.FL, pm.SqmTotal plansqm, cyclesqm, orgunitds, case when pm.Activity in (0, 9) then 'Day Sqm' else 'Day M' end as aa,        \r\n" +
                //"       SUBSTRING(MOCycleNum, 1, 4) b1,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 5, 4) b2, SUBSTRING(MOCycleNum, 9, 4) b3, SUBSTRING(MOCycleNum, 13, 4) b4,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 17, 4) b5, SUBSTRING(MOCycleNum, 21, 4) b6, SUBSTRING(MOCycleNum, 25, 4) b7, SUBSTRING(MOCycleNum, 29, 4) b8,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 33, 4) b9, SUBSTRING(MOCycleNum, 37, 4) b10, SUBSTRING(MOCycleNum, 41, 4) b11, SUBSTRING(MOCycleNum, 45, 4) b12,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 49, 4) b13, SUBSTRING(MOCycleNum, 53, 4) b14, SUBSTRING(MOCycleNum, 57, 4) b15, SUBSTRING(MOCycleNum, 61, 4) b16,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 65, 4) b17, SUBSTRING(MOCycleNum, 69, 4) b18, SUBSTRING(MOCycleNum, 73, 4) b19, SUBSTRING(MOCycleNum, 77, 4) b20,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 81, 4) b21, SUBSTRING(MOCycleNum, 85, 4) b22, SUBSTRING(MOCycleNum, 89, 4) b23, SUBSTRING(MOCycleNum, 93, 4) b24,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 97, 4) b25, SUBSTRING(MOCycleNum, 101, 4) b26, SUBSTRING(MOCycleNum, 105, 4) b27, SUBSTRING(MOCycleNum, 109, 4) b28,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 113, 4) b29, SUBSTRING(MOCycleNum, 117, 4) b30, SUBSTRING(MOCycleNum, 121, 4) b31, SUBSTRING(MOCycleNum, 125, 4) b32,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 129, 4) b33, SUBSTRING(MOCycleNum, 133, 4) b34, SUBSTRING(MOCycleNum, 137, 4) b35, SUBSTRING(MOCycleNum, 141, 4) b36,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 145, 4) b37, SUBSTRING(MOCycleNum, 149, 4) b38, SUBSTRING(MOCycleNum, 153, 4) b39, SUBSTRING(MOCycleNum, 157, 4) b40,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 161, 4) b41, SUBSTRING(MOCycleNum, 165, 4) b42, SUBSTRING(MOCycleNum, 169, 4) b43, SUBSTRING(MOCycleNum, 173, 4) b44,       \r\n" +
                //"       SUBSTRING(MOCycleNum, 177, 4) b45 , s1.ReportToSectionid, pm.activity      \r\n" +
                //"         from vw_PlanMonth pm, Workplace w, section s, section s1 where pm.workplaceid = w.workplaceid      \r\n" +
                //"       and pm.Sectionid = s.SectionID and pm.Prodmonth = s.Prodmonth      \r\n" +
                //"      and s.reporttoSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
                //"      and pm.prodmonth = '" + prodmonth + "' and pm.Plancode = 'MP'     \r\n" +
                //"       and s1.ReportToSectionid = '"+reportSettings.SectionID+"') c on a.Workplaceid = c.workplaceid  and a.activity = c.activity  and a.minid = c.minid      \r\n" +
                //"  , (select 0 act1, max(rand) rr FROM[dbo].[Rates_Stoping]) d      \r\n" +
                //"union      \r\n" +




                //"   select   case when MAX(pmold1) is null then 'Red' when MAX(pmold1) is not null and MAX(pmold1) < @prev then 'orange' else '' end as newwpflag,       \r\n" +
                //"   Line, sbid, sbname, minid, minname, WorkplaceID,  Description, Act,       \r\n" +
                //"   sum(DevTotAdv) DevTotAdv, sum(DevWasteAdv) DevWasteAdv, sum(DevTons) DevTons, sum(DevCont) DevCont, sum(StpTons) StpTons       \r\n" +
                //"   , sum(StpCont) StpCont, max(MainGroup) MainGroup, max(FL) FL, convert(int, sum(plansqm)) plansqm       \r\n" +
                //"   , convert(int, sum(cyclesqm)) cyclesqm, MAX(orgunitds) orgunitds, MAX(aa)  aa       \r\n" +
                //"   , MAX(adv) adv, convert(int, sum(tons)) tons, convert(int, sum(content)) content, MAX(offreefsqm) offreefsqm, 0 Budget, MAX(activity) activity        \r\n" +
                //"   ,MAX(extrabudget) extrabudget, MAX(extrabudget1) extrabudget1       \r\n" +
                //"   ,MAX(aa1) aa1,MAX(aa2) aa2,MAX(aa3) aa3,MAX(aa4) aa4,MAX(aa5) aa5,MAX(aa6) aa6,MAX(aa7) aa7,MAX(aa8) aa8,MAX(aa9) aa9,MAX(aa10) aa10       \r\n" +
                //"   ,MAX(aa11) aa11,MAX(aa12) aa12,MAX(aa13) aa13,MAX(aa14) aa14,MAX(aa15) aa15,MAX(aa16) aa16,MAX(aa17) aa17,MAX(aa18) aa18,MAX(aa19) aa19,MAX(aa20) aa20       \r\n" +
                //"   ,MAX(aa21) aa21,MAX(aa22) aa22,MAX(aa23) aa23,MAX(aa24) aa24,MAX(aa25) aa25,MAX(aa26) aa26,MAX(aa27) aa27,MAX(aa28) aa28,MAX(aa29) aa29,MAX(aa30) aa30       \r\n" +
                //"   ,MAX(aa31) aa31,MAX(aa32) aa32,MAX(aa33) aa33,MAX(aa34) aa34,MAX(aa35) aa35,MAX(aa36) aa36,MAX(aa37) aa37,MAX(aa38) aa38,MAX(aa39) aa39,MAX(aa40) aa40       \r\n" +
                //"   ,MAX(aa41) aa41,MAX(aa42) aa42,MAX(aa43) aa43,MAX(aa44) aa44,MAX(aa45) aa45       \r\n" +
                //"   , MAX(SectionID) SectionID, MAX(Mprass) Mprass, MAX(wpexternalid) wpexternalid, MAX(Surv) Surv       \r\n" +
                //"   , MAX(pmold1) newptt,  2 Line2,       \r\n" +
                //"   MAX(sbid1) sbid1, MAX(sbname1) sbname1, MAX(minid1) minid1, MAX(minname1) minname1,       \r\n" +
                //"   MAX(wp1) wp1,MAX(wd1) wd1, 9 Act1a, MAX(Act1) Act1,       \r\n" +
                //"   MAX(MainGroup1) MainGroup1,        \r\n" +
                //"   sum(FL1) FL1, convert(int, sum(plansqm1)) plansqm1, convert(int, sum(cyclesqma)) cyclesqma , MAX(da) da, MAX(aaa) aaa       \r\n" +
                //"   ,MAX(a1) a1,MAX(a2) a2,MAX(a3) a3,MAX(a4) a4,MAX(a5) a5,MAX(a6) a6,MAX(a7) a7,MAX(a8) a8,MAX(a9) a9,MAX(a10) a10       \r\n" +
                //"   ,MAX(a11) a11,MAX(a12) a12,MAX(a13) a13,MAX(a14) a14,MAX(a15) a15,MAX(a16) a16,MAX(a17) a17,MAX(a18) a18,MAX(a19) a19,MAX(a20) a20       \r\n" +
                //"   ,MAX(a21) a21,MAX(a22) a22,MAX(a23) a23,MAX(a24) a24,MAX(a25) a25,MAX(a26) a26,MAX(a27) a27,MAX(a28) a28,MAX(a29) a29,MAX(a30) a30       \r\n" +
                //"   ,MAX(a31) a31,MAX(a32) a32,MAX(a33) a33,MAX(a34) a34,MAX(a35) a35,MAX(a36) a36,MAX(a37) a37,MAX(a38) a38,MAX(a39) a39,MAX(a40) a40       \r\n" +
                //"   ,MAX(a41) a41,MAX(a42) a42,MAX(a43) a43,MAX(a44) a44,MAX(a45) a45       \r\n" +
                //"   , MAX(SectionID) SectionID2,    0 MonthSqm  ,     \r\n" +
                //"   MAX(Line3) Line3,       \r\n" +
                //"   MAX(sbid1) sbid2, MAX(sbname1) sbname2, MAX(minid1) minid2, MAX(minname1) minname2,       \r\n" +
                //"   MAX(wp1) wp2,MAX(wd1) wd2, MAX(Act1) Act2,       \r\n" +
                //"   MAX(MainGroup1) MainGroup2,        \r\n" +
                //"   sum(FL1) FL2, convert(int, sum(plansqm1)) plansqm2, convert(int, sum(cyclesqma)) cyclesqma2 , MAX(da) da2, MAX(aaa) aaa2       \r\n" +
                //"   ,MAX(b1) b1,MAX(b2) b2,MAX(b3) b3,MAX(b4) b4,MAX(b5) b5,MAX(b6) b6,MAX(b7) b7,MAX(b8) b8,MAX(b9) b9,MAX(b10) b10       \r\n" +
                //"   ,MAX(b11) b11,MAX(b12) b12,MAX(b13) b13,MAX(b14) b14,MAX(b15) b15,MAX(b16) b16,MAX(b17) b17,MAX(b18) b18,MAX(b19) b19,MAX(b20) b20       \r\n" +
                //"   ,MAX(b21) b21,MAX(b22) b22,MAX(b23) b23,MAX(b24) b24,MAX(b25) b25,MAX(b26) b26,MAX(b27) b27,MAX(b28) b28,MAX(b29) b29,MAX(b30) b30       \r\n" +
                //"   ,MAX(b31) b31,MAX(b32) b32,MAX(b33) b33,MAX(b34) b34,MAX(b35) b35,MAX(b36) b36,MAX(b37) b37,MAX(b38) b38,MAX(b39) b39,MAX(b40) b40       \r\n" +
                //"   ,MAX(b41) b41,MAX(b42) b42,MAX(b43) b43,MAX(b44) b44,MAX(b45) b45       \r\n" +
                //"   ,max(mo) mo , 9 ActF, pm, sum(rr) rr      \r\n" +
                //"    from(      \r\n" +
                //"   select 1 Line, s1.SectionID sbid, s1.Name sbname, s.SectionID minid, s.Name minname,      \r\n" +
                //"   v.WorkplaceID, w.Description, 'Vamping' Act,      \r\n" +
                //"   0 DevTotAdv,      \r\n" +
                //"   0 DevWasteAdv,      \r\n" +
                //"   0 DevTons,      \r\n" +
                //"   0 DevCont,      \r\n" +
                //"   plantons StpTons,      \r\n" +
                //"   PlanContent StpCont,      \r\n" +
                //"   'C.Vamping'  MainGroup,      \r\n" +
                //"   0 FL, PlanSqm plansqm, PlanSqm cyclesqm, orgunitds,      \r\n" +
                //"   'Default Cycle' aa, 0 adv, plantons tons, PlanContent content, 0 offreefsqm, 0 Budget, 9 activity,      \r\n" +
                //"   0 extrabudget, 0 extrabudget1,      \r\n" +
                //"   '' aa1,      \r\n" +
                //"   '' aa2, '' aa3, '' aa4,      \r\n" +
                //"   '' aa5, '' aa6, '' aa7, '' aa8,      \r\n" +
                //"   '' aa9, '' aa10, '' aa11, '' aa12,      \r\n" +
                //"   '' aa13, '' aa14,'' aa15, '' aa16,      \r\n" +
                //"   '' aa17, '' aa18, '' aa19, '' aa20,      \r\n" +
                //"   '' aa21, '' aa22, '' aa23, '' aa24,      \r\n" +
                //"   '' aa25, '' aa26, '' aa27, '' aa28,      \r\n" +
                //"   '' aa29, '' aa30, '' aa31, '' aa32,      \r\n" +
                //"   '' aa33, '' aa34, '' aa35, '' aa36,      \r\n" +
                //"   '' aa37, '' aa38, '' aa39, '' aa40,      \r\n" +
                //"   '' aa41, '' aa42, '' aa43, '' aa44,      \r\n" +
                //"   '' aa45,      \r\n" +
                //"   s2.SectionID, '' Mprass,'' wpexternalid, '' Surv,      \r\n" +
                //"   2 Line3,      \r\n" +
                //"    s1.SectionID sbid1, s1.Name sbname1, s.SectionID minid1, s.Name minname1,      \r\n" +
                //"   v.WorkplaceID wp1, w.Description wd1, 'Vamping' Act1,      \r\n" +
                //"   'C.Vamping'  MainGroup1,      \r\n" +
                //"   0 FL1, PlanSqm plansqm1, PlanSqm cyclesqma, orgunitds da, 'MO Cycle' aaa,      \r\n" +
                //"   case when calendardate = @VampingStart and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a1,      \r\n" +
                //"   case when calendardate = @VampingStart + 1 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 1 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a2,      \r\n" +
                //"   case when calendardate = @VampingStart + 2 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 2 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a3,      \r\n" +
                //"   case when calendardate = @VampingStart + 3 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 3 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a4,      \r\n" +
                //"   case when calendardate = @VampingStart + 4 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 4 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a5,      \r\n" +
                //"   case when calendardate = @VampingStart + 5 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 5 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a6,      \r\n" +
                //"   case when calendardate = @VampingStart + 6 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 6 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a7,      \r\n" +
                //"   case when calendardate = @VampingStart + 7 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 7 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a8,      \r\n" +
                //"   case when calendardate = @VampingStart + 8 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 8 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a9,      \r\n" +
                //"   case when calendardate = @VampingStart + 9 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 9 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a10,      \r\n" +
                //"   case when calendardate = @VampingStart + 10 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 10 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a11,      \r\n" +
                //"   case when calendardate = @VampingStart + 11 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 11 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a12,      \r\n" +
                //"   case when calendardate = @VampingStart + 12 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 12 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a13,      \r\n" +
                //"   case when calendardate = @VampingStart + 13 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 13 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a14,      \r\n" +
                //"   case when calendardate = @VampingStart + 14 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 14 and workingday = 'N' then 'OFF'       \r\n" +      
                //"   else '' end as a15,      \r\n" +
                //"   case when calendardate = @VampingStart + 15 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 15 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a16,      \r\n" +
                //"   case when calendardate = @VampingStart + 16 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 16 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a17,      \r\n" +
                //"   case when calendardate = @VampingStart + 17 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 17 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a18,      \r\n" +
                //"   case when calendardate = @VampingStart + 18 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 18 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a19,      \r\n" +
                //"   case when calendardate = @VampingStart + 19 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 19 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a20,      \r\n" +
                //"   case when calendardate = @VampingStart + 20 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 20 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a21,      \r\n" +
                //"   case when calendardate = @VampingStart + 21 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 21 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a22,      \r\n" +
                //"   case when calendardate = @VampingStart + 22 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 22 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a23,      \r\n" +
                //"   case when calendardate = @VampingStart + 23 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 23 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a24,      \r\n" +
                //"   case when calendardate = @VampingStart + 24 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 24 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a25,      \r\n" +
                //"   case when calendardate = @VampingStart + 25 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 25 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a26,      \r\n" +
                //"   case when calendardate = @VampingStart + 26 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 26 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a27,      \r\n" +
                //"   case when calendardate = @VampingStart + 27 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 27 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a28,      \r\n" +
                //"   case when calendardate = @VampingStart + 28 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 28 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a29,      \r\n" +
                //"   case when calendardate = @VampingStart + 29 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 29 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a30,      \r\n" +
                //"   case when calendardate = @VampingStart + 30 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 30 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a31,      \r\n" +
                //"   case when calendardate = @VampingStart + 31 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 31 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a32,      \r\n" +
                //"   case when calendardate = @VampingStart + 32 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 32 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a33,      \r\n" +
                //"   case when calendardate = @VampingStart + 33 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 33 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a34,      \r\n" +
                //"   case when calendardate = @VampingStart + 34 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 34 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a35,      \r\n" +
                //"   case when calendardate = @VampingStart + 35 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 35 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a36,      \r\n" +
                //"   case when calendardate = @VampingStart + 36 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 36 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a37,      \r\n" +
                //"   case when calendardate = @VampingStart + 37 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 37 and workingday = 'N' then 'OFF'       \r\n" +
                //"    else '' end as a38,      \r\n" +
                //"   case when calendardate = @VampingStart + 38 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 38 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a39,      \r\n" +
                //"   case when calendardate = @VampingStart + 39 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 39 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a40,      \r\n" +
                //"   case when calendardate = @VampingStart + 40 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 40 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a41,      \r\n" +
                //"   case when calendardate = @VampingStart + 41 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 41 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a42,      \r\n" +
                //"   case when calendardate = @VampingStart + 42 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 42 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a43,      \r\n" +
                //"   case when calendardate = @VampingStart + 43 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 43 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a44,      \r\n" +
                //"   case when calendardate = @VampingStart + 44 and workingday = 'Y' then PlanActivity      \r\n" +
                //"   when calendardate = @VampingStart + 44 and workingday = 'N' then 'OFF'       \r\n" +
                //"   else '' end as a45,      \r\n" +
                //"   s2.SectionID ReporttoSectioniD,      \r\n" +
                //"   3 Line3a,      \r\n" +
                //"    s1.SectionID sbid3, s1.Name sbname3, s.SectionID minid3, s.Name minname3,      \r\n" +
                //"   v.WorkplaceID wp2, w.Description wd2, 'Vamping' Act3,      \r\n" +
                //"   'C.Vamping'  MainGroupaaa,      \r\n" +
                //"   0 FLz, PlanSqm plansqmz, PlanSqm cyclesqm1, orgunitds ds1, 'Day Sqm' aaz,      \r\n" +
                //"   case when calendardate = @VampingStart and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b1,       \r\n" +
                //"   case when calendardate = @VampingStart + 1 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b2,       \r\n" +
                //"   case when calendardate = @VampingStart + 2 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b3,       \r\n" +
                //"   case when calendardate = @VampingStart + 3 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))       \r\n" +
                //"   else '' end as b4,       \r\n" +
                //"   case when calendardate = @VampingStart + 4 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b5,       \r\n" +
                //"   case when calendardate = @VampingStart + 5 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b6,       \r\n" +
                //"   case when calendardate = @VampingStart + 6 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b7,       \r\n" +
                //"   case when calendardate = @VampingStart + 7 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b8,       \r\n" +
                //"   case when calendardate = @VampingStart + 8 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b9,       \r\n" +
                //"   case when calendardate = @VampingStart + 9 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b10,       \r\n" +
                //"   case when calendardate = @VampingStart + 10 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b11,       \r\n" +
                //"   case when calendardate = @VampingStart + 11 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b12,       \r\n" +
                //"   case when calendardate = @VampingStart + 12 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b13,       \r\n" +
                //"   case when calendardate = @VampingStart + 13 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b14,       \r\n" +
                //"   case when calendardate = @VampingStart + 14 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b15,       \r\n" +
                //"   case when calendardate = @VampingStart + 15 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b16,       \r\n" +
                //"   case when calendardate = @VampingStart + 16 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b17,       \r\n" +
                //"   case when calendardate = @VampingStart + 17 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b18,       \r\n" +
                //"   case when calendardate = @VampingStart + 18 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b19,       \r\n" +
                //"   case when calendardate = @VampingStart + 19 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b20,       \r\n" +
                //"   case when calendardate = @VampingStart + 20 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b21,       \r\n" +
                //"   case when calendardate = @VampingStart + 21 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b22,       \r\n" +
                //"   case when calendardate = @VampingStart + 22 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b23,       \r\n" +
                //"   case when calendardate = @VampingStart + 23 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b24,       \r\n" +
                //"   case when calendardate = @VampingStart + 24 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b25,       \r\n" +
                //"   case when calendardate = @VampingStart + 25 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b26,       \r\n" +
                //"   case when calendardate = @VampingStart + 26 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b27,       \r\n" +
                //"   case when calendardate = @VampingStart + 27 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b28,       \r\n" +
                //"   case when calendardate = @VampingStart + 28 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b29,       \r\n" +
                //"   case when calendardate = @VampingStart + 29 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b30,       \r\n" +
                //"   case when calendardate = @VampingStart + 30 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))       \r\n" +
                //"   else '' end as b31,       \r\n" +
                //"   case when calendardate = @VampingStart + 31 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b32,       \r\n" +
                //"   case when calendardate = @VampingStart + 32 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b33,       \r\n" +
                //"   case when calendardate = @VampingStart + 33 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b34,       \r\n" +
                //"   case when calendardate = @VampingStart + 34 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))       \r\n" +
                //"   else '' end as b35,       \r\n" +
                //"   case when calendardate = @VampingStart + 35 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b36,       \r\n" +
                //"   case when calendardate = @VampingStart + 36 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b37,       \r\n" +
                //"   case when calendardate = @VampingStart + 37 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b38,       \r\n" +
                //"   case when calendardate = @VampingStart + 38 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b39,       \r\n" +
                //"   case when calendardate = @VampingStart + 39 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b40,       \r\n" +
                //"   case when calendardate = @VampingStart + 40 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b41,       \r\n" +
                //"   case when calendardate = @VampingStart + 41 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b42,       \r\n" +
                //"   case when calendardate = @VampingStart + 42and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b43,       \r\n" +
                //"   case when calendardate = @VampingStart + 43 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b44,       \r\n" +
                //"   case when calendardate = @VampingStart + 44 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
                //"   else '' end as b45,        \r\n" +
                //"   s2.SectionID mo, v.Prodmonth pm, 0 rr, pmold1      \r\n" +
                //"    from Planning_Vamping v, Section s , Section s1, Section s2, (select w.*, pmold1 from Workplace w left outer join   (select workplaceid wz, max(prodmonth) pmold1 from Planning_Vamping      \r\n" +
                //"    where prodmonth < '" + prodmonth + "' group by workplaceid) newwp on  w.workplaceid = newwp.wz) w, Workplace wt      \r\n" +
                //"   where v.Sectionid = s.SectionID and v.Prodmonth = s.Prodmonth      \r\n" +
                //"    and s.ReportToSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
                //"    and s1.ReportToSectionid = s2.SectionID and s1.Prodmonth = s2.Prodmonth      \r\n" +
                //"    and v.WorkplaceID = w.WorkplaceID  and w.workplaceid = wt.workplaceid     \r\n" +
                //"    and v.Prodmonth = '" + prodmonth + "' and s2.SectionID = '"+reportSettings.SectionID+"') a      \r\n" +
                //"    group by Line, sbid, sbname, minid, minname, WorkplaceID, Description, Act, pm      \r\n" +
                //"   order by  a.sbid, a.MainGroup, a.OrgUnitDS, a.Workplaceid, a.aa1 Desc, a.Line";



                //            _dbManPlan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //            _dbManPlan.queryReturnType = MWDataManager.ReturnType.DataTable;
                //            _dbManPlan.ResultsTableName = "Planning";
                //            _dbManPlan.ExecuteInstruction();


                /////


            }
        }

        void LoadTotalPlanningReportDynamic()
        {
            string prodmonth = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);

            MWDataManager.clsDataAccess _dbManPlan = new MWDataManager.clsDataAccess();
            _dbManPlan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManPlan.SqlStatement = " select 'Planning Report (Dynamic)' label1, '" + SysSettings.Banner + "' banner, " +
                        " '" + prodmonth + "' Myprodmonth , 'Total Mine' thesection, " +
                        " * from (select s2reptosecid, mosection, ReptoSecid, sectionname, " +
                        " sum(ReefMeters) ReefMeters,   SUM(WasteMeters) WasteMeters,   " +
                        " SUM(TotalMeters) TotalMeters, SUM(DevOunces) DevOunces,    " +
                        " SUM(StopeOunces) StopeOunces,   SUM(TotalOunces) TotalOunces,  " +
                        " SUM(StopeTons) StopeTons,   SUM(DevTons) DevTons, SUM(TotalTons) TotalTons,    " +
                        " SUM(Facelength) Facelength, SUM(Sqm) Sqm,   SUM(AveFAdv) AveFAdv from (     " +
                        " select sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 sectionname,  " +
                        " case when pm.Activity = 1 then Content/1000 else 0 end as DevOunces,     " +
                        " case when pm.Activity in (0,9)   then Content/1000 else 0 end as StopeOunces,   " +
                        " Content/1000 TotalOunces,    case when pm.activity = 1   then Tons else 0 END as DevTons,   " +
                        " case when pm.activity in (0,9)   then Tons else 0 END as StopeTons,     " +
                        " Tons TotalTons, FL Facelength, SqmTotal Sqm,     case when FL > 0  " +
                        " then SqmTotal/FL  else 0 end as AveFAdv, " +
                        " case when ReefWaste = 'R' and pm.activity = 1 then Adv else 0 end as ReefMeters,       " +
                        " case when ReefWaste = 'W' and pm.activity = 1 then Adv else 0 end as WasteMeters,  " +
                        " case when  pm.activity = 1 then Adv else 0 end as TotalMeters " +
                        " from planmonth pm, sections_complete sc, workplace w   where  " +
                        " pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   " +
                        " pm.prodmonth = '" + prodmonth + "') a group by s2reptosecid, mosection, ReptoSecid, sectionname) q " +


                        " left outer join  " +
                        " (select SUM(budget_sqm) budget_Sqm, SUM(budget_dev) budget_Dev, SUM(budget_StopeTons) budget_StopeTons,   " +
                        " SUM(budget_DevTons) budget_DevTons, SUM(budget_Fl) budget_Fl, SUM(budget_StopeOZ) budget_StopeOZ,   " +
                        " SUM(budget_DevOZ) budget_DevOZ, SUM(budget_ReefMeters) budget_ReefMeters,    " +
                        " SUM(budget_WasteMeters) budget_WasteMeters, SUM(AveFAdv) AveFAdv, secid, ReportToSectionid from  " +
                        " (  (select * from (  select Sqm budget_Sqm, Dev budget_Dev, StopeTons budget_StopeTons,  " +
                        " DevTons budget_DevTons,   Fl budget_Fl, StopeOZ budget_StopeOZ, DevOZ budget_DevOZ,    " +
                        " ReefMeters budget_ReefMeters, WasteMeters budget_WasteMeters, *  " +
                        " from ( select case when Fl > 0 then Sqm/Fl else 0 end as AveFAdv, * from    " +
                        " (  select case when Type = 'Sqm' then  case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1      " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2  when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4  when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6  when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8  when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10  when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12  END end as Sqm ,      " +
                        " case when Type = 'Dev' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as Dev ,       " +
                        " case when Type = 'StopeTons' then     " +
                        " case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1    when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3    when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5    when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7    when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9    when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11    when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12     " +
                        " END end as StopeTons ,      " +
                        " case when Type = 'DevTons' then     " +
                        " case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as DevTons ,       " +
                        " case when Type = 'Fl' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9    " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as Fl ,       " +
                        " case when Type = 'StopeOZ' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as StopeOZ ,      " +
                        " case when Type = 'DevOZ' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as DevOZ ,      " +
                        " case when Type = 'ReefMeters' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11    " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as ReefMeters ,      " +
                        " case when Type = 'WasteMeters' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as WasteMeters ,  * from budget     " +
                        " where finyear = SUBSTRING('" + prodmonth + "',1,4)   ) a  ) b   " +
                        " left outer join     " +
                        " (Select SectionID secid, ReportToSectionid, name from SECTION     " +
                        " where Prodmonth = '" + prodmonth + "') c   on b.Sectionid = c.secid   )b ) )a   " +
                        " group by secid, ReportToSectionid) e on q.RepToSecID = e.secid ";
            _dbManPlan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPlan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPlan.ResultsTableName = "Planning";
            textBox1.Text = _dbManPlan.SqlStatement;
            _dbManPlan.ExecuteInstruction();

            DataSet DsPlanning = new DataSet();
            DsPlanning.Tables.Add(_dbManPlan.ResultsDataTable);

            theReport.RegisterData(DsPlanning);

            theReport.Load(TGlobalItems.ReportsFolder + "\\PlanningRep2.frx");

            if (TParameters.DesignReport)
            {
                theReport.Design();
            }

            //theReport.Design();

            //pcReport.Clear();
            //theReport.Prepare();
            //theReport.Preview = pcReport;
            //theReport.ShowPrepared();

            theReport.Prepare();
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;

        }

        void LoadTotalPlanningReportLocked()
        {
            string prodmonth = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);

            MWDataManager.clsDataAccess _dbManPlan = new MWDataManager.clsDataAccess();
            _dbManPlan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManPlan.SqlStatement = " select 'Planning Report (Locked)' label1, '" + SysSettings.Banner + "' banner, " +
                        " '" + prodmonth + "' Myprodmonth , 'Total Mine' thesection, " +
                        " * from (select s2reptosecid, mosection, ReptoSecid, sectionname, " +
                        " sum(ReefMeters) ReefMeters,   SUM(WasteMeters) WasteMeters,   " +
                        " SUM(TotalMeters) TotalMeters, SUM(DevOunces) DevOunces,    " +
                        " SUM(StopeOunces) StopeOunces,   SUM(TotalOunces) TotalOunces,  " +
                        " SUM(StopeTons) StopeTons,   SUM(DevTons) DevTons, SUM(TotalTons) TotalTons,    " +
                        " SUM(Facelength) Facelength, SUM(Sqm) Sqm,   SUM(AveFAdv) AveFAdv from (     " +
                        " select sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 sectionname,  " +
                        " case when pm.Activity = 1 then Content/1000 else 0 end as DevOunces,     " +
                        " case when pm.Activity in (0,9)   then Content/1000 else 0 end as StopeOunces,   " +
                        " Content/1000 TotalOunces,    case when pm.activity = 1   then Tons else 0 END as DevTons,   " +
                        " case when pm.activity in (0,9)   then Tons else 0 END as StopeTons,     " +
                        " Tons TotalTons, FL Facelength, SqmTotal Sqm,     case when FL > 0  " +
                        " then SqmTotal/FL  else 0 end as AveFAdv, " +
                        " case when ReefWaste = 'R' and pm.activity = 1 then Adv else 0 end as ReefMeters,       " +
                        " case when ReefWaste = 'W' and pm.activity = 1 then Adv else 0 end as WasteMeters,  " +
                        " case when  pm.activity = 1 then Adv else 0 end as TotalMeters " +
                        " from planmonthlocked pm, sections_complete sc, workplace w   where  " +
                        " pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   " +
                        " pm.prodmonth = '" + prodmonth + "') a group by s2reptosecid, mosection, ReptoSecid, sectionname) q   " +


                        " left outer join   " +
                        " (select SUM(budget_sqm) budget_Sqm, SUM(budget_dev) budget_Dev, SUM(budget_StopeTons) budget_StopeTons,   " +
                        " SUM(budget_DevTons) budget_DevTons, SUM(budget_Fl) budget_Fl, SUM(budget_StopeOZ) budget_StopeOZ,   " +
                        " SUM(budget_DevOZ) budget_DevOZ, SUM(budget_ReefMeters) budget_ReefMeters,    " +
                        " SUM(budget_WasteMeters) budget_WasteMeters, SUM(AveFAdv) AveFAdv, secid, ReportToSectionid from  " +
                        " (  (select * from (  select Sqm budget_Sqm, Dev budget_Dev, StopeTons budget_StopeTons,  " +
                        " DevTons budget_DevTons,   Fl budget_Fl, StopeOZ budget_StopeOZ, DevOZ budget_DevOZ,    " +
                        " ReefMeters budget_ReefMeters, WasteMeters budget_WasteMeters, *  " +
                        " from ( select case when Fl > 0 then Sqm/Fl else 0 end as AveFAdv, * from    " +
                        " (  select case when Type = 'Sqm' then  case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1      " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2  when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4  when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6  when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8  when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10  when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12  END end as Sqm ,      " +
                        " case when Type = 'Dev' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as Dev ,       " +
                        " case when Type = 'StopeTons' then     " +
                        " case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1    when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3    when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5    when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7    when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9    when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11    when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12     " +
                        " END end as StopeTons ,      " +
                        " case when Type = 'DevTons' then     " +
                        " case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as DevTons ,       " +
                        " case when Type = 'Fl' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9    " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as Fl ,       " +
                        " case when Type = 'StopeOZ' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as StopeOZ ,      " +
                        " case when Type = 'DevOZ' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as DevOZ ,      " +
                        " case when Type = 'ReefMeters' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11    " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as ReefMeters ,      " +
                        " case when Type = 'WasteMeters' then    case when SUBSTRING('" + prodmonth + "',5,2) = '01' then Mnth1     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '02' then Mnth2    when SUBSTRING('" + prodmonth + "',5,2) = '03' then Mnth3     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '04' then Mnth4    when SUBSTRING('" + prodmonth + "',5,2) = '05' then Mnth5     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '06' then Mnth6    when SUBSTRING('" + prodmonth + "',5,2) = '07' then Mnth7     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '08' then Mnth8    when SUBSTRING('" + prodmonth + "',5,2) = '09' then Mnth9     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '10' then Mnth10    when SUBSTRING('" + prodmonth + "',5,2) = '11' then Mnth11     " +
                        " when SUBSTRING('" + prodmonth + "',5,2) = '12' then Mnth12    END end as WasteMeters ,  * from budget     " +
                        " where finyear = SUBSTRING('" + prodmonth + "',1,4)   ) a  ) b   " +
                        " left outer join     " +
                        " (Select SectionID secid, ReportToSectionid, name from SECTION     " +
                        " where Prodmonth = '" + prodmonth + "') c   on b.Sectionid = c.secid   )b ) )a   " +
                        " group by secid, ReportToSectionid) e on q.RepToSecID = e.secid ";
            _dbManPlan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPlan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPlan.ResultsTableName = "Planning";
            textBox1.Text = _dbManPlan.SqlStatement;
            _dbManPlan.ExecuteInstruction();

            DataSet DsPlanning = new DataSet();
            DsPlanning.Tables.Add(_dbManPlan.ResultsDataTable);

            theReport.RegisterData(DsPlanning);

            theReport.Load(TGlobalItems.ReportsFolder + "\\PlanningRep2.frx");

            if (TParameters.DesignReport)
            {
                theReport.Design();
            }

            //theReport.Design();

            //pcReport.Clear();
            //theReport.Prepare();
            //theReport.Preview = pcReport;
            //theReport.ShowPrepared();

            theReport.Prepare();
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;

        }

        void LoadDetailPlanningReportDynamic()
        {
            string prodmonth = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " select 'Planning Detail Report (Dynamic)' label1, '" + SysSettings.Banner + "' banner, " +
                                  " '" + prodmonth + "' Myprodmonth , 'Total Mine' thesection, Fl,  " +
                                  " * from (select Vamps, s2reptosecid, mosection, ReptoSecid, MoName, Fl, Adv, SW, sqmtotal*sw SQMSW,  " +
                                  " ReefMeters,    WasteMeters,  Cubics, Dens, orgunitday OrgUnitDS, ShiftBossName, MinerName, " +
                                  " TotalMeters,  DevOunces,  wpDesc,  wpID, SbSecID,MinerSecID, " +
                                  " StopeOunces,    TotalOunces,  " +
                                  " StopeTons,    DevTons, TotalTons,    " +
                                  " Facelength,  SqmTotal, AveFAdv,StopeAdv,DevAdv from (     " +
                                  " select Vamps, w.Description wpDesc, w.workplaceid wpID, NAME_1 ShiftBossName, SECTIONID_1 SbSecID, sc.SECTIONID MinerSecID, NAME MinerName,orgunitday OrgUnitDS, pm.FL Fl, Adv, pm.SW SW,  " +
                                  " Cubics, pm.Dens Dens, sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid,  " +
                                  " name_2 MoName,  case when pm.Activity = 1 then Content/1000 else 0 end as DevOunces,     " +
                                  " case when pm.Activity in (0,9)   then Content/1000 else 0 end as StopeOunces,   " +
                                  " Content/1000 TotalOunces,    case when pm.activity = 1   then Tons else 0 END as DevTons,   " +
                                  " case when pm.activity in (0,9)   then Tons else 0 END as StopeTons,     " +
                                  " Tons TotalTons, FL Facelength, SqmTotal,     case when FL > 0  " +
                                  "  then SqmTotal/FL  else 0 end as AveFAdv, " +
                                  "  case when ReefWaste = 'R' and pm.activity = 1 then Adv else 0 end as ReefMeters,       " +
                                  " case when ReefWaste = 'W' and pm.activity = 1 then Adv else 0 end as WasteMeters,  " +
                                  " case when  pm.activity = 1 then Adv else 0 end as TotalMeters, " +
                                  "case when pm.Activity IN (0,9) then pm.MetresAdvance else 0 end as StopeAdv, " +
                                  "case when pm.Activity =1 then pm.MetresAdvance else 0 end as DevAdv " +
                                  " from planmonth pm, sections_complete sc, workplace w   where  " +
                                  " pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   " +
                                  " pm.prodmonth = '" + prodmonth + "' and sc.SECTIONID_2 = '" + reportSettings.SectionID + "') a " +
                                  " ) q order by SbSecID, MinerSecID, OrgUnitDS, wpDesc";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ResultsTableName = "planning";
            textBox1.Text = _dbMan.SqlStatement;
            _dbMan.ExecuteInstruction();

            DataSet ds = new DataSet();
            ds.Tables.Add(_dbMan.ResultsDataTable);

            theReport.RegisterData(ds);

            theReport.Load(TGlobalItems.ReportsFolder + "\\PlanningRepDetailDynamic.frx");

            if (TParameters.DesignReport)
            {
                theReport.Design();
            }

            // theReport.Design();

            //pcReport.Clear();
            //theReport.Prepare();
            //theReport.Preview = pcReport;
            //theReport.ShowPrepared();

            theReport.Prepare();
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;

        }

        void LoadDetailPlanningReportLocked()
        {
            string prodmonth = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " select 'Planning Detail Report (Locked)' label1, '" + SysSettings.Banner + "' banner, " +
                                  " '" + prodmonth + "' Myprodmonth , 'Total Mine' thesection, Fl,  " +
                                  " * from (select Vamps, s2reptosecid, mosection, ReptoSecid, MoName, Fl, Adv, SW, sqmtotal*sw SQMSW, " +
                                  " ReefMeters,    WasteMeters,  Cubics, Dens, OrgUnitDS, ShiftBossName, MinerName, " +
                                  " TotalMeters,  DevOunces,  wpDesc, wpID,  SbSecID,MinerSecID, " +
                                  " StopeOunces,    TotalOunces,  " +
                                  " StopeTons,    DevTons, TotalTons,    " +
                                  " Facelength,  SqmTotal, AveFAdv from (     " +
                                  " select Vamps, w.Description wpDesc, w.workplaceid wpID, NAME_1 ShiftBossName, SECTIONID_1 SbSecID, sc.SECTIONID MinerSecID, NAME MinerName, OrgUnitDS, pm.FL Fl, Adv, pm.SW SW,  " +
                                  " Cubics, pm.Dens Dens, sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid,  " +
                                  " name_2 MoName,  case when pm.Activity = 1 then Content/1000 else 0 end as DevOunces,     " +
                                  " case when pm.Activity in (0,9)   then Content/1000 else 0 end as StopeOunces,   " +
                                  " Content/1000 TotalOunces,    case when pm.activity = 1   then Tons else 0 END as DevTons,   " +
                                  " case when pm.activity in (0,9)   then Tons else 0 END as StopeTons,     " +
                                  " Tons TotalTons, FL Facelength, SqmTotal,     case when FL > 0  " +
                                  "  then SqmTotal/FL  else 0 end as AveFAdv, " +
                                  "  case when ReefWaste = 'R' and pm.activity = 1 then Adv else 0 end as ReefMeters,       " +
                                  " case when ReefWaste = 'W' and pm.activity = 1 then Adv else 0 end as WasteMeters,  " +
                                  " case when  pm.activity = 1 then Adv else 0 end as TotalMeters " +
                                  " from planmonthlocked pm, sections_complete sc, workplace w   where  " +
                                  " pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   " +
                                  " pm.prodmonth = '" + prodmonth + "' and sc.SECTIONID_2 = '" + reportSettings.SectionID + "') a " +
                                  " )q  order by SbSecID, MinerSecID, OrgUnitDS, wpDesc ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ResultsTableName = "planning";
            textBox1.Text = _dbMan.SqlStatement;
            _dbMan.ExecuteInstruction();

            DataSet ds = new DataSet();
            ds.Tables.Add(_dbMan.ResultsDataTable);

            theReport.RegisterData(ds);

            theReport.Load(TGlobalItems.ReportsFolder + "\\PlanningRepDetailDynamic.frx");

            if (TParameters.DesignReport)
            {
                theReport.Design();
            }

            //theReport.Design();

            //pcReport.Clear();
            //theReport.Prepare();
            //theReport.Preview = pcReport;
            //theReport.ShowPrepared();

            theReport.Prepare();
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;

        }

       

        private void rpSelection_EditValueChanged(object sender, EventArgs e)
        {
            pgPlanningReportNewStyle.PostEditor();

            if (reportSettings.Selection == "Total Mine")
                iSection.Visible = false;

            else if (reportSettings.Selection == "Detail")
                iSection.Visible = true;
        }

        private void pgPlanningReportNewStyle_Click(object sender, EventArgs e)
        {

        }
    }
}
