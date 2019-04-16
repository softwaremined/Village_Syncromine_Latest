using System;
using System.Data;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.Kriging
{
    class clsKriging : clsBase
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessageClass = new Global.sysMessages.sysMessagesClass();

        public DataTable get_Sysset()
        {
            try
            {
                theData.SqlStatement = " select * from Sysset ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable; ;

            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable; ;
            }
        }
        public DataTable get_Sections(string _prodmonth, string _hierid)
        {
            try
            {
                theData.SqlStatement = " Select SectionID, Name " +
                                        " from Section s where s.Prodmonth = '" + _prodmonth + "' and " +
                                        " Hierarchicalid = '" + _hierid + "' " +
                            " order by SectionID ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable; ;
            }
        }
        public DataTable get_Kriging(string _prodmonth, string _sectionID, int _weekno, int _sampling, string _dateTrue)
        {
            try
            {

                theData.SqlStatement = "[sp_Kriging_Load]";
                theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                theData.ResultsTableName = "Kriging";

                SqlParameter[] _paramCollection =
                            {
                        theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, _prodmonth),
                        theData.CreateParameter("@SectionID", SqlDbType.Text, 20, _sectionID),
                        theData.CreateParameter("@WeekNo", SqlDbType.Int, 2, _weekno),
                        theData.CreateParameter("@Sampling", SqlDbType.Int, 2, _sampling),
                        theData.CreateParameter("@DateTrue", SqlDbType.VarChar, 3, _dateTrue),
                    };

                theData.ParamCollection = _paramCollection;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable;
            }
        }
        public DataTable get_Planning(string _prodmonth, string _sectionID)
        {
            try
            {

                theData.SqlStatement = " select p.* from Kriging p \r\n " +
                            " inner join Section_Complete sc on \r\n " +
                            "    sc.ProdMonth = p.Prodmonth and \r\n " +
                            "    sc.SectionID = p.SectionID \r\n " +
                            " inner join Seccal s on \r\n " +
                            "  sc.SectionID_1 = s.SectionID and \r\n " +
                            "  sc.ProdMonth = s.ProdMonth \r\n " +
                            " where p.ProdMonth = '" + _prodmonth + "' and " +
                            "      sc.SectionID_2 = '" + _sectionID + "' and s.BeginDate < GETDATE()  \r\n ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable;
            }
        }
        public DataTable get_Calendars(string _prodmonth, string _sectionID)
        {
            try
            {

                theData.SqlStatement = //" select BeginDate = convert(varchar(10), max(s.BeginDate), 120), \r\n " +
                                       //" EndDate = convert(varchar(10), max(s.EndDate), 120)  \r\n " +
                            " select BeginDate = max(s.BeginDate), \r\n " +
                            " EndDate = max(s.EndDate)  \r\n " +
                            " from Seccal s  \r\n " +
                            " inner join Section_Complete sc on  \r\n " +
                            "   sc.Prodmonth = s.Prodmonth and  \r\n " +
                            "   sc.SECTIONID_1 = s.SectionID  \r\n " +
                            " where sc.SectionID_2 = '" + _sectionID + "' and  \r\n " +
                            "       s.Prodmonth = '" + _prodmonth + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable;
            }
        }
        public DataTable get_WeekNo(string _prodmonth, string _sectionID)
        {
            try
            {

                theData.SqlStatement = " select max(k.WeekNo) WeekNo \r\n " +
                            " from Kriging k  \r\n " +
                            " inner join Section_Complete sc on \r\n " +
                            "   sc.ProdMonth = k.ProdMonth and \r\n " +
                            "   sc.SectionID = k.SectionID \r\n " +
                            " where k.Prodmonth = '" + _prodmonth + "' and " +
                            "      sc.SectionID_2 = '" + _sectionID + "'  \r\n ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
                return theData.ResultsDataTable;
            }
        }
        public Boolean save_Data(DataTable dtDataTable, int _save, string _userid, string _prodmonth, string _sectionid)
        {
            try
            {
                theData.SqlStatement = "";
                theData.SqlStatement = theData.SqlStatement +
                           " delete Kriging from Kriging k \r\n " +
                           "   inner join Section_Complete sc on \r\n " +
                           "     sc.ProdMonth = k.ProdMonth and \r\n " +
                           "     sc.sectionid = k.sectionid \r\n " +
                           "   where k.ProdMonth = '"+ _prodmonth +"' and \r\n " +
                           "         sc.SectionID_2 = '"+ _sectionid +"' and \r\n " +
                           "         k.WeekNo = '" + _save + "' \r\n ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                theData.SqlStatement = "";
                foreach (DataRow dr in dtDataTable.Rows)
                {
                    if (dr["Unit"] != null)
                    {
                        if (dr["Unit"].ToString() == "Total g/t")
                        {
                            DateTime _today = DateTime.Now;
                            if (_save == 2)
                            {
                                theData.SqlStatement = theData.SqlStatement +
                                    " insert into Kriging  \r\n " +
                                    "  (Prodmonth,  SectionID, Workplaceid, Activity, WeekNo, gt, cmgt, \r\n " +
                                    "   StopeWidth, ChannelWidth, EndHeight, EndWidth, UserID, CreateDate)  \r\n " +
                                    " values ( " +
                                    " '" + dr["ProdMonth"] + "', \r\n " +
                                    " '" + dr["SectionID"] + "',  \r\n " +
                                    " '" + dr["WorkplaceID"] + "',  \r\n " +
                                    " '" + dr["Activity"] + "',   \r\n " +
                                    " '" + _save + "', \r\n " +
                                    " '" + dr["W2gt"] + "', '" + dr["W2cmgt"] + "', \r\n " +
                                    " '" + dr["W2SW"] + "', '" + dr["W2CW"] + "', \r\n " +
                                    " '" + dr["W2EH"] + "', '" + dr["W2EW"] + "', \r\n " +
                                    " '"+ _userid +"', '"+ _today + "' ) \r\n ";
                            }
                            if (_save == 3)
                            {
                                theData.SqlStatement = theData.SqlStatement +
                                    " insert into Kriging  \r\n " +
                                    "  (Prodmonth,  SectionID, Workplaceid, Activity, WeekNo, gt, cmgt, \r\n " +
                                    "   StopeWidth, ChannelWidth, EndHeight, EndWidth, UserID, CreateDate)  \r\n " +
                                    " values ( " +
                                    " '" + dr["ProdMonth"] + "', \r\n " +
                                    " '" + dr["SectionID"] + "',  \r\n " +
                                    " '" + dr["WorkplaceID"] + "',  \r\n " +
                                    " '" + dr["Activity"] + "',   \r\n " +
                                    " '" + _save + "', \r\n " +
                                    " '" + dr["W3gt"] + "', '" + dr["W3cmgt"] + "', \r\n " +
                                    " '" + dr["W3SW"] + "', '" + dr["W3CW"] + "', \r\n " +
                                    " '" + dr["W3EH"] + "', '" + dr["W3EW"] + "', \r\n " +
                                    " '" + _userid + "', '" + _today + "' ) \r\n ";
                            }
                            if (_save == 4)
                            {
                                theData.SqlStatement = theData.SqlStatement +
                                    " insert into Kriging  \r\n " +
                                    "  (Prodmonth,  SectionID, Workplaceid, Activity, WeekNo, gt, cmgt, \r\n " +
                                    "   StopeWidth, ChannelWidth, EndHeight, EndWidth, UserID, CreateDate)  \r\n " +
                                    " values ( " +
                                    " '" + dr["ProdMonth"] + "', \r\n " +
                                    " '" + dr["SectionID"] + "',  \r\n " +
                                    " '" + dr["WorkplaceID"] + "',  \r\n " +
                                    " '" + dr["Activity"] + "',   \r\n " +
                                    " '" + _save + "', \r\n " +
                                    " '" + dr["W4gt"] + "', '" + dr["W4cmgt"] + "', \r\n " +
                                    " '" + dr["W4SW"] + "', '" + dr["W4CW"] + "', \r\n " +
                                    " '" + dr["W4EH"] + "', '" + dr["W4EW"] + "', \r\n " +
                                    " '" + _userid + "', '" + _today + "' ) \r\n ";
                            }
                            if (_save == 5)
                            {
                                theData.SqlStatement = theData.SqlStatement +
                                    " insert into Kriging  \r\n " +
                                    "  (Prodmonth,  SectionID, Workplaceid, Activity, WeekNo, gt, cmgt, \r\n " +
                                    "   StopeWidth, ChannelWidth, EndHeight, EndWidth, UserID, CreateDate)  \r\n " +
                                    " values ( " +
                                    " '" + dr["ProdMonth"] + "', \r\n " +
                                    " '" + dr["SectionID"] + "',  \r\n " +
                                    " '" + dr["WorkplaceID"] + "',  \r\n " +
                                    " '" + dr["Activity"] + "',   \r\n " +
                                    " '" + _save + "', \r\n " +
                                    " '" + dr["W5gt"] + "', '" + dr["W5cmgt"] + "', \r\n " +
                                    " '" + dr["W5SW"] + "', '" + dr["W5CW"] + "', \r\n " +
                                    " '" + dr["W5EH"] + "', '" + dr["W5EW"] + "', \r\n " +
                                    " '" + _userid + "', '" + _today + "' ) \r\n ";
                            }
                        } 
 
                    }
                }
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();

                //Update Planning
                decimal TheGT = 0;
                decimal TheCMGT = 0;
                decimal TheSW = 0;
                decimal TheCW = 0;
                decimal TheEH = 0;
                decimal TheEW = 0;
                decimal TheDens = 0;

                theData.SqlStatement = "";
                foreach (DataRow dr in dtDataTable.Rows)
                {
                    if (dr["Unit"] != null)
                    {
                        if (dr["Unit"].ToString() == "Total g/t")
                        {                                                     
                            TheGT = 0;
                            TheCMGT = 0;
                            TheSW = 0;
                            TheCW = 0;                                                                                   
                            TheEH = 0;
                            TheEW = 0;
                            TheDens = Convert.ToDecimal(dr["Dens"].ToString());
                            if (Convert.ToInt32(dr["Activity"].ToString()) != 1)
                            {
                                if (_save == 2)
                                {
                                    TheGT = Convert.ToDecimal(dr["W2GT"].ToString());
                                    TheCMGT = Convert.ToDecimal(dr["W2CMGT"].ToString());
                                    TheSW = Convert.ToDecimal(dr["W2SW"].ToString());
                                    TheCW = Convert.ToDecimal(dr["W2CW"].ToString());
                                }
                                if (_save == 3)
                                {
                                    TheGT = Convert.ToDecimal(dr["W3GT"].ToString());
                                    TheCMGT = Convert.ToDecimal(dr["W3CMGT"].ToString());
                                    TheSW = Convert.ToDecimal(dr["W3SW"].ToString());
                                    TheCW = Convert.ToDecimal(dr["W3CW"].ToString());
                                }
                                if (_save == 4)
                                {
                                    TheGT = Convert.ToDecimal(dr["W4GT"].ToString());
                                    TheCMGT = Convert.ToDecimal(dr["W4CMGT"].ToString());
                                    TheSW = Convert.ToDecimal(dr["W4SW"].ToString());
                                    TheCW = Convert.ToDecimal(dr["W4CW"].ToString());
                                }
                                if (_save == 5)
                                {
                                    TheGT = Convert.ToDecimal(dr["W5GT"].ToString());
                                    TheCMGT = Convert.ToDecimal(dr["W5CMGT"].ToString());
                                    TheSW = Convert.ToDecimal(dr["W5SW"].ToString());
                                    TheCW = Convert.ToDecimal(dr["W5CW"].ToString());
                                }

                                theData.SqlStatement = theData.SqlStatement +
                                    " Update PlanMonth  \r\n " +
                                    " set SW = " + TheSW + ", \r\n " +
                                    "     CW = " + TheCW + ", \r\n " +
                                    "     GT = " + TheGT + ", \r\n " +
                                    "     CMGT = " + TheCMGT + ", \r\n " +
                                    "     Tons = SQM * " + TheSW + " * " + TheDens + " / 100, \r\n " +
                                    "     ReefTons = ReefSqm * " + TheSW + " * " + TheDens + " / 100, \r\n " +
                                    "     WasteTons = WasteSqm * " + TheSW + " * " + TheDens + " / 100, \r\n " +
                                    "     KG = ReefSqm * " + TheCMGT + " * " + TheDens + " / 100 / 1000, \r\n " +
                                    "     CubicsTons = CubicMetres * " + TheGT + ", \r\n " +
                                    "     CubicsReefTons = CubicsReef * " + TheGT + ", \r\n " +
                                    "     CubicsWasteTons = CubicsWaste * " + TheGT + ", \r\n " +
                                    "     CubicGT = " + TheGT + ", \r\n " +
                                    "     CubicGrams = CubicsReef * " + TheGT + " * " + TheDens + " \r\n " +
                                    " where ProdMonth = '" + dr["ProdMonth"].ToString() + "' and \r\n " +
                                    "       SectionID = '" + dr["SectionID"].ToString() + "' and \r\n " +
                                    "       WorkPlaceID = '" + dr["WorkplaceID"].ToString() + "' and \r\n " +
                                    "       Activity = '" + dr["Activity"].ToString() + "' \r\n " +

                                    " Update Planning \r\n " +
                                    " set BookGrams =  isnull(BookReefSqm,0) * " + TheCMGT + " * " + TheDens + " / 100, \r\n " +
                                    "     BookReefTons = isnull(BookReefSqm,0) * " + TheSW + " * " + TheDens + " / 100, \r\n " +
                                    "     BookWasteTons = isnull(BookWasteSqm,0) * " + TheSW + " * " + TheDens + " / 100, \r\n " +
                                    "     BookTons = isnull(BookSqm,0) * " + TheSW + " * " + TheDens + " / 100, \r\n " +
                                    "     BookSW = " + TheSW + ", \r\n " +
                                    "     BookCW = " + TheCW + ", \r\n " +
                                    "     Bookcmgt = " + TheCMGT + ", \r\n " +
                                    "     Bookgt = " + TheGT + ", \r\n " +
                                    "     BookCubicTons = BookCubicMetres * " + TheGT + ", \r\n " +
                                    "     BookCubicGT = " + TheGT + ", \r\n " +
                                    "     BookCubicGrams = case when BookReef = 'R' then BookCubicMetres * " + TheGT + " * " + TheDens + " else 0 end \r\n " +
                                    " where ProdMonth = '" + dr["ProdMonth"].ToString() + "' and \r\n " +
                                    "       SectionID = '" + dr["SectionID"].ToString() + "' and  \r\n " +
                                    "       WorkPlaceID = '" + dr["WorkplaceID"].ToString() + "' and  \r\n " +
                                    "       Activity = '" + dr["Activity"].ToString() + "' ";  // and " +
                                    //"       isnull(BookSqm,0) > 0 \r\n ";
                                theData.SqlStatement = theData.SqlStatement +
                                    " Update Planning \r\n " +
                                    " set Grams = isnull(ReefSQM,0) * " + TheCMGT + " * " + TheDens + " / 100, \r\n " +
                                    "     ReefTons = (isnull(ReefSQM,0) * " + TheSW + " * " + TheDens + " / 100), \r\n " +
                                    "     WasteTons = isnull(WasteSQM,0) * " + TheSW + " * " + TheDens + " / 100, \r\n " +
                                    "     Tons = isnull(SQM,0) * " + TheSW + " * " + TheDens + " / 100, \r\n " +
                                    "     SW = " + TheSW + ", \r\n " +
                                    "     CW = " + TheCW + ", \r\n " +
                                    "     cmgt = " + TheCMGT + ", \r\n " +
                                    "     gt = " + TheGT + ", \r\n " +
                                    "     CubicTons = CubicMetres * " + TheGT + ", \r\n " +
                                    "     CubicGT = " + TheGT + ", \r\n " +
                                    "     CubicGrams = CubicMetres * " + TheGT + " * " + TheDens + " \r\n " +
                                    " where ProdMonth = '" + dr["ProdMonth"].ToString() + "' and \r\n " +
                                    "       SectionID = '" + dr["SectionID"].ToString() + "' and  \r\n " +
                                    "       WorkPlaceID = '" + dr["WorkplaceID"].ToString() + "' and  \r\n " +
                                    "       Activity = '" + dr["Activity"].ToString() + "' "; //and \r\n " +
                                    //"       isnull(SQM,0) > 0 \r\n ";
                            }
                            else
                            {
                                if (_save == 2)
                                {
                                    TheEH = Convert.ToDecimal(dr["W2EH"].ToString());
                                    TheEW = Convert.ToDecimal(dr["W2EW"].ToString());
                                    TheGT = Convert.ToDecimal(dr["W2GT"].ToString());
                                    TheCMGT = Convert.ToDecimal(dr["W2CMGT"].ToString());
                                }
                                if (_save == 3)
                                {
                                    TheEH = Convert.ToDecimal(dr["W3EH"].ToString());
                                    TheEW = Convert.ToDecimal(dr["W3EW"].ToString());
                                    TheGT = Convert.ToDecimal(dr["W3GT"].ToString());
                                    TheCMGT = Convert.ToDecimal(dr["W3CMGT"].ToString());
                                }
                                if (_save == 4)
                                {
                                    TheEH = Convert.ToDecimal(dr["W4EH"].ToString());
                                    TheEW = Convert.ToDecimal(dr["W4EW"].ToString());
                                    TheGT = Convert.ToDecimal(dr["W4GT"].ToString());
                                    TheCMGT = Convert.ToDecimal(dr["W4CMGT"].ToString());
                                }
                                if (_save == 5)
                                {
                                    TheEH = Convert.ToDecimal(dr["W5EH"].ToString());
                                    TheEW = Convert.ToDecimal(dr["W5EW"].ToString());
                                    TheGT = Convert.ToDecimal(dr["W5GT"].ToString());
                                    TheCMGT = Convert.ToDecimal(dr["W5CMGT"].ToString());
                                }

                                theData.SqlStatement = theData.SqlStatement +
                                    " Update PlanMonth \r\n " +
                                    " set DWidth = " + TheEW + ", \r\n " +
                                    "     DHeight = " + TheEH + ", \r\n " +
                                    "     GT = " + TheGT + ", \r\n " +
                                    "     CMGT = " + TheCMGT + ", \r\n " +
                                    "     Tons = Metresadvance * " + TheEH + " * " + TheEW + " * " + TheDens + ", \r\n " +
                                    "     ReefTons = ReefAdv * " + TheEH + " * " + TheEW + " * " + TheDens + ", \r\n " +
                                    "     WasteTons = WasteAdv * " + TheEH + " * " + TheEW + " * " + TheDens + ", \r\n " +
                                    "     KG = ReefAdv * " + TheEH + " * " + TheCMGT + " * " + TheDens + " / 100 / 1000, \r\n " +
                                    "     CubicsTons = CubicMetres * " + TheGT + ", \r\n " +
                                    "     CubicsReefTons = CubicsReef * " + TheGT + ", \r\n " +
                                    "     CubicsWasteTons = CubicsWaste * " + TheGT + ", \r\n " +
                                    "     CubicGT = " + TheGT + ", \r\n " +
                                    "     CubicGrams = CubicsReef * " + TheGT + " * " + TheDens + " \r\n " +
                                    " where ProdMonth = '" + dr["ProdMonth"].ToString() + "' and \r\n " +
                                    "       SectionID = '" + dr["SectionID"].ToString() + "' and  \r\n " +
                                    "       WorkPlaceID = '" + dr["WorkplaceID"].ToString() + "' and  \r\n " +
                                    "       Activity = '" + dr["Activity"].ToString() + "'   \r\n " +

                                    " UPdate Planning  \r\n " +
                                    " set BookGrams = isnull(BookReefAdv,0) * " + TheEW + " * " + TheCMGT + " * " + TheDens + " / 100, \r\n " +
                                    "     BookReefTons = isnull(BookReefAdv,0) * " + TheEW + " * " + TheEH + " * " + TheDens + ", \r\n " +
                                    "     BookWasteTons = isnull(BookWasteAdv,0) * " + TheEW + " * " + TheEH + " * " + TheDens + ", \r\n " +
                                    "     BookTons = isnull(BookMetresadvance,0) * " + TheEW + " * " + TheEH + " * " + TheDens + ", \r\n " +
                                    "     BookGT = " + TheGT + ", \r\n " +
                                    "     Bookcmgt = " + TheCMGT + ", \r\n " +
                                    "     BookCubicTons = BookCubicMetres * " + TheGT + ", \r\n " +
                                    "     BookCubicGT = " + TheGT + ", \r\n " +
                                    "     BookCubicGrams = case when BookReef = 'R' then BookCubicMetres * " + TheGT + " * " + TheDens + " else 0 end \r\n " +
                                    " where ProdMonth = '" + dr["ProdMonth"].ToString() + "' and \r\n " +
                                    "       SectionID = '" + dr["SectionID"].ToString() + "' and  \r\n " +
                                    "       WorkPlaceID = '" + dr["WorkplaceID"].ToString() + "' and  \r\n " +
                                    "       Activity = '" + dr["Activity"].ToString() + "' ";  // and \r\n " +
                                                                                               // "       isnull(BookMetresadvance,0) > 0 \r\n ";
                                theData.SqlStatement = theData.SqlStatement +
                                    " UPdate Planning  \r\n " +
                                    " set Grams = isnull(ReefAdv,0) * " + TheEW + " * " + TheCMGT + " * " + TheDens + " / 100, \r\n " +
                                    "     Tons = isnull(Metresadvance,0) * " + TheEW + " * " + TheEH + " * " + TheDens + ", \r\n " +
                                    "     ReefTons =  isnull(ReefAdv,0) * " + TheEW + " * " + TheEH + " * " + TheDens + ",  \r\n " +
                                    "     WasteTons =  isnull(WasteAdv,0) * " + TheEW + " * " + TheEH + " * " + TheDens + ", \r\n " +
                                    "     GT = " + TheGT + ",  \r\n " +
                                    "     Cmgt = " + TheCMGT + ", \r\n " +
                                    "     CubicTons = CubicMetres * " + TheGT + ", \r\n " +
                                    "     CubicGT = " + TheGT + ", \r\n " +
                                    "     CubicGrams = CubicMetres * " + TheGT + " * " + TheDens + " \r\n " +
                                    " where ProdMonth = '" + dr["ProdMonth"].ToString() + "' and \r\n " +
                                    "       SectionID = '" + dr["SectionID"].ToString() + "' and  \r\n " +
                                    "       WorkPlaceID = '" + dr["WorkplaceID"].ToString() + "' and  \r\n " +
                                    "       Activity = '" + dr["Activity"].ToString() + "' ";  // and \r\n " +
                                    //"       isnull(Metresadvance,0) > 0 \r\n ";
                            }
                        }

                    }
                }
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return true;
            }
            catch (Exception _exception)
            {
                _sysMessageClass.viewMessage(MessageType.Error, "UPDATING ERROR", resHarmonyPAS.systemTag, "clsKriging", "save_Data", _exception.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
        }
    }
}
