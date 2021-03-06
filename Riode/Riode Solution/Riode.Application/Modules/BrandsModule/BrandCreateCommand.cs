using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.Application.Core.Extensions;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.BrandsModule
{
    public class BrandCreateCommand:IRequest<long>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            public BrandCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                this.ctx = ctx;
                this.db = db;
            }
            public async Task<long> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsModelStateValid())
                {
                    var brand = new Brand();
                    brand.Name = request.Name;
                    brand.Description = request.Description;

                    //todo
                    db.Brands.Add(brand);
                    await db.SaveChangesAsync(cancellationToken);
                    return brand.Id;
                }

                return 0;

            }
        }
    }

    
}
