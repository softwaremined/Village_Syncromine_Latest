using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using Mineware.Systems.Global;
using DevExpress.XtraScheduler.Commands;
using Mineware.Systems.GlobalConnect;
using FastReport;
using FastReport.Export.Pdf;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.XtraNavBar.ViewInfo;
using DevExpress.XtraNavBar;

namespace Mineware.Systems.Production.SysAdminScreens.Labour
{
    public partial class ucEarlyMorningReport : Mineware.Systems.Global.ucBaseUserControl
    {
        string Loaded = "N";
        Report theReport = new Report();
        Procedures procs = new Procedures();
        public ucEarlyMorningReport()
        {
            InitializeComponent();
        }

        private void ucEarlyMorningReport_Load(object sender, EventArgs e)
        {
            // get sections
            MWDataManager.clsDataAccess _dbManGetISAfterStart = new MWDataManager.clsDataAccess();
            _dbManGetISAfterStart.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManGetISAfterStart.SqlStatement = "select * from [ValidMOs] order by mo  ";
            //"Prodmonth = (select currentproductionmonth from sysset) and Hierarchicalid = 4 " +
            // "and sectionid in ( "+
            // "select mosectionid from dbo.Cads_MonthPlan where  Prodmonth = (select currentproductionmonth from sysset)) " +
            //  "order by sectionid  ";

            _dbManGetISAfterStart.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManGetISAfterStart.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManGetISAfterStart.ExecuteInstruction();

            DataTable dtSections = _dbManGetISAfterStart.ResultsDataTable;

            DevExpress.XtraNavBar.NavBarItem itema = navBarControl1.Items.Add();
            itema.Caption = "";
            navBarControl1.ActiveGroup.ItemLinks.Add(itema);


            listBox2.Items.Add("Top 20 Early Morning Report (Kgs)");
            listBox2.Items.Add("Top 20 Early Morning Report (cmgt)");


            foreach (DataRow dr in dtSections.Rows)
            {
                //DevExpress.XtraNavBar.NavBarItem item = navBarControl1.Items.Add();
                // item.Caption = dr["mo"].ToString() + ":" + dr["name"].ToString();
                // navBarControl1.ActiveGroup.ItemLinks.Add(item);
                listBox2.Items.Add(dr["mo"].ToString() + ":" + dr["name"].ToString());

            }

            // itema.Visible = false;

            // navBarGroup4.SelectedLinkIndex = 0;
            itema.Visible = false;
            listBox2.SelectedIndex = -1;


            // get Engineers
            MWDataManager.clsDataAccess _dbManGetISAfterStart1 = new MWDataManager.clsDataAccess();
            _dbManGetISAfterStart1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManGetISAfterStart1.SqlStatement = "select * from Dbo.EarlyMorning_Engineers   " +
                                            " " +
                                            " " +
                                            " " +
                                            "order by workgroupcode  ";

            _dbManGetISAfterStart1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManGetISAfterStart1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManGetISAfterStart1.ExecuteInstruction();

            DataTable dtSections1 = _dbManGetISAfterStart1.ResultsDataTable;

            navBarControl1.ActiveGroup = navBarGroup2;

            DevExpress.XtraNavBar.NavBarItem itema1 = navBarControl1.Items.Add();
            itema1.Caption = "";
            navBarControl1.ActiveGroup.ItemLinks.Add(itema1);


            foreach (DataRow dr in dtSections1.Rows)
            {
                //DevExpress.XtraNavBar.NavBarItem itema2 = navBarControl1.Items.Add();
                //itema2.Caption = dr["workgroupcode"].ToString() + ":" + dr["employeename"].ToString();
                //navBarControl1.ActiveGroup.ItemLinks.Add(itema2);

                listBox3.Items.Add(dr["workgroupcode"].ToString() + ":" + dr["employeename"].ToString());

            }

            // itema.Visible = false;

            // navBarGroup4.SelectedLinkIndex = 0;
            itema1.Visible = false;
            listBox3.SelectedIndex = -1;





            // Changelbl.Text = "";

            Loaded = "Y";


            listBox4.Items.Add("4:Finance");
            listBox4.Items.Add("5:Services");
            listBox4.Items.Add("6:Human Resources");
            listBox4.Items.Add("9:Capital");

            listBox4.SelectedIndex = -1;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SysAdminBtn_Click(object sender, EventArgs e)
        {

            DataTable Neil = new DataTable();


            string sec1 = "";


            if ((Changelbl.Text == "Top 20 Early Morning Report (Kgs)") || (Changelbl.Text == "Top 20 Early Morning Report (cmgt)"))
                sec1 = Changelbl.Text;
            else
                sec1 = procs.ExtractBeforeColon(Changelbl.Text);

            string sec2 = "";
            if ((Changelbl.Text == "Top 20 Early Morning Report (Kgs)") || (Changelbl.Text == "Top 20 Early Morning Report (cmgt)"))
            {
                sec2 = Changelbl.Text;
            }
            else
            {
                sec2 = procs.ExtractBeforeColon(Changelbl.Text);

                sec2 = sec2 + "         ";
                sec2 = sec2.Substring(1, 3);
            }

            string done = "N";

            if (sec1 == "Top 20 Early Morning Report (cmgt)")
            {
                done = "Y";

                MWDataManager.clsDataAccess _dbManabc = new MWDataManager.clsDataAccess();
                _dbManabc.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManabc.SqlStatement = " select  sum( UGYFinal-(total-ug)) ugoutFinal from  [dbo].[tbl_GangClock]  " +
                                      " where gangno in ( select OrgUnitDS COLLATE Latin1_General_CI_AS from Top20Panels where title = 'cmgt' )  ";

                _dbManabc.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManabc.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManabc.ResultsTableName = "Lossesabc";  //get table name
                _dbManabc.ExecuteInstruction();



                DataSet ReportDatasetabc = new DataSet();
                ReportDatasetabc.Tables.Add(_dbManabc.ResultsDataTable);
                theReport.RegisterData(ReportDatasetabc);


                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select '" + Changelbl.Text + "' lbl, *, ISNULL  ( total , 0 ) aa1   " +
                                      " , ISNULL  ( ug , 0 ) ug1, ISNULL  ( total , 0 ) total1, ISNULL  ( SqmTotal , 0 ) SqmTotal1, ISNULL  ( adv , 0.0 ) adv1, actnum1-compl var1,   ActNum1- ISNULL  ( total , 0 ) var2,   ISNULL  ( total , 0 ) -  ISNULL  ( ug , 0 )  other " +
                                      " , case when ISNULL  (  ActNum1 , 0 ) > 0 then (ISNULL  ( SqmTotal , 0 ) + ISNULL  ( adv , 0.0 ))/ISNULL  (  ActNum1 , 0 ) else 0 end as sqmm from (select  case when substring(gangno,10,3) in( '110') And substring(gangno,13,1) in( '1')  " +
                                      " then '3) Stoping'  when substring(gangno,10,3) in( '100') then '2) Development'  " +
                                      " when substring(gangno,10,3) in( '001') then '1) Management'  when substring(gangno,10,3) in( '110') " +
                                      " And substring(gangno,13,1) in( '2')  then '4) Cleaning'  when substring(gangno,10,3) in( '102', '121') " +
                                      " then '5) Construction'  when substring(gangno,10,3) in( '112') then '6) Equiping'  " +
                                      " when substring(gangno,10,3) in( '120') then '7) Tramming'  else '8) Other' END as calsaa,  " +
                                      " aa,  gangno a " +
                                      " , GangName xx,  convert(numeric(7,0),actcomp) compl, convert(numeric(7,0),ActNum) ActNum1 " +
                                      " from ( " +


                                      " select OrgUnit gangno, substring(OrgUnit,6,1)+'0'+substring(OrgUnit,7,1) aa, * from dbo.vw_orgunits  " +
                                      " where orgunit in (select distinct(OrgUnitDS) from Top20Panels  where title = 'cmgt') " +
                                        " ) a " +

                                      // " union  select '3) Stoping' calsaa, '" + sec2 + "' aa, 'No Stp Gang Assigned' a, '' xx, convert(numeric(7,0),0) compl , convert(numeric(7,0),0) Actnum  " +
                                      // " union  select '2) Development' calsaa, '" + sec2 + "' aa, 'No Dev Gang Assigned' a, '' xx, convert(numeric(7,0),0) compl , convert(numeric(7,0),0) Actnum) a " +


                                      ") a left outer join " +


                                      " (select  GangNo gang, convert(numeric(7,0), UG) UG, convert(numeric(7,0),Total) Total, convert(numeric(7,0), UGOut) UGOut,  convert(numeric(7,0), UGYFinal-(total-ug)) ugoutFinal from  [dbo].[tbl_GangClock]  " +
                                      " where GangNo in (select distinct(OrgUnitDS) from Top20Panels  where title = 'cmgt' " +
                                        " ) ) b  on a.a = b.gang " +

                                      " left outer join  " +
                                      " (select org, sum(SqmTotal) SqmTotal, sum(adv) adv from " +
                                      " ( select  case when OrgUnitDS <> '' then  " +
                                      " OrgUnitDS when OrgUnitDS = ''  " +
                                      " and Activity <> 1 then 'No Stp Gang Assigned'   " +
                                      " when OrgUnitDS = ''  " +
                                      " and Activity = 1 then 'No Dev Gang Assigned'  " +
                                      " end as org, SqmTotal,  " +
                                      " case when activity = 1 then Adv else 0  end as adv  " +
                                      " from PlanMonth p , Section s , Section s1  " +
                                      " where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and  " +
                                      " s.ReportToSectionid = s1.sectionid and s.prodmonth = s1.prodmonth and   " +
                                      " OrgUnitDS IN (select distinct(OrgUnitDS) COLLATE Latin1_General_CI_AS from Top20Panels where title = 'cmgt' ) and p.Prodmonth =  " +
                                      "(select currentproductionmonth from sysset)) c " +
                                      //" (select max(p.prodmonth) pm from tbl_Planning p, tbl_Section s , tbl_Section s1  " +
                                      //" where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and   " +
                                      //" s.ReportToSectionid = s1.sectionid and s.prodmonth = s1.prodmonth and  " +
                                      //" calendardate = CONVERT(VARCHAR(10),GETDATE()-100,111) and s1.ReportToSectionid = '" + sec1 + "')) c  " +
                                      " group by org) c  on a.a = c.org  order by calsaa , a  ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Losses";  //get table name
                _dbMan.ExecuteInstruction();



                DataSet ReportDataset = new DataSet();
                ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                theReport.RegisterData(ReportDataset);

            }

            if (done == "N")
            {

                MWDataManager.clsDataAccess _dbManabc = new MWDataManager.clsDataAccess();
                _dbManabc.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManabc.SqlStatement = " select sum(ugoutFinal) ugoutFinal from (select  case when (UG-ugout)+(ugy-ugouty) > 0 then (UG-ugout)+(ugy-ugouty) else 0 end as ugoutFinal from  tbl_GangClock  " +
                                      " where gangno in ( select distinct(orgunit) COLLATE Latin1_General_CI_AS from  dbo.vw_orgunitsCombined where substring(orgunit,13,1) <> '11' and    substring(orgunit,6,1)+'0'+substring(orgunit,7,1) = '" + sec2 + "' and substring(orgunit,5,1) = '1' and substring(orgunit,8,2) <> 'SH' and substring(orgunit,8,2) <> 'MJ' )) a  ";

                _dbManabc.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManabc.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManabc.ResultsTableName = "Lossesabc";  //get table name
                _dbManabc.ExecuteInstruction();



                DataSet ReportDatasetabc = new DataSet();
                ReportDatasetabc.Tables.Add(_dbManabc.ResultsDataTable);
                theReport.RegisterData(ReportDatasetabc);


                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select '" + Changelbl.Text + "' lbl, *, ISNULL  ( total , 0 ) aa1   " +
                                      " , ISNULL  ( ug , 0 ) ug1, ISNULL  ( total , 0 ) total1, ISNULL  ( SqmTotal , 0 ) SqmTotal1, ISNULL  ( adv , 0.0 ) adv1, ISNULL  (actnum1,0)-ISNULL  (compl,0) var1,   ActNum1- ISNULL  ( total , 0 ) var2,   ISNULL  ( total , 0 ) -  ISNULL  ( ug , 0 )  other " +
                                      " , case when ISNULL  (  ActNum1 , 0 ) > 0 then (ISNULL  ( SqmTotal , 0 ) + ISNULL  ( adv , 0.0 ))/ISNULL  (  ActNum1 , 0 ) else 0 end as sqmm from (select  case when substring(gangno,10,3) in( '110') And substring(gangno,13,1) in( '1')  " +
                                      " then '3) Stoping'  when substring(gangno,10,3) in( '100') then '2) Development'  " +
                                      " when substring(gangno,10,3) in( '001') then '1) Management'  when substring(gangno,10,3) in( '110') " +
                                      " And substring(gangno,13,1) in( '2')  then '4) Cleaning'  when substring(gangno,10,3) in( '102', '121') " +
                                      " then '5) Construction'  when substring(gangno,10,3) in( '112') then '6) Equiping'  " +
                                      " when substring(gangno,10,3) in( '120') then '7) Tramming'  else '8) Other' END as calsaa,  " +
                                      " aa,  gangno a " +
                                      " , GangName xx,  convert(numeric(7,0),actcomp) compl, convert(numeric(7,0),ActNum) ActNum1 " +
                                      " from ( " +


                                      " select OrgUnit gangno, substring(OrgUnit,6,1)+'0'+substring(OrgUnit,7,1) aa, * from dbo.vw_orgunitsCombined  " +
                                      " where substring(OrgUnit,13,1) <> '11' " +
                                      " and substring(OrgUnit,6,1)+'0'+substring(OrgUnit,7,1) = '" + sec2 + "' and substring(OrgUnit,5,1) = '1' and substring(OrgUnit,8,2) <> 'SH' and substring(OrgUnit,8,2) <> 'MJ') a " +

                                      " union  select '3) Stoping' calsaa, '" + sec2 + "' aa, 'No Stp Gang Assigned' a, '' xx, convert(numeric(7,0),0) compl , convert(numeric(7,0),0) Actnum  " +
                                      " union  select '2) Development' calsaa, '" + sec2 + "' aa, 'No Dev Gang Assigned' a, '' xx, convert(numeric(7,0),0) compl , convert(numeric(7,0),0) Actnum) a " +


                                      " left outer join " +
                                      //" (select gang, SUM(UG) UG, SUM(Total) Total from   " +
                                      // " (select empno, gang, MAX(UG) UG, MAX(Total) Total from  " +
                                      // " (  select case when reader_description like '%ndergroun%' Then 1 when reader_code like '%UG%' Then 1 else 0 end as UG, 1 Total, " +
                                      //   " emp_empno empno, gang_number gang   " +
                                      //   " from AFZAVRCLSQL04.SKYCOM.dbo.CLOCKS  " + 
                                      //   " where substring(gang_number,13,1) <> '11' and   " +
                                      //  " substring(gang_number,6,1)+'0'+substring(gang_number,7,1) = '" + sec2 + "'  " +
                                      //   " and CONVERT(VARCHAR(10),clock_time,111)  = CONVERT(VARCHAR(10),GETDATE(),111)) a group by empno, gang)  " +
                                      //   " a group by gang) b  on a.a = b.gang   " +

                                      " (select  GangNo COLLATE Latin1_General_CI_AS gang, convert(numeric(7,0), UG) UG, convert(numeric(7,0),Total) Total, convert(numeric(7,0), UGOut) UGOut,  case when (UG-ugout)+(ugy-ugouty) > 0 then convert(numeric(7,0),(UG-ugout)+(ugy-ugouty)) else 0 end as ugoutFinal from  [tbl_GangClock]  " +
                                      " where substring(GangNo,13,1) <> '11' and    substring(GangNo,6,1)+'0'+substring(GangNo,7,1) = '" + sec2 + "' ) b  on a.a = b.gang " +

                                      " left outer join  " +
                                      " (select org, sum(SqmTotal) SqmTotal, sum(adv) adv from " +
                                      " ( select  case when SUBSTRING(OrgUnitDS,1,15) <> '' then  " +
                                      " OrgUnitDS  when SUBSTRING(OrgUnitDS,1,15) = ''  " +
                                      " and Activity <> 1 then 'No Stp Gang Assigned'   " +
                                      " when OrgUnitDS = ''  " +
                                      " and Activity = 1 then 'No Dev Gang Assigned'  " +
                                      " end as org, SqmTotal,  " +
                                      " case when activity = 1 then Adv else 0  end as adv  " +
                                      " from tbl_PlanMonth p , tbl_Section s , tbl_Section s1  " +
                                      " where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and  " +
                                      " s.ReportToSectionid = s1.sectionid and s.prodmonth = s1.prodmonth and   " +
                                      " s1.ReportToSectionid = '" + sec1 + "' and p.Prodmonth =  " +
                                      " (select max(p.prodmonth) pm from tbl_Planning p, tbl_Section s , tbl_Section s1  " +
                                      " where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and   " +
                                      " s.ReportToSectionid = s1.sectionid and s.prodmonth = s1.prodmonth and  " +
                                      " calendardate >= CONVERT(VARCHAR(10),GETDATE()-100,111) and s1.ReportToSectionid = '" + sec1 + "' and calendardate <= CONVERT(VARCHAR(10),GETDATE(),111) )) c  " +
                                      " group by org) c  on a.a = c.org  " +


                                      " union  " +
                                       "select '" + Changelbl.Text + "' lbl, '9) Gangs Without People' calsaa  , '" + sec2 + "' aa,  gang COLLATE Latin1_General_CI_AS a, gangdesc  COLLATE Latin1_General_CI_AS xx,  " +
                                       "Comp compl, 0 ActNum1, gang , 0 UG, 0 Total, 0 UGOut,  " +
                                       "0 ugoutfinal, null org, null sqmtotal, null adv, 0 aa1, 0 ug1, 0 total1, 0 sqmtotal1, 0 adv1,  " +
                                       "Comp*-1 var1, 0 var2, 0 other, 0 sqmm    " +

                                       "from tbl_GangComp    " +
                                       " where   " +
                                       "substring(gang,13,1) <> '11' " +
                                      " and substring(gang,6,1)+'0'+substring(gang,7,1) = '" + sec2 + "' and substring(gang,5,1) = '1' and substring(gang,8,2) <> 'SH' and substring(gang,8,2) <> 'MJ'  " +
                                       // "substring(Code,13,1) <> '11'  and substring(Code,6,1)+'0'+substring(Code,7,1) = '204'  " +
                                       //  " and substring(Code,5,1) = '1' and substring(Code,8,2) <> 'SH' and substring(Code,8,2) <> 'MJ'  and   " +
                                       " and gang not in (select orgunit COLLATE Latin1_General_CI_AS from vw_orgunitsCombined)  " +
                                       " and substring(gang,1,4) in     " +
                                       " (select  " +
                                       " substring(orgunit,1,4)  COLLATE Latin1_General_CI_AS from vw_orgunitsCombined where  substring(orgunit,1,1) = '2') and Comp > 0  " +


                                       " order by calsaa , a   ";



                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Losses";  //get table name
                _dbMan.ExecuteInstruction();



                DataSet ReportDataset = new DataSet();
                ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                theReport.RegisterData(ReportDataset);

            }

            theReport.Load("EarlyMorningShiftRprt.frx");

            //theReport.Design();

            pcReport.Clear();
            theReport.Prepare();
            theReport.Preview = pcReport;
            theReport.ShowPrepared();

            Application.Idle +=
                new System.EventHandler(this.Application_Idle);
        }


        private void Application_Idle(Object sender, EventArgs e)
        {
            if (theReport.GetParameterValue("Param1") != null)
            {
                ReportLbl.Text = theReport.GetParameterValue("Param1").ToString();
            }

            if (theReport.GetParameterValue("Param3") != null)
            {
                ClickLbl.Text = theReport.GetParameterValue("Param3").ToString();
            }

        }

        private void navBarControl1_CustomDrawLink(object sender, DevExpress.XtraNavBar.ViewInfo.CustomDrawNavBarElementEventArgs e)
        {
            if (e.ObjectInfo.State == DevExpress.Utils.Drawing.ObjectState.Pressed)
            {
                // if a link is not hot tracked or pressed it is drawn in the normal way
                if (e.ObjectInfo.State == ObjectState.Hot || e.ObjectInfo.State == ObjectState.Pressed)
                {
                    Rectangle rect = e.RealBounds;
                    rect.Inflate(-1, -1);
                    LinearGradientBrush brush;
                    Rectangle imageRect;
                    Rectangle textRect;
                    StringFormat textFormat = new StringFormat();
                    textFormat.LineAlignment = StringAlignment.Center;

                    // identifying the painted link
                    NavLinkInfoArgs linkInfo = e.ObjectInfo as NavLinkInfoArgs;
                    if (linkInfo.Link.Group.GroupCaptionUseImage == NavBarImage.Large)
                    {
                        // adjusting the rectangles for the image and text and specifying the text's alignment
                        // if a large image is displayed within a link
                        imageRect = rect;
                        imageRect.Inflate(-(rect.Width - 32) / 2, -2);
                        textRect = rect;
                        int textHeight = Convert.ToInt16(e.Graphics.MeasureString(e.Caption,
                          e.Appearance.Font).Height);
                        textFormat.Alignment = StringAlignment.Center;
                    }
                    else
                    {
                        // adjusting the rectangles for the image and text and specifying the text's alignment
                        // if a small image is displayed within a link
                        imageRect = rect;
                        imageRect.Width = 16;
                        imageRect.Offset(2, 2);
                        textRect = new Rectangle(rect.Left + 23, rect.Top, rect.Width - 23, rect.Height);
                        textFormat.Alignment = StringAlignment.Near;
                    }

                    // creating different brushes for the hot tracked and pressed states of a link
                    if (e.ObjectInfo.State == ObjectState.Hot)
                    {
                        brush = new LinearGradientBrush(rect, Color.Orange, Color.PeachPuff,
                          LinearGradientMode.Horizontal);
                        // shifting image and text up when a link is hot tracked
                        imageRect.Offset(0, -1);
                        textRect.Offset(0, -1);
                    }
                    else
                        brush = new LinearGradientBrush(rect, Color.YellowGreen, Color.YellowGreen,
                          LinearGradientMode.Horizontal);

                    // painting borders
                    e.Graphics.FillRectangle(new SolidBrush(Color.PeachPuff), e.RealBounds);
                    // painting background
                    e.Graphics.FillRectangle(brush, rect);
                    // painting image
                    if (e.Image != null)
                        e.Graphics.DrawImageUnscaled(e.Image, imageRect);
                    // painting caption
                    e.Graphics.DrawString(e.Caption, e.Appearance.Font, new SolidBrush(Color.Black),
                      textRect, textFormat);
                    // prohibiting default link painting
                    e.Handled = true;
                }

            }
        }

        private void navBarControl1_SelectedLinkChanged(object sender, NavBarSelectedLinkChangedEventArgs e)
        {
            navBarControl1.SelectedLink.Item.AppearancePressed.ForeColor = Color.Red;
            Changelbl.Text = navBarControl1.SelectedLink.Caption;
        }

        private void Changelbl_TextChanged(object sender, EventArgs e)
        {
            if (Changelbl.Text != "")
            {
                if (Loaded == "Y")
                {
                    this.Cursor = Cursors.WaitCursor;
                    SysAdminBtn_Click(null, null);
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void pcReport_Click(object sender, EventArgs e)
        {
            if (theReport.GetParameterValue("Param1") != null)
                ReportLbl.Text = theReport.GetParameterValue("Param1").ToString();
        }

        private void ClickLbl_TextChanged(object sender, EventArgs e)
        {
            //frmEarlyReportDetail MorningForm = (frmEarlyReportDetail)IsBookingFormAlreadyOpen(typeof(frmEarlyReportDetail));
            //if (MorningForm == null)
            //{
            //    MorningForm = new frmEarlyReportDetail(this);
            //    MorningForm.Orglbl.Text = ClickLbl.Text;
            //    MorningForm.Text = "Early Morning Shift Report";
            //    MorningForm.Show();
            //}
            //else
            //{
            //    MorningForm.WindowState = FormWindowState.Maximized;
            //    MorningForm.Select();
            //}
            //this.Cursor = Cursors.Default;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                Changelbl.Text = listBox2.SelectedItem.ToString();
                listBox3.SelectedIndex = -1;
                listBox4.SelectedIndex = -1;

                Changelbl.Text = "";
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex >= 0)
            {
                Changelabel2.Text = "";
                Changelabel2.Text = listBox3.SelectedItem.ToString();
                listBox2.SelectedIndex = -1;
                listBox4.SelectedIndex = -1;
            }
        }

        private void Changelabel2_TextChanged(object sender, EventArgs e)
        {
            if (Loaded == "Y")
            {
                if (Changelabel2.Text != "")
                {
                    this.Cursor = Cursors.WaitCursor;
                    LoadEng();
                    this.Cursor = Cursors.Default;
                }
            }
        }


        void LoadEng()
        {
            string sec2 = "";

            sec2 = procs.ExtractBeforeColon(Changelabel2.Text);
            MWDataManager.clsDataAccess _dbManabc = new MWDataManager.clsDataAccess();

            if (SysSettings.Banner == "Tau Tona")
            {
                sec2 = sec2.Substring(0, 5);

                sec2 = sec2 + '%';

                _dbManabc.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManabc.SqlStatement = "  select  sum( UGYFinal) ugoutFinal from  [dbo].[tbl_GangClock]   " +
                                        "  where gangno in ( " +
                                        " select gangno from employeeall where workgroupcode like '" + sec2 + "' and substring(gangno,5,1) = '3') ";


                _dbManabc.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManabc.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManabc.ResultsTableName = "Lossesabc";  //get table name
                _dbManabc.ExecuteInstruction();
            }
            else
            {

                // MWDataManager.clsDataAccess _dbManabc = new MWDataManager.clsDataAccess();
                _dbManabc.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManabc.SqlStatement = "  select  sum( UGYFinal) ugoutFinal from  [dbo].[tbl_GangClock]   " +
                                        "  where substring(gangno,5,1) = '3' and gangno like ( " +
                                        " select top(1) SUBSTRING(gangno,1,7) + '%' aa from dbo.EarlyMorning_Engineers where workgroupcode = '" + sec2 + "')  and GangNo in (select distinct(GangNo)from employeeall where substring(GangNo,5,1) = '3' ) ";


                _dbManabc.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManabc.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManabc.ResultsTableName = "Lossesabc";  //get table name
                _dbManabc.ExecuteInstruction();


            }



            DataSet ReportDatasetabc = new DataSet();
            ReportDatasetabc.Tables.Add(_dbManabc.ResultsDataTable);
            theReport.RegisterData(ReportDatasetabc);

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();

            if ((SysSettings.Banner == "Tau Tona"))
            {
                // MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select '" + Changelabel2.Text + "' lbl, *, ISNULL  ( total , 0 ) aa1   " +
                                      " , ISNULL  ( ug , 0 ) ug1, ISNULL  ( total , 0 ) total1, ISNULL  ( SqmTotal , 0 ) SqmTotal1, ISNULL  ( adv , 0.0 ) adv1, actnum1-compl var1,   ActNum1- ISNULL  ( total , 0 ) var2,   ISNULL  ( total , 0 ) -  ISNULL  ( ug , 0 )  other " +
                                      " , case when ISNULL  (  ActNum1 , 0 ) > 0 then (ISNULL  ( SqmTotal , 0 ) + ISNULL  ( adv , 0.0 ))/ISNULL  (  ActNum1 , 0 ) else 0 end as sqmm from (select  " +
                                      " case when substring(gangno,6,7) in( '1200001') then '  Management' " +
                                      "   when substring(gangno,6,7) in( '1200002') then ' Eng Sup'  " +
                                      "   when substring(gangno,10,3) in( '168') then ' Lamproom'  " +
                                      "  when substring(gangno,11,1) in( '3') then ' Shaft'  " +

                                      "  when substring(gangno,6,7) in( '1100001') then '  Management' " +
                                      "  when substring(gangno,6,7) in( '1300001') then '  Management'" +
                                      "  when substring(gangno,6,7) in( '1400001') then '  Management' " +
                                      "  when substring(gangno,6,7) in( '1500001') then '  Management'" +
                                      "  when substring(gangno,6,7) in( '1600001') then '  Management'" +
                                      "  when substring(gangno,6,7) in( '1700001') then '  Management'" +

                                      "  when substring(gangno,6,7) in( '1100002') then ' Eng Sup'" +
                                      "   when substring(gangno,6,7) in( '1300002') then ' Eng Sup' " +
                                      "  when substring(gangno,6,7) in( '1400002') then ' Eng Sup'" +
                                      "  when substring(gangno,6,7) in( '1500002') then ' Eng Sup' " +
                                      "   when substring(gangno,6,7) in( '1600002') then ' Eng Sup'" +
                                      "   when substring(gangno,6,7) in( '1700002') then ' Eng Sup' " +


                                      "  when substring(gangno,8,3) in( '411') then ' Backfill' " +

                                      "  when substring(gangno,9,3) in( '116') then ' Boxfronts, Battery Bays'" +

                                      " else ' other'  END as calsaa,  " +
                                      " aa,  gangno a " +
                                      " , GangName xx,  convert(numeric(7,0),actcomp) compl, convert(numeric(7,0),ActNum) ActNum1 " +
                                      " from ( " +


                                      " select OrgUnit gangno, substring(OrgUnit,6,1)+'0'+substring(OrgUnit,7,1) aa, * from dbo.vw_orgunits  " +
                                      " where OrgUnit in (select gangno from employeeall where workgroupcode like '" + sec2 + "' and substring(gangno,5,1) = '3') " +
                                      " ) a " +

                                      " union  select '3) Stoping' calsaa, '" + sec2 + "' aa, 'No Stp Gang Assigned' a, '' xx, convert(numeric(7,0),0) compl , convert(numeric(7,0),0) Actnum  " +
                                      " union  select '2) Development' calsaa, '" + sec2 + "' aa, 'No Dev Gang Assigned' a, '' xx, convert(numeric(7,0),0) compl , convert(numeric(7,0),0) Actnum) a " +


                                      " left outer join " +
                                      //" (select gang, SUM(UG) UG, SUM(Total) Total from   " +
                                      // " (select empno, gang, MAX(UG) UG, MAX(Total) Total from  " +
                                      // " (  select case when reader_description like '%ndergroun%' Then 1 when reader_code like '%UG%' Then 1 else 0 end as UG, 1 Total, " +
                                      //   " emp_empno empno, gang_number gang   " +
                                      //   " from AFZAVRCLSQL04.SKYCOM.dbo.CLOCKS  " + 
                                      //   " where substring(gang_number,13,1) <> '11' and   " +
                                      //  " substring(gang_number,6,1)+'0'+substring(gang_number,7,1) = '" + sec2 + "'  " +
                                      //   " and CONVERT(VARCHAR(10),clock_time,111)  = CONVERT(VARCHAR(10),GETDATE(),111)) a group by empno, gang)  " +
                                      //   " a group by gang) b  on a.a = b.gang   " +

                                      " (select  GangNo gang, convert(numeric(7,0), UG) UG, convert(numeric(7,0),Total) Total, convert(numeric(7,0), UGOut) UGOut,  convert(numeric(7,0), UGYFinal) ugoutFinal from  [dbo].[tbl_GangClock]  " +
                                      " where GangNo in (select gangno from employeeall where workgroupcode like '" + sec2 + "' and substring(gangno,5,1) = '3') ) b  on a.a = b.gang " +

                                      " left outer join  " +
                                      " (select org, sum(SqmTotal) SqmTotal, sum(adv) adv from " +
                                      " ( select  case when OrgUnitDS,1,15 <> '' then  " +
                                      " OrgUnitDS  when OrgUnitDS = ''  " +
                                      " and Activity <> 1 then 'No Stp Gang Assigned'   " +
                                      " when OrgUnitDS = ''  " +
                                      " and Activity = 1 then 'No Dev Gang Assigned'  " +
                                      " end as org, SqmTotal,  " +
                                      " case when activity = 1 then Adv else 0  end as adv  " +
                                      " from tbl_PlanMonth p , tbl_Section s , tbl_Section s1  " +
                                      " where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and  " +
                                      " s.ReportToSectionid = s1.sectionid and s.prodmonth = s1.prodmonth and   " +
                                      " s1.ReportToSectionid = '" + sec2 + "' and p.Prodmonth =  " +
                                      " (select max(p.prodmonth) pm from tbl_Planning p, tbl_Section s , tbl_Section s1  " +
                                      " where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and   " +
                                      " s.ReportToSectionid = s1.sectionid and s.prodmonth = s1.prodmonth and  " +
                                      " calendardate = CONVERT(VARCHAR(10),GETDATE()-100,111) and s1.ReportToSectionid = '" + sec2 + "')) c  " +
                                      " group by org) c  on a.a = c.org where  calsaa not in ('2) Development','3) Stoping')   order by calsaa , a  ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Losses";  //get table name
                _dbMan.ExecuteInstruction();
            }

            else
            {

                // MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select '" + Changelabel2.Text + "' lbl, *, ISNULL  ( total , 0 ) aa1   " +
                                      " , ISNULL  ( ug , 0 ) ug1, ISNULL  ( total , 0 ) total1, ISNULL  ( SqmTotal , 0 ) SqmTotal1, ISNULL  ( adv , 0.0 ) adv1, actnum1-compl var1,   ActNum1- ISNULL  ( total , 0 ) var2,   ISNULL  ( total , 0 ) -  ISNULL  ( ug , 0 )  other " +
                                      " , case when ISNULL  (  ActNum1 , 0 ) > 0 then (ISNULL  ( SqmTotal , 0 ) + ISNULL  ( adv , 0.0 ))/ISNULL  (  ActNum1 , 0 ) else 0 end as sqmm from (select  " +
                                      " case when substring(gangno,6,7) in( '1200001') then '  Management' " +
                                      "   when substring(gangno,6,7) in( '1200002') then ' Eng Sup'  " +
                                      "   when substring(gangno,10,3) in( '168') then ' Lamproom'  " +
                                      "  when substring(gangno,11,1) in( '3') then ' Shaft'  " +

                                      "  when substring(gangno,6,7) in( '1100001') then '  Management' " +
                                      "  when substring(gangno,6,7) in( '1300001') then '  Management'" +
                                      "  when substring(gangno,6,7) in( '1400001') then '  Management' " +
                                      "  when substring(gangno,6,7) in( '1500001') then '  Management'" +
                                      "  when substring(gangno,6,7) in( '1600001') then '  Management'" +
                                      "  when substring(gangno,6,7) in( '1700001') then '  Management'" +

                                      "  when substring(gangno,6,7) in( '1100002') then ' Eng Sup'" +
                                      "   when substring(gangno,6,7) in( '1300002') then ' Eng Sup' " +
                                      "  when substring(gangno,6,7) in( '1400002') then ' Eng Sup'" +
                                      "  when substring(gangno,6,7) in( '1500002') then ' Eng Sup' " +
                                      "   when substring(gangno,6,7) in( '1600002') then ' Eng Sup'" +
                                      "   when substring(gangno,6,7) in( '1700002') then ' Eng Sup' " +


                                      "  when substring(gangno,8,3) in( '411') then ' Backfill' " +

                                      "  when substring(gangno,9,3) in( '116') then ' Boxfronts, Battery Bays'" +

                                      " else ' other'  END as calsaa,  " +
                                      " aa,  gangno a " +
                                      " , GangName xx,  convert(numeric(7,0),actcomp) compl, convert(numeric(7,0),ActNum) ActNum1 " +
                                      " from ( " +


                                      " select OrgUnit gangno, substring(OrgUnit,6,1)+'0'+substring(OrgUnit,7,1) aa, * from dbo.vw_orgunits  " +
                                      " where substring(OrgUnit,5,1) = '3' and OrgUnit like (select top(1) SUBSTRING(gangno,1,7) + '%' aa from dbo.EarlyMorning_Engineers where workgroupcode = '" + sec2 + "') " +
                                      " ) a " +

                                      " union  select '3) Stoping' calsaa, '" + sec2 + "' aa, 'No Stp Gang Assigned' a, '' xx, convert(numeric(7,0),0) compl , convert(numeric(7,0),0) Actnum  " +
                                      " union  select '2) Development' calsaa, '" + sec2 + "' aa, 'No Dev Gang Assigned' a, '' xx, convert(numeric(7,0),0) compl , convert(numeric(7,0),0) Actnum) a " +


                                      " left outer join " +
                                      //" (select gang, SUM(UG) UG, SUM(Total) Total from   " +
                                      // " (select empno, gang, MAX(UG) UG, MAX(Total) Total from  " +
                                      // " (  select case when reader_description like '%ndergroun%' Then 1 when reader_code like '%UG%' Then 1 else 0 end as UG, 1 Total, " +
                                      //   " emp_empno empno, gang_number gang   " +
                                      //   " from AFZAVRCLSQL04.SKYCOM.dbo.CLOCKS  " + 
                                      //   " where substring(gang_number,13,1) <> '11' and   " +
                                      //  " substring(gang_number,6,1)+'0'+substring(gang_number,7,1) = '" + sec2 + "'  " +
                                      //   " and CONVERT(VARCHAR(10),clock_time,111)  = CONVERT(VARCHAR(10),GETDATE(),111)) a group by empno, gang)  " +
                                      //   " a group by gang) b  on a.a = b.gang   " +

                                      " (select  GangNo gang, convert(numeric(7,0), UG) UG, convert(numeric(7,0),Total) Total, convert(numeric(7,0), UGOut) UGOut,  convert(numeric(7,0), UGYFinal) ugoutFinal from  [dbo].[tbl_GangClock]  " +
                                      " where GangNo like (select  top(1) SUBSTRING(gangno,1,7) + '%' aa from dbo.EarlyMorning_Engineers where workgroupcode = '" + sec2 + "') ) b  on a.a = b.gang " +

                                      " left outer join  " +
                                      " (select org, sum(SqmTotal) SqmTotal, sum(adv) adv from " +
                                      " ( select  case when SUBSTRING(OrgUnitDS,1,15) <> '' then  " +
                                      " SUBSTRING(OrgUnitDS,1,15)  when SUBSTRING(OrgUnitDS,1,15) = ''  " +
                                      " and Activity <> 1 then 'No Stp Gang Assigned'   " +
                                      " when SUBSTRING(OrgUnitDS,1,15) = ''  " +
                                      " and Activity = 1 then 'No Dev Gang Assigned'  " +
                                      " end as org, SqmTotal,  " +
                                      " case when activity = 1 then Adv else 0  end as adv  " +
                                      " from tbl_PlanMonth p , tbl_Section s , tbl_Section s1  " +
                                      " where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and  " +
                                      " s.ReportToSectionid = s1.sectionid and s.prodmonth = s1.prodmonth and   " +
                                      " s1.ReportToSectionid = '" + sec2 + "' and p.Prodmonth =  " +
                                      " (select max(p.prodmonth) pm from tbl_Planning p, tbl_Section s , tbl_Section s1  " +
                                      " where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and   " +
                                      " s.ReportToSectionid = s1.sectionid and s.prodmonth = s1.prodmonth and  " +
                                      " calendardate = CONVERT(VARCHAR(10),GETDATE()-100,111) and s1.ReportToSectionid = '" + sec2 + "')) c  " +
                                      " group by org) c  on a.a = c.org where  calsaa not in ('2) Development','3) Stoping')   order by calsaa , a  ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Losses";  //get table name
                _dbMan.ExecuteInstruction();



            }


            DataSet ReportDataset = new DataSet();
            ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
            theReport.RegisterData(ReportDataset);




            theReport.Load("EarlyMorningShiftRprt.frx");

            //theReport.Design();

            pcReport.Clear();
            theReport.Prepare();
            theReport.Preview = pcReport;
            theReport.ShowPrepared();

            Application.Idle +=
                new System.EventHandler(this.Application_Idle);

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex >= 0)
            {
                ChangeLbl3.Text = "";
                ChangeLbl3.Text = listBox4.SelectedItem.ToString();
                listBox2.SelectedIndex = -1;
                listBox3.SelectedIndex = -1;
            }
        }


        void Loadother()
        {
            string sec3 = "";

            sec3 = procs.ExtractBeforeColon(ChangeLbl3.Text);


            MWDataManager.clsDataAccess _dbManabc = new MWDataManager.clsDataAccess();
            _dbManabc.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManabc.SqlStatement = "  select  sum( UGYFinal) ugoutFinal from  [dbo].[tbl_GangClock]   " +
                                    "  where gangno in ( " +
                                    " select distinct(GangNo) from dbo.EmployeeAll where SUBSTRING(GangNo, 5,1) not in ('1','3') and SUBSTRING(GangNo, 5,1) = '" + sec3 + "') ";


            _dbManabc.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManabc.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManabc.ResultsTableName = "Lossesabc";  //get table name
            _dbManabc.ExecuteInstruction();



            DataSet ReportDatasetabc = new DataSet();
            ReportDatasetabc.Tables.Add(_dbManabc.ResultsDataTable);
            theReport.RegisterData(ReportDatasetabc);


            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " select '" + Changelabel2.Text + "' lbl, *, ISNULL  ( total , 0 ) aa1   " +
                                  " , ISNULL  ( ug , 0 ) ug1, ISNULL  ( total , 0 ) total1, ISNULL  ( SqmTotal , 0 ) SqmTotal1, ISNULL  ( adv , 0.0 ) adv1, actnum1-compl var1,   ActNum1- ISNULL  ( total , 0 ) var2,   ISNULL  ( total , 0 ) -  ISNULL  ( ug , 0 )  other " +
                                  " , case when ISNULL  (  ActNum1 , 0 ) > 0 then (ISNULL  ( SqmTotal , 0 ) + ISNULL  ( adv , 0.0 ))/ISNULL  (  ActNum1 , 0 ) else 0 end as sqmm from (select  " +
                                  " case when substring(gangno,6,7) in( '1200001') then '  Management' " +
                                  "   when substring(gangno,6,7) in( '1200002') then ' Eng Sup'  " +
                                  "   when substring(gangno,10,3) in( '168') then ' Lamproom'  " +
                                  "  when substring(gangno,11,1) in( '3') then ' Shaft'  " +

                                  "  when substring(gangno,6,7) in( '1100001') then '  Management' " +
                                  "  when substring(gangno,6,7) in( '1300001') then '  Management'" +
                                  "  when substring(gangno,6,7) in( '1400001') then '  Management' " +
                                  "  when substring(gangno,6,7) in( '1500001') then '  Management'" +
                                  "  when substring(gangno,6,7) in( '1600001') then '  Management'" +
                                  "  when substring(gangno,6,7) in( '1700001') then '  Management'" +

                                  "  when substring(gangno,6,7) in( '1100002') then ' Eng Sup'" +
                                  "   when substring(gangno,6,7) in( '1300002') then ' Eng Sup' " +
                                  "  when substring(gangno,6,7) in( '1400002') then ' Eng Sup'" +
                                  "  when substring(gangno,6,7) in( '1500002') then ' Eng Sup' " +
                                  "   when substring(gangno,6,7) in( '1600002') then ' Eng Sup'" +
                                  "   when substring(gangno,6,7) in( '1700002') then ' Eng Sup' " +




                                  "  when substring(gangno,8,3) in( '411') then ' Backfill' " +



                                  "  when substring(gangno,10,3) in( '545') and SUBSTRING(GangNo, 5,1) = '5' then ' BPF Dpt'" +
                                   " when substring(gangno,6,2) in( '13') and SUBSTRING(GangNo, 5,1) = '5' then ' Survey'" +
                                   "  when substring(gangno,10,3) in( '565') and SUBSTRING(GangNo, 5,1) = '5' then ' Rock Mechanics'  " +

                                   "  when substring(gangno,10,3) in( '530') and SUBSTRING(GangNo, 5,1) = '5' then ' Geology'" +

                                   "  when substring(gangno,10,3) in( '501') and SUBSTRING(GangNo, 5,1) = '5' then ' Vent' " +

                                  " else ' other'  END as calsaa,  " +
                                  " aa,  gangno a " +
                                  " , GangName xx,  convert(numeric(7,0),actcomp) compl, convert(numeric(7,0),ActNum) ActNum1 " +
                                  " from ( " +


                                  " select OrgUnit gangno, substring(OrgUnit,6,1)+'0'+substring(OrgUnit,7,1) aa, * from dbo.vw_orgunits  " +
                                  " where OrgUnit in (select distinct(GangNo) from dbo.EmployeeAll where SUBSTRING(GangNo, 5,1) not in ('1','3') and SUBSTRING(GangNo, 5,1) = '" + sec3 + "') " +
                                  " ) a " +

                                  " union  select '3) Stoping' calsaa, '" + sec3 + "' aa, 'No Stp Gang Assigned' a, '' xx, convert(numeric(7,0),0) compl , convert(numeric(7,0),0) Actnum  " +
                                  " union  select '2) Development' calsaa, '" + sec3 + "' aa, 'No Dev Gang Assigned' a, '' xx, convert(numeric(7,0),0) compl , convert(numeric(7,0),0) Actnum) a " +


                                  " left outer join " +
                                  //" (select gang, SUM(UG) UG, SUM(Total) Total from   " +
                                  // " (select empno, gang, MAX(UG) UG, MAX(Total) Total from  " +
                                  // " (  select case when reader_description like '%ndergroun%' Then 1 when reader_code like '%UG%' Then 1 else 0 end as UG, 1 Total, " +
                                  //   " emp_empno empno, gang_number gang   " +
                                  //   " from AFZAVRCLSQL04.SKYCOM.dbo.CLOCKS  " + 
                                  //   " where substring(gang_number,13,1) <> '11' and   " +
                                  //  " substring(gang_number,6,1)+'0'+substring(gang_number,7,1) = '" + sec2 + "'  " +
                                  //   " and CONVERT(VARCHAR(10),clock_time,111)  = CONVERT(VARCHAR(10),GETDATE(),111)) a group by empno, gang)  " +
                                  //   " a group by gang) b  on a.a = b.gang   " +

                                  " (select  GangNo gang, convert(numeric(7,0), UG) UG, convert(numeric(7,0),Total) Total, convert(numeric(7,0), UGOut) UGOut,  convert(numeric(7,0), UGYFinal) ugoutFinal from  [dbo].[tbl_GangClock]  " +
                                  " where GangNo in (select distinct(GangNo) from dbo.EmployeeAll where SUBSTRING(GangNo, 5,1) not in ('1','3') and SUBSTRING(GangNo, 5,1) = '" + sec3 + "') ) b  on a.a = b.gang " +

                                  " left outer join  " +
                                  " (select org, sum(SqmTotal) SqmTotal, sum(adv) adv from " +
                                  " ( select  case when SUBSTRING(OrgUnitDS,1,15) <> '' then  " +
                                  " SUBSTRING(OrgUnitDS,1,15)  when SUBSTRING(OrgUnitDS,1,15) = ''  " +
                                  " and Activity <> 1 then 'No Stp Gang Assigned'   " +
                                  " when SUBSTRING(OrgUnitDS,1,15) = ''  " +
                                  " and Activity = 1 then 'No Dev Gang Assigned'  " +
                                  " end as org, SqmTotal,  " +
                                  " case when activity = 1 then Adv else 0  end as adv  " +
                                  " from tbl_PlanMonth p , tbl_Section s , tbl_Section s1  " +
                                  " where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and  " +
                                  " s.ReportToSectionid = s1.sectionid and s.prodmonth = s1.prodmonth and   " +
                                  " s1.ReportToSectionid = '" + sec3 + "' and p.Prodmonth =  " +
                                  " (select max(p.prodmonth) pm from tbl_Planning p, tbl_Section s , tbl_Section s1  " +
                                  " where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and   " +
                                  " s.ReportToSectionid = s1.sectionid and s.prodmonth = s1.prodmonth and  " +
                                  " calendardate = CONVERT(VARCHAR(10),GETDATE()-100,111) and s1.ReportToSectionid = '" + sec3 + "')) c  " +
                                  " group by org) c  on a.a = c.org where  calsaa not in ('2) Development','3) Stoping')   order by calsaa , a  ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ResultsTableName = "Losses";  //get table name
            _dbMan.ExecuteInstruction();



            DataSet ReportDataset = new DataSet();
            ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
            theReport.RegisterData(ReportDataset);




            theReport.Load("EarlyMorningShiftRprt.frx");

            //theReport.Design();

            pcReport.Clear();
            theReport.Prepare();
            theReport.Preview = pcReport;
            theReport.ShowPrepared();

            Application.Idle +=
                new System.EventHandler(this.Application_Idle);

        }

        private void ChangeLbl3_TextChanged(object sender, EventArgs e)
        {
            if (ChangeLbl3.Text != "")
            {
                if (Loaded == "Y")
                {
                    this.Cursor = Cursors.WaitCursor;
                    Loadother();
                    this.Cursor = Cursors.Default;
                }
            }
        }
    }
}
