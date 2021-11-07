using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities;
using Riode.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        readonly RiodeDbContext db;
        public BlogController(RiodeDbContext db)
        {
            this.db = db;
        }

        //[Authorize(Policy = "blog.details")]
        public IActionResult Details(long id)
        {
            var datas =new CategoryBlogPostViewModel();

            datas.Categories = db.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .ThenInclude(c => c.Children)
                .Where(c => c.ParentId == null && c.DeletedByUserId == null)
                .ToList();


            datas.BlogPost = db.BlogPosts
                .Include(c=>c.Comments)
                .ThenInclude(c=> c.CreatedByUser)
                .Include(c => c.Comments)
                .ThenInclude(c => c.Children)
                .FirstOrDefault(s => s.DeletedByUserId == null && s.Id == id && s.PublishedDate !=null);

            if (datas.Categories == null || datas.BlogPost == null)
            {
                return NotFound();
            }

            return View(datas);
        }

        //[Authorize(Policy = "blog.index")]
        public IActionResult Index()
        {
            var datas = db.BlogPosts
                .Where(b => b.DeletedByUserId == null)
                .ToList();
            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(long? commentId, long postId, string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                return Json(new
                {
                    error=true,
                    message = "Serh bos buraxila bilmez!"
                });
            }

            if (postId<1)
            {
                return Json(new
                {
                    error = true,
                    message = "Post movcud deyil!"
                });
            }


            var post = await db.BlogPosts.FirstOrDefaultAsync(c => c.Id == postId);


            if (post== null)
            {
                return Json(new
                {
                    error = true,
                    message = "Post movcud deyil!"
                });
            }

            var commentModel = new BlogPostComment
            {
                ParentId = commentId,
                BlogPostId = postId,
                Comment = comment
                //,CreatedByUserId= User.GetCurrentUserId()
            };
            if (commentId.HasValue && await db.BlogPostComments.AnyAsync(c => c.Id == commentId))
                commentModel.ParentId = commentId;

            await db.BlogPostComments.AddAsync(commentModel);
            await db.SaveChangesAsync();



            //return Json(new
            //{
            //    error = false,
            //    message = "Elave edildi!"
            //});
            commentModel = await db.BlogPostComments
                .Include(c => c.CreatedByUserId)
                .Include(c=>c.Parent)
                .FirstOrDefaultAsync(c => c.Id == commentModel.Id);

            return PartialView("_Comment",commentModel);
        }
    }
}
