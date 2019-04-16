using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class frmSampleSched : DevExpress.XtraEditors.XtraForm
    {

        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        public frmSampleSched()
        {
            InitializeComponent();
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

        public string ExtractAfterColon(string TheString)
        {
            string AfterColon;

            int index = TheString.IndexOf(":"); // Kry die postion van die :

            AfterColon = TheString.Substring(index + 1); // kry alles na :

            return AfterColon;
        }

        private void frmSampleSched_Load(object sender, EventArgs e)
        {
            //tabControl1.TabPages[0].Text = "  Allocations  ";
            //tabControl1.TabPages[0].Text = "    Problems   ";


            if (Samp2Pnl.Visible == true)
            {
                ResetGrid();
                try
                {

                    SampGrid.Rows.Clear();
                    SampGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 7.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    SampGrid.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    SampGrid.ColumnCount = 9;

                    SampGrid.Columns[0].HeaderText = "Date";
                    SampGrid.Columns[0].Width = 70;
                    SampGrid.Columns[0].ReadOnly = true;


                    SampGrid.Columns[1].HeaderText = "FW";
                    SampGrid.Columns[1].Width = 40;
                    SampGrid.Columns[1].ReadOnly = true;
                    SampGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    SampGrid.Columns[2].HeaderText = "HW";
                    SampGrid.Columns[2].Width = 40;
                    SampGrid.Columns[2].ReadOnly = true;
                    SampGrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    SampGrid.Columns[3].HeaderText = "Chn";
                    SampGrid.Columns[3].Width = 40;
                    SampGrid.Columns[3].ReadOnly = true;
                    SampGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    SampGrid.Columns[4].HeaderText = "SW";
                    SampGrid.Columns[4].Width = 40;
                    SampGrid.Columns[4].ReadOnly = false;
                    SampGrid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    SampGrid.Columns[5].HeaderText = "Alloc. SW";
                    SampGrid.Columns[5].Width = 40;
                    SampGrid.Columns[5].ReadOnly = false;
                    SampGrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;




                    SampGrid.Columns[6].HeaderText = "Note";
                    SampGrid.Columns[6].Width = 300;
                    SampGrid.Columns[6].ReadOnly = false;
                    ((DataGridViewTextBoxColumn)SampGrid.Columns[6]).MaxInputLength = 253;


                    SampGrid.Columns[7].Visible = false;
                    SampGrid.Columns[8].Visible = false;

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    //_dbMan.SqlStatement = " select *,  CONVERT(VARCHAR(24),calendardate,113) aa, CONVERT(VARCHAR(11),calendardate,106) dd  from [SAMPLING_Imported_Notes] s, workplace w where s.gmsiwpis = w.gmsiwpid ";
                    //_dbMan.SqlStatement = _dbMan.SqlStatement + "and w.description = '" + WPLabel2.Text + "' order by calendardate desc ";

                    _dbMan.SqlStatement = " select *,  CONVERT(VARCHAR(24),calendardate,113) aa, CONVERT(VARCHAR(11),calendardate,106) dd  from [SAMPLING_Imported_Notes] s, workplace w where s.gmsiwpis = w.gmsiwpid ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + "and w.description = '" + WPLabel2.Text + "' order by calendardate desc ";

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();

                    DataTable Samples = _dbMan.ResultsDataTable;

                    SampGrid.RowCount = Samples.Rows.Count;

                    int x = 0;
                    foreach (DataRow row in Samples.Rows)
                    {
                        SampGrid.Rows[x].Cells[0].Value = row["dd"].ToString();
                        SampGrid.Rows[x].Cells[1].Value = row["footwall"].ToString();
                        SampGrid.Rows[x].Cells[2].Value = row["hangwall"].ToString();
                        SampGrid.Rows[x].Cells[3].Value = row["corrcut"].ToString();
                        SampGrid.Rows[x].Cells[4].Value = row["swidth"].ToString();
                        SampGrid.Rows[x].Cells[5].Value = row["allocatedwidth"].ToString();
                        SampGrid.Rows[x].Cells[6].Value = row["notes"].ToString();
                        SampGrid.Rows[x].Cells[7].Value = row["aa"].ToString();

                        SampGrid.Rows[x].Cells[8].Value = row["gmsiwpis"].ToString();

                        SampGrid.Rows[x].Cells[5].Style.ForeColor = Color.Blue;
                        SampGrid.Rows[x].Cells[6].Style.ForeColor = Color.Blue;

                        SampGrid.Rows[x].Height = 20;
                        x = x + 1;
                    }


                }
                catch { }
            }

            else
            {


                //DateLbl.Text = System.DateTime.Now.ToString("dd MMM yyyy");

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbMan.SqlStatement = " select ltrim(employeename) empinitial ";
                //_dbMan.SqlStatement = " select ltrim(SUBSTRING(employeename, CHARINDEX(',', employeename)  + 1, 2))+  '. '+SUBSTRING(employeename, 0, CHARINDEX(',', employeename)) empinitial ";

                

                _dbMan.SqlStatement = _dbMan.SqlStatement + " from employeeall ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where occno in (SELECT [OccNo]   \r\n"+
                                      " FROM[EmployeeOccupation] where OccDescription like '%samp%') ";

                _dbMan.SqlStatement = _dbMan.SqlStatement + " order by employeename ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                DataTable Samplers = _dbMan.ResultsDataTable;

                SamplerListBox.Items.Add("");

                foreach (DataRow row in Samplers.Rows)
                {
                    SamplerListBox.Items.Add((row["empinitial"].ToString()));
                }

                ///
                MWDataManager.clsDataAccess _dbManGeo = new MWDataManager.clsDataAccess();
                _dbManGeo.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbManGeo.SqlStatement = " select ltrim(employeename) empinitial ";
                _dbManGeo.SqlStatement = _dbManGeo.SqlStatement + " from employeeall ";
                    _dbManGeo.SqlStatement = _dbManGeo.SqlStatement + " where occno in (SELECT [OccNo]  FROM[EmployeeOccupation] where OccDescription like '%Geol%')  ";
                _dbManGeo.SqlStatement = _dbManGeo.SqlStatement + "  ";
                _dbManGeo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGeo.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGeo.ExecuteInstruction();

                DataTable Geol = _dbManGeo.ResultsDataTable;


                GeoListBox.Items.Add("");

                foreach (DataRow row in Geol.Rows)
                {
                    GeoListBox.Items.Add((row["empinitial"].ToString()));
                }

                ////New

                MWDataManager.clsDataAccess _dbManSampProb = new MWDataManager.clsDataAccess();
                _dbManSampProb.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbManSampProb.SqlStatement = " select * from dbo.tbl_Code_Problems_Sample ";
                _dbManSampProb.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSampProb.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManSampProb.ResultsTableName = "Image";
                _dbManSampProb.ExecuteInstruction();

                DataTable SampProbs = _dbManSampProb.ResultsDataTable;


                ProbLB.Items.Add("");

                foreach (DataRow row in SampProbs.Rows)
                {
                    ProbLB.Items.Add(row["ProblemID"].ToString() + ":" + row["Description"].ToString());

                }

                ProbLB.SelectedIndex = 0;



                MWDataManager.clsDataAccess _dbManSamp2 = new MWDataManager.clsDataAccess();
                _dbManSamp2.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbManSamp2.SqlStatement = " select * from tb_SamplingGrid where workplace = '" + WpLbl.Text + "' and thedate =  '" + SSAMPdt.Value + "' ";
                _dbManSamp2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSamp2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManSamp2.ResultsTableName = "Image";
                _dbManSamp2.ExecuteInstruction();

                DataTable Sampler = _dbManSamp2.ResultsDataTable;

                if (Sampler.Rows.Count > 0)
                {
                    if (Sampler.Rows[0]["Samp_Name"].ToString() != "")
                    {
                        SamplerListBox.SelectedItem = Sampler.Rows[0]["Samp_Name"].ToString();
                    }
                    if (Sampler.Rows[0]["Geo_Name"].ToString() != "")
                    {
                        GeoListBox.SelectedItem = Sampler.Rows[0]["Geo_Name"].ToString();
                    }
                }

                ///////Load Problems

                MWDataManager.clsDataAccess _dbManSampProb2 = new MWDataManager.clsDataAccess();
                _dbManSampProb2.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbManSampProb2.SqlStatement = " select * from tbl_Sampling_Problem_Book where wpid = '" + WPLbl2.Text + "' and date =  '" + SSAMPdt.Value + "' ";
                _dbManSampProb2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSampProb2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManSampProb2.ResultsTableName = "Image";
                _dbManSampProb2.ExecuteInstruction();

                DataTable SampProbs2 = _dbManSampProb2.ResultsDataTable;

                if (SampProbs2.Rows.Count > 0)
                {
                    ProbLB.SelectedItem = SampProbs2.Rows[0]["ProblemID"].ToString() + ":" + SampProbs2.Rows[0]["Descrption"].ToString();
                }



            }
        }


        void ResetGrid()
        {
            SampGrid.ColumnCount = 10;

            SampGrid.Columns[0].Visible = true;
            SampGrid.Columns[1].Visible = true;
            SampGrid.Columns[2].Visible = true;
            SampGrid.Columns[3].Visible = true;
            SampGrid.Columns[4].Visible = true;
            SampGrid.Columns[5].Visible = true;
            SampGrid.Columns[6].Visible = true;
            SampGrid.Columns[7].Visible = true;
            SampGrid.Columns[8].Visible = true;
            SampGrid.Columns[9].Visible = true;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

            if (Samp2Pnl.Visible == false)
            {
                if (Samplabel.Text != "N")
                {
                    MWDataManager.clsDataAccess _dbManGeo = new MWDataManager.clsDataAccess();
                    _dbManGeo.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManGeo.SqlStatement = " delete from tb_SamplingGrid " +
                                          " where workplace = '" + WpLbl.Text + "' and thedate >= '" + String.Format("{0:yyyy-MM-dd}", Starttm.Value) + "' and thedate <=  '" + String.Format("{0:yyyy-MM-dd}", Endtm.Value) + "' " +
                                          "  and geo_name = '' " +
                                          "  ";
                    _dbManGeo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManGeo.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManGeo.ExecuteInstruction();



                    //MWDataManager.clsDataAccess _dbManGeo = new MWDataManager.clsDataAccess();
                    _dbManGeo.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManGeo.SqlStatement = " insert into tb_SamplingGrid " +
                                          " Values('" + String.Format("{0:yyyy-MM-dd}", SSAMPdt.Value) + "','" + WpLbl.Text + "','" + SamplerListBox.SelectedItem.ToString() + "','') " +
                                          "  " +
                                          "  ";
                    _dbManGeo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManGeo.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManGeo.ExecuteInstruction();

                    ////new

                    MWDataManager.clsDataAccess _dbManSamp = new MWDataManager.clsDataAccess();
                    _dbManSamp.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManSamp.SqlStatement = " delete from tbl_Sampling_Problem_Book " +
                                          " where wpid = '" + WPLbl2.Text + "' and date = '" + SSAMPdt.Value + "' \r\n" +
                                          "  Insert into  tbl_Sampling_Problem_Book values ( '" + WPLbl2.Text + "', '" + SSAMPdt.Value + "', " +
                                          " '" + ExtractBeforeColon(ProbLB.SelectedItem.ToString()) + "', '" + ExtractAfterColon(ProbLB.SelectedItem.ToString()) + "' ) ";
                    _dbManSamp.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManSamp.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManSamp.ExecuteInstruction();
                }


                if (Samplabel.Text == "N")
                {
                    MWDataManager.clsDataAccess _dbManGeo = new MWDataManager.clsDataAccess();
                    _dbManGeo.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManGeo.SqlStatement = " delete from tb_SamplingGrid " +
                                          " where workplace = '" + WpLbl.Text + "' and thedate = '" + String.Format("{0:yyyy-MM-dd}", SSAMPdt.Value) + "' " +
                                          "  and samp_name <> '' " +
                                          "  ";
                    _dbManGeo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManGeo.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManGeo.ExecuteInstruction();

                    ////new



                    MWDataManager.clsDataAccess _dbManSamp = new MWDataManager.clsDataAccess();
                    _dbManSamp.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManSamp.SqlStatement = " delete from tbl_Sampling_Problem_Book " +
                                          " where wpid = '" + WPLbl2.Text + "' and date = '" + SSAMPdt.Value + "' \r\n" +
                                          "  Insert into  tbl_Sampling_Problem_Book values ( '" + WPLbl2.Text + "', '" + SSAMPdt.Value + "', " +
                                          " '" + ExtractBeforeColon(ProbLB.SelectedItem.ToString()) + "', '" + ExtractAfterColon(ProbLB.SelectedItem.ToString()) + "' ) ";
                    _dbManSamp.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManSamp.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManSamp.ExecuteInstruction();

                }


                if (Geolabel.Text != "N")
                {
                    MWDataManager.clsDataAccess _dbManGeo = new MWDataManager.clsDataAccess();
                    _dbManGeo.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManGeo.SqlStatement = " delete from tb_SamplingGrid " +
                                          " where workplace = '" + WpLbl.Text + "' and thedate >= '" + String.Format("{0:yyyy-MM-dd}", Starttm.Value) + "' and thedate <=  '" + String.Format("{0:yyyy-MM-dd}", Endtm.Value) + "' " +
                                          "  and samp_name = '' " +
                                          "  ";
                    _dbManGeo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManGeo.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManGeo.ExecuteInstruction();



                    //MWDataManager.clsDataAccess _dbManGeo = new MWDataManager.clsDataAccess();
                    _dbManGeo.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManGeo.SqlStatement = " insert into tb_SamplingGrid " +
                                          " Values('" + String.Format("{0:yyyy-MM-dd}", SSAMPdt.Value) + "','" + WpLbl.Text + "','','" + GeoListBox.SelectedItem.ToString() + "') " +
                                          "  " +
                                          "  ";
                    _dbManGeo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManGeo.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManGeo.ExecuteInstruction();
                }

                if (Geolabel.Text == "N")
                {
                    MWDataManager.clsDataAccess _dbManGeo = new MWDataManager.clsDataAccess();
                    _dbManGeo.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                    _dbManGeo.SqlStatement = " delete from tb_SamplingGrid " +
                                          " where workplace = '" + WpLbl.Text + "' and thedate = '" + String.Format("{0:yyyy-MM-dd}", SSAMPdt.Value) + "' " +
                                          "  and geo_name <> '' " +
                                          "  ";
                    _dbManGeo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManGeo.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManGeo.ExecuteInstruction();

                }
            }
            else
            {
                MWDataManager.clsDataAccess _dbManGeo = new MWDataManager.clsDataAccess();
                _dbManGeo.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                for (int k = 0; k <= SampGrid.Rows.Count - 1; k++)
                {
                    //string test = SampGrid.Rows[k].Cells[7].Value.ToString();
                    string allocatedwidth = SampGrid.Rows[k].Cells[5].Value.ToString();
                    string Note = "";

                    if (string.IsNullOrEmpty(allocatedwidth))
                    {
                        allocatedwidth = "0";
                    }

                    if (!string.IsNullOrEmpty(SampGrid.Rows[k].Cells[6].Value.ToString()))
                    {
                        Note = SampGrid.Rows[k].Cells[6].Value.ToString();
                    }

                    _dbManGeo.SqlStatement = _dbManGeo.SqlStatement + " update  [SAMPLING_Imported_Notes] set notes = '" + SampGrid.Rows[k].Cells[6].Value.ToString() + "', allocatedwidth =  '" + SampGrid.Rows[k].Cells[5].Value.ToString() + "' where gmsiwpis = '" + SampGrid.Rows[k].Cells[8].Value.ToString() + "' and CONVERT(VARCHAR(24),calendardate,113) = '" + SampGrid.Rows[k].Cells[7].Value.ToString() + "' ";

                }

                _dbManGeo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGeo.queryReturnType = MWDataManager.ReturnType.DataTable;
                if (SampGrid.Rows.Count > 0)
                    _dbManGeo.ExecuteInstruction();
                
            }

            Global.sysNotification.TsysNotification.showNotification("Data Saved", "Sampling Data Updated ", Color.CornflowerBlue);
            Close();
        }

        private void SamplerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SamplerListBox.SelectedItem.ToString() != "")
                Samplabel.Text = SamplerListBox.SelectedItem.ToString();
            else
                Samplabel.Text = "N";
        }

        private void GeoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GeoListBox.SelectedItem.ToString() != "")
                Geolabel.Text = GeoListBox.SelectedItem.ToString();
            else
                Geolabel.Text = "N";
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void SampGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var dgw = (DataGridView)sender;
            dgw.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}