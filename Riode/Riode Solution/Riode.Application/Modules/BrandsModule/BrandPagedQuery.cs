using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using Riode.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.BrandsModule
{
    public class BrandPagedQuery:IRequest<PagedViewModel<Brand>>
    {
        int pageIndex;
        int pageSize;
        public int PageIndex {
            get {
                if (pageIndex > 0)
                {
                    return pageIndex;
                }
                return 1;
            }
            set {
                if (value > 0)
                {
                    pageIndex = value;
                }
                else
                {
                    pageIndex = 1;
                }
            }
        }
        public int PageSize
        {
            get
            {
                if (pageSize > 0)
                {
                    return pageSize;
                }
                return 15;
            }
            set
            {
                if (value > 0)
                {
                    pageSize = value;
                }
                else
                {
                    pageSize = 1;
                }
            }
        }

        public class BrandPagedQueryHandler : IRequestHandler<BrandPagedQuery, PagedViewModel<Brand>>
        {
            readonly RiodeDbContext db;
            public BrandPagedQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Brand>> Handle(BrandPagedQuery request, CancellationToken cancellationToken)
            {
                var query = db.Brands.Where(s => s.DeletedByUserId == null).AsQueryable();
                //int recordCount =await query.CountAsync();
                //var pagedData =await query.Skip((request.PageIndex - 1) * request.PageSize)
                //                    .Take(request.PageSize)
                //                    .ToListAsync(cancellationToken);


                return new PagedViewModel<Brand>(query,request.PageIndex,request.PageSize);
            }
        }
    }
}
