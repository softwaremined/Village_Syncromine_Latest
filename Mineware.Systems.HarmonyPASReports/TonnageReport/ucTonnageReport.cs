using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using FastReport;
using System.Threading;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Reports.TonnageReport
{
    public partial class ucTonnageReport : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        DataTable tblDays = new DataTable("NewDays");
        public string checking;
        bool blnForceBoxholeInPlanning = false;
        //private DataTable dtSections;
        DataTable dtSections = new DataTable();
        private clsTonnageReportSettings  reportSettings = new clsTonnageReportSettings();
        public ucTonnageReport()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        public override bool prepareReport()
        {
            bool theResult = false;
            theReportThread = new Thread(new ParameterizedThreadStart(createReport));
            theReportThread.SetApartmentState(ApartmentState.STA);
            theReportThread.Start(reportSettings);
            theResult = true;
            return theResult;
        }

        private void createReport(Object theReportSettings)
        {
            StringBuilder sb = new StringBuilder();
            DataSet dsTonnage = new DataSet();
            DataSet dsTonnageSQL = new DataSet();

            FastReport.Report theReport = new FastReport.Report();

            #region Reef || Waste

            char cReefWaste;
            if (reportSettings .Type =="Reef")
                cReefWaste = 'R';
            else if (reportSettings.Type == "Waste")
                cReefWaste = 'W';
            else
                cReefWaste = 'T';


            #endregion

            bool bProgressive = false;
            if (reportSettings.GraphType == "Progressive")
                bProgressive = true;

            #region Database Call

            DateTime? dtStart = null;
            DateTime? dtEnd = null;

            #region Get Start & End Date

            if (reportSettings .ProdMonthSelection =="Prodmonth")
            {
                if (!dtStart.HasValue && !dtEnd.HasValue)
                {

                    MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                    _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan1.SqlStatement = "select * from CALENDARMILL WHERE MillMonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'";
                    _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan1.ExecuteInstruction();

                    bool bContinue = false;

                    foreach (DataRow dr in _dbMan1.ResultsDataTable.Rows)
                    {
                        if (!dtStart.HasValue)
                        {
                            dtStart = Convert.ToDateTime(dr["StartDate"]);
                            bContinue = true;
                        }

                        if (!dtEnd.HasValue)
                        {
                            dtEnd = Convert.ToDateTime(dr["EndDate"]);
                            bContinue = true;
                        }

                        if (bContinue)
                        {
                            bContinue = false;
                            continue;
                        }

                        if (Convert.ToDateTime(dr["StartDate"]) < dtStart)
                            dtStart = Convert.ToDateTime(dr["StartDate"]);

                        if (Convert.ToDateTime(dr["EndDate"]) > dtEnd)
                            dtEnd = Convert.ToDateTime(dr["EndDate"]);

                    }
                }
            }
            else
            {
                dtStart = reportSettings.StartDate;
                dtEnd = reportSettings.EndDate;
            }

            #endregion

            DateTime dtCurrent = dtStart.Value;
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            SqlConnection pConn = new SqlConnection(_dbMan.ConnectionString);
            SqlCommand pCmd = new SqlCommand();
            pCmd.CommandType = CommandType.Text;
            pCmd.Connection = pConn;

            #region Tonnage

            #region Planned / Broken

            sb.AppendLine();
            sb.AppendLine("select CalendarDate, p.Tons, p.ReefTons, p.WasteTons, BookTons, ");
            sb.AppendLine("	BookTonsReef = case when p.Activity IN(0,9) then BookReefTons else case when ReefWaste = 'R' then BookTons else null end end, ");
            //sb.AppendLine("	BookTonsWaste = case when activity = 1 then BookTonsWaste else case when ReefWaste != 'R' then BookTons else null end end, ");
            sb.AppendLine("	BookTonsWaste = case when p.activity IN(0,9,1) then BookWasteTons else case when ReefWaste in ('R','W') then BookTons else null end end, ");
            sb.AppendLine("	ReefWaste, p.Activity");
            sb.AppendLine("from PLANNING p inner join planmonth pp on p.prodmonth=pp.prodmonth and p.activity=pp.activity and p.sectionid=pp.sectionid and p.workplaceid=pp.workplaceid and p.plancode=pp.plancode");
            sb.AppendLine("WHERE p.plancode='MP' and CalendarDate >= '" + DisplayFmt.Date_SQL(dtStart) + "'");
            sb.AppendLine("and CalendarDate <= '" + DisplayFmt.Date_SQL(dtEnd) + "'");

            #endregion

            #region Trammed

            sb.AppendLine();
            sb.AppendLine("-- Tonnage Trammed");
            sb.AppendLine("select CalendarDate, sum((ATons + DTons + NTons)) AS Tons");
            sb.AppendLine("from BOOKINGTramming");
            sb.AppendLine("WHERE CalendarDate >= '" + DisplayFmt.Date_SQL(dtStart) + "'");
            sb.AppendLine("and CalendarDate <= '" + DisplayFmt.Date_SQL(dtEnd) + "' ");
            if (cReefWaste != 'T')
                sb.AppendLine("and ReefWaste = '" + cReefWaste + "'");
            sb.AppendLine("GROUP BY CalendarDate");
            sb.AppendLine("order by CalendarDate");

            #endregion

            #region Hoisted

            sb.AppendLine();
            sb.AppendLine("-- Tonnage Hoisted");
            sb.AppendLine(" select distinct(cl.CalendarDate) CalendarDate,");
            if (reportSettings .Type =="Reef")
                sb.AppendLine(" b.ReefTons AS Tons");
            else if (reportSettings.Type == "Waste")
                sb.AppendLine(" b.WasteTons AS Tons");
            else
                sb.AppendLine(" b.WasteTons + b.ReefTons AS Tons");
            sb.AppendLine("	from CODE_CALENDAR cc ");
            sb.AppendLine("	INNER JOIN CALENDARMILL cm on ");
            sb.AppendLine("       cc.CalendarCode = cm.CalendarCode ");
            sb.AppendLine("	INNER JOIN CALTYPE cl on ");
            sb.AppendLine("       cl.CalendarCode = cc.CalendarCode and ");
            sb.AppendLine("       cl.CalendarDate >= '" + DisplayFmt.Date_SQL(dtStart) + "' and ");
            sb.AppendLine("       cl.CalendarDate <= '" + DisplayFmt.Date_SQL(dtEnd) + "' ");
            sb.AppendLine("   left join BookingHoist b on ");
            sb.AppendLine("       b.CalendarDate = cl.CalendarDate");
            sb.AppendLine("   WHERE cc.Description = 'Mill Calendar' ");
            sb.AppendLine("   order by cl.CalendarDate ");

            #endregion

            #region Treated

            sb.AppendLine();
            sb.AppendLine("-- Tonnage Treated");
            sb.AppendLine("  select distinct(cl.CalendarDate) CalendarDate, ");
            sb.AppendLine("   Tons = convert(numeric(13,0),b.TonsTreated)");
            sb.AppendLine("   from CODE_CALENDAR cc ");
            sb.AppendLine("   INNER JOIN CALENDARMILL cm on ");
            sb.AppendLine("       cc.CalendarCode = cm.CalendarCode ");
            sb.AppendLine("    INNER JOIN CALTYPE cl on ");
            sb.AppendLine("       cl.CalendarCode = cc.CalendarCode and ");
            sb.AppendLine("       cl.CalendarDate >= '" + DisplayFmt.Date_SQL(dtStart) + "' and ");
            sb.AppendLine("       cl.CalendarDate <= '" + DisplayFmt.Date_SQL(dtEnd) + "' ");
            sb.AppendLine("   left join BookingMilling b on ");
            sb.AppendLine("       b.CalendarDate = cl.CalendarDate ");
            sb.AppendLine("   WHERE cc.Description = 'Mill Calendar' ");
            sb.AppendLine("   order by cl.CalendarDate  ");

            #endregion

            #endregion


            if (cReefWaste != 'W')
            {
                #region Grade

                #region Planned

                sb.AppendLine();
                sb.AppendLine("-- Grade Planned / Broken");
                sb.AppendLine("select p.CalendarDate,");
                sb.AppendLine("	isnull(sum(cast(p.grams as numeric(10,5))), 0) AS 'Content',");
                sb.AppendLine("	isnull(sum(cast(p.ReefTons as numeric(10,5))), 0) AS 'TonsReef',");
                sb.AppendLine("	case when sum(p.ReefTons) <> 0 ");
                sb.AppendLine("		then (sum(cast(p.grams as numeric(10,6))) / sum(p.ReefTons)) ");
                sb.AppendLine("		else 0 ");
                sb.AppendLine("	end");
                sb.AppendLine("	AS 'PlannedGradeDaily',");
                sb.AppendLine();
                sb.AppendLine("	isnull(sum(p.BookGrams), 0) AS 'BookGrams',");
                sb.AppendLine("	isnull(sum(p.BookReefTons), 0) AS 'BookReefTons',");
                sb.AppendLine("	case when sum(p.BookReefTons) <> 0 ");
                sb.AppendLine("		then (sum(p.BookGrams) / sum(p.BookReefTons)) ");
                sb.AppendLine("		else 0 ");
                sb.AppendLine("	end");
                sb.AppendLine("	AS 'BookedGradeDaily'");
                sb.AppendLine("from Planning p ");
                sb.AppendLine("left outer join PLANMONTH pp on");
                sb.AppendLine("  pp.Workplaceid = p.WorkplaceID and ");
                sb.AppendLine("  pp.Prodmonth = p.Prodmonth and  ");
                sb.AppendLine("  pp.SectionID = p.SectionID and ");
                sb.AppendLine("  pp.Activity = p.Activity");
                sb.AppendLine("WHERE p.CalendarDate >= '" + DisplayFmt.Date_SQL(dtStart) + "'");
                sb.AppendLine("and p.CalendarDate <= '" + DisplayFmt.Date_SQL(dtEnd) + "'");
                sb.AppendLine("GROUP BY CalendarDate");
                #endregion

                #region Belt

                if (bProgressive)
                {
                    sb.AppendLine();
                    sb.AppendLine("-- KGs Hoisted");
                    sb.AppendLine("select CalendarDate, case when reeftons <> 0 then (Gold * 1000)/ReefTons else 0 end AS BeltGrade");
                    sb.AppendLine("from BOOKINGHoist");
                    sb.AppendLine("WHERE CalendarDate >= '" + DisplayFmt.Date_SQL(dtStart) + "'");
                    sb.AppendLine("and CalendarDate <= '" + DisplayFmt.Date_SQL(dtEnd) + "'");
                }
                else
                {
                    sb.AppendLine();
                    sb.AppendLine("-- Grade Belt");
                    sb.AppendLine("  select distinct(cl.CalendarDate), b.BeltGrade");
                    sb.AppendLine("   from CODE_CALENDAR cc ");
                    sb.AppendLine("   INNER JOIN CALENDARMILL cm on ");
                    sb.AppendLine("       cc.CalendarCode = cm.CalendarCode ");
                    sb.AppendLine("    INNER JOIN CALTYPE cl on ");
                    sb.AppendLine("       cl.CalendarCode = cc.CalendarCode and ");
                    sb.AppendLine("       cl.CalendarDate >= cm.StartDate and ");
                    sb.AppendLine("       cl.CalendarDate <= cm.EndDate ");
                    sb.AppendLine("   left join BookingHoist b on ");
                    sb.AppendLine("       b.CalendarDate = cl.CalendarDate");
                    sb.AppendLine("   WHERE cc.Description = 'Mill Calendar' ");
                    sb.AppendLine("   and b.CalendarDate >= '" + DisplayFmt.Date_SQL(dtStart) + "'");
                    sb.AppendLine("   and b.CalendarDate <= '" + DisplayFmt.Date_SQL(dtEnd) + "'");
                    sb.AppendLine("   order by cl.CalendarDate ");
                }
                #endregion

                #endregion

                #region KGs

                #region Planned / Broken



                sb.AppendLine();
                sb.AppendLine("-- KGs Planned / Broken");
                sb.AppendLine("select CalendarDate, ");
                sb.AppendLine("	convert(decimal(18,3), ISNULL(SUM(grams), 0) / 1000) AS Content, ");
                sb.AppendLine("	convert(decimal(18,3), ISNULL(SUM(BookGrams), 0) / 1000) AS BookGrams");
                sb.AppendLine("from PLANNING");
                sb.AppendLine("WHERE CalendarDate >= '" + DisplayFmt.Date_SQL(dtStart) + "'");
                sb.AppendLine("and CalendarDate <= '" + DisplayFmt.Date_SQL(dtEnd) + "'");
                sb.AppendLine("GROUP BY CalendarDate");

                #endregion

                #region Hoisted

                sb.AppendLine();
                sb.AppendLine("-- KGs Hoisted");
                sb.AppendLine("  select distinct(cl.CalendarDate) CalendarDate,");
                sb.AppendLine("		convert(decimal(18,3),(isnull(b.ReefTons,0) * isnull(b.BeltGrade,0))/1000) AS Tons");
                sb.AppendLine("   from CODE_CALENDAR cc ");
                sb.AppendLine("   INNER JOIN CALENDARMILL cm on ");
                sb.AppendLine("       cc.CalendarCode = cm.CalendarCode and ");
                sb.AppendLine("       cm.MillMonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'   INNER JOIN CALTYPE cl on ");
                sb.AppendLine("       cl.CalendarCode = cc.CalendarCode and ");
                sb.AppendLine("       cl.CalendarDate >= cm.StartDate and ");
                sb.AppendLine("       cl.CalendarDate <= cm.EndDate ");
                sb.AppendLine("   left join BookingHoist b on ");
                sb.AppendLine("       b.CalendarDate = cl.CalendarDate ");
                sb.AppendLine("   WHERE cc.Description = 'Mill Calendar' ");
                sb.AppendLine("   order by cl.CalendarDate ");

                #endregion

                #endregion

                #region GT

                sb.AppendLine();
                sb.AppendLine("-- Grade GT");
                sb.AppendLine("SELECT CalendarDate, CASE WHEN SUM(ATons + NTons + DTons) <> 0 THEN SUM((ATons + NTons + DTons) * gt)/SUM(ATons + NTons + DTons) ELSE 0 END AS 'GT' FROM BOOKINGTramming ");
                sb.AppendLine("WHERE CalendarDate >= '" + DisplayFmt.Date_SQL(dtStart) + "' AND CalendarDate <= '" + DisplayFmt.Date_SQL(dtEnd) + "' ");
                sb.AppendLine("GROUP BY CalendarDate");

                #endregion

            }

            #region Unidentified Trammed

            sb.AppendLine();
            sb.AppendLine("-- Tonnage Trammed");
            sb.AppendLine("select CalendarDate, sum((ATons + DTons + NTons)) AS Tons");
            sb.AppendLine("from BOOKINGTramming");
            sb.AppendLine("WHERE CalendarDate >= '" + DisplayFmt.Date_SQL(dtStart) + "'");
            sb.AppendLine("and CalendarDate <= '" + DisplayFmt.Date_SQL(dtEnd) + "' AND SectionID = 'Unidentified' ");
            if (cReefWaste != 'T')
                sb.AppendLine("and ReefWaste = '" + cReefWaste + "'");
            sb.AppendLine("GROUP BY CalendarDate");
            sb.AppendLine("order by CalendarDate");

            #endregion

            pCmd.CommandText = sb.ToString();

            pConn.Open();
            SqlDataAdapter pAdap = new SqlDataAdapter(pCmd);
            pAdap.Fill(dsTonnageSQL);
            pConn.Close();

            dsTonnageSQL.Tables[0].TableName = "TonnagePlannedBroken";
            dsTonnageSQL.Tables[1].TableName = "TonnageTrammed";
            dsTonnageSQL.Tables[2].TableName = "TonnageHoisted";
            dsTonnageSQL.Tables[3].TableName = "TonnageTreated";

            if (cReefWaste != 'W')
            {
                dsTonnageSQL.Tables[4].TableName = "GradePlannedBroken";
                dsTonnageSQL.Tables[5].TableName = "GradeBelt";
                dsTonnageSQL.Tables[6].TableName = "KGsPlannedBroken";
                dsTonnageSQL.Tables[7].TableName = "KGsHoisted";
                dsTonnageSQL.Tables[8].TableName = "GT";
                dsTonnageSQL.Tables[9].TableName = "TonnageUnidentifiedTrammed";
                dsTonnageSQL.Tables.Add("TonnageUnidentifiedPercentage");
            }
            else
            {
                dsTonnageSQL.Tables[4].TableName = "TonnageUnidentifiedTrammed";
                dsTonnageSQL.Tables.Add("TonnageUnidentifiedPercentage");
            }
            dsTonnageSQL.Tables["TonnageUnidentifiedPercentage"].Columns.Add("CalendarDate", typeof(DateTime));
            dsTonnageSQL.Tables["TonnageUnidentifiedPercentage"].Columns.Add("Tons", typeof(Decimal));
            #endregion

            #region Tonnage

            #region Initialze DataTable and Add Columns

            DataTable tblTonnage = new DataTable("Tonnage");

            tblTonnage.Columns.Add(" ");

            while (dtCurrent <= dtEnd)
            {
                try
                {
                    tblTonnage.Columns.Add(dtCurrent.ToString("dd/MM"));

                    dtCurrent = dtCurrent.AddDays(1);
                }
                catch (Exception x)
                {
                    return;
                }
            }

            //if (!tblTonnage.Columns.Contains("31"))
            //    tblTonnage.Columns.Add("31");

            #endregion

            #region Planned / Broken

            DataRow drTonnagePlanned = tblTonnage.NewRow();
            drTonnagePlanned[0] = "Planned";
            DataRow drTonnageBooked = tblTonnage.NewRow();
            drTonnageBooked[0] = "Broken";

            dtCurrent = dtStart.Value;
            while (dtCurrent <= dtEnd)
            {
                decimal fPlanned = 0.0m;
                decimal fBooked = 0.0m;

                foreach (DataRow dr in dsTonnageSQL.Tables["TonnagePlannedBroken"].Rows)
                {
                    if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                    {
                        switch (reportSettings .Type )
                        {
                            case "Reef": // Reef
                                if (dr["ReefTons"] != DBNull.Value)
                                    fPlanned += Convert.ToDecimal(dr["ReefTons"]);
                                if (dr["BookTonsReef"] != DBNull.Value)
                                    fBooked += Convert.ToDecimal(dr["BookTonsReef"]);
                                break;
                            case "Waste": // Waste
                                if (dr["WasteTons"] != DBNull.Value)
                                    fPlanned += Convert.ToDecimal(dr["WasteTons"]);
                                if (dr["BookTonsWaste"] != DBNull.Value)
                                    fBooked += Convert.ToDecimal(dr["BookTonsWaste"]);
                                break;
                            case "Total": // Total
                                if (dr["WasteTons"] != DBNull.Value)
                                    fPlanned += Convert.ToDecimal(dr["WasteTons"]);
                                if (dr["ReefTons"] != DBNull.Value)
                                    fPlanned += Convert.ToDecimal(dr["ReefTons"]);

                                if (dr["BookTonsWaste"] != DBNull.Value)
                                    fBooked += Convert.ToDecimal(dr["BookTonsWaste"]);
                                if (dr["BookTonsReef"] != DBNull.Value)
                                    fBooked += Convert.ToDecimal(dr["BookTonsReef"]);
                                break;
                        }
                    }
                }
                if (bProgressive)
                {
                    if (dtStart.Value.Date == dtCurrent.Date)
                    {
                        drTonnagePlanned[dtCurrent.ToString("dd/MM")] = Math.Round(fPlanned);
                        drTonnageBooked[dtCurrent.ToString("dd/MM")] = Math.Round(fBooked);
                    }
                    else
                    {
                        drTonnagePlanned[dtCurrent.ToString("dd/MM")] = Math.Round(fPlanned + Convert.ToDecimal(drTonnagePlanned[dtCurrent.AddDays(-1).ToString("dd/MM")]));
                        drTonnageBooked[dtCurrent.ToString("dd/MM")] = Math.Round(fBooked + Convert.ToDecimal(drTonnageBooked[dtCurrent.AddDays(-1).ToString("dd/MM")]));
                    }
                }
                else
                {
                    drTonnagePlanned[dtCurrent.ToString("dd/MM")] = Math.Round(fPlanned);
                    drTonnageBooked[dtCurrent.ToString("dd/MM")] = Math.Round(fBooked);
                }
                dtCurrent = dtCurrent.AddDays(1);
            }
            tblTonnage.Rows.Add(drTonnagePlanned);
            tblTonnage.Rows.Add(drTonnageBooked);

            #endregion

            #region Trammed


            DataRow drTramming = tblTonnage.NewRow();

            drTramming[0] = "Trammed";

            dtCurrent = dtStart.Value;
            while (dtCurrent <= dtEnd)
            {
                decimal fTons = 0.0m;
                foreach (DataRow dr in dsTonnageSQL.Tables["TonnageTrammed"].Rows)
                {
                    if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                    {
                        if (dr["Tons"] != DBNull.Value)
                            fTons = Convert.ToDecimal(dr["Tons"]);

                        if (bProgressive)
                        {
                            if (dtStart.Value.Date == dtCurrent.Date)
                            {
                                drTramming[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                            }
                            else
                            {
                                decimal fPreviousTons = 0.0m;
                                if (drTramming[dtCurrent.AddDays(-1).ToString("dd/MM")] != DBNull.Value)
                                    fPreviousTons = Convert.ToDecimal(drTramming[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                                drTramming[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons + fPreviousTons, 0);
                            }
                        }
                        else
                        {
                            drTramming[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                        }
                    }
                }
                if (drTramming[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                {
                    if (bProgressive)
                    {
                        if (dtStart.Value.Date == dtCurrent.Date)
                        {
                            drTramming[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                        }
                        else
                        {
                            decimal fPreviousTons = 0.0m;
                            if (drTramming[dtCurrent.AddDays(-1).ToString("dd/MM")] != DBNull.Value)
                                fPreviousTons = Convert.ToDecimal(drTramming[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                            drTramming[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons + fPreviousTons, 0);
                        }
                    }
                    else
                    {
                        drTramming[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(0.0m, 0);
                    }
                }
                dtCurrent = dtCurrent.AddDays(1);
            }

            tblTonnage.Rows.Add(drTramming);

            #endregion

            #region Hoisted


            DataRow drHoisted = tblTonnage.NewRow();

            drHoisted[0] = "Hoisted";

            dtCurrent = dtStart.Value;
            while (dtCurrent <= dtEnd)
            {
                decimal fTons = 0.0m;
                foreach (DataRow dr in dsTonnageSQL.Tables["TonnageHoisted"].Rows)
                {
                    if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                    {
                        if (dr["Tons"] != DBNull.Value)
                            fTons = Convert.ToDecimal(dr["Tons"]);
                        if (bProgressive)
                        {
                            if (dtStart.Value.Date == dtCurrent.Date)
                            {
                                drHoisted[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                            }
                            else
                            {
                                decimal fPreviousTons = 0.0m;
                                if (drHoisted[dtCurrent.AddDays(-1).ToString("dd/MM")] != DBNull.Value)
                                    fPreviousTons = Convert.ToDecimal(drHoisted[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                                drHoisted[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons + fPreviousTons, 0);
                            }
                        }
                        else
                        {
                            drHoisted[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                        }
                    }
                }
                if (drHoisted[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                {
                    if (bProgressive)
                    {
                        if (dtStart.Value.Date == dtCurrent.Date)
                        {
                            drHoisted[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                        }
                        else
                        {
                            decimal fPreviousTons = 0.0m;
                            if (drHoisted[dtCurrent.AddDays(-1).ToString("dd/MM")] != DBNull.Value)
                                fPreviousTons = Convert.ToDecimal(drHoisted[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                            drHoisted[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons + fPreviousTons, 0);
                        }
                    }
                    else
                    {
                        drHoisted[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(0.0m, 0);
                    }
                }

                dtCurrent = dtCurrent.AddDays(1);
            }

            tblTonnage.Rows.Add(drHoisted);

            #endregion

            #region Treated

            DataRow drTreated = tblTonnage.NewRow();

            drTreated[0] = "Treated";

            dtCurrent = dtStart.Value;
            while (dtCurrent <= dtEnd)
            {
                decimal fTons = 0.0m;
                foreach (DataRow dr in dsTonnageSQL.Tables["TonnageTreated"].Rows)
                {
                    if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                    {
                        if (dr["Tons"] != DBNull.Value)
                            fTons = Convert.ToDecimal(dr["Tons"]);
                        if (bProgressive)
                        {
                            if (dtStart.Value.Date == dtCurrent.Date)
                            {
                                drTreated[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                            }
                            else
                            {
                                decimal fPreviousTons = 0.0m;
                                if (drTreated[dtCurrent.AddDays(-1).ToString("dd/MM")] != DBNull.Value)
                                    fPreviousTons = Convert.ToDecimal(drTreated[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                                drTreated[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons + fPreviousTons, 0);
                            }
                        }
                        else
                        {
                            drTreated[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                        }
                    }
                }
                if (drTreated[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                {
                    if (bProgressive)
                    {
                        if (dtStart.Value.Date == dtCurrent.Date)
                        {
                            drTreated[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                        }
                        else
                        {
                            decimal fPreviousTons = 0.0m;
                            if (drTreated[dtCurrent.AddDays(-1).ToString("dd/MM")] != DBNull.Value)
                                fPreviousTons = Convert.ToDecimal(drTreated[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                            drTreated[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons + fPreviousTons, 0);
                        }
                    }
                    else
                    {
                        drTreated[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(0.0m, 0);
                    }
                }
                dtCurrent = dtCurrent.AddDays(1);
            }

            tblTonnage.Rows.Add(drTreated);

            #endregion

            #region Unidentified Trammed

            DataRow drUnidentifiedTrammed = tblTonnage.NewRow();

            drUnidentifiedTrammed[0] = "Unidentified Trammed";

            dtCurrent = dtStart.Value;
            while (dtCurrent <= dtEnd)
            {
                decimal fTons = 0.0m;
                foreach (DataRow dr in dsTonnageSQL.Tables["TonnageUnidentifiedTrammed"].Rows)
                {
                    if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                    {
                        if (dr["Tons"] != DBNull.Value)
                            fTons = Convert.ToDecimal(dr["Tons"]);
                        if (bProgressive)
                        {
                            if (dtStart.Value.Date == dtCurrent.Date)
                            {
                                drUnidentifiedTrammed[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                            }
                            else
                            {
                                decimal fPreviousTons = 0.0m;
                                if (drUnidentifiedTrammed[dtCurrent.AddDays(-1).ToString("dd/MM")] != DBNull.Value)
                                    fPreviousTons = Convert.ToDecimal(drUnidentifiedTrammed[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                                drUnidentifiedTrammed[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons + fPreviousTons, 0);
                            }
                        }
                        else
                        {
                            drUnidentifiedTrammed[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                        }
                    }
                }
                if (drUnidentifiedTrammed[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                {
                    if (bProgressive)
                    {
                        if (dtStart.Value.Date == dtCurrent.Date)
                        {
                            drUnidentifiedTrammed[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons, 0);
                        }
                        else
                        {
                            decimal fPreviousTons = 0.0m;
                            if (drUnidentifiedTrammed[dtCurrent.AddDays(-1).ToString("dd/MM")] != DBNull.Value)
                                fPreviousTons = Convert.ToDecimal(drUnidentifiedTrammed[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                            drUnidentifiedTrammed[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(fTons + fPreviousTons, 0);
                        }
                    }
                    else
                    {
                        drUnidentifiedTrammed[dtCurrent.ToString("dd/MM")] = DisplayFmt.CustomDecimal(0.0m, 0);
                    }
                }
                dtCurrent = dtCurrent.AddDays(1);
            }

            tblTonnage.Rows.Add(drUnidentifiedTrammed);

            #endregion

            #region Unidentified Percentage

            DataRow drUnidentifiedPercentage = tblTonnage.NewRow();

            drUnidentifiedPercentage[0] = "Unidentified Percentage";

            for (int i = 1; i < drUnidentifiedTrammed.ItemArray.Length; i++)
            {
                if (Convert.ToDecimal(drTramming[i]) != 0)
                {
                    drUnidentifiedPercentage[i] = Math.Round((Convert.ToDecimal(drUnidentifiedTrammed[i]) / Convert.ToDecimal(drTramming[i])) * 100, 2);
                }
                else
                {
                    drUnidentifiedPercentage[i] = 0;
                }
            }

            tblTonnage.Rows.Add(drUnidentifiedPercentage);

            #endregion

            tblTonnage.TableName = "Tonnage";
            tblTonnage.AcceptChanges();
            dsTonnage.Tables.Add(tblTonnage);

            #endregion

            #region Grade

            #region Initialze DataTable and Add Columns

            DataTable tblGrade = new DataTable("Grade");

            tblGrade.Columns.Add(" ");
            dtCurrent = dtStart.Value;
            while (dtCurrent <= dtEnd)
            {
                tblGrade.Columns.Add(dtCurrent.ToString("dd/MM"));

                dtCurrent = dtCurrent.AddDays(1);
            }

            #endregion

            if (cReefWaste != 'W')
            {

                #region Planned


                DataRow drPlanned = tblGrade.NewRow();
                drPlanned[0] = "Planned";

                dtCurrent = dtStart.Value;
                bool bMyFirst = true;
                while (dtCurrent <= dtEnd)
                {
                    foreach (DataRow dr in dsTonnageSQL.Tables["GradePlannedBroken"].Rows)
                    {
                        if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                        {
                            if (bProgressive)
                            {
                                if (bMyFirst)
                                {
                                    drPlanned[dtCurrent.ToString("dd/MM")] = DisplayFmt.Money(dr["PlannedGradeDaily"]);
                                    bMyFirst = false;
                                }
                                else
                                {
                                    string strExpression = "CalendarDate <= #" + dtCurrent.ToString() + "#";
                                    decimal fContent = 0.0m;
                                    decimal fTons = 0.0m;

                                    foreach (DataRow drToday in dsTonnageSQL.Tables["GradePlannedBroken"].Select(strExpression))
                                    {
                                        fContent += Convert.ToDecimal(drToday["Content"]);
                                        fTons += Convert.ToDecimal(drToday["TonsReef"]);
                                    }
                                    if (fTons > 0)
                                    {
                                        drPlanned[dtCurrent.ToString("dd/MM")] = DisplayFmt.Money(fContent / fTons);
                                    }

                                }
                            }
                            else
                            {
                                drPlanned[dtCurrent.ToString("dd/MM")] = DisplayFmt.Money(dr["PlannedGradeDaily"]);
                            }
                        }
                    }
                    if (drPlanned[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                    {
                        //if (bProgressive)
                        //    drPlanned[dtCurrent.ToString("dd")] = drPlanned[dtCurrent.AddDays(-1).ToString("dd")];
                        //else
                        drPlanned[dtCurrent.ToString("dd/MM")] = 0.0m;
                    }
                    dtCurrent = dtCurrent.AddDays(1);
                }

                tblGrade.Rows.Add(drPlanned);

                #endregion

                #region Broken

                DataRow drBroken = tblGrade.NewRow();
                drBroken[0] = "Broken";

                dtCurrent = dtStart.Value;
                bMyFirst = true;
                while (dtCurrent <= dtEnd)
                {
                    foreach (DataRow dr in dsTonnageSQL.Tables["GradePlannedBroken"].Rows)
                    {
                        if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                        {
                            if (bProgressive)
                            {
                                if (bMyFirst)
                                {
                                    drBroken[dtCurrent.ToString("dd/MM")] = DisplayFmt.Money(dr["BookedGradeDaily"]);
                                    bMyFirst = false;
                                }
                                else
                                {
                                    string strExpression = "CalendarDate <= #" + dtCurrent.ToString() + "#";
                                    decimal fContent = 0.0m;
                                    decimal fTons = 0.0m;

                                    foreach (DataRow drToday in dsTonnageSQL.Tables["GradePlannedBroken"].Select(strExpression))
                                    {
                                        fContent += Convert.ToDecimal(drToday["BookGrams"]);
                                        fTons += Convert.ToDecimal(drToday["BookReefTons"]);
                                    }
                                    if (fContent != 0 && fTons != 0)
                                        drBroken[dtCurrent.ToString("dd/MM")] = DisplayFmt.Money(fContent / fTons);
                                }
                            }
                            else
                            {
                                drBroken[dtCurrent.ToString("dd/MM")] = DisplayFmt.Money(dr["BookedGradeDaily"]);
                            }
                        }
                    }
                    if (drBroken[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                    {
                        //if (bProgressive)
                        //    drPlanned[dtCurrent.ToString("dd")] = drPlanned[dtCurrent.AddDays(-1).ToString("dd")];
                        //else
                        drBroken[dtCurrent.ToString("dd/MM")] = 0.0m;
                    }
                    dtCurrent = dtCurrent.AddDays(1);
                }

                tblGrade.Rows.Add(drBroken);

                #endregion

                #region Belt


                DataRow drBelt = tblGrade.NewRow();
                drBelt[0] = "Belt";

                dtCurrent = dtStart.Value;
                while (dtCurrent <= dtEnd)
                {
                    decimal fGrade = 0.0m;
                    foreach (DataRow dr in dsTonnageSQL.Tables["GradeBelt"].Rows)
                    {
                        if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                        {
                            if (dr["BeltGrade"] != DBNull.Value)
                                fGrade = Convert.ToDecimal(dr["BeltGrade"]);
                         
                            drBelt[dtCurrent.ToString("dd/MM")] = DisplayFmt.Money(fGrade);
                          
                        }
                    }
                    if (drBelt[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                    {
                        if (bProgressive)
                        {
                            if (dtStart.Value.Date == dtCurrent.Date)
                            {
                                drBelt[dtCurrent.ToString("dd/MM")] = fGrade;
                            }
                            else
                            {
                                decimal fPreviousGrade = 0.0m;
                                if (drBelt[dtCurrent.AddDays(-1).ToString("dd/MM")] != DBNull.Value)
                                    fPreviousGrade = Convert.ToDecimal(drBelt[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                                drBelt[dtCurrent.ToString("dd/MM")] = fGrade + fPreviousGrade;
                            }
                        }
                        else
                        {
                            drBelt[dtCurrent.ToString("dd/MM")] = 0.0m;
                        }
                    }
                    dtCurrent = dtCurrent.AddDays(1);
                }

                tblGrade.Rows.Add(drBelt);

                #endregion

                #region G/t

                DataRow drGT = tblGrade.NewRow();
                drGT[0] = "G/t";

                dtCurrent = dtStart.Value;
                while (dtCurrent <= dtEnd)
                {
                    decimal fGT = 0.0m;
                    foreach (DataRow dr in dsTonnageSQL.Tables["GT"].Rows)
                    {
                        if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                        {
                            if (dr["GT"] != DBNull.Value)
                                fGT = Convert.ToDecimal(dr["GT"]);

                            drGT[dtCurrent.ToString("dd/MM")] = DisplayFmt.Money(fGT);
                        }
                    }
                    if (drGT[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                    {
                        if (bProgressive)
                        {
                            if (dtStart.Value.Date == dtCurrent.Date)
                            {
                                drGT[dtCurrent.ToString("dd/MM")] = fGT;
                            }
                            else
                            {
                                decimal fPreviousGrade = 0.0m;
                                if (drGT[dtCurrent.AddDays(-1).ToString("dd/MM")] != DBNull.Value)
                                    fPreviousGrade = Convert.ToDecimal(drGT[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                                drGT[dtCurrent.ToString("dd/MM")] = fGT + fPreviousGrade;
                            }
                        }
                        else
                        {
                            drGT[dtCurrent.ToString("dd/MM")] = 0.0m;
                        }
                    }
                    dtCurrent = dtCurrent.AddDays(1);
                }

                tblGrade.Rows.Add(drGT);

                #endregion

            }

            tblGrade.TableName = "Grade";
            tblGrade.AcceptChanges();
            dsTonnage.Tables.Add(tblGrade);


            #endregion

            #region KG

            #region Initialze DataTable and Add Columns

            DataTable tblKGs = new DataTable("KGs");

            tblKGs.Columns.Add(" ");
            dtCurrent = dtStart.Value;
            while (dtCurrent <= dtEnd)
            {
                tblKGs.Columns.Add(dtCurrent.ToString("dd/MM"));

                dtCurrent = dtCurrent.AddDays(1);
            }

            #endregion

            if (cReefWaste != 'W')
            {
                #region Planned


                DataRow drPlannedKG = tblKGs.NewRow();
                drPlannedKG[0] = "Planned";

                dtCurrent = dtStart.Value;
                while (dtCurrent <= dtEnd)
                {
                    foreach (DataRow dr in dsTonnageSQL.Tables["KGsPlannedBroken"].Rows)
                    {
                        if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                        {
                            if (bProgressive)
                            {
                                decimal fPrevious = 0.0m;
                                try
                                {
                                    fPrevious = Convert.ToDecimal(drPlannedKG[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                                }
                                catch { }
                                drPlannedKG[dtCurrent.ToString("dd/MM")] = Convert.ToDecimal(dr["Content"]) + fPrevious;
                            }
                            else
                                drPlannedKG[dtCurrent.ToString("dd/MM")] = dr["Content"];
                        }
                    }
                    if (drPlannedKG[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                    {
                        if (bProgressive)
                        {
                            try
                            {
                                drPlannedKG[dtCurrent.ToString("dd/MM")] = drPlannedKG[dtCurrent.AddDays(-1).ToString("dd/MM")];
                            }
                            catch { }
                        }
                        else
                            drPlannedKG[dtCurrent.ToString("dd/MM")] = 0.0m;
                    }

                    dtCurrent = dtCurrent.AddDays(1);
                }

                tblKGs.Rows.Add(drPlannedKG);

                #endregion

                #region Broken

                DataRow drBrokenKG = tblKGs.NewRow();
                drBrokenKG[0] = "Broken";

                dtCurrent = dtStart.Value;
                while (dtCurrent <= dtEnd)
                {
                    foreach (DataRow dr in dsTonnageSQL.Tables["KGsPlannedBroken"].Rows)
                    {
                        if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                        {
                            if (bProgressive)
                            {
                                decimal fPrevious = 0.0m;
                                try
                                {
                                    fPrevious = Convert.ToDecimal(drBrokenKG[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                                }
                                catch { }
                                drBrokenKG[dtCurrent.ToString("dd/MM")] = Convert.ToDecimal(dr["BookGrams"]) + fPrevious;
                            }
                            else
                                drBrokenKG[dtCurrent.ToString("dd/MM")] = dr["BookGrams"];
                        }
                    }
                    if (drBrokenKG[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                    {
                        if (bProgressive)
                        {
                            try
                            {
                                drBrokenKG[dtCurrent.ToString("dd/MM")] = drBrokenKG[dtCurrent.AddDays(-1).ToString("dd/MM")];
                            }
                            catch { drBrokenKG[dtCurrent.ToString("dd/MM")] = 0.0m; }
                        }
                        else
                            drBrokenKG[dtCurrent.ToString("dd/MM")] = 0.0m;
                    }

                    dtCurrent = dtCurrent.AddDays(1);
                }

                tblKGs.Rows.Add(drBrokenKG);

                #endregion

                #region Hoisted



                DataRow drHoistedKG = tblKGs.NewRow();
                drHoistedKG[0] = "GOS";

                dtCurrent = dtStart.Value;
                while (dtCurrent <= dtEnd)
                {
                    foreach (DataRow dr in dsTonnageSQL.Tables["KGsHoisted"].Rows)
                    {
                        if (Convert.ToDateTime(dr["CalendarDate"]).Date == dtCurrent.Date)
                        {
                            if (bProgressive)
                            {
                                decimal fPrevious = 0.0m;
                                try
                                {
                                    fPrevious = Convert.ToDecimal(drHoistedKG[dtCurrent.AddDays(-1).ToString("dd/MM")]);
                                }
                                catch { }
                                drHoistedKG[dtCurrent.ToString("dd/MM")] = Convert.ToDecimal(dr["Tons"]) + fPrevious;
                            }
                            else
                                drHoistedKG[dtCurrent.ToString("dd/MM")] = dr["Tons"];
                        }
                    }
                    if (drHoistedKG[dtCurrent.ToString("dd/MM")] == DBNull.Value)
                    {
                        if (bProgressive)
                        {
                            try
                            {
                                drHoistedKG[dtCurrent.ToString("dd/MM")] = drHoistedKG[dtCurrent.AddDays(-1).ToString("dd/MM")];
                            }
                            catch { }
                        }
                        else
                            drHoistedKG[dtCurrent.ToString("dd/MM")] = 0.0m;
                    }

                    dtCurrent = dtCurrent.AddDays(1);
                }

                tblKGs.Rows.Add(drHoistedKG);

                #endregion

            }
            tblKGs.AcceptChanges();
            tblKGs.TableName = "KGs";
            dsTonnage.Tables.Add(tblKGs);

            #endregion

            #region Custom Data

            DataTable tblCustom = new DataTable("Custom");
            tblCustom.Columns.Add("Banner");
            tblCustom.Columns.Add("ProdMonth");
            tblCustom.Columns.Add("ReefWaste");
            tblCustom.Columns.Add("GraphType");
            tblCustom.Columns.Add("Section");

            string strReefWaste = "Reef";
            if (cReefWaste == 'W')
                strReefWaste = "Waste";
            else if (cReefWaste == 'T')
                strReefWaste = "Total";

            string strGraphType = "Daily";
            if (bProgressive)
                strGraphType = "Progressive";

            DataRow drCustom = tblCustom.NewRow();
            drCustom["Banner"] = SysSettings.Banner;                                               
            drCustom["ProdMonth"] = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth); 
            drCustom["ReefWaste"] = strReefWaste;
            drCustom["GraphType"] = strGraphType;
            drCustom["Section"] = reportSettings.SectionID;
            tblCustom.Rows.Add(drCustom);
            tblCustom.AcceptChanges();


            #region Initialze DataTable and Add Columns
            //Linda

            DataSet dsDays = new DataSet();

            {
                cntDays();
                tblDays.TableName = "NewDays";
                dsDays.Tables.Add(tblDays);
                theReport.RegisterData(tblDays, "NewDays");
            }

          
            DateTime theStart = dtStart.Value;
            DateTime theEnd = dtEnd.Value;

            TimeSpan aa = theEnd.Date - theStart.Date;
            int days = (int)aa.TotalDays;


    
            int cntRow = 0;
            if (blnForceBoxholeInPlanning && reportSettings .SectionID  != "1")
            {
                int strHier = 1;
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                if (reportSettings .ProdMonthSelection =="Prodmonth")
                {
                    _dbMan1.SqlStatement = "select top(1) Hierarchicalid from SECTION \r\n " +
                        " where SectionID = '" + reportSettings .SectionID  + "'\r\n " +
                        " and Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' ";
                }
                else
                {
                    DateTime fromdate = new DateTime();
                    fromdate = Convert.ToDateTime(reportSettings.StartDate );
                    DateTime todate = new DateTime();
                    todate = Convert.ToDateTime(reportSettings .EndDate );
                    _dbMan1.SqlStatement = " select top(1) Hierarchicalid \r\n \r\n " +
                        " from section where prodmonth >= '" + string.Format("{0:yyyyMM}", fromdate) + "' and prodmonth <= '" + string.Format("{0:yyyyMM}", todate) + "'";
                }
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();
                string strTypeReport = "P";
                if (reportSettings .ProdMonthSelection  == "FromToDate")
                    strTypeReport = "D";
                string strProgDay = "D";
                if (reportSettings .GraphType =="Progressive")
                    strProgDay = "P";
                string strRW = "R";
                if (reportSettings .Type =="Waste")
                    strRW = "W";
                else
                    if (reportSettings.Type == "Total")
                    strRW = "";

                strHier = Convert.ToInt32(_dbMan1.ResultsDataTable.Rows[0][0].ToString());
                MWDataManager.clsDataAccess _dbManDaily = new MWDataManager.clsDataAccess();
                try
                {
                    _dbManDaily.SqlStatement = "Report_Tonnage";
                    _dbManDaily.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    SqlParameter[] _paramCollection =
                    {
                        _dbManDaily.CreateParameter("@UserID", SqlDbType.VarChar, 20, clsUserInfo.UserID),
                        _dbManDaily.CreateParameter("@Prodmonth", SqlDbType.VarChar, 6, TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) ),
                        _dbManDaily.CreateParameter("@SectionID", SqlDbType.VarChar, 20, reportSettings .SectionID ),
                        _dbManDaily.CreateParameter("@HierID", SqlDbType.Int, 0, strHier),
                        _dbManDaily.CreateParameter("@TypeReport", SqlDbType.VarChar, 1, strTypeReport),
                        _dbManDaily.CreateParameter("@BDate", SqlDbType.VarChar, 10, reportSettings .StartDate ),
                        _dbManDaily.CreateParameter("@EDate", SqlDbType.VarChar, 10, reportSettings .EndDate ),
                        _dbManDaily.CreateParameter("@ProgDay", SqlDbType.VarChar, 1, strProgDay),
                        _dbManDaily.CreateParameter("@ReefWaste", SqlDbType.VarChar, 1, strRW),
                    };
                    _dbManDaily.ParamCollection = _paramCollection;
                    _dbManDaily.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManDaily.ExecuteInstruction();
                }
                catch (Exception _exception)
                {
                    throw new ApplicationException("Report Section:GradeSheet" + _exception.Message, _exception);
                }
                _dbManDaily.ResultsDataTable.TableName = "NewTonnage";
                cntRow = _dbManDaily.ResultsDataTable.Rows.Count;
                DataTable dt = new DataTable();
                dt = _dbManDaily.ResultsDataTable;
                dsTonnage.Tables.Add(dt);
            }

            #endregion

            theReport.RegisterData(tblCustom, "CustomData");

            #endregion

            theReport.RegisterData(dsTonnage);

            theReport.Load(TGlobalItems.ReportsFolder + "\\TonningReport.frx");
            if (reportSettings .ProdMonthSelection =="FromToDate")
            {
                theReport.SetParameterValue("FromTo", "T");
                DateTime fromdate = new DateTime();
                fromdate = Convert.ToDateTime(reportSettings.StartDate);
                DateTime todate = new DateTime();
                todate = Convert.ToDateTime(reportSettings.EndDate);
                theReport.SetParameterValue("From", fromdate.Date.ToShortDateString());
                theReport.SetParameterValue("To", todate.Date.ToShortDateString());
            }
            else
            {
            }

            if (cReefWaste == 'W')
            {
                theReport.Pages[1].Visible = false;
                theReport.Pages[2].Visible = false;
            }
            else
            {
                theReport.Pages[1].Visible = true;
                theReport.Pages[2].Visible = true;
            }

            if (blnForceBoxholeInPlanning && reportSettings .SectionID  != "1")
            {
                theReport.Pages[0].Visible = false;
                theReport.Pages[1].Visible = false;
                theReport.Pages[2].Visible = false;
                theReport.Pages[3].Visible = true;
            }
            else
            {
                theReport.Pages[3].Visible = false;
            }

            //theReport.Pages[0].Visible = true;
            //theReport.Pages[1].Visible = true;
            //theReport.Pages[2].Visible = true;
            //theReport.Pages[3].Visible = true;

            if (cntRow == null || cntRow <= 0)
            {
                theReport.Pages[3].Visible = false;
            }

            //if (MessageBox.Show("Design?", "Design?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            // theReport.Design();
            theReport.Prepare();
            theReport.Refresh();
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;
        }

        private void ucTonnageReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            // reportSettings.ReportDate = DateTime.Now;
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "(SELECT MAX(MillMonth)MillMonth FROM CALENDARMILL WHERE StartDate <= GETDATE())";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            DataTable dt = new DataTable();
            dt = _dbMan.ResultsDataTable;
            foreach (DataRow dr in dt.Rows)
            {
                reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(dr["MillMonth"].ToString());
            }
            //reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MillMonth .ToString ());
            reportSettings.StartDate = DateTime.Now;
            iFromDate.Properties.Value = reportSettings.StartDate;
            reportSettings.EndDate = DateTime.Now;
            iToDate.Properties.Value = reportSettings.EndDate;
            iProdmonth.Properties.Value = reportSettings.Prodmonth;
           reportSettings.ProdMonthSelection  = "Prodmonth";
            reportSettings.Type = "Reef";
            reportSettings.GraphType = "Daily";
            Load_Sections();
            iFromDate.Visible = false;
            iToDate.Visible = false;
            iProdmonth.Visible = true;
            iSection.Visible = false;
          pgTonnageRepSettings   .SelectedObject = reportSettings;
        }

      

        private void Load_Sections()
        {
            //if (reportSettings.ProdMonthSelection == "Prodmonth")
            //{
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            if (reportSettings.ProdMonthSelection == "Prodmonth")
                {
                    _dbMan.SqlStatement = "select (SectionId + ':' + Name) Section, HierarchicalId from Section WHERE Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  " +
                                           "order by Hierarchicalid, SectionId ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();
                }
                else
                {
                    _dbMan.SqlStatement =
                        " select distinct((SectionId + ':' + Name)) Section, HierarchicalId \r\n " +
                        " from \r\n " +
                        " ( \r\n " +
                        "     select s.* from Section s \r\n " +
                        "     inner join Seccal scal on \r\n " +
                        "       s.Prodmonth = scal.Prodmonth and \r\n " +
                        "       s.SectionID = scal.Sectionid \r\n " +
                        "     where  \r\n " +
                        "         (scal.BeginDate <= '2014-04-01' and \r\n " +
                        "         scal.EndDate >= '2014-04-01') \r\n " +
                        "     union all \r\n " +
                        "     select s.* from Section s \r\n " +
                        "     inner join Seccal scal on \r\n " +
                        "       s.Prodmonth = scal.Prodmonth and \r\n " +
                        "       s.SectionID = scal.Sectionid \r\n " +
                        "     where   \r\n " +
                        "         (scal.BeginDate <= '2014-04-30' and \r\n " +
                        "         scal.EndDate >= '2014-04-30') \r\n " +
                        " ) a \r\n ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();
                }       
               riSection.ValueMember = "HierarchicalId";
               riSection.DisplayMember = "Section";
               riSection.DataSource = _dbMan.ResultsDataTable;
          //  }

        }

        private void riProdmonthSelection_EditValueChanged(object sender, EventArgs e)
        {
            pgTonnageRepSettings.PostEditor();
            if(reportSettings .ProdMonthSelection   =="Prodmonth")
            {
                iFromDate.Visible = false;
                iToDate.Visible = false;
                iProdmonth.Visible = true;
                Load_Sections();
            }
            else
            {
                iFromDate.Visible = true;
                iToDate.Visible = true;
                iProdmonth.Visible = false;
                Load_Sections();
            }
        }

        private void cntDays()
        {
            if (reportSettings.ProdMonthSelection == "Prodmonth")
            {
                MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
                _dbManDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManDate.SqlStatement = " select count(ct.CalendarDate) dd \r\n " +
                                          " from CALENDARMILL cm \r\n " +
                                          " inner join CALTYPE ct on \r\n " +
                                          "   ct.CalendarCode = cm.CalendarCode and \r\n " +
                                          "   ct.CalendarDate >= cm.StartDate and \r\n " +
                                          "   ct.CalendarDate <= cm.EndDate \r\n " +
                                          " where cm.MillMonth= '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' ";
                _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManDate.ExecuteInstruction();
                tblDays = _dbManDate.ResultsDataTable;
            }
            else
            {
                string CalenC = "";
                MWDataManager.clsDataAccess _dbManCalenC = new MWDataManager.clsDataAccess();
                _dbManCalenC.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManCalenC.SqlStatement =
                            " select distinct(CalendarCode) CalendarCode \r\n " +
                            " from CALENDARMILL cm \r\n " +
                            " where (cm.StartDate <= '" + String.Format("{0:yyyy-MM-dd}",reportSettings.StartDate ) + "' and \r\n " +
                            "        cm.EndDate >= '" + String.Format("{0:yyyy-MM-dd}", reportSettings.StartDate) + "') or \r\n " +
                            "       (cm.StartDate <= '" + String.Format("{0:yyyy-MM-dd}", reportSettings.EndDate ) + "' and \r\n " +
                            "        cm.EndDate >= '" + String.Format("{0:yyyy-MM-dd}", reportSettings.EndDate) + "') ";
                _dbManCalenC.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManCalenC.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManCalenC.ExecuteInstruction();

                DataTable dd = _dbManCalenC.ResultsDataTable;
                if (dd.Rows.Count != 0)
                {
                    CalenC = dd.Rows[0]["calendarCode"].ToString();
                    MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
                    _dbManDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManDate.SqlStatement =
                                " select count(ct.CalendarDate) dd \r\n " +
                                " from CALTYPE ct \r\n " +
                                "     where \r\n " +
                                "     ct.CalendarDate >= '" + String.Format("{0:yyyy-MM-dd}", reportSettings.StartDate) + "' and \r\n " +
                                "     ct.CalendarDate <= '" + String.Format("{0:yyyy-MM-dd}", reportSettings.EndDate) + "' and \r\n " +
                                "     ct.CalendarCode = '" + CalenC + "' ";
                    _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManDate.ExecuteInstruction();
                    tblDays = _dbManDate.ResultsDataTable;
                }
            }
        }
    }
}
