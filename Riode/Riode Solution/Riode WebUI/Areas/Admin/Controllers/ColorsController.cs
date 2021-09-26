using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.AppCode.Application.ColorsModule;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using Riode_WebUI.Models.ViewModels;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorsController : Controller
    {
        private readonly RiodeDbContext db;
        readonly IMediator mediator;

        public ColorsController(RiodeDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        // GET: Admin/Colors
        public async Task<IActionResult> Index(ColorPagedQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
                return NotFound();

            return View(response);
        }

        // GET: Admin/Colors/Details/5
        public async Task<IActionResult> Details(ColorSingleQuery query)
        {
            var response =await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // GET: Admin/Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColorCreateCommand command)
        {
            var response = await mediator.Send(command);
            if(response > 0)
                return RedirectToAction(nameof(Index));
            
            return View(response);
        }

        // GET: Admin/Colors/Edit/5
        public async Task<IActionResult> Edit(ColorSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            var vm =new ColorViewModel();
            vm.Name = response.Name;
            vm.Description = response.Description;
            vm.HexCode = response.HexCode;
            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ColorUpdateCommand command)
        {
            var response =await mediator.Send(command);

            if(response > 0)
            {
                return RedirectToAction(nameof(Index));
            }
               
            return View(response);
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(ColorDeleteCommand command)
        {
            var response = await mediator.Send(command);
            return Json(response);
        }
    }
}
