using Microsoft.AspNetCore.Mvc;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly RiodeDbContext db;
        public HomeController(RiodeDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactPost model)
        {
            if (ModelState.IsValid)
            {
                db.ContactPosts.Add(model);
                db.SaveChanges();

                return Json(new { 
                    error=false,
                    message= "Your request has been successfully submitted"

                });
            }
            return Json(new
            {
                error = true,
                message = "Please try again.."

            });
        }

        public IActionResult About()
        {
            return View();
        }


        public IActionResult FAQ()
        {
            var datas = db.Faqs
                .Where(f => f.DeletedByUserId == null)
                .ToList();
            return View(datas);
        }
    }
}
