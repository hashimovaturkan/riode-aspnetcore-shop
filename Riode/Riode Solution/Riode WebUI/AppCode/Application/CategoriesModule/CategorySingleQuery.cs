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
    public class CategorySingleQuery : IRequest<Category>
    {
        public long? Id { get; set; }

        public class CategorySingleQueryHandler : IRequestHandler<CategorySingleQuery, Category>
        {
            readonly RiodeDbContext db;
            public CategorySingleQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<Category> Handle(CategorySingleQuery request, CancellationToken cancellationToken)
            {
                //sual: burda null olub olmasini nie yoxlamirig <=0 eliyirik
                if (request.Id <= 0 || request.Id == null)
                {
                    return null;
                }

                var category = await db.Categories
                    .Include(c => c.Parent)
                    .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedByUserId == null, cancellationToken);

                return category;
            }
        }
    }
}
