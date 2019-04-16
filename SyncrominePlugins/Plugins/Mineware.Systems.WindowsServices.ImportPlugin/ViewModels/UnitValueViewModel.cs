using System.Diagnostics;

namespace Mineware.Systems.WindowsServices.ImportPlugin.ViewModels
{
	[DebuggerDisplay("Version = {Version}, Unit = {UnitOfMeasure}, Value = {Value}")]
	public class UnitValueViewModel
	{
		public string Version { get; set; }

		public string UnitOfMeasure { get; set; }

		public string Value { get; set; }
	}
}