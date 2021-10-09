﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.FaqsModule
{
    public class FaqSingleQuery:IRequest<Faq>
    {
        public long? Id { get; set; }

        public class FaqSingleQueryHandler : IRequestHandler<FaqSingleQuery, Faq>
        {
            readonly RiodeDbContext db;
            public FaqSingleQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<Faq> Handle(FaqSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id <= 0)
                    return null;

                var faq = await db.Faqs.FirstOrDefaultAsync(c => c.Id == request.Id && c.DeletedByUserId == null, cancellationToken);

                return faq;
            }
        }
    }
}
