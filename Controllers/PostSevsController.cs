using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AURA.Data;
using AURA.Models;

using System.Data;
using System.Net;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using AURA.ViewModels;
using System.Text;

//using System.Web.UI;
//using System.Web.UI.WebControls;
using SelectPdf;
using Microsoft.AspNetCore.Authorization;

namespace AURA.Controllers
{
    public class PostSevsController : Controller
    {
        private readonly PostContext _context;

        public PostSevsController(PostContext context)
        {
            _context = context;
        }

        // GET: PostSevs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostSevs.ToListAsync());
        }

        // GET: PostSevs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSev = await _context.PostSevs
                .FirstOrDefaultAsync(m => m.SevId == id);
            if (postSev == null)
            {
                return NotFound();
            }

            return View(postSev);
        }

        // GET: PostSevs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostSevs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SevId,SevZero,SevDigit,SevInvo,SevDate,SevDesc,SevAmou,SevAc1,SevAc2,SevAcf,SevSign,SevStage,SevPart,SevCust,SevStat,SevPaym,SevRefe,SevHidd,SevChec,SevNote")] PostSev postSev)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postSev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postSev);
        }

        // GET: PostSevs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSev = await _context.PostSevs.FindAsync(id);
            if (postSev == null)
            {
                return NotFound();
            }
            return View(postSev);
        }

        // POST: PostSevs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SevId,SevZero,SevDigit,SevInvo,SevDate,SevDesc,SevAmou,SevAc1,SevAc2,SevAcf,SevSign,SevStage,SevPart,SevCust,SevStat,SevPaym,SevRefe,SevHidd,SevChec,SevNote")] PostSev postSev)
        {
            if (id != postSev.SevId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postSev);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostSevExists(postSev.SevId))
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
            return View(postSev);
        }

        // GET: PostSevs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSev = await _context.PostSevs
                .FirstOrDefaultAsync(m => m.SevId == id);
            if (postSev == null)
            {
                return NotFound();
            }

            return View(postSev);
        }

        // POST: PostSevs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postSev = await _context.PostSevs.FindAsync(id);
            _context.PostSevs.Remove(postSev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //download csv
        public IActionResult DownloadPostDetailCommaSeperatedFile(string startDate)
        {
            //var qTotal = _context.PostSevs.Where(q => q.SevZero == zero && q.SevHidd == "FALSE").Sum(c => c.SevAmou);

            DateTime parseDate = DateTime.Parse(startDate);

            try
            {
                var postSevs = _context.PostSevs.Where(m => m.SevDate == parseDate).ToList();
                
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder exportData = stringBuilder;

                
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"BILLING");
                stringBuilder.AppendLine($"TOTAL DUE:");
                foreach (var author in postSevs)
                {
                    stringBuilder.AppendLine($"{author.SevDigit}.,{author.SevDate.Date},{author.SevDesc},{author.SevAmou}");

                }


                return File(Encoding.UTF8.GetBytes
                (stringBuilder.ToString()), "text/csv", "PostDetail" + ".csv");
            }
            catch
            {
                return RedirectToAction(nameof(PostIndex));
            }
        }



        private bool PostSevExists(int id)
        {
            return _context.PostSevs.Any(e => e.SevId == id);
        }
    }
}
