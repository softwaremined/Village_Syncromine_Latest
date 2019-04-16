using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using Mineware.Systems.WindowsServices.Configuration;

namespace Mineware.Systems.WindowsServices.Shell
{
	public partial class MinewareWindowsServiceHost : ServiceBase
	{
		private List<IWinNtServicePlugin> _services;

		public MinewareWindowsServiceHost()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			try
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Start Service Host");
				EventLog.WriteEntry("Starting Service");

				RunBaseService();
			}
			catch (Exception ex)
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Failed to import monthly successfully. {ex.Message}, Inner: {ex.InnerException?.Message}");
				EventLog.WriteEntry("Could not start service : " + ex);
			}
		}

		protected override void OnStop()
		{
			if (_services == null)
			{
				return;
			}
			LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Attempting to stop services");
			foreach (var service in _services)
			{
				service.Stop();
				var cnt = 0;
				while (service.Status != Status.Stopped && cnt < 5)
				{
					System.Threading.Thread.Sleep(300);
					cnt++;
				}
				if (service.Status != Status.Stopped)
				{
					EventLog.WriteEntry("Service did not stop");
				}

			}
			_services = null;
			LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Services Stopped");
			EventLog.WriteEntry("Service Stopped");
		}

		private void RunBaseService()
		{
			try
			{
				var pc = Process.GetCurrentProcess();
				var dir = pc.MainModule.FileName.Substring(0, pc.MainModule.FileName.LastIndexOf(@"\", StringComparison.Ordinal));
				Directory.SetCurrentDirectory(dir);
				EventLog.WriteEntry("System dir :" + dir);

				var config = ConfigurationManager.GetSection("services") as ServiceConfigSection;
				if (config == null)
				{
					LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : No Config FOund");
					throw new Exception("No Config found");
				}
				_services = LoadAndRunServices(config);
				if (_services.Count == 0)
				{
					LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : No Services Found");
					throw new Exception("No services to run");
				}
				else
				{
					LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Found {_services.Count} Services");
				}
			}
			catch (Exception ex)
			{
				_services = null;
				EventLog.WriteEntry("Error in service : " + ex.Message + ":" + ex.Source + "--:--" + ex.StackTrace, EventLogEntryType.Error);
				EventLog.WriteEntry("Serice will be stopped");
				Stop();
			}
		}


		private List<IWinNtServicePlugin> LoadAndRunServices(ServiceConfigSection config)
		{
			EventLog.WriteEntry("Loading WinNtServicePlugin with config", EventLogEntryType.Information);
			var resultList = new List<IWinNtServicePlugin>();

			foreach (ServiceConfig service in config.Services)
			{
				try
				{
					var type = Type.GetType(service.ServiceType);

					var srv = CreateWinNtService(type, service.Params, service.Name);

					var trd = new System.Threading.Thread(() => srv.Run());
					trd.Start();
					resultList.Add(srv);
				}
				catch (Exception ex)
				{
					LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Could not load service {service.ServiceType} and will be ignored. Error :{ex}");
					EventLog.WriteEntry($"Could not load service {service.ServiceType} and will be ignored. Error :{ex}", EventLogEntryType.Error);
				}
			}
			return resultList;
		}

		public IWinNtServicePlugin CreateWinNtService(Type pluginType, string parameters, string name)
		{
			if (pluginType == null)
			{
				throw new ArgumentNullException(nameof(pluginType));
			}

			var obj = Activator.CreateInstance(pluginType);
			var srv = obj as IWinNtServicePlugin;
			if (srv == null)
			{
				throw new Exception("Type  not implement IWinNtService :" + pluginType);
			}
			srv.Parameters = parameters;
			srv.Name = name;

			return srv;
		}

		private static void LogMessage(string message)
		{
			try
			{

				var baseDirectory = Thread.GetDomain().BaseDirectory;
				var directory = Path.Combine(baseDirectory, "ServiceLog");
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				using (var sw = File.AppendText(Path.Combine(directory, "LogFile.txt")))
				{
					sw.WriteLine(message);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}
