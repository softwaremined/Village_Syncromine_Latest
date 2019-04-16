using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mineware.Systems.WindowsServices.Extensions
{
	public static class SqlExtensions
	{
		public static void AddSqlParameter<T>(this SqlCommand command, string key, T value)
		{
			var parameter = new SqlParameter
			{
				ParameterName = key,
				Value = value
			};

			command.Parameters.Add(parameter);
		}

		public static DataTable ExecuteQuery(this SqlConnection connection, string sql)
		{
			using (var command = new SqlCommand(sql, connection))
			{
				using (var reader = command.ExecuteReader())
				{
					var table = new DataTable();
					table.Load(reader);
					return table;
				}
			}
		}

		public static DataTable ExecuteQueryWithParameters(this SqlConnection connection, string sql, Dictionary<string, object> values)
		{
			using (var command = new SqlCommand(sql, connection))
			{
				foreach (var keyValuePair in values)
				{
					command.AddSqlParameter(keyValuePair.Key, keyValuePair.Value);
				}
				using (var reader = command.ExecuteReader())
				{
					var table = new DataTable();
					table.Load(reader);
					return table;
				}
			}
		}
	}
}