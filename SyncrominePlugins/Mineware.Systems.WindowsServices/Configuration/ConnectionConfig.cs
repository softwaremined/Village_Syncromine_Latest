using System.Configuration;

namespace Mineware.Systems.WindowsServices.Configuration
{
	public class ConnectionConfig : ConfigurationElement
	{
		[ConfigurationProperty("operation", DefaultValue = "", IsRequired = true)]
		public string Operation => (string)this["operation"];

		[ConfigurationProperty("connectionString", DefaultValue = "", IsRequired = true)]
		public string Connection => (string)this["connectionString"];
	}
}