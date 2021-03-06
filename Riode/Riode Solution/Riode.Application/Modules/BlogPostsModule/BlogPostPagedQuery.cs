using MediatR;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using Riode.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Application.Modules.BlogPostsModule
{
    public class BlogPostPagedQuery : IRequest<PagedViewModel<BlogPost>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 15;

        public class BlogPostPagedQueryHandler : IRequestHandler<BlogPostPagedQuery, PagedViewModel<BlogPost>>
        {
            readonly RiodeDbContext db;
            public BlogPostPagedQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<BlogPost>> Handle(BlogPostPagedQuery request, CancellationToken cancellationToken)
            {
                var query = db.BlogPosts.Where(s => s.DeletedByUserId == null && s.PublishedDate != null).AsQueryable();
                

                return new PagedViewModel<BlogPost>(query, request.PageIndex, request.PageSize);
            }
        }
    }
}
