using MediatR;
using Riode.Application.Core.Infrastructure;
using Riode.Domain.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.ColorsModule
{
    public class ColorDeleteCommand:IRequest<CommandJsonResponse>
    {
        public long? Id { get; set; }

        public class ColorDeleteCommandHandler : IRequestHandler<ColorDeleteCommand, CommandJsonResponse>
        {
            readonly RiodeDbContext db;
            public ColorDeleteCommandHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<CommandJsonResponse> Handle(ColorDeleteCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();


                if (request.Id <= 0 || request.Id == null)
                {
                    response.Error = true;
                    response.Message = "The information is incomplete!";
                    goto end;
                }

                var color = db.Colors.FirstOrDefault(b => b.Id == request.Id && b.DeletedByUserId == null);

                if (color == null)
                {
                    response.Error = true;
                    response.Message = "There is no data!";
                    goto end;
                }

                color.DeletedDate = DateTime.Now;
                color.DeletedByUserId = 1;
                response.Error = false;
                response.Message = "Successfully operation!";

                await db.SaveChangesAsync(cancellationToken);

                end:
                    return response;
            }
        }
    }
}
