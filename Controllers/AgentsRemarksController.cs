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
    public class AgentsRemarksController : Controller
    {
        private readonly PostContext _context;

        public AgentsRemarksController(PostContext context)
        {
            _context = context;
        }

        // GET: AgentsRemarks
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgentsRemarks.ToListAsync());
        }

        // GET: AgentsRemarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsRemarks = await _context.AgentsRemarks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsRemarks == null)
            {
                return NotFound();
            }

            return View(agentsRemarks);
        }

        // GET: AgentsRemarks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentsRemarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,RemarkSubject,RemarkText")] AgentsRemarks agentsRemarks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentsRemarks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentsRemarks);
        }

        // GET: AgentsRemarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsRemarks = await _context.AgentsRemarks.FindAsync(id);
            if (agentsRemarks == null)
            {
                return NotFound();
            }
            return View(agentsRemarks);
        }

        // POST: AgentsRemarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RemarkSubject,RemarkText")] AgentsRemarks agentsRemarks)
        {
            if (id != agentsRemarks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentsRemarks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentsRemarksExists(agentsRemarks.Id))
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
            return View(agentsRemarks);
        }

        // GET: AgentsRemarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsRemarks = await _context.AgentsRemarks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsRemarks == null)
            {
                return NotFound();
            }

            return View(agentsRemarks);
        }

        // POST: AgentsRemarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentsRemarks = await _context.AgentsRemarks.FindAsync(id);
            _context.AgentsRemarks.Remove(agentsRemarks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentsRemarksExists(int id)
        {
            return _context.AgentsRemarks.Any(e => e.Id == id);
        }
    }
}
