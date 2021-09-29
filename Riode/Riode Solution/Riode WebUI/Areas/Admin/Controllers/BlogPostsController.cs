using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.AppCode.Application.BlogPostsModule;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class BlogPostsController : Controller
    {
        readonly RiodeDbContext db;
        readonly IWebHostEnvironment env;
        readonly IMediator mediator;

        public BlogPostsController(RiodeDbContext db,IWebHostEnvironment env, IMediator mediator)
        {
            this.mediator = mediator;
            this.db = db;
            this.env = env;
        }

        [Authorize(Policy = "admin.blogpost.index")]
        public async Task<IActionResult> Index(BlogPostPagedQuery query)
        {
            var response = await mediator.Send(query);

            return View(response);
        }



        [Authorize(Policy = "admin.blogpost.details")]
        public async Task<IActionResult> Details(BlogPostSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            ViewData["CategorytId"] = db.Categories.Select(s => s.Id == response.CategoryId);
            //ViewData["CategorytId"] = new SelectList(db.Categories, "Id", "Name");
            return View(response);
        }

        [Authorize(Policy = "admin.blogpost.create")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.blogpost.create")]
        public async Task<IActionResult> Create(BlogPostCreateCommand command)
        {
            var response = await mediator.Send(command);

            if (response == -1)
                ModelState.AddModelError("file", "There is not image");

            if (response > 0)
                return RedirectToAction(nameof(Index));

            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", command.CategoryId);
            return View(command);
            //if (file == null)
            //{
            //    ModelState.AddModelError("file", "There is not image");
            //}

            //if (ModelState.IsValid)
            //{
            //    string extension = Path.GetExtension(file.FileName);
            //    blogPost.ImageUrl = $"{Guid.NewGuid()}{extension}";

            //    string physicalFileName = Path.Combine(env.ContentRootPath,
            //                                           "wwwroot",
            //                                           "uploads",
            //                                           "images",
            //                                           "blog",
            //                                           "mask",
            //                                           blogPost.ImageUrl);

            //    using (var stream = new FileStream(physicalFileName, FileMode.Create, FileAccess.Write))
            //    {
            //        await file.CopyToAsync(stream);
            //    }

            //    db.Add(blogPost);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", blogPost.CategoryId);
            //return View(blogPost);
        }

        [Authorize(Policy = "admin.blogpost.edit")]
        public async Task<IActionResult> Edit(BlogPostSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", response.CategoryId);
            return View(response);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.blogpost.edit")]
        public async Task<IActionResult> Edit(long id, BlogPost blogPost, IFormFile file, string fileTemp)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(fileTemp) && file==null)
            {
                ModelState.AddModelError("file", "Image was added!");
            }

           

            if (ModelState.IsValid)
            {
                try
                {
                    //db.Update(blogPost);

                    var entity = await db.BlogPosts.FirstOrDefaultAsync(b=> b.Id == id && b.DeletedByUserId == null);

                    if(entity == null)
                    {
                        return NotFound();
                    }

                    entity.Title = blogPost.Title;
                    entity.Content = blogPost.Content;
                    entity.CategoryId = blogPost.CategoryId;

                    if (file != null)
                    {
                        string extension = Path.GetExtension(file.FileName);
                        blogPost.ImageUrl = $"{Guid.NewGuid()}{extension}";

                        string physicalFileName = Path.Combine(env.ContentRootPath,
                                                               "wwwroot",
                                                               "uploads",
                                                               "images",
                                                               "blog",
                                                               "mask",
                                                               blogPost.ImageUrl);

                        using (var stream = new FileStream(physicalFileName, FileMode.Create, FileAccess.Write))
                        {
                            await file.CopyToAsync(stream);
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

                        entity.ImageUrl = blogPost.ImageUrl;
                        
                    }


                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "Name", "Id", blogPost.CategoryId);
            return View(blogPost);
        }

        
        [HttpPost]
        [Authorize(Policy = "admin.blogpost.delete")]
        public async Task<IActionResult> Delete(BlogPostDeleteCommand command)
        {
            var result = await mediator.Send(command);
            return Json(result);
        }

        private bool BlogPostExists(long id)
        {
            return db.BlogPosts.Any(e => e.Id == id && e.DeletedByUserId == null);
        }
    }
}
