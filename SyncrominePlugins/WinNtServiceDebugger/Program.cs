using System;
using System.Windows.Forms;
using Mineware.Systems.WindowsServices;

namespace WinNtServiceDebugger
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new PluginWindow());
		}
	}
}
