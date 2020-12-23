using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AURA.Data;
using AURA.Models;

namespace AURA.Controllers
{
    public class TermsConditionsController : Controller
    {
        private readonly PostContext _context;

        public TermsConditionsController(PostContext context)
        {
            _context = context;
        }

        // GET: TermsConditions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TermsConditions.ToListAsync());
        }

        // GET: TermsConditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termsConditions = await _context.TermsConditions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (termsConditions == null)
            {
                return NotFound();
            }

            return View(termsConditions);
        }

        // GET: TermsConditions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TermsConditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EntryDate,Text")] TermsConditions termsConditions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(termsConditions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(termsConditions);
        }

        //create -- richtext editor
        

        // GET: TermsConditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termsConditions = await _context.TermsConditions.FindAsync(id);
            if (termsConditions == null)
            {
                return NotFound();
            }
            return View(termsConditions);
        }

        // POST: TermsConditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EntryDate,Text")] TermsConditions termsConditions)
        {
            if (id != termsConditions.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(termsConditions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TermsConditionsExists(termsConditions.ID))
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
            return View(termsConditions);
        }

        // GET: TermsConditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termsConditions = await _context.TermsConditions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (termsConditions == null)
            {
                return NotFound();
            }

            return View(termsConditions);
        }

        // POST: TermsConditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var termsConditions = await _context.TermsConditions.FindAsync(id);
            _context.TermsConditions.Remove(termsConditions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TermsConditionsExists(int id)
        {
            return _context.TermsConditions.Any(e => e.ID == id);
        }
    }
}
