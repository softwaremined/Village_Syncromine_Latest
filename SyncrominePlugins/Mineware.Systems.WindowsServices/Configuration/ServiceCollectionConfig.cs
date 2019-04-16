using System;
using System.Configuration;

namespace Mineware.Systems.WindowsServices.Configuration
{
	[ConfigurationCollection(typeof(ConfigurationElement),
	CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class ServiceCollectionConfig : ConfigurationElementCollection
	{

		static ServiceCollectionConfig()
		{
			_properties = new ConfigurationPropertyCollection();

		}

		private static readonly ConfigurationPropertyCollection _properties;


		#region Properties
		protected override ConfigurationPropertyCollection Properties => _properties;

		public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.AddRemoveClearMap;

		#endregion


		public ServiceConfig this[int index]
		{
			get
			{
				return (ServiceConfig)base.BaseGet(index);
			}
			set
			{
				BaseAdd(index, value);
			}
		}

		public ServiceConfig this[string name] => (ServiceConfig)base.BaseGet(name);

		#region Overrides
		protected override ConfigurationElement CreateNewElement()
		{
			return new ServiceConfig();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			var serviceConfig = element as ServiceConfig;
			if (serviceConfig != null)
			{
				return serviceConfig.Name;
			}

			throw new InvalidOperationException("Config Invalid");
		}
		#endregion
	}
}