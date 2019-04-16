using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using Mineware.Systems.Global;
using DevExpress.XtraScheduler.Commands;
using Mineware.Systems.GlobalConnect;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using Mineware.Systems.Production.SysAdminScreens.SetupCycles;
using FastReport;
using System.IO;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class ucMinersNotes : Mineware.Systems.Global.ucBaseUserControl
    {
        public ucMinersNotes()
        {
            InitializeComponent();
        }

        private void Closebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void ucMinersNotes_Load(object sender, EventArgs e)
        {
            int test = TUserInfo.DepartmentID;



            //return;
            loadWorkNoteWorkplaces();
        }


        private void loadWorkNoteWorkplaces()
        {
            MWDataManager.clsDataAccess _dbManMain = new MWDataManager.clsDataAccess();
            _dbManMain.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManMain.SqlStatement = "Select * from (   \r\n" +
                                    "Select wp.WorkplaceID , wp.description WP, s2.ReportToSectionid mo  from planning p, workplace wp, SECTION s1, SECTION s2 \r\n" +
                                    "where p.Prodmonth = s1.prodmonth and p.SectionID = s1.SectionID  \r\n" +
                                    "and s1.Prodmonth = s2.Prodmonth and s1.ReportToSectionid = s2.SectionID  \r\n" +
                                    "and p.workplaceid = wp.workplaceID  \r\n" +
                                    "and p.Prodmonth = (select max(Prodmonth) from PLANNING )   \r\n" +
                                    "group by wp.WorkplaceID ,wp.description,s2.ReportToSectionid  \r\n" +
                                     " ) a left outer join  \r\n" +
                                    " (select a.Workplace ,a.MoNote LatestMoNote  from tbl_MoNotes a, (select workplace, max(calendardate) dd from tbl_MoNotes group by workplace) b   \r\n" +
                                    "where a.Workplace = b.Workplace and a.calendardate = b.dd) b  \r\n" +
                                    " on a.WorkplaceID = b.Workplace  \r\n" +
                                    "order by mo,WorkplaceID ";

            _dbManMain.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManMain.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManMain.ExecuteInstruction();

            DataTable dtMain = _dbManMain.ResultsDataTable;

            MWDataManager.clsDataAccess _dbManSub = new MWDataManager.clsDataAccess();
            _dbManSub.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManSub.SqlStatement = " Select a.WorkplaceID,b.calendardate,b.MoNote, a.mo from (   \r\n" +
" Select wp.WorkplaceID ,wp.description WP, s2.ReportToSectionid mo  from planning p, workplace wp, SECTION s1, SECTION s2   \r\n" +
"where p.Prodmonth = s1.prodmonth and p.SectionID = s1.SectionID   \r\n" +
"and s1.Prodmonth = s2.Prodmonth and s1.ReportToSectionid = s2.SectionID   \r\n" +
"and p.workplaceid = wp.workplaceID   \r\n" +
"and p.Prodmonth = (select max(Prodmonth) from PLANNING )    \r\n" +
"group by  wp.WorkplaceID,wp.description,s2.ReportToSectionid   \r\n" +
" ) a   \r\n" +
" left outer join   \r\n" +
" (Select workplace, CONVERT(varchar(50), calendardate, 111) calendardate, MoNote from tbl_MoNotes where MoNote is not null  ) b   \r\n" +
" on a.WorkplaceID = b.Workplace ";


            _dbManSub.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManSub.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManSub.ExecuteInstruction();

            DataTable dtSub = _dbManSub.ResultsDataTable;

            DataSet dsRelations = new DataSet();

            dsRelations.Tables.Add(dtMain);
            dsRelations.Tables.Add(dtSub);

            dsRelations.Relations.Clear();

            DataColumn MainColumn = dsRelations.Tables[0].Columns[0];
            DataColumn SubColumn = dsRelations.Tables[1].Columns[0];
            dsRelations.Relations.Add("Detials", MainColumn, SubColumn);

            gcMOWorkplaces.DataSource = dsRelations.Tables[0];
            gcMOWorkplaces.LevelTree.Nodes.Add("Detials", gvWPLevel);

            colworkplaceID.FieldName = "WorkplaceID";
            colWorkplace.FieldName = "WP";
            colLatestMoNote.FieldName = "LatestMoNote";
            //colChecked.FieldName = "MainChecked";

            //colWpSectionid.FieldName = "sec";
            colWPID.FieldName = "workplace";
            colNote.FieldName = "MoNote";
            colDate.FieldName = "calendardate";

            colSection.FieldName = "mo";
        }

        private void Savebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Int32[] selectedRowHandles = gvMoLevel.GetSelectedRows();

            if (Notestxt.Text == "")
            {
                MessageBox.Show("Please Fill in a note before saving");
                return;
            }


            MWDataManager.clsDataAccess _dbManSub = new MWDataManager.clsDataAccess();
            _dbManSub.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];

                string Workplaceid = gvMoLevel.GetRowCellValue(selectedRowHandle, gvMoLevel.Columns["WorkplaceID"]).ToString();

                _dbManSub.SqlStatement = _dbManSub.SqlStatement + "  insert into tbl_MoNotes values (  \r\n" +
                    "  '" + Workplaceid + "',getdate(), '" + TUserInfo.UserID + "' , '" + Notestxt.Text + "'  )   \r\n\r\n";

            }

            _dbManSub.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManSub.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManSub.ExecuteInstruction();

            Notestxt.Text = "";

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Notes saved", Color.CornflowerBlue);
            loadWorkNoteWorkplaces();



            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];

                gvMoLevel.SetMasterRowExpanded(selectedRowHandle, true);
            }
        }

        private void RCRockEngineering_Click(object sender, EventArgs e)
        {

        }

        private void gvMoLevel_DoubleClick(object sender, EventArgs e)
        {
            if (gvMoLevel.FocusedColumn.FieldName == "MainChecked")
            {
                if (gvMoLevel.GetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.FocusedColumn.FieldName).ToString() == "N")
                {
                    gvMoLevel.SetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.FocusedColumn.FieldName, "Y");
                    CheckWPforMain(gvMoLevel.GetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.Columns["sec"]).ToString(), "Y");
                    return;
                }

                if (gvMoLevel.GetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.FocusedColumn).ToString() == "Y")
                {
                    gvMoLevel.SetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.FocusedColumn, "N");
                    CheckWPforMain(gvMoLevel.GetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.Columns["sec"]).ToString(), "N");
                    return;
                }
            }
        }

        private void gvWPLevel_DoubleClick(object sender, EventArgs e)
        {
            GridView currentView = (GridView)sender;

            if (currentView.FocusedColumn.FieldName == "Checked")
            {
                if (currentView.GetRowCellValue(currentView.FocusedRowHandle, currentView.FocusedColumn).ToString() == "N")
                {
                    currentView.SetRowCellValue(currentView.FocusedRowHandle, currentView.FocusedColumn, "Y");
                    return;
                }

                if (currentView.GetRowCellValue(currentView.FocusedRowHandle, currentView.FocusedColumn).ToString() == "Y")
                {
                    currentView.SetRowCellValue(currentView.FocusedRowHandle, currentView.FocusedColumn, "N");
                    return;
                }
            }
        }

        private void CheckWPforMain(string _section, string _Value)
        {

            for (int i = 0; i < gcMOWorkplaces.Views.Count; i++)
            {
                GridView CurrentView = (GridView)gcMOWorkplaces.Views[i];

                if (CurrentView.Name == "gvWPLevel")
                {
                    for (int subRow = 0; subRow < CurrentView.RowCount; subRow++)
                    {
                        if (CurrentView.GetRowCellValue(subRow, CurrentView.Columns["sec"]).ToString() == _section)
                        {
                            CurrentView.SetRowCellValue(subRow, CurrentView.Columns["Checked"], _Value);
                        }
                    }
                }


            }
        }
    }
}
