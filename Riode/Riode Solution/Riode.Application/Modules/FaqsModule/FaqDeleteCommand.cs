using MediatR;
using Riode.Application.Core.Infrastructure;
using Riode.Domain.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.FaqsModule
{
    public class FaqDeleteCommand:IRequest<CommandJsonResponse>
    {
        public long? Id { get; set; }

        public class FaqDeleteCommandHandler : IRequestHandler<FaqDeleteCommand, CommandJsonResponse>
        {
            readonly RiodeDbContext db;
            public FaqDeleteCommandHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<CommandJsonResponse> Handle(FaqDeleteCommand request, CancellationToken cancellationToken)
            {
                if (request.Id <=0 || request.Id == null)
                {
                    return new CommandJsonResponse() {
                        Error = true,
                        Message = "The information is incomplete!"
                    };

                }
                var faq =db.Faqs.FirstOrDefault(m=> m.Id == request.Id && m.DeletedByUserId == null);

                if(faq == null)
                {
                    return new CommandJsonResponse()
                    {
                        Error = true,
                        Message = "There is no data!"
                    };
                }

                faq.DeletedByUserId = 1;
                faq.DeletedDate = DateTime.Now;
                await db.SaveChangesAsync(cancellationToken);

                return new CommandJsonResponse()
                {
                    Error = false,
                    Message = "Successfully operation!"
                };

            }
        }
    }
}
