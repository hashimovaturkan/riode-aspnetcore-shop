using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.Application.Core.Extensions;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.FaqsModule
{
    public class FaqCreateCommand:IRequest<long>
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public class FaqCreateCommandHandler : IRequestHandler<FaqCreateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            public FaqCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
            }
            public async Task<long> Handle(FaqCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsModelStateValid())
                {
                    var faq =new Faq();
                    faq.Answer = request.Answer;
                    faq.Question = request.Question;

                    await db.Faqs.AddAsync(faq);
                    await db.SaveChangesAsync(cancellationToken);

                    return faq.Id;
                }
                return 0;
            }
        }
    }
}
