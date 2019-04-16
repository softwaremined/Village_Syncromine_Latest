using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.ProductionGlobal
{
    
    static public class GlobalVar
    {

        static int _ProdMonth, _TemplateID, _WorkPlaceID, _ActivityType;
        static string _captureOption, _section;
        static bool _Readonly;
        

        public static int ProdMonth
        {
            get { return _ProdMonth; }
            set { _ProdMonth = value; }
        }

        public static string section
        {
            get { return _section; }
            set { _section = value; }
        }

        public static int TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }

        public static int WorkPlaceID
        {
            get { return _WorkPlaceID; }
            set { _WorkPlaceID = value; }
        }

        public static int ActivityType
        {
            get { return _ActivityType; }
            set { _ActivityType = value; }
        }

        public static string captureOption
        {
            get { return _captureOption; }
            set { _captureOption = value; }
        }

        public static bool Readonly
        {
            get { return _Readonly; }
            set { _Readonly = value; }
        }


    }
}
