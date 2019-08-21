using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
	public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
	{
		private readonly ApplicationDbContext _db;

		public FrequencyRepository(ApplicationDbContext db):base(db)
		{
			_db = db;
		}

		public IEnumerable<SelectListItem> GetFrequencyListForDropDown()
		{
			return _db.Frequency.Select(i => new SelectListItem()
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
		}

		public void Update(Frequency frequency)
		{
			var obj = _db.Frequency.FirstOrDefault(i => i.Id == frequency.Id);

			obj.Name = frequency.Name;
			obj.FrequencyCount = frequency.FrequencyCount;

			_db.SaveChanges();
		}
	}
}
