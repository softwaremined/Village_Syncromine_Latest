using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Mineware.Systems.Global;
using Mineware.Systems.Global.sysMessages;
using Mineware.Systems.GlobalConnect;
using MWDataManager;
using Mineware.Systems.GlobalExtensions;
//using Mineware.Systems.ProductionGlobal;

namespace Mineware.Systems.Production.Controls.BookingsABS
{
	public class clsBookingsABS : clsBase
	{
		private readonly sysMessagesClass _sysMessagesClass = new sysMessagesClass();
        //private readonly SysSettings _sysUser = new SysSettings();

        public string Prob_NoteID;
		public string Prob_SBNotes;


        public DataTable CheckProlemExist(string _ProblemID,int _Acivity)
        {
            theData.SqlStatement =
                " select ProblemID from CODE_PROBLEM \r\n " +
                " where ProblemID = '" + _ProblemID + "' and activity = '"+ _Acivity + "' ";
            theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = ReturnType.DataTable;
            theData.ExecuteInstruction();
            var errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_Sysset", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            return theData.ResultsDataTable;
        }

        public DataTable get_Sysset()
		{
			theData.SqlStatement = " select *, theRunDate = Convert(varchar(10), RunDate, 120) from Sysset ";
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			theData.ExecuteInstruction();
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_Sysset", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}


        public DataTable get_ProdmonthDateChange()
        {
            theData.SqlStatement = " select max(p.Prodmonth) pm from planning p, section_complete sc  where p.sectionid = sc.sectionid and p.prodmonth = sc.prodmonth and sc.SectionID_1 = @Section and p.calendardate = @CalendarDate ";
            theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = ReturnType.DataTable;
            theData.ExecuteInstruction();
            var errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_Sysset", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            return theData.ResultsDataTable;
        }

        public DataTable get_Users(string _userid)
		{
			theData.SqlStatement =
				" select BackDateBooking, DaysBackDate = isnull(DaysBackDate,0) \r\n " +
				" from Users where UserID = '" + _userid + "' ";
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			theData.ExecuteInstruction();
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_Sysset", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable get_UserBookSection(string _prodmonth, string _userid)
		{
			theData.SqlStatement = " Select SectionID from Users_Section " +
			                       " where UserID = '" + _userid + "' and LinkType = 'P' ";
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			theData.ExecuteInstruction();

			var _sectionid = "";
			//string _sqlStatement = "";
			sb.Clear();
			foreach (DataRow dr in theData.ResultsDataTable.Rows)
			{
				_sectionid = dr["SectionID"].ToString();

				theData.SqlStatement = " Select HierarchicalID from Section " +
				                       " where Prodmonth = '" + _prodmonth + "' and " +
				                       " SectionID = '" + dr["SectionID"] + "' ";
				theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
				theData.queryReturnType = ReturnType.DataTable;
				theData.ExecuteInstruction();

				var _hierid = 0;
				if (theData.ResultsDataTable.Rows.Count > 0)
				{
					_hierid = Convert.ToInt32(theData.ResultsDataTable.Rows[0]["HierarchicalID"].ToString());
				}
				if (_hierid >= 1 &&
				    _hierid <= 5)
				{
					theData.SqlStatement = " Select SectionID, Name " +
					                       " from Section s where s.Prodmonth = '" + _prodmonth + "' and " +
					                       " Hierarchicalid > '" + _hierid + "' " +
					                       " order by SectionID ";
					theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
					theData.queryReturnType = ReturnType.DataTable;
					theData.ExecuteInstruction();

					sb.AppendLine("select distinct SectionID_1 SectionID, Name_1 Name from PlanMonth a ");
					sb.AppendLine("inner join section_complete b on ");
					sb.AppendLine(" a.prodmonth = b.prodmonth and ");
					sb.AppendLine("  a.sectionid = b.sectionid ");
					sb.AppendLine("where a.prodmonth = '" + _prodmonth + "' and  ");
					if (_hierid == 1)
					{
						sb.AppendLine(" b.SectionID_5 = '" + _sectionid + "' ");
					}
					if (_hierid == 2)
					{
						sb.AppendLine(" b.SectionID_4 = '" + _sectionid + "' ");
					}
					if (_hierid == 3)
					{
						sb.AppendLine(" b.SectionID_3 = '" + _sectionid + "' ");
					}
					if (_hierid == 4)
					{
						sb.AppendLine(" b.SectionID_2 = '" + _sectionid + "' ");
					}
					if (_hierid == 5)
					{
						sb.AppendLine(" b.SectionID_1 = '" + _sectionid + "' ");
					}
					sb.AppendLine(" union");
				}
			}
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			theData.SqlStatement = sb.ToString();
			theData.SqlStatement = theData.SqlStatement.Substring(0, theData.SqlStatement.Length - 7);
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_UserBookSection", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable get_BookCodeStp()
		{
			theData.SqlStatement = " Select BookingCode BookCodeStp, BookingCode+':'+Description BookDescStp \r\n " +
			                       " from Code_Booking where Activity = 0 ";
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			theData.ExecuteInstruction();
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_BookCodeStp", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable get_BookCodeDev()
		{
			theData.SqlStatement = " Select BookingCode BookCodeDev, BookingCode+':'+Description BookDescDev \r\n " +
			                       " from Code_Booking where Activity = 1 ";
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			theData.ExecuteInstruction();
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_BookCodeDev", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable get_CausedLostBlast(string TheActivity, string ProblemID)
		{
			theData.SqlStatement = " select CausedLostBlast = case when isnull(CausedLostBlast,'') = 'Y' then 'Y' else 'N' end \r\n " +
			                       " from CODE_PROBLEM  \r\n " +
			                       " where ProblemID = '" + ProblemID + "' and \r\n " +
			                       "       activity = '" + TheActivity + "' and Deleted <> 'Y'";
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsSICCapture", "get_Problems_Desc", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable get_Pegs(string _sectionid, string _wpid, string _bookdate)
		{
			theData.ResultsTableName = "Dev_Pegs";
			theData.SqlStatement =
				//" select WorkplaceID WPID, PegID = a.PegID +':'+convert(varchar(10), cast(a.Value as numeric(8,1))) \r\n " +
				//" from PEG a where a.WorkplaceID in \r\n " +
				//"     (select distinct(WorkplaceID) \r\n " +
				//"      from Planning p \r\n " +
				//"      inner join Section_Complete sc on \r\n " +
				//"        p.ProdMonth = sc.ProdMonth and \r\n " +
				//"        p.SectionID = sc.SectionID \r\n " +
				//"      where p.CalendarDate = '" + _bookdate + "' and \r\n " +
				//"            sc.SectionID_1  = '" + _sectionid + "' and \r\n " +
				//"            p.Activity = 1) order by WorkplaceID,    Value desc ";
				" select p.WorkplaceID WPID, \r\n " +
				"   PegID = case when isnull(a.PegID, '') = '' then 'START:0.0' \r\n " +
				"              else a.PegID + ':' + convert(varchar(10), cast(a.Value as numeric(8, 1))) end \r\n " +
				"   from Planning p \r\n " +
				"   left outer join Peg a on \r\n " +
				"        p.WorkplaceID = a.WorkplaceID \r\n " +
				"   inner join Section_Complete sc on \r\n " +
				"       p.ProdMonth = sc.ProdMonth and \r\n " +
				"       p.SectionID = sc.SectionID \r\n " +
				"   where p.CalendarDate = '" + _bookdate + "' and \r\n " +
				"       sc.SectionID_1 = '" + _sectionid + "' and \r\n " +
				"       p.Activity = 1 \r\n " +
				"   order by p.WorkplaceID,    Value desc ";

			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_Pegs", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable GetShiftInfo(string prodmonth, string section)
		{
			theData.SqlStatement = string.Format("select Name_1,  Begindate, Enddate, TotalShifts, COUNT(a.CALENDARDATE) Shift \r" +
			                                     "from  \r" +
			                                     "(select distinct Name_1, c.BeginDate Begindate, c.Enddate, c.TotalShifts, Calendardate from Planmonth a inner join  \r" +
			                                     "SECTION_COMPLETE b on  \r" +
			                                     "a.Prodmonth = b.PRODMONTH and  \r" +
			                                     "a.SectionID = b.SECTIONID " +
			                                     "inner join seccal c on  \r" +
			                                     "b.prodmonth = c.prodmonth  \r" +
			                                     "and b.sectionId_1 = c.sectionid  \r" +
			                                     "Inner join caltype d on \r" +
			                                     " c.calendarCode = d.CalendarCode and \r" +
			                                     " c.BeginDate <= d.CalendarDate and \r" +
			                                     " c.Enddate >= d.CalendarDate \r" +
			                                     "where a.prodmonth = " + prodmonth +
			                                     " and b.SectionID_1 = '" + section + "'  \r" +
			                                     "and d.CALENDARDATE <= (select min(Rundate) from sysset)  \r" +
			                                     "and d.WorkingDay = 'Y') a " +
			                                     "group by Name_1, Begindate, Enddate, TotalShifts");

			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "GetShiftInfo", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}


        public DataTable GetProdmonth( string section, string _bookdate)
        {
            theData.SqlStatement = string.Format("  \r" +                                           
                                            

                                                  "  select max(p.prodmonth) Prodmonth \r" +
                                                  " from planning p, Section_Complete s \r" +
                                                  " where s.Prodmonth = p.prodmonth \r" +
                                                  " and s.SectionID = p.Sectionid \r" +
                                                  " and p.calendardate = '" + _bookdate + "' \r" +
                                                  " and s.SectionID_1 = '" + section + "' \r" +


                                                 " ");

            theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = ReturnType.DataTable;
            var errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "GetShiftInfo", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            return theData.ResultsDataTable;
        }


        public DataTable GetActivity(string section, string _bookdate)
        {
            theData.SqlStatement = string.Format(" \r" +


                                                "  select max(p.Activity)Activity  \r" +
                                                  " from planning p, Section_Complete s \r" +
                                                  " where s.Prodmonth = p.prodmonth \r" +
                                                  " and s.SectionID = p.Sectionid \r" +
                                                  " and p.calendardate = '" + _bookdate + "' \r" +
                                                  " and s.SectionID_1 = '" + section + "' \r" +

                                                 " ");

            theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = ReturnType.DataTable;
            var errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "GetShiftInfo", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            return theData.ResultsDataTable;

            
        }

        public DataTable get_WorkplacesSTP(string prodmonth, string sectionId)
		{
			theData.SqlStatement = "[sp_Load_BookABSStoping]";
			theData.queryExecutionType = ExecutionType.StoreProcedure;
			theData.ResultsTableName = "STP_Bookings";

			SqlParameter[] _paramCollection =
			{
				theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, prodmonth),
				theData.CreateParameter("@SectionID", SqlDbType.Text, 20, sectionId),
				theData.CreateParameter("@DaysBackdate", SqlDbType.Int, 2, 0),
				theData.CreateParameter("@Shift", SqlDbType.Int, 2, 3)
			};

			theData.ParamCollection = _paramCollection;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_WorkplacesSTP", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable get_WorkplacesDev(string _prodmonth, string _peername)
		{
			theData.SqlStatement = "[sp_Load_BookABSDevelopment]";
			theData.queryExecutionType = ExecutionType.StoreProcedure;
			theData.ResultsTableName = "Dev_Bookings";

			SqlParameter[] _paramCollection =
			{
				theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, _prodmonth),
				theData.CreateParameter("@SectionID", SqlDbType.Text, 50, _peername),
				theData.CreateParameter("@DaysBackdate", SqlDbType.Int, 2, 0),
				theData.CreateParameter("@Shift", SqlDbType.Int, 2, 3)
			};

			theData.ParamCollection = _paramCollection;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_WorkplacesDev", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable get_Problems_Types(int TheActivity)
		{
			theData.SqlStatement = "select PROBLEMTypeID + ':' + Description ProblemType \r\n " +
			                       " from CODE_PROBLEM_TYPE where Activity = '" + TheActivity + "' \r\n " +
			                       " and Deleted <> 'Y' \r\n " +
			                       " order by convert(Numeric(7,0),PROBLEMTypeID) ";
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_Problems_Types", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable get_Problems_Groups(int TheActivity, string ProblemGroup)
		{
			theData.SqlStatement = " select pt.PROBLEMID + ':' + cp.Description ProblemNote \r\n " +
			                       " from PROBLEM_TYPE pt \r\n " +
			                       " inner join CODE_PROBLEM cp on \r\n " +
			                       "   pt.ProblemID = cp.PROBLEMID and \r\n " +
			                       "   pt.Activity = cp.activity \r\n " +
			                       " where pt.ProblemTypeID = '" + ProblemGroup + "' and \r\n " +
			                       "       pt.activity = '" + TheActivity + "' and cp.Deleted <> 'Y'";
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_Problems_Groups", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}


        public DataTable get_PlannedStoppages(int TheActivity)
        {
            theData.SqlStatement = "select CycleCode+':'+Description PS from (Select CycleCode, Description, \r\n " +
                                   " case when stoping = 'Y' then '0'when developement = 'Y' then '1' \r\n " +
                                   " when Ledging = 'Y' then '9' end Activity from Code_Cycle \r\n " +
                                   " where canblast = 0 and cyclecode <> '')A where Activity = '" + TheActivity + "' ";
            theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = ReturnType.DataTable;
            var errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_Problems_Types", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            return theData.ResultsDataTable;
        }

        public DataTable get_Problems_Desc(string TheActivity, string ProblemID)
		{
			theData.SqlStatement = " select PROBLEMID + ':' + Description ProblemDesc \r\n " +
			                       " from CODE_PROBLEM  \r\n " +
			                       " where ProblemID = '" + ProblemID + "' and \r\n " +
			                       "       activity = '" + TheActivity + "' and Deleted <> 'Y'";
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_Problems_Desc", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable get_DetailStoping(string prodmonth, string peername, string bookdate)
		{
			theData.SqlStatement = "[sp_Load_BookABSStoping_Detail]";
			theData.queryExecutionType = ExecutionType.StoreProcedure;
			theData.ResultsTableName = "Stp_Bookings_Detail";

			SqlParameter[] _paramCollection =
			{
				theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, prodmonth),
				theData.CreateParameter("@SectionID", SqlDbType.Text, 20, peername),
				theData.CreateParameter("@BookDate", SqlDbType.VarChar, 10, bookdate)
			};

			theData.ParamCollection = _paramCollection;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_DetailStoping", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public DataTable get_DetailDevelopment(string prodmonth, string peername, string bookdate)
		{
			theData.SqlStatement = "[sp_Load_BookABSDevelopment_Detail]";
			theData.queryExecutionType = ExecutionType.StoreProcedure;
			theData.ResultsTableName = "Dev_Bookings_Detail";

			SqlParameter[] _paramCollection =
			{
				theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, prodmonth),
				theData.CreateParameter("@SectionID", SqlDbType.Text, 20, peername),
				theData.CreateParameter("@BookDate", SqlDbType.VarChar, 10, bookdate)
			};

			theData.ParamCollection = _paramCollection;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "get_DetailDevelopment", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return theData.ResultsDataTable;
			}
			return theData.ResultsDataTable;
		}

		public bool Save_Stoping(DataTable dtStp, string adjbook, string lblmeas, string checkmeaslvl)
		{
			theData.SqlStatement = "";
			foreach (DataRow dr in dtStp.Rows)
			{
				var CheckSqm = "";
				if (dr["CheckSqm"] != null)
				{
					if (dr["CheckSqm"].ToString() != "")
					{
						CheckSqm = dr["CheckSqm"].ToString();
					}
				}

				var AdjSqm = "";
				var AdjTons = "";
				var AdjGrams = "";
				if (adjbook == "Y")
				{
					if (dr["AdjSqm"] != null)
					{
						if (dr["AdjSqm"].ToString() != "")
						{
							AdjSqm = dr["AdjSqm"].ToString();
							AdjTons = dr["AdjTons"].ToString();
							AdjGrams = dr["AdjGrams"].ToString();
						}
					}
				}

				var mofc = "";
				if (lblmeas == "Check Measurement Day")
				{
					mofc = dr["MOFC"].ToString();
				}
				var _absnote = "";
				if (dr["ABSNotes"] != null)
				{
					if (dr["ABSNotes"].ToString() != "")
					{
						_absnote = dr["ABSNotes"].ToString();
						_absnote = _absnote.Replace("'", "''");
					}
				}
				var _sbossnote = "";
				if (dr["ProblemID"].ToString() != "")
				{
					if (dr["SBossNotes"] != null)
					{
						if (dr["SBossNotes"].ToString() != "")
						{
							_sbossnote = dr["SBossNotes"].ToString();
							_sbossnote = _sbossnote.Replace("'", "''");
						}
					}
				}
				theData.SqlStatement = theData.SqlStatement +
				                       " update planning set \r\n " +
				                       " BookCode = '" + dr["BookCodeStp"] + "', \r\n " +
				                       " ABSCode = '" + dr["ABSCodeDisplay"] + "', ABSNotes = '" + _absnote + "', \r\n " +
				                       " ABSPicNo = '" + dr["ABSPicNo"] + "', ABSPrec = '" + dr["ABSPrec"] + "', \r\n " +
				                       " BookFL = '" + dr["BookFL"] + "' , BookMetresAdvance = '" + dr["BookAdv"] + "' , \r\n " +
				                       " BookSqm = '" + dr["BookSqm"] + "', \r\n " +
				                       " BookReefAdv = '" + dr["BookAdvReef"] + "', \r\n " +
				                       " BookWasteAdv = '" + dr["BookAdvWaste"] + "', \r\n " +
									   " BookReefSqm = '" + dr["BookReefSqm"] + "', \r\n " +
				                       " BookWasteSqm = '" + dr["BookWasteSqm"] + "', \r\n " +
				                       " BookTons =  '" + dr["BookTons"] + "', \r\n " +
				                       " BookReefTons =  '" + dr["BookReefTons"] + "', \r\n " +
				                       " BookWasteTons =  '" + dr["BookWasteTons"] + "', \r\n " +
				                       " BookGrams =  '" + dr["BookGrams"] + "', \r\n " +
				                       " BookCmgt = '" + dr["BookCmgt"] + "', \r\n " +
				                       " BookSW   =  '" + dr["BookSW"] + "', \r\n " +
				                       " BookCubicMetres   =  '" + dr["BookCubicMetres"] + "', \r\n " +
				                       " BookCubicTons   =  '" + dr["BookCubicTons"] + "', \r\n " +
				                       " BookCubicGT   =  '" + dr["BookCubicGT"] + "', \r\n " +
				                       " BookCubicGrams   =  '" + dr["BookCubicGrams"] + "', \r\n " +
				                       " ProblemID = '" + dr["ProblemID"] + "', \r\n " +
				                       " SBossNotes = '" + _sbossnote + "', \r\n " +
				                       " CausedLostBlast = '" + dr["CausedLostBlast"] + "' \r\n ";
				if (lblmeas == "Check Measurement Day")
				{
					//if (checkmeaslvl == "MO")
					//{
						theData.SqlStatement = theData.SqlStatement + " , CheckSqm = '" + CheckSqm + "'  ";
						theData.SqlStatement = theData.SqlStatement + " , AdjSqm  = '" + AdjSqm + "'  ";
						theData.SqlStatement = theData.SqlStatement + " , AdjTons  = '" + AdjTons + "'  ";
						theData.SqlStatement = theData.SqlStatement + " , AdjCont  = '" + AdjGrams + "'  ";
						theData.SqlStatement = theData.SqlStatement + " , MOFC  = '" + mofc + "'  ";
					//}
				}
				//else
				//{
				//	theData.SqlStatement = theData.SqlStatement + " , CheckSqm = 0 ";
				//	theData.SqlStatement = theData.SqlStatement + " , AdjSqm  = 0 ";
				//	theData.SqlStatement = theData.SqlStatement + " , AdjTons  = 0  ";
				//	theData.SqlStatement = theData.SqlStatement + " , AdjCont  = 0  ";
				//}

				theData.SqlStatement = theData.SqlStatement +
				                       " where ProdMonth =  '" + dr["ProdMonth"] + "' and \r\n " +
				                       "       SectionID = '" + dr["SectionID"] + "' and \r\n " +
				                       "       WorkplaceID =  '" + dr["WPID"] + "' and \r\n " +
				                       "       Activity = '" + dr["Activity"] + "' and \r\n " +
				                       "       CalendarDate = '" + dr["CalendarDate"] + "' and \r\n " +
				                       "       PlanCode = 'MP' \r\n ";

				//if (dr["CalendarDate"].ToString() == String.Format("{0:yyyy-MM-dd}", DateTime.Now))
				//{
				//    theData.SqlStatement = theData.SqlStatement + 
				//        " update planning \r\n " +
				//        " set ABSCode = '" + dr["ABSCodeDisplay"] + "', \r\n " +
				//        "     ABSPrec = '" + dr["ABSCPrec"] + "', \r\n " +
				//        "     ABSPicNo = '" + dr["ABSPicNo"] + "' \r\n " +
				//        " where ProdMonth =  '" + dr["ProdMonth"] + "'' and \r\n " + 
				//        "       SectionID = '" + dr["SectionID"] + "' and \r\n " +
				//        "       WorkplaceID =  '" + dr["WPID"] + "'' and \r\n " +
				//        "       Activity = '" + dr["Activity"] + "' and \r\n " +
				//        "       CalendarDate = '" + dr["CalendarDate"] + "' and \r\n " +
				//        "       PlanCode = 'MP' \r\n ";
				//}

				// do Audit trail

				//_dbMan.SqlStatement = _dbMan.SqlStatement + "insert into booking_Audit values( getdate() ,'" + String.Format("{0:yyyy-MM-dd}", dtBookDate.Value) + "', '" + workplaceid + "', '" + sec + "' ";
				//_dbMan.SqlStatement = _dbMan.SqlStatement + ",'' , '0',   '" + booksqm + "', '0', '" + machine + "', '" + clsUserInfo.UserID.ToString() + "') ";
			}
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "Save_Stoping", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return false;
			}
			return true;
		}

		public bool Save_Development(DataTable dtDev)
		{
			theData.SqlStatement = "";
			foreach (DataRow dr in dtDev.Rows)
			{
				var _absnote = "";
				if (dr["ABSNotes"] != null)
				{
					if (dr["ABSNotes"].ToString() != "")
					{
						_absnote = dr["ABSNotes"].ToString();
						_absnote = _absnote.Replace("'", "''");
					}
				}
				var _sbossnote = "";
				if (dr["SBossNotes"] != null)
				{
					if (dr["SBossNotes"].ToString() != "")
					{
						_sbossnote = dr["SBossNotes"].ToString();
						_sbossnote = _sbossnote.Replace("'", "''");
					}
				}
				theData.SqlStatement = theData.SqlStatement +
				                       " update planning set \r\n " +
				                       " BookCode = '" + dr["BookCodeDev"] + "', \r\n " +
				                       " ABSCode = '" + dr["ABSCodeDisplay"] + "', ABSNotes = '" + _absnote + "', \r\n " +
				                       " ABSPicNo = '" + dr["ABSPicNo"] + "', ABSPrec = '" + dr["ABSPrec"] + "', \r\n " +
				                       //" BookHeight = '" + dr["DHeight"] + "' , \r\n " +
				                       // " BookWidth = '" + dr["DWidth"] + "' , \r\n " +
				                       " PegID = '" + dr["PegID"] + "' , \r\n " +
				                       " PegDist = '" + dr["Pegdist"] + "' , \r\n " +
				                       " PegToFace = '" + dr["PegToFace"] + "', \r\n " +
				                       " BookMetresAdvance = " + dr["BookAdv"] + " , \r\n " +
				                       " BookReefAdv = " + dr["BookReefAdv"] + " , \r\n " +
				                       " BookWasteAdv = " + dr["BookWasteAdv"] + " , \r\n " +
				                       " BookSqm = 0, \r\n " +
				                       " BookReefSqm = 0, \r\n " +
				                       " BookWasteSqm = 0, \r\n " +
				                       " BookTons =  " + dr["BookTons"] + ", \r\n " +
				                       " BookReefTons =  " + dr["BookReefTons"] + ", \r\n " +
				                       " BookWasteTons =  " + dr["BookWasteTons"] + ", \r\n " +
				                       " BookGrams =  " + dr["BookGrams"] + ", \r\n " +
				                       " BookCmgt = " + dr["cmgt"] + ", \r\n " +
				                       " BookCubicMetres   =  '" + dr["BookCubicMetres"] + "', \r\n " +
				                       " BookCubicTons   =  '" + dr["BookCubicTons"] + "', \r\n " +
				                       " BookCubicGT   =  '" + dr["BookCubicGT"] + "', \r\n " +
				                       " BookCubicGrams   =  '" + dr["BookCubicGrams"] + "', \r\n " +
				                       " ProblemID = '" + dr["ProblemID"] + "', \r\n " +
				                       " SBossNotes = '" + _sbossnote + "', \r\n " +
				                       " CausedLostBlast = '" + dr["CausedLostBlast"] + "' \r\n ";


				theData.SqlStatement = theData.SqlStatement +
				                       " where ProdMonth =  '" + dr["ProdMonth"] + "' and \r\n " +
				                       "       SectionID = '" + dr["SectionID"] + "' and \r\n " +
				                       "       WorkplaceID =  '" + dr["WPID"] + "' and \r\n " +
				                       "       Activity = '" + dr["Activity"] + "' and \r\n " +
				                       "       CalendarDate = '" + dr["CalendarDate"] + "' and \r\n " +
				                       "       PlanCode = 'MP' \r\n ";

				// do Audit trail

				//_dbMan.SqlStatement = _dbMan.SqlStatement + "insert into booking_Audit values( getdate() ,'" + String.Format("{0:yyyy-MM-dd}", dtBookDate.Value) + "', '" + workplaceid + "', '" + sec + "' ";
				//_dbMan.SqlStatement = _dbMan.SqlStatement + ",'' , '0',   '" + booksqm + "', '0', '" + machine + "', '" + clsUserInfo.UserID.ToString() + "') ";
			}
			theData.queryExecutionType = ExecutionType.GeneralSQLStatement;
			theData.queryReturnType = ReturnType.DataTable;
			var errorMsg = theData.ExecuteInstruction();
			if (errorMsg.success == false)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "Save_Development", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return false;
			}
			return true;
		}

		/// <summary>
		///     Generates a StoredProcedure to retrieve Recon Bookings with the given parameters
		/// </summary>
		/// <param name="prodMonth">Production Month in question</param>
		/// <param name="section">Section ID in question</param>
		/// <param name="bookDate">Booking Date</param>
		/// <returns>List of ReconBookingModel containing Progressive & Existing Recon data</returns>
		public IEnumerable<ReconBookingModel> LoadBookingRecon(string prodMonth, string section, string bookDate)
		{
			theData.SqlStatement = "[sp_Select_BookingRecon]";
			theData.queryExecutionType = ExecutionType.StoreProcedure;
			theData.ResultsTableName = "STP_Bookings";

			SqlParameter[] _paramCollection =
			{
				theData.CreateParameter("@Prodmonth", SqlDbType.VarChar, 6, prodMonth),
				theData.CreateParameter("@Section", SqlDbType.VarChar, 30, section),
				theData.CreateParameter("@CalendarDate", SqlDbType.DateTime, 50, bookDate)
			};

			theData.ParamCollection = _paramCollection;
			theData.queryReturnType = ReturnType.DataTable;
			var queryResult = theData.ExecuteInstruction();
			if (!queryResult.success)
			{
				_sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "LoadBookingRecon", queryResult.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
				return null;
			}

			var dt = theData.ResultsDataTable;
			if (dt.Rows.Count == 0)
			{
				return Enumerable.Empty<ReconBookingModel>();
			}

			var resultList = dt.GetDataFromDataTable<ReconBookingModel>();
			return resultList;
		}

        public bool SaveBookingRecon(IEnumerable<ReconBookingModel> reconList)
        {
            theData.SqlStatement = "[sp_InsertUpd_BookingRecon]";
            theData.queryExecutionType = ExecutionType.StoreProcedure;
            theData.ResultsTableName = "bookingRecon";
            theData.queryReturnType = ReturnType.DataTable;

            foreach (var model in reconList)
            {
                string Bar = "";
                try
                {
                    if (model.Approved != true)
                    {
                        Bar = "N";
                    }
                    else
                    {
                        Bar = "Y";
                    }
                }
                catch
                {
                    Bar = "N";
                }
                string user = TUserInfo.UserID;
                if (TUserInfo.UserID == null)
                {
                    user = TUserInfo.UserID.ToString();
                }
                else
                {
                    user = model.UserId;
                    user = UserCurrentInfo.UserID;
                }
                string MOComment = "";
                try
                {
                    MOComment = model.MOComment;
                }
                catch
                {
                    if (model.MOComment == null)
                    {
                        MOComment = "";
                    }
                }


                //TUserInfo.UserID;

                SqlParameter[] paramCollection =
                    {
                    theData.CreateParameter("@Prodmonth", SqlDbType.VarChar, 8, model.ProdMonth),
                    theData.CreateParameter("@SectionID", SqlDbType.VarChar, 10, model.SectionId),
                    theData.CreateParameter("@WorkplaceID", SqlDbType.VarChar, 12, model.WorkplaceId),
                    theData.CreateParameter("@Calendardate", SqlDbType.DateTime, 50, model.Calendardate),
                    theData.CreateParameter("@Activity", SqlDbType.Int, 2, model.ActivityCode),
                    theData.CreateParameter("@ShiftDay", SqlDbType.Int, 2, model.ShiftDay),
                    theData.CreateParameter("@ReconFL", SqlDbType.Decimal, 12, model.ReconFaceLength),
                    theData.CreateParameter("@ReconAdv", SqlDbType.Decimal, 12, model.ReconAdvance),
                    theData.CreateParameter("@ReconCubics", SqlDbType.Decimal, 12, model.ReconCubics),
                    //theData.CreateParameter("@UserID", SqlDbType.VarChar, 50,  model.UserID),                
                    //New
                    theData.CreateParameter("@MOFC", SqlDbType.Decimal, 12, model.MOFC),
                    theData.CreateParameter("@MOComment", SqlDbType.VarChar, 200, model.MOComment),
                    theData.CreateParameter("@BlastBar", SqlDbType.VarChar, 10, Bar),
                };
                    theData.ParamCollection = paramCollection;
                    var executingResult = theData.ExecuteInstruction();
                    if (executingResult.success == false)
                    {
                        _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resHarmonyPAS.systemTag, "clsBookingsABS", "SaveBookingRecon", executingResult.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                        return false;
                    }

                
                }

            return true;

        }
    }
}