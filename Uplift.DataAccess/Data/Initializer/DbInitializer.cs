using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.Models;
using Uplift.Utility;

namespace Uplift.DataAccess.Data.Initializer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager )
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public void Initialize()
		{
			try
			{
				if (_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception ex)
			{ 
			}

			if (_db.Roles.Any(r => r.Name == StaticDetails.Admin)) return;

			_roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(StaticDetails.Manager)).GetAwaiter().GetResult();

			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "admin@gmail.com",
				Email="admin@gmail.com",
				EmailConfirmed=true,
				Name = "Nathan Pascual"
			},"Common123*").GetAwaiter().GetResult();

			IdentityUser user = _db.Users.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();
			_userManager.AddToRoleAsync(user, StaticDetails.Admin).GetAwaiter().GetResult();
		}
	}
}
