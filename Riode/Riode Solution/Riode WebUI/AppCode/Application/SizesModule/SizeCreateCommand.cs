using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode_WebUI.AppCode.Extensions;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.SizesModule
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
