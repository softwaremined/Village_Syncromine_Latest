using System;

namespace Mineware.Systems.WindowsServices.WorkplaceStripperPlugin
{
	public class SqlConnectionException : Exception
	{
		public SqlConnectionException()
		{ }

		public SqlConnectionException(string message) : base(message)
		{ }

		public SqlConnectionException(string message, Exception innerException) : base(message, innerException)
		{ }
	}
}