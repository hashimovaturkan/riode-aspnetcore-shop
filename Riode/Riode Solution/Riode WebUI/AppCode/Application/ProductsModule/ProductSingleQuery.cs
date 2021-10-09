using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.ProductsModule
{
    public class ProductSingleQuery : IRequest<Product>
    {
        public long? Id { get; set; }

        public class ProductSingleQueryHandler : IRequestHandler<ProductSingleQuery, Product>
        {
            readonly RiodeDbContext db;
            public ProductSingleQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<Product> Handle(ProductSingleQuery request, CancellationToken cancellationToken)
            {
                //sual: burda null olub olmasini nie yoxlamirig <=0 eliyirik
                if (request.Id <= 0 || request.Id == null)
                {
                    return null;
                }

                var product = await db.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.Images.Where(i => i.DeletedDate == null))
                    .Include(p => p.SpecificationValues.Where(s => s.DeletedByUserId == null))
                    .ThenInclude(s => s.Specification)
                    .Include(p => p.ProductSizeColorCollections.Where(s => s.ProductId == request.Id))
                    .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedByUserId == null, cancellationToken);

                return product;
            }
        }
    }
}
