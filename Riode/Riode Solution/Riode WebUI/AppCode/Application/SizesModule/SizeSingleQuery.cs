using MediatR;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.SizesModule
{
    public class SizeSingleQuery:IRequest<Size>
    {
        public long? Id { get; set; }

        public class SizeSingleQueryHandler : IRequestHandler<SizeSingleQuery, Size>
        {
            readonly RiodeDbContext db;
            public SizeSingleQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<Size> Handle(SizeSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null && request.Id <= 0)
                    return null;

                var size = db.Sizes.FirstOrDefault(s=>s.Id == request.Id && s.DeletedDate==null);

                return size;
            }
        }
    }
}
