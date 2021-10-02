using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.AppCode.Extensions;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Application.BlogPostsModule
{
    public class BlogPostUpdateCommand : BlogPostViewModel, IRequest<BlogPost>
    {

        public class BrandUpdateCommandHandler : IRequestHandler<BlogPostUpdateCommand, BlogPost>
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

            public async Task<BlogPost> Handle(BlogPostUpdateCommand request, CancellationToken cancellationToken)
            {
                if (request.Id != request.BlogPost.Id 
                    && request.Id<=0 
                    && request.Id ==null)
                {
                    return null;
                }

                if (string.IsNullOrWhiteSpace(request.fileTemp) && request.file == null)
                {
                    ctx.ActionContext.ModelState.AddModelError("file", "Image was added!");
                }

                if (ctx.IsModelStateValid())
                {
                        var entity = await db.BlogPosts.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedByUserId == null);

                        if (entity == null)
                        {
                            return null;
                        }

                        entity.Title = request.BlogPost.Title;
                        entity.Content = request.BlogPost.Content;
                        entity.CategoryId = request.BlogPost.CategoryId;

                        if (request.file != null)
                        {
                            string extension = Path.GetExtension(request.file.FileName);
                            request.BlogPost.ImageUrl = $"{Guid.NewGuid()}{extension}";

                            string physicalFileName = Path.Combine(env.ContentRootPath,
                                                                   "wwwroot",
                                                                   "uploads",
                                                                   "images",
                                                                   "blog",
                                                                   "mask",
                                                                   request.BlogPost.ImageUrl);

                            using (var stream = new FileStream(physicalFileName, FileMode.Create, FileAccess.Write))
                            {
                                await request.file.CopyToAsync(stream);
                            }

                            if (!string.IsNullOrWhiteSpace(entity.ImageUrl))
                            {
                                System.IO.File.Delete(Path.Combine(env.ContentRootPath,
                                                                  "wwwroot",
                                                                  "uploads",
                                                                  "images",
                                                                  "blog",
                                                                  "mask",
                                                                  entity.ImageUrl));
                            }

                            entity.ImageUrl = request.BlogPost.ImageUrl;

                        }

                        await db.SaveChangesAsync();
                        return entity;
                    
                }
                return null;

            }
        }
    }
}
