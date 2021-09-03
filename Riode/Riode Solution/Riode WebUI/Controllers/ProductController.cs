using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductSimple()
        {
            return View();
        }

        public IActionResult Details(long id)
        {
            var db = new RiodeDbContext();
            var data = db.Products
                 .Include(p => p.Images)
                 .Include(p=>p.Brand)
                 .Include(p=>p.Category)
                 .FirstOrDefault(s => s.DeletedByUserId == null && s.Id==id);

            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
    }
}
