using System;
using System.Collections.Generic;
using System.Text;
using Uplift.DataAccess.Data.Repository.ISPRepository;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
	public interface IUnitOfWork:IDisposable
	{
		ICategoryRepository Category { get; }
		IFrequencyRepository Frequency { get; }

		IServiceRepository Service { get; }

		IOrderHeaderRepository OrderHeader { get; }

		IOrderDetailsRepository OrderDetails { get; }

		IUserRepository User { get; }

		#region StoredProcedure
		ISP_Call SP_Call { get; }
		#endregion
		void Save();
	}
}
