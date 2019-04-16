using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using System.Drawing;
using MWDataManager;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.TrammingBooking
{
    class clsTrammingBookingData : clsBase
    {
        public DataTable GetProdMonth()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select CurrentProductionMonth Prodmonth from SYSSET");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return newData.ResultsDataTable;
        }

        public DataTable GetSections(string _prodmonth)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("Select distinct b.SectionID_2 SectionID, b.SectionID_2 + ' : ' + Name_2 Name ");
                sb.AppendLine("from PLANMONTH a ");
                sb.AppendLine("inner join SECTION_COMPLETE b on ");
                sb.AppendLine("a.PRODMONTH = b.PRODMONTH and ");
                sb.AppendLine("a.SECTIONID = b.SECTIONID ");
                sb.AppendLine("where a.PRODMONTH = '"+ _prodmonth + "' ");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return newData.ResultsDataTable;
        }

        public DataTable GetTrammingBookingData(string Prodmonth, string Section, DateTime Bookdate)
        {
            try
            {
                theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                theData.SqlStatement = "sp_Load_Tramming_Booking";

                SqlParameter[] _paramCollection =
                        {
                            theData.CreateParameter("@Prodmonth", SqlDbType.Decimal, 0, Prodmonth),
                            theData.CreateParameter("@Section", SqlDbType.VarChar, 0, Section),
                            theData.CreateParameter("@Bookdate", SqlDbType.DateTime, 0, Bookdate.ToString("MM/dd/yyyy")),
                        };

                theData.ParamCollection = _paramCollection;

                theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();

                return theData.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return theData.ResultsDataTable;
        }

        public Boolean SaveTrammingBooking(string Prodmonth, string Section, DateTime Bookdate, DataTable TheDatatable)
        {
            bool HasError = false;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            try
            {
                DataRow[] HoppersRow = TheDatatable.Select("Units = 'Hoppers'");

                foreach (DataRow dr in HoppersRow)
                {
                    try
                    {
                        theData.SqlStatement = "DELETE FROM Book_Tramming \n" +
                                                "WHERE Prodmonth = " + Prodmonth.ToString() + "\n" +
                                                " AND Sectionid = '" + Section.ToString() + "'\n" +
                                                " AND Workplaceid = '" + dr["Workplaceid"].ToString() + "'\n" +
                                                " AND BookDate = '" + Bookdate.ToShortDateString() + "'";

                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();


                        theData.SqlStatement = "INSERT INTO Book_Tramming (Prodmonth, Sectionid, Workplaceid, BookDate, Morning, Afternoon, Night) VALUES (\n" +
                                               Prodmonth.ToString() + ",\n" +
                                               " '" + Section.ToString() + "',\n" +
                                               " '" + dr["Workplaceid"] + "',\n" +
                                               " '" + Bookdate.ToShortDateString() + "',\n" +
                                               dr["Morning"] + ", \n" +
                                               dr["Afternoon"] + ", \n" +
                                               dr["Night"] + ")";

                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();
                    }
                    catch (Exception _exception)
                    {
                        theData.SqlStatement = "UPDATE Book_Tramming SET \n" +
                                                "Morning = " + dr["Morning"].ToString() + ", \n" +
                                                "Afternoon = " + dr["Afternoon"].ToString() + ", \n" +
                                                "Night = " + dr["Night"].ToString() + "\n" +
                                                "WHERE Prodmonth = " + Prodmonth.ToString() + "\n" +
                                                " AND Sectionid = '" + Section.ToString() + "'\n" +
                                                " AND Workplaceid = '" + dr["Workplaceid"].ToString() + "'\n" +
                                                " AND BookDate = '" + Bookdate.ToShortDateString() + "'";

                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();
                    }
                }

                DataRow[] CommetsRow = TheDatatable.Select("Units = 'Comments'");

                foreach (DataRow dr in CommetsRow)
                {
                    try
                    {
                        theData.SqlStatement = "DELETE FROM Book_Tramming_Comments \n" +
                                                "WHERE Prodmonth = " + Prodmonth.ToString() + "\n" +
                                                " AND Sectionid = '" + Section.ToString() + "'\n" +
                                                " AND Workplaceid = '" + dr["Workplaceid"].ToString() + "'\n" +
                                                " AND BookDate = '" + Bookdate.ToShortDateString() + "'";

                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();


                        theData.SqlStatement = "INSERT INTO Book_Tramming_Comments (Prodmonth, Sectionid, Workplaceid, BookDate, Morning, Afternoon, Night) VALUES (\n" +
                                               Prodmonth.ToString() + ",\n" +
                                               " '" + Section.ToString() + "',\n" +
                                               " '" + dr["Workplaceid"] + "',\n" +
                                               " '" + Bookdate.ToShortDateString() + "',\n" +
                                               " '" + dr["Morning"] + "', \n" +
                                               " '" + dr["Afternoon"] + "', \n" +
                                               " '" + dr["Night"] + "')";

                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();
                    }
                    catch (Exception _exception)
                    {
                        theData.SqlStatement = "UPDATE Book_Tramming_Comments SET \n" +
                                                "Morning = '" + dr["Morning"].ToString() + "', \n" +
                                                "Afternoon = '" + dr["Afternoon"].ToString() + "', \n" +
                                                "Night = '" + dr["Night"].ToString() + "'\n" +
                                                "WHERE Prodmonth = " + Prodmonth.ToString() + "\n" +
                                                " AND Sectionid = '" + Section.ToString() + "'\n" +
                                                " AND Workplaceid = '" + dr["Workplaceid"].ToString() + "'\n" +
                                                " AND BookDate = '" + Bookdate.ToShortDateString() + "'";

                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();
                    }
                }

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Tramming Booking Data Saved Successfully", Color.CornflowerBlue);

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
                HasError = true;
            }

            if (HasError == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
