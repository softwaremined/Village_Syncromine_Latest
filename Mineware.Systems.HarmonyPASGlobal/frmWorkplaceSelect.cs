using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;


#region Comments
// Created By : Dolf van den berg
// Creation Date : 2011/01/24
// Description : List all the workplaces for preplanning. This screen returns a StringList with all the workplace descriptions selected

#endregion Comments 

namespace Mineware.Systems.ProductionGlobal
{
    public partial class frmWorkplaceSelect : DevExpress.XtraEditors.XtraForm
    {
        DataTable theWorkplaceList = new DataTable();
        private bool formStatus = false;
        string theWPName;
        string theActivity;
        string theProdMonth;
        public  string theSystemDBTag = "";
        public  TUserCurrentInfo UserCurrentInfo;

        public frmWorkplaceSelect()
        {
            InitializeComponent();
        }

        private DataTable LoadWorkPlaces(string Activity, string ProdMonth, ArrayList currentWorkPlaces)
        {
            //CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            ////BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;

            //BMEBL.SetsystemDBTag = this.theSystemDBTag;
            //BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            MWDataManager.clsDataAccess _WorkPlaceList = new MWDataManager.clsDataAccess();
            _WorkPlaceList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WorkPlaceList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WorkPlaceList.queryReturnType = MWDataManager.ReturnType.DataTable;
            if (Activity == "0" || Activity == "1")
            {
                _WorkPlaceList.SqlStatement = "Select CAST(0 AS BIT) Selected,w.Workplaceid, w.DESCRIPTION from Workplace w " +
                    // "Where (w.type in ('" + Activity + "')) " +
                            "Where (w.Activity in ('" + Activity + "')) " +
                            "AND ((w.Inactive <> 'Y') OR (w.Inactive IS NULL)) " +
                            "AND w.Workplaceid not in (Select pre.Workplaceid from planmonth pre where Prodmonth = '" + ProdMonth + "') " +
                    //        "UNION " +
                    //// "SELECT CAST(0 AS BIT) Selected,NULL Workplaceid, DESCRIPTION FROM dbo.PrePlanningWorkplace pp " +
                    //       "SELECT CAST(0 AS BIT) Selected,NULL Workplaceid, DESCRIPTION FROM dbo.WORKPLACE_PREPLANNING pp " +
                    //// "Where (pp.Type in ('" + Activity + "')) " +
                    //       "Where (pp.Activity in ('" + Activity + "')) " +
                    //        "and pp.DESCRIPTION not in (Select pre.Workplaceid from planmonth pre where Prodmonth = '" + ProdMonth + "') " +
                            "order by w.DESCRIPTION ";
                _WorkPlaceList.ExecuteInstruction();
            }
            else if(theActivity == "2")
            {
                //_WorkPlaceList.SqlStatement = "select * from ( " +
                //                                "Select  CAST(0 AS BIT) Selected,w.Workplaceid, w.DESCRIPTION from " +
                //                                 "Workplace w Where " +
                //                                 "((w.DELETED <> 'Y') OR (w.Deleted IS NULL)) AND " +
                //                                 " w.Workplaceid not in (Select pre.Workplaceid from PLANMONTH_SUNDRYMINING  pre where Prodmonth = '" + ProdMonth + "' and WorkplaceID is not null) " +

                //                                 " UNION SELECT CAST(0 AS BIT) Selected,NULL Workplaceid, DESCRIPTION FROM dbo.WORKPLACE_PREPLANNING pp " +
                //                                 " Where  pp.DESCRIPTION not in (Select pre.Workplaceid from PLANMONTH_SUNDRYMINING pre " +
                //                                 " where Prodmonth = '" + ProdMonth + "' ) )data where WorkplaceID is not null order by Description ";

                _WorkPlaceList.SqlStatement = "SELECT CAST(0 AS BIT) Selected,w.Workplaceid, w.DESCRIPTION from Workplace w Where (w.Inactive <> 'Y') OR (w.Inactive IS NULL)";
                _WorkPlaceList.ExecuteInstruction();
            }
            else if (theActivity == "8")
            {
                //_WorkPlaceList.SqlStatement = "select * from ( " +
                //                                "Select  CAST(0 AS BIT) Selected,w.Workplaceid, w.DESCRIPTION from " +
                //                                 "Workplace w Where " +
                //                                 "((w.DELETED <> 'Y') OR (w.Deleted IS NULL)) AND " +
                //                                 " w.Workplaceid not in (Select pre.Workplaceid from PLANMONTH_OLDGOLD  pre where Prodmonth = '" + ProdMonth + "' and WorkplaceID is not null) " +

                //                                 " UNION SELECT CAST(0 AS BIT) Selected,NULL Workplaceid, DESCRIPTION FROM dbo.WORKPLACE_PREPLANNING pp " +
                //                                 " Where  pp.DESCRIPTION not in (Select pre.Workplaceid from PLANMONTH_OLDGOLD pre " +
                //                                 " where Prodmonth = '" + ProdMonth + "' ) )data where WorkplaceID is not null order by Description ";
                _WorkPlaceList.SqlStatement = "SELECT CAST(0 AS BIT) Selected,w.Workplaceid, w.DESCRIPTION from Workplace w Where (w.Inactive <> 'Y') OR (w.Inactive IS NULL)";
                _WorkPlaceList.ExecuteInstruction();
            }
            for (int i = 0; i < currentWorkPlaces.Count; i++)
            {
                DataRow[] customerRow = _WorkPlaceList.ResultsDataTable.Select("DESCRIPTION = '" + currentWorkPlaces[i].ToString() + "'");
                if (customerRow.Length != 0)
                {
                    customerRow[0]["Selected"] = 1;
                }
            }
            return _WorkPlaceList.ResultsDataTable;
            

        }



        private DataTable LoadWorkPlacesOCR(  ArrayList currentWorkPlaces)
        {
            //CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            ////BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;

            //BMEBL.SetsystemDBTag = this.theSystemDBTag;
            //BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;
            MWDataManager.clsDataAccess _WorkPlaceList = new MWDataManager.clsDataAccess();
            _WorkPlaceList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WorkPlaceList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WorkPlaceList.queryReturnType = MWDataManager.ReturnType.DataTable;
        
                _WorkPlaceList.SqlStatement = "Select CAST(0 AS BIT) Selected,w.Workplaceid, w.DESCRIPTION from Workplace w " +
                            // "Where (w.type in ('" + Activity + "')) " +
                            "Where  " +
                            " ((w.Inactive <> 'Y') OR (w.Inactive IS NULL)) " +
                            "--AND w.Workplaceid not in (Select pre.Workplaceid from planmonth pre where Prodmonth = '201806') " +
                            //        "UNION " +
                            //// "SELECT CAST(0 AS BIT) Selected,NULL Workplaceid, DESCRIPTION FROM dbo.PrePlanningWorkplace pp " +
                            //       "SELECT CAST(0 AS BIT) Selected,NULL Workplaceid, DESCRIPTION FROM dbo.WORKPLACE_PREPLANNING pp " +
                            //// "Where (pp.Type in ('" + Activity + "')) " +
                            //       "Where (pp.Activity in ('" + Activity + "')) " +
                            //        "and pp.DESCRIPTION not in (Select pre.Workplaceid from planmonth pre where Prodmonth = '" + ProdMonth + "') " +
                            "order by w.DESCRIPTION ";
                _WorkPlaceList.ExecuteInstruction();
         
        
           
            for (int i = 0; i < currentWorkPlaces.Count; i++)
            {
                DataRow[] customerRow = _WorkPlaceList.ResultsDataTable.Select("DESCRIPTION = '" + currentWorkPlaces[i].ToString() + "'");
                if (customerRow.Length != 0)
                {
                    customerRow[0]["Selected"] = 1;
                }
            }
            return _WorkPlaceList.ResultsDataTable;


        }



        public ArrayList GetWorkPlaceList(string Activity, string ProdMonth, ArrayList currentWorkPlaceList)
        {
            
            theProdMonth = ProdMonth;
            ArrayList SelectedWorkplaces = new ArrayList();
            theActivity = "";

            switch (Activity)
            {
                case "0":
                    theActivity = "0";
                    break;
                case "1":
                    theActivity = "1";
                    break;

                case "2":
                    theActivity = "2";
                    break;

                case "8":
                    theActivity = "8";
                    break;

            }

            theWorkplaceList = LoadWorkPlaces(theActivity, ProdMonth, currentWorkPlaceList);
            gcWorkPlaces.DataSource = theWorkplaceList;

            this.ShowDialog();

            if (formStatus == false)
            {
                SelectedWorkplaces.Clear();
            }
            else
            {
                SelectedWorkplaces.Clear();
                DataRow[] newData = theWorkplaceList.Select("(Selected = 'True')",
                                        "Selected", DataViewRowState.CurrentRows);
                for (int i = 0; i < newData.Length; i++)
                {
                    SelectedWorkplaces.Add(newData[i]["DESCRIPTION"].ToString());
                }

            }


            return SelectedWorkplaces;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            formStatus = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            formStatus = false;
            Close();
        }

        private void viewWorkplaces_RowCountChanged(object sender, EventArgs e)
        {
            GridView view;
            view = sender as GridView;
            MWDataManager.clsDataAccess _WorkPlacePresint = new MWDataManager.clsDataAccess();
            string wpName = "";
            if (view.EditingValue != null)
            { wpName = view.EditingValue.ToString(); }
            else { wpName = ""; }
            _WorkPlacePresint.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WorkPlacePresint.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WorkPlacePresint.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WorkPlacePresint.SqlStatement = "SELECT [DESCRIPTION] FROM dbo.WORKPLACE WHERE DESCRIPTION = '" + wpName + "' or WORKPLACEID = '" + wpName + "' " +
                                             "UNION " +
                                             //"SELECT [DESCRIPTION] FROM dbo.PrePlanningWorkplace WHERE DESCRIPTION = '" + wpName + "'";
                                             "SELECT [DESCRIPTION] FROM dbo.WORKPLACE WHERE DESCRIPTION = '" + wpName + "'";
            _WorkPlacePresint.ExecuteInstruction();
            if (view.RowCount == 0 && _WorkPlacePresint.ResultsDataTable.Rows.Count == 0)
            {
               // btnAddToPrePlanningWorkplace.Visible = true; 
                btnOK.Enabled = false;

            }
            else { 
               // btnAddToPrePlanningWorkplace.Visible = false; 
                btnOK.Enabled = true; }
        }

        private void btnAddToPrePlanningWorkplace_Click(object sender, EventArgs e)
        {
            // MWDataManager.clsDataAccess _AddPrePlanningWP = new MWDataManager.clsDataAccess();
            //_AddPrePlanningWP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_AddPrePlanningWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_AddPrePlanningWP.queryReturnType = MWDataManager.ReturnType.longNumber;

            //_AddPrePlanningWP.SqlStatement = "INSERT INTO [dbo].[WORKPLACE_PREPLANNING] " +
            //                                 "           ([DESCRIPTION] " +
            //                                 "           ,[Activity]) " +
            //                                 "    VALUES " +
            //                                 "          ('" + theWPName.ToUpper() + "' " +
            //                                 "          ,'" + theActivity + "')";
            //_AddPrePlanningWP.ExecuteInstruction();
            ////frmAddPrePlanningWorkplace frmAddPrePlanningWorkplace = new frmAddPrePlanningWorkplace();
            //bool wpAdded = true; //frmAddPrePlanningWorkplace.newPrePlanningWorkplace(theWPName);
            //if (wpAdded == true)
            //{
            //    ArrayList SelectedWorkplaces = new ArrayList();
            //    DataTable tempData =  (DataTable)gcWorkPlaces.DataSource;
            //    foreach (DataRow dr in tempData.Rows)
            //    {
            //        if (Convert.ToBoolean(dr["Selected"]) == true)
            //        {
            //            SelectedWorkplaces.Add(dr["DESCRIPTION"].ToString());
            //        }
            //    }
            //    theWorkplaceList = LoadWorkPlaces(theActivity, theProdMonth, SelectedWorkplaces);
            //    gcWorkPlaces.DataSource = theWorkplaceList;
            //}
        }

        private void gcWorkPlaces_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void viewWorkplaces_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void viewWorkplaces_ColumnFilterChanged(object sender, EventArgs e)
        {
            GridView view;
            view = sender as GridView;
            if (view.EditingValue != null)
            {
                theWPName = view.EditingValue.ToString();
            }
        }

        private void frmWorkplaceSelect_Load(object sender, EventArgs e)
        {

        }
    }
}