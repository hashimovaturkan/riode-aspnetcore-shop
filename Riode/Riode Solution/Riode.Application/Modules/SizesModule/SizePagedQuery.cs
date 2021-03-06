using MediatR;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using Riode.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.SizesModule
{
    public class SizePagedQuery:IRequest<PagedViewModel<Size>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public class SizePagedQueryHandler : IRequestHandler<SizePagedQuery, PagedViewModel<Size>>
        {
            readonly RiodeDbContext db;
            public SizePagedQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Size>> Handle(SizePagedQuery request, CancellationToken cancellationToken)
            {
                var sizes = db.Sizes.Where(s => s.DeletedByUserId == null).AsQueryable();

                return new PagedViewModel<Size>(sizes, request.PageIndex, request.PageSize);
            }
        }
    }
}
