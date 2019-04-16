using System;
using System.Threading;

namespace Mineware.Systems.WindowsServices
{
	public abstract class BaseWinNtServicePlugin : IWinNtServicePlugin
	{
		Status _status = Status.Stopped;

		/// <summary>
		/// Entry Method to execute a Plugin
		/// </summary>
		protected abstract void RunInstance();

		private int _initialised;
		private void InitialisePluginIfRequried()
		{
			if (Interlocked.Exchange(ref _initialised, 1) != 0)
			{
				return;
			}

			OnInitializePlugin();
		}

		protected virtual void OnInitializePlugin()
		{

		}

		public void Run()
		{
			try
			{
				InitialisePluginIfRequried();

				Status = Status.Running;

				OnBeforeRun();
				while (!(Status == Status.Stopped || Status == Status.Stopping))
				{
					RunInstance();
					while (Status == Status.Paused)
					{
						Thread.Sleep(30000);
					}
					Thread.Sleep(TimeSpan.FromSeconds(30));
				}
				OnAfterRun();
			}
			catch (Exception ex)
			{
				ErrorStatus = ErrorStatus.Error;
				ErrorInfo = ex.ToString();
			}
			Status = Status.Stopped;

		}

		protected virtual void OnAfterRun()
		{
		}

		protected virtual void OnBeforeRun()
		{
		}

		public string Parameters { get; set; }

		public Status Status
		{
			get
			{
				lock (this)
				{
					return _status;
				}
			}
			protected
			set
			{
				lock (this)
				{
					_status = value;
				}
			}
		}


		public ErrorStatus ErrorStatus { get; protected set; }

		public void Pause()
		{
			lock (this)
			{
				if (Status == Status.Running)
				{
					Status = Status.Paused;
				}
			}
		}

		public void Stop()
		{
			lock (this)
			{
				if (Status != Status.Stopped)
				{
					Status = Status.Stopping;
				}
			}
		}

		public string ErrorInfo { get; protected set; }

		public void Resume()
		{
			lock (this)
			{
				if (Status == Status.Paused)
				{
					Status = Status.Running;
				}
			}
		}

		public string Name { get; set; }
	}
}
