using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.AppCode.Application.SizesModule;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AllowAnonymous]
    public class SizesController : Controller
    {
        readonly IMediator mediator;

        public SizesController( IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Policy ="admin.sizes.index")]
        public async Task<IActionResult> Index(SizePagedQuery query)
        {
            var response =await mediator.Send(query);

            if(response == null)
                return NotFound();

            return View(response);
        }

        [Authorize(Policy = "admin.sizes.details")]
        public async Task<IActionResult> Details(SizeSingleQuery query)
        {
            var response =await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        [Authorize(Policy = "admin.sizes.create")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.sizes.create")]
        public async Task<IActionResult> Create(SizeCreateCommand command)
        {
            var response =await mediator.Send(command);
            if(response > 0)
                return RedirectToAction(nameof(Index));
            
            return View(command);
        }

        [Authorize(Policy = "admin.sizes.edit")]
        public async Task<IActionResult> Edit(SizeSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            var vm = new SizeViewModel();
            vm.Id= response.Id;
            vm.Name = response.Name;
            vm.Description = response.Description;
            vm.Abbr = response.Abbr;
            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.sizes.edit")]
        public async Task<IActionResult> Edit(SizeUpdateCommand command)
        {
            var response =await mediator.Send(command);
            if(response > 0)
                return RedirectToAction(nameof(Index));
            
            return View(command);
        }


        [HttpPost]
        [Authorize(Policy = "admin.sizes.delete")]
        public async Task<IActionResult> Delete(SizeDeleteCommand command)
        {
            var response = await mediator.Send(command);
            return Json(response);
        }

    }
}
