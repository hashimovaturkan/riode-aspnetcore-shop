using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    public class ElementController : Controller
    {
        public IActionResult ElementProducts()
        {
            return View();
        }
    }
}
