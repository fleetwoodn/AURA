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
    public class AgentsRolesController : Controller
    {
        private readonly PostContext _context;

        public AgentsRolesController(PostContext context)
        {
            _context = context;
        }

        // GET: AgentsRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgentsRoles.ToListAsync());
        }

        // GET: AgentsRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsRoles = await _context.AgentsRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsRoles == null)
            {
                return NotFound();
            }

            return View(agentsRoles);
        }

        // GET: AgentsRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentsRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,AgentRole,StartDate,EndDate,PayType,PayRate,Notes")] AgentsRoles agentsRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentsRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentsRoles);
        }

        // GET: AgentsRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsRoles = await _context.AgentsRoles.FindAsync(id);
            if (agentsRoles == null)
            {
                return NotFound();
            }
            return View(agentsRoles);
        }

        // POST: AgentsRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,AgentRole,StartDate,EndDate,PayType,PayRate,Notes")] AgentsRoles agentsRoles)
        {
            if (id != agentsRoles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentsRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentsRolesExists(agentsRoles.Id))
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
            return View(agentsRoles);
        }

        // GET: AgentsRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsRoles = await _context.AgentsRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsRoles == null)
            {
                return NotFound();
            }

            return View(agentsRoles);
        }

        // POST: AgentsRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentsRoles = await _context.AgentsRoles.FindAsync(id);
            _context.AgentsRoles.Remove(agentsRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentsRolesExists(int id)
        {
            return _context.AgentsRoles.Any(e => e.Id == id);
        }
    }
}
