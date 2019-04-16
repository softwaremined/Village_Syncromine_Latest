using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraTreeList.Nodes;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning.HR;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.HR
{
    public partial class frmHRStandardsAndNorms : ucBaseUserControl 
    {
        private int theTargetID;
        private int theActivity;
        private DataTable theMainData;
        private DataTable selectedDesignationsData = new DataTable();
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow groupA = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();

        public frmHRStandardsAndNorms()
        {
            InitializeComponent();

           
        }

        private static string FullNameByNode(TreeListNode node, int columnId)
        {
            string ret = node.GetValue(columnId).ToString();
            while (node.ParentNode != null)
            {
                node = node.ParentNode;
                ret = ret;
            }
            return ret;
        }

        #region Build Dev section
        private void buildDev()
        {
            DevExpress.XtraVerticalGrid.Rows.CategoryRow group1 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            DevExpress.XtraVerticalGrid.Rows.EditorRow theOption = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            DevExpress.XtraVerticalGrid.Rows.EditorRow theDevType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            DevExpress.XtraVerticalGrid.Rows.EditorRow theNightShift = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            DevExpress.XtraVerticalGrid.Rows.EditorRow theNumEnds = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            DevExpress.XtraVerticalGrid.Rows.EditorRow theRiggType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            DevExpress.XtraVerticalGrid.Rows.EditorRow tippingDist = new DevExpress.XtraVerticalGrid.Rows.EditorRow();

            vgcOptions.Rows.Clear();
            groupA.ChildRows.Clear();

            group1.Properties.Caption = "Working place characteristic";



            theOption.Properties.Caption = "Option";
            theOption.Properties.FieldName = "theOption";
            theOption.OptionsRow.AllowFocus = false;

            theDevType.Properties.Caption = "Development type";
            theDevType.Properties.FieldName = "theType";
            theDevType.OptionsRow.AllowFocus = false;

            theNightShift.Properties.Caption = "Night shift?";
            theNightShift.Properties.FieldName = "NightShift";
            theNightShift.Properties.RowEdit = riNightShift;


            theNumEnds.Properties.Caption = "Number of ends";
            theNumEnds.Properties.FieldName = "NumberOfEnds";

            theRiggType.Properties.Caption = "Rigg use / type";
            theRiggType.Properties.FieldName = "RiggTypeID";
            theRiggType.Properties.RowEdit = riDrillRigTypes;

            MWDataManager.clsDataAccess _theMiningTypes = new MWDataManager.clsDataAccess();
            _theMiningTypes.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _theMiningTypes.SqlStatement = "SELECT [DrillRigID],[DrillRigDesc]  FROM [DrillRigTypes]";
            _theMiningTypes.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _theMiningTypes.queryReturnType = MWDataManager.ReturnType.DataTable;
            _theMiningTypes.ExecuteInstruction();

            riDrillRigTypes.DataSource = _theMiningTypes.ResultsDataTable;
            riDrillRigTypes.ValueMember = "DrillRigID";
            riDrillRigTypes.DisplayMember = "DrillRigDesc";

            tippingDist.Properties.Caption = "Tipping distance (fill in to the nearest 10m)";
            tippingDist.Properties.FieldName = "TippingDistance";


            // group1.Appearance.ParentAppearance.Font.Bold .Font .Bold = true;
            vgcOptions.Rows.Add(group1);
            //   vgcOptions.Rows.Add(theOption);
            group1.ChildRows.Add(theOption);
            group1.ChildRows.Add(theDevType);
            group1.ChildRows.Add(theNightShift);
            group1.ChildRows.Add(theNumEnds);
            group1.ChildRows.Add(theRiggType);
            group1.ChildRows.Add(tippingDist);

            groupA.Properties.Caption = "Designation (crew composition)";
            vgcOptions.Rows.Add(groupA);


        }

        #endregion 

        #region Build the stoping section
        private void buildStoping()
        {
            DevExpress.XtraVerticalGrid.Rows.CategoryRow group1 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            DevExpress.XtraVerticalGrid.Rows.EditorRow theOption = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            DevExpress.XtraVerticalGrid.Rows.EditorRow theStopeType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            DevExpress.XtraVerticalGrid.Rows.EditorRow theNightShift = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            DevExpress.XtraVerticalGrid.Rows.CategoryRow panelLengthGroup = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            DevExpress.XtraVerticalGrid.Rows.MultiEditorRow thePanLength = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRow();
            DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties thePanLengthMax = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties();
            DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties thePanLengthMin = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties();
            DevExpress.XtraVerticalGrid.Rows.CategoryRow stopeWidthGroup = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            DevExpress.XtraVerticalGrid.Rows.MultiEditorRow theStopeWidth = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRow();
            DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties theStopeWidthMax = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties();
            DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties theStopeWidthMin = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties();
            DevExpress.XtraVerticalGrid.Rows.EditorRow theNumGully = new DevExpress.XtraVerticalGrid.Rows.EditorRow();

            vgcOptions.Rows.Clear();
            groupA.ChildRows.Clear();
        
            group1.Properties.Caption = "Working place characteristic";


            
            theOption.Properties.Caption = "Option";
            theOption.Properties.FieldName = "theOption";
            theOption.OptionsRow.AllowFocus = false;

            theStopeType.Properties.Caption = "Stoping type";
            theStopeType.Properties.FieldName = "theType";
            theStopeType.OptionsRow.AllowFocus = false;

            theNightShift.Properties.Caption = "Night shift?";
            theNightShift.Properties.FieldName = "NightShift";
            theNightShift.Properties.RowEdit = riNightShift;

            panelLengthGroup.Properties.Caption = "Panel length (m)";

            thePanLengthMin.Caption = "Min";
            thePanLengthMin.FieldName = "PanelLengthMin";
            thePanLengthMax.Caption = "Max";
            thePanLengthMax.FieldName = "PanelLengthMax";
            thePanLength.PropertiesCollection.Add(thePanLengthMin);
            thePanLength.PropertiesCollection.Add(thePanLengthMax);

            stopeWidthGroup.Properties.Caption = "Stoping width";

            theStopeWidthMin.Caption = "Min";
            theStopeWidthMax.Caption = "Max";
            theStopeWidthMax.FieldName = "SWMax";
            theStopeWidthMin.FieldName = "SWMin";
            theStopeWidth.PropertiesCollection.Add(theStopeWidthMin);
            theStopeWidth.PropertiesCollection.Add(theStopeWidthMax);

            theNumGully.Properties.Caption = "Number of winches in operation (excl. centre gulley)";
            theNumGully.Properties.FieldName = "NumWinch";


            // group1.Appearance.ParentAppearance.Font.Bold .Font .Bold = true;
            vgcOptions.Rows.Add(group1);
         //   vgcOptions.Rows.Add(theOption);
            group1.ChildRows.Add(theOption);
            group1.ChildRows.Add(theStopeType);
            group1.ChildRows.Add(theNightShift);
            group1.ChildRows.Add(panelLengthGroup);
            group1.ChildRows.Add(stopeWidthGroup);
            panelLengthGroup.ChildRows.Add(thePanLength);
            stopeWidthGroup.ChildRows.Add(theStopeWidth);
            group1.ChildRows.Add(theNumGully);

            groupA.Properties.Caption = "Designation (crew composition)";
            vgcOptions.Rows.Add(groupA);
            

        }
        #endregion

        #region load data from DB using spHRStdAndNormGetData
        private void LoadData(int TargetID)
        {
            MWDataManager.clsDataAccess _theData = new MWDataManager.clsDataAccess();
            _theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _theData.SqlStatement = "sp_HRStdNorm_GetData";
            _theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _theData.queryReturnType = MWDataManager.ReturnType.DataTable;




            SqlParameter[] _paramCollection = 
             {                                   
                 _theData.CreateParameter("@TargetID", SqlDbType.Int, 0,TargetID),
             };


            _theData.ParamCollection = _paramCollection;

            _theData.ExecuteInstruction();

            //theMainData = _theData.ResultsDataTable.Clone();
            theMainData = _theData.ResultsDataTable.Copy();
            vgcOptions.DataSource = theMainData;

            _theData.SqlStatement = "SELECT * FROM HRSTDNORMDESIGNATION WHERE TargetID = " + TargetID.ToString();
            _theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _theData.ExecuteInstruction();


            selectedDesignationsData = _theData.ResultsDataTable.Copy();

            foreach (DataRow r in selectedDesignationsData.Rows)
            {
                if (theMainData.Columns.Contains(r["Designation"].ToString()) == false)
                {
                    addDesignationsCol(r["Designation"].ToString());
                }

                if (theMainData.Columns.Contains(r["Designation"].ToString()) == true)
                {
                    foreach (DataRow rr in theMainData.Rows)
                    {
                        _theData.SqlStatement = "SELECT *FROM [HRSTDNORMDESIGNATIONDATA] " +
                                                "WHERE StdAndNormID = " + rr["StdAndNormID"].ToString() +" and " +
                                                "Designation = '" + r["Designation"].ToString() + "'";
                        _theData.ExecuteInstruction();

                        if (_theData.ResultsDataTable.Rows.Count == 1)
                        {
                            foreach (DataRow rrr in _theData.ResultsDataTable.Rows)
                            {
                                rr[ r["Designation"] + " Day"] = rrr["Day"];
                                rr[ r["Designation"] + " Night"] = rrr["Night"];
                            }


                        }

                    }

                }

            }



        }
        #endregion

        #region set the myning type selected and load standard and norm
        private void tlMiningTypes_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (theMainData != null)
            { saveData(); }
            string s = FullNameByNode(e.Node, 0);
            MWDataManager.clsDataAccess _theData = new MWDataManager.clsDataAccess();
            _theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _theData.SqlStatement = "SELECT TargetID,Activity FROM " + TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Bonus_Database + "dbo.Bonus_poolDEfaults WHERE Description = '" + s + "'";
            _theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _theData.ExecuteInstruction();

            if (_theData.ResultsDataTable.Rows.Count != 0)
            {
                foreach (DataRow r in _theData.ResultsDataTable.Rows)
                {
                    theTargetID = Convert.ToInt32(r["TargetID"].ToString());
                    theActivity = Convert.ToInt32(r["Activity"].ToString());
                }

                switch (theActivity)
                {
                    case 0: 
                        buildStoping();
                        break;
                    case 3:
                        buildStoping();
                        break;
                    case 1:
                        buildDev();
                        break;
                
                }
                LoadData(theTargetID);
            }
            else 
            {
                vgcOptions.Rows.Clear();
                groupA.ChildRows.Clear();
            }
            
           
        }
        #endregion

        #region add different designations to grid
        private void addDesignationsCol(string theDesignation)
        {
            DataColumn newCol = new DataColumn();
            newCol.ColumnName = theDesignation;
            DataColumn newColDay = new DataColumn();
            newColDay.ColumnName = theDesignation + " Day";
            newColDay.DataType = typeof(Int32);
            DataColumn newColNight = new DataColumn();
            newColNight.ColumnName = theDesignation + " Night";
            newColNight.DataType = typeof(Int32);

            theMainData.Columns.Add(newCol);
            theMainData.Columns.Add(newColDay);
            theMainData.Columns.Add(newColNight);

            DevExpress.XtraVerticalGrid.Rows.CategoryRow group2 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            DevExpress.XtraVerticalGrid.Rows.MultiEditorRow theDesig = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRow();
            DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties theDesigDay = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties();
            DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties theDesigNight = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties();


            theDesigDay.Caption = "Day Crew";
            theDesigDay.FieldName = theDesignation + " Day";
            theDesigNight.Caption = "Night Crew";
            theDesigNight.FieldName = theDesignation + " Night";
            theDesig.PropertiesCollection.Add(theDesigDay);
            theDesig.PropertiesCollection.Add(theDesigNight);


            group2.Properties.Caption = theDesignation;
            groupA.ChildRows.Add(group2);
            group2.ChildRows.Add(theDesig);
         //   vgcOptions.Rows.Add(group2);
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {


           // using (frmHRDesignations frmHRDesignations = new frmHRDesignations { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo })
            frmHRDesignations frmHRDesignations = new frmHRDesignations { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
           // {
                selectedDesignationsData = frmHRDesignations.loadDesignations(theTargetID);
                MWDataManager.clsDataAccess _theData = new MWDataManager.clsDataAccess();
                _theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _theData.SqlStatement = "DELETE FROM HRSTDNORMDESIGNATION WHERE TargetID = " + theTargetID.ToString();
                _theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _theData.ExecuteInstruction();
                foreach (DataRow r in selectedDesignationsData.Rows)
                {
                    if (theMainData.Columns.Contains(r["Designation"].ToString()) == false)
                    {
                        addDesignationsCol(r["Designation"].ToString());
                    }
                    _theData.SqlStatement = "INSERT INTO HRSTDNORMDESIGNATION VALUES(" + theTargetID.ToString() + ",'" + r["Designation"].ToString() + "')";
                    _theData.ExecuteInstruction();
                }
           // }


        }

        #region save data to DB using spHRStdAndNormUpdateData
        private void saveData()
        {
            foreach (DataRow r in theMainData.Rows)
            {
                MWDataManager.clsDataAccess _theData = new MWDataManager.clsDataAccess();
                _theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _theData.SqlStatement = "sp_HRStdNorm_UpdateData";
                _theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                _theData.queryReturnType = MWDataManager.ReturnType.DataTable;

                SqlParameter[] _paramCollection = 
                {                                   
                 _theData.CreateParameter("@StdAndNormID", SqlDbType.Int, 0,r["StdAndNormID"]),
                 _theData.CreateParameter("@NightShift", SqlDbType.Bit, 0,r["NightShift"]),
                 _theData.CreateParameter("@PanelLengthMin", SqlDbType.Decimal, 0,r["PanelLengthMin"]),
                 _theData.CreateParameter("@PanelLengthMax", SqlDbType.Decimal, 0,r["PanelLengthMax"]),
                 _theData.CreateParameter("@SWMin", SqlDbType.Decimal, 0,r["SWMin"]),
                 _theData.CreateParameter("@SWMax", SqlDbType.Decimal, 0,r["SWMax"]),
                 _theData.CreateParameter("@NumWinch", SqlDbType.Int, 0,r["NumWinch"]),
                 _theData.CreateParameter("@NumberOfEnds", SqlDbType.Int, 0,r["NumberOfEnds"]),
                 _theData.CreateParameter("@RiggTypeID", SqlDbType.Int, 0,r["RiggTypeID"]),
                 _theData.CreateParameter("@TippingDistance", SqlDbType.Int, 0,r["TippingDistance"]),
                };

                _theData.ParamCollection = _paramCollection;

                _theData.ExecuteInstruction();

                foreach (DataRow rr in selectedDesignationsData.Rows)
                {
                    if (theMainData.Columns.Contains(rr["Designation"].ToString()) == true)
                    {

                        MWDataManager.clsDataAccess _saveDesignationsData = new MWDataManager.clsDataAccess();
                        _saveDesignationsData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _saveDesignationsData.SqlStatement = "sp_HRStdNorm_UpdateDesignationData";
                        _saveDesignationsData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                        _saveDesignationsData.queryReturnType = MWDataManager.ReturnType.longNumber;

                        SqlParameter[] _paramCollection2 = 
                        {                                   
                          _saveDesignationsData.CreateParameter("@StdAndNormID", SqlDbType.Int, 0,r["StdAndNormID"]),
                          _saveDesignationsData.CreateParameter("@Designation", SqlDbType.VarChar, 150,rr["Designation"]),
                          _saveDesignationsData.CreateParameter("@Day", SqlDbType.Int, 0,r[rr["Designation"] + " Day"]),
                          _saveDesignationsData.CreateParameter("@Night", SqlDbType.Int, 0,r[rr["Designation"] + " Night"]),
                        };

                        _saveDesignationsData.ParamCollection = _paramCollection2;

                        _saveDesignationsData.ExecuteInstruction();


                    }
                }

            }
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {

            saveData();   

        }

        private void btnNewOption_Click(object sender, EventArgs e)
        {

            MWDataManager.clsDataAccess _theData = new MWDataManager.clsDataAccess();
            _theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _theData.SqlStatement = "sp_HRStdNorm_AddOption";
            _theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _theData.queryReturnType = MWDataManager.ReturnType.DataTable;

            SqlParameter[] _paramCollection = 
            {                                  
               _theData.CreateParameter("@TargetID", SqlDbType.Int, 0,theTargetID),
               _theData.CreateParameter("@Activity", SqlDbType.VarChar, 150,theActivity),
            };

            _theData.ParamCollection = _paramCollection;

            _theData.ExecuteInstruction();


            saveData();
            switch (theActivity)
            {
                case 0:
                    buildStoping();
                    break;
                case 1:
                    buildDev();
                    break;

            }
            LoadData(theTargetID);   

        }

        private void frmHRStandardsAndNorms_Load(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _HRStandards = new MWDataManager.clsDataAccess();
            _HRStandards.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _HRStandards.SqlStatement = "sp_HR_MiningStandards";
            _HRStandards.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _HRStandards.queryReturnType = MWDataManager.ReturnType.DataTable;
            _HRStandards.ExecuteInstruction();
            //    _HRStandards.ResultsTableName = "ProblemListDrill";

            tlMiningTypes.DataSource = _HRStandards.ResultsDataTable;
        }

        private void btnDeleteOption_Click(object sender, EventArgs e)
        {

        }

    }
}