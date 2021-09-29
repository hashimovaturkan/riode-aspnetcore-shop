using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode_WebUI.AppCode.Extensions;
using Riode_WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.SizesModule
{
    public class SizeUpdateCommand:SizeModelView,IRequest<long>
    {

        public class SizeUpdateCommandHandler : IRequestHandler<SizeUpdateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            public SizeUpdateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                this.ctx = ctx;
                this.db = db;
            }
            public async Task<long> Handle(SizeUpdateCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id <= 0)
                    return 0;

                var size = db.Sizes.FirstOrDefault(s => s.Id == request.Id && s.DeletedByUserId == null);
                if (size == null)
                    return 0;

                if (ctx.IsModelStateValid())
                {
                    size.Name = request.Name;
                    size.Description = request.Description;
                    size.Abbr = request.Abbr;

                    await db.SaveChangesAsync(cancellationToken);

                    return size.Id;
                }
                return 0;
            }
        }
    }
}
