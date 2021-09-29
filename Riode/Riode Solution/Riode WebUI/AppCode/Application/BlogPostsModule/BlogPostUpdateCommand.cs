using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.BlogPostsModule
{
    public class BlogPostUpdateCommand : BlogPostViewModel, IRequest<long>
    {

        public class BrandUpdateCommandHandler : IRequestHandler<BlogPostUpdateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IWebHostEnvironment env;
            public BrandUpdateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx, IWebHostEnvironment env)
            {
                this.env = env;
                this.ctx = ctx;
                this.db = db;
            }

            public async Task<long> Handle(BlogPostUpdateCommand request, CancellationToken cancellationToken)
            {
                


                return 0;
            }
        }
    }
}
