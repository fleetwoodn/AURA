using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AURA.Data;
using AURA.Models;
using Microsoft.AspNetCore.Authorization;

namespace AURA.Controllers
{
    [Authorize(Roles = "Admin,Sale")]
    public class MiscTextsController : Controller
    {
        private readonly PostContext _context;

        public MiscTextsController(PostContext context)
        {
            _context = context;
        }

        // GET: MiscTexts
        public async Task<IActionResult> Index()
        {
            return View(await _context.MiscText.ToListAsync());
        }

        // GET: MiscTexts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miscText = await _context.MiscText
                .FirstOrDefaultAsync(m => m.ID == id);



            if (miscText == null)
            {
                return NotFound();
            }

            ViewBag.fulltext = miscText.FullText;

            return View(miscText);
        }

        // GET: MiscTexts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MiscTexts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,FullText,LastUpdate")] MiscText miscText)
        {
            if (ModelState.IsValid)
            {
                miscText.LastUpdate = DateTime.Now.Date;
                _context.Add(miscText);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miscText);
        }

        // GET: MiscTexts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miscText = await _context.MiscText.FindAsync(id);
            if (miscText == null)
            {
                return NotFound();
            }
            return View(miscText);
        }

        // POST: MiscTexts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,FullText,LastUpdate")] MiscText miscText)
        {
            if (id != miscText.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    miscText.LastUpdate = DateTime.Now.Date;
                    _context.Update(miscText);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiscTextExists(miscText.ID))
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
            return View(miscText);
        }

        // GET: MiscTexts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miscText = await _context.MiscText
                .FirstOrDefaultAsync(m => m.ID == id);
            if (miscText == null)
            {
                return NotFound();
            }

            return View(miscText);
        }

        // POST: MiscTexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miscText = await _context.MiscText.FindAsync(id);
            _context.MiscText.Remove(miscText);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiscTextExists(int id)
        {
            return _context.MiscText.Any(e => e.ID == id);
        }
    }
}
