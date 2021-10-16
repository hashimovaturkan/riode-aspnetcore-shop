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

namespace Riode.Application.Modules.SizesModule
{
    public class SizeCreateCommand:IRequest<long>
    {
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class SizeCreateCommandHandler : IRequestHandler<SizeCreateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            public SizeCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                this.ctx = ctx;
                this.db = db;
            }
            public async Task<long> Handle(SizeCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsModelStateValid())
                {
                    var size = new Size();
                    size.Abbr = request.Abbr;
                    size.Name = request.Name;
                    size.Description = request.Description;

                    db.Sizes.Add(size);
                    await db.SaveChangesAsync();

                    return size.Id;

                }

                return 0;
            }
        }
    }
}
