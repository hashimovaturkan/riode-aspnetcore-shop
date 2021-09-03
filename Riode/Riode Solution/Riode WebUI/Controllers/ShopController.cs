using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using Riode_WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            var db = new RiodeDbContext();
            var viewModel = new CategoryViewModel(); 
            viewModel.Categories = db.Categories
                .Include(c=>c.Parent)
                .Include(c=>c.Children)
                .ThenInclude(c=>c.Children)
                .Where(c=>c.ParentId==null && c.DeletedByUserId==null)
                .ToList();

            viewModel.Brands = db.Brands
                .Where(b => b.DeletedByUserId == null)
                .ToList();

            viewModel.Colors = db.Colors
                .Where(c => c.DeletedByUserId == null)
                .ToList();

            viewModel.Sizes = db.Sizes
                .Where(s => s.DeletedByUserId == null)
                .ToList();

            viewModel.Products = db.Products
                .Include(p=>p.Images.Where(i=>i.IsMain == true))
                .Where(s => s.DeletedByUserId == null)
                .ToList();
            return View(viewModel);
        }

        public IActionResult Details(long id)
        {
            var db = new RiodeDbContext();
            var data = db.Products
                 .Include(p => p.Images)
                 .Include(p => p.Brand)
                 .Include(p => p.Category)
                 .FirstOrDefault(s => s.DeletedByUserId == null && s.Id == id);

            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

    }
}
