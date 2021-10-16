using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.BlogPostsModule
{
    public class BlogPostSingleQuery : IRequest<BlogPost>
    {
        public long? Id { get; set; }

        public class BlogPostSingleQueryHandler : IRequestHandler<BlogPostSingleQuery, BlogPost>
        {
            readonly RiodeDbContext db;
            public BlogPostSingleQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }

            public async Task<BlogPost> Handle(BlogPostSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0 || request.Id == null)
                {
                    return null;
                }

                var blog = await db.BlogPosts
                    .Include(b => b.Category)
                    .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedByUserId == null, cancellationToken);

                return blog;
            }
        }
    }
}

