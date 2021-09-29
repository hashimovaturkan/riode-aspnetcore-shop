using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode_WebUI.AppCode.Application.BlogPostsModule;
using Riode_WebUI.AppCode.Extensions;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.BlogPostsModule
{
    public class BlogPostCreateCommand : IRequest<long>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public IFormFile file { get; set; }
        public long? CategoryId { get; set; }

        public class BlogPostCreateCommandHandler : IRequestHandler<BlogPostCreateCommand, long>
        {
            readonly RiodeDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IWebHostEnvironment env;
            public BlogPostCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx,IWebHostEnvironment env)
            {
                this.env = env;
                this.ctx = ctx;
                this.db = db;
            }
            public async Task<long> Handle(BlogPostCreateCommand request, CancellationToken cancellationToken)
            {
                if (request.file == null)
                {
                    return -1;
                }

                if (ctx.IsModelStateValid())
                {
                    var blog = new BlogPost();
                    blog.Title = request.Title;
                    blog.Content = request.Content;
                    blog.CategoryId = request.CategoryId;
                    blog.PublishedDate = request.PublishedDate;

                    string extension = Path.GetExtension(request.file.FileName);
                    blog.ImageUrl = $"{Guid.NewGuid()}{extension}";

                    string physicalFileName = Path.Combine(env.ContentRootPath,
                                                           "wwwroot",
                                                           "uploads",
                                                           "images",
                                                           "blog",
                                                           "mask",
                                                           blog.ImageUrl);

                    using (var stream = new FileStream(physicalFileName, FileMode.Create, FileAccess.Write))
                    {
                        await request.file.CopyToAsync(stream);
                    }

                    db.Add(blog);
                    await db.SaveChangesAsync();

                    return blog.Id;
                }

                return 0;

            }
        }
    }
}

  

