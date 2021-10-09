using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.BrandsModule
{
    public class BrandChooseQuery : IRequest<List<Brand>>
    {
        public class BrandChooseQueryHandler : IRequestHandler<BrandChooseQuery, List<Brand>>
        {
            readonly RiodeDbContext db;
            public BrandChooseQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }

            public async Task<List<Brand>> Handle(BrandChooseQuery request, CancellationToken cancellationToken)
            {
                var brands = await db.Brands.Where(c => c.DeletedByUserId == null).ToListAsync();
                return brands;
            }
        }
    }
}
