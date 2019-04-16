using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionGlobal
{
    public static class TProductionGlobal
    {
        public static List<TUserProduction> UserInfo = new List<TUserProduction>();
        public static List<TSysSettings> SystemSettingsProduction = new List<TSysSettings>();
        public static WPASMenuStructure WPASMenuStructure = new WPASMenuStructure();

        static TProductionGlobal()
        {
            WPASMenuStructure.setMenuItems();
        }

        public static DateTime ProdMonthAsDate(string theProdmonth)
        {
            DateTime theResult = DateTime.Now;
            int theYear = Convert.ToInt32(theProdmonth.Substring(0, 4));
            int theMonth = Convert.ToInt32(theProdmonth.Substring(4, 2));
            theResult = new DateTime(theYear, theMonth, 1);
            return theResult;

        }

        public static TUserProduction getUserInfo(string SiteTag)
        {
            TUserProduction theResult = new TUserProduction();
            foreach (TUserProduction ui in UserInfo)
            {
                if (ui.SiteTag == SiteTag)
                {
                    theResult = ui;
                    break;
                }
            }

            return theResult;
        }

        public static TSysSettings getSystemSettingsProductioInfo(string SiteTag)
        {
            TSysSettings theResult = new TSysSettings();
            foreach (TSysSettings ui in SystemSettingsProduction)
            {
                if (ui.SiteTag == SiteTag)
                {
                    theResult = ui;
                    break;
                }
            }

            return theResult;
        }

        public static void SetProductionGlobalInfo(string sysDBTag)
        {
            DataTable siteList = TConnections.GetSiteList();
            foreach (DataRow dr in siteList.Rows)
            {
                try
                {

                TSysSettings theProductionInfo = new TSysSettings();
                theProductionInfo.SiteTag = dr["Name"].ToString();
                theProductionInfo.GetSysSettings(sysDBTag, dr["Name"].ToString());
                SystemSettingsProduction.Add(theProductionInfo);
                }
                catch (Exception)
                {

                    
                }
            }
        }

        public static void SetUserInfo(string sysDBTag)
        {
            DataTable siteList = TConnections.GetSiteList();
            foreach (DataRow dr in siteList.Rows)
            {
                TUserProduction theUser = new TUserProduction();
                theUser.SiteTag = dr["Name"].ToString();
                theUser.SetUserInfo(TUserInfo.UserID, sysDBTag, dr["Name"].ToString());
                UserInfo.Add(theUser);
            }
        }

        public static string ProdMonthAsString(DateTime theProdmont)
        {

            string theResult = "";
            string year = theProdmont.Year.ToString();
            string month = theProdmont.Month.ToString();
            if (month.Length == 1)
                month = "0" + month;
            theResult = year + month;
            return theResult;

        }

        public static int ProdMonthAsInt(DateTime theProdmont)
        {

            string theResult = "";
            string year = theProdmont.Year.ToString();
            string month = theProdmont.Month.ToString();
            if (month.Length == 1)
                month = "0" + month;
            theResult = year + month;
            return Convert.ToInt32(theResult);

        }
        public static string ExtractAfterColon(string TheString)
        {
            string AfterColon = "";

            int index = TheString.IndexOf(":"); // Kry die postion van die :

            AfterColon = TheString.Substring(index + 1); // kry alles na :

            return AfterColon;
        }

        public static string ExtractBeforeColon(string TheString)
        {
            if (TheString != "")
            {
                string BeforeColon;

                int index = TheString.IndexOf(":");

                BeforeColon = TheString.Substring(0, index);

                return BeforeColon;
            }
            else
            {
                return "";
            }
        }
        public static decimal spinMonth(decimal TheMonth)
        {
            if (TheMonth != 0)
            {
                decimal _themonth;
                Decimal month = TheMonth;
                String PMonth = month.ToString();
                PMonth.Substring(4, 2);
                if (Convert.ToInt32(PMonth.Substring(4, 2)) > 12)
                {
                    // MessageBox.Show(PMonth);
                    int M = Convert.ToInt32(PMonth.Substring(0, 4));
                    M++;
                    PMonth = M.ToString();
                    PMonth = PMonth + "01";
                    _themonth = Convert.ToInt32(PMonth);
                    return _themonth;
                }
                else
                {
                    if (Convert.ToInt32(PMonth.Substring(4, 2)) < 1)
                    {
                        int M = Convert.ToInt32(PMonth.Substring(0, 4));
                        M--;
                        PMonth = M.ToString();
                        PMonth = PMonth + "12";
                        _themonth = Convert.ToDecimal(PMonth);
                        return _themonth;
                    }
                    else
                    {
                        _themonth = TheMonth;
                        return _themonth;
                    }
                }
            }
            else
                return 0;

        }

        public class clsValidation
        {
            public enum ValidationType
            {
                MWInteger5,
                MWInteger12,
                MWCleanText,
                MWDouble5D1,
                MWDouble5D2,
                MWDouble12D2,
                MWDouble5D3,
                MWDouble5D4,
                MWBinary,
                MWDate
            }
            public ValidationType MWValidationType;

            public string _MWInput;
            public string MWInput { set { _MWInput = value; } }

            public bool Validate()
            {
                try
                {
                    switch (MWValidationType)
                    {
                        case ValidationType.MWCleanText:

                            // Clean Text without ' and / \ only a-z A-Z and -

                            Regex ValFactor1 = new Regex(@"^\s*[a-zA-Z0-9,\s\-]+\s*$");
                            if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                        case ValidationType.MWDate:
                            break;
                        case ValidationType.MWInteger5:

                            // Limits to 5 left

                            ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}$");
                            if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }
                        case ValidationType.MWInteger12:

                            // Limits to 12 left

                            ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,12}$");
                            if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                        case ValidationType.MWBinary:

                            // Limits to 1 left

                            ValFactor1 = new Regex(@"(^([0]|[1])$)\d{0,1}$"); //Regex(@"^(?=.*[1]?.*$)\d{0,1}$");
                            if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                        case ValidationType.MWDouble5D1:

                            // Limits to 5 left and only 1 decimal

                            ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,2})?$");
                            if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                        case ValidationType.MWDouble5D2:

                            // Limits to 5 left and only 2 decimals

                            //ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,2})?$");
                            ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,1})?$");
                            if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                        case ValidationType.MWDouble12D2:

                            // Limits to 12 left and only 2 decimals

                            ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,12}(?:\.\d{0,2})?$");
                            if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }
                        case ValidationType.MWDouble5D3:

                            // Limits to 5 left and only 1 decimals

                            ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,3})?$");
                            if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                        case ValidationType.MWDouble5D4:

                            // Limits to 5 left and only 1 decimals

                            ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,4})?$");
                            if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }


                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }
    }
}
