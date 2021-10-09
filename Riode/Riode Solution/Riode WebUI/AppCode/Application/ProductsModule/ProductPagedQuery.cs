using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using Riode_WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.ProductsModule
{
    public class ProductPagedQuery : IRequest<PagedViewModel<Product>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public class ProductPagedQueryHandler : IRequestHandler<ProductPagedQuery, PagedViewModel<Product>>
        {
            readonly RiodeDbContext db;
            public ProductPagedQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Product>> Handle(ProductPagedQuery request, CancellationToken cancellationToken)
            {
                var query = db.Products
                            .Include(p => p.Brand)
                            .Include(p => p.Category)
                            .Include(p => p.Images.Where(i => i.DeletedDate == null))
                            .Include(p => p.ProductSizeColorCollections)
                            .Where(s => s.DeletedByUserId == null).AsQueryable();
                
                return new PagedViewModel<Product>(query, request.PageIndex, request.PageSize);
            }
        }
    }
}
