﻿using System;
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
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
		private readonly ApplicationDbContext _db;

		public OrderHeaderRepository(ApplicationDbContext db):base(db)
		{
			_db = db;
		}

		public void ChangeOrderStatus(int orderHeaderId, string status)
		{
			var order = _db.OrderHeader.FirstOrDefault(o => o.Id == orderHeaderId);
			order.Status = status;
			_db.SaveChanges();
		}

	}
}
