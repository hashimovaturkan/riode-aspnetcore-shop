using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.CategoriesModule
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
                var categories = await db.Categories.Where(c=>c.DeletedByUserId == null).ToListAsync();
                return categories;
            }
        }
    }
}
