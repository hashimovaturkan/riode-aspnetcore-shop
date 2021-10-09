using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.AppCode.Infrastructure;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.CategoriesModule
{
    public class CategoryDeleteCommand : IRequest<CommandJsonResponse>
    {
        public long? Id { get; set; }

        public class CategoryDeleteCommandHandler : IRequestHandler<CategoryDeleteCommand, CommandJsonResponse>
        {
            readonly RiodeDbContext db;
            public CategoryDeleteCommandHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<CommandJsonResponse> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();


                //sehvdi bu? neleri yoxlayag?
                if (request.Id <= 0 || request.Id == null)
                {
                    response.Error = true;
                    response.Message = "The information is incomplete!";
                    goto end;
                }

                //var category = db.Categories.FirstOrDefault(b => b.Id == request.Id && b.DeletedByUserId == null);

                var category =await db.Categories
                    .Include(c => c.Children)
                    .ThenInclude(c => c.Children)
                    .Where(b => b.Id == request.Id && b.DeletedByUserId == null)
                    .ToListAsync();


                if (category == null)
                {
                    response.Error = true;
                    response.Message = "There is no data!";
                    goto end;
                }

                foreach (var item in category)
                {
                    item.DeletedDate = DateTime.Now;
                    item.DeletedByUserId = 1;
                }

                //category.DeletedDate = DateTime.Now;
                //category.DeletedByUserId = 1;
                response.Error = false;
                response.Message = "Successfully operation!";

                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
