using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning.PlanningProtocolCapture;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PlanningProtocolCapture
{
    public partial class frmLockUnlockData : Form 
    {
        DataTable mainData = new DataTable();
        DataTable distinctValuesWorkplace = new DataTable();
        DataRow[] theResult;
        string theapproveType;
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        public frmLockUnlockData()
        {
            InitializeComponent();
        }

        public DataTable setLockUnlockData(DataTable theData,DataTable theDataNew, string approveType)
        {
            theapproveType = approveType;
            string FieldRequired;
            string theValue;
           // mainData = theData;
            mainData = theDataNew;
           // DataRow[] theResult;
            DataRow[] res1;
            DataRow[] res2;
            DataView viewWorkplace = new DataView(theData);
          //  distinctValuesWorkplace = viewWorkplace.ToTable(true, "WPDESCRIPTION", "WORKPLACEID", "TemplateID", "TemplateName", "PRODMONTH", "SECTIONID", "ActivityType","FieldRequired","theValue","Approved","FieldName");
            distinctValuesWorkplace = viewWorkplace.ToTable(true, "DESCRIPTION", "WORKPLACEID", "TemplateID","TemplateName", "PRODMONTH", "SECTIONID", "ActivityType", "Approved");
            if (approveType == "APPROVE")

            {  theResult = distinctValuesWorkplace.Select("Approved = 'NO'");
              // if(distinctValuesWorkplace .Select ("Approved='NO' and FieldRequired=1 and TheValue=''"))
               // if(distinctValuesWorkplace .wp
           // res1 = distinctValuesWorkplace.Select("FieldRequired=1");
            btnApprove.Text = "Approve";
            this.Text = "Approve Data";
            gcCanApprove.Caption = "Can Approve";
            }
            else { theResult = distinctValuesWorkplace.Select("Approved = 'YES'"); btnApprove.Text = "Un approve"; this.Text = "Un approve Data"; gcCanApprove.Caption = "Can Un approve"; }

            if (theResult.Length > 0)
            {
                DataColumn canApprove = new DataColumn();
                canApprove.ColumnName = "canApprove";
                if (approveType == "APPROVE")
                    canApprove.Caption = "Approve";
                else canApprove.Caption = "Un approve";
                canApprove.DataType = System.Type.GetType("System.Boolean");
                canApprove.DefaultValue = false;
                distinctValuesWorkplace.Columns.Add(canApprove);
                gridWorkplace.DataSource = distinctValuesWorkplace;


                foreach (DataRow r in distinctValuesWorkplace.Rows)
                {
                    editProdMonth.Text = r["PRODMONTH"].ToString();
                    editSection.Text = r["SECTIONID"].ToString();
                    editTemplateName.Text = r["TemplateName"].ToString();
                    //  listWorkplace.Items.Add(r["WPDESCRIPTION"].ToString());

                }

                if (approveType == "APPROVE")
                {
                    gcApproved.FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Approved] = 'NO'");
                }
                else { gcApproved.FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Approved] = 'YES'"); }

                //  object val = "Poland";
                //DevExpress.XtraGrid.Columns.GridColumn columnCountry = gwWorkplaces.Columns["ShipCountry"];
                
                //columnCustomer.FilterInfo = new ColumnFilterInfo(columnCountry, val);


                //gwWorkplaces.ActiveFilterString = "[Approve] = NO";
                ShowDialog();
            }
            else { MessageBox.Show("No comments available for approval."); }

            return mainData;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {

            //foreach (DataRow r in distinctValuesWorkplace.Rows)
            //{
            //    editProdMonth.Text = r["PRODMONTH"].ToString();
            //    editSection.Text = r["SECTIONID"].ToString();
            //    editTemplateName.Text = r["TemplateName"].ToString();
            //    listWorkplace.Items.Add(r["WPDESCRIPTION"].ToString());

            //}

            if (theapproveType == "APPROVE")
            {

                //MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
                //_SaveData.ConnectionString = TUserInfo.ConnectionString;
                //_SaveData.SqlStatement = "spPlanProt_ApproveData";
                //_SaveData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                string text = "";
                    string separator = "";
                foreach (DataRow r in distinctValuesWorkplace.Rows)
                {

                    string textCMB = text;
                    MWDataManager.clsDataAccess _RequiredData = new MWDataManager.clsDataAccess();
                    _RequiredData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _RequiredData.SqlStatement = "sp_RequiredCountOfDataApproval";
                    _RequiredData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                    if (Convert.ToBoolean(r["canApprove"].ToString()) == true)
                    {
                   
                    SqlParameter[] _paramCollection1 = 
                    {
                    _RequiredData.CreateParameter("@Prodmonth", SqlDbType.Int, 7,Convert.ToInt32(r["PRODMONTH"].ToString())),
                    _RequiredData.CreateParameter("@SectionID ", SqlDbType.VarChar, 50,r["SECTIONID"].ToString()),
                    _RequiredData.CreateParameter("@WorkplaceID", SqlDbType.VarChar , 50,r["WORKPLACEID"]),
                    _RequiredData.CreateParameter("@TemplateID", SqlDbType.Int, 0,r["TemplateID"]),
                   // _RequiredData.CreateParameter("@ApprovedBy", SqlDbType.VarChar, 0,TUserInfo.UserID),
                    _RequiredData.CreateParameter("@ActivityType", SqlDbType.Int, 0,r["ActivityType"]),
                  //  _SaveData.CreateParameter("@ApproveItem", SqlDbType.VarChar, 0,"YES"),

                    };

                    _RequiredData.ParamCollection = _paramCollection1;
                    _RequiredData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _RequiredData.ExecuteInstruction();

                    DataTable dt = new DataTable();
                    dt = _RequiredData.ResultsDataTable;
                   

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["FIELDCOUNT"]) > 0)
                        {

                            text += separator + r["DESCRIPTION"];
                            separator = ",";
                           // textCMB += text;

                            string ss = string.Join(",", r["DESCRIPTION"]);
                           
                        }
                        // }
                        else
                        {
                            MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
                            _SaveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                           // _SaveData.SqlStatement = "spPlanProt_ApproveData";
                            _SaveData.SqlStatement = "sp_PlanProt_ApproveData";
                            _SaveData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                            // if (Convert.ToBoolean(r["canApprove"].ToString()) == true)
                            // {

                            SqlParameter[] _paramCollection = 
                    {
                    _SaveData.CreateParameter("@Prodmonth", SqlDbType.Int, 7,Convert.ToInt32(r["PRODMONTH"].ToString())),
                    _SaveData.CreateParameter("@SectionID ", SqlDbType.VarChar, 50,r["SECTIONID"].ToString()),
                    _SaveData.CreateParameter("@WorkplaceID", SqlDbType.VarChar , 50,r["WORKPLACEID"]),
                    _SaveData.CreateParameter("@TemplateID", SqlDbType.Int, 0,r["TemplateID"]),
                    _SaveData.CreateParameter("@ApprovedBy", SqlDbType.VarChar, 0,TUserInfo.UserID),
                    _SaveData.CreateParameter("@ActivityType", SqlDbType.Int, 0,r["ActivityType"]),
                    _SaveData.CreateParameter("@ApproveItem", SqlDbType.VarChar, 0,"YES"),

                    };
                            //foreach (DataRow s in mainData .Rows )
                            //{
                            //    if (s["FieldRe
                            // bool result = false;
                            // var ind = 0;
                            //  DataRow s;
                            foreach (DataRow s in mainData.Rows)
                            {
                                if (s["WORKPLACEID"].ToString() == r["WORKPLACEID"].ToString())
                                //else
                                {
                                    s["Approved"] = "YES";
                                }

                            }
                            _SaveData.ParamCollection = _paramCollection;
                            _SaveData.queryReturnType = MWDataManager.ReturnType.longNumber;
                            clsDataResult result = _SaveData.ExecuteInstruction();
                                if (result.success == false)
                                {
                                    MessageBox.Show(result.Message);
                                }
                            dt.AcceptChanges();
                            ucPlanProtDataView ucp = new ucPlanProtDataView();
                            ucp.Refresh();
                            ucp.Update();
                            ucp.gridPlanProtData.RefreshData();
                            ucp.gridPlanProtData.Update();
                        }
                    }
                   
                    }

                }
                if (text == "," )
                {
                    MessageBox.Show("Please provide all data required for the Workplaces " + text, "Cannot approve the Workplace", MessageBoxButtons.OK);
                }
                else
                {}
                mainData.Select("");
                Close();
            }

            if (theapproveType == "UNAPPROVE")
            {
                MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
                _SaveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _SaveData.SqlStatement = "sp_PlanProt_ApproveData";
                _SaveData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                foreach (DataRow r in distinctValuesWorkplace.Rows)
                {
                    if (Convert.ToBoolean(r["canApprove"].ToString()) == true)
                    {

                        SqlParameter[] _paramCollection = 
                    {
                    _SaveData.CreateParameter("@Prodmonth", SqlDbType.Int, 7,Convert.ToInt32(r["PRODMONTH"].ToString())),
                    _SaveData.CreateParameter("@SectionID ", SqlDbType.VarChar, 50,r["SECTIONID"].ToString()),
                    _SaveData.CreateParameter("@WorkplaceID", SqlDbType.VarChar, 50,r["WORKPLACEID"]),
                    _SaveData.CreateParameter("@TemplateID", SqlDbType.Int, 0,r["TemplateID"]),
                    _SaveData.CreateParameter("@ApprovedBy", SqlDbType.VarChar, 0,TUserInfo.UserID),
                    _SaveData.CreateParameter("@ActivityType", SqlDbType.Int, 0,r["ActivityType"]),
                    _SaveData.CreateParameter("@ApproveItem", SqlDbType.VarChar, 0,"NO"),

                    };


                        foreach (DataRow s in mainData.Rows)
                        {
                            if (s["WORKPLACEID"].ToString() == r["WORKPLACEID"].ToString())
                                s["Approved"] = "NO";
                        }

                        _SaveData.ParamCollection = _paramCollection;
                        _SaveData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        _SaveData.ExecuteInstruction();
                    }

                }
            }


            mainData.Select("");
            Close();
       // }
        }

        private void gwWorkplaces_FilterEditorCreated(object sender, DevExpress.XtraGrid.Views.Base.FilterControlEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            // foreach (DataRow r in distinctValuesWorkplace.Select ("Approved='NO'"))
            foreach (DataRow r in theResult )
             {
                 DataTable dt = new DataTable();
                 r["canApprove"] = checkEdit1.Checked;
                 if (checkEdit1.Checked == true)
                 {
                     checkEdit1.Text = "Un Select All";
                 }
                 else
                 {
                     checkEdit1.Text = "Select All";
                 }
             }
        }
    }
}