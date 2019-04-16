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
using Mineware.Systems.Production.SysAdminScreens.SetupCycles;
using FastReport;

using System.Diagnostics;
//using System.ComponentModel
using System.IO;
using System.Text.RegularExpressions;

using System.Threading;
using System.Globalization;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class ucSampling : Mineware.Systems.Global.ucBaseUserControl
    {

        clsDeptCapt _clsSetupCycles = new clsDeptCapt();
        Procedures procs = new Procedures();

        string wpSamp = "";
        string clickcol = "0";
        string status = "";
        string ClkCol = "";

        Report WPNewRep = new Report();

        public ucSampling()
        {
            InitializeComponent();
        }

        private void ucSampling_Load(object sender, EventArgs e)
        {
            _clsSetupCycles.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            Cursor.Current = Cursors.WaitCursor;
            LoadSampling();
            Cursor.Current = Cursors.Default;
        }

        void LoadSampling()
        {

            MWDataManager.clsDataAccess _dbManWPST = new MWDataManager.clsDataAccess();
            _dbManWPST.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPST.SqlStatement = " declare @NO1 datetime  \r\n" +
                                        "declare @NO2 datetime \r\n " +
                                        "declare @NO3 datetime \r\n " +
                                        "declare @NO4 datetime \r\n " +
                                        "declare @NO5 datetime \r\n " +
                                        "declare @NO6 datetime \r\n " +
                                        "declare @NO7 datetime \r\n " +
                                        "declare @NO8 datetime \r\n " +
                                        "declare @NO9 datetime \r\n " +
                                        "declare @NO10 datetime \r\n " +
                                        "declare @NO11 datetime \r\n " +
                                        "declare @NO12 datetime \r\n " +
                                        "declare @NO13 datetime \r\n " +
                                        "declare @NO14 datetime \r\n " +

                                        "declare @NO15 datetime  \r\n" +
                                        "declare @NO16 datetime  \r\n" +
                                        "declare @NO17 datetime  \r\n" +
                                        "declare @NO18 datetime  \r\n" +
                                        "declare @NO19 datetime  \r\n" +
                                        "declare @NO20 datetime  \r\n" +
                                        "declare @NO21 datetime  \r\n" +

                                        "declare @NO22 datetime  \r\n" +
                                        "declare @NO23 datetime  \r\n" +
                                        "declare @NO24 datetime  \r\n" +
                                        "declare @NO25 datetime  \r\n" +
                                        "declare @NO26 datetime  \r\n" +
                                        "declare @NO27 datetime  \r\n" +
                                        "declare @NO28 datetime  \r\n" +

                                        "set @NO1 = (  \r\n" +
                                        "select max(calendardate) firstmonday  \r\n" +
                                        "from planning p where calendardate < getdate() and datename(dw,calendardate) = 'Monday')  \r\n" +

                                        "set @NO2 = @NO1+ 1  \r\n" +
                                        "set @NO3 = @NO2+ 1  \r\n" +
                                        "set @NO4 = @NO3+ 1  \r\n" +
                                        "set @NO5 = @NO4+ 1  \r\n" +
                                        "set @NO6 = @NO5+ 1  \r\n" +
                                        "set @NO7 = @NO6+ 1  \r\n" +
                                        "set @NO8 = @NO7+ 1  \r\n" +
                                        "set @NO9 = @NO8+ 1  \r\n" +
                                        "set @NO10 = @NO9+ 1  \r\n" +
                                        "set @NO11 = @NO10+ 1  \r\n" +
                                        "set @NO12 = @NO11+ 1  \r\n" +
                                        "set @NO13 = @NO12+ 1  \r\n" +
                                        "set @NO14 = @NO13+ 1  \r\n" +

                                        "set @NO15 = @NO13+ 2  \r\n" +
                                        "set @NO16 = @NO13+ 3  \r\n" +
                                        "set @NO17 = @NO13+ 4  \r\n" +
                                        "set @NO18 = @NO13+ 5  \r\n" +
                                        "set @NO19 = @NO13+ 6  \r\n" +
                                        "set @NO20 = @NO13+ 7  \r\n" +
                                        "set @NO21 = @NO13+ 8  \r\n" +

                                        "set @NO22 = @NO13+ 9  \r\n" +
                                        "set @NO23 = @NO13+ 10  \r\n" +
                                        "set @NO24 = @NO13+ 11  \r\n" +
                                        "set @NO25 = @NO13+ 12  \r\n" +
                                        "set @NO26 = @NO13+ 13  \r\n" +
                                        "set @NO27 = @NO13+ 14  \r\n" +
                                        "set @NO28 = @NO13+ 15  \r\n" +

                                         " select a.*,  \r\n" +
                                         " case  \r\n" +
                                         " when b.samp_name is not null and ab.Geo_Name is null then isnull(Day1Fin,'')  + 'Samp'  \r\n" +
                                         " when b.samp_name is not null and ab.Geo_Name is not null then isnull(Day1Fin,'')  + 'Both'   \r\n" +
                                         " when b.samp_name is null and ab.Geo_Name is not null then isnull(Day1Fin,'')  + 'Geol'  \r\n " +
                                         " else Day1Fin end as Day1Finf,  \r\n" +

                                          " case  \r\n" +
                                         " when c.samp_name is not null and ac.Geo_Name is null then isnull(Day2Fin,'')  + 'Samp'  \r\n" +
                                         " when c.samp_name is not null and ac.Geo_Name is not null then isnull(Day2Fin,'')  + 'Both'   \r\n" +
                                         " when c.samp_name is null and ac.Geo_Name is not null then isnull(Day2Fin,'')  + 'Geol'   \r\n" +
                                         " else Day2Fin end as Day2Finf,  \r\n" +

                                          " case  \r\n" +
                                         " when d.samp_name is not null and ad.Geo_Name is null then isnull(Day3Fin,'')  + 'Samp'  \r\n" +
                                         " when d.samp_name is not null and ad.Geo_Name is not null then isnull(Day3Fin,'')  + 'Both'   \r\n" +
                                         " when d.samp_name is null and ad.Geo_Name is not null then isnull(Day3Fin,'')  + 'Geol'   \r\n" +
                                         " else Day3Fin end as Day3Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when e.samp_name is not null and ae.Geo_Name is null then isnull(Day4Fin,'')  + 'Samp'  \r\n" +
                                         " when e.samp_name is not null and ae.Geo_Name is not null then isnull(Day4Fin,'')  + 'Both'   \r\n" +
                                         " when e.samp_name is null and ae.Geo_Name is not null then isnull(Day4Fin,'')  + 'Geol'   \r\n" +
                                         " else Day4Fin end as Day4Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when f.samp_name is not null and af.Geo_Name is null then isnull(Day5Fin,'')  + 'Samp'  \r\n" +
                                         " when f.samp_name is not null and af.Geo_Name is not null then isnull(Day5Fin,'')  + 'Both'   \r\n" +
                                         " when f.samp_name is null and af.Geo_Name is not null then isnull(Day5Fin,'')  + 'Geol'   \r\n" +
                                         " else Day5Fin end as Day5Finf,  \r\n" +



                                         " case  \r\n" +
                                         " when g.samp_name is not null and ag.Geo_Name is null then isnull(Day6Fin,'')  + 'Samp'  \r\n" +
                                         " when g.samp_name is not null and ag.Geo_Name is not null then isnull(Day6Fin,'')  + 'Both'   \r\n" +
                                         " when g.samp_name is null and ag.Geo_Name is not null then isnull(Day6Fin,'')  + 'Geol'   \r\n" +
                                         " else Day6Fin end as Day6Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when h.samp_name is not null and ah.Geo_Name is null then isnull(Day7Fin,'')  + 'Samp'  \r\n" +
                                         " when h.samp_name is not null and ah.Geo_Name is not null then isnull(Day7Fin,'')  + 'Both'   \r\n" +
                                         " when h.samp_name is null and ah.Geo_Name is not null then isnull(Day7Fin,'')  + 'Geol'   \r\n" +
                                         " else Day7Fin end as Day7Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when i.samp_name is not null and ai.Geo_Name is null then isnull(Day8Fin,'')  + 'Samp'  \r\n" +
                                         " when i.samp_name is not null and ai.Geo_Name is not null then isnull(Day8Fin,'')  + 'Both'   \r\n" +
                                         " when i.samp_name is null and ai.Geo_Name is not null then isnull(Day8Fin,'')  + 'Geol'   \r\n" +
                                         " else Day8Fin end as Day8Finf,  \r\n" +

                                         " case  \r\n" +
                                         " when j.samp_name is not null and aj.Geo_Name is null then isnull(Day9Fin,'')  + 'Samp'  \r\n" +
                                         " when j.samp_name is not null and aj.Geo_Name is not null then isnull(Day9Fin,'')  + 'Both'   \r\n" +
                                         " when j.samp_name is null and aj.Geo_Name is not null then isnull(Day9Fin,'')  + 'Geol'   \r\n" +
                                         " else Day9Fin end as Day9Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when k.samp_name is not null and ak.Geo_Name is null then isnull(Day10Fin,'')  + 'Samp'  \r\n" +
                                         " when k.samp_name is not null and ak.Geo_Name is not null then isnull(Day10Fin,'')  + 'Both'   \r\n" +
                                         " when k.samp_name is null and ak.Geo_Name is not null then isnull(Day10Fin,'')  + 'Geol'   \r\n" +
                                         " else Day10Fin end as Day10Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when l.samp_name is not null and al.Geo_Name is null then isnull(Day11Fin,'')  + 'Samp'  \r\n" +
                                         " when l.samp_name is not null and al.Geo_Name is not null then isnull(Day11Fin,'')  + 'Both'   \r\n" +
                                         " when l.samp_name is null and al.Geo_Name is not null then isnull(Day11Fin,'')  + 'Geol'   \r\n" +
                                         " else Day11Fin end as Day11Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when m.samp_name is not null and am.Geo_Name is null then isnull(Day12Fin,'')  + 'Samp'  \r\n" +
                                         " when m.samp_name is not null and am.Geo_Name is not null then isnull(Day12Fin,'')  + 'Both'   \r\n" +
                                         " when m.samp_name is null and am.Geo_Name is not null then isnull(Day12Fin,'')  + 'Geol'   \r\n" +
                                         " else Day12Fin end as Day12Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when n.samp_name is not null and an.Geo_Name is null then isnull(Day13Fin,'')  + 'Samp'  \r\n" +
                                         " when n.samp_name is not null and an.Geo_Name is not null then isnull(Day13Fin,'')  + 'Both'   \r\n" +
                                         " when n.samp_name is null and an.Geo_Name is not null then isnull(Day13Fin,'')  + 'Geol'   \r\n" +
                                         " else Day13Fin end as Day13Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when o.samp_name is not null and ao.Geo_Name is null then isnull(Day14Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and ao.Geo_Name is not null then isnull(Day14Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and ao.Geo_Name is not null then isnull(Day14Fin,'')  + 'Geol'   \r\n" +
                                         " else Day14Fin end as Day14Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when o.samp_name is not null and ap.Geo_Name is null then isnull(Day15Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and ap.Geo_Name is not null then isnull(Day15Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and ap.Geo_Name is not null then isnull(Day15Fin,'')  + 'Geol'   \r\n" +
                                         " else Day15Fin end as Day15Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when o.samp_name is not null and aq.Geo_Name is null then isnull(Day16Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and aq.Geo_Name is not null then isnull(Day16Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and aq.Geo_Name is not null then isnull(Day16Fin,'')  + 'Geol'   \r\n" +
                                         " else Day16Fin end as Day16Finf,  \r\n" +

                                          " case  \r\n" +
                                         " when o.samp_name is not null and ar.Geo_Name is null then isnull(Day17Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and ar.Geo_Name is not null then isnull(Day17Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and ar.Geo_Name is not null then isnull(Day17Fin,'')  + 'Geol'   \r\n" +
                                         " else Day17Fin end as Day17Finf,  \r\n" +

                                          " case  \r\n" +
                                         " when o.samp_name is not null and asa.Geo_Name is null then isnull(Day18Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and asa.Geo_Name is not null then isnull(Day18Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and asa.Geo_Name is not null then isnull(Day18Fin,'')  + 'Geol'   \r\n" +
                                         " else Day18Fin end as Day18Finf,  \r\n" +

                                          " case  \r\n" +
                                         " when o.samp_name is not null and at.Geo_Name is null then isnull(Day19Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and at.Geo_Name is not null then isnull(Day19Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and at.Geo_Name is not null then isnull(Day19Fin,'')  + 'Geol'   \r\n" +
                                         " else Day19Fin end as Day19Finf,  \r\n" +

                                          " case  \r\n" +
                                         " when o.samp_name is not null and au.Geo_Name is null then isnull(Day20Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and au.Geo_Name is not null then isnull(Day20Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and au.Geo_Name is not null then isnull(Day20Fin,'')  + 'Geol'   \r\n" +
                                         " else Day20Fin end as Day20Finf,  \r\n" +

                                          " case  \r\n" +
                                         " when o.samp_name is not null and av.Geo_Name is null then isnull(Day21Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and av.Geo_Name is not null then isnull(Day21Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and av.Geo_Name is not null then isnull(Day21Fin,'')  + 'Geol'   \r\n" +
                                         " else Day21Fin end as Day21Finf,  \r\n" +

                                          " case  \r\n" +
                                         " when o.samp_name is not null and aw.Geo_Name is null then isnull(Day22Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and aw.Geo_Name is not null then isnull(Day22Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and aw.Geo_Name is not null then isnull(Day22Fin,'')  + 'Geol'   \r\n" +
                                         " else Day22Fin end as Day22Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when o.samp_name is not null and ax.Geo_Name is null then isnull(Day23Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and ax.Geo_Name is not null then isnull(Day23Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and ax.Geo_Name is not null then isnull(Day23Fin,'')  + 'Geol'   \r\n" +
                                         " else Day23Fin end as Day23Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when o.samp_name is not null and ay.Geo_Name is null then isnull(Day24Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and ay.Geo_Name is not null then isnull(Day24Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and ay.Geo_Name is not null then isnull(Day24Fin,'')  + 'Geol'   \r\n" +
                                         " else Day24Fin end as Day24Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when o.samp_name is not null and az.Geo_Name is null then isnull(Day25Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and az.Geo_Name is not null then isnull(Day25Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and az.Geo_Name is not null then isnull(Day25Fin,'')  + 'Geol'   \r\n" +
                                         " else Day25Fin end as Day25Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when o.samp_name is not null and aaa.Geo_Name is null then isnull(Day26Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and aaa.Geo_Name is not null then isnull(Day26Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and aaa.Geo_Name is not null then isnull(Day26Fin,'')  + 'Geol'   \r\n" +
                                         " else Day26Fin end as Day26Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when o.samp_name is not null and aab.Geo_Name is null then isnull(Day27Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and aab.Geo_Name is not null then isnull(Day27Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and aab.Geo_Name is not null then isnull(Day27Fin,'')  + 'Geol'   \r\n" +
                                         " else Day27Fin end as Day27Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when o.samp_name is not null and aac.Geo_Name is null then isnull(Day28Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and aac.Geo_Name is not null then isnull(Day28Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and aac.Geo_Name is not null then isnull(Day28Fin,'')  + 'Geol'   \r\n" +
                                         " else Day28Fin end as Day28Finf,  \r\n" +






                                         //"   case when c.samp_name is not null then isnull(Day2Fin,'') + '-' else Day2Fin end as Day2Finf,  \r\n" +

                                         //"   case when d.samp_name is not null then isnull(Day3Fin,'') + '-' else Day3Fin end as Day3Finf,  \r\n" +
                                         //"   case when e.samp_name is not null then isnull(Day4Fin,'') + '-' else Day4Fin end as Day4Finf,  \r\n" +
                                         //"   case when f.samp_name is not null then isnull(Day5Fin,'') + '-' else Day5Fin end as Day5Finf,  \r\n" +
                                         //"   case when g.samp_name is not null then isnull(Day6Fin,'') + '-' else Day6Fin end as Day6Finf,  \r\n" +
                                         //"   case when h.samp_name is not null then isnull(Day7Fin,'') + '-' else Day7Fin end as Day7Finf,  \r\n" +
                                         //"   case when i.samp_name is not null then isnull(Day8Fin,'') + '-' else Day8Fin end as Day8Finf,  \r\n" +
                                         //"   case when j.samp_name is not null then isnull(Day9Fin,'') + '-' else Day9Fin end as Day9Finf,  \r\n" +
                                         //"   case when k.samp_name is not null then isnull(Day10Fin,'') + '-' else Day10Fin end as Day10Finf,  \r\n" +
                                         //"   case when l.samp_name is not null then isnull(Day11Fin,'') + '-' else Day11Fin end as Day11Finf,  \r\n" +
                                         //"   case when m.samp_name is not null then isnull(Day12Fin,'') + '-' else Day12Fin end as Day12Finf,  \r\n" +
                                         //"   case when n.samp_name is not null then isnull(Day13Fin,'') + '-' else Day13Fin end as Day13Finf,  \r\n" +
                                         //"   case when o.samp_name is not null then isnull(Day14Fin,'') + '-' else Day14Fin end as Day14Finf,  \r\n" +




                                         "  b.samp_name nn1, c.samp_name nn2, d.samp_name nn3  \r\n" +
                                         "  , isnull(b.samp_name,'')+isnull(c.samp_name,'')+isnull(d.samp_name,'')+isnull(e.samp_name,'')+isnull(f.samp_name,'')+isnull(g.samp_name,'')+isnull(h.samp_name,'')+isnull(i.samp_name,'')+isnull(j.samp_name,'')+isnull(k.samp_name,'')  \r\n" +
                                         " +isnull(l.samp_name,'')+isnull(m.samp_name,'')+isnull(n.samp_name,'')+isnull(o.samp_name,'') ssamp  \r\n" +

                                         "  , isnull(ab.Geo_Name,'')+isnull(ac.Geo_Name,'')+isnull(ad.Geo_Name,'')+isnull(ae.Geo_Name,'')+isnull(af.Geo_Name,'')+isnull(ag.Geo_Name,'')+isnull(ah.Geo_Name,'')+isnull(ai.Geo_Name,'')+isnull(aj.Geo_Name,'')+isnull(ak.Geo_Name,'')  \r\n" +
                                         " +isnull(al.Geo_Name,'')+isnull(am.Geo_Name,'')+isnull(an.Geo_Name,'')+isnull(ao.Geo_Name,'') sgeo \r\n \r\n" +

                                         " ,case when PrintUser is not null then 'Printed '+convert(varchar(10),numprint)  \r\n" +
                                         " when PrintUser is null and NoteUser is not null then 'Note Added'  else ''  end as Flagg  \r\n" +


                                         " from ( \r\n \r\n" +



            " select a.*, b.Name, a.section+':'+name MoName from(  \r\n" +
             " select *, CONVERT(VARCHAR(11),SampDate,106) SampDate1 from tbl_SamplingInfo s  )a,   \r\n" +
             " (select * from section where prodmonth = (select max(prodmonth) from planning where calendardate = CONVERT(VARCHAR(11),getdate(),106))) b  \r\n" +
             " where a.section = b.sectionid  \r\n \r\n" +




                                        ") a  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO1 and samp_name <> '') b on a.description = b.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO2 and samp_name <> '') c on a.description = c.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO3 and samp_name <> '') d on a.description = d.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO4 and samp_name <> '') e on a.description = e.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO5 and samp_name <> '') f on a.description = f.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO6 and samp_name <> '') g on a.description = g.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO7 and samp_name <> '') h on a.description = h.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO8 and samp_name <> '') i on a.description = i.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO9 and samp_name <> '') j on a.description = j.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO10 and samp_name <> '') k on a.description = k.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO11 and samp_name <> '') l on a.description = l.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO12 and samp_name <> '') m on a.description = m.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO13 and samp_name <> '') n on a.description = n.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO14 and samp_name <> '') o on a.description = o.workplace  \r\n" +


                                         "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO1 and Geo_Name <> '') ab on a.description = ab.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO2 and Geo_Name <> '') ac on a.description = ac.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO3 and Geo_Name <> '') ad on a.description = ad.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO4 and Geo_Name <> '') ae on a.description = ae.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO5 and Geo_Name <> '') af on a.description = af.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO6 and Geo_Name <> '') ag on a.description = ag.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO7 and Geo_Name <> '') ah on a.description = ah.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO8 and Geo_Name <> '') ai on a.description = ai.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO9 and Geo_Name <> '') aj on a.description = aj.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO10 and Geo_Name <> '') ak on a.description = ak.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO11 and Geo_Name <> '') al on a.description = al.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO12 and Geo_Name <> '') am on a.description = am.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO13 and Geo_Name <> '') an on a.description = an.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO14 and Geo_Name <> '') ao on a.description = ao.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO15 and Geo_Name <> '') ap on a.description = ap.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO16 and Geo_Name <> '') aq on a.description = aq.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO17 and Geo_Name <> '') ar on a.description = ar.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO18 and Geo_Name <> '') asa on a.description = asa.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO19 and Geo_Name <> '') at on a.description = at.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO20 and Geo_Name <> '') au on a.description = au.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO21 and Geo_Name <> '') av on a.description = av.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO22 and Geo_Name <> '') aw on a.description = aw.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO23 and Geo_Name <> '') ax on a.description = ax.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO24 and Geo_Name <> '') ay on a.description = ay.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO25 and Geo_Name <> '') az on a.description = az.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO26 and Geo_Name <> '') aaa on a.description = aaa.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO27 and Geo_Name <> '') aab on a.description = aab.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[vw_SamplingGrid] where thedate = @NO28 and Geo_Name <> '') aac on a.description = aac.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [tbl_Sampling_WorkOrder_Print] a,   \r\n" +
                                        "(select workplace wp, max(printdate) pp, count(printdate) numprint from [tbl_Sampling_WorkOrder_Print]  where thedate =  \r\n" +
                                        "(select  min(CalendarDate) dd from PLANNING where CalendarDate > GETDATE()  and  datename(dw,calendardate) = 'Friday') \r\n" +
                                        "    group by workplace) b where a.workplace = b.wp and a.printdate = b.pp and thedate = (select  min(CalendarDate) dd from PLANNING where CalendarDate > GETDATE()  \r\n" +
                                      " and  datename(dw,calendardate) = 'Friday')) xxa on a.description = xxa.workplace  \r\n" +


                                      "left outer join  \r\n" +
                                      "(select * from  tbl_Sampling_WorkOrder_Note  a,   \r\n" +
                                      "(select workplace wp, max(notedate) pp from [tbl_Sampling_WorkOrder_Note] group by workplace) b where a.workplace = b.wp   \r\n" +
                                      "and a.notedate = b.pp and thedate = (select  min(CalendarDate) dd   \r\n" +
                                      "from PLANNING where CalendarDate > GETDATE() and  datename(dw,calendardate) = 'Friday') ) xxb on a.description = xxb.workplace  \r\n" +

                                     " order by a.description";

            _dbManWPST.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST.ExecuteInstruction();

            DataTable dt = _dbManWPST.ResultsDataTable;

            //Commented Out
            if (dt.Rows.Count > 0)
            {
                WPlabel.Text = dt.Rows[0]["description"].ToString();
                WPlabel.Visible = false;
            }

            DataSet ds = new DataSet();

            if (ds.Tables.Count > 0)
                ds.Tables.Clear();

            ds.Tables.Add(dt);

            MWDataManager.clsDataAccess _dbManWPSTDetail = new MWDataManager.clsDataAccess();
            _dbManWPSTDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbManWPSTDetail.SqlStatement = "  select * from (  \r\n" +
            //                                "select calendardate aa, w.description,  \r\n" +
            //                                 " CONVERT(VARCHAR(11),calendardate,106) SampDate,  footwall FWWidth, corrcut ChnWidth, Hangwall HWWidth, gt cmgt, SWidth, AllocatedWidth [Alloc. SW], Notes    \r\n" +
            //                                " from [dbo].[SAMPLING_Imported_notes] s, workplace w where s.gmsiwpis = convert(varchar(25),w.gmsiwpid)   \r\n" +

            //                                "union  \r\n" +

            //                                "select null aa, description, null SampDate, null SWidth, null ChnWidth, null cmgt, null HWWidth,   \r\n" +
            //                                "null FWWidth, null [Alloc. SW], null Notes  from [dbo].[tbl_SamplingInfo] where sampdate is null  \r\n" +

            //                                ") a   \r\n" +
            //                                "where description in  (select description from tbl_SamplingInfo)   \r\n" +
            //                                 "and sampdate is not null   \r\n" +

            //                                "order by description, sampdate desc ";


            _dbManWPSTDetail.SqlStatement = "    \r\n" +


            "           declare @NO1 datetime \r\n" +
 "  declare @NO2 datetime \r\n" +
  "  declare @NO3 datetime \r\n" +
  "  declare @NO4 datetime \r\n" +
 "   declare @NO5 datetime \r\n" +
 "   declare @NO6 datetime \r\n" +
 "   declare @NO7 datetime \r\n" +
  "  declare @NO8 datetime \r\n" +
 "   declare @NO9 datetime \r\n" +
 "   declare @NO10 datetime \r\n" +
 "   declare @NO11 datetime \r\n" +
 "   declare @NO12 datetime \r\n" +
 "   declare @NO13 datetime \r\n" +
 "   declare @NO14 datetime \r\n" +
 "   declare @NO15 datetime \r\n" +
 "  declare @NO16 datetime \r\n" +
 "  declare @NO17 datetime \r\n" +
 "  declare @NO18 datetime \r\n" +
 "  declare @NO19 datetime \r\n" +
 "  declare @NO20 datetime \r\n" +
 "  declare @NO21 datetime \r\n" +
 "  declare @NO22 datetime \r\n" +
 "  declare @NO23 datetime \r\n" +
 "  declare @NO24 datetime \r\n" +
 "  declare @NO25 datetime \r\n" +
 "  declare @NO26 datetime \r\n" +
 "  declare @NO27 datetime \r\n" +
 "  declare @NO28 datetime \r\n" +
 "  set @NO1 = ( \r\n" +
 "  select max(calendardate) firstmonday \r\n" +
 "  from planning p where calendardate < getdate() and datename(dw, calendardate) = 'Monday')   \r\n" +
 "  set @NO2 = @NO1 + 1 \r\n" +
 "  set @NO3 = @NO2 + 1 \r\n" +
 "  set @NO4 = @NO3 + 1 \r\n" +
 "  set @NO5 = @NO4 + 1 \r\n" +
 "  set @NO6 = @NO5 + 1 \r\n" +
 "  set @NO7 = @NO6 + 1 \r\n" +
 "  set @NO8 = @NO7 + 1 \r\n" +
 "  set @NO9 = @NO8 + 1 \r\n" +
 "  set @NO10 = @NO9 + 1 \r\n" +
 "  set @NO11 = @NO10 + 1 \r\n" +
 "  set @NO12 = @NO11 + 1 \r\n" +
 "  set @NO13 = @NO12 + 1 \r\n" +
 "  set @NO14 = @NO13 + 1 \r\n" +
 "  set @NO15 = @NO13 + 2 \r\n" +
 "  set @NO16 = @NO13 + 3 \r\n" +
 "  set @NO17 = @NO13 + 4 \r\n" +
 "  set @NO18 = @NO13 + 5 \r\n" +
 "  set @NO19 = @NO13 + 6 \r\n" +
 "  set @NO20 = @NO13 + 7 \r\n" +
 "  set @NO21 = @NO13 + 8 \r\n" +
 "  set @NO22 = @NO13 + 9 \r\n" +
 "  set @NO23 = @NO13 + 10 \r\n" +
 "  set @NO24 = @NO13 + 11 \r\n" +
 "  set @NO25 = @NO13 + 12 \r\n" +
 "  set @NO26 = @NO13 + 13 \r\n" +
 "  set @NO27 = @NO13 + 14 \r\n" +
 "  set @NO28 = @NO13 + 15 \r\n" +


  "    select distinct *from( \r\n" +
 "  select calendardate aa, w.description, \r\n" +
 "   CONVERT(VARCHAR(11), calendardate, 106) SampDate, footwall FWWidth, corrcut ChnWidth, Hangwall HWWidth, gt cmgt, SWidth, AllocatedWidth[Alloc.SW], Notes \r\n" +
 "   from[dbo].[SAMPLING_Imported_notes] s, workplace w where s.gmsiwpis = convert(varchar(25), w.gmsiwpid) \r\n" +
 "  union \r\n" +
 "  select null aa, description, null SampDate, null SWidth, null ChnWidth, null cmgt, null HWWidth, \r\n" +
 "  null FWWidth, null[Alloc.SW], null Notes  from[dbo].[tbl_SamplingInfo] where sampdate is null \r\n" +
 "  ) a \r\n" +
 "  where description in   \r\n" +

 "  ( \r\n" +


  "  select a.Description \r\n" +


 "   from( \r\n" +

  "  select a.*, b.Name, a.section + ':' + name MoName from( \r\n" +
 "   select *, CONVERT(VARCHAR(11), SampDate, 106) SampDate1 from tbl_SamplingInfo s  )a, \r\n" +
 "   (select * from section where prodmonth = (select max(prodmonth) from planning where calendardate = CONVERT(VARCHAR(11), getdate(), 106))) b \r\n" +
  "    where a.section = b.sectionid \r\n" +


 "  ) a \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO1 and samp_name <> '') b on a.description = b.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO2 and samp_name <> '') c on a.description = c.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO3 and samp_name <> '') d on a.description = d.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO4 and samp_name <> '') e on a.description = e.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO5 and samp_name <> '') f on a.description = f.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO6 and samp_name <> '') g on a.description = g.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO7 and samp_name <> '') h on a.description = h.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO8 and samp_name <> '') i on a.description = i.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO9 and samp_name <> '') j on a.description = j.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO10 and samp_name <> '') k on a.description = k.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO11 and samp_name <> '') l on a.description = l.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO12 and samp_name <> '') m on a.description = m.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO13 and samp_name <> '') n on a.description = n.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO14 and samp_name <> '') o on a.description = o.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO1 and Geo_Name <> '') ab on a.description = ab.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO2 and Geo_Name <> '') ac on a.description = ac.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO3 and Geo_Name <> '') ad on a.description = ad.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO4 and Geo_Name <> '') ae on a.description = ae.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO5 and Geo_Name <> '') af on a.description = af.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO6 and Geo_Name <> '') ag on a.description = ag.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO7 and Geo_Name <> '') ah on a.description = ah.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO8 and Geo_Name <> '') ai on a.description = ai.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO9 and Geo_Name <> '') aj on a.description = aj.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO10 and Geo_Name <> '') ak on a.description = ak.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO11 and Geo_Name <> '') al on a.description = al.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO12 and Geo_Name <> '') am on a.description = am.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO13 and Geo_Name <> '') an on a.description = an.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO14 and Geo_Name <> '') ao on a.description = ao.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO15 and Geo_Name <> '') ap on a.description = ap.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO16 and Geo_Name <> '') aq on a.description = aq.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO17 and Geo_Name <> '') ar on a.description = ar.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO18 and Geo_Name <> '') asa on a.description = asa.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO19 and Geo_Name <> '') at on a.description = at.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO20 and Geo_Name <> '') au on a.description = au.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO21 and Geo_Name <> '') av on a.description = av.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO22 and Geo_Name <> '') aw on a.description = aw.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO23 and Geo_Name <> '') ax on a.description = ax.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO24 and Geo_Name <> '') ay on a.description = ay.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO25 and Geo_Name <> '') az on a.description = az.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO26 and Geo_Name <> '') aaa on a.description = aaa.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO27 and Geo_Name <> '') aab on a.description = aab.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[dbo].[vw_SamplingGrid] where thedate = @NO28 and Geo_Name <> '') aac on a.description = aac.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from[tbl_Sampling_WorkOrder_Print] a, \r\n" +
 "  (select workplace wp, max(printdate) pp, count(printdate) numprint from[tbl_Sampling_WorkOrder_Print]  where thedate = \r\n" +
 "  (select  min(CalendarDate) dd from PLANNING where CalendarDate > GETDATE()  and  datename(dw, calendardate) = 'Friday') \r\n" +
 "      group by workplace) b where a.workplace = b.wp and a.printdate = b.pp and thedate = (select  min(CalendarDate) dd from PLANNING where CalendarDate > GETDATE() \r\n" +
 "   and datename(dw, calendardate) = 'Friday')) xxa on a.description = xxa.workplace \r\n" +
 "  left outer join \r\n" +
 "  (select * from tbl_Sampling_WorkOrder_Note a, \r\n" +
 "  (select workplace wp, max(notedate) pp from[tbl_Sampling_WorkOrder_Note] group by workplace) b where a.workplace = b.wp \r\n" +
 "  and a.notedate = b.pp and thedate = (select  min(CalendarDate) dd \r\n" +
 "  from PLANNING where CalendarDate > GETDATE() and datename(dw, calendardate) = 'Friday') ) xxb on a.description = xxb.workplace \r\n" +
 "   --order by a.description \r\n" +


 "  )    \r\n" +
 "  and sampdate is not null \r\n" +
 "  order by description, sampdate desc \r\n";

            _dbManWPSTDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPSTDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPSTDetail.ExecuteInstruction();

            DataTable dtDetail = _dbManWPSTDetail.ResultsDataTable;

            ds.Tables.Add(dtDetail);

            DataColumn keyColumn1 = ds.Tables[0].Columns[2];
            DataColumn foreignKeyColumn1 = ds.Tables[1].Columns[1];
            ds.Relations.Add("CategoriesProducts", keyColumn1, foreignKeyColumn1);


            gridControl6.DataSource = ds.Tables[0];


            GridView cardView1 = new GridView(gridControl6);
            gridControl6.LevelTree.Nodes.Add("CategoriesProducts", cardView1);
            cardView1.ViewCaption = "Sampling History";



            cardView1.PopulateColumns(ds.Tables[1]);

            DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit mEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            mEdit.MaxLength = 50000;
            gridControl6.RepositoryItems.Add(mEdit);
            cardView1.Columns[1].ColumnEdit = mEdit;
            cardView1.OptionsView.RowAutoHeight = true;
            cardView1.OptionsView.ShowIndicator = false;

            cardView1.Columns[0].Visible = false;
            cardView1.Columns[1].Visible = false;
            cardView1.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            cardView1.Columns[2].OptionsColumn.AllowSize = false;
            cardView1.Columns[2].OptionsColumn.AllowEdit = false;
            cardView1.Columns[2].OptionsColumn.FixedWidth = true;
            cardView1.Columns[2].Width = 120;


            cardView1.Columns[3].OptionsColumn.AllowSize = false;
            cardView1.Columns[3].OptionsColumn.AllowEdit = false;
            cardView1.Columns[3].OptionsColumn.FixedWidth = true;
            cardView1.Columns[3].Width = 60;

            cardView1.Columns[4].OptionsColumn.AllowSize = false;
            cardView1.Columns[4].OptionsColumn.AllowEdit = false;
            cardView1.Columns[4].OptionsColumn.FixedWidth = true;
            cardView1.Columns[4].Width = 60;


            cardView1.Columns[5].OptionsColumn.AllowSize = false;
            cardView1.Columns[5].OptionsColumn.AllowEdit = false;
            cardView1.Columns[5].OptionsColumn.FixedWidth = true;
            cardView1.Columns[5].Width = 60;


            cardView1.Columns[6].OptionsColumn.AllowSize = false;
            cardView1.Columns[6].OptionsColumn.AllowEdit = false;
            cardView1.Columns[6].OptionsColumn.FixedWidth = true;
            cardView1.Columns[6].Width = 60;

            cardView1.Columns[7].OptionsColumn.AllowSize = false;
            cardView1.Columns[7].OptionsColumn.AllowEdit = false;
            cardView1.Columns[7].OptionsColumn.FixedWidth = true;
            cardView1.Columns[7].Width = 60;

            cardView1.Columns[8].OptionsColumn.AllowSize = false;
            cardView1.Columns[8].OptionsColumn.AllowEdit = false;
            cardView1.Columns[8].OptionsColumn.FixedWidth = true;
            cardView1.Columns[8].Width = 70;



            col1SecID.FieldName = "MoName";
            col1SecID.Visible = false;
            col1Wp.FieldName = "Description";


            Col1Top20.FieldName = "Top20";
            col1LastSamp.FieldName = "SampDate1";

            Col1Day1.FieldName = "Day1Finf";
            SampDay1.FieldName = "ssamp";
            Geol1.FieldName = "sgeo";
            Status.FieldName = "Flagg";

            Col1Day2.FieldName = "Day2Finf";
            Col1Day3.FieldName = "Day3Finf";
            Col1Day4.FieldName = "Day4Finf";
            Col1Day5.FieldName = "Day5Finf";
            Col1Day6.FieldName = "Day6Finf";
            Col1Day7.FieldName = "Day7Finf";
            Col1Day8.FieldName = "Day8Finf";
            Col1Day9.FieldName = "Day9Finf";
            Col1Day10.FieldName = "Day10Finf";
            Col1Day11.FieldName = "Day11Finf";
            Col1Day12.FieldName = "Day12Finf";
            Col1Day13.FieldName = "Day13Finf";
            Col1Day14.FieldName = "Day14Finf";

            Col1Day15.FieldName = "Day15Finf";
            Col1Day16.FieldName = "Day16Finf";
            Col1Day17.FieldName = "Day17Finf";
            Col1Day18.FieldName = "Day18Finf";
            Col1Day19.FieldName = "Day19Finf";
            Col1Day20.FieldName = "Day20Finf";
            Col1Day21.FieldName = "Day21Finf";


            Col1Day22.FieldName = "Day22Finf";
            Col1Day23.FieldName = "Day23Finf";
            Col1Day24.FieldName = "Day24Finf";
            Col1Day25.FieldName = "Day25Finf";
            Col1Day26.FieldName = "Day26Finf";
            Col1Day27.FieldName = "Day27Finf";
            Col1Day28.FieldName = "Day28Finf";

            bandedGridColumn1.FieldName = "Day1Cycle";
            bandedGridColumn2.FieldName = "Day2Cycle";
            bandedGridColumn3.FieldName = "Day3Cycle";
            bandedGridColumn4.FieldName = "Day4Cycle";
            bandedGridColumn5.FieldName = "Day5Cycle";
            bandedGridColumn6.FieldName = "Day6Cycle";
            bandedGridColumn7.FieldName = "Day7Cycle";
            bandedGridColumn8.FieldName = "Day8Cycle";
            bandedGridColumn9.FieldName = "Day9Cycle";
            bandedGridColumn10.FieldName = "Day10Cycle";

            bandedGridColumn11.FieldName = "Day11Cycle";
            bandedGridColumn12.FieldName = "Day12Cycle";
            bandedGridColumn13.FieldName = "Day13Cycle";
            bandedGridColumn14.FieldName = "Day14Cycle";
            bandedGridColumn15.FieldName = "Day15Cycle";
            bandedGridColumn16.FieldName = "Day16Cycle";
            bandedGridColumn17.FieldName = "Day17Cycle";
            bandedGridColumn18.FieldName = "Day18Cycle";
            bandedGridColumn19.FieldName = "Day19Cycle";
            bandedGridColumn20.FieldName = "Day20Cycle";

            bandedGridColumn21.FieldName = "Day21Cycle";
            bandedGridColumn22.FieldName = "Day22Cycle";
            bandedGridColumn23.FieldName = "Day23Cycle";
            bandedGridColumn24.FieldName = "Day24Cycle";
            bandedGridColumn25.FieldName = "Day25Cycle";
            bandedGridColumn26.FieldName = "Day26Cycle";
            bandedGridColumn27.FieldName = "Day27Cycle";
            bandedGridColumn28.FieldName = "Day28Cycle";




            // FieldInfo fi = typeof(GridColumn).GetField("minWidth", BindingFlags.NonPublic | BindingFlags.Instance);
            Col1Top20.Width = 45;


            MWDataManager.clsDataAccess _dbManWPST1 = new MWDataManager.clsDataAccess();
            _dbManWPST1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPST1.SqlStatement = "  " +

                                        "  declare @NO1 datetime  \r\n" +
                                        " declare @NO2 datetime  \r\n" +
                                        " declare @NO3 datetime  \r\n" +
                                        " declare @NO4 datetime  \r\n" +
                                        " declare @NO5 datetime  \r\n" +
                                        " declare @NO6 datetime  \r\n" +
                                        " declare @NO7 datetime  \r\n" +
                                        " declare @NO8 datetime  \r\n" +
                                        " declare @NO9 datetime  \r\n" +
                                        " declare @NO10 datetime  \r\n" +
                                        " declare @NO11 datetime  \r\n" +
                                        " declare @NO12 datetime  \r\n" +
                                        " declare @NO13 datetime  \r\n" +
                                        " declare @NO14 datetime  \r\n" +

                                        "declare @NO15 datetime  \r\n" +
                                        "declare @NO16 datetime  \r\n" +
                                        "declare @NO17 datetime  \r\n" +
                                        "declare @NO18 datetime  \r\n" +
                                        "declare @NO19 datetime  \r\n" +
                                        "declare @NO20 datetime  \r\n" +
                                        "declare @NO21 datetime  \r\n" +

                                        "declare @NO22 datetime  \r\n" +
                                        "declare @NO23 datetime  \r\n" +
                                        "declare @NO24 datetime  \r\n" +
                                        "declare @NO25 datetime  \r\n" +
                                        "declare @NO26 datetime  \r\n" +
                                        "declare @NO27 datetime  \r\n" +
                                        "declare @NO28 datetime  \r\n" +


                                        " set @NO1 = (  \r\n" +
                                        " select max(calendardate) firstmonday  \r\n" +
                                        " from planning p where calendardate < getdate() and datename(dw,calendardate) = 'Monday')  \r\n" +

                                        " set @NO2 = @NO1+ 1  \r\n" +
                                        " set @NO3 = @NO2+ 1  \r\n" +
                                        " set @NO4 = @NO3+ 1  \r\n" +
                                        " set @NO5 = @NO4+ 1  \r\n" +
                                        " set @NO6 = @NO5+ 1  \r\n" +
                                        " set @NO7 = @NO6+ 1  \r\n" +
                                        " set @NO8 = @NO7+ 1  \r\n" +
                                        " set @NO9 = @NO8+ 1  \r\n" +
                                        " set @NO10 = @NO9+ 1  \r\n" +
                                        " set @NO11 = @NO10+ 1  \r\n" +
                                        " set @NO12 = @NO11+ 1  \r\n" +
                                        " set @NO13 = @NO12+ 1  \r\n" +
                                        " set @NO14 = @NO13+ 1  \r\n" +


                                        "set @NO15 = @NO13+ 2  \r\n" +
                                        "set @NO16 = @NO13+ 3  \r\n" +
                                        "set @NO17 = @NO13+ 4  \r\n" +
                                        "set @NO18 = @NO13+ 5  \r\n" +
                                        "set @NO19 = @NO13+ 6  \r\n" +
                                        "set @NO20 = @NO13+ 7  \r\n" +
                                        "set @NO21 = @NO13+ 8  \r\n" +

                                        "set @NO22 = @NO13+ 9  \r\n" +
                                        "set @NO23 = @NO13+ 10  \r\n" +
                                        "set @NO24 = @NO13+ 11  \r\n" +
                                        "set @NO25 = @NO13+ 12  \r\n" +
                                        "set @NO26 = @NO13+ 13  \r\n" +
                                        "set @NO27 = @NO13+ 14  \r\n" +
                                        "set @NO28 = @NO13+ 15  \r\n" +


                                        " select @NO1 dd, ' ' +substring(datename(dw,@NO1),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO1,106),1,6) a,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO2),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO2,106),1,6) b,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO3),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO3,106),1,6) c,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO4),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO4,106),1,6) d,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO5),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO5,106),1,6) e,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO6),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO6,106),1,6) f,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO7),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO7,106),1,6) g,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO8),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO8,106),1,6) h,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO9),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO9,106),1,6) i,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO10),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO10,106),1,6) j,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO11),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO11,106),1,6) k,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO12),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO12,106),1,6) l,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO13),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO13,106),1,6) m,  \r\n" +
                                        " ' ' +substring(datename(dw,@NO14),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO14,106),1,6) n,  \r\n" +

            " ' ' +substring(datename(dw,@NO15),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO15,106),1,6) o,  \r\n" +
            " ' ' +substring(datename(dw,@NO16),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO16,106),1,6) p,  \r\n" +
            " ' ' +substring(datename(dw,@NO17),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO17,106),1,6) q,  \r\n" +
            " ' ' +substring(datename(dw,@NO18),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO18,106),1,6) r,  \r\n" +
            " ' ' +substring(datename(dw,@NO19),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO19,106),1,6) s,  \r\n" +
            " ' ' +substring(datename(dw,@NO20),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO20,106),1,6) t,  \r\n" +
            " ' ' +substring(datename(dw,@NO21),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO21,106),1,6) u,  \r\n" +
            " ' ' +substring(datename(dw,@NO22),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO22,106),1,6) v,  \r\n" +
            " ' ' +substring(datename(dw,@NO23),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO23,106),1,6) w,  \r\n" +
            " ' ' +substring(datename(dw,@NO24),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO24,106),1,6) x,  \r\n" +
            " ' ' +substring(datename(dw,@NO25),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO25,106),1,6) y,  \r\n" +
            " ' ' +substring(datename(dw,@NO26),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO26,106),1,6) z,  \r\n" +
            " ' ' +substring(datename(dw,@NO27),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO27,106),1,6) za,  \r\n" +
            " ' ' +substring(datename(dw,@NO28),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO28,106),1,6) zb ";

            _dbManWPST1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST1.ExecuteInstruction();

            DataTable dt1 = _dbManWPST1.ResultsDataTable;

            DataSet ds1 = new DataSet();

            if (ds1.Tables.Count > 0)
                ds1.Tables.Clear();

            ds1.Tables.Add(dt1);

            FirstSampdt.Value = Convert.ToDateTime(dt1.Rows[0]["dd"].ToString());


            //gridControl6.DataSource = ds1.Tables[0];

            //Col1Day1.Caption = dt1.Rows[0]["a"].ToString();

            // gridBand16.Caption = dt1.Rows[0]["a"].ToString();

            Col1Day1.Caption = dt1.Rows[0]["a"].ToString();
            Col1Day2.Caption = dt1.Rows[0]["b"].ToString();
            Col1Day3.Caption = dt1.Rows[0]["c"].ToString();
            Col1Day4.Caption = dt1.Rows[0]["d"].ToString();
            Col1Day5.Caption = dt1.Rows[0]["e"].ToString();
            Col1Day6.Caption = dt1.Rows[0]["f"].ToString();
            Col1Day7.Caption = dt1.Rows[0]["g"].ToString();
            Col1Day8.Caption = dt1.Rows[0]["h"].ToString();
            Col1Day9.Caption = dt1.Rows[0]["i"].ToString();
            Col1Day10.Caption = dt1.Rows[0]["j"].ToString();
            Col1Day11.Caption = dt1.Rows[0]["k"].ToString();
            Col1Day12.Caption = dt1.Rows[0]["l"].ToString();
            Col1Day13.Caption = dt1.Rows[0]["m"].ToString();
            Col1Day14.Caption = dt1.Rows[0]["n"].ToString();

            Col1Day15.Caption = dt1.Rows[0]["o"].ToString();
            Col1Day16.Caption = dt1.Rows[0]["p"].ToString();
            Col1Day17.Caption = dt1.Rows[0]["q"].ToString();
            Col1Day18.Caption = dt1.Rows[0]["r"].ToString();
            Col1Day19.Caption = dt1.Rows[0]["s"].ToString();
            Col1Day20.Caption = dt1.Rows[0]["t"].ToString();
            Col1Day21.Caption = dt1.Rows[0]["u"].ToString();
            Col1Day22.Caption = dt1.Rows[0]["v"].ToString();
            Col1Day23.Caption = dt1.Rows[0]["w"].ToString();
            Col1Day24.Caption = dt1.Rows[0]["x"].ToString();
            Col1Day25.Caption = dt1.Rows[0]["y"].ToString();
            Col1Day26.Caption = dt1.Rows[0]["z"].ToString();
            Col1Day27.Caption = dt1.Rows[0]["za"].ToString();
            Col1Day28.Caption = dt1.Rows[0]["zb"].ToString();


            // do workindays

            MWDataManager.clsDataAccess _dbManWPSTWD = new MWDataManager.clsDataAccess();
            _dbManWPSTWD.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPSTWD.SqlStatement = "  " +
                                        "declare @NO1 datetime set @NO1 =  \r\n" +
                                        "(select max(calendardate) firstmonday  \r\n" +
                                        "from planning p where calendardate < getdate() and datename(dw,calendardate) = 'Monday')  \r\n" +

                                        "select calendardate, max(workingday) zz from [dbo].[CALTYPE]  \r\n" +
                                        "where calendardate >= @NO1 and calendarcode not in ('Geo-Drill', 'Mill', 'Mining')  \r\n" +


                                        "and calendardate < @NO1 + 28 group by calendardate order by calendardate ";

            _dbManWPSTWD.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPSTWD.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPSTWD.ExecuteInstruction();

            DataTable dt2 = _dbManWPSTWD.ResultsDataTable;


            int dd = 1;
            foreach (DataRow row in dt2.Rows)
            {
                if (row["zz"].ToString() == "N")
                {
                    if (dd == 1)
                        Col1Day1.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 2)
                        Col1Day2.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 3)
                        Col1Day3.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 4)
                        Col1Day4.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 5)
                        Col1Day5.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 6)
                        Col1Day6.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 7)
                        Col1Day7.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 8)
                        Col1Day8.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 9)
                        Col1Day9.AppearanceCell.BackColor = Color.WhiteSmoke;

                    if (dd == 10)
                        Col1Day10.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 11)
                        Col1Day11.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 12)
                        Col1Day12.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 13)
                        Col1Day13.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 14)
                        Col1Day14.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 15)
                        Col1Day15.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 16)
                        Col1Day16.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 17)
                        Col1Day17.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 18)
                        Col1Day18.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 19)
                        Col1Day19.AppearanceCell.BackColor = Color.WhiteSmoke;

                    if (dd == 20)
                        Col1Day20.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 21)
                        Col1Day21.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 22)
                        Col1Day22.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 23)
                        Col1Day22.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 24)
                        Col1Day24.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 25)
                        Col1Day25.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 26)
                        Col1Day26.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 27)
                        Col1Day27.AppearanceCell.BackColor = Color.WhiteSmoke;
                    if (dd == 28)
                        Col1Day28.AppearanceCell.BackColor = Color.WhiteSmoke;



                }

                dd = dd + 1;
            }


            bandedGridView11.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            bandedGridView11.Columns[0].GroupIndex = 0;

            bandedGridView11.ExpandAllGroups();

        }

        private void bandedGridView11_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            GridView View = sender as GridView;
            string ss = "";

            if (e.Column == View.Columns[2])
            {
                if (!View.GetRowCellValue(e.RowHandle, e.Column).Equals(DBNull.Value))
                {
                    ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                    if (View.GetRowCellValue(e.RowHandle, e.Column).ToString() == "Top20")
                    {
                        e.Graphics.DrawImage(pictureBox1.Image, e.Bounds.X + 2, e.Bounds.Y - 19, 40, 52);
                        e.Handled = false;
                    }
                }


                //e.Appearance.BackColor = Color.Red;
            }


            if (e.Column == View.Columns[19])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss != "")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }


            if (e.Column == View.Columns[20])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss != "")
                    e.Appearance.BackColor = Color.MistyRose;

            }

            if (e.Column == View.Columns[5])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;


            }



            if (e.Column == View.Columns[6])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();

                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }


            if (e.Column == View.Columns[7])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }

            if (e.Column == View.Columns[8])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }




            if (e.Column == View.Columns[9])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }

            if (e.Column == View.Columns[10])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }


            if (e.Column == View.Columns[11])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }


            if (e.Column == View.Columns[12])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }



            if (e.Column == View.Columns[13])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }


            if (e.Column == View.Columns[14])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }


            if (e.Column == View.Columns[15])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }


            if (e.Column == View.Columns[16])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }


            if (e.Column == View.Columns[17])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }




            if (e.Column == View.Columns[18])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }


            if (e.Column == View.Columns[19])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesSamp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                if (ss == "Samp")
                    e.Appearance.BackColor = Color.PaleGoldenrod;

            }



            if (e.Column == View.Columns[5])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;


            }



            if (e.Column == View.Columns[6])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();

                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }


            if (e.Column == View.Columns[7])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }

            if (e.Column == View.Columns[8])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }

            if (e.Column == View.Columns[9])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }



            if (e.Column == View.Columns[10])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }


            if (e.Column == View.Columns[11])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }


            if (e.Column == View.Columns[12])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }

            if (e.Column == View.Columns[13])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }


            if (e.Column == View.Columns[14])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }


            if (e.Column == View.Columns[15])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }


            if (e.Column == View.Columns[16])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }


            if (e.Column == View.Columns[17])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }


            if (e.Column == View.Columns[18])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }


            if (e.Column == View.Columns[19])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesGeol")
                    e.Appearance.BackColor = Color.MistyRose;
                if (ss == "Geol")
                    e.Appearance.BackColor = Color.MistyRose;

            }




            if (e.Column == View.Columns[5])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[6])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[7])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[8])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[9])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[10])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[11])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[12])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[13])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[14])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[15])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[16])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[17])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }

            if (e.Column == View.Columns[18])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }



            if (e.Column == View.Columns[19])
            {
                ss = View.GetRowCellValue(e.RowHandle, e.Column).ToString();
                if (ss == "YesBoth")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                if (ss == "Both")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }


            }



            if (View.GetRowCellValue(e.RowHandle, "Day1Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 5)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day1Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day1Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }


            if (View.GetRowCellValue(e.RowHandle, "Day2Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 6)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day2Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day2Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }




            if (View.GetRowCellValue(e.RowHandle, "Day3Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 7)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day3Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day3Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            if (View.GetRowCellValue(e.RowHandle, "Day4Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 8)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day4Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day4Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }


            if (View.GetRowCellValue(e.RowHandle, "Day5Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 9)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day5Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day5Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            if (View.GetRowCellValue(e.RowHandle, "Day6Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 10)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day6Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day6Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            //return;


            if (View.GetRowCellValue(e.RowHandle, "Day7Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 11)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day7Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day7Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            if (View.GetRowCellValue(e.RowHandle, "Day8Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 12)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day8Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day8Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            if (View.GetRowCellValue(e.RowHandle, "Day9Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 13)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day9Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day9Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            if (View.GetRowCellValue(e.RowHandle, "Day10Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 14)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day10Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day10Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            // return;


            if (View.GetRowCellValue(e.RowHandle, "Day11Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 15)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day11Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day11Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }


            if (View.GetRowCellValue(e.RowHandle, "Day12Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 16)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day12Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day12Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }


            if (View.GetRowCellValue(e.RowHandle, "Day13Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 17)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day13Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day13Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }



            //if (View.GetRowCellValue(e.RowHandle, "Day14Cycle").ToString() != "")
            //{
            //    if (e.Column.AbsoluteIndex == 20)
            //    {
            //        e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day14Cycle").ToString();
            //        e.Appearance.ForeColor = Color.Gainsboro;
            //        if (View.GetRowCellValue(e.RowHandle, "Day14Cycle").ToString() == "BL")
            //            e.Appearance.ForeColor = Color.RosyBrown;

            //    }
            //}

            // return;


            if (View.GetRowCellValue(e.RowHandle, "Day15Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 21)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day15Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day15Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            //return;

            if (View.GetRowCellValue(e.RowHandle, "Day16Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 22)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day16Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day16Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            //return;


            if (View.GetRowCellValue(e.RowHandle, "Day17Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 23)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day17Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day17Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            if (View.GetRowCellValue(e.RowHandle, "Day18Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 24)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day18Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day18Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            //return;

            if (View.GetRowCellValue(e.RowHandle, "Day19Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 25)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day19Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day19Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            if (View.GetRowCellValue(e.RowHandle, "Day20Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 26)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day20Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day20Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            //return;

            if (View.GetRowCellValue(e.RowHandle, "Day21Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 27)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day21Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day21Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }


            if (View.GetRowCellValue(e.RowHandle, "Day22Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 28)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day22Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day22Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            // return;


            if (View.GetRowCellValue(e.RowHandle, "Day23Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 29)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day23Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day23Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            if (View.GetRowCellValue(e.RowHandle, "Day24Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 30)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day24Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day24Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }


            if (View.GetRowCellValue(e.RowHandle, "Day25Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 31)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day25Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day25Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            if (View.GetRowCellValue(e.RowHandle, "Day26Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 32)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day26Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day26Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }


            if (View.GetRowCellValue(e.RowHandle, "Day27Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 33)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day27Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day27Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }

            if (View.GetRowCellValue(e.RowHandle, "Day28Cycle").ToString() != "")
            {
                if (e.Column.AbsoluteIndex == 34)
                {
                    e.DisplayText = View.GetRowCellValue(e.RowHandle, "Day28Cycle").ToString();
                    e.Appearance.ForeColor = Color.Gainsboro;
                    if (View.GetRowCellValue(e.RowHandle, "Day28Cycle").ToString() == "BL")
                        e.Appearance.ForeColor = Color.RosyBrown;

                }
            }
        }

        string SectionID = "";
        string WPID = "";

        private void bandedGridView11_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //MainDblClk = "Y";
            wpSamp = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[1]).ToString();
            status = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[35]).ToString();

            SectionID = procs.ExtractBeforeColon(bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns["MoName"]).ToString()); ;
            WPlabel.Text = wpSamp;
            clickcol = e.Column.ToString();

            ClkCol = e.Column.ToString();

            clickcol = "0";

            if (e.Column.Name.ToString() == "Col1Day1")
                clickcol = "0";
            if (e.Column.Name.ToString() == "Col1Day2")
                clickcol = "1";
            if (e.Column.Name.ToString() == "Col1Day3")
                clickcol = "2";
            if (e.Column.Name.ToString() == "Col1Day4")
                clickcol = "3";
            if (e.Column.Name.ToString() == "Col1Day5")
                clickcol = "4";
            if (e.Column.Name.ToString() == "Col1Day6")
                clickcol = "5";
            if (e.Column.Name.ToString() == "Col1Day7")
                clickcol = "6";
            if (e.Column.Name.ToString() == "Col1Day8")
                clickcol = "7";
            if (e.Column.Name.ToString() == "Col1Day9")
                clickcol = "8";
            if (e.Column.Name.ToString() == "Col1Day10")
                clickcol = "9";
            if (e.Column.Name.ToString() == "Col1Day11")
                clickcol = "10";
            if (e.Column.Name.ToString() == "Col1Day12")
                clickcol = "11";
            if (e.Column.Name.ToString() == "Col1Day13")
                clickcol = "12";
            if (e.Column.Name.ToString() == "Col1Day14")
                clickcol = "13";

            if (e.Column.Name.ToString() == "Col1Day15")
                clickcol = "14";
            if (e.Column.Name.ToString() == "Col1Day16")
                clickcol = "15";
            if (e.Column.Name.ToString() == "Col1Day17")
                clickcol = "16";
            if (e.Column.Name.ToString() == "Col1Day18")
                clickcol = "17";
            if (e.Column.Name.ToString() == "Col1Day19")
                clickcol = "18";
            if (e.Column.Name.ToString() == "Col1Day20")
                clickcol = "19";
            if (e.Column.Name.ToString() == "Col1Day21")
                clickcol = "20";


            if (e.Column.Name.ToString() == "Col1Day22")
                clickcol = "21";
            if (e.Column.Name.ToString() == "Col1Day23")
                clickcol = "22";
            if (e.Column.Name.ToString() == "Col1Day24")
                clickcol = "23";
            if (e.Column.Name.ToString() == "Col1Day25")
                clickcol = "24";
            if (e.Column.Name.ToString() == "Col1Day26")
                clickcol = "25";
            if (e.Column.Name.ToString() == "Col1Day27")
                clickcol = "26";
            if (e.Column.Name.ToString() == "Col1Day28")
                clickcol = "27";

            //string Act = "";
            //MWDataManager.clsDataAccess _dbManWPSTDetail22 = new MWDataManager.clsDataAccess();
            //_dbManWPSTDetail22.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbManWPSTDetail22.SqlStatement = " exec [sp_OCR_WorkplaceDetailPrint]  '" + wpSamp + "' \r\n" +

            //" ";
            //_dbManWPSTDetail22.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbManWPSTDetail22.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbManWPSTDetail22.ExecuteInstruction();
            //string bb = wpSamp.Replace(" ", "_");
            //string ActivityDesc = "Stoping";
            //if (Act == "1")
            //    ActivityDesc = "Development";
            //if (Act == "9")
            //    ActivityDesc = "Ledging";
            ////string aaa = "" + _dbManWPSTDetail22.ResultsDataTable.Rows[0][0].ToString() + " " + bb + " " + activity + " " + ActivityDesc + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0][4].ToString() + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0][5].ToString() + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0][6].ToString().Replace(" ", "_") + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0][7].ToString() ;
            //string aaa = "" + _dbManWPSTDetail22.ResultsDataTable.Rows[0][0].ToString() + " " + bb + " " + Act + " " + ActivityDesc + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0][4].ToString().Replace(" ", "_") + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0][5].ToString().Replace(" ", "_") + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0][6].ToString().Replace(" ", "_") + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0][7].ToString().Replace(" ", "_") + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0][8].ToString().Replace(" ", "_") + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0][9].ToString().Replace(" ", "_") + " " + _dbManWPSTDetail22.ResultsDataTable.Rows[0]["MOSectionID"].ToString().Replace(" ", "_") + " " + System.DateTime.Today.ToShortDateString().Replace("/", "") + " " + System.DateTime.Today.ToShortDateString().Replace("/", "") + " " + System.DateTime.Today.ToShortDateString().Replace("/", "") + " " + System.DateTime.Today.ToShortDateString().Replace("/","") + "";
            //Param = aaa;

            MWDataManager.clsDataAccess _dbManbarcode = new MWDataManager.clsDataAccess();
            _dbManbarcode.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManbarcode.SqlStatement = " Select Description, convert(decimal(18,0), activity) activity, WorkplaceID from workplace where Description = '" + wpSamp + "' ";
            _dbManbarcode.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManbarcode.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManbarcode.ResultsTableName = "Bcode";
            _dbManbarcode.ExecuteInstruction();

            WPID = _dbManbarcode.ResultsDataTable.Rows[0]["WorkplaceID"].ToString();
            DataSet ReportDatasetReportBar = new DataSet();
            ReportDatasetReportBar.Tables.Add(_dbManbarcode.ResultsDataTable);

            string activity = _dbManbarcode.ResultsDataTable.Rows[0]["activity"].ToString();

            string bb = _dbManbarcode.ResultsDataTable.Rows[0]["Description"].ToString().Replace(" ", "_");
            string ActivityDesc = "Stoping";
            if (activity == "1")
                ActivityDesc = "Development";
            if (activity == "9")
                ActivityDesc = "Ledging";

            string aaa = "";
            MWDataManager.clsDataAccess _dbManWPSTDetailSB = new MWDataManager.clsDataAccess();
            _dbManWPSTDetailSB.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPSTDetailSB.SqlStatement = " select Name SBNAME, EmployeeNo SBEMPNO, '' aa, substring('" + SectionID + "',0,4) MOSec from Section  \r\n" +
                                            " where sectionID = substring('" + SectionID + "',0,5)   \r\n" +
                                            " and prodmonth = (Select CurrentProductionMonth from sysset)  \r\n" +
                                            " and Hierarchicalid = 5  \r\n" +
                 " ";
            _dbManWPSTDetailSB.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPSTDetailSB.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPSTDetailSB.ExecuteInstruction();


            MWDataManager.clsDataAccess _dbManWPSTDetailMO = new MWDataManager.clsDataAccess();
            _dbManWPSTDetailMO.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPSTDetailMO.SqlStatement = " select Name MONAME, EmployeeNo MOEMPNO from Section  \r\n" +
                                            " where sectionID = substring('" + SectionID + "',0,4)  \r\n" +
                                            " and prodmonth = (Select CurrentProductionMonth from sysset)  \r\n" +
                                            " and Hierarchicalid = 4  \r\n" +
                 " ";
            _dbManWPSTDetailMO.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPSTDetailMO.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPSTDetailMO.ExecuteInstruction();

            try
            {

                aaa = "" + WPID + " " + bb + " " + activity + " " + ActivityDesc + " " + _dbManWPSTDetailSB.ResultsDataTable.Rows[0]["aa"].ToString().Replace(" ", "_") + " " + _dbManWPSTDetailSB.ResultsDataTable.Rows[0]["MOSec"].ToString().Replace(" ", "_") + " " + _dbManWPSTDetailSB.ResultsDataTable.Rows[0]["SBEMPNO"].ToString().Replace(" ", "_") + " " + _dbManWPSTDetailSB.ResultsDataTable.Rows[0]["SBNAME"].ToString().Replace(" ", "_") + " " + _dbManWPSTDetailMO.ResultsDataTable.Rows[0]["MOEMPNO"].ToString().Replace(" ", "_") + " " + _dbManWPSTDetailMO.ResultsDataTable.Rows[0]["MONAME"].ToString().Replace(" ", "_") + " " + _dbManWPSTDetailSB.ResultsDataTable.Rows[0]["MOSec"].ToString() + " " + _dbManWPSTDetailSB.ResultsDataTable.Rows[0]["MOSec"].ToString() + " " + System.DateTime.Today.ToShortDateString().Replace("/", "") + "";
            }
            catch { return; }
            Param = aaa;


        }

        string Param = "";

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Commented Out
            //if ( == "N")
            //{
            //    MessageBox.Show("Currently " + clsUserInfo.UserName + " doesn't have the Authoirsation to generate Work Orders", "Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //FrmNotes FrmScanner = new FrmNotes();
            //FrmScanner._theSystemDBTag = theSystemDBTag;
            //FrmScanner._UserCurrentInfoConnection = UserCurrentInfo.Connection;
            //FrmScanner.WPlabel.Text = WPlabel.Text;
            //FrmScanner.ShowDialog();

            try
            {

                //Write Checklist id to textfile


                string path = @"C:\Mineware\Syncromine\OCRForm.Txt";


                using (StreamWriter writetext = new StreamWriter(path))
                {
                    writetext.WriteLine("1273");//Sampling Work Order
                }




                ///
                Process Shec = new Process();
                Shec.StartInfo.FileName = @"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe";
                Shec.StartInfo.Arguments = Param;
                Shec.StartInfo.UseShellExecute = false;
                Shec.StartInfo.CreateNoWindow = true;
                Shec.StartInfo.RedirectStandardOutput = true;
                Shec.StartInfo.Verb = "runas";
                Shec.Start();
                Shec.WaitForExit();
                // Shec.WaitForExit();




            }
            catch
            {
                MessageBox.Show(@"C:\Mineware\Syncromine\OCRScheduling\OCRApp\OCRWebApiAccess.exe", "Cant Find OCR Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            LoadSampling();

        }

        private void bandedGridView11_DoubleClick(object sender, EventArgs e)
        {
            if (ClkCol == "Status")
            {
                if (status == "")
                {
                    simpleButton2_Click(null, null);
                }
                else
                {
                    simpleButton2_Click(null, null);
                }

            }


            if (ClkCol != "Workplace" && ClkCol != "Status")
            {
                frmSampleSched RepFrm  = new frmSampleSched();
                RepFrm._theSystemDBTag = theSystemDBTag;
                RepFrm._UserCurrentInfoConnection = UserCurrentInfo.Connection;
                RepFrm.WpLbl.Text = wpSamp;
                    RepFrm.WPLbl2.Text = wpSamp;
                    RepFrm.Starttm.Value = FirstSampdt.Value;
                    RepFrm.Samp2Pnl.Visible = false;
                    RepFrm.SampPnl.Visible = true;
                RepFrm.SampPnl.BringToFront();
                RepFrm.SampPnl.Dock = DockStyle.Fill;
                    RepFrm.Endtm.Value = FirstSampdt.Value.AddDays(13);
                    RepFrm.SSAMPdt.Value = FirstSampdt.Value.AddDays(Convert.ToInt32(clickcol));
                    RepFrm.DateLbl.Text = RepFrm.SSAMPdt.Value.ToString("dd MMM yyyy");
                    RepFrm.DateLbl2.Text = RepFrm.SSAMPdt.Value.ToString("dd MMM yyyy");
                    RepFrm.ShowDialog();
            }
            //MainDblClk = "N";

            if (ClkCol == "Workplace")
            {
                frmSampleSched RepFrm =  new frmSampleSched();
                RepFrm._theSystemDBTag = theSystemDBTag;
                RepFrm._UserCurrentInfoConnection = UserCurrentInfo.Connection;
                RepFrm.WPLabel2.Text = wpSamp;
                    //RepFrm.Starttm.Value = FirstSampdt.Value;
                    RepFrm.SampPnl.Visible = false;
                    RepFrm.Samp2Pnl.Visible = true;
                RepFrm.SampPnl.BringToFront();
                RepFrm.Samp2Pnl.Dock = DockStyle.Fill;
                    //RepFrm.Endtm.Value = FirstSampdt.Value.AddDays(13);
                    //RepFrm.SSAMPdt.Value = FirstSampdt.Value.AddDays(Convert.ToInt32(clickcol));
                    //RepFrm.DateLbl.Text = RepFrm.SSAMPdt.Value.ToString("dd MMM yyyy");
                    RepFrm.ShowDialog();
            }

            LoadSampling();
        }

        private void Print1Btn_Click(object sender, EventArgs e)
        {
            //gridControl6.ShowPrintPreview();
            MWDataManager.clsDataAccess _dbManWPST = new MWDataManager.clsDataAccess();
            _dbManWPST.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPST.SqlStatement = " declare @NO1 datetime \r\n" +
                                        "declare @NO2 datetime  \r\n" +
                                        "declare @NO3 datetime  \r\n" +
                                        "declare @NO4 datetime  \r\n" +
                                        "declare @NO5 datetime  \r\n" +
                                        "declare @NO6 datetime  \r\n" +
                                        "declare @NO7 datetime  \r\n" +
                                        "declare @NO8 datetime  \r\n" +
                                        "declare @NO9 datetime  \r\n" +
                                        "declare @NO10 datetime  \r\n" +
                                        "declare @NO11 datetime  \r\n" +
                                        "declare @NO12 datetime  \r\n" +
                                        "declare @NO13 datetime  \r\n" +
                                        "declare @NO14 datetime  \r\n" +

                                        "set @NO1 = (  \r\n" +
                                        "select max(calendardate) firstmonday  \r\n" +
                                        "from planning p where calendardate < getdate() and datename(dw,calendardate) = 'Monday')  \r\n" +

                                        "set @NO2 = @NO1+ 1  \r\n" +
                                        "set @NO3 = @NO2+ 1  \r\n" +
                                        "set @NO4 = @NO3+ 1  \r\n" +
                                        "set @NO5 = @NO4+ 1  \r\n" +
                                        "set @NO6 = @NO5+ 1  \r\n" +
                                        "set @NO7 = @NO6+ 1  \r\n" +
                                        "set @NO8 = @NO7+ 1  \r\n" +
                                        "set @NO9 = @NO8+ 1  \r\n" +
                                        "set @NO10 = @NO9+ 1  \r\n" +
                                        "set @NO11 = @NO10+ 1  \r\n" +
                                        "set @NO12 = @NO11+ 1  \r\n" +
                                        "set @NO13 = @NO12+ 1  \r\n" +
                                        "set @NO14 = @NO13+ 1  \r\n" +

                                         " select '" + SysSettings.Banner + "' banner, a.*,  \r\n" +
                                         " case  \r\n" +
                                         " when b.samp_name is not null and ab.Geo_Name is null then isnull(Day1Fin,'')  + 'Samp'  \r\n" +
                                         " when b.samp_name is not null and ab.Geo_Name is not null then isnull(Day1Fin,'')  + 'Both'   \r\n" +
                                         " when b.samp_name is null and ab.Geo_Name is not null then isnull(Day1Fin,'')  + 'Geol'   \r\n" +
                                         " else Day1Fin end as Day1Finf,  \r\n" +

                                          " case  \r\n" +
                                         " when c.samp_name is not null and ac.Geo_Name is null then isnull(Day2Fin,'')  + 'Samp'  \r\n" +
                                         " when c.samp_name is not null and ac.Geo_Name is not null then isnull(Day2Fin,'')  + 'Both'   \r\n" +
                                         " when c.samp_name is null and ac.Geo_Name is not null then isnull(Day2Fin,'')  + 'Geol'   \r\n" +
                                         " else Day2Fin end as Day2Finf,  \r\n" +

                                          " case  \r\n" +
                                         " when d.samp_name is not null and ad.Geo_Name is null then isnull(Day3Fin,'')  + 'Samp'  \r\n" +
                                         " when d.samp_name is not null and ad.Geo_Name is not null then isnull(Day3Fin,'')  + 'Both'   \r\n" +
                                         " when d.samp_name is null and ad.Geo_Name is not null then isnull(Day3Fin,'')  + 'Geol'   \r\n" +
                                         " else Day3Fin end as Day3Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when e.samp_name is not null and ae.Geo_Name is null then isnull(Day4Fin,'')  + 'Samp'  \r\n" +
                                         " when e.samp_name is not null and ae.Geo_Name is not null then isnull(Day4Fin,'')  + 'Both'   \r\n" +
                                         " when e.samp_name is null and ae.Geo_Name is not null then isnull(Day4Fin,'')  + 'Geol'   \r\n" +
                                         " else Day4Fin end as Day4Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when f.samp_name is not null and af.Geo_Name is null then isnull(Day5Fin,'')  + 'Samp'  \r\n" +
                                         " when f.samp_name is not null and af.Geo_Name is not null then isnull(Day5Fin,'')  + 'Both'   \r\n" +
                                         " when f.samp_name is null and af.Geo_Name is not null then isnull(Day5Fin,'')  + 'Geol'   \r\n" +
                                         " else Day5Fin end as Day5Finf,  \r\n" +



                                         " case  \r\n" +
                                         " when g.samp_name is not null and ag.Geo_Name is null then isnull(Day6Fin,'')  + 'Samp'  \r\n" +
                                         " when g.samp_name is not null and ag.Geo_Name is not null then isnull(Day6Fin,'')  + 'Both'   \r\n" +
                                         " when g.samp_name is null and ag.Geo_Name is not null then isnull(Day6Fin,'')  + 'Geol'   \r\n" +
                                         " else Day6Fin end as Day6Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when h.samp_name is not null and ah.Geo_Name is null then isnull(Day7Fin,'')  + 'Samp'  \r\n" +
                                         " when h.samp_name is not null and ah.Geo_Name is not null then isnull(Day7Fin,'')  + 'Both'   \r\n" +
                                         " when h.samp_name is null and ah.Geo_Name is not null then isnull(Day7Fin,'')  + 'Geol'   \r\n" +
                                         " else Day7Fin end as Day7Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when i.samp_name is not null and ai.Geo_Name is null then isnull(Day8Fin,'')  + 'Samp'  \r\n" +
                                         " when i.samp_name is not null and ai.Geo_Name is not null then isnull(Day8Fin,'')  + 'Both'   \r\n" +
                                         " when i.samp_name is null and ai.Geo_Name is not null then isnull(Day8Fin,'')  + 'Geol'   \r\n" +
                                         " else Day8Fin end as Day8Finf,  \r\n" +

                                         " case  \r\n" +
                                         " when j.samp_name is not null and aj.Geo_Name is null then isnull(Day9Fin,'')  + 'Samp'  \r\n" +
                                         " when j.samp_name is not null and aj.Geo_Name is not null then isnull(Day9Fin,'')  + 'Both'   \r\n" +
                                         " when j.samp_name is null and aj.Geo_Name is not null then isnull(Day9Fin,'')  + 'Geol'   \r\n" +
                                         " else Day9Fin end as Day9Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when k.samp_name is not null and ak.Geo_Name is null then isnull(Day10Fin,'')  + 'Samp'  \r\n" +
                                         " when k.samp_name is not null and ak.Geo_Name is not null then isnull(Day10Fin,'')  + 'Both'   \r\n" +
                                         " when k.samp_name is null and ak.Geo_Name is not null then isnull(Day10Fin,'')  + 'Geol'   \r\n" +
                                         " else Day10Fin end as Day10Finf,  \r\n" +


                                         " case  \r\n" +
                                         " when l.samp_name is not null and al.Geo_Name is null then isnull(Day11Fin,'')  + 'Samp'  \r\n" +
                                         " when l.samp_name is not null and al.Geo_Name is not null then isnull(Day11Fin,'')  + 'Both'   \r\n" +
                                         " when l.samp_name is null and al.Geo_Name is not null then isnull(Day11Fin,'')  + 'Geol'   \r\n" +
                                         " else Day11Fin end as Day11Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when m.samp_name is not null and am.Geo_Name is null then isnull(Day12Fin,'')  + 'Samp'  \r\n" +
                                         " when m.samp_name is not null and am.Geo_Name is not null then isnull(Day12Fin,'')  + 'Both'   \r\n" +
                                         " when m.samp_name is null and am.Geo_Name is not null then isnull(Day12Fin,'')  + 'Geol'   \r\n" +
                                         " else Day12Fin end as Day12Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when n.samp_name is not null and an.Geo_Name is null then isnull(Day13Fin,'')  + 'Samp'  \r\n" +
                                         " when n.samp_name is not null and an.Geo_Name is not null then isnull(Day13Fin,'')  + 'Both'   \r\n" +
                                         " when n.samp_name is null and an.Geo_Name is not null then isnull(Day13Fin,'')  + 'Geol'   \r\n" +
                                         " else Day13Fin end as Day13Finf,  \r\n" +


                                          " case  \r\n" +
                                         " when o.samp_name is not null and ao.Geo_Name is null then isnull(Day14Fin,'')  + 'Samp'  \r\n" +
                                         " when o.samp_name is not null and ao.Geo_Name is not null then isnull(Day14Fin,'')  + 'Both'   \r\n" +
                                         " when o.samp_name is null and ao.Geo_Name is not null then isnull(Day14Fin,'')  + 'Geol'   \r\n" +
                                         " else Day14Fin end as Day14Finf,  \r\n" +

                                         //"   case when c.samp_name is not null then isnull(Day2Fin,'') + '-' else Day2Fin end as Day2Finf,  \r\n" +

                                         //"   case when d.samp_name is not null then isnull(Day3Fin,'') + '-' else Day3Fin end as Day3Finf,  \r\n" +
                                         //"   case when e.samp_name is not null then isnull(Day4Fin,'') + '-' else Day4Fin end as Day4Finf,  \r\n" +
                                         //"   case when f.samp_name is not null then isnull(Day5Fin,'') + '-' else Day5Fin end as Day5Finf,  \r\n" +
                                         //"   case when g.samp_name is not null then isnull(Day6Fin,'') + '-' else Day6Fin end as Day6Finf,  \r\n" +
                                         //"   case when h.samp_name is not null then isnull(Day7Fin,'') + '-' else Day7Fin end as Day7Finf,  \r\n" +
                                         //"   case when i.samp_name is not null then isnull(Day8Fin,'') + '-' else Day8Fin end as Day8Finf,  \r\n" +
                                         //"   case when j.samp_name is not null then isnull(Day9Fin,'') + '-' else Day9Fin end as Day9Finf,  \r\n" +
                                         //"   case when k.samp_name is not null then isnull(Day10Fin,'') + '-' else Day10Fin end as Day10Finf,  \r\n" +
                                         //"   case when l.samp_name is not null then isnull(Day11Fin,'') + '-' else Day11Fin end as Day11Finf,  \r\n" +
                                         //"   case when m.samp_name is not null then isnull(Day12Fin,'') + '-' else Day12Fin end as Day12Finf,  \r\n" +
                                         //"   case when n.samp_name is not null then isnull(Day13Fin,'') + '-' else Day13Fin end as Day13Finf,  \r\n" +
                                         //"   case when o.samp_name is not null then isnull(Day14Fin,'') + '-' else Day14Fin end as Day14Finf,  \r\n" +




                                         "  b.samp_name nn1, c.samp_name nn2, d.samp_name nn3  \r\n" +
                                         "  , isnull(b.samp_name,'')+isnull(c.samp_name,'')+isnull(d.samp_name,'')+isnull(f.samp_name,'')+isnull(g.samp_name,'')+isnull(h.samp_name,'')+isnull(i.samp_name,'')+isnull(j.samp_name,'')+isnull(k.samp_name,'')  \r\n" +
                                         " +isnull(l.samp_name,'')+isnull(m.samp_name,'')+isnull(n.samp_name,'')+isnull(o.samp_name,'') ssamp  \r\n" +

                                         "  , isnull(ab.Geo_Name,'')+isnull(ac.Geo_Name,'')+isnull(ad.Geo_Name,'')+isnull(af.Geo_Name,'')+isnull(ag.Geo_Name,'')+isnull(ah.Geo_Name,'')+isnull(ai.Geo_Name,'')+isnull(aj.Geo_Name,'')+isnull(k.Geo_Name,'')  \r\n" +
                                         " +isnull(al.Geo_Name,'')+isnull(am.Geo_Name,'')+isnull(an.Geo_Name,'')+isnull(ao.Geo_Name,'') sgeo  \r\n" +


                                         " from (  \r\n" +



            " select a.*, b.Name, a.section+':'+name MoName from(  \r\n" +
             " select *, CONVERT(VARCHAR(11),SampDate,106) SampDate1, CONVERT(VARCHAR(11),SampDate,111) SampDate2  from tbl_SamplingInfo s  )a,   \r\n" +
             " (select * from section where prodmonth = (select max(prodmonth) from planning where calendardate = CONVERT(VARCHAR(11),getdate(),106))) b  \r\n" +
             " where a.section = b.sectionid  \r\n" +




                                        ") a  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO1 and samp_name <> '') b on a.description = b.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO2 and samp_name <> '') c on a.description = c.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO3 and samp_name <> '') d on a.description = d.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO4 and samp_name <> '') e on a.description = e.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO5 and samp_name <> '') f on a.description = f.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO6 and samp_name <> '') g on a.description = g.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO7 and samp_name <> '') h on a.description = h.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO8 and samp_name <> '') i on a.description = i.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO9 and samp_name <> '') j on a.description = j.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO10 and samp_name <> '') k on a.description = k.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO11 and samp_name <> '') l on a.description = l.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO12 and samp_name <> '') m on a.description = m.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO13 and samp_name <> '') n on a.description = n.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO14 and samp_name <> '') o on a.description = o.workplace  \r\n" +


                                         "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO1 and Geo_Name <> '') ab on a.description = ab.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO2 and Geo_Name <> '') ac on a.description = ac.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO3 and Geo_Name <> '') ad on a.description = ad.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO4 and Geo_Name <> '') ae on a.description = ae.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO5 and Geo_Name <> '') af on a.description = af.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO6 and Geo_Name <> '') ag on a.description = ag.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO7 and Geo_Name <> '') ah on a.description = ah.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO8 and Geo_Name <> '') ai on a.description = ai.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO9 and Geo_Name <> '') aj on a.description = aj.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO10 and Geo_Name <> '') ak on a.description = ak.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO11 and Geo_Name <> '') al on a.description = al.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO12 and Geo_Name <> '') am on a.description = am.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO13 and Geo_Name <> '') an on a.description = an.workplace  \r\n" +

                                        "left outer join  \r\n" +
                                        "(select * from [dbo].[tb_SamplingGrid] where thedate = @NO14 and Geo_Name <> '') ao on a.description = ao.workplace  \r\n" +



             " order by section ";

            _dbManWPST.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST.ExecuteInstruction();

            DataTable dt = _dbManWPST.ResultsDataTable;

            DataSet ds = new DataSet();

            ds.Tables.Add(_dbManWPST.ResultsDataTable);

            MWDataManager.clsDataAccess _dbManWPSTDetail = new MWDataManager.clsDataAccess();
            _dbManWPSTDetail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPSTDetail.SqlStatement = "  select *, '' Remarks from (  \r\n" +
                                            "select calendardate aa, w.description, CONVERT(VARCHAR(11),calendardate,106) SampDate, SWidth, corrcut ChnWidth, gt cmgt, Hangwall HWWidth,   \r\n" +
                                            "footwall FWWidth from [dbo].[SAMPLING_Imported] s, workplace w where s.gmsiwpis = w.gmsiwpid  \r\n" +

                                            "union  \r\n" +


                                            "select null aa, description, null SampDate, null SWidth, null ChnWidth, null cmgt, null HWWidth,   \r\n" +
                                            "null FWWidth  from [dbo].[tbl_SamplingInfo] where sampdate is null  \r\n" +

                                            ") a   \r\n" +
                                            "where description in  (select description from tbl_SamplingInfo)   \r\n" +


                                            "order by description, sampdate desc ";

            _dbManWPSTDetail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPSTDetail.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPSTDetail.ExecuteInstruction();

            DataTable dtDetail = _dbManWPSTDetail.ResultsDataTable;

            ds.Tables.Add(dtDetail);

            MWDataManager.clsDataAccess _dbManWPST1 = new MWDataManager.clsDataAccess();
            _dbManWPST1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWPST1.SqlStatement = "   \r\n" +

                                        "  declare @NO1 datetime  \r\n" +
                                        " declare @NO2 datetime  \r\n" +
                                        " declare @NO3 datetime  \r\n" +
                                        " declare @NO4 datetime  \r\n" +
                                        " declare @NO5 datetime  \r\n" +
                                        " declare @NO6 datetime  \r\n" +
                                        " declare @NO7 datetime  \r\n" +
                                        " declare @NO8 datetime  \r\n" +
                                        " declare @NO9 datetime  \r\n" +
                                        " declare @NO10 datetime  \r\n" +
                                        " declare @NO11 datetime  \r\n" +
                                        " declare @NO12 datetime  \r\n" +
                                        " declare @NO13 datetime  \r\n" +
                                        " declare @NO14 datetime  \r\n" +

                                        " set @NO1 = (  \r\n" +
                                        " select max(calendardate) firstmonday  \r\n" +
                                        " from planning p where calendardate < getdate() and datename(dw,calendardate) = 'Monday')  \r\n" +

                                        " set @NO2 = @NO1+ 1  \r\n" +
                                        " set @NO3 = @NO2+ 1  \r\n" +
                                        " set @NO4 = @NO3+ 1  \r\n" +
                                        " set @NO5 = @NO4+ 1  \r\n" +
                                        " set @NO6 = @NO5+ 1  \r\n" +
                                        " set @NO7 = @NO6+ 1  \r\n" +
                                        " set @NO8 = @NO7+ 1  \r\n" +
                                        " set @NO9 = @NO8+ 1  \r\n" +
                                        " set @NO10 = @NO9+ 1  \r\n" +
                                        " set @NO11 = @NO10+ 1  \r\n" +
                                        " set @NO12 = @NO11+ 1  \r\n" +
                                        " set @NO13 = @NO12+ 1  \r\n" +
                                        " set @NO14 = @NO13+ 1  \r\n" +


                                        " select @NO1 dd, substring(datename(dw,@NO1),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO1,106),1,6) a,  \r\n" +
                                        " substring(datename(dw,@NO2),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO2,106),1,6) b,  \r\n" +
                                        " substring(datename(dw,@NO3),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO3,106),1,6) c,  \r\n" +
                                        " substring(datename(dw,@NO4),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO4,106),1,6) d,  \r\n" +
                                        " substring(datename(dw,@NO5),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO5,106),1,6) e,  \r\n" +
                                        " substring(datename(dw,@NO6),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO6,106),1,6) f,  \r\n" +
                                        " substring(datename(dw,@NO7),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO7,106),1,6) g,  \r\n" +
                                        " substring(datename(dw,@NO8),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO8,106),1,6) h,  \r\n" +
                                        " substring(datename(dw,@NO9),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO9,106),1,6) i,  \r\n" +
                                        " substring(datename(dw,@NO10),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO10,106),1,6) j,  \r\n" +
                                        " substring(datename(dw,@NO11),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO11,106),1,6) k,  \r\n" +
                                        " substring(datename(dw,@NO12),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO12,106),1,6) l,  \r\n" +
                                        " substring(datename(dw,@NO13),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO13,106),1,6) m,  \r\n" +
                                        " substring(datename(dw,@NO14),1,3) + ' '+ substring(CONVERT(VARCHAR(11),@NO14,106),1,6) n ";

            _dbManWPST1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST1.ResultsTableName = "Table3";
            _dbManWPST1.ExecuteInstruction();

            DataTable dt1 = _dbManWPST1.ResultsDataTable;

            DataSet ds1 = new DataSet();

            if (ds1.Tables.Count > 0)
                ds1.Tables.Clear();

            ds1.Tables.Add(dt1);

            WPNewRep.RegisterData(ds);
            WPNewRep.RegisterData(ds1);

            WPNewRep.Load(TGlobalItems.ReportsFolder + "\\PrintNewSamp.frx");
            //WPNewRep.Design();
            WPNewRep.Show();

        }

        private void PrintPreviewbtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Print1Btn_Click(null, null);
        }

        private void GenerateWorkorderbtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //simpleButton2_Click(null, null);

            //using Mineware.Systems.Production.SysAdminScreens.SetupCycles;

            if (WPID == "")
            {
                MessageBox.Show(@"Error", "Please select a workplace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SamplingChecklistAdditInfo Propfrm = new SamplingChecklistAdditInfo();

            Propfrm._theSystemDBTag = theSystemDBTag;
            Propfrm._UserCurrentInfoConnection = UserCurrentInfo.Connection;
            Propfrm.label4.Text = WPID;
            Propfrm.label6.Text = wpSamp;
            Propfrm.label5.Text = SectionID;

            Propfrm.ShowDialog();
        }

        private void RCRockEngineering_Click(object sender, EventArgs e)
        {

        }
    }
}
