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
    public class PostFivsController : Controller
    {
        private readonly PostContext _context;

        public PostFivsController(PostContext context)
        {
            _context = context;
        }

        // GET: PostFivs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostFivs.ToListAsync());
        }

        // GET: PostFivs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFiv = await _context.PostFivs
                .FirstOrDefaultAsync(m => m.FivId == id);
            if (postFiv == null)
            {
                return NotFound();
            }

            return View(postFiv);
        }

        // GET: PostFivs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostFivs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FivId,FivZero,FivDigit,FivPrio,FivCode,FivText")] PostFiv postFiv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postFiv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postFiv);
        }

        // GET: PostFivs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFiv = await _context.PostFivs.FindAsync(id);
            if (postFiv == null)
            {
                return NotFound();
            }
            return View(postFiv);
        }

        // POST: PostFivs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FivId,FivZero,FivDigit,FivPrio,FivCode,FivText")] PostFiv postFiv)
        {
            if (id != postFiv.FivId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postFiv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostFivExists(postFiv.FivId))
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
            return View(postFiv);
        }

        // GET: PostFivs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFiv = await _context.PostFivs
                .FirstOrDefaultAsync(m => m.FivId == id);
            if (postFiv == null)
            {
                return NotFound();
            }

            return View(postFiv);
        }

        // POST: PostFivs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postFiv = await _context.PostFivs.FindAsync(id);
            _context.PostFivs.Remove(postFiv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostFivExists(int id)
        {
            return _context.PostFivs.Any(e => e.FivId == id);
        }
    }
}
