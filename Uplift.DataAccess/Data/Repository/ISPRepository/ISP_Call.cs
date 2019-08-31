using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.DataAccess.Data.Repository.ISPRepository
{
	public interface ISP_Call:IDisposable
	{
		IEnumerable<T> ReturnList<T>(string procedureName,DynamicParameters param=null);

		T ExecuteScalar<T>(string procedureName, DynamicParameters param = null);

		void ExecuteQuery(string procedureName, DynamicParameters param = null);

	}
}
