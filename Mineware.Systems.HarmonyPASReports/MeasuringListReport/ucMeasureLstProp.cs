using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using FastReport;
using System.Threading;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.MeasuringListReport
{
    public partial class ucMeasureLstProp : DevExpress.XtraEditors.XtraForm
    {
        string theSystemDBTag = "DBHARMONYPAS";
      public   string Connection;
        public ucMeasureLstProp()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string MLmonth = MLPMLbl.Text;
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = Connection;
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.SqlStatement = " select * from dbo.MOMeas_Stope where prodmonth = '" + MLmonth + "' " +
                                        " and workplaceid = '" + MLWPLbl.Text.ToString() + "' and activity = '" + MLACTLbl.Text.ToString() + "' " +
                                        "and sectionid = '" + MLSecLbl.Text.ToString() + "' ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            //richTextBox1.Text = _dbMan.SqlStatement.ToString();

            DataTable Neil = _dbMan.ResultsDataTable;   
            checkEdit1.Checked = false;
            checkEdit2.Checked = false;
            checkEdit3.Checked = false;
            checkEdit4.Checked = false;
            checkEdit5.Checked = false;
            checkEdit6.Checked = false;

            if (Neil.Rows.Count > 0)
            {
                if (Neil.Rows[0]["tick1"].ToString() == "2")
                    checkEdit1.Checked = true;

                if (Neil.Rows[0]["tick2"].ToString() == "2")
                    checkEdit2.Checked = true;

                if (Neil.Rows[0]["tick3"].ToString() == "2")
                    checkEdit3.Checked = true;

                if (Neil.Rows[0]["tick4"].ToString() == "2")
                    checkEdit4.Checked = true;

                if (Neil.Rows[0]["tick5"].ToString() == "2")
                    checkEdit5.Checked = true;

                if (Neil.Rows[0]["tick6"].ToString() == "2")
                    checkEdit6.Checked = true;

            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          //  ucMeasuringListReport uc = new ucMeasuringListReport();
         //   uc.ParamLbl.Text = "";
            this.Close();

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string MLmonth = MLPMLbl.Text;
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString =Connection;
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.SqlStatement = " delete MOMeas_Stope where prodmonth = '" + MLmonth + "' " +
                             " and workplaceid = '" + MLWPLbl.Text.ToString() + "' and activity = '" + MLACTLbl.Text.ToString() + "' " +
                             "and sectionid = '" + MLSecLbl.Text.ToString() + "' ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
            _dbMan.ExecuteInstruction();

            string sqm = "1";
            if (checkEdit1.Checked == true)
                sqm = "2";

            string Dip = "1";
            if (checkEdit2.Checked == true)
                Dip = "2";

            string strike = "1";
            if (checkEdit3.Checked == true)
                strike = "2";

            string wb = "1";
            if (checkEdit4.Checked == true)
                wb = "2";

            string sweep = "1";
            if (checkEdit5.Checked == true)
                sweep = "2";

            string vamp = "1";
            if (checkEdit6.Checked == true)
                vamp = "2";

            string spare = "1";


            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = Connection;
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan1.SqlStatement = " insert into MOMeas_Stope  VALUES('" + MLmonth + "', " +
                                  " '" + MLSecLbl.Text.ToString() + "','" + MLWPLbl.Text.ToString() + "', " +
                                  " '" + MLACTLbl.Text.ToString() + "','" + sqm + "', '" + Dip + "','" + strike + "','" + wb + "','" + sweep + "','" + vamp + "', '" + spare + "' " +
                                  " ) ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.longNumber;
            _dbMan1.ExecuteInstruction();

            //MsgFrm.Text = "Record Inserted";
            //Procedures.MsgText = "Problem Group Added";
            //MsgFrm.Show();

            this.Close();
        }

        private void ucMeasureLstProp_Load(object sender, EventArgs e)
        {
            simpleButton1_Click(null ,null );
        }
    }
}
