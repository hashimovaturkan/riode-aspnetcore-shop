using MediatR;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using Riode.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.ColorsModule
{
    public class ColorPagedQuery:IRequest<PagedViewModel<Color>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public class ColorPagedQueryHandler : IRequestHandler<ColorPagedQuery, PagedViewModel<Color>>
        {
            readonly RiodeDbContext db;
            public ColorPagedQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Color>> Handle(ColorPagedQuery request, CancellationToken cancellationToken)
            {
                var query = db.Colors.Where(s => s.DeletedByUserId == null).AsQueryable();


                return new PagedViewModel<Color>(query, request.PageIndex, request.PageSize);
            }
        }
    }
}
