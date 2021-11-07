using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.CategoriesModule
{
    public class CategoryChooseQuery :IRequest<List<Category>>
    {
        public class CategoryChooseQueryHandler: IRequestHandler<CategoryChooseQuery, List<Category>>
        {
            readonly RiodeDbContext db;
            public CategoryChooseQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }

            public async Task<List<Category>> Handle(CategoryChooseQuery request, CancellationToken cancellationToken)
            {
                var categories = await db.Categories.Include(c=> c.Children)
                                    .ThenInclude(c => c.Children)
                                    .Where(c=>c.DeletedByUserId == null && c.ParentId == null).ToListAsync();
                return categories;
            }
        }
    }
}
