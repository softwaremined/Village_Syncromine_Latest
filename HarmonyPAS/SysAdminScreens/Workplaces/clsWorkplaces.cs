using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using DevExpress.XtraGrid.Columns;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Production.SysAdminScreens.Workplaces
{
    class clsWorkplaces : clsBase
    {
        public DataTable Load_cmbWPSearchSurfaceWP()
        {
            theData.SqlStatement = " select '<<<ALL>>>' TypeCode \r\n " +
               " union All \r\n " +
               " select distinct(t.TypeCode +':'+  t.Description) TypeCode FROM WPType_Setup s \r\n " +
               "  inner join CODE_WPTYPE t on\r\n " +
               "    t.TypeCode = s.TypeCode\r\n " +
               "  WHERE s.SetupCode = 'SU' and t.Selected = 'Y' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;
        }

        public DataTable Load_cmbWPSearchUnNonWP()
        {


            theData.SqlStatement = " select '<<<ALL>>>' TypeCode \r\n " +
                " union All \r\n " +
                " select distinct(t.TypeCode +':'+  t.Description) TypeCode FROM WPType_Setup s \r\n " +
                "  inner join CODE_WPTYPE t on\r\n " +
                "    t.TypeCode = s.TypeCode\r\n " +
                "  WHERE s.SetupCode = 'OUG' and t.Selected = 'Y' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;

        }

        public DataTable Load_cmbWPSearchUnProdWP()
        {

            theData.SqlStatement = " select '<<<ALL>>>' TypeCode \r\n " +
                " union All \r\n " +
                " select distinct(t.TypeCode +':'+  t.Description) TypeCode FROM WPType_Setup s \r\n " +
                "  inner join CODE_WPTYPE t on\r\n " +
                "    t.TypeCode = s.TypeCode\r\n " +
                "  WHERE s.SetupCode in ( 'S','D') and t.Selected = 'Y' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;

        }

        public DataTable Load_cmbWPSearchStatus()
        {

            theData.SqlStatement = " select '<<<ALL>>>' WPStatus union All \r\n " +
                    "   select WPStatus +':' + Description WPStatus from Code_WPStatus ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            return theData.ResultsDataTable;

        }

        public DataTable LoadWorkplace(string wptype)
        {
            if (wptype == "")
            {
                theData.SqlStatement = " select w.*, e.description Endtype, r.description Reef, o.name Level1 from workplace w " +
                                           "left outer join endtype e on  w.endtypeid = e.endtypeid " +
                                           "left outer join reef r on  w.reefid = r.reefid " +
                                           "left outer join oreflowentities o on  w.oreflowid = o.oreflowid ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
            else
            {
                theData.SqlStatement = " select w.*, e.description Endtype, r.description Reef, o.name Level1 from workplace w " +
                                          "left outer join endtype e on  w.endtypeid = e.endtypeid " +
                                          "left outer join reef r on  w.reefid = r.reefid " +
                                          "left outer join oreflowentities o on  w.oreflowid = o.oreflowid INNER JOIN CODE_WPTYPE t on  "+
                                          "  t.TypeCode = w.TypeCode inner join WPTYPE_SETUP s on      "+
                                          "   t.TypeCode = s.TypeCode WHERE s.SetupCode in('"+ wptype+"') and t.Selected = 'Y' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
                return theData.ResultsDataTable;
            }
        }

      public   void SaveWorkplace(string WorkplaceIDTxt,string WpDescTxt, string ddlDivision, string ddlWPType, string ddlWPLevel, string ddlGrid,
            string ddlDetail, string ddlDirection, string ddlNumber, string ddlDescription, string ddlDescriptionNumber, string ddlReef,
            string cbeSubSections,string cbeStopingProcessCodes, string EndTypeLU, string EndWidthTxt, string EndHeightTxt, string workplacetype, string txtDensity,string txtBrokenRockDensity,
            bool InActiveReason, string Edit, string rbReef, bool PriorityCbx, string lblStatus, string RiskRatingtxt, string Statuscmb,string  rbtHotCold,
           string DefaultAdvEdit,bool cbInactive,string cboxInActiveReason, bool theClassify)
        {
          //  Valid = "N";
            if (WorkplaceIDTxt == "")
            {
                MessageBox.Show("Please enter a valid Workplace ID", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //WorkplaceIDTxt.Focus();
                return;
            }

            String divisionID = "";
            if (ddlDivision == "")
            {
                MessageBox.Show("Please choose a division", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //ddlDivision.Focus();
                return;
            }
            else
               // divisionID = procs.ExtractBeforeColon(ddlDivision.Text);
            divisionID = ExtractBeforeColon(ddlDivision);

           String wptypeID = "";
            if (ddlWPType == "")
            {
                MessageBox.Show("Please choose a workplace type", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //ddlWPType.Focus();
                return;
            }
            else
                //  wptypeID = procs.ExtractBeforeColon(ddlWPType.Text);
                wptypeID = ExtractBeforeColon(ddlWPType);

            String levelID = "";
            if (ddlWPLevel == "")
            {
                MessageBox.Show("Please choose a level", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //ddlWPLevel.Focus();
                return;
            }
            else
            {
                // levelID = ddlWPLevel.EditValue.ToString();



                theData.SqlStatement = "SELECT oreflowid FROM oreflowentities WHERE oreflowcode = 'Lvl' and name = '" + ExtractAfterColon(ddlWPLevel) + "'";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
             
                    DataTable dt1 = new DataTable();
                    dt1 = theData.ResultsDataTable;
                    foreach (DataRow dr in dt1.Rows)
                    {
                    levelID = dr["oreflowid"].ToString();
                    }
               

              //  levelID = ExtractBeforeColon(ddlWPLevel);
                //MWDataManager.clsDataAccess _dbMann = new MWDataManager.clsDataAccess();
                ////_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                //_dbMann.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbMann.queryReturnType = MWDataManager.ReturnType.DataTable;
                //String sqlStatement = "SELECT oreflowid FROM oreflowentities WHERE oreflowcode = 'Lvl' and name = '" + procs.ExtractAfterColon(ddlWPLevel.Text) + "'";
                //_dbMann.SqlStatement = sqlStatement;
                //_dbMann.ExecuteInstruction();
                //DataTable lt = _dbMann.ResultsDataTable;
                //levelIDlbl.Text = lt.Rows[0][0].ToString();
                //levelID = levelIDlbl.Text;
            }

            String gridID = "";
            if (ddlGrid == "")
            {
                MessageBox.Show("Please choose a grid", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //ddlGrid.Focus();
                return;
            }
            else
               // gridID = procs.ExtractBeforeColon(ddlGrid.Text);
            gridID = ExtractBeforeColon(ddlGrid);

            String detailID = "";
            if (ddlDetail == "")
            {
                MessageBox.Show("Please choose a detail record", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //ddlDetail.Focus();
                return;
            }
            else
              //  detailID = procs.ExtractBeforeColon(ddlDetail.Text);
            detailID = ExtractBeforeColon(ddlDetail);

            String directionID = "";
            if (ddlDirection == "")
            {
                MessageBox.Show("Please choose a direction", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //ddlDirection.Focus();
                return;
            }
            else
             //   directionID = procs.ExtractBeforeColon(ddlDirection.Text);
            directionID = ExtractBeforeColon(ddlDirection);

            String numberID = "";
            if (ddlNumber == "")
            {
                MessageBox.Show("Please choose a number", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //ddlNumber.Focus();
                return;
            }
            else
            //    numberID = procs.ExtractBeforeColon(ddlNumber.Text);
            numberID = ExtractBeforeColon(ddlNumber);

            String descriptionID = "";
            if (ddlDescription == "")
            {
                MessageBox.Show("Please choose a description", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //ddlDescription.Focus();
                return;
            }
            else
              //  descriptionID = procs.ExtractBeforeColon(ddlDescription.Text);
            descriptionID = ExtractBeforeColon(ddlDescription);

            String descriptionNumberID = "";
            if (ddlDescriptionNumber == "")
            {
                MessageBox.Show("Please choose a description number", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //ddlDescriptionNumber.Focus();
                return;
            }
            else
               // descriptionNumberID = procs.ExtractBeforeColon(ddlDescriptionNumber.Text);
            descriptionNumberID = ExtractBeforeColon(ddlDescriptionNumber);

            String reefID = "";
            if (workplacetype == "S" || workplacetype == "D")
            {
                if (ddlReef == "")
                {
                    MessageBox.Show("Please choose a reef", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //ddlReef.Focus();
                    return;
                }
                else if (cbeSubSections == "")
                {
                    MessageBox.Show("Please select sub section.", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //this.cbeSubSections.Focus();
                    return;
                }
                else
                //    reefID = procs.ExtractBeforeColon(ddlReef.Text);
                reefID = ExtractBeforeColon(ddlReef);
            }

            String Act = "";
            String endType = "";
            String endWidth = null;
            String endHeight = null;
            if (workplacetype == "D")
            {
                if (EndTypeLU == "")
                {
                    MessageBox.Show("Please choose an End Type.", "Data Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //EndTypeLU.Focus();
                    return;
                }
                Act = "1";
                endType = EndTypeLU;
                endWidth = EndWidthTxt;
                endHeight = EndHeightTxt;
            }

            if (workplacetype == "S")
            {
                //if (RiskRatingtxt.Text == "")
                //{
                //    MessageBox.Show("Please enter Risk Rating between 1 and 23.", "Incorrect Risk Rating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    RiskRatingtxt.Focus();
                //    return;
                //}

                //if (Convert.ToInt16(RiskRatingtxt.Text) > 23 || Convert.ToInt16(RiskRatingtxt.Text) < 1)
                //{
                //    MessageBox.Show("Please enter Risk Rating between 1 and 23.", "Incorrect Risk Rating", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    RiskRatingtxt.Focus();
                //    return;
                //}
                Act = "0";
            }

            if (workplacetype == "SU")
                Act = "10";

            if (workplacetype == "OUG")
                Act = "11";

            //if (InActiveReason == true)
            //{
            //    if (Convert .ToBoolean (InActiveReason) == false )
            //    {
            //        MessageBox.Show("Please enter a Reason.", "No Reason Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        //cboxInActiveReason.Focus();
            //        return;
            //    }
            //}


            //Now Save

            theData.SqlStatement = "SELECT * FROM Code_Wpdivision WHERE DivisionCode = '" + divisionID + "'";
                  theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
      

            string mine = "";
            //if (this.blnIsCentralizedDatabase)
            //{
            //    if (_dbMan.ResultsDataTable != null && _dbMan.ResultsDataTable.Rows.Count > 0)
            //    {
            //        mine = _dbMan.ResultsDataTable.Rows[0]["Mine"].ToString();
            //    }
            //}

            if (Edit == "A")
            {

                theData.SqlStatement = "SELECT * FROM Workplace WHERE WorkplaceID = '" + WorkplaceIDTxt + "'";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();
              

                if (theData.ResultsDataTable.Rows.Count > 0)
                {
                   MessageBox.Show("WorkplaceID already exists.", "Duplicate Workplace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (Edit == "A")
                theData.SqlStatement = "SELECT * FROM Workplace WHERE WorkplaceID != '" + WorkplaceIDTxt + "' AND Description =  '" + WpDescTxt + "' AND Activity = '" + Act + "'" ;

            if (Edit == "E")
                theData.SqlStatement = "SELECT * FROM Workplace WHERE Workplaceid = '" + WorkplaceIDTxt + "'" ;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            DataTable dt = theData.ResultsDataTable;

            String RW = "W";
            string Priority = "";
            if (rbReef == "0")
                RW = "R";
            if (PriorityCbx == true)
                Priority = "Y";
            else
                Priority = "N";

            decimal fDensity = 0;
            if (!string.IsNullOrWhiteSpace(txtDensity))
                fDensity = Convert.ToDecimal(txtDensity);

            // ********* START **********
            // Shaista Anjum Added for BrokenRockDensity : 18/JAN/2013
            decimal fBrokenRockDensity = 0;
            if (!string.IsNullOrWhiteSpace(txtBrokenRockDensity))
                fBrokenRockDensity = Convert.ToDecimal(txtBrokenRockDensity);
            // ********* END **********

            if (dt.Rows.Count != 0)
            {
                if (Edit == "A")
                {
                    //MessageBox.Show("Cannot Insert Duplicate Workplace", "Duplicate Workplace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {

                    String sqlStatement = "";
                    sqlStatement += "UPDATE Workplace SET ";
                    sqlStatement += "Description = '" + WpDescTxt + "', ";
                    sqlStatement += "DivisionCode = '" + divisionID + "', ";
                    sqlStatement += "TypeCode = '" + wptypeID + "', ";
                    sqlStatement += "OreFlowID = '" + levelID + "', ";
                    sqlStatement += "GridCode = '" + gridID + "', ";
                    sqlStatement += "DetailCode = '" + detailID + "', ";
                    sqlStatement += "Direction = '" + directionID + "', ";
                    sqlStatement += "NumberCode = '" + numberID + "', ";
                    sqlStatement += "DescrCode = '" + descriptionID + "', ";
                    sqlStatement += "DescrNoCode = '" + descriptionNumberID + "' ";
                    // if (cbInactive.Checked) sqlStatement += "Inactive = 'Y' "; else sqlStatement += "Inactive = 'N' ";

                    sqlStatement += ", SubSection = " + (ExtractBeforeColon ( cbeSubSections) == "" ? "NULL" : ExtractBeforeColon(cbeSubSections)) + ", ";
                    sqlStatement += "ProcessCode = " + (ExtractBeforeColon(cbeStopingProcessCodes) == "" ? "NULL" : ExtractBeforeColon(cbeStopingProcessCodes));
                    if (lblStatus == "Status : Pending Classification")
                    {
                        sqlStatement += ",WPStatus = 'P' ";
                        sqlStatement += ",Inactive = '' ";
                    }
                    if (lblStatus == "Status : Active")
                    {
                        sqlStatement += ",WPStatus = 'A' ";
                        sqlStatement += ",Inactive = 'N' ";
                    }
                    if (lblStatus == "Status : Inactive")
                    {
                        sqlStatement += ",WPStatus = 'I' ";
                        sqlStatement += ",Inactive = 'Y' ";
                    }
                    if (rbtHotCold == "1")
                        sqlStatement += ",Classification = 'C' ";
                    else
                        if (rbtHotCold == "0")
                        sqlStatement += ",Classification = 'H' ";
                    else
                        sqlStatement += ",Classification = '' ";



                    if (workplacetype == "S")
                    {
                        sqlStatement += ", RiskRating = '" + Convert.ToInt16(RiskRatingtxt) + "', ";
                        sqlStatement += "ReefID = '" + reefID + "', ";
                       // sqlStatement += "Line = '" + LineTxt.Text + "', ";
                        sqlStatement += "Line = '', ";
                        sqlStatement += "Mech = '', ";
                       // sqlStatement += "CAP = '" + Line2Txt.Text + "', ";  //Panel / End
                        sqlStatement += "CAP = '', ";  //Panel / End
                        sqlStatement += "ReefWaste = '" + RW + "', ";
                        sqlStatement += "EndTypeID = null, ";
                       // sqlStatement += "StopingCode = '" + Statuscmb.Text + "', ";
                        sqlStatement += "StopingCode = '', ";
                        sqlStatement += "EndWidth = null, ";
                        sqlStatement += "EndHeight = null, ";
                        sqlStatement += "Priority = '', ";
                        sqlStatement += "Density = " + fDensity + ", ";
                        sqlStatement += "BrokenRockDensity = " + fBrokenRockDensity + ", "; // Shaista Anjum Added for BrokenRockDensity : 18/JAN/2013
                        sqlStatement += "DefaultAdv = '" + DefaultAdvEdit + "' ";
                    }

                    if (workplacetype == "D")
                    {
                        sqlStatement += ", RiskRating = '" + Convert.ToInt16(RiskRatingtxt) + "', ";
                        sqlStatement += "ReefID = '" + reefID + "', ";
                        //sqlStatement += "Line = '" + LineTxt.Text + "', ";
                        sqlStatement += "Line = '', ";
                        sqlStatement += "Mech = '', ";
                        //sqlStatement += "CAP = '" + Line2Txt.Text + "', ";  //Panel / End
                        sqlStatement += "CAP = '', ";  //Panel / End
                        sqlStatement += "ReefWaste = '" + RW + "', ";
                        sqlStatement += "EndTypeID = '" + endType + "', ";
                        sqlStatement += "StopingCode = '', ";
                        sqlStatement += "EndWidth = '" + endWidth + "', ";
                        sqlStatement += "EndHeight = '" + endHeight + "', ";
                        sqlStatement += "Density = " + fDensity + ", ";
                        sqlStatement += "BrokenRockDensity = " + fBrokenRockDensity + ", "; // Shaista Anjum Added for BrokenRockDensity : 18/JAN/2013
                        sqlStatement += "Priority = '" + Priority + "' ";
                    }

                    sqlStatement += " WHERE WorkplaceID = '" + WorkplaceIDTxt + "'" ;
                    theData.SqlStatement = sqlStatement;
                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    theData.ExecuteInstruction();

                    if (InActiveReason == true)
                    {
                        DateTime theDate = DateTime.Now;
                        String sqlStatement1 = "";
                        sqlStatement1 += "INSERT INTO WorkPlace_Inactivation (WorkplaceID, CalendarDate, UserID, ";
                        sqlStatement1 += "NewStatus, Reason ";
                        //if (this.blnIsCentralizedDatabase)
                        //{
                        //    sqlStatement1 += ", Mine ";
                        //}
                        String sqlStatementValues1 = "";
                        sqlStatementValues1 += "VALUES ('" + WorkplaceIDTxt + "', '" + theDate + "', ";
                        sqlStatementValues1 += "  '" + TUserInfo .UserID  + "', ";
                        if (cbInactive == true)
                            sqlStatementValues1 += " 'I', ";
                        else
                            sqlStatementValues1 += " 'A', ";
                        sqlStatementValues1 += " '" + cboxInActiveReason + "'";
                        //if (this.blnIsCentralizedDatabase)
                        //{
                        //    sqlStatementValues1 += ", '" + mine + "'";
                        //}
                        sqlStatement1 += ") " + sqlStatementValues1 + ") ";
                        theData.SqlStatement = sqlStatement1;
                        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        theData.ExecuteInstruction();
                      
                    }
                    if (theClassify == true)
                    {
                        DateTime theDate = DateTime.Now;
                        String sqlStatement1 = "";
                        sqlStatement1 += "INSERT INTO WorkPlace_Classification (WorkplaceID, CalendarDate, UserID, ";
                        sqlStatement1 += "NewClassification ";
                        //if (this.blnIsCentralizedDatabase)
                        //{
                        //    sqlStatement1 += ", Mine ";
                        //}
                        String sqlStatementValues1 = "";
                        sqlStatementValues1 += "  VALUES ('" + WorkplaceIDTxt + "', '" + theDate + "', ";
                        sqlStatementValues1 += "  '" + TUserInfo.UserID + "', ";
                        if (rbtHotCold == "1")
                            sqlStatementValues1 += " 'C' ";
                        else
                            if (rbtHotCold == "0")
                            sqlStatementValues1 += " 'H' ";
                        else
                            sqlStatementValues1 += " '' ";
                        //if (this.blnIsCentralizedDatabase)
                        //{
                        //    sqlStatementValues1 += ", '" + mine + "'";
                        //}
                        sqlStatement1 += ") " + sqlStatementValues1 + ") ";
                        theData.SqlStatement = sqlStatement1;
                        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        theData.ExecuteInstruction();
                    }

                    //frmMessageFrm MsgFrm = new frmMessageFrm();
                    //MsgFrm.Text = "Record Updated";
                    //Procedures.MsgText = "workplace Updated";
                    //MsgFrm.Show();
                }
            }
            else
            {
                DateTime theDate = DateTime.Now;
                String sqlStatement = "";
                sqlStatement += "INSERT INTO Workplace (WorkplaceID, Description, Activity, DivisionCode, TypeCode, ";
                sqlStatement += "OreFlowID, GridCode, ";
                sqlStatement += "DetailCode, Direction, NumberCode, DescrCode, DescrNoCode, SubSection, ProcessCode ";

                String sqlStatementValues = "";
                sqlStatementValues += "VALUES ('" + WorkplaceIDTxt + "' ";
                sqlStatementValues += ",'" + WpDescTxt + "' ";
                sqlStatementValues += ",'" + Act + "' ";
                sqlStatementValues += ",'" + divisionID + "' ";
                sqlStatementValues += ",'" + wptypeID + "' ";
                sqlStatementValues += ",'" + levelID + "' ";
                sqlStatementValues += ",'" + gridID + "' ";
                sqlStatementValues += ",'" + detailID + "' ";
                sqlStatementValues += ",'" + directionID + "' ";
                sqlStatementValues += ",'" + numberID + "' ";
                sqlStatementValues += ",'" + descriptionID + "' ";
                sqlStatementValues += ",'" + descriptionNumberID + "' ";
                sqlStatementValues += ",";

                sqlStatementValues += (ExtractBeforeColon ( cbeSubSections) == "" ? "NULL" : ExtractBeforeColon(cbeSubSections));
                sqlStatementValues += ",";
                sqlStatementValues += (ExtractBeforeColon(cbeStopingProcessCodes) == "" ? "NULL" : ExtractBeforeColon(cbeStopingProcessCodes));


                //if (this.blnIsCentralizedDatabase)
                //{
                //    sqlStatement += ", Mine ";
                //    sqlStatementValues += ",'" + mine + "' ";
                //}


                if (workplacetype == "S")
                {
                    sqlStatement += ", RiskRating, ReefID, Line, Mech, CAP, ReefWaste, EndTypeID ";
                    sqlStatement += ", StopingCode, EndWidth, EndHeight ";
                    sqlStatement += ", Priority, Density, BrokenRockDensity, DefaultAdv";// Shaista Anjum Modified for BrokenRockDensity : 18/JAN/2013
                    sqlStatementValues += ", '" + Convert.ToInt16(RiskRatingtxt) + "' ";
                    sqlStatementValues += ", '" + reefID + "' ";
                    sqlStatementValues += ", '' ";
                    sqlStatementValues += ", '' ";  //Panel / End
                    sqlStatementValues += ", '' ";
                    sqlStatementValues += ", '" + RW + "' ";
                    sqlStatementValues += ", null ";
                    sqlStatementValues += ", '" + Statuscmb + "' ";
                    sqlStatementValues += ", null ";
                    sqlStatementValues += ", null ";
                    sqlStatementValues += ", '' ";
                    sqlStatementValues += "," + fDensity;
                    sqlStatementValues += "," + fBrokenRockDensity;// Shaista Anjum Added for BrokenRockDensity : 18/JAN/2013
                    sqlStatementValues += ", '" + DefaultAdvEdit + "'";
                }

                if (workplacetype == "D")
                {
                    sqlStatement += ", RiskRating, ReefID, Line, Mech, Cap, ReefWaste, EndTypeID ";
                    sqlStatement += ", StopingCode, EndWidth, EndHeight, Density, BrokenRockDensity, Priority ";// Shaista Anjum Modified for BrokenRockDensity : 18/JAN/2013
                    sqlStatementValues += ", '" + Convert.ToInt16(RiskRatingtxt) + "' ";
                    sqlStatementValues += ", '" + reefID + "' ";
                    sqlStatementValues += ", '' ";
                    sqlStatementValues += ", '' ";
                    sqlStatementValues += ", '' ";  //Panel / End
                    sqlStatementValues += ", '" + RW + "' ";
                    sqlStatementValues += ", '" + endType + "' ";
                    sqlStatementValues += ", '' ";
                    sqlStatementValues += ", '" + endWidth + "' ";
                    sqlStatementValues += ", '" + endHeight + "' ";
                    sqlStatementValues += "," + fDensity;
                    sqlStatementValues += "," + fBrokenRockDensity;// Shaista Anjum Added for BrokenRockDensity : 18/JAN/2013
                    sqlStatementValues += ", '" + Priority + "' ";
                }
                sqlStatement += ",Inactive, WPStatus, Classification, UserID, CreationDate ";
                if (lblStatus == "Status : Pending Classification")
                {
                    sqlStatementValues += ",'N', 'P' ";
                }
                if (lblStatus == "Status : Active")
                {
                    sqlStatementValues += ",'N','A' ";
                }
                if (lblStatus== "Status : Inactive")
                {
                    sqlStatementValues += ",Y','I' ";
                }
                sqlStatementValues += ", '', '" +TUserInfo  .UserID + "', '" + theDate + "' ";
                sqlStatement += ") " + sqlStatementValues + ") ";

                theData.SqlStatement = sqlStatement;
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();


                String sqlStatement2 = "";
                sqlStatement2 += "update Code_WPDivision";
                sqlStatement2 += " set WPLastUsed =  ";
                sqlStatement2 += " (select (WPLastUsed + 1) aa from Code_WPDivision ";
                sqlStatement2 += " where DivisionCode = '" + ExtractBeforeColon ( ddlDivision) + "')";
                sqlStatement2 += " where DivisionCode = '" + ExtractBeforeColon(ddlDivision) + "' ";
                theData.SqlStatement = sqlStatement2;
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();

            }

            //frmMessageFrm MsgFrm2 = new frmMessageFrm();
            //MsgFrm2.Text = "Record Inserted";
            //Procedures.MsgText = "Workplace Inserted";
            //MsgFrm2.Show();
            //Valid = "Y";
        }

        //private void Load_Workplaces_New()
        //{
        //    string theStatus = "";
        //    if (cmbWPSearchStatus.SelectedIndex != -1)
        //    {
        //        if (cmbWPSearchStatus.SelectedItem.ToString() != "<<<ALL>>>")
        //        {
        //            if (proc.ExtractBeforeColon(cmbWPSearchStatus.SelectedItem.ToString()) == "A")
        //                theStatus = "A";
        //            else
        //                if (proc.ExtractBeforeColon(cmbWPSearchStatus.SelectedItem.ToString()) == "I")
        //                theStatus = "I";
        //            else
        //                    if (proc.ExtractBeforeColon(cmbWPSearchStatus.SelectedItem.ToString()) == "P")
        //                theStatus = "P";
        //        }
        //    }
        //    string theDescription = "";
        //    string theWPType = "";
        //    if (cmbWPSearchUnProdWP.SelectedIndex != -1)
        //    {
        //        if (cmbWPSearchUnProdWP.SelectedItem.ToString() == "<<<ALL>>>")
        //            theDescription = "('D','S')";
        //        else
        //            theWPType = proc.ExtractBeforeColon(cmbWPSearchUnProdWP.SelectedItem.ToString());
        //    }
        //    if (cmbWPSearchUnNonWP.SelectedIndex != -1)
        //    {
        //        if (cmbWPSearchUnNonWP.SelectedItem.ToString() == "<<<ALL>>>")
        //            theDescription = "('OUG')";
        //        else
        //            theWPType = proc.ExtractBeforeColon(cmbWPSearchUnNonWP.SelectedItem.ToString());
        //    }
        //    if (cmbWPSearchSurfaceWP.SelectedIndex != -1)
        //    {
        //        if (cmbWPSearchSurfaceWP.SelectedItem.ToString() == "<<<ALL>>>")
        //            theDescription = "('SU')";
        //        else
        //            theWPType = proc.ExtractBeforeColon(cmbWPSearchSurfaceWP.SelectedItem.ToString());
        //    }
        //    if ((cmbWPSearchUnProdWP.SelectedItem.ToString() == "<<<ALL>>>") &
        //        (cmbWPSearchUnNonWP.SelectedItem.ToString() == "<<<ALL>>>") &
        //        (cmbWPSearchSurfaceWP.SelectedItem.ToString() == "<<<ALL>>>"))
        //    {
        //        theDescription = "";
        //        theWPType = "";
        //    }



        //        MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
        //        _dbMan.SqlStatement = " select distinct(w.WorkplaceID) aa, w.*, e.description Endtype, r.description Reef, o.name Level1 from workplace w \r\n " +
        //                                "left outer join endtype e on  w.endtypeid = e.endtypeid \r\n " +
        //                                "left outer join reef r on  w.reefid = r.reefid \r\n " +
        //                                "left outer join oreflowentities o on  w.oreflowid = o.oreflowid \r\n ";
        //        if (SysSettings.IsCentralized.ToString() == "1")
        //        {
        //            _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.mine = o.mine2";
        //        }

        //        if ((theDescription != "") || (theWPType != ""))
        //        {
        //            _dbMan.SqlStatement = _dbMan.SqlStatement + " left outer join WPType_Setup sc on w.TypeCode = sc.TypeCode \r\n " +
        //                                    " inner join Code_WPType ct on ct.TypeCode = sc.TypeCode and ct.Selected = 'Y' \r\n ";
        //        }

        //        bool theUseAnd = false;
        //        bool theWhere = false;
        //        if (SysSettings.IsCentralized.ToString() == "1")
        //        {
        //            _dbMan.SqlStatement = _dbMan.SqlStatement + " where w.DivisionCode='" + ddlWorkplaceDivision.SelectedValue.ToString() + "' ";
        //            theUseAnd = true;
        //            theWhere = true;
        //        }
        //        if ((theWhere == false) &
        //            ((txtWPSearchText.Text != "") ||
        //            (theDescription != "") ||
        //            (theWPType != "") ||
        //            (theStatus != "")))
        //        {
        //            _dbMan.SqlStatement = _dbMan.SqlStatement + " where ";
        //            theWhere = true;
        //        }
        //        if (theDescription != "")
        //        {
        //            if (theUseAnd == false)
        //            {
        //                _dbMan.SqlStatement = _dbMan.SqlStatement + " sc.SetupCode in " + theDescription + " \r\n ";
        //                theUseAnd = true;
        //            }
        //            else
        //                _dbMan.SqlStatement = _dbMan.SqlStatement + " and sc.SetupCode in " + theDescription + " \r\n ";
        //        }
        //        if (theWPType != "")
        //        {
        //            if (theUseAnd == false)
        //            {
        //                _dbMan.SqlStatement = _dbMan.SqlStatement + " w.TypeCode = '" + theWPType + "' \r\n ";
        //                theUseAnd = true;
        //            }
        //            else
        //                _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.TypeCode = '" + theWPType + "' \r\n ";
        //        }
        //        if (theStatus != "")
        //        {
        //            if (theUseAnd == false)
        //            {
        //                if (theStatus == "A")
        //                    _dbMan.SqlStatement = _dbMan.SqlStatement + " (w.Inactive = 'N' or WPStatus = 'P') \r\n ";
        //                else
        //                    if (theStatus == "I")
        //                    _dbMan.SqlStatement = _dbMan.SqlStatement + " w.Inactive = 'Y'  \r\n ";
        //                else
        //                    _dbMan.SqlStatement = _dbMan.SqlStatement + " w.WPStatus = 'P'  \r\n ";
        //                theUseAnd = true;
        //            }
        //            else
        //            {
        //                if (theStatus == "A")
        //                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and (w.Inactive = 'N' or WPStatus = 'P') \r\n ";
        //                else
        //                    if (theStatus == "I")
        //                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.Inactive = 'Y' \r\n ";
        //                else
        //                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.WPStatus = 'P' \r\n ";
        //                theUseAnd = true;
        //            }
        //        }

        //        if (txtWPSearchText.Text != "")
        //        {
        //            if (rdgrpWPSearchIDName.SelectedIndex == 0)
        //            {
        //                if (theUseAnd == false)
        //                {
        //                    _dbMan.SqlStatement = _dbMan.SqlStatement + " w.WorkplaceID like '" + txtWPSearchText.Text + "%' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "%'\r\n ";
        //                    //  _dbMan.SqlStatement = _dbMan.SqlStatement + " w.WorkplaceID like '" + txtWPSearchText.Text + "' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "'\r\n ";
        //                    theUseAnd = true;
        //                }
        //                else
        //                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.WorkplaceID like '" + txtWPSearchText.Text + "%' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "%'\r\n ";
        //                //_dbMan.SqlStatement = _dbMan.SqlStatement + " and w.WorkplaceID like '" + txtWPSearchText.Text + "' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "'\r\n ";
        //            }
        //            if (rdgrpWPSearchIDName.SelectedIndex == 1)
        //            {
        //                if (theUseAnd == false)
        //                {
        //                    _dbMan.SqlStatement = _dbMan.SqlStatement + " w.Description like '" + txtWPSearchText.Text + "%' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "%'\r\n ";
        //                    //  _dbMan.SqlStatement = _dbMan.SqlStatement + " w.Description like '" + txtWPSearchText.Text + "' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "'\r\n ";
        //                    theUseAnd = true;
        //                }
        //                else
        //                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.Description like '" + txtWPSearchText.Text + "%' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "%'\r\n ";
        //                // _dbMan.SqlStatement = _dbMan.SqlStatement + " and w.Description like '" + txtWPSearchText.Text + "' or w.OldWorkplaceid like '" + txtWPSearchText.Text + "'\r\n ";
        //            }
        //        }
        //        _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        //        _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
        //        _dbMan.ExecuteInstruction();

        //        WorkplaceDt = _dbMan.ResultsDataTable;



        public string ExtractAfterColon(string TheString)
        {
            string AfterColon;

            int index = TheString.IndexOf(":"); // Kry die postion van die :

            AfterColon = TheString.Substring(index + 1); // kry alles na :

            return AfterColon;
        }

        public string ExtractBeforeColon(string TheString)
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



    }
}
