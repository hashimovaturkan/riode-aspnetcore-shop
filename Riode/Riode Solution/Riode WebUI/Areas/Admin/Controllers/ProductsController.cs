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
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using Riode.Domain.Models.FormModels;
using Riode.Application.Modules.ProductsModule;
using Riode.Application.Modules.CategoriesModule;
using Riode.Application.Modules.BrandsModule;

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
            
        }

        
        [HttpPost]
        [Authorize(Policy = "admin.products.delete")]
        public async Task<IActionResult> Delete(ProductDeleteCommand command)
        {
            var result = await mediator.Send(command);
            return Json(result);
        }

        [HttpPost]
        [Authorize(Policy = "admin.products.choose")]
        public async Task<IActionResult> ChooseSpecification(ProductChooseSpecificationCommand command)
        {
            var result = await mediator.Send(command);
            return Json(result);
            
        }


    }

    
}
