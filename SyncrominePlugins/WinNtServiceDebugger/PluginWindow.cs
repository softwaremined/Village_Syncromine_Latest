using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mineware.Systems.WindowsServices;

namespace WinNtServiceDebugger
{
	public partial class PluginWindow : Form
	{
		private Assembly _assembly;
		public PluginWindow()
		{
			InitializeComponent();
		}

		private void btnLoadAssembly_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog {Filter = @"DLLs (Mineware.Systems.WindowsServices.**.dll)|Mineware.Systems.WindowsServices.**.dll"};
			if (dialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			_assembly = Assembly.LoadFrom(dialog.FileName);
			label2.Text = _assembly.FullName;
			var types = _assembly.GetTypes();
			var ntServicePluginTypes = types.Where((x) => x != null && (x.BaseType != null && x.BaseType.Name == "BaseWinNtServicePlugin"));
			foreach (var type in ntServicePluginTypes)
			{
				if (type.AssemblyQualifiedName != null)
				{
					lstBxTypes.Items.Add(type.AssemblyQualifiedName);
				}
			}
			if (lstBxTypes.Items.Count > 0)
			{
				lstBxTypes.SelectedIndex = 0;
			}
		}

		private void btnRunAssembly_Click(object sender, EventArgs e)
		{
			if (lstBxTypes.SelectedIndex == -1)
			{
				MessageBox.Show(@"Need to select a plugin");
				return;
			}

			var pluginType = Type.GetType(lstBxTypes.SelectedItem.ToString());
			var name = _assembly.FullName;
			CreateAndRunWinNtService(pluginType, string.Empty, name);
		}

		private IWinNtServicePlugin srv = null;

		public void CreateAndRunWinNtService(Type pluginType, string parameters, string name)
		{
			if (pluginType == null)
			{
				throw new ArgumentNullException(nameof(pluginType));
			}

			
			var obj = Activator.CreateInstance(pluginType); //Remember that the DefaultBootstrapper has to be initialised (DefaultBootstrapper.Instance.RunIfRequired(); //Require Dns.Container)
			srv = obj as IWinNtServicePlugin;
			if (srv == null)
			{
				throw new Exception("Type  not implement IWinNtService :" + pluginType);
			}
			srv.Parameters = parameters;
			srv.Name = name;

			var task = new Task(srv.Run);
			task.Start();
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			srv?.Stop();
		}
	}
}
