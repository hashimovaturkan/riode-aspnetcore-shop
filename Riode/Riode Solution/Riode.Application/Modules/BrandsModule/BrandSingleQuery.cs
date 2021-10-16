using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.BrandsModule
{
    public class BrandSingleQuery : IRequest<Brand>
    {
        public long? Id { get; set; }

        public class BrandSingleQueryHandler : IRequestHandler<BrandSingleQuery, Brand>
        {
            readonly RiodeDbContext db;
            public BrandSingleQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<Brand> Handle(BrandSingleQuery request, CancellationToken cancellationToken)
            {
                //sual: burda null olub olmasini nie yoxlamirig <=0 eliyirik
                if (request.Id<=0 || request.Id == null)
                {
                    return null;
                }

                var brand = await db.Brands
                .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedByUserId == null, cancellationToken);

                return brand;
            }
        }
    }

   

}
