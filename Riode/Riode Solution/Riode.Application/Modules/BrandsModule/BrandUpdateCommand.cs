using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Riode.Application.Core.Extensions;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.BrandsModule
{
    public class BrandUpdateCommand: BrandViewModel, IRequest<long>
    {
        
        public class BrandUpdateCommandHandler : IRequestHandler<BrandUpdateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            public BrandUpdateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                this.ctx = ctx;
                this.db = db;
            }

            public async Task<long> Handle(BrandUpdateCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null  || request.Id < 0)
                    return 0;

                var brand =await db.Brands.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedByUserId == null);

                if(brand == null)
                {
                    return 0;
                }

                if (ctx.IsModelStateValid())
                {
                    brand.Name = request.Name;
                    brand.Description = request.Description;

                    await db.SaveChangesAsync(cancellationToken);

                    return brand.Id;
                }
                return 0;

            }
        }
    }
}
