using MediatR;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.BrandsModule
{
    public class BrandCreateCommand:IRequest<Brand>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommand, Brand>
    {
        readonly RiodeDbContext db;
        public BrandCreateCommandHandler(RiodeDbContext db)
        {
            this.db = db;
        }
        public async Task<Brand> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
        {
            var brand = new Brand();
            brand.Name = request.Name;
            brand.Description = request.Description;

            //todo
            db.Brands.Add(brand);
            await db.SaveChangesAsync(cancellationToken);
            return brand;
        }
    }
}
