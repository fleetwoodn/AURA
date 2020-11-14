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
    [Authorize]
    public class PostFousController : Controller
    {
        private readonly PostContext _context;

        public PostFousController(PostContext context)
        {
            _context = context;
        }

        // GET: PostFous
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostFous.ToListAsync());
        }

        // GET: PostFous/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFou = await _context.PostFous
                .FirstOrDefaultAsync(m => m.FouId == id);
            if (postFou == null)
            {
                return NotFound();
            }

            return View(postFou);
        }

        // GET: PostFous/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostFous/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FouId,FouZero,FouDigit,FouName,FouPhon,FouEmai,FouAddr,FouPost,FouOrg,FouNote")] PostFou postFou)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postFou);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postFou);
        }

        // GET: PostFous/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFou = await _context.PostFous.FindAsync(id);
            if (postFou == null)
            {
                return NotFound();
            }
            return View(postFou);
        }

        // POST: PostFous/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FouId,FouZero,FouDigit,FouName,FouPhon,FouEmai,FouAddr,FouPost,FouOrg,FouNote")] PostFou postFou)
        {
            if (id != postFou.FouId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postFou);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostFouExists(postFou.FouId))
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
            return View(postFou);
        }

        // GET: PostFous/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFou = await _context.PostFous
                .FirstOrDefaultAsync(m => m.FouId == id);
            if (postFou == null)
            {
                return NotFound();
            }

            return View(postFou);
        }

        // POST: PostFous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postFou = await _context.PostFous.FindAsync(id);
            _context.PostFous.Remove(postFou);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostFouExists(int id)
        {
            return _context.PostFous.Any(e => e.FouId == id);
        }
    }
}
