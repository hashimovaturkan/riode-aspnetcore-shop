using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
