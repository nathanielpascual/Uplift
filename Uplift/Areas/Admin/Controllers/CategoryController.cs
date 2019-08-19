﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace Uplift.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class CategoryController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;
		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
        public IActionResult Index()
        {
            return View();
        }

		#region API
		[HttpGet]
		public IActionResult GetAll()
		{
			return Json(new { data = _unitOfWork.Category.GetAll() });
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var obj = _unitOfWork.Category.Get(id);

			if (obj == null)
			{
				return Json(new { success= false, message="Error while deleting."});
			}

			_unitOfWork.Category.Remove(obj);
			_unitOfWork.Save();

			return Json(new { success = true, message = "Deleted Successful." });
		}
		#endregion
	}
}