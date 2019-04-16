using Mineware.Systems.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Mineware.Systems.Global;
using DevExpress.XtraEditors;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports
{
 public    class PlanningProtocolReportProperties:clsBase
    {
        private DateTime _Prodmonth;
        public DateTime Prodmonth { get { return _Prodmonth; } set { _Prodmonth = value; } }


        private string _Code;
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value; UpdateWorkplceArg e = new UpdateWorkplceArg(); OnUpdateWorkplceRequest(e);
            }
        }

         private DataTable _Workplce;
        public DataTable Workplce
        {
            get
            {
                _Workplce = loadWorkplce();
                return _Workplce;
            }

        }


        private DataTable _Indicatestatus;
        public DataTable Indicatestatus
        {
            get
            {
                _Indicatestatus = loadIndicatestatus();
                return _Indicatestatus;
            }

        }


        private DataTable loadIndicatestatus()
        {
            MWDataManager.clsDataAccess _DataStatus = new MWDataManager.clsDataAccess();
            _DataStatus.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
            _DataStatus.SqlStatement = "SELECT 'L' theStatus, COUNT(Plancode) TheCount FROM PLANMONTH a inner join Section_complete b on \n" +
                                       "a.prodmonth = b.Prodmonth and \n" +
                                       "a.Sectionid = b.SectionID \n" +
                                       "WHERE SectionID_2 = '" + SECTIONID_2  + "' and \n" +
                                       "a.Prodmonth = " + TProductionGlobal .ProdMonthAsString (Prodmonth ) + "  and \n" +
                                       "Activity = " + Code  + " and \n" +
                                       "Plancode = 'MP' AND LOCKED=1\n" +
                                       "UNION \n" +
                                       "SELECT 'D' theStatus,COUNT(Plancode) TheCount FROM PLANMONTH a inner join Section_complete b on \n" +
                                       "a.prodmonth = b.Prodmonth and \n" +
                                       "a.Sectionid = b.SectionID\n" +
                                       "WHERE SectionID_2 = '" + SECTIONID_2 + "' and \n" +
                                       "a.Prodmonth = " + TProductionGlobal.ProdMonthAsString(Prodmonth) + " and \n" +
                                       "Activity = " + Code + " and \n" +
                                       "Plancode='MP' AND LOCKED=0";
            _DataStatus.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _DataStatus.queryReturnType = MWDataManager.ReturnType.DataTable;
            _DataStatus.ExecuteInstruction();

            return _DataStatus.ResultsDataTable;
        }

     private DataTable loadWorkplce()
     {
          string ACTIVITY ="";
           ACTIVITY   = Code;
             
                    MWDataManager.clsDataAccess WPData = new MWDataManager.clsDataAccess();
                    WPData.ConnectionString = TConnections.GetConnectionString(systemDBTag, UserCurrentInfo.Connection);
                    string Activity;
                    //if (ACTIVITY == "0")
                    //{
                    //    Activity = "S";
                    //}
                    //else { Activity = "D"; }
                    WPData.SqlStatement = "SELECT WP.WORKPLACEID,WP.DESCRIPTION FROM dbo.WORKPLACE WP " +
                                          "WHERE WP.[Activity] = '" + ACTIVITY + "' AND " +
                                          "(WP.DELETED <> 'Y' OR " +
                                          "WP.DELETED IS NULL)";
                    WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    WPData.ExecuteInstruction();

         return WPData .ResultsDataTable ;

                    //rpWPSelection.DataSource = WPData.ResultsDataTable; 
                    //rpWPSelection.DisplayMember = "DESCRIPTION";
                    //rpWPSelection.ValueMember = "WORKPLACEID";
     }

     public event UpdateWorkplce UpdateWorkplceRequest;
     protected void OnUpdateWorkplceRequest(UpdateWorkplceArg e)
     {
         if (UpdateWorkplceRequest != null)
         {
             UpdateWorkplceRequest(this, e);
         }
     }
     public delegate void UpdateWorkplce(object sender, UpdateWorkplceArg e);

     public class UpdateWorkplceArg : EventArgs
     {

     }


     public event UpdatePlanningData UpdatePlanningDataRequest;
     protected void OnUpdatePlanningDataRequest(UpdatePlanningDataArg e)
     {
         if (UpdatePlanningDataRequest != null)
         {
             UpdatePlanningDataRequest(this, e);
         }
     }
     public delegate void UpdatePlanningData(object sender, UpdatePlanningDataArg e);

     public class UpdatePlanningDataArg : EventArgs
     {

     }



     public event UpdateMOWPSelection UpdateMOWPSelectionRequest;
     protected void OnUpdateMOWPSelection(UpdateMOWPSelectionArg e)
     {
         if (UpdateMOWPSelectionRequest != null)
         {
             UpdateMOWPSelectionRequest(this, e);
         }
     }
     public delegate void UpdateMOWPSelection(object sender, UpdateMOWPSelectionArg e);

     public class UpdateMOWPSelectionArg : EventArgs
     {
     
     }



        private string _SECTIONID_2;
        public string SECTIONID_2
        {
            get
            {
                return _SECTIONID_2;
            }
            set
            {
                _SECTIONID_2 = value; UpdatePlanningDataArg e = new UpdatePlanningDataArg(); OnUpdatePlanningDataRequest(e);
            }
        }

        private string _WORKPLACEID;
        public string WORKPLACEID
        {
            get
            {
                return _WORKPLACEID;
            }
            set
            {
                _WORKPLACEID = value;
            }
        }

        private string   _Print;
        public string Print { get { return _Print; } set { _Print = value; UpdateMOWPSelectionArg e = new UpdateMOWPSelectionArg(); OnUpdateMOWPSelection(e); } }

        private string _PPD;
        public string PPD { get { return _PPD; } set { _PPD = value; } }
    }
}
