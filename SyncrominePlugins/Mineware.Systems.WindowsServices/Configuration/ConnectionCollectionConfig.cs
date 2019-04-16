using System;
using System.Configuration;

namespace Mineware.Systems.WindowsServices.Configuration
{
	[ConfigurationCollection(typeof(ConnectionConfig))]
	public class ConnectionCollectionConfig : ConfigurationElementCollection
	{
		internal const string PropertyName = "connection";

		public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;
		protected override string ElementName => PropertyName;

		protected override bool IsElementName(string elementName)
		{
			return elementName.Equals(PropertyName,
				StringComparison.InvariantCultureIgnoreCase);
		}

		public override bool IsReadOnly()
		{
			return false;
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionConfig();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ConnectionConfig)element).Operation;
		}

		public ConnectionConfig this[int idx] => (ConnectionConfig)BaseGet(idx);
	}
}