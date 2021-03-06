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
    public class AgentsController : Controller
    {
        private readonly PostContext _context;

        public AgentsController(PostContext context)
        {
            _context = context;
        }

        // GET: Agents
        public async Task<IActionResult> Index(string searchString)
        {
            var agts = _context.Agents.OrderByDescending(p => p.AuraId).Take(100);

            if (!String.IsNullOrEmpty(searchString))
            {
                agts = from m in _context.Agents
                         select m;

                agts = agts.Where(s => s.AuraId.Contains(searchString)
                    || s.FullName.Contains(searchString)
                    || s.PhoneNumber.Contains(searchString)
                    );
            }


            return View(await agts.ToListAsync());
        }

        // GET: Agents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agents = await _context.Agents
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (agents == null)
            {
                return NotFound();
            }

            return View(agents);
        }

        // GET: Agents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,AuraId,FullName,NickName,PhoneNumber,EmailAddress,BirthDate,TaxId,StartDate,EndDate,AuraRole,TaxType,BackupWitholding,PaymentType,PaymentDetail,StreetAddress,PostCode")] Agents agents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agents);
        }

        // GET: Agents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agents = await _context.Agents.FindAsync(id);
            if (agents == null)
            {
                return NotFound();
            }
            return View(agents);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,AuraId,FullName,NickName,PhoneNumber,EmailAddress,BirthDate,TaxId,StartDate,EndDate,AuraRole,TaxType,BackupWitholding,PaymentType,PaymentDetail,StreetAddress,PostCode")] Agents agents)
        {
            if (id != agents.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentsExists(agents.UserId))
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
            return View(agents);
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agents = await _context.Agents
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (agents == null)
            {
                return NotFound();
            }

            return View(agents);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agents = await _context.Agents.FindAsync(id);
            _context.Agents.Remove(agents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentsExists(int id)
        {
            return _context.Agents.Any(e => e.UserId == id);
        }
    }
}
