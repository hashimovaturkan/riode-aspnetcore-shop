using MediatR;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using Riode.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.FaqsModule
{
    public class FaqPagedQuery:IRequest<PagedViewModel<Faq>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public class FaqPagedQueryHandler : IRequestHandler<FaqPagedQuery, PagedViewModel<Faq>>
        {
            readonly RiodeDbContext db;
            public FaqPagedQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Faq>> Handle(FaqPagedQuery request, CancellationToken cancellationToken)
            {
                var query = db.Faqs.Where(s => s.DeletedByUserId == null).AsQueryable();

                return new PagedViewModel<Faq>(query, request.PageIndex, request.PageSize);
            }
        }

    }
}
