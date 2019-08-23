using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class ServiceController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostEnvironment;
		public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_hostEnvironment = hostEnvironment;
		}
        public IActionResult Index()
        {
            return View();
        }

		#region API
		[HttpGet]
		public IActionResult GetAll()
		{
			return Json(new { data = _unitOfWork.Service.GetAll(includeProperties:"Category,Frequency") });
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var obj = _unitOfWork.Service.Get(id);

			if (obj == null)
			{
				return Json(new { success= false, message="Error while deleting."});
			}

			_unitOfWork.Service.Remove(obj);
			_unitOfWork.Save();

			return Json(new { success = true, message = "Deleted Successful." });
		}
		#endregion
	}
}