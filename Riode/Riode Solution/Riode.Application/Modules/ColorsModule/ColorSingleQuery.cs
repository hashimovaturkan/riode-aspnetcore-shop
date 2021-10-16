using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.ColorsModule
{
    public class ColorSingleQuery:IRequest<Color>
    {
        public long? Id { get; set; }

        public class ColorSingleQueryHandler : IRequestHandler<ColorSingleQuery, Color>
        {
            readonly RiodeDbContext db;
            public ColorSingleQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<Color> Handle(ColorSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id <= 0)
                    return null;

                var color =await db.Colors.FirstOrDefaultAsync(c=>c.Id == request.Id && c.DeletedByUserId==null,cancellationToken);

                return color;
            }
        }
    }
}
