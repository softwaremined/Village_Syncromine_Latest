using System.Configuration;

namespace Mineware.Systems.WindowsServices.Configuration
{
	public class ConnectionSection : ConfigurationSection
	{
		[ConfigurationProperty("connections")]
		public ConnectionCollectionConfig Connections => (ConnectionCollectionConfig)base["connections"];
	}
}