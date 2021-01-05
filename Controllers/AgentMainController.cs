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
using Microsoft.AspNetCore.Authorization;

namespace AURA.Controllers
{
    [Authorize]
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

        public IActionResult AgentDetail(int? UserId = 1111)
        {
            if (UserId == null)
            {
                return NotFound();
            }

            //Agents agents = _context.Agents.Where(m => m.UserId == UserId);
            Agents agents = _context.Agents.Find(UserId);

            //PostOne postOne = _context.PostOnes.Find(id);
            //PostOne postOne = _context.PostOnes.FirstOrDefault(m => m.OneZero == zero);

            //var uThr = _context.PostThrs.Where(m => m.ThrZero == zero).ToList();

            var uPho = _context.AgentsPhones.Where(m => m.UserId == UserId).ToList();
            var uEma = _context.AgentsEmails.Where(m => m.UserId == UserId).ToList();
            var uAdd = _context.AgentsAddresses.Where(m => m.UserId == UserId).ToList();
            var uRol = _context.AgentsRoles.Where(m => m.UserId == UserId).ToList();
            var uPay = _context.AgentsPayments.Where(m => m.UserId == UserId).ToList();
            var uRem = _context.AgentsRemarks.Where(m => m.UserId == UserId).ToList();

            var viewModel = new AgentDetailVM
            {
                UserId = agents.UserId,
                AuraId = agents.AuraId,
                FullName = agents.FullName,
                NickName = agents.NickName,
                BirthDate = agents.BirthDate,
                TaxId = agents.TaxId,
                StartDate = agents.StartDate,
                EndDate = agents.EndDate,
                AuraRole = agents.AuraRole,

                agentsPhones = uPho,
                agentsEmails = uEma,
                agentsAddresses = uAdd,
                agentsRoles = uRol,
                agentsPayments = uPay,
                agentsRemarks = uRem,

            };

            return View(viewModel);
        }

        //****************agent

        // GET: Agents/Create
        public IActionResult CreateAgent(int? UserId)
        {
            ViewBag.userid = UserId;

            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAgent([Bind("UserId,AuraId,FullName,NickName,BirthDate,TaxId,StartDate,EndDate,AuraRole")] Agents agents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AgentDetail), new { UserId = agents.UserId });
            }
            return View(agents);
        }

        // GET: Agents/Edit/5
        public async Task<IActionResult> EditAgent(int? UserId)
        {
            if (UserId == null)
            {
                return NotFound();
            }

            var agents = await _context.Agents.FindAsync(UserId);
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
        public async Task<IActionResult> EditAgent(int UserId, [Bind("UserId,AuraId,FullName,NickName,BirthDate,TaxId,StartDate,EndDate,AuraRole")] Agents agents)
        {
            if (UserId != agents.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(agents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AgentDetail), new { UserId = agents.UserId });
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(agents);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!AgentsExists(agents.UserId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }

            
            
            return View(agents);
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> DeleteAgent(int? UserId)
        {
            if (UserId == null)
            {
                return NotFound();
            }

            var agents = await _context.Agents
                .FirstOrDefaultAsync(m => m.UserId == UserId);
            if (agents == null)
            {
                return NotFound();
            }

            return View(agents);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("DeleteAgent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAgent(int UserId)
        {
            var agents = await _context.Agents.FindAsync(UserId);
            _context.Agents.Remove(agents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentsExists(int UserId)
        {
            return _context.Agents.Any(e => e.UserId == UserId);
        }


        //**************agent phone

        // GET: AgentsPhones/Create
        public IActionResult CreatePhone(int? UserId)
        {
            ViewBag.userid = UserId;

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

                agentsPhone.EmailText = agentsPhone.PhoneNumber +"@"+ agentsPhone.CarrierName;

                _context.Add(agentsPhone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsPhone.UserId });
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
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsPhone.UserId });
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
        public IActionResult CreateEmail(int UserId)
        {
            ViewBag.userid = UserId;

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
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsEmail.UserId });
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
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsEmail.UserId });
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
        public IActionResult CreateAddress(string UserId)
        {
            ViewBag.userid = UserId;

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
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsAddress.UserId });
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
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsAddress.UserId });
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
            return RedirectToAction(nameof(AgentDetail), new { UserId = agentsAddress.UserId });
        }

        private bool AgentsAddressExists(int id)
        {
            return _context.AgentsAddresses.Any(e => e.Id == id);
        }

        //**************agent roles

        // GET: AgentsRoles/Create
        public IActionResult CreateRole(string UserId)
        {
            ViewBag.userid = UserId;

            return View();
        }

        // POST: AgentsRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole([Bind("Id,UserId,AgentRole,StartDate,EndDate,PayType,PayRate,Notes")] AgentsRoles agentsRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentsRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsRoles.UserId });
            }
            return View(agentsRoles);
        }

        // GET: AgentsRoles/Edit/5
        public async Task<IActionResult> EditRole(int? id)
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
        public async Task<IActionResult> EditRole(int id, [Bind("Id,UserId,AgentRole,StartDate,EndDate,PayType,PayRate,Notes")] AgentsRoles agentsRoles)
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
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsRoles.UserId });
            }
            return View(agentsRoles);
        }

        // GET: AgentsRoles/Delete/5
        public async Task<IActionResult> DeleteRole(int? id)
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
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedRole(int id)
        {
            var agentsRoles = await _context.AgentsRoles.FindAsync(id);
            _context.AgentsRoles.Remove(agentsRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AgentDetail), new { UserId = agentsRoles.UserId });
        }

        private bool AgentsRolesExists(int id)
        {
            return _context.AgentsRoles.Any(e => e.Id == id);
        }

        //**************agent remarks

        // GET: AgentsRemarks/Create
        public IActionResult CreateRemark(int UserId)
        {
            ViewBag.userid = UserId;

            return View();
        }

        // POST: AgentsRemarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRemark([Bind("Id,UserId,RemarkSubject,RemarkText")] AgentsRemarks agentsRemarks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentsRemarks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsRemarks.UserId });
            }
            return View(agentsRemarks);
        }

        // GET: AgentsRemarks/Edit/5
        public async Task<IActionResult> EditRemark(int? id)
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
        public async Task<IActionResult> EditRemark(int id, [Bind("Id,UserId,RemarkSubject,RemarkText")] AgentsRemarks agentsRemarks)
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
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsRemarks.UserId });
            }
            return View(agentsRemarks);
        }

        // GET: AgentsRemarks/Delete/5
        public async Task<IActionResult> DeleteRemark(int? id)
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
        [HttpPost, ActionName("DeleteRemark")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedRemark(int id)
        {
            var agentsRemarks = await _context.AgentsRemarks.FindAsync(id);
            _context.AgentsRemarks.Remove(agentsRemarks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AgentDetail), new { UserId = agentsRemarks.UserId });
        }

        private bool AgentsRemarksExists(int id)
        {
            return _context.AgentsRemarks.Any(e => e.Id == id);
        }

        //**************agent payment

        // GET: AgentsPayments/Create
        public IActionResult CreatePayment(int UserId)
        {
            ViewBag.userid = UserId;

            return View();
        }

        // POST: AgentsPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePayment([Bind("Id,UserId,PaymentType,PaymentDetail,Notes")] AgentsPayment agentsPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentsPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsPayment.UserId });
            }
            return View(agentsPayment);
        }

        // GET: AgentsPayments/Edit/5
        public async Task<IActionResult> EditPayment(int? id)
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
        public async Task<IActionResult> EditPayment(int id, [Bind("Id,UserId,PaymentType,PaymentDetail,Notes")] AgentsPayment agentsPayment)
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
                return RedirectToAction(nameof(AgentDetail), new { UserId = agentsPayment.UserId });
            }
            return View(agentsPayment);
        }

        // GET: AgentsPayments/Delete/5
        public async Task<IActionResult> DeletePayment(int? id)
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
        [HttpPost, ActionName("DeletePayment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedPayment(int id)
        {
            var agentsPayment = await _context.AgentsPayments.FindAsync(id);
            _context.AgentsPayments.Remove(agentsPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AgentDetail), new { UserId = agentsPayment.UserId });
        }

        private bool AgentsPaymentExists(int id)
        {
            return _context.AgentsPayments.Any(e => e.Id == id);
        }

    }
}
