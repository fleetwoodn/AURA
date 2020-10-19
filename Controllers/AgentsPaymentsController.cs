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
    public class AgentsPaymentsController : Controller
    {
        private readonly PostContext _context;

        public AgentsPaymentsController(PostContext context)
        {
            _context = context;
        }

        // GET: AgentsPayments
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgentsPayments.ToListAsync());
        }

        // GET: AgentsPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsPayment = await _context.AgentsPayments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsPayment == null)
            {
                return NotFound();
            }

            return View(agentsPayment);
        }

        // GET: AgentsPayments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentsPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,PaymentType,PaymentDetail,Notes")] AgentsPayment agentsPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentsPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentsPayment);
        }

        // GET: AgentsPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsPayment = await _context.AgentsPayments.FindAsync(id);
            if (agentsPayment == null)
            {
                return NotFound();
            }
            return View(agentsPayment);
        }

        // POST: AgentsPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PaymentType,PaymentDetail,Notes")] AgentsPayment agentsPayment)
        {
            if (id != agentsPayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentsPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentsPaymentExists(agentsPayment.Id))
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
            return View(agentsPayment);
        }

        // GET: AgentsPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentsPayment = await _context.AgentsPayments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentsPayment == null)
            {
                return NotFound();
            }

            return View(agentsPayment);
        }

        // POST: AgentsPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentsPayment = await _context.AgentsPayments.FindAsync(id);
            _context.AgentsPayments.Remove(agentsPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentsPaymentExists(int id)
        {
            return _context.AgentsPayments.Any(e => e.Id == id);
        }
    }
}
