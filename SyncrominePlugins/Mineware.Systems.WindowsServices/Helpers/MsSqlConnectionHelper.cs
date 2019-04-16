using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Mineware.Systems.WindowsServices.Attributes;

namespace Mineware.Systems.WindowsServices.Helpers
{
	public class MsSqlConnectionHelper
	{
		private readonly SqlConnection _connection;

		public MsSqlConnectionHelper()
		{
			_connection = new SqlConnection(ConnectionString);
		}

		public MsSqlConnectionHelper(string connectionString)
		{
			_connection = new SqlConnection(connectionString);
		}

		public string ConnectionString
		{
			get
			{
				var setting = ConfigurationManager.ConnectionStrings["default"];
				if (setting == null)
				{
					throw new InvalidOperationException("ConnectionString was not specified in Config");
				}

				return setting.ConnectionString;
			}
		}

		public IEnumerable<TResult> ExecuteTransactionalQuery<TResult>(string procedureName, SqlParameter[] parameters, string instruction)
		{
			_connection.Open();
			var resultList = new List<TResult>();
			var transaction = _connection.BeginTransaction();
			Exception error = null;
			try
			{
				var result = GetInstructionResult<TResult>(transaction, procedureName, parameters, instruction);
				if (result == null)
				{
					throw new InvalidDataException("Null Result is not expected");
				}
				
				resultList.AddRange(result);
			}
			catch (Exception ex)
			{
				// Attempt to roll back the transaction.
				try
				{
					transaction.Rollback();
				}
				catch (Exception ex2)
				{
					// This catch block will handle any errors that may have occurred
					// on the server that would cause the rollback to fail, such as
					// a closed connection.
					var eventLog = new EventLog();
					eventLog.WriteEntry($"Rollback Exception Type: {ex2.GetType()}");
					eventLog.WriteEntry($"  Message: {ex2.Message}");

				}
				error = ex;
				throw;
			}
			finally
			{
				if (error == null)
				{
					transaction.Commit();
				}
				if (_connection.State != ConnectionState.Closed)
				{
					_connection.Close();
				}
			}
			return resultList;
		}

		public void ExecuteTransactionalInstruction(string procedureName, SqlParameter[] parameters, string instruction)
		{
			Exception error = null;
			_connection.Open();
			var transaction = _connection.BeginTransaction();
			try
			{
				ExecuteInstruction(transaction, procedureName, parameters, instruction);
			}
			catch (TimeoutException ex)
			{
				var eventLog = new EventLog();
				eventLog.WriteEntry($"Rollback Exception Type: {ex.GetType()}");
				eventLog.WriteEntry($"  Message: {ex.Message}");
			}
			catch (Exception ex)
			{
				error = ex;
				// Attempt to roll back the transaction.
				try
				{
					transaction.Rollback();
				}
				catch (Exception ex2)
				{
					// This catch block will handle any errors that may have occurred
					// on the server that would cause the rollback to fail, such as
					// a closed connection.
					var eventLog = new EventLog();
					eventLog.WriteEntry($"Rollback Exception Type: {ex2.GetType()}");
					eventLog.WriteEntry($"  Message: {ex2.Message}");

				}
				throw;
			}
			finally
			{
				if (error == null)
				{
					transaction.Commit();
				}
				if (_connection.State != ConnectionState.Closed)
				{
					_connection.Close();
				}
			}
		}

		private IEnumerable<TResult> GetInstructionResult<TResult>(SqlTransaction transaction, string procedureName, SqlParameter[] parameters, string instruction)
		{
			IEnumerable<TResult> result;

			// If we have a procedure name - execute proc with xml as param - else,
			// our instruction is the query that we must execute - with no parameters....
			using (var command = new SqlCommand(procedureName ?? instruction, _connection, transaction))
			{
				command.CommandTimeout = _connection.ConnectionTimeout;
				if (procedureName != null)
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
				}
				else
				{
					command.CommandType = CommandType.Text;
				}
				
				using (var reader = command.ExecuteReader())
				{
					result = DataReaderMapToList<TResult>(reader);
				}
			}

			return result;
		}

		private void ExecuteInstruction(SqlTransaction transaction, string procedureName, SqlParameter[] parameters, string instruction)
		{
			// If we have a procedure name - execute proc with xml as param - else,
			// our instruction is the query that we must execute - with no parameters....
			using (var command = new SqlCommand(procedureName ?? instruction, _connection, transaction))
			{
				command.CommandTimeout = _connection.ConnectionTimeout;
				if (procedureName != null)
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
				}
				else
				{
					command.CommandType = CommandType.Text;
				}

				command.ExecuteNonQuery();
			}
		}

		public static List<T> DataReaderMapToList<T>(IDataReader dr)
		{
			var list = new List<T>();
			while (dr.Read())
			{
				var obj = Activator.CreateInstance<T>();
				foreach (var prop in obj.GetType().GetProperties())
				{
					var propertyName = prop.Name;
					var dataAttribute = prop.GetCustomAttributes(typeof(DataAttribute), false).FirstOrDefault();
					if (dataAttribute != null)
					{
						var attribute = (DataAttribute)dataAttribute;
						if (!string.IsNullOrEmpty(attribute.PropertyName))
						{
							propertyName = attribute.PropertyName;
						}
					}
					else
					{
						propertyName = prop.Name;
					}

					if (!object.Equals(dr[propertyName], DBNull.Value))
					{
						try
						{
							Type propertyType = prop.PropertyType;
							if (propertyType.IsGenericType)
							{
								propertyType =Nullable.GetUnderlyingType(propertyType);
							}
							var value = Convert.ChangeType(dr[propertyName], propertyType);
							prop.SetValue(obj, value, null);
						}
						catch (Exception ex)
						{

							throw;
						}
					}
				}
				list.Add(obj);
			}
			return list;
		}
	}
}