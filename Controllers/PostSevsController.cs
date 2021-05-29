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
    [Authorize(Roles = "Admin,Sale")]
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





        private bool PostSevExists(int id)
        {
            return _context.PostSevs.Any(e => e.SevId == id);
        }


        public IActionResult DownloadSevTable()
        {
            return View();
        }

        /// <summary>
        /// Downloads the post one comma seperated file.
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("DownloadSevTable")]
        public IActionResult DownloadCommaSeperatedFileSev(DateTime startDate)
        {
            //DateTime sDate = DateTime.Parse("9/1/2020");
            DateTime sDate = startDate;
            DateTime eDate = DateTime.Now;

            try
            {
                var postSevs = _context.PostSevs.Where(m => m.SevDate >= sDate
                    && m.SevDate <= eDate);
                //var postSevs = _context.PostSevs.ToList();
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder exportData = stringBuilder;
                stringBuilder.AppendLine(sDate + "-" + eDate);
                stringBuilder.AppendLine("Id,Zero,Digit,Date,Desc,Amount,Ac1,Ac2,Acf,Sign,Stage,Party,Customer,Status,?Payment,Reference,?Hidden,Check,Note");
                foreach (var author in postSevs)
                {
                    stringBuilder.AppendLine($"{author.SevId},{author.SevZero},{author.SevDigit},{author.SevDate},{author.SevDesc},{author.SevAmou},{author.SevAc1},{author.SevAc2},{author.SevAcf},{author.SevSign},{author.SevStage},{author.SevPart},{author.SevCust},{author.SevStat},{author.SevPaym},{author.SevRefe},{author.SevHidd},{author.SevChec},{author.SevNote}");
                }
                return File(Encoding.UTF8.GetBytes
                (stringBuilder.ToString()), "text/csv", "PostSev" + sDate + "-" + eDate + ".csv");
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

                       

        }

        //sevreport
        public IActionResult SevReport()
        {
            return View();
        }

        [HttpPost, ActionName("SevReport")]
        public IActionResult SevReportExport(DateTime sDate, DateTime eDate, string ac1, string ac2, string acf)
        {
            try
            {
                var report = _context.PostSevs.Where(m =>
                    m.SevDate.Date >= sDate.Date &&
                    m.SevDate.Date <= eDate.Date &&
                    (ac1 == null || m.SevAc1 == ac1) &&
                    (ac2 == null || m.SevAc2 == ac2) &&
                    (acf == null || m.SevAcf == acf)
                    ).ToList();

                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder exportData = stringBuilder;

                stringBuilder.AppendLine(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                stringBuilder.AppendLine(sDate.Date.ToString("yyyy-MM-dd"));
                stringBuilder.AppendLine(eDate.Date.ToString("yyyy-MM-dd"));
                stringBuilder.AppendLine(ac1);
                stringBuilder.AppendLine(ac2);
                stringBuilder.AppendLine(acf);

                stringBuilder.AppendLine("===,===,===");

                stringBuilder.AppendLine("Id,Zero,Digit,Date,Desc,Amount,Ac1,Ac2,Acf,Sign,Stage,Party,Customer,Status,?Payment,Reference,?Hidden,Check,Note");
                foreach (var author in report)
                {
                    stringBuilder.AppendLine($"{author.SevId},{author.SevZero},{author.SevDigit},{author.SevDate.Date},{author.SevDesc},{author.SevAmou},{author.SevAc1},{author.SevAc2},{author.SevAcf},{author.SevSign},{author.SevStage},{author.SevPart},{author.SevCust},{author.SevStat},{author.SevPaym},{author.SevRefe},{author.SevHidd},{author.SevChec},{author.SevNote}");
                }
                return File(Encoding.UTF8.GetBytes
                (stringBuilder.ToString()), "text/csv", "SevReport" + DateTime.Now.Date + sDate + "-" + eDate + ".csv");
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
