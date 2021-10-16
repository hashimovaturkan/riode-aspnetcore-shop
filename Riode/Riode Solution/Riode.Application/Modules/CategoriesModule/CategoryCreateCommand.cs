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

namespace Riode.Application.Modules.CategoriesModule
{
    public class CategoryCreateCommand : IRequest<long>
    {
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Description { get; set; }
    }

    public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, long>
    {
        readonly RiodeDbContext db;
        readonly IActionContextAccessor ctx;
        public CategoryCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
        {
            this.ctx = ctx;
            this.db = db;
        }
        public async Task<long> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
        {
            if (ctx.IsModelStateValid())
            {
                var category = new Category();
                category.Name = request.Name;
                category.Description = request.Description;
                category.ParentId = request.ParentId;

                //todo
                db.Categories.Add(category);
                await db.SaveChangesAsync(cancellationToken);
                return category.Id;
            }

            return 0;

        }
    }
}
