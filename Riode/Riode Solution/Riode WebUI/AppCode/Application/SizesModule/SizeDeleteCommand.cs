using MediatR;
using Riode_WebUI.AppCode.Infrastructure;
using Riode_WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.SizesModule
{
    public class SizeDeleteCommand:IRequest<CommandJsonResponse>
    {
        public long? Id { get; set; }

        public class SizeDeleteCommandHandler : IRequestHandler<SizeDeleteCommand, CommandJsonResponse>
        {
            readonly RiodeDbContext db;
            public SizeDeleteCommandHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<CommandJsonResponse> Handle(SizeDeleteCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();

                if(request.Id ==null && request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "The information is incomplete!";
                    return response;
                }

                var size = db.Sizes.FirstOrDefault(s => s.Id == request.Id && s.DeletedByUserId == null);
                if (size == null)
                {
                    response.Error = true;
                    response.Message = "There is no data!";
                    return response;
                }

                size.DeletedByUserId = 1;
                size.DeletedDate = DateTime.Now; 
                response.Error = false;
                response.Message = "Successfully operation!";

                await db.SaveChangesAsync(cancellationToken);

                return response;

            }
        }
    }
}
