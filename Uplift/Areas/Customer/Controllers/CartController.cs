using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Extensions;
using Uplift.Models;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		[BindProperty]
		private CartViewModel CartVM { get; set; }

		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			CartVM = new CartViewModel()
			{
				OrderHeader = new OrderHeader(),
				ServiceList = new List<Service>()
			};
		}
        public IActionResult Index()
        {
			if (HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart) != null)
			{
				List<int> sessionList = new List<int>();
				sessionList = HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart);

				foreach (int serviceId in sessionList)
				{
					CartVM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(x => x.Id == serviceId, includeProperties: "Frequency,Category"));
				}
			}
            return View(CartVM);
        }

		public IActionResult Summary()
		{
			if (HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart) != null)
			{
				List<int> sessionList = new List<int>();
				sessionList = HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart);

				foreach (int serviceId in sessionList)
				{
					CartVM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(x => x.Id == serviceId, includeProperties: "Frequency,Category"));
				}
			}
			return View(CartVM);
		}

		public IActionResult Remove(int serviceId)
		{
			List<int> sessionList = new List<int>();
			sessionList = HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart);
			sessionList.Remove(serviceId);

			if (sessionList.Count == 0)
				sessionList = null;

			HttpContext.Session.SetObject(StaticDetails.SessionCart, sessionList);

			return RedirectToAction(nameof(Index));
		}
    }
}