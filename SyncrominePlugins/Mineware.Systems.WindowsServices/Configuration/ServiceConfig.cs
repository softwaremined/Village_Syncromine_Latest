using System.Configuration;

namespace Mineware.Systems.WindowsServices.Configuration
{
	public class ServiceConfig : ConfigurationElement
	{
		/// <summary>
		/// Predefines the valid properties and prepares
		/// the property collection.
		/// </summary>
		static ServiceConfig()
		{
			// Predefine properties here
			_name = new ConfigurationProperty(
				"name",
				typeof(string),
				null,
				ConfigurationPropertyOptions.IsKey
			);
			_serviceType = new ConfigurationProperty(
				"serviceType",
				typeof(string),
				null,
				ConfigurationPropertyOptions.IsRequired
			);

			_params = new ConfigurationProperty(
				"params",
				typeof(string),
				null,
				ConfigurationPropertyOptions.None
			);

			properties = new ConfigurationPropertyCollection
			{
				_name,
				_serviceType,
				_params
			};

		}


		private static readonly ConfigurationProperty _name;
		private static readonly ConfigurationProperty _serviceType;
		private static readonly ConfigurationProperty _params;

		private static ConfigurationPropertyCollection properties;

		[ConfigurationProperty("name", IsRequired = true)]
		public string Name => (string)base[_name];

		[ConfigurationProperty("serviceType", IsRequired = true)]
		public string ServiceType => (string)base[_serviceType];

		[ConfigurationProperty("params", IsRequired = false)]
		public string Params => (string)base[_params];


		protected override ConfigurationPropertyCollection Properties => properties;
	}
}