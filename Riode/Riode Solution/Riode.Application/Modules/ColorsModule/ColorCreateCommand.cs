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

namespace Riode.Application.Modules.ColorsModule
{
    public class ColorCreateCommand:IRequest<long>
    {
        public string HexCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class ColorCreateCommandHandler : IRequestHandler<ColorCreateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            public ColorCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                this.ctx = ctx;
                this.db = db;
            }
            public async Task<long> Handle(ColorCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsModelStateValid())
                {
                    var color = new Color();
                    color.Name = request.Name;
                    color.HexCode = request.HexCode;
                    color.Description = request.Description;

                    await db.Colors.AddAsync(color, cancellationToken);
                    await db.SaveChangesAsync();

                    return color.Id;
                }
                return 0;
            }
        }
    }
}
