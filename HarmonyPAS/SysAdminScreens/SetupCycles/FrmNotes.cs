using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using FastReport;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SetupCycles
{
    public partial class FrmNotes : DevExpress.XtraEditors.XtraForm
    {
        //public PropFrm(SysAdminFrm _SysAdminFrm)
        //{
        //    InitializeComponent();
        //    SysAdminFrm = _SysAdminFrm;
        //}

        Report theReport = new Report();

        string openfrm = "";

        string wpNewSamp = "";

        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        public FrmNotes()
        {
            InitializeComponent();
        }

        private void FrmNotes_Load(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
            _dbManDate.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManDate.SqlStatement = " select  max(CalendarDate) dd from PLANNING where CalendarDate < GETDATE() " +
                                        "and  datename(dw,calendardate) = 'Sunday' ";
            _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDate.ResultsTableName = "Table3";
            _dbManDate.ExecuteInstruction();

            DataTable dtdd = _dbManDate.ResultsDataTable;


            dateTimePicker1.Value = Convert.ToDateTime(dtdd.Rows[0]["dd"].ToString());
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(+5);

            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(+7);


            dateTimePicker3.Value = dateTimePicker1.Value;

            

            LoadNoteInfo();

            radioGroup2.SelectedIndex = 1;
            

            openfrm = "Y";
        }


        public void LoadNoteInfo()
        {

            MWDataManager.clsDataAccess _dbManWPST4 = new MWDataManager.clsDataAccess();
            _dbManWPST4.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST4.SqlStatement = " select * from  tbl_Sampling_WorkOrder_Note  a, " +
                                        "(select workplace wp, max(notedate) pp from [tbl_Sampling_WorkOrder_Note] " +
                                        "group by workplace) b where a.workplace = b.wp " +
                                        "and a.notedate = b.pp and thedate = '" + String.Format("{0:yyyy-MM-dd}", dateTimePicker3.Value) + "' and  workplace = '" + WPlabel.Text + "'  ";

            _dbManWPST4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST4.ResultsTableName = "Table3";
            _dbManWPST4.ExecuteInstruction();

            DataTable dtDate = _dbManWPST4.ResultsDataTable;

            NotesTxt.Text = "";

            radioGroup1.SelectedIndex = 0;

            foreach (DataRow dr in dtDate.Rows)
            {
                NotesTxt.Text = dr["Note"].ToString();

                if (dr["latestworder"].ToString() == "1")
                    radioGroup1.SelectedIndex = 1;
                if (dr["latestworder"].ToString() == "2")
                    radioGroup1.SelectedIndex = 2;
            }

            //  ShowBtn_Click(null, null);

            //openfrm = "Y";
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string SampSec = "";
            wpNewSamp = "";
            wpNewSamp = WPlabel.Text + "                          ";

            if (SysSettings.Banner == "Great Noligwa")
            {
                SampSec = "MKEVAL     ";
            }

            if (SysSettings.Banner == "Kopanang")
            {
                SampSec = "KPEVAL     ";
            }

            //if (SysSettings.Banner == "Moab Khotsong")
            //{
                SampSec = "MKEVAL     ";
            //}

            if (SysSettings.Banner == "Mponeng")
            {
                SampSec = "MPEVAL     ";
            }

            if (SysSettings.Banner == "Savuka")
            {
                SampSec = "TTEVAL      ";
            }

            if (SysSettings.Banner == "Tau Tona")
            {
                SampSec = "TTEVAL      ";
            }

            MWDataManager.clsDataAccess _dbManWPST11 = new MWDataManager.clsDataAccess();
            _dbManWPST11.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST11.SqlStatement = " select '" + SampSec + "' section " +

                                        "   " +
                                        "  " +
                                        "  " +
                                        "  ";

            _dbManWPST11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST11.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST11.ResultsTableName = "Table4";
            _dbManWPST11.ExecuteInstruction();

            DataSet dsABS20 = new DataSet();
            dsABS20.Tables.Add(_dbManWPST11.ResultsDataTable);

            theReport.RegisterData(dsABS20);



            MWDataManager.clsDataAccess _dbManWPST1 = new MWDataManager.clsDataAccess();
            _dbManWPST1.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST1.SqlStatement = " select '" + wpNewSamp + "' wp, CONVERT(VARCHAR(11),getdate(),111) date " +

                                        "   " +
                                        "  " +
                                        "  " +
                                        "  ";

            _dbManWPST1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST1.ResultsTableName = "Table1";
            _dbManWPST1.ExecuteInstruction();

            DataSet dsABS2 = new DataSet();
            dsABS2.Tables.Add(_dbManWPST1.ResultsDataTable);

            theReport.RegisterData(dsABS2);



            MWDataManager.clsDataAccess _dbManWPST2 = new MWDataManager.clsDataAccess();
            _dbManWPST2.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST2.SqlStatement = "select *, datediff(day,datesubmitted,getdate()) ss  from tbl_Incidents  " +
                                        "where workplace = '" + WPlabel.Text + "' and disciplinename = 'RMS' and hazard = 'A'  " +
                                        "and datesubmitted = (  " +

                                        "select max(datesubmitted) dd from tbl_Incidents    " +
                                        "where workplace = '" + WPlabel.Text + "' and disciplinename = 'RMS' and hazard = 'A')  ";
            _dbManWPST2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST2.ResultsTableName = "Table2";
            _dbManWPST2.ExecuteInstruction();




            DataSet dsABS1 = new DataSet();
            dsABS1.Tables.Add(_dbManWPST2.ResultsDataTable);

            theReport.RegisterData(dsABS1);

            //////new////////

            // get number
            MWDataManager.clsDataAccess _dbManWPST4a = new MWDataManager.clsDataAccess();
            _dbManWPST4a.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST4a.SqlStatement = "select isnull(max(LatestWOrder), 100000000) a from [tbl_Sampling_WorkOrder_Note]   ";

            _dbManWPST4a.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST4a.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST4a.ResultsTableName = "Table3";
            _dbManWPST4a.ExecuteInstruction();

            DataTable dtDatea = _dbManWPST4a.ResultsDataTable;

            decimal number1 = Convert.ToDecimal(dtDatea.Rows[0]["a"].ToString()) + 1;


            MWDataManager.clsDataAccess _dbManWPST3 = new MWDataManager.clsDataAccess();
            _dbManWPST3.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST3.SqlStatement = "select '" + String.Format("{0:yyyy-MM-dd}", dateTimePicker3.Value) + "'  dd, '" + number1.ToString() + "' "; //convert(varchar(100), min(CalendarDate),121 ) dd, '" + number1.ToString() + "'  from PLANNING where CalendarDate > GETDATE() " +
                                                                                                                                                     // " and  datename(dw,calendardate) = 'Friday'  ";

            _dbManWPST3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST3.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST3.ResultsTableName = "Table3";
            _dbManWPST3.ExecuteInstruction();

            DataTable dtDate = _dbManWPST3.ResultsDataTable;



            DateTxt.Text = "";

            foreach (DataRow dr in dtDate.Rows)
            {
                DateTxt.Text = dr["dd"].ToString();
            }

            string impact = "0";
            if (radioGroup1.SelectedIndex == 0)
                impact = "0";
            if (radioGroup1.SelectedIndex == 1)
                impact = "1";
            if (radioGroup1.SelectedIndex == 2)
                impact = "2";

            if (DateTxt.Text != "")
            {
                // if (NotesTxt.Text != "")
                // {
                //MWDataManager.clsDataAccess _dbManWPST6 = new MWDataManager.clsDataAccess();
                //_dbManWPST6.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbManWPST6.SqlStatement = "delete from tbl_Sampling_WorkOrder_Note where workplace = '" + WPlabel.Text + "' and thedate = '" + DateTxt.Text + "'  ";

                //_dbManWPST6.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManWPST6.queryReturnType = MWDataManager.ReturnType.DataTable;
                //      //_dbManWPST5.ResultsTableName = "Table30";
                //_dbManWPST6.ExecuteInstruction();

                if (openfrm == "Y")
                {



                    MWDataManager.clsDataAccess _dbManWPST5 = new MWDataManager.clsDataAccess();
                    _dbManWPST5.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManWPST5.SqlStatement = "insert into tbl_Sampling_WorkOrder_Note values ('" + WPlabel.Text + "','" + DateTxt.Text + "',  '" + NotesTxt.Text + "', '" + impact + "', getdate(), '" + TUserInfo.UserID + "' )  ";

                    _dbManWPST5.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManWPST5.queryReturnType = MWDataManager.ReturnType.DataTable;
                    //_dbManWPST5.ResultsTableName = "Table30";
                    _dbManWPST5.ExecuteInstruction();





                }
                // }
            }


            MWDataManager.clsDataAccess _dbManWPST4 = new MWDataManager.clsDataAccess();
            _dbManWPST4.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            //_dbManWPST4.SqlStatement = " select *, datepart(week,  getdate()) ww , (select substring(workplaceid,1,1) + convert(varchar(50),((convert(decimal(18,0),substring(workplaceid,2,5))+100000)*100)+datepart(week,  getdate())) from workplace "+
            //                            "where description = '" + WPlabel.Text + "' ) zz from  (select * from [tbl_Sampling_WorkOrder_Note] a, "+ 
            //                            "(select workplace wp, max(notedate) pp from [tbl_Sampling_WorkOrder_Note] "+ 
            //                            "group by workplace) b where a.workplace = b.wp "+
            //                            "and a.notedate = b.pp and thedate = (select  min(CalendarDate) dd from PLANNING where CalendarDate > GETDATE() " + 
            //                           "and  datename(dw,calendardate) = 'Friday')) aa where workplace = '" + WPlabel.Text + "' and thedate = '" + DateTxt.Text + "'  ";

            _dbManWPST4.SqlStatement = " select a.Workplace, TheDate, Note, LatestWOrder LatestWOrder, NoteDate, NoteUser, a.Workplace wp, notedate pp \r\n" +
                                       // " , case when datepart(year,  getdate()) = 2016 then datepart(week,  getdate())-1 else datepart(week,  getdate()) end as ww " + 
                                       ", DATEPART(ISOWK,'" + String.Format("{0:yyyy-MM-dd}", dateTimePicker3.Value) + "') ww \r\n" +

                                       " , zz " +
                                        " from (select '" + WPlabel.Text + "' Workplace, '" + DateTxt.Text + "' TheDate, substring(workplaceid,1,1) +  \r\n" +
                                        " convert(varchar(50),((convert(decimal(18,0),substring(workplaceid,3,5))+100000)*100)+DATEPART(ISOWK,'" + String.Format("{0:yyyy-MM-dd}", dateTimePicker3.Value) + "'))  zz  \r\n " +
                                        " from workplace where description = '" + WPlabel.Text + "') a left outer join  \r\n" +
                                        " ( " +
                                       " select * from (select Note, NoteUser, NoteDate, Workplace, LatestWOrder  from [tbl_Sampling_WorkOrder_Note] a,  \r\n" +
                                       "  (select workplace wp, max(notedate) pp from  \r\n" +
                                        " [tbl_Sampling_WorkOrder_Note] group by workplace) b where a.workplace = b.wp and a.notedate = b.pp and thedate =  \r\n" +
                                        " ('" + String.Format("{0:yyyy-MM-dd}", dateTimePicker3.Value) + "')) aa  \r\n" +
                                        " where workplace = '" + WPlabel.Text + "') b on a.workplace = b.workplace ";


            _dbManWPST4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST4.ResultsTableName = "Table3";
            _dbManWPST4.ExecuteInstruction();

            DataSet dsABS3 = new DataSet();
            dsABS3.Tables.Add(_dbManWPST4.ResultsDataTable);

            theReport.RegisterData(dsABS3);


            MWDataManager.clsDataAccess _dbManWPST5a = new MWDataManager.clsDataAccess();
            _dbManWPST5a.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST5a.SqlStatement = " select dd1, ww, thedate, task, soname, dd, mon, tue, wed, thu, fri, sat, stp, dev, activity, zz from (select *, case when activity <> 1 then stp else dev end as zz from (SELECT convert(varchar(100),[Basic start date],106) dd1, \r\n " +
                                        "datepart(year,getdate())*100+ (datepart(week,getdate())-1) ww,  \r\n " +
                                        "[Basic start date] TheDate, [WMES Task Description] Task, \r\n " +
                                        "soname,  substring(datename(dw,[Basic start date]),1,3) dd, \r\n " +
                                        "case  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Mon'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like 'Bl%' then 'Red'  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Mon'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like '%Bl%' then 'Orange' \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Mon'  \r\n " +
                                        " and [WMES Task Description] <> ''  and [WMES Task Description] not like '%Bl%' and [WMES Task Description] not like 'Bl%' then 'Yellow' \r\n " +
                                        "else '' end as Mon, \r\n " +

                                        "case  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Tue'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like 'Bl%' then 'Red'  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Tue'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like '%Bl%' then 'Orange' \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Tue'  \r\n " +
                                        "and [WMES Task Description] <> ''  and [WMES Task Description] not like '%Bl%' and [WMES Task Description] not like 'Bl%' then 'Yellow' \r\n " +
                                        "else '' end as Tue, \r\n " +

                                        "case  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Wed'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like 'Bl%' then 'Red'  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Wed'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like '%Bl%' then 'Orange' \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Wed'  \r\n " +
                                        " and [WMES Task Description] <> '' and [WMES Task Description] not like '%Bl%' and [WMES Task Description] not like 'Bl%' then 'Yellow' \r\n " +
                                        "else '' end as Wed, \r\n " +

                                        "case  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Thu'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like 'Bl%' then 'Red'  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Thu'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like '%Bl%' then 'Orange' \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Thu'  \r\n " +
                                        " and [WMES Task Description] <> ''  and [WMES Task Description] not like '%Bl%' and [WMES Task Description] not like 'Bl%' then 'Yellow' \r\n " +
                                        "else '' end as Thu, \r\n " +

                                        "case  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Fri'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like 'Bl%' then 'Red'  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Fri'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like '%Bl%' then 'Orange' \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Fri'  \r\n " +
                                        " and [WMES Task Description] <> ''  and [WMES Task Description] not like '%Bl%' and [WMES Task Description] not like 'Bl%' then 'Yellow' \r\n " +
                                        "else '' end as Fri, \r\n " +

                                        "case  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Sat'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like 'Bl%' then 'Red'  \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Sat'  \r\n " +
                                        "and [WMES Plan Output] > 0 and [WMES Task Description] like '%Bl%' then 'Orange' \r\n " +
                                        "when substring(datename(dw,[Basic start date]),1,3) = 'Sat'  \r\n " +
                                        " and [WMES Task Description] <> ''  and [WMES Task Description] not like '%Bl%' and [WMES Task Description] not like 'Bl%' then 'Yellow' \r\n " +
                                        "else '' end as Sat, convert(varchar(10),CAST([Mineware Plan Output] AS int)) stp, convert(varchar(10),convert(decimal(18,1),[Mineware Plan Output])) dev, activity \r\n " +
                                        "  FROM [AGA_ONE].[dbo].[SAP_tbl_rpt_Compliance] a, workplace w  where a.workplace = w.description and  \r\n " +

                                        //"  [SAP Week]= datepart(year,getdate())*100+ (datepart(week,getdate())-1) AND [Operation Indicator] = 0 \r\n " +

                                        "  [SAP Week]= (DATEPART(year,'" + String.Format("{0:yyyy-MM-dd}", dateTimePicker3.Value) + "')*100)+ DATEPART(ISOWK,'" + String.Format("{0:yyyy-MM-dd}", dateTimePicker3.Value) + "') AND [Operation Indicator] = 0 \r\n " +
                                        "  and workplace = '" + WPlabel.Text + "'  \r\n " +
                                        "    and [WMES Record ID] is not null ) a ) a group by dd1, ww, thedate, task, soname, dd, mon, tue, wed, thu, fri, sat, stp, dev, activity, zz order by TheDate  ";

            _dbManWPST5a.SqlStatement = "Select GETDATE() dd1,'' ww,'' thedate,'' task,'' soname,'' dd,'' mon,'' tue,'' wed,'' thu,'' fri,'' sat,'' stp,'' dev,'' activity,'' zz ";

            _dbManWPST5a.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST5a.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST5a.ResultsTableName = "ExecSched";
            _dbManWPST5a.ExecuteInstruction();

            DataSet dsABS4 = new DataSet();
            dsABS4.Tables.Add(_dbManWPST5a.ResultsDataTable);

            theReport.RegisterData(dsABS4);


            theReport.Load(TGlobalItems.ReportsFolder + "\\WorkOrderSamp.frx");

            //theReport.Design();

            pcReport.Clear();
            theReport.Prepare();
            theReport.Preview = pcReport;
            theReport.ShowPrepared();
        }
    }
}