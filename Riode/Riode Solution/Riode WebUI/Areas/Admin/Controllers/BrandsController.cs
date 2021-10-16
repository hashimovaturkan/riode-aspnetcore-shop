using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using Riode.Application.Modules.BrandsModule;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AllowAnonymous]
    public class BrandsController : Controller
    {
        readonly IMediator mediator;
        public BrandsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Policy = "admin.brands.index")]
        public async Task<IActionResult> Index(BrandPagedQuery query)
        {
            var response =await mediator.Send(query);

            return View(response);
        }

        [Authorize(Policy = "admin.brands.details")]
        public async Task<IActionResult> Details(BrandSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        [Authorize(Policy = "admin.brands.create")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.brands.create")]
        public async Task<IActionResult> Create(BrandCreateCommand command)
        {
            var id = await mediator.Send(command);
            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [Authorize(Policy = "admin.brands.edit")]
        public async Task<IActionResult> Edit(BrandSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            //SUAL: burda nie elave viewmodel yazadiriqki ondansa ele brand yazaqda viewda model kimi
            //onsuz heccur user nese eliye bilmez
            var vm = new BrandViewModel();
            vm.Id = response.Id;
            vm.Name = response.Name;
            vm.Description = response.Description;
            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.brands.edit")]
        public async Task<IActionResult> Edit(BrandUpdateCommand command)
        {
            
            var response =await mediator.Send(command);

            if (response > 0)
                return RedirectToAction(nameof(Index));
            
            return View(command);
        }


        [HttpPost]
        [Authorize(Policy = "admin.brands.delete")]
        public async Task<IActionResult> Delete(BrandDeleteCommand command)
        {

            var result = await mediator.Send(command);
            return Json(result);

        }

    }
}
