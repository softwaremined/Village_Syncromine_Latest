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
using Mineware.Systems.ProductionGlobal;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Mineware.Systems.GlobalConnect;



namespace Mineware.Systems.Production.SysAdminScreens.Workplaces
{
    
    public partial class ileWorkplaceEdit : EditFormUserControl 
    {
        //public SysAdminFrm SysAdminFrm;
        //public frmServices ServiceFrm;
        private bool theClassifyNeeded;
        public string theDiv;
        private string theDivision;
        public string FormType;
        public int Locx;
        public int Locy;
        private string GridMine;
        private string theWPType;
        public string EditID="";
        public string EditID1;
        public bool theClassify;
        public string workplacetype;
        public string division;
        public string wpt;
        public string Valid;
        private string thePegNo;
        private string ReefWaste = "R";
        private int maxEndType = 0;
        private string ProbValid = "Y";
        private bool InActiveReason;
        public string ProbID = "";
        public string WpNo = "";
        public string WpDesc = "";
        public string SW = "";
        public string CW = "";
        public string FW = "";
        public string HW = "";
        public string Grade = "";
        public string RIF = "";
        public string DefaultAdvEdit = "";

        public string PegNo = "";
        public string PegValue = "";
        public string Letter1 = "";
        public string Letter2 = "";
        public string Letter3 = "";

        public string WPType = "";


        decimal prodmonth = 0;
        public string EndType = "";
        string Priority = "";

        public string Locked = "0";
        public string Edit = "";

        protected DateTime? dtPreviousStart;
        protected DateTime? dtPreviousEnd;
         string EndtypeIDLb = "";

        protected bool bIsLoading = true;
        protected string strCalType;
        public string edit = "";
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsWorkplaces _clsWorkplaces = new clsWorkplaces();
        public ileWorkplaceEdit()
        {
            InitializeComponent();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl20_Click(object sender, EventArgs e)
        {

        }

       public  void LoadWorkplace(string edit,string editid, int wptype)
        {
            TUserProduction up = new TUserProduction();
            up.SetUserInfo(UserCurrentInfo.UserID, this.theSystemDBTag, this.UserCurrentInfo.Connection);
            if (wptype == 0)
            {
                workplacetype = "S";
            }
            if(wptype == 1)
            {
                workplacetype = "D";
            }
            if (wptype == 3)
            {
                workplacetype = "OUG";
            }
            if (wptype == 2)
            {
                workplacetype = "SU";
            }
            Edit = edit;
            EditID = editid;
            //Workplacepanel.Visible = true;
            //Workplacepanel.Dock = DockStyle.Fill;
            //DesktopLocation = new Point(Locx - Width, Locy);
            Text = "Workplace Capture";
            WorkplaceIDTxt.Text = "";
            WorkplaceIDTxt.Enabled = false;
            txtReefDescription.Enabled = false;
            InActiveReason = false;
            theClassify = false;

            EndTypeLU.EditValue = "aa";

            this.lblWorkplaceCommonArea.Visible = false;
            this.cbeCommonArea.Visible = false;
            this.lblWorkplaceCommonAreaSubSection.Visible = false;
            this.cbeSubSections.Visible = false;
            this.lblStopingProcessCodes.Visible = false;
            this.cbeStopingProcessCodes.Visible = false;

            rbtHotCold.SelectedIndex = -1;
            //rbtCold.Checked = false;
            //rbtHot.Checked = false;
            rbtHotCold.Enabled = false;
            grpClassify.Enabled = false;
            cboxInActiveReason.Enabled = false;
            cbInactive.Enabled = false;

            //Stoping Velde
            //Statuscmb.Enabled = false;
            //RiskRatingtxt.Enabled = false;
            //DefaultAdvEdit.Enabled = false;
            txtDensity.Enabled = false;
            txtBrokenRockDensity.Enabled = false;
            rbReefWaste.Enabled = false;
            //rbReef.Enabled = false;
            //rbWaste.Enabled = false;
            //WPReefWasteRadio.Enabled = false;
            cbeCommonArea.Enabled = false;
            cbeSubSections.Enabled = false;
            cbeStopingProcessCodes.Enabled = false;

            //development velde
            PriorityCbx.Enabled = false;
            EndTypeLU.Enabled = false;
            EndHeightTxt.Enabled = false;
            EndWidthTxt.Enabled = false;
           // Statuscmb.Enabled = false;
            txtDensity.Enabled = false;
          //  RiskRatingtxt.Enabled = false;
            txtBrokenRockDensity.Enabled = false;
            rbReefWaste.Enabled = false;
            //rbReef.Enabled = false;
            //rbWaste.Enabled = false;
            //WPReefWasteRadio.Enabled = false;
            cbeCommonArea.Enabled = false;
            cbeSubSections.Enabled = false;
            WpDescTxt.Enabled = false;

            // wp name velde
            ddlWPType.Enabled = false;
            ddlWPLevel.Enabled = false;
            ddlGrid.Enabled = false;
            ddlDetail.Enabled = false;
            ddlDirection.Enabled = false;
            ddlNumber.Enabled = false;
            ddlDescription.Enabled = false;
            ddlDescriptionNumber.Enabled = false;
            ddlReef.Enabled = false ;
            ddlDivision.Enabled = false;

            //this.lblWorkplaceCommonArea.Visible = false;
            //this.cbeCommonArea.Visible = false;
            //this.lblWorkplaceCommonAreaSubSection.Visible = false;
            //this.cbeSubSections.Visible = false;
            //this.lblStopingProcessCodes.Visible = false;
            //this.cbeStopingProcessCodes.Visible = false;

            this.lblBrokenRockDensity.Visible = true;// Shaista Anjum Added BrokenRockDensity : 18/JAN/2013


            bool theEditable = false;
            bool theEditable2 = true;
            //if (SysSettings.IsCentralized.ToString() == "1")
            //{

            //    //theDiv = procs.ExtractBeforeColon(theDiv);
            //    MWDataManager.clsDataAccess _dbManA = new MWDataManager.clsDataAccess();
            //    _dbManA.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //    _dbManA.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    _dbManA.SqlStatement = " select Editable FROM CODE_WPDivision \r\n " +
            //        " where DivisionCode = '" + theDiv + "' ";
            //    _dbManA.ExecuteInstruction();
            //    DataTable dtDivision = _dbManA.ResultsDataTable;

            //    if (dtDivision.Rows.Count > 0)
            //    {
            //        if (dtDivision.Rows[0]["Editable"].ToString() == "1")
            //            theEditable = true;
            //    }
            //}

         

            if (Edit == "A")
            {
                WorkplaceIDTxt.Text = "";
                WpDescTxt.Text = "";
                ddlWPType.EditValue = "";
                ddlWPLevel.EditValue = "";
                ddlGrid.EditValue = "";
                ddlDetail.EditValue = "";
                ddlDirection.EditValue = "";
                ddlNumber.EditValue = "";
                ddlDescription.EditValue = "";
                ddlDescriptionNumber.EditValue = "";
                ddlReef.EditValue = "";
                ddlDivision.EditValue = "";

                cbeCommonArea.EditValue = "";
                cbeSubSections.EditValue = "";
                cbeStopingProcessCodes.EditValue = "";
                EndTypeLU.EditValue = "";
                cboxInActiveReason.EditValue = "";
                EndHeightTxt.Text = "";
                EndWidthTxt.Text = "";
                txtCreationDate.Text = "";
                PriorityCbx.Checked = false;
                cbInactive.Checked = false;
                //WorkplaceIDTxt.Enabled = true;

                cbInactive.Enabled = false;
                cbInactive.Checked = false;

                //if ((theEditable2 == true) & ((SysSettings.IsCentralized.ToString() == "0") ||
                //    ((SysSettings.IsCentralized.ToString() == "1") &
                //     (theEditable == true))))
                //{
                if(theEditable2 == true || theEditable == true)
                { 
                    switch (workplacetype)
                    {
                        case "S":
                            if ((up.WPEditAttribute == "Y") ||
                                (up.WPProduction == "Y"))
                            {
                                //Statuscmb.Enabled = true;
                                //RiskRatingtxt.Enabled = true;
                                //DefaultAdvEdit.Enabled = true;
                                txtDensity.Enabled = true;
                                txtBrokenRockDensity.Enabled = true;
                        rbReefWaste.Enabled = true;
                        //rbReef.Enabled = true;
                        //rbWaste.Enabled = true;
                        //WPReefWasteRadio.Enabled = true;

                                cbeCommonArea.Enabled = true;
                                cbeSubSections.Enabled = true;
                                cbeStopingProcessCodes.Enabled = true;
                            }
                            break;

                        case "D":
                            if ((up.WPEditAttribute == "Y") ||
                                (up.WPProduction == "Y"))
                            {

                                EndTypeLU.Enabled = true;
                                EndHeightTxt.Enabled = true;
                                EndWidthTxt.Enabled = true;
                                //Statuscmb.Enabled = true;
                                txtDensity.Enabled = true;
                                //RiskRatingtxt.Enabled = true;
                                txtBrokenRockDensity.Enabled = true;
                                 rbReefWaste.Enabled = true;
                                //rbReef.Enabled = true;
                                //rbWaste.Enabled = true;
                                //WPReefWasteRadio.Enabled = true;
                                cbeCommonArea.Enabled = true;
                                cbeSubSections.Enabled = true;
                                PriorityCbx.Enabled = true;
                            }
                            break;
                        case "SU":
                            {
                             
                                lblStatus.Text = "Status : Pending Classification";
                            }
                            break;
                    }

                    if ((up.WPEditName == "Y") ||
                        ((up.WPProduction == "Y") & (workplacetype == "S")) ||
                        ((up.WPProduction == "Y") & (workplacetype == "D")) ||
                        ((up.WPSurface == "Y") & (workplacetype == "SU")) ||
                        ((up.WPUnderGround == "Y") & (workplacetype == "OUG")))
                    {
                        if (Edit == "A")
                            ddlDivision.Enabled = true;
                        ddlWPType.Enabled = true;
                        ddlWPLevel.Enabled = true;
                        ddlGrid.Enabled = true;
                        ddlDetail.Enabled = true;
                        ddlDirection.Enabled = true;
                        ddlNumber.Enabled = true;
                        ddlDescription.Enabled = true;
                        ddlDescriptionNumber.Enabled = true;
                        ddlReef.Enabled = true;
                    }
                }
            }
            else  // else van add
            {

                //if ((SysSettings.IsCentralized.ToString() == "0") ||
                //    ((SysSettings.IsCentralized.ToString() == "1") &
                //     (theEditable == true)))
                if(theEditable == true || theEditable2 == true )
                {
                    //DefaultAdvEdit.Enabled = false;
                    //RiskRatingtxt.Enabled = false;
                    //Statuscmb.Enabled = false;
                    //Line2Txt.Enabled = false;
                    //LineTxt.Enabled = false;
                    //WPReefWasteRadio.Enabled = false;

                    switch (workplacetype)
                    {
                        case "S":
                            if (up.WPEditAttribute == "Y")
                            {
                                cbInactive.Enabled = true;
                                //Statuscmb.Enabled = true;
                                //RiskRatingtxt.Enabled = true;
                                //DefaultAdvEdit.Enabled = true;
                                txtDensity.Enabled = true;
                                txtBrokenRockDensity.Enabled = true;
                            rbReefWaste.Enabled = true;
                            //rbReef.Enabled = true;
                            //rbWaste.Enabled = true;
                            //WPReefWasteRadio.Enabled = true;
                                cbeCommonArea.Enabled = true;
                                cbeSubSections.Enabled = true;
                                cbeStopingProcessCodes.Enabled = true;
                          //  ddlReef.Enabled = true;
                                 }
                            break;

                        case "D":
                            if (up.WPEditAttribute == "Y")
                            {
                                cbInactive.Enabled = true;
                                PriorityCbx.Enabled = true;
                                EndTypeLU.Enabled = true;
                                EndHeightTxt.Enabled = true;
                                EndWidthTxt.Enabled = true;
                                //Statuscmb.Enabled = true;
                                txtDensity.Enabled = true;
                                //RiskRatingtxt.Enabled = true;
                                txtBrokenRockDensity.Enabled = true;
                            //rbReef.Enabled = true;
                            //rbWaste.Enabled = true;
                            rbReefWaste.Enabled = true;
                                //WPReefWasteRadio.Enabled = true;
                                cbeCommonArea.Enabled = true;
                                cbeSubSections.Enabled = true;
                                //  ddlReef.Enabled = true;
                            }
                            break;
                        case "SU":
                            if (up.WPEditAttribute == "Y")
                                cbInactive.Enabled = true;
                            break;
                        case "OUG":
                            if (up.WPEditAttribute == "Y")
                                cbInactive.Enabled = true;
                            break;
                    }
                    if (up.WPEditName == "Y")
                    {
                        if (Edit == "A")
                        {
                            ddlDivision.Enabled = true;
                        }
                        ddlWPType.Enabled = true;
                        ddlWPLevel.Enabled = true;
                        ddlGrid.Enabled = true;
                        ddlDetail.Enabled = true;
                        ddlDirection.Enabled = true;
                        ddlNumber.Enabled = true;
                        ddlDescription.Enabled = true;
                        ddlDescriptionNumber.Enabled = true;
                        ddlReef.Enabled = true;
                   // }
                    }
                    if (up.WPClassify == "Y")
                    {
                        if (theClassifyNeeded == true)
                        {
                            rbtHotCold.Enabled = true;
                        rbtHotCold.Visible = true;
                        //rbtCold.Enabled = true;
                        //rbtHot.Enabled = true;
                        grpClassify.Enabled = true;
                            //rbtCold.Visible = true;
                            //rbtHot.Visible = true;
                            grpClassify.Visible = true;
                            txtClassificationDate.Visible = true;
                            lblClassificationDate.Visible = true;
                        }
                    }
                }
            }
            switch (workplacetype)
            {
                case "S":
                    //label67.Visible = true;
                    //Statuscmb.Visible = true;

                    //RiskRatingtxt.Visible = true;
                    //label74.Visible = true;

                    //DefaultAdvEdit.Visible = true;
                    //label75.Visible = true;

                    lblReef.Visible = true;
                    ddlReef.Visible = true;
                    txtReefDescription.Visible = true;

                    EndTypeLU.Visible = false ;
                    EndTypeLb.Visible = false;

                    EndHeightTxt.Visible = false;
                    EndWidthTxt.Visible = false;
                    EndheightLb.Visible = false;
                    EndwidthLb.Visible = false;

                    txtDensity.Visible = true;
                    lblDensity.Visible = true;

                    this.txtBrokenRockDensity.Visible = true;// Shaista Anjum Added BrokenRockDensity : 18/JAN/2013
                    this.lblBrokenRockDensity.Visible = true;// Shaista Anjum Added BrokenRockDensity : 18/JAN/2013



                    MWDataManager.clsDataAccess _dbMan11 = new MWDataManager.clsDataAccess();
                    _dbMan11.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan11.SqlStatement = "select convert(decimal(18,2),BrokenRockDensity)BrokenRockDensity,convert(decimal(18,2),RockDensity)RockDensity from sysset ";

                    _dbMan11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan11.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan11.ExecuteInstruction();

                    if (Edit == "A")
                    {
                        this.txtDensity.Text =Convert.ToDecimal(_dbMan11.ResultsDataTable.Rows[0]["RockDensity"].ToString()).ToString();
                        this.txtBrokenRockDensity.Text = Convert.ToDecimal(_dbMan11.ResultsDataTable.Rows[0]["BrokenRockDensity"].ToString()).ToString();
                        //  rbReef.Checked = true;
                        rbReefWaste.SelectedIndex = 0;
                    }

                    //WPReefRadio_SelectedIndexChanged(null, null);
                    gbReefWaste.Visible = true;

                    this.lblWorkplaceCommonArea.Visible = true;
                    this.cbeCommonArea.Visible = true;
                    this.lblWorkplaceCommonAreaSubSection.Visible = true;
                    this.cbeSubSections.Visible = true;
                    this.lblStopingProcessCodes.Visible = true;
                    this.cbeStopingProcessCodes.Visible = true;
                    break;

                case "D":
                    PriorityCbx.Visible = true;

                    EndTypeLU.Visible = true;
                    EndTypeLb.Visible = true;

                    EndHeightTxt.Visible = true;
                    EndWidthTxt.Visible = true;
                    EndheightLb.Visible = true;
                    EndwidthLb.Visible = true;

                    lblReef.Visible = true;
                    ddlReef.Visible = true;
                    txtReefDescription.Visible = true;

                    //Statuscmb.Visible = false;
                    //label67.Visible = false;

                    txtDensity.Visible = true;
                    lblDensity.Visible = true;

                    //RiskRatingtxt.Visible = false;
                    //label74.Visible = false;

                    this.txtBrokenRockDensity.Visible = true;// Shaista Anjum Added BrokenRockDensity : 18/JAN/2013
                    this.lblBrokenRockDensity.Visible = true;// Shaista Anjum Added BrokenRockDensity : 18/JAN/2013


                    gbReefWaste.Visible = true;

                    MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                    _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan1.SqlStatement = "select BrokenRockDensity, RockDensity  from sysset ";

                    _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan1.ExecuteInstruction();

                    if (Edit == "A")
                    {
                        this.txtDensity.Text = Convert.ToDecimal(_dbMan1.ResultsDataTable.Rows[0]["RockDensity"].ToString()).ToString();
                        this.txtBrokenRockDensity.Text = Convert.ToDecimal(_dbMan1.ResultsDataTable.Rows[0]["BrokenRockDensity"].ToString()).ToString();
                        //  rbReef.Checked = true;
                        rbReefWaste.SelectedIndex = 0;
                    }

                    this.lblWorkplaceCommonArea.Visible = true;
                    this.cbeCommonArea.Visible = true;
                    this.lblWorkplaceCommonAreaSubSection.Visible = true;
                    this.cbeSubSections.Visible = true;

                    break;
                case "SU":
                    //WorkplaceIDTxt.Text = "SU";
                    //WorkplaceIDTxt.Text = "";
                    EndTypeLU.Visible = false;
                    EndTypeLb.Visible = false;

                    EndHeightTxt.Visible = false;
                    EndWidthTxt.Visible = false;
                    EndheightLb.Visible = false;
                    EndwidthLb.Visible = false;
                    // this.lblBrokenRockDensity.Visible = true ;
                    //Statuscmb.Visible = false;
                    //label67.Visible = false;

                    //RiskRatingtxt.Visible = false;
                    //label74.Visible = false;

                    //Line2Txt.Visible = false;
                    //Line.Visible = false;
                    //LineTxt.Visible = false;
                    //Panel.Visible = false;

                    lblReef.Visible = false;
                    ddlReef.Visible = false;
                    txtReefDescription.Visible = false;
                    txtDensity.Enabled = false;
                    //RiskRatingtxt.Enabled = true;
                    txtBrokenRockDensity.Enabled = false;
                    this.lblBrokenRockDensity.Visible = false;
                    txtBrokenRockDensity.Visible = false;// Shaista Anjum Added BrokenRockDensity : 18/JAN/2013
                    lblBrokenRockDensity.Visible = false;
                    lblDensity.Visible = false;
                    txtDensity.Visible = false;
                    //lblReef.Visible = true;
                    //ddlReef.Visible = true;
                    //txtReefDescription.Visible = true;
                    //WPReefWasteRadio.Visible = false;
                    //rbtCold.Visible = false;
                    //rbtHot.Visible = false

                    //grpClassify.Visible = false;
                    //txtClassificationDate.Visible = false;
                    //lblClassificationDate.Visible = false;
                    break;
                case "OUG":
                    //WorkplaceIDTxt.Text = "OUG";
                    //WorkplaceIDTxt.Text = "";
                    EndTypeLU.Visible = false;
                    EndTypeLb.Visible = false;

                    EndHeightTxt.Visible = false;
                    EndWidthTxt.Visible = false;
                    EndheightLb.Visible = false;
                    EndwidthLb.Visible = false;
                    txtDensity.Enabled = false;
                    //RiskRatingtxt.Enabled = true;
                    txtBrokenRockDensity.Enabled = false;
                    this.lblBrokenRockDensity.Visible = false;
                    txtBrokenRockDensity.Visible = false;// Shaista Anjum Added BrokenRockDensity : 18/JAN/2013
                    lblBrokenRockDensity.Visible = false;
                    lblDensity.Visible = false;
                    txtDensity.Visible = false;
                    //Statuscmb.Visible = false;
                    //label67.Visible = false;

                    //RiskRatingtxt.Visible = false;
                    //label74.Visible = false;

                    //Line2Txt.Visible = false;
                    //Line.Visible = false;
                    //LineTxt.Visible = false;
                    //Panel.Visible = false;

                    lblReef.Visible = false;
                    ddlReef.Visible = false;
                    txtReefDescription.Visible = false;

                    gbReefWaste.Visible = false;
                    //WPReefWasteRadio.Visible = false;
                    break;
            }

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag , UserCurrentInfo.Connection);
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;

            //Load Division
            //if (SysSettings.IsCentralized.ToString() == "0")
            //{
                _dbMan.SqlStatement = "    select DivisionCode+':'+Description Division FROM CODE_WPDivision WHERE Selected = 'Y' ORDER BY DivisionCode";
                _dbMan.ExecuteInstruction();
                DataTable dtDivision = _dbMan.ResultsDataTable;
            ddlDivision.Properties.DataSource = dtDivision;
            ddlDivision.Properties.DisplayMember = "Division";
            ddlDivision.Properties.ValueMember = "Division";


            //    if (dtDivision.Rows.Count > 0)
            //    {
            //        foreach (DataRow r in dtDivision.Rows)
            //            ddlDivision.Properties.Items.Add(r["DivisionCode"].ToString() + ":" + r["Description"].ToString());
            //    }
            //}
            //else
            //{
            //    ddlDivision.Properties.Items.Add(theDiv);
            //    ddlDivision.Text = theDiv;
            //    ddlDivision.Enabled = false;
            //    LoadDivision();
            //}

            //Load WPType
            _dbMan.SqlStatement = " select t.TypeCode+':'+ t.Description WorkplaceType FROM WPType_Setup s \r\n " +
                                  " inner join CODE_WPTYPE t on \r\n " +
                                  "   t.TypeCode = s.TypeCode \r\n " +
                                  " WHERE s.SetupCode = '" + workplacetype + "' and t.Selected = 'Y' ";
            _dbMan.ExecuteInstruction();
            DataTable dtWPType = _dbMan.ResultsDataTable;
            ddlWPType.Properties.DataSource = dtWPType;
            ddlWPType.Properties.DisplayMember = "WorkplaceType";
            ddlWPType.Properties.ValueMember = "WorkplaceType";

            //if (dtWPType.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtWPType.Rows)
            //        ddlWPType.Properties.Items.Add(r["TypeCode"].ToString() + ":" + r["Description"].ToString());
            //}


  
            //Load Reef
            _dbMan.SqlStatement = " select cast(ReefID as varchar(20))+':'+Description Reef FROM Reef WHERE Selected = '1' ORDER BY Description";
            _dbMan.ExecuteInstruction();
            DataTable dtReef = _dbMan.ResultsDataTable;
            ddlReef.Properties.DataSource = dtReef;
            ddlReef.Properties.DisplayMember = "Reef";
            ddlReef.Properties.ValueMember = "Reef";
            //if (dtReef.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtReef.Rows)
            //        ddlReef.Properties.Items.Add(r["ReefID"].ToString() + ":" + r["Description"].ToString());
            //}

            //Load EndType
            _dbMan.SqlStatement = " select Description [EndType], EndWidth Width, EndHeight Height, reefwaste [R/W], endtypeid from endtype ";
            _dbMan.ExecuteInstruction();
            DataTable dtEndType = _dbMan.ResultsDataTable;

            if (dtEndType.Rows.Count > 0)
            {
                EndTypeLU.Properties.DataSource = dtEndType;
                EndTypeLU.Properties.DisplayMember = "EndType";
                EndTypeLU.Properties.ValueMember = "EndType";
            }

            //_dbMan.SqlStatement = "Select * from StatusUpdate ";
            //_dbMan.ExecuteInstruction();
            //DataTable dtStatus = _dbMan.ResultsDataTable;

            //if (dtStatus.Rows.Count > 0)
            //{
            //    foreach (DataRow drStatus in dtStatus.Rows)
            //        Statuscmb.Items.Add(drStatus["Status"].ToString());
            //}

            _dbMan.SqlStatement = "SELECT cast(Id as varchar(20))+':'+Name ProcessCodes FROM StopingProcessCodes ORDER BY Name";
            _dbMan.ExecuteInstruction();
            DataTable dtProcessCodes = _dbMan.ResultsDataTable;
            cbeStopingProcessCodes.Properties.DataSource = dtProcessCodes;
            cbeStopingProcessCodes.Properties.DisplayMember = "ProcessCodes";
            cbeStopingProcessCodes.Properties.ValueMember = "ProcessCodes";
            //if (dtProcessCodes.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtProcessCodes.Rows)
            //        this.cbeStopingProcessCodes.Properties.Items.Add(r["Id"].ToString() + ":" + r["Name"].ToString());
            //}

            ////Load Inactive/Active Reasons
            //_dbMan.SqlStatement = " select * FROM WorkPlace_Inactivation_Reason order by Reason";
            //_dbMan.ExecuteInstruction();

            //Load Inactive/Active Reasons
            _dbMan.SqlStatement = " select Reason from WorkPlace_Inactivation_Reason \r\n " +
                                  " where Inactive = 'N' and Selected = 'Y'  ";
            _dbMan.ExecuteInstruction();
            DataTable dtInactiveReason = _dbMan.ResultsDataTable;
            cboxInActiveReason.Properties.DataSource = dtInactiveReason;
            cboxInActiveReason.Properties.DisplayMember = "Reason";
            cboxInActiveReason.Properties.ValueMember = "Reason";
            //if (dtInactiveReason.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtInactiveReason.Rows)
            //        cboxInActiveReason.Properties.Items.Add(r["Reason"].ToString());
            //}

            //Load Inactive/Active Reasons date
            try
            {
                _dbMan.SqlStatement = " select min(CalendarDate) theDate FROM WorkPlace_Classification \r\n " +
                            " where WorkplaceID = '" + EditID + "' ";
                _dbMan.ExecuteInstruction();
                DataTable dtClasDate = _dbMan.ResultsDataTable;

                if (dtClasDate.Rows.Count > 0)
                {
                    txtClassificationDate.Text = dtClasDate.Rows[0]["theDate"].ToString();
                }
            }
            catch (Exception ex)
            {
            }

            if (EditID != "")
            {
                WorkplaceIDTxt.Text = EditID;
                WorkplaceIDTxt.ReadOnly = true;

                // load user info
                String sqlStatement = "";
                sqlStatement += "SELECT w.*, o.name Lvl, e.description End1, \r\n";
                sqlStatement += "Division = cwpd.DivisionCode + ':' + cwpd.Description, \r\n";
                sqlStatement += "WPType = cwpt.TypeCode + ':' + cwpt.Description, \r\n";
                sqlStatement += "WPLevel = o.LevelNumber + ':' + o.Name, \r\n";
                sqlStatement += "Grid = cg.Grid + ':' + cg.Description, \r\n";
                sqlStatement += "Detail = cwd.DetailCode + ':' + cwd.Description, \r\n";
                sqlStatement += "Directions = cd.Direction + ':' + cd.Description, \r\n";
                sqlStatement += "Number = cwpn.NumberCode + ':' + cwpn.Description, \r\n";
                sqlStatement += "Descr = cwdesc.DescrCode + ':' + cwdesc.Description, \r\n";
                sqlStatement += "DescrNo = cwdescn.DescrNumberCode + ':' + cwdescn.Description, \r\n";
                sqlStatement += "Density = isnull(w.Density, 0), \r\n";
                sqlStatement += "WPStatus = isnull(w.WPStatus, ''), \r\n";
                sqlStatement += "Classification = isnull(w.Classification, '') \r\n";
                if (workplacetype == "S" || workplacetype == "D") sqlStatement += ", Reef = Convert(varchar, r.ReefID) + ':' + r.Description \r\n";
                sqlStatement += "FROM Workplace w \r\n";
                sqlStatement += "left join oreflowentities o on w.oreflowid = o.oreflowid and w.DivisionCode = o.Division \r\n";
                sqlStatement += "left join endtype e on w.endtypeid = e.endtypeid \r\n";
                sqlStatement += "left join CODE_DIRECTION cd on w.Direction = cd.Direction \r\n";
                sqlStatement += "left join CODE_WPDIVISION cwpd on w.DivisionCode = cwpd.DivisionCode \r\n";
                sqlStatement += "left join CODE_WPTYPE cwpt on w.TypeCode = cwpt.TypeCode \r\n";
                sqlStatement += "left join CODE_GRID cg on w.GridCode = cg.Grid \r\n";
                sqlStatement += "left join CODE_WPDETAIL cwd on w.DetailCode = cwd.DetailCode \r\n";
                sqlStatement += "left join CODE_WPNUMBER cwpn on w.NumberCode = cwpn.NumberCode \r\n";
                sqlStatement += "left join CODE_WPDESCRIPTION cwdesc on w.DescrCode = cwdesc.DescrCode \r\n";
                sqlStatement += "left join CODE_WPDESCRIPTIONNO cwdescn on w.DescrNoCode = cwdescn.DescrNumberCode \r\n";
                sqlStatement += "left join REEF r on w.ReefID = r.ReefID \r\n";
                sqlStatement += "WHERE Workplaceid = '" + EditID + "' \r\n";
                _dbMan.SqlStatement = sqlStatement;
                _dbMan.ExecuteInstruction();
                DataTable SubA = _dbMan.ResultsDataTable;
                if (SubA.Rows.Count == 0)
                { }
                else
                {
                    theWPType = ExtractBeforeColon(SubA.Rows[0]["WPType"].ToString());
                    theDiv = ExtractBeforeColon(SubA.Rows[0]["Division"].ToString());
                }
                Load_WPTypeSelection();
                //if (clsUserInfo.WPClassify == "Y")
                //{
                    if (theClassifyNeeded == true)
                    {
                        //rbtCold.Enabled = true;
                        //rbtHot.Enabled = true;
                        rbtHotCold.Enabled = true;
                        grpClassify.Enabled = true;
                        //rbtCold.Visible = true;
                        //rbtHot.Visible = true;
                        grpClassify.Visible = true;
                        txtClassificationDate.Visible = true;
                        lblClassificationDate.Visible = true;
                    }
                //}
                //WpDescriptionTxt.Text = SubA.Rows[0]["Description"].ToString();
                //if (SubA.Rows.Count == 0)
                //{
                //}
                //else
                //{
                    ddlDivision.EditValue = SubA.Rows[0]["Division"].ToString();
                    ddlWPType.EditValue = SubA.Rows[0]["WPType"].ToString();
                    //ddlWPLevel.SelectedText = SubA.Rows[0]["OreFlowID"].ToString();
                    ddlWPLevel.EditValue = SubA.Rows[0]["OreFlowID"].ToString();
                    // ddlWPLevel.EditValue = SubA.Rows[0]["OreFlowID"].ToString();
                    //levelIDlbl.Text = SubA.Rows[0]["OreFlowID"].ToString();
                    //ddlGrid.Text = SubA.Rows[0]["Grid"].ToString();
                    ddlGrid.EditValue = SubA.Rows[0]["Grid"].ToString();
                    //ddlGrid.SelectedItem = SubA.Rows[0]["Grid"].ToString();
                    //ddlDetail.Text = SubA.Rows[0]["Detail"].ToString();
                    ddlDetail.EditValue = SubA.Rows[0]["Detail"].ToString();
                    //ddlDetail.SelectedItem = SubA.Rows[0]["Detail"].ToString();
                    //ddlDirection.Text = SubA.Rows[0]["Directions"].ToString();
                    ddlDirection.EditValue = SubA.Rows[0]["Directions"].ToString();
                    //ddlDirection.SelectedItem = SubA.Rows[0]["Directions"].ToString();
                    //ddlNumber.Text = SubA.Rows[0]["Number"].ToString();
                    ddlNumber.EditValue = SubA.Rows[0]["Number"].ToString();
                    //ddlNumber.SelectedItem = SubA.Rows[0]["Number"].ToString();
                    //ddlDescription.Text = SubA.Rows[0]["Descr"].ToString();
                    ddlDescription.EditValue = SubA.Rows[0]["Descr"].ToString();
                    //ddlDescription.SelectedItem = SubA.Rows[0]["Descr"].ToString();
                    //ddlDescriptionNumber.Text = SubA.Rows[0]["DescrNo"].ToString();
                    ddlDescriptionNumber.EditValue = SubA.Rows[0]["DescrNo"].ToString();
                    //ddlDescriptionNumber.SelectedItem = SubA.Rows[0]["DescrNo"].ToString();

                    //Statuscmb.Text = SubA.Rows[0]["StopingCode"].ToString();
                    //RiskRatingtxt.Text = SubA.Rows[0]["RiskRating"].ToString();

                    txtCreationDate.Text = SubA.Rows[0]["CreationDate"].ToString();

                    if (SubA.Rows[0]["Priority"].ToString() == "Y") PriorityCbx.Checked = true; else PriorityCbx.Checked = false;

                if (SubA.Rows[0]["WPStatus"].ToString() == "P")
                    lblStatus.Text = "Status : Pending Classisfication";
                if (SubA.Rows[0]["WPStatus"].ToString() == "A")
                    lblStatus.Text = "Status : Active";
                if (SubA.Rows[0]["WPStatus"].ToString() == "I")
                    lblStatus.Text = "Status : InActive";

                cbInactive.Checked = false  ;
                    if (SubA.Rows[0]["InActive"].ToString() != "N")
                        cbInactive.Checked = true  ;

                    //rbtCold.Checked = false;
                    //rbtHot.Checked = false;
                    if (SubA.Rows[0]["Classification"].ToString() == "C")
                        rbtHotCold.SelectedIndex = 1;
                    //rbtCold.Checked = true;
                    else
                        if (SubA.Rows[0]["Classification"].ToString() == "H")
                        rbtHotCold.SelectedIndex = 0;
                    //rbtHot.Checked = true;

                    //LineTxt.Text = SubA.Rows[0]["Line"].ToString();
                    //Line2Txt.Text = SubA.Rows[0]["Cap"].ToString();

                    //WPReefWasteRadio.SelectedIndex = 1;

                    gbReefWaste.Visible = true;

                    EndTypeLU.Text = SubA.Rows[0]["end1"].ToString();
                    //EndtypeIDLb.Text = SubA.Rows[0]["EndTypeID"].ToString();
                    EndHeightTxt.Text = SubA.Rows[0]["endheight"].ToString();
                    EndWidthTxt.Text = SubA.Rows[0]["endwidth"].ToString();
                //DefaultAdvEdit.Text = SubA.Rows[0]["defaultadv"].ToString();

                if (workplacetype == "D")
                {
                    PriorityCbx.Visible = true;
                    lblReef.Visible = true;
                    ddlReef.Visible = true;
                }


                    if (SubA.Rows[0]["reefwaste"].ToString() == "R")
                    {
                        //WPReefWasteRadio.SelectedIndex = 0;
                        rbReefWaste.SelectedIndex = 0;
                        //rbReef.Checked = true;
                        //rbWaste.Checked = false;
                    }
                    else
                    {
                        //rbReef.Checked = false;
                        //rbWaste.Checked = true;
                        rbReefWaste.SelectedIndex = 1;
                        //WPReefWasteRadio.SelectedIndex = 1;
                    }

                    if (workplacetype == "S" || workplacetype == "D")
                    {
                    //  ddlReef.Text = SubA.Rows[0]["Reef"].ToString();
                    lblReef.Visible = true;
                    ddlReef.Visible = true;
                    ddlReef.EditValue = SubA.Rows[0]["Reef"].ToString();
                    txtDensity.Text = SubA.Rows[0]["Density"].ToString();
                        this.txtBrokenRockDensity.Text = SubA.Rows[0]["BrokenRockDensity"].ToString();// Shaista Anjum Added for BrokenRockDensity : 18/JAN/2013                   
                        string subSection = SubA.Rows[0]["SubSection"].ToString();

                        if (!string.IsNullOrEmpty(subSection))
                        {
                            _dbMan.SqlStatement = "SELECT (CAST(CA.Id AS VARCHAR(100)) + ':' + CA.Name ) AS Area, (CAST(CASS.Id AS VARCHAR(100)) + ':' + CASS.Name ) AS SubSection FROM CommonAreas AS CA INNER JOIN CommonAreaSubSections AS CASS ON CA.Id = CASS.CommonArea WHERE CASS.Id=" + subSection;
                            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _dbMan.ExecuteInstruction();
                            if (_dbMan.ResultsDataTable != null && _dbMan.ResultsDataTable.Rows.Count > 0)
                            {
                                this.cbeCommonArea.EditValue = _dbMan.ResultsDataTable.Rows[0]["Area"].ToString();
                                this.cbeSubSections.EditValue = _dbMan.ResultsDataTable.Rows[0]["SubSection"].ToString();
                            }
                        }

                        string strProcessCode = SubA.Rows[0]["ProcessCode"].ToString();

                        if (!string.IsNullOrEmpty(strProcessCode))
                        {
                            _dbMan.SqlStatement = "SELECT * FROM StopingProcessCodes WHERE Id=" + strProcessCode;
                            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                            _dbMan.ExecuteInstruction();
                            if (_dbMan.ResultsDataTable != null && _dbMan.ResultsDataTable.Rows.Count > 0)
                            {
                                this.cbeStopingProcessCodes.EditValue = _dbMan.ResultsDataTable.Rows[0]["Id"].ToString() + ":" + _dbMan.ResultsDataTable.Rows[0]["Name"].ToString();
                            }
                        }

                    }
               // }
            }
            else
            {
                //if (SysSettings.IsCentralized.ToString() == "1")
                //{
                //    MWDataManager.clsDataAccess _dbManA = new MWDataManager.clsDataAccess();
                //    _dbManA.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //    _dbManA.queryReturnType = MWDataManager.ReturnType.DataTable;

                //    //Load Division
                //    _dbManA.SqlStatement = " select Division = divisionCode + ':' + Description FROM CODE_WPDivision WHERE Selected = 'Y' and DivisionCode = '" + theDiv + "' ";
                //    _dbManA.ExecuteInstruction();
                //    DataTable dtDivA = _dbManA.ResultsDataTable;

                //    if (dtDivA.Rows.Count > 0)
                //    {
                //        ddlDivision.Text = dtDivA.Rows[0]["Division"].ToString();
                //        ddlDivision.Enabled = false;
                //    }
                //}
            }
        }

        private void Load_WPTypeSelection()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;

            //get Classification        
            _dbMan.SqlStatement = " select Classification from Code_WPType \r\n " +
                        " where TypeCode = '" + theWPType + "' \r\n ";
            _dbMan.ExecuteInstruction();
            DataTable dtClassify = _dbMan.ResultsDataTable;

            theClassifyNeeded = false;
            if (dtClassify.Rows.Count > 0)
            {
                if (dtClassify.Rows[0]["Classification"].ToString() == "Y")
                {
                    lblStatus.Text = "Status : Pending Classification";
                    theClassifyNeeded = true;
                }
                else
                { }
                lblStatus.Text = "Status : Active";
            }
            //Load Level           
          //  _dbMan.SqlStatement = " select o.LevelNumber, o.Name, (o.LevelNumber + ':' + o.Name) LevelName, o.OreFlowID \r\n " +
        _dbMan.SqlStatement = " select (o.LevelNumber + ':' + o.Name) LevelName, o.OreFlowID \r\n " +
                        " from CODE_WPTypeLevelLink w \r\n " +
                        " inner join OREFLOWENTITIES o on \r\n " +
                        "   o.OreFlowCode = 'Lvl' and \r\n " +
                        "   o.OreFlowID = w.OreflowID and \r\n " +
                        "   o.Division = w.Division \r\n " +
                        " where w.Division = '" + theDiv + "' and w.TypeCode = '" + theWPType + "' \r\n ";
            _dbMan.ExecuteInstruction();
            DataTable dtLevel = _dbMan.ResultsDataTable;
            ddlWPLevel.Properties.DataSource = null;
            ddlWPLevel.Properties.DataSource = _dbMan.ResultsDataTable;
            //if (dtLevel.Rows.Count > 0)
            //{
                ddlWPLevel.Properties.DisplayMember = "LevelName";
                ddlWPLevel.Properties.ValueMember = "OreFlowID";
               // ddlWPLevel.Properties.DataSource = _dbMan.ResultsDataTable;
            //}
            ddlWPLevel.Text = "";
            //Load Grid

           // _dbMan.SqlStatement = " select g.Grid+':'+Description Grid, g.*  from CODE_WPTypeGridLink w \r\n " +
                  _dbMan.SqlStatement = " select g.Grid+':'+Description Grid   from CODE_WPTypeGridLink w \r\n " +
                    " inner join Code_Grid g on \r\n " +
                    "   g.Division = w.Division and \r\n " +
                    "   g.Grid = w.Grid \r\n " +
                    " where w.Division = '" + theDiv + "' and w.TypeCode = '" + theWPType + "' ";
            _dbMan.ExecuteInstruction();
            DataTable dtGrid = _dbMan.ResultsDataTable;
            ddlGrid.Properties.DataSource = null;
            ddlGrid.Properties.DataSource = _dbMan.ResultsDataTable;
            ddlGrid.Properties.DisplayMember = "Grid";
            ddlGrid.Properties.ValueMember = "Grid";
            
       
            //if (dtGrid.Rows.Count > 0)
            //{
            //    foreach (DataRow r in _dbMan.ResultsDataTable.Rows)
            //        ddlGrid.Properties.Items.Add(r["Grid"].ToString() + ":" + r["Description"].ToString());
            //}
            //ddlGrid.Text = "";
            ddlGrid.ItemIndex  = -1;

            //Load Detail
           // _dbMan.SqlStatement = " select d.DetailCode+':'+Description Detail,d.* from CODE_WPTypeWPDetailLink wp \r\n " +
                 _dbMan.SqlStatement = " select d.DetailCode+':'+Description Detail  from CODE_WPTypeWPDetailLink wp \r\n " +
                        " inner join CODE_WPDetail d on \r\n " +
                        "   d.DetailCode = wp.DetailCode and \r\n " +
                        "   d.Selected = 'Y' \r\n " +
                        " where wp.TypeCode = '" + theWPType + "' \r\n " +
                        " ORDER BY d.DetailCode ";
            _dbMan.ExecuteInstruction();
            DataTable dtDetail = _dbMan.ResultsDataTable;
            ddlDetail.Properties.DataSource = null;
            ddlDetail.Properties.DataSource = _dbMan.ResultsDataTable;
            ddlDetail.Properties.DisplayMember = "Detail";
            ddlDetail.Properties.ValueMember = "Detail";
          
            //if (dtDetail.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtDetail.Rows)
            //        ddlDetail.Properties.Items.Add(r["DetailCode"].ToString() + ":" + r["Description"].ToString());
            //}
            //ddlDetail.Text = "";
            ddlDetail.ItemIndex  = -1;

            //Load Direction
            //_dbMan.SqlStatement = " select * FROM CODE_Direction";
            //_dbMan.SqlStatement = " select d.Direction+':'+Description Direction, d.* from CODE_WPTypeWPDirectionLink wp \r\n " +
                  _dbMan.SqlStatement = " select d.Direction+':'+Description Direction from CODE_WPTypeWPDirectionLink wp \r\n " +
                        " inner join CODE_Direction d on \r\n " +
                        "   d.Direction = wp.Direction \r\n " +
                        " where wp.TypeCode = '" + theWPType + "' \r\n " +
                        " ORDER BY d.Direction ";
            _dbMan.ExecuteInstruction();
            DataTable dtDirection = _dbMan.ResultsDataTable;
            ddlDirection.Properties.DataSource = null;
            ddlDirection.Properties.DataSource = _dbMan.ResultsDataTable;
            ddlDirection.Properties.DisplayMember = "Direction";
            ddlDirection.Properties.ValueMember = "Direction";
         
            //if (dtDirection.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtDirection.Rows)
            //        ddlDirection.Properties.Items.Add(r["Direction"].ToString() + ":" + r["Description"].ToString());
            //}
            ddlDirection.ItemIndex = -1;
            //ddlDirection.Text = "";

            //Load Number
            //_dbMan.SqlStatement = " select * FROM CODE_WPNumber WHERE Selected = 'Y' ORDER BY NumberCode";
          //  _dbMan.SqlStatement = " select n.NumberCode+':'+Description Number, n.* from CODE_WPTypeWPNumberLink wp \r\n " +
                 _dbMan.SqlStatement = " select n.NumberCode+':'+Description Number  from CODE_WPTypeWPNumberLink wp \r\n " +
                        " inner join CODE_WPNumber n on \r\n " +
                        "   n.NumberCode = wp.NumberCode and \r\n " +
                        "   n.Selected = 'Y' \r\n " +
                        " where wp.TypeCode = '" + theWPType + "' \r\n " +
                        " ORDER BY n.NumberCode ";
            _dbMan.ExecuteInstruction();
            DataTable dtNumber = _dbMan.ResultsDataTable;
            ddlNumber.Properties.DataSource = null;
            ddlNumber.Properties.DataSource = _dbMan.ResultsDataTable;
            ddlNumber.Properties.DisplayMember = "Number";
            ddlNumber.Properties.ValueMember = "Number";
        
            //if (dtNumber.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtNumber.Rows)
            //        ddlNumber.Properties.Items.Add(r["NumberCode"].ToString() + ":" + r["Description"].ToString());
            //}
            //ddlNumber.SelectedText = "";
            ddlNumber.ItemIndex = -1;

            //Load Description
            //_dbMan.SqlStatement = " select * FROM CODE_WPDescription WHERE Selected = 'Y' ORDER BY DescrCode";
            //_dbMan.SqlStatement = " select d.DescrCode+':'+Description Description, d.* from CODE_WPTypeWPDescLink wp \r\n " +
                  _dbMan.SqlStatement = " select d.DescrCode+':'+Description Description  from CODE_WPTypeWPDescLink wp \r\n " +
                        " inner join CODE_WPDescription d on \r\n " +
                        "   d.DescrCode = wp.DescrCode and \r\n " +
                        "   d.Selected = 'Y' \r\n " +
                        " where wp.TypeCode = '" + theWPType + "' \r\n " +
                        " ORDER BY d.DescrCode ";
            _dbMan.ExecuteInstruction();
            DataTable dtDescription = _dbMan.ResultsDataTable;
            ddlDescription.Properties.DataSource = null;
            ddlDescription.Properties.DataSource = _dbMan.ResultsDataTable;
            ddlDescription.Properties.DisplayMember = "Description";
            ddlDescription.Properties.ValueMember = "Description";
          
            //if (dtDescription.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtDescription.Rows)
            //        ddlDescription.Properties.Items.Add(r["DescrCode"].ToString() + ":" + r["Description"].ToString());
            //}
            //ddlDescription.SelectedText = "";
            ddlDescription.ItemIndex = -1;
            //Load Description Number
            //_dbMan.SqlStatement = " select * FROM CODE_WPDescriptionNo WHERE Selected = 'Y' ORDER BY DescrNumberCode";
            //_dbMan.SqlStatement = " select d.DescrNumberCode+':'+Description DescNo, d.* from CODE_WPTypeWPDescNoLink wp \r\n " +
                  _dbMan.SqlStatement = " select d.DescrNumberCode+':'+Description DescNo  from CODE_WPTypeWPDescNoLink wp \r\n " +
                        " inner join CODE_WPDescriptionNo d on \r\n " +
                        "   d.DescrNumberCode = wp.DescrNumberCode and \r\n " +
                        "   d.Selected = 'Y' \r\n " +
                        " where wp.TypeCode = '" + theWPType + "' \r\n " +
                        " ORDER BY d.DescrNumberCode ";
            _dbMan.ExecuteInstruction();
            DataTable dtDescriptionNumber = _dbMan.ResultsDataTable;
            ddlDescriptionNumber.Properties.DataSource = null;
            ddlDescriptionNumber.Properties.DataSource = _dbMan.ResultsDataTable;
            ddlDescriptionNumber.Properties.DisplayMember = "DescNo";
            ddlDescriptionNumber.Properties.ValueMember = "DescNo";
        
            //if (dtDescriptionNumber.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtDescriptionNumber.Rows)
            //        ddlDescriptionNumber.Properties.Items.Add(r["DescrNumberCode"].ToString() + ":" + r["Description"].ToString());
            //}
            //ddlDescriptionNumber.SelectedText = "";
            ddlDescriptionNumber.ItemIndex = -1;
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

        private void frmWorkplaceEdit_Load(object sender, EventArgs e)
        {
           // LoadWorkplace(edit);
        }

        private void ddlDivision_EditValueChanged(object sender, EventArgs e)
        {
            LoadDivision();
        }

        private void LoadDivision()
        {
            if (!string.IsNullOrEmpty(this.ddlDivision.Text.Trim()))
            {
                theDiv = ExtractBeforeColon(ddlDivision.EditValue .ToString ());
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess()

                {
                    queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement,
                    queryReturnType = MWDataManager.ReturnType.DataTable
                };
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                string strDivision = this.ddlDivision.Text.Trim().Substring(0, 2);

                _dbMan.SqlStatement = String.Format("SELECT Density FROM CODE_WPDivision WHERE DivisionCode = '{0}';", strDivision);

                _dbMan.ExecuteInstruction();
                if (_dbMan.ResultsDataTable.Rows.Count > 0)
                {
                    this.txtDensity.Text = _dbMan.ResultsDataTable.Rows[0][0].ToString();
                }

                //Load Workplaceid
                if (EditID == "")
                {
                    _dbMan.SqlStatement = "SELECT * FROM Code_WPDivision WHERE DivisionCode = '" + strDivision + "' ";
                    _dbMan.ExecuteInstruction();

                    DataTable theData = _dbMan.ResultsDataTable;

                    if (theData.Rows.Count != 0)
                    {
                       // string fmt = "00000000";
                        string fmt = "000000";
                        int TheWP = Convert.ToInt32(theData.Rows[0]["WPLastUsed"].ToString()) + 1;
                        string WPID = theData.Rows[0]["DivisionCode"].ToString() + TheWP.ToString(fmt);
                        WorkplaceIDTxt.Text = WPID.ToString();
                    }
                }

                //Load Common Areas
              //  this.cbeCommonArea.Properties.Items.Clear();
                //if (SysSettings.IsCentralized.ToString() == "0")
                //{
                    _dbMan.SqlStatement = "SELECT cast(Id as varchar(20))+':'+ Name Areas FROM CommonAreas ORDER BY Name";
                    _dbMan.ExecuteInstruction();
                //}
                //else
                //{
                //    _dbMan.SqlStatement = " select c.* from CommonAreas c \r\n " +
                //        " left outer join code_wpdivision wp on \r\n " +
                //        "     c.Mine = wp.Mine \r\n " +
                //        "where wp.DivisionCode = '" + strDivision + "' \r\n " +
                //        " order by DivisionCode ";
                //    _dbMan.ExecuteInstruction();
                //}
                DataTable dtCommonAreas = _dbMan.ResultsDataTable;
                cbeCommonArea.Properties.DataSource = dtCommonAreas;
                cbeCommonArea.Properties.DisplayMember = "Areas";
                cbeCommonArea.Properties.ValueMember = "Areas";
                //if (dtCommonAreas.Rows.Count > 0)
                //{
                //    foreach (DataRow r in _dbMan.ResultsDataTable.Rows)
                //        this.cbeCommonArea.Properties.Items.Add(r["Id"].ToString() + ":" + r["Name"].ToString());
                //}
            }
        }

        private void ddlWPLevel_EditValueChanged(object sender, EventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = ddlWPLevel.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
            //object val = view.GetRowCellValue(view.FocusedRowHandle, "OreFlowID");
            //if (val != null)
               // levelIDlbl.Text = val.ToString();


            WPNameGen();
        }

        private void ddlWPType_TextChanged(object sender, EventArgs e)
        {
            theWPType = ExtractBeforeColon(ddlWPType.EditValue.ToString());
            Load_WPTypeSelection();
        }

        void WPNameGen()
        {
            WpDescTxt.Text = "";
            if (ExtractBeforeColon(ddlWPLevel.Text) != "00")
                WpDescTxt.Text += ExtractBeforeColon(ddlWPLevel.Text) + " ";

            if (ExtractBeforeColon(ddlGrid.Text) != "000")
                WpDescTxt.Text += ExtractBeforeColon(ddlGrid.Text) + " ";

            if (ExtractBeforeColon(ddlDetail.Text) != "000")
                WpDescTxt.Text += ExtractBeforeColon(ddlDetail.Text) + " ";

            if (ExtractBeforeColon(ddlDirection.Text) != "000")
                WpDescTxt.Text += ExtractBeforeColon(ddlDirection.Text) + " ";

            if (ExtractBeforeColon(ddlNumber.Text) != "00")
                WpDescTxt.Text += ExtractAfterColon(ddlNumber.Text) + " ";

            if (ExtractBeforeColon(ddlDescription.Text) != "000")
                WpDescTxt.Text += ExtractBeforeColon(ddlDescription.Text) + " ";

            if (ExtractBeforeColon(ddlDescriptionNumber.Text) != "00")
                WpDescTxt.Text += ExtractAfterColon(ddlDescriptionNumber.Text) + " ";

            if (workplacetype == "SU" || workplacetype == "OUG")
                WpDescTxt.Text += ExtractAfterColon(ddlWPType.Text);
            else
                WpDescTxt.Text += txtReefDescription.Text;

            WpDescTxt.Text = WpDescTxt.Text.Trim();
        }

        private void EndHeightTxt_EditValueChanged(object sender, EventArgs e)
        {

        }

        public  void btnUpdate_Click(object sender, EventArgs e)
        {
            string cboxInActiveReason1 = "";
            if (edit == "A")
            {
                if (WorkplaceIDTxt.Text == "")
                {
                    MessageBox.Show("Please enter a valid Workplace ID", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //WorkplaceIDTxt.Focus();
                    return;
                }

                String divisionID = "";
                if (ddlDivision.EditValue.ToString() == "" || ddlDivision.EditValue == null)
                {
                    MessageBox.Show("Please choose a division", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //ddlDivision.Focus();
                    return;
                }


                String wptypeID = "";
                if (ddlWPType.EditValue.ToString() == "" || ddlWPType.EditValue == null)
                {
                    MessageBox.Show("Please choose a workplace type", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //ddlWPType.Focus();
                    return;
                }


                String levelID = "";
                if (ddlWPLevel.EditValue.ToString() == "" || ddlWPLevel.EditValue == null)
                {
                    MessageBox.Show("Please choose a level", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //ddlWPLevel.Focus();
                    return;
                }


                String gridID = "";
                if (ddlGrid.EditValue.ToString() == "" || ddlGrid.EditValue == null)
                {
                    MessageBox.Show("Please choose a grid", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //ddlGrid.Focus();
                    return;
                }

                String detailID = "";
                if (ddlDetail.EditValue.ToString() == "" || ddlDetail.EditValue == null)
                {
                    MessageBox.Show("Please choose a detail record", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //ddlDetail.Focus();
                    return;
                }

                String directionID = "";
                if (ddlDirection.EditValue.ToString() == "" || ddlDirection.EditValue == null)
                {
                    MessageBox.Show("Please choose a direction", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //ddlDirection.Focus();
                    return;
                }


                String numberID = "";
                if (ddlNumber.EditValue.ToString() == "" || ddlNumber.EditValue == null)
                {
                    MessageBox.Show("Please choose a number", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //ddlNumber.Focus();
                    return;
                }


                String descriptionID = "";
                if (ddlDescription.EditValue.ToString() == "" || ddlDescription.EditValue == null)
                {
                    MessageBox.Show("Please choose a description", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //ddlDescription.Focus();
                    return;
                }


                String descriptionNumberID = "";
                if (ddlDescriptionNumber.EditValue.ToString() == "" || ddlDescriptionNumber.EditValue == null)
                {
                    MessageBox.Show("Please choose a description number", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //ddlDescriptionNumber.Focus();
                    return;
                }


                if (cboxInActiveReason.EditValue.ToString() == "" || cboxInActiveReason == null)
                {
                    cboxInActiveReason1 = "";
                    //MessageBox.Show("Please choose a description number", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ////ddlDescriptionNumber.Focus();
                    //return;
                }
                else
                {
                    cboxInActiveReason1 = cboxInActiveReason.EditValue.ToString();
                }




                String reefID = "";
                if (workplacetype == "S" || workplacetype == "D")
                {
                    if (ddlReef.EditValue.ToString() == "" || ddlReef.EditValue == null)
                    {
                        MessageBox.Show("Please choose a reef", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //ddlReef.Focus();
                        return;
                    }
                    else if (cbeSubSections.EditValue.ToString() == "" || cbeSubSections.EditValue == null)
                    {
                        MessageBox.Show("Please select sub section.", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //this.cbeSubSections.Focus();
                        return;
                    }

                }
            }
                String Act = "";
                String endType = "";
                String endWidth = null;
                String endHeight = null;
                if (workplacetype == "D")
                {
                      if (EndTypeLU.EditValue.ToString() == "" || EndTypeLU.EditValue == null)
                    //DevExpress.XtraGrid.Views.Grid.GridView view3 =
                    //EndTypeLU.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
                    //object val3 = view3.GetRowCellValue(view3.FocusedRowHandle, "endtypeid");
                    //if (val3 != null)
                    //    EndtypeIDLb = val3.ToString();
                  //  if (EndtypeIDLb =="")
                    {
                        EndType = "";
                        MessageBox.Show("Please choose an End Type.", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //EndTypeLU.Focus();
                        return;
                    }
                    else
                    {
                        MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                        _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                        //if (TSysSettings.IsCentralizedDatabase .ToString () == "0")
                        //{

                        _dbMan.SqlStatement = "SELECT EndTypeID from EndType where Description='"+ EndTypeLU.EditValue.ToString ()+"'";
                    _dbMan.ExecuteInstruction();
                    DataTable dt = new DataTable();
                        dt = _dbMan.ResultsDataTable;
                        foreach (DataRow dr in dt.Rows)
                        {
                            EndType = dr["EndTypeID"].ToString();
                        }
                    }

                }
                else
                {
                    EndType = "";
                }
          //  }

            bool theClassify;
            if(workplacetype == "D" || workplacetype =="SU" || workplacetype == "S")
            {
                theClassify = true;
            }
            else
            {
                theClassify = false ;
            }
            _clsWorkplaces.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
             
         

            _clsWorkplaces.SaveWorkplace(WorkplaceIDTxt.Text, WpDescTxt.Text, ddlDivision.Text, ddlWPType.Text, ddlWPLevel.Text, ddlGrid.Text,
               ddlDetail.Text, ddlDirection.Text, ddlNumber.Text, ddlDescription.Text, ddlDescriptionNumber.Text, ddlReef.Text,
               cbeSubSections.Text, cbeStopingProcessCodes.Text, EndType, EndWidthTxt.Text, EndHeightTxt.Text, workplacetype, txtDensity.Text, txtBrokenRockDensity.Text,
               InActiveReason, Edit,rbReefWaste .SelectedIndex .ToString (), PriorityCbx.Checked , lblStatus.Text , "1", "", rbtHotCold.SelectedIndex.ToString(),
              DefaultAdvEdit, cbInactive.Checked, cboxInActiveReason.Text, theClassify);
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Workplace data Saved", Color.CornflowerBlue);
        }

        private void cbeCommonArea_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cbeCommonArea.Text.Trim()))
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                //if (TSysSettings.IsCentralizedDatabase .ToString () == "0")
                //{

                    _dbMan.SqlStatement = "SELECT cast(Id as varchar(20))+':'+Name SubSection,* FROM CommonAreaSubSections WHERE CommonArea = " + ExtractBeforeColon(this.cbeCommonArea.EditValue .ToString ());

                //}
                //else
                //{
                //    string strDivision = this.ddlDivision.Text.Trim().Substring(0, 2);

                //    _dbMan.SqlStatement = " select cast(s.Id as varchar(20))+':'+s.Name SubSection,s.* from CommonAreaSubSections s \r\n " +
                //            " left outer join CommonAreas a on \r\n " +
                //            "   s.CommonArea = a.id  \r\n " +
                          
                //            " left outer join CODE_WPDIVISION d on \r\n " +
                //            "   d.Mine = s.Mine \r\n " +
                //            " where d.DivisionCode = '" + strDivision + "' ";
                //}

                //Load Sub Sections
                _dbMan.ExecuteInstruction();
                DataTable dtSubSectiobs = _dbMan.ResultsDataTable;

               
                cbeSubSections.Properties.DataSource = dtSubSectiobs;
                cbeSubSections.Properties.DisplayMember = "SubSection";
                cbeSubSections.Properties.ValueMember = "SubSection";
                //if (dtSubSectiobs.Rows.Count > 0)
                //{
                //    foreach (DataRow r in _dbMan.ResultsDataTable.Rows)
                //        this.cbeSubSections.Properties.Items.Add(r["Id"].ToString() + ":" + r["Name"].ToString());
                //}
            }
        }

        private void EndTypeLU_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view =
       EndTypeLU.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
            object val = view.GetRowCellValue(view.FocusedRowHandle, "Width");

            if (val != null)
                EndWidthTxt.Text = val.ToString();

            DevExpress.XtraGrid.Views.Grid.GridView view1 =
                 EndTypeLU.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
            object val1 = view1.GetRowCellValue(view1.FocusedRowHandle, "Height");
            if (val1 != null)
                EndHeightTxt.Text = val1.ToString();

            string EndtypeIDLb = "";
            DevExpress.XtraGrid.Views.Grid.GridView view3 =
                    EndTypeLU.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
            object val3 = view3.GetRowCellValue(view3.FocusedRowHandle, "endtypeid");
            if (val3 != null)
                EndtypeIDLb = val3.ToString();

            DevExpress.XtraGrid.Views.Grid.GridView view2 =
                 EndTypeLU.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
            object val2 = view2.GetRowCellValue(view2.FocusedRowHandle, "R/W");

            // WPReefWasteRadio.SelectedIndex = 1;
            rbReefWaste.SelectedIndex = 1;
            //rbReef.Checked = false;
            //rbWaste.Checked = true;

            if (val2 != null)
                if (val2.ToString() == "R")
                {
                    rbReefWaste.SelectedIndex = 0;
                    //rbReef.Checked = true;
                    //rbWaste.Checked = false;
                    //WPReefWasteRadio.SelectedIndex = 0;
                }



            WPNameGen();
        }

        private void cbInactive_Click(object sender, EventArgs e)
        {
            if (cbInactive.Checked == false )
                lblStatus.Text = "Status : Inactive";
            else
                lblStatus.Text = "Status : Active";
            cboxInActiveReason.Enabled = true;
            InActiveReason = true;
        }

        private void rbtHotCold_Click(object sender, EventArgs e)
        {
            if (rbtHotCold.SelectedIndex == 0)
            {
                lblStatus.Text = "Status : Active";
                theClassify = true;
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
