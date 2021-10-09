using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Riode_WebUI.AppCode.Application.SpecificationsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecificationsController : Controller
    {
        readonly IMediator mediator;

        public SpecificationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Policy = "admin.specifications.index")]
        public async Task<IActionResult> Index(SpecificationPagedQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
                return NotFound();

            return View(response);
        }

        [Authorize(Policy = "admin.specifications.details")]
        public async Task<IActionResult> Details(SpecificationSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        [Authorize(Policy = "admin.specifications.create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.specifications.create")]
        public async Task<IActionResult> Create(SpecificationCreateCommand command)
        {
            var response = await mediator.Send(command);
            if (response > 0)
                return RedirectToAction(nameof(Index));

            return View(command);
        }

        [Authorize(Policy = "admin.specifications.edit")]
        public async Task<IActionResult> Edit(SpecificationSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            var vm = new SpecificationViewModel();
            //vm.Id = response.Id;
            //vm.Name = response.Name;
            //vm.Description = response.Description;
            //vm.Abbr = response.Abbr;
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.specifications.edit")]
        public async Task<IActionResult> Edit(SpecificationUpdateCommand command)
        {
            var response = await mediator.Send(command);
            //if (response > 0)
                return RedirectToAction(nameof(Index));

            return View(command);
        }


        [HttpPost]
        [Authorize(Policy = "admin.specifications.delete")]
        public async Task<IActionResult> Delete(SpecificationDeleteCommand command)
        {
            var response = await mediator.Send(command);
            return Json(response);
        }
    }
}
