using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    public class BlogController : Controller
    {
        readonly RiodeDbContext db;
        public BlogController(RiodeDbContext db)
        {
            this.db = db;
        }
        public IActionResult Details(long id)
        {
            var data = db.BlogPosts
                .Include(b=>b.Images)
                .FirstOrDefault(s => s.DeletedByUserId == null && s.Id == id);

            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        public IActionResult Index()
        {
            var datas = db.BlogPosts
                .Include(b => b.Images.Where(c => c.IsMain))
                .Where(b => b.DeletedByUserId == null)
                .ToList();
            return View(datas);
        }
    }
}
