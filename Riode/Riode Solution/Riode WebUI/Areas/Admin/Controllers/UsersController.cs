using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Application.Core.Extensions;
using Riode.Domain.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        readonly RiodeDbContext db;
        public UsersController(RiodeDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.users.index")]
        public async Task<IActionResult> Index()
        {
            var data =await db.Users.ToListAsync();
            return View(data);
        }

        [Authorize(Policy = "admin.users.details")]
        public async Task<IActionResult> Details(long id)
        {
            var data =await db.Users.FirstOrDefaultAsync(c=> c.Id == id);
            if (data == null)
                return NotFound();

            ViewBag.Roles =await (from r in db.Roles
                             join ur in db.UserRoles
                             on new { RoleId = r.Id, UserId = data.Id } equals new { ur.RoleId, ur.UserId } into lJoin
                             from lj in lJoin.DefaultIfEmpty()
                             select Tuple.Create(r.Id, r.Name, lj != null))
                             .ToListAsync();

            ViewBag.Principals = (from p in Extension.principals
                                   join uc in db.UserClaims
                                   on new { ClaimValue = "1", ClaimType = p , UserId = data.Id} equals new { uc.ClaimValue, uc.ClaimType, uc.UserId } into lJoin
                                   from lj in lJoin.DefaultIfEmpty()
                                   select Tuple.Create(p, lj != null))
                             .ToList();

            return View(data);
        }
    }
}
