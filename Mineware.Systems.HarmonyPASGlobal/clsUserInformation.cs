using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Mineware.Systems.ProductionGlobal
{
   public  static class clsUserInfo
    {

        #region class properties and globals

        private static string m_UserID = "";
        private static string m_UserName = "";
        private static string m_UserBookSection = "";
        private static int m_Hier = 0;
        private static string m_Tram = "";
        private static string m_Hoist = "";
        private static string m_mill = "";
        private static string m_book = "";
        private static string m_dropraise = "N";
        private static string m_sys = "N";
        private static string m_plan = "N";
        private static string m_samp = "N";
        private static string m_Surv = "N";
        private static string m_Expl = "N";
        private static string m_BackDateBooking = "N";
        private static string m_BackDateBookingDays = "";
        private static bool bSingleDB = true;
        private static DataTable tblDBs;
        private static string m_WPProduction = "N";
        private static string m_WPSurface = "N";
        private static string m_WPUnderground = "N";
        private static string m_WPEditName = "N";
        private static string m_WPEditAttribute = "N";
        private static string m_WPClassify = "N";
        private static string m_WPGiveAccess = "N";


        public static string UserID { get { return m_UserID; } set { m_UserID = value; } }
        public static string UserName { get { return m_UserName; } set { m_UserName = value; } }
        public static string UserBookSection { get { return m_UserBookSection; } set { m_UserBookSection = value; } }
        public static int Hier { get { return m_Hier; } set { m_Hier = value; } }

        public static string Tram { get { return m_Tram; } set { m_Tram = value; } }
        public static string Hoist { get { return m_Hoist; } set { m_Hoist = value; } }
        public static string mill { get { return m_mill; } set { m_mill = value; } }
        public static string book { get { return m_book; } set { m_book = value; } }
        public static string dropraise { get { return m_dropraise; } set { m_dropraise = value; } }
        public static string sys { get { return m_sys; } set { m_sys = value; } }
        public static string plan { get { return m_plan; } set { m_plan = value; } }
        public static string samp { get { return m_samp; } set { m_samp = value; } }
        public static string Surv { get { return m_Surv; } set { m_Surv = value; } }
        public static string Expl { get { return m_Expl; } set { m_Expl = value; } }
        public static string BackDateBooking { get { return m_BackDateBooking; } set { m_BackDateBooking = value; } }
        public static string BackDateBookingDays { get { return m_BackDateBookingDays; } set { m_BackDateBookingDays = value; } }
        public static bool SingleDB { get { return bSingleDB; } set { bSingleDB = value; } }
        public static DataTable DBs { get { return tblDBs; } set { tblDBs = value; } }
        public static string WPProduction { get { return m_WPProduction; } set { m_WPProduction = value; } }
        public static string WPSurface { get { return m_WPSurface; } set { m_WPSurface = value; } }
        public static string WPUnderground { get { return m_WPUnderground; } set { m_WPUnderground = value; } }
        public static string WPEditName { get { return m_WPEditName; } set { m_WPEditName = value; } }
        public static string WPEditAttribute { get { return m_WPEditAttribute; } set { m_WPEditAttribute = value; } }
        public static string WPClassify { get { return m_WPClassify; } set { m_WPClassify = value; } }
        public static string WPGiveAccess { get { return m_WPGiveAccess; } set { m_WPGiveAccess = value; } }


        #endregion class properties and globals
    }


}
