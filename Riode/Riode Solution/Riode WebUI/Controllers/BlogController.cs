using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.ViewModels;
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
            var datas =new CategoryBlogPostViewModel();

            datas.Categories = db.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .ThenInclude(c => c.Children)
                .Where(c => c.ParentId == null && c.DeletedByUserId == null)
                .ToList();


            datas.BlogPost = db.BlogPosts
                .Include(b => b.Images)
                .FirstOrDefault(s => s.DeletedByUserId == null && s.Id == id);

            if (datas.Categories == null || datas.BlogPost == null)
            {
                return NotFound();
            }

            return View(datas);
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
