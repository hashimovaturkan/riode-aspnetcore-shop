using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Riode_WebUI.AppCode.Application.ColorsModule;
using Riode_WebUI.Models.DataContexts;
using System.Threading.Tasks;

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

        [Authorize(Policy ="admin.colors.index")]
        public async Task<IActionResult> Index(ColorPagedQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
                return NotFound();

            return View(response);
        }

        [Authorize(Policy ="admin.colors.details")]
        public async Task<IActionResult> Details(ColorSingleQuery query)
        {
            var response =await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        [Authorize(Policy = "admin.colors.create")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.colors.create")]
        public async Task<IActionResult> Create(ColorCreateCommand command)
        {
            var response = await mediator.Send(command);
            if(response > 0)
                return RedirectToAction(nameof(Index));
            
            return View(command);
        }

        [Authorize(Policy = "admin.colors.edit")]
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
        [Authorize(Policy = "admin.colors.edit")]
        public async Task<IActionResult> Edit(ColorUpdateCommand command)
        {
            var response =await mediator.Send(command);

            if(response > 0)
            {
                return RedirectToAction(nameof(Index));
            }
               
            return View(command);
        }

        
        [HttpPost]
        [Authorize(Policy = "admin.colors.delete")]
        public async Task<IActionResult> Delete(ColorDeleteCommand command)
        {
            var response = await mediator.Send(command);
            return Json(response);
        }
    }
}
