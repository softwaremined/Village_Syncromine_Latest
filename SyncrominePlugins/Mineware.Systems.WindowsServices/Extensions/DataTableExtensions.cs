using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mineware.Systems.WindowsServices.Extensions
{
	public static class DataTableExtensions
	{
		public static IEnumerable<T> GetDataFromDataTable<T>(this DataTable table)
		{
			if (table.Rows.Count == 0)
			{
				return Enumerable.Empty<T>();
			}

			var resultList = new List<T>();
			var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (DataRow row in table.Rows)
			{
				var model = Activator.CreateInstance<T>();
				//var properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
				resultList.Add(model);
				foreach (var property in properties)
				{
					string outvalue;
					if (row.TryGetColumnFromRow(property.Name, out outvalue))
					{
						var type = property.PropertyType;
						if (type.IsGenericType)
						{
							type = Nullable.GetUnderlyingType(type);
						}

						if (string.IsNullOrEmpty(outvalue))
						{
							property.SetValue(model, default(Type), null);
						}
						else if (type.IsEnum)
						{
							property.SetValue(model, Enum.ToObject(type, Convert.ToInt32(outvalue)), null);
						}
						else
						{
							property.SetValue(model, Convert.ChangeType(outvalue, type), null);
						}
					}
				}
			}

			return resultList;
		}

		public static string ToCsv(this DataTable table)
		{
			var sb = new StringBuilder();

			var columnNames = table.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
			//sb.AppendLine(string.Join(",", columnNames));

			foreach (DataRow row in table.Rows)
			{
				IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
				sb.AppendLine(string.Join(",", fields));
			}

			return sb.ToString();
		}
	}
}