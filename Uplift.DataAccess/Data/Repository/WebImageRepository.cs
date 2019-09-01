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
	public class WebImageRepository : Repository<WebImage>, IWebImageRepository
	{
		private readonly ApplicationDbContext _db;

		public WebImageRepository(ApplicationDbContext db):base(db)
		{
			_db = db;
		}
		

		public void Update(WebImage webImage)
		{
			var obj = _db.WebImage.FirstOrDefault(i => i.Id == webImage.Id);

			obj.Name = webImage.Name;
			obj.Image = webImage.Image;

			_db.SaveChanges();
		}

	}
}
