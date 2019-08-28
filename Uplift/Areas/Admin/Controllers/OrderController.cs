﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class OrderController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;
		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Details(int id)
		{
			OrderViewModel orderVM = new OrderViewModel()
			{
				OrderHeader = _unitOfWork.OrderHeader.Get(id),
				OrderDetails = _unitOfWork.OrderDetails.GetAll(filter: o => o.OrderHeaderId == id)
			};
			return View(orderVM);
		}

		#region API
		public IActionResult GetAllOrders()
		{
			return Json(new { data = _unitOfWork.OrderHeader.GetAll() });
		}

		public IActionResult GetAllPendingOrders()
		{ 
			return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter:o=>o.Status==StaticDetails.StatusSubmitted) });
		}

		public IActionResult GetAllApprovedOrders()
		{
			return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == StaticDetails.StatusApproved) });
		}
		#endregion
	}
}