using System;
using System.Data;

namespace Mineware.Systems.WindowsServices.Extensions
{
	public static class DataRowExtensions
	{
		public static bool TryGetColumnFromRow(this DataRow dr, string columnName, out string value)
		{
			if (dr.Table.Columns.Contains(columnName))
			{
				var type = dr.Table.Columns[columnName].DataType;
				if (type.IsGenericType)
				{
					type = Nullable.GetUnderlyingType(type);
				}

				var columnValue = dr[columnName];
				value = (string)Convert.ChangeType(columnValue, typeof(string));

				//This is a hack to ensure a Numeric Value
				if (type.IsNumericType())
				{
					if (string.IsNullOrEmpty(value))
					{
						value = "0";
					}
				}
				return true;
			}

			value = null;
			return false;
		}
	}
}