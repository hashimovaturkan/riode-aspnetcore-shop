using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode_WebUI.AppCode.Extensions;
using Riode_WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.ColorsModule
{
    public class ColorUpdateCommand:ColorViewModel,IRequest<long>
    {
        public class ColorUpdateCommandHandler : IRequestHandler<ColorUpdateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            public ColorUpdateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                this.ctx = ctx;
                this.db = db;
            }
            public async Task<long> Handle(ColorUpdateCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id <= 0)
                    return 0;

                var color = db.Colors.FirstOrDefault(c => c.Id == request.Id && c.DeletedByUserId == null);
                if(color == null)
                {
                    return 0;
                }

                if (ctx.IsModelStateValid())
                {
                    color.Name = request.Name;
                    color.Description = request.Description;
                    color.HexCode = request.HexCode;

                    await db.SaveChangesAsync(cancellationToken);

                    return color.Id;

                }

                return 0;

            }
        }
    }
}
