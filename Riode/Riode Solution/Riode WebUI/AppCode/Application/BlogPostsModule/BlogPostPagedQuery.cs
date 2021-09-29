using MediatR;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using Riode_WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.BlogPostsModule
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
                var query = db.BlogPosts.Where(s => s.DeletedByUserId == null).AsQueryable();
                

                return new PagedViewModel<BlogPost>(query, request.PageIndex, request.PageSize);
            }
        }
    }
}
