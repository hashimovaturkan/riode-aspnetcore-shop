using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Riode.Application.Core.Extensions;
using Riode.Domain.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.CategoriesModule
{
    public class CategoryUpdateCommand : CategoryViewModel, IRequest<long>
    {

        public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            public CategoryUpdateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                this.ctx = ctx;
                this.db = db;
            }

            public async Task<long> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id < 0)
                    return 0;

                var category = await db.Categories.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedByUserId == null);

                if (category == null)
                {
                    return 0;
                }

                if (ctx.IsModelStateValid())
                {
                    category.Name = request.Name;
                    category.Description = request.Description;
                    category.ParentId = request.ParentId;


                    await db.SaveChangesAsync(cancellationToken);

                    return category.Id;
                }
                return 0;

            }
        }
    }
}
