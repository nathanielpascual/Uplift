using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
	public class ServiceRepository : Repository<Service>, IServiceRepository
	{
		private readonly ApplicationDbContext _db;

		public ServiceRepository(ApplicationDbContext db):base(db)
		{
			_db = db;
		}
		

		public void Update(Service service)
		{
			var obj = _db.Service.FirstOrDefault(i => i.Id == service.Id);

			obj.Name = service.Name;
			obj.LongDescription = service.LongDescription;
			obj.Price = service.Price;
			obj.ImageUrl = service.ImageUrl;
			obj.FrequencyId = service.FrequencyId;
			obj.CategoryId = service.CategoryId;

			_db.SaveChanges();
		}

	}
}
