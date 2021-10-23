using MediatR;
using Riode.Application.Core.Infrastructure;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.BrandsModule
{
    public class BrandDeleteCommand:IRequest<CommandJsonResponse>
    {
        public long? Id { get; set; }

        public BrandDeleteCommand()
        {

        }

        public BrandDeleteCommand(long? id)
        {
            id = Id;
        }

        public class BrandDeleteCommandHandler : IRequestHandler<BrandDeleteCommand, CommandJsonResponse>
        {
            readonly RiodeDbContext db;
            public BrandDeleteCommandHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<CommandJsonResponse> Handle(BrandDeleteCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();


                //sehvdi bu? neleri yoxlayag?
                if (request.Id <= 0 || request.Id == null)
                {
                    response.Error = true;
                    response.Message = "The information is incomplete!";
                    goto end;
                }

                var brand = db.Brands.FirstOrDefault(b => b.Id == request.Id && b.DeletedByUserId == null);

                if (brand == null)
                {
                    response.Error = true;
                    response.Message = "There is no data!";
                    goto end;
                }

                brand.DeletedDate = DateTime.Now;
                brand.DeletedByUserId = 1;
                response.Error = false;
                response.Message = "Successfully operation!";

                await db.SaveChangesAsync(cancellationToken);

                end:
                return response;
            }
        }
    }
}
