using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;

namespace Riode_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizesController : Controller
    {
        private readonly RiodeDbContext db;

        public SizesController(RiodeDbContext db)
        {
            this.db = db;
        }

        // GET: Admin/Sizes
        public async Task<IActionResult> Index(int page = 1)
        {
            int take = 5;

            ViewBag.PageCount = Decimal.Ceiling((decimal)db.Sizes.Where(b => b.DeletedByUserId == null).Count() / take);
            return View(await db.Sizes.Where(s=>s.DeletedByUserId == null)
                                             .Skip((page - 1) * take)
                                            .Take(take)
                                            .ToListAsync());
        }

        // GET: Admin/Sizes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await db.Sizes
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedByUserId == null);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // GET: Admin/Sizes/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Abbr,Name,Description,Id,CreatedByUserId,CreatedDate,DeletedByUserId,DeletedDate")] Size size)
        {
            if (ModelState.IsValid)
            {
                db.Add(size);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(size);
        }

        // GET: Admin/Sizes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await db.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Abbr,Name,Description,Id,CreatedByUserId,CreatedDate,DeletedByUserId,DeletedDate")] Size size)
        {
            if (id != size.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(size);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeExists(size.Id))
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
            return View(size);
        }

        // GET: Admin/Sizes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await db.Sizes
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedByUserId == null);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // POST: Admin/Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var size = await db.Sizes.FindAsync(id);
            db.Sizes.Remove(size);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SizeExists(long id)
        {
            return db.Sizes.Any(e => e.Id == id  && e.DeletedByUserId == null);
        }
    }
}
