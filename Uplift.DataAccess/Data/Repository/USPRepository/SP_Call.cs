using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Uplift.DataAccess.Data.Repository.ISPRepository;

namespace Uplift.DataAccess.Data.Repository.USPRepository
{
	public class SP_Call : ISP_Call
	{
		private readonly ApplicationDbContext _db;
		private static string ConnectionString = "";
		public SP_Call(ApplicationDbContext db)
		{
			_db = db;
			ConnectionString = _db.Database.GetDbConnection().ConnectionString;	
		}

		public void ExecuteQuery(string procedureName, DynamicParameters param = null)
		{
			using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
			{
				sqlCon.Open();
			    sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

			}
		}

		public T ExecuteScalar<T>(string procedureName, DynamicParameters param = null)
		{
			using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
			{
				sqlCon.Open();
				return (T)Convert.ChangeType(sqlCon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure),typeof(T));

			}
		}

		public IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
		{
			using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
			{
				sqlCon.Open();
				return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

			}
		}

		public void Dispose()
		{
			_db.Dispose();
		}
	}
}
