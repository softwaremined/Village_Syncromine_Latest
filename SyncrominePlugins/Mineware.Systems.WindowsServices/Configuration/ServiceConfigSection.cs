using System.Configuration;

namespace Mineware.Systems.WindowsServices.Configuration
{
	public class ServiceConfigSection : ConfigurationSection
	{
		static ServiceConfigSection()
		{
			_services = new ConfigurationProperty("services", typeof(ServiceCollectionConfig), null, ConfigurationPropertyOptions.IsRequired);
			_properties = new ConfigurationPropertyCollection
			{
				_services
			};
		}

		private static readonly ConfigurationPropertyCollection _properties;
		private static ConfigurationProperty _services;

		protected override ConfigurationPropertyCollection Properties => _properties;

		[ConfigurationProperty("services")]
		public ServiceCollectionConfig Services
		{
			get
			{
				return (ServiceCollectionConfig)base["services"];
			}
		}

	}
}