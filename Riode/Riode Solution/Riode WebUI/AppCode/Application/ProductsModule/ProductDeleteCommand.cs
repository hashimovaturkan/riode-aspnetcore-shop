using MediatR;
using Riode_WebUI.AppCode.Infrastructure;
using Riode_WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.ProductsModule
{
    public class ProductDeleteCommand : IRequest<CommandJsonResponse>
    {
        public long? Id { get; set; }

        public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, CommandJsonResponse>
        {
            readonly RiodeDbContext db;
            public ProductDeleteCommandHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<CommandJsonResponse> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();

                if (request.Id <= 0 || request.Id == null)
                {
                    response.Error = true;
                    response.Message = "The information is incomplete!";
                    goto end;
                }

                var product = db.Products.FirstOrDefault(b => b.Id == request.Id && b.DeletedByUserId == null);

                if (product == null)
                {
                    response.Error = true;
                    response.Message = "There is no data!";
                    goto end;
                }

                product.DeletedDate = DateTime.Now;
                product.DeletedByUserId = 1;
                response.Error = false;
                response.Message = "Successfully operation!";

                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
