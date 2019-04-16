namespace Mineware.Systems.WindowsServices
{
	public interface IWinNtServicePlugin
	{
		void Run();
		string Name { get; set; }
		string Parameters { get; set; }
		Status Status { get; }
		ErrorStatus ErrorStatus { get; }
		string ErrorInfo { get; }
		void Pause();
		void Resume();
		void Stop();

	}

	public enum Status { None, Running, Stopping, Stopped, Paused }
	public enum ErrorStatus { None, Warning, Error }
}