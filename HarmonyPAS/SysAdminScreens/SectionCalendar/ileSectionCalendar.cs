using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.ProductionGlobal;
using System;
using System.Data;
using System.Drawing;

namespace Mineware.Systems.Production.SysAdminScreens.SectionCalendar
{


    public partial class ileSectionCalendar : EditFormUserControl
    {
        clsDataResult dataresult;
        private string CurrentProdMonth;
    public decimal ProdMnth = 0;
    public string SectionID;
    public string SectionName;
    public string Caltype;
    public string TotalShifts;
    public DateTime SDate;
    public DateTime EDate;
    public string FormType;
    public string EditID;
    public string Valid;
    public string prodmonth;
    public string Edit = string.Empty;
    public string ErrorMsg;
    public bool ErrorFound;
    public TUserCurrentInfo UserCurrentInfo;
    public string theSystemDBTag;
    public string xValue;
    public string MAXDATE;
    bool caltype;
    string combotype = string.Empty;
    public bool result = false;

    public ileSectionCalendar()
    {
        InitializeComponent();
    }

    public void stopdata(TUserCurrentInfo userinfo, string section, string PRODMONTH)
    {
        UserCurrentInfo = userinfo;
        SectionID = section;
        prodmonth = PRODMONTH;
    }

    public string ExtractBeforeColon(string TheString)
    {
        if(TheString != string.Empty)
        {
            string BeforeColon;

            int index = TheString.IndexOf(":");

            BeforeColon = TheString.Substring(0, index);

            return BeforeColon;
        } else
        {
            return string.Empty;
        }
    }

    private void lstApply_DoubleClick(object sender, EventArgs e)
    {
        try
        {
            if(lstApply.Items.Count != 0)
            {
                if(lstApply.SelectedIndex != -1)
                {
                    CalcTotalShifts();
                    dteTestDate.DateTime = dteTestDate.DateTime.AddDays(-1);
                    lstAvailable.Items.Add(lstApply.SelectedItem);
                    lstApply.Items.RemoveAt(lstApply.SelectedIndex);
                    buildSecArray();
                }
            }
        } catch(Exception _exception)
        {
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error",
                                                                                      _exception.Message,
                                                                                      Color.Red);
        }
    }

    private void lstAvailable_DoubleClick(object sender, EventArgs e)
    {
        try
        {
            if(lstAvailable.Items.Count != 0)
            {
                if(lstAvailable.SelectedIndex != -1)
                {
                    CalcTotalShifts();
                    dteTestDate.DateTime = dteTestDate.DateTime.AddDays(-1);
                    lstApply.Items.Add(lstAvailable.SelectedItem);
                    lstAvailable.Items.RemoveAt(lstAvailable.SelectedIndex);
                    buildSecArray();
                }
            }
        } catch(Exception _exception)
        {
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error",
                                                                                      _exception.Message,
                                                                                      Color.Red);
        }
    }

    private void btnAvailtoApply_Click(object sender, EventArgs e)
    {
        try
        {
            if(lstAvailable.Items.Count != 0)
            {
                if(lstAvailable.SelectedIndex != -1)
                {
                    CalcTotalShifts();
                    dteTestDate.DateTime = dteTestDate.DateTime.AddDays(-1);
                    lstApply.Items.Add(lstAvailable.SelectedItem);
                    lstAvailable.Items.RemoveAt(lstAvailable.SelectedIndex);
                    buildSecArray();
                }
            }
        } catch(Exception _exception)
        {
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error",_exception.Message,Color.Red);
        }
    }

    private void btnApplytoAvail_Click(object sender, EventArgs e)
    {
        try
        {
            if(lstApply.Items.Count != 0)
            {
                if(lstApply.SelectedIndex != -1)
                {
                    CalcTotalShifts();
                    dteTestDate.DateTime = dteTestDate.DateTime.AddDays(-1);
                    lstAvailable.Items.Add(lstApply.SelectedItem);
                    lstApply.Items.RemoveAt(lstApply.SelectedIndex);
                    buildSecArray();
                }
            }
        } catch(Exception _exception)
        {
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error",
                                                                                      _exception.Message,
                                                                                      Color.Red);
        }
    }

    private void buildSecArray()
    {
        try
        {
            var TheData1 = new MWDataManager.clsDataAccess();
            TheData1.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag,
                                                                         UserCurrentInfo.Connection);
            TheData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TheData1.SqlStatement = " delete from sect where userid = '" + TUserInfo.UserID + "' ";

            for(int x = 0; x <= lstApply.Items.Count - 1; x++)
            {
                xValue = ExtractBeforeColon(lstApply.Items[x].ToString());
                TheData1.SqlStatement = TheData1.SqlStatement +
                    " insert into sect (userid, sectionid, calendarCode, begindate, enddate, totalshifts) values " +
                    " ('" +
                    TUserInfo.UserID +
                    "', '" +
                    xValue +
                    "', \r\n " +
                    " '" +
                    cmbCalTypes.Text +
                    "', \r\n " +
                    " '" +
                    String.Format("{0:yyyy-MM-dd}", dteBeginDate.EditValue) +
                    "', \r\n " +
                    " '" +
                    String.Format("{0:yyyy-MM-dd}", dteEndDate.EditValue) +
                    "', \r\n " +
                    " '" +
                    lblTotalShifts.Text +
                    "') ";
            }
            TheData1.queryReturnType = MWDataManager.ReturnType.longNumber;
            dataresult = TheData1.ExecuteInstruction();

            if (!dataresult.success)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", dataresult.Message, Color.Red);
            }
                var dt = new DataTable();
        } catch(Exception _exception)
        {
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error",
                                                                                      _exception.Message,
                                                                                      Color.Red);
        }
    }

    private void LoadTypeCalendars()
    {
        try
        {
            MWDataManager.clsDataAccess _dbManCalType = new MWDataManager.clsDataAccess();
            _dbManCalType.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag,
                                                                              UserCurrentInfo.Connection);
            _dbManCalType.SqlStatement = "select * from CODE_CALENDAR ";
            if(FormType == "SecCalendar")
            {
                _dbManCalType.SqlStatement = _dbManCalType.SqlStatement +
                    " where Description = 'Mine Calen'  ";
            }
            if(FormType == "MillCalendar")
            {
                _dbManCalType.SqlStatement = _dbManCalType.SqlStatement +
                    " where Description = 'Mill Calendar'  ";
            }
            _dbManCalType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManCalType.queryReturnType = MWDataManager.ReturnType.DataTable;
                dataresult = _dbManCalType.ExecuteInstruction();

                if (!dataresult.success)
                {
                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", dataresult.Message, Color.Red);
                }

                DataTable CalType = _dbManCalType.ResultsDataTable;

            cmbCalTypes.Properties.DataSource = CalType;
            cmbCalTypes.EditValue = "C3 ME NF";
        } catch(Exception _exception)
        {
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error",
                                                                                      _exception.Message,
                                                                                      Color.Red);
        }
    }

    public void LoadTheSections()
    {
        try
        {
            lstApply.Items.Clear();
            lstAvailable.Items.Clear();
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            if(((txtBDate.Text != string.Empty) &
                    (txtEDate.Text != string.Empty)) ||
                ((txtBDate.Text != string.Empty) &
                    (txtEDate.Text == string.Empty))
                )
            {
                DateTime aa = Convert.ToDateTime(dteBeginDate.EditValue).AddDays(-1);
                _dbMan.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag,
                                                                           UserCurrentInfo.Connection);
                _dbMan.SqlStatement =
                    " --select distinct(SectionID) SectionID, Name  \r\n " +
                    " --from ( \r\n " +
                    " select s.SectionID SectionID, s.Name Name \r\n " +
                    " from SECTION s \r\n " +
                    " left outer join seccal sc on \r\n " +
                    " sc.Prodmonth = s.Prodmonth and \r\n " +
                    " sc.Sectionid = s.SectionID \r\n " +
                    " where s.Prodmonth = '" +
                    prodmonth +
                    "' and s.Hierarchicalid = " + Convert.ToString(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID + 1) + " \r\n " +
                    ////" and sc.startdate = '" +
                    ////txtBDate.Text +
                    ////"' \r\n " +
                    //" union all \r\n " +
                    //" select s.SectionID SectionID, s.Name Name \r\n " +
                    //" from SECTION s \r\n " +
                    //" left outer join seccal sc on \r\n " +
                    //" sc.Prodmonth = s.Prodmonth and \r\n " +
                    //" sc.Sectionid = s.SectionID \r\n " +
                    //" where s.Prodmonth = '" +
                    //prodmonth +
                    //"' and s.Hierarchicalid = " + Convert.ToString(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID + 1) + " \r\n " +
                    ////" and sc.EndDate = '" +
                    ////String.Format("{0:yyyy-MM-dd}", aa) +
                    ////"' " +
                    " --) a";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                dataresult = _dbMan.ExecuteInstruction();

                if (!dataresult.success)
                {
                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", dataresult.Message, Color.Red);
                }
                DataTable dt = new DataTable();
                dt = _dbMan.ResultsDataTable;
                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        if(txtSectionID.Text == dr["SectionID"].ToString())
                            lstApply.Items.Add(dr["SectionID"].ToString() + ':' + dr["name"].ToString()); else
                            lstAvailable.Items.Add(dr["SectionID"].ToString() + ':' + dr["name"].ToString());
                    }
                }
            } else
            {
                if((txtBDate.Text == string.Empty) &
                    (txtEDate.Text == string.Empty))
                {
                    _dbMan.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag,
                                                                               UserCurrentInfo.Connection);
                    _dbMan.SqlStatement =
                        " select s.SectionID SectionID, s.Name Name \r\n " +
                        " from SECTION s \r\n " +
                        " left outer join seccal sc on \r\n " +
                        " sc.Prodmonth = s.Prodmonth and \r\n " +
                        " sc.Sectionid = s.SectionID \r\n " +
                        " where s.Prodmonth = '" +
                        txtProdMonth.Text +
                        "' and s.Hierarchicalid = "+Convert.ToString(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).MOHierarchicalID+1)+" \r\n " +
                        " and sc.begindate is null and sc.EndDate is null \r\n ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();
                    DataTable dtsect = new DataTable();
                    dtsect = _dbMan.ResultsDataTable;
                    if(dtsect.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dtsect.Rows)
                        {
                            if(txtSectionID.Text == dr["SectionID"].ToString())
                                lstApply.Items.Add(dr["SectionID"].ToString() + ':' + dr["name"].ToString()); else
                                lstAvailable.Items.Add(dr["SectionID"].ToString() + ':' + dr["name"].ToString());
                        }
                    }
                }
            }
            lstApply.Enabled = true;
            lstAvailable.Enabled = true;
            btnApplytoAvail.Enabled = true;
            btnAvailtoApply.Enabled = true;
        } catch(Exception _exception)
        {
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error",
                                                                                      _exception.Message,
                                                                                      Color.Red);
        }
    }

    private void CalcTotalShifts()
    {
        try
        {
            if(dteBeginDate.EditValue != null && dteBeginDate.EditValue.ToString() != string.Empty &&
                dteEndDate.EditValue != null &&
                dteEndDate.EditValue.ToString() != string.Empty)
            {
                if(Convert.ToDateTime(dteBeginDate.EditValue) <= Convert.ToDateTime(dteEndDate.EditValue))
                {
                    MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
                    _dbManDate.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag,
                                                                                   UserCurrentInfo.Connection);
                    _dbManDate.SqlStatement = "select COUNT(Workingday) from CALTYPE " +
                        " where CalendarCode = '" +
                        cmbCalTypes.Text +
                        "' and " +
                        " Workingday = 'Y' and " +
                        " CalendarDate >= '" +
                        String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dteBeginDate.EditValue)) +
                        "' and " +
                        " CalendarDate <= '" +
                        String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dteEndDate.EditValue)) +
                        "'  ";
                    _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
                    dataresult = _dbManDate.ExecuteInstruction();

                    if (!dataresult.success)
                    {
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", dataresult.Message, Color.Red);
                    }

                        lblTotalShifts.Text = _dbManDate.ResultsDataTable.Rows[0][0].ToString();
                    TotalShifts = lblTotalShifts.Text;
                }

                else
                {
                    //Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error",
                    //                                                                          "Start Date cannot be greater than the End Date",
                    //                                                                          Color.Red);
                    //lblTotalShifts.Text = "0";
                }
            }
        } catch(Exception _exception)
        {
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error",
                                                                                      _exception.Message,
                                                                                      Color.Red);
        }
    }

    private void ileSectionCalendars_Load(object sender, EventArgs e)
    {
        LoadScreen();
    }

    private void LoadScreen()
    {
        lstApply.Items.Clear();
        lstAvailable.Items.Clear();
        txtProdMonth.Text = prodmonth;

        dteEndDate.Enabled = true;
        dteBeginDate.Enabled = true;
        cmbCalTypes.Enabled = true;

        if(txtBDate.Text != string.Empty)
        {
            dteBeginDate.Enabled = false;
        }

        lstAvailable.Items.Add(SectionID+":");

        LoadTypeCalendars();
        LoadTheSections();
    }

    private void dteBeginDate_EditValueChanged(object sender, EventArgs e)
    {
        CalcTotalShifts();
        buildSecArray();
    }

    private void dteEndDate_EditValueChanged(object sender, EventArgs e)
    {
        CalcTotalShifts();
        buildSecArray();
    }

    private void cmbCalTypes_EditValueChanged_1(object sender, EventArgs e)
    {
        CalcTotalShifts();
        buildSecArray();
    }

    private void ileSectionCalendars_Enter(object sender, EventArgs e)
    {
        LoadScreen();
    }

        private void lstAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
