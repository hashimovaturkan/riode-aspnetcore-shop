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
    public class ColorsController : Controller
    {
        private readonly RiodeDbContext db;

        public ColorsController(RiodeDbContext db)
        {
            this.db = db;
        }

        // GET: Admin/Colors
        public async Task<IActionResult> Index(int page = 1)
        {
            int take = 5;
            ViewBag.PageCount = Decimal.Ceiling((decimal)db.Colors.Where(b => b.DeletedByUserId == null).Count() / take);

            return View(await db.Colors.Where(s => s.DeletedByUserId == null).ToListAsync());
        }

        // GET: Admin/Colors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await db.Colors
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedByUserId == null);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Admin/Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HexCode,Name,Description,Id,CreatedByUserId,CreatedDate,DeletedByUserId,DeletedDate")] Color color)
        {
            if (ModelState.IsValid)
            {
                db.Add(color);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Admin/Colors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await db.Colors.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("HexCode,Name,Description,Id,CreatedByUserId,CreatedDate,DeletedByUserId,DeletedDate")] Color color)
        {
            if (id != color.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(color);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.Id))
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
            return View(color);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await db.Colors
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedByUserId == null);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Admin/Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var color = await db.Colors.FindAsync(id);
            db.Colors.Remove(color);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColorExists(long id)
        {
            return db.Colors.Any(e => e.Id == id && e.DeletedByUserId == null);
        }
    }
}
