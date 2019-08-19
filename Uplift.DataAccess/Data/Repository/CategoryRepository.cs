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
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;

		public CategoryRepository(ApplicationDbContext db):base(db)
		{
			_db = db;
		}
		
		public IEnumerable<SelectListItem> GetCategoryListForDropDown()
		{
			return _db.Category.Select(i => new SelectListItem()
			{
				Text = i.Name,
				Value = i.Id.ToString()
			}) ;
		}

		public void Update(Models.Category category)
		{
			var obj = _db.Category.FirstOrDefault(i => i.Id == category.Id);

			obj.Name = category.Name;
			obj.DisplayOrder = category.DisplayOrder;

			_db.SaveChanges();
		}

	}
}
