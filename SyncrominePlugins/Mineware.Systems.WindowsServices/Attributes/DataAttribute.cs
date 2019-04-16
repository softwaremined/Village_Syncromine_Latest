using System;

namespace Mineware.Systems.WindowsServices.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class DataAttribute : Attribute
	{
		public string PropertyName { get; private set; }

		public DataAttribute(string propertyName)
		{
			PropertyName = propertyName;
		}

		public DataAttribute()
		{ }
	}
}