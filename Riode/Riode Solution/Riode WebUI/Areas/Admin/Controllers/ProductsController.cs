using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using Riode_WebUI.Models.FormModels;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly RiodeDbContext db;
        private readonly IWebHostEnvironment env;

        public ProductsController(RiodeDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var riodeDbContext = db.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p=>p.Images.Where(i => i.DeletedDate == null))
                .Include(p=>p.ProductSizeColorCollections)
                .Where(s => s.DeletedByUserId == null);
            return View(await riodeDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var product = await db.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images.Where(i => i.DeletedDate == null))
                .Include(p => p.ProductSizeColorCollections.Where(s => s.ProductId == id))
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {

            ViewData["ColorId"] = db.ProductSizeColorCollection.Select(p=>p.Color).ToList();
            ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, ImageItemFormModel[] images)
        {
            if(images == null || !images.Any(i=>i.File != null))
            {
                ModelState.AddModelError("Images", "There are not images");
            }

            if (ModelState.IsValid)
            {
                product.Images = new List<ProductImage>();
                foreach (var image in images.Where(i => i.File != null))
                {
                    string extension = Path.GetExtension(image.File.FileName); //.jpg
                    string imagePath = $"{DateTime.Now:yyyyMMddHHmmss}-{Guid.NewGuid()}{extension}";
                    string physicalPath = Path.Combine(env.ContentRootPath,
                        "wwwroot",
                        "uploads",
                        "images",
                        "product",
                        imagePath);

                    using(var stream = new FileStream(physicalPath, FileMode.Create, FileAccess.Write))
                    {
                        await image.File.CopyToAsync(stream);
                    }

                    product.Images.Add(new ProductImage {
                        IsMain = image.IsMain,
                        FileName = imagePath
                    });
                }

                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products.Include(k=>k.Images.Where(i=>i.DeletedDate==null))
                                            .FirstOrDefaultAsync(k => k.Id == id && k.DeletedDate==null);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var entity =await db.Products
                    .Include(p=>p.Images.Where(pi=>pi.DeletedByUserId == null))
                    .FirstOrDefaultAsync(p => p.Id == product.Id && p.DeletedByUserId == null);
                entity.Name = product.Name;
                entity.Description = product.Description;

                /// Silinmishler ---start
                int[] deletedImageIds = product.Files.Where(p => p.Id > 0 && string.IsNullOrWhiteSpace(p.TempPath))
                                                .Select(p => p.Id.Value)
                                                .ToArray();

                foreach (var itemId in deletedImageIds)
                {
                    var oldImage = await db.ProductImages.FirstOrDefaultAsync(pi => pi.ProductId == entity.Id && pi.Id == itemId);

                    if (oldImage == null)
                        continue;

                    oldImage.DeletedDate = DateTime.Now;
                    oldImage.DeletedByUserId = 1;
                }
                /// Silinmishler ---end

                foreach (var item in product.Files.Where(f=>(f.Id>0 && !string.IsNullOrWhiteSpace(f.TempPath)) //bazada olub deyismeyenler
                || (f.File != null && f.Id ==null)))///yeni yarananlar
                {
                    if(item.File == null) //deyismeyenler
                    {
                        var oldImage = await db.ProductImages.FirstOrDefaultAsync(pi => pi.ProductId == entity.Id && pi.Id == item.Id);

                        if (oldImage == null)
                            continue;

                        oldImage.IsMain = item.IsMain;
                    }
                    else if(item.File != null) //yeni elave olunanlar
                    {
                        string extension = Path.GetExtension(item.File.FileName);
                        string fileName = $"{Guid.NewGuid()}{extension}";

                        string physicalFileName = Path.Combine(env.ContentRootPath,
                                                               "wwwroot",
                                                               "uploads",
                                                               "images",
                                                               "product",
                                                               fileName);

                        using (var stream = new FileStream(physicalFileName, FileMode.Create, FileAccess.Write))
                        {
                            await item.File.CopyToAsync(stream);
                        }

                        entity.Images.Add(new ProductImage
                        {
                            FileName = fileName,
                            IsMain = item.IsMain
                        });

                    }
                }





                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Id", product.BrandId);
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(long id)
        {
            return db.Products.Any(e => e.Id == id);
        }
    }
}
