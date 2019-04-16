using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.ProductionGlobal
{
    public class SetProdmonth
    {
        private decimal _theProdMonth;

        public void getNewProdmonth(decimal theProdMonth)
        {

            Decimal month = theProdMonth;
            String PMonth = month.ToString();
            PMonth.Substring(4, 2);
            _theProdMonth = -1;
            if (Convert.ToInt32(PMonth.Substring(4, 2)) > 12)
            {
                // MessageBox.Show(PMonth);
                int M = Convert.ToInt32(PMonth.Substring(0, 4));
                M++;
                PMonth = M.ToString();
                PMonth = PMonth + "01";
                _theProdMonth = Convert.ToInt32(PMonth);
            }
            else
            {
                if (Convert.ToInt32(PMonth.Substring(4, 2)) < 1)
                {
                    int M = Convert.ToInt32(PMonth.Substring(0, 4));
                    M--;
                    PMonth = M.ToString();
                    PMonth = PMonth + "12";
                    _theProdMonth = Convert.ToInt32(PMonth);
                }
            }





        }

        public decimal getProdmonth
        {
            get { return _theProdMonth; }

        }



    }
}
