using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode_WebUI.AppCode.Extensions;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.SpecificationsModule
{
    public class SpecificationCreateCommand : IRequest<long>
    {
        [Required]
        public string Name { get; set; }
        public ICollection<SpecificationCategoryItem> SpecificationCategoryItems { get; set; }

        public class SpecificationCreateCommandHandler : IRequestHandler<SpecificationCreateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            public SpecificationCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                this.ctx = ctx;
                this.db = db;
            }
            public async Task<long> Handle(SpecificationCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsModelStateValid())
                {
                    var brand = new Brand();
                    brand.Name = request.Name;
                    //brand.Description = request.Description;

                    //todo
                    db.Brands.Add(brand);
                    await db.SaveChangesAsync(cancellationToken);
                    return brand.Id;
                }

                return 0;

            }
        }
    }
}
