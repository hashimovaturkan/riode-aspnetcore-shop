using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.AppCode.Application.BrandsModule;
using Riode_WebUI.AppCode.Application.CategoriesModule;
using Riode_WebUI.AppCode.Application.ProductsModule;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using Riode_WebUI.Models.FormModels;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AllowAnonymous]
    public class ProductsController : Controller
    {
        private readonly RiodeDbContext db;
        private readonly IWebHostEnvironment env;
        readonly IMediator mediator;

        public ProductsController(RiodeDbContext db, IWebHostEnvironment env, IMediator mediator)
        {
            this.db = db;
            this.env = env;
            this.mediator = mediator;
        }

        [Authorize(Policy = "admin.products.index")]
        public async Task<IActionResult> Index(ProductPagedQuery query)
        {
            var response = await mediator.Send(query);

            return View(response);
        }

        [Authorize(Policy = "admin.products.details")]
        public async Task<IActionResult> Details(ProductSingleQuery query)
        {

            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            var brand = await mediator.Send(new BrandChooseQuery());
            var category = await mediator.Send(new CategoryChooseQuery());
            ViewData["BrandId"] = brand.Where(b=> b.Id==response.BrandId).FirstOrDefault(b => b.DeletedByUserId==null).Name;
            ViewData["CategoryId"] = category.Where(b => b.Id == response.CategoryId).FirstOrDefault(b => b.DeletedByUserId == null).Name;

            return View(response);
        }

        [Authorize(Policy = "admin.products.create")]
        public async Task<IActionResult> Create()
        {

            //ViewData["ColorId"] = db.ProductSizeColorCollection.Select(p=>p.Color).ToList();
            ViewData["BrandId"] = new SelectList(await mediator.Send(new BrandChooseQuery()), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(await mediator.Send(new CategoryChooseQuery()), "Id", "Name");
            ViewBag.Specifications = db.Specifications.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.products.create")]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            var response = await mediator.Send(command);

            ViewData["BrandId"] = new SelectList(await mediator.Send(new BrandChooseQuery()), "Id", "Name", command.BrandId);
            ViewData["CategoryId"] = new SelectList(await mediator.Send(new CategoryChooseQuery()), "Id", "Name", command.CategoryId);

            if (response > 0)
                return Ok(new
                {
                    error = false,
                    message="It was created successfully!"
                });

            return Ok(new
            {
                error = true,
                message= "Please, try again!"
            });
        }

        [Authorize(Policy = "admin.products.edit")]
        public async Task<IActionResult> Edit(ProductSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            var vm = new ProductViewModel();
            vm.Id = response.Id;
            vm.Name = response.Name;
            vm.Description = response.Description;
            vm.ShortDescription = response.ShortDescription;
            vm.SkuCode = response.SkuCode;
            vm.Images = response.Images;
            vm.Files = response.Files;
            vm.BrandId = response.BrandId;
            vm.CategoryId = response.CategoryId;
            vm.ProductSizeColorCollections = response.ProductSizeColorCollections;

            ViewBag.Specifications = db.Specifications.ToList();
            ViewData["BrandId"] = new SelectList(await mediator.Send(new BrandChooseQuery()), "Id", "Name", response.BrandId);
            ViewData["CategoryId"] = new SelectList(await mediator.Send(new CategoryChooseQuery()), "Id", "Name", response.CategoryId);

            //var productEditCommand = mapper.Map<ProductEditCommand>(product);

            //sual burani nece cixarim
            vm.SelectedSpecifications = (from spec in db.Specifications
                                        join sv in db.SpecificationValues.Where(ss => ss.ProductId == response.Id) on spec.Id equals sv.SpecificationId into lSv
                                        from l in lSv.DefaultIfEmpty()
                                        select new SelectedSpecificationFormModel
                                        {
                                            Id = spec.Id,
                                            Name = spec.Name,
                                            Value = l.Value
                                        })
                                         .ToArray();

            return View(vm);

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var product = await db.Products.Include(k=>k.Images.Where(i=>i.DeletedDate==null))
            //                                .FirstOrDefaultAsync(k => k.Id == id && k.DeletedDate==null);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name", response.BrandId);
            //ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", response.CategoryId);
            //return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.products.edit")]
        public async Task<IActionResult> Edit(ProductUpdateCommand command)
        {
            var response = await mediator.Send(command);

            if (response > 0)
                return RedirectToAction(nameof(Index));

            ViewData["BrandId"] = new SelectList(await mediator.Send(new BrandChooseQuery()), "Id", "Name", command.BrandId);
            ViewData["CategoryId"] = new SelectList(await mediator.Send(new CategoryChooseQuery()), "Id", "Name", command.CategoryId);

            return View(command);
            //    if (id != product.Id)
            //    {
            //        return NotFound();
            //    }

            //    if (ModelState.IsValid)
            //    {
            //        var entity =await db.Products
            //            .Include(p=>p.Images.Where(pi=>pi.DeletedByUserId == null))
            //            .FirstOrDefaultAsync(p => p.Id == product.Id && p.DeletedByUserId == null);
            //        entity.Name = product.Name;
            //        entity.Description = product.Description;

            //        /// Silinmishler ---start
            //        int[] deletedImageIds = product.Files.Where(p => p.Id > 0 && string.IsNullOrWhiteSpace(p.TempPath))
            //                                        .Select(p => p.Id.Value)
            //                                        .ToArray();

            //        foreach (var itemId in deletedImageIds)
            //        {
            //            var oldImage = await db.ProductImages.FirstOrDefaultAsync(pi => pi.ProductId == entity.Id && pi.Id == itemId);

            //            if (oldImage == null)
            //                continue;

            //            oldImage.DeletedDate = DateTime.Now;
            //            oldImage.DeletedByUserId = 1;
            //        }
            //        /// Silinmishler ---end

            //        foreach (var item in product.Files.Where(f=>(f.Id>0 && !string.IsNullOrWhiteSpace(f.TempPath)) //bazada olub deyismeyenler
            //        || (f.File != null && f.Id ==null)))///yeni yarananlar
            //        {
            //            if(item.File == null) //deyismeyenler
            //            {
            //                var oldImage = await db.ProductImages.FirstOrDefaultAsync(pi => pi.ProductId == entity.Id && pi.Id == item.Id);

            //                if (oldImage == null)
            //                    continue;

            //                oldImage.IsMain = item.IsMain;
            //            }
            //            else if(item.File != null) //yeni elave olunanlar
            //            {
            //                string extension = Path.GetExtension(item.File.FileName);
            //                string fileName = $"{Guid.NewGuid()}{extension}";

            //                string physicalFileName = Path.Combine(env.ContentRootPath,
            //                                                       "wwwroot",
            //                                                       "uploads",
            //                                                       "images",
            //                                                       "product",
            //                                                       fileName);

            //                using (var stream = new FileStream(physicalFileName, FileMode.Create, FileAccess.Write))
            //                {
            //                    await item.File.CopyToAsync(stream);
            //                }

            //                entity.Images.Add(new ProductImage
            //                {
            //                    FileName = fileName,
            //                    IsMain = item.IsMain
            //                });

            //            }
            //        }





            //        await db.SaveChangesAsync();
            //        return RedirectToAction(nameof(Index));
            //    }
            //    ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Id", product.BrandId);
            //    ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Id", product.CategoryId);
            //    return View(product);
        }

        //[Authorize(Policy = "admin.products.delete")]
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await db.Products
        //        .Include(p => p.Brand)
        //        .Include(p => p.Category)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        
        [HttpPost]
        [Authorize(Policy = "admin.products.delete")]
        public async Task<IActionResult> Delete(ProductDeleteCommand command)
        {
            var result = await mediator.Send(command);
            return Json(result);
            //var product = await db.Products.FindAsync(id);
            //db.Products.Remove(product);
            //await db.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        //private bool ProductExists(long id)
        //{
        //    return db.Products.Any(e => e.Id == id);
        //}
    }
}
