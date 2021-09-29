using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    public class OrderController : Controller
    {
        [Authorize(Policy = "ui.order.cart")]
        public IActionResult Cart()
        {
            return View();
        }

        [Authorize(Policy = "ui.order.checkout")]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize(Policy = "ui.order.index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
