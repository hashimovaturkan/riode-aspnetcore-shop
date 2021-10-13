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
    public class ProductChooseSpecificationCommand : IRequest<ChooseSpecificationJsonResponse>
    {
        public long? defaultFormName { get; set; }

        public class ProductChooseSpecificationCommandHandler : IRequestHandler<ProductChooseSpecificationCommand, ChooseSpecificationJsonResponse>
        {
            readonly RiodeDbContext db;
            public ProductChooseSpecificationCommandHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<ChooseSpecificationJsonResponse> Handle(ProductChooseSpecificationCommand request, CancellationToken cancellationToken)
            {
                var specification = db.Specifications.FirstOrDefault(s => s.Id == request.defaultFormName && s.DeletedByUserId == null);
                var response = new ChooseSpecificationJsonResponse();
                if (specification == null)
                {
                    response.Error = true;
                    return response;
                }


                response.Id = specification.Id;
                response.Name = specification.Name;
                response.Error = false;
                return response; 
            }
        }
    }
}
