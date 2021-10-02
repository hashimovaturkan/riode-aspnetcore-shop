using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.AppCode.Application.CategoriesModule;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AllowAnonymous]
    public class CategoriesController : Controller
    {
        private readonly RiodeDbContext db;
        readonly IMediator mediator;

        public CategoriesController(RiodeDbContext db,IMediator mediator)
        {
            this.mediator = mediator;
            this.db = db;
        }

        [Authorize(Policy = "admin.categories.index")]
        public async Task<IActionResult> Index(CategoryPagedQuery query)
        {
            var response = await mediator.Send(query);

            return View(response);
        }

        [Authorize(Policy = "admin.categories.details")]
        public async Task<IActionResult> Details(CategorySingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = db.Categories.Select(s=>s.Id == response.ParentId);
            //ViewData["ParentId"] = new SelectList(db.Categories, "Id", "Name");
            return View(response);
        }

        [Authorize(Policy = "admin.categories.create")]
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.categories.create")]
        public async Task<IActionResult> Create(CategoryCreateCommand command)
        {
            var response = await mediator.Send(command);
            if (response > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["ParentId"] = new SelectList(db.Categories, "Id", "Name", command.ParentId);
            return View(command);
        }

        [Authorize(Policy = "admin.categories.edit")]
        public async Task<IActionResult> Edit(CategorySingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            var vm = new CategoryViewModel();
            vm.Id = response.Id;
            vm.Name = response.Name;
            vm.Description = response.Description;
            vm.ParentId = response.ParentId;
            
            ViewData["ParentId"] = new SelectList(db.Categories, "Id", "Name", response.ParentId);
            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.categories.edit")]
        public async Task<IActionResult> Edit(CategoryUpdateCommand command)
        {
            var response = await mediator.Send(command);

            if (response > 0)
                return RedirectToAction(nameof(Index));

            
            ViewData["ParentId"] = new SelectList(db.Categories, "Id", "Name", command.ParentId);
            return View(command);
        }


        
        [HttpPost]
        [Authorize(Policy = "admin.categories.delete")]
        public async Task<IActionResult> Delete(CategoryDeleteCommand command)
        {
            var result = await mediator.Send(command);
            return Json(result);
        }

    }
}
