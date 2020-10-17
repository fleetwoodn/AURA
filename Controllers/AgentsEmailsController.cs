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
    public class AgentsEmailsController : Controller
    {
        private readonly PostContext _context;

        public AgentsEmailsController(PostContext context)
        {
            _context = context;
        }

        // GET: AgentsEmails
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgentsEmails.ToListAsync());
        }

        // GET: AgentsEmails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsEmail = await _context.AgentsEmails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsEmail == null)
            {
                return NotFound();
            }

            return View(agentsEmail);
        }

        // GET: AgentsEmails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentsEmails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,EmailAddress,Notes")] AgentsEmail agentsEmail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentsEmail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentsEmail);
        }

        // GET: AgentsEmails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsEmail = await _context.AgentsEmails.FindAsync(id);
            if (agentsEmail == null)
            {
                return NotFound();
            }
            return View(agentsEmail);
        }

        // POST: AgentsEmails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,EmailAddress,Notes")] AgentsEmail agentsEmail)
        {
            if (id != agentsEmail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentsEmail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentsEmailExists(agentsEmail.Id))
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
            return View(agentsEmail);
        }

        // GET: AgentsEmails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsEmail = await _context.AgentsEmails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsEmail == null)
            {
                return NotFound();
            }

            return View(agentsEmail);
        }

        // POST: AgentsEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentsEmail = await _context.AgentsEmails.FindAsync(id);
            _context.AgentsEmails.Remove(agentsEmail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentsEmailExists(int id)
        {
            return _context.AgentsEmails.Any(e => e.Id == id);
        }
    }
}
