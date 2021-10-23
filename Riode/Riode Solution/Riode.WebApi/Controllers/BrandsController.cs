using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Riode.Application.Modules.BrandsModule;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        readonly IMediator mediator;
        public BrandsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{PageIndex}/{PageSize}")]
        [Authorize(Policy = "api.brands.getall")]
        [SwaggerOperation(Summary = "Brendləri səhifə şəklində əldə etmək.",
            Description = "Səhifənin indexini və tutumunu verməklə cari səhifədəki brendləri əldə etmək olar.")]
        public async Task<IActionResult> Get([FromRoute] BrandPagedQuery query)
        {
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{Id}")]
        [Authorize(Policy = "api.brands.get")]
        [SwaggerOperation(Summary = "İd'ə uyğun brendi əldə etmək.",
            Description = "Uyğun individual kodunu verməklə brendi əldə etmək olar.")]
        public async Task<IActionResult> Get([FromRoute] BrandSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "api.brands.add")]
        [SwaggerOperation(Summary = "Brend yaratmaq",
            Description = "Daxil olunmuş məlumatlar əsasında brend'in yaradılması.")]
        public async Task<IActionResult> Create(BrandCreateCommand command)
        {
            var id = await mediator.Send(command);
            if (id > 0)
            {
                return CreatedAtAction(nameof(Get), routeValues: new { 
                    Id = id
                }, null);
            }
            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPut]
        [Authorize(Policy = "api.brands.edit")]
        [SwaggerOperation(Summary = "Brendi dəyişmək",
            Description = "Daxil olunmuş məlumatlar əsasında brend'in dəyişmək.")]
        public async Task<IActionResult> Edit(BrandSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }
            
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Policy = "api.brands.delete")]
        [SwaggerOperation(Summary = "İd'ə əsasən brendi silmək",
            Description = "Daxil olunmuş individual kodun əsasında brend'i silmək.")]
        public async Task<IActionResult> Delete(long id)
        {

            var result = await mediator.Send(new BrandDeleteCommand(id));
            return NoContent();

        }
    }
}
