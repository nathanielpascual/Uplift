using System;
using System.Collections.Generic;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.DataAccess.Data.Repository.ISPRepository;
using Uplift.DataAccess.Data.Repository.USPRepository;

namespace Uplift.DataAccess.Data.Repository
{
	public class UnitOfWork:IUnitOfWork
	{
		private readonly ApplicationDbContext _db;

		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);
			Frequency = new FrequencyRepository(_db);
			Service = new ServiceRepository(_db);
			OrderHeader = new OrderHeaderRepository(_db);
			OrderDetails = new OrderDetailsRepository(_db);
			User = new UserRepository(_db);
			WebImage = new WebImageRepository(_db);
			SP_Call = new SP_Call(_db);
		}

		public ICategoryRepository Category { get; private set; }
		public IFrequencyRepository Frequency { get; private set; }
		public IServiceRepository Service { get; private set; }
		public IOrderHeaderRepository OrderHeader { get; private set; }
		public IOrderDetailsRepository OrderDetails { get; private set; }
		public IUserRepository User { get; private set; }
		public IWebImageRepository WebImage { get; private set; }
		#region StoredProcedure
		public ISP_Call SP_Call { get; private set; }

		#endregion
		public void Dispose()
		{
			_db.Dispose();
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
