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
    public class ManifestsController : Controller
    {
        private readonly PostContext _context;

        public ManifestsController(PostContext context)
        {
            _context = context;
        }

        // GET: Manifests
        public async Task<IActionResult> Index()
        {
            return View(await _context.Manifests.ToListAsync());
        }

        // GET: Manifests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifest = await _context.Manifests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manifest == null)
            {
                return NotFound();
            }

            return View(manifest);
        }

        // GET: Manifests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manifests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Zero,Type,Name,Wrap,Assembly,Notes")] Manifest manifest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manifest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manifest);
        }

        // GET: Manifests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifest = await _context.Manifests.FindAsync(id);
            if (manifest == null)
            {
                return NotFound();
            }
            return View(manifest);
        }

        // POST: Manifests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Zero,Type,Name,Wrap,Assembly,Notes")] Manifest manifest)
        {
            if (id != manifest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manifest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManifestExists(manifest.Id))
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
            return View(manifest);
        }

        // GET: Manifests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifest = await _context.Manifests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manifest == null)
            {
                return NotFound();
            }

            return View(manifest);
        }

        // POST: Manifests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manifest = await _context.Manifests.FindAsync(id);
            _context.Manifests.Remove(manifest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManifestExists(int id)
        {
            return _context.Manifests.Any(e => e.Id == id);
        }

        ///OTHER PARTS
        ///
        public IActionResult AddObject(Manifest manifest, string Type, string NameSelect, string NameWrite)
        {
            

            return View();
        }
    }
}
