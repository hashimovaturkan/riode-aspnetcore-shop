using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Riode.Application.Modules.BrandsModule;
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

        [HttpGet()]
        public async Task<IActionResult> Get([FromRoute]BrandPagedQuery query)
        {
            var response = await mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]BrandSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(response);
        }
    }
}
