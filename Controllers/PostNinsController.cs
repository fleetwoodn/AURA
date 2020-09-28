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

namespace AURA.Controllers
{
    public class PostNinsController : Controller
    {
        private readonly PostContext _context;

        public PostNinsController(PostContext context)
        {
            _context = context;
        }

        // GET: PostNins
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostNins.ToListAsync());
        }

        // GET: PostNins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postNin = await _context.PostNins
                .FirstOrDefaultAsync(m => m.NinId == id);
            if (postNin == null)
            {
                return NotFound();
            }

            return View(postNin);
        }

        // GET: PostNins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostNins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NinId,NinZero,NinDigit,NinFile,NinCapt,NinNote")] PostNin postNin, IFormFile uploadFile)
        {
            if (uploadFile != null && uploadFile.Length > 0)
            {
                var fileTime = DateTime.UtcNow.ToString("yyMMddHHmmss");
                var fileName = fileTime + Path.GetFileName(uploadFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/NinFileUploads", fileName);

                postNin.NinFile = fileName;
                if (ModelState.IsValid)
                {

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadFile.CopyToAsync(fileStream);
                    }


                    _context.Add(postNin);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(postNin);
        }

        // GET: PostNins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postNin = await _context.PostNins.FindAsync(id);
            if (postNin == null)
            {
                return NotFound();
            }
            return View(postNin);
        }

        // POST: PostNins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NinId,NinZero,NinDigit,NinFile,NinCapt,NinNote")] PostNin postNin)
        {
            if (id != postNin.NinId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postNin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostNinExists(postNin.NinId))
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
            return View(postNin);
        }

        // GET: PostNins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postNin = await _context.PostNins
                .FirstOrDefaultAsync(m => m.NinId == id);
            if (postNin == null)
            {
                return NotFound();
            }

            return View(postNin);
        }

        // POST: PostNins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postNin = await _context.PostNins.FindAsync(id);
            _context.PostNins.Remove(postNin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostNinExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }
    }
}
