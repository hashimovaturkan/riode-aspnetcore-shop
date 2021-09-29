using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using Riode_WebUI.Models.FormModels;
using Riode_WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        readonly RiodeDbContext db;
        public ShopController(RiodeDbContext db)
        {
            this.db = db;
        }

        [Authorize(Policy = "ui.shop.index")]
        public IActionResult Index()
        {
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

        [HttpPost]
        //json formatda
        //public IActionResult Filter([FromBody]ShopFilterFormModel model)
        [Authorize(Policy = "ui.shop.filter")]
        public IActionResult Filter(ShopFilterFormModel model)
        {
            var query = db.Products
                .Include(p => p.Images.Where(i => i.IsMain == true))
                .Include(q => q.Brand)
                .Include(s => s.ProductSizeColorCollections)
                .Where(s => s.DeletedByUserId == null)
                .AsQueryable();

            if (model?.Brands?.Count()>0)
            {
                query = query.Where(p => model.Brands.Contains((int)p.BrandId));
            }

            if (model?.Sizes?.Count() > 0)
            {
                query = query.Where(p => p.ProductSizeColorCollections
                        .Any(q => model.Sizes.Contains((int)q.SizeId)));
            }

            if (model?.Colors?.Count() > 0)
            {
                query = query.Where(p => p.ProductSizeColorCollections
                        .Any(q => model.Colors.Contains((int)q.ColorId)));
            }


            return PartialView("_ProductContainer", query.ToList());
            //return Json(new{
            //    error=false,
            //    data = query.ToList()
            //});
        }

        [Authorize(Policy = "ui.shop.details")]
        public IActionResult Details(long id)
        {
            var data = db.Products
                 .Include(p => p.Images)
                 .Include(p => p.Brand)
                 .Include(p => p.Category)
                 .Include(p => p.SpecificationValues.Where(s=>s.DeletedByUserId==null))
                 .ThenInclude(s=>s.Specification)
                 .FirstOrDefault(s => s.DeletedByUserId == null && s.Id == id);

            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

    }
}
