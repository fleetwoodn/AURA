using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Net;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using AURA.Data;
using AURA.Models;
using AURA.ViewModels;
using Microsoft.EntityFrameworkCore;




namespace AURA.Controllers
{
    public class AgentMainController : Controller
    {
        private readonly PostContext _context;

        public AgentMainController(PostContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Agents.ToListAsync());
        }

        public async Task<IActionResult> AgentDetail(string id)
        {
            return View();
        }

        //****************agent

        // GET: Agents/Create
        public IActionResult CreateAgent()
        {
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAgent([Bind("UserId,FullName,NickName,BirthDate,TaxId,StartDate,EndDate,AuraRole")] Agents agents)
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
        public async Task<IActionResult> EditAgent(string id)
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
        public async Task<IActionResult> EditAgent(string id, [Bind("UserId,FullName,NickName,BirthDate,TaxId,StartDate,EndDate,AuraRole")] Agents agents)
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
        public async Task<IActionResult> DeleteAgent(string id)
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
        [HttpPost, ActionName("DeleteAgent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAgent(string id)
        {
            var agents = await _context.Agents.FindAsync(id);
            _context.Agents.Remove(agents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentsExists(string id)
        {
            return _context.Agents.Any(e => e.UserId == id);
        }


        //**************agent phone

        // GET: AgentsPhones/Create
        public IActionResult CreatePhone()
        {
            return View();
        }

        // POST: AgentsPhones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePhone([Bind("Id,UserId,PhoneNumber,CountryCode,CarrierName,EmailText,Notes")] AgentsPhone agentsPhone)
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
        public async Task<IActionResult> EditPhone(int? id)
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
        public async Task<IActionResult> EditPhone(int id, [Bind("Id,UserId,PhoneNumber,CountryCode,CarrierName,EmailText,Notes")] AgentsPhone agentsPhone)
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
        public async Task<IActionResult> DeletePhone(int? id)
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
        public async Task<IActionResult> DeleteConfirmedPhone(int id)
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



        //**************agent email

        // GET: AgentsEmails/Create
        public IActionResult CreateEmail()
        {
            return View();
        }

        // POST: AgentsEmails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmail([Bind("Id,UserId,EmailAddress,Notes")] AgentsEmail agentsEmail)
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
        public async Task<IActionResult> EditEmail(int? id)
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
        public async Task<IActionResult> EditEmail(int id, [Bind("Id,UserId,EmailAddress,Notes")] AgentsEmail agentsEmail)
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
        public async Task<IActionResult> DeleteEmail(int? id)
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
        [HttpPost, ActionName("DeleteEmail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedEmail(int id)
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

        //***************agent address

        // GET: AgentsAddresses/Create
        public IActionResult CreateAddress()
        {
            return View();
        }

        // POST: AgentsAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAddress([Bind("Id,UserId,StreetAddress,PostCode,Notes")] AgentsAddress agentsAddress)
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
        public async Task<IActionResult> EditAddress(int? id)
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
        public async Task<IActionResult> EditAddress(int id, [Bind("Id,UserId,StreetAddress,PostCode,Notes")] AgentsAddress agentsAddress)
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
        public async Task<IActionResult> DeleteAddress(int? id)
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
        [HttpPost, ActionName("DeleteAddress")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAddress(int id)
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

        //**************agent roles


        //**************agent remarks


        //**************agent payment

    }
}
