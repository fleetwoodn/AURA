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
    public class PostEigsController : Controller
    {
        private readonly PostContext _context;

        public PostEigsController(PostContext context)
        {
            _context = context;
        }

        // GET: PostEigs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostEigs.ToListAsync());
        }

        // GET: PostEigs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEig = await _context.PostEigs
                .FirstOrDefaultAsync(m => m.EigId == id);
            if (postEig == null)
            {
                return NotFound();
            }

            return View(postEig);
        }

        // GET: PostEigs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostEigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EigId,EigZero,EigDigit,EigAgen,EigRole,EigLoad,EigNote")] PostEig postEig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postEig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postEig);
        }

        // GET: PostEigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEig = await _context.PostEigs.FindAsync(id);
            if (postEig == null)
            {
                return NotFound();
            }
            return View(postEig);
        }

        // POST: PostEigs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EigId,EigZero,EigDigit,EigAgen,EigRole,EigLoad,EigNote")] PostEig postEig)
        {
            if (id != postEig.EigId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postEig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostEigExists(postEig.EigId))
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
            return View(postEig);
        }

        // GET: PostEigs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEig = await _context.PostEigs
                .FirstOrDefaultAsync(m => m.EigId == id);
            if (postEig == null)
            {
                return NotFound();
            }

            return View(postEig);
        }

        // POST: PostEigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postEig = await _context.PostEigs.FindAsync(id);
            _context.PostEigs.Remove(postEig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostEigExists(int id)
        {
            return _context.PostEigs.Any(e => e.EigId == id);
        }
    }
}
