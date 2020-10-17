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
    public class AgentsPhonesController : Controller
    {
        private readonly PostContext _context;

        public AgentsPhonesController(PostContext context)
        {
            _context = context;
        }

        // GET: AgentsPhones
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgentsPhones.ToListAsync());
        }

        // GET: AgentsPhones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsPhone = await _context.AgentsPhones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsPhone == null)
            {
                return NotFound();
            }

            return View(agentsPhone);
        }

        // GET: AgentsPhones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentsPhones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,PhoneNumber,CountryCode,CarrierName,EmailText,Notes")] AgentsPhone agentsPhone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentsPhone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentsPhone);
        }

        // GET: AgentsPhones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsPhone = await _context.AgentsPhones.FindAsync(id);
            if (agentsPhone == null)
            {
                return NotFound();
            }
            return View(agentsPhone);
        }

        // POST: AgentsPhones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PhoneNumber,CountryCode,CarrierName,EmailText,Notes")] AgentsPhone agentsPhone)
        {
            if (id != agentsPhone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentsPhone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentsPhoneExists(agentsPhone.Id))
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
            return View(agentsPhone);
        }

        // GET: AgentsPhones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsPhone = await _context.AgentsPhones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsPhone == null)
            {
                return NotFound();
            }

            return View(agentsPhone);
        }

        // POST: AgentsPhones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentsPhone = await _context.AgentsPhones.FindAsync(id);
            _context.AgentsPhones.Remove(agentsPhone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentsPhoneExists(int id)
        {
            return _context.AgentsPhones.Any(e => e.Id == id);
        }
    }
}
