using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.ProductionGlobal
{
    public class DisplayFmt
    {
        public static string Date_ddMMyyyy(object objDate)
        {
            try
            {
                return Convert.ToDateTime(objDate).ToString("dd/MM/yyyy");
            }
            catch
            {
                return "...";
            }
        }

        public static string Date_SQL(object objDate)
        {
            try
            {
                return Convert.ToDateTime(objDate).ToString("yyyy-MM-dd");
            }
            catch
            {
                return "...";
            }
        }

        public static string Perc_WithSign(object objPerc)
        {
            try
            {
                return string.Format("{0:0.00} %", Convert.ToDecimal(objPerc));
            }
            catch
            {
                return "...";
            }
        }
        public static string PercSmall_WithSign(object objPerc)
        {
            try
            {
                return string.Format("{0:0.00} %", Convert.ToDecimal(objPerc) * 100);
            }
            catch
            {
                return "...";
            }
        }

        public static object Money(object objMoney)
        {
            try
            {
                return string.Format("{0:0.00}", Convert.ToDecimal(objMoney));
            }
            catch
            {
                return "...";
            }
        }

        public static object CustomDecimal(object objValue, int nDecimalPlaced)
        {
            try
            {
                string strFormat = "{0:0.";

                for (int i = 0; i < nDecimalPlaced; i++)
                {
                    strFormat += "0";
                }
                strFormat += "}";

                return string.Format(strFormat, Convert.ToDecimal(objValue));
            }
            catch
            {
                return "...";
            }
        }

        public static string ExcapeSQL(object objValue)
        {
            try
            {
                return objValue.ToString().Replace("'", "''");
            }
            catch { return ""; }
        }
    }
}
