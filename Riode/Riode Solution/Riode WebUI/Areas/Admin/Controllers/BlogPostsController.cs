using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        readonly RiodeDbContext db;
        readonly IWebHostEnvironment env;

        public BlogPostsController(RiodeDbContext db,IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        // GET: Admin/BlogPosts
        public async Task<IActionResult> Index(int page=1)
        {
            int take = 5;

            ViewBag.PageCount = Decimal.Ceiling((decimal)db.BlogPosts.Where(b => b.DeletedByUserId == null).Count() / take);
            var riodeDbContext = db.BlogPosts.Where(b=>b.DeletedByUserId == null)
                                            .Include(b => b.Category)
                                            .OrderByDescending(b=>b.Id)
                                            .Skip((page-1)*take)
                                            .Take(take);
            return View(await riodeDbContext.ToListAsync());
        }



        // GET: Admin/BlogPosts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await db.BlogPosts
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedByUserId == null);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: Admin/BlogPosts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost blogPost, IFormFile file)
        {

            if (file == null)
            {
                ModelState.AddModelError("file", "There is not image");
            }

            if (ModelState.IsValid)
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

                using (var stream=new FileStream(physicalFileName, FileMode.Create,FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }

                db.Add(blogPost);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", blogPost.CategoryId);
            return View(blogPost);
        }

        // GET: Admin/BlogPosts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await db.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", blogPost.CategoryId);
            return View(blogPost);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Admin/BlogPosts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await db.BlogPosts
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedByUserId == null);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: Admin/BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var blogPost = await db.BlogPosts.FindAsync(id);
            db.BlogPosts.Remove(blogPost);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(long id)
        {
            return db.BlogPosts.Any(e => e.Id == id && e.DeletedByUserId == null);
        }
    }
}
