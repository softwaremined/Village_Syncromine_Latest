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
using Mineware.Systems.ProductionGlobal.Helpers;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionGlobal
{
    public partial class frmRemoveWorkplace : Form
    {
        DataTable theWorkplaceList = new DataTable();
        private bool formStatus = false;
        string theWPName;
        string theActivity;
        string theProdMonth;
        private GridRadioGroupColumnHelper _Helper;
        DataTable dt = new DataTable();
        string desc;
        public string theSystemDBTag = "";
        public  TUserCurrentInfo UserCurrentInfo;
        
        public frmRemoveWorkplace()
        {            
            InitializeComponent();
            this.gcolWPID.VisibleIndex = 1;
            this.gcolDESCRIPTION.VisibleIndex = 2;
              _Helper = new GridRadioGroupColumnHelper(viewWorkplaces );
            //_Helper.SelectedRowChanged += new EventHandler(_Helper_SelectedRowChanged);

        }

        void _Helper_SelectedRowChanged(object sender, EventArgs e)
        {
           // Text = _Helper.SelectedDataSourceRowIndex.ToString();
        }
       // }

        private DataTable LoadWorkPlaces(string Activity, string ProdMonth, ArrayList currentWorkPlaces)
        {
            int activity1;
            if (Activity == "S")
            {
                activity1 = 0;
            }
            else if (Activity == "2")
            {
                activity1 = 2;
            }
            else
            {
                activity1 =1;
            }

            MWDataManager.clsDataAccess _WorkPlaceList = new MWDataManager.clsDataAccess();
            _WorkPlaceList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WorkPlaceList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WorkPlaceList.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_WorkPlaceList.SqlStatement = "Select CAST(0 AS BIT) Selected,w.Workplaceid, w.DESCRIPTION from Workplce w " +
            //            "Where (w.type in ('" + Activity + "')) " +
            //            "AND ((w.DELETED <> 'Y') OR (w.Deleted IS NULL)) " +
            //            "AND w.Workplaceid not in (Select pre.Workplaceid from PrePlanning pre where Prodmonth = '" + ProdMonth + "') " +
            //            "UNION " +
            //            "SELECT CAST(0 AS BIT) Selected,NULL Workplaceid, DESCRIPTION FROM dbo.PrePlanningWorkplace pp " +
            //            "Where (pp.Type in ('" + Activity + "')) " +
            //            "and pp.DESCRIPTION not in (Select pre.Workplaceid from PrePlanning pre where Prodmonth = '" + ProdMonth + "') " +
            //            "order by w.DESCRIPTION ";
            if (activity1 == 0 || activity1 == 1)
            {
                _WorkPlaceList.SqlStatement = "Select w.Workplaceid, w.DESCRIPTION from Workplace w " +
                            "Where (w.Activity in ('" + activity1 + "')) " +
                            "AND ((w.DELETED <> 'Y') OR (w.Deleted IS NULL)) " +
                            "AND w.Workplaceid not in (Select pre.Workplaceid from planmonth pre where Prodmonth = '" + ProdMonth + "') ";
                            //"UNION " +
                    //"SELECT NULL Workplaceid, DESCRIPTION FROM dbo.PreplanningWorkplace pp " +
                            //"SELECT NULL Workplaceid, DESCRIPTION FROM dbo.Workplace pp " +
                            //"Where (pp.Activity in ('" + activity1 + "')) " +
                            //"and pp.DESCRIPTION not in (Select pre.Workplaceid from planmonth pre where Prodmonth = '" + ProdMonth + "') " +
                            //"order by w.DESCRIPTION ";
                _WorkPlaceList.ExecuteInstruction();
            }
            else if (activity1 == 2)
            {
                _WorkPlaceList.SqlStatement = "select * from ( " +
                                               "Select  CAST(0 AS BIT) Selected,w.Workplaceid, w.DESCRIPTION from " +
                                                "Workplace w Where " +
                                                "((w.DELETED <> 'Y') OR (w.Deleted IS NULL)) AND " +
                                                " w.Workplaceid not in (Select pre.Workplaceid from PLANMONTH_SUNDRYMINING  pre where Prodmonth = '" + ProdMonth + "' and WorkplaceID is not null) " +

                                                " UNION SELECT CAST(0 AS BIT) Selected,NULL Workplaceid, DESCRIPTION FROM dbo.workplace pp " +
                                                " Where  pp.DESCRIPTION not in (Select pre.Workplaceid from PLANMONTH_SUNDRYMINING pre " +
                                                " where Prodmonth = '" + ProdMonth + "' ) )data where WorkplaceID is not null order by Description ";
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



        public ArrayList GetWorkPlaceList(string Activity, string ProdMonth, ArrayList currentWorkPlaceList)
        {

            theProdMonth = ProdMonth;
            ArrayList SelectedWorkplaces = new ArrayList();
            theActivity = "";

            switch (Activity)
            {
                case "0":
                    theActivity = "S";
                    break;
                case "1":
                    theActivity = "D";
                    break;

                case "2":
                    theActivity = "2";
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
             desc =   this.viewWorkplaces.GetRowCellValue(viewWorkplaces.FocusedRowHandle, "DESCRIPTION").ToString();
               // DataRow[] newData = theWorkplaceList.Select("(Selected = 'True')",
                                       // "Selected", DataViewRowState.CurrentRows);
               // for (int i = 0; i < newData.Length; i++)
               // {
                 //   SelectedWorkplaces.Add(newData[i]["DESCRIPTION"].ToString());
               // }
                SelectedWorkplaces .Add (desc );

            }


            return SelectedWorkplaces;
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
                                             "SELECT [DESCRIPTION] FROM dbo.Workplace WHERE DESCRIPTION = '" + wpName + "'";
            _WorkPlacePresint.ExecuteInstruction();
            if (view.RowCount == 0 && _WorkPlacePresint.ResultsDataTable.Rows.Count == 0)
            {
               // btnAddToPrePlanningWorkplace.Visible = true; 
                btnOK.Enabled = false;

            }
            else 
            { //btnAddToPrePlanningWorkplace.Visible = false; 
                btnOK.Enabled = true; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            formStatus = true;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            formStatus = false ;
            Close();
        }

        private void viewWorkplaces_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int checkedRowIndex = -1;
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Selected" && (bool)e.Value)
            {
                int rowHandle = view.GetRowHandle(checkedRowIndex);
                view.SetRowCellValue(rowHandle, "Selected", false);
                checkedRowIndex = view.GetDataSourceRowIndex(e.RowHandle);

               
            }

           
        }

        private void viewWorkplaces_RowCellClick(object sender, RowCellClickEventArgs e)
        {
         // desc =  viewWorkplaces.GetDataRow(e.RowHandle)["DESCRIPTION"].ToString();
         // desc = viewWorkplaces.GetRowCellValue  (viewWorkplaces .FocusedRowHandle ,"DESCRIPTION").ToString();
        }

    }
}
