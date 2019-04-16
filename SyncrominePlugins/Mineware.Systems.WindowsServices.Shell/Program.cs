using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Mineware.Systems.WindowsServices.Shell
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
#if (!DEBUG)
			var servicesToRun = new ServiceBase[] 
			{ 
				new MinewareWindowsServiceHost() 
			};

			ServiceBase.Run(servicesToRun);
#else
			var myServ = new MinewareWindowsServiceHost();
			ServiceBase.Run(myServ);
			// here Process is my Service function
			// that will run when my service onstart is call
			// you need to call your own method or function name here instead of Process();
#endif
		}
	}
}
