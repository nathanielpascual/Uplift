using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
    public class WebImageController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;
		

		public WebImageController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Upsert(int? id)
		{
			WebImage webImage = new WebImage();

			if (id == null)
			{
				return View(webImage);
			}

			webImage = _unitOfWork.WebImage.Get(id.GetValueOrDefault());

			if (webImage == null) 
			{
				return NotFound();
			}

			return View(webImage);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(WebImage webImage)
		{
			if (ModelState.IsValid) {
				var files = HttpContext.Request.Form.Files;

				if (files.Count > 0)
				{
					byte[] img = null;
					using (var fileStream = files[0].OpenReadStream())
					{
						using (var memoryStream = new MemoryStream())
						{
							fileStream.CopyTo(memoryStream);
							img = memoryStream.ToArray();
						}
					}

					webImage.Image = img;
				}

				if (webImage.Id == 0)
				{
					
					_unitOfWork.WebImage.Add(webImage);
				}
				else
				{
					var webImg = _unitOfWork.WebImage.Get(webImage.Id);

					if (files.Count > 0)
					{
						webImg.Image = webImage.Image;
					}
					else
					{
						webImg.Image = webImg.Image;
					}

					webImg.Name = webImage.Name;

					_unitOfWork.WebImage.Update(webImg);
				}

				_unitOfWork.Save();

				return RedirectToAction(nameof(Index));
			}

			return View(webImage);
		}


		#region API
		[HttpGet]
		public IActionResult GetAll()
		{
			return Json(new { data = _unitOfWork.WebImage.GetAll() });
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var obj = _unitOfWork.WebImage.Get(id);

			if (obj == null)
			{
				return Json(new { success= false, message="Error while deleting."});
			}

			_unitOfWork.WebImage.Remove(obj);
			_unitOfWork.Save();

			return Json(new { success = true, message = "Deleted Successful." });
		}
		#endregion
	}
}