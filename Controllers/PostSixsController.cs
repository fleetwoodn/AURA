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
    public class PostSixsController : Controller
    {
        private readonly PostContext _context;

        public PostSixsController(PostContext context)
        {
            _context = context;
        }

        // GET: PostSixes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostSixs.ToListAsync());
        }

        // GET: PostSixes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSix = await _context.PostSixs
                .FirstOrDefaultAsync(m => m.SixId == id);
            if (postSix == null)
            {
                return NotFound();
            }

            return View(postSix);
        }

        // GET: PostSixes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostSixes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SixId,SixZero,SixDigit,SixDate,SixType,SixDeta,SixAmou,SixStat,SixNote")] PostSix postSix)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postSix);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postSix);
        }

        // GET: PostSixes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSix = await _context.PostSixs.FindAsync(id);
            if (postSix == null)
            {
                return NotFound();
            }
            return View(postSix);
        }

        // POST: PostSixes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SixId,SixZero,SixDigit,SixDate,SixType,SixDeta,SixAmou,SixStat,SixNote")] PostSix postSix)
        {
            if (id != postSix.SixId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postSix);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostSixExists(postSix.SixId))
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
            return View(postSix);
        }

        // GET: PostSixes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSix = await _context.PostSixs
                .FirstOrDefaultAsync(m => m.SixId == id);
            if (postSix == null)
            {
                return NotFound();
            }

            return View(postSix);
        }

        // POST: PostSixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postSix = await _context.PostSixs.FindAsync(id);
            _context.PostSixs.Remove(postSix);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostSixExists(int id)
        {
            return _context.PostSixs.Any(e => e.SixId == id);
        }
    }
}
