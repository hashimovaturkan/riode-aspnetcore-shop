using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Riode_WebUI.AppCode.Extensions;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactPostsController : Controller
    {
        private readonly RiodeDbContext db;
        readonly IConfiguration configuration;

        public ContactPostsController(RiodeDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        [Authorize(Policy = "admin.contactposts.index")]
        public async Task<IActionResult> Index(long typeId)
        {
            var query = db.ContactPosts.AsQueryable()
                .Where(q => q.DeletedByUserId == null);

            ViewBag.Inbox = query.Count();
            ViewBag.Unread = query.Where(q => q.AnswerByUserId == null).Count();
            ViewBag.Sent = query.Where(q => q.AnswerByUserId != null).Count();
            ViewBag.Marked = query.Where(q => q.IsMarked == true).Count();

            switch (typeId)
            {
                case 1:
                    query = query.Where(q => q.AnswerByUserId == null);
                    break;
                case 2:
                    query = query.Where(q => q.AnswerByUserId != null);
                    break;
                case 3:
                    query = query.Where(q => q.IsMarked == true);
                    break;
                default:
                    break;

            }


            return View(await query.ToListAsync());
        }

        [Authorize(Policy = "admin.contactposts.details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPost = await db.ContactPosts
                .FirstOrDefaultAsync(m => m.Id == id
                                    && m.DeletedByUserId == null
                                    && m.AnswerDate == null);
            if (contactPost == null)
            {
                return NotFound();
            }

            return View(contactPost);
        }

        [HttpPost]
        [Authorize(Policy = "admin.contactposts.answer")]
        public async Task<IActionResult> Answer([Bind("Id,Answer")] ContactPost model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            db.Database.BeginTransaction();
            
            var contactPost = await db.ContactPosts
            .FirstOrDefaultAsync(m => m.Id == model.Id
                                && m.DeletedByUserId == null
                                && m.AnswerDate == null);
            if (contactPost == null)
            {
                return NotFound();
            }
            //baxanlarda 3cu cedvel mentiqi nedi tutmadim
            //sual burda nece contactpostun dbden oldugunu basa dusur
            //db.contactPost kimi bunlari yazmali deyildik?
            //cavab: cunki select ile isleyir onu nezere al selectnende isleyirse avtomatik dbnan elaqesi qalir contactpostun
            contactPost.Answer = model.Answer;
            contactPost.AnswerDate = DateTime.Now;
            contactPost.AnswerByUserId = 1;
            await db.SaveChangesAsync();
            var mailSend = configuration.SendEmail(contactPost.Email, "Riode Answer", $"{model.Answer}");

            if (mailSend == false)
            {
                db.Database.RollbackTransaction();
                return View("Details",model);
            }

            db.Database.CommitTransaction();

            return Redirect(nameof(Index));
        }


        [Authorize(Policy = "admin.contactposts.marked")]
        public async Task<IActionResult> Marked(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPostItem = await db.ContactPosts
                .FirstOrDefaultAsync(m => m.Id == id
                                    && m.DeletedByUserId == null);

            if (contactPostItem == null)
            {
                return NotFound();
            }

            if (contactPostItem.IsMarked == true)
            {
                contactPostItem.IsMarked = false;
            }
            else
            {
                contactPostItem.IsMarked = true;

            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
