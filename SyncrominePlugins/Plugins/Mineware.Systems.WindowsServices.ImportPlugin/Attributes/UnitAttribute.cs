using System;

namespace Mineware.Systems.WindowsServices.ImportPlugin.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class UnitAttribute : Attribute
	{
		public UnitAttribute()
		{ }

		public UnitAttribute(string version, string unitOfMeasure)
		{
			Version = version;
			UnitOfMeasure = unitOfMeasure;
		}

		public UnitAttribute(string version, string stopeUnitOfMeasure, string devUnitOfMeasure)
		{
			Version = version;
			StopeUnitOfMeasure = stopeUnitOfMeasure;
			DevUnitOfMeasure = devUnitOfMeasure;
		}

		public string Version { get; set; }

		public string AlternateVersion { get; set; }

		public string UnitOfMeasure { get; set; }

		public string StopeUnitOfMeasure { get; set; }

		public string DevUnitOfMeasure { get; set; }
	}
}