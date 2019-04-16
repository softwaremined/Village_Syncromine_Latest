using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    public partial class ucSystemSettingsInterfaces : ucBaseUserControl
    {
        clsSystemSettings _clsSystemSettings = new clsSystemSettings();
        private DataTable dtSysset;
        public ucSystemSettingsInterfaces()
        {
            InitializeComponent();
        }

        private void ucSystemSettingsInterfaces_Load(object sender, EventArgs e)
        {
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSysset = _clsSystemSettings.getSysset();
            try
            {
                if (dtSysset.Rows.Count > 0)
                {
                    txtDirCalendars.Text = dtSysset.Rows[0]["Calendarfile"].ToString();
                    txtDirSections.Text = dtSysset.Rows[0]["Sectionfile"].ToString();
                    txtDirPlanning.Text = dtSysset.Rows[0]["Plandir"].ToString();
                    txtDirBookings.Text = dtSysset.Rows[0]["Bookdir"].ToString();
                    txtDirCosting.Text = dtSysset.Rows[0]["Costfile"].ToString();
                    txtDirCostingHeader.Text = dtSysset.Rows[0]["Headerfile"].ToString();
                    txtDirNetwordPathToCosting.Text = dtSysset.Rows[0]["Cpmpath"].ToString();
                }
            }
            catch (Exception _exception)
            {
                MessageBox.Show(_exception.ToString());
            }

        }

        private void btnSaveInterfaces_Click(object sender, EventArgs e)
        {
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            bool theSave = _clsSystemSettings.updateInterfaceSettings(
                        txtDirStandards.Text, txtDirCalendars.Text, txtDirSections.Text, 
                        txtDirPlanning.Text, txtDirBookings.Text, txtDirCosting.Text, 
                        txtDirCostingHeader.Text, txtDirNetwordPathToCosting.Text);
            if (theSave == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "System Settings Interface Data Updated ", Color.CornflowerBlue);
            }

        }

        private void btnDirStandards_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Text files | *.txt";
            f.InitialDirectory = Application.ExecutablePath;

            if (f.ShowDialog() == DialogResult.OK)
                if (f.FileName != null && f.CheckFileExists == true)
                {
                    txtDirStandards.Text = f.FileName;
                }
        }

        private void btnDirCalendars_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Text files | *.txt";
            f.InitialDirectory = Application.ExecutablePath;

            if (f.ShowDialog() == DialogResult.OK)
                if (f.FileName != null && f.CheckFileExists == true)
                {
                    txtDirCalendars.Text = f.FileName;
                }
        }

        private void btnDirSections_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Text files | *.txt";
            f.InitialDirectory = Application.ExecutablePath;

            if (f.ShowDialog() == DialogResult.OK)
                if (f.FileName != null && f.CheckFileExists == true)
                {
                    txtDirSections.Text = f.FileName;
                }
        }

        private void btnDirPlanning_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "XML files | *.xml";
            f.InitialDirectory = Application.ExecutablePath;

            if (f.ShowDialog() == DialogResult.OK)
                if (f.FileName != null && f.CheckFileExists == true)
                {
                    txtDirPlanning.Text = f.FileName;
                }
        }

        private void btnDirBookings_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Text files | *.txt";
            f.InitialDirectory = Application.ExecutablePath;

            if (f.ShowDialog() == DialogResult.OK)
                if (f.FileName != null && f.CheckFileExists == true)
                {
                    txtDirBookings.Text = f.FileName;
                }
        }

        private void btnDirNetwordPathToCosting_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Text files | *.txt";
            f.InitialDirectory = Application.ExecutablePath;

            if (f.ShowDialog() == DialogResult.OK)
                if (f.FileName != null && f.CheckFileExists == true)
                {
                    txtDirNetwordPathToCosting.Text = f.FileName;
                }
        }

        private void btnDirCosting_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            //f.RootFolder = Application.ExecutablePath;

            if (f.ShowDialog() == DialogResult.OK)
                if (f.SelectedPath != null)
                {
                    txtDirCosting.Text = f.SelectedPath;
                }
        }

        private void btnDirCostingHeader_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            //f.RootFolder = Application.ExecutablePath;

            if (f.ShowDialog() == DialogResult.OK)
                if (f.SelectedPath != null)
                {
                    txtDirCostingHeader.Text = f.SelectedPath;
                }
        }
    }
}
