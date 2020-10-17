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
    public class AgentsAddressesController : Controller
    {
        private readonly PostContext _context;

        public AgentsAddressesController(PostContext context)
        {
            _context = context;
        }

        // GET: AgentsAddresses
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgentsAddresses.ToListAsync());
        }

        // GET: AgentsAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsAddress = await _context.AgentsAddresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsAddress == null)
            {
                return NotFound();
            }

            return View(agentsAddress);
        }

        // GET: AgentsAddresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentsAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,StreetAddress,PostCode,Notes")] AgentsAddress agentsAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentsAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentsAddress);
        }

        // GET: AgentsAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsAddress = await _context.AgentsAddresses.FindAsync(id);
            if (agentsAddress == null)
            {
                return NotFound();
            }
            return View(agentsAddress);
        }

        // POST: AgentsAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,StreetAddress,PostCode,Notes")] AgentsAddress agentsAddress)
        {
            if (id != agentsAddress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentsAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentsAddressExists(agentsAddress.Id))
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
            return View(agentsAddress);
        }

        // GET: AgentsAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsAddress = await _context.AgentsAddresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsAddress == null)
            {
                return NotFound();
            }

            return View(agentsAddress);
        }

        // POST: AgentsAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentsAddress = await _context.AgentsAddresses.FindAsync(id);
            _context.AgentsAddresses.Remove(agentsAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentsAddressExists(int id)
        {
            return _context.AgentsAddresses.Any(e => e.Id == id);
        }
    }
}
