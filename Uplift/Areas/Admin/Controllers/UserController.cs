using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
	[Authorize(Roles=StaticDetails.Admin)]
	[Area("Admin")]
    public class UserController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;

		public UserController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public IActionResult Index()
        {
			var claimIdentity = (ClaimsIdentity)this.User.Identity;

			var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(_unitOfWork.User.GetAll(u=>u.Id!=claims.Value));
        }

		public IActionResult Lock(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			_unitOfWork.User.LockUser(id);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Unlock(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			_unitOfWork.User.UnlockUser(id);
			return RedirectToAction(nameof(Index));
		}
	}
}