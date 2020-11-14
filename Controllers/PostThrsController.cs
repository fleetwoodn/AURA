using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AURA.Data;
using AURA.Models;

using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace AURA.Controllers
{
    [Authorize]
    public class PostThrsController : Controller
    {
        private readonly PostContext _context;

        public PostThrsController(PostContext context)
        {
            _context = context;
        }

        // GET: PostThrs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostThrs.ToListAsync());
        }

        // GET: PostThrs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postThr = await _context.PostThrs
                .FirstOrDefaultAsync(m => m.ThrId == id);
            if (postThr == null)
            {
                return NotFound();
            }

            return View(postThr);
        }

        // GET: PostThrs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostThrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThrId,ThrZero,ThrDigit,ThrDate,ThrText")] PostThr postThr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postThr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postThr);
        }

        // GET: PostThrs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postThr = await _context.PostThrs.FindAsync(id);
            if (postThr == null)
            {
                return NotFound();
            }
            return View(postThr);
        }

        // POST: PostThrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThrId,ThrZero,ThrDigit,ThrDate,ThrText")] PostThr postThr)
        {
            if (id != postThr.ThrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postThr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostThrExists(postThr.ThrId))
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
            return View(postThr);
        }

        // GET: PostThrs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postThr = await _context.PostThrs
                .FirstOrDefaultAsync(m => m.ThrId == id);
            if (postThr == null)
            {
                return NotFound();
            }

            return View(postThr);
        }

        // POST: PostThrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postThr = await _context.PostThrs.FindAsync(id);
            _context.PostThrs.Remove(postThr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostThrExists(int id)
        {
            return _context.PostThrs.Any(e => e.ThrId == id);
        }

        /// <summary>
        /// Downloads the post one comma seperated file.
        /// </summary>
        /// <returns></returns>
        public IActionResult DownloadCommaSeperatedFileThr()
        {
            try
            {
                var postThrs = _context.PostThrs.ToList();
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder exportData = stringBuilder;
                stringBuilder.AppendLine("0/,Stage,Lead Agent,Party ID,Title");
                foreach (var author in postThrs)
                {
                    stringBuilder.AppendLine($"{author.ThrId},{ author.ThrZero},{ author.ThrDigit},{ author.ThrDate},{ author.ThrText}");
                }
                return File(Encoding.UTF8.GetBytes
                (stringBuilder.ToString()), "text/csv", "PostOne.csv");
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
