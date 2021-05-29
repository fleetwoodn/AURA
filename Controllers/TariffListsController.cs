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
    public class TariffListsController : Controller
    {
        private readonly PostContext _context;

        public TariffListsController(PostContext context)
        {
            _context = context;
        }

        // GET: TariffLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.TariffLists.ToListAsync());
        }

        // GET: TariffLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tariffList = await _context.TariffLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tariffList == null)
            {
                return NotFound();
            }

            return View(tariffList);
        }

        // GET: TariffLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TariffLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Amount,Updated,Notes")] TariffList tariffList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tariffList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tariffList);
        }

        // GET: TariffLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tariffList = await _context.TariffLists.FindAsync(id);
            if (tariffList == null)
            {
                return NotFound();
            }
            return View(tariffList);
        }

        // POST: TariffLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Amount,Updated,Notes")] TariffList tariffList)
        {
            if (id != tariffList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tariffList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TariffListExists(tariffList.Id))
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
            return View(tariffList);
        }

        // GET: TariffLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tariffList = await _context.TariffLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tariffList == null)
            {
                return NotFound();
            }

            return View(tariffList);
        }

        // POST: TariffLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tariffList = await _context.TariffLists.FindAsync(id);
            _context.TariffLists.Remove(tariffList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TariffListExists(int id)
        {
            return _context.TariffLists.Any(e => e.Id == id);
        }
    }
}
