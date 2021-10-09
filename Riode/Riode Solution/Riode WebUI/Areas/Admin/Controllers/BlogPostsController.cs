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
using Riode_WebUI.AppCode.Application.BrandsModule;
using Riode_WebUI.AppCode.Application.CategoriesModule;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AllowAnonymous]
    public class BlogPostsController : Controller
    {
        
        readonly IWebHostEnvironment env;
        readonly IMediator mediator;

        public BlogPostsController(IWebHostEnvironment env, IMediator mediator)
        {
            this.mediator = mediator;
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

            var category = await mediator.Send(new CategoryChooseQuery());
            ViewData["CategoryId"] = category.Where(b => b.Id == response.CategoryId).FirstOrDefault(b => b.DeletedByUserId == null).Name;
            return View(response);
        }

        [Authorize(Policy = "admin.blogpost.create")]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await mediator.Send(new CategoryChooseQuery()), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.blogpost.create")]
        public async Task<IActionResult> Create(BlogPostCreateCommand command)
        {
            var response = await mediator.Send(command);

            if (response != null)
                return RedirectToAction(nameof(Index));

            ViewData["CategoryId"] = new SelectList(await mediator.Send(new CategoryChooseQuery()), "Id", "Name", command.CategoryId);

            return View(command);
            
        }

        [Authorize(Policy = "admin.blogpost.edit")]
        public async Task<IActionResult> Edit(BlogPostSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            var blogPost = new BlogPost();
            blogPost.Id = response.Id;
            blogPost.ImageUrl = response.ImageUrl;
            blogPost.PublishedDate = response.PublishedDate;
            blogPost.Title = response.Title;
            blogPost.Content = response.Content;
            blogPost.CategoryId = response.CategoryId;

            var vm = new BlogPostViewModel();
            vm.BlogPost = blogPost;


            ViewData["CategoryId"] = new SelectList(await mediator.Send(new CategoryChooseQuery()), "Id", "Name", response.CategoryId);
            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.blogpost.edit")]
        
        public async Task<IActionResult> Edit(BlogPostUpdateCommand command)
        {
            var response =await mediator.Send(command);

            if(response != null)
                return RedirectToAction(nameof(Index));
            
            return View(command);
        }


        [HttpPost]
        [Authorize(Policy = "admin.blogpost.delete")]
        public async Task<IActionResult> Delete(BlogPostDeleteCommand command)
        {
            var result = await mediator.Send(command);
            return Json(result);
        }

    }
}
