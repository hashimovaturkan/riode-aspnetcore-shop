using MediatR;
using Riode_WebUI.AppCode.Infrastructure;
using Riode_WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.BlogPostsModule
{
    public class BlogPostDeleteCommand : IRequest<CommandJsonResponse>
    {
        public long? Id { get; set; }

        public class BlogPostDeleteCommandHandler : IRequestHandler<BlogPostDeleteCommand, CommandJsonResponse>
        {
            readonly RiodeDbContext db;
            public BlogPostDeleteCommandHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<CommandJsonResponse> Handle(BlogPostDeleteCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();


                //sehvdi bu? neleri yoxlayag?
                if (request.Id <= 0 || request.Id == null)
                {
                    response.Error = true;
                    response.Message = "The information is incomplete!";
                    goto end;
                }

                var blog = db.BlogPosts.FirstOrDefault(b => b.Id == request.Id && b.DeletedByUserId == null);

                if (blog == null)
                {
                    response.Error = true;
                    response.Message = "There is no data!";
                    goto end;
                }

                blog.DeletedDate = DateTime.Now;
                blog.DeletedByUserId = 1;
                response.Error = false;
                response.Message = "Successfully operation!";

                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
