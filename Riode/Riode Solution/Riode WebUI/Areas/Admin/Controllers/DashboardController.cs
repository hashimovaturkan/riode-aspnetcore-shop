using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Mail()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }
        public IActionResult Invoice()
        {
            return View();
        }
    }
}
