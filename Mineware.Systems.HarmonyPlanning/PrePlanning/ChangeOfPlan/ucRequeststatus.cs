using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Mineware.Systems.Global;
using Mineware.Systems.Planning.PrePlanning.ChangeOfPlan.DataClass;
using Mineware.Systems.ProductionGlobal;
using System.Text;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    public partial class ucRequeststatus : ucBaseUserControl  
    {
        public MWDataManager.clsDataAccess _RequestList = new MWDataManager.clsDataAccess();
        public MWDataManager.clsDataAccess _RequestList1 = new MWDataManager.clsDataAccess();
        public string MOID = "";
        public string Prodmonth = "";



        //     public int DSRow;
        ucPrePlanningMain frm1;
        
        public ucRequeststatus()
        {
           // this.ClientSize = FormWindowState.Maximized;
             
            InitializeComponent();
           // loadRequestedData();
              //  loadRequestList();
               // int DSRow = gvRequests1.FocusedRowHandle;
               
        }

     

        public void loadRequestList()
        {
           
            _RequestList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _RequestList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _RequestList.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_RequestList.SqlStatement = "sp_RequestPlanningUserList '" + TUserInfo.UserID + "'";
            //_RequestList.ExecuteInstruction();
            //if (_RequestList.ResultsDataTable.DefaultView.Count == 0)
            //{
            //    Visible = false;
            //}
            //else
            //{
            //    gcRequestList.DataSource = _RequestList.ResultsDataTable;
            //    ShowDialog();
            _RequestList.SqlStatement = "sp_RevisedPlanning_Status";
            _RequestList.ExecuteInstruction();
            //gcAppDecList.DataSource = _RequestList.ResultsDataTable;
          //  gridControl1.DataSource = _RequestList.ResultsDataTable;
            DataTable dd = new DataTable();
            dd = _RequestList.ResultsDataTable;
            loadRequestedData();
        }

        public void loadRequestedData()
        {

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "sp_StatusDetails";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //SqlParameter[] _paramCollection =      
            //    { 
            //         _dbMan.CreateParameter("@Prodmonth", SqlDbType.Int , 7,TSystemSettings.CurrentProductionMonth),


            //    };
            //_dbMan.ParamCollection = _paramCollection;
            // _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
            string y = Prodmonth.Substring(0, 4);
            string m = Prodmonth.Substring(4, 2);

            String histProdmonth = (Convert.ToInt32(y) - 1).ToString() + m;

            _dbMan.ExecuteInstruction();
            StringBuilder theSQl = new StringBuilder();
            theSQl.AppendLine("SELECT DISTINCT sd.ChangeRequestID, \n"); 
            theSQl.AppendLine("sd.WorkplaceID,\n"); 
            theSQl.AppendLine("sd.ChangeType,\n"); 
            theSQl.AppendLine("sd.ProdMonth,\n"); 
            theSQl.AppendLine("sd.[Workplace Name],\n");
            theSQl.AppendLine("sd.Name,sd.Comments,ChangeID \n"); 
            theSQl.AppendLine(",CONVERT(varchar(50), ppcra.RequestDate,103) RequestDate \n"); 
            theSQl.AppendLine(",max(Status) Status from StatusDetails sd \n"); 
            theSQl.AppendLine("left join PREPLANNING_CHANGEREQUEST_APPROVAL ppcra on \n"); 
            theSQl.AppendLine("ppcra.ChangeRequestID =sd.ChangeRequestID  \n"); 
            theSQl.AppendLine("INNER JOIN\n"); 
            theSQl.AppendLine("[dbo].[PREPLANNING_CHANGEREQUEST] PPCR on\n"); 
            theSQl.AppendLine("ppcra.ChangeRequestID = PPCR.ChangeRequestID\n"); 
            theSQl.AppendLine("INNER JOIN SECTION_COMPLETE SC on\n"); 
            theSQl.AppendLine("'"+ MOID + "' = sc.SECTIONID_2 and\n"); 
            theSQl.AppendLine("PPCR.ProdMonth = SC.PRODMONTH\n"); 
            theSQl.AppendLine("WHERE \n");
            theSQl.AppendLine("sd.Prodmonth >= "+histProdmonth+" \n");
            theSQl.AppendLine("And SC.SECTIONID_2 = '"+MOID+"' --in (SELECT SectionID from SECTION WHERE SectionID IN (SELECT SectionID FROM USERS_SECTION where UserID = '" + UserCurrentInfo.UserID + "' and LinkType = 'P')) or\n");
            theSQl.AppendLine("group by sd.ChangeRequestID,sd.ProdMonth,sd.[Workplace Name],sd.Name,sd.Comments,sd.WorkplaceID,sd.ChangeType,ppcra.RequestDate,ChangeID \n");
            theSQl.AppendLine("order by sd.ProdMonth desc, sd.ChangeRequestID desc"); 


            statusDetails.Clear();
            SqlDataAdapter oleDBAdapter1 = new SqlDataAdapter(theSQl.ToString(), TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection));
            SqlDataAdapter oleDBAdapter2 = new SqlDataAdapter(" SELECT Distinct sd.ChangeRequestID, Dep.Description Department, sd.ProdMonth, sd.[Workplace Name], ppcra.Comments , \n"+
                "Status from StatusDetails sd left join PREPLANNING_CHANGEREQUEST_APPROVAL\n"+
                "ppcra on ppcra.ChangeRequestID =sd.ChangeRequestID and  sd.Department=ppcra.Department  \n"+
                "inner join tblDepartments Dep on 	sd.Department = dep.[DepartmentID]  group by sd.ChangeRequestID, Dep.Description, sd.ProdMonth,sd.[Workplace Name],ppcra.Comments,Status ", TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection));
            oleDBAdapter1.Fill(statusDetails ._StatusDetails );
            oleDBAdapter2.Fill(statusDetails .StatusDetails1 );

            _RequestList1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _RequestList1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _RequestList1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _RequestList1.SqlStatement = "sp_PrePlanning_RequestUserList '" + TUserInfo.UserID + "'";
            _RequestList1.ExecuteInstruction();
            if (_RequestList1.ResultsDataTable.DefaultView.Count == 0)
            {
                //Visible = false;
               // gcRequestList.Visible = false;
                
                focusControle1.Visible = false;
                labelControl2.Visible = false;
            }
            else
            {
                gcRequestList.DataSource = _RequestList1.ResultsDataTable;
                
               // focusControle1.Visible = true;
               // labelControl2.Visible = true;

            }
        }

        //private void gvRequests1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        //{
        //    int DSRow = gvRequests1.GetDataSourceRowIndex(e.RowHandle);
        //    if (DSRow >= 0)
        //    {
        //        ReplanningTypes ReplanningTypes = new ReplanningTypes();
        //        replanningType theType = ReplanningTypes.getReplanningType(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ChangeID"].ToString()));
        //        switch (theType)
        //        {
        //            case replanningType.rpStopWp:
        //                ucRequeststatus ucr=new ucRequeststatus() ;
        //                ucr.Controls.Find ("btnApprove",true)[0].Enabled =true ;
        //               // btnApprove.Enabled = true;
        //                ucStopWorkplace ucStopWorkplace = new ucStopWorkplace();
        //                focusControle.Controls.Clear();
        //                ucStopWorkplace.Parent = focusControle;
        //                ucStopWorkplace.Dock = DockStyle.Fill;
        //                ucStopWorkplace.LoadDetails(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeRequestID"].ToString()));
        //                break;
        //            case replanningType.rpCrewChnages:
        //                ucRequeststatus ucr2=new ucRequeststatus() ;
        //                ucr2.Controls.Find ("btnApprove",true)[0].Enabled =true ;
        //              //  btnApprove.Enabled = true;
        //                ucCrewMinerChange ucCrewMinerChange = new ucCrewMinerChange();

        //                focusControle.Controls.Clear();
        //                ucCrewMinerChange.Parent = focusControle;
        //                ucCrewMinerChange.Dock = DockStyle.Fill;
        //                ucCrewMinerChange.LoadDetails(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeRequestID"].ToString()));
        //                break;
        //            case replanningType.rpNewWP:
        //                ucRequeststatus ucr3=new ucRequeststatus() ;
        //                ucr3.Controls.Find ("btnApprove",true)[0].Enabled =true ;
        //              //  btnApprove.Enabled = true;
        //                ucAddWorkplace ucAddWorkplace = new ucAddWorkplace();
        //                focusControle.Controls.Clear();
        //                ucAddWorkplace.Parent = focusControle;
        //                ucAddWorkplace.Dock = DockStyle.Fill;
        //                ucAddWorkplace.LoadDetails(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeRequestID"].ToString()));
        //                break;

        //            case replanningType.rpCallCahnges:
        //                ucRequeststatus ucr4=new ucRequeststatus() ;
        //                ucr4.Controls.Find ("btnApprove",true)[0].Enabled =true ;
        //               // btnApprove.Enabled = true;
        //                ucPlanningValueChanges ucPlanningValueChanges = new ucPlanningValueChanges();
        //                focusControle.Controls.Clear();
        //                ucPlanningValueChanges.Parent = focusControle;
        //                ucPlanningValueChanges.Dock = DockStyle.Fill;
        //                ucPlanningValueChanges.LoadDetails(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeRequestID"].ToString()));
        //                break;
        //        }


        //    }
        //    else
        //    {
        //        focusControle.Controls.Clear();
        //        gcRequestsList.Visible = false;
        //        focusControle.Visible = false;
        //        labelControl2.Visible = false;
        //        Visible = false;
        //    }
        
        //}

        public void loadapprove()
        {
            _RequestList1.ResultsDataTable.AcceptChanges();
            gcRequestList.DataSource = _RequestList1.ResultsDataTable;

            gvRequests.RefreshData();

            frmDeclineApproveChnageOfPLan frmDeclineApproveChnageOfPLan1 = new frmDeclineApproveChnageOfPLan { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
           //int DSRow = gvRequests1.GetDataSourceRowIndex(e.RowHandle);
           // gvRequests1.FocusedRowHandle = 0;
           
           // for (int i = -1; i < gvRequests1.RowCount; i++)
          //  {
            int currentrow = 0;
          //int index=  _RequestList1.ResultsDataTable.Rows.IndexOf(_RequestList1.ResultsDataTable.Rows.Find("ApproveRequestID"));
          //gvRequests1.FocusedRowHandle = gvRequests1.GetRowHandle(index);
          //  currentrow = gvRequests1.GetDataSourceRowIndex(gvRequests1.FocusedRowHandle);
               // int cellvalue = Convert.ToInt32(gvRequests1.GetRowCellValue(i, "ApproveRequestID"));
                //  gvRequests1.FocusedRowHandle = i;
           // gvRequests1.RefreshData();
          //  gvRequests1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
           // bool valid = true;
           // gvRequests.FocusedRowHandle = 0;
            int DSRow = gvRequests.GetDataSourceRowIndex(gvRequests.FocusedRowHandle); 
            if (DSRow < 0 )
            {
                MessageBox.Show("Please select a record", "", MessageBoxButtons.OK);


            }
            else
            {
                frmDeclineApproveChnageOfPLan1.Approve(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ApproveRequestID"].ToString()),_RequestList1 .ResultsDataTable .Rows[DSRow ]["ChangeType"].ToString (), _RequestList1 .ResultsDataTable .Rows[DSRow ]["WPName"].ToString ());
                if (frmDeclineApproveChnageOfPLan1.canceledAction == true)
                {
                    gcRequestList.Refresh();
                    gvRequests.RefreshData();
                    // gvRequests1.DeleteRow(gvRequests1.FocusedRowHandle);
                    //gvRequests.DeleteRow(DSRow);
                    //focusControle1.Controls.Clear();
                    //  gcAppDecList.Refresh();
                    gridControl1.Refresh();
                    loadRequestedData();
                }
                else
                {
                    gcRequestList.Refresh();
                    gvRequests.RefreshData();
                    // gvRequests1.DeleteRow(gvRequests1.FocusedRowHandle);
                    gvRequests.DeleteRow(DSRow);
                    focusControle1.Controls.Clear();
                    //  gcAppDecList.Refresh();
                    gridControl1.Refresh();
                    loadRequestedData();
                }
                //<<<<<<< .mine
                //=======
                //  frmDeclineApproveChnageOfPLan1.Approve(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[gvRequests.FocusedRowHandle]["ApproveRequestID"].ToString()));

                //>>>>>>> .r1489
                //gcRequestList.Refresh();
                
                //gvRequests.RefreshData();
                //// gvRequests1.DeleteRow(gvRequests1.FocusedRowHandle);
                //gvRequests.DeleteRow(DSRow);
              
                //focusControle1.Controls.Clear();
                ////  }
                foreach (Control control in focusControle1.Controls)
                {
                    control.Dispose();
                }
            }
          //  gcAppDecList.Refresh();
          //  btnApprove.Enabled = false;
            //gridControl1.Refresh();
            //gridView4.RefreshData();
            //gridView6.RefreshData();
           // loadRequestedData();
        }

        public void loaddecline()
        {
            _RequestList1.ResultsDataTable.AcceptChanges();
            gcRequestList.DataSource = _RequestList1.ResultsDataTable;

            gvRequests.RefreshData();
            frmDeclineApproveChnageOfPLan frmDeclineApproveChnageOfPLan2 = new frmDeclineApproveChnageOfPLan { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
          int  DSRow = gvRequests.GetDataSourceRowIndex(gvRequests.FocusedRowHandle); 
          if (DSRow == -1 || DSRow == -2147483648)
          {
              MessageBox.Show("Please select a record", "", MessageBoxButtons.OK);
              //if(DSRow == -2147483648)
              //{
              //    MessageBox .Show ("There are no records present","", MessageBoxButtons .OK );
              //}

          }
          else
          {
              frmDeclineApproveChnageOfPLan2.Decline(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ApproveRequestID"].ToString()),_RequestList1 .ResultsDataTable .Rows[DSRow ]["ChangeType"].ToString (), _RequestList1 .ResultsDataTable .Rows[DSRow ]["WPName"].ToString ());
              if (frmDeclineApproveChnageOfPLan2.canceledAction == true)
              {
                  gcRequestList.Refresh();
                  gvRequests.RefreshData();
                  // gvRequests1.DeleteRow(gvRequests1.FocusedRowHandle);
                  //gvRequests.DeleteRow(DSRow);
                  //focusControle1.Controls.Clear();
                  //  gcAppDecList.Refresh();
                  gridControl1.Refresh();
                  loadRequestedData();
              }
              else
              {
                  gcRequestList.Refresh();
                  gvRequests.RefreshData();
                  // gvRequests1.DeleteRow(gvRequests1.FocusedRowHandle);
                  gvRequests.DeleteRow(DSRow);
                  focusControle1.Controls.Clear();
                  //  gcAppDecList.Refresh();
                  gridControl1.Refresh();
                  loadRequestedData();
              }

          }
        }

        public  void gvRequests1_RowClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //focusControle1.Controls.Clear();
            _RequestList1.ResultsDataTable.AcceptChanges();
            gcRequestList.DataSource = _RequestList1.ResultsDataTable;
            
            gvRequests.RefreshData();
          //  focusControle1.Focus();

          int   DSRow = gvRequests.GetDataSourceRowIndex(e.RowHandle);
            if (DSRow >= 0)
            {
                //focusControle1.Controls.Clear();
        
                ReplanningTypes ReplanningTypes = new ReplanningTypes();
                replanningType theType = ReplanningTypes.getReplanningType(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ChangeID"].ToString()));
                switch (theType)
                {
                    case replanningType.rpStopWp:
                        ucRequeststatus ucr = new ucRequeststatus();
                       
                       // ucr.Controls.Find("btnApprove", true)[0].Enabled = true;
                        //frm1.btnApprove.Enabled = true;
                        // btnApprove.Enabled = true;
                        ucStopWorkplace ucStopWorkplace = new ucStopWorkplace { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucStopWorkplace.Parent = focusControle1;
                        ucStopWorkplace.Dock = DockStyle.Fill;
                        ucStopWorkplace.LoadDetails(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ChangeRequestID1"].ToString()));
                        break;
                    case replanningType.rpCrewChnages:
                       // ucRequeststatus ucr2 = new ucRequeststatus();
                       // ucr2.Controls["btnApprove"].Enabled = true;
                      //  frmPrePlanningMain frm1 = new frmPrePlanningMain();
                      //  frm1.Controls["btnApprove"].Enabled = true;
                        //ucr2.Controls.Find("btnApprove", true)[0].Enabled = true;
                        //  btnApprove.Enabled = true;
                        ucCrewMinerChange ucCrewMinerChange = new ucCrewMinerChange { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };

                        focusControle1.Controls.Clear();
                        ucCrewMinerChange.Parent = focusControle1;
                        ucCrewMinerChange.Dock = DockStyle.Fill;
                        ucCrewMinerChange.LoadDetails(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ChangeRequestID1"].ToString()));
                        break;
                    case replanningType.rpNewWP:
                       // ucRequeststatus ucr3 = new ucRequeststatus();
                       // ucr3.Controls.Find("btnApprove", true)[0].Enabled = true;
                      //  frmPrePlanningMain frm2 = new frmPrePlanningMain();
                     //   frm2.Controls["btnApprove"].Enabled = true;
                        //  btnApprove.Enabled = true;
                        //frm1.btnApprove.Enabled = true;
                        ucAddWorkplace ucAddWorkplace = new ucAddWorkplace { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucAddWorkplace.Parent = focusControle1;
                        ucAddWorkplace.Dock = DockStyle.Fill;
                        ucAddWorkplace.LoadDetails(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ChangeRequestID1"].ToString()));
                        break;

                    case replanningType.rpCallCahnges:
                        
                       // ucRequeststatus ucr4 = new ucRequeststatus();
                        //ucr4.Controls.Find("btnApprove", true)[0].Enabled  = true;
                       ucPrePlanningMain frm3 = new ucPrePlanningMain();
                      // frm3.rpReplanningStatus.Ribbon.Controls["btnApprove"].Enabled = true;
                       //frm3.btnApprove.Enabled = true;
                    //   frm1.btnApprove.Enabled = true;
                        
                       // frm3.Controls["btnApprove"].Enabled = true;
                        // btnApprove.Enabled = true;
                       ucPlanningValueChanges ucPlanningValueChanges = new ucPlanningValueChanges { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucPlanningValueChanges.Parent = focusControle1;
                        ucPlanningValueChanges.Dock = DockStyle.Fill;
                        ucPlanningValueChanges.LoadDetails(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ChangeRequestID1"].ToString()));
                        frm3.btnEnabled();
                        break;

                    case replanningType .rpMoveWP :
                         ucPrePlanningMain frm31 = new ucPrePlanningMain();

                         MoveBookings ucMoveBookings = new MoveBookings { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucMoveBookings.Parent = focusControle1;
                        ucMoveBookings.Dock = DockStyle.Fill;
                        ucMoveBookings.LoadDetails(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ChangeRequestID1"].ToString()));
                        frm31.btnEnabled();
                        break;

                    case replanningType.rpStartWP :
                        ucPrePlanningMain frm32 = new ucPrePlanningMain();

                        StartWorkplace StartWP = new StartWorkplace { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        StartWP.Parent = focusControle1;
                        StartWP.Dock = DockStyle.Fill;
                        StartWP.LoadDetails(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ChangeRequestID1"].ToString()));
                        frm32.btnEnabled();
                        break;

                    case replanningType.rpMiningMethodChange:
                        ucPrePlanningMain frm3111 = new ucPrePlanningMain();

                        ucMiningMethodChange ucMMChange = new ucMiningMethodChange { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucMMChange.Parent = focusControle1;
                        ucMMChange.Dock = DockStyle.Fill;
                        ucMMChange.LoadDetails(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ChangeRequestID1"].ToString()));
                        // frm311.btnEnabled();
                        break;


                    case replanningType.rpDrillRig:
                        ucPrePlanningMain frm31111 = new ucPrePlanningMain();

                        ucDrillRiggChanges ucDRChange = new ucDrillRiggChanges { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucDRChange.Parent = focusControle1;
                        ucDRChange.Dock = DockStyle.Fill;
                        ucDRChange.LoadDetails(Convert.ToInt32(_RequestList1.ResultsDataTable.Rows[DSRow]["ChangeRequestID1"].ToString()));
                        // frm311.btnEnabled();
                        break;
                }


            }
            else
            {
                focusControle1.Controls.Clear();
                gcRequestList.Visible = false;
                focusControle1.Visible = false;
                labelControl2.Visible = false;
                Visible = false;
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

          
        }

        private void gridView1_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            
        }

        private void gcAppDecList_Click(object sender, EventArgs e)
        {

        }

        private void gvRequests_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            gvRequests.GetSelectedRows();
        }

        private void gvRequests_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            //e.Info.ImageIndex = -1;
            //gvRequests.OptionsView.ShowIndicator = false;
            //gvRequests.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
           // gvRequests.OptionsSelection.EnableAppearanceFocusedCell = false;
           // gvRequests.OptionsSelection.EnableAppearanceFocusedRow = false;
           // gvRequests.OptionsSelection.EnableAppearanceHideSelection = false;
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            //gridView4.SetMasterRowExpandedEx(0, 0, true);
            //gridView4.SetMasterRowExpanded(1, true);
            //gridView4.SetMasterRowExpanded(3, true);
        }

        private void gridView4_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
         {
            if (e.Column.AbsoluteIndex == 7)
            {
                if (Convert.ToInt32(e.CellValue ) == 0)
                {
                    e.DisplayText = "";
                    e.Graphics.FillRectangle(new SolidBrush(e.Appearance.BackColor), e.Bounds);
                    e.Graphics.DrawImage(imageCollection1.Images[0],
                                     e.Bounds.Left + (e.Bounds.Width - imageCollection1.Images[0].Width) / 2,
                                     e.Bounds.Top + (e.Bounds.Height - imageCollection1.Images[0].Height) / 2);
                }

                if (Convert.ToInt32(e.CellValue) == 1)
                {
                    e.DisplayText = "";
                    e.Graphics.FillRectangle(new SolidBrush(e.Appearance.BackColor), e.Bounds);
                    e.Graphics.DrawImage(imageCollection1.Images[1],
                                     e.Bounds.Left + (e.Bounds.Width - imageCollection1.Images[1].Width) / 2,
                                     e.Bounds.Top + (e.Bounds.Height - imageCollection1.Images[1].Height) / 2);
                }

                if (Convert.ToInt32(e.CellValue) == 2)
                {
                    e.DisplayText = "";
                    e.Graphics.FillRectangle(new SolidBrush(e.Appearance.BackColor), e.Bounds);
                    e.Graphics.DrawImage(imageCollection1.Images[2],
                                     e.Bounds.Left + (e.Bounds.Width - imageCollection1.Images[2].Width) / 2,
                                     e.Bounds.Top + (e.Bounds.Height - imageCollection1.Images[2].Height) / 2);
                }
            }
        }

        private void gridView5_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
          
        }

        private void gridView6_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.AbsoluteIndex == 4)
            {
                if (Convert.ToInt32(e.CellValue) == 0)
                {
                    e.DisplayText = "";
                    e.Graphics.FillRectangle(new SolidBrush(e.Appearance.BackColor), e.Bounds);
                    e.Graphics.DrawImage(imageCollection1.Images[0],
                                     e.Bounds.Left + (e.Bounds.Width - imageCollection1.Images[0].Width) / 2,
                                     e.Bounds.Top + (e.Bounds.Height - imageCollection1.Images[0].Height) / 2);
                }

                if (Convert.ToInt32(e.CellValue) == 1)
                {
                    e.DisplayText = "";
                    e.Graphics.FillRectangle(new SolidBrush(e.Appearance.BackColor), e.Bounds);
                    e.Graphics.DrawImage(imageCollection1.Images[1],
                                     e.Bounds.Left + (e.Bounds.Width - imageCollection1.Images[1].Width) / 2,
                                     e.Bounds.Top + (e.Bounds.Height - imageCollection1.Images[1].Height) / 2);
                }

                if (Convert.ToInt32(e.CellValue) == 2)
                {
                    e.DisplayText = "";
                    e.Graphics.FillRectangle(new SolidBrush(e.Appearance.BackColor), e.Bounds);
                    e.Graphics.DrawImage(imageCollection1.Images[2],
                                     e.Bounds.Left + (e.Bounds.Width - imageCollection1.Images[2].Width) / 2,
                                     e.Bounds.Top + (e.Bounds.Height - imageCollection1.Images[2].Height) / 2);
                }
            }
        }

        private void gridView4_RowClick(object sender, RowClickEventArgs e)
        {
            DataTable dt = new DataTable();
             dt=   _RequestList.ResultsDataTable;
            //  int DSRow = gvRequests.GetDataSourceRowIndex(e.RowHandle);
             DataRow dr = gridView4.GetFocusedDataRow();

            if (dr!= null)
            {
                ReplanningTypes ReplanningTypes = new ReplanningTypes();
                replanningType theType = ReplanningTypes.getReplanningType(Convert.ToInt32(dr["ChangeID"].ToString()));
                switch (theType)
                {
                    case replanningType.rpStopWp:
                        ucRequeststatus ucr = new ucRequeststatus();

                        // ucr.Controls.Find("btnApprove", true)[0].Enabled = true;
                        //frm1.btnApprove.Enabled = true;
                        // btnApprove.Enabled = true;
                       // focusControle1.Controls.Clear();
                        ucStopWorkplace ucStopWorkplace = new ucStopWorkplace { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                         focusControle1.Controls.Clear();

                        //frmStatus frmst = new frmStatus();
                        // frmst.panelControl1.Controls.Clear();
                        //panelControl1.Controls.Clear();
                        //  ucStopWorkplace.Parent = panelControl1;
                        ucStopWorkplace.Parent = focusControle1;
                        ucStopWorkplace.Dock = DockStyle.Fill;
                        ucStopWorkplace.LoadDetails(Convert.ToInt32(dr["ChangeRequestID"].ToString()));
                        // frmst.ShowDialog();
                        break;
                    case replanningType.rpCrewChnages:
                        // ucRequeststatus ucr2 = new ucRequeststatus();
                        // ucr2.Controls["btnApprove"].Enabled = true;
                        //  frmPrePlanningMain frm1 = new frmPrePlanningMain();
                        //  frm1.Controls["btnApprove"].Enabled = true;
                        //ucr2.Controls.Find("btnApprove", true)[0].Enabled = true;
                        //  btnApprove.Enabled = true;
                        //focusControle1.Controls.Clear();
                        ucCrewMinerChange ucCrewMinerChange = new ucCrewMinerChange { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        //frmStatus frmst1 = new frmStatus();
                        //frmst1.panelControl1.Controls.Clear();
                        // panelControl1.Controls.Clear();
                        // ucCrewMinerChange.Parent = panelControl1;
                        ucCrewMinerChange.Parent = focusControle1;
                        ucCrewMinerChange.Dock = DockStyle.Fill;
                        ucCrewMinerChange.LoadDetails(Convert.ToInt32(dr["ChangeRequestID"].ToString()));
                       // frmst1.ShowDialog();
                        break;
                    case replanningType.rpNewWP:
                        // ucRequeststatus ucr3 = new ucRequeststatus();
                        // ucr3.Controls.Find("btnApprove", true)[0].Enabled = true;
                        //  frmPrePlanningMain frm2 = new frmPrePlanningMain();
                        //   frm2.Controls["btnApprove"].Enabled = true;
                        //  btnApprove.Enabled = true;
                        //frm1.btnApprove.Enabled = true;
                       // focusControle1.Controls.Clear();
                        ucAddWorkplace ucAddWorkplace = new ucAddWorkplace { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        //frmStatus frmst3 = new frmStatus();
                        //frmst3.panelControl1.Controls.Clear();
                        // panelControl1.Controls.Clear();
                        // ucAddWorkplace.Parent = panelControl1;
                        ucAddWorkplace.Parent = focusControle1;
                        ucAddWorkplace.Dock = DockStyle.Fill;
                        ucAddWorkplace.LoadDetails(Convert.ToInt32(dr["ChangeRequestID"].ToString()));
                       // frmst3.ShowDialog();
                        break;

                    case replanningType.rpCallCahnges:

                        // ucRequeststatus ucr4 = new ucRequeststatus();
                        //ucr4.Controls.Find("btnApprove", true)[0].Enabled  = true;
                        ucPrePlanningMain frm3 = new ucPrePlanningMain { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        // frm3.rpReplanningStatus.Ribbon.Controls["btnApprove"].Enabled = true;
                        //frm3.btnApprove.Enabled = true;
                        //   frm1.btnApprove.Enabled = true;

                        // frm3.Controls["btnApprove"].Enabled = true;
                        // btnApprove.Enabled = true;
                       // focusControle1.Controls.Clear();
                        ucPlanningValueChanges ucPlanningValueChanges = new ucPlanningValueChanges { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        //frmStatus frmst2 = new frmStatus();
                        //frmst2.panelControl1.Controls.Clear();
                        //panelControl1.Controls.Clear();
                        // ucPlanningValueChanges.Parent = panelControl1;
                        ucPlanningValueChanges.Parent = focusControle1;
                        ucPlanningValueChanges.Dock = DockStyle.Fill;
                        ucPlanningValueChanges.LoadDetails(Convert.ToInt32(dr["ChangeRequestID"].ToString()));
                        //frm3.btnEnabled();
                       // frmst2.ShowDialog();
                        break;

                    case replanningType.rpMoveWP:
                        //ucPrePlanningMain frm31 = new ucPrePlanningMain();

                        MoveBookings ucMoveBookings = new MoveBookings { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucMoveBookings.Parent = focusControle1;
                        ucMoveBookings.Dock = DockStyle.Fill;
                        ucMoveBookings.LoadDetails(Convert.ToInt32(dr["ChangeRequestID"].ToString()));
                       // frm31.btnEnabled();
                        break;

                    case replanningType.rpStartWP :
                        //ucPrePlanningMain frm311 = new ucPrePlanningMain();

                        StartWorkplace ucStartWP = new StartWorkplace { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucStartWP.Parent = focusControle1;
                        ucStartWP.Dock = DockStyle.Fill;
                        ucStartWP.LoadDetails(Convert.ToInt32(dr["ChangeRequestID"].ToString()));
                       // frm311.btnEnabled();
                        break;

                    case replanningType.rpMiningMethodChange :
                        ucPrePlanningMain frm3111 = new ucPrePlanningMain();

                        ucMiningMethodChange ucMMChange = new ucMiningMethodChange { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucMMChange.Parent = focusControle1;
                        ucMMChange.Dock = DockStyle.Fill;
                        ucMMChange.LoadDetails(Convert.ToInt32(dr["ChangeRequestID"].ToString()));
                        // frm311.btnEnabled();
                        break;

                    case replanningType.rpDrillRig :
                        //ucPrePlanningMain frm31111 = new ucPrePlanningMain();

                        ucDrillRiggChanges ucDRChange = new ucDrillRiggChanges { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucDRChange.Parent = focusControle1;
                        ucDRChange.Dock = DockStyle.Fill;
                        ucDRChange.LoadDetails(Convert.ToInt32(dr["ChangeRequestID"].ToString()));
                        // frm311.btnEnabled();
                        break;
                    case replanningType.rpDeleteWorkplace:
                        //ucPrePlanningMain frm311111 = new ucPrePlanningMain();

                        ucDeleteWorkplace ucDelete = new ucDeleteWorkplace { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        focusControle1.Controls.Clear();
                        ucDelete.Parent = focusControle1;
                        ucDelete.Dock = DockStyle.Fill;
                        ucDelete.LoadDetails(Convert.ToInt32(dr["ChangeRequestID"].ToString()));
                        // frm311.btnEnabled();
                        break;
                }
            }
            else
            {
                focusControle1.Controls.Clear();
                gcRequestList.Visible = false;
                focusControle1.Visible = false;
                labelControl2.Visible = false;
                Visible = false;
            }
        }

        private void focusControle1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gcRequestList_Click(object sender, EventArgs e)
        {
            focusControle1.Controls.Clear();
        }

        private void ucRequeststatus_Load(object sender, EventArgs e)
        {
            //loadRequestedData();
            loadRequestList();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void focusControle1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
