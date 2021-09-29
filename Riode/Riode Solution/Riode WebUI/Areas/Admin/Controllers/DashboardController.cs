using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AllowAnonymous]     bu authorize dan daha basqindi
    public class DashboardController : Controller
    {
        [Authorize(Policy = "admin.dashboard.index")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "admin.dashboard.mail")]
        public IActionResult Mail()
        {
            return View();
        }

        [Authorize(Policy = "admin.dashboard.chat")]
        public IActionResult Chat()
        {
            return View();
        }

        [Authorize(Policy = "admin.dashboard.invoice")]
        public IActionResult Invoice()
        {
            return View();
        }
    }
}
